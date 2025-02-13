﻿using System.Diagnostics;
using System.Runtime.InteropServices;

// Start visual studio as administrator
namespace HelloWorld
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Replace with the title of the window you want to hide
            string windowTitle = Console.Title = "Hello, World!";
            IntPtr hWnd = NativeMethods.FindWindow(null, windowTitle);
            HideShowWindow();

            Process[] processes = Process.GetProcessesByName("VsDebugConsole");
            if (processes.Length > 0)
            {
                Process proc = processes[0];
                hWnd = proc.MainWindowHandle;
                HideShowWindow();
            }
            else
            {
                Console.WriteLine("Process not found.");
            }

            Console.WriteLine("Hello, World!");
            void HideShowWindow()
            {
                if (hWnd != IntPtr.Zero)
                {
                    // Hide the window
                    NativeMethods.ShowWindow(hWnd, ShowWindowCommands.SW_HIDE);
                    Console.WriteLine($"The window '{windowTitle}' is now hidden.");
                    NativeMethods.ShowWindow(hWnd, ShowWindowCommands.SW_SHOW);
                    Console.WriteLine($"The window '{windowTitle}' is now shown.");
                }
                else
                {
                    Console.WriteLine($"Window with title '{windowTitle}' not found.");
                }
            }
        }
    }
    public static class ShowWindowCommands
    {
        public const int SW_HIDE = 0;
        public const int SW_SHOW = 5;
    }
    public static partial class NativeMethods
    {
        [LibraryImport("user32.dll", StringMarshalling = StringMarshalling.Utf16, EntryPoint = "FindWindowW")]
        public static partial IntPtr FindWindow(string? lpClassName, string lpWindowName);

        [LibraryImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static partial bool ShowWindow(IntPtr hWnd, int nCmdShow);
    }
}
