using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCL.CommandLine;
using TCL.Extensions;

namespace TCL.CommandLine.Example
{
    /// <summary>
    /// The actual logic for the program. This will be called automatically
    /// by the framework and the settings will be pre-parsed (if successful).
    /// </summary>
    public class ExampleProgram : CommandLineProgram<Options>
    {
        /// <summary>
        /// The for for this program to run.
        /// </summary>
        /// <param name="options">The command line options passed in by the user.
        /// This method will only be called if the options are correctly parsed.</param>
        /// <returns></returns>
        protected override int DoWork(Options options)
        {
            // all this program is going to do is move any files found
            // in the pickup directory into the dropoff directory

            var pickupDir = new DirectoryInfo(options.PickupDirectoryPath);
            var dropoffDir = new DirectoryInfo(options.DropoffDirectoryPath);

            var filesToMove = pickupDir.EnumerateFiles();

            foreach (var fileToMove in filesToMove)
            {
                var newPath = Path.Combine(options.DropoffDirectoryPath, fileToMove.Name);
                fileToMove.MoveTo(newPath);
            }

            //return the exit code
            return 0;
        }
    }
}
