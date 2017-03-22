using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;
using System.Data;

namespace BusinessLayer
{
    //This is the Negocios
    public class BCategory
    {
        public static string Insert(string name, string description)
        {
            DCategory Obj = new DCategory();
            Obj.Name = name;
            Obj.Description = description;

            return Obj.Insert(Obj);
        }

        public static string Edit(int idcategory, string name, string description)
        {
            DCategory Obj = new DCategory();
            Obj.Idcategory = idcategory;
            Obj.Name = name;
            Obj.Description = description;

            return Obj.Edit(Obj);
        }

        public static string Delete(int idcategory)
        {
            DCategory Obj = new DCategory();
            Obj.Idcategory = idcategory;
            

            return Obj.Delete(Obj);
        }

        public static DataTable ShowValues()
        {
            return new DCategory().ShowValues();
        }

        public static DataTable SearchName(string searchtext)
        {
            DCategory Obj = new DCategory();
            Obj.Textsearch = searchtext;
            return Obj.ShowName(Obj);
        }
    }
}
