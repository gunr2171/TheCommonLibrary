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
    class Procedure1 : Procedure<InputData>
    {
        public override string IsCorrectForThisProcedure(InputData inputData, Logging.LoggingManager lm)
        {
            //this function determines if the input data is "appropriate" for this procedure.
            //use this method to check if your inputs are legit values and to see if values exist in a DB, file system, or elsewhere.
            //you need to return the error string message for why the input data did not pass the test.
            //this text will be displayed to the user. if the data did pass the test then return "null".

            //remember to make your testing strict. if 2 or more procedures can handle the input data then
            //the program will not know which procedure to run and stop execution.

            //the logging manger is passed along in case you want to log anything


            //only go if the name starts with a letter between A and N
            var firstLetterOfName = inputData.PersonName.ToLower().First();

            var isCorrect = firstLetterOfName >= 'a' && firstLetterOfName <= 'n';

            if (!isCorrect)
                return "Starting letter {0} is not in the right range 'A-N'".FormatInline(firstLetterOfName);

            //passes check
            return null;

            //with this example, we are checking A-N and O-Z. If a name does not start with a letter (like a number, space, or other character)
            //then the program will not be able to find a procedure to run, stopping the program. you can use this to your advantage.
            //load up these methods will a lot of input validation (to verify data is legit) so that it clearly obvious if data is inputted incorrectly
            //or nothing needs to be done for it.
        }

        public override void RunProcedure(InputData inputData, Logging.LoggingManager lm)
        {
            //you can log messages to the UI and/or the log file. The entry type for UI only changes the color, it doesn't do anything else.
            lm.AddUILogMessage("Going to begin", Logging.UILoggingEntryType.Warning, true); //true means you will also send this message to the lot file
            lm.AddFileLogMessage("File-only message");

            var numbers = Enumerable.Range(0, 5);

            foreach(var num in numbers)
            {
                Thread.Sleep(1000);
                lm.AddUILogMessage("#" + num, Logging.UILoggingEntryType.Information);
            }

            lm.AddUILogMessage("Done!", Logging.UILoggingEntryType.Success);
        }

        public override string GetProcedureName()
        {
            //this is a human-readable name of the procedure, and is displayed on the UI.
            return "A-N Names Procedure";
        }
    }
}
