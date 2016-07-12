Imports System.Windows.Forms

Public Class TimeCardMainForm
    Private Sub UserMasterToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UserMasterToolStripMenuItem.Click
        Dim frmUM As frmUserMaster
        Dim iChilds As Integer
        Dim bFound As Boolean
        bFound = False
        For iChilds = 0 To MdiChildren.Count - 1
            If MdiChildren(iChilds).Name = "frmUserMaster" Then
                bFound = True
                Exit For
            End If
        Next
        If bFound Then
            frmUM = MdiChildren(iChilds)
            frmUM.Activate()
        Else
            frmUM = New frmUserMaster
            frmUM.MdiParent = Me
            frmUM.Show()
        End If

    End Sub

    Private Sub TimeCardMasterToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TimeCardMasterToolStripMenuItem.Click
        Dim frmTCM As frmTimeCardMaster
        Dim iChilds As Integer
        Dim bFound As Boolean
        bFound = False
        For iChilds = 0 To MdiChildren.Count - 1
            If MdiChildren(iChilds).Name = "frmTimeCardMaster" Then
                bFound = True
                Exit For
            End If
        Next
        If bFound Then
            frmTCM = MdiChildren(iChilds)
            frmTCM.Activate()
        Else
            frmTCM = New frmTimeCardMaster
            frmTCM.MdiParent = Me
            frmTCM.Show()
        End If

    End Sub

    Private Sub TimeCardDetailsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TimeCardDetailsToolStripMenuItem.Click
        Dim frmTCD As frmTimeCardDetails
        Dim iChilds As Integer
        Dim bFound As Boolean
        bFound = False
        For iChilds = 0 To MdiChildren.Count - 1
            If MdiChildren(iChilds).Name = "frmTimeCardDetails" Then
                bFound = True
                Exit For
            End If
        Next
        If bFound Then
            frmTCD = MdiChildren(iChilds)
            frmTCD.Activate()
        Else
            frmTCD = New frmTimeCardDetails
            frmTCD.MdiParent = Me
            frmTCD.Show()
        End If

    End Sub

    Private Sub TimeCardMainForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.WindowState = FormWindowState.Maximized
    End Sub
End Class
