using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCMARKMANAGE.Models
{
    public class Password
    {

        private string rn, op, np,fid;
        public string RollNo
        {
            get
            {
                return rn;
            }
            set
            {
                rn = value;
            }
        }
        public string Facultyid
        {
            get
            {
                return fid;
            }
            set
            {
                fid = value;
            }
        }
        public string OldPassword
        {
            get
            {
                return op;
            }
            set
            {
                op = value;
            }
        }
        public string NewPassword
        {
            get
            {
                return np;
            }
            set
            {
                np = value;
            }
        }

    }
}