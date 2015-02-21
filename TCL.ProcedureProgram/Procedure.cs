using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCL.ProcedureProgram.Logging;

namespace TCL.ProcedureProgram
{
    public abstract class Procedure<TInputData>
    {
        /// <summary>
        /// Determines if this procedure is appropriate for the given foil data. Does not do any work.
        /// Returns text indicating why the procedure is incorrect, or null if the procedure is correct.
        /// </summary>
        /// <param name="topBarcodeData"></param>
        /// <param name="foilIdOnFoil"></param>
        /// <param name="newFoilId"></param>
        /// <param name="wafermapFileContents"></param>
        /// <returns></returns>
        public abstract string IsCorrectForThisProcedure(TInputData inputData, LoggingManager lm);

        public abstract void RunProcedure(TInputData inputData, LoggingManager lm);

        public abstract string GetProcedureName();
    }
}
