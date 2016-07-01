<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmReport
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
        Me.rptViewTimeCard = New CrystalDecisions.Windows.Forms.CrystalReportViewer()
        Me.rptTimeCard1 = New TimeCard.rptTimeCard()
        Me.SuspendLayout()
        '
        'rptViewTimeCard
        '
        Me.rptViewTimeCard.ActiveViewIndex = 0
        Me.rptViewTimeCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.rptViewTimeCard.Cursor = System.Windows.Forms.Cursors.Default
        Me.rptViewTimeCard.Dock = System.Windows.Forms.DockStyle.Fill
        Me.rptViewTimeCard.Location = New System.Drawing.Point(0, 0)
        Me.rptViewTimeCard.Name = "rptViewTimeCard"
        Me.rptViewTimeCard.ReportSource = Me.rptTimeCard1
        Me.rptViewTimeCard.Size = New System.Drawing.Size(777, 477)
        Me.rptViewTimeCard.TabIndex = 0
        '
        'frmReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(777, 477)
        Me.Controls.Add(Me.rptViewTimeCard)
        Me.Name = "frmReport"
        Me.Text = "frmReport"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents rptViewTimeCard As CrystalDecisions.Windows.Forms.CrystalReportViewer
    Friend WithEvents rptTimeCard1 As rptTimeCard
End Class
