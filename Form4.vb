Imports MySql.Data.MySqlClient
Public Class Form4
    Dim connection As New MySqlConnection("datasource=localhost;port=3306;username=root;password=;database=cricketdb")
    Private Sub Form4_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub btnFeedbackReset_Click(sender As Object, e As EventArgs) Handles btnFeedbackReset.Click
        txtName.Text = ""
        txtEmail.Text = ""
        txtPhone.Text = ""
        txtFeedback.Text = ""
    End Sub

    Private Sub btnFeedbackSubmit_Click(sender As Object, e As EventArgs) Handles btnFeedbackSubmit.Click

        If isPresent(txtName.Text) AndAlso isPresent(txtEmail.Text) AndAlso isPresent(txtFeedback.Text) AndAlso isValid(txtPhone.Text) Then
            Dim query As String = "INSERT INTO `feedback`(`name`, `email`, `phone`, `feedback`) VALUES (@name,@email,@phone,@description)"
            Dim exeQuery As New MySqlCommand(query, connection)
            exeQuery.Parameters.Add("@name", MySqlDbType.VarChar).Value = txtName.Text
            exeQuery.Parameters.Add("@email", MySqlDbType.VarChar).Value = txtEmail.Text
            exeQuery.Parameters.Add("@phone", MySqlDbType.Decimal).Value = Convert.ToDecimal(txtPhone.Text)
            exeQuery.Parameters.Add("@description", MySqlDbType.VarChar).Value = txtFeedback.Text
            connection.Open()

            If exeQuery.ExecuteNonQuery() = 1 Then
                MessageBox.Show("Data Succesfully Inserted " & vbCrLf & "Thank you " & txtName.Text & " for Your Feedback!", "Thanks")
                Call btnFeedbackReset_Click(btnFeedbackReset, New EventArgs())
            Else
                MessageBox.Show("Database Error", "Error")
            End If

            connection.Close()

            End If
    End Sub

    Private Function isPresent(ByVal text As String) As Boolean
        If text = "" Then
            MessageBox.Show("Please Enter All fields", "Entry Error")
            Return False
        Else
            Return True
        End If
    End Function

    Private Function isValid(ByVal text As String) As Boolean
        Dim number As Decimal
        If text = "" Then
            Return False
        Else
            Try
                number = Convert.ToDecimal(text)
                If text.Length >= 10 Then
                    Return True
                Else
                    MessageBox.Show("Please Enter 10 digit Phone number", "Entry Error")
                    Return False
                End If
            Catch ex As Exception
                MessageBox.Show("Invalid Phone Number, Please enter in numeric format", "Entry Error")
                txtPhone.Select()
            End Try

            Return False

        End If
    End Function

    Private Sub txtPhone_TextChanged(sender As Object, e As EventArgs) Handles txtPhone.TextChanged

    End Sub
End Class