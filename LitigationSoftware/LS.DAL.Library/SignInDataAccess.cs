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
        public SignInResponse InitiateSignInProcess(UserLogin user)
        {
            try
            {
                Log.Info("Started call to InitiateSignInProcess");
                Log.Info("parameter values" + JsonConvert.SerializeObject(user));
                Command.CommandText = "SP_LOGIN_MANAGER";
                Command.CommandType = CommandType.StoredProcedure;

                Command.Parameters.Clear();
                Command.Parameters.AddWithValue("@USERNAME", user.Username);
                Command.Parameters.AddWithValue("@PASSWORD", user.Password);
                Command.Parameters.AddWithValue("@IP_ADDRESS", user.IPAddress);
                Connection.Open();

                SqlDataAdapter dataAdapter = new SqlDataAdapter(Command);
                DataSet ds = new DataSet();
                dataAdapter.Fill(ds);
                SignInResponse result = new SignInResponse();
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        var resultRow = ds.Tables[0].Rows[0];
                        result = new SignInResponse
                        {
                            IsPasswordVerified = Convert.ToBoolean(resultRow["IS_PASSWORD_VERIFIED"]),
                            SessionId = resultRow["SESSION_ID"] != DBNull.Value ? resultRow["SESSION_ID"].ToString() : null,
                            TwoFactorAuthDone = Convert.ToBoolean(resultRow["TWO_FACTOR_AUTH_DONE"]),
                            IsUserActive = Convert.ToBoolean(resultRow["IS_USER_ACTIVE"]),
                            TwoFactorAuthTimestamp = !string.IsNullOrEmpty(resultRow["TWO_FACTOR_AUTH_TS"].ToString())
                                                   ? Convert.ToDateTime(resultRow["TWO_FACTOR_AUTH_TS"].ToString())
                                                   : (DateTime?)null,
                            UserDeviceId = !string.IsNullOrEmpty(resultRow["USER_DEVICE_ID"].ToString()) ? Convert.ToInt32(resultRow["USER_DEVICE_ID"].ToString()) : (int?)null,
                            UserId = !string.IsNullOrEmpty(resultRow["USER_ID"].ToString()) ? Convert.ToInt32(resultRow["USER_ID"].ToString()) : (int?)null
                        };

                        if (ds.Tables.Count > 1
                            && ds.Tables[1].Rows.Count > 0)
                        {
                            result.UserRoles = new List<UserRole>();
                            foreach (DataRow drRole in ds.Tables[1].Rows)
                            {
                                result.UserRoles.Add(new UserRole
                                {
                                    UserId = result.UserId.Value,
                                    RoleId = Convert.ToInt32(drRole["RoleId"].ToString()),
                                    RoleName = drRole["RoleName"].ToString(),
                                    IsDefault = Convert.ToBoolean(drRole["IsDefault"])
                                });
                            }
                        }
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
