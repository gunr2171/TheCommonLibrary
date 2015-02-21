using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCL.ProcedureProgram;
using TCL.Extensions;

namespace TCL.ProcedureProgram.Example
{
    class UserInputProvider : UserInputProvider<InputData>
    {
        public override List<string> GetInputFields()
        {
            //get the raw list of fields that need to be displayed.

            return new List<string>()
            {
                "Name",
                "Age",
                "City"
            };
        }

        public override InputData ParseUserInput(Dictionary<string, string> rawInputs)
        {
            //this function needs to parse the inputs from the user and return your InputData object
            
            //do input validation here (like simple "is this an integer")
            //but you need to throw an exception if something does not work. the calling
            //method will catch that exception and halt execution

            //note that the names of the properties don't have to match up with your input fields
            //there is no correlation between them - it's totally up to you to transform the
            //raw input values into an object you want to manage.

            //here I will check if any of the fields are empty and stop if there are
            var anyEmptyFields = rawInputs
                .Any(x=> x.Value.IsNullOrWhiteSpace());

            if(anyEmptyFields)
                throw new Exception("All fields are required");

            return new InputData
            {
                City = rawInputs["City"],
                Age = rawInputs["Age"].Parse<int>(), //the Parse function will error out if the input can't be parsed, which is ok
                PersonName = rawInputs["Name"],
            };
        }
    }
}
