using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;
using System.Data;

namespace BusinessLayer
{
    public class BSale
    {

        public static string Insert(int idclient, int staff, DateTime date, 
            string type, string serie, string corelativ, string vat, DataTable dtDetail)
        {
            DSale Obj = new DSale();
            Obj.IDStaff = staff;
            Obj.IDClient = idclient;
            Obj.Date = date;
            Obj.Type_Receipt = type;
            Obj.Serie = serie;
            Obj.Corelation = corelativ;
            Obj.Vat = vat;
            


            //Details of th purchase
            List<DDetail_Sale> details = new List<DDetail_Sale>();
            foreach (DataRow row in dtDetail.Rows)
            {
                DDetail_Sale detail = new DDetail_Sale();


                //detail.IDSale= Convert.ToInt32(row["idsale"].ToString());
                detail.IDDetail_Input = Convert.ToInt32(row["idinput_detail"].ToString());
                detail.Quantity = Convert.ToInt32(row["quantity"].ToString());
                detail.Value = Convert.ToDecimal(row["value"].ToString());
                detail.Discount = Convert.ToDecimal(row["discount"].ToString());
                
                

                details.Add(detail);
            }


            return Obj.Insert(Obj, details);
        }


        public static string Delete(int id)
        {
            DSale Obj = new DSale();
            Obj.IDSale = id;


            return Obj.Delete(Obj);
        }


        public static DataTable SearchNameDate(string searchtext, string searchtext2)
        {
            DSale Obj = new DSale();
            

            return Obj.SearchDate(searchtext, searchtext2);
        }

        public static DataTable Show()
        {
            return new DSale().ShowSale();
        }

        public static DataTable ShowDetailsSale(string textsearch)
        {

            DSale Obj = new DSale();

            return Obj.ShowDetailsSale(textsearch);
        }

        public static DataTable ShowProduct_Sale_Name(string textsearch)
        {

            DSale Obj = new DSale();

            return Obj.ShowProduct_Sale_Name(textsearch);
        }

        public static DataTable ShowProduct_Sale_Code(string textsearch)
        {

            DSale Obj = new DSale();

            return Obj.ShowProduct_Sale_Code(textsearch);
        }
    }
}
