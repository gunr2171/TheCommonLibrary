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
    /// Base class for any common connection to an Postgres database.
    /// </summary>
    public abstract class PostgresAccessorBase
    {
        /// <summary>
        /// Gets the connection string that will be used for the Postgres connection.
        /// </summary>
        public string ConnectionString { get; private set; }

        /// <summary>
        /// Creates a new PostgresAccessorBase object with the given connection string.
        /// </summary>
        /// <param name="cs"></param>
        public PostgresAccessorBase(string cs)
        {
            if (string.IsNullOrWhiteSpace(cs))
                throw new ArgumentNullException("cs", "Connection string null or empty");

            ConnectionString = cs;
        }

        /// <summary>
        /// Use to configure the PostgresCommand object before use.
        /// </summary>
        /// <param name="cmd">The PostgresCommand object used for this request.</param>
        /// <param name="value">The command value.</param>
        protected abstract void ConfigurePostgresCommand(NpgsqlCommand cmd, string value);

        /// <summary>
        /// Runs a script and uses the full dataset of what is returned.
        /// </summary>
        /// <typeparam name="T">The data type of the returned object.</typeparam>
        /// <param name="value">The string to run on the server. What this is, is defined by higher classes.</param>
        /// <param name="getResults">A function that takes the results of the query and converts it to the output data type.</param>
        /// <returns></returns>
        protected T RunCommand<T>(string value, Func<DataSet, T> getResults)
        {
            using (DataSet ds = new DataSet())
            {
                using (var conn = new NpgsqlConnection(ConnectionString))
                {
                    conn.Open();
                    using (NpgsqlTransaction trans = conn.BeginTransaction())
                    {
                        try
                        {
                            using (NpgsqlCommand cmd = conn.CreateCommand())
                            {
                                cmd.Transaction = trans;

                                ConfigurePostgresCommand(cmd, value);

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
                            throw; // CA2200 preserve stack details, shield at caller
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
        /// <param name="value">The string to run on the server. What this is, is defined by higher classes.</param>
        /// <param name="getResults">A function that takes the results of the query and converts it to the output data type.</param>
        /// <returns></returns>
        protected async Task<T> RunCommandAsync<T>(string value, Func<DataSet, T> getResults)
        {
            return await Task.Run(() => RunCommand<T>(value, getResults));
        }
    }
}
