using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using DataLayer;
using System.Data;

namespace BusinessLayer
{
    public class BProduct
    {
        public static string Insert(string code, string name, string description, byte[] image, int idcat, int idpre )
        {
            DProduct Obj = new DProduct();
            Obj.Code = code;
            Obj.Image = image;
            Obj.IdCategory = idcat;
            Obj.IdPresentation = idpre;
            Obj.Name = name;
            Obj.Description = description;

            return Obj.Insert(Obj);
        }

        public static string Edit(int id, string code, string name, string description, byte[] image, int idcat, int idpre)
        {
            DProduct Obj = new DProduct();
            Obj.Id = id;
            Obj.Name = name;
            Obj.Description = description;
            Obj.Code = code;
            Obj.Image = image;
            Obj.IdCategory = idcat;
            Obj.IdPresentation = idpre;
            return Obj.Edit(Obj);
        }

        public static string Delete(int id)
        {
            DProduct Obj = new DProduct();
            Obj.Id = id;


            return Obj.Delete(Obj);
        }

        public static DataTable ShowValues()
        {
            return new DProduct().ShowValues();
        }

        public static DataTable SearchName(string searchtext)
        {
            DProduct Obj = new DProduct();
            Obj.TextSearch1 = searchtext;
            return Obj.ShowName(Obj);
        }

        public static DataTable StockProduct()
        {

            return new DProduct().Stock_Product();
        }


    }
}
