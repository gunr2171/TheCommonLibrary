using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCL.DataAccess.Postgres
{
    /// <summary>
    /// Base class for Postgres database connections which use a simple script for querying.
    /// </summary>
    public abstract class PostgresScriptAccessorBase : PostgresAccessorBase
    {
        /// <summary>
        /// Creates a new PostgresScriptAccessorBase object with the given connection string
        /// </summary>
        /// <param name="cs">The connection string to the database.</param>
        public PostgresScriptAccessorBase(string cs) : base(cs) { }

        /// <summary>
        /// Runs a script and uses the full dataset of what is returned.
        /// </summary>
        /// <typeparam name="T">The data type of the returned object.</typeparam>
        /// <param name="PostgresScript">The script to run.</param>
        /// <param name="parametersAction">An action detailing any modifications to the PostgresParameterCollection object.
        /// Use this for adding parameters to the request.</param>
        /// <param name="getResults">A function that takes the results of the query and returns the indicated output.</param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Review Postgres queries for security vulnerabilities")]
        protected T RunScript<T>(string PostgresScript, Action<NpgsqlParameterCollection> parametersAction, Func<DataSet, T> getResults)
        {
            using (DataSet ds = new DataSet())
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(ConnectionString))
                {
                    conn.Open();
                    using (NpgsqlTransaction trans = conn.BeginTransaction())
                    {
                        try
                        {
                            using (NpgsqlCommand cmd = conn.CreateCommand())
                            {
                                cmd.Transaction = trans;

                                cmd.CommandType = System.Data.CommandType.Text;
                                cmd.CommandText = PostgresScript;

                                if (parametersAction != null)
                                    parametersAction(cmd.Parameters);

                                using (NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd))
                                {
                                    da.Fill(ds);
                                }
                            }

                            trans.Commit();
                        }
                        catch
                        {
                            trans.Rollback();
                            throw; //CA2200 
                        }
                    }
                }

                if (getResults != null)
                    return getResults(ds);
                else
                    return default(T);
            }
        }

        /// <summary>
        /// Runs a script async and uses the full dataset of what is returned.
        /// </summary>
        /// <typeparam name="T">The data type of the returned object.</typeparam>
        /// <param name="PostgresScript">The script to run.</param>
        /// <param name="parametersAction">An action detailing any modifications to the PostgresParameterCollection object.
        /// Use this for adding parameters to the request.</param>
        /// <param name="getResults">A function that takes the results of the query and returns the indicated output.</param>
        /// <returns></returns>
        protected async Task<T> RunScriptAsync<T>(string PostgresScript, Action<NpgsqlParameterCollection> parametersAction, Func<DataSet, T> getResults)
        {
            return await Task.Run(() => RunScript<T>(PostgresScript, parametersAction, getResults));
        }

        /// <summary>
        /// Runs the script and gets the first table returned.
        /// </summary>
        /// <typeparam name="T">The data type of the returned object.</typeparam>
        /// <param name="PostgresScript">The script to run.</param>
        /// <param name="parametersAction">An action detailing any modifications to the PostgresParameterCollection object.
        /// Use this for adding parameters to the request.</param>
        /// <param name="getResults">A function that takes the results of the query and returns the indicated output.</param>
        /// <returns></returns>
        protected T RunScript<T>(string PostgresScript, Action<NpgsqlParameterCollection> parametersAction, Func<DataTable, T> getResults)
        {
            return RunScript(PostgresScript, parametersAction, new Func<DataSet, T>((ds) => getResults(ds.Tables[0])));
        }

        /// <summary>
        /// Runs the script async and gets the first table returned.
        /// </summary>
        /// <typeparam name="T">The data type of the returned object.</typeparam>
        /// <param name="PostgresScript">The script to run.</param>
        /// <param name="parametersAction">An action detailing any modifications to the PostgresParameterCollection object.
        /// Use this for adding parameters to the request.</param>
        /// <param name="getResults">A function that takes the results of the query and returns the indicated output.</param>
        /// <returns></returns>
        protected async Task<T> RunScriptAsync<T>(string PostgresScript, Action<NpgsqlParameterCollection> parametersAction, Func<DataTable, T> getResults)
        {
            return await Task.Run(() => RunScript<T>(PostgresScript, parametersAction, getResults));
        }

        /// <summary>
        /// Runs the script and gets the first row in the first table returned.
        /// </summary>
        /// <typeparam name="T">The data type of the returned object.</typeparam>
        /// <param name="PostgresScript">The script to run.</param>
        /// <param name="parametersAction">An action detailing any modifications to the PostgresParameterCollection object.
        /// Use this for adding parameters to the request.</param>
        /// <param name="getResults">A function that takes the results of the query and returns the indicated output.</param>
        /// <returns></returns>
        protected T RunScript<T>(string PostgresScript, Action<NpgsqlParameterCollection> parametersAction, Func<DataRow, T> getResults)
        {
            return RunScript(PostgresScript, parametersAction, new Func<DataTable, T>((dt) => getResults(dt.Rows[0])));
        }

        /// <summary>
        /// Runs the script async and gets the first row in the first table returned.
        /// </summary>
        /// <typeparam name="T">The data type of the returned object.</typeparam>
        /// <param name="PostgresScript">The script to run.</param>
        /// <param name="parametersAction">An action detailing any modifications to the PostgresParameterCollection object.
        /// Use this for adding parameters to the request.</param>
        /// <param name="getResults">A function that takes the results of the query and returns the indicated output.</param>
        /// <returns></returns>
        protected async Task<T> RunScriptAsync<T>(string PostgresScript, Action<NpgsqlParameterCollection> parametersAction, Func<DataRow, T> getResults)
        {
            return await Task.Run(() => RunScript<T>(PostgresScript, parametersAction, getResults));
        }

        /// <summary>
        /// Runs a script and does not get the results.
        /// </summary>
        /// <param name="PostgresScript"></param>
        /// <param name="parametersAction">An action detailing any modifications to the PostgresParameterCollection object.
        /// Use this for adding parameters to the request.</param>
        protected void RunScript(string PostgresScript, Action<NpgsqlParameterCollection> parametersAction)
        {
            RunScript(PostgresScript, parametersAction, (Func<DataSet, object>)null);
        }

        /// <summary>
        /// Runs a script async and does not get the results.
        /// </summary>
        /// <param name="PostgresScript"></param>
        /// <param name="parametersAction">An action detailing any modifications to the PostgresParameterCollection object.
        /// Use this for adding parameters to the request.</param>
        protected async Task RunScriptAsync(string PostgresScript, Action<NpgsqlParameterCollection> parametersAction)
        {
            await Task.Run(() => RunScript(PostgresScript, parametersAction, (Func<DataSet, object>)null));
        }

        /// <summary>
        /// Configures the PostgresCommand object so the CommandType is "Text" and passes in the command text.
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="value"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Review Postgres queries for security vulnerabilities", Justification = "the purpose of value is to be a string that gets executed")]
        protected override void ConfigurePostgresCommand(NpgsqlCommand cmd, string value)
        {
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = value;
        }
    }
}
