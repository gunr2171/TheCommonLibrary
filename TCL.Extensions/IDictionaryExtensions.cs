using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCL.Extensions
{
    /// <summary>
    /// Extensions for IDictionary objects.
    /// </summary>
    public static class IDictionaryExtensions
    {
        /// <summary>
        /// Gets the value from the given key, or the default value if the key can't be found.
        /// </summary>
        /// <typeparam name="TKey">The data type of the key.</typeparam>
        /// <typeparam name="TValue">The data type of the value.</typeparam>
        /// <param name="dictionary">The dictionary to search through.</param>
        /// <param name="key">The key to search for.</param>
        /// <param name="defaultValue">If the key can't be found in the dictionary, this value will be returned.</param>
        /// <returns></returns>
        public static TValue GetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue defaultValue)
        {
            TValue outValue;
            if (dictionary.TryGetValue(key, out outValue))
            {
                return outValue;
            }
            else
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// Gets the value from the given key, or null if the key can't be found.
        /// </summary>
        /// <typeparam name="TKey">The data type of the key.</typeparam>
        /// <typeparam name="TValue">The data type of the value.</typeparam>
        /// <param name="dictionary">The dictionary to search through.</param>
        /// <param name="key">The key to search for.</param>
        /// <returns></returns>
        public static TValue GetValueOrNull<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
            where TValue : class
        {
            return dictionary.GetValueOrDefault(key, null);
        }
    }
}
