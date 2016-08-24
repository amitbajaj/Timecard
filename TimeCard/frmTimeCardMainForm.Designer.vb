<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmTimeCardMainForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub


    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.MenuStrip = New System.Windows.Forms.MenuStrip()
        Me.UserMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.UserMasterToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TimeCardMasterToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TimeCardDetailsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CustomerMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CustomerToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CustomerProjectStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ProjectPhasesStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ProjectJobsStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ProjectTimeCardToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ProjectTimeCardDetailsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ReportsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.UserReportToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CustomerReportToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.StatusStrip = New System.Windows.Forms.StatusStrip()
        Me.ToolStripStatusLabel = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.CustomerReportMatrixToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuStrip.SuspendLayout()
        Me.StatusStrip.SuspendLayout()
        Me.SuspendLayout()
        '
        'MenuStrip
        '
        Me.MenuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.UserMenuItem, Me.CustomerMenuItem, Me.ReportsToolStripMenuItem})
        Me.MenuStrip.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip.Name = "MenuStrip"
        Me.MenuStrip.Size = New System.Drawing.Size(766, 24)
        Me.MenuStrip.TabIndex = 5
        Me.MenuStrip.Text = "MenuStrip"
        '
        'UserMenuItem
        '
        Me.UserMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.UserMasterToolStripMenuItem, Me.TimeCardMasterToolStripMenuItem, Me.TimeCardDetailsToolStripMenuItem})
        Me.UserMenuItem.Name = "UserMenuItem"
        Me.UserMenuItem.Size = New System.Drawing.Size(47, 20)
        Me.UserMenuItem.Text = "Users"
        '
        'UserMasterToolStripMenuItem
        '
        Me.UserMasterToolStripMenuItem.Name = "UserMasterToolStripMenuItem"
        Me.UserMasterToolStripMenuItem.Size = New System.Drawing.Size(165, 22)
        Me.UserMasterToolStripMenuItem.Text = "&User Master"
        '
        'TimeCardMasterToolStripMenuItem
        '
        Me.TimeCardMasterToolStripMenuItem.Name = "TimeCardMasterToolStripMenuItem"
        Me.TimeCardMasterToolStripMenuItem.Size = New System.Drawing.Size(165, 22)
        Me.TimeCardMasterToolStripMenuItem.Text = "&TimeCard Master"
        '
        'TimeCardDetailsToolStripMenuItem
        '
        Me.TimeCardDetailsToolStripMenuItem.Name = "TimeCardDetailsToolStripMenuItem"
        Me.TimeCardDetailsToolStripMenuItem.Size = New System.Drawing.Size(165, 22)
        Me.TimeCardDetailsToolStripMenuItem.Text = "TimeCard &Details"
        '
        'CustomerMenuItem
        '
        Me.CustomerMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CustomerToolStripMenuItem, Me.CustomerProjectStripMenuItem, Me.ProjectPhasesStripMenuItem, Me.ProjectJobsStripMenuItem, Me.ProjectTimeCardToolStripMenuItem, Me.ProjectTimeCardDetailsToolStripMenuItem})
        Me.CustomerMenuItem.Name = "CustomerMenuItem"
        Me.CustomerMenuItem.Size = New System.Drawing.Size(76, 20)
        Me.CustomerMenuItem.Text = "Customers"
        '
        'CustomerToolStripMenuItem
        '
        Me.CustomerToolStripMenuItem.Name = "CustomerToolStripMenuItem"
        Me.CustomerToolStripMenuItem.Size = New System.Drawing.Size(204, 22)
        Me.CustomerToolStripMenuItem.Text = "&Customer Master"
        '
        'CustomerProjectStripMenuItem
        '
        Me.CustomerProjectStripMenuItem.Name = "CustomerProjectStripMenuItem"
        Me.CustomerProjectStripMenuItem.Size = New System.Drawing.Size(204, 22)
        Me.CustomerProjectStripMenuItem.Text = "Customer &Project"
        '
        'ProjectPhasesStripMenuItem
        '
        Me.ProjectPhasesStripMenuItem.Name = "ProjectPhasesStripMenuItem"
        Me.ProjectPhasesStripMenuItem.Size = New System.Drawing.Size(204, 22)
        Me.ProjectPhasesStripMenuItem.Text = "Project Phases"
        '
        'ProjectJobsStripMenuItem
        '
        Me.ProjectJobsStripMenuItem.Name = "ProjectJobsStripMenuItem"
        Me.ProjectJobsStripMenuItem.Size = New System.Drawing.Size(204, 22)
        Me.ProjectJobsStripMenuItem.Text = "Project Jobs"
        '
        'ProjectTimeCardToolStripMenuItem
        '
        Me.ProjectTimeCardToolStripMenuItem.Name = "ProjectTimeCardToolStripMenuItem"
        Me.ProjectTimeCardToolStripMenuItem.Size = New System.Drawing.Size(204, 22)
        Me.ProjectTimeCardToolStripMenuItem.Text = "Project TimeCard"
        '
        'ProjectTimeCardDetailsToolStripMenuItem
        '
        Me.ProjectTimeCardDetailsToolStripMenuItem.Name = "ProjectTimeCardDetailsToolStripMenuItem"
        Me.ProjectTimeCardDetailsToolStripMenuItem.Size = New System.Drawing.Size(204, 22)
        Me.ProjectTimeCardDetailsToolStripMenuItem.Text = "Project TimeCard Details"
        '
        'ReportsToolStripMenuItem
        '
        Me.ReportsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.UserReportToolStripMenuItem, Me.CustomerReportToolStripMenuItem, Me.CustomerReportMatrixToolStripMenuItem})
        Me.ReportsToolStripMenuItem.Name = "ReportsToolStripMenuItem"
        Me.ReportsToolStripMenuItem.Size = New System.Drawing.Size(59, 20)
        Me.ReportsToolStripMenuItem.Text = "Reports"
        '
        'UserReportToolStripMenuItem
        '
        Me.UserReportToolStripMenuItem.Name = "UserReportToolStripMenuItem"
        Me.UserReportToolStripMenuItem.Size = New System.Drawing.Size(208, 22)
        Me.UserReportToolStripMenuItem.Text = "User Report"
        '
        'CustomerReportToolStripMenuItem
        '
        Me.CustomerReportToolStripMenuItem.Name = "CustomerReportToolStripMenuItem"
        Me.CustomerReportToolStripMenuItem.Size = New System.Drawing.Size(208, 22)
        Me.CustomerReportToolStripMenuItem.Text = "Customer Report"
        '
        'StatusStrip
        '
        Me.StatusStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripStatusLabel})
        Me.StatusStrip.Location = New System.Drawing.Point(0, 431)
        Me.StatusStrip.Name = "StatusStrip"
        Me.StatusStrip.Size = New System.Drawing.Size(766, 22)
        Me.StatusStrip.TabIndex = 7
        Me.StatusStrip.Text = "StatusStrip"
        '
        'ToolStripStatusLabel
        '
        Me.ToolStripStatusLabel.Name = "ToolStripStatusLabel"
        Me.ToolStripStatusLabel.Size = New System.Drawing.Size(39, 17)
        Me.ToolStripStatusLabel.Text = "Status"
        '
        'CustomerReportMatrixToolStripMenuItem
        '
        Me.CustomerReportMatrixToolStripMenuItem.Name = "CustomerReportMatrixToolStripMenuItem"
        Me.CustomerReportMatrixToolStripMenuItem.Size = New System.Drawing.Size(208, 22)
        Me.CustomerReportMatrixToolStripMenuItem.Text = "Customer Report (Matrix)"
        '
        'frmTimeCardMainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(766, 453)
        Me.Controls.Add(Me.MenuStrip)
        Me.Controls.Add(Me.StatusStrip)
        Me.IsMdiContainer = True
        Me.MainMenuStrip = Me.MenuStrip
        Me.Name = "frmTimeCardMainForm"
        Me.Text = "TimeCardMainForm"
        Me.MenuStrip.ResumeLayout(False)
        Me.MenuStrip.PerformLayout()
        Me.StatusStrip.ResumeLayout(False)
        Me.StatusStrip.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolTip As System.Windows.Forms.ToolTip
    Friend WithEvents ToolStripStatusLabel As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents StatusStrip As System.Windows.Forms.StatusStrip
    Friend WithEvents MenuStrip As System.Windows.Forms.MenuStrip
    Friend WithEvents UserMenuItem As ToolStripMenuItem
    Friend WithEvents CustomerMenuItem As ToolStripMenuItem
    Friend WithEvents CustomerProjectStripMenuItem As ToolStripMenuItem
    Friend WithEvents UserMasterToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents TimeCardMasterToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents TimeCardDetailsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents CustomerToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ProjectTimeCardToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ProjectTimeCardDetailsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ProjectJobsStripMenuItem As ToolStripMenuItem
    Friend WithEvents ProjectPhasesStripMenuItem As ToolStripMenuItem
    Friend WithEvents ReportsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents UserReportToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents CustomerReportToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents CustomerReportMatrixToolStripMenuItem As ToolStripMenuItem
End Class
