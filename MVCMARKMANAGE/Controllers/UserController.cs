using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCMARKMANAGE.Models;
using System.Data;
using MySql.Data.MySqlClient;

namespace MVCMARKMANAGE.Controllers
{
    public class UserController : Controller
    {
        
        private MySqlConnection sqlcon = new MySqlConnection("server=localhost;database=student_information;userid=root;pwd=Faheem123");
        private List<addclass> emplist = new List<addclass>();
        private List<studentregister> stdlist = new List<studentregister>();
        private List<facultyregister> faclist = new List<facultyregister>();
        private List<cfmap> fclist = new List<cfmap>();
        private List<Mark> slist = new List<Mark>();
        public ActionResult showallfacultyview()
        {
            MySqlDataAdapter getfacData = new MySqlDataAdapter("Select * from FacultyInfo", sqlcon);
            DataSet dsfac = new DataSet();
            getfacData.Fill(dsfac, "fac");

            facultyregister fac = null;

            foreach (DataRow dr in dsfac.Tables["fac"].Rows)
            {
                fac  = new facultyregister();
                fac.Fid = dr["Fid"].ToString();
                fac.FactName = dr["FactName"].ToString();
                fac.Gender = dr["Gender"].ToString();
                fac.Address = dr["Address"].ToString();
                fac.Location = dr["Location"].ToString();
                fac.Phone = dr["Phone"].ToString();
                fac.Email = dr["Email"].ToString();
                fac.Password = dr["pwd"].ToString();
               
                faclist.Add(fac);
            }

            return View(faclist);
        }
        public ActionResult showallclassciew()
        {
            MySqlDataAdapter getEmpData = new MySqlDataAdapter("Select * from ClassInfo", sqlcon);
            DataSet dsEmp = new DataSet();
            getEmpData.Fill(dsEmp, "emp");

            addclass emp = null;

            foreach (DataRow dr in dsEmp.Tables["emp"].Rows)
            {
                emp = new addclass();
                emp.ClassID = int.Parse(dr["ClassID"].ToString());
                
                emp.ClassName = dr["ClassName"].ToString();
               
                emplist.Add(emp);
            }

            return View(emplist);
        }

        public ActionResult studentview()
        {
            MySqlDataAdapter getstdData = new MySqlDataAdapter("Select  rollno,stdname,gender,address,location,phone,email,pwd,classname  from Studentreg,classinfo where studentreg.classid=classinfo.classid order by rollno", sqlcon);
            //MySqlDataAdapter getstdData = new MySqlDataAdapter("Select * from StudentReg", sqlcon);
            DataSet dsstd = new DataSet();
            getstdData.Fill(dsstd, "std");

            studentregister std = null;

            foreach (DataRow dr in dsstd.Tables["std"].Rows)
            {
                std = new studentregister();
                std.RollNumber =dr["Rollno"].ToString();
                std.StdName = dr["StdName"].ToString();
                std.Gender = dr["Gender"].ToString();
                std.Address = dr["Address"].ToString();
                std.Location = dr["Location"].ToString();
                std.Phone = dr["Phone"].ToString();
                std.Email = dr["Email"].ToString();
                std.Password = dr["pwd"].ToString();
                std.ClassName =dr["Classname"].ToString();
                stdlist.Add(std);
            }

            return View(stdlist);
        }


        // GET: User
        public ActionResult Homeview()
        {
            return View();
        }
        public ActionResult classesview()
        {
            return View();
        }

        [HttpPost]
        public ActionResult classesview(addclass e)
        {
                bool chk = false;
                MySqlDataAdapter Checking = new MySqlDataAdapter("Select * from classinfo", sqlcon);
                DataSet dsCk = new DataSet();
                Checking.Fill(dsCk, "check");

                    foreach (DataRow dr in dsCk.Tables["check"].Rows)
                    {
                        if (dr["classname"].ToString().Equals(e.ClassName) == true)
                        {
                            chk = true;
                            break;
                        }
                    }
                if(!chk)
            {
                try
                {


                    sqlcon.Open();
                    MySqlCommand cmdAddEmp = new MySqlCommand("Insert into ClassInfo(ClassName) values(@ClassName)", sqlcon);
                    cmdAddEmp.Parameters.AddWithValue("@ClassName", e.ClassName);


                    cmdAddEmp.ExecuteNonQuery();
                    ViewBag.update = "Added Succesfully";


                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    finally
                    {
                        sqlcon.Close();


                    }
                }
            else
            {
                ViewBag.update = "Class Already Exists";
            }



            return View();
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult addstdview()
        {
            MySqlDataAdapter _da = new MySqlDataAdapter("Select * From classinfo", sqlcon);
            DataTable _dt = new DataTable();
            _da.Fill(_dt);
            ViewBag.CityList = ToSelectList(_dt, "classId", "classname");
            return View();
        }

        [HttpPost]
        public ActionResult addstdview(studentregister c)
        {
            try
            {


                sqlcon.Open();
                MySqlCommand cmdAddEmp = new MySqlCommand("Insert into StudentReg(Rollno,StdName,Gender,Address,Location,Phone,Email,Pwd,ClassID) values(@Rollno,@StdName,@Gender,@Address,@Location,@Phone,@Email,@Password,@ClassID)", sqlcon);
                cmdAddEmp.Parameters.AddWithValue("@Rollno", c.RollNumber);
                cmdAddEmp.Parameters.AddWithValue("@StdName", c.StdName);
                cmdAddEmp.Parameters.AddWithValue("@Gender", c.Gender);
                cmdAddEmp.Parameters.AddWithValue("@Address", c.Address);
                cmdAddEmp.Parameters.AddWithValue("@Location", c.Location);
                cmdAddEmp.Parameters.AddWithValue("@Phone", c.Phone);
                cmdAddEmp.Parameters.AddWithValue("@Email", c.Email);
                cmdAddEmp.Parameters.AddWithValue("@Password", c.Password);
                cmdAddEmp.Parameters.AddWithValue("@ClassID", c.ClassId);
                cmdAddEmp.ExecuteNonQuery();
                ViewBag.update = "Added Succesfully";


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                sqlcon.Close();


            }
            return RedirectToAction("studentview", "User");
        }

        public ActionResult addfacview()
        {
            return View();
        }

        [HttpPost]
        public ActionResult addfacview(facultyregister c)
        {
            try
            {


                sqlcon.Open();
                MySqlCommand cmdAddEmp = new MySqlCommand("Insert into FacultyInfo(Fid,FactName,Gender,Address,Location,Phone,Email,Pwd) values(@Fid,@FactName,@Gender,@Address,@Location,@Phone,@Email,@Password)", sqlcon);
                cmdAddEmp.Parameters.AddWithValue("@Fid", c.Fid);
                cmdAddEmp.Parameters.AddWithValue("@FactName", c.FactName);
                cmdAddEmp.Parameters.AddWithValue("@Gender", c.Gender);
                cmdAddEmp.Parameters.AddWithValue("@Address", c.Address);
                cmdAddEmp.Parameters.AddWithValue("@Location", c.Location);
                cmdAddEmp.Parameters.AddWithValue("@Phone", c.Phone);
                cmdAddEmp.Parameters.AddWithValue("@Email", c.Email);
                cmdAddEmp.Parameters.AddWithValue("@Password", c.Password);
               
                cmdAddEmp.ExecuteNonQuery();
                ViewBag.update = "Added Succesfully";


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                sqlcon.Close();


            }
            return RedirectToAction("showallfacultyview", "User");
        }



        public ActionResult cfmapview()
        {
            return View();
        }

        [HttpPost]
      


            public DataTable GetData()
        {
            MySqlDataAdapter data = new MySqlDataAdapter("Select * from facultyinfo", sqlcon);
            DataTable dt = new DataTable();
            data.Fill(dt);
            return dt;
        }
        public DataTable GetData1()
        {
            MySqlDataAdapter data = new MySqlDataAdapter("Select * from classinfo", sqlcon);
            DataTable d = new DataTable();
            data.Fill(d);
            return d;
        }
        public void AddFacultyClass(cfmap s)
        {
            try
            {
                sqlcon.Open();
                MySqlCommand cmdAddCls = new MySqlCommand("Insert into classfaculty(classid,fid) values(@classid,@fid)", sqlcon);
                cmdAddCls.Parameters.AddWithValue("@classid", s.ClassId);
                cmdAddCls.Parameters.AddWithValue("@fid", s.Fid);
                cmdAddCls.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                sqlcon.Close();
            }
        }
        public bool Check(string classid, string facultyid)
        {
            bool chk = true;
            try
            {
                MySqlDataAdapter Check = new MySqlDataAdapter("Select * from classfaculty", sqlcon);
                DataSet dsCk = new DataSet();
                Check.Fill(dsCk, "check");

                foreach (DataRow dr in dsCk.Tables["check"].Rows)
                {
                    if (dr["classid"].ToString().Equals(classid) == true || dr["fid"].ToString().Equals(facultyid) == true)
                    {
                        chk = false;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return chk;
        }
        public DataSet GetData3()
        {
            MySqlDataAdapter data = new MySqlDataAdapter("Select classname,factname from classinfo,facultyinfo,classfaculty where classinfo.classid=classfaculty.classid and facultyinfo.fid=classfaculty.fid", sqlcon);
            DataSet dsFact = new DataSet();
            data.Fill(dsFact, "fact");
            return dsFact;



        }
        






public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Users log)
        {
            try
            {
                string check = (log.Ctypes).ToString();
                if(check=="Admin")
                {
                    if (log.Username.Equals("smmadmin") && log.Password.Equals("smmadmin"))
                    {
                        return RedirectToAction("Homeview", "User");
                    }
                    else
                    {
                        ViewBag.info = "Please check username/password";
                    }
                }
                else if(check=="Faculty")
                {
                    bool che = true;
                        MySqlDataAdapter Check = new MySqlDataAdapter("Select email from facultyinfo,classfaculty where facultyinfo.fid=classfaculty.fid", sqlcon);
                        DataSet dsCk = new DataSet();
                        Check.Fill(dsCk, "chek");

                        foreach (DataRow drp in dsCk.Tables["chek"].Rows)
                        {
                            if (drp["email"].ToString().Equals(log.Username) == true)
                            {
                                che = false;
                                break;
                            }
                        }
                    if (!che)
                    {
                        DataSet dsLog = new DataSet();
                        MySqlDataAdapter LgCheck = new MySqlDataAdapter("Select * from facultyinfo where email='" + log.Username + "' and pwd='" + log.Password + "'", sqlcon);
                        LgCheck.Fill(dsLog, "log");
                        if (dsLog.Tables["log"].Rows.Count == 1)
                        {
                            Session["fid"] = dsLog.Tables["log"].Rows[0]["fid"].ToString();
                            Session["factname"] = dsLog.Tables["log"].Rows[0]["factname"].ToString();
                            Session["location"] = dsLog.Tables["log"].Rows[0]["location"].ToString();
                            Session["phone"] = dsLog.Tables["log"].Rows[0]["phone"].ToString();
                            return RedirectToAction("FactHome1", "User");
                        }
                        else
                        {
                            ViewBag.info = "Please check username/password";
                        }
                    }
                    else
                    {
                        ViewBag.info = "Please check username/password";
                    }
                    
                }
                else if(check=="Student")
                {
                    DataSet dsLog = new DataSet();
                    MySqlDataAdapter LgCheck = new MySqlDataAdapter("Select * from studentreg where email='" + log.Username + "' and pwd='" + log.Password + "'", sqlcon);
                    LgCheck.Fill(dsLog, "log");
                    if (dsLog.Tables["log"].Rows.Count == 1)
                    {
                        Session["rollno"] = dsLog.Tables["log"].Rows[0]["rollno"].ToString();
                        Session["stdname"] = dsLog.Tables["log"].Rows[0]["stdname"].ToString();
                        Session["location"] = dsLog.Tables["log"].Rows[0]["location"].ToString();
                        Session["phone"] = dsLog.Tables["log"].Rows[0]["phone"].ToString();
                        return RedirectToAction("StdHome1", "User");
                    }
                    else
                    {
                        ViewBag.info = "Please check username/password";
                    }

                }
                else
                {
                    ViewBag.info = "Please check username/password";
                }
            }
            catch (Exception ex)
            {
                ViewBag.info = ex.Message;
            }
            finally
            {
                sqlcon.Close();
            }
            return View();
        }

        [NonAction]
        public SelectList ToSelectList(DataTable table, string valueField, string textField)
        {
            List<SelectListItem> list = new List<SelectListItem>();

            foreach (DataRow row in table.Rows)
            {
                list.Add(new SelectListItem()
                {
                    Text = row[textField].ToString(),
                    Value = row[valueField].ToString()
                });
            }

            return new SelectList(list, "Value", "Text");
        }
        public ActionResult hi()
        {
            return View();
        }
        public ActionResult facmap()
        {

            MySqlDataAdapter _da = new MySqlDataAdapter("Select * From classinfo", sqlcon);
            DataTable _dt = new DataTable();
            _da.Fill(_dt);
            ViewBag.CityList = ToSelectList(_dt, "classId", "classname");
            MySqlDataAdapter _de = new MySqlDataAdapter("select * from facultyinfo", sqlcon);
            DataTable _du = new DataTable();
            _de.Fill(_du);
            ViewBag.CityList1 = ToSelectList(_du, "fId", "factname");
            return View();
        }
        [HttpPost]
        public ActionResult facmap(cfmap c)
        {
            bool chk = true;
                MySqlDataAdapter Check = new MySqlDataAdapter("Select * from classfaculty", sqlcon);
                DataSet dsCk = new DataSet();
                Check.Fill(dsCk, "check");

                foreach (DataRow dr in dsCk.Tables["check"].Rows)
                {
                    if (dr["classid"].ToString().Equals(c.ClassId) == true || dr["fid"].ToString().Equals(c.Fid) == true)
                    {
                        chk = false;
                        break;
                    }
                }
                if(chk)
            {
                try
                {
                    sqlcon.Open();
                    MySqlCommand cmdAddCls = new MySqlCommand("Insert into classfaculty(classid,fid) values(@cid,@fid)", sqlcon);
                    cmdAddCls.Parameters.AddWithValue("@cid", c.ClassId);
                    cmdAddCls.Parameters.AddWithValue("@fid", c.Fid);
                    cmdAddCls.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    sqlcon.Close();
                }
                return RedirectToAction("facshow", "User");
            }
                else
            {
                MySqlDataAdapter _da = new MySqlDataAdapter("Select * From classinfo", sqlcon);
                DataTable _dt = new DataTable();
                _da.Fill(_dt);
                ViewBag.CityList = ToSelectList(_dt, "classId", "classname");
                MySqlDataAdapter _de = new MySqlDataAdapter("select * from facultyinfo", sqlcon);
                DataTable _du = new DataTable();
                _de.Fill(_du);
                ViewBag.CityList1 = ToSelectList(_du, "fId", "factname");
                ViewBag.info = "Already Mapped";
                return View();
            }


           
        }
        public ActionResult facshow()
        {

            MySqlDataAdapter data = new MySqlDataAdapter("Select classname,factname from classinfo,facultyinfo,classfaculty where classinfo.classid=classfaculty.classid and facultyinfo.fid=classfaculty.fid", sqlcon);
            DataSet dsFact = new DataSet();
            data.Fill(dsFact, "cf");
            cfmap c = null;

            foreach (DataRow dr in dsFact.Tables["cf"].Rows)
            {
                c = new cfmap();
                c.Fname = dr["factname"].ToString();
                c.classname = dr["classname"].ToString();
                fclist.Add(c);
            }

            return View(fclist);
        }
        public ActionResult FactHome()
        {
            return View();
        }
        public ActionResult StdHome()
        {
            return View();
        }
        public ActionResult StdPc()
        {
            return View();
        }
        [HttpPost]
        public ActionResult StdPc(Password p)
        {
            p.RollNo = Session["rollno"].ToString();
            bool che = false;
                MySqlDataAdapter Check = new MySqlDataAdapter("Select pwd,rollno from studentreg", sqlcon);
                DataSet dsCk = new DataSet();
                Check.Fill(dsCk, "chek");

                foreach (DataRow dr in dsCk.Tables["chek"].Rows)
                {
                    if (dr["pwd"].ToString().Equals(p.OldPassword) == true && dr["rollno"].ToString().Equals(p.RollNo) == true)
                    {

                        sqlcon.Open();
                        MySqlCommand cmdAddCls = new MySqlCommand("Update studentreg set pwd=@npwd where rollno=@rno", sqlcon);
                        cmdAddCls.Parameters.AddWithValue("@rno", p.RollNo);
                        cmdAddCls.Parameters.AddWithValue("@npwd", p.NewPassword);
                        cmdAddCls.ExecuteNonQuery();
                        sqlcon.Close();
                        che = true;
                        break;
                    }
                }
            if(che)
            {
                ViewBag.info = "Password Changed";
            }
            else
            {
                ViewBag.info = "Incorrect Password Entered";
            }

            return View();
        }
        public ActionResult FactPc()
        {
            return View();
        }
        [HttpPost]
        public ActionResult FactPc(Password p)
        {
            p.Facultyid = Session["fid"].ToString();
            bool che = false;
            MySqlDataAdapter Check = new MySqlDataAdapter("Select pwd,fid from facultyinfo", sqlcon);
            DataSet dsCk = new DataSet();
            Check.Fill(dsCk, "chek");

            foreach (DataRow dr in dsCk.Tables["chek"].Rows)
            {
                if (dr["pwd"].ToString().Equals(p.OldPassword) == true && dr["fid"].ToString().Equals(p.Facultyid) == true)
                {

                    sqlcon.Open();
                    MySqlCommand cmdAddCls = new MySqlCommand("Update facultyinfo set pwd=@npwd where fid=@rno", sqlcon);
                    cmdAddCls.Parameters.AddWithValue("@rno", p.Facultyid);
                    cmdAddCls.Parameters.AddWithValue("@npwd", p.NewPassword);
                    cmdAddCls.ExecuteNonQuery();
                    sqlcon.Close();
                    che = true;
                    break;
                }
            }
            if (che)
            {
                ViewBag.info = "Password Changed";
            }
            else
            {
                ViewBag.info = "Incorrect Password Entered";
            }

            return View();
        }
        public ActionResult StdMark()
        {
            try
            {
                string RollNo = Session["rollno"].ToString();
                MySqlDataAdapter data = new MySqlDataAdapter("select distinct examtype,science,maths,computers,total,average,grade,classname,factname from marksinfo,studentreg,classinfo,classfaculty,facultyinfo where marksinfo.rollno=studentreg.rollno and marksinfo.classid=classinfo.classid and marksinfo.fid=facultyinfo.fid and studentreg.rollno='" + RollNo + "'order by examtype desc", sqlcon);
                DataSet dsFact = new DataSet();
                data.Fill(dsFact, "std");

                Mark st = null;

                foreach (DataRow dr in dsFact.Tables["std"].Rows)
                {
                    st = new Mark();
                    st.ExamType = dr["examtype"].ToString();
                    st.StdName = dr["factname"].ToString();
                    st.ClassName = dr["classname"].ToString();
                    st.Science = int.Parse(dr["science"].ToString());
                    st.Maths = int.Parse(dr["maths"].ToString());
                    st.Computer = int.Parse(dr["computers"].ToString());
                    st.Average = float.Parse(dr["average"].ToString());
                    st.Total = int.Parse(dr["total"].ToString());
                    st.Grade = char.Parse(dr["grade"].ToString());
                    slist.Add(st);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return View(slist);
              
            }
        public ActionResult Qmark()
        {
            
           
            string fid = Session["fid"].ToString();
            MySqlDataAdapter data = new MySqlDataAdapter("select distinct examtype,science,maths,computers,total,average,grade,stdname,classname,factname from marksinfo,studentreg,classinfo,classfaculty,facultyinfo where marksinfo.rollno=studentreg.rollno and marksinfo.classid=classinfo.classid and marksinfo.fid=facultyinfo.fid and examtype='Qtrly' and facultyinfo.fid='" + fid + "'order by studentreg.stdname", sqlcon);
            DataSet dsFact = new DataSet();
            data.Fill(dsFact, "fact");

            Mark st = null;

            foreach (DataRow dr in dsFact.Tables["fact"].Rows)
            {
                st = new Mark();
                st.ExamType = dr["examtype"].ToString();
                st.ClassName = dr["classname"].ToString();
                st.Science = int.Parse(dr["science"].ToString());
                st.Maths = int.Parse(dr["maths"].ToString());
                st.Computer = int.Parse(dr["computers"].ToString());
                st.Average = float.Parse(dr["average"].ToString());
                st.Total = int.Parse(dr["total"].ToString());
                st.Grade = char.Parse(dr["grade"].ToString());
                st.StdName = dr["stdname"].ToString();
                slist.Add(st);
            }
            return View(slist);
        }
        public ActionResult Hmark()
        {


            string fid = Session["fid"].ToString();
            MySqlDataAdapter data = new MySqlDataAdapter("select distinct examtype,science,maths,computers,total,average,grade,stdname,classname,factname from marksinfo,studentreg,classinfo,classfaculty,facultyinfo where marksinfo.rollno=studentreg.rollno and marksinfo.classid=classinfo.classid and marksinfo.fid=facultyinfo.fid and examtype='Halfrly' and facultyinfo.fid='" + fid + "'order by studentreg.stdname", sqlcon);
            DataSet dsFact = new DataSet();
            data.Fill(dsFact, "fact");

            Mark st = null;

            foreach (DataRow dr in dsFact.Tables["fact"].Rows)
            {
                st = new Mark();
                st.ExamType = dr["examtype"].ToString();
                st.ClassName = dr["classname"].ToString();
                st.Science = int.Parse(dr["science"].ToString());
                st.Maths = int.Parse(dr["maths"].ToString());
                st.Computer = int.Parse(dr["computers"].ToString());
                st.Average = float.Parse(dr["average"].ToString());
                st.Total = int.Parse(dr["total"].ToString());
                st.Grade = char.Parse(dr["grade"].ToString());
                st.StdName = dr["stdname"].ToString();
                slist.Add(st);
            }
            return View(slist);
        }
        public ActionResult Fmark()
        {


            string fid = Session["fid"].ToString();
            MySqlDataAdapter data = new MySqlDataAdapter("select distinct examtype,science,maths,computers,total,average,grade,stdname,classname,factname from marksinfo,studentreg,classinfo,classfaculty,facultyinfo where marksinfo.rollno=studentreg.rollno and marksinfo.classid=classinfo.classid and marksinfo.fid=facultyinfo.fid and examtype='Final' and facultyinfo.fid='" + fid + "'order by studentreg.stdname", sqlcon);
            DataSet dsFact = new DataSet();
            data.Fill(dsFact, "fact");

            Mark st = null;

            foreach (DataRow dr in dsFact.Tables["fact"].Rows)
            {
                st = new Mark();
                st.ExamType = dr["examtype"].ToString();
                st.ClassName = dr["classname"].ToString();
                st.Science = int.Parse(dr["science"].ToString());
                st.Maths = int.Parse(dr["maths"].ToString());
                st.Computer = int.Parse(dr["computers"].ToString());
                st.Average = float.Parse(dr["average"].ToString());
                st.Total = int.Parse(dr["total"].ToString());
                st.Grade = char.Parse(dr["grade"].ToString());
                st.StdName = dr["stdname"].ToString();
                slist.Add(st);
            }
            return View(slist);
        }
        public ActionResult Sinfo()
        {
            string fid = Session["fid"].ToString();
            MySqlDataAdapter data = new MySqlDataAdapter("select  rollno,stdname,studentreg.gender,studentreg.address,studentreg.location,studentreg.phone,studentreg.email,studentreg.pwd,classinfo.classname from classinfo,studentreg,classfaculty,facultyinfo  where facultyinfo.fid=classfaculty.fid and studentreg.classid=classfaculty.classid and studentreg.classid=classinfo.classid and facultyinfo.fid='" + fid + "'", sqlcon);
            DataSet dsstd = new DataSet();
            data.Fill(dsstd, "std");

            studentregister std = null;

            foreach (DataRow dr in dsstd.Tables["std"].Rows)
            {
                std = new studentregister();
                std.RollNumber = dr["Rollno"].ToString();
                std.StdName = dr["StdName"].ToString();
                std.Gender = dr["Gender"].ToString();
                std.Address = dr["Address"].ToString();
                std.Location = dr["Location"].ToString();
                std.Phone = dr["Phone"].ToString();
                std.Email = dr["Email"].ToString();
                std.Password = dr["pwd"].ToString();
                std.ClassName = dr["Classname"].ToString();
                stdlist.Add(std);
            }

            return View(stdlist);
        }
        public ActionResult AddMarks()
        {
            string fid = Session["fid"].ToString();
                //MySqlDataAdapter data = new MySqlDataAdapter("select  rollno, stdname from classinfo, studentreg, classfaculty, facultyinfo  where facultyinfo.fid = classfaculty.fid and studentreg.classid = classfaculty.classid and studentreg.classid = classinfo.classid and facultyinfo.fid = '" + fid + "'", sqlcon);
                //DataTable dt = new DataTable();
                //data.Fill(dt);
                //return dt;

                //MySqlDataAdapter data = new MySqlDataAdapter("select distinct classinfo.classid,classinfo.classname from classinfo, studentreg, classfaculty, facultyinfo  where facultyinfo.fid = classfaculty.fid and studentreg.classid = classfaculty.classid and studentreg.classid = classinfo.classid and facultyinfo.fid = '" + fid + "'", sqlcon);
                //DataTable dt = new DataTable();
                //data.Fill(dt);
                //return dt;
            MySqlDataAdapter _da = new MySqlDataAdapter("select distinct classinfo.classid,classinfo.classname from classinfo, studentreg, classfaculty, facultyinfo  where facultyinfo.fid = classfaculty.fid and studentreg.classid = classfaculty.classid and studentreg.classid = classinfo.classid and facultyinfo.fid = '" + fid + "'", sqlcon);
            DataTable _dt = new DataTable();
            _da.Fill(_dt);
            ViewBag.CityList = ToSelectList(_dt, "classId", "classname");
            MySqlDataAdapter _de = new MySqlDataAdapter("select  rollno, stdname from classinfo, studentreg, classfaculty, facultyinfo  where facultyinfo.fid = classfaculty.fid and studentreg.classid = classfaculty.classid and studentreg.classid = classinfo.classid and facultyinfo.fid = '" + fid + "'", sqlcon);
            DataTable _du = new DataTable();
            _de.Fill(_du);
            ViewBag.CityList1 = ToSelectList(_du, "rollno", "stdname");
            return View();
        }
        [HttpPost]
        public ActionResult AddMarks(Mark s)
        {
            s.ExamType = (s.Ctypes).ToString();
            bool chk = false;
                MySqlDataAdapter Check = new MySqlDataAdapter("Select * from marksinfo", sqlcon);
                DataSet dsCk = new DataSet();
                Check.Fill(dsCk, "check");

                foreach (DataRow dr in dsCk.Tables["check"].Rows)
                {
                    if (dr["rollno"].ToString().Equals(s.RollNo) == true && dr["examtype"].ToString().Equals(s.ExamType) == true)
                    {
                        chk = true;
                        break;
                    }
                }
            if (!chk)
            {
                float y = (float.Parse((s.Computer).ToString()) + float.Parse((s.Science).ToString()) + float.Parse((s.Maths).ToString())) / 3;
                s.Total = s.Computer + s.Maths + s.Science;
                s.Average = y;
                if ((s.Total / 3) >= 90)
                {
                    s.Grade = 'A';
                }
                else if ((s.Total / 3) >= 80)
                {
                    s.Grade = 'B';
                }
                else if ((s.Total / 3) >= 70)
                {
                    s.Grade = 'C';
                }
                else if ((s.Total / 3) >= 60)
                {
                    s.Grade = 'D';
                }
                else
                {
                    s.Grade = 'F';
                }

                try
                {
                    sqlcon.Open();
                    MySqlCommand cmdAddStd = new MySqlCommand("Insert into marksinfo(examtype,science,maths,computers,total,average,grade,rollno,classid,fid) values(@et,@sc,@ma,@co,@tot,@avg,@gr,@rno,@cid,@fid)", sqlcon);
                    cmdAddStd.Parameters.AddWithValue("@et", s.ExamType);
                    cmdAddStd.Parameters.AddWithValue("@sc", s.Science);
                    cmdAddStd.Parameters.AddWithValue("@ma", s.Maths);
                    cmdAddStd.Parameters.AddWithValue("@co", s.Computer);
                    cmdAddStd.Parameters.AddWithValue("@tot", s.Total);
                    cmdAddStd.Parameters.AddWithValue("@avg", s.Average);
                    cmdAddStd.Parameters.AddWithValue("@gr", s.Grade);
                    cmdAddStd.Parameters.AddWithValue("@rno", s.RollNo);
                    cmdAddStd.Parameters.AddWithValue("@cid", s.ClassId);
                    cmdAddStd.Parameters.AddWithValue("@fid", Session["fid"].ToString());
                    cmdAddStd.ExecuteNonQuery();
                    ViewBag.info = "Mark Updated";
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    sqlcon.Close();
                }
            }
            else
            {
                ViewBag.info = "Mark Already Exist";
            }
            string fid = Session["fid"].ToString();
            //MySqlDataAdapter data = new MySqlDataAdapter("select  rollno, stdname from classinfo, studentreg, classfaculty, facultyinfo  where facultyinfo.fid = classfaculty.fid and studentreg.classid = classfaculty.classid and studentreg.classid = classinfo.classid and facultyinfo.fid = '" + fid + "'", sqlcon);
            //DataTable dt = new DataTable();
            //data.Fill(dt);
            //return dt;

            //MySqlDataAdapter data = new MySqlDataAdapter("select distinct classinfo.classid,classinfo.classname from classinfo, studentreg, classfaculty, facultyinfo  where facultyinfo.fid = classfaculty.fid and studentreg.classid = classfaculty.classid and studentreg.classid = classinfo.classid and facultyinfo.fid = '" + fid + "'", sqlcon);
            //DataTable dt = new DataTable();
            //data.Fill(dt);
            //return dt;
            MySqlDataAdapter _da = new MySqlDataAdapter("select distinct classinfo.classid,classinfo.classname from classinfo, studentreg, classfaculty, facultyinfo  where facultyinfo.fid = classfaculty.fid and studentreg.classid = classfaculty.classid and studentreg.classid = classinfo.classid and facultyinfo.fid = '" + fid + "'", sqlcon);
            DataTable _dt = new DataTable();
            _da.Fill(_dt);
            ViewBag.CityList = ToSelectList(_dt, "classId", "classname");
            MySqlDataAdapter _de = new MySqlDataAdapter("select  rollno, stdname from classinfo, studentreg, classfaculty, facultyinfo  where facultyinfo.fid = classfaculty.fid and studentreg.classid = classfaculty.classid and studentreg.classid = classinfo.classid and facultyinfo.fid = '" + fid + "'", sqlcon);
            DataTable _du = new DataTable();
            _de.Fill(_du);
            ViewBag.CityList1 = ToSelectList(_du, "rollno", "stdname");
            return View();
        }
        public ActionResult StdHome1()
        {
            return View();
        }
        public ActionResult FactHome1()
        {
            return View();
        }
    }

    
    
}