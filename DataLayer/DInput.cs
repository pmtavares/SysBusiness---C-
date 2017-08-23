using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace DataLayer
{
    public class DInput
    {
        private int _IdInput;
        
        private int _IdStaff;
        
        private int _IdSupplier;
        
        private DateTime _DateInput;

        private string _TypeDoc;
        
        private string _SerieDoc;
                
        private string _Corelativ;
                
        private decimal _Vat;

        private string _CurrentStatus;
               
     

        
        public int IdInput
        {
            get { return _IdInput; }
            set { _IdInput = value; }
        }

        public int IdStaff
        {
            get { return _IdStaff; }
            set { _IdStaff = value; }
        }

        public int IdSupplier
        {
            get { return _IdSupplier; }
            set { _IdSupplier = value; }
        }

        public DateTime DateInput
        {
            get { return _DateInput; }
            set { _DateInput = value; }
        }

        public string TypeDoc
        {
            get { return _TypeDoc; }
            set { _TypeDoc = value; }
        }

        public string SerieDoc
        {
            get { return _SerieDoc; }
            set { _SerieDoc = value; }
        }

        public string Corelativ
        {
            get { return _Corelativ; }
            set { _Corelativ = value; }
        }

        public decimal Vat
        {
            get { return _Vat; }
            set { _Vat = value; }
        }

        public string CurrentStatus
        {
            get { return _CurrentStatus; }
            set { _CurrentStatus = value; }
        }

        

        public DInput()
        {

        }

        public DInput(int idinput, int idstaff, int idsupplier, DateTime dateinput, string typeDoc, string serie, string corelativ,
            decimal vat, string currentStatus)
        {

            this.IdInput = idinput;
            this.IdStaff = idstaff;
            this.IdSupplier = IdSupplier;
            this.DateInput = dateinput;
            this.TypeDoc = typeDoc;
            this.SerieDoc = serie;
            this.Corelativ = corelativ;
            this.Vat = vat;
            this.CurrentStatus = currentStatus;
        }


        public string Insert(DInput Input, List<DDetail_Input> Details)
        {
            string resp = "";
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                SqlCon.ConnectionString = Conection.cn;
                SqlCon.Open();

                SqlTransaction sqlTra = SqlCon.BeginTransaction();



                SqlCommand SqlCmd = new SqlCommand();
                SqlCmd.Connection = SqlCon;
                SqlCmd.Transaction = sqlTra;
                SqlCmd.CommandText = "spinsert_inputproduct";
                SqlCmd.CommandType = CommandType.StoredProcedure;

                //Initialize variables
                SqlParameter ParId = new SqlParameter();
                ParId.ParameterName = "@idinput";
                ParId.SqlDbType = SqlDbType.Int;
                ParId.Direction = ParameterDirection.Output;
                SqlCmd.Parameters.Add(ParId);

                SqlParameter ParStaff = new SqlParameter();
                ParStaff.ParameterName = "@idstaff";
                ParStaff.SqlDbType = SqlDbType.Int;
                ParStaff.Value = Input.IdStaff;

                SqlCmd.Parameters.Add(ParStaff);


                SqlParameter ParSupplier = new SqlParameter();
                ParSupplier.ParameterName = "@idsupplier";
                ParSupplier.SqlDbType = SqlDbType.Int;
                ParSupplier.Value = Input.IdSupplier;

                SqlCmd.Parameters.Add(ParSupplier);


                SqlParameter ParDate = new SqlParameter();
                ParDate.ParameterName = "@date";
                ParDate.SqlDbType = SqlDbType.DateTime;
                ParDate.Value = Input.DateInput;

                SqlCmd.Parameters.Add(ParDate);

                SqlParameter ParType = new SqlParameter();
                ParType.ParameterName = "@type";
                ParType.SqlDbType = SqlDbType.VarChar;
                ParType.Size = 20;
                ParType.Value = Input.TypeDoc;

                SqlCmd.Parameters.Add(ParType);



                SqlParameter ParSerie = new SqlParameter();
                ParSerie.ParameterName = "@serie";
                ParSerie.SqlDbType = SqlDbType.VarChar;
                ParSerie.Size = 40;
                ParSerie.Value = Input.SerieDoc;

                SqlCmd.Parameters.Add(ParSerie);



                SqlParameter ParCorelative = new SqlParameter();
                ParCorelative.ParameterName = "@corelativ";
                ParCorelative.SqlDbType = SqlDbType.VarChar;
                ParCorelative.Size = 7;
                ParCorelative.Value = Input.Corelativ;

                SqlCmd.Parameters.Add(ParCorelative);


                SqlParameter ParVat = new SqlParameter();
                ParVat.ParameterName = "@vat";
                ParVat.SqlDbType = SqlDbType.Decimal;
                ParVat.Precision = 4;
                ParVat.Scale = 2;
                ParVat.Value = Input.Vat;

                SqlCmd.Parameters.Add(ParVat);

           
                SqlParameter ParCurrentStatus = new SqlParameter();
                ParCurrentStatus.ParameterName = "@currentstatus";
                ParCurrentStatus.SqlDbType = SqlDbType.VarChar;
                ParCurrentStatus.Size = 7;
                ParCurrentStatus.Value = Input.CurrentStatus;

                SqlCmd.Parameters.Add(ParCurrentStatus);


                //Execute the command
                resp = SqlCmd.ExecuteNonQuery() == 1 ? "OK" : "Not Inserted";


                if(resp.Equals("OK"))
                {
                    //Obtain the input code id that was generated
                    this.IdInput = Convert.ToInt32(SqlCmd.Parameters["@idinput"].Value);

                    foreach(DDetail_Input det in Details)
                    {
                        det.IdInput = this.IdInput;
                        
                        //call insert method from details input

                        resp = det.Insert(det, ref SqlCon, ref sqlTra);

                        if(!resp.Equals("OK"))
                        {
                            break;
                        }

                    }

                }

                if(resp.Equals("OK"))
                {

                    //Just when we use the commit is when all data are saved
                    sqlTra.Commit();
                }
                else
                {
                    sqlTra.Rollback(); //cancel everything
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

        public string Cancel(DInput input)
        {
            string resp = "";
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                SqlCon.ConnectionString = Conection.cn;
                SqlCon.Open();

                SqlCommand SqlCmd = new SqlCommand();

                SqlCmd.Connection = SqlCon;
                SqlCmd.CommandText = "spcancel_input";
                SqlCmd.CommandType = CommandType.StoredProcedure;

                //Initialize variables
                SqlParameter ParId = new SqlParameter();
                ParId.ParameterName = "@idinput"; //same name as in the procedure
                ParId.SqlDbType = SqlDbType.Int;
                ParId.Value = input.IdInput;
                SqlCmd.Parameters.Add(ParId);



                //Execute the command
                resp = SqlCmd.ExecuteNonQuery() == 1 ? "OK" : "Cancelation not done";



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

        public DataTable ShowInput()
        {
            DataTable DtResult = new DataTable("inputproduct");
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                SqlCon.ConnectionString = Conection.cn;
                SqlCommand SqlCmd = new SqlCommand();
                SqlCmd.Connection = SqlCon;
                SqlCmd.CommandText = "spshow_inputproduct";
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

        public DataTable SearchNameDate(string SearchText, string SearchText2)
        {
            DataTable DtResult = new DataTable("inputproduct");
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                SqlCon.ConnectionString = Conection.cn;
                SqlCommand SqlCmd = new SqlCommand();
                SqlCmd.Connection = SqlCon;
                SqlCmd.CommandText = "spsearch_input_date";
                SqlCmd.CommandType = CommandType.StoredProcedure;


                SqlParameter ParSearch = new SqlParameter();
                ParSearch.ParameterName = "@textsearch";
                ParSearch.SqlDbType = SqlDbType.VarChar;
                ParSearch.Size = 20;
                ParSearch.Value = SearchText;
                SqlCmd.Parameters.Add(ParSearch);

                SqlParameter ParSearch2 = new SqlParameter();
                ParSearch2.ParameterName = "@textsearch2";
                ParSearch2.SqlDbType = SqlDbType.VarChar;
                ParSearch2.Size = 20;
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


        public DataTable ShowDetails(string textSearch)
        {
            DataTable DtResult = new DataTable("inputdetail");
           
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                SqlCon.ConnectionString = Conection.cn;
                SqlCommand SqlCmd = new SqlCommand();
                SqlCmd.Connection = SqlCon;
                SqlCmd.CommandText = "sp_show_input_details";
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
