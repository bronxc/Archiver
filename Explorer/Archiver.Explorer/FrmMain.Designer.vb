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
        Me.ArchiveTree = New System.Windows.Forms.TreeView()
        Me.btnOpen = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnNew = New System.Windows.Forms.Button()
        Me.btnAddDirectory = New System.Windows.Forms.Button()
        Me.btnAddFile = New System.Windows.Forms.Button()
        Me.btnExtracAll = New System.Windows.Forms.Button()
        Me.Details = New System.Windows.Forms.ListView()
        Me.btnTreeShow = New System.Windows.Forms.Button()
        Me.btnTreeHide = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'ArchiveTree
        '
        Me.ArchiveTree.Location = New System.Drawing.Point(12, 46)
        Me.ArchiveTree.Name = "ArchiveTree"
        Me.ArchiveTree.Size = New System.Drawing.Size(318, 504)
        Me.ArchiveTree.TabIndex = 1
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
        Me.btnExtracAll.Location = New System.Drawing.Point(744, 10)
        Me.btnExtracAll.Name = "btnExtracAll"
        Me.btnExtracAll.Size = New System.Drawing.Size(77, 29)
        Me.btnExtracAll.TabIndex = 8
        Me.btnExtracAll.Text = "Extract"
        Me.btnExtracAll.UseVisualStyleBackColor = True
        '
        'Details
        '
        Me.Details.GridLines = True
        Me.Details.Location = New System.Drawing.Point(336, 46)
        Me.Details.MultiSelect = False
        Me.Details.Name = "Details"
        Me.Details.Size = New System.Drawing.Size(485, 504)
        Me.Details.TabIndex = 9
        Me.Details.UseCompatibleStateImageBehavior = False
        Me.Details.View = System.Windows.Forms.View.Details
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
        'FrmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(833, 562)
        Me.Controls.Add(Me.btnTreeHide)
        Me.Controls.Add(Me.btnTreeShow)
        Me.Controls.Add(Me.Details)
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
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ArchiveTree As System.Windows.Forms.TreeView
    Friend WithEvents btnOpen As System.Windows.Forms.Button
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnNew As System.Windows.Forms.Button
    Friend WithEvents btnAddDirectory As System.Windows.Forms.Button
    Friend WithEvents btnAddFile As System.Windows.Forms.Button
    Friend WithEvents btnExtracAll As System.Windows.Forms.Button
    Friend WithEvents Details As System.Windows.Forms.ListView
    Friend WithEvents btnTreeShow As System.Windows.Forms.Button
    Friend WithEvents btnTreeHide As System.Windows.Forms.Button

End Class
