Imports System.Drawing.Imaging
Imports System.Runtime.InteropServices




Module Img2Array

    Public WrtLog As Boolean
    Public WrtMap As Boolean
    Public WrtCat As Boolean
    Public WrtFrameNum As Boolean
    Public WrtAstroData As Boolean
    Public MinPix As Integer
    Public Resolution As Double
    Public Frames As Integer
    Public StartGap As Double
    Public Var1 As Double
    Public Var2 As Double
    Public OutputPath As String = My.Computer.FileSystem.SpecialDirectories.Temp & "\AstroAnimation"
    Public AstroList() As Integer

    Public Sub DoGraphics(Source As String)
        Dim x As Integer
        Dim y As Integer
        Dim F As Integer

        Logger.Flog("", LogCMD.Init)

        HUDFAnimatorForm.ToolStripStatusLabel1.Text = "1 Init" : HUDFAnimatorForm.Refresh()

        'Scrive in un CSV i tempi di elaborazione
        If WrtLog = True Then
            Logger.Flog("Log.csv", LogCMD.Delete)
            Logger.Flog("Log.csv", LogCMD.NewFile)
            Logger.Flog("Log.csv", LogCMD.Write, "Fase;Data Ora")
            Logger.Flog("Log.csv", LogCMD.Write, "START                   " & ";" & DateTime.Now)
        End If

        'PixelSize is 3 bytes for a 24bpp Argb image.
        'Change this value appropriately
        Dim PixelSize As Integer = 3

        'Load the bitmap
        Dim bm As Bitmap = Image.FromFile(Source)

        HUDFAnimatorForm.ToolStripStatusLabel1.Text = "2 Loading image" : HUDFAnimatorForm.Refresh()

        Dim bmData As BitmapData = bm.LockBits(New Rectangle(0, 0, bm.Width, bm.Height), System.Drawing.Imaging.ImageLockMode.ReadWrite, bm.PixelFormat)
        Dim pixels(bm.Width - 1, bm.Height - 1) As Color
        Dim pixelsOUT(bm.Width - 1, bm.Height - 1) As Color

        For x = 0 To bmData.Width - 1
            For y = 0 To bmData.Height - 1
                '24bpp rgb bitmap
                Dim blueOfs As Integer = (bmData.Stride * x) + (PixelSize * y)
                Dim greenOfs As Integer = blueOfs + 1
                Dim redOfs As Integer = greenOfs + 1
                Dim red As Integer = Marshal.ReadByte(bmData.Scan0, redOfs)
                Dim green As Integer = Marshal.ReadByte(bmData.Scan0, greenOfs)
                Dim blue As Integer = Marshal.ReadByte(bmData.Scan0, blueOfs)
                pixels(x, y) = Color.FromArgb(red, green, blue)
            Next
        Next

        HUDFAnimatorForm.ToolStripStatusLabel1.Text = "3 Cut dark pixels" : HUDFAnimatorForm.Refresh()
        If WrtLog = True Then Logger.Flog("Log.csv", LogCMD.Write, "START Filtro luminosità " & ";" & DateTime.Now)
        'Esclude i pixel la cui luminosità sia al di sotto di un determinato valore minimo
        Dim H As Single
        Dim S As Single
        Dim L As Single
        For x = 0 To bmData.Width - 1
            For y = 0 To bmData.Height - 1
                H = pixels(x, y).GetHue
                S = pixels(x, y).GetSaturation
                L = pixels(x, y).GetBrightness
                If L < 0.1 Then pixels(x, y) = Color.FromArgb(0, 0, 0)
            Next
        Next

        HUDFAnimatorForm.ToolStripStatusLabel1.Text = "4 Cut small objects" : HUDFAnimatorForm.Refresh()
        If WrtLog = True Then Logger.Flog("Log.csv", LogCMD.Write, "START Filtro area       " & ";" & DateTime.Now)
        'Esclude l'insieme di pixel la cui superficie totale non superi un quadrato di lato W
        Dim Wdt As Integer = 1
        Dim Nx As Integer
        Dim Ny As Integer
        Dim C_TOT As Integer = 0
        Dim C_EAST As Integer = 0
        Dim C_WEST As Integer = 0
        Dim C_NORTH As Integer = 0
        Dim C_SOUTH As Integer = 0
        Dim Counter As Integer = 0
        If Wdt > 0 Then
            For x = 0 To bmData.Width - 1
                If x / (bmData.Width - 1) * 100 = CInt(x / (bmData.Width - 1) * 100) Then
                    HUDFAnimatorForm.ToolStripProgressBar1.Value = CInt(x / (bmData.Width - 1) * 100)
                    HUDFAnimatorForm.Refresh()
                End If
                For y = 0 To bmData.Height - 1

                    Nx = x + Wdt
                    For Ny = Wdt To Wdt * -1 Step -1
                        C_TOT = C_TOT + 1
                        If Nx >= 0 And Nx < (bmData.Width - 1) And (Ny + y) >= 0 And (Ny + y) <= (bm.Height - 1) Then
                            'If pixels(Nx, Ny + y).R = 0 And pixels(Nx, Ny + y).G = 0 And pixels(Nx, Ny + y).B = 0 Then C_EAST = C_EAST + 1
                            If pixels(Nx, Ny + y) = Color.FromArgb(0, 0, 0) Then C_EAST += 1
                        End If
                    Next

                    Nx = x - Wdt
                    For Ny = Wdt To Wdt * -1 Step -1
                        If Nx >= 0 And Nx < (bmData.Width - 1) And Ny + y >= 0 And Ny + y <= (bm.Height - 1) Then
                            'If pixels(Nx, Ny + y).R = 0 And pixels(Nx, Ny + y).G = 0 And pixels(Nx, Ny + y).B = 0 Then C_WEST = C_WEST + 1
                            If pixels(Nx, Ny + y) = Color.FromArgb(0, 0, 0) Then C_WEST += 1
                        End If
                    Next

                    Ny = y - Wdt
                    For Nx = Wdt To Wdt * -1 Step -1
                        If Nx + x >= 0 And Nx + x < (bmData.Width - 1) And Ny >= 0 And Ny <= (bm.Height - 1) Then
                            'If pixels(Nx + x, Ny).R = 0 And pixels(Nx + x, Ny).G = 0 And pixels(Nx + x, Ny).B = 0 Then C_SOUTH = C_SOUTH + 1
                            If pixels(Nx + x, Ny) = Color.FromArgb(0, 0, 0) Then C_SOUTH += 1
                        End If
                    Next

                    Ny = y + Wdt
                    For Nx = Wdt To Wdt * -1 Step -1
                        If Nx + x >= 0 And Nx + x < (bmData.Width - 1) And Ny >= 0 And Ny <= (bm.Height - 1) Then
                            'If pixels(Nx + x, Ny).R = 0 And pixels(Nx + x, Ny).G = 0 And pixels(Nx + x, Ny).B = 0 Then C_NORTH = C_NORTH + 1
                            If pixels(Nx + x, Ny) = Color.FromArgb(0, 0, 0) Then C_NORTH += 1
                        End If
                    Next

                    If C_EAST + C_WEST + C_NORTH + C_SOUTH = 4 * C_TOT Then
                        For Nx = -1 * Wdt To Wdt
                            For Ny = -1 * Wdt To Wdt
                                pixels(x, y) = Color.FromArgb(0, 0, 0)
                                Counter += 1
                            Next
                        Next
                    End If

                    C_NORTH = 0
                    C_SOUTH = 0
                    C_EAST = 0
                    C_WEST = 0
                    C_TOT = 0

                Next
            Next
        End If

        'verifica origine e direzione assi

        'For x = 50 To 100
        '    pixels(x, 0) = Color.Red
        'Next
        'For y = 50 To 100
        '    pixels(0, y) = Color.White
        'Next

        HUDFAnimatorForm.ToolStripStatusLabel1.Text = "5 Identify objects" : HUDFAnimatorForm.Refresh()
        If WrtLog = True Then Logger.Flog("Log.csv", LogCMD.Write, "START trova oggetti     " & ";" & DateTime.Now)
        'Assegna un numero oggetto ai pixel contigui con colore diverso da nero
        Dim Item As Integer = 0
        Dim Hue As Integer = 1
        Dim Brightness As Integer = 2
        Dim ItemNumber As Integer = 0
        Dim WSpan As Integer = bmData.Width - 1
        Dim HSpan As Integer = bm.Height - 1
        Dim AstroMap(WSpan, HSpan, 3) As Integer
        Dim NewItem As Boolean
        Dim J As Integer
        For x = 0 To bmData.Width - 1
            If x / (bmData.Width - 1) * 100 = CInt(x / (bmData.Width - 1) * 100) Then
                HUDFAnimatorForm.ToolStripProgressBar1.Value = CInt(x / (bmData.Width - 1) * 100)
                HUDFAnimatorForm.Refresh()
            End If
            For y = 0 To bmData.Height - 1
                If Not (pixels(x, y) = Color.FromArgb(0, 0, 0)) Then

                    NewItem = True

                    'NE
                    If (x - 1 >= 0 And x - 1 < (WSpan) And y + 1 >= 0 And y + 1 <= (WSpan)) Then
                        If AstroMap(y + 1, x - 1, Item) > 0 Then
                            AstroMap(y, x, Item) = AstroMap(y + 1, x - 1, Item)
                            For J = y To 0 Step -1
                                If AstroMap(J, x, Item) > 0 Then
                                    AstroMap(J, x, Item) = AstroMap(y + 1, x - 1, Item)
                                Else
                                    J = 0
                                End If
                            Next
                            NewItem = False
                        End If
                    End If

                    'NW
                    If NewItem And (x - 1 >= 0 And x - 1 < (WSpan) And y - 1 >= 0 And y - 1 <= (WSpan)) Then
                        If AstroMap(y - 1, x - 1, Item) > 0 Then
                            AstroMap(y, x, Item) = AstroMap(y - 1, x - 1, Item)
                            For J = y To 0 Step -1
                                If AstroMap(J, x, Item) > 0 Then
                                    AstroMap(J, x, Item) = AstroMap(y - 1, x - 1, Item)
                                Else
                                    J = 0
                                End If
                            Next
                            NewItem = False
                        End If
                    End If

                    'N
                    If NewItem And (x - 1 >= 0 And x - 1 < (WSpan) And y - 0 >= 0 And y - 0 <= (WSpan)) Then
                        If AstroMap(y - 0, x - 1, Item) > 0 Then
                            AstroMap(y, x, Item) = AstroMap(y - 0, x - 1, Item)
                            For J = y To 0 Step -1
                                If AstroMap(J, x, Item) > 0 Then
                                    AstroMap(J, x, Item) = AstroMap(y - 0, x - 1, Item)
                                Else
                                    J = 0
                                End If
                            Next
                            NewItem = False
                        End If
                    End If

                    'W
                    If NewItem And (x - 0 >= 0 And x - 0 < (WSpan) And y - 1 >= 0 And y - 1 <= (WSpan)) Then
                        If AstroMap(y - 1, x - 0, Item) > 0 Then
                            AstroMap(y, x, Item) = AstroMap(y - 1, x - 0, Item)
                            For J = y To 0 Step -1
                                If AstroMap(J, x, Item) > 0 Then
                                    AstroMap(J, x, Item) = AstroMap(y - 1, x - 0, Item)
                                Else
                                    J = 0
                                End If
                            Next
                            NewItem = False
                        End If
                    End If

                    If NewItem = True Then
                        ItemNumber += 1
                        AstroMap(y, x, Item) = ItemNumber
                    End If

                End If
            Next
        Next

        HUDFAnimatorForm.ToolStripStatusLabel1.Text = "6 Merging contiguos objects" : HUDFAnimatorForm.Refresh()
        If WrtLog = True Then Logger.Flog("Log.csv", LogCMD.Write, "START unisci oggetti    " & ";" & DateTime.Now)
        'collega oggetti in contatto
        Dim Ref As Integer
        Dim Obsolete As Integer
        Dim x1 As Integer
        Dim y1 As Integer
        Dim ToMerge As Boolean
        For x = 0 To bmData.Width - 1
            For y = 0 To bmData.Height - 1
                ToMerge = True
                'SW
                If (x + 1 >= 0 And x + 1 < (WSpan) And y - 1 >= 0 And y - 1 <= (WSpan)) Then
                    If AstroMap(y - 1, x + 1, Item) > 0 And AstroMap(y, x, Item) > 0 And AstroMap(y - 1, x + 1, Item) <> AstroMap(y, x, Item) Then
                        Ref = AstroMap(y, x, Item)
                        Obsolete = AstroMap(y - 1, x + 1, Item)
                        For x1 = 0 To bmData.Width - 1
                            For y1 = 0 To bmData.Height - 1
                                If AstroMap(y1, x1, Item) = Obsolete Then AstroMap(y1, x1, Item) = Ref
                            Next
                        Next
                        ToMerge = False
                    End If
                End If

                'S
                If ToMerge = True And ((x + 1 >= 0 And x + 1 < (WSpan) And y - 0 >= 0 And y - 0 <= (WSpan))) Then
                    If AstroMap(y - 0, x + 1, Item) > 0 And AstroMap(y, x, Item) > 0 And AstroMap(y - 0, x + 1, Item) <> AstroMap(y, x, Item) Then
                        Ref = AstroMap(y, x, Item)
                        Obsolete = AstroMap(y - 0, x + 1, Item)
                        For x1 = 0 To bmData.Width - 1
                            For y1 = 0 To bmData.Height - 1
                                If AstroMap(y1, x1, Item) = Obsolete Then AstroMap(y1, x1, Item) = Ref
                            Next
                        Next
                        ToMerge = False
                    End If
                End If

                'SE
                If ToMerge = True And ((x + 1 >= 0 And x + 1 < (WSpan) And y + 1 >= 0 And y + 1 <= (WSpan))) Then
                    If AstroMap(y + 1, x + 1, Item) > 0 And AstroMap(y, x, Item) > 0 And AstroMap(y + 1, x + 1, Item) <> AstroMap(y, x, Item) Then
                        Ref = AstroMap(y, x, Item)
                        Obsolete = AstroMap(y + 1, x + 1, Item)
                        For x1 = 0 To bmData.Width - 1
                            For y1 = 0 To bmData.Height - 1
                                If AstroMap(y1, x1, Item) = Obsolete Then AstroMap(y1, x1, Item) = Ref
                            Next
                        Next
                        ToMerge = False
                    End If
                End If

                'E
                If ToMerge = True And ((x + 0 >= 0 And x + 0 < (WSpan) And y + 1 >= 0 And y + 1 <= (WSpan))) Then
                    If AstroMap(y + 1, x + 0, Item) > 0 And AstroMap(y, x, Item) > 0 And AstroMap(y + 1, x + 0, Item) <> AstroMap(y, x, Item) Then
                        Ref = AstroMap(y, x, Item)
                        Obsolete = AstroMap(y + 1, x + 0, Item)
                        For x1 = 0 To bmData.Width - 1
                            For y1 = 0 To bmData.Height - 1
                                If AstroMap(y1, x1, Item) = Obsolete Then AstroMap(y1, x1, Item) = Ref
                            Next
                        Next
                        ToMerge = False
                    End If
                End If
            Next
        Next

        HUDFAnimatorForm.ToolStripStatusLabel1.Text = "7 Create astro list" : HUDFAnimatorForm.Refresh()
        If WrtLog = True Then Logger.Flog("Log.csv", LogCMD.Write, "START lista oggetti     " & ";" & DateTime.Now)
        'Elenca univocamente gli oggetti
        ReDim AstroList(0)
        Dim I As Integer
        Dim Found As Boolean
        Dim Messaggio As String = ""
        For x = 0 To bmData.Width - 1
            For y = 0 To bmData.Height - 1
                If AstroMap(y, x, Item) <> 0 Then
                    Found = False
                    For I = 0 To AstroList.Length - 1
                        If AstroMap(y, x, Item) = AstroList(I) Then Found = True
                    Next
                    If Found = False Then
                        ReDim Preserve AstroList(AstroList.Length)
                        AstroList(AstroList.Length - 1) = AstroMap(y, x, Item)
                    End If
                End If
            Next
        Next

        If WrtAstroData = True Then
            Logger.Flog("AstroList.csv", LogCMD.Delete)
            For n = 1 To AstroList.Length - 1
                Messaggio = Messaggio & n & ";" & AstroList(n)
                Messaggio = Messaggio & vbCrLf
            Next
            Logger.Flog("AstroList.csv", LogCMD.Write, Messaggio)
        End If

        HUDFAnimatorForm.ToolStripStatusLabel1.Text = "8 compute astro properties" : HUDFAnimatorForm.Refresh()
        If WrtLog = True Then Logger.Flog("Log.csv", LogCMD.Write, "START attributi oggetti " & ";" & DateTime.Now)
        'Calcolo area in pixel, media R G B, luminosità, coordinate centro
        Dim Area As Integer = 1
        Dim R_AVG As Integer = 2
        Dim G_AVG As Integer = 3
        Dim B_AVG As Integer = 4
        Dim L_AVG As Integer = 5
        Dim X_AVG As Integer = 6
        Dim Y_AVG As Integer = 7
        Dim MAX_L As Integer = 8
        Dim MAX_R As Integer = 9
        Dim MAX_G As Integer = 10
        Dim MAX_B As Integer = 11
        Dim RANK As Integer = 12
        Dim RANK_N As Integer = 13
        Dim RelSize As Integer = 14
        Dim Npixel As Integer
        Dim Tpixel As Integer
        Dim Sum_R As Double
        Dim Sum_G As Double
        Dim Sum_B As Double
        Dim Sum_L As Double
        Dim Xcenter As Integer
        Dim YCenter As Integer
        Dim AstroData(AstroList.Length - 1, 14) As Double
        For n = 1 To AstroList.Length - 1
            Npixel = 0 : Sum_R = 0 : Sum_G = 0 : Sum_B = 0 : Sum_L = 0 : Xcenter = 0 : YCenter = 0 : AstroData(n, MAX_L) = 0 : AstroData(n, MAX_R) = 0 : AstroData(n, MAX_G) = 0 : AstroData(n, MAX_B) = 0
            For x = 0 To bmData.Width - 1
                For y = 0 To bmData.Height - 1
                    If AstroMap(y, x, Item) = AstroList(n) Then
                        Npixel += 1
                        Sum_R += pixels(x, y).R
                        Sum_G += pixels(x, y).G
                        Sum_B += pixels(x, y).B
                        Sum_L += pixels(x, y).GetBrightness
                        Xcenter += x
                        YCenter += y
                        If pixels(x, y).GetBrightness > AstroData(n, MAX_L) Then
                            AstroData(n, MAX_L) = pixels(x, y).GetBrightness
                            AstroData(n, MAX_R) = pixels(x, y).R
                            AstroData(n, MAX_G) = pixels(x, y).G
                            AstroData(n, MAX_B) = pixels(x, y).B
                        End If
                    End If
                Next
            Next
            AstroData(n, Item) = AstroList(n)
            AstroData(n, Area) = Npixel
            AstroData(n, R_AVG) = Sum_R / Npixel
            AstroData(n, G_AVG) = Sum_G / Npixel
            AstroData(n, B_AVG) = Sum_B / Npixel
            AstroData(n, L_AVG) = Sum_L / Npixel
            AstroData(n, X_AVG) = Int(Xcenter / Npixel)
            AstroData(n, Y_AVG) = Int(YCenter / Npixel)
            If Npixel > Tpixel Then Tpixel = Npixel
            If AstroData(n, Area) >= MinPix Then
                AstroData(n, RANK) = ((AstroData(n, R_AVG) + 1) / (AstroData(n, B_AVG) + 1)) / ((AstroData(n, R_AVG) + AstroData(n, B_AVG) + 1))
            Else
                AstroData(n, RANK) = 0
            End If
        Next

        If WrtLog = True Then Logger.Flog("Log.csv", LogCMD.Write, "START distanza oggetti  " & ";" & DateTime.Now)

        HUDFAnimatorForm.ToolStripStatusLabel1.Text = "9 compute astro distance" : HUDFAnimatorForm.Refresh()
        'Calcola l'ordine di avanzamento in base alla luminosità
        Dim ToSort(AstroList.Length - 1) As Integer
        Dim Keys(AstroList.Length - 1) As Integer
        For n = 0 To AstroList.Length - 1
            ToSort(n) = CInt(AstroData(n, L_AVG) * 10000)
            Keys(n) = AstroData(n, Item)
        Next n

        Array.Sort(ToSort, Keys)

        For m = 1 To AstroList.Length - 1
            For n = 1 To AstroList.Length - 1
                If AstroData(n, Item) = Keys(m) Then
                    AstroData(n, RANK_N) = m
                End If
            Next n
        Next m

        If WrtAstroData = True Then
            'Scrive in un CSV i dati degli oggetti
            Messaggio = "ID;Object;Area;R;G;B;L;Xc;Yc;MaxL;MaxR;MaxG;MaxB;Rank;Rank_N;RelSize" & vbCrLf
            Logger.Flog("AstroData.csv", LogCMD.Delete)
            For n = 1 To AstroList.Length - 1
                Messaggio = Messaggio & n & ";" & AstroData(n, Item) & ";" & AstroData(n, Area) & ";" & AstroData(n, R_AVG) & ";" & AstroData(n, G_AVG) & ";" & AstroData(n, B_AVG) & ";" & AstroData(n, L_AVG) & ";" & AstroData(n, X_AVG) & ";" & AstroData(n, Y_AVG) & ";" & AstroData(n, MAX_L) & ";" & AstroData(n, MAX_R) & ";" & AstroData(n, MAX_B) & ";" & AstroData(n, MAX_B) & ";" & AstroData(n, RANK) & ";" & AstroData(n, RANK_N) & ";" & AstroData(n, RelSize)
                Messaggio = Messaggio & vbCrLf
            Next
            Logger.Flog("AstroData.csv", LogCMD.Write, Messaggio)
        End If

        'Scrive in un CSV la mappa degli oggetti
        Dim DataRow As String
        If WrtMap = True Then Logger.Flog("AstroMap.csv", LogCMD.Delete)
        For x = 0 To bmData.Width - 1
            DataRow = ""
            For y = 0 To bmData.Height - 1
                DataRow = DataRow & ";" & CStr(AstroMap(y, x, Item))
            Next
            If WrtMap = True Then Logger.Flog("AstroMap.csv", LogCMD.Write, DataRow)
        Next

        Dim xt As Integer
        Dim yt As Integer

        HUDFAnimatorForm.ToolStripStatusLabel1.Text = "10 creating astro catalog" : HUDFAnimatorForm.Refresh()
        'Crea il catalogo in immagine degli oggetti
        If WrtCat = True Then
            If WrtLog = True Then Logger.Flog("Log.csv", LogCMD.Write, "START catalogo oggetti  " & ";" & DateTime.Now)
            For F = 1 To AstroList.Length - 1
                If F / (AstroList.Length - 1) * 100 = CInt(F / (AstroList.Length - 1) * 100) Then
                    HUDFAnimatorForm.ToolStripProgressBar1.Value = CInt(F / (AstroList.Length - 1) * 100)
                    HUDFAnimatorForm.Refresh()
                End If
                If AstroData(F, Area) >= MinPix Then
                    Dim newimage As New Bitmap(500, 500, PixelFormat.Format24bppRgb)
                    Dim bm1 As Bitmap = newimage
                    Dim bmDataOUT As BitmapData = bm1.LockBits(New Rectangle(0, 0, bm1.Width, bm1.Height), System.Drawing.Imaging.ImageLockMode.ReadWrite, bm1.PixelFormat)
                    For x = 0 To bmDataOUT.Width - 1
                        For y = 0 To bmDataOUT.Height - 1
                            pixelsOUT(x, y) = Color.FromArgb(0, 0, 0)
                        Next
                    Next
                    For x = 0 To bmData.Width - 1
                        For y = 0 To bmData.Height - 1
                            xt = x + 251 - AstroData(F, X_AVG)
                            yt = y + 251 - AstroData(F, Y_AVG)
                            If AstroMap(y, x, Item) = AstroList(F) Then
                                If xt >= 0 And xt < 500 And yt >= 0 And yt < 500 Then pixelsOUT(xt, yt) = pixels(x, y)
                            Else
                                If xt >= 0 And xt < 500 And yt >= 0 And yt < 500 Then pixelsOUT(xt, yt) = Color.FromArgb(0, 0, 0)
                            End If
                        Next
                    Next

                    For x = 0 To bmDataOUT.Width - 1
                        For y = 0 To bmDataOUT.Height - 1
                            'Get the various color offset locations for each pixel.
                            'This calculation is for a 24bpp rgb bitmap
                            Dim blueOfs As Integer = (bmDataOUT.Stride * x) + (PixelSize * y)
                            Dim greenOfs As Integer = blueOfs + 1
                            Dim redOfs As Integer = greenOfs + 1

                            'Set each component of the pixel
                            'There are 3 bytes that make up each pixel (24bpp rgb)
                            Marshal.WriteByte(bmDataOUT.Scan0, blueOfs, pixelsOUT(x, y).B)
                            Marshal.WriteByte(bmDataOUT.Scan0, greenOfs, pixelsOUT(x, y).G)
                            Marshal.WriteByte(bmDataOUT.Scan0, redOfs, pixelsOUT(x, y).R)
                        Next
                    Next

                    Dim Magnify As Integer = 1
                    bm1.UnlockBits(bmDataOUT)
                    Using gr As Graphics = Graphics.FromImage(bm1)
                        Dim Afnt As New Font("Arbutus Slab", 12, FontStyle.Regular)
                        Dim Abrush As Brush = Brushes.LightGreen
                        Dim p As New System.Drawing.Pen(Brushes.White, Magnify)
                        gr.DrawString("ID " & CStr(F), Afnt, Abrush, 25, 25 * Magnify)
                        gr.DrawString("Item " & AstroData(F, Item), Afnt, Abrush, 25, 50 * Magnify)
                        gr.DrawString("Area " & AstroData(F, Area) & "pixel - Center " & AstroData(F, X_AVG) & "/" & AstroData(F, Y_AVG), Afnt, Abrush, 25, 75 * Magnify)
                        gr.DrawString("(R/B)/(R+B) " & AstroData(F, RANK), Afnt, Abrush, 25, 100 * Magnify)
                        'gr.DrawString("Distance " & AstroData(F, RANK_N), Afnt, Abrush, 25, 125 * Magnify)

                        Dim RBrush As New SolidBrush(Color.FromArgb(AstroData(F, R_AVG), AstroData(F, G_AVG), AstroData(F, B_AVG)))
                        Dim rect As New Rectangle(30 * Magnify, 155 * Magnify, 30 * Magnify, 30 * Magnify)
                        gr.FillRectangle(RBrush, rect)

                        Dim rectz As New Rectangle()

                        Dim RBrushM As New SolidBrush(Color.FromArgb(AstroData(F, MAX_R), AstroData(F, MAX_G), AstroData(F, MAX_B)))
                        Dim rectM As New Rectangle(70 * Magnify, 155 * Magnify, 30 * Magnify, 30 * Magnify)
                        gr.FillRectangle(RBrushM, rectM)

                    End Using
                    bm1.Save(OutputPath & "\CATALOG_" & CStr(F) & ".TIF", System.Drawing.Imaging.ImageFormat.Tiff)
                End If
            Next
        End If

        HUDFAnimatorForm.ToolStripStatusLabel1.Text = "11 creating animation" : HUDFAnimatorForm.Refresh()
        'Animazione
        Dim Frame As Integer
        Dim Vres As Integer = 1080 * Resolution
        Dim XRes As Integer = 1920 * Resolution
        Dim FrameOut(Vres, XRes) As Color
        Dim Overlay(Vres, XRes) As Integer
        Dim Opposto As Double
        Dim Adiacente As Double
        Dim Ipotenusa As Double
        Dim Xn As Double
        Dim Yn As Double
        Dim Counters(AstroList.Length, 3) As Integer

        For Frame = 1 To Frames Step 1
            If Frame / Frames * 100 = CInt(Frame / Frames * 100) Then
                HUDFAnimatorForm.ToolStripProgressBar1.Value = CInt(Frame / Frames * 100)
                HUDFAnimatorForm.Refresh()
            End If
            Dim Xspan = pixels.GetUpperBound(pixels.Rank - 1)
            Dim Yspan = pixels.GetUpperBound(pixels.Rank - 2)
            Dim newimageA As New Bitmap(XRes, Vres, PixelFormat.Format24bppRgb)
            Dim bmA As Bitmap = newimageA
            Dim bmADataOUT As BitmapData = bmA.LockBits(New Rectangle(0, 0, XRes, Vres), System.Drawing.Imaging.ImageLockMode.ReadWrite, bmA.PixelFormat)
            For y = 0 To Vres
                For x = 0 To XRes
                    FrameOut(y, x) = Color.FromArgb(0, 0, 0)
                    Overlay(y, x) = 0
                Next
            Next

            Dim Side As Integer = Xspan + 1

            For x = 0 To Xspan
                For y = 0 To Yspan
                    'xP = rP cos θP
                    'yP = rP sin θP

                    F = Getindex(AstroMap(y, x, Item))

                    If AstroData(F, Area) >= MinPix Then

                        If ((AstroList.Length - 1) - AstroData(F, RANK_N)) / StartGap < Frame And Counters(F, 3) <> Frame Then 'StartGap 0,25
                            Counters(F, 1) += 1
                            Counters(F, 2) = Var1 * (Counters(F, 1) / Var2) ^ 2
                            Counters(F, 3) = Frame
                        End If

                        'NW
                        If AstroData(F, X_AVG) <= Xspan / 2 And AstroData(F, Y_AVG) <= Yspan / 2 Then
                            Opposto = (Side / 2) - AstroData(F, X_AVG)
                            Adiacente = (Side / 2) - AstroData(F, Y_AVG)
                            Ipotenusa = Math.Sqrt(Adiacente ^ 2 + Opposto ^ 2)
                            Ipotenusa = Ipotenusa + Counters(F, 2) 'Frame
                            Yn = y - Math.Abs(Math.Round((Side / 2) - AstroData(F, Y_AVG) - (Ipotenusa * Math.Cos(Math.Atan(Opposto / Adiacente))), 0))
                            Xn = x - Math.Abs(Math.Round((Side / 2) - AstroData(F, X_AVG) - (Ipotenusa * Math.Sin(Math.Atan(Opposto / Adiacente))), 0))
                        End If

                        'SW
                        If AstroData(F, X_AVG) > Xspan / 2 And AstroData(F, Y_AVG) <= Yspan / 2 Then
                            Opposto = AstroData(F, X_AVG) - (Side / 2)
                            Adiacente = (Side / 2) - AstroData(F, Y_AVG)
                            Ipotenusa = Math.Sqrt(Adiacente ^ 2 + Opposto ^ 2)
                            Ipotenusa = Ipotenusa + Counters(F, 2) 'Frame
                            Yn = y - Math.Abs(Math.Round((Side / 2) - AstroData(F, Y_AVG) - (Ipotenusa * Math.Cos(Math.Atan(Opposto / Adiacente))), 0))
                            Xn = x + Math.Abs(Math.Round(AstroData(F, X_AVG) - (Side / 2) - (Ipotenusa * Math.Sin(Math.Atan(Opposto / Adiacente))), 0))
                        End If

                        'NE
                        If AstroData(F, X_AVG) <= Xspan / 2 And AstroData(F, Y_AVG) > Yspan / 2 Then
                            Opposto = (Side / 2) - AstroData(F, X_AVG)
                            Adiacente = AstroData(F, Y_AVG) - (Side / 2)
                            Ipotenusa = Math.Sqrt(Adiacente ^ 2 + Opposto ^ 2)
                            Ipotenusa = Ipotenusa + Counters(F, 2) 'Frame
                            Yn = y + Math.Abs(Math.Round(AstroData(F, Y_AVG) - (Side / 2) - (Ipotenusa * Math.Cos(Math.Atan(Opposto / Adiacente))), 0))
                            Xn = x - Math.Abs(Math.Round((Side / 2) - AstroData(F, X_AVG) - (Ipotenusa * Math.Sin(Math.Atan(Opposto / Adiacente))), 0))
                        End If

                        'SE
                        If AstroData(F, X_AVG) > Xspan / 2 And AstroData(F, Y_AVG) > Yspan / 2 Then
                            Opposto = AstroData(F, X_AVG) - (Side / 2)
                            Adiacente = AstroData(F, Y_AVG) - (Side / 2)
                            Ipotenusa = Math.Sqrt(Adiacente ^ 2 + Opposto ^ 2)
                            Ipotenusa = Ipotenusa + Counters(F, 2) 'Frame
                            Yn = y + Math.Abs(Math.Round(AstroData(F, Y_AVG) - (Side / 2) - (Ipotenusa * Math.Cos(Math.Atan(Opposto / Adiacente))), 0))
                            Xn = x + Math.Abs(Math.Round(AstroData(F, X_AVG) - (Side / 2) - (Ipotenusa * Math.Sin(Math.Atan(Opposto / Adiacente))), 0))
                        End If

                        xt = Math.Round(Xn + (Vres / 2) - (Side / 2), 0)
                        yt = Math.Round(Yn + (XRes / 2) - (Side / 2), 0)

                        If xt >= 0 And xt < Vres And yt >= 0 And yt < XRes Then
                            If Overlay(xt, yt) < AstroData(F, RANK_N) Then
                                FrameOut(xt, yt) = pixels(x, y)
                                Overlay(xt, yt) = AstroData(F, RANK_N)
                            End If
                        End If
                    End If
                Next
            Next

            Dim Xa As Integer
            Dim Ya As Integer
            For Xa = 0 To Vres - 1 '1079
                For Ya = 0 To XRes - 1 '1919
                    Dim blueOfs As Integer = (bmADataOUT.Stride * Xa) + (PixelSize * Ya)
                    Dim greenOfs As Integer = blueOfs + 1
                    Dim redOfs As Integer = greenOfs + 1
                    Marshal.WriteByte(bmADataOUT.Scan0, blueOfs, FrameOut(Xa, Ya).B)
                    Marshal.WriteByte(bmADataOUT.Scan0, greenOfs, FrameOut(Xa, Ya).G)
                    Marshal.WriteByte(bmADataOUT.Scan0, redOfs, FrameOut(Xa, Ya).R)
                Next
            Next
            bmA.UnlockBits(bmADataOUT)
            If WrtFrameNum = True Then
                Using gr As Graphics = Graphics.FromImage(bmA)
                    Dim Afnt As New Font("Arbutus Slab", 12, FontStyle.Regular)
                    Dim Abrush As Brush = Brushes.LightGreen
                    Dim p As New System.Drawing.Pen(Brushes.White, 1)
                    gr.DrawString("frame " & CStr(Frame), Afnt, Abrush, 25, 25)
                End Using
            End If

            bmA.Save(OutputPath & "\" & CStr(Frame) & "_ANIMAZIONE.png", System.Drawing.Imaging.ImageFormat.Png)
            HUDFAnimatorForm.PB2.Image = Image.FromFile(OutputPath & "\" & CStr(Frame) & "_ANIMAZIONE.png")
            HUDFAnimatorForm.Refresh()

        Next 'Frame


        HUDFAnimatorForm.ToolStripStatusLabel1.Text = "12 write base image" : HUDFAnimatorForm.Refresh()
        'Update the bitmap from the pixels array
        For x = 0 To bmData.Width - 1
            For y = 0 To bmData.Height - 1
                Dim blueOfs As Integer = (bmData.Stride * x) + (PixelSize * y)
                Dim greenOfs As Integer = blueOfs + 1
                Dim redOfs As Integer = greenOfs + 1
                Marshal.WriteByte(bmData.Scan0, blueOfs, pixels(x, y).B)
                Marshal.WriteByte(bmData.Scan0, greenOfs, pixels(x, y).G)
                Marshal.WriteByte(bmData.Scan0, redOfs, pixels(x, y).R)
            Next
        Next
        bm.UnlockBits(bmData)
        bm.Save(OutputPath & "\OUTPUT.TIF", System.Drawing.Imaging.ImageFormat.Tiff)

        If WrtLog = True Then Logger.Flog("Log.csv", LogCMD.Write, "END                     " & ";" & DateTime.Now)
        If WrtLog = True Then Logger.Flog("Log.csv", LogCMD.Close)

        HUDFAnimatorForm.ToolStripStatusLabel1.Text = "12 END" : HUDFAnimatorForm.Refresh()

    End Sub

    Public Function Getindex(Obj As Integer) As Integer
        Dim n As Integer
        For n = 0 To AstroList.Length - 1
            If AstroList(n) = Obj Then
                Exit For
            End If
        Next
        Return n
    End Function
End Module

