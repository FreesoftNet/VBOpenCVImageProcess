<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSet
    Inherits System.Windows.Forms.Form

    'フォームがコンポーネントの一覧をクリーンアップするために dispose をオーバーライドします。
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Windows フォーム デザイナで必要です。
    Private components As System.ComponentModel.IContainer

    'メモ: 以下のプロシージャは Windows フォーム デザイナで必要です。
    'Windows フォーム デザイナを使用して変更できます。  
    'コード エディタを使って変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.cmdSet = New System.Windows.Forms.Button()
        Me.cmdCancel = New System.Windows.Forms.Button()
        Me.txtValue1 = New System.Windows.Forms.RichTextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtValue2 = New System.Windows.Forms.RichTextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtValue3 = New System.Windows.Forms.RichTextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtValue4 = New System.Windows.Forms.RichTextBox()
        Me.SuspendLayout()
        '
        'cmdSet
        '
        Me.cmdSet.Location = New System.Drawing.Point(258, 85)
        Me.cmdSet.Name = "cmdSet"
        Me.cmdSet.Size = New System.Drawing.Size(75, 23)
        Me.cmdSet.TabIndex = 4
        Me.cmdSet.Text = "Ok"
        Me.cmdSet.UseVisualStyleBackColor = True
        '
        'cmdCancel
        '
        Me.cmdCancel.Location = New System.Drawing.Point(339, 85)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.Size = New System.Drawing.Size(75, 23)
        Me.cmdCancel.TabIndex = 5
        Me.cmdCancel.Text = "Cancel"
        Me.cmdCancel.UseVisualStyleBackColor = True
        '
        'txtValue1
        '
        Me.txtValue1.Location = New System.Drawing.Point(12, 36)
        Me.txtValue1.Multiline = False
        Me.txtValue1.Name = "txtValue1"
        Me.txtValue1.Size = New System.Drawing.Size(95, 25)
        Me.txtValue1.TabIndex = 0
        Me.txtValue1.Text = ""
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(55, 12)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "パラメータ1"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(113, 9)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(55, 12)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "パラメータ2"
        '
        'txtValue2
        '
        Me.txtValue2.Location = New System.Drawing.Point(113, 36)
        Me.txtValue2.Multiline = False
        Me.txtValue2.Name = "txtValue2"
        Me.txtValue2.Size = New System.Drawing.Size(95, 25)
        Me.txtValue2.TabIndex = 1
        Me.txtValue2.Text = ""
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(214, 9)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(55, 12)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "パラメータ3"
        '
        'txtValue3
        '
        Me.txtValue3.Location = New System.Drawing.Point(214, 36)
        Me.txtValue3.Multiline = False
        Me.txtValue3.Name = "txtValue3"
        Me.txtValue3.Size = New System.Drawing.Size(95, 25)
        Me.txtValue3.TabIndex = 2
        Me.txtValue3.Text = ""
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(315, 9)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(55, 12)
        Me.Label4.TabIndex = 9
        Me.Label4.Text = "パラメータ4"
        '
        'txtValue4
        '
        Me.txtValue4.Location = New System.Drawing.Point(315, 36)
        Me.txtValue4.Multiline = False
        Me.txtValue4.Name = "txtValue4"
        Me.txtValue4.Size = New System.Drawing.Size(95, 25)
        Me.txtValue4.TabIndex = 3
        Me.txtValue4.Text = ""
        '
        'frmSet
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(426, 130)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txtValue4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtValue3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtValue2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtValue1)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.cmdSet)
        Me.Name = "frmSet"
        Me.Text = "設定"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents cmdSet As System.Windows.Forms.Button
    Friend WithEvents cmdCancel As System.Windows.Forms.Button
    Friend WithEvents txtValue1 As System.Windows.Forms.RichTextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtValue2 As System.Windows.Forms.RichTextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtValue3 As System.Windows.Forms.RichTextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtValue4 As System.Windows.Forms.RichTextBox
End Class
