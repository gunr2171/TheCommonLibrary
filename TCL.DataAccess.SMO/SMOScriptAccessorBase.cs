using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCL.DataAccess.MsSql;

namespace TCL.DataAccess.SMO
{
    /// <summary>
    /// Builds on top of the SqlScriptAccessorBase by allowing SMO scripts to be ran (scripts that include the "GO" keyword)
    /// </summary>
    public abstract class SMOScriptAccessorBase : SqlScriptAccessorBase
    {
        /// <summary>
        /// Creates a new SMOScriptAccessorBase object with the given connection string.
        /// </summary>
        /// <param name="cs">The connection string to use in the database connection.</param>
        public SMOScriptAccessorBase(string cs) : base(cs) { }

        /// <summary>
        /// Runs the script with SMO, so that "GO" statements are recognized. However, you are not able to retrieve any results from the command.
        /// </summary>
        /// <param name="sqlScript">The script to run on the server.</param>
        /// <param name="useTransaction">If true, the script will be wrapped in a transaction.</param>
        protected void RunBatchScript(string sqlScript, bool useTransaction)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                Server server = new Server(new ServerConnection(conn));
                server.ConnectionContext.BatchSeparator = "GO";

                if (useTransaction)
                    server.ConnectionContext.BeginTransaction();

                try
                {
                    server.ConnectionContext.ExecuteNonQuery(sqlScript);

                    if (useTransaction)
                        server.ConnectionContext.CommitTransaction();
                }
                catch
                {
                    if (useTransaction)
                        server.ConnectionContext.RollBackTransaction();
                    throw;
                }
            }
        }

        /// <summary>
        /// Runs the script with SMO, so that "GO" statements are recognized. However, you are not able to retrieve any results from the command.
        /// The script will be wrapped in a transaction and will be rolled back if an exception is thrown during execution.
        /// </summary>
        /// <param name="sqlScript">The script to run on the server.</param>
        protected void RunBatchScript(string sqlScript)
        {
            RunBatchScript(sqlScript, true);
        }
    }
}
