using Newtonsoft.Json;
using LS.DAL.Interface;
using LS.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


namespace LS.DAL.Library
{
    public class  SignInDataAccess : DataAccessBase, ISignInDataAccess
    {
        public UserLogin InitiateSignInProcess(UserLogin user)
        {
            try
            {
                Log.Info("Started call to InitiateSignInProcess");
                Log.Info("parameter values" + JsonConvert.SerializeObject(user));
                Command.CommandText = "SP_LOGIN_MANAGER";
                Command.CommandType = CommandType.StoredProcedure;

                Command.Parameters.Clear();
                Command.Parameters.AddWithValue("@USERNAME", user.EmailAddress);
                Command.Parameters.AddWithValue("@PASSWORD", user.Password);
                Connection.Open();

                SqlDataAdapter dataAdapter = new SqlDataAdapter(Command);
                DataSet ds = new DataSet();
                dataAdapter.Fill(ds);
                UserLogin result = new UserLogin();
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        var resultRow = ds.Tables[0].Rows[0];
                        result = new UserLogin
                        {
                            Id = Convert.ToInt32(resultRow["Id"]),
                            FirstName = resultRow["FirstName"] != DBNull.Value ? resultRow["FirstName"].ToString() : null,
                            LastName = resultRow["LastName"] != DBNull.Value ? resultRow["LastName"].ToString() : null,
                            EmailAddress = resultRow["EmailAddress"] != DBNull.Value ? resultRow["EmailAddress"].ToString() : null,
                            PhoneNumber = resultRow["PhoneNumber"] != DBNull.Value ? resultRow["PhoneNumber"].ToString() : null,
                            Password = resultRow["Password"] != DBNull.Value ? resultRow["Password"].ToString() : null
                        };                        
                    }
                }
                Log.Info("End call to InitiateSignInProcess");
                return result;

            }
            catch (Exception ex)
            {
                throw;
            }

            finally
            {
                Connection.Close();
            }
        }
    }
}
