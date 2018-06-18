using Newtonsoft.Json;
using LS.DAL.Interface;
using LS.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace LS.DAL.Library
{
    public class ITReturnDetailsDataAccess : DataAccessBase, IITReturnDetailsDataAccess
    {
        #region Methods

        public ITReturnDetailsResponse InsertorUpdateITReturnDetails(ITReturnDetails itReturnDetails)
        {
            try
            {
                Log.Info("Started call to InsertorUpdateITReturnDetails");
                Log.Info("parameter values" + JsonConvert.SerializeObject(itReturnDetails));
                Command.CommandText = "SP_ITRETURNDETAILS_MANAGER";
                Command.CommandType = CommandType.StoredProcedure;
                Command.Parameters.Clear();
                
                Command.Parameters.AddWithValue("@ITRETURNDETAILS_XML", GetXMLFromObject(itReturnDetails));
                if (itReturnDetails.AddedBy.HasValue)
                {
                    Command.Parameters.AddWithValue("@USER_ID", itReturnDetails.AddedBy.Value);
                }
                if (itReturnDetails.ModifiedBy.HasValue)
                {
                    Command.Parameters.AddWithValue("@USER_ID", itReturnDetails.ModifiedBy.Value);
                }
                Connection.Open();
                SqlDataReader reader = Command.ExecuteReader();

                ITReturnDetailsResponse result = new ITReturnDetailsResponse();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result = new ITReturnDetailsResponse
                        {
                            Message = reader["ReturnMessage"] != DBNull.Value ? reader["ReturnMessage"].ToString() : null,
                            IsSuccess = Convert.ToBoolean(reader["Result"].ToString())
                        };
                    }
                }
                Log.Info("End call to InsertorUpdateITReturnDetails");

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

        #endregion
    }
}
