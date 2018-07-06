﻿using Newtonsoft.Json;
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

        public ComplianceDocumentsResponse InsertUpdateComplianceDocuments(ComplianceDocuments complianceDocuments, string operation)
        {
            try
            {
                Log.Info("Started call to InsertorUpdateComplianceDocuments");
                Log.Info("parameter values" + JsonConvert.SerializeObject(new { complianceDocuments = complianceDocuments, operation = operation }));
                Command.CommandText = "SP_COMPLIANCE_DOCUMENT_MANAGER";
                Command.CommandType = CommandType.StoredProcedure;
                Command.Parameters.Clear();

                Command.Parameters.AddWithValue("@COMPLIANCE_DOCUMENT_XML", GetXMLFromObject(complianceDocuments));
                if (!string.IsNullOrEmpty(operation))
                {
                    Command.Parameters.AddWithValue("@OPERATION", operation);
                }
                if (complianceDocuments.AddedBy.HasValue)
                {
                    Command.Parameters.AddWithValue("@USER_ID", complianceDocuments.AddedBy.Value);
                }
                if (!complianceDocuments.AddedBy.HasValue && complianceDocuments.ModifiedBy.HasValue)
                {
                    Command.Parameters.AddWithValue("@USER_ID", complianceDocuments.ModifiedBy.Value);
                }
                Connection.Open();
                SqlDataReader reader = Command.ExecuteReader();

                ComplianceDocumentsResponse result = new ComplianceDocumentsResponse();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result = new ComplianceDocumentsResponse
                        {
                            Message = reader["ReturnMessage"] != DBNull.Value ? reader["ReturnMessage"].ToString() : null,
                            IsSuccess = Convert.ToBoolean(reader["Result"].ToString())
                        };
                    }
                }
                Log.Info("End call to InsertorUpdateComplianceDocuments");

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

        public ComplianceDocumentsResponse GetComplianceDocumentsList(int companyId, int fyayId, 
            int? complianceId, int? complianceDocumentId)
        {
            try
            {
                Log.Info("Started call to GetComplianceDocumentsList");
                Log.Info("parameter values" + JsonConvert.SerializeObject(new { companyId = companyId, fyayId = fyayId,
                    complianceId = complianceId , complianceDocumentId = complianceDocumentId }));
                Command.CommandText = "SP_GET_COMPLIANCE_DOCUMENT_LIST";
                Command.CommandType = CommandType.StoredProcedure;
                Command.Parameters.AddWithValue("@COMPANY_ID", companyId);
                Command.Parameters.AddWithValue("@FYAY_ID", fyayId);
                if (complianceId.HasValue)
                {
                    Command.Parameters.AddWithValue("@COMPLIANCE_ID", complianceId);
                }
                if (complianceDocumentId.HasValue)
                {
                    Command.Parameters.AddWithValue("@COMPLIANCE_DOCUMENT_ID", complianceDocumentId);
                }
                Connection.Open();

                SqlDataReader reader = Command.ExecuteReader();
                ComplianceDocumentsResponse result = new ComplianceDocumentsResponse();
                result.ComplianceDocumentsList = new List<ComplianceDocumentsDisplay>();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result.ComplianceDocumentsList.Add(new ComplianceDocumentsDisplay
                        {
                            ComplianceDescription = reader["ComplianceDescription"] != DBNull.Value ? reader["ComplianceDescription"].ToString() : null,
                            FileName = reader["FileName"] != DBNull.Value ? reader["FileName"].ToString() : null,
                            FilePath = reader["FilePath"] != DBNull.Value ? reader["FilePath"].ToString() : null,
                            CompanyName = reader["CompanyName"] != DBNull.Value ? reader["CompanyName"].ToString() : null,
                            FinancialYear = reader["FinancialYear"] != DBNull.Value ? reader["FinancialYear"].ToString() : null,
                            AssessmentYear = reader["AssessmentYear"] != DBNull.Value ? reader["AssessmentYear"].ToString() : null,
                            Active = Convert.ToBoolean(reader["Active"].ToString()),
                            Id = Convert.ToInt32(reader["Id"].ToString()),
                            ComplianceId = Convert.ToInt32(reader["ComplianceId"].ToString()),
                            CompanyId = Convert.ToInt32(reader["CompanyId"].ToString()),
                            FYAYId = Convert.ToInt32(reader["FYAYId"].ToString())
                        });
                    }
                }
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
