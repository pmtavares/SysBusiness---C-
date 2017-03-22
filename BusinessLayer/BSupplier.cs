using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;
using System.Data;

namespace BusinessLayer
{
    public class BSupplier
    {
        public static string Insert(string company, string businessDepart,
            string documentType, string document_Number, string address, string phone, string email, string url)
        {
            DSupplier Obj = new DSupplier();
            Obj.Company = company;
            Obj.Business_Department = businessDepart;
            Obj.Document_Type = documentType;
            Obj.Document_Number = document_Number;
            Obj.Address = address;
            Obj.Phone = phone;
            Obj.Email = email;
            Obj.Url = url;

            return Obj.Insert(Obj);
        }

        public static string Edit(int id, string company, string businessDepart,
            string documentType, string document_Number, string address, string phone, string email, string url)
        {
            DSupplier Obj = new DSupplier();
            Obj.Id = id;
            Obj.Company = company;
            Obj.Business_Department = businessDepart;
            Obj.Document_Type = documentType;
            Obj.Document_Number = document_Number;
            Obj.Address = address;
            Obj.Phone = phone;
            Obj.Url = url;
            Obj.Email = email;

            return Obj.Edit(Obj);
        }

        public static string Delete(int id)
        {
            DSupplier Obj = new DSupplier();
            Obj.Id = id;


            return Obj.Delete(Obj);
        }

        public static DataTable ShowValues()
        {
            return new DSupplier().ShowValues();
        }

        public static DataTable SearchName(string searchtext)
        {
            DSupplier Obj = new DSupplier();
            Obj.SearchText = searchtext;

            return Obj.ShowName(Obj);
        }

        public static DataTable SearchDocument(string searchtext)
        {
            DSupplier Obj = new DSupplier();
            Obj.SearchText = searchtext;

            return Obj.SearchDocument(Obj);
        }
    }
}
