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

        public ITReturnComplexAPIModelResponse InsertorUpdateITReturnDetails(ITReturnComplexAPIModel itReturnDetails)
        {
            try
            {
                Log.Info("Started call to InsertorUpdateITReturnDetails");
                Log.Info("parameter values" + JsonConvert.SerializeObject(itReturnDetails));
                Command.CommandText = "SP_ITRETURNDETAILS_MANAGER";
                Command.CommandType = CommandType.StoredProcedure;
                Command.Parameters.Clear();
                
                Command.Parameters.AddWithValue("@ITRETURNDETAILS_XML", GetXMLFromObject(itReturnDetails.ITReturnDetailsObject));
                Command.Parameters.AddWithValue("@EXTENSIONDETAILS_XML", GetXMLFromObject(itReturnDetails.ExtensionList));
                if (itReturnDetails.ITReturnDetailsObject.AddedBy.HasValue)
                {
                    Command.Parameters.AddWithValue("@USER_ID", itReturnDetails.ITReturnDetailsObject.AddedBy.Value);
                }
                if (itReturnDetails.ITReturnDetailsObject.ModifiedBy.HasValue)
                {
                    Command.Parameters.AddWithValue("@USER_ID", itReturnDetails.ITReturnDetailsObject.ModifiedBy.Value);
                }
                Connection.Open();
                SqlDataReader reader = Command.ExecuteReader();

                ITReturnComplexAPIModelResponse result = new ITReturnComplexAPIModelResponse();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result = new ITReturnComplexAPIModelResponse
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
                else if (complianceDocuments.ModifiedBy.HasValue)
                {
                    Command.Parameters.AddWithValue("@USER_ID", complianceDocuments.ModifiedBy.Value);
                }
                else if (complianceDocuments.DeletedBy.HasValue)
                {
                    Command.Parameters.AddWithValue("@USER_ID", complianceDocuments.DeletedBy.Value);
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


        public ITReturnDetailsListResponse GetExistingITReturnDetailsList(int companyId, int fyayId,int? itsectionid,int? itreturnid)
        {
            try
            {
                Log.Info("Started call to GetExistingITReturnDetailsList");
                Log.Info("parameter values" + JsonConvert.SerializeObject(new
                {
                    companyId = companyId,
                    fyayId = fyayId,
                    itsectionid = itsectionid,
                    itreturnid = itreturnid
                }));
                Command.CommandText = "SP_GET_ITReturnDetails";
                Command.CommandType = CommandType.StoredProcedure;
                Command.Parameters.AddWithValue("@COMPANY_ID", companyId);
                Command.Parameters.AddWithValue("@FYAYID", fyayId);
                if (itsectionid.HasValue && itsectionid > 0)
                {
                    Command.Parameters.AddWithValue("@ITSectionID", itsectionid);
                }
                if(itreturnid.HasValue && itreturnid >0)
                {
                    Command.Parameters.AddWithValue("@ITReturnID", itreturnid);
                }

                Connection.Open();

                SqlDataReader reader = Command.ExecuteReader();
                ITReturnDetailsListResponse result = new ITReturnDetailsListResponse();
                result.ITReturnDetailsListObject = new List<ITReturnDetails>();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result.ITReturnDetailsListObject.Add(new ITReturnDetails
                        {
                            Id = int.Parse(reader["ITReturnDetailsId"].ToString()),
                            ITSectionID = reader["ITSectionID"] != DBNull.Value ? int.Parse(reader["ITSectionID"].ToString()) : 0,
                            ITSectionDescription = reader["Description"] != DBNull.Value ? reader["Description"].ToString() : null,
                            ITReturnFillingDate = reader["ITReturnFillingDate"] != DBNull.Value ? Convert.ToDateTime(reader["ITReturnFillingDate"].ToString()) : (DateTime?)null,
                            ITReturnDueDate = reader["ITReturnDueDate"] != DBNull.Value ? Convert.ToDateTime(reader["ITReturnDueDate"].ToString()) : (DateTime?)null,
                            FYAYID = Convert.ToInt32(reader["FYAYId"].ToString()),
                            CompanyID = Convert.ToInt32(reader["CompanyID"].ToString()),
                            CompanyName = reader["CompanyName"] != DBNull.Value ? reader["CompanyName"].ToString() : null,
                            HousePropIncome = reader["HousePropIncome"] != DBNull.Value ? Convert.ToInt32(reader["HousePropIncome"].ToString()) : (int?)null,
                            IncomefromBusinessProf = Convert.ToBoolean(reader["IncomefromBusinessProf"]),
                            RevisedReturnFile = Convert.ToBoolean(reader["RevisedReturnFile"]),
                            IsReturn = Convert.ToBoolean(reader["IsReturn"]),
                            IncomefromCapGainsNonSTT = reader["IncomefromCapGainsNonSTT"] != DBNull.Value ? Convert.ToInt32(reader["IncomefromCapGainsNonSTT"].ToString()) : (int?)null,
                            IncomefromCapGainsSTT = reader["IncomefromCapGainsSTT"] != DBNull.Value ? Convert.ToInt32(reader["IncomefromCapGainsSTT"].ToString()) : (int?)null,
                            UnabsorbedDepreciation = reader["UnabsorbedDepreciation"] != DBNull.Value ? Convert.ToInt32(reader["UnabsorbedDepreciation"].ToString()) : (int?)null,
                            Broughtforwardlosses = reader["Broughtforwardlosses"] != DBNull.Value ? Convert.ToInt32(reader["Broughtforwardlosses"].ToString()) : (int?)null,
                            IncomeFromOtherSources = reader["IncomeFromOtherSources"] != DBNull.Value ? Convert.ToInt32(reader["IncomeFromOtherSources"].ToString()) : (int?)null,
                            DeductChapterVIA = reader["DeductChapterVIA"] != DBNull.Value ? Convert.ToInt32(reader["DeductChapterVIA"].ToString()) : (int?)null,
                            ProfitUS115JB = reader["ProfitUS115JB"] != DBNull.Value ? Convert.ToInt32(reader["ProfitUS115JB"].ToString()) : (int?)null,
                            AdvanceTax1installment = reader["AdvanceTax1installment"] != DBNull.Value ? Convert.ToInt32(reader["AdvanceTax1installment"].ToString()) : (int?)null,
                            AdvanceTax2installment = reader["AdvanceTax2installment"] != DBNull.Value ? Convert.ToInt32(reader["AdvanceTax2installment"].ToString()) : (int?)null,
                            AdvanceTax3installment = reader["AdvanceTax3installment"] != DBNull.Value ? Convert.ToInt32(reader["AdvanceTax3installment"].ToString()) : (int?)null,
                            AdvanceTax4installment = reader["AdvanceTax4installment"] != DBNull.Value ? Convert.ToInt32(reader["AdvanceTax4installment"].ToString()) : (int?)null,
                            TDS = reader["TDS"] != DBNull.Value ? Convert.ToInt32(reader["TDS"].ToString()) : (int?)null,
                            TCSPaidbyCompany = reader["TCSPaidbyCompany"] != DBNull.Value ? Convert.ToInt32(reader["TCSPaidbyCompany"].ToString()) : (int?)null,
                            SelfAssessmentTax = reader["SelfassessmentTax"] != DBNull.Value ? Convert.ToInt32(reader["SelfassessmentTax"]) : (int?)null,
                            MATCredit = reader["MATCredit"] != DBNull.Value ? Convert.ToInt32(reader["MATCredit"].ToString()) : (int?)null,
                            InterestUS234A = reader["InterestUS234A"] != DBNull.Value ? Convert.ToInt32(reader["InterestUS234A"].ToString()) : (int?)null,
                            InterestUS234B = reader["InterestUS234B"] != DBNull.Value ? Convert.ToInt32(reader["InterestUS234B"].ToString()) : (int?)null,
                            InterestUS234C = reader["InterestUS234C"] != DBNull.Value ? Convert.ToInt32(reader["InterestUS234C"].ToString()) : (int?)null,
                            InterestUS244A = reader["InterestUS244A"] != DBNull.Value ? Convert.ToInt32(reader["InterestUS244A"].ToString()) : (int?)null,
                            RefundReceived = reader["RefundReceived"] != DBNull.Value ? Convert.ToInt32(reader["RefundReceived"].ToString()) : (int?)null
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

        public List<ITReturnDetailsExtension> GetExistingITReturnDetailsExtension(int? itreturnid)
        {
            try
            {
                Log.Info("Started call to GetExistingITReturnDetailsExtension");
                Log.Info("parameter values" + JsonConvert.SerializeObject(new
                {
                    itreturnid = itreturnid
                }));
                Command.CommandText = "SP_GET_ITRETURNDETAILSEXTENSION";
                Command.CommandType = CommandType.StoredProcedure;
                if (itreturnid.HasValue && itreturnid > 0)
                {
                    Command.Parameters.AddWithValue("@ITRETURNDETAILS_ID", itreturnid);
                }

                Connection.Open();

                SqlDataReader reader = Command.ExecuteReader();
                List<ITReturnDetailsExtension> itrde = new List<ITReturnDetailsExtension>();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        itrde.Add(new ITReturnDetailsExtension
                        {
                            Id = int.Parse(reader["Id"].ToString()),
                            ITReturnDetailsId = int.Parse(reader["ITReturnDetailsId"].ToString()),
                            ITSubHeadId = int.Parse(reader["ITSubHeadId"].ToString()),
                            ITSubHeadValue = decimal.Parse(reader["ITSubHeadValue"].ToString()),
                            Active = bool.Parse(reader["Active"].ToString()) 
                        });
                    }
                }
                return itrde;
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
