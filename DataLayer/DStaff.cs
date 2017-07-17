using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace DataLayer
{
    public class DStaff
    {
        private int _Id;
                
        private string _Name;

        private string _Surname;

        private string _Gender;
               
        private DateTime _Dob;
                
        private string _Document_Number;

        private string _Address;
                
        private string _Phone;
               
        private string _Email;
              
        private string _Access;
              
        private string _Username;
                
        private string _Password;

        
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
        public string Access
        {
            get { return _Access; }
            set { _Access = value; }
        }
        public string Username
        {
            get { return _Username; }
            set { _Username = value; }
        }
        public string Password
        {
            get { return _Password; }
            set { _Password = value; }
        }
        public string SearchText
        {
            get { return _SearchText; }
            set { _SearchText = value; }
        }

         public DStaff()
        {

        }

         public DStaff(int id, string name, string surname, string gender, DateTime dob, 
             string document_number, string address, string phone, string email, string access,
             string username, string password, string textsearch)
        {
            this.Id = id;
            this.Name = name;
            this.Surname = surname;
            this.Gender = gender;
            this.Dob = dob;
            this.Document_Number = document_number;
            this.Address = address;
            this.Phone = phone;
            this.Access = access;
            this.Email = email;
            this.Username = username;
            this.Password = password;
            this.SearchText = textsearch;

        }

         public string Insert(DStaff staff)
         {
             string resp = "";
             SqlConnection SqlCon = new SqlConnection();
             try
             {
                 SqlCon.ConnectionString = Conection.cn;
                 SqlCon.Open();

                 SqlCommand SqlCmd = new SqlCommand();

                 SqlCmd.Connection = SqlCon;
                 SqlCmd.CommandText = "spinsert_staff";
                 SqlCmd.CommandType = CommandType.StoredProcedure;

                 //Initialize variables
                 SqlParameter ParId = new SqlParameter();
                 ParId.ParameterName = "@id";
                 ParId.SqlDbType = SqlDbType.Int;
                 ParId.Direction = ParameterDirection.Output;

                 SqlCmd.Parameters.Add(ParId);

                 SqlParameter ParName= new SqlParameter();
                 ParName.ParameterName = "@name";
                 ParName.SqlDbType = SqlDbType.VarChar;
                 ParName.Size = 20;
                 ParName.Value = staff.Name;

                 SqlCmd.Parameters.Add(ParName);

                 SqlParameter ParSurname = new SqlParameter();
                 ParSurname.ParameterName = "@surname";
                 ParSurname.SqlDbType = SqlDbType.VarChar;
                 ParSurname.Size = 40;
                 ParSurname.Value = staff.Surname;

                 SqlCmd.Parameters.Add(ParSurname);

                

                 SqlParameter ParGender = new SqlParameter();
                 ParGender.ParameterName = "@genre";
                 ParGender.SqlDbType = SqlDbType.VarChar;
                 ParGender.Size = 1;
                 ParGender.Value = staff.Gender;

                 SqlCmd.Parameters.Add(ParGender);

                 SqlParameter ParDob = new SqlParameter();
                 ParDob.ParameterName = "@dob";
                 ParDob.SqlDbType = SqlDbType.DateTime;
                 //ParDocumentType.Size = 50;
                 ParDob.Value = staff.Dob;

                 SqlCmd.Parameters.Add(ParDob);

                 SqlParameter ParDocumentNumber = new SqlParameter();
                 ParDocumentNumber.ParameterName = "@document_number";
                 ParDocumentNumber.SqlDbType = SqlDbType.VarChar;
                 ParDocumentNumber.Size = 8;
                 ParDocumentNumber.Value = staff.Document_Number;

                 SqlCmd.Parameters.Add(ParDocumentNumber);

                 SqlParameter ParAddress = new SqlParameter();
                 ParAddress.ParameterName = "@address";
                 ParAddress.SqlDbType = SqlDbType.VarChar;
                 ParAddress.Size = 100;
                 ParAddress.Value = staff.Address;

                 SqlCmd.Parameters.Add(ParAddress);

                 SqlParameter ParPhone = new SqlParameter();
                 ParPhone.ParameterName = "@phone";
                 ParPhone.SqlDbType = SqlDbType.VarChar;
                 ParPhone.Size = 10;
                 ParPhone.Value = staff.Phone;

                 SqlCmd.Parameters.Add(ParPhone);


                 SqlParameter ParEmail = new SqlParameter();
                 ParEmail.ParameterName = "@email";
                 ParEmail.SqlDbType = SqlDbType.VarChar;
                 ParEmail.Size = 50;
                 ParEmail.Value = staff.Email;

                 SqlCmd.Parameters.Add(ParEmail);

                 SqlParameter ParUsername = new SqlParameter();
                 ParUsername.ParameterName = "@username";
                 ParUsername.SqlDbType = SqlDbType.VarChar;
                 ParUsername.Size = 20;
                 ParUsername.Value = staff.Username;

                 SqlCmd.Parameters.Add(ParUsername);

                 SqlParameter ParPassword = new SqlParameter();
                 ParPassword.ParameterName = "@password";
                 ParPassword.SqlDbType = SqlDbType.VarChar;
                 ParPassword.Size = 20;
                 ParPassword.Value = staff.Password;

                 SqlCmd.Parameters.Add(ParPassword);

                 SqlParameter ParAccess = new SqlParameter();
                 ParAccess.ParameterName = "@access";
                 ParAccess.SqlDbType = SqlDbType.VarChar;
                 ParAccess.Size = 20;
                 ParAccess.Value = staff.Access;

                 SqlCmd.Parameters.Add(ParAccess);


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

         public string Edit(DStaff staff)
         {
             string resp = "";
             SqlConnection SqlCon = new SqlConnection();
             try
             {
                 SqlCon.ConnectionString = Conection.cn;
                 SqlCon.Open();

                 SqlCommand SqlCmd = new SqlCommand();

                 SqlCmd.Connection = SqlCon;
                 SqlCmd.CommandText = "spedit_staff";
                 SqlCmd.CommandType = CommandType.StoredProcedure;

                 //Initialize variables
                 SqlParameter ParId = new SqlParameter();
                 ParId.ParameterName = "@id";
                 ParId.SqlDbType = SqlDbType.Int;
                 ParId.Value = staff.Id;
                 //ParId.Direction = ParameterDirection.Output;

                 SqlCmd.Parameters.Add(ParId);

                 SqlParameter ParName = new SqlParameter();
                 ParName.ParameterName = "@name";
                 ParName.SqlDbType = SqlDbType.VarChar;
                 ParName.Size = 20;
                 ParName.Value = staff.Name;

                 SqlCmd.Parameters.Add(ParName);

                 SqlParameter ParSurname = new SqlParameter();
                 ParSurname.ParameterName = "@surname";
                 ParSurname.SqlDbType = SqlDbType.VarChar;
                 ParSurname.Size = 40;
                 ParSurname.Value = staff.Surname;

                 SqlCmd.Parameters.Add(ParSurname);



                 SqlParameter ParGender = new SqlParameter();
                 ParGender.ParameterName = "@gender";
                 ParGender.SqlDbType = SqlDbType.VarChar;
                 ParGender.Size = 1;
                 ParGender.Value = staff.Gender;

                 SqlCmd.Parameters.Add(ParGender);

                 SqlParameter ParDob = new SqlParameter();
                 ParDob.ParameterName = "@dob";
                 ParDob.SqlDbType = SqlDbType.Date;
                 ParDob.Value = staff.Dob;

                 SqlCmd.Parameters.Add(ParDob);

                 SqlParameter ParDocumentNumber = new SqlParameter();
                 ParDocumentNumber.ParameterName = "@document_number";
                 ParDocumentNumber.SqlDbType = SqlDbType.VarChar;
                 ParDocumentNumber.Size = 8;
                 ParDocumentNumber.Value = staff.Document_Number;

                 SqlCmd.Parameters.Add(ParDocumentNumber);

                 SqlParameter ParAddress = new SqlParameter();
                 ParAddress.ParameterName = "@address";
                 ParAddress.SqlDbType = SqlDbType.VarChar;
                 ParAddress.Size = 100;
                 ParAddress.Value = staff.Address;

                 SqlCmd.Parameters.Add(ParAddress);

                 SqlParameter ParPhone = new SqlParameter();
                 ParPhone.ParameterName = "@phone";
                 ParPhone.SqlDbType = SqlDbType.VarChar;
                 ParPhone.Size = 10;
                 ParPhone.Value = staff.Phone;

                 SqlCmd.Parameters.Add(ParPhone);


                 SqlParameter ParEmail = new SqlParameter();
                 ParEmail.ParameterName = "@email";
                 ParEmail.SqlDbType = SqlDbType.VarChar;
                 ParEmail.Size = 50;
                 ParEmail.Value = staff.Email;

                 SqlCmd.Parameters.Add(ParEmail);

                 SqlParameter ParAccess = new SqlParameter();
                 ParAccess.ParameterName = "@access";
                 ParAccess.SqlDbType = SqlDbType.VarChar;
                 ParAccess.Size = 20;
                 ParAccess.Value = staff.Access;

                 SqlCmd.Parameters.Add(ParAccess);




                 SqlParameter ParUsername = new SqlParameter();
                 ParUsername.ParameterName = "@username";
                 ParUsername.SqlDbType = SqlDbType.VarChar;
                 ParUsername.Size = 20;
                 ParUsername.Value = staff.Username;

                 SqlCmd.Parameters.Add(ParUsername);

                 SqlParameter ParPassword = new SqlParameter();
                 ParPassword.ParameterName = "@password";
                 ParPassword.SqlDbType = SqlDbType.VarChar;
                 ParPassword.Size = 20;
                 ParPassword.Value = staff.Password;

                 SqlCmd.Parameters.Add(ParPassword);


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


         public string Delete(DStaff staff)
         {
             string resp = "";
             SqlConnection SqlCon = new SqlConnection();
             try
             {
                 SqlCon.ConnectionString = Conection.cn;
                 SqlCon.Open();

                 SqlCommand SqlCmd = new SqlCommand();

                 SqlCmd.Connection = SqlCon;
                 SqlCmd.CommandText = "spdelete_staff";
                 SqlCmd.CommandType = CommandType.StoredProcedure;

                 //Initialize variables
                 SqlParameter ParId = new SqlParameter();
                 ParId.ParameterName = "@id"; //same name as in the procedure
                 ParId.SqlDbType = SqlDbType.Int;
                 ParId.Value = staff.Id;

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

         public DataTable ShowName(DStaff staff)
         {
             DataTable DtResult = new DataTable("staff");
             SqlConnection SqlCon = new SqlConnection();
             try
             {
                 SqlCon.ConnectionString = Conection.cn;
                 SqlCommand SqlCmd = new SqlCommand();
                 SqlCmd.Connection = SqlCon;
                 SqlCmd.CommandText = "spsearch_staff_name";
                 SqlCmd.CommandType = CommandType.StoredProcedure;


                 SqlParameter ParSearch = new SqlParameter();
                 ParSearch.ParameterName = "@texsearch";
                 ParSearch.SqlDbType = SqlDbType.VarChar;
                 ParSearch.Size = 50;
                 ParSearch.Value = staff.SearchText;
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


         public DataTable SearchDocument(DStaff staff)
         {
             DataTable DtResult = new DataTable("staff");
             SqlConnection SqlCon = new SqlConnection();
             try
             {
                 SqlCon.ConnectionString = Conection.cn;
                 SqlCommand SqlCmd = new SqlCommand();
                 SqlCmd.Connection = SqlCon;
                 SqlCmd.CommandText = "spsearch_staff_document";
                 SqlCmd.CommandType = CommandType.StoredProcedure;


                 SqlParameter ParSearch = new SqlParameter();
                 ParSearch.ParameterName = "@texsearch";
                 ParSearch.SqlDbType = SqlDbType.VarChar;
                 ParSearch.Size = 50;
                 ParSearch.Value = staff.SearchText;
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

         public DataTable ShowValues()
         {
             DataTable DtResult = new DataTable("staff"); // Table on sql
             SqlConnection SqlCon = new SqlConnection();
             try
             {
                 SqlCon.ConnectionString = Conection.cn;
                 SqlCommand SqlCmd = new SqlCommand();
                 SqlCmd.Connection = SqlCon;
                 SqlCmd.CommandText = "spshow_staff";
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


         public DataTable Login(DStaff staff)
         {
             DataTable DtResult = new DataTable("staff");
             SqlConnection SqlCon = new SqlConnection();
             try
             {
                 SqlCon.ConnectionString = Conection.cn;
                 SqlCommand SqlCmd = new SqlCommand();
                 SqlCmd.Connection = SqlCon;
                 SqlCmd.CommandText = "splogin";
                 SqlCmd.CommandType = CommandType.StoredProcedure;


                 SqlParameter ParUsername = new SqlParameter();
                 ParUsername.ParameterName = "@username";
                 ParUsername.SqlDbType = SqlDbType.VarChar;
                 ParUsername.Size = 20;
                 ParUsername.Value = staff.Username;
                 SqlCmd.Parameters.Add(ParUsername);


                 SqlParameter ParPassword = new SqlParameter();
                 ParPassword.ParameterName = "@password";
                 ParPassword.SqlDbType = SqlDbType.VarChar;
                 ParPassword.Size = 20;
                 ParPassword.Value = staff.Password;

                 SqlCmd.Parameters.Add(ParPassword);


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
