using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace DataLayer
{
    public class DProduct
    {
        private int _Id;
        private string _Code;
        private string _Name;
        private string _Description;
        private byte[] _Image;
        private int _IdCategory;
        private int _IdPresentation;
        private string TextSearch;

    

        public int Id
        {
            get { return _Id; }
            set { _Id = value; }
        }
        public string Code
        {
            get { return _Code; }
            set { _Code = value; }
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
        
        public byte[] Image
        {
            get { return _Image; }
            set { _Image = value; }
        }
        
        public int IdCategory
        {
            get { return _IdCategory; }
            set { _IdCategory = value; }
        }
        
        
      
        public int IdPresentation
        {
            get { return _IdPresentation; }
            set { _IdPresentation = value; }
        }

        public string TextSearch1
        {
            get { return TextSearch; }
            set { TextSearch = value; }
        }


        public DProduct()
        {

        }

        public DProduct(int id, string code, string name, string description, byte[] image, int idcategory, int idpresentation, string textsearch)
        {
            this.Id = id;
            this.Code = code;
            this.Name = name;
            this.Description = description;
            this.IdCategory = idcategory;
            this.IdPresentation = idpresentation;
            this.TextSearch1 = textsearch;


        }

        public string Insert(DProduct product)
        {
            string resp = "";
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                SqlCon.ConnectionString = Conection.cn;
                SqlCon.Open();

                SqlCommand SqlCmd = new SqlCommand();

                SqlCmd.Connection = SqlCon;
                SqlCmd.CommandText = "spinsert_product";
                SqlCmd.CommandType = CommandType.StoredProcedure;

                //Initialize variables
                SqlParameter ParId = new SqlParameter();
                ParId.ParameterName = "@id";
                ParId.SqlDbType = SqlDbType.Int;
                ParId.Direction = ParameterDirection.Output;

                SqlCmd.Parameters.Add(ParId);

                SqlParameter ParCode = new SqlParameter();
                ParCode.ParameterName = "@code";
                ParCode.SqlDbType = SqlDbType.VarChar;
                ParCode.Size = 50;
                ParCode.Value = product.Code;

                SqlCmd.Parameters.Add(ParCode);

                SqlParameter ParName = new SqlParameter();
                ParName.ParameterName = "@name";
                ParName.SqlDbType = SqlDbType.VarChar;
                ParName.Size = 50;
                ParName.Value = product.Name;

                SqlCmd.Parameters.Add(ParName);

                SqlParameter ParDesc = new SqlParameter();
                ParDesc.ParameterName = "@description";
                ParDesc.SqlDbType = SqlDbType.VarChar;
                ParDesc.Size = 1024;
                ParDesc.Value = product.Description;

                SqlCmd.Parameters.Add(ParDesc);

                SqlParameter ParImage = new SqlParameter();
                ParImage.ParameterName = "@image";
                ParImage.SqlDbType = SqlDbType.Image;
                ParImage.Value = product.Image;

                SqlCmd.Parameters.Add(ParImage);

                SqlParameter ParIdCategory = new SqlParameter();
                ParIdCategory.ParameterName = "@idcategory";
                ParIdCategory.SqlDbType = SqlDbType.Int;
                ParIdCategory.Value = product.IdCategory;

                SqlCmd.Parameters.Add(ParIdCategory);

                SqlParameter ParIdPresentation = new SqlParameter();
                ParIdPresentation.ParameterName = "@idpresentation";
                ParIdPresentation.SqlDbType = SqlDbType.Int;
                ParIdPresentation.Value = product.IdPresentation;

                SqlCmd.Parameters.Add(ParIdPresentation);


                //Execute the command
                resp = SqlCmd.ExecuteNonQuery() == 1 ? "OK" : "Not Inserted";

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

        public string Edit(DProduct product)
        {
            string resp = "";
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                SqlCon.ConnectionString = Conection.cn;
                SqlCon.Open();

                SqlCommand SqlCmd = new SqlCommand();

                SqlCmd.Connection = SqlCon;
                SqlCmd.CommandText = "spedit_product";
                SqlCmd.CommandType = CommandType.StoredProcedure;

                //Initialize variables
                SqlParameter ParIdProduct = new SqlParameter();
                ParIdProduct.ParameterName = "@id";
                ParIdProduct.SqlDbType = SqlDbType.Int;
                //ParIdProduct.Direction = ParameterDirection.Output;
                ParIdProduct.Value = product.Id;

                SqlCmd.Parameters.Add(ParIdProduct);



                SqlParameter ParCode = new SqlParameter();
                ParCode.ParameterName = "@code";
                ParCode.SqlDbType = SqlDbType.VarChar;
                ParCode.Size = 50;
                ParCode.Value = product.Code;

                SqlCmd.Parameters.Add(ParCode);

                SqlParameter ParName = new SqlParameter();
                ParName.ParameterName = "@name";
                ParName.SqlDbType = SqlDbType.VarChar;
                ParName.Size = 50;
                ParName.Value = product.Name;

                SqlCmd.Parameters.Add(ParName);

                SqlParameter ParDesc = new SqlParameter();
                ParDesc.ParameterName = "@description";
                ParDesc.SqlDbType = SqlDbType.VarChar;
                ParDesc.Size = 256;
                ParDesc.Value = product.Description;

                SqlCmd.Parameters.Add(ParDesc);


                SqlParameter ParImage = new SqlParameter();
                ParImage.ParameterName = "@image";
                ParImage.SqlDbType = SqlDbType.Image;
                ParImage.Value = product.Image;

                SqlCmd.Parameters.Add(ParImage);

                SqlParameter ParIdCategory = new SqlParameter();
                ParIdCategory.ParameterName = "@idcategory";
                ParIdCategory.SqlDbType = SqlDbType.Int;
                ParIdCategory.Value = product.IdCategory;

                SqlCmd.Parameters.Add(ParIdCategory);

                SqlParameter ParIdPresentation = new SqlParameter();
                ParIdPresentation.ParameterName = "@idpresentation";
                ParIdPresentation.SqlDbType = SqlDbType.Int;
                ParIdPresentation.Value = product.IdPresentation;

                SqlCmd.Parameters.Add(ParIdPresentation);

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

        public string Delete(DProduct product)
        {
            string resp = "";
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                SqlCon.ConnectionString = Conection.cn;
                SqlCon.Open();

                SqlCommand SqlCmd = new SqlCommand();

                SqlCmd.Connection = SqlCon;
                SqlCmd.CommandText = "spdelete_product";
                SqlCmd.CommandType = CommandType.StoredProcedure;

                //Initialize variables
                SqlParameter ParId = new SqlParameter();
                ParId.ParameterName = "@id"; //same name as in the procedure
                ParId.SqlDbType = SqlDbType.Int;
                ParId.Value = product.Id;

                SqlCmd.Parameters.Add(ParId);



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
            DataTable DtResult = new DataTable("product"); // Table on sql
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                SqlCon.ConnectionString = Conection.cn;
                SqlCommand SqlCmd = new SqlCommand();
                SqlCmd.Connection = SqlCon;
                SqlCmd.CommandText = "spshow_product";
                SqlCmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter SqlData = new SqlDataAdapter(SqlCmd);
                SqlData.Fill(DtResult);



            }
            catch (Exception ex)
            {
                DtResult = null;
                Console.Write("Could not get information: Exception");
            }
            return DtResult;

        }

        public DataTable ShowName(DProduct product)
        {
            DataTable DtResult = new DataTable("product");
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                SqlCon.ConnectionString = Conection.cn;
                SqlCommand SqlCmd = new SqlCommand();
                SqlCmd.Connection = SqlCon;
                SqlCmd.CommandText = "spsearch_product";
                SqlCmd.CommandType = CommandType.StoredProcedure;


                SqlParameter ParSearch = new SqlParameter();
                ParSearch.ParameterName = "@textsearch";
                ParSearch.SqlDbType = SqlDbType.VarChar;
                ParSearch.Size = 50;
                ParSearch.Value = product.TextSearch1;
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
