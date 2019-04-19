using log4net;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace LS.DAL.Library
{
    public class DataAccessBase
    {
        #region Properties
        public SqlConnection Connection { get; set; }
        public SqlCommand Command { get; set; }
        public static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        #endregion

        #region Constructors 
        public DataAccessBase()
        {
            Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString);
            Command = new SqlCommand();
            Command.Connection = Connection;
        }
        #endregion

        #region Methods

        public void LogError(Exception error)
        {
            try
            {
                using (SqlConnection errConnection = new SqlConnection(Connection.ConnectionString))
                {
                    SqlCommand errCommand = new SqlCommand();
                    errCommand.Connection = errConnection;
                    errCommand.CommandText = "SP_LOG_ERRORS";
                    errCommand.CommandType = CommandType.StoredProcedure;
                    errCommand.Parameters.AddWithValue("@MESSAGE", error.Message);
                    errCommand.Parameters.AddWithValue("@STACK_TRACE", error.StackTrace);
                    errCommand.Parameters.AddWithValue("@EXCEPTION_TYPE", error.GetType().ToString());
                    errCommand.Parameters.AddWithValue("@SOURCE", error.Source);
                    errCommand.Parameters.AddWithValue("@USER_ID", 1);
                    errConnection.Open();
                    errCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
            }
        }

        public static string GetXMLFromObject(object o)
        {
            // removes version
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.OmitXmlDeclaration = true;

            XmlSerializer xsSubmit = new XmlSerializer(o.GetType());
            try
            {
                using (StringWriter sw = new StringWriter())
                using (XmlWriter writer = XmlWriter.Create(sw, settings))
                {
                    // removes namespace
                    var xmlns = new XmlSerializerNamespaces();
                    xmlns.Add(string.Empty, string.Empty);

                    xsSubmit.Serialize(writer, o, xmlns);
                    return sw.ToString(); // Your XML
                }
            }
            catch (Exception ex)
            {
                //Handle Exception Code
                return null;
            }
            finally
            {
            }
        }

        #endregion
    }
}
