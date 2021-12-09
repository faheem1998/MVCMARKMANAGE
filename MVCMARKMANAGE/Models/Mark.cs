using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCMARKMANAGE.Models
{
    public class Mark
    {
        private int sc, ma, co, tot, cid;
        private string fid, rollno, Et,name,cname;
        private char gr;
        private float avg;
        public string ExamType
        {
            get
            {
                return Et;
            }
            set
            {
                Et = value;
            }
        }
        public string StdName
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }
        public string ClassName
        {
            get
            {
                return cname;
            }
            set
            {
                cname = value;
            }
        }
        public int ClassId
        {
            get
            {
                return cid;
            }
            set
            {
                cid = value;
            }
        }

        public string RollNo
        {
            get
            {
                return rollno;
            }
            set
            {
                rollno = value;
            }
        }
        public int Science
        {
            get
            {
                return sc;
            }
            set
            {
                sc = value;
            }
        }
        public int Maths
        {
            get
            {
                return ma;
            }
            set
            {
                ma = value;
            }
        }
        public int Computer
        {
            get
            {
                return co;
            }
            set
            {
                co = value;
            }
        }
        public string FacultyId
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
        public int Total
        {
            get
            {
                return tot;
            }
            set
            {
                tot = value;
            }
        }
        public float Average
        {
            get
            {
                return avg;
            }
            set
            {
                avg = value;
            }
        }
        public char Grade
        {
            get
            {
                return gr;
            }
            set
            {
                gr = value;
            }
        }
        public Ty Ctypes { get; set; }

    }
    public enum Ty
    {
        Qtrly,
        Halfrly,
        Final,

    }
}