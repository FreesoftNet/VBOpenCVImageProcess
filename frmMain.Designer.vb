<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmMain
    Inherits System.Windows.Forms.Form

    'フォームがコンポーネントの一覧をクリーンアップするために dispose をオーバーライドします。
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Windows フォーム デザイナで必要です。
    Private components As System.ComponentModel.IContainer

    'メモ: 以下のプロシージャは Windows フォーム デザイナで必要です。
    'Windows フォーム デザイナを使用して変更できます。  
    'コード エディタを使って変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        'Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMain))
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.ファイルFToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.新規画像作成ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.開くOToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.toolStripSeparator = New System.Windows.Forms.ToolStripSeparator()
        Me.現在の画像を保存ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.名前を付けて保存AToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.toolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.終了XToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.編集EToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.UndoMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.グレイスケールToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.コントラスト処理カラーToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.二値化手動ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.適応的二値化ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.縮小MenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.拡大MenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem2 = New System.Windows.Forms.ToolStripMenuItem()
        Me.左９０度回転ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.右９０度回転ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.左右反転ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.上下反転ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.回転ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.論理反転ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.切り抜きToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.回転自動補正ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.サイズ変更ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.文字入力ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.フィルタToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.フィルタブラーToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.フィルタメディアンToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.フィルタガウシアンToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.フィルタバイラテラルToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ヒスト平均ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.フィルタソーベルToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.フィルタラプラシアンToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.フィルタキャニーToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.フィルタアンシャープマスキングToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.二値化処理ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.収縮ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.膨張ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.輪郭線抽出ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.合成論理積ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.表示VToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.縮小して表示50ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.縮小して表示25ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.縮小して表示10ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.等倍で表示ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ツールTToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.テンプレートマッチングToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.USBカメラToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ヘルプHToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.バージョン情報AToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.picImage = New System.Windows.Forms.PictureBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.MenuStrip1.SuspendLayout()
        CType(Me.picImage, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ファイルFToolStripMenuItem, Me.編集EToolStripMenuItem, Me.フィルタToolStripMenuItem, Me.二値化処理ToolStripMenuItem, Me.表示VToolStripMenuItem, Me.ツールTToolStripMenuItem, Me.ヘルプHToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(632, 24)
        Me.MenuStrip1.TabIndex = 17
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'ファイルFToolStripMenuItem
        '
        Me.ファイルFToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.新規画像作成ToolStripMenuItem, Me.開くOToolStripMenuItem, Me.toolStripSeparator, Me.現在の画像を保存ToolStripMenuItem, Me.名前を付けて保存AToolStripMenuItem, Me.toolStripSeparator1, Me.終了XToolStripMenuItem})
        Me.ファイルFToolStripMenuItem.Name = "ファイルFToolStripMenuItem"
        Me.ファイルFToolStripMenuItem.Size = New System.Drawing.Size(66, 20)
        Me.ファイルFToolStripMenuItem.Text = "ファイル(&F)"
        '
        '新規画像作成ToolStripMenuItem
        '
        Me.新規画像作成ToolStripMenuItem.Name = "新規画像作成ToolStripMenuItem"
        Me.新規画像作成ToolStripMenuItem.Size = New System.Drawing.Size(177, 22)
        Me.新規画像作成ToolStripMenuItem.Text = "新規画像作成(&N)"
        '
        '開くOToolStripMenuItem
        '
        'Me.開くOToolStripMenuItem.Image = CType(resources.GetObject("開くOToolStripMenuItem.Image"), System.Drawing.Image)
        Me.開くOToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.開くOToolStripMenuItem.Name = "開くOToolStripMenuItem"
        Me.開くOToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.O), System.Windows.Forms.Keys)
        Me.開くOToolStripMenuItem.Size = New System.Drawing.Size(177, 22)
        Me.開くOToolStripMenuItem.Text = "開く(&O)"
        '
        'toolStripSeparator
        '
        Me.toolStripSeparator.Name = "toolStripSeparator"
        Me.toolStripSeparator.Size = New System.Drawing.Size(174, 6)
        '
        '現在の画像を保存ToolStripMenuItem
        '
        Me.現在の画像を保存ToolStripMenuItem.Name = "現在の画像を保存ToolStripMenuItem"
        Me.現在の画像を保存ToolStripMenuItem.Size = New System.Drawing.Size(177, 22)
        Me.現在の画像を保存ToolStripMenuItem.Text = "現在の画像を保存"
        '
        '名前を付けて保存AToolStripMenuItem
        '
        Me.名前を付けて保存AToolStripMenuItem.Name = "名前を付けて保存AToolStripMenuItem"
        Me.名前を付けて保存AToolStripMenuItem.Size = New System.Drawing.Size(177, 22)
        Me.名前を付けて保存AToolStripMenuItem.Text = "名前を付けて保存(&A)"
        '
        'toolStripSeparator1
        '
        Me.toolStripSeparator1.Name = "toolStripSeparator1"
        Me.toolStripSeparator1.Size = New System.Drawing.Size(174, 6)
        '
        '終了XToolStripMenuItem
        '
        Me.終了XToolStripMenuItem.Name = "終了XToolStripMenuItem"
        Me.終了XToolStripMenuItem.Size = New System.Drawing.Size(177, 22)
        Me.終了XToolStripMenuItem.Text = "終了(&X)"
        '
        '編集EToolStripMenuItem
        '
        Me.編集EToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.UndoMenuItem, Me.グレイスケールToolStripMenuItem, Me.コントラスト処理カラーToolStripMenuItem, Me.二値化手動ToolStripMenuItem, Me.適応的二値化ToolStripMenuItem, Me.縮小MenuItem, Me.拡大MenuItem, Me.ToolStripMenuItem2, Me.左９０度回転ToolStripMenuItem, Me.右９０度回転ToolStripMenuItem, Me.左右反転ToolStripMenuItem, Me.上下反転ToolStripMenuItem, Me.回転ToolStripMenuItem, Me.論理反転ToolStripMenuItem, Me.切り抜きToolStripMenuItem, Me.回転自動補正ToolStripMenuItem, Me.サイズ変更ToolStripMenuItem, Me.文字入力ToolStripMenuItem,Me.合成論理積ToolStripMenuItem})
        Me.編集EToolStripMenuItem.Name = "編集EToolStripMenuItem"
        Me.編集EToolStripMenuItem.Size = New System.Drawing.Size(57, 20)
        Me.編集EToolStripMenuItem.Text = "編集(&E)"
        '
        'UndoMenuItem
        '
        Me.UndoMenuItem.Name = "UndoMenuItem"
        Me.UndoMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Z), System.Windows.Forms.Keys)
        Me.UndoMenuItem.Size = New System.Drawing.Size(252, 22)
        Me.UndoMenuItem.Text = "元に戻す"
        '
        'グレイスケールToolStripMenuItem
        '
        Me.グレイスケールToolStripMenuItem.Name = "グレイスケールToolStripMenuItem"
        Me.グレイスケールToolStripMenuItem.Size = New System.Drawing.Size(252, 22)
        Me.グレイスケールToolStripMenuItem.Text = "グレイスケール"
        '
        'コントラスト処理カラーToolStripMenuItem
        '
        Me.コントラスト処理カラーToolStripMenuItem.Name = "コントラスト処理カラーToolStripMenuItem"
        Me.コントラスト処理カラーToolStripMenuItem.Size = New System.Drawing.Size(252, 22)
        Me.コントラスト処理カラーToolStripMenuItem.Text = "コントラスト処理（カラー）"
        '
        '二値化手動ToolStripMenuItem
        '
        Me.二値化手動ToolStripMenuItem.Name = "二値化手動ToolStripMenuItem"
        Me.二値化手動ToolStripMenuItem.Size = New System.Drawing.Size(252, 22)
        Me.二値化手動ToolStripMenuItem.Text = "２値化（手動）"
        '
        '適応的二値化ToolStripMenuItem
        '
        Me.適応的二値化ToolStripMenuItem.Name = "適応的二値化ToolStripMenuItem"
        Me.適応的二値化ToolStripMenuItem.Size = New System.Drawing.Size(252, 22)
        Me.適応的二値化ToolStripMenuItem.Text = "適応的２値化"
        '
        '縮小MenuItem
        '
        Me.縮小MenuItem.Name = "縮小MenuItem"
        Me.縮小MenuItem.Size = New System.Drawing.Size(252, 22)
        Me.縮小MenuItem.Text = "縮小（50%）"
        '
        '拡大MenuItem
        '
        Me.拡大MenuItem.Name = "拡大MenuItem"
        Me.拡大MenuItem.Size = New System.Drawing.Size(252, 22)
        Me.拡大MenuItem.Text = "拡大（200%）"
        '
        'ToolStripMenuItem2
        '
        Me.ToolStripMenuItem2.Name = "ToolStripMenuItem2"
        Me.ToolStripMenuItem2.Size = New System.Drawing.Size(252, 22)
        Me.ToolStripMenuItem2.Text = "拡大・縮小（倍率指定）"
        '
        '左９０度回転ToolStripMenuItem
        '
        Me.左９０度回転ToolStripMenuItem.Name = "左９０度回転ToolStripMenuItem"
        Me.左９０度回転ToolStripMenuItem.Size = New System.Drawing.Size(252, 22)
        Me.左９０度回転ToolStripMenuItem.Text = "左９０度回転"
        '
        '右９０度回転ToolStripMenuItem
        '
        Me.右９０度回転ToolStripMenuItem.Name = "右９０度回転ToolStripMenuItem"
        Me.右９０度回転ToolStripMenuItem.Size = New System.Drawing.Size(252, 22)
        Me.右９０度回転ToolStripMenuItem.Text = "右９０度回転"
        '
        '左右反転ToolStripMenuItem
        '
        Me.左右反転ToolStripMenuItem.Name = "左右反転ToolStripMenuItem"
        Me.左右反転ToolStripMenuItem.Size = New System.Drawing.Size(252, 22)
        Me.左右反転ToolStripMenuItem.Text = "左右反転"
        '
        '上下反転ToolStripMenuItem
        '
        Me.上下反転ToolStripMenuItem.Name = "上下反転ToolStripMenuItem"
        Me.上下反転ToolStripMenuItem.Size = New System.Drawing.Size(252, 22)
        Me.上下反転ToolStripMenuItem.Text = "上下反転"
        '
        '回転ToolStripMenuItem
        '
        Me.回転ToolStripMenuItem.Name = "回転ToolStripMenuItem"
        Me.回転ToolStripMenuItem.Size = New System.Drawing.Size(252, 22)
        Me.回転ToolStripMenuItem.Text = "回転（回転角指定）"
        '
        '論理反転ToolStripMenuItem
        '
        Me.論理反転ToolStripMenuItem.Name = "論理反転ToolStripMenuItem"
        Me.論理反転ToolStripMenuItem.Size = New System.Drawing.Size(252, 22)
        Me.論理反転ToolStripMenuItem.Text = "論理反転"
        '
        '切り抜きToolStripMenuItem
        '
        Me.切り抜きToolStripMenuItem.Name = "切り抜きToolStripMenuItem"
        Me.切り抜きToolStripMenuItem.Size = New System.Drawing.Size(252, 22)
        Me.切り抜きToolStripMenuItem.Text = "切り抜き（クリップボードにコピー）"
        '
        '回転自動補正ToolStripMenuItem
        '
        Me.回転自動補正ToolStripMenuItem.Name = "回転自動補正ToolStripMenuItem"
        Me.回転自動補正ToolStripMenuItem.Size = New System.Drawing.Size(252, 22)
        Me.回転自動補正ToolStripMenuItem.Text = "回転（自動補正(要グレースケール)）"
        '
        'サイズ変更ToolStripMenuItem
        '
        Me.サイズ変更ToolStripMenuItem.Name = "サイズ変更ToolStripMenuItem"
        Me.サイズ変更ToolStripMenuItem.Size = New System.Drawing.Size(252, 22)
        Me.サイズ変更ToolStripMenuItem.Text = "サイズ変更"
        '
        '文字入力ToolStripMenuItem
        '
        Me.文字入力ToolStripMenuItem.Name = "文字入力ToolStripMenuItem"
        Me.文字入力ToolStripMenuItem.Size = New System.Drawing.Size(252, 22)
        Me.文字入力ToolStripMenuItem.Text = "文字入力"
        '
        '合成論理積ToolStripMenuItem
        '
        Me.合成論理積ToolStripMenuItem.Name = "合成論理積ToolStripMenuItem"
        Me.合成論理積ToolStripMenuItem.Size = New System.Drawing.Size(158, 22)
        Me.合成論理積ToolStripMenuItem.Text = "合成（論理積）"
        '
        'フィルタToolStripMenuItem
        '
        Me.フィルタToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.フィルタブラーToolStripMenuItem, Me.フィルタメディアンToolStripMenuItem, Me.フィルタガウシアンToolStripMenuItem, Me.フィルタバイラテラルToolStripMenuItem, Me.ヒスト平均ToolStripMenuItem1, Me.フィルタソーベルToolStripMenuItem, Me.フィルタラプラシアンToolStripMenuItem, Me.フィルタキャニーToolStripMenuItem, Me.フィルタアンシャープマスキングToolStripMenuItem})
        Me.フィルタToolStripMenuItem.Name = "フィルタToolStripMenuItem"
        Me.フィルタToolStripMenuItem.Size = New System.Drawing.Size(52, 20)
        Me.フィルタToolStripMenuItem.Text = "フィルタ"
        '
        'フィルタブラーToolStripMenuItem
        '
        Me.フィルタブラーToolStripMenuItem.Name = "フィルタブラーToolStripMenuItem"
        Me.フィルタブラーToolStripMenuItem.Size = New System.Drawing.Size(213, 22)
        Me.フィルタブラーToolStripMenuItem.Text = "フィルタ(ブラー)"
        '
        'フィルタメディアンToolStripMenuItem
        '
        Me.フィルタメディアンToolStripMenuItem.Name = "フィルタメディアンToolStripMenuItem"
        Me.フィルタメディアンToolStripMenuItem.Size = New System.Drawing.Size(213, 22)
        Me.フィルタメディアンToolStripMenuItem.Text = "フィルタ(メディアン)"
        '
        'フィルタガウシアンToolStripMenuItem
        '
        Me.フィルタガウシアンToolStripMenuItem.Name = "フィルタガウシアンToolStripMenuItem"
        Me.フィルタガウシアンToolStripMenuItem.Size = New System.Drawing.Size(213, 22)
        Me.フィルタガウシアンToolStripMenuItem.Text = "フィルタ(ガウシアン)"
        '
        'フィルタバイラテラルToolStripMenuItem
        '
        Me.フィルタバイラテラルToolStripMenuItem.Name = "フィルタバイラテラルToolStripMenuItem"
        Me.フィルタバイラテラルToolStripMenuItem.Size = New System.Drawing.Size(213, 22)
        Me.フィルタバイラテラルToolStripMenuItem.Text = "フィルタ(バイラテラル)"
        '
        'ヒスト平均ToolStripMenuItem1
        '
        Me.ヒスト平均ToolStripMenuItem1.Name = "ヒスト平均ToolStripMenuItem1"
        Me.ヒスト平均ToolStripMenuItem1.Size = New System.Drawing.Size(213, 22)
        Me.ヒスト平均ToolStripMenuItem1.Text = "ヒスト平均(要グレイスケール)"
        '
        'フィルタソーベルToolStripMenuItem
        '
        Me.フィルタソーベルToolStripMenuItem.Name = "フィルタソーベルToolStripMenuItem"
        Me.フィルタソーベルToolStripMenuItem.Size = New System.Drawing.Size(213, 22)
        Me.フィルタソーベルToolStripMenuItem.Text = "フィルタ(ソーベル)"
        '
        'フィルタラプラシアンToolStripMenuItem
        '
        Me.フィルタラプラシアンToolStripMenuItem.Name = "フィルタラプラシアンToolStripMenuItem"
        Me.フィルタラプラシアンToolStripMenuItem.Size = New System.Drawing.Size(213, 22)
        Me.フィルタラプラシアンToolStripMenuItem.Text = "フィルタ(ラプラシアン)"
        '
        'フィルタキャニーToolStripMenuItem
        '
        Me.フィルタキャニーToolStripMenuItem.Name = "フィルタキャニーToolStripMenuItem"
        Me.フィルタキャニーToolStripMenuItem.Size = New System.Drawing.Size(213, 22)
        Me.フィルタキャニーToolStripMenuItem.Text = "フィルタ(キャニー)"
        '
        'フィルタアンシャープマスキングToolStripMenuItem
        '
        Me.フィルタアンシャープマスキングToolStripMenuItem.Name = "フィルタアンシャープマスキングToolStripMenuItem"
        Me.フィルタアンシャープマスキングToolStripMenuItem.Size = New System.Drawing.Size(213, 22)
        Me.フィルタアンシャープマスキングToolStripMenuItem.Text = "フィルタ(アンシャープマスキング)"
        '
        '二値化処理ToolStripMenuItem
        '
        Me.二値化処理ToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.収縮ToolStripMenuItem, Me.膨張ToolStripMenuItem})
        Me.二値化処理ToolStripMenuItem.Name = "二値化処理ToolStripMenuItem"
        Me.二値化処理ToolStripMenuItem.Size = New System.Drawing.Size(79, 20)
        Me.二値化処理ToolStripMenuItem.Text = "２値化処理"
        '
        '収縮ToolStripMenuItem
        '
        Me.収縮ToolStripMenuItem.Name = "収縮ToolStripMenuItem"
        Me.収縮ToolStripMenuItem.Size = New System.Drawing.Size(147, 22)
        Me.収縮ToolStripMenuItem.Text = "収縮"
        '
        '膨張ToolStripMenuItem
        '
        Me.膨張ToolStripMenuItem.Name = "膨張ToolStripMenuItem"
        Me.膨張ToolStripMenuItem.Size = New System.Drawing.Size(147, 22)
        Me.膨張ToolStripMenuItem.Text = "膨張"
        '
        '輪郭線抽出ToolStripMenuItem
        '
        Me.輪郭線抽出ToolStripMenuItem.Name = "輪郭線抽出ToolStripMenuItem"
        Me.輪郭線抽出ToolStripMenuItem.Size = New System.Drawing.Size(147, 22)
        Me.輪郭線抽出ToolStripMenuItem.Text = "輪郭線抽出"
        '
        '表示VToolStripMenuItem
        '
        Me.表示VToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.縮小して表示50ToolStripMenuItem, Me.縮小して表示25ToolStripMenuItem, Me.縮小して表示10ToolStripMenuItem, Me.等倍で表示ToolStripMenuItem})
        Me.表示VToolStripMenuItem.Name = "表示VToolStripMenuItem"
        Me.表示VToolStripMenuItem.Size = New System.Drawing.Size(58, 20)
        Me.表示VToolStripMenuItem.Text = "表示(&V)"
        '
        '縮小して表示50ToolStripMenuItem
        '
        Me.縮小して表示50ToolStripMenuItem.Name = "縮小して表示50ToolStripMenuItem"
        Me.縮小して表示50ToolStripMenuItem.Size = New System.Drawing.Size(168, 22)
        Me.縮小して表示50ToolStripMenuItem.Text = "縮小して表示(50%)"
        '
        '縮小して表示25ToolStripMenuItem
        '
        Me.縮小して表示25ToolStripMenuItem.Name = "縮小して表示25ToolStripMenuItem"
        Me.縮小して表示25ToolStripMenuItem.Size = New System.Drawing.Size(168, 22)
        Me.縮小して表示25ToolStripMenuItem.Text = "縮小して表示(25%)"
        '
        '縮小して表示10ToolStripMenuItem
        '
        Me.縮小して表示10ToolStripMenuItem.Name = "縮小して表示10ToolStripMenuItem"
        Me.縮小して表示10ToolStripMenuItem.Size = New System.Drawing.Size(168, 22)
        Me.縮小して表示10ToolStripMenuItem.Text = "縮小して表示(10%)"
        '
        '等倍で表示ToolStripMenuItem
        '
        Me.等倍で表示ToolStripMenuItem.Name = "等倍で表示ToolStripMenuItem"
        Me.等倍で表示ToolStripMenuItem.Size = New System.Drawing.Size(168, 22)
        Me.等倍で表示ToolStripMenuItem.Text = "等倍で表示"
        
        '
        'ツールTToolStripMenuItem
        '
        Me.ツールTToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.テンプレートマッチングToolStripMenuItem, Me.USBカメラToolStripMenuItem})
        Me.ツールTToolStripMenuItem.Name = "ツールTToolStripMenuItem"
        Me.ツールTToolStripMenuItem.Size = New System.Drawing.Size(60, 20)
        Me.ツールTToolStripMenuItem.Text = "ツール(&T)"
        '
        'テンプレートマッチングToolStripMenuItem
        '
        Me.テンプレートマッチングToolStripMenuItem.Name = "テンプレートマッチングToolStripMenuItem"
        Me.テンプレートマッチングToolStripMenuItem.Size = New System.Drawing.Size(176, 22)
        Me.テンプレートマッチングToolStripMenuItem.Text = "テンプレートマッチング"
        '
        'USBカメラToolStripMenuItem
        '
        Me.USBカメラToolStripMenuItem.Name = "USBカメラToolStripMenuItem"
        Me.USBカメラToolStripMenuItem.Size = New System.Drawing.Size(176, 22)
        Me.USBカメラToolStripMenuItem.Text = "USBカメラ"
        '
        'ヘルプHToolStripMenuItem
        '
        Me.ヘルプHToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.バージョン情報AToolStripMenuItem})
        Me.ヘルプHToolStripMenuItem.Name = "ヘルプHToolStripMenuItem"
        Me.ヘルプHToolStripMenuItem.Size = New System.Drawing.Size(65, 20)
        Me.ヘルプHToolStripMenuItem.Text = "ヘルプ(&H)"
        '
        'バージョン情報AToolStripMenuItem
        '
        Me.バージョン情報AToolStripMenuItem.Name = "バージョン情報AToolStripMenuItem"
        Me.バージョン情報AToolStripMenuItem.Size = New System.Drawing.Size(167, 22)
        Me.バージョン情報AToolStripMenuItem.Text = "バージョン情報(&A)..."
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 560)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(632, 22)
        Me.StatusStrip1.TabIndex = 26
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'picImage
        '
        Me.picImage.Location = New System.Drawing.Point(31, 13)
        Me.picImage.Name = "picImage"
        Me.picImage.Size = New System.Drawing.Size(100, 50)
        Me.picImage.TabIndex = 27
        Me.picImage.TabStop = False
        '
        'Panel1
        '
        Me.Panel1.AutoScroll = True
        Me.Panel1.Controls.Add(Me.picImage)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 24)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(632, 536)
        Me.Panel1.TabIndex = 28


        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(632, 582)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "frmMain"
        Me.Text = "OpenCV利用事例集"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        CType(Me.picImage, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents ファイルFToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 開くOToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents toolStripSeparator As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents 名前を付けて保存AToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents toolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents 終了XToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 編集EToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ツールTToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ヘルプHToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents バージョン情報AToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 右９０度回転ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 適応的二値化ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 表示VToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents テンプレートマッチングToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents コントラスト処理カラーToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 現在の画像を保存ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents グレイスケールToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents 左９０度回転ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 上下反転ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 左右反転ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 回転ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents サイズ変更ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 文字入力ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 論理反転ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 合成論理積ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 縮小して表示50ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 縮小して表示25ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 等倍で表示ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents UndoMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 縮小MenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 拡大MenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 二値化処理ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents フィルタToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents フィルタブラーToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents フィルタメディアンToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents フィルタガウシアンToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents フィルタバイラテラルToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ヒスト平均ToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 二値化手動ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents フィルタソーベルToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents フィルタラプラシアンToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents フィルタキャニーToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 収縮ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 膨張ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 輪郭線抽出ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents フィルタアンシャープマスキングToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents picImage As System.Windows.Forms.PictureBox
    Friend WithEvents ToolStripMenuItem2 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 切り抜きToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents USBカメラToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents 縮小して表示10ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 回転自動補正ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 新規画像作成ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Timer1 As System.Windows.Forms.Timer

End Class
