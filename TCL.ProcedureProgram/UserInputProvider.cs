using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCL.ProcedureProgram
{
    /// <summary>
    /// Provides the list of user input fields and a way to convert those fields into the specified data object.
    /// </summary>
    /// <typeparam name="TInputData"></typeparam>
    public abstract class UserInputProvider<TInputData>
    {
        /// <summary>
        /// Returns the names of the fields that the user must input.
        /// </summary>
        /// <returns></returns>
        public abstract List<string> GetInputFields();

        /// <summary>
        /// Given the raw user input, parse it into the output data object
        /// </summary>
        /// <param name="rawInputs"></param>
        /// <returns></returns>
        public abstract TInputData ParseUserInput(Dictionary<string, string> rawInputs);
    }
}
