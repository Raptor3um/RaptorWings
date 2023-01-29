Module miningsetting_rtm
    Public Sub Miningsetting()
        'Automate mining settings when changing in the mining menu / Mining Einstellungen automatisieren bei Änderungen im Mining Menü
        'This function is run through with every change in the mining menu / Diese funktion wird bei jeder Änderung im Mining Menü durchlaufen
        Cursor.Current = Cursors.WaitCursor

        Dim pool As String = Form1.ComboBox2.Text 'Fill the variable with the selected pool designation / Variable mit der gewählten Poolbeeichnung befüllen

        'Set Variable Solo to False and activate the associated checkbox / Variable Solo auf False setzten und die zugehörige Checkbox aktivieren
        Dim solo As String = False
        Form1.CheckBox3.Enabled = True

        'If Checkbox3 (SOLO) was clicked set the variable Solo to True / Wenn Cehckbox3 (SOLO) angeklikt wurde die Variable Solo auf True setzten
        If Form1.CheckBox3.Checked = True Then
            solo = True
        End If

        'Enter pool data of the selected pool in the controls /Pooldaten des ausgewählten Pools in die Steuerelemente eintragen
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

        'Enter pool data of the selected pool in the controls /Pooldaten des ausgewählten Pools in die Steuerelemente eintragen
        If pool = "Raptoreum.Zone" Then
            Form1.ComboBox4.Items.Clear()
            Form1.ComboBox4.Items.Add("stratum+tcp://europe-1.raptoreum.zone:3333")
            Form1.ComboBox4.Items.Add("stratum+tcp://europe-1.raptoreum.zone:4444")
            Form1.ComboBox4.Items.Add("stratum+tcp://europe-2.raptoreum.zone:3333")
            Form1.ComboBox4.Items.Add("stratum+tcp://europe-2.raptoreum.zone:4444")
            Form1.ComboBox4.Items.Add("stratum+tcp://usa-east-1.raptoreum.zone:3333")
            Form1.ComboBox4.Items.Add("stratum+tcp://usa-east-1.raptoreum.zone:4444")
            Form1.ComboBox4.Items.Add("stratum+tcp://usa-east-2.raptoreum.zone:3333")
            Form1.ComboBox4.Items.Add("stratum+tcp://usa-east-2.raptoreum.zone:4444")
            Form1.ComboBox4.Items.Add("stratum+tcp://usa-west.raptoreum.zone:3333")
            Form1.ComboBox4.Items.Add("stratum+tcp://usa-west.raptoreum.zone:4444")
            Form1.ComboBox4.Items.Add("stratum+tcp://asia.raptoreum.zone:3333")
            Form1.ComboBox4.Items.Add("stratum+tcp://asia.raptoreum.zone:4444")
            Form1.ComboBox4.SelectedIndex = 0
            Form1.TextBox2.Enabled = True
            Form1.TextBox2.Text = "x"
            Form1.CheckBox3.Checked = False
            Form1.CheckBox3.Enabled = False

        End If

        'Enter pool data of the selected pool in the controls /Pooldaten des ausgewählten Pools in die Steuerelemente eintragen
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
