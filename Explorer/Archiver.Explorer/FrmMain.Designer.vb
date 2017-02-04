<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmMain
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.ArchiveTree = New System.Windows.Forms.TreeView()
        Me.mneQuickMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ExtractToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.EditToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripSeparator()
        Me.RemoveToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.btnOpen = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnNew = New System.Windows.Forms.Button()
        Me.btnAddDirectory = New System.Windows.Forms.Button()
        Me.btnAddFile = New System.Windows.Forms.Button()
        Me.btnExtracAll = New System.Windows.Forms.Button()
        Me.lvDetails = New System.Windows.Forms.ListView()
        Me.btnTreeShow = New System.Windows.Forms.Button()
        Me.btnTreeHide = New System.Windows.Forms.Button()
        Me.StatusStrip = New System.Windows.Forms.StatusStrip()
        Me.ToolStripProgressBar = New System.Windows.Forms.ToolStripProgressBar()
        Me.StripStatusText = New System.Windows.Forms.ToolStripStatusLabel()
        Me.mneQuickMenu.SuspendLayout()
        Me.StatusStrip.SuspendLayout()
        Me.SuspendLayout()
        '
        'ArchiveTree
        '
        Me.ArchiveTree.Font = New System.Drawing.Font("Lucida Console", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ArchiveTree.Location = New System.Drawing.Point(12, 46)
        Me.ArchiveTree.Name = "ArchiveTree"
        Me.ArchiveTree.Size = New System.Drawing.Size(318, 517)
        Me.ArchiveTree.TabIndex = 1
        '
        'mneQuickMenu
        '
        Me.mneQuickMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ExtractToolStripMenuItem, Me.EditToolStripMenuItem, Me.ToolStripMenuItem1, Me.RemoveToolStripMenuItem})
        Me.mneQuickMenu.Name = "mneExtract"
        Me.mneQuickMenu.Size = New System.Drawing.Size(114, 76)
        '
        'ExtractToolStripMenuItem
        '
        Me.ExtractToolStripMenuItem.Name = "ExtractToolStripMenuItem"
        Me.ExtractToolStripMenuItem.Size = New System.Drawing.Size(113, 22)
        Me.ExtractToolStripMenuItem.Text = "Extract"
        '
        'EditToolStripMenuItem
        '
        Me.EditToolStripMenuItem.Name = "EditToolStripMenuItem"
        Me.EditToolStripMenuItem.Size = New System.Drawing.Size(113, 22)
        Me.EditToolStripMenuItem.Text = "Edit"
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(110, 6)
        '
        'RemoveToolStripMenuItem
        '
        Me.RemoveToolStripMenuItem.Name = "RemoveToolStripMenuItem"
        Me.RemoveToolStripMenuItem.Size = New System.Drawing.Size(113, 22)
        Me.RemoveToolStripMenuItem.Text = "Remove"
        '
        'btnOpen
        '
        Me.btnOpen.Location = New System.Drawing.Point(66, 11)
        Me.btnOpen.Name = "btnOpen"
        Me.btnOpen.Size = New System.Drawing.Size(48, 28)
        Me.btnOpen.TabIndex = 2
        Me.btnOpen.Text = "Open"
        Me.btnOpen.UseVisualStyleBackColor = True
        '
        'btnSave
        '
        Me.btnSave.Location = New System.Drawing.Point(120, 11)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(48, 28)
        Me.btnSave.TabIndex = 3
        Me.btnSave.Text = "Save"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'btnNew
        '
        Me.btnNew.Location = New System.Drawing.Point(12, 11)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(48, 28)
        Me.btnNew.TabIndex = 4
        Me.btnNew.Text = "New"
        Me.btnNew.UseVisualStyleBackColor = True
        '
        'btnAddDirectory
        '
        Me.btnAddDirectory.Location = New System.Drawing.Point(410, 11)
        Me.btnAddDirectory.Name = "btnAddDirectory"
        Me.btnAddDirectory.Size = New System.Drawing.Size(68, 29)
        Me.btnAddDirectory.TabIndex = 5
        Me.btnAddDirectory.Text = "+ Directory"
        Me.btnAddDirectory.UseVisualStyleBackColor = True
        '
        'btnAddFile
        '
        Me.btnAddFile.Location = New System.Drawing.Point(336, 11)
        Me.btnAddFile.Name = "btnAddFile"
        Me.btnAddFile.Size = New System.Drawing.Size(68, 29)
        Me.btnAddFile.TabIndex = 7
        Me.btnAddFile.Text = "+ File"
        Me.btnAddFile.UseVisualStyleBackColor = True
        '
        'btnExtracAll
        '
        Me.btnExtracAll.Location = New System.Drawing.Point(795, 10)
        Me.btnExtracAll.Name = "btnExtracAll"
        Me.btnExtracAll.Size = New System.Drawing.Size(104, 29)
        Me.btnExtracAll.TabIndex = 8
        Me.btnExtracAll.Text = "Extract Archive"
        Me.btnExtracAll.UseVisualStyleBackColor = True
        '
        'lvDetails
        '
        Me.lvDetails.Font = New System.Drawing.Font("Lucida Console", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lvDetails.GridLines = True
        Me.lvDetails.Location = New System.Drawing.Point(336, 46)
        Me.lvDetails.MultiSelect = False
        Me.lvDetails.Name = "lvDetails"
        Me.lvDetails.Size = New System.Drawing.Size(563, 517)
        Me.lvDetails.TabIndex = 9
        Me.lvDetails.UseCompatibleStateImageBehavior = False
        Me.lvDetails.View = System.Windows.Forms.View.Details
        '
        'btnTreeShow
        '
        Me.btnTreeShow.Location = New System.Drawing.Point(256, 11)
        Me.btnTreeShow.Name = "btnTreeShow"
        Me.btnTreeShow.Size = New System.Drawing.Size(34, 29)
        Me.btnTreeShow.TabIndex = 10
        Me.btnTreeShow.Text = "+"
        Me.btnTreeShow.UseVisualStyleBackColor = True
        '
        'btnTreeHide
        '
        Me.btnTreeHide.Location = New System.Drawing.Point(296, 11)
        Me.btnTreeHide.Name = "btnTreeHide"
        Me.btnTreeHide.Size = New System.Drawing.Size(34, 29)
        Me.btnTreeHide.TabIndex = 11
        Me.btnTreeHide.Text = "-"
        Me.btnTreeHide.UseVisualStyleBackColor = True
        '
        'StatusStrip
        '
        Me.StatusStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripProgressBar, Me.StripStatusText})
        Me.StatusStrip.Location = New System.Drawing.Point(0, 566)
        Me.StatusStrip.Name = "StatusStrip"
        Me.StatusStrip.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.StatusStrip.Size = New System.Drawing.Size(911, 22)
        Me.StatusStrip.SizingGrip = False
        Me.StatusStrip.TabIndex = 13
        '
        'ToolStripProgressBar
        '
        Me.ToolStripProgressBar.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.ToolStripProgressBar.Name = "ToolStripProgressBar"
        Me.ToolStripProgressBar.Size = New System.Drawing.Size(575, 16)
        Me.ToolStripProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous
        '
        'StripStatusText
        '
        Me.StripStatusText.Name = "StripStatusText"
        Me.StripStatusText.Size = New System.Drawing.Size(38, 17)
        Me.StripStatusText.Text = "Ready"
        '
        'FrmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(911, 588)
        Me.Controls.Add(Me.StatusStrip)
        Me.Controls.Add(Me.btnTreeHide)
        Me.Controls.Add(Me.btnTreeShow)
        Me.Controls.Add(Me.lvDetails)
        Me.Controls.Add(Me.btnExtracAll)
        Me.Controls.Add(Me.btnAddFile)
        Me.Controls.Add(Me.btnAddDirectory)
        Me.Controls.Add(Me.btnNew)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.btnOpen)
        Me.Controls.Add(Me.ArchiveTree)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmMain"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Archive Explorer"
        Me.mneQuickMenu.ResumeLayout(False)
        Me.StatusStrip.ResumeLayout(False)
        Me.StatusStrip.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ArchiveTree As System.Windows.Forms.TreeView
    Friend WithEvents btnOpen As System.Windows.Forms.Button
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnNew As System.Windows.Forms.Button
    Friend WithEvents btnAddDirectory As System.Windows.Forms.Button
    Friend WithEvents btnAddFile As System.Windows.Forms.Button
    Friend WithEvents btnExtracAll As System.Windows.Forms.Button
    Friend WithEvents lvDetails As System.Windows.Forms.ListView
    Friend WithEvents btnTreeShow As System.Windows.Forms.Button
    Friend WithEvents btnTreeHide As System.Windows.Forms.Button
    Friend WithEvents mneQuickMenu As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ExtractToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EditToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents RemoveToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents StatusStrip As System.Windows.Forms.StatusStrip
    Friend WithEvents StripStatusText As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripProgressBar As System.Windows.Forms.ToolStripProgressBar

End Class
