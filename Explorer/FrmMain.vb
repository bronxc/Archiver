Imports Archiver.Entities
Public Class FrmMain
    Private Property Proxy As Proxy
    Private Property Provider As Provider
    Private Sub FrmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Proxy = New Proxy(New Provider)
        AddHandler Me.Proxy.BeforeCall, AddressOf Me.DebugProxyBeforeCall
        AddHandler Me.Proxy.AfterCall, AddressOf Me.DebugProxyAfterCall
        AddHandler Me.Proxy.ExceptionCaught, AddressOf Me.DebugProxyException

        Me.Provider = Me.Proxy.Wrap(Of Provider)()
        AddHandler Me.Provider.Status, AddressOf Me.ProviderStatus
        AddHandler Me.Provider.ProgressionRead, AddressOf Me.ProviderProgress
        AddHandler Me.Provider.ProgressionWrite, AddressOf Me.ProviderProgress

        Me.lvDetails.Columns.Add("Entry", Me.lvDetails.ClientRectangle.Width \ 2, HorizontalAlignment.Center)
        Me.lvDetails.Columns.Add("Details", Me.lvDetails.ClientRectangle.Width \ 2, HorizontalAlignment.Left)

    End Sub
    Private Sub NewToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MenuFileNew.Click
        Me.Provider.Run(Sub() Me.Provider.Create("Entrypoint"))
        Me.TreeviewUpdate(Me.ArchiveTree, Me.Provider, True)
    End Sub
    Private Sub SaveToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MenuFileSave.Click
        Using ofd As New SaveFileDialog With {.Title = "Save As",
                                            .Filter = "Arc file|*.arc",
                                            .InitialDirectory = Application.StartupPath}
            If (ofd.ShowDialog(Me) = Windows.Forms.DialogResult.OK) Then
                BackgroundWorker.Run(Sub()
                                         Me.GuiState(False)
                                         Me.Provider.Run(Sub() Me.Provider.Save(ofd.FileName, True))
                                         Me.GuiState(True)
                                     End Sub)
            End If
        End Using
    End Sub
    Private Sub OpenToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MenuFileOpen.Click
        Using ofd As New OpenFileDialog With {.Title = "Select archive",
                                              .Multiselect = False,
                                              .Filter = "Arc files|*.arc",
                                              .InitialDirectory = Application.StartupPath}
            If (ofd.ShowDialog(Me) = Windows.Forms.DialogResult.OK) Then
                BackgroundWorker.Run(Sub()
                                         Me.GuiState(False)
                                         Me.Provider.Run(Sub() Me.Provider.Open(ofd.FileName))
                                         Me.TreeviewUpdate(Me.ArchiveTree, Me.Provider, True)
                                         Me.GuiState(True)
                                     End Sub)
            End If
        End Using
    End Sub
    Private Sub menuAddFile_Click(sender As Object, e As EventArgs) Handles menuAddFile.Click
        Using ofd As New OpenFileDialog With {.Title = "Select file",
                                              .Multiselect = False,
                                              .InitialDirectory = Application.StartupPath}
            If (ofd.ShowDialog(Me) = Windows.Forms.DialogResult.OK) Then
                BackgroundWorker.Run(Sub()
                                         Me.GuiState(False)
                                         Me.Provider.Run(Sub() Me.Provider.AddFile(ofd.FileName))
                                         Me.TreeviewUpdate(Me.ArchiveTree, Me.Provider, True)
                                         Me.GuiState(True)
                                     End Sub)
            End If
        End Using
    End Sub
    Private Sub menuAddFilder_Click(sender As Object, e As EventArgs) Handles menuAddFolder.Click
        Using ofb As New FolderBrowserDialog With {.Description = "Select folder",
                                                   .SelectedPath = Application.StartupPath}
            If (ofb.ShowDialog(Me) = Windows.Forms.DialogResult.OK) Then
                BackgroundWorker.Run(Sub()
                                         Me.GuiState(False)
                                         Me.Provider.Run(Sub() Me.Provider.AddDirectory(ofb.SelectedPath))
                                         Me.TreeviewUpdate(Me.ArchiveTree, Me.Provider, True)
                                         Me.GuiState(True)
                                     End Sub)
            End If
        End Using
    End Sub
    Private Sub menuExtractAll_Click(sender As Object, e As EventArgs) Handles menuExtractAll.Click
        If (Me.Provider.Entrypoint IsNot Nothing) Then
            Using ofb As New FolderBrowserDialog With {.Description = "Select folder",
                                                       .SelectedPath = Application.StartupPath}
                If (ofb.ShowDialog(Me) = Windows.Forms.DialogResult.OK) Then
                    BackgroundWorker.Run(Sub()
                                             Me.GuiState(False)
                                             Me.Provider.Run(Sub() Me.Provider.Extract(ofb.SelectedPath, Me.Provider.Entrypoint))
                                             Me.GuiState(True)
                                         End Sub)
                End If
            End Using
        End If
    End Sub
    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MenuFileExit.Click
        Application.Exit()
    End Sub
    Private Sub menuExpandAll_Click(sender As Object, e As EventArgs) Handles menuExpandAll.Click
        Me.ArchiveTree.ExpandAll()
    End Sub
    Private Sub menuCollapseAll_Click(sender As Object, e As EventArgs) Handles menuCollapseAll.Click
        Me.ArchiveTree.CollapseAll()
        If (Me.ArchiveTree.Nodes.Count > 0) Then
            Me.ArchiveTree.Nodes(0).Expand()
        End If
    End Sub
    Private Sub lvDetails_MouseClick(sender As Object, e As MouseEventArgs) Handles lvDetails.MouseClick
        If (e.Button = Windows.Forms.MouseButtons.Right) Then
            If (Me.lvDetails.SelectedItems IsNot Nothing AndAlso Me.lvDetails.SelectedItems.Count > 0) Then
                Me.mneQuickMenu.Show(Me.lvDetails.PointToScreen(e.Location))
            End If
        End If
    End Sub
    Private Sub lvDetails_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles lvDetails.MouseDoubleClick
        If (Me.lvDetails.SelectedItems IsNot Nothing AndAlso TypeOf Me.lvDetails.SelectedItems(0).Tag Is String) Then
            Dim results As New List(Of Entity)
            Dim guid As String = Me.lvDetails.SelectedItems(0).Tag.ToString
            If (Me.Provider.Run(Function() Me.Provider.Entrypoint.FindByGuid(guid, results))) Then
                Using frmedit As New frmEditor() With {.Target = results.First}
                    frmedit.ShowDialog(Me)
                    Me.TreeviewUpdate(Me.ArchiveTree, Me.Provider, True)
                End Using
            End If
        End If
    End Sub
    Private Sub RemoveToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RemoveToolStripMenuItem.Click
        If (Me.lvDetails.SelectedItems IsNot Nothing AndAlso TypeOf Me.lvDetails.SelectedItems(0).Tag Is String) Then
            Dim results As New List(Of Entity)
            Dim guid As String = Me.lvDetails.SelectedItems(0).Tag.ToString
            If (Me.Provider.Run(Function() Me.Provider.Entrypoint.FindByGuid(guid, results))) Then
                If (results.First.Type = EntityType.Entrypoint) Then
                    If (Me.ShowMessage("Confirm deletion: All files!") = DialogResult.OK) Then
                        Me.MenuFileNew.PerformClick()
                    End If
                ElseIf (results.First.Type = EntityType.Directory) Then
                    If (Me.ShowMessage(String.Format("Confirm deletion: '{0}'", results.First.Name)) = DialogResult.OK) Then
                        results.First.Delete()
                        Me.TreeviewUpdate(Me.ArchiveTree, Me.Provider, True)
                    End If
                ElseIf (results.First.Type = EntityType.File) Then
                    If (Me.ShowMessage(String.Format("Confirm deletion: '{0}'", results.First.Name)) = DialogResult.OK) Then
                        results.First.Delete()
                        Me.TreeviewUpdate(Me.ArchiveTree, Me.Provider, True)
                    End If
                End If
            End If
        End If
    End Sub
    Private Sub ArchiveTree_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles ArchiveTree.AfterSelect
        Dim result As New List(Of Archiver.Entities.Entity)
        If (Me.Provider.Run(Function() Me.Provider.Entrypoint.FindByGuid(e.Node.Tag.ToString, result))) Then
            Me.UpdateListDetails(result.First)
        Else
            Me.lvDetails.Items.Clear()
        End If
    End Sub
    Private Sub ProviderStatus(Current As String)
        If (Me.StatusStrip.InvokeRequired) Then
            Me.StatusStrip.Invoke(Sub() Me.ProviderStatus(Current))
        Else
            Me.labelStatus.Text = String.Format("Status: {0}", Current)
        End If
    End Sub
    Private Sub ProviderProgress(Position As Int64, Total As Int64, Elapsed As TimeSpan)
        If (Me.StatusStrip.InvokeRequired) Then
            Me.StatusStrip.Invoke(Sub() Me.ProviderProgress(Position, Total, Elapsed))
        Else
            If (Total = 0) Then
                Me.ProgressBar.Value = 0
                Me.WriteLog(String.Format("Performance: {0}", Elapsed.ToString), Color.DarkBlue)
            Else
                Me.ProgressBar.Maximum = Convert.ToInt32(Total)
                Me.ProgressBar.Value = Convert.ToInt32(Position)
            End If
        End If
    End Sub
    Private Sub UpdateListDetails(e As Entity)
        Dim lvitem As ListViewItem = Nothing
        Me.lvDetails.Items.Clear()
        If (e.Type = EntityType.Entrypoint) Then
            For Each subEntity As Entity In CType(e, Entrypoint).Content
                lvitem = Me.lvDetails.Items.Add(subEntity.Name, Me.GetImageFromEntity(subEntity))
                lvitem.Tag = subEntity.Guid
                lvitem.SubItems.Add(subEntity.ToString)
            Next
        ElseIf (e.Type = EntityType.Directory) Then
            For Each subEntity As Entity In CType(e, Directory).Content
                lvitem = Me.lvDetails.Items.Add(subEntity.Name, Me.GetImageFromEntity(subEntity))
                lvitem.Tag = subEntity.Guid
                lvitem.SubItems.Add(subEntity.ToString)
            Next
        ElseIf (e.Type = EntityType.File) Then
            lvitem = Me.lvDetails.Items.Add(e.Name, Me.GetImageFromEntity(e))
            lvitem.Tag = e.Guid
            lvitem.SubItems.Add(e.ToString)
        End If
    End Sub
    Private Function GetImageFromEntity(e As Entity) As Integer
        If (e.Type = EntityType.Directory) Then Return 1 Else Return 2
    End Function
    Private Sub WriteLog(Message As String, color As Color)
        If (Me.rtbDebugLog.InvokeRequired) Then
            Me.rtbDebugLog.Invoke(Sub() Me.WriteLog(Message, color))
        Else
            Me.rtbDebugLog.SelectionStart = Me.rtbDebugLog.TextLength
            Me.rtbDebugLog.SelectionLength = 0
            Me.rtbDebugLog.SelectionColor = color
            Me.rtbDebugLog.AppendText(String.Format("[{0}] {1} {2}", DateTime.Now.ToShortTimeString, Message, ControlChars.CrLf))
            Me.rtbDebugLog.SelectionColor = Me.rtbDebugLog.ForeColor
            Me.rtbDebugLog.ScrollToCaret()
        End If
    End Sub
    Private Sub GuiState(bool As Boolean)
        If (Me.InvokeRequired) Then
            Me.Invoke(Sub() Me.GuiState(bool))
        Else
            Me.MenuStrip.Enabled = bool
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
                If (TypeOf base Is Entrypoint) Then
                    For Each Entity As Entity In CType(base, Entrypoint).Content
                        node.Nodes.Add(Me.TreeviewCreateNodes(Entity))
                    Next
                End If
            Case EntityType.Directory
                If (TypeOf base Is Directory) Then
                    For Each Entity As Entity In CType(base, Directory).Content
                        node.Nodes.Add(Me.TreeviewCreateNodes(Entity))
                    Next
                End If
        End Select
        Return node
    End Function
End Class
