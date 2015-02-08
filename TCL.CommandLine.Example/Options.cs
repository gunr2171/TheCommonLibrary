using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCL.CommandLine;

namespace TCL.CommandLine.Example
{
    /// <summary>
    /// The command line options passed into the program for the program to use.
    /// </summary>
    public class Options : CommandLineOptions
    {
        /// <summary>
        /// The path of the directory to pick up files
        /// </summary>
        [Option("pickup-dir", Required = true, HelpText = "the path of the directory to pick up files")]
        public string PickupDirectoryPath { get; set; }

        /// <summary>
        /// The path of the directory to drop off files
        /// </summary>
        [Option("drop-off-dir", Required = true, HelpText = "the path of the directory to drop off files")]
        public string DropoffDirectoryPath { get; set; }
    }
}
