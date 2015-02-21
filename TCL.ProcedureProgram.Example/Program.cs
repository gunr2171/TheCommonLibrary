using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using TCL.ProcedureProgram;

namespace TCL.ProcedureProgram.Example
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //this object will run the procedure program
            //the generic types are used for determining what the user input is and how to parse it.
            var runner = new ProcedureProgramRunner<UserInputProvider, InputData>("Example Procedure Program", "helpfile.html");

            //add in the procedures
            runner.AddProcedure<Procedure1>();
            runner.AddProcedure<Procedure2>();

            //run it
            runner.RunProcedureProgram();
        }
    }
}
