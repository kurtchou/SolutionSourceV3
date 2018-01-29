using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SolutionSource.Domain
{
    public class UsefulLinks : ActiveRecord
    {
        public override int ID { get; set; }//asset id
        public override DateTime Created { get; set; }

        public string Offering { get; set; }
        public string NavID { get; set; }
        public string Link { get; set; }
        public string Title { get; set; }
        public string ATitle { get; set; }
        public string FTitle { get; set; }
        public int FID { get; set; }//file id
    }
}