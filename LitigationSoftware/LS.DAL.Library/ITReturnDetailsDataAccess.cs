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
                            IsSuccess = Convert.ToBoolean(reader["Result"].ToString()),
                            ITReturnDetailsObject = new ITReturnDetails
                            {
                                Id = Convert.ToInt32(reader["ITReturDetailsId"].ToString())
                            }
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
                            HousePropIncome = reader["HousePropIncome"] != DBNull.Value ? Convert.ToDecimal(reader["HousePropIncome"].ToString()) : (decimal?)null,
                            IncomefromBusinessProf = Convert.ToBoolean(reader["IncomefromBusinessProf"]),
                            RevisedReturnFile = Convert.ToBoolean(reader["RevisedReturnFile"]),
                            IsReturn = Convert.ToBoolean(reader["IsReturn"]),
                            IncomefromCapGainsNonSTT = reader["IncomefromCapGainsNonSTT"] != DBNull.Value ? Convert.ToDecimal(reader["IncomefromCapGainsNonSTT"].ToString()) : (decimal?)null,
                            IncomefromCapGainsSTT = reader["IncomefromCapGainsSTT"] != DBNull.Value ? Convert.ToDecimal(reader["IncomefromCapGainsSTT"].ToString()) : (decimal?)null,
                            UnabsorbedDepreciation = reader["UnabsorbedDepreciation"] != DBNull.Value ? Convert.ToDecimal(reader["UnabsorbedDepreciation"].ToString()) : (decimal?)null,
                            Broughtforwardlosses = reader["Broughtforwardlosses"] != DBNull.Value ? Convert.ToDecimal(reader["Broughtforwardlosses"].ToString()) : (decimal?)null,
                            IncomeFromOtherSources = reader["IncomeFromOtherSources"] != DBNull.Value ? Convert.ToDecimal(reader["IncomeFromOtherSources"].ToString()) : (decimal?)null,
                            DeductChapterVIA = reader["DeductChapterVIA"] != DBNull.Value ? Convert.ToDecimal(reader["DeductChapterVIA"].ToString()) : (decimal?)null,
                            ProfitUS115JB = reader["ProfitUS115JB"] != DBNull.Value ? Convert.ToDecimal(reader["ProfitUS115JB"].ToString()) : (decimal?)null,
                            AdvanceTax1installment = reader["AdvanceTax1installment"] != DBNull.Value ? Convert.ToDecimal(reader["AdvanceTax1installment"].ToString()) : (decimal?)null,
                            AdvanceTax2installment = reader["AdvanceTax2installment"] != DBNull.Value ? Convert.ToDecimal(reader["AdvanceTax2installment"].ToString()) : (decimal?)null,
                            AdvanceTax3installment = reader["AdvanceTax3installment"] != DBNull.Value ? Convert.ToDecimal(reader["AdvanceTax3installment"].ToString()) : (decimal?)null,
                            AdvanceTax4installment = reader["AdvanceTax4installment"] != DBNull.Value ? Convert.ToDecimal(reader["AdvanceTax4installment"].ToString()) : (decimal?)null,
                            TDS = reader["TDS"] != DBNull.Value ? Convert.ToDecimal(reader["TDS"].ToString()) : (decimal?)null,
                            TCSPaidbyCompany = reader["TCSPaidbyCompany"] != DBNull.Value ? Convert.ToDecimal(reader["TCSPaidbyCompany"].ToString()) : (decimal?)null,
                            SelfAssessmentTax = reader["SelfassessmentTax"] != DBNull.Value ? Convert.ToDecimal(reader["SelfassessmentTax"]) : (decimal?)null,
                            MATCredit = reader["MATCredit"] != DBNull.Value ? Convert.ToDecimal(reader["MATCredit"].ToString()) : (decimal?)null,
                            InterestUS234A = reader["InterestUS234A"] != DBNull.Value ? Convert.ToDecimal(reader["InterestUS234A"].ToString()) : (decimal?)null,
                            InterestUS234B = reader["InterestUS234B"] != DBNull.Value ? Convert.ToDecimal(reader["InterestUS234B"].ToString()) : (decimal?)null,
                            InterestUS234C = reader["InterestUS234C"] != DBNull.Value ? Convert.ToDecimal(reader["InterestUS234C"].ToString()) : (decimal?)null,
                            InterestUS244A = reader["InterestUS244A"] != DBNull.Value ? Convert.ToDecimal(reader["InterestUS244A"].ToString()) : (decimal?)null,
                            RefundReceived = reader["RefundReceived"] != DBNull.Value ? Convert.ToDecimal(reader["RefundReceived"].ToString()) : (decimal?)null
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
