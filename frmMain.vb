Imports System.Runtime.InteropServices
Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.IO
Imports System.Text
Imports VBOpenCV

Imports System.Windows.Forms
Imports System.Text.RegularExpressions
Imports System.Collections
Imports System.Collections.generic

'2018/3/16 FncBitmapToIplImage(iplImage)の内部をBitmapConverter.ToIplImageに置き換えてメモリリークを防いだ。
'2018/3/16 FncIplImageToBitmap(iplImage)を実装してclone処理で置き換え
'2018/3/20 usbカメラで新しくフレームを表示しないようにした。
'2018/3/21 マクロ機能を実装開始
'2018/4/2  bmpファイルを読めるようにした。usbカメラのバグ修正。
'2018/4/15 マクロの機能追加
'2020/3/7  マクロの機能削除
'2020/4/8  公開?

Public Class frmMain

    Private bBinFlag As Boolean

    Public sSavePath As String = System.Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\" + My.Application.Info.AssemblyName

    'Private iFileNo As Integer = 0
    Private filename As String

    'Private beforeValue1 As Double
    'Private beforeValue2 As Double
    'Private beforeValue3 As Double
    'Private beforeValue4 As Double
    
    Private vbOpenCv As New VBOpenCV


    Public Sub UndoImageProc(ByRef picImage As PictureBox)

        vbOpenCv.bmpCurrentImage = vbOpenCv.bmpUndoImage.Clone
        vbOpenCv.DrawImage(picImage)

    End Sub

    Public Sub OpenImageProc()
        vbOpenCv.ClearData()
        vbOpenCv.OpenFile(vbOpenCv.bmpOriginalImage)
        If Not (vbOpenCv.bmpOriginalImage Is Nothing) Then
            vbOpenCv.CopyImage(vbOpenCv.bmpOriginalImage, vbOpenCv.bmpCurrentImage)
            if vbOpenCv.bmpCurrentImage is nothing then
                msgbox("read error!")
            end if
            vbOpenCv.DrawImage(picImage)
        End If

    End Sub


    Private Sub SmoothProc(ByRef bmpImage As Bitmap, ByVal iType As Integer)
        Dim dlg As New frmSet
        Dim value1 As Double = 0
        Dim value2 As Double = 0
        Dim value3 As Double = 0

        dlg.Label1.Text = "パラメータ"
        dlg.paranum = 1
        Select Case iType
            Case 0
                'SmoothType.Blur
                dlg.paranum = 1
                dlg.txtValue1.Text = "5"
            Case 1
                'SmoothType.Gaussian
                dlg.paranum = 1
                dlg.txtValue1.Text = "5"
            Case 2
                'SmoothType.Median
                dlg.paranum = 1
                dlg.txtValue1.Text = "5"
            Case 3
                'Bilateral
                dlg.paranum = 3
                dlg.Label1.Text = "アパーチャサイズ"
                dlg.Label2.Text = "	空間領域のシグマ"
                dlg.Label3.Text = "色領域のシグマ"
                dlg.txtValue1.Text = "25"
                dlg.txtValue2.Text = "1"
                dlg.txtValue3.Text = "1"
            Case Else
                dlg.paranum = 1
                dlg.txtValue1.Text = "5"
        End Select

        dlg.ShowDialog()
        If dlg.canceled = True Then
            Exit Sub
        End If

        dlg.Dispose()

        Select Case iType
            Case 1, 2
                value1 = dlg.value1
            Case 3
                value1 = dlg.value1
                value2 = dlg.value2
                value3 = dlg.value3
            Case Else
                value1 = dlg.value1
        End Select

        vbOpenCv.Smooth(bmpImage, iType, value1, value2, value3)
    End Sub

    Private Sub CannyProc(ByRef bmpImage As Bitmap)
        Dim dlg As New frmSet
        Dim value1 As Double = 50
        Dim value2 As Double = 200
        Dim value3 As Double = 3

        dlg.paranum = 3
        dlg.Label1.Text = "パラメータ1"
        dlg.Label2.Text = "パラメータ2"
        dlg.Label3.Text = "パラメータ3"
        dlg.txtValue1.Text = value1.ToString
        dlg.txtValue2.Text = value2.ToString
        dlg.txtValue3.Text = value3.ToString

        dlg.ShowDialog()
        If dlg.canceled = True Then
            Exit Sub
        End If
        value1 = dlg.value1
        value2 = dlg.value2
        value3 = dlg.value3
        dlg.Dispose()
        
        vbOpenCv.Canny(bmpImage, value1, value2, value3)
        
    End Sub

    Private Sub ErodeProc(ByRef bmpImage As Bitmap)
        Dim dlg As New frmSet
        Dim value1 As Double = 0

        dlg.Label1.Text = "パラメータ"
        dlg.txtValue1.Text = "1"

        dlg.ShowDialog()
        If dlg.canceled = True Then
            Exit Sub
        End If
        value1 = dlg.value1

        dlg.Dispose()

        vbOpenCv.Erode(bmpImage, value1)

    End Sub

    Private Sub DilateProc(ByRef bmpImage As Bitmap)
        Dim dlg As New frmSet
        Dim value1 As Double = 0

        dlg.Label1.Text = "パラメータ"
        dlg.txtValue1.Text = "1"

        dlg.ShowDialog()
        If dlg.canceled = True Then
            Exit Sub
        End If
        value1 = dlg.value1

        dlg.Dispose()
        
        vbOpenCv.Dilate(bmpImage, value1)
    End Sub


    Private Sub UnSharpMaskingProc(ByRef bmpImage As Bitmap, ByRef value1 As Double)

        Dim dlg As New frmSet
        'Dim value1 As Double = 9

        dlg.paranum = 1
        dlg.Label1.Text = "パラメータ(K)"
        dlg.txtValue1.Text = value1.ToString

        dlg.ShowDialog()
        If dlg.canceled = True Then
            Exit Sub
        End If

        value1 = dlg.value1
        dlg.Dispose()
        vbOpenCv.UnSharpMasking(bmpImage, value1)

    End Sub

    Private Sub ApproxPolyContoursProc()
        Dim dlg As New frmSet
        Dim value1 As Double = 0

        dlg.Label1.Text = "パラメータ(Level)"
        dlg.txtValue1.Text = "1"

        dlg.ShowDialog()
        If dlg.canceled = True Then
            Exit Sub
        End If
        value1 = dlg.value1

        dlg.Dispose()
        'vbOpenCv.ApproxPolyContours(vbOpenCv.bmpCurrentImage, value1)
        vbOpenCv.ApproxPolyContours(vbOpenCv.bmpCurrentImage)
    End Sub


    Private Function ClippingProc(ByRef bmpImage As Bitmap, ByRef value1 As Double, ByRef value2 As Double, ByRef value3 As Double, ByRef value4 As Double) As String
        Dim dlg As New frmSet

        dlg.paranum = 4
        dlg.Label1.Text = "切り抜き位置(x1,y1,x2,y2)"
        dlg.txtValue1.Text = value1.ToString
        dlg.txtValue2.Text = value2.ToString
        dlg.txtValue3.Text = value3.ToString
        dlg.txtValue4.Text = value4.ToString

        dlg.ShowDialog()
        If dlg.canceled = True Then
            Exit Function
        End If

        value1 = dlg.value1
        value2 = dlg.value2
        value3 = dlg.value3
        value4 = dlg.value4
        dlg.Dispose()

        Return vbOpenCv.Clipping(bmpImage, value1, value2, value3, value4)

    End Function

    Public Sub ContrastProc(ByRef bmpImage As Bitmap, ByRef value1 As Double)
        Dim dlg As New frmSet
        'Dim value1 As Double = 0
        Dim value2 As Double = 0
        Dim value3 As Double = 0
        Dim value4 As Double = 0

        dlg.Label1.Text = "パラメータ"
        dlg.paranum = 1
        dlg.txtValue1.Text = "128"

        dlg.ShowDialog()
        If dlg.canceled = True Then
            Exit Sub
        End If
        value1 = dlg.value1
        dlg.Dispose()

        vbOpenCv.Contrast(bmpImage, value1)

    End Sub


    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        bBinFlag = False
        StatusStrip1.Items.Add("")
        'picImage.Location = New Point(0, 25)
        '2017/08/11 opencvと型が被るので明示的に宣言
        picImage.Location = New System.Drawing.Point(0, 25)
        picImage.SizeMode = PictureBoxSizeMode.AutoSize

        UndoMenuItem.Enabled = False
        
        '1 MainLeft
        Me.Left = CInt(GetSetting(My.Application.Info.ProductName + "_NET", "Settings", "MainLeft", "30"))
        '2 MainTop
        Me.Top = CInt(GetSetting(My.Application.Info.ProductName + "_NET", "Settings", "MainTop", "30"))
        '3 MainWidth
        Me.Width = CInt(GetSetting(My.Application.Info.ProductName + "_NET", "Settings", "MainWidth", "600"))
        '4 MainHeight
        Me.Height = CInt(GetSetting(My.Application.Info.ProductName + "_NET", "Settings", "MainHeight", "600"))
        '5 Scale
        vbOpenCv.dScale = CDbl(GetSetting(My.Application.Info.ProductName + "_NET", "Settings", "Scale", "1.0"))
        If vbOpenCv.dScale = 0 Then
            vbOpenCv.dScale = 1.0
        End If
        If vbOpenCv.dScale = 1.0 Then
            縮小して表示25ToolStripMenuItem.Checked = False
            縮小して表示50ToolStripMenuItem.Checked = False
            等倍で表示ToolStripMenuItem.Checked = True
        ElseIf vbOpenCv.dScale = 0.5 Then
            縮小して表示25ToolStripMenuItem.Checked = False
            縮小して表示50ToolStripMenuItem.Checked = True
            等倍で表示ToolStripMenuItem.Checked = False
        Else
            縮小して表示25ToolStripMenuItem.Checked = True
            縮小して表示50ToolStripMenuItem.Checked = False
            等倍で表示ToolStripMenuItem.Checked = False
        End If

    End Sub

    Private Sub 開くOToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 開くOToolStripMenuItem.Click

        OpenImageProc()
    End Sub

    Private Sub 右９０度回転ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 右９０度回転ToolStripMenuItem.Click
        If vbOpenCv.CheckCurrentImage(vbOpenCv.bmpCurrentImage) = False Then
            Exit Sub
        End If
        vbOpenCv.CopyImage(vbOpenCv.bmpCurrentImage, vbOpenCv.bmpUndoImage)
        UndoMenuItem.Enabled = True
        vbOpenCv.bmpCurrentImage.RotateFlip(RotateFlipType.Rotate90FlipNone)
        vbOpenCv.DrawImage(picImage)
    End Sub

    Private Sub ヒスト平均ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub 適応的二値化ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 適応的二値化ToolStripMenuItem.Click
        If vbOpenCv.CheckCurrentImage(vbOpenCv.bmpCurrentImage) = False Then
            Exit Sub
        End If
        'Dim iThreshold As Integer
        'iThreshold = FncAutoGetImageThreshold(bmpLayerImage(1))
        'Binarization(bmpLayerImage(1), iThreshold, bmpLayerImage(2))
        vbOpenCv.CopyImage(vbOpenCv.bmpCurrentImage, vbOpenCv.bmpUndoImage)
        UndoMenuItem.Enabled = True

        vbOpenCv.AdaptiveBinarization(vbOpenCv.bmpCurrentImage)

        vbOpenCv.DrawImage(picImage)
    End Sub

    Private Sub 名前を付けて保存AToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 名前を付けて保存AToolStripMenuItem.Click
        If vbOpenCv.CheckCurrentImage(vbOpenCv.bmpCurrentImage) = False Then
            Exit Sub
        End If

        Dim sfd As New SaveFileDialog
        sfd.DefaultExt = "jpg"
        'sfd.Filter = "jpgファイル(*.jpg)|*.jpg|pgmファイル(*.pgm)|*.pgm"
        sfd.Filter = "jpgファイル(*.jpg)|*.jpg"
        If sfd.ShowDialog() <> DialogResult.OK Then
            Return
        End If

        vbOpenCv.bmpCurrentImage.Save(sfd.FileName, System.Drawing.Imaging.ImageFormat.Jpeg)
        MsgBox("保存しました。")

    End Sub

    Private Sub 終了XToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 終了XToolStripMenuItem.Click
        SubEndProc()
        Application.Exit()
    End Sub

    Private Sub テンプレートマッチングToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles テンプレートマッチングToolStripMenuItem.Click
        vbOpenCv.CopyImage(vbOpenCv.bmpCurrentImage, vbOpenCv.bmpUndoImage)
        UndoMenuItem.Enabled = True
        vbOpenCv.TemplateMating(vbOpenCv.bmpCurrentImage)
        vbOpenCv.DrawImage(picImage)

    End Sub

    Private Sub コントラスト処理カラーToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles コントラスト処理カラーToolStripMenuItem.Click

        If vbOpenCv.CheckCurrentImage(vbOpenCv.bmpCurrentImage) = False Then
            Exit Sub
        End If
        vbOpenCv.CopyImage(vbOpenCv.bmpCurrentImage, vbOpenCv.bmpUndoImage)
        UndoMenuItem.Enabled = True

        Dim value1 As Double
        ContrastProc(vbOpenCv.bmpCurrentImage, value1)
        vbOpenCv.DrawImage(picImage)

    End Sub

    Private Sub 現在の画像を保存ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 現在の画像を保存ToolStripMenuItem.Click
        If vbOpenCv.CheckCurrentImage(vbOpenCv.bmpCurrentImage) = False Then
            Exit Sub
        End If

        If filename = "" Then
            Exit Sub
        End If
        vbOpenCv.bmpCurrentImage.Save(filename, System.Drawing.Imaging.ImageFormat.Jpeg)
        MsgBox("保存しました。")
    End Sub

    Private Sub グレイスケールToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles グレイスケールToolStripMenuItem.Click

        If vbOpenCv.CheckCurrentImage(vbOpenCv.bmpCurrentImage) = False Then
            Exit Sub
        End If
        vbOpenCv.CopyImage(vbOpenCv.bmpCurrentImage, vbOpenCv.bmpUndoImage)
        UndoMenuItem.Enabled = True

        vbOpenCv.GrayScale(vbOpenCv.bmpCurrentImage)
        vbOpenCv.DrawImage(picImage)

    End Sub


    Private Sub 左９０度回転ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 左９０度回転ToolStripMenuItem.Click
        If vbOpenCv.CheckCurrentImage(vbOpenCv.bmpCurrentImage) = False Then
            Exit Sub
        End If
        vbOpenCv.CopyImage(vbOpenCv.bmpCurrentImage, vbOpenCv.bmpUndoImage)
        UndoMenuItem.Enabled = True
        vbOpenCv.bmpCurrentImage.RotateFlip(RotateFlipType.Rotate270FlipNone)
        vbOpenCv.DrawImage(picImage)
    End Sub

    Private Sub 上下反転ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 上下反転ToolStripMenuItem.Click
        If vbOpenCv.CheckCurrentImage(vbOpenCv.bmpCurrentImage) = False Then
            Exit Sub
        End If
        vbOpenCv.CopyImage(vbOpenCv.bmpCurrentImage, vbOpenCv.bmpUndoImage)
        UndoMenuItem.Enabled = True
        vbOpenCv.bmpCurrentImage.RotateFlip(RotateFlipType.RotateNoneFlipY)
        vbOpenCv.DrawImage(picImage)
    End Sub

    Private Sub 左右反転ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 左右反転ToolStripMenuItem.Click
        If vbOpenCv.CheckCurrentImage(vbOpenCv.bmpCurrentImage) = False Then
            Exit Sub
        End If
        vbOpenCv.CopyImage(vbOpenCv.bmpCurrentImage, vbOpenCv.bmpUndoImage)
        UndoMenuItem.Enabled = True
        vbOpenCv.bmpCurrentImage.RotateFlip(RotateFlipType.RotateNoneFlipX)
        vbOpenCv.DrawImage(picImage)
    End Sub


    Private Sub 回転ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 回転ToolStripMenuItem.Click
        Dim value1 As Double

        If vbOpenCv.CheckCurrentImage(vbOpenCv.bmpCurrentImage) = False Then
            Exit Sub
        End If

        Dim dlg As New frmSet

        dlg.paranum = 1
        dlg.Label1.Text = "角度(度)"
        dlg.ShowDialog()

        If dlg.canceled = True Then
            Exit Sub
        End If

        value1 = dlg.value1
        dlg.Dispose()

        If value1 <> 0 Then
            vbOpenCv.CopyImage(vbOpenCv.bmpCurrentImage, vbOpenCv.bmpUndoImage)
            UndoMenuItem.Enabled = True
            Dim canvas As New Bitmap(vbOpenCv.bmpCurrentImage.Width, vbOpenCv.bmpCurrentImage.Height)
            Dim g As Graphics = Graphics.FromImage(canvas)
            g.ResetTransform()
            g.RotateTransform(value1)
            g.DrawImage(vbOpenCv.bmpCurrentImage, New Rectangle(0, 0, vbOpenCv.bmpCurrentImage.Width, vbOpenCv.bmpCurrentImage.Height))
            vbOpenCv.bmpCurrentImage = canvas
            vbOpenCv.DrawImage(picImage)
        End If
    End Sub

    Private Sub 合成論理積ToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles 合成論理積ToolStripMenuItem.Click

        Dim tmp As Bitmap = Nothing

        vbOpenCv.OpenFile(tmp)
        vbOpenCv.CopyImage(vbOpenCv.bmpCurrentImage, vbOpenCv.bmpUndoImage)
        UndoMenuItem.Enabled = True
        'SubGrayScale(tmp)
        vbOpenCv.AndImage(vbOpenCv.bmpCurrentImage, tmp)
                
        vbOpenCv.DrawImage(picImage)

    End Sub


    Private Sub バージョン情報AToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles バージョン情報AToolStripMenuItem.Click
        'frmAbout.ShowDialog()
	'2020/1/28
	Dim dlg = New frmAbout
	dlg.ShowDialog()

    End Sub


    Private Sub 縮小して表示50ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 縮小して表示50ToolStripMenuItem.Click
        縮小して表示50ToolStripMenuItem.Checked = True
        縮小して表示25ToolStripMenuItem.Checked = False
        縮小して表示10ToolStripMenuItem.Checked = False
        等倍で表示ToolStripMenuItem.Checked = False
        vbOpenCv.dScale = 0.5
        vbOpenCv.DrawImage(picImage)
    End Sub

    Private Sub 縮小して表示25ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 縮小して表示25ToolStripMenuItem.Click
        縮小して表示50ToolStripMenuItem.Checked = False
        縮小して表示25ToolStripMenuItem.Checked = True
        縮小して表示10ToolStripMenuItem.Checked = False
        等倍で表示ToolStripMenuItem.Checked = False
        vbOpenCv.dScale = 0.25
        vbOpenCv.DrawImage(picImage)

    End Sub

    Private Sub 等倍で表示ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 等倍で表示ToolStripMenuItem.Click
        縮小して表示50ToolStripMenuItem.Checked = False
        縮小して表示25ToolStripMenuItem.Checked = False
        縮小して表示10ToolStripMenuItem.Checked = False
        等倍で表示ToolStripMenuItem.Checked = True
        vbOpenCv.dScale = 1
        vbOpenCv.DrawImage(picImage)

    End Sub

    Private Sub UndoMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UndoMenuItem.Click
        If vbOpenCv.bmpUndoImage Is Nothing Then
            Exit Sub
        End If

        UndoImageProc(picImage)
        UndoMenuItem.Enabled = False
    End Sub

    Private Sub 縮小MenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 縮小MenuItem.Click
        If vbOpenCv.CheckCurrentImage(vbOpenCv.bmpCurrentImage) = False Then
            Exit Sub
        End If
        vbOpenCv.CopyImage(vbOpenCv.bmpCurrentImage, vbOpenCv.bmpUndoImage)
        UndoMenuItem.Enabled = True
        vbOpenCv.ScaleCopyImage(vbOpenCv.bmpUndoImage, vbOpenCv.bmpCurrentImage, 0.5)
        vbOpenCv.DrawImage(picImage)
    End Sub

    Private Sub 拡大MenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 拡大MenuItem.Click
        If vbOpenCv.CheckCurrentImage(vbOpenCv.bmpCurrentImage) = False Then
            Exit Sub
        End If
        vbOpenCv.CopyImage(vbOpenCv.bmpCurrentImage, vbOpenCv.bmpUndoImage)
        UndoMenuItem.Enabled = True
        vbOpenCv.ScaleCopyImage(vbOpenCv.bmpUndoImage, vbOpenCv.bmpCurrentImage, 2)
        vbOpenCv.DrawImage(picImage)
    End Sub


    Private Sub フィルタブラーToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles フィルタブラーToolStripMenuItem.Click
        If vbOpenCv.CheckCurrentImage(vbOpenCv.bmpCurrentImage) = False Then
            Exit Sub
        End If

        vbOpenCv.CopyImage(vbOpenCv.bmpCurrentImage, vbOpenCv.bmpUndoImage)
        UndoMenuItem.Enabled = True
        SmoothProc(vbOpenCv.bmpCurrentImage, 0)
        vbOpenCv.DrawImage(picImage)

    End Sub

    Private Sub フィルタメディアンToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles フィルタメディアンToolStripMenuItem.Click
        If vbOpenCv.CheckCurrentImage(vbOpenCv.bmpCurrentImage) = False Then
            Exit Sub
        End If
        vbOpenCv.CopyImage(vbOpenCv.bmpCurrentImage, vbOpenCv.bmpUndoImage)
        UndoMenuItem.Enabled = True
        SmoothProc(vbOpenCv.bmpCurrentImage, 1)
        vbOpenCv.DrawImage(picImage)

    End Sub

    Private Sub フィルタガウシアンToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles フィルタガウシアンToolStripMenuItem.Click
        If vbOpenCv.CheckCurrentImage(vbOpenCv.bmpCurrentImage) = False Then
            Exit Sub
        End If
        vbOpenCv.CopyImage(vbOpenCv.bmpCurrentImage, vbOpenCv.bmpUndoImage)
        UndoMenuItem.Enabled = True
        SmoothProc(vbOpenCv.bmpCurrentImage, 2)
        vbOpenCv.DrawImage(picImage)

    End Sub

    Private Sub フィルタバイラテラルToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles フィルタバイラテラルToolStripMenuItem.Click
        If vbOpenCv.CheckCurrentImage(vbOpenCv.bmpCurrentImage) = False Then
            Exit Sub
        End If
        vbOpenCv.CopyImage(vbOpenCv.bmpCurrentImage, vbOpenCv.bmpUndoImage)
        UndoMenuItem.Enabled = True
        SmoothProc(vbOpenCv.bmpCurrentImage, 3)
        vbOpenCv.DrawImage(picImage)

    End Sub

    Private Sub ヒスト平均ToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ヒスト平均ToolStripMenuItem1.Click
        If vbOpenCv.CheckCurrentImage(vbOpenCv.bmpCurrentImage) = False Then
            Exit Sub
        End If
        vbOpenCv.CopyImage(vbOpenCv.bmpCurrentImage, vbOpenCv.bmpUndoImage)
        UndoMenuItem.Enabled = True
        vbOpenCv.HistStretch(vbOpenCv.bmpCurrentImage)
        vbOpenCv.DrawImage(picImage)

    End Sub

    Private Sub 二値化手動ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 二値化手動ToolStripMenuItem.Click

        Dim dlg As New frmSet
        Dim value1 As Double = 0

        dlg.Label1.Text = "パラメータ"
        dlg.paranum = 1
        dlg.txtValue1.Text = "128"

        dlg.ShowDialog()
        If dlg.canceled = True Then
            Exit Sub
        End If
        value1 = dlg.value1
        dlg.Dispose()

        If vbOpenCv.CheckCurrentImage(vbOpenCv.bmpCurrentImage) = False Then
            Exit Sub
        End If
        vbOpenCv.CopyImage(vbOpenCv.bmpCurrentImage, vbOpenCv.bmpUndoImage)
        UndoMenuItem.Enabled = True

        vbOpenCv.Binarization(vbOpenCv.bmpCurrentImage, CInt(value1))

        vbOpenCv.DrawImage(picImage)

    End Sub

    Private Sub フィルタソーベルToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles フィルタソーベルToolStripMenuItem.Click
        If vbOpenCv.CheckCurrentImage(vbOpenCv.bmpCurrentImage) = False Then
            Exit Sub
        End If
        vbOpenCv.CopyImage(vbOpenCv.bmpCurrentImage, vbOpenCv.bmpUndoImage)
        UndoMenuItem.Enabled = True
        vbOpenCv.Sobel(vbOpenCv.bmpCurrentImage)
        vbOpenCv.DrawImage(picImage)

    End Sub

    Private Sub フィルタラプラシアンToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles フィルタラプラシアンToolStripMenuItem.Click
        If vbOpenCv.CheckCurrentImage(vbOpenCv.bmpCurrentImage) = False Then
            Exit Sub
        End If
        vbOpenCv.CopyImage(vbOpenCv.bmpCurrentImage, vbOpenCv.bmpUndoImage)
        UndoMenuItem.Enabled = True
        vbOpenCv.Laplacian(vbOpenCv.bmpCurrentImage)
        vbOpenCv.DrawImage(picImage)
    End Sub

    Private Sub フィルタキャニーToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles フィルタキャニーToolStripMenuItem.Click
        If vbOpenCv.CheckCurrentImage(vbOpenCv.bmpCurrentImage) = False Then
            Exit Sub
        End If
        vbOpenCv.CopyImage(vbOpenCv.bmpCurrentImage, vbOpenCv.bmpUndoImage)
        UndoMenuItem.Enabled = True
        CannyProc(vbOpenCv.bmpCurrentImage)
        vbOpenCv.DrawImage(picImage)
    End Sub

    Private Sub 収縮ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 収縮ToolStripMenuItem.Click
        If vbOpenCv.CheckCurrentImage(vbOpenCv.bmpCurrentImage) = False Then
            Exit Sub
        End If
        vbOpenCv.CopyImage(vbOpenCv.bmpCurrentImage, vbOpenCv.bmpUndoImage)
        UndoMenuItem.Enabled = True
        DilateProc(vbOpenCv.bmpCurrentImage)
        vbOpenCv.DrawImage(picImage)

    End Sub

    Private Sub 膨張ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 膨張ToolStripMenuItem.Click
        If vbOpenCv.CheckCurrentImage(vbOpenCv.bmpCurrentImage) = False Then
            Exit Sub
        End If
        vbOpenCv.CopyImage(vbOpenCv.bmpCurrentImage, vbOpenCv.bmpUndoImage)
        UndoMenuItem.Enabled = True
        ErodeProc(vbOpenCv.bmpCurrentImage)
        vbOpenCv.DrawImage(picImage)

    End Sub


    Private Sub 輪郭線抽出ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 輪郭線抽出ToolStripMenuItem.Click
        If vbOpenCv.CheckCurrentImage(vbOpenCv.bmpCurrentImage) = False Then
            Exit Sub
        End If
        vbOpenCv.CopyImage(vbOpenCv.bmpCurrentImage, vbOpenCv.bmpUndoImage)
        UndoMenuItem.Enabled = True
        vbOpenCv.ApproxPolyContours(vbOpenCv.bmpCurrentImage)
        vbOpenCv.DrawImage(picImage)

    End Sub

    Private Sub フィルタアンシャープマスキングToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles フィルタアンシャープマスキングToolStripMenuItem.Click
        If vbOpenCv.CheckCurrentImage(vbOpenCv.bmpCurrentImage) = False Then
            Exit Sub
        End If
        vbOpenCv.CopyImage(vbOpenCv.bmpCurrentImage, vbOpenCv.bmpUndoImage)
        UndoMenuItem.Enabled = True
        Dim value1 As Double = 9
        vbOpenCv.UnSharpMasking(vbOpenCv.bmpCurrentImage, value1)
        vbOpenCv.DrawImage(picImage)

    End Sub

    Private Sub 論理反転ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 論理反転ToolStripMenuItem.Click
        vbOpenCv.CopyImage(vbOpenCv.bmpCurrentImage, vbOpenCv.bmpUndoImage)
        UndoMenuItem.Enabled = True
        vbOpenCv.NotImage(vbOpenCv.bmpCurrentImage)
        vbOpenCv.DrawImage(picImage)
    End Sub

    Sub SubEndProc()
        'レジストリに保存
        SubSave()

    End Sub

    Private Sub SubSave()
        'Dim iTmp As Integer
        If Me.Left >= 0 Then
            '1 MainLeft
            SaveSetting(My.Application.Info.ProductName + "_NET", "Settings", "MainLeft", CStr(Me.Left))
        End If
        If Me.Top >= 0 Then
            '2 MainTop
            SaveSetting(My.Application.Info.ProductName + "_NET", "Settings", "MainTop", CStr(Me.Top))
        End If
        If Me.Width >= 0 Then
            '3 MainWidth
            SaveSetting(My.Application.Info.ProductName + "_NET", "Settings", "MainWidth", CStr(Me.Width))
        End If
        If Me.Height >= 0 Then
            '4 MainHeight
            SaveSetting(My.Application.Info.ProductName + "_NET", "Settings", "MainHeight", CStr(Me.Height))
        End If

        SaveSetting(My.Application.Info.ProductName + "_NET", "Settings", "Scale", CStr(vbOpenCv.dScale))



    End Sub

    Private Sub frmMain_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If e.CloseReason <> CloseReason.ApplicationExitCall Then
            e.Cancel = True ' フォームが閉じるのをキャンセル
            Me.Visible = False ' フォームの非表示
        End If

    End Sub

    Private Sub ToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem2.Click
        Dim value1 As Double

        If vbOpenCv.CheckCurrentImage(vbOpenCv.bmpCurrentImage) = False Then
            Exit Sub
        End If

        Dim dlg As New frmSet

        dlg.paranum = 1
        dlg.Label1.Text = "倍率"
        dlg.ShowDialog()

        If dlg.canceled = True Then
            Exit Sub
        End If

        value1 = dlg.value1
        dlg.Dispose()

        If vbOpenCv.CheckCurrentImage(vbOpenCv.bmpCurrentImage) = False Then
            Exit Sub
        End If
        vbOpenCv.CopyImage(vbOpenCv.bmpCurrentImage, vbOpenCv.bmpUndoImage)
        UndoMenuItem.Enabled = True
        vbOpenCv.ScaleCopyImage(vbOpenCv.bmpUndoImage, vbOpenCv.bmpCurrentImage, value1)

        vbOpenCv.DrawImage(picImage)


    End Sub

    Private Sub 切り抜きToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 切り抜きToolStripMenuItem.Click

        If vbOpenCv.CheckCurrentImage(vbOpenCv.bmpCurrentImage) = False Then
            Exit Sub
        End If
        vbOpenCv.CopyImage(vbOpenCv.bmpCurrentImage, vbOpenCv.bmpUndoImage)
        UndoMenuItem.Enabled = True

        Dim value1 As Double = 0
        Dim value2 As Double = 0
        Dim value3 As Double = 0
        Dim value4 As Double = 0
        'If beforeValue1 = 0 And beforeValue2 = 0 And beforeValue3 = 0 And beforeValue4 = 0 Then
        value1 = 0
        value2 = 0
        value3 = vbOpenCv.bmpCurrentImage.Width
        value4 = vbOpenCv.bmpCurrentImage.Height
        'Else
        'value1 = beforeValue1
        'value2 = beforeValue2
        'value3 = beforeValue3
        'value4 = beforeValue4
        'End If

        Dim ret As String = vbOpenCv.Clipping(vbOpenCv.bmpCurrentImage, value1, value2, value3, value4)
        vbOpenCv.DrawImage(picImage)

        'beforeValue1 = value1
        'beforeValue2 = value2
        'beforeValue3 = value3
        'beforeValue4 = value4

    End Sub

    Private Sub USBカメラToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles USBカメラToolStripMenuItem.Click
        vbOpenCv.USBCameraProc(picImage)
    End Sub

    '2018/3/10
    Private Sub 縮小して表示10ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 縮小して表示10ToolStripMenuItem.Click
        縮小して表示50ToolStripMenuItem.Checked = False
        縮小して表示25ToolStripMenuItem.Checked = False
        縮小して表示10ToolStripMenuItem.Checked = True
        等倍で表示ToolStripMenuItem.Checked = False
        vbOpenCv.dScale = 0.1
        vbOpenCv.DrawImage(picImage)
    End Sub

    Private Sub 回転自動補正ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 回転自動補正ToolStripMenuItem.Click
        vbOpenCv.CopyImage(vbOpenCv.bmpCurrentImage, vbOpenCv.bmpUndoImage)
        UndoMenuItem.Enabled = True
        vbOpenCv.AutoAdjustAngle(vbOpenCv.bmpCurrentImage)
        vbOpenCv.DrawImage(picImage)

    End Sub

    Private Sub 新規画像作成ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 新規画像作成ToolStripMenuItem.Click
        vbOpenCv.bmpOriginalImage = New Bitmap(4962, 7014, System.Drawing.Imaging.PixelFormat.Format24bppRgb)
        filename = ".\新規画像.jpg"
        vbOpenCv.bmpOriginalImage.Save(filename, System.Drawing.Imaging.ImageFormat.Jpeg)
        vbOpenCv.OpenFile2(vbOpenCv.bmpOriginalImage, filename)
        vbOpenCv.CopyImage(vbOpenCv.bmpOriginalImage, vbOpenCv.bmpCurrentImage)

        vbOpenCv.DrawImage(picImage)
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        'https://dobon.net/vb/dotnet/system/cursorposition.html
        'マウスポインタの位置を取得する
        'X座標を取得する
        Dim x As Integer = Cursor.Position.X - Left - 10
        'Y座標を取得する
        Dim y As Integer = Cursor.Position.Y - Top - 80 'ツールバーの幅?

        'Dim keisu As Double = 0.7575 * (7 / 8) * (70 / 66)
        Dim keisu As Double = 1
        '拡大縮小を配慮
        x = x / vbOpenCv.dScale * keisu
        y = y / vbOpenCv.dScale * keisu

        Me.StatusStrip1.Items(0).Text = filename + ",x = " + x.ToString + ",y = " + y.ToString + "スクロール"

        'マウスポインタの位置を画面左上（座標 (0, 0)）にする
        'Cursor.Position = New System.Drawing.Point(0, 0)
    End Sub


    Private Sub サイズ変更ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles サイズ変更ToolStripMenuItem.Click
        Dim value1 As Double
        Dim value2 As Double

        If vbOpenCv.CheckCurrentImage(vbOpenCv.bmpCurrentImage) = False Then
            Exit Sub
        End If

        Dim dlg As New frmSet

        dlg.paranum = 2
        value1 = vbOpenCv.bmpCurrentImage.Width
        value2 = vbOpenCv.bmpCurrentImage.Height

        dlg.Text = "サイズ変更"
        dlg.Label1.Text = "幅"
        dlg.Label2.Text = "高さ"
        dlg.txtValue1.Text = value1.ToString
        dlg.txtValue2.Text = value2.ToString

        dlg.ShowDialog()

        If dlg.canceled = True Then
            Exit Sub
        End If

        value1 = dlg.value1
        value2 = dlg.value2

        dlg.Dispose()

        vbOpenCv.CopyImage(vbOpenCv.bmpCurrentImage, vbOpenCv.bmpUndoImage)
        UndoMenuItem.Enabled = True

        Dim posx As Integer = (value1 - vbOpenCv.bmpCurrentImage.Width) / 2

        If posx < 0 Then
            posx = 0
        End If

        Dim posy As Integer = (value2 - vbOpenCv.bmpCurrentImage.Height) / 2

        If posy < 0 Then
            posy = 0
        End If
        
        Dim bmpDestImage As Bitmap = Nothing

        vbOpenCv.ReSizeImage(vbOpenCv.bmpCurrentImage, bmpDestImage, posx, posy, value1, value2)

        vbOpenCv.CopyImage(bmpDestImage, vbOpenCv.bmpCurrentImage)

        vbOpenCv.DrawImage(picImage)
    End Sub


    Private Sub 文字入力ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 文字入力ToolStripMenuItem.Click

        If vbOpenCv.CheckCurrentImage(vbOpenCv.bmpCurrentImage) = False Then
            Exit Sub
        End If
        vbOpenCv.CopyImage(vbOpenCv.bmpCurrentImage, vbOpenCv.bmpUndoImage)
        UndoMenuItem.Enabled = True

        vbOpenCv.DrawString(vbOpenCv.bmpCurrentImage)

        vbOpenCv.DrawImage(picImage)


    End Sub
End Class
