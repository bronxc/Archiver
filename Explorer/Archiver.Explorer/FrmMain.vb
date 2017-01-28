Imports System.Reflection
Public Class FrmMain
    Private Property Provider As Archiver.Provider
    Private Sub FrmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Provider = New Provider
        Me.Details.Columns.Add("Name", Me.Details.ClientRectangle.Width \ 3, HorizontalAlignment.Left)
        Me.Details.Columns.Add("Created", Me.Details.ClientRectangle.Width \ 3, HorizontalAlignment.Left)
        Me.Details.Columns.Add("Type", Me.Details.ClientRectangle.Width \ 3, HorizontalAlignment.Left)
    End Sub
    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        Me.Provider.CreateNew("root")
        Me.TreeviewUpdate(Me.ArchiveTree, Me.Provider, True)
    End Sub
    Private Sub btnAddFile_Click(sender As Object, e As EventArgs) Handles btnAddFile.Click
        Using ofd As New OpenFileDialog With {.Title = "Select file",
                                              .Multiselect = False,
                                              .InitialDirectory = Application.StartupPath}
            If (ofd.ShowDialog(Me) = Windows.Forms.DialogResult.OK) Then
                BackgroundWorker.Run(Sub()
                                         Me.ButtonsEnabled(False)
                                         Me.Provider.AddFile(ofd.FileName)
                                         Me.TreeviewUpdate(Me.ArchiveTree, Me.Provider, True)
                                         Me.ButtonsEnabled(True)
                                     End Sub)
            End If
        End Using
    End Sub
    Private Sub btnAddDirectory_Click(sender As Object, e As EventArgs) Handles btnAddDirectory.Click
        Using ofb As New FolderBrowserDialog With {.Description = "Select folder",
                                                   .SelectedPath = Application.StartupPath}
            If (ofb.ShowDialog(Me) = Windows.Forms.DialogResult.OK) Then
                BackgroundWorker.Run(Sub()
                                         Me.ButtonsEnabled(False)
                                         Me.Provider.AddDirectory(ofb.SelectedPath)
                                         Me.TreeviewUpdate(Me.ArchiveTree, Me.Provider, True)
                                         Me.ButtonsEnabled(True)
                                     End Sub)
            End If
        End Using
    End Sub
    Private Sub btnOpen_Click(sender As Object, e As EventArgs) Handles btnOpen.Click
        Using ofd As New OpenFileDialog With {.Title = "Select archive",
                                              .Multiselect = False,
                                              .Filter = "Arc files|*.arc",
                                              .InitialDirectory = Application.StartupPath}
            If (ofd.ShowDialog(Me) = Windows.Forms.DialogResult.OK) Then
                BackgroundWorker.Run(Sub()
                                         Me.ButtonsEnabled(False)
                                         Me.Provider.Open(ofd.FileName)
                                         Me.TreeviewUpdate(Me.ArchiveTree, Me.Provider, True)
                                         Me.ButtonsEnabled(True)
                                     End Sub)
            End If
        End Using
    End Sub
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Using ofd As New SaveFileDialog With {.Title = "Save As",
                                              .Filter = "Arc file|*.arc",
                                              .InitialDirectory = Application.StartupPath}
            If (ofd.ShowDialog(Me) = Windows.Forms.DialogResult.OK) Then
                BackgroundWorker.Run(Sub()
                                         Me.ButtonsEnabled(False)
                                         Me.Provider.SaveAs(ofd.FileName, True, IO.Compression.CompressionLevel.Optimal)
                                         Me.ButtonsEnabled(True)
                                     End Sub)
            End If
        End Using
    End Sub
    Private Sub btnExtracAll_Click(sender As Object, e As EventArgs) Handles btnExtracAll.Click
        If (Me.Provider IsNot Nothing AndAlso Me.Provider.Root IsNot Nothing) Then
            Using ofb As New FolderBrowserDialog With {.Description = "Select folder",
                                                  .SelectedPath = Application.StartupPath}
                If (ofb.ShowDialog(Me) = Windows.Forms.DialogResult.OK) Then
                    BackgroundWorker.Run(Sub()
                                             Me.ButtonsEnabled(False)
                                             Me.Provider.Extract(ofb.SelectedPath, Me.Provider.Root)
                                             Me.ButtonsEnabled(True)
                                         End Sub)
                End If
            End Using
        End If
    End Sub
    Private Sub ArchiveTree_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles ArchiveTree.AfterSelect
        Dim result As New List(Of Archiver.Entity)
        If (Me.Provider.Root.FindByGuid(e.Node.Tag.ToString, result)) Then
            Me.Details.Items.Clear()
            Me.UpdateDetails(result.First)
        Else
            Me.Details.Items.Clear()
        End If
    End Sub
    Private Sub UpdateDetails(selected As Archiver.Entity)

        Dim lvitem As ListViewItem = Me.Details.Items.Add(selected.Name)
        lvitem.SubItems.Add(selected.Created.ToString)
        lvitem.SubItems.Add(selected.Type.ToString)

        If (selected.Type = EntityType.Root) Then
            For Each e As Archiver.Entity In CType(selected, Archiver.Root).Content
                lvitem = Me.Details.Items.Add(String.Format("..\{0}", e.Name))
                lvitem.SubItems.Add(e.Created.ToString)
                lvitem.SubItems.Add(e.Type.ToString)
            Next
        ElseIf (selected.Type = EntityType.Directory) Then
            For Each e As Archiver.Entity In CType(selected, Archiver.Directory).Content
                lvitem = Me.Details.Items.Add(String.Format("..\{0}", e.Name))
                lvitem.SubItems.Add(e.Created.ToString)
                lvitem.SubItems.Add(e.Type.ToString)
            Next
        End If
    End Sub
    Private Sub ButtonsEnabled(bool As Boolean)
        If (Me.btnNew.InvokeRequired) Then
            Me.btnNew.Invoke(Sub() Me.ButtonsEnabled(bool))
        Else
            Me.btnNew.Enabled = bool
            Me.btnAddFile.Enabled = bool
            Me.btnAddDirectory.Enabled = bool
            Me.btnOpen.Enabled = bool
            Me.btnSave.Enabled = bool
            Me.btnExtracAll.Enabled = bool
        End If
    End Sub
    Private Sub TreeviewUpdate(ctrl As TreeView, provider As Archiver.Provider, clear As Boolean)
        If (ctrl.InvokeRequired) Then
            ctrl.Invoke(Sub() Me.TreeviewUpdate(ctrl, provider, clear))
        Else
            If (provider IsNot Nothing AndAlso provider.Root IsNot Nothing) Then
                With ctrl
                    If (clear) Then
                        .Nodes.Clear()
                        Me.Details.Items.Clear()
                    End If
                    .BeginUpdate()
                    .Nodes.Add(Me.TreeviewCreateNodes(provider.Root))
                    .ExpandAll()
                    .EndUpdate()
                End With
            End If

        End If
    End Sub
    Private Function TreeviewCreateNodes(base As Archiver.Entity) As TreeNode
        Dim node As New TreeNode(base.Name) With {.Tag = base.Guid}
        Select Case base.Type
            Case EntityType.Root
                For Each Entity As Entity In CType(base, Root).Content
                    node.Nodes.Add(Me.TreeviewCreateNodes(Entity))
                Next
            Case EntityType.Directory
                For Each Entity As Entity In CType(base, Directory).Content
                    node.Nodes.Add(Me.TreeviewCreateNodes(Entity))
                Next
        End Select
        Return node
    End Function
End Class
