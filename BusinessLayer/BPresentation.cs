using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using DataLayer;

namespace BusinessLayer
{
    public class BPresentation
    {
        public static string Insert(string name, string description)
        {
            DPresentation Obj = new DPresentation();
            Obj.Name = name;
            Obj.Description = description;

            return Obj.Insert(Obj);
        }

        public static string Edit(int idpresentation, string name, string description)
        {
            DPresentation Obj = new DPresentation();
            Obj.Idpresentation = idpresentation;
            Obj.Name = name;
            Obj.Description = description;

            return Obj.Edit(Obj);
        }

        public static string Delete(int idpresentation)
        {
            DPresentation Obj = new DPresentation();
            Obj.Idpresentation = idpresentation;


            return Obj.Delete(Obj);
        }

        public static DataTable ShowValues()
        {
            return new DPresentation().ShowValues();
        }

        public static DataTable SearchName(string searchtext)
        {
            DPresentation Obj = new DPresentation();
            Obj.Textsearch = searchtext;
            return Obj.ShowName(Obj);
        }
    }
}
