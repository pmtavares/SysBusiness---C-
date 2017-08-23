using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace DataLayer
{
    public class DSale
    {
        private int _IDSale;
        private int _IDClient;
        private int _IDStaff;
        private DateTime _Date;
        private string _Type_Receipt;

        private string _Serie;


        private string _Corelation;


        private string _Vat;

        public int IDSale
        {
            get { return _IDSale; }
            set { _IDSale = value; }
        }
        

        public int IDClient
        {
            get { return _IDClient; }
            set { _IDClient = value; }
        }
        

        public int IDStaff
        {
            get { return _IDStaff; }
            set { _IDStaff = value; }
        }
        

        public DateTime Date
        {
            get { return _Date; }
            set { _Date = value; }
        }
        

        public string Type_Receipt
        {
            get { return _Type_Receipt; }
            set { _Type_Receipt = value; }
        }
        

        public string Serie
        {
            get { return _Serie; }
            set { _Serie = value; }
        }

        public string Corelation
        {
            get { return _Corelation; }
            set { _Corelation = value; }
        }

        public string Vat
        {
            get { return _Vat; }
            set { _Vat = value; }
        }


        public DSale()
        {

        }

        public DSale(int idclient, int idstaff, DateTime datetime, string typereceipt,
            string serie, string corelation, string vat)
        {
            this.IDClient = idclient;
            this.IDStaff = idstaff;
            this.Date = datetime;
            this.Type_Receipt = typereceipt;
            this.Corelation = corelation;
            this.Serie = serie;
            this.Vat = vat;
        }

        //Reduce stock method
        public string Reduce(int detail_input, int quantity)
        {
            string resp = "";
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                SqlCon.ConnectionString = Conection.cn;
                SqlCon.Open();

                SqlCommand SqlCmd = new SqlCommand();

                SqlCmd.Connection = SqlCon;
                SqlCmd.CommandText = "spreduce_stock";
                SqlCmd.CommandType = CommandType.StoredProcedure;

                //Initialize variables
                SqlParameter ParIdDetailInput = new SqlParameter();
                ParIdDetailInput.ParameterName = "@idinput_detail"; //same name as in the procedure
                ParIdDetailInput.SqlDbType = SqlDbType.Int;
                ParIdDetailInput.Value = detail_input;
                SqlCmd.Parameters.Add(ParIdDetailInput);

                SqlParameter ParQuantity = new SqlParameter();
                ParQuantity.ParameterName = "@quantity"; //same name as in the procedure
                ParQuantity.SqlDbType = SqlDbType.Int;
                ParQuantity.Value = quantity;
                SqlCmd.Parameters.Add(ParQuantity);

                //Execute the command
                resp = SqlCmd.ExecuteNonQuery() == 1 ? "OK" : "Update not done";



            }
            catch (Exception ex)
            {
                resp = "Something went wrong: " + ex.Message;
            }
           // finally
           // {
               // if (SqlCon.State == ConnectionState.Open)
               // {
                   // SqlCon.Close();
               // }
            //}

            return resp;

        }

        public string Insert(DSale sale, List<DDetail_Sale> detail)
        {
            string resp = "";
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                SqlCon.ConnectionString = Conection.cn;
                SqlCon.Open();

                SqlTransaction SqlTra = SqlCon.BeginTransaction();

                SqlCommand SqlCmd = new SqlCommand();
                SqlCmd.Connection = SqlCon;
                SqlCmd.Transaction = SqlTra;
                SqlCmd.CommandText = "spinsert_sale";
                SqlCmd.CommandType = CommandType.StoredProcedure;

                //Initialize variables
                SqlParameter ParIdSale = new SqlParameter();
                ParIdSale.ParameterName = "@idsale";
                ParIdSale.SqlDbType = SqlDbType.Int;
                ParIdSale.Direction = ParameterDirection.Output;
                SqlCmd.Parameters.Add(ParIdSale);

                SqlParameter ParIdClient = new SqlParameter();
                ParIdClient.ParameterName = "@idclient";
                ParIdClient.SqlDbType = SqlDbType.Int;
                ParIdClient.Value = sale.IDClient;
                SqlCmd.Parameters.Add(ParIdClient);


                SqlParameter ParIdStaff = new SqlParameter();
                ParIdStaff.ParameterName = "@idstaff";
                ParIdStaff.SqlDbType = SqlDbType.Int;
                ParIdStaff.Value = sale.IDStaff;
                SqlCmd.Parameters.Add(ParIdStaff);
                
                SqlParameter ParDate= new SqlParameter();
                ParDate.ParameterName = "@date";
                ParDate.SqlDbType = SqlDbType.Date;
                ParDate.Value = sale.Date;

                SqlCmd.Parameters.Add(ParDate);



                SqlParameter ParTypeReceipt = new SqlParameter();
                ParTypeReceipt.ParameterName = "@type_receipt";
                ParTypeReceipt.SqlDbType = SqlDbType.VarChar;
                ParTypeReceipt.Size = 50;
                ParTypeReceipt.Value = sale.Type_Receipt;

                SqlCmd.Parameters.Add(ParTypeReceipt);

                SqlParameter ParSerie= new SqlParameter();
                ParSerie.ParameterName = "@serie";
                ParSerie.SqlDbType = SqlDbType.VarChar;
                ParSerie.Size = 4;
                ParSerie.Value = sale.Serie;

                SqlCmd.Parameters.Add(ParSerie);

                SqlParameter ParCorelation = new SqlParameter();
                ParCorelation.ParameterName = "@corelation";
                ParCorelation.SqlDbType = SqlDbType.VarChar;
                ParCorelation.Size = 7;
                ParCorelation.Value = sale.Corelation;

                SqlCmd.Parameters.Add(ParCorelation);

                SqlParameter ParVat = new SqlParameter();
                ParVat.ParameterName = "@vat";
                ParVat.SqlDbType = SqlDbType.Decimal;
                ParVat.Scale = 2;
                ParVat.Precision = 4;
                ParVat.Value = sale.Vat;

                SqlCmd.Parameters.Add(ParVat);

                

                //Execute the command
                resp = SqlCmd.ExecuteNonQuery() == 1 ? "OK" : "Not Inserted";

                if (resp.Equals("OK"))
                {
                    //Obtain the sale code id that was generated
                    this.IDSale = Convert.ToInt32(SqlCmd.Parameters["@idsale"].Value);

                    foreach (DDetail_Sale det in detail)
                    {
                        det.IDSale = this.IDSale;

                        //call insert method from details input

                        resp = det.Insert(det, ref SqlCon, ref SqlTra);

                        if (!resp.Equals("OK"))
                        {
                            break;
                        }
                        else
                        {
                            resp = Reduce(det.IDDetail_Input, det.Quantity);
                            if(!resp.Equals("OK"))
                            {
                                break;
                            }
                        }
                        
                    } 

                }

                if (resp.Equals("OK"))
                {

                    //Just when we use the commit is when all data are saved
                    SqlTra.Commit();
                }
                else
                {
                    SqlTra.Rollback(); //cancel everything
                }

            }
            catch (Exception ex)
            {
                resp = "Something went wrong: " + ex.Message;
            }
            finally
            {
                if (SqlCon.State == ConnectionState.Open)
                {
                    SqlCon.Close();
                }

            }
            return resp;
        }

        public string Delete(DSale sale)
        {
            string resp = "";
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                SqlCon.ConnectionString = Conection.cn;
                SqlCon.Open();

                SqlCommand SqlCmd = new SqlCommand();

                SqlCmd.Connection = SqlCon;
                SqlCmd.CommandText = "spdelete_sale";
                SqlCmd.CommandType = CommandType.StoredProcedure;

                //Initialize variables
                SqlParameter ParId = new SqlParameter();
                ParId.ParameterName = "@idsale"; //same name as in the procedure
                ParId.SqlDbType = SqlDbType.Int;
                ParId.Value = sale.IDSale;
                SqlCmd.Parameters.Add(ParId);



                //Execute the command
                resp = SqlCmd.ExecuteNonQuery() == 1 ? "Check if it was deleted " : "OK";



            }
            catch (Exception ex)
            {
                resp = "Something went wrong: " + ex.Message;
            }
            finally
            {
                if (SqlCon.State == ConnectionState.Open)
                {
                    SqlCon.Close();
                }
            }

            return resp;

        }

        public DataTable ShowSale()
        {
            DataTable DtResult = new DataTable("sale");
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                SqlCon.ConnectionString = Conection.cn;
                SqlCommand SqlCmd = new SqlCommand();
                SqlCmd.Connection = SqlCon;
                SqlCmd.CommandText = "spshow_sale";
                SqlCmd.CommandType = CommandType.StoredProcedure;

                
                SqlDataAdapter SqlData = new SqlDataAdapter(SqlCmd);
                SqlData.Fill(DtResult);


            }
            catch (Exception ex)
            {
                DtResult = null;
            }
            return DtResult;
        }

        public DataTable SearchDate(string SearchText, string SearchText2)
        {
            DataTable DtResult = new DataTable("sale");
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                SqlCon.ConnectionString = Conection.cn;
                SqlCommand SqlCmd = new SqlCommand();
                SqlCmd.Connection = SqlCon;
                SqlCmd.CommandText = "spsearch_sale_date";
                SqlCmd.CommandType = CommandType.StoredProcedure;


                SqlParameter ParSearch = new SqlParameter();
                ParSearch.ParameterName = "@textsearch";
                ParSearch.SqlDbType = SqlDbType.VarChar;
                ParSearch.Size = 50;
                ParSearch.Value = SearchText;
                SqlCmd.Parameters.Add(ParSearch);

                SqlParameter ParSearch2 = new SqlParameter();
                ParSearch2.ParameterName = "@textsearch2";
                ParSearch2.SqlDbType = SqlDbType.VarChar;
                ParSearch2.Size = 50;
                ParSearch2.Value = SearchText2;
                SqlCmd.Parameters.Add(ParSearch2);


                SqlDataAdapter SqlData = new SqlDataAdapter(SqlCmd);
                SqlData.Fill(DtResult);


            }
            catch (Exception ex)
            {
                DtResult = null;
            }
            return DtResult;
        }


        public DataTable ShowProduct_Sale_Name(string textSearch)
        {
            DataTable DtResult = new DataTable("product");

            SqlConnection SqlCon = new SqlConnection();
            try
            {
                SqlCon.ConnectionString = Conection.cn;
                SqlCommand SqlCmd = new SqlCommand();
                SqlCmd.Connection = SqlCon;
                SqlCmd.CommandText = "spsearchproduct_sale_name";
                SqlCmd.CommandType = CommandType.StoredProcedure;


                SqlParameter ParSearch = new SqlParameter();
                ParSearch.ParameterName = "@textsearch";
                ParSearch.SqlDbType = SqlDbType.VarChar;
                ParSearch.Value = textSearch;
                ParSearch.Size = 50;
                SqlCmd.Parameters.Add(ParSearch);
                
                SqlDataAdapter SqlData = new SqlDataAdapter(SqlCmd);
                SqlData.Fill(DtResult);


            }
            catch (Exception ex)
            {
                DtResult = null;
            }
            return DtResult;
        }

        public DataTable ShowProduct_Sale_Code(string textSearch)
        {
            DataTable DtResult = new DataTable("product");

            SqlConnection SqlCon = new SqlConnection();
            try
            {
                SqlCon.ConnectionString = Conection.cn;
                SqlCommand SqlCmd = new SqlCommand();
                SqlCmd.Connection = SqlCon;
                SqlCmd.CommandText = "spsearchproduct_sale_code";
                SqlCmd.CommandType = CommandType.StoredProcedure;


                SqlParameter ParSearch = new SqlParameter();
                ParSearch.ParameterName = "@textsearch";
                ParSearch.SqlDbType = SqlDbType.VarChar;
                ParSearch.Value = textSearch;
                ParSearch.Size = 50;
                SqlCmd.Parameters.Add(ParSearch);




                SqlDataAdapter SqlData = new SqlDataAdapter(SqlCmd);
                SqlData.Fill(DtResult);


            }
            catch (Exception ex)
            {
                DtResult = null;
            }
            return DtResult;
        }
        public DataTable ShowDetailsSale(string textSearch)
        {
            DataTable DtResult = new DataTable("detail_sale");

            SqlConnection SqlCon = new SqlConnection();
            try
            {
                SqlCon.ConnectionString = Conection.cn;
                SqlCommand SqlCmd = new SqlCommand();
                SqlCmd.Connection = SqlCon;
                SqlCmd.CommandText = "spshow_detail_sale";
                SqlCmd.CommandType = CommandType.StoredProcedure;


                SqlParameter ParSearch = new SqlParameter();
                ParSearch.ParameterName = "@searchtext";
                ParSearch.SqlDbType = SqlDbType.VarChar;
                ParSearch.Value = textSearch;
                ParSearch.Size = 20;
                SqlCmd.Parameters.Add(ParSearch);




                SqlDataAdapter SqlData = new SqlDataAdapter(SqlCmd);
                SqlData.Fill(DtResult);


            }
            catch (Exception ex)
            {
                DtResult = null;
            }
            return DtResult;
        }
    }
}
