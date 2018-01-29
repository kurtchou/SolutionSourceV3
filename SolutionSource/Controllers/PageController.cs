using Newtonsoft.Json;
using SolutionSource.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace SolutionSource.Controllers
{
    public class PageController : Controller
    {
        public ActionResult ViewID(int id)
        {
            
            var page = ActiveRecord.GetAllWithSQL<PPSNav>(@"
select ID, Title, ParentID, ActiveFlag, LongDesc, OrderID, LevelID, ShowAssets, ViewType, Created
                    from PPSNav where ActiveFlag = '1' and [Group] = 'SS' order by CAST(OrderID  AS INT)
            ");

            var pageById = ActiveRecord.GetAllWithSQL<PPSNav>(@"
select ID, Title, ParentID, ActiveFlag, 
cast(
	cast(N'' as xml).value('xs:base64Binary(sql:column(""LongDesc""))', 'varbinary(max)') as VARCHAR(MAX) 
) LongDesc, OrderID, LevelID, ShowAssets, ViewType, Created
                    from PPSNav where ActiveFlag = '1' and ID =
            " + id);

            ViewBag.NavID = id;
            ViewBag.ImgPath = "../../Content/img/PS/TitleIcon_ID" + id + ".png";//main icon

            //breadcrumb
            string _htmlBread = buildBreadCrumb(page, id);
            ViewBag.htmlBread = _htmlBread.Substring(3);//take out first 3 characters

            //those page with child
            var _hasChild = page
                   .Where(t => t.ParentID == id.ToString())
                   .ToList();

            ViewBag.showChild = false;
            if (_hasChild.Count > 0) {
                ViewBag.showChild = true;
                ViewBag.childPages = _hasChild;
            }

            ViewBag.viewType = pageById.FirstOrDefault().ViewType;

            //find assets and files based on nav id
            var assets = ActiveRecord.GetAllWithSQL<AssetsAndFiles>(@"
select a.id as FID, b.id as ID, b.Status, a.title as FTitle, b.Title as ATitle, b.ShortDesc, a.Link, b.Region, CONVERT(datetime, a.PublishDate) as PublishDate, b.NavID, b.Offering, a.Confidentiality, a.ContentGroup, a.ContentType0, a.Mark, a.ContentTypeOrderID, b.OrderID, a.OrderID as FileOrderID, a.Created
from PPSSSFiles as a inner join PPSSSAssets as b on a.AssetID = b.id
where (b.NavID = '" + id + @"' or b.Offering like '%"  + id + @";#%') and b.Status = 'Active' and a.ContentType0 <> 'Useful Links' 
and a.ContentGroup <> 'Wins'
            ");

            ViewBag.showAsset = false;
            if (assets.Count > 0) {
                ViewBag.showAsset = true;
                ViewBag.contentViewAssetsPortfolio = assets //portoflio
                    .Where(t => t.ContentGroup == "Portfolio")
                    .GroupBy(t => t.Confidentiality.Contains("Internal") ? "Internal Facing" : "Customer Facing")
                    .ToDictionary(k => k.Key, v => v.ToList());

                ViewBag.contentViewAssets = assets //non portoflio
                    .Where(t => t.ContentGroup != "Portfolio")
                    .GroupBy(t => t.ContentGroup)
                    .ToDictionary(k => k.Key, v => v.ToList());

                ViewBag.assetViewAssets = assets
                   .OrderBy(t => Int32.Parse(t.OrderID))//asset order id should already be grouped by navid
                   .ToList(); ;
            }

            //find contacts
            var contacts = ActiveRecord.GetAllWithSQL<Contacts>(@"
select ID, Created, Region, SSServices, Email, Title, FName, LName
from PPSSSContact
where SSServices like '%" + id + @";#%'
Order By Case Region
    When 'Worldwide' Then 1
    When 'AMS' Then 2
    When 'APJ' Then 3
    When 'EMEA' Then 4
    Else 5 End 
            ");
            ViewBag.showContact = false;
            if (contacts.Count > 0) {
                ViewBag.showContact = true;
                ViewBag.contacts = contacts;
            }

            //find useful links
            var usefulLinks = ActiveRecord.GetAllWithSQL<UsefulLinks>(@"
select a.id as FID, b.id as ID, a.title as FTitle, b.Title as ATitle, a.Link, b.NavID, b.Offering, a.Created
from PPSSSFiles as a inner join PPSSSAssets as b on a.AssetID = b.id
where (b.NavID = '" + id + @"' or b.Offering like '%" + id + @";#%') and b.Status = 'Active' and a.ContentType0 = 'Useful Links'
            ");
            ViewBag.showUsefulLinks = false;
            if (usefulLinks.Count > 0)
            {
                ViewBag.showUsefulLinks = true;
                ViewBag.usefulLinks = usefulLinks;
            }

            return View(pageById);
        }

        private string buildBreadCrumb(List<PPSNav> nav, int id) {
            StringBuilder breadHtml = new StringBuilder();
            int iniId = id;
            while (iniId != -1) {
                var _nav = nav
                    .Where(t => t.ID == iniId)
                    .ToList();

                iniId = Int32.Parse(_nav.FirstOrDefault().ParentID);//find parent using parent ID, until it is -1
                if (iniId != -1) {//ignore root, which is support services
                    breadHtml.Insert(0, @" / <a href=""/Page/ViewID/" + _nav.FirstOrDefault().ID + "\">" + _nav.FirstOrDefault().Title + "</a>");//basically prepend
                }
                
            }
            return breadHtml.ToString();
        }

        [HttpPost]
        public ActionResult GetChildNav(int id)
        {
            string sql = @"select ID, Title, ParentID, ActiveFlag, OrderID, LevelID, ShowAssets, ViewType, Created
                    from PPSNav where ParentID = '" + id + "' and ActiveFlag = '1'";

            var page = ActiveRecord.GetAllWithSQL<PPSNav>(sql);
            
            return Json(JsonConvert.SerializeObject(page), JsonRequestBehavior.AllowGet);
        }
    }
}