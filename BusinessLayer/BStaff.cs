using DataLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class BStaff
    {

        public static string Insert(string name, string surname, string gender, DateTime dob,
             string document_number, string address, string phone, string email, string access,
             string username, string password)
        {
            DStaff Obj = new DStaff();
            Obj.Name = name;
            Obj.Surname = surname;
            Obj.Gender = gender;
            Obj.Dob = dob;
            Obj.Document_Number = document_number;
            Obj.Address = address;
            Obj.Phone = phone;
            Obj.Access = access;
            Obj.Email = email;
            Obj.Username = username;
            Obj.Password = password;
           

            return Obj.Insert(Obj);
        }

        public static string Edit(int id, string name, string surname, string gender, DateTime dob,
            string document_number, string address, string phone, string email, string access,
            string username, string password)
        {
            DStaff Obj = new DStaff();
            Obj.Id = id;
            Obj.Name = name;
            Obj.Surname = surname;
            Obj.Gender = gender;
            Obj.Dob = dob;
            Obj.Document_Number = document_number;
            Obj.Address = address;
            Obj.Phone = phone;
            Obj.Access = access;
            Obj.Email = email;
            Obj.Username = username;
            Obj.Password = password;


            return Obj.Edit(Obj);
        }

        public static string Delete(int id)
        {
            DStaff Obj = new DStaff();
            Obj.Id = id;


            return Obj.Delete(Obj);
        }


        public static DataTable SearchName(string searchtext)
        {
            DStaff Obj = new DStaff();
            Obj.SearchText = searchtext;

            return Obj.ShowName(Obj);
        }

        public static DataTable SearchDocument(string searchtext)
        {
            DStaff Obj = new DStaff();
            Obj.SearchText = searchtext;

            return Obj.SearchDocument(Obj);
        }


        public static DataTable Login(string username, string password)
        {
            DStaff Obj = new DStaff();
            Obj.Username = username;
            Obj.Password = password;

            return Obj.Login(Obj);
        }

        public static DataTable ShowValues()
        {
            return new DStaff().ShowValues();
        }
    }
}
