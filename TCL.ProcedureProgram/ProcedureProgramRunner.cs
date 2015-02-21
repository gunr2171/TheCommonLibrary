using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TCL.ProcedureProgram.FormControls;

namespace TCL.ProcedureProgram
{
    /// <summary>
    /// Main entry point for the Procedure program.
    /// </summary>
    /// <typeparam name="TUserInputProvider">The type that will provide the user input fields and parsing.</typeparam>
    /// <typeparam name="TInputData">The object that user input will be parsed into and sent to the procedures.</typeparam>
    public class ProcedureProgramRunner<TUserInputProvider, TInputData>
        where TUserInputProvider : UserInputProvider<TInputData>, new()
    {
        private frmProcedureInterface<TUserInputProvider, TInputData> form;

        /// <summary>
        /// Creates a new ProcedureProgramRunner with a given name and file path to a help file.
        /// </summary>
        /// <param name="procedureProgramName">The name of the program. Will be displayed as the form's title.</param>
        /// <param name="htmlHelpFilePath">A path to the html file to display when a user clicks the help button. Can be left null if there is no file.</param>
        public ProcedureProgramRunner(string procedureProgramName, string htmlHelpFilePath)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            form = new frmProcedureInterface<TUserInputProvider,TInputData>(procedureProgramName, htmlHelpFilePath);
        }

        /// <summary>
        /// Adds a procedure to the list of procedures to run for this program.
        /// </summary>
        /// <typeparam name="TProcedure">The type of procedure.</typeparam>
        public void AddProcedure<TProcedure>()
            where TProcedure : Procedure<TInputData>, new()
        {
            form.AddProcedure<TProcedure>();
        }

        /// <summary>
        /// Launches the Procedure Interface form which will control all future actions. 
        /// Make sure you call this AFTER adding the procedures you want to use.
        /// </summary>
        public void RunProcedureProgram()
        {
            form.ShowDialog();
        }
    }
}
