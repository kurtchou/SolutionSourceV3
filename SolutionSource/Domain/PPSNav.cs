using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SolutionSource.Domain
{
    public class PPSNav : ActiveRecord
    {
        public override int ID { get; set; }
        public override DateTime Created { get; set; }
        public string Title { get; set; }
        public string ParentID { get; set; }
        public string ActiveFlag { get; set; }
        public string Group { get; set; }
        public string LongDesc { get; set; }
        public string OrderID { get; set; }
        public string LevelID { get; set; }
        public string ShowAssets { get; set; }
        public string ViewType { get; set; }
    }
}