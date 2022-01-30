Imports System.Windows.Forms

Public Class Authentication

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        TextBox1.PasswordChar = "*"
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        If TextBox1.Text = "1234" Then
            RestoreMenu.Enabled = False
            HideMenu.Enabled = True
            Close()
            ShowWindow(WinDesktop, SW_SHOW)
        Else
            MessageBox.Show("Invalid authentication")
        End If

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Close()
    End Sub
End Class