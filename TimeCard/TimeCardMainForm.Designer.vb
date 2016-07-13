<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class TimeCardMainForm
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
        Me.UserMasterToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TimeCardMasterToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TimeCardDetailsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.StatusStrip = New System.Windows.Forms.StatusStrip()
        Me.ToolStripStatusLabel = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.MenuStrip.SuspendLayout()
        Me.StatusStrip.SuspendLayout()
        Me.SuspendLayout()
        '
        'MenuStrip
        '
        Me.MenuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.UserMasterToolStripMenuItem, Me.TimeCardMasterToolStripMenuItem, Me.TimeCardDetailsToolStripMenuItem})
        Me.MenuStrip.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip.Name = "MenuStrip"
        Me.MenuStrip.Size = New System.Drawing.Size(766, 24)
        Me.MenuStrip.TabIndex = 5
        Me.MenuStrip.Text = "MenuStrip"
        '
        'UserMasterToolStripMenuItem
        '
        Me.UserMasterToolStripMenuItem.Name = "UserMasterToolStripMenuItem"
        Me.UserMasterToolStripMenuItem.Size = New System.Drawing.Size(81, 20)
        Me.UserMasterToolStripMenuItem.Text = "&User Master"
        '
        'TimeCardMasterToolStripMenuItem
        '
        Me.TimeCardMasterToolStripMenuItem.Name = "TimeCardMasterToolStripMenuItem"
        Me.TimeCardMasterToolStripMenuItem.Size = New System.Drawing.Size(110, 20)
        Me.TimeCardMasterToolStripMenuItem.Text = "&TimeCard Master"
        '
        'TimeCardDetailsToolStripMenuItem
        '
        Me.TimeCardDetailsToolStripMenuItem.Name = "TimeCardDetailsToolStripMenuItem"
        Me.TimeCardDetailsToolStripMenuItem.Size = New System.Drawing.Size(109, 20)
        Me.TimeCardDetailsToolStripMenuItem.Text = "TimeCard &Details"
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
        'TimeCardMainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(766, 453)
        Me.Controls.Add(Me.MenuStrip)
        Me.Controls.Add(Me.StatusStrip)
        Me.IsMdiContainer = True
        Me.MainMenuStrip = Me.MenuStrip
        Me.Name = "TimeCardMainForm"
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
    Friend WithEvents UserMasterToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents TimeCardMasterToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents TimeCardDetailsToolStripMenuItem As ToolStripMenuItem
End Class
