Option Strict Off
Option Explicit On
Friend Class frmOutput
	Inherits System.Windows.Forms.Form
	
    Private Sub CmdCopyClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdCopy.Click
        Try
            My.Computer.Clipboard.Clear() ' Clipboard オブジェクトの内容を消去します。
            My.Computer.Clipboard.SetText(txtOutputString.Text)
            Me.Dispose()
        Catch ex As Exception
            MsgBox("クリップボードへのコピーに失敗しました。")
        End Try
    End Sub
	



    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dispose()
    End Sub
End Class