﻿Imports System.Drawing
Imports System.Runtime.InteropServices
Imports System.Windows.Forms

Module Module1

    <DllImport("user32.dll", SetLastError:=True)>
    Public Function SetParent(ByVal hWndChild As IntPtr, ByVal hWndNewParent As IntPtr) As IntPtr
    End Function

    <DllImport("user32.dll")>
    Private Function GetShellWindow() As IntPtr
    End Function

    <DllImport("user32.dll")>
    Private Function GetDesktopWindow() As IntPtr
    End Function

    Private notifyIcon As NotifyIcon
    Public processHandle As IntPtr
    Public WinShell As IntPtr
    Public WinDesktop As IntPtr
    Public HideMenu As MenuItem
    Public RestoreMenu As MenuItem

    Sub Main()
        notifyIcon = New NotifyIcon()
        notifyIcon.Icon = SystemIcons.Application
        notifyIcon.Text = "Monitor"
        notifyIcon.Visible = True
        Dim menu = New ContextMenu()
        HideMenu = New MenuItem("Hide", New EventHandler(AddressOf Minimize_Click))
        RestoreMenu = New MenuItem("Restore", New EventHandler(AddressOf Maximize_Click))
        menu.MenuItems.Add(RestoreMenu)
        menu.MenuItems.Add(HideMenu)
        menu.MenuItems.Add(New MenuItem("Exit", New EventHandler(AddressOf CleanExit)))
        notifyIcon.ContextMenu = menu

        ' You need to spin off your actual work in a different thread so that the Notify Icon works correctly
        Call Task.Factory.StartNew(New Action(AddressOf Run))
        processHandle = Process.GetCurrentProcess().MainWindowHandle
        WinShell = GetShellWindow()
        WinDesktop = GetDesktopWindow()

        ' Hide the Window
        ' ResizeWindow(false);
        RestoreMenu.Enabled = False
        ' This is required for triggering WinForms activity in Console app
        Call Application.Run()
    End Sub

    Private Sub Run()
        Dim [integer] = 0
        Console.WriteLine("Listening to messages")

        While True
            Console.WriteLine("Current status: " & [integer])
            Threading.Thread.Sleep(1000)
            [integer] += 1
        End While
    End Sub

    Private Sub CleanExit(ByVal sender As Object, ByVal e As EventArgs)
        notifyIcon.Visible = False
        Call Application.Exit()
        Environment.Exit(1)
    End Sub

    Private Sub Minimize_Click(ByVal sender As Object, ByVal e As EventArgs)
        ResizeWindow(False)
    End Sub

    Private Sub Maximize_Click(ByVal sender As Object, ByVal e As EventArgs)
        Call ResizeWindow()
    End Sub

    Private Sub ResizeWindow(ByVal Optional Restore As Boolean = True)
        If Restore Then
            Dim auth As New Authentication
            auth.Show()

        Else
            RestoreMenu.Enabled = True
            HideMenu.Enabled = False
            SetParent(processHandle, WinShell)
        End If
    End Sub

End Module
