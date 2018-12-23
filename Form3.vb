Imports MySql.Data.MySqlClient
Public Class Form3
    Dim connection As New MySqlConnection("datasource=localhost;port=3306;username=root;password=;database=cricketdb")
    Dim value As String = ""
    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbo1.SelectedIndexChanged

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btnFeedback.Click
        Form4.Show()
    End Sub

    Private Sub Form3_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Me.WindowState = FormWindowState.Maximized
        GroupBox1.Hide()
        Dim operations() As String = {"Highest Runs", "Highest Score By Individuals", "Best Average in Batting", "Most Centuries", "Most Fifties", "Most Ducks",
                                      "Most Wickets", "Best Economy", "Best All Rounder",
                                      "Most Catches As Keeper", "Most Catches As Fielder", "Most Stumps", "ICC Rankings",
                                      "Highest Score By Teams", "Lowest Score By Teams",
                                      "Most Runs in ODI & Test Formats", "Most Wickets in ODI & Test Formats", "Teams Winning Ratio"}

        For Each operation As String In operations
            cbo1.Items.Add(operation)
        Next

        Dim SelectString As String = "Select an Option.."
        cbo1.Text = SelectString

    End Sub

    Private Sub btnCombobox_Click(sender As Object, e As EventArgs) Handles btnCombobox.Click
        GroupBox1.Show()
        value = cbo1.Text
        If value = "Most Runs in ODI & Test Formats" Or value = "Most Wickets in ODI & Test Formats" Then
            GroupBox1.Hide()
            odi.Checked = True
            Call btnFormat_Click(sender, New EventArgs)
        End If
    End Sub

    Private Sub btnFormat_Click(sender As Object, e As EventArgs) Handles btnFormat.Click

        'connection.Open()
        Dim SDA As New MySqlDataAdapter
        Dim dbDataset As New DataTable
        Dim bSource As New BindingSource
        Dim query As String = ""
        Dim Table_name As String = ""
        If test.Checked Then
            Table_name = "test_"
        ElseIf odi.Checked Then
            Table_name = "odi_"
        ElseIf t20.Checked Then
            Table_name = "t20_"
        Else
            MessageBox.Show("Please Select a Format", "Entry Error")
            GroupBox1.Select()
        End If
        Dim temp As String = Table_name
        Try
            Select Case value
                Case "Highest Runs"
                    Table_name &= "batting"
                    query = "Select Player,Country,Matches_Played,Innings_Batted,Runs_Scored, Highest_Innings_Score_Num As HighestScore From " & Table_name & " Order by Runs_Scored DESC LIMIT 10"
                Case "Highest Score By Individuals"
                    Table_name &= "batting"
                    query = "Select Player,Country,Matches_Played,Innings_Batted,Runs_Scored, Highest_Innings_Score_Num As HighestScore From " & Table_name & " Order by Highest_Innings_Score_Num DESC LIMIT 10"
                Case "Best Average in Batting"
                    Table_name &= "batting"
                    query = "Select Player,Country,Matches_Played,Innings_Batted,Runs_Scored, Round(Runs_Scored/Innings_Batted,2) As Average From " & Table_name & " Where Matches_Played >= 50 Order by Average DESC LIMIT 10"
                Case "Most Centuries"
                    Table_name &= "batting"
                    query = "Select Player,Country,Matches_Played,Innings_Batted,Runs_Scored,Hundreds_Scored From " & Table_name & " Order by Hundreds_Scored DESC LIMIT 10"
                Case "Most Fifties"
                    Table_name &= "batting"
                    query = "Select Player,Country,Matches_Played,Innings_Batted,Runs_Scored,Fifties From " & Table_name & " Order by Fifties DESC LIMIT 10"
                Case "Most Ducks"
                    Table_name &= "batting"
                    query = "Select Player,Country,Matches_Played,Innings_Batted,Runs_Scored, Ducks_Scored As Number_Of_Ducks From " & Table_name & " Order by Ducks_Scored DESC LIMIT 10"
                Case "Most Wickets"
                    Table_name &= "bowling"
                    query = "Select Player,Innings_Bowled,Wickets_Taken As Wickets,Best_Bowling From " & Table_name & " Order by Wickets_Taken DESC LIMIT 10"
                Case "Best Economy"
                    Table_name &= "bowling"
                    query = "Select Player,Innings_Bowled,Wickets_Taken As Wickets,Economy_Rate As Economy From " & Table_name & " Where Innings_Bowled > 50  Order by Economy_Rate ASC LIMIT 10"
                Case "Best All Rounder"
                    Table_name &= "batting"
                    query = "Select Player,Matches_Played,Innings_Batted,Runs_Scored From " & Table_name & " Where Player In (Select Player from " & temp & "allround Order by Diff DESC)"
                Case "Most Catches As Keeper"
                    Table_name &= "fielding"
                    query = "Select Player,Innings_Fielded,	Catches_Keeper From " & Table_name & " Order by Catches_Keeper DESC LIMIT 10"
                Case "Most Catches As Fielder"
                    Table_name &= "fielding"
                    query = "Select Player,Innings_Fielded,	Catches_Fielder From " & Table_name & " Order by Catches_Fielder DESC LIMIT 10"
                Case "Most Stumps"
                    Table_name &= "fielding"
                    query = "Select Player,Innings_Fielded,Stumpings_Made From " & Table_name & " Order by Stumpings_Made DESC LIMIT 10"
                Case "ICC Rankings"
                    Table_name &= "rankings"
                    query = "Select Country,Matches,Points,Rating From " & Table_name & " LIMIT 10"
                Case "Highest Score By Teams"
                    Table_name &= "teambatting"
                    query = "Select Country,Highest_Team_Score_Batting From " & Table_name & " Order by Highest_Team_Score_Batting DESC LIMIT 10"
                Case "Lowest Score By Teams"
                    Table_name &= "teambatting"
                    query = "Select Country,Lowest_Completed_Score_Batting From " & Table_name & " Order by Lowest_Completed_Score_Batting ASC LIMIT 10"
                Case "Teams Winning Ratio"
                    Table_name &= "teambatting"
                    If test.Checked Then
                        query = "Select Country,Teams_Matches_Played,Matches_Won,Matches_Lost,Matches_Tied,Matches_Drawn, Round(Matches_Won/Teams_Matches_Played,2) As Win_Ratio From " & Table_name & " Order by Win_Ratio DESC LIMIT 10"
                    Else
                        query = "Select Country,Teams_Matches_Played,Matches_Won,Matches_Lost,Matches_Tied,Matches_With_No_Result, Round(Matches_Won/Teams_Matches_Played,2) As Win_Ratio From " & Table_name & " Order by Win_Ratio DESC LIMIT 10"
                    End If
                Case "Most Runs in ODI & Test Formats"
                    Table_name = "batting"
                    query = " Select odi_batting.Player As Player,odi_batting.Country,(odi_batting.Runs_Scored+test_batting.Runs_Scored) As Total_Runs From odi_batting LEFT OUTER JOIN test_batting ON odi_batting.Player=test_batting.Player 
                              UNION Select odi_batting.Player As Player,odi_batting.Country,(odi_batting.Runs_Scored+test_batting.Runs_Scored) As Total_Runs From odi_batting RIGHT OUTER JOIN test_batting ON odi_batting.Player=test_batting.Player Order by Total_Runs DESC LIMIT 10"
                Case "Most Wickets in ODI & Test Formats"
                    query = " Select odi_bowling.Player As Player,(odi_bowling.Wickets_Taken+test_bowling.Wickets_Taken) As Total_Wickets From odi_bowling LEFT OUTER JOIN test_bowling ON odi_bowling.Player=test_bowling.Player 
                              UNION Select odi_bowling.Player As Player,(odi_bowling.Wickets_Taken+test_bowling.Wickets_Taken) As Total_Wickets From odi_bowling RIGHT OUTER JOIN test_bowling ON odi_bowling.Player=test_bowling.Player ORDER BY Total_Wickets DESC Limit 10"
                Case "Select an Option.."
                    MessageBox.Show("Please Select a valid Option and" & vbCrLf & " Click Submit Button", "Entry Error")
                    cbo1.Select()
                    Exit Sub
            End Select
            Dim exeQuery As New MySqlCommand(query, connection)
            SDA.SelectCommand = exeQuery
            SDA.Fill(dbDataset)
            bSource.DataSource = dbDataset
            DataGridView1.DataSource = bSource
            SDA.Update(dbDataset)

            connection.Close()

        Catch ex As Exception
            MessageBox.Show(ex.Message)

        End Try


    End Sub

End Class