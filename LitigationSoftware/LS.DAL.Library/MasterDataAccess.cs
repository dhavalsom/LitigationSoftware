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
                            Id = Convert.ToInt32(reader["ID"].ToString())
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

        public List<ITSection> GetITSection()
        {
            try
            {
                Log.Info("Started call to GetITSection");

                Command.CommandText = "SP_GET_ITSection";
                Command.CommandType = CommandType.StoredProcedure;
                Connection.Open();

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
                            Id = Convert.ToInt32(reader["ID"].ToString())
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
                Log.Info("End call to InsertUpdateITSection");

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

        public List<ITHeadMaster> GetITHeadMaster()
        {
            try
            {
                Log.Info("Started call to GetITHeadMaster");
                Command.CommandText = "SP_GET_IT_HEAD_MASTER";
                Command.CommandType = CommandType.StoredProcedure;
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
                            CanAddSubHead = Convert.ToBoolean(reader["CanAddSubHead"].ToString()),
                            Active = Convert.ToBoolean(reader["Active"].ToString()),
                            Id = Convert.ToInt32(reader["Id"].ToString())
                        });
                    }
                }
                Log.Info("End call to GetITHeadMaster:" + JsonConvert.SerializeObject(result));
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

        public List<ITSubHeadMaster> GetITSubHeadMaster(int? itHeadId)
        {
            try
            {
                Log.Info("Started call to GetITSubHeadMaster");
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
                            IsAllowance = Convert.ToBoolean(reader["IsAllowance"].ToString()),
                            Active = Convert.ToBoolean(reader["Active"].ToString()),
                            Id = Convert.ToInt32(reader["Id"].ToString())
                        });
                    }
                }

                Log.Info("End call to GetITSubHeadMaster:" + JsonConvert.SerializeObject(result));
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

        public ITSubHeadMasterResponse InsertUpdateITSubHeadMaster(ITSubHeadMaster objITSubHeadMaster)
        {
            try
            {
                Log.Info("Started call to InsertUpdateITSubHeadMaster");
                Log.Info("parameter values" + JsonConvert.SerializeObject(objITSubHeadMaster));
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
                Log.Info("End call to InsertUpdateITSubHeadMaster");

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

        public List<ComplianceMaster> GetComplianceMaster(int? complianceId)
        {
            try
            {
                Log.Info("Started call to GetComplianceMaster");
                Log.Info("parameter values" + JsonConvert.SerializeObject(complianceId));
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
                Log.Info("End call to GetComplianceMaster:" + JsonConvert.SerializeObject(result));
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

        public ComplianceMasterResponse InsertUpdateComplianceMaster(ComplianceMaster objComplianceMaster)
        {
            try
            {
                Log.Info("Started call to InsertUpdateComplianceMaster");
                Log.Info("parameter values" + JsonConvert.SerializeObject(objComplianceMaster));
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
                Log.Info("End call to InsertUpdateComplianceMaster");

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
