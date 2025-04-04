using System;
using System.ComponentModel;
using System.Diagnostics;

void main()
{
    string answer;
    int numAnswer;
    bool run = true;
    // Run program while true
    do
    {
        Console.WriteLine("Program created by MechaSoau\n" +
            "1. Get all running processes\n" +
            "2. Search for process\n" +
            "3. Kill process\n" +
            "4. Exit\n" +
            "Input: ");
        answer = Console.ReadLine();
        // Make sure user answer is not empty 
        while(string.IsNullOrEmpty(answer))
        {
            Console.WriteLine("Your input can not be empty\n");
            answer = Console.ReadLine();
        }
        // Convert user answer into an int 
        numAnswer = Convert.ToInt32(answer);
        // Switch statement 
        switch(numAnswer)
        {
            // Users selection wants to get all running processes 
            case 1:
                GetProcess();
                break;
            // Users selection wants to get process by name
            case 2:
                GetByName();
                break;
            // User wants to kill processes by name 
            case 3:
                KillProcesses();
                break;
            // user is exiting program
            case 4:
                run = false;
                break;
        }

    }while (run);
}
main();
void GetProcess()
{
    // Try to get all processes running
    try
    {
        Process[] process = Process.GetProcesses();
        // List all processes
        foreach (Process p in process)
        {
            Console.WriteLine(p.ToString());
        }
    }
    // Catch statements for errors 
    catch(ArgumentException ex)
    {
        Console.WriteLine(ex.Message + "\n");
    }
    catch(PlatformNotSupportedException ex)
    {
        Console.WriteLine(ex.Message + "\n");
    }
    catch(InvalidOperationException ex)
    {
        Console.WriteLine(ex.Message + "\n");
    }
    catch(Win32Exception ex)
    {
        Console.WriteLine(ex.Message + "\n");
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message + "\n");
    }
}

void GetByName()
{
    string processName;
    Console.WriteLine("Enter the name of the process you are looking for\n");
    processName = Console.ReadLine();
    // Make sure answer is not empty
    while(string.IsNullOrEmpty(processName))
    {
        Console.WriteLine("You can not have a empty name\n");
        processName = Console.ReadLine();
    }
    // Attempt to pull all processes by name
    try
    {
        Process[] processes = Process.GetProcessesByName(processName);
        // If no processes are found under that input
        if (processes.Length == 0)
        {
            Console.WriteLine("I did not find anything under that name\n");
        }
        // If processes are found list each one that is found 
        foreach (Process p in processes)
        {
            Console.WriteLine(p.ToString());
        }
    }
    // Catch statements for errors 
    catch (ArgumentException ex)
    {
        Console.WriteLine(ex.Message + "\n");
    }
    catch(PlatformNotSupportedException ex)
    {
        Console.WriteLine(ex.Message + "\n");
    }
    catch(Win32Exception ex)
    {
        Console.WriteLine(ex.Message + "\n");
    }
    catch(Exception ex) 
    {
        Console.WriteLine(ex.Message + "\n");
    }
}

static void KillProcesses()
{
    try
    {
        Console.WriteLine("Enter the name of the program that you want to terminate:");
        string killName = Console.ReadLine();

        while (string.IsNullOrWhiteSpace(killName))
        {
            Console.WriteLine("Name of application cannot be empty");
            killName = Console.ReadLine();
        }

        // Get processes by name
        Process[] processes = Process.GetProcessesByName(killName);

        if (processes.Length == 0)
        {
            Console.WriteLine($"No running process found with the name: {killName}");
        }
        else
        {
            foreach (Process p in processes)
            {
                try
                {
                    p.Kill();
                    Console.WriteLine($"Killed {p.ProcessName} (PID: {p.Id})");
                }
                catch (UnauthorizedAccessException)
                {
                    Console.WriteLine($"Access denied: Cannot kill {p.ProcessName}. Try running as Administrator.");
                }
            }
        }
    }
    catch (Win32Exception e)
    {
        Console.WriteLine($"Win32 Error: {e.Message}");
    }
    catch (NotSupportedException e)
    {
        Console.WriteLine($"Not Supported: {e.Message}");
    }
    catch (InvalidOperationException e)
    {
        Console.WriteLine($"Invalid Operation: {e.Message}");
    }
    catch (AggregateException e)
    {
        Console.WriteLine($"Aggregate Exception: {e.Message}");
    }
    catch (Exception e)
    {
        Console.WriteLine($"General Error: {e.Message}");
    }
}
