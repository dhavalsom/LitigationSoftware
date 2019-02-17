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

        public ITReturnComplexAPIModelResponse InsertorUpdateITReturnDetails
            (ITReturnComplexAPIModel itReturnDetails, string operation)
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
                if(!string.IsNullOrEmpty(operation))
                {
                    Command.Parameters.AddWithValue("@OPERATION", operation);
                }
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
                            ITSectionCategoryID = reader["SECTIONCATEGORYID"] != DBNull.Value ? int.Parse(reader["SECTIONCATEGORYID"].ToString()) : 0,
                            ITSectionCategoryDesc = reader["CategoryDesc"] != DBNull.Value ? reader["CategoryDesc"].ToString() : null,
                            ITSectionID = reader["ITSectionID"] != DBNull.Value ? int.Parse(reader["ITSectionID"].ToString()) : 0,
                            ITSectionDescription = reader["Description"] != DBNull.Value ? reader["Description"].ToString() : null,
                            ITReturnFillingDate = reader["ITReturnFillingDate"] != DBNull.Value ? Convert.ToDateTime(reader["ITReturnFillingDate"].ToString()) : (DateTime?)null,
                            ITReturnDueDate = reader["ITReturnDueDate"] != DBNull.Value ? Convert.ToDateTime(reader["ITReturnDueDate"].ToString()) : (DateTime?)null,
                            FYAYID = Convert.ToInt32(reader["FYAYId"].ToString()),
                            CompanyID = Convert.ToInt32(reader["CompanyID"].ToString()),
                            CompanyName = reader["CompanyName"] != DBNull.Value ? reader["CompanyName"].ToString() : null,
                            HousePropIncome = reader["HousePropIncome"] != DBNull.Value ? Convert.ToDecimal(reader["HousePropIncome"].ToString()) : (decimal?)null,
                            IncomefromBusinessProf = reader["IncomefromBusinessProf"] != DBNull.Value ? Convert.ToDecimal(reader["IncomefromBusinessProf"].ToString()) : (decimal?)null,
                            IncomefromCapGainsLTCG = reader["IncomefromCapGainsLTCG"] != DBNull.Value ? Convert.ToDecimal(reader["IncomefromCapGainsLTCG"].ToString()) : (decimal?)null,
                            IncomefromCapGainsSTCG = reader["IncomefromCapGainsSTCG"] != DBNull.Value ? Convert.ToDecimal(reader["IncomefromCapGainsSTCG"].ToString()) : (decimal?)null,
                            IncomefromSpeculativeBusiness = reader["IncomefromSpeculativeBusiness"] != DBNull.Value ? Convert.ToDecimal(reader["IncomefromSpeculativeBusiness"].ToString()) : (decimal?)null,
                            Broughtforwardlosses = reader["Broughtforwardlosses"] != DBNull.Value ? Convert.ToBoolean(reader["Broughtforwardlosses"].ToString()) : false,
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
                            RefundReceived = reader["RefundReceived"] != DBNull.Value ? Convert.ToDecimal(reader["RefundReceived"].ToString()) : (decimal?)null,
                            IncomefromSalary = reader["IncomefromSalary"] != DBNull.Value ? Convert.ToDecimal(reader["IncomefromSalary"].ToString()) : (decimal?)null,
                            TDS26AS = reader["TDS26AS"] != DBNull.Value ? Convert.ToDecimal(reader["TDS26AS"].ToString()) : (decimal?)null,
                            TDSasperBooks = reader["TDSasperBooks"] != DBNull.Value ? Convert.ToDecimal(reader["TDSasperBooks"].ToString()) : (decimal?)null,
                            IsReturn = reader["IsReturn"] != DBNull.Value ? Convert.ToBoolean(reader["IsReturn"].ToString()) : false,
                            TaxCollectedAtSource = reader["TaxCollectedAtSource"] != DBNull.Value ? Convert.ToDecimal(reader["TaxCollectedAtSource"].ToString()) : (decimal?)null,
                            ForeignTaxCredit = reader["ForeignTaxCredit"] != DBNull.Value ? Convert.ToDecimal(reader["ForeignTaxCredit"].ToString()) : (decimal?)null,
                            InterestUS234D = reader["InterestUS234D"] != DBNull.Value ? Convert.ToDecimal(reader["InterestUS234D"].ToString()) : (decimal?)null,
                            InterestUS220 = reader["InterestUS220"] != DBNull.Value ? Convert.ToDecimal(reader["InterestUS220"].ToString()) : (decimal?)null,
                            RefundAdjusted = reader["RefundAdjusted"] != DBNull.Value ? Convert.ToDecimal(reader["RefundAdjusted"].ToString()) : (decimal?)null,
                            RegularAssessment = reader["RegularAssessment"] != DBNull.Value ? Convert.ToDecimal(reader["RegularAssessment"].ToString()) : (decimal?)null,
                            RefundAlreadyReceived = reader["RefundAlreadyReceived"] != DBNull.Value ? Convert.ToDecimal(reader["RefundAlreadyReceived"].ToString()) : (decimal?)null,
                            SelfAssessmentTaxDate = reader["SelfAssessmentTaxDate"] != DBNull.Value ? Convert.ToDateTime(reader["SelfAssessmentTaxDate"].ToString()) : (DateTime?)null,
                            AdvanceTax1installmentDate = reader["AdvanceTax1installmentDate"] != DBNull.Value ? Convert.ToDateTime(reader["AdvanceTax1installmentDate"].ToString()) : (DateTime?)null,
                            AdvanceTax2installmentDate = reader["AdvanceTax2installmentDate"] != DBNull.Value ? Convert.ToDateTime(reader["AdvanceTax2installmentDate"].ToString()) : (DateTime?)null,
                            AdvanceTax3installmentDate = reader["AdvanceTax3installmentDate"] != DBNull.Value ? Convert.ToDateTime(reader["AdvanceTax3installmentDate"].ToString()) : (DateTime?)null,
                            AdvanceTax4installmentDate = reader["AdvanceTax4installmentDate"] != DBNull.Value ? Convert.ToDateTime(reader["AdvanceTax4installmentDate"].ToString()) : (DateTime?)null,
                            RefundAdjustedDate = reader["RefundAdjustedDate"] != DBNull.Value ? Convert.ToDateTime(reader["RefundAdjustedDate"].ToString()) : (DateTime?)null,
                            RegularAssessmentDate = reader["RegularAssessmentDate"] != DBNull.Value ? Convert.ToDateTime(reader["RegularAssessmentDate"].ToString()) : (DateTime?)null,
                            RefundAlreadyReceivedDate = reader["RefundAlreadyReceivedDate"] != DBNull.Value ? Convert.ToDateTime(reader["RefundAlreadyReceivedDate"].ToString()) : (DateTime?)null,
                            RITotalIncome = reader["RITotalIncome"] != DBNull.Value ? Convert.ToDecimal(reader["RITotalIncome"].ToString()) : 0,
                            RISurcharge = reader["RISurcharge"] != DBNull.Value ? Convert.ToDecimal(reader["RISurcharge"].ToString()) : 0,
                            RIEducationCess = reader["RIEducationCess"] != DBNull.Value ? Convert.ToDecimal(reader["RIEducationCess"].ToString()) : 0,
                            MATTotalIncome = reader["MATTotalIncome"] != DBNull.Value ? Convert.ToDecimal(reader["MATTotalIncome"].ToString()) : 0,
                            MATEducationCess = reader["MATEducationCess"] != DBNull.Value ? Convert.ToDecimal(reader["MATEducationCess"].ToString()) : 0,
                            MATSurcharge = reader["MATSurcharge"] != DBNull.Value ? Convert.ToDecimal(reader["MATSurcharge"].ToString()) : 0,
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
                            ITHeadId = int.Parse(reader["ITHeadId"].ToString()),
                            ITSubHeadId = int.Parse(reader["ITSubHeadId"].ToString()),
                            ITSubHeadValue = decimal.Parse(reader["ITSubHeadValue"].ToString()),
                            ITSubHeadDate = reader["ITSubHeadDate"] != DBNull.Value ? DateTime.Parse(reader["ITSubHeadDate"].ToString()) : (DateTime?)null,
                            IsAllowance = bool.Parse(reader["IsAllowance"].ToString()),
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

        public ITReturnDocumentsResponse InsertUpdateITReturnDocuments(ITReturnDocuments itReturnDocuments
            , string operation)
        {
            try
            {
                Log.Info("Started call to InsertUpdateITReturnDocuments");
                Log.Info("parameter values" + JsonConvert.SerializeObject(
                    new { itReturnDocuments = itReturnDocuments, operation = operation }));
                Command.CommandText = "SP_IT_RETURN_DOCUMENT_MANAGER";
                Command.CommandType = CommandType.StoredProcedure;
                Command.Parameters.Clear();

                Command.Parameters.AddWithValue("@IT_RETURN_DOCUMENT_XML", GetXMLFromObject(itReturnDocuments));
                if (!string.IsNullOrEmpty(operation))
                {
                    Command.Parameters.AddWithValue("@OPERATION", operation);
                }
                if (itReturnDocuments.AddedBy.HasValue)
                {
                    Command.Parameters.AddWithValue("@USER_ID", itReturnDocuments.AddedBy.Value);
                }
                else if (itReturnDocuments.ModifiedBy.HasValue)
                {
                    Command.Parameters.AddWithValue("@USER_ID", itReturnDocuments.ModifiedBy.Value);
                }
                else if (itReturnDocuments.DeletedBy.HasValue)
                {
                    Command.Parameters.AddWithValue("@USER_ID", itReturnDocuments.DeletedBy.Value);
                }
                Connection.Open();
                SqlDataReader reader = Command.ExecuteReader();

                ITReturnDocumentsResponse result = new ITReturnDocumentsResponse();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result = new ITReturnDocumentsResponse
                        {
                            Message = reader["ReturnMessage"] != DBNull.Value ? reader["ReturnMessage"].ToString() : null,
                            IsSuccess = Convert.ToBoolean(reader["Result"].ToString())
                        };
                    }
                }
                Log.Info("End call to InsertUpdateITReturnDocuments");

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

        public ITReturnDocumentsResponse GetITReturnDocumentsList(int? companyId,
            int? fyayId, int? itReturnDetailsId, int? itHeadId, int? itReturnDocumentId)
        {
            try
            {
                Log.Info("Started call to GetITReturnDocumentsList");
                Log.Info("parameter values" + JsonConvert.SerializeObject(new
                {
                    companyId = companyId,
                    fyayId = fyayId,
                    itReturnDetailsId = itReturnDetailsId,
                    itHeadId = itHeadId,
                    itReturnDocumentId = itReturnDocumentId,
                }));
                Command.CommandText = "SP_GET_IT_RETURN_DOCUMENT_LIST";
                Command.CommandType = CommandType.StoredProcedure;
                if (companyId.HasValue)
                {
                    Command.Parameters.AddWithValue("@COMPANY_ID", companyId);
                }
                if (fyayId.HasValue)
                {
                    Command.Parameters.AddWithValue("@FYAY_ID", fyayId);
                }
                if (itReturnDetailsId.HasValue)
                {
                    Command.Parameters.AddWithValue("@IT_RETURN_DETAILS_ID", itReturnDetailsId);
                }
                if (itHeadId.HasValue)
                {
                    Command.Parameters.AddWithValue("@IT_HEAD_ID", itHeadId);
                }
                if (itReturnDocumentId.HasValue)
                {
                    Command.Parameters.AddWithValue("@IT_RETURN_DOCUMENT_ID", itReturnDocumentId);
                }
                Connection.Open();

                SqlDataReader reader = Command.ExecuteReader();
                ITReturnDocumentsResponse result = new ITReturnDocumentsResponse();
                result.ITReturnDocumentsList = new List<ITReturnDocumentsDisplay>();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result.ITReturnDocumentsList.Add(new ITReturnDocumentsDisplay
                        {
                            Id = Convert.ToInt32(reader["Id"].ToString()),
                            ITReturnDetailsId = Convert.ToInt32(reader["ITReturnDetailsId"].ToString()),
                            ITHeadId = Convert.ToInt32(reader["ITHeadId"].ToString()),
                            ExcelSrNo = reader["ExcelSrNo"] != DBNull.Value ? reader["ExcelSrNo"].ToString() : null,
                            ITHeadDescription = reader["ITHeadDescription"] != DBNull.Value ? reader["ITHeadDescription"].ToString() : null,
                            PropertyName = reader["PropertyName"] != DBNull.Value ? reader["PropertyName"].ToString() : null,
                            FileName = reader["FileName"] != DBNull.Value ? reader["FileName"].ToString() : null,
                            FilePath = reader["FilePath"] != DBNull.Value ? reader["FilePath"].ToString() : null,
                            CompanyId = Convert.ToInt32(reader["CompanyId"].ToString()),
                            CompanyName = reader["CompanyName"] != DBNull.Value ? reader["CompanyName"].ToString() : null,
                            FYAYId = Convert.ToInt32(reader["FYAYId"].ToString()),
                            FinancialYear = reader["FinancialYear"] != DBNull.Value ? reader["FinancialYear"].ToString() : null,
                            AssessmentYear = reader["AssessmentYear"] != DBNull.Value ? reader["AssessmentYear"].ToString() : null,
                            Active = Convert.ToBoolean(reader["Active"].ToString()),                            
                        });
                    }
                }
                Log.Info("End call to GetITReturnDocumentsList");
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

        public ITReturnDetailsListResponse GetLitigationAndSimulation(int companyId)
        {
            try
            {
                Log.Info("Started call to GetLitigationAndSimulation");
                Log.Info("parameter values" + JsonConvert.SerializeObject(new
                {
                    companyId = companyId
                }));
                Command.CommandText = "SP_GET_LITIGATION_AND_SIMULATION";
                Command.CommandType = CommandType.StoredProcedure;
                Command.Parameters.AddWithValue("@COMPANY_ID", companyId);
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
                            FYAYID = Convert.ToInt32(reader["FYAYId"].ToString()),
                            ITSectionID = reader["ITSectionID"] != DBNull.Value ? int.Parse(reader["ITSectionID"].ToString()) : 0,
                            ITSectionDescription = reader["Description"] != DBNull.Value ? reader["Description"].ToString() : null,
                            ITSectionCategoryID = reader["SECTIONCATEGORYID"] != DBNull.Value ? int.Parse(reader["SECTIONCATEGORYID"].ToString()) : 0,
                            ITSectionCategoryDesc = reader["CategoryDesc"] != DBNull.Value ? reader["CategoryDesc"].ToString() : null,
                            ITReturnFillingDate = reader["ITReturnFillingDate"] != DBNull.Value ? Convert.ToDateTime(reader["ITReturnFillingDate"].ToString()) : (DateTime?)null,
                            ITReturnDueDate = reader["ITReturnDueDate"] != DBNull.Value ? Convert.ToDateTime(reader["ITReturnDueDate"].ToString()) : (DateTime?)null,
                            FinancialYear = reader["FinancialYear"] != DBNull.Value ? reader["FinancialYear"].ToString() : null,
                            AssessmentYear = reader["AssessmentYear"] != DBNull.Value ? reader["AssessmentYear"].ToString() : null,
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

        public LAndSCommentsResponse GetLAndSCommentList(int? companyId, int? itSubHeadId)
        {
            try
            {
                Log.Info("Started call to GetLAndSCommentList");
                Log.Info("parameter values" + JsonConvert.SerializeObject(new
                {
                    companyId = companyId,
                    itSubHeadId = itSubHeadId
                }));
                Command.CommandText = "SP_GET_L_AND_S_COMMENT_LIST";
                Command.CommandType = CommandType.StoredProcedure;
                if (companyId.HasValue && companyId > 0)
                {
                    Command.Parameters.AddWithValue("@COMPANY_ID", companyId);
                }
                if (itSubHeadId.HasValue && itSubHeadId > 0)
                {
                    Command.Parameters.AddWithValue("@IT_SUB_HEAD_ID", itSubHeadId);
                }
                Connection.Open();

                SqlDataReader reader = Command.ExecuteReader();
                LAndSCommentsResponse result = new LAndSCommentsResponse();
                result.LAndSCommentsList = new List<LAndSComments>();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result.LAndSCommentsList.Add(new LAndSComments
                        {
                            Id = int.Parse(reader["Id"].ToString()),
                            CompanyId = int.Parse(reader["CompanyId"].ToString()),
                            CompanyName = reader["CompanyName"] != DBNull.Value ? reader["CompanyName"].ToString() : string.Empty,
                            ITSubHeadId = int.Parse(reader["ITSubHeadId"].ToString()),
                            ITSubHeadDescription = reader["ITSubHeadDescription"] != DBNull.Value ? reader["ITSubHeadDescription"].ToString() : string.Empty,
                            Comment = reader["Comment"] != DBNull.Value ? reader["Comment"].ToString() : string.Empty,
                            Active = bool.Parse(reader["Active"].ToString())
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

        public LAndSCommentsResponse InsertUpdateLAndSComments(LAndSComments landsComments
           , string operation)
        {
            try
            {
                Log.Info("Started call to InsertUpdateLAndSComments");
                Log.Info("parameter values" + JsonConvert.SerializeObject(
                    new { landsComments = landsComments, operation = operation }));
                Command.CommandText = "SP_L_AND_S_COMMENT_MANAGER";
                Command.CommandType = CommandType.StoredProcedure;
                Command.Parameters.Clear();

                Command.Parameters.AddWithValue("@L_AND_S_COMMENT_XML", GetXMLFromObject(landsComments));
                if (!string.IsNullOrEmpty(operation))
                {
                    Command.Parameters.AddWithValue("@OPERATION", operation);
                }
                if (landsComments.AddedBy.HasValue)
                {
                    Command.Parameters.AddWithValue("@USER_ID", landsComments.AddedBy.Value);
                }
                else if (landsComments.ModifiedBy.HasValue)
                {
                    Command.Parameters.AddWithValue("@USER_ID", landsComments.ModifiedBy.Value);
                }
                else if (landsComments.DeletedBy.HasValue)
                {
                    Command.Parameters.AddWithValue("@USER_ID", landsComments.DeletedBy.Value);
                }
                Connection.Open();
                SqlDataReader reader = Command.ExecuteReader();

                LAndSCommentsResponse result = new LAndSCommentsResponse();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result = new LAndSCommentsResponse
                        {
                            Message = reader["ReturnMessage"] != DBNull.Value ? reader["ReturnMessage"].ToString() : null,
                            IsSuccess = Convert.ToBoolean(reader["Result"].ToString())
                        };
                    }
                }
                Log.Info("End call to InsertUpdateITReturnDocuments");

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

        public SPIncomeDetailsResponse GetSPIncomeDetailsList(int? itReturnDetailsId, int? itHeadId)
        {
            try
            {
                Log.Info("Started call to GetSPIncomeDetailsList");
                Log.Info("parameter values" + JsonConvert.SerializeObject(new
                {
                    itReturnDetailsId = itReturnDetailsId,
                    itHeadId = itHeadId
                }));
                Command.CommandText = "SP_GET_SP_INCOME_DETAILS";
                Command.CommandType = CommandType.StoredProcedure;
                if (itReturnDetailsId.HasValue && itReturnDetailsId > 0)
                {
                    Command.Parameters.AddWithValue("@IT_RETURN_DETAILS_ID", itReturnDetailsId);
                }
                if (itHeadId.HasValue && itHeadId > 0)
                {
                    Command.Parameters.AddWithValue("@IT_HEAD_ID", itHeadId);
                }
                Connection.Open();

                SqlDataReader reader = Command.ExecuteReader();
                SPIncomeDetailsResponse result = new SPIncomeDetailsResponse();
                result.SPIncomeDetailsList = new List<SPIncomeDetails>();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result.SPIncomeDetailsList.Add(new SPIncomeDetails
                        {
                            Id = int.Parse(reader["Id"].ToString()),
                            ITHeadId = int.Parse(reader["ITHeadId"].ToString()),
                            ITHeadDescription = reader["ITHeadDescription"] != DBNull.Value ? reader["ITHeadDescription"].ToString() : string.Empty,
                            PropertyName = reader["PropertyName"] != DBNull.Value ? reader["PropertyName"].ToString() : string.Empty,
                            ITReturnDetailsId = int.Parse(reader["ITReturnDetailsId"].ToString()),
                            SPIncomeDescription = reader["SPIncomeDescription"] != DBNull.Value ? reader["SPIncomeDescription"].ToString() : string.Empty,
                            SPIncomeDate = reader["SPIncomeDate"] != DBNull.Value ? DateTime.Parse(reader["SPIncomeDate"].ToString()) : (DateTime?)null,
                            SPIncomeValue = reader["SPIncomeValue"] != DBNull.Value ? decimal.Parse(reader["SPIncomeValue"].ToString()) : (decimal?)null,
                            TaxRate = reader["TaxRate"] != DBNull.Value ? decimal.Parse(reader["TaxRate"].ToString()) : (decimal?)null,
                            Active = bool.Parse(reader["Active"].ToString()),                            
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

        public SPIncomeDetailsResponse InsertUpdateSPIncomeDetails(SPIncomeDetails spIncomeDetails
           , string operation)
        {
            try
            {
                Log.Info("Started call to InsertUpdateSPIncomeDetails");
                Log.Info("parameter values" + JsonConvert.SerializeObject(
                    new { spIncomeDetails = spIncomeDetails, operation = operation }));
                Command.CommandText = "SP_INCOME_DETAILS_MANAGER";
                Command.CommandType = CommandType.StoredProcedure;
                Command.Parameters.Clear();

                Command.Parameters.AddWithValue("@SP_INCOME_DETAILS_XML", GetXMLFromObject(spIncomeDetails));
                if (!string.IsNullOrEmpty(operation))
                {
                    Command.Parameters.AddWithValue("@OPERATION", operation);
                }
                if (spIncomeDetails.AddedBy.HasValue)
                {
                    Command.Parameters.AddWithValue("@USER_ID", spIncomeDetails.AddedBy.Value);
                }
                else if (spIncomeDetails.ModifiedBy.HasValue)
                {
                    Command.Parameters.AddWithValue("@USER_ID", spIncomeDetails.ModifiedBy.Value);
                }
                else if (spIncomeDetails.DeletedBy.HasValue)
                {
                    Command.Parameters.AddWithValue("@USER_ID", spIncomeDetails.DeletedBy.Value);
                }
                Connection.Open();
                SqlDataReader reader = Command.ExecuteReader();

                SPIncomeDetailsResponse result = new SPIncomeDetailsResponse();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result = new SPIncomeDetailsResponse
                        {
                            Message = reader["ReturnMessage"] != DBNull.Value ? reader["ReturnMessage"].ToString() : null,
                            IsSuccess = Convert.ToBoolean(reader["Result"].ToString())
                        };
                    }
                }
                Log.Info("End call to InsertUpdateSPIncomeDetails");

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

        public BusinessLossDetailsResponse InsertUpdateBusinessLossDetails
            (BusinessLossDetails businessLossDetails, string operation)
        {
            try
            {
                Log.Info("Started call to InsertUpdateBusinessLossDetails");
                Log.Info("parameter values" + JsonConvert.SerializeObject(
                    new { businessLossDetails = businessLossDetails, operation = operation }));
                Command.CommandText = "SP_BUSINESS_LOSS_DETAILS_MANAGER";
                Command.CommandType = CommandType.StoredProcedure;
                Command.Parameters.Clear();

                Command.Parameters.AddWithValue("@BUSINESS_LOSS_DETAILS_XML", GetXMLFromObject(businessLossDetails));
                if (!string.IsNullOrEmpty(operation))
                {
                    Command.Parameters.AddWithValue("@OPERATION", operation);
                }
                if (businessLossDetails.AddedBy.HasValue)
                {
                    Command.Parameters.AddWithValue("@USER_ID", businessLossDetails.AddedBy.Value);
                }
                else if (businessLossDetails.ModifiedBy.HasValue)
                {
                    Command.Parameters.AddWithValue("@USER_ID", businessLossDetails.ModifiedBy.Value);
                }
                else if (businessLossDetails.DeletedBy.HasValue)
                {
                    Command.Parameters.AddWithValue("@USER_ID", businessLossDetails.DeletedBy.Value);
                }
                Connection.Open();
                SqlDataReader reader = Command.ExecuteReader();

                BusinessLossDetailsResponse result = new BusinessLossDetailsResponse();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result = new BusinessLossDetailsResponse
                        {
                            Message = reader["ReturnMessage"] != DBNull.Value ? reader["ReturnMessage"].ToString() : null,
                            IsSuccess = Convert.ToBoolean(reader["Result"].ToString())
                        };
                    }
                }
                Log.Info("End call to InsertUpdateBusinessLossDetails");

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

        public BusinessLossDetailsResponse GetBusinessLossDetailsList(int? companyId, int? fyayId
            , int? itSectionCategoryId, int? businessLossDetailsId)
        {
            try
            {
                Log.Info("Started call to GetBusinessLossDetailsList");
                Log.Info("parameter values" + JsonConvert.SerializeObject(new
                {
                    companyId = companyId,
                    fyayId = fyayId,
                    itSectionCategoryId = itSectionCategoryId,
                    businessLossDetailsId = businessLossDetailsId,
                }));
                Command.CommandText = "SP_GET_BUSINESS_LOSS_DETAILS_LIST";
                Command.CommandType = CommandType.StoredProcedure;
                if (companyId.HasValue && companyId > 0)
                {
                    Command.Parameters.AddWithValue("@COMPANY_ID", companyId);
                }
                if (fyayId.HasValue && fyayId > 0)
                {
                    Command.Parameters.AddWithValue("@FYAY_ID", fyayId);
                }
                if (itSectionCategoryId.HasValue && itSectionCategoryId > 0)
                {
                    Command.Parameters.AddWithValue("@IT_SECTION_CATEGORY_ID", itSectionCategoryId);
                }
                if (businessLossDetailsId.HasValue && businessLossDetailsId > 0)
                {
                    Command.Parameters.AddWithValue("@BUSINESS_LOSS_DETAILS_ID", businessLossDetailsId);
                }
                Connection.Open();

                SqlDataAdapter da = new SqlDataAdapter(Command);
                DataSet ds = new DataSet();
                da.Fill(ds);
                BusinessLossDetailsResponse result = new BusinessLossDetailsResponse();
                result.BusinessLossDetailsList = new List<BusinessLossDetails>();

                if (ds != null && ds.Tables.Count > 0)
                {
                    foreach(DataRow drBLDetails in ds.Tables[0].Rows)
                    {
                        result.BusinessLossDetailsList.Add(new BusinessLossDetails
                        {
                            IsCurrentYear = bool.Parse(drBLDetails["IsCurrentYear"].ToString()),
                            Id = int.Parse(drBLDetails["Id"].ToString()),
                            CompanyId = int.Parse(drBLDetails["CompanyId"].ToString()),
                            CompanyName = drBLDetails["CompanyName"] != DBNull.Value ? drBLDetails["CompanyName"].ToString() : String.Empty,
                            FYAYId = int.Parse(drBLDetails["FYAYId"].ToString()),
                            FinancialYear = drBLDetails["FinancialYear"] != DBNull.Value ? drBLDetails["FinancialYear"].ToString() : String.Empty,
                            AssessmentYear = drBLDetails["AssessmentYear"] != DBNull.Value ? drBLDetails["AssessmentYear"].ToString() : String.Empty,
                            ITSectionCategoryId = int.Parse(drBLDetails["ITSectionCategoryId"].ToString()),
                            CategoryDesc = drBLDetails["CategoryDesc"] != DBNull.Value ? drBLDetails["CategoryDesc"].ToString() : String.Empty,

                            IncomeCapGainsLTCG_BF = drBLDetails["IncomeCapGainsLTCG_BF"] != DBNull.Value ? decimal.Parse(drBLDetails["IncomeCapGainsLTCG_BF"].ToString()) : (decimal?)null,
                            IncomeCapGainsSTCG_BF = drBLDetails["IncomeCapGainsSTCG_BF"] != DBNull.Value ? decimal.Parse(drBLDetails["IncomeCapGainsSTCG_BF"].ToString()) : (decimal?)null,
                            IncomeBusinessProf_BF = drBLDetails["IncomeBusinessProf_BF"] != DBNull.Value ? decimal.Parse(drBLDetails["IncomeBusinessProf_BF"].ToString()) : (decimal?)null,
                            IncomeSpeculativeBusiness_BF = drBLDetails["IncomeSpeculativeBusiness_BF"] != DBNull.Value ? decimal.Parse(drBLDetails["IncomeSpeculativeBusiness_BF"].ToString()) : (decimal?)null,
                            UnabsorbedDepreciation_BF = drBLDetails["UnabsorbedDepreciation_BF"] != DBNull.Value ? decimal.Parse(drBLDetails["UnabsorbedDepreciation_BF"].ToString()) : (decimal?)null,
                            HousePropIncome_BF = drBLDetails["HousePropIncome_BF"] != DBNull.Value ? decimal.Parse(drBLDetails["HousePropIncome_BF"].ToString()) : (decimal?)null,
                            IncomeOtherSources_BF = drBLDetails["IncomeOtherSources_BF"] != DBNull.Value ? decimal.Parse(drBLDetails["IncomeOtherSources_BF"].ToString()) : (decimal?)null,

                            IncomeCapGainsLTCG_CY = drBLDetails["IncomeCapGainsLTCG_CY"] != DBNull.Value ? decimal.Parse(drBLDetails["IncomeCapGainsLTCG_CY"].ToString()) : (decimal?)null,
                            IncomeCapGainsSTCG_CY = drBLDetails["IncomeCapGainsSTCG_CY"] != DBNull.Value ? decimal.Parse(drBLDetails["IncomeCapGainsSTCG_CY"].ToString()) : (decimal?)null,
                            IncomeBusinessProf_CY = drBLDetails["IncomeBusinessProf_CY"] != DBNull.Value ? decimal.Parse(drBLDetails["IncomeBusinessProf_CY"].ToString()) : (decimal?)null,
                            IncomeSpeculativeBusiness_CY = drBLDetails["IncomeSpeculativeBusiness_CY"] != DBNull.Value ? decimal.Parse(drBLDetails["IncomeSpeculativeBusiness_CY"].ToString()) : (decimal?)null,
                            UnabsorbedDepreciation_CY = drBLDetails["UnabsorbedDepreciation_CY"] != DBNull.Value ? decimal.Parse(drBLDetails["UnabsorbedDepreciation_CY"].ToString()) : (decimal?)null,
                            HousePropIncome_CY = drBLDetails["HousePropIncome_CY"] != DBNull.Value ? decimal.Parse(drBLDetails["HousePropIncome_CY"].ToString()) : (decimal?)null,
                            IncomeOtherSources_CY = drBLDetails["IncomeOtherSources_CY"] != DBNull.Value ? decimal.Parse(drBLDetails["IncomeOtherSources_CY"].ToString()) : (decimal?)null,

                            IncomeCapGainsLTCG_UL = drBLDetails["IncomeCapGainsLTCG_UL"] != DBNull.Value ? decimal.Parse(drBLDetails["IncomeCapGainsLTCG_UL"].ToString()) : (decimal?)null,
                            IncomeCapGainsSTCG_UL = drBLDetails["IncomeCapGainsSTCG_UL"] != DBNull.Value ? decimal.Parse(drBLDetails["IncomeCapGainsSTCG_UL"].ToString()) : (decimal?)null,
                            IncomeBusinessProf_UL = drBLDetails["IncomeBusinessProf_UL"] != DBNull.Value ? decimal.Parse(drBLDetails["IncomeBusinessProf_UL"].ToString()) : (decimal?)null,
                            IncomeSpeculativeBusiness_UL = drBLDetails["IncomeSpeculativeBusiness_UL"] != DBNull.Value ? decimal.Parse(drBLDetails["IncomeSpeculativeBusiness_UL"].ToString()) : (decimal?)null,
                            UnabsorbedDepreciation_UL = drBLDetails["UnabsorbedDepreciation_UL"] != DBNull.Value ? decimal.Parse(drBLDetails["UnabsorbedDepreciation_UL"].ToString()) : (decimal?)null,
                            HousePropIncome_UL = drBLDetails["HousePropIncome_UL"] != DBNull.Value ? decimal.Parse(drBLDetails["HousePropIncome_UL"].ToString()) : (decimal?)null,
                            IncomeOtherSources_UL = drBLDetails["IncomeOtherSources_UL"] != DBNull.Value ? decimal.Parse(drBLDetails["IncomeOtherSources_UL"].ToString()) : (decimal?)null,

                            IncomeCapGainsLTCG_UALL = drBLDetails["IncomeCapGainsLTCG_UALL"] != DBNull.Value ? decimal.Parse(drBLDetails["IncomeCapGainsLTCG_UALL"].ToString()) : (decimal?)null,
                            IncomeCapGainsSTCG_UALL = drBLDetails["IncomeCapGainsSTCG_UALL"] != DBNull.Value ? decimal.Parse(drBLDetails["IncomeCapGainsSTCG_UALL"].ToString()) : (decimal?)null,
                            IncomeBusinessProf_UALL = drBLDetails["IncomeBusinessProf_UALL"] != DBNull.Value ? decimal.Parse(drBLDetails["IncomeBusinessProf_UALL"].ToString()) : (decimal?)null,
                            IncomeSpeculativeBusiness_UALL = drBLDetails["IncomeSpeculativeBusiness_UALL"] != DBNull.Value ? decimal.Parse(drBLDetails["IncomeSpeculativeBusiness_UALL"].ToString()) : (decimal?)null,
                            UnabsorbedDepreciation_UALL = drBLDetails["UnabsorbedDepreciation_UALL"] != DBNull.Value ? decimal.Parse(drBLDetails["UnabsorbedDepreciation_UALL"].ToString()) : (decimal?)null,
                            HousePropIncome_UALL = drBLDetails["HousePropIncome_UALL"] != DBNull.Value ? decimal.Parse(drBLDetails["HousePropIncome_UALL"].ToString()) : (decimal?)null,
                            IncomeOtherSources_UALL = drBLDetails["IncomeOtherSources_UALL"] != DBNull.Value ? decimal.Parse(drBLDetails["IncomeOtherSources_UALL"].ToString()) : (decimal?)null,

                            Active = bool.Parse(drBLDetails["Active"].ToString()),
                        });
                    }

                    if(ds.Tables.Count > 1)
                    {
                        foreach (DataRow drITReturnDetails in ds.Tables[1].Rows)
                        {
                            result.ITReturnDetailsObject = new ITReturnDetails
                            {
                                Id = int.Parse(drITReturnDetails["Id"].ToString()),
                                IncomefromCapGainsLTCG = drITReturnDetails["IncomefromCapGainsLTCG"] != DBNull.Value ? decimal.Parse(drITReturnDetails["IncomefromCapGainsLTCG"].ToString()) : (decimal?)null,
                                IncomefromCapGainsSTCG = drITReturnDetails["IncomefromCapGainsSTCG"] != DBNull.Value ? decimal.Parse(drITReturnDetails["IncomefromCapGainsSTCG"].ToString()) : (decimal?)null,
                                IncomefromBusinessProf = drITReturnDetails["IncomefromBusinessProf"] != DBNull.Value ? decimal.Parse(drITReturnDetails["IncomefromBusinessProf"].ToString()) : (decimal?)null,
                                IncomefromSpeculativeBusiness = drITReturnDetails["IncomefromSpeculativeBusiness"] != DBNull.Value ? decimal.Parse(drITReturnDetails["IncomefromSpeculativeBusiness"].ToString()) : (decimal?)null,
                                HousePropIncome = drITReturnDetails["HousePropIncome"] != DBNull.Value ? decimal.Parse(drITReturnDetails["HousePropIncome"].ToString()) : (decimal?)null,
                                IncomeFromOtherSources = drITReturnDetails["IncomeFromOtherSources"] != DBNull.Value ? decimal.Parse(drITReturnDetails["IncomeFromOtherSources"].ToString()) : (decimal?)null,
                            };
                        }
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

        public MATCreditDetailsResponse InsertUpdateMATCreditDetails
            (MATCreditDetails matCreditDetails, string operation)
        {
            try
            {
                Log.Info("Started call to InsertUpdateMATCreditDetails");
                Log.Info("parameter values" + JsonConvert.SerializeObject(
                    new { matCreditDetails = matCreditDetails, operation = operation }));
                Command.CommandText = "SP_MAT_CREDIT_DETAILS_MANAGER";
                Command.CommandType = CommandType.StoredProcedure;
                Command.Parameters.Clear();

                Command.Parameters.AddWithValue("@MAT_CREDIT_DETAILS_XML", GetXMLFromObject(matCreditDetails));
                if (!string.IsNullOrEmpty(operation))
                {
                    Command.Parameters.AddWithValue("@OPERATION", operation);
                }
                if (matCreditDetails.AddedBy.HasValue)
                {
                    Command.Parameters.AddWithValue("@USER_ID", matCreditDetails.AddedBy.Value);
                }
                else if (matCreditDetails.ModifiedBy.HasValue)
                {
                    Command.Parameters.AddWithValue("@USER_ID", matCreditDetails.ModifiedBy.Value);
                }
                else if (matCreditDetails.DeletedBy.HasValue)
                {
                    Command.Parameters.AddWithValue("@USER_ID", matCreditDetails.DeletedBy.Value);
                }
                Connection.Open();
                SqlDataReader reader = Command.ExecuteReader();

                MATCreditDetailsResponse result = new MATCreditDetailsResponse();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result = new MATCreditDetailsResponse
                        {
                            Message = reader["ReturnMessage"] != DBNull.Value ? reader["ReturnMessage"].ToString() : null,
                            IsSuccess = Convert.ToBoolean(reader["Result"].ToString())
                        };
                    }
                }
                Log.Info("End call to InsertUpdateMATCreditDetails");

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

        public MATCreditDetailsResponse GetMATCreditDetailsList(int? companyId, int? fyayId
            , int? itSectionCategoryId, int? matCreditDetailsId)
        {
            try
            {
                Log.Info("Started call to GetMATCreditDetailsList");
                Log.Info("parameter values" + JsonConvert.SerializeObject(new
                {
                    companyId = companyId,
                    fyayId = fyayId,
                    itSectionCategoryId = itSectionCategoryId,
                    matCreditDetailsId = matCreditDetailsId,
                }));
                Command.CommandText = "SP_GET_MAT_CREDIT_DETAILS_LIST";
                Command.CommandType = CommandType.StoredProcedure;
                if (companyId.HasValue && companyId > 0)
                {
                    Command.Parameters.AddWithValue("@COMPANY_ID", companyId);
                }
                if (fyayId.HasValue && fyayId > 0)
                {
                    Command.Parameters.AddWithValue("@FYAY_ID", fyayId);
                }
                if (itSectionCategoryId.HasValue && itSectionCategoryId > 0)
                {
                    Command.Parameters.AddWithValue("@IT_SECTION_CATEGORY_ID", itSectionCategoryId);
                }
                if (matCreditDetailsId.HasValue && matCreditDetailsId > 0)
                {
                    Command.Parameters.AddWithValue("@MAT_CREDIT_DETAILS_ID", matCreditDetailsId);
                }
                Connection.Open();

                SqlDataAdapter da = new SqlDataAdapter(Command);
                DataSet ds = new DataSet();
                da.Fill(ds);
                MATCreditDetailsResponse result = new MATCreditDetailsResponse();
                result.MATCreditDetailsList = new List<MATCreditDetails>();

                if (ds != null && ds.Tables.Count > 0)
                {
                    foreach (DataRow drBLDetails in ds.Tables[0].Rows)
                    {
                        result.MATCreditDetailsList.Add(new MATCreditDetails
                        {
                            Id = int.Parse(drBLDetails["Id"].ToString()),
                            CompanyId = int.Parse(drBLDetails["CompanyId"].ToString()),
                            FYAYId = int.Parse(drBLDetails["FYAYId"].ToString()),
                            FinancialYear = drBLDetails["FinancialYear"] != DBNull.Value ? drBLDetails["FinancialYear"].ToString() : String.Empty,
                            AssessmentYear = drBLDetails["AssessmentYear"] != DBNull.Value ? drBLDetails["AssessmentYear"].ToString() : String.Empty,
                            ITSectionCategoryId = int.Parse(drBLDetails["ITSectionCategoryId"].ToString()),

                            BusinessLosses_BF = drBLDetails["BusinessLosses_BF"] != DBNull.Value ? decimal.Parse(drBLDetails["BusinessLosses_BF"].ToString()) : (decimal?)null,
                            UnabsorbedDepreciation_BF = drBLDetails["UnabsorbedDepreciation_BF"] != DBNull.Value ? decimal.Parse(drBLDetails["UnabsorbedDepreciation_BF"].ToString()) : (decimal?)null,

                            BusinessLosses_CY = drBLDetails["BusinessLosses_CY"] != DBNull.Value ? decimal.Parse(drBLDetails["BusinessLosses_CY"].ToString()) : (decimal?)null,
                            UnabsorbedDepreciation_CY = drBLDetails["UnabsorbedDepreciation_CY"] != DBNull.Value ? decimal.Parse(drBLDetails["UnabsorbedDepreciation_CY"].ToString()) : (decimal?)null,

                            BusinessLosses_UL = drBLDetails["BusinessLosses_UL"] != DBNull.Value ? decimal.Parse(drBLDetails["BusinessLosses_UL"].ToString()) : (decimal?)null,
                            UnabsorbedDepreciation_UL = drBLDetails["UnabsorbedDepreciation_UL"] != DBNull.Value ? decimal.Parse(drBLDetails["UnabsorbedDepreciation_UL"].ToString()) : (decimal?)null,

                            Active = bool.Parse(drBLDetails["Active"].ToString()),
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
