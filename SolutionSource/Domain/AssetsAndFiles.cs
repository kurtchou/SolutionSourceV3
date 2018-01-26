using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SolutionSource.Domain
{
    public class AssetsAndFiles : ActiveRecord
    {
        public override int ID { get; set; }//asset id
        public override DateTime Created { get; set; }

        public int FID { get; set; }//file id
        public string Status { get; set; }
        public string FTitle { get; set; }
        public string ATitle { get; set; }
        public string Link { get; set; }
        public string Region { get; set; }
        public string PublishDate { get; set; }
        public string NavID { get; set; }
        public string Offering { get; set; }
        public string Confidentiality { get; set; }
        public string ContentGroup { get; set; }
        public string ContentType0 { get; set; }
        public string ContentTypeOrderID { get; set; }
        public string OrderID { get; set; }
        public string Mark { get; set; }
        public string ShortDesc { get; set; }
        public string FileOrderID { get; set; }
    }
}