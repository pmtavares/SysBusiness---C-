using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DataLayer;

namespace BusinessLayer
{
    public class BInput
    {

        public static string Insert( int idstaff , int supplier, DateTime date, string type,
            string serie, string corelativ, decimal vat, string status, DataTable dtDetail)
        {
            DInput Obj = new DInput();
            Obj.IdStaff = idstaff;
            Obj.IdSupplier = supplier;
            Obj.DateInput = date;
            Obj.TypeDoc = type;
            Obj.SerieDoc = serie;
            Obj.Corelativ = corelativ;
            Obj.Vat = vat;
            Obj.CurrentStatus = status;
           

            //Details of th purchase
            List<DDetail_Input> details = new List<DDetail_Input>();
            foreach(DataRow row in dtDetail.Rows)
            {
                DDetail_Input detail = new DDetail_Input();


                detail.IdProduct = Convert.ToInt32(row["idproduct"].ToString());
                detail.ValuePurchase = Convert.ToDecimal(row["value_purchased"].ToString());
                detail.ValueSold = Convert.ToDecimal(row["value_sold"].ToString());
                detail.InitialStoque = Convert.ToInt32(row["initial_stoque"].ToString());
                detail.CurrentStoque = Convert.ToInt32(row["initial_stoque"].ToString());
                detail.ProductioDate = Convert.ToDateTime(row["produced_date"].ToString());
                detail.ExpiredDate = Convert.ToDateTime(row["expired_date"].ToString());

                details.Add(detail);
            }


            return Obj.Insert(Obj, details);
        }

       
        public static string Cancel(int id)
        {
            DInput Obj = new DInput();
            Obj.IdInput = id;


            return Obj.Cancel(Obj);
        }


        public static DataTable SearchNameDate(string searchtext, string searchtext2)
        {
            DInput Obj = new DInput();
            //Obj.SearchText = searchtext;

            return Obj.SearchNameDate(searchtext, searchtext2);
        }

        public static DataTable Show()
        {
            return new DInput().ShowInput();
        }

        public static DataTable ShowDetails(string textsearch)
        {

            DInput Obj = new DInput();

            return Obj.ShowDetails(textsearch);
        }
    }
}
