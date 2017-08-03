using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;
using System;
using System.Data;

namespace BusinessLayer
{
    public class BClient
    {

        public static string Insert(string name, string surname, string gender, DateTime dob,
             string document_type, string document_number, string address, string phone, string email)
        {
            DClient Obj = new DClient();
            Obj.Name = name;
            Obj.Surname = surname;
            Obj.Gender = gender;
            Obj.Dob = dob;
            Obj.TypeDocument = document_type;
            Obj.DocumentNumber = document_number;
            Obj.Address = address;
            Obj.Phone = phone;
            Obj.Email = email;
            

            return Obj.Insert(Obj);
        }

        public static string Edit(int id, string name, string surname, string gender, DateTime dob,
            string document_type, string document_number, string address, string phone, string email)
        {
            DClient Obj = new DClient();
            Obj.Id = id;
            Obj.Name = name;
            Obj.Surname = surname;
            Obj.Gender = gender;
            Obj.Dob = dob;
            Obj.TypeDocument = document_type;
            Obj.DocumentNumber = document_number;
            Obj.Address = address;
            Obj.Phone = phone;
            
            Obj.Email = email;
            


            return Obj.Edit(Obj);
        }

        public static string Delete(int id)
        {
            DClient Obj = new DClient();
            Obj.Id = id;


            return Obj.Delete(Obj);
        }


        public static DataTable SearchName(string searchtext)
        {
            DClient Obj = new DClient();
            Obj.SearchText = searchtext;

            return Obj.SearchName(Obj);
        }

        public static DataTable SearchDocument(string searchtext)
        {
            DClient Obj = new DClient();
            Obj.SearchText = searchtext;

            return Obj.SearchDocument(Obj);
        }


        

        public static DataTable ShowValues()
        {
            return new DClient().ShowValues();
        }
    }
}
