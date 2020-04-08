Imports System.Runtime.InteropServices
Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.IO
Imports System.Text
Imports OpenCvSharp
Imports OpenCvSharp.CPlusPlus
Imports OpenCvSharp.Extensions

Imports System.Windows.Forms
Imports System.Text.RegularExpressions
Imports System.Collections
Imports System.Collections.generic


Public Class VBOpenCV

    <StructLayout(LayoutKind.Sequential)>
    Public Structure ImageData
        Public width As Integer
        Public height As Integer
        Public depth As Integer
        Public widthStep As Integer
        Public imageData As IntPtr
    End Structure


    Public alResult As ArrayList
    Public dScale As Double = 1
    Public bmpOriginalImage As Bitmap
    Public bmpCurrentImage As Bitmap
    Public bmpUndoImage As Bitmap
    Public bmpUndoImage2 As Bitmap

    Private bmpExtractImage As Bitmap
    Private bmpRegular As Bitmap

    Dim clBlock As New Collection

    Structure stString
        Dim X1 As Integer
        Dim Y1 As Integer
        Dim X2 As Integer
        Dim Y2 As Integer
        Dim RowNo As Integer
    End Structure

    Dim stStr() As stString

    Dim iStrNo As Integer = 0
    Dim iRowNo As Integer = 0

    Dim alPatternXStartPos As New ArrayList
    Dim alPatternXEndPos As New ArrayList
    Dim alPatternYStartPos As New ArrayList
    Dim alPatternYEndPos As New ArrayList

    Public iYHistogram() As Integer
    Public iXHistogram() As Integer
    Public iXHistogram2() As Integer
    Private iHist(255) As Integer

    Private iYStartPos As Integer
    Private iYEndPos As Integer
    Private iXStartPos As Integer
    Private iXEndPos As Integer

    Private iStringYStartPos As Integer
    Private iStringYEndPos As Integer
    Private iStringXStartPos As Integer
    Private iStringXEndPos As Integer

    Public iExtractStringAreaXThreshold As Integer
    Public iExtractStringAreaYThreshold As Integer
    Public iExtractStringAreaXOffset As Integer
    Public iExtractStringAreaYOffset As Integer

    Private bBinFlag As Boolean

    Private iImageTop As Integer = 0

    Public sSavePath As String = System.Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\" + My.Application.Info.AssemblyName

    Public iplImage As New IplImage

    Private iFileNo As Integer = 0
    Private filename As String

    Private beforeValue1 As Double
    Private beforeValue2 As Double
    Private beforeValue3 As Double
    Private beforeValue4 As Double

    Public Sub ClearData()
        iXStartPos = 0
        iXEndPos = 0
        iYStartPos = 0
        iYEndPos = 0
        bmpCurrentImage = Nothing
        bmpOriginalImage = Nothing
        bmpUndoImage = Nothing
        bmpExtractImage = Nothing
        bmpRegular = Nothing
        iStringYStartPos = 0
        iStringYEndPos = 0
        iStringXStartPos = 0
        iStringXEndPos = 0
        iXHistogram = Nothing
        iYHistogram = Nothing

    End Sub

    Public Sub DrawImage(picImage As PictureBox)
        If Not bmpCurrentImage Is Nothing Then

            Dim canvas As New Bitmap(CInt(bmpCurrentImage.Width * dScale), CInt(bmpCurrentImage.Height * dScale))

            Dim g0 As Graphics = Graphics.FromImage(canvas)

            g0.DrawImage(bmpCurrentImage, 0, 0, CInt(bmpCurrentImage.Width * dScale), CInt(bmpCurrentImage.Height * dScale))
            picImage.Image = canvas

            g0.Dispose()
        Else
            Exit Sub
        End If

        Dim g As Graphics = Graphics.FromImage(picImage.Image)

        If bBinFlag Then
            '
            For iLoop As Integer = 0 To clBlock.Count - 1
                Try
                    Dim iX1 As Integer = clBlock.Item(iLoop + 1).Item("X1") / 4
                    Dim iX2 As Integer = clBlock.Item(iLoop + 1).Item("X2") / 4
                    Dim iY1 As Integer = clBlock.Item(iLoop + 1).Item("Y1") / 4
                    Dim iY2 As Integer = clBlock.Item(iLoop + 1).Item("Y2") / 4
                    g.DrawRectangle(Pens.Red, iX1, iImageTop + iY1, iX2 - iX1 + 1, iY2 - iY1 + 1)
                Catch ex As Exception
                End Try
            Next

            If stStr Is Nothing Then
            Else
                For iLoop As Integer = 0 To stStr.Length - 1
                    Try
                        Dim iX1 As Integer = stStr(iLoop).X1 / 4
                        Dim iX2 As Integer = stStr(iLoop).X2 / 4
                        Dim iY1 As Integer = stStr(iLoop).Y1 / 4
                        Dim iY2 As Integer = stStr(iLoop).Y2 / 4
                        g.DrawRectangle(Pens.Red, iX1, iImageTop + iY1, iX2 - iX1 + 1, iY2 - iY1 + 1)
                    Catch ex As Exception
                    End Try
                Next
            End If

        End If

        g.Dispose()

    End Sub


    Public Sub AndImage(ByRef bmpImage As Bitmap, ByRef img As Bitmap)

        If CheckCurrentImage(bmpImage) = False Then
            Exit Sub
        End If

        Dim tmp As New IplImage

        iplImage = BitmapToIplImage(bmpImage)
        tmp = Cv.CreateImage(Cv.GetSize(iplImage), BitDepth.U8, 3)
        tmp = BitmapToIplImage(img)
        Cv.And(iplImage, tmp, iplImage)
        
        bmpImage = IplImageToBitmap(iplImage)
        Cv.ReleaseImage(tmp)

    End Sub

    Public Function GetFilename(ByVal sRootPath As String, ByVal sPattern As String) As String()
        Dim sFileNames() As String = Nothing
        Dim iLoop As Integer
        ' このディレクトリ内のすべてのファイルを検索する
        For Each sFilePath As String In Directory.GetFiles(sRootPath, sPattern)
            ReDim Preserve sFileNames(iLoop)
            sFileNames(iLoop) = sFilePath
            iLoop += 1
        Next sFilePath

        Return sFileNames
    End Function


    Public Function AdjustBin(ByVal iInput As Integer) As Integer
        If iInput < 0 Then
            Return 0
        ElseIf iInput > 255 Then
            Return 255
        End If
        Return iInput
    End Function

    Public Sub SetHist(ByVal bmpImage As Bitmap)
        Dim iX As Integer
        Dim iY As Integer
        Dim getC As Color

        'クリア
        'Me.StatusStrip1.Items(0).Text = "ヒストグラムクリア中です。"
        For iY = 0 To bmpImage.Height - 1
            For iX = 0 To bmpImage.Width - 1
                getC = bmpImage.GetPixel(iX, iY)
                iHist(getC.R) = 0
            Next

        Next

        For iY = 0 To bmpImage.Height - 1
            For iX = 0 To bmpImage.Width - 1
                getC = bmpImage.GetPixel(iX, iY)
                iHist(getC.R) += 1
            Next
            Application.DoEvents()
            'Me.StatusStrip1.Items(0).Text = "ヒストグラム" + (iY + 1).ToString + "/" + (bmpImage.Height).ToString + "処理中です。"
        Next

    End Sub

    Public Sub MyHistStretch(ByRef bmpImage As Bitmap)
        Dim iX As Integer
        Dim iY As Integer
        Dim igetC(2) As Integer

        SetHist(bmpImage)

        Dim iLt As Integer
        Dim iHistThreshold As Integer
        iHistThreshold = bmpImage.Width * bmpImage.Height * 0.015

        Dim i As Integer

        For i = 0 To 255
            iLt += iHist(i)
            If iLt > iHistThreshold Then
                iLt = i
                Exit For
            End If
        Next

        Dim iHt As Integer
        For i = 255 To 0 Step -1
            iHt += iHist(i)
            If iHt > iHistThreshold Then
                iHt = i
                Exit For
            End If
        Next

        Dim iOriginalRange As Integer = iHt - iLt + 1
        Dim bStretchFactor As Double = 1.0
        Dim bStretchedRange As Double = CDbl(iOriginalRange) + bStretchFactor * CDbl(255 - iOriginalRange)
        Dim bScaleFacter As Double = bStretchedRange / iOriginalRange

        Dim bmpImageData As New bmpData24(bmpImage)

        For iY = 0 To bmpImage.Height - 1
            For iX = 0 To bmpImage.Width - 1
                igetC = bmpImageData.GetPixel(iX, iY)
                Dim iValue As Integer
                iValue = CInt(bScaleFacter * igetC(0)) - iLt
                iValue = AdjustBin(iValue)
                igetC(0) = iValue
                igetC(1) = iValue
                igetC(2) = iValue
                bmpImageData.SetPixel(iX, iY, igetC)
            Next
        Next

        bmpImageData.Dispose()

    End Sub

    Public Sub MyBinarization(ByRef bmpImage As Bitmap, ByVal iThreshold As Integer)
        Dim iX As Integer
        Dim iY As Integer
        Dim iGray As Integer
        Dim igetC(2) As Integer
        Dim iSum As Integer = 0

        'Me.StatusStrip1.Items(0).Text = "２値化の処理中..."
        'Application.DoEvents()

        'コントラスト
        'For iY = 0 To bmpImage.Height - 1
        '    For iX = 0 To bmpImage.Width - 1
        '        getC = bmpImage.GetPixel(iX, iY)
        '        Dim v As Double
        '        v = 0.1
        '        iGray = (getC.R - iThreshold) * (1 + v) * (1 + v) + iThreshold
        '        If iGray < 0 Then
        '            iGray = 0
        '        End If
        '        If iGray > 255 Then
        '            iGray = 255
        '        End If
        '        bmpImage.SetPixel(iX, iY, Color.FromArgb(iGray, iGray, iGray))

        '    Next
        'Next

        'ヒストグラム平均
        Dim bmpTempImage = New Bitmap(bmpImage.Width, bmpImage.Height)

        '2値化
        Dim bmpInputData As New bmpData24(bmpImage)
        Dim bmpOutputData As New bmpData24(bmpTempImage)
        For iY = 0 To bmpUndoImage.Height - 1
            For iX = 0 To bmpImage.Width - 1
                igetC = bmpInputData.GetPixel(iX, iY)
                If igetC(0) > iThreshold Then
                    iGray = 255
                Else
                    iGray = 0
                End If
                igetC(0) = iGray
                igetC(1) = iGray
                igetC(2) = iGray
                bmpOutputData.SetPixel(iX, iY, igetC)
                'bmpOutputImage.SetPixel(iX, iY, Color.FromArgb(iGray, iGray, iGray))

            Next
        Next

        bmpInputData.Dispose()
        bmpOutputData.Dispose()

        bmpImage = bmpTempImage

        bBinFlag = True

    End Sub


    Public Sub MyFilter2(ByRef bmpImage As Bitmap, ByVal bFilter(,) As Double)
        Dim iX As Integer
        Dim iY As Integer
        'Dim getC As Color

        Dim bmpImageData As New bmpData24(bmpImage)

        For iY = 1 To bmpImage.Height - 2
            For iX = 1 To bmpImage.Width - 2
                Dim iYY As Integer
                Dim iXX As Integer
                Dim iGray As Integer = 0
                '
                Dim iColor(2) As Integer

                Dim iData(2) As Integer
                For iColorLoop As Integer = 0 To 2 'Grayの場合はここをコメント
                    For iYY = 0 To 2
                        For iXX = 0 To 2
                            iData = bmpImageData.GetPixel((iX + iXX - 1), (iY + iYY - 1))
                            'iGray += FncAdjustBin(CInt(CDbl(iData(0)) * bFilter(iXX, iYY)))
                            iColor(iColorLoop) += AdjustBin(CInt(CDbl(iData(iColorLoop)) * bFilter(iXX, iYY)))
                        Next
                    Next
                Next 'Grayの場合はここをコメント

                'iData(0) = iGray
                'iData(1) = iGray
                'iData(2) = iGray

                iData(0) = iColor(0) 'Grayの場合はここをコメント
                iData(1) = iColor(1) 'Grayの場合はここをコメント
                iData(2) = iColor(2) 'Grayの場合はここをコメント

                bmpImageData.SetPixel(iX, iY, iData)

            Next

        Next

        bmpImageData.Dispose()

        'check
        'Me.Refresh()
        'DrawImage()
    End Sub

    Public Sub AreaPaint(ByRef bmImage As Bitmap, ByVal iX As Integer, ByVal iY As Integer, ByVal iDepth As Integer)
        Dim getC0 As Color

        If iDepth >= 1000 Then
            Exit Sub
        End If

        getC0 = bmImage.GetPixel(iX, iY)

        If getC0.R = 0 Then
            Dim iGray As Integer
            iGray = 255
            bmImage.SetPixel(iX, iY, Color.FromArgb(iGray, iGray, iGray))

            Dim getC As Color

            getC = bmImage.GetPixel(iX - 1, iY)
            If getC.R = 0 Then
                AreaPaint(bmImage, iX - 1, iY, iDepth + 1)
            End If
            getC = bmImage.GetPixel(iX + 1, iY)
            If getC.R = 0 Then
                AreaPaint(bmImage, iX + 1, iY, iDepth + 1)
            End If
            getC = bmImage.GetPixel(iX, iY - 1)
            If getC.R = 0 Then
                AreaPaint(bmImage, iX, iY - 1, iDepth + 1)
            End If
            getC = bmImage.GetPixel(iX, iY + 1)
            If getC.R = 0 Then
                AreaPaint(bmImage, iX, iY + 1, iDepth + 1)
            End If

        End If


    End Sub

    Public Function MySAD(ByVal bmpTarget As Bitmap, ByVal bmpTemplate As Bitmap, ByVal iXOffset As Integer, ByVal iYOffset As Integer, ByVal bFlag As Boolean) As Integer
        Dim iSum As Double = 0
        Dim iT As Integer
        Dim iI As Integer
        Dim iX As Integer
        Dim iY As Integer

        Dim bmpTargetData As New bmpData24(bmpTarget)
        Dim bmpTemplateData As New bmpData24(bmpTemplate)

        For iY = 0 To bmpTemplate.Height - 1
            For iX = 0 To bmpTemplate.Width - 1
                Dim iIgetC(2) As Integer
                Dim iTgetC(2) As Integer

                iIgetC = bmpTargetData.GetPixel(iX + iXOffset, iY + iYOffset)
                'iI = iIgetC(0) / 255
                '2011/9/21modified
                iI = iIgetC(0)
                iTgetC = bmpTemplateData.GetPixel(iX, iY)
                'iT = iTgetC(0) / 255
                '2011/9/21modified
                iT = iTgetC(0)
                If bFlag = True Then
                    iSum += Math.Abs(iI - iT)
                Else
                    If iY < bmpTemplate.Height / 2 Then
                        iSum += 0.5 * Math.Abs(iI - iT)
                    Else
                        iSum += 2 * Math.Abs(iI - iT)
                    End If
                End If
            Next
        Next


        bmpTargetData.Dispose()
        bmpTemplateData.Dispose()

        '2011/9/21add
        iSum /= 256

        Return CInt(iSum)

    End Function

    Public Sub GetPixel(ByVal stride As Integer, ByVal rgbValues() As Byte, ByVal iX As Integer, ByVal iY As Integer, ByRef iB As Integer, ByRef iG As Integer, ByRef iR As Integer)
        Dim iXX As Long
        Dim initX As Long

        initX = stride * iY
        iXX = initX + iX * 3
        iB = rgbValues(iXX + 0)
        iG = rgbValues(iXX + 1)
        iR = rgbValues(iXX + 2)
    End Sub

    Public Sub MyGrayScale(ByRef bmpImage As Bitmap)
        Dim iX As Integer
        Dim iY As Integer
        Dim iGray As Integer

        'Me.StatusStrip1.Items(0).Text = "グレイスケールで処理中..."
        'Application.DoEvents()

        If False Then
            Dim getC As Color

            For iY = 0 To bmpImage.Height - 1
                For iX = 0 To bmpImage.Width - 1
                    getC = bmpImage.GetPixel(iX, iY)
                    iGray = getC.R * 0.299 + getC.G * 0.587 + getC.B * 0.114
                    bmpImage.SetPixel(iX, iY, Color.FromArgb(iGray, iGray, iGray))

                Next
            Next
        Else
            Dim bmpGrayData As New bmpData24(bmpImage)
            For iY = 0 To bmpImage.Height - 1

                For iX = 0 To bmpImage.Width - 1
                    Dim iData(2) As Integer

                    iData = bmpGrayData.GetPixel(iX, iY)
                    'b, g, r
                    iGray = iData(2) * 0.299 + iData(1) * 0.587 + iData(0) * 0.114
                    'R * 0.299 + G * 0.587 + B * 0.114
                    iData(0) = iGray
                    iData(1) = iGray
                    iData(2) = iGray
                    bmpGrayData.SetPixel(iX, iY, iData)
                Next
            Next

            bmpGrayData.Dispose()
        End If

    End Sub

    Public Sub OpenFile2(ByRef bmpOriginalImage As Bitmap, filename As String)
        ClearData()
        iplImage = Cv.LoadImage(filename, LoadMode.AnyColor)
        '?
        bmpOriginalImage = New Bitmap(iplImage.Width, iplImage.Height, iplImage.WidthStep, System.Drawing.Imaging.PixelFormat.Format24bppRgb, CInt(iplImage.ImageData))

    End Sub

    Public Sub OpenFile(ByRef bmpOriginalImage As Bitmap)
        bBinFlag = False


        Dim ofd As New OpenFileDialog
        ofd.DefaultExt = "jpg"
        'ofd.Filter = "jpgファイル(*.jpg)|*.jpg|pgmファイル(*.pgm)|*.pgm"
        ofd.Filter = "jpgファイル(*.jpg)|*.jpg|pngファイル(*.png))|*.png|bmpファイル(*.bmp)|*.bmp|pgmファイル(*.pgm)|*.pgm"
        If ofd.ShowDialog() <> DialogResult.OK Then
            bmpOriginalImage = Nothing
            Exit Sub
        End If
        If ofd.FileName <> "" Then
            Dim sCheck As String = System.IO.Path.GetExtension(ofd.FileName).ToLower
            If sCheck = ".jpg" Or sCheck = ".png" Or sCheck = ".bmp" Then
                Try
                    'iplImage = Cv.LoadImage(ofd.FileName, LoadMode.AnyColor)
                    iplImage = Cv.LoadImage(ofd.FileName, LoadMode.AnyColor)
                    filename = ofd.FileName

                Catch e1 As FileNotFoundException
                End Try

                'bmpOriginalImage = New Bitmap(ofd.FileName)
                'bmpOriginalImage = New Bitmap(iplImage.Width, iplImage.Height, iplImage.WidthStep, System.Drawing.Imaging.PixelFormat.Format24bppRgb, CInt(iplImage.ImageData))
                bmpOriginalImage = IplImageToBitmap(iplImage)

            Else
                Dim fs As New System.IO.FileStream(ofd.FileName,
                    System.IO.FileMode.Open,
                    System.IO.FileAccess.Read)
                'ファイルを読み込むバイト型配列を作成する
                Dim bs(fs.Length - 1) As Byte
                Dim tmp(0) As Byte
                Dim iSize As Integer
                iSize = fs.Read(tmp, 0, 1)
                If tmp(0) <> Asc("P") Then
                    MsgBox("画像フォーマットが違います", MsgBoxStyle.Critical)
                End If
                iSize = fs.Read(tmp, 0, 1)
                If tmp(0) <> Asc("5") Then
                    MsgBox("そのフォーマットには対応していません。", MsgBoxStyle.Critical)
                End If
                '改行読み込み
                iSize = fs.Read(tmp, 0, 1)
                'コメント行を読む
                iSize = fs.Read(tmp, 0, 1)
                If tmp(0) = Asc("#") Then
                    Dim sComment As String = ""
                    While tmp(0) <> Asc(vbLf)
                        iSize = fs.Read(tmp, 0, 1)
                        sComment += Chr(tmp(0))
                    End While
                    iSize = fs.Read(tmp, 0, 1)
                End If

                Dim sWidth As String = ""
                While tmp(0) <> CInt(Asc(" "))
                    sWidth += Chr(tmp(0))
                    iSize = fs.Read(tmp, 0, 1)
                End While
                iSize = fs.Read(tmp, 0, 1)
                Dim sHeight As String = ""
                While tmp(0) <> Asc(vbLf)
                    sHeight += Chr(tmp(0))
                    iSize = fs.Read(tmp, 0, 1)
                End While

                iSize = fs.Read(tmp, 0, 1)

                Dim sBrightness As String = ""
                While tmp(0) <> Asc(vbLf)
                    sBrightness += Chr(tmp(0))
                    iSize = fs.Read(tmp, 0, 1)
                End While


                bmpOriginalImage = New Bitmap(CInt(sWidth), CInt(sHeight))
                Dim bmpGrayData As New bmpData24(bmpOriginalImage)
                Dim iX As Integer = 0
                Dim iY As Integer = 0
                For iY = 0 To bmpOriginalImage.Height - 1

                    For iX = 0 To bmpOriginalImage.Width - 1
                        Dim iData(2) As Integer
                        Dim iGray As Integer

                        iData = bmpGrayData.GetPixel(iX, iY)
                        iSize = fs.Read(tmp, 0, 1)
                        iGray = tmp(0)
                        iData(0) = iGray
                        iData(1) = iGray
                        iData(2) = iGray
                        bmpGrayData.SetPixel(iX, iY, iData)
                    Next
                Next

                fs.Close()
                bmpGrayData.Dispose()

            End If
        End If

    End Sub

    Public Sub MyFilterProcess(ByRef bmpImage As Bitmap)
        'フィルタ
        Dim bFilter(3, 3) As Double
        Dim k As Double = 0.5

        bFilter(0, 0) = -k / 9.0 : bFilter(1, 0) = -k / 9.0 : bFilter(2, 0) = -k / 9.0
        bFilter(0, 1) = -k / 9.0 : bFilter(1, 1) = 1.0 + 8.0 * k / 9.0 : bFilter(2, 1) = -k / 9.0
        bFilter(0, 2) = -k / 9.0 : bFilter(1, 2) = -k / 9.0 : bFilter(2, 2) = -k / 9.0

        MyFilter2(bmpImage, bFilter)

    End Sub

    '自分自身の実装
    Public Function IplImageToBitmap(ByRef iplImage As IplImage) As Bitmap
        Return iplImage.ToBitmap
    End Function

    '自分自身の実装
    Public Function BitmapToIplImage(ByRef bmpImage As Bitmap) As IplImage

        If bmpImage Is Nothing Then
            BitmapToIplImage = Nothing
            Exit Function
        End If

        Return BitmapConverter.ToIplImage(bmpImage)


        'Dim bmpData As BitmapData
        'Dim ptr As IntPtr
        'Dim bytes As Long
        'Dim stride As Integer
        'Dim rgbValues() As Byte
        'Dim iplNewImage As IplImage

        ''bitmapをiplimageに変換
        'iplNewImage = Cv.CreateImage(Cv.Size(bmpImage.Width, bmpImage.Height), BitDepth.U8, 3)
        'Dim rect As Rectangle = New Rectangle(0, 0, bmpImage.Width, bmpImage.Height)
        'bmpData = bmpImage.LockBits(rect, ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb)

        'ptr = bmpData.Scan0 'bmpの先頭アドレス
        'stride = bmpData.Stride 'スキャン幅

        'bytes = stride * bmpImage.Height
        'ReDim rgbValues(bytes - 1)

        ''Copy the RGB values into the array.
        'System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes - 1)
        'System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, iplNewImage.ImageData, bytes - 1)
        'bmpImage.UnlockBits(bmpData)

        'Return iplNewImage

    End Function

    Public Sub Binarization(ByRef bmpImage As Bitmap, ByVal iThreshold As Integer)
        Dim dst As New IplImage

        Dim gray_img As New IplImage
        Dim ocvtype As ThresholdType = ThresholdType.Binary
        Dim maxValue As Double = 255

        iplImage = BitmapToIplImage(bmpImage)
        gray_img = Cv.CreateImage(Cv.GetSize(iplImage), BitDepth.U8, 1)
        Cv.CvtColor(iplImage, gray_img, ColorConversion.BgrToGray)
        dst = Cv.CreateImage(Cv.GetSize(gray_img), BitDepth.U8, 1)
        Cv.Threshold(gray_img, dst, iThreshold, maxValue, ocvtype)
        Cv.CvtColor(dst, iplImage, ColorConversion.GrayToBgr)
        'bmpImage = New Bitmap(iplImage.Width, iplImage.Height, iplImage.WidthStep, System.Drawing.Imaging.PixelFormat.Format24bppRgb, CInt(iplImage.ImageData))
        bmpImage = IplImageToBitmap(iplImage)
        bBinFlag = True

    End Sub


    Public Sub AdaptiveBinarization(ByRef bmpImage As Bitmap)
        Dim dst As New IplImage
        Dim dst2 As New IplImage
        Dim gray_img As New IplImage
        Dim ocvtype As ThresholdType = ThresholdType.Binary Or ThresholdType.Otsu
        Dim minValue As Double = 0
        Dim maxValue As Double = 255
        Dim iplImage2 As New IplImage

        iplImage = BitmapToIplImage(bmpImage)
        gray_img = Cv.CreateImage(Cv.GetSize(iplImage), BitDepth.U8, 1)
        Cv.CvtColor(iplImage, gray_img, ColorConversion.BgrToGray)
        dst = Cv.CreateImage(Cv.GetSize(gray_img), BitDepth.U8, 1)

        'Cv.Threshold(gray_img, dst, minValue, maxValue, ocvtype)
        'Cv.AdaptiveThreshold(gray_img, dst, maxValue, AdaptiveThresholdType.MeanC, ThresholdType.Binary, 5, 0)
        Cv.AdaptiveThreshold(gray_img, dst, maxValue, AdaptiveThresholdType.MeanC, ThresholdType.Binary, 7, 8)
        'Cv.AdaptiveThreshold(gray_img, dst, maxValue, AdaptiveThresholdType.GaussianC, ThresholdType.Binary, 11, 8)

        'bmpLayerImage(2) = New Bitmap(dst.Width, dst.Height, dst.WidthStep, System.Drawing.Imaging.PixelFormat.Format24bppRgb, CInt(dst.ImageData))
        'bmpLayerImage(2) = New Bitmap(dst.Width, dst.Height, dst.WidthStep, System.Drawing.Imaging.PixelFormat.Format1bppIndexed, CInt(dst.ImageData))
        'bmpLayerImage(2) = New Bitmap(dst.Width, dst.Height, dst.WidthStep, System.Drawing.Imaging.PixelFormat.Format16bppGrayScale, CInt(dst.ImageData))
        'bmpLayerImage(2) = New Bitmap(dst.Width, dst.Height, dst.WidthStep, System.Drawing.Imaging.PixelFormat.Format8bppIndexed, CInt(dst.ImageData))

        'bmpLayerImage(2) = New Bitmap(dst.Width, dst.Height)
        Cv.CvtColor(dst, iplImage, ColorConversion.GrayToBgr)
        'bmpImage = New Bitmap(iplImage.Width, iplImage.Height, iplImage.WidthStep, System.Drawing.Imaging.PixelFormat.Format24bppRgb, CInt(iplImage.ImageData))
        bmpImage = IplImageToBitmap(iplImage)
        bBinFlag = True
        Cv.ReleaseImage(dst)
        Cv.ReleaseImage(dst2)
        Cv.ReleaseImage(gray_img)

    End Sub

    Public Sub GrayScale(ByRef bmpImage As Bitmap)
        If CheckCurrentImage(bmpImage) = False Then
            Exit Sub
        End If
        Dim dst As New IplImage

        iplImage = BitmapToIplImage(bmpImage)
        dst = Cv.CreateImage(Cv.GetSize(iplImage), BitDepth.U8, 1)
        Cv.CvtColor(iplImage, dst, ColorConversion.BgrToGray)
        Cv.CvtColor(dst, iplImage, ColorConversion.GrayToBgr)
        'bmpImage = New Bitmap(iplImage.Width, iplImage.Height, iplImage.WidthStep, System.Drawing.Imaging.PixelFormat.Format24bppRgb, CInt(iplImage.ImageData))
        bmpImage = IplImageToBitmap(iplImage)
        Cv.ReleaseImage(dst)

    End Sub

    Public Sub HistStretch(ByRef bmpImage As Bitmap)
        Dim gray As New IplImage
        Dim dst As New IplImage

        iplImage = BitmapToIplImage(bmpImage)
        dst = Cv.CreateImage(Cv.GetSize(iplImage), BitDepth.U8, 1)
        gray = Cv.CreateImage(Cv.GetSize(iplImage), BitDepth.U8, 1)
        Cv.CvtColor(iplImage, gray, ColorConversion.BgrToGray)
        Cv.EqualizeHist(gray, dst)

        Cv.CvtColor(dst, iplImage, ColorConversion.GrayToBgr)
        'bmpImage = New Bitmap(iplImage.Width, iplImage.Height, iplImage.WidthStep, System.Drawing.Imaging.PixelFormat.Format24bppRgb, CInt(iplImage.ImageData))
        bmpImage = IplImageToBitmap(iplImage)
    End Sub


    Public Sub Smooth(ByRef bmpImage As Bitmap, ByVal iType As Integer, value1 as Integer, value2 as Integer, value3 as Integer)

        iplImage = BitmapToIplImage(bmpImage)
        Dim dst As New IplImage
        dst = iplImage.Clone
        Try
            Select Case iType
                Case 0
                    Cv.Smooth(iplImage, iplImage, SmoothType.Blur, value1)
                Case 1
                    Cv.Smooth(iplImage, iplImage, SmoothType.Gaussian, value1)
                Case 2
                    Cv.Smooth(iplImage, iplImage, SmoothType.Median, value1)
                Case 3
                    '動作しない
                    'Cv.Smooth(iplImage, iplImage, SmoothType.Bilateral, value1, value1, value2, value3)
                    Cv.Smooth(iplImage, dst, SmoothType.Bilateral, value1, value1, value2, value3)
                    iplImage = dst.Clone
                Case Else
                    Cv.Smooth(iplImage, iplImage, SmoothType.Gaussian, value1)
            End Select
        Catch ex As Exception
            MsgBox("フィルターの実行でエラー：" + ex.Message)
            Exit Sub
        End Try
        bmpImage = IplImageToBitmap(iplImage)
    End Sub

    Public Sub Sobel(ByRef bmpImage As Bitmap)

        iplImage = BitmapToIplImage(bmpImage)

        Dim xSoble As IplImage = Cv.CreateImage(Cv.GetSize(iplImage), BitDepth.S16, iplImage.NChannels)
        Dim ySoble As IplImage = Cv.CreateImage(Cv.GetSize(iplImage), BitDepth.S16, iplImage.NChannels)
        Dim xySoble As IplImage = Cv.CreateImage(Cv.GetSize(iplImage), BitDepth.S16, iplImage.NChannels)

        Cv.Sobel(iplImage, xSoble, 1, 0)
        Cv.Sobel(iplImage, ySoble, 0, 1)
        Cv.Add(xSoble, ySoble, xySoble)

        Cv.ConvertScaleAbs(xySoble, iplImage)

        bmpImage = IplImageToBitmap(iplImage)
        Cv.ReleaseImage(xSoble)
        Cv.ReleaseImage(ySoble)
        Cv.ReleaseImage(xySoble)

    End Sub

    Public Sub Laplacian(ByRef bmpImage As Bitmap)
        Dim gray As New IplImage
        Dim dst As New IplImage
        Dim tmp_img As IplImage

        iplImage = BitmapToIplImage(bmpImage)
        tmp_img = Cv.CreateImage(Cv.GetSize(iplImage), BitDepth.S16, 1)
        dst = Cv.CreateImage(Cv.GetSize(iplImage), BitDepth.U8, 1)
        gray = Cv.CreateImage(Cv.GetSize(iplImage), BitDepth.U8, 1)

        Cv.CvtColor(iplImage, gray, ColorConversion.BgrToGray)
        Cv.Laplace(gray, tmp_img)
        Cv.ConvertScaleAbs(tmp_img, dst)
        Cv.CvtColor(dst, iplImage, ColorConversion.GrayToBgr)

        'bmpImage = New Bitmap(iplImage.Width, iplImage.Height, iplImage.WidthStep, System.Drawing.Imaging.PixelFormat.Format24bppRgb, CInt(iplImage.ImageData))
        bmpImage = IplImageToBitmap(iplImage)
        Cv.ReleaseImage(dst)
        Cv.ReleaseImage(gray)
        Cv.ReleaseImage(tmp_img)

    End Sub

    
    Public Sub Canny(ByRef bmpImage As Bitmap, value1 As Integer, value2 As Integer, value3 As Integer)

        Dim gray As New IplImage
        Dim dst As New IplImage

        iplImage = BitmapToIplImage(bmpImage)
        dst = Cv.CreateImage(Cv.GetSize(iplImage), BitDepth.U8, 1)
        gray = Cv.CreateImage(Cv.GetSize(iplImage), BitDepth.U8, 1)

        Cv.CvtColor(iplImage, gray, ColorConversion.BgrToGray)
        Try
            Cv.Canny(gray, dst, value1, value2, value3)
        Catch ex As Exception
            MsgBox("Cannyでエラー：" + ex.Message)
            Exit Sub
        End Try
        Cv.CvtColor(dst, iplImage, ColorConversion.GrayToBgr)

        bmpImage = IplImageToBitmap(iplImage)
        Cv.ReleaseImage(dst)
        Cv.ReleaseImage(gray)

    End Sub

    Public Sub Erode(ByRef bmpImage As Bitmap, value1 As Integer)

        Dim gray As New IplImage
        Dim dst As New IplImage

        iplImage = BitmapToIplImage(bmpImage)
        dst = Cv.CreateImage(Cv.GetSize(iplImage), BitDepth.U8, 1)
        gray = Cv.CreateImage(Cv.GetSize(iplImage), BitDepth.U8, 1)

        Cv.CvtColor(iplImage, gray, ColorConversion.BgrToGray)
        Cv.Erode(gray, dst, Nothing, value1)
        Cv.CvtColor(dst, iplImage, ColorConversion.GrayToBgr)

        bmpImage = IplImageToBitmap(iplImage)
        Cv.ReleaseImage(dst)
        Cv.ReleaseImage(gray)

    End Sub

    Public Sub Dilate(ByRef bmpImage As Bitmap, value1 As Integer)

        Dim gray As New IplImage
        Dim dst As New IplImage

        iplImage = BitmapToIplImage(bmpImage)
        dst = Cv.CreateImage(Cv.GetSize(iplImage), BitDepth.U8, 1)
        gray = Cv.CreateImage(Cv.GetSize(iplImage), BitDepth.U8, 1)

        Cv.CvtColor(iplImage, gray, ColorConversion.BgrToGray)
        Cv.Dilate(gray, dst, Nothing, value1)
        Cv.CvtColor(dst, iplImage, ColorConversion.GrayToBgr)

        bmpImage = IplImageToBitmap(iplImage)
        Cv.ReleaseImage(dst)
        Cv.ReleaseImage(gray)

    End Sub


    Public Sub UnSharpMasking(ByRef bmpImage As Bitmap, ByRef value1 As Double)
        Dim k As Single = value1
        Dim KernelData() As Single = {
        -k / 9.0, -k / 9.0, -k / 9.0,
        -k / 9.0, 1 + (8 * k) / 9.0, -k / 9.0,
        -k / 9.0, -k / 9.0, -k / 9.0}

        iplImage = BitmapToIplImage(bmpImage)
        Dim kernel As CvMat = Cv.Mat(3, 3, MatrixType.F32C1, KernelData)
        Cv.Filter2D(iplImage, iplImage, kernel)

        bmpImage = IplImageToBitmap(iplImage)
    End Sub


    Public Sub NotImage(ByRef bmpImage As Bitmap)
        If CheckCurrentImage(bmpImage) = False Then
            Exit Sub
        End If

        iplImage = BitmapToIplImage(bmpImage)

        Cv.Not(iplImage, iplImage)
        'bmpImage = New Bitmap(iplImage.Width, iplImage.Height, iplImage.WidthStep, System.Drawing.Imaging.PixelFormat.Format24bppRgb, CInt(iplImage.ImageData))
        bmpImage = IplImageToBitmap(iplImage)
    End Sub

    'Public Sub ApproxPolyContours(ByRef bmpImage As Bitmap, value1 As Integer)
    Public Sub ApproxPolyContours(ByRef bmpImage As Bitmap)

        Dim gray As New IplImage
        Dim dst As New IplImage

        iplImage = BitmapToIplImage(bmpImage)
        gray = Cv.CreateImage(Cv.GetSize(iplImage), BitDepth.U8, 1)

        Cv.CvtColor(iplImage, gray, ColorConversion.BgrToGray)

        Dim storage As New CvMemStorage
        Dim contour As CvSeq(Of CvPoint) = Nothing
        Dim iFind_num As Integer
        iFind_num = Cv.FindContours(gray, storage, contour, CvContour.SizeOf, ContourRetrieval.List, ContourChain.ApproxSimple)

        Dim ContoursColor As CvScalar
        ContoursColor = Cv.RGB(255, 0, 0)
        'Cv.DrawContours(iplImage, contour, -1, ContoursColor, value1)
        Cv.DrawContours(iplImage, contour, -1, ContoursColor, 0)
        'bmpImage = New Bitmap(iplImage.Width, iplImage.Height, iplImage.WidthStep, System.Drawing.Imaging.PixelFormat.Format24bppRgb, CInt(iplImage.ImageData))
        bmpImage = IplImageToBitmap(iplImage)
        Cv.ReleaseImage(dst)
        Cv.ReleaseImage(gray)

    End Sub

    'なぜか動作しなかった。
    'http://www.thedesignium.com/development/8237
    'frameをtrimmingする
    Public Function Trimming(src As IplImage, x As Integer, y As Integer, width As Integer, height As Integer) As IplImage
        Dim dest As IplImage = New IplImage(width, height, src.Depth, src.NChannels)
        Cv.SetImageROI(src, New CvRect(x, y, width, height))
        dest = Cv.CloneImage(src)
        Cv.ResetImageROI(src)
        Return dest
    End Function

    'http://milk-tea.myvnc.com/blog/adiary.cgi/0132
    'CvMatからIplImageへ変換する
    Public Function ConvertFromCvMat(cvmat As CvMat) As IplImage
        Return Cv.GetImage(cvmat)
    End Function

    'IplImageからCvMatに変換する
    Public Function ConvertFromIplImage(image As IplImage) As Mat
        'Dim ret As CvMat = Cv.GetImage(image) 動作しない、cvMatは使用しない
        Dim ret As Mat = New Mat(image)

        Return ret

    End Function

    Public Function Clipping2(ByRef bmpImage As Bitmap, value1 As Integer, value2 As Integer, value3 As Integer, value4 As Integer) As String

        iplImage = BitmapToIplImage(bmpImage)

        'http://daily-tech.hatenablog.com/entry/2016/05/25/040236

        iplImage.SetROI(New CvRect(value1, value2, value3, value4))
        Dim selectImage As New IplImage(value3, value4, BitDepth.U8, 3)
        '順番に注意
        Cv.Copy(iplImage, selectImage)
        iplImage.ResetROI()

        'これがないと動かないようです。
        iplImage = selectImage.Clone

        Cv.ReleaseImage(selectImage)

        'bmpImage = New Bitmap(iplImage.Width, iplImage.Height, iplImage.WidthStep, System.Drawing.Imaging.PixelFormat.Format24bppRgb, CInt(iplImage.ImageData))
        bmpImage = IplImageToBitmap(iplImage)
        Return "(" + value1.ToString + "," + value2.ToString + "," + value3.ToString + "," + value4.ToString + ")"
    End Function


    Public Function Clipping(ByRef bmpImage As Bitmap, ByRef value1 As Double, ByRef value2 As Double, ByRef value3 As Double, ByRef value4 As Double) As String

        'Dim rect As New CvRect(value1, value2, value3, value4)


        iplImage = BitmapToIplImage(bmpImage)

        'http://daily-tech.hatenablog.com/entry/2016/05/25/040236

        iplImage.SetROI(New CvRect(value1, value2, value3 - value1, value4 - value2))
        Dim selectImage As New IplImage(value3 - value1, value4 - value2, BitDepth.U8, 3)
        '順番に注意
        Cv.Copy(iplImage, selectImage)
        iplImage.ResetROI()

        'これがないと動かないようです。
        iplImage = selectImage.Clone

        Cv.ReleaseImage(selectImage)

        'bmpImage = New Bitmap(iplImage.Width, iplImage.Height, iplImage.WidthStep, System.Drawing.Imaging.PixelFormat.Format24bppRgb, CInt(iplImage.ImageData))
        bmpImage = IplImageToBitmap(iplImage)
        Return "(" + value1.ToString + "," + value2.ToString + "," + value3.ToString + "," + value4.ToString + ")"
    End Function

    Public Function GetEval(ByVal iHistogram As Integer()) As Double

        Dim iLoop As Integer
        Dim iCount As Integer

        Dim dMean1 As Double = 0
        Dim dMean2 As Double = 0
        Dim dOmega1 As Integer = 0
        Dim dOmega2 As Integer = 0
        Dim dResult As Double = 0

        iCount = iHistogram.Length

        '閾値を動的に決める。
        Dim iMax As Integer = 0

        For i As Integer = 0 To iCount - 1

            If iMax < iHistogram(i) Then
                iMax = iHistogram(i)
            End If

        Next

        Dim iMin As Integer = iMax

        For i As Integer = 0 To iCount - 1
            If iMin > iHistogram(i) Then
                iMin = iHistogram(i)
            End If
        Next


        Dim iYThresholdOffset As Integer = 0

        Dim iThreshold As Integer = CDbl(iMax - iMin) / 2


        For iLoop = 0 To iHistogram.Length - 1
            If iHistogram(iLoop) < iThreshold Then
                dMean1 += iHistogram(iLoop)
                dOmega1 += 1
            Else
                dMean2 += iHistogram(iLoop)
                dOmega2 += 1
            End If
        Next

        dMean1 /= dOmega1
        dMean2 /= dOmega2
        'dResult = dOmega1 * dOmega2 * (dMean1 - dMean2) ^ 2
        '単純に黒画像の平均値とする
        dResult = dMean2

        Return dResult

    End Function


    'ヒストグラムX
    Public Sub SetXHistogram(ByVal bmpImage As Bitmap, ByRef iXHistogram() As Integer, ByVal iStartPos As Integer, ByVal iEndPos As Integer)
        ReDim iXHistogram(bmpImage.Width - 1)
        Dim igetC(2) As Integer

        Dim bmpImageData As New bmpData24(bmpImage)
        Dim iX As Integer
        Dim iY As Integer

        'For iX = 0 To bmpImage.Width - 1
        For iX = iStartPos To iEndPos - 1
            For iY = 0 To bmpImage.Height - 1
                igetC = bmpImageData.GetPixel(iX, iY)
                iXHistogram(iX) += (1 - igetC(0) / 255)
            Next
        Next

        bmpImageData.Dispose()

    End Sub

    'ヒストグラムY
    Public Sub SetYHistogram(ByVal bmpImage As Bitmap, ByRef iYHistogram() As Integer, ByVal iYStartPos As Integer, ByVal iYEndPos As Integer)
        ReDim iYHistogram(bmpImage.Height - 1)
        Dim igetC(2) As Integer

        Dim bmpImageData As New bmpData24(bmpImage)
        Dim iX As Integer
        Dim iY As Integer

        For iY = iYStartPos To iYEndPos
            For iX = 0 To bmpImage.Width - 1
                igetC = bmpImageData.GetPixel(iX, iY)
                iYHistogram(iY) += (1 - igetC(0) / 255)
            Next
        Next

        bmpImageData.Dispose()

    End Sub

    '文字行のヒストグラムY
    Public Sub SetStringYHistogram(ByVal bmpImage As Bitmap, ByRef iYHistogram() As Integer, ByVal iXStartPos As Integer, ByVal iXEndPos As Integer)
        Dim getC As Color
        Dim iX As Integer
        Dim iY As Integer

        For iY = iStringYStartPos To iStringYEndPos
            For iX = iXStartPos To iXEndPos
                getC = bmpImage.GetPixel(iX, iY)
                iYHistogram(iY - iStringYStartPos) += (1 - getC.R / 255)
            Next
        Next
    End Sub

    Public Sub JointArea(ByVal alLocalPatternXStartPos As ArrayList, ByVal alLocalPatternXEndPos As ArrayList)
        Dim iDeleteLoop As Integer = 0

        '領域幅の平均を求める
        Dim dWidth As Double = 0
        Dim iCount As Integer = alLocalPatternXStartPos.Count

        iCount = alLocalPatternXStartPos.Count

        For iLoop As Integer = 0 To iCount - 1
            dWidth += Math.Abs(alLocalPatternXEndPos(iLoop) - alLocalPatternXStartPos(iLoop) + 1)
        Next
        dWidth = dWidth / iCount

        For iLoop As Integer = 0 To iCount - 2
            If iLoop >= (iCount - 1) Then
                Exit For
            End If
            Dim dTempWidth1 As Integer
            Dim dTempWidth2 As Integer
            Try
                dTempWidth1 = Math.Abs(alLocalPatternXEndPos(iDeleteLoop) - alLocalPatternXStartPos(iDeleteLoop) + 1)
                dTempWidth2 = Math.Abs(alLocalPatternXEndPos(iDeleteLoop + 1) - alLocalPatternXStartPos(iDeleteLoop) + 1)
            Catch ex As Exception
            End Try
            '領域の結合
            If dTempWidth2 < dWidth * 2 Then
                alLocalPatternXStartPos(iDeleteLoop + 1) = alLocalPatternXStartPos(iDeleteLoop)
                alLocalPatternXStartPos.RemoveAt(iDeleteLoop)
                alLocalPatternXEndPos.RemoveAt(iDeleteLoop)
                iCount += -1
                iDeleteLoop += 1
            End If
        Next

    End Sub

    Public Sub HorizontalExtractStringArea(ByRef bmpImage As Bitmap, ByVal iStartX As Integer, ByVal iStartY As Integer, ByVal iRowNo As Integer)
        Dim iOffset As Integer = 0

        Dim i As Integer
        Dim iXThreshold As Integer = 3
        Dim iYThreshold As Integer = 3

        Dim iLocalXStartPos As Integer = iOffset
        Dim iLocalXEndPos As Integer = bmpImage.Width - 1 - iOffset
        Dim iLocalYStartPos As Integer = iOffset
        Dim iLocalYEndPos As Integer = bmpImage.Height - 1 - iOffset

        Dim alLocalPatternXStartPos As New ArrayList
        Dim alLocalPatternXEndPos As New ArrayList
        Dim alLocalPatternYStartPos As New ArrayList
        Dim alLocalPatternYEndPos As New ArrayList

        Dim iXLocalHistogram(bmpImage.Width) As Integer
        Dim iYLocalHistogram(bmpImage.Height) As Integer

        SetXHistogram(bmpImage, iXLocalHistogram, 0, bmpImage.Width - 1)
        SetYHistogram(bmpImage, iYLocalHistogram, 0, bmpImage.Height - 1)

        Dim iXPatternStartPos As Integer = 0
        Dim iXPatternEndPos As Integer = 0
        Dim iYPatternStartPos As Integer = 0
        Dim iYPatternEndPos As Integer = 0

        For i = iLocalXStartPos To iLocalXEndPos
            If iXLocalHistogram(i) > iXThreshold And iXPatternStartPos = 0 Then
                iXPatternStartPos = i
            End If

            If iXLocalHistogram(i) <= iXThreshold And iXPatternStartPos <> 0 And iXPatternEndPos = 0 Or i = iLocalXEndPos Then
                '開始位置が見つかった場合
                If iXPatternStartPos <> 0 Then
                    iXPatternEndPos = i
                    alLocalPatternXStartPos.Add(iXPatternStartPos - iOffset)
                    alLocalPatternXEndPos.Add(iXPatternEndPos + iOffset)
                    iXPatternStartPos = 0
                    iXPatternEndPos = 0
                End If
            End If

        Next

        JointArea(alLocalPatternXStartPos, alLocalPatternXEndPos)
        JointArea(alLocalPatternXStartPos, alLocalPatternXEndPos)
        JointArea(alLocalPatternXStartPos, alLocalPatternXEndPos)
        JointArea(alLocalPatternXStartPos, alLocalPatternXEndPos)

        For i = iLocalYStartPos To iLocalYEndPos
            If iYLocalHistogram(i) > iYThreshold And iYPatternStartPos = 0 Then
                iYPatternStartPos = i
            End If

            If iYLocalHistogram(i) <= iYThreshold And iYPatternStartPos <> 0 And iYPatternEndPos = 0 Or i = iLocalYEndPos Then
                '開始位置が見つかった場合
                If iYPatternStartPos <> 0 Then
                    iYPatternEndPos = i
                    alLocalPatternYStartPos.Add(iYPatternStartPos - iOffset)
                    alLocalPatternYEndPos.Add(iYPatternEndPos + iOffset)
                    iYPatternStartPos = 0
                    iYPatternEndPos = 0
                End If
            End If

        Next


        For iLoop As Integer = 0 To alLocalPatternXStartPos.Count - 1
            Dim iScale As Integer = 1
            Dim iX1 As Integer = (iStartX + alLocalPatternXStartPos(iLoop)) * iScale
            Dim iY1 As Integer = (iStartY + alLocalPatternYStartPos(0)) * iScale
            Dim iX2 As Integer = (iStartX + alLocalPatternXEndPos(iLoop)) * iScale
            Dim iY2 As Integer = (iStartY + alLocalPatternYEndPos(alLocalPatternYEndPos.Count - 1)) * iScale

            stStr(iStrNo).X1 = iX1
            stStr(iStrNo).Y1 = iY1
            stStr(iStrNo).X2 = iX2
            stStr(iStrNo).Y2 = iY2
            stStr(iStrNo).RowNo = iRowNo

            iStrNo += 1

            ReDim Preserve stStr(iStrNo)

            iFileNo += 1
        Next

    End Sub

    Public Sub ClearArea(ByRef bmpImage As Bitmap, ByVal iStartX As Integer, ByVal iStartY As Integer, ByVal iEndX As Integer, ByVal iEndY As Integer)
        Dim iX As Integer
        Dim iY As Integer
        Dim iGray As Integer

        Dim bmpImageData As New bmpData24(bmpImage)

        Dim igetC(2) As Integer
        iGray = 255

        igetC(0) = iGray
        igetC(1) = iGray
        igetC(2) = iGray
        For iY = iStartY To iEndY
            For iX = iStartX To iEndX
                bmpImageData.SetPixel(iX, iY, igetC)

            Next
        Next
        bmpImageData.Dispose()

    End Sub

    '2018/3/10
    Public Sub AutoAdjustAngle(ByRef bmpImage As Bitmap)
        If CheckCurrentImage(bmpImage) = False Then
            Exit Sub
        End If

        Dim gray As New IplImage
        Dim dst As New IplImage

        iplImage = BitmapToIplImage(bmpImage)
        gray = Cv.CreateImage(Cv.GetSize(iplImage), BitDepth.U8, 1)

        Cv.CvtColor(iplImage, gray, ColorConversion.BgrToGray)
        Cv.Smooth(gray, gray, SmoothType.Gaussian, 5)
        Cv.Threshold(gray, gray, 0, 255, ThresholdType.Binary Or ThresholdType.Otsu)
        Cv.Canny(gray, gray, 50, 200)

        Dim lines As CvSeq
        Dim storage As New CvMemStorage
        'lines = Cv.HoughLines2(gray, storage, HoughLinesMethod.Standard, 1, Math.PI / 180, 100)
        lines = Cv.HoughLines2(gray, storage, HoughLinesMethod.Standard, 1, Math.PI / 180, 300)
        Dim dMean As Double = 0
        Dim dAngle As Double = 0
        Dim iCount As Integer

        If lines.Total = 0 Then
            MsgBox("線抽出できませんでした。")
            Cv.ReleaseImage(dst)
            Cv.ReleaseImage(gray)
            Exit Sub
        End If

        For i As Integer = 0 To lines.Total - 1
            Dim elem As CvLineSegmentPolar = lines.GetSeqElem(Of CvLineSegmentPolar)(i).Value
            Dim rho As Double = elem.Rho
            Dim theta As Double = elem.Theta
            Dim a As Double = Math.Cos(theta)
            Dim b As Double = Math.Sin(theta)
            Dim x0 As Double = a * rho
            Dim y0 As Double = b * rho
            Dim pt1 As CvPoint = New CvPoint(Cv.Round(x0 + 1000 * (-b)), Cv.Round(y0 + 1000 * (a)))
            Dim pt2 As CvPoint = New CvPoint(Cv.Round(x0 - 1000 * (-b)), Cv.Round(y0 - 1000 * (a)))

            'http://cave.under.jp/_contents/bookscan.html
            'プログラムのわかりやすさを追求するなら度にする？
            'まさかの縦書き対応
            If theta > 1 Then
                '2018/3/10 異常値を除く
                '30度以内なら処理をやる。
                'マジックナンバー？
                'If theta > 0.523599 Then
                iplImage.Line(pt1, pt2, CvColor.Red, 3, LineType.AntiAlias, 0)
                dMean += theta
                iCount += 1
                'End If

            End If
        Next

        If iCount = 0 Then
            MsgBox("線抽出できませんでした。")
            Cv.ReleaseImage(dst)
            Cv.ReleaseImage(gray)
            Exit Sub
        End If


        dMean /= iCount
        dAngle = dMean * 180 / Math.PI - 90

        Cv.ReleaseImage(dst)
        Cv.ReleaseImage(gray)
        'このやり方は古い?
        'bmpImage = New Bitmap(iplImage.Width, iplImage.Height, iplImage.WidthStep, System.Drawing.Imaging.PixelFormat.Format24bppRgb, CInt(iplImage.ImageData))
        bmpImage = IplImageToBitmap(iplImage)


        'ここの処理も疑問cvの関数を使用していない。
        Dim canvas As New Bitmap(bmpImage.Width, bmpImage.Height)
        Dim g As Graphics = Graphics.FromImage(canvas)
        g.ResetTransform()
        'g.RotateTransform(-dAngle)
        g.RotateTransform(-dAngle)
        g.DrawImage(bmpImage, New Rectangle(0, 0, bmpImage.Width, bmpImage.Height))
        bmpImage = canvas

    End Sub


    Public Sub SaveJpg(ByVal bmpImage As Bitmap, ByVal sFileName As String, ByVal iXStartPos As Integer, ByVal iYStartPos As Integer, ByVal iXEndPos As Integer, ByVal iYEndPos As Integer)
        Dim iY As Integer
        Dim iX As Integer
        Dim bmpExtractImage As Bitmap
        bmpExtractImage = New Bitmap(iXEndPos - iXStartPos + 1, iYEndPos - iYStartPos + 1)

        '能率が悪い(setROIができないときのやり方)
        For iY = iYStartPos To iYEndPos
            For iX = iXStartPos To iXEndPos
                Dim getC As Color
                getC = bmpImage.GetPixel(iX, iY)
                bmpExtractImage.SetPixel(iX - iXStartPos, iY - iYStartPos, Color.FromArgb(getC.R, getC.G, getC.B))
            Next
        Next
        bmpExtractImage.Save(sFileName, System.Drawing.Imaging.ImageFormat.Jpeg)

    End Sub

    Public Sub SavePgm(ByVal bmpImage As Bitmap, ByVal sFileName As String, ByVal iXStartPos As Integer, ByVal iYStartPos As Integer, ByVal iXEndPos As Integer, ByVal iYEndPos As Integer)
        Dim iY As Integer
        Dim iX As Integer
        Dim iWidth As Integer = iXEndPos - iXStartPos + 1
        Dim iHeight As Integer = iYEndPos - iYStartPos + 1

        Dim iSize As Integer = iWidth * iHeight
        Dim data(iSize) As Byte

        For iY = iYStartPos To iYEndPos
            For iX = iXStartPos To iXEndPos
                Dim getC As Color
                getC = bmpImage.GetPixel(iX, iY)
                data((iX - iXStartPos) + (iY - iYStartPos) * iWidth) = getC.G
            Next
        Next

        Dim fs As System.IO.FileStream
        fs = New System.IO.FileStream(sFileName, System.IO.FileMode.Append)
        Dim sWriteString = "P5" + vbLf + "# CREATOR TS-Image" + vbLf + iWidth.ToString + " " + iHeight.ToString + vbLf + "255" + vbLf

        Dim strbyte() As Byte = System.Text.Encoding.GetEncoding(932).GetBytes(sWriteString)
        fs.Write(strbyte, 0, strbyte.Length)
        fs.Write(data, 0, data.Length)
        fs.Close()

    End Sub

    Public Class bmpData24
        Implements IDisposable

        Private bmpData As BitmapData
        Private ptr As IntPtr
        Private bytes As Long
        Private stride As Integer
        Private rgbValues() As Byte
        Private iMyLayerIndex As Integer
        Private bmpImage As Bitmap


        Public Sub New(ByRef bmpInputImage As Bitmap)
            bmpImage = bmpInputImage

            '高速化
            Dim rect As Rectangle = New Rectangle(0, 0, bmpImage.Width, bmpImage.Height)
            bmpData = bmpImage.LockBits(rect, ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb)

            ptr = bmpData.Scan0 'bmpの先頭アドレス
            stride = bmpData.Stride 'スキャン幅

            bytes = stride * bmpImage.Height
            ReDim rgbValues(bytes - 1)

            'Copy the RGB values into the array.
            System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes)

        End Sub

        Public Function GetPixel(ByVal iX As Integer, ByVal iY As Integer) As Integer()
            Dim iXX As Integer
            Dim initX As Integer
            Dim iRet(2) As Integer

            initX = stride * iY
            iXX = initX + iX * 3
            'b, g, r
            iRet(0) = rgbValues(iXX + 0)
            iRet(1) = rgbValues(iXX + 1)
            iRet(2) = rgbValues(iXX + 2)
            Return iRet
        End Function

        Public Sub SetPixel(ByVal iX As Integer, ByVal iY As Integer, ByVal iData() As Integer)
            Dim iXX As Long
            Dim initX As Long

            initX = stride * iY
            iXX = initX + iX * 3
            rgbValues(iXX + 0) = iData(0)
            rgbValues(iXX + 1) = iData(1)
            rgbValues(iXX + 2) = iData(2)
        End Sub


        Public Sub Dispose() Implements IDisposable.Dispose
            System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, bytes)
            bmpImage.UnlockBits(bmpData)
        End Sub

    End Class

    Public Sub AdjustCopyImageArea(ByRef bmpSoureImage As Bitmap, ByRef bmpDestinationImage As Bitmap)

        Dim iWidth As Integer = bmpSoureImage.Width
        Dim iHeight As Integer = bmpSoureImage.Height
        Dim canvas As Bitmap

        If iHeight > iWidth Then
            canvas = New Bitmap(iHeight, iHeight)
        Else
            canvas = New Bitmap(iWidth, iWidth)
        End If

        Dim g As Graphics = Graphics.FromImage(canvas)

        g.FillRectangle(Brushes.White, g.VisibleClipBounds)

        '切り取る部分の範囲を決定する。ここでは、
        Dim srcRect As Rectangle
        srcRect = New Rectangle(0, 0, iWidth, iHeight)

        '描画する部分の範囲を決定する。ここでは、(0, 0)の位置に描画する
        Dim desRect As Rectangle
        If iHeight > iWidth Then
            Dim iAdjust As Integer = (iHeight - iWidth) / 2
            desRect = New Rectangle(iAdjust, 0, iWidth, iHeight)
        Else
            Dim iAdjust As Integer = (iHeight - iWidth) / 2
            desRect = New Rectangle(0, iAdjust, iWidth, iHeight)
        End If
        '画像の一部を描画する
        g.DrawImage(bmpSoureImage, desRect, srcRect, GraphicsUnit.Pixel)
        bmpDestinationImage = canvas

    End Sub

    Public Sub CopyImageArea(ByRef bmpSoureImage As Bitmap, ByRef bmpDestinationImage As Bitmap, ByVal iStartX As Integer, ByVal iStartY As Integer, ByVal iEndX As Integer, ByVal iEndY As Integer)

        Dim iWidth As Integer = iEndX - iStartX + 1
        Dim iHeight As Integer = iEndY - iStartY + 1
        Dim canvas As New Bitmap(iWidth, iHeight)
        Dim g As Graphics = Graphics.FromImage(canvas)

        '切り取る部分の範囲を決定する。ここでは、
        Dim srcRect As New Rectangle(iStartX, iStartY, iWidth, iHeight)
        '描画する部分の範囲を決定する。ここでは、(0, 0)の位置に描画する
        Dim desRect As New Rectangle(0, 0, iWidth, iHeight)
        '画像の一部を描画する
        g.DrawImage(bmpSoureImage, desRect, srcRect, GraphicsUnit.Pixel)
        bmpDestinationImage = canvas

    End Sub

    'なぜクローンしないか？
    Public Sub CopyImage(ByRef bmpSoureImage As Bitmap, ByRef bmpDestinationImage As Bitmap)
        If bmpSoureImage Is Nothing Then
            Exit Sub
        End If

        'Dim iWidth As Integer = bmpSoureImage.Width
        'Dim iHeight As Integer = bmpSoureImage.Height
        'Dim canvas As New Bitmap(iWidth, iHeight)
        'Dim g As Graphics = Graphics.FromImage(canvas)

        'g.DrawImage(bmpSoureImage, New Rectangle(0, 0, iWidth, iHeight))
        'bmpDestinationImage = canvas

        bmpDestinationImage = bmpSoureImage.Clone

    End Sub

    Public Sub ReSizeImage(ByRef bmpSoureImage As Bitmap, ByRef bmpDestinationImage As Bitmap, startx As Integer, starty As Integer, orgx As Integer, orgy As Integer)
        If bmpSoureImage Is Nothing Then
            Exit Sub
        End If

        Dim iWidth As Integer = bmpSoureImage.Width
        Dim iHeight As Integer = bmpSoureImage.Height

        bmpDestinationImage = New Bitmap(orgx, orgy, System.Drawing.Imaging.PixelFormat.Format24bppRgb)

        Dim canvas As New Bitmap(bmpDestinationImage.Width, bmpDestinationImage.Height)

        Dim g As Graphics = Graphics.FromImage(canvas)

        g.DrawImage(bmpSoureImage, New Rectangle(startx, starty, iWidth, iHeight))
        bmpDestinationImage = canvas

    End Sub

    Public Sub ScaleCopyImage(ByRef bmpSoureImage As Bitmap, ByRef bmpDestinationImage As Bitmap, ByVal dScale As Double)

        Dim iWidth As Integer = bmpSoureImage.Width * dScale
        Dim iHeight As Integer = bmpSoureImage.Height * dScale
        Dim canvas As New Bitmap(iWidth, iHeight)
        Dim g As Graphics = Graphics.FromImage(canvas)

        g.ResetTransform()
        g.ScaleTransform(dScale, dScale)
        g.DrawImage(bmpSoureImage, New Rectangle(0, 0, bmpSoureImage.Width, bmpSoureImage.Height))
        bmpDestinationImage = canvas
        g.Dispose()

    End Sub


    Public Sub ClearAreaPosition()
        '領域の位置情報初期化
        iXStartPos = 0
        iYStartPos = 0
        '領域の初期値を設定
        iXEndPos = bmpCurrentImage.Width - 1
        iYEndPos = bmpCurrentImage.Height - 1

        iStringXStartPos = 0
        iStringYStartPos = 0
        iStringXEndPos = iXEndPos
        iStringYEndPos = iYEndPos

    End Sub

    Public Sub TemplateMating(ByRef bmpImage As Bitmap)
        Dim dst As New IplImage
        Dim tmp As New IplImage
        Dim minloc As New CvPoint
        Dim maxloc As New CvPoint
        Dim minval As Double
        Dim maxval As Double

        Dim ofd As New OpenFileDialog
        ofd.DefaultExt = "jpg"
        'ofd.Filter = "jpgファイル(*.jpg)|*.jpg|pgmファイル(*.pgm)|*.pgm"
        ofd.Filter = "jpgファイル(*.jpg)|*.jpg"
        If ofd.ShowDialog() <> DialogResult.OK Then
            tmp = Nothing
            Exit Sub
        End If
        If ofd.FileName <> "" Then
            If System.IO.Path.GetExtension(ofd.FileName).ToLower = ".jpg" Then
                Try
                    tmp = Cv.LoadImage(ofd.FileName)

                Catch e1 As FileNotFoundException
                End Try
            End If
        End If

        iplImage = BitmapToIplImage(bmpImage)

        Dim dstsize As CvSize = Cv.Size(iplImage.Width - tmp.Width + 1, iplImage.Height - tmp.Height + 1)

        dst = Cv.CreateImage(dstsize, BitDepth.F32, 1)

        Cv.MatchTemplate(iplImage, tmp, dst, MatchTemplateMethod.CCoeffNormed)
        Cv.MinMaxLoc(dst, minval, maxval, minloc, maxloc, Nothing)

        iplImage.Rectangle(maxloc, Cv.Point(maxloc.X + tmp.Width, maxloc.Y + tmp.Height), CvColor.Red, 3)

        bmpImage = IplImageToBitmap(iplImage)
        Cv.ReleaseImage(dst)

    End Sub

    Public Sub Contrast(ByRef bmpImage As Bitmap, threshold As Double)
        Dim iX As Integer
        Dim iY As Integer

        Dim bmpGrayData As New bmpData24(bmpImage)

        'コントラスト
        For iY = 0 To bmpImage.Height - 1
            For iX = 0 To bmpImage.Width - 1
                Dim iData(2) As Integer
                Dim iCont(2) As Integer
                For iColorLoop As Integer = 0 To 2
                    iData = bmpGrayData.GetPixel(iX, iY)
                    Dim v As Double
                    v = 0.1
                    iCont(iColorLoop) = (iData(iColorLoop) - CInt(threshold)) * (1 + v) * (1 + v) + CInt(threshold)
                    If iCont(iColorLoop) < 0 Then
                        iCont(iColorLoop) = 0
                    End If
                    If iCont(iColorLoop) > 255 Then
                        iCont(iColorLoop) = 255
                    End If
                Next
                'debug
                'iCont(0) = 255
                'iCont(1) = 255
                'iCont(2) = 255
                bmpGrayData.SetPixel(iX, iY, iCont)

            Next
        Next


        bmpGrayData.Dispose()
    End Sub

    Public Function CheckCurrentImage(bmpImage As Image)
        If bmpImage Is Nothing Then
            MsgBox("画像ファイルがロードされていません。")
            CheckCurrentImage = False
            Exit Function
        End If
        CheckCurrentImage = True
        Exit Function
    End Function

    Public Sub USBCameraProc(ByRef picImage As PictureBox)
    
        'http://thinkami.hatenablog.com/entry/2014/07/31/055508
        'https://detail.chiebukuro.yahoo.co.jp/qa/question_detail/q1342332123

        'CreateCameraCaptureの引数はカメラのIndex(通常は0から始まる)
        Dim capture As CvCapture = Cv.CreateCameraCapture(0)

        Dim frame As IplImage = New IplImage()

        'W320 x H240のウィンドウを作る
        Dim w As Double = 640
        Dim h As Double = 480
        Cv.SetCaptureProperty(capture, CaptureProperty.FrameWidth, w)
        Cv.SetCaptureProperty(capture, CaptureProperty.FrameHeight, h)

        System.Threading.Thread.Sleep(1000)

        '何かキーを押すまでは、Webカメラの画像を表示し続ける
        While 1

            Dim c As Integer = Cv.WaitKey(1)
            'noroi
            'Dim c As Integer = Cv.WaitKey(0)
            'esc key
            If c = 27 Or c = Asc("q") Then
              '使い終わったWindow「Capture」を破棄
               Cv.DestroyWindow("Capture")

                MsgBox("終了しました。")
                Exit While
            ElseIf c = Asc("c") Then
                MsgBox("キャプチャ")
                frame.SaveImage(".\captureresult.bmp")

                OpenFile2(bmpOriginalImage, ".\captureresult.bmp")
                If Not (bmpOriginalImage Is Nothing) Then
                    CopyImage(bmpOriginalImage, bmpCurrentImage)
                End If
                DrawImage(picImage)
              '使い終わったWindow「Capture」を破棄
               Cv.DestroyWindow("Capture")

                Exit While
            End If
            'System.Diagnostics.Debug.Print(c.ToString)

            System.Threading.Thread.Sleep(100)

            'カメラからフレームを取得
            frame = Cv.QueryFrame(capture)
            'iplImage = Cv.QueryFrame(capture)


            'Window「Capture」を作って、Webカメラの画像を表示
            Cv.ShowImage("Capture", frame)
            'bmpImage = IplImageToBitmap(iplImage)

            DrawImage(picImage)
            ' bmp以外にも、jpegやpngでの保存が可能
            'noroi
            'Application.DoEvents()

        End While


    End Sub

    Public Sub DrawString(ByRef bmpImage As Bitmap)
        iplImage = BitmapToIplImage(bmpImage)

        Dim TextFont As CvFont = Nothing
        Dim TextSize As CvSize = Nothing
        Dim TextPos As CvPoint = Nothing
        Dim DrawText As String = "Test String"


        Cv.InitFont(TextFont, FontFace.HersheyTriplex, 1, 1)
        Cv.GetTextSize(DrawText, TextFont, TextSize, 0)
        TextPos = Cv.Point(100, 100)
        Cv.PutText(iplImage, DrawText, TextPos, TextFont, Cv.RGB(255, 255, 255))

        bmpImage = IplImageToBitmap(iplImage)

    End Sub


End Class
