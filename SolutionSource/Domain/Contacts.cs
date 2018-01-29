using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SolutionSource.Domain
{
    public class Contacts : ActiveRecord
    {
        public override int ID { get; set; }//asset id
        public override DateTime Created { get; set; }
        public string Title { get; set; }
        public string Email { get; set; }
        public string Region { get; set; }
        public string SSServices { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
    }
}