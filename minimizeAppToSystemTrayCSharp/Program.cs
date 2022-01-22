using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace minimizeAppToSystemTrayCSharp
{
    class Program
    {

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);
        [DllImport("user32.dll")]
        private static extern IntPtr GetShellWindow();
        [DllImport("user32.dll")]
        private static extern IntPtr GetDesktopWindow();

        private static NotifyIcon notifyIcon;
        public static IntPtr processHandle;
        public static IntPtr WinShell;
        public static IntPtr WinDesktop;
        public static MenuItem HideMenu;
        public static MenuItem RestoreMenu;

        static void Main(string[] args)
        {
            notifyIcon = new NotifyIcon();
            notifyIcon.Icon = SystemIcons.Application;
            notifyIcon.Text = "Monitor";
            notifyIcon.Visible = true;

            var menu = new ContextMenu();
            HideMenu = new MenuItem("Hide", new EventHandler(Minimize_Click));
            RestoreMenu = new MenuItem("Restore", new EventHandler(Maximize_Click));
            menu.MenuItems.Add(RestoreMenu);
            menu.MenuItems.Add(HideMenu);
            menu.MenuItems.Add(new MenuItem("Exit", new EventHandler(CleanExit)));
            notifyIcon.ContextMenu = menu;

            // You need to spin off your actual work in a different thread so that the Notify Icon works correctly
            Task.Factory.StartNew(new Action(Run));
            processHandle = Process.GetCurrentProcess().MainWindowHandle;
            WinShell = GetShellWindow();
            WinDesktop = GetDesktopWindow();

            // Hide the Window
            // ResizeWindow(false);
            RestoreMenu.Enabled = false;
            /// This is required for triggering WinForms activity in Console app
            Application.Run();
        }

        private static void Run()
        {
            int integer = 0;
            Console.WriteLine("Listening to messages");
            while (true)
            {
                Console.WriteLine("Current status: " + integer);
                System.Threading.Thread.Sleep(1000);
                integer += 1;
            }
        }

        private static void CleanExit(object sender, EventArgs e)
        {
            notifyIcon.Visible = false;
            Application.Exit();
            Environment.Exit(1);
        }

        private static void Minimize_Click(object sender, EventArgs e)
        {
            ResizeWindow(false);
        }

        private static void Maximize_Click(object sender, EventArgs e)
        {
            ResizeWindow();
        }

        private static void ResizeWindow(bool Restore = true)
        {
            if (Restore)
            {
                new Authentication().Show();
            }
            else
            {
                RestoreMenu.Enabled = true;
                HideMenu.Enabled = false;
                SetParent(processHandle, WinShell);
            }
        }
    }
}
