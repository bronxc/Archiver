Imports System.Reflection
Imports Archiver.Entities

Public Class FrmMain
    Private Property Provider As Archiver.Provider
    Private Sub FrmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Provider = New Provider
        AddHandler Me.Provider.Searching, AddressOf Me.ProviderSearching
        AddHandler Me.Provider.ProgressionRead, AddressOf Me.ProviderProgress
        AddHandler Me.Provider.ProgressionWrite, AddressOf Me.ProviderProgress


        Me.lvDetails.Columns.Add("Name", Me.lvDetails.ClientRectangle.Width \ 3, HorizontalAlignment.Left)
        Me.lvDetails.Columns.Add("Type", Me.lvDetails.ClientRectangle.Width \ 3, HorizontalAlignment.Left)
        Me.lvDetails.Columns.Add("Details", Me.lvDetails.ClientRectangle.Width \ 3, HorizontalAlignment.Left)
    End Sub
    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        Me.Provider.Create("Entrypoint")
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
        If (Me.Provider IsNot Nothing AndAlso Me.Provider.Entrypoint IsNot Nothing) Then
            Using ofb As New FolderBrowserDialog With {.Description = "Select folder",
                                                       .SelectedPath = Application.StartupPath}
                If (ofb.ShowDialog(Me) = Windows.Forms.DialogResult.OK) Then
                    BackgroundWorker.Run(Sub()
                                             Me.ButtonsEnabled(False)
                                             Me.Provider.Extract(ofb.SelectedPath, Me.Provider.Entrypoint)
                                             Me.ButtonsEnabled(True)
                                         End Sub)
                End If
            End Using
        End If
    End Sub
    Private Sub btnTreeShow_Click(sender As Object, e As EventArgs) Handles btnTreeShow.Click
        Me.ArchiveTree.ExpandAll()
    End Sub
    Private Sub btnTreeHide_Click(sender As Object, e As EventArgs) Handles btnTreeHide.Click
        Me.ArchiveTree.CollapseAll()
        If (Me.ArchiveTree.Nodes.Count > 0) Then
            Me.ArchiveTree.Nodes(0).Expand()
        End If
    End Sub
    Private Sub ArchiveTree_MouseClick(sender As Object, e As MouseEventArgs) Handles ArchiveTree.MouseClick
        If (e.Button = Windows.Forms.MouseButtons.Right) Then
            If (Me.ArchiveTree.SelectedNode IsNot Nothing) Then
                Me.mneQuickMenu.Show(Me.ArchiveTree.PointToScreen(e.Location))
            End If
        End If
    End Sub
    Private Sub ExtractToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExtractToolStripMenuItem.Click

    End Sub
    Private Sub EditToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EditToolStripMenuItem.Click

    End Sub
    Private Sub RemoveToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RemoveToolStripMenuItem.Click
        If (TypeOf Me.ArchiveTree.SelectedNode.Tag Is String) Then
            Dim results As New List(Of Entity)
            Dim guid As String = Me.ArchiveTree.SelectedNode.Tag.ToString
            If (Me.Provider.Entrypoint.FindByGuid(guid, results)) Then
                If (results.First.Type = EntityType.Entrypoint) Then
                    If (Me.AreYouSure("Confirm deletion: Everything!") = DialogResult.OK) Then
                        Me.btnNew.PerformClick()
                    End If
                ElseIf (results.First.Type = EntityType.Directory) Then
                    If (Me.AreYouSure(String.Format("Confirm deletion: '{0}'", results.First.Name)) = DialogResult.OK) Then
                        results.First.Delete()
                        Me.TreeviewUpdate(Me.ArchiveTree, Me.Provider, True)
                    End If
                ElseIf (results.First.Type = EntityType.File) Then
                    If (Me.AreYouSure(String.Format("Confirm deletion: '{0}'", results.First.Name)) = DialogResult.OK) Then
                        results.First.Delete()
                        Me.TreeviewUpdate(Me.ArchiveTree, Me.Provider, True)
                    End If
                End If
            End If
        End If
    End Sub
    Private Sub ArchiveTree_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles ArchiveTree.AfterSelect
        Dim result As New List(Of Archiver.Entities.Entity)
        If (Me.Provider.Entrypoint.FindByGuid(e.Node.Tag.ToString, result)) Then
            Me.lvDetails.Items.Clear()
            Me.UpdateDetails(result.First)
        Else
            Me.lvDetails.Items.Clear()
        End If
    End Sub
    Private Sub ProviderSearching(Current As String)
        If (Me.StatusStrip.InvokeRequired) Then
            Me.StatusStrip.Invoke(Sub() Me.ProviderSearching(Current))
        Else
            Me.StripStatusText.Text = Current
        End If
    End Sub
    Private Sub ProviderProgress(Read As Long, Total As Long)
        If (Me.StatusStrip.InvokeRequired) Then
            Me.StatusStrip.Invoke(Sub() Me.ProviderProgress(Read, Total))
        Else
            If (Total = 0) Then
                Me.ToolStripProgressBar.Value = 0
            Else
                Me.ToolStripProgressBar.Maximum = Convert.ToInt32(Total)
                Me.ToolStripProgressBar.Value = Convert.ToInt32(Read)
            End If
        End If
    End Sub
    Private Sub UpdateDetails(selected As Archiver.Entities.Entity)

        Dim lvitem As ListViewItem = Me.lvDetails.Items.Add(selected.Name)
        lvitem.SubItems.Add(selected.Type.ToString)
        lvitem.SubItems.Add(selected.ToString)

        If (selected.Type = EntityType.Entrypoint) Then
            For Each e As Entity In CType(selected, Entrypoint).Content
                lvitem = Me.lvDetails.Items.Add(String.Format("- {0}", e.Name))
                lvitem.SubItems.Add(e.Type.ToString)
                lvitem.SubItems.Add(e.ToString)
            Next
        ElseIf (selected.Type = EntityType.Directory) Then
            For Each e As Entity In CType(selected, Directory).Content
                lvitem = Me.lvDetails.Items.Add(String.Format("- {0}", e.Name))
                lvitem.SubItems.Add(e.Type.ToString)
                lvitem.SubItems.Add(e.ToString)
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
            Me.btnTreeHide.Enabled = bool
            Me.btnTreeShow.Enabled = bool
        End If
    End Sub
    Private Sub TreeviewUpdate(ctrl As TreeView, provider As Provider, clear As Boolean)
        If (ctrl.InvokeRequired) Then
            ctrl.Invoke(Sub() Me.TreeviewUpdate(ctrl, provider, clear))
        Else
            If (provider IsNot Nothing AndAlso provider.Entrypoint IsNot Nothing) Then
                With ctrl
                    If (clear) Then
                        .Nodes.Clear()
                        Me.lvDetails.Items.Clear()
                    End If
                    .BeginUpdate()
                    .Nodes.Add(Me.TreeviewCreateNodes(provider.Entrypoint))
                    If (.Nodes.Count > 0) Then
                        .Nodes(0).Expand()
                        .SelectedNode = .Nodes(0)
                    End If
                    .EndUpdate()
                End With
            End If

        End If
    End Sub
    Private Function TreeviewCreateNodes(base As Entity) As TreeNode
        Dim node As New TreeNode(base.Name) With {.Tag = base.Guid}
        Select Case base.Type
            Case EntityType.Entrypoint
                For Each Entity As Entity In CType(base, Entrypoint).Content
                    node.Nodes.Add(Me.TreeviewCreateNodes(Entity))
                Next
            Case EntityType.Directory
                For Each Entity As Entity In CType(base, Directory).Content
                    node.Nodes.Add(Me.TreeviewCreateNodes(Entity))
                Next
        End Select
        Return node
    End Function
    Private Function AreYouSure(Message As String) As DialogResult
        Return MessageBox.Show(Message, "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation)
    End Function
End Class
