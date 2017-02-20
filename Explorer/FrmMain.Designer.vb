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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmMain))
        Me.ArchiveTree = New System.Windows.Forms.TreeView()
        Me.mneQuickMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.RemoveToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ImageCollection = New System.Windows.Forms.ImageList(Me.components)
        Me.StatusStrip = New System.Windows.Forms.StatusStrip()
        Me.ProgressBar = New System.Windows.Forms.ToolStripProgressBar()
        Me.labelStatus = New System.Windows.Forms.ToolStripStatusLabel()
        Me.MenuStrip = New System.Windows.Forms.ToolStrip()
        Me.MenuFile = New System.Windows.Forms.ToolStripDropDownButton()
        Me.MenuFileNew = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuFileSave = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuFileOpen = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem2 = New System.Windows.Forms.ToolStripSeparator()
        Me.MenuFileExit = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuExpandAll = New System.Windows.Forms.ToolStripButton()
        Me.menuCollapseAll = New System.Windows.Forms.ToolStripButton()
        Me.menuAddFile = New System.Windows.Forms.ToolStripButton()
        Me.menuAddFolder = New System.Windows.Forms.ToolStripButton()
        Me.menuExtractAll = New System.Windows.Forms.ToolStripButton()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.lvDetails = New System.Windows.Forms.ListView()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.rtbDebugLog = New System.Windows.Forms.RichTextBox()
        Me.labelAuthor = New System.Windows.Forms.ToolStripLabel()
        Me.mneQuickMenu.SuspendLayout()
        Me.StatusStrip.SuspendLayout()
        Me.MenuStrip.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.SuspendLayout()
        '
        'ArchiveTree
        '
        Me.ArchiveTree.Font = New System.Drawing.Font("Lucida Console", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ArchiveTree.Location = New System.Drawing.Point(0, 47)
        Me.ArchiveTree.Name = "ArchiveTree"
        Me.ArchiveTree.Size = New System.Drawing.Size(312, 491)
        Me.ArchiveTree.TabIndex = 1
        '
        'mneQuickMenu
        '
        Me.mneQuickMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.RemoveToolStripMenuItem})
        Me.mneQuickMenu.Name = "mneExtract"
        Me.mneQuickMenu.Size = New System.Drawing.Size(114, 26)
        '
        'RemoveToolStripMenuItem
        '
        Me.RemoveToolStripMenuItem.Name = "RemoveToolStripMenuItem"
        Me.RemoveToolStripMenuItem.Size = New System.Drawing.Size(113, 22)
        Me.RemoveToolStripMenuItem.Text = "Remove"
        '
        'ImageCollection
        '
        Me.ImageCollection.ImageStream = CType(resources.GetObject("ImageCollection.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageCollection.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageCollection.Images.SetKeyName(0, "archive.png")
        Me.ImageCollection.Images.SetKeyName(1, "folder.png")
        Me.ImageCollection.Images.SetKeyName(2, "file.png")
        '
        'StatusStrip
        '
        Me.StatusStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ProgressBar, Me.labelStatus})
        Me.StatusStrip.Location = New System.Drawing.Point(0, 541)
        Me.StatusStrip.Name = "StatusStrip"
        Me.StatusStrip.Size = New System.Drawing.Size(920, 22)
        Me.StatusStrip.SizingGrip = False
        Me.StatusStrip.TabIndex = 13
        '
        'ProgressBar
        '
        Me.ProgressBar.Name = "ProgressBar"
        Me.ProgressBar.Size = New System.Drawing.Size(310, 16)
        '
        'labelStatus
        '
        Me.labelStatus.Name = "labelStatus"
        Me.labelStatus.Size = New System.Drawing.Size(76, 17)
        Me.labelStatus.Text = "Status: Ready"
        '
        'MenuStrip
        '
        Me.MenuStrip.AutoSize = False
        Me.MenuStrip.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.MenuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MenuFile, Me.menuExpandAll, Me.menuCollapseAll, Me.menuAddFile, Me.menuAddFolder, Me.menuExtractAll, Me.labelAuthor})
        Me.MenuStrip.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip.Name = "MenuStrip"
        Me.MenuStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System
        Me.MenuStrip.Size = New System.Drawing.Size(920, 44)
        Me.MenuStrip.TabIndex = 14
        '
        'MenuFile
        '
        Me.MenuFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.MenuFile.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MenuFileNew, Me.MenuFileSave, Me.MenuFileOpen, Me.ToolStripMenuItem2, Me.MenuFileExit})
        Me.MenuFile.Image = CType(resources.GetObject("MenuFile.Image"), System.Drawing.Image)
        Me.MenuFile.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.MenuFile.Name = "MenuFile"
        Me.MenuFile.Size = New System.Drawing.Size(45, 41)
        Me.MenuFile.Text = "menuFile"
        '
        'MenuFileNew
        '
        Me.MenuFileNew.Name = "MenuFileNew"
        Me.MenuFileNew.Size = New System.Drawing.Size(100, 22)
        Me.MenuFileNew.Text = "New"
        '
        'MenuFileSave
        '
        Me.MenuFileSave.Name = "MenuFileSave"
        Me.MenuFileSave.Size = New System.Drawing.Size(100, 22)
        Me.MenuFileSave.Text = "Save"
        '
        'MenuFileOpen
        '
        Me.MenuFileOpen.Name = "MenuFileOpen"
        Me.MenuFileOpen.Size = New System.Drawing.Size(100, 22)
        Me.MenuFileOpen.Text = "Open"
        '
        'ToolStripMenuItem2
        '
        Me.ToolStripMenuItem2.Name = "ToolStripMenuItem2"
        Me.ToolStripMenuItem2.Size = New System.Drawing.Size(97, 6)
        '
        'MenuFileExit
        '
        Me.MenuFileExit.Name = "MenuFileExit"
        Me.MenuFileExit.Size = New System.Drawing.Size(100, 22)
        Me.MenuFileExit.Text = "Exit"
        '
        'menuExpandAll
        '
        Me.menuExpandAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.menuExpandAll.Image = CType(resources.GetObject("menuExpandAll.Image"), System.Drawing.Image)
        Me.menuExpandAll.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.menuExpandAll.Name = "menuExpandAll"
        Me.menuExpandAll.Size = New System.Drawing.Size(36, 41)
        '
        'menuCollapseAll
        '
        Me.menuCollapseAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.menuCollapseAll.Image = CType(resources.GetObject("menuCollapseAll.Image"), System.Drawing.Image)
        Me.menuCollapseAll.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.menuCollapseAll.Name = "menuCollapseAll"
        Me.menuCollapseAll.Size = New System.Drawing.Size(36, 41)
        '
        'menuAddFile
        '
        Me.menuAddFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.menuAddFile.Image = CType(resources.GetObject("menuAddFile.Image"), System.Drawing.Image)
        Me.menuAddFile.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.menuAddFile.Name = "menuAddFile"
        Me.menuAddFile.Size = New System.Drawing.Size(36, 41)
        '
        'menuAddFolder
        '
        Me.menuAddFolder.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.menuAddFolder.Image = CType(resources.GetObject("menuAddFolder.Image"), System.Drawing.Image)
        Me.menuAddFolder.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.menuAddFolder.Name = "menuAddFolder"
        Me.menuAddFolder.Size = New System.Drawing.Size(36, 41)
        '
        'menuExtractAll
        '
        Me.menuExtractAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.menuExtractAll.Image = CType(resources.GetObject("menuExtractAll.Image"), System.Drawing.Image)
        Me.menuExtractAll.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.menuExtractAll.Name = "menuExtractAll"
        Me.menuExtractAll.Size = New System.Drawing.Size(36, 41)
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(318, 47)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(602, 491)
        Me.TabControl1.TabIndex = 15
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.lvDetails)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(594, 465)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Viewer"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'lvDetails
        '
        Me.lvDetails.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lvDetails.Font = New System.Drawing.Font("Lucida Console", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lvDetails.LabelWrap = False
        Me.lvDetails.LargeImageList = Me.ImageCollection
        Me.lvDetails.Location = New System.Drawing.Point(3, 3)
        Me.lvDetails.MultiSelect = False
        Me.lvDetails.Name = "lvDetails"
        Me.lvDetails.Size = New System.Drawing.Size(588, 459)
        Me.lvDetails.SmallImageList = Me.ImageCollection
        Me.lvDetails.TabIndex = 10
        Me.lvDetails.UseCompatibleStateImageBehavior = False
        Me.lvDetails.View = System.Windows.Forms.View.Details
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.rtbDebugLog)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(520, 428)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Debug"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'rtbDebugLog
        '
        Me.rtbDebugLog.BackColor = System.Drawing.Color.White
        Me.rtbDebugLog.Dock = System.Windows.Forms.DockStyle.Fill
        Me.rtbDebugLog.Font = New System.Drawing.Font("Lucida Sans Unicode", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rtbDebugLog.Location = New System.Drawing.Point(3, 3)
        Me.rtbDebugLog.Name = "rtbDebugLog"
        Me.rtbDebugLog.ReadOnly = True
        Me.rtbDebugLog.Size = New System.Drawing.Size(514, 422)
        Me.rtbDebugLog.TabIndex = 0
        Me.rtbDebugLog.Text = ""
        Me.rtbDebugLog.WordWrap = False
        '
        'labelAuthor
        '
        Me.labelAuthor.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.labelAuthor.Name = "labelAuthor"
        Me.labelAuthor.Size = New System.Drawing.Size(95, 41)
        Me.labelAuthor.Text = "Archiver by Barret"
        '
        'FrmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(920, 563)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.MenuStrip)
        Me.Controls.Add(Me.StatusStrip)
        Me.Controls.Add(Me.ArchiveTree)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmMain"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Archive Explorer"
        Me.mneQuickMenu.ResumeLayout(False)
        Me.StatusStrip.ResumeLayout(False)
        Me.StatusStrip.PerformLayout()
        Me.MenuStrip.ResumeLayout(False)
        Me.MenuStrip.PerformLayout()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ArchiveTree As System.Windows.Forms.TreeView
    Friend WithEvents mneQuickMenu As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents RemoveToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents StatusStrip As System.Windows.Forms.StatusStrip
    Friend WithEvents ImageCollection As System.Windows.Forms.ImageList
    Friend WithEvents MenuStrip As System.Windows.Forms.ToolStrip
    Friend WithEvents MenuFile As System.Windows.Forms.ToolStripDropDownButton
    Friend WithEvents MenuFileNew As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MenuFileSave As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MenuFileOpen As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents MenuFileExit As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuExpandAll As System.Windows.Forms.ToolStripButton
    Friend WithEvents menuCollapseAll As System.Windows.Forms.ToolStripButton
    Friend WithEvents menuAddFile As System.Windows.Forms.ToolStripButton
    Friend WithEvents menuAddFolder As System.Windows.Forms.ToolStripButton
    Friend WithEvents menuExtractAll As System.Windows.Forms.ToolStripButton
    Friend WithEvents ProgressBar As System.Windows.Forms.ToolStripProgressBar
    Friend WithEvents labelStatus As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents lvDetails As System.Windows.Forms.ListView
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents rtbDebugLog As System.Windows.Forms.RichTextBox
    Friend WithEvents labelAuthor As System.Windows.Forms.ToolStripLabel
End Class
