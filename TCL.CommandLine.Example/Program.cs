using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCL.CommandLine;

namespace TCL.CommandLine.Example
{
    class Program
    {
        static int Main(string[] args)
        {
            return CommandLineProgramRunner.RunProgram<ExampleProgram, Options>(args);
        }
    }
}
