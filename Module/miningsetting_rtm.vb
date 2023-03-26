'Copyright(c) 2023 The Raptoreum developers
'Copyright(c) 2023 Germardies

Module miningsetting_rtm
    Public Sub Miningsetting()
        Cursor.Current = Cursors.WaitCursor

        Dim pool As String = Form1.ComboBox2.Text

        Dim solo As String = False
        Form1.CheckBox3.Enabled = True

        If Form1.CheckBox3.Checked = True Then
            solo = True
        End If

        If pool = "Raptorhash.com" Then
            Form1.ComboBox4.Items.Clear()
            Form1.ComboBox4.Items.Add("statum+tcp://na.raptorhash.com:6900")
            Form1.ComboBox4.Items.Add("statum+tcp://na.raptorhash.com:6500")
            Form1.ComboBox4.SelectedIndex = 0
            Form1.TextBox2.Enabled = False
            If solo = True Then
                Form1.TextBox2.Text = "c=RTM,m=solo"
            Else
                Form1.TextBox2.Text = "c=RTM"
            End If
        End If

        If pool = "Raptoreum.Zone" And Form1.CheckBox3.Checked = False Then
            Form1.ComboBox4.Items.Clear()
            Form1.ComboBox4.Items.Add("stratum+tcp://europe.raptoreum.zone:3333")
            Form1.ComboBox4.Items.Add("stratum+tcps://europe.raptoreum.zone:4444")
            Form1.ComboBox4.Items.Add("stratum+tcp://usa-east.raptoreum.zone:3333")
            Form1.ComboBox4.Items.Add("stratum+tcps://usa-east.raptoreum.zone:4444")
            Form1.ComboBox4.Items.Add("stratum+tcp://usa-west.raptoreum.zone:3333")
            Form1.ComboBox4.Items.Add("stratum+tcps://usa-west.raptoreum.zone:4444")
            Form1.ComboBox4.Items.Add("stratum+tcp://asia.raptoreum.zone:3333")
            Form1.ComboBox4.Items.Add("stratum+tcps://asia.raptoreum.zone:4444")
            Form1.ComboBox4.SelectedIndex = 0
            Form1.TextBox2.Enabled = True
            Form1.TextBox2.Text = "x"
            Form1.CheckBox3.Enabled = True
        End If

        If pool = "Raptoreum.Zone" And Form1.CheckBox3.Checked = True Then
            Form1.ComboBox4.Items.Clear()
            Form1.ComboBox4.Items.Add("stratum+tcp://europe.raptoreum.zone:4010")
            Form1.ComboBox4.Items.Add("stratum+tcps://europe.raptoreum.zone:5010")
            Form1.ComboBox4.Items.Add("stratum+tcp://usa-east.raptoreum.zone:4010")
            Form1.ComboBox4.Items.Add("stratum+tcps://usa-east.raptoreum.zone:5010")
            Form1.ComboBox4.Items.Add("stratum+tcp://usa-west.raptoreum.zone:5010")
            Form1.ComboBox4.Items.Add("stratum+tcps://usa-west.raptoreum.zone:4444")
            Form1.ComboBox4.Items.Add("stratum+tcp://asia.raptoreum.zone:4010")
            Form1.ComboBox4.Items.Add("stratum+tcps://asia.raptoreum.zone:5010")
            Form1.ComboBox4.SelectedIndex = 0
            Form1.TextBox2.Enabled = True
            Form1.TextBox2.Text = "x"
            Form1.CheckBox3.Enabled = True
        End If

        If pool = "FlockPool" Then
            Form1.ComboBox4.Items.Clear()
            Form1.ComboBox4.Items.Add("stratum+tcps://eu.flockpool.com:5555")
            Form1.ComboBox4.Items.Add("stratum+tcp://eu.flockpool.com:4444")
            Form1.ComboBox4.Items.Add("stratum+tcps://us.flockpool.com:5555")
            Form1.ComboBox4.Items.Add("stratum+tcp://us.flockpool.com:4444")
            Form1.ComboBox4.Items.Add("stratum+tcps://us-west.flockpool.com:5555")
            Form1.ComboBox4.Items.Add("stratum+tcp://us-west.flockpool.com:4444")
            Form1.ComboBox4.Items.Add("stratum+tcps://asia.flockpool.com:5555")
            Form1.ComboBox4.Items.Add("stratum+tcp://asia.flockpool.com:4444")
            Form1.ComboBox4.SelectedIndex = 0
            Form1.TextBox2.Enabled = True
            Form1.TextBox2.Text = "x"

            Form1.CheckBox3.Checked = False
            Form1.CheckBox3.Enabled = False
        End If
        Cursor.Current = Cursors.Default
    End Sub
End Module
