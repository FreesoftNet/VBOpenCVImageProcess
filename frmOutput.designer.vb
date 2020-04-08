<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmOutput
#Region "Windows フォーム デザイナによって生成されたコード "
	<System.Diagnostics.DebuggerNonUserCode()> Public Sub New()
		MyBase.New()
		'この呼び出しは、Windows フォーム デザイナで必要です。
		InitializeComponent()
	End Sub
	'Form は、コンポーネント一覧に後処理を実行するために dispose をオーバーライドします。
	<System.Diagnostics.DebuggerNonUserCode()> Protected Overloads Overrides Sub Dispose(ByVal Disposing As Boolean)
		If Disposing Then
			If Not components Is Nothing Then
				components.Dispose()
			End If
		End If
		MyBase.Dispose(Disposing)
	End Sub
	'Windows フォーム デザイナで必要です。
	Private components As System.ComponentModel.IContainer
	Public ToolTip1 As System.Windows.Forms.ToolTip
	Public WithEvents cmdCopy As System.Windows.Forms.Button
	Public WithEvents txtOutputString As System.Windows.Forms.RichTextBox
	'メモ: 以下のプロシージャは Windows フォーム デザイナで必要です。
	'Windows フォーム デザイナを使って変更できます。
	'コード エディタを使用して、変更しないでください。
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdCopy = New System.Windows.Forms.Button()
        Me.txtOutputString = New System.Windows.Forms.RichTextBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'cmdCopy
        '
        Me.cmdCopy.BackColor = System.Drawing.SystemColors.Control
        Me.cmdCopy.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdCopy.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdCopy.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdCopy.Location = New System.Drawing.Point(6, 90)
        Me.cmdCopy.Margin = New System.Windows.Forms.Padding(2)
        Me.cmdCopy.Name = "cmdCopy"
        Me.cmdCopy.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdCopy.Size = New System.Drawing.Size(80, 18)
        Me.cmdCopy.TabIndex = 2
        Me.cmdCopy.Tag = "OK"
        Me.cmdCopy.Text = "ｺﾋﾟｰ(&C)"
        Me.cmdCopy.UseVisualStyleBackColor = False
        '
        'txtOutputString
        '
        Me.txtOutputString.Location = New System.Drawing.Point(6, 6)
        Me.txtOutputString.Margin = New System.Windows.Forms.Padding(2)
        Me.txtOutputString.Name = "txtOutputString"
        Me.txtOutputString.Size = New System.Drawing.Size(260, 78)
        Me.txtOutputString.TabIndex = 1
        Me.txtOutputString.Text = ""
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.SystemColors.Control
        Me.Button1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Button1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Button1.Location = New System.Drawing.Point(186, 90)
        Me.Button1.Margin = New System.Windows.Forms.Padding(2)
        Me.Button1.Name = "Button1"
        Me.Button1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Button1.Size = New System.Drawing.Size(80, 18)
        Me.Button1.TabIndex = 3
        Me.Button1.Tag = "OK"
        Me.Button1.Text = "閉じる"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'frmOutput
        '
        Me.AcceptButton = Me.cmdCopy
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.CancelButton = Me.cmdCopy
        Me.ClientSize = New System.Drawing.Size(271, 114)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.cmdCopy)
        Me.Controls.Add(Me.txtOutputString)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Location = New System.Drawing.Point(4, 23)
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.Name = "frmOutput"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "認識結果出力"
        Me.ResumeLayout(False)

    End Sub
#End Region
	
	Sub FrmOutputLoad(sender As Object, e As EventArgs)
		
    End Sub
    Public WithEvents Button1 As System.Windows.Forms.Button
End Class