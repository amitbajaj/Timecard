Imports System.Windows.Forms

Public Class frmTimeCardMainForm
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

    Private Sub CustomerToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CustomerToolStripMenuItem.Click
        Dim frmCustMaster As frmCustomerMaster
        Dim iChilds As Integer
        Dim bFound As Boolean
        bFound = False
        For iChilds = 0 To MdiChildren.Count - 1
            If MdiChildren(iChilds).Name = "frmCustomerMaster" Then
                bFound = True
                Exit For
            End If
        Next
        If bFound Then
            frmCustMaster = MdiChildren(iChilds)
            frmCustMaster.Activate()
        Else
            frmCustMaster = New frmCustomerMaster
            frmCustMaster.MdiParent = Me
            frmCustMaster.Show()
        End If
    End Sub

    Private Sub CustomerProjectStripMenuItem_Click(sender As Object, e As EventArgs) Handles CustomerProjectStripMenuItem.Click
        Dim frmProjMaster As frmProjectMaster
        Dim iChilds As Integer
        Dim bFound As Boolean
        bFound = False
        For iChilds = 0 To MdiChildren.Count - 1
            If MdiChildren(iChilds).Name = "frmProjectMaster" Then
                bFound = True
                Exit For
            End If
        Next
        If bFound Then
            frmProjMaster = MdiChildren(iChilds)
            frmProjMaster.Activate()
        Else
            frmProjMaster = New frmProjectMaster
            frmProjMaster.MdiParent = Me
            frmProjMaster.Show()
        End If
    End Sub

    Private Sub ProjectTimeCardToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ProjectTimeCardToolStripMenuItem.Click
        Dim frmPrjTmCrdMaster As frmProjectTimeCardMaster
        Dim iChilds As Integer
        Dim bFound As Boolean
        bFound = False
        For iChilds = 0 To MdiChildren.Count - 1
            If MdiChildren(iChilds).Name = "frmProjectTimeCardMaster" Then
                bFound = True
                Exit For
            End If
        Next
        If bFound Then
            frmPrjTmCrdMaster = MdiChildren(iChilds)
            frmPrjTmCrdMaster.Activate()
        Else
            frmPrjTmCrdMaster = New frmProjectTimeCardMaster
            frmPrjTmCrdMaster.MdiParent = Me
            frmPrjTmCrdMaster.Show()
        End If

    End Sub

    Private Sub ProjectTimeCardDetailsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ProjectTimeCardDetailsToolStripMenuItem.Click
        Dim frmProjTimeCard As frmProjectTimeCard
        Dim iChilds As Integer
        Dim bFound As Boolean
        bFound = False
        For iChilds = 0 To MdiChildren.Count - 1
            If MdiChildren(iChilds).Name = "frmProjectTimeCard" Then
                bFound = True
                Exit For
            End If
        Next
        If bFound Then
            frmProjTimeCard = MdiChildren(iChilds)
            frmProjTimeCard.Activate()
        Else
            frmProjTimeCard = New frmProjectTimeCard
            frmProjTimeCard.MdiParent = Me
            frmProjTimeCard.Show()
        End If

    End Sub

    Private Sub ProjectJobsStripMenuItem_Click(sender As Object, e As EventArgs) Handles ProjectJobsStripMenuItem.Click
        Dim frmProjJobs As frmProjectJobs
        Dim iChilds As Integer
        Dim bFound As Boolean
        bFound = False
        For iChilds = 0 To MdiChildren.Count - 1
            If MdiChildren(iChilds).Name = "frmProjectJobs" Then
                bFound = True
                Exit For
            End If
        Next
        If bFound Then
            frmProjJobs = MdiChildren(iChilds)
            frmProjJobs.Activate()
        Else
            frmProjJobs = New frmProjectJobs
            frmProjJobs.MdiParent = Me
            frmProjJobs.Show()
        End If
    End Sub

    Private Sub ProjectPhasesStripMenuItem_Click(sender As Object, e As EventArgs) Handles ProjectPhasesStripMenuItem.Click
        Dim frmProjPhases As frmProjectPhases
        Dim iChilds As Integer
        Dim bFound As Boolean
        bFound = False
        For iChilds = 0 To MdiChildren.Count - 1
            If MdiChildren(iChilds).Name = "frmProjectPhases" Then
                bFound = True
                Exit For
            End If
        Next
        If bFound Then
            frmProjPhases = MdiChildren(iChilds)
            frmProjPhases.Activate()
        Else
            frmProjPhases = New frmProjectPhases
            frmProjPhases.MdiParent = Me
            frmProjPhases.Show()
        End If
    End Sub

    Private Sub UserReportToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UserReportToolStripMenuItem.Click
        Dim oReportForm As frmReport
        Dim iChilds As Integer
        Dim bFound As Boolean
        bFound = False
        For iChilds = 0 To MdiChildren.Count - 1
            If MdiChildren(iChilds).Name = "frmReport" Then
                bFound = True
                Exit For
            End If
        Next
        If bFound Then
            oReportForm = MdiChildren(iChilds)
            oReportForm.Activate()
        Else
            oReportForm = New frmReport
            oReportForm.MdiParent = Me
            oReportForm.Show()
        End If

    End Sub

    Private Sub CustomerReportToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CustomerReportToolStripMenuItem.Click
        Dim oReportForm As New frmTimeCardReport
        Dim iChilds As Integer
        Dim bFound As Boolean
        bFound = False
        For iChilds = 0 To MdiChildren.Count - 1
            If MdiChildren(iChilds).Name = "frmTimeCardReport" Then
                bFound = True
                Exit For
            End If
        Next
        If bFound Then
            oReportForm = MdiChildren(iChilds)
            oReportForm.Activate()
        Else
            oReportForm = New frmTimeCardReport
            oReportForm.MdiParent = Me
            oReportForm.Show()
        End If
    End Sub

    Private Sub CustomerReportMatrixToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CustomerReportMatrixToolStripMenuItem.Click
        Dim oReportForm As New frmTimeCardMatrixReport
        Dim iChilds As Integer
        Dim bFound As Boolean
        bFound = False
        For iChilds = 0 To MdiChildren.Count - 1
            If MdiChildren(iChilds).Name = "frmTimeCardMatrixReport" Then
                bFound = True
                Exit For
            End If
        Next
        If bFound Then
            oReportForm = MdiChildren(iChilds)
            oReportForm.Activate()
        Else
            oReportForm = New frmTimeCardMatrixReport
            oReportForm.MdiParent = Me
            oReportForm.Show()
        End If
    End Sub
End Class
