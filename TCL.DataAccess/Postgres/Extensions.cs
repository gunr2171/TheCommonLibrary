using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCL.DataAccess.Postgres
{
    public static class Extensions
    {
        /// <summary>
        /// Adds a parameter to the parameter collection with it's accompanying value.
        /// If the value passed in is null it will be converted to DBNull.Value.
        /// </summary>
        /// <param name="collection">The collection to add the parameter to.</param>
        /// <param name="parameterName">The name of the parameter. Like ":ParamName". REMEMBER, the prefix for Postgres parameters are different than others!</param>
        /// <param name="value">The value of the parameter.</param>
        public static void AddParam(this NpgsqlParameterCollection collection, string parameterName, object value)
        {
            if (value == null)
            {
                collection.AddWithValue(parameterName, DBNull.Value);
            }
            else
            {
                collection.AddWithValue(parameterName, value);
            }
        }
    }
}
