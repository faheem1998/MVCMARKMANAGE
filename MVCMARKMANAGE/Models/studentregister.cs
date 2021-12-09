using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace MVCMARKMANAGE.Models
{
    //create table StudentReg(Rollno varchar(5) primary key, StdName varchar(20),Gender varchar(10),Address varchar(100),Location varchar(20),
//Phone char (10),Email varchar(20),Password varchar(20),ClassID int,foreign key(ClassID) references ClassInfo(ClassID));
    public class studentregister
    {
        public string RollNumber
        {
            get;

            set;
            
        }
      

        public string StdName
        {
            get;

            set;
           
        }
        public string Gender
        {
            get;

            set
           ;
        }
        public string Address
        {
            get
           ;
            set
           ;
        }
        public string Location
        {
            get
           ;
            set
           ;
        }
        [Required(ErrorMessage = "Mobile Number is required.")]
        [RegularExpression(@"^([0-9]{10})$", ErrorMessage = "Invalid Mobile Number.")]
        public string Phone
        {
            get
           ;
            set
           ;
        }
        public string Email
        {
            get
            ;
            set
           ;
        }
        public string Password
        {
            get
            ;
            set
           ;
       
        }
        public int ClassId { get; set; }
        public string ClassName
        {
            get
            ;
            set
           ;
        }
    }

}
   