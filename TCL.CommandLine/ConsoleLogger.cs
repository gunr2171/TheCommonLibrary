using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCL.Extensions;

namespace TCL.CommandLine
{
    /// <summary>
    /// Helper class to write formatted and colored messages to the Console screen.
    /// </summary>
    public class ConsoleLogger
    {
        /// <summary>
        /// Logs the line with the default "info" color.
        /// </summary>
        /// <param name="message">The message to display to the screen.</param>
        public static void LogLine(string message)
        {
            LogLine(message, LogType.Info);
        }

        /// <summary>
        /// Logs the line with a given type. The type will dictate the output color.
        /// </summary>
        /// <param name="message">The message to display to the screen.</param>
        /// <param name="lineType">The type of message.</param>
        public static void LogLine(string message, LogType lineType)
        {
            var lineColor = lineType.GetAttributeValue<LogColorAttribute, ConsoleColor>(x => x.TextColor);

            var lastColor = Console.ForegroundColor;

            Console.ForegroundColor = lineColor;
            Console.WriteLine(message);

            Console.ForegroundColor = lastColor;
        }

        /// <summary>
        /// Formats the given exception and prints it to the Console window. It includes all exception messages and the stack trace.
        /// </summary>
        /// <param name="ex">The message to display.</param>
        public static void LogException(Exception ex)
        {
            LogLine(ex.Message, LogType.Error);
            LogLine(ex.StackTrace, LogType.Error);

            if (ex.InnerException != null)
            {
                LogException(ex.InnerException);
            }
        }

        /// <summary>
        /// An enum that describes what the type of message this is.
        /// </summary>
        public enum LogType
        {
            [LogColor(ConsoleColor.White)]
            Info,

            [LogColor(ConsoleColor.Gray)]
            Detail,

            [LogColor(ConsoleColor.Red)]
            Error,

            [LogColor(ConsoleColor.Yellow)]
            Warning,

            [LogColor(ConsoleColor.Green)]
            Success,
        }

        /// <summary>
        /// Used to store the font color for a particular LogType.
        /// </summary>
        internal class LogColorAttribute : System.Attribute
        {
            public ConsoleColor TextColor { get; private set; }

            public LogColorAttribute(ConsoleColor textColor)
            {
                TextColor = textColor;
            }
        }
    }
}
