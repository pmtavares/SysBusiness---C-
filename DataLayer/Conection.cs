using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    class Conection
    {

        //This is static. Have another connection type. 
        //public static string cn = "Data Source = pedro-PC\\sqlexpress; Initial Catalog = dbBusiness; Integrated Security=true";
        public static string cn = Properties.Settings.Default.dbBusinessConnectionString;
    }
}
