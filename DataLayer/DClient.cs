using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace DataLayer
{
    public class DClient
    {

        private int _Id;

        private string _Name;

        private string _Surname;

        private string _Gender;

        private DateTime _Dob;

        private string _TypeDocument;
        
        private string _DocumentNumber;

        private string _Address;

        private string _Phone;

        private string _Email;

        
        private string _SearchText;

        public int Id
        {
            get { return _Id; }
            set { _Id = value; }
        }
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        public string Surname
        {
            get { return _Surname; }
            set { _Surname = value; }
        }
        public string Gender
        {
            get { return _Gender; }
            set { _Gender = value; }
        }
        public DateTime Dob
        {
            get { return _Dob; }
            set { _Dob = value; }
        }

        public string TypeDocument
        {
            get { return _TypeDocument; }
            set { _TypeDocument = value; }
        }
        public string DocumentNumber
        {
            get { return _DocumentNumber; }
            set { _DocumentNumber = value; }
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
     
        
        public string SearchText
        {
            get { return _SearchText; }
            set { _SearchText = value; }
        }

         public DClient()
        {

        }

         public DClient(int id, string name, string surname, string gender, DateTime dob, 
             string type_document, string document_number, string address, string phone, string email,
              string textsearch)
        {
            this.Id = id;
            this.Name = name;
            this.Surname = surname;
            this.Gender = gender;
            this.Dob = dob;
            this.TypeDocument = type_document;
            this.DocumentNumber = document_number;
            this.Address = address;
            this.Phone = phone;
            
            this.Email = email;
           
            this.SearchText = textsearch;

        }
         public string Insert(DClient client)
         {
             string resp = "";
             SqlConnection SqlCon = new SqlConnection();
             try
             {
                 SqlCon.ConnectionString = Conection.cn;
                 SqlCon.Open();

                 SqlCommand SqlCmd = new SqlCommand();
                 SqlCmd.Connection = SqlCon;
                 SqlCmd.CommandText = "spinsert_client";
                 SqlCmd.CommandType = CommandType.StoredProcedure;

                 //Initialize variables
                 SqlParameter ParId = new SqlParameter();
                 ParId.ParameterName = "@id";
                 ParId.SqlDbType = SqlDbType.Int;
                 ParId.Direction = ParameterDirection.Output;
                 SqlCmd.Parameters.Add(ParId);

                 SqlParameter ParName = new SqlParameter();
                 ParName.ParameterName = "@name";
                 ParName.SqlDbType = SqlDbType.VarChar;
                 ParName.Size = 50;
                 ParName.Value = client.Name;

                 SqlCmd.Parameters.Add(ParName);

                 SqlParameter ParSurname = new SqlParameter();
                 ParSurname.ParameterName = "@surname";
                 ParSurname.SqlDbType = SqlDbType.VarChar;
                 ParSurname.Size = 50;
                 ParSurname.Value = client.Surname;

                 SqlCmd.Parameters.Add(ParSurname);



                 SqlParameter ParGender = new SqlParameter();
                 ParGender.ParameterName = "@gender";
                 ParGender.SqlDbType = SqlDbType.VarChar;
                 ParGender.Size = 1;
                 ParGender.Value = client.Gender;

                 SqlCmd.Parameters.Add(ParGender);

                 SqlParameter ParDob = new SqlParameter();
                 ParDob.ParameterName = "@date_birth";
                 ParDob.SqlDbType = SqlDbType.DateTime;
                 ParDob.Value = client.Dob;

                 SqlCmd.Parameters.Add(ParDob);

                 SqlParameter ParDocumentType = new SqlParameter();
                 ParDocumentType.ParameterName = "@document_type";
                 ParDocumentType.SqlDbType = SqlDbType.VarChar;
                 ParDocumentType.Size = 50;
                 ParDocumentType.Value = client.TypeDocument;

                 SqlCmd.Parameters.Add(ParDocumentType);

                 SqlParameter ParDocumentNumber = new SqlParameter();
                 ParDocumentNumber.ParameterName = "@document_number";
                 ParDocumentNumber.SqlDbType = SqlDbType.VarChar;
                 ParDocumentNumber.Size = 50;
                 ParDocumentNumber.Value = client.DocumentNumber;

                 SqlCmd.Parameters.Add(ParDocumentNumber);

                 SqlParameter ParAddress = new SqlParameter();
                 ParAddress.ParameterName = "@address";
                 ParAddress.SqlDbType = SqlDbType.VarChar;
                 ParAddress.Size = 150;
                 ParAddress.Value = client.Address;

                 SqlCmd.Parameters.Add(ParAddress);

                 SqlParameter ParPhone = new SqlParameter();
                 ParPhone.ParameterName = "@phone";
                 ParPhone.SqlDbType = SqlDbType.VarChar;
                 ParPhone.Size = 20;
                 ParPhone.Value = client.Phone;

                 SqlCmd.Parameters.Add(ParPhone);


                 SqlParameter ParEmail = new SqlParameter();
                 ParEmail.ParameterName = "@email";
                 ParEmail.SqlDbType = SqlDbType.VarChar;
                 ParEmail.Size = 50;
                 ParEmail.Value = client.Email;

                 SqlCmd.Parameters.Add(ParEmail);

      


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

         public string Edit(DClient client)
         {
             string resp = "";
             SqlConnection SqlCon = new SqlConnection();
             try
             {
                 SqlCon.ConnectionString = Conection.cn;
                 SqlCon.Open();

                 SqlCommand SqlCmd = new SqlCommand();
                 SqlCmd.Connection = SqlCon;
                 SqlCmd.CommandText = "spedit_client";
                 SqlCmd.CommandType = CommandType.StoredProcedure;

                 //Initialize variables
                 SqlParameter ParId = new SqlParameter();
                 ParId.ParameterName = "@id";
                 ParId.SqlDbType = SqlDbType.Int;
                 ParId.Value = client.Id;
                 SqlCmd.Parameters.Add(ParId);

                 SqlParameter ParName = new SqlParameter();
                 ParName.ParameterName = "@name";
                 ParName.SqlDbType = SqlDbType.VarChar;
                 ParName.Size = 50;
                 ParName.Value = client.Name;

                 SqlCmd.Parameters.Add(ParName);

                 SqlParameter ParSurname = new SqlParameter();
                 ParSurname.ParameterName = "@surname";
                 ParSurname.SqlDbType = SqlDbType.VarChar;
                 ParSurname.Size = 50;
                 ParSurname.Value = client.Surname;

                 SqlCmd.Parameters.Add(ParSurname);



                 SqlParameter ParGender = new SqlParameter();
                 ParGender.ParameterName = "@gender";
                 ParGender.SqlDbType = SqlDbType.VarChar;
                 ParGender.Size = 1;
                 ParGender.Value = client.Gender;

                 SqlCmd.Parameters.Add(ParGender);

                 SqlParameter ParDob = new SqlParameter();
                 ParDob.ParameterName = "@date_birth";
                 ParDob.SqlDbType = SqlDbType.DateTime;
                 ParDob.Value = client.Dob;

                 SqlCmd.Parameters.Add(ParDob);

                 SqlParameter ParDocumentType = new SqlParameter();
                 ParDocumentType.ParameterName = "@document_type";
                 ParDocumentType.SqlDbType = SqlDbType.VarChar;
                 ParDocumentType.Size = 50;
                 ParDocumentType.Value = client.TypeDocument;

                 SqlCmd.Parameters.Add(ParDocumentType);

                 SqlParameter ParDocumentNumber = new SqlParameter();
                 ParDocumentNumber.ParameterName = "@document_number";
                 ParDocumentNumber.SqlDbType = SqlDbType.VarChar;
                 ParDocumentNumber.Size = 50;
                 ParDocumentNumber.Value = client.DocumentNumber;

                 SqlCmd.Parameters.Add(ParDocumentNumber);

                 SqlParameter ParAddress = new SqlParameter();
                 ParAddress.ParameterName = "@address";
                 ParAddress.SqlDbType = SqlDbType.VarChar;
                 ParAddress.Size = 150;
                 ParAddress.Value = client.Address;

                 SqlCmd.Parameters.Add(ParAddress);

                 SqlParameter ParPhone = new SqlParameter();
                 ParPhone.ParameterName = "@phone";
                 ParPhone.SqlDbType = SqlDbType.VarChar;
                 ParPhone.Size = 20;
                 ParPhone.Value = client.Phone;

                 SqlCmd.Parameters.Add(ParPhone);


                 SqlParameter ParEmail = new SqlParameter();
                 ParEmail.ParameterName = "@email";
                 ParEmail.SqlDbType = SqlDbType.VarChar;
                 ParEmail.Size = 50;
                 ParEmail.Value = client.Email;

                 SqlCmd.Parameters.Add(ParEmail);




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

         public string Delete(DClient client)
         {
             string resp = "";
             SqlConnection SqlCon = new SqlConnection();
             try
             {
                 SqlCon.ConnectionString = Conection.cn;
                 SqlCon.Open();

                 SqlCommand SqlCmd = new SqlCommand();

                 SqlCmd.Connection = SqlCon;
                 SqlCmd.CommandText = "spdelete_client";
                 SqlCmd.CommandType = CommandType.StoredProcedure;

                 //Initialize variables
                 SqlParameter ParId = new SqlParameter();
                 ParId.ParameterName = "@id"; //same name as in the procedure
                 ParId.SqlDbType = SqlDbType.Int;
                 ParId.Value = client.Id;

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
             DataTable DtResult = new DataTable("client"); // Table on sql
             SqlConnection SqlCon = new SqlConnection();
             try
             {
                 SqlCon.ConnectionString = Conection.cn;
                 SqlCommand SqlCmd = new SqlCommand();
                 SqlCmd.Connection = SqlCon;
                 SqlCmd.CommandText = "spshow_client";
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


         public DataTable SearchName(DClient client)
         {
             DataTable DtResult = new DataTable("client");
             SqlConnection SqlCon = new SqlConnection();
             try
             {
                 SqlCon.ConnectionString = Conection.cn;
                 SqlCommand SqlCmd = new SqlCommand();
                 SqlCmd.Connection = SqlCon;
                 SqlCmd.CommandText = "spsearch_client_name";
                 SqlCmd.CommandType = CommandType.StoredProcedure;


                 SqlParameter ParSearch = new SqlParameter();
                 ParSearch.ParameterName = "@texsearch";
                 ParSearch.SqlDbType = SqlDbType.VarChar;
                 ParSearch.Size = 50;
                 ParSearch.Value = client.SearchText;
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
         public DataTable SearchDocument(DClient client)
         {
             DataTable DtResult = new DataTable("client");
             SqlConnection SqlCon = new SqlConnection();
             try
             {
                 SqlCon.ConnectionString = Conection.cn;
                 SqlCommand SqlCmd = new SqlCommand();
                 SqlCmd.Connection = SqlCon;
                 SqlCmd.CommandText = "spsearch_client_document";
                 SqlCmd.CommandType = CommandType.StoredProcedure;


                 SqlParameter ParSearch = new SqlParameter();
                 ParSearch.ParameterName = "@texsearch";
                 ParSearch.SqlDbType = SqlDbType.VarChar;
                 ParSearch.Size = 50;
                 ParSearch.Value = client.SearchText;
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
