using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace DataLayer
{
    public class DDetail_Input
    {
        private int _IdDetail_Product;
        private int _IdProduct;
        private int _IdInput;
        private decimal _ValuePurchase;
        private decimal _ValueSold;
        private int _InitialStoque;
        private int _CurrentStoque;
        private DateTime _ProductioDate;
        private DateTime _ExpiredDate;
        private string _SearchText;
        private string _SearchText2;

        public int IdDetail_Product
        {
            get { return _IdDetail_Product; }
            set { _IdDetail_Product = value; }
        }
        

        public int IdProduct
        {
            get { return _IdProduct; }
            set { _IdProduct = value; }
        }
        

        public int IdInput
        {
            get { return _IdInput; }
            set { _IdInput = value; }
        }
        

        public decimal ValuePurchase
        {
            get { return _ValuePurchase; }
            set { _ValuePurchase = value; }
        }
        

        public decimal ValueSold
        {
            get { return _ValueSold; }
            set { _ValueSold = value; }
        }
        

        public int InitialStoque
        {
            get { return _InitialStoque; }
            set { _InitialStoque = value; }
        }
        

        public int CurrentStoque
        {
            get { return _CurrentStoque; }
            set { _CurrentStoque = value; }
        }
        

        public DateTime ProductioDate
        {
            get { return _ProductioDate; }
            set { _ProductioDate = value; }
        }
        

        public DateTime ExpiredDate
        {
            get { return _ExpiredDate; }
            set { _ExpiredDate = value; }
        }
        

        public string SearchText
        {
            get { return _SearchText; }
            set { _SearchText = value; }
        }
        

        public string SearchText2
        {
            get { return _SearchText2; }
            set { _SearchText2 = value; }
        }

        public DDetail_Input()
        {

        }
        public DDetail_Input(int iddetail_input, int idinput, int idproduct, decimal value_purchase, decimal value_sold,
            int initial_stoque, int current_stoque, DateTime production_date, DateTime expired_date, string searcText, string searcText2)
        {
            this.IdDetail_Product = iddetail_input;
            this.IdInput = idinput;
            this.IdProduct = idproduct;
            this.ValuePurchase = value_purchase;
            this.ValueSold = value_sold;
            this.InitialStoque = initial_stoque;
            this.CurrentStoque = current_stoque;
            this.ProductioDate = production_date;
            this.ExpiredDate = expired_date;
            this.SearchText = searcText;
            this.SearchText2 = searcText2;
       
        }


        public string Insert(DDetail_Input detail, ref SqlConnection SqlCon, ref SqlTransaction sqlTra)
        {
            string resp = "";
            
            try
            {
                SqlCommand SqlCmd = new SqlCommand();
                SqlCmd.Connection = SqlCon;
                SqlCmd.Transaction = sqlTra;
                SqlCmd.CommandText = "spinsert_input_detail";
                SqlCmd.CommandType = CommandType.StoredProcedure;


                //SqlCon.ConnectionString = Conection.cn;
                //SqlCon.Open();

                            

                //Initialize variables
                SqlParameter ParId = new SqlParameter();
                ParId.ParameterName = "@iddetail_input";
                ParId.SqlDbType = SqlDbType.Int;
                ParId.Direction = ParameterDirection.Output;

                SqlCmd.Parameters.Add(ParId);

                SqlParameter ParIdInput = new SqlParameter();
                ParIdInput.ParameterName = "@idinput";
                ParIdInput.SqlDbType = SqlDbType.Int;
                ParIdInput.Value = detail.IdInput;

                SqlCmd.Parameters.Add(ParIdInput);

                SqlParameter ParIdProduct = new SqlParameter();
                ParIdProduct.ParameterName = "@idproduct";
                ParIdProduct.SqlDbType = SqlDbType.Int;
                ParIdProduct.Value = detail.IdProduct;

                SqlCmd.Parameters.Add(ParIdProduct);

                SqlParameter ParPurchasePrice = new SqlParameter();
                ParPurchasePrice.ParameterName = "@purchase_price";
                ParPurchasePrice.SqlDbType = SqlDbType.Money;
                ParPurchasePrice.Value = detail.ValuePurchase;

                SqlCmd.Parameters.Add(ParPurchasePrice);



                SqlParameter ParPurchaseSold = new SqlParameter();
                ParPurchaseSold.ParameterName = "@purchase_sold";
                ParPurchaseSold.SqlDbType = SqlDbType.Money;
                ParPurchaseSold.Value = detail.ValueSold;

                SqlCmd.Parameters.Add(ParPurchaseSold);

                SqlParameter ParInitialStock = new SqlParameter();
                ParInitialStock.ParameterName = "@initial_stoque";
                ParInitialStock.SqlDbType = SqlDbType.Int;
                ParInitialStock.Value = detail.InitialStoque;

                SqlCmd.Parameters.Add(ParInitialStock);

             
                SqlParameter ParCurrentStock = new SqlParameter();
                ParCurrentStock.ParameterName = "@current_stoque";
                ParCurrentStock.SqlDbType = SqlDbType.VarChar;
                ParCurrentStock.Value = detail.CurrentStoque;

                SqlCmd.Parameters.Add(ParCurrentStock);

                SqlParameter ParProducedDate = new SqlParameter();
                ParProducedDate.ParameterName = "@produced_date";
                ParProducedDate.SqlDbType = SqlDbType.DateTime;
                ParProducedDate.Value = detail.ProductioDate;

                SqlCmd.Parameters.Add(ParProducedDate);


                SqlParameter ParExpireDate = new SqlParameter();
                ParExpireDate.ParameterName = "@expire_date";
                ParExpireDate.SqlDbType = SqlDbType.DateTime;
                ParExpireDate.Value = detail.ExpiredDate;

                SqlCmd.Parameters.Add(ParExpireDate);

                
                           


                //Execute the command
                resp = SqlCmd.ExecuteNonQuery() == 1 ? "OK" : "Not Inserted";

            }
            catch (Exception ex)
            {
                resp = "Something went wrong: " + ex.Message;
            }
            //We dont have the finally because we need to keep it opened for the others connections
            return resp;
        }

    }
}
