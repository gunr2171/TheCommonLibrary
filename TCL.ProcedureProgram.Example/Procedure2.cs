using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCL.ProcedureProgram;
using TCL.Extensions;
using System.Threading;

namespace TCL.ProcedureProgram.Example
{
    class Procedure2 : Procedure<InputData>
    {
        //for more explanations see the comments in the other procedure

        public override string IsCorrectForThisProcedure(InputData inputData, Logging.LoggingManager lm)
        {
            //only go if the name starts with a letter between O and Z
            var firstLetterOfName = inputData.PersonName.ToLower().First();

            var isCorrect = firstLetterOfName >= 'o' && firstLetterOfName <= 'z';

            if (!isCorrect)
                return "Starting letter {0} is not in the right range 'A-N'".FormatInline(firstLetterOfName);

            //passes check
            return null;
        }

        public override void RunProcedure(InputData inputData, Logging.LoggingManager lm)
        {
            var numbers = Enumerable.Range(10, 5);

            foreach (var num in numbers)
            {
                Thread.Sleep(1000);
                lm.AddUILogMessage("#" + num, Logging.UILoggingEntryType.Information);
            }
        }

        public override string GetProcedureName()
        {
            return "O-Z Names Procedure";
        }
    }
}
