using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCMARKMANAGE.Models
{
    //create table ClassInfo(ClassID int primary key auto_increment, ClassName varchar(10));

    public class addclass
    {
        public int ClassID
        {
            get;set;
        }

        public string ClassName
        {
            get;set;
        }
    }
}