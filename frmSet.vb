Public Class frmSet

    Public paranum As Integer
    Public value1 As Double
    Public value2 As Double
    Public value3 As Double
    Public value4 As Double
    Public canceled As Boolean

    Private Sub cmdSet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSet.Click
        If txtValue1.Text = "" Then
            MsgBox("ílÇ™ì¸óÕÇ≥ÇÍÇƒÇ¢Ç‹ÇπÇÒÅB", vbExclamation)
            Exit Sub
        End If
        Try
            value1 = CDbl(txtValue1.Text)
        Catch ex As Exception
            value1 = Double.MaxValue
        End Try
        If txtValue2.Visible = True Then
            Try
                value2 = CDbl(txtValue2.Text)
            Catch ex As Exception
                value2 = Double.MaxValue
            End Try
        End If
        If txtValue3.Visible = True Then
            Try
                value3 = CDbl(txtValue3.Text)
            Catch ex As Exception
                value3 = Double.MaxValue
            End Try
        End If
        If txtValue4.Visible = True Then
            Try
                value4 = CDbl(txtValue4.Text)
            Catch ex As Exception
                value4 = Double.MaxValue
            End Try
        End If

        Me.Dispose()
    End Sub

    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        canceled = True
        Me.Dispose()
    End Sub

    Private Sub frmSet_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        canceled = False
        Select Case paranum
            Case 1
                Label2.Visible = False
                Label3.Visible = False
                Label4.Visible = False
                txtValue2.Visible = False
                txtValue3.Visible = False
                txtValue4.Visible = False
            Case 2
                Label2.Visible = True
                Label3.Visible = False
                Label4.Visible = False
                txtValue2.Visible = True
                txtValue3.Visible = False
                txtValue4.Visible = False
            Case 3
                Label2.Visible = True
                Label3.Visible = True
                Label4.Visible = False
                txtValue2.Visible = True
                txtValue3.Visible = True
                txtValue4.Visible = False
            Case 4
                Label2.Visible = True
                Label3.Visible = True
                Label4.Visible = True
                txtValue2.Visible = True
                txtValue3.Visible = True
                txtValue4.Visible = True
            Case Else
                Label2.Visible = False
                Label3.Visible = False
                Label4.Visible = False
                txtValue2.Visible = False
                txtValue3.Visible = False
                txtValue4.Visible = False

        End Select

        'txtValue1.Focus()
    End Sub
End Class