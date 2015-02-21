using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCL.ProcedureProgram.Logging;

namespace TCL.ProcedureProgram
{
    /// <summary>
    /// A procedure to run by the program.
    /// </summary>
    /// <typeparam name="TInputData"></typeparam>
    public abstract class Procedure<TInputData>
    {
        /// <summary>
        /// Determines if this procedure is appropriate for the given foil data. Does not do any work.
        /// Returns text indicating why the procedure is incorrect, or null if the procedure is correct.
        /// </summary>
        /// <param name="inputData">The data provided by the user.</param>
        /// <param name="lm">The logging manager.</param>
        /// <returns></returns>
        public abstract string IsCorrectForThisProcedure(TInputData inputData, LoggingManager lm);

        /// <summary>
        /// The main logic for the procedure.
        /// </summary>
        /// <param name="inputData">The data provided by the user.</param>
        /// <param name="lm">The logging manager.</param>
        public abstract void RunProcedure(TInputData inputData, LoggingManager lm);

        /// <summary>
        /// Gets the human readable name of the procedure.
        /// </summary>
        /// <returns></returns>
        public abstract string GetProcedureName();
    }
}
