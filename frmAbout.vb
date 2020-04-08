Imports System.Reflection

Public NotInheritable Class frmAbout

    Private Sub frmAbout_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
        ' フォームのタイトルを設定します。
        Dim ApplicationTitle As String
        If My.Application.Info.Title <> "" Then
            ApplicationTitle = My.Application.Info.Title
        Else
            ApplicationTitle = System.IO.Path.GetFileNameWithoutExtension(My.Application.Info.AssemblyName)
        End If
        Me.Text = String.Format("{0} のバージョン情報", ApplicationTitle)
        ' バージョン情報ボックスに表示されたテキストをすべて初期化します。
        ' TODO: [プロジェクト] メニューの下にある [プロジェクト プロパティ] ダイアログの [アプリケーション] ペインで、アプリケーションのアセンブリ情報を 
        '    カスタマイズします。
        Me.LabelProductName.Text = My.Application.Info.ProductName
        'Me.LabelVersion.Text = String.Format("バージョン {0}", My.Application.Info.Version.ToString)
        '2008/12/14修正
        Me.LabelVersion.Text = String.Format("バージョン {0}", Assembly.GetExecutingAssembly().GetName().Version())
        Me.LabelCopyright.Text = My.Application.Info.Copyright
        Me.LabelCompanyName.Text = My.Application.Info.CompanyName
        Me.TextBoxDescription.Text = My.Application.Info.Description

    End Sub

    Private Sub OKButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles OKButton.Click
        Me.Close()
    End Sub


End Class
