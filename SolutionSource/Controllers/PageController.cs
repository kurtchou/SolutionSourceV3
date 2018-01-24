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
select ID, Title, ParentID, ActiveFlag, 
cast(
	cast(N'' as xml).value('xs:base64Binary(sql:column(""LongDesc""))', 'varbinary(max)') as VARCHAR(MAX) 
) LongDesc, OrderID, LevelID, ShowAssets, ViewType, Created
                    from PPSNav where ActiveFlag = '1' order by CAST(OrderID  AS INT)
            ");

            ViewBag.NavID = id;
            ViewBag.ImgPath = "../../Content/img/PS/TitleIcon_ID" + id + ".png";//main icon

            string _htmlBread = buildBreadCrumb(page, id);
            ViewBag.htmlBread = _htmlBread.Substring(3);//take out first 3 characters

            var _hasChild = page
                   .Where(t => t.ParentID == id.ToString())
                   .ToList();

            ViewBag.showChild = false;
            if (_hasChild.Count > 0) {
                ViewBag.showChild = true;
                ViewBag.childPages = _hasChild;
            }
            

            return View(page);
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