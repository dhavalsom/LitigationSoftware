using LS.DAL.Interface;
using LS.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace LS.DAL.Library
{
    public class MasterDataAccess : DataAccessBase, IMasterDataAccess
    {
        #region Methods
        public List<Company> GetCompanies()
        {
            try
            {
                Log.Info("Started call to GetCompanies");
                Command.CommandText = "SP_GET_COMPANY";
                Command.CommandType = CommandType.StoredProcedure;
                Connection.Open();

                SqlDataReader reader = Command.ExecuteReader();
                List<Company> result = new List<Company>();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result.Add(new Company
                        {
                            CompanyName = reader["CompanyName"] != DBNull.Value ? reader["CompanyName"].ToString() : null,
                            PANNumber = reader["PANNumber"] != DBNull.Value ? reader["PANNumber"].ToString() : null,
                            Id = Convert.ToInt32(reader["ID"].ToString()),
                            CategoryID = reader["CategoryID"] != DBNull.Value ? Convert.ToInt32(reader["CategoryID"].ToString()) : 0
                        });
                    }
                }
                Log.Info("End call to GetCompanies. Result:"
                    + JsonConvert.SerializeObject(result));
                return result;
            }
            catch (Exception ex)
            {
                Log.Error("Error in GetCompanies. Error:"
                    + JsonConvert.SerializeObject(ex));
                LogError(ex);
                throw;
            }

            finally
            {
                Connection.Close();
            }
        }

        public List<CompanyCategory> GetCompanyCategories()
        {
            try
            {
                Log.Info("Started call to GetCompanyCategories");

                Command.CommandText = "SP_GET_COMPANYCATEGORIES";
                Command.CommandType = CommandType.StoredProcedure;
                Connection.Open();

                SqlDataReader reader = Command.ExecuteReader();
                List<CompanyCategory> result = new List<CompanyCategory>();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result.Add(new CompanyCategory
                        {
                            CategoryDesc = reader["CategoryDesc"] != DBNull.Value ? reader["CategoryDesc"].ToString() : null,
                            Id = Convert.ToInt32(reader["ID"].ToString())
                        });
                    }
                }
                Log.Info("End call to GetCompanyCategories. Result:"
                    + JsonConvert.SerializeObject(result));
                return result;
            }
            catch (Exception ex)
            {
                Log.Error("Error in GetCompanyCategories. Error:"
                       + JsonConvert.SerializeObject(ex));
                LogError(ex);
                throw;
            }
            finally
            {
                Connection.Close();
            }
        }

        public bool CreateCompany(Company comp)
        {
            try
            {
                Log.Info("Started call to CreateCompany");
                Log.Info("parameter values" + JsonConvert.SerializeObject(comp));
                Command.CommandText = "SP_CREATE_COMPANY";
                Command.CommandType = CommandType.StoredProcedure;
                Command.Parameters.Clear();

                SqlDataAdapter da = new SqlDataAdapter(Command);
                da.SelectCommand.Parameters.Add(new SqlParameter("@COMPANY_DETAIL_XML", SqlDbType.NVarChar, 10000));
                da.SelectCommand.Parameters["@COMPANY_DETAIL_XML"].Value = GetXMLFromObject(comp);
                da.SelectCommand.Parameters.Add(new SqlParameter("@USER_ID", SqlDbType.BigInt, 100));
                da.SelectCommand.Parameters["@USER_ID"].Value = !string.IsNullOrEmpty(comp.LoggedInUserID) ? Convert.ToInt32(comp.LoggedInUserID) : 1;
                Connection.Open();

                int? result = (int?)Command.ExecuteScalar();
                Log.Info("End call to CreateCompany with result  = " + result);

                if (result == null || result == 1)
                    return true;
                else
                    return false;

            }
            catch (Exception ex)
            {
                Log.Error("Error in CreateCompany. Error:"
                       + JsonConvert.SerializeObject(ex));
                LogError(ex);
                throw;
            }
            finally
            {
                Connection.Close();
            }
        }

        public List<FYAY> GetFYAY()
        {
            try
            {
                Log.Info("Started call to GetFYAY");
                Command.CommandText = "SP_GET_FYAY";
                Command.CommandType = CommandType.StoredProcedure;
                Connection.Open();

                SqlDataReader reader = Command.ExecuteReader();
                List<FYAY> result = new List<FYAY>();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result.Add(new FYAY
                        {
                            FinancialYear = reader["FinancialYear"] != DBNull.Value ? reader["FinancialYear"].ToString() : null,
                            AssessmentYear = reader["AssessmentYear"] != DBNull.Value ? reader["AssessmentYear"].ToString() : null,
                            IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString()),
                            Active = Convert.ToBoolean(reader["IsDefault"].ToString()),
                            Id = Convert.ToInt32(reader["ID"].ToString())
                        });
                    }
                }
                Log.Info("End call to GetFYAY. Result:"
                    + JsonConvert.SerializeObject(result));
                return result;
            }
            catch (Exception ex)
            {
                Log.Error("Error in GetFYAY. Error:"
                       + JsonConvert.SerializeObject(ex));
                LogError(ex);
                throw;
            }
            finally
            {
                Connection.Close();
            }
        }

        public List<ITSectionCategory> GetITSectionCategory()
        {
            try
            {
                Log.Info("Started call to GetITSectionCategory");

                Command.CommandText = "SP_GET_ITSectionCategory";
                Command.CommandType = CommandType.StoredProcedure;
                Connection.Open();

                SqlDataReader reader = Command.ExecuteReader();
                List<ITSectionCategory> result = new List<ITSectionCategory>();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result.Add(new ITSectionCategory
                        {
                            Description = reader["CategoryDesc"] != DBNull.Value ? reader["CategoryDesc"].ToString() : null,
                            IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString()),
                            Active = Convert.ToBoolean(reader["IsDefault"].ToString()),
                            Id = Convert.ToInt32(reader["Id"].ToString())
                        });
                    }
                }
                Log.Info("End call to GetITSectionCategory. Result:"
                    + JsonConvert.SerializeObject(result));
                return result;
            }
            catch (Exception ex)
            {
                Log.Error("Error in GetITSectionCategory. Error:"
                       + JsonConvert.SerializeObject(ex));
                LogError(ex);
                throw;
            }

            finally
            {
                Connection.Close();
            }
        }

        public List<ITSection> GetITSection(int categoryId)
        {
            try
            {
                Log.Info("Started call to GetITSection");
                Log.Info("parameter values" + JsonConvert.SerializeObject(new
                {
                    categoryId = categoryId
                }));
                Command.CommandText = "SP_GET_ITSection";
                Command.CommandType = CommandType.StoredProcedure;
                Connection.Open();

                Command.Parameters.Clear();

                Command.Parameters.AddWithValue("@ITSectionCategory_ID", categoryId);
                Command.Parameters.AddWithValue("@ACTIVE", true);

                SqlDataReader reader = Command.ExecuteReader();
                List<ITSection> result = new List<ITSection>();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result.Add(new ITSection
                        {
                            Description = reader["Description"] != DBNull.Value ? reader["Description"].ToString() : null,
                            IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString()),
                            IsReturn = Convert.ToBoolean(reader["IsReturn"].ToString()),
                            Active = Convert.ToBoolean(reader["IsDefault"].ToString()),
                            Id = Convert.ToInt32(reader["ID"].ToString()),
                            SectionCategoryId = reader["SectionCategoryId"] != DBNull.Value ? Convert.ToInt32(reader["SectionCategoryId"].ToString()) : (int?)null
                        });
                    }
                }
                Log.Info("End call to GetITSection. Result:"
                    + JsonConvert.SerializeObject(result));
                return result;
            }
            catch (Exception ex)
            {
                Log.Error("Error in GetITSection. Error:"
                       + JsonConvert.SerializeObject(ex));
                LogError(ex);
                throw;
            }

            finally
            {
                Connection.Close();
            }
        }

        public ITSectionResponse InsertUpdateITSection(ITSection objITSection)
        {
            try
            {
                Log.Info("Started call to InsertUpdateITSection");
                Log.Info("parameter values" + JsonConvert.SerializeObject(objITSection));
                Command.CommandText = "SP_IT_SECTION_MANAGER";
                Command.CommandType = CommandType.StoredProcedure;
                Command.Parameters.Clear();

                Command.Parameters.AddWithValue("@IT_SECTION_XML", GetXMLFromObject(objITSection));
                
                if (objITSection.AddedBy.HasValue)
                {
                    Command.Parameters.AddWithValue("@USER_ID", objITSection.AddedBy.Value);
                }
                if (objITSection.ModifiedBy.HasValue)
                {
                    Command.Parameters.AddWithValue("@USER_ID", objITSection.ModifiedBy.Value);
                }
                Connection.Open();
                SqlDataReader reader = Command.ExecuteReader();

                ITSectionResponse result = new ITSectionResponse();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result = new ITSectionResponse
                        {
                            Message = reader["ReturnMessage"] != DBNull.Value ? reader["ReturnMessage"].ToString() : null,
                            IsSuccess = Convert.ToBoolean(reader["Result"].ToString())
                        };
                    }
                }
                Log.Info("End call to InsertUpdateITSection. Result:"
                    + JsonConvert.SerializeObject(result));
                return result;
            }
            catch (Exception ex)
            {
                Log.Error("Error in InsertUpdateITSection. Error:"
                       + JsonConvert.SerializeObject(ex));
                LogError(ex);
                throw;
            }
            finally
            {
                Connection.Close();
            }
        }

        public List<ITHeadMaster> GetITHeadMaster(bool? IsTaxComputed)
        {
            try
            {
                Log.Info("Started call to GetITHeadMaster");
                Log.Info("parameter values" + JsonConvert.SerializeObject(new
                {
                    IsTaxComputed = IsTaxComputed
                }));
                Command.CommandText = "SP_GET_IT_HEAD_MASTER";
                Command.CommandType = CommandType.StoredProcedure;
                Command.Parameters.Clear();

                if (IsTaxComputed.HasValue)
                {
                    Command.Parameters.AddWithValue("@IsTaxComputed", IsTaxComputed.Value);
                }
                Command.Parameters.AddWithValue("@ACTIVE", 1);

                Connection.Open();
                SqlDataReader reader = Command.ExecuteReader();
                List<ITHeadMaster> result = new List<ITHeadMaster>();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result.Add(new ITHeadMaster
                        {
                            ExcelSrNo = reader["ExcelSrNo"] != DBNull.Value ? reader["ExcelSrNo"].ToString() : null,
                            Description = reader["Description"] != DBNull.Value ? reader["Description"].ToString() : null,
                            PropertyName = reader["PropertyName"] != DBNull.Value ? reader["PropertyName"].ToString() : null,
                            CanAddSubHead = reader["CanAddSubHead"] != DBNull.Value ? Convert.ToBoolean(reader["CanAddSubHead"].ToString()) : false,
                            CanAddDocuments = reader["CanAddDocuments"] != DBNull.Value ? Convert.ToBoolean(reader["CanAddDocuments"].ToString()) : false,
                            HasDate = reader["HasDate"] != DBNull.Value ? Convert.ToBoolean(reader["HasDate"].ToString()) : false,
                            IsSpecialIncomeEnabled = reader["IsSpecialIncomeEnabled"] != DBNull.Value ? Convert.ToBoolean(reader["IsSpecialIncomeEnabled"].ToString()) : false,
                            IsROI = reader["IsROI"] != DBNull.Value ? Convert.ToBoolean(reader["IsROI"].ToString()) : false,
                            Active = Convert.ToBoolean(reader["Active"].ToString()),
                            Id = Convert.ToInt32(reader["Id"].ToString()),
                            IsTaxComputed = Convert.ToBoolean(reader["IsTaxComputed"].ToString())
                        });
                    }
                }
                Log.Info("End call to GetITHeadMaster:"
                    + JsonConvert.SerializeObject(result));
                return result;
            }
            catch (Exception ex)
            {
                Log.Error("Error in GetITHeadMaster. Error:"
                       + JsonConvert.SerializeObject(ex));
                LogError(ex);
                throw;
            }

            finally
            {
                Connection.Close();
            }
        }

        public List<ITSubHeadMaster> GetITSubHeadMaster(int? itHeadId)
        {
            try
            {
                Log.Info("Started call to GetITSubHeadMaster");
                Log.Info("parameter values" + JsonConvert.SerializeObject(new
                {
                    itHeadId = itHeadId
                }));
                Command.CommandText = "SP_GET_IT_SUB_HEAD_MASTER";
                Command.CommandType = CommandType.StoredProcedure;
                Connection.Open();
                Command.Parameters.AddWithValue("@ACTIVE", true);
                SqlDataReader reader = Command.ExecuteReader();
                List<ITSubHeadMaster> result = new List<ITSubHeadMaster>();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result.Add(new ITSubHeadMaster
                        {
                            ITHeadId = Convert.ToInt32(reader["ITHeadId"].ToString()),
                            Description = reader["Description"] != DBNull.Value ? reader["Description"].ToString() : null,
                            ITHeadName = reader["ITHeadName"] != DBNull.Value ? reader["ITHeadName"].ToString() : null,
                            IsAllowance = reader["IsAllowance"] != DBNull.Value ? Convert.ToBoolean(reader["IsAllowance"].ToString()) : false,
                            HasDate = reader["HasDate"] != DBNull.Value ? Convert.ToBoolean(reader["HasDate"].ToString()) : false,
                            Active = Convert.ToBoolean(reader["Active"].ToString()),
                            Id = Convert.ToInt32(reader["Id"].ToString())
                        });
                    }
                }

                Log.Info("End call to GetITSubHeadMaster:"
                    + JsonConvert.SerializeObject(result));
                return result;
            }
            catch (Exception ex)
            {
                Log.Error("Error in GetITSubHeadMaster. Error:"
                       + JsonConvert.SerializeObject(ex));
                LogError(ex);
                throw;
            }
            finally
            {
                Connection.Close();
            }
        }

        public ITSubHeadMasterResponse InsertUpdateITSubHeadMaster
            (ITSubHeadMaster objITSubHeadMaster)
        {
            try
            {
                Log.Info("Started call to InsertUpdateITSubHeadMaster");
                Log.Info("parameter values" 
                    + JsonConvert.SerializeObject(objITSubHeadMaster));
                Command.CommandText = "SP_IT_SUB_HEAD_MANAGER";
                Command.CommandType = CommandType.StoredProcedure;
                Command.Parameters.Clear();

                Command.Parameters.AddWithValue("@IT_SUB_HEAD_XML", GetXMLFromObject(objITSubHeadMaster));

                if (objITSubHeadMaster.AddedBy.HasValue)
                {
                    Command.Parameters.AddWithValue("@USER_ID", objITSubHeadMaster.AddedBy.Value);
                }
                if (objITSubHeadMaster.ModifiedBy.HasValue)
                {
                    Command.Parameters.AddWithValue("@USER_ID", objITSubHeadMaster.ModifiedBy.Value);
                }
                Connection.Open();
                SqlDataReader reader = Command.ExecuteReader();

                ITSubHeadMasterResponse result = new ITSubHeadMasterResponse();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result = new ITSubHeadMasterResponse
                        {
                            Id = Convert.ToInt32(reader["Id"].ToString()),
                            Message = reader["ReturnMessage"] != DBNull.Value ? reader["ReturnMessage"].ToString() : null,
                            IsSuccess = Convert.ToBoolean(reader["Result"].ToString())
                        };
                    }
                }
                Log.Info("End call to InsertUpdateITSubHeadMaster. Result:"
                    + JsonConvert.SerializeObject(result));
                return result;
            }
            catch (Exception ex)
            {
                Log.Error("Error in InsertUpdateITSubHeadMaster. Error:"
                       + JsonConvert.SerializeObject(ex));
                LogError(ex);
                throw;
            }
            finally
            {
                Connection.Close();
            }
        }

        public List<ComplianceMaster> GetComplianceMaster(int? complianceId)
        {
            try
            {
                Log.Info("Started call to GetComplianceMaster");
                Log.Info("parameter values" + JsonConvert.SerializeObject(new
                {
                    complianceId = complianceId
                }));
                Command.CommandText = "SP_GET_COMPLIANCE_LIST";
                Command.CommandType = CommandType.StoredProcedure;
                Connection.Open();
                SqlDataReader reader = Command.ExecuteReader();
                List<ComplianceMaster> result = new List<ComplianceMaster>();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result.Add(new ComplianceMaster
                        {
                            SrNo = Convert.ToInt32(reader["SrNo"].ToString()),
                            Description = reader["Description"] != DBNull.Value ? reader["Description"].ToString() : null,
                            Active = Convert.ToBoolean(reader["Active"].ToString()),
                            Id = Convert.ToInt32(reader["Id"].ToString())
                        });
                    }
                }
                Log.Info("End call to GetComplianceMaster:"
                    + JsonConvert.SerializeObject(result));
                return result;
            }
            catch (Exception ex)
            {
                Log.Error("Error in GetComplianceMaster. Error:"
                       + JsonConvert.SerializeObject(ex));
                LogError(ex);
                throw;
            }
            finally
            {
                Connection.Close();
            }
        }

        public ComplianceMasterResponse InsertUpdateComplianceMaster
            (ComplianceMaster objComplianceMaster)
        {
            try
            {
                Log.Info("Started call to InsertUpdateComplianceMaster");
                Log.Info("parameter values " 
                    + JsonConvert.SerializeObject(objComplianceMaster));
                Command.CommandText = "SP_COMPLIANCE_MANAGER";
                Command.CommandType = CommandType.StoredProcedure;
                Command.Parameters.Clear();

                Command.Parameters.AddWithValue("@COMPLIANCE_XML", GetXMLFromObject(objComplianceMaster));

                if (objComplianceMaster.AddedBy.HasValue)
                {
                    Command.Parameters.AddWithValue("@USER_ID", objComplianceMaster.AddedBy.Value);
                }
                if (objComplianceMaster.ModifiedBy.HasValue)
                {
                    Command.Parameters.AddWithValue("@USER_ID", objComplianceMaster.ModifiedBy.Value);
                }
                Connection.Open();
                SqlDataReader reader = Command.ExecuteReader();

                ComplianceMasterResponse result = new ComplianceMasterResponse();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result = new ComplianceMasterResponse
                        {
                            Message = reader["ReturnMessage"] != DBNull.Value ? reader["ReturnMessage"].ToString() : null,
                            IsSuccess = Convert.ToBoolean(reader["Result"].ToString())
                        };
                    }
                }
                Log.Info("End call to InsertUpdateComplianceMaster. Result:"
                    + JsonConvert.SerializeObject(result));
                return result;
            }
            catch (Exception ex)
            {
                Log.Error("Error in InsertUpdateComplianceMaster. Error:"
                       + JsonConvert.SerializeObject(ex));
                LogError(ex);
                throw;
            }
            finally
            {
                Connection.Close();
            }
        }

        public List<StandardData> GetStandardData(int? FYAYID, int? standarddataId)
        {
            try
            {
                Log.Info("Started call to GetStandardData");
                Log.Info("parameter values" + JsonConvert.SerializeObject(new
                {
                    FYAYID = FYAYID,
                    standarddataId = standarddataId
                }));
                Command.CommandText = "SP_GET_STANDARDDATA_LIST";
                Command.CommandType = CommandType.StoredProcedure;
                Connection.Open();

                Command.Parameters.Clear();

                Command.Parameters.AddWithValue("@FYAY_ID", FYAYID);
                Command.Parameters.AddWithValue("@StandardData_ID", standarddataId);
                Command.Parameters.AddWithValue("@ACTIVE", true);

                SqlDataReader reader = Command.ExecuteReader();
                List<StandardData> result = new List<StandardData>();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result.Add(new StandardData
                        {
                            FYAYID = Convert.ToInt32(reader["FYAYID"].ToString()),
                            FinancialYear = reader["FinancialYear"] != DBNull.Value ? reader["FinancialYear"].ToString() : null,
                            AssessmentYear = reader["AssessmentYear"] != DBNull.Value ? reader["AssessmentYear"].ToString() : null,
                            BasicTaxRate =  reader["BasicTaxRate"] != DBNull.Value ? Convert.ToDecimal(reader["BasicTaxRate"].ToString()) : (decimal?)null,
                            MATRate = reader["MATRate"] != DBNull.Value ? Convert.ToDecimal(reader["MATRate"].ToString()):(decimal?)null,
                            EducationCess = reader["EducationCess"] != DBNull.Value ? Convert.ToDecimal(reader["EducationCess"].ToString()):(decimal?)null,
                            Active = Convert.ToBoolean(reader["Active"].ToString()),
                            Id = Convert.ToInt32(reader["Id"].ToString())
                        });
                    }
                }
                Log.Info("End call to GetStandardData:"
                    + JsonConvert.SerializeObject(result));
                return result;
            }
            catch (Exception ex)
            {
                Log.Error("Error in GetStandardData. Error:"
                       + JsonConvert.SerializeObject(ex));
                LogError(ex);
                throw;
            }
            finally
            {
                Connection.Close();
            }
        }

        public List<SurchargeData> GetSurchargeData(int? FYAYID, int? surchargedataId
            , int? entitycategorytypeid)
        {
            try
            {
                Log.Info("Started call to SurchargeData");
                Log.Info("parameter values" + JsonConvert.SerializeObject(new
                {
                    FYAYID = FYAYID,
                    surchargedataId = surchargedataId,
                    entitycategorytypeid = entitycategorytypeid
                }));
                Command.CommandText = "SP_GET_SURCHARGEDATA_LIST";
                Command.CommandType = CommandType.StoredProcedure;
                Connection.Open();

                Command.Parameters.Clear();

                Command.Parameters.AddWithValue("@FYAY_ID", FYAYID);
                Command.Parameters.AddWithValue("@SurchargeData_ID", surchargedataId);
                Command.Parameters.AddWithValue("@entitycategorytypeid", entitycategorytypeid);
                Command.Parameters.AddWithValue("@ACTIVE", true);

                SqlDataReader reader = Command.ExecuteReader();
                List<SurchargeData> result = new List<SurchargeData>();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result.Add(new SurchargeData
                        {
                            FYAYID = Convert.ToInt32(reader["FYAYID"].ToString()),
                            FinancialYear = reader["FinancialYear"] != DBNull.Value ? reader["FinancialYear"].ToString() : null,
                            AssessmentYear = reader["AssessmentYear"] != DBNull.Value ? reader["AssessmentYear"].ToString() : null,
                            surchargefromthreshold = reader["surchargefromthreshold"] != DBNull.Value ? Convert.ToDecimal(reader["surchargefromthreshold"].ToString()) : (decimal?)null,
                            surchargetothreshold = reader["surchargetothreshold"] != DBNull.Value ? Convert.ToDecimal(reader["surchargetothreshold"].ToString()) : (decimal?)null,
                            surchargerate = reader["surchargerate"] != DBNull.Value ? Convert.ToDecimal(reader["surchargerate"].ToString()) : (decimal?)null,
                            entitycategorytypeid = reader["entitycategorytypeid"] != DBNull.Value ? Convert.ToInt32(reader["entitycategorytypeid"].ToString()) : (int?)null,
                            Active = Convert.ToBoolean(reader["Active"].ToString()),
                            Id = Convert.ToInt32(reader["Id"].ToString())
                        });
                    }
                }
                Log.Info("End call to SurchargeData:" 
                    + JsonConvert.SerializeObject(result));
                return result;
            }
            catch (Exception ex)
            {
                Log.Error("Error in GetSurchargeData. Error:"
                       + JsonConvert.SerializeObject(ex));
                LogError(ex);
                throw;
            }

            finally
            {
                Connection.Close();
            }
        }

        public List<DocumentCategoryMaster> GetDocumentCategoryMaster(bool? IsActive)
        {
            try
            {
                Log.Info("Started call to GetDocumentCategoryMaster");
                Log.Info("parameter values" + JsonConvert.SerializeObject(new
                {
                    IsActive = IsActive
                }));
                Command.CommandText = "SP_GET_DOCUMENT_CATEGORY_MASTER";
                Command.CommandType = CommandType.StoredProcedure;
                Command.Parameters.Clear();

                if (IsActive.HasValue)
                {
                    Command.Parameters.AddWithValue("@ACTIVE", IsActive.Value);
                }
                Connection.Open();
                SqlDataReader reader = Command.ExecuteReader();
                List<DocumentCategoryMaster> result = new List<DocumentCategoryMaster>();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result.Add(new DocumentCategoryMaster
                        {
                            Description = reader["Description"] != DBNull.Value ? reader["Description"].ToString() : null,
                            Active = Convert.ToBoolean(reader["Active"].ToString()),
                            Id = Convert.ToInt32(reader["Id"].ToString()),
                        });
                    }
                }
                Log.Info("End call to GetDocumentCategoryMaster:" 
                    + JsonConvert.SerializeObject(result));
                return result;
            }
            catch (Exception ex)
            {
                Log.Error("Error in GetDocumentCategoryMaster. Error:"
                       + JsonConvert.SerializeObject(ex));
                LogError(ex);
                throw;
            }
            finally
            {
                Connection.Close();
            }
        }

        public List<SubDocumentCategoryMaster> GetSubDocumentCategoryMaster(int? documentCategoryId
            , bool? IsActive)
        {
            try
            {
                Log.Info("Started call to GetSubDocumentCategoryMaster");
                Log.Info("parameter values" + JsonConvert.SerializeObject(new
                {
                    documentCategoryId = documentCategoryId,
                    IsActive = IsActive
                }));
                Command.CommandText = "SP_GET_SUB_DOCUMENT_CATEGORY_MASTER";
                Command.CommandType = CommandType.StoredProcedure;
                Connection.Open();
                if (documentCategoryId.HasValue)
                {
                    Command.Parameters.AddWithValue("@DOCUMENT_CATEGORY_ID", documentCategoryId.Value);
                }
                if (IsActive.HasValue)
                {
                    Command.Parameters.AddWithValue("@ACTIVE", IsActive.Value);
                }
                SqlDataReader reader = Command.ExecuteReader();
                List<SubDocumentCategoryMaster> result = new List<SubDocumentCategoryMaster>();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result.Add(new SubDocumentCategoryMaster
                        {
                            DocumentCategoryId = Convert.ToInt32(reader["DocumentCategoryId"].ToString()),
                            Description = reader["Description"] != DBNull.Value ? reader["Description"].ToString() : null,
                            DocumentCategoryName = reader["DocumentCategoryName"] != DBNull.Value ? reader["DocumentCategoryName"].ToString() : null,
                            Active = Convert.ToBoolean(reader["Active"].ToString()),
                            Id = Convert.ToInt32(reader["Id"].ToString())
                        });
                    }
                }

                Log.Info("End call to GetSubDocumentCategoryMaster:"
                    + JsonConvert.SerializeObject(result));
                return result;
            }
            catch (Exception ex)
            {
                Log.Error("Error in GetSubDocumentCategoryMaster. Error:"
                       + JsonConvert.SerializeObject(ex));
                LogError(ex);
                throw;
            }

            finally
            {
                Connection.Close();
            }
        }

        public SubDocumentCategoryMasterResponse InsertUpdateSubDocumentCategoryMaster
            (SubDocumentCategoryMaster objSubDocumentCategoryMaster)
        {
            try
            {
                Log.Info("Started call to InsertUpdateSubDocumentCategoryMaster");
                Log.Info("parameter values" 
                    + JsonConvert.SerializeObject(objSubDocumentCategoryMaster));
                Command.CommandText = "SP_SUB_DOCUMENT_CATEGORY_MANAGER";
                Command.CommandType = CommandType.StoredProcedure;
                Command.Parameters.Clear();

                Command.Parameters.AddWithValue("@SUB_DOCUMENT_CATEGORY_XML", GetXMLFromObject(objSubDocumentCategoryMaster));

                if (objSubDocumentCategoryMaster.AddedBy.HasValue)
                {
                    Command.Parameters.AddWithValue("@USER_ID", objSubDocumentCategoryMaster.AddedBy.Value);
                }
                if (objSubDocumentCategoryMaster.ModifiedBy.HasValue)
                {
                    Command.Parameters.AddWithValue("@USER_ID", objSubDocumentCategoryMaster.ModifiedBy.Value);
                }
                Connection.Open();
                SqlDataReader reader = Command.ExecuteReader();

                SubDocumentCategoryMasterResponse result = new SubDocumentCategoryMasterResponse();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result = new SubDocumentCategoryMasterResponse
                        {
                            Id = Convert.ToInt32(reader["Id"].ToString()),
                            Message = reader["ReturnMessage"] != DBNull.Value ? reader["ReturnMessage"].ToString() : null,
                            IsSuccess = Convert.ToBoolean(reader["Result"].ToString())
                        };
                    }
                }
                Log.Info("End call to InsertUpdateSubDocumentCategoryMaster. Result:"
                    + JsonConvert.SerializeObject(result));
                return result;
            }
            catch (Exception ex)
            {
                Log.Error("Error in InsertUpdateSubDocumentCategoryMaster. Error:"
                       + JsonConvert.SerializeObject(ex));
                LogError(ex);
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
