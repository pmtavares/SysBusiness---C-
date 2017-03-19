using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;


namespace DataLayer
{
    public class DSupplier
    {
        private int _Id;
        private string _Company;
        private string _Business_Department;
        private string _Document_Type;
        private string _Document_Number;
        private string _Address;
        private string _Phone;
        private string _Email;
        private string _Url;
        private string _SearchText;

       

        public int Id
        {
            get { return _Id; }
            set { _Id = value; }
        }

        public string Company
        {
            get { return _Company; }
            set { _Company = value; }
        }

        public string Business_Department
        {
            get { return _Business_Department; }
            set { _Business_Department = value; }
        }

        public string Document_Type
        {
            get { return _Document_Type; }
            set { _Document_Type = value; }
        }

        public string Document_Number
        {
            get { return _Document_Number; }
            set { _Document_Number = value; }
        }

        public string Address
        {
            get { return _Address; }
            set { _Address = value; }
        }
        public string Phone
        {
            get { return _Phone; }
            set { _Phone = value; }
        }

        public string Email
        {
            get { return _Email; }
            set { _Email = value; }
        }

        public string Url
        {
            get { return _Url; }
            set { _Url = value; }
        }

        public string SearchText
        {
            get { return _SearchText; }
            set { _SearchText = value; }
        }

        public DSupplier()
        {

        }

        public DSupplier(int id, string company, string businessDepart, 
            string documentType, string document_Number, string address, string phone, string email, string url, string textsearch)
        {
            this.Id = id;
            this.Company = company;
            this.Business_Department = businessDepart;
            this.Document_Type = documentType;
            this.Document_Number = document_Number;
            this.Address = address;
            this.Phone = phone;
            this.Url = url;
            this.Email = email;
            this.SearchText = textsearch;

        }

        public string Insert(DSupplier supplier)
        {
            string resp = "";
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                SqlCon.ConnectionString = Conection.cn;
                SqlCon.Open();

                SqlCommand SqlCmd = new SqlCommand();

                SqlCmd.Connection = SqlCon;
                SqlCmd.CommandText = "spinsert_supplier";
                SqlCmd.CommandType = CommandType.StoredProcedure;

                //Initialize variables
                SqlParameter ParId = new SqlParameter();
                ParId.ParameterName = "@id";
                ParId.SqlDbType = SqlDbType.Int;
                ParId.Direction = ParameterDirection.Output;

                SqlCmd.Parameters.Add(ParId);

                SqlParameter ParCompany = new SqlParameter();
                ParCompany.ParameterName = "@company";
                ParCompany.SqlDbType = SqlDbType.VarChar;
                ParCompany.Size = 150;
                ParCompany.Value = supplier.Company;

                SqlCmd.Parameters.Add(ParCompany);

                SqlParameter ParBusiness_department = new SqlParameter();
                ParBusiness_department.ParameterName = "@business_department";
                ParBusiness_department.SqlDbType = SqlDbType.VarChar;
                ParBusiness_department.Size = 50;
                ParBusiness_department.Value = supplier.Business_Department;

                SqlCmd.Parameters.Add(ParBusiness_department);

                SqlParameter ParDocumentType = new SqlParameter();
                ParDocumentType.ParameterName = "@document_type";
                ParDocumentType.SqlDbType = SqlDbType.VarChar;
                ParDocumentType.Size = 50;
                ParDocumentType.Value = supplier.Document_Type;

                SqlCmd.Parameters.Add(ParDocumentType);

                SqlParameter ParDocument_number = new SqlParameter();
                ParDocument_number.ParameterName = "@document_number";
                ParDocument_number.SqlDbType = SqlDbType.VarChar;
                ParDocumentType.Size = 50;
                ParDocument_number.Value = supplier.Document_Number;

                SqlCmd.Parameters.Add(ParDocument_number);

                SqlParameter ParAddress = new SqlParameter();
                ParAddress.ParameterName = "@address";
                ParAddress.SqlDbType = SqlDbType.VarChar;
                ParAddress.Size = 150;
                ParAddress.Value = supplier.Address;

                SqlCmd.Parameters.Add(ParAddress);

                SqlParameter ParPhone = new SqlParameter();
                ParPhone.ParameterName = "@phone";
                ParPhone.SqlDbType = SqlDbType.VarChar;
                ParPhone.Size = 50;
                ParPhone.Value = supplier.Phone;

                SqlCmd.Parameters.Add(ParPhone);


                SqlParameter ParEmail = new SqlParameter();
                ParEmail.ParameterName = "@email";
                ParEmail.SqlDbType = SqlDbType.VarChar;
                ParEmail.Size = 50;
                ParEmail.Value = supplier.Email;

                SqlCmd.Parameters.Add(ParEmail);

                SqlParameter ParUrl= new SqlParameter();
                ParUrl.ParameterName = "@url";
                ParUrl.SqlDbType = SqlDbType.VarChar;
                ParUrl.Size = 50;
                ParUrl.Value = supplier.Url;

                SqlCmd.Parameters.Add(ParUrl);


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
        public string Edit(DSupplier supplier)
        {
            string resp = "";
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                SqlCon.ConnectionString = Conection.cn;
                SqlCon.Open();

                SqlCommand SqlCmd = new SqlCommand();

                SqlCmd.Connection = SqlCon;
                SqlCmd.CommandText = "spedit_supplier";
                SqlCmd.CommandType = CommandType.StoredProcedure;

                //Initialize variables
                SqlParameter ParId = new SqlParameter();
                ParId.ParameterName = "@id";
                ParId.SqlDbType = SqlDbType.Int;
                ParId.Value = supplier.Id;
                //ParId.Direction = ParameterDirection.Output;

                SqlCmd.Parameters.Add(ParId);

                SqlParameter ParCompany = new SqlParameter();
                ParCompany.ParameterName = "@company";
                ParCompany.SqlDbType = SqlDbType.VarChar;
                ParCompany.Size = 50;
                ParCompany.Value = supplier.Company;

                SqlCmd.Parameters.Add(ParCompany);

                SqlParameter ParBusiness_department = new SqlParameter();
                ParBusiness_department.ParameterName = "@business_department";
                ParBusiness_department.SqlDbType = SqlDbType.VarChar;
                ParBusiness_department.Size = 50;
                ParBusiness_department.Value = supplier.Business_Department;

                SqlCmd.Parameters.Add(ParBusiness_department);

                SqlParameter ParDocumentType = new SqlParameter();
                ParDocumentType.ParameterName = "@document_type";
                ParDocumentType.SqlDbType = SqlDbType.VarChar;
                ParDocumentType.Size = 50;
                ParDocumentType.Value = supplier.Document_Type;

                SqlCmd.Parameters.Add(ParDocumentType);

                SqlParameter ParDocument_number = new SqlParameter();
                ParDocument_number.ParameterName = "@document_number";
                ParDocument_number.SqlDbType = SqlDbType.VarChar;
                ParDocumentType.Size = 50;
                ParDocument_number.Value = supplier.Document_Number;

                SqlCmd.Parameters.Add(ParDocument_number);

                SqlParameter ParAddress = new SqlParameter();
                ParAddress.ParameterName = "@address";
                ParAddress.SqlDbType = SqlDbType.VarChar;
                ParAddress.Size = 150;
                ParAddress.Value = supplier.Address;

                SqlCmd.Parameters.Add(ParAddress);

                SqlParameter ParPhone = new SqlParameter();
                ParPhone.ParameterName = "@phone";
                ParPhone.SqlDbType = SqlDbType.VarChar;
                ParPhone.Size = 50;
                ParPhone.Value = supplier.Phone;

                SqlCmd.Parameters.Add(ParPhone);


                SqlParameter ParEmail = new SqlParameter();
                ParEmail.ParameterName = "@email";
                ParEmail.SqlDbType = SqlDbType.VarChar;
                ParEmail.Size = 50;
                ParEmail.Value = supplier.Email;

                SqlCmd.Parameters.Add(ParEmail);

                SqlParameter ParUrl = new SqlParameter();
                ParUrl.ParameterName = "@url";
                ParUrl.SqlDbType = SqlDbType.VarChar;
                ParUrl.Size = 50;
                ParUrl.Value = supplier.Url;

                SqlCmd.Parameters.Add(ParUrl);

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

        public string Delete(DSupplier supplier)
        {
            string resp = "";
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                SqlCon.ConnectionString = Conection.cn;
                SqlCon.Open();

                SqlCommand SqlCmd = new SqlCommand();

                SqlCmd.Connection = SqlCon;
                SqlCmd.CommandText = "spdelete_supplier";
                SqlCmd.CommandType = CommandType.StoredProcedure;

                //Initialize variables
                SqlParameter ParId = new SqlParameter();
                ParId.ParameterName = "@id"; //same name as in the procedure
                ParId.SqlDbType = SqlDbType.Int;
                ParId.Value = supplier.Id;

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
            DataTable DtResult = new DataTable("supplier"); // Table on sql
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                SqlCon.ConnectionString = Conection.cn;
                SqlCommand SqlCmd = new SqlCommand();
                SqlCmd.Connection = SqlCon;
                SqlCmd.CommandText = "spshow_supplier";
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

        public DataTable ShowName(DSupplier supplier)
        {
            DataTable DtResult = new DataTable("supplier");
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                SqlCon.ConnectionString = Conection.cn;
                SqlCommand SqlCmd = new SqlCommand();
                SqlCmd.Connection = SqlCon;
                SqlCmd.CommandText = "spsearch_supplier_company";
                SqlCmd.CommandType = CommandType.StoredProcedure;


                SqlParameter ParSearch = new SqlParameter();
                ParSearch.ParameterName = "@searchtext";
                ParSearch.SqlDbType = SqlDbType.VarChar;
                ParSearch.Size = 50;
                ParSearch.Value = supplier.SearchText;
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


        public DataTable SearchDocument(DSupplier supplier)
        {
            DataTable DtResult = new DataTable("supplier");
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                SqlCon.ConnectionString = Conection.cn;
                SqlCommand SqlCmd = new SqlCommand();
                SqlCmd.Connection = SqlCon;
                SqlCmd.CommandText = "spsearch_supplier_document";
                SqlCmd.CommandType = CommandType.StoredProcedure;


                SqlParameter ParSearch = new SqlParameter();
                ParSearch.ParameterName = "@searchtext";
                ParSearch.SqlDbType = SqlDbType.VarChar;
                ParSearch.Size = 50;
                ParSearch.Value = supplier.SearchText;
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
