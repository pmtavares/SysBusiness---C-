using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace DataLayer
{
    public class DDetail_Sale
    {
        private int _IDDetail_Sale;
        private int _IDSale;
        private int _IDDetail_Input;
        private int _Quantity;
        private decimal _Value;
        private decimal _Discount;

        public int IDDetail_Sale
        {
            get { return _IDDetail_Sale; }
            set { _IDDetail_Sale = value; }
        }

      
        

        public int IDSale
        {
            get { return _IDSale; }
            set { _IDSale = value; }
        }
        

        public int IDDetail_Input
        {
            get { return _IDDetail_Input; }
            set { _IDDetail_Input = value; }
        }
      

        public int Quantity
        {
            get { return _Quantity; }
            set { _Quantity = value; }
        }
        

        public decimal Value
        {
            get { return _Value; }
            set { _Value = value; }
        }
        

        public decimal Discount
        {
            get { return _Discount; }
            set { _Discount = value; }
        }



        public  DDetail_Sale()
        {

        }


        public DDetail_Sale(int iddetail_sale, int idsale, int iddetail_input,
            int quantity, decimal value, decimal discount )
        {
            this.IDDetail_Input  = iddetail_input;
            this.IDDetail_Sale = iddetail_sale;
            this.IDSale = idsale;
            this.Quantity = quantity;
            this.Value = value;
            this.Discount = discount;
        
        }



        public string Insert(DDetail_Sale detailsale, ref SqlConnection sqlCon, ref SqlTransaction sqlTra)
        {
            string resp = "";
            //SqlConnection SqlCon = new SqlConnection();
            try
            {
                //SqlCon.ConnectionString = Conection.cn;
                //SqlCon.Open();

                SqlCommand SqlCmd = new SqlCommand();
                SqlCmd.Connection = sqlCon;
                SqlCmd.Transaction = sqlTra;
                SqlCmd.CommandText = "spinsert_detail_sale";
                SqlCmd.CommandType = CommandType.StoredProcedure;

                //Initialize variables
                SqlParameter ParIdDetailSale = new SqlParameter();
                ParIdDetailSale.ParameterName = "@iddetalhe_sale";
                ParIdDetailSale.SqlDbType = SqlDbType.Int;
                ParIdDetailSale.Direction = ParameterDirection.Output;
                SqlCmd.Parameters.Add(ParIdDetailSale);

                SqlParameter ParIdSale = new SqlParameter();
                ParIdSale.ParameterName = "@idsale";
                ParIdSale.SqlDbType = SqlDbType.Int;
                ParIdSale.Value = detailsale.IDSale;
                SqlCmd.Parameters.Add(ParIdSale);

                SqlParameter ParIddetailInput = new SqlParameter();
                ParIddetailInput.ParameterName = "@idinput_detail";
                ParIddetailInput.SqlDbType = SqlDbType.Int;
                ParIddetailInput.Value = detailsale.IDDetail_Input;
                SqlCmd.Parameters.Add(ParIddetailInput);

                SqlParameter ParQuantity = new SqlParameter();
                ParQuantity.ParameterName = "@quantity";
                ParQuantity.SqlDbType = SqlDbType.Int;
                ParQuantity.Value = detailsale.Quantity;
                SqlCmd.Parameters.Add(ParQuantity);


                SqlParameter ParValue = new SqlParameter();
                ParValue.ParameterName = "@value";
                ParValue.SqlDbType = SqlDbType.Money;
                ParValue.Value = detailsale.Value;
                SqlCmd.Parameters.Add(ParValue);

                SqlParameter ParDiscount = new SqlParameter();
                ParDiscount.ParameterName = "@discount";
                ParDiscount.SqlDbType = SqlDbType.Money;
                ParDiscount.Value = detailsale.Discount;
                SqlCmd.Parameters.Add(ParDiscount);




                //Execute the command
                resp = SqlCmd.ExecuteNonQuery() == 1 ? "OK" : "Not Inserted";

            }
            catch (Exception ex)
            {
                resp = "Something went wrong on details sale: " + ex.Message;
            }
            
            
            return resp;
        }
    }
}
