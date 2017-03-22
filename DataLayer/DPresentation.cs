using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;


namespace DataLayer
{
    public class DPresentation
    {

        private int _Idpresentation;
        private string _Name;
        private string _Description;
        private string _Textsearch;

        public int Idpresentation
        {
            get { return _Idpresentation; }
            set { _Idpresentation = value; }
        }

        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }
        public string Textsearch
        {
            get { return _Textsearch; }
            set { _Textsearch = value; }
        }

        public DPresentation ()
        {

        }

        public DPresentation(int idpresentation, string name, string description, string searchtext)
        {
            this.Idpresentation = idpresentation;
            this.Name = name;
            this.Description = description;
            this.Textsearch = searchtext;
        }

        public string Insert(DPresentation presentation)
        {
            string resp = "";
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                SqlCon.ConnectionString = Conection.cn;
                SqlCon.Open();

                SqlCommand SqlCmd = new SqlCommand();

                SqlCmd.Connection = SqlCon;
                SqlCmd.CommandText = "spinsert_presentation";
                SqlCmd.CommandType = CommandType.StoredProcedure;

                //Initialize variables
                SqlParameter ParIdPresentation = new SqlParameter();
                ParIdPresentation.ParameterName = "@idpresentation";
                ParIdPresentation.SqlDbType = SqlDbType.Int;
                ParIdPresentation.Direction = ParameterDirection.Output;

                SqlCmd.Parameters.Add(ParIdPresentation);

                SqlParameter ParName = new SqlParameter();
                ParName.ParameterName = "@name";
                ParName.SqlDbType = SqlDbType.VarChar;
                ParName.Size = 50;
                ParName.Value = presentation.Name;

                SqlCmd.Parameters.Add(ParName);

                SqlParameter ParDesc = new SqlParameter();
                ParDesc.ParameterName = "@description";
                ParDesc.SqlDbType = SqlDbType.VarChar;
                ParDesc.Size = 256;
                ParDesc.Value = presentation.Description;

                SqlCmd.Parameters.Add(ParDesc);

                //Execute the command
                resp = SqlCmd.ExecuteNonQuery() == 1 ? "OK" : "Not Inserted";

            }
            catch(Exception ex)
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

        public string Edit(DPresentation presentation)
        {
            string resp = "";
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                SqlCon.ConnectionString = Conection.cn;
                SqlCon.Open();

                SqlCommand SqlCmd = new SqlCommand();

                SqlCmd.Connection = SqlCon;
                SqlCmd.CommandText = "spedit_presentation";
                SqlCmd.CommandType = CommandType.StoredProcedure;

                //Initialize variables
                SqlParameter ParIdPresentation = new SqlParameter();
                ParIdPresentation.ParameterName = "@idpresentation";
                ParIdPresentation.SqlDbType = SqlDbType.Int;
                ParIdPresentation.Value = presentation.Idpresentation;

                SqlCmd.Parameters.Add(ParIdPresentation);

                SqlParameter ParName = new SqlParameter();
                ParName.ParameterName = "@name";
                ParName.SqlDbType = SqlDbType.VarChar;
                ParName.Size = 50;
                ParName.Value = presentation.Name;

                SqlCmd.Parameters.Add(ParName);

                SqlParameter ParDesc = new SqlParameter();
                ParDesc.ParameterName = "@description";
                ParDesc.SqlDbType = SqlDbType.VarChar;
                ParDesc.Size = 256;
                ParDesc.Value = presentation.Description;

                SqlCmd.Parameters.Add(ParDesc);

                //Execute the command
                resp = SqlCmd.ExecuteNonQuery() == 1 ? "OK" : "Not Edited";

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

        public string Delete(DPresentation category)
        {
             string resp = "";
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                SqlCon.ConnectionString = Conection.cn;
                SqlCon.Open();

                SqlCommand SqlCmd = new SqlCommand();

                SqlCmd.Connection = SqlCon;
                SqlCmd.CommandText = "spdelete_presentation";
                SqlCmd.CommandType = CommandType.StoredProcedure;

                //Initialize variables
                SqlParameter ParIdPresentation = new SqlParameter();
                ParIdPresentation.ParameterName = "@idpresentation";
                ParIdPresentation.SqlDbType = SqlDbType.Int;
                ParIdPresentation.Value = category.Idpresentation;

                SqlCmd.Parameters.Add(ParIdPresentation);



                //Execute the command
                resp = SqlCmd.ExecuteNonQuery() == 1 ? "OK" : "Not deleted";

                

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

        public DataTable ShowValues()
        {
            DataTable DtResult = new DataTable("presentation"); // Table on sql
            SqlConnection SqlCon = new SqlConnection();
            try{
                SqlCon.ConnectionString = Conection.cn;
                SqlCommand SqlCmd = new SqlCommand();
                SqlCmd.Connection = SqlCon;
                SqlCmd.CommandText = "spshow_presentation";
                SqlCmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter SqlData = new SqlDataAdapter(SqlCmd);
                SqlData.Fill(DtResult);

               

            }catch(Exception ex)
            {
                DtResult = null;
                Console.Write("Could not get information: Exception");
            }
            return DtResult;

        }

        public DataTable ShowName(DPresentation presentation)
        {
            DataTable DtResult = new DataTable("presentation");
            SqlConnection SqlCon = new SqlConnection();
            try{
                SqlCon.ConnectionString = Conection.cn;
                SqlCommand SqlCmd = new SqlCommand();
                SqlCmd.Connection = SqlCon;
                SqlCmd.CommandText = "spsearch_presentation_name";
                SqlCmd.CommandType = CommandType.StoredProcedure;
                
               
                SqlParameter ParSearch = new SqlParameter();
                ParSearch.ParameterName = "@textsearch";
                ParSearch.SqlDbType = SqlDbType.VarChar;
                ParSearch.Size = 50;
                ParSearch.Value = presentation.Textsearch;
                SqlCmd.Parameters.Add(ParSearch);


                SqlDataAdapter SqlData = new SqlDataAdapter(SqlCmd);
                SqlData.Fill(DtResult);
               

            }catch(Exception ex)
            {
                DtResult = null;
            }
            return DtResult;
        }
    }

    
}
