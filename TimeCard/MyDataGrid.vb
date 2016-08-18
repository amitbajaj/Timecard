Public Class MyDataGrid
    Public Event RemoveRow(sender As Object, e As EventArgs)
    Public Event CellEndEdit(sender As Object, e As DataGridViewCellEventArgs)

    Public Sub New()
        InitializeComponent()
        With DGVData
            .AllowUserToAddRows = False
            .AllowUserToDeleteRows = False
            .AllowUserToOrderColumns = False
            .AllowUserToResizeColumns = True
            .AllowUserToResizeRows = False
            .Rows.Clear()
            .Columns.Clear()
        End With
    End Sub

    Public Sub AddColumn(sColName As String, sDisplayName As String, bReadOnly As Boolean, sColType As String)
        Dim oCol As New DataGridViewTextBoxColumn
        oCol.Name = sColName
        oCol.HeaderText = sDisplayName
        oCol.SortMode = DataGridViewColumnSortMode.NotSortable
        oCol.ReadOnly = bReadOnly
        DGVData.Columns.Add(oCol)
        oCol = Nothing
    End Sub

    Public Sub FinalizeGridColumns()
        Dim col2 As New DataGridViewButtonColumn

        col2 = New DataGridViewButtonColumn()
        col2.Name = "addNewRow"
        col2.Text = "A"
        col2.HeaderText = "A"
        col2.UseColumnTextForButtonValue = True
        col2.SortMode = DataGridViewColumnSortMode.NotSortable
        col2.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader
        DGVData.Columns.Add(col2)

        col2 = New DataGridViewButtonColumn()
        col2.Name = "delCurRow"
        col2.Text = "D"
        col2.HeaderText = "D"
        col2.UseColumnTextForButtonValue = True
        col2.SortMode = DataGridViewColumnSortMode.NotSortable
        col2.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader
        DGVData.Columns.Add(col2)

    End Sub

    Private Sub DGVData_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGVData.CellContentClick
        If e.RowIndex >= 0 Then
            If e.ColumnIndex = DGVData.Columns("addNewRow").Index Then
                DGVData.Rows.Add()
            ElseIf e.ColumnIndex = DGVData.Columns("delCurRow").Index Then
                RaiseEvent RemoveRow(sender, e)
            End If
        End If
    End Sub

    Private Sub DGVData_KeyDown(sender As Object, e As KeyEventArgs) Handles DGVData.KeyDown
        If e.KeyCode = Asc(vbCr) Then
            With DGVData
                If .CurrentCell.ColumnIndex < .ColumnCount - 2 Then
                    .CurrentCell = .Rows.Item(.CurrentCell.RowIndex).Cells.Item(.CurrentCell.ColumnIndex + 1)
                ElseIf .CurrentCell.ColumnIndex = .ColumnCount - 1 Then
                    RaiseEvent RemoveRow(sender, e)
                ElseIf .CurrentCell.RowIndex = .Rows.Count - 1 And .CurrentCell.ColumnIndex = .ColumnCount - 2 Then
                    .Rows.Add()
                    .CurrentCell = .Rows.Item(.Rows.Count - 1).Cells.Item(1)
                ElseIf .CurrentCell.RowIndex < .Rows.Count - 1 Then
                    .CurrentCell = .Rows.Item(.CurrentCell.RowIndex + 1).Cells.Item(1)
                End If
                e.Handled = True
            End With
        End If
    End Sub

    Private Sub DGVData_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles DGVData.CellEndEdit
        RaiseEvent CellEndEdit(sender, e)
    End Sub
End Class
