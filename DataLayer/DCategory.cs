using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace DataLayer
{
    public class DCategory
    {
        private int _Idcategory;
        private string _Name;
        private string _Description;
        private string _Textsearch;

        

        public int Idcategory
        {
            get { return _Idcategory; }
            set { _Idcategory = value; }
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

        //Create constructor
        public DCategory ()
        {

        }

        public DCategory(int idcategory, string name, string description, string searchtext)
        {
            this.Idcategory = idcategory;
            this.Name = name;
            this.Description = description;
            this.Textsearch = searchtext;
        }

        public string Insert(DCategory category)
        {
            string resp = "";
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                SqlCon.ConnectionString = Conection.cn;
                SqlCon.Open();

                SqlCommand SqlCmd = new SqlCommand();

                SqlCmd.Connection = SqlCon;
                SqlCmd.CommandText = "spinsert_category";
                SqlCmd.CommandType = CommandType.StoredProcedure;

                //Initialize variables
                SqlParameter ParIdCategory = new SqlParameter();
                ParIdCategory.ParameterName = "@idcategoria";
                ParIdCategory.SqlDbType = SqlDbType.Int;
                ParIdCategory.Direction = ParameterDirection.Output;

                SqlCmd.Parameters.Add(ParIdCategory);

                SqlParameter ParName = new SqlParameter();
                ParName.ParameterName = "@name";
                ParName.SqlDbType = SqlDbType.VarChar;
                ParName.Size = 50;
                ParName.Value = category.Name;

                SqlCmd.Parameters.Add(ParName);

                SqlParameter ParDesc = new SqlParameter();
                ParDesc.ParameterName = "@description";
                ParDesc.SqlDbType = SqlDbType.VarChar;
                ParDesc.Size = 256;
                ParDesc.Value = category.Description;

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

        public string Edit(DCategory category)
        {
            string resp = "";
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                SqlCon.ConnectionString = Conection.cn;
                SqlCon.Open();

                SqlCommand SqlCmd = new SqlCommand();

                SqlCmd.Connection = SqlCon;
                SqlCmd.CommandText = "spedit_category";
                SqlCmd.CommandType = CommandType.StoredProcedure;

                //Initialize variables
                SqlParameter ParIdCategory = new SqlParameter();
                ParIdCategory.ParameterName = "@idcategory";
                ParIdCategory.SqlDbType = SqlDbType.Int;
                ParIdCategory.Value = category.Idcategory;

                SqlCmd.Parameters.Add(ParIdCategory);

                SqlParameter ParName = new SqlParameter();
                ParName.ParameterName = "@name";
                ParName.SqlDbType = SqlDbType.VarChar;
                ParName.Size = 50;
                ParName.Value = category.Name;

                SqlCmd.Parameters.Add(ParName);

                SqlParameter ParDesc = new SqlParameter();
                ParDesc.ParameterName = "@description";
                ParDesc.SqlDbType = SqlDbType.VarChar;
                ParDesc.Size = 256;
                ParDesc.Value = category.Description;

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

        public string Delete(DCategory category)
        {
             string resp = "";
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                SqlCon.ConnectionString = Conection.cn;
                SqlCon.Open();

                SqlCommand SqlCmd = new SqlCommand();

                SqlCmd.Connection = SqlCon;
                SqlCmd.CommandText = "spdelete_category";
                SqlCmd.CommandType = CommandType.StoredProcedure;

                //Initialize variables
                SqlParameter ParIdCategory = new SqlParameter();
                ParIdCategory.ParameterName = "@idcategory";
                ParIdCategory.SqlDbType = SqlDbType.Int;
                ParIdCategory.Value = category.Idcategory;

                SqlCmd.Parameters.Add(ParIdCategory);



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
            DataTable DtResult = new DataTable("category");
            SqlConnection SqlCon = new SqlConnection();
            try{
                SqlCon.ConnectionString = Conection.cn;
                SqlCommand SqlCmd = new SqlCommand();
                SqlCmd.Connection = SqlCon;
                SqlCmd.CommandText = "spshow_category";
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

        public DataTable ShowName(DCategory category)
        {
            DataTable DtResult = new DataTable("category");
            SqlConnection SqlCon = new SqlConnection();
            try{
                SqlCon.ConnectionString = Conection.cn;
                SqlCommand SqlCmd = new SqlCommand();
                SqlCmd.Connection = SqlCon;
                SqlCmd.CommandText = "spsearch_name";
                SqlCmd.CommandType = CommandType.StoredProcedure;
                
               
                SqlParameter ParSearch = new SqlParameter();
                ParSearch.ParameterName = "@textsearch";
                ParSearch.SqlDbType = SqlDbType.VarChar;
                ParSearch.Size = 50;
                ParSearch.Value = category.Textsearch;
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
