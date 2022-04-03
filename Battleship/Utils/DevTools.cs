using System.Diagnostics;
using System.Globalization;

namespace Utils;

public class DevTools
{
    /// <summary>
    /// Doxygen generates descriptions for public functions
    /// </summary>
    public static void GitAddAllAndCommit()
    {
        var executeProcess = (string command, string argument) =>
        {
            var proc = new Process 
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = command,
                    Arguments = argument,
                    RedirectStandardOutput = true,
                }
            };
            proc.Start();
            string output = "";
            while (!proc.StandardOutput.EndOfStream)
            {
                output = proc.StandardOutput.ReadLine() ?? "unexpected";
            }
            proc.WaitForExit();
            return output;
        };
        var currentTime = $"{DateTime.Now.ToString(CultureInfo.CreateSpecificCulture("en-GB"))}";
        executeProcess("git", @"add -A");
        Console.WriteLine(executeProcess("git", $@"commit -m ""program executed at {currentTime}"""));
    }
}