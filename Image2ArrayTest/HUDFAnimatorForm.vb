Public Class HUDFAnimatorForm
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If CB_Log.Checked Then WrtLog = True Else WrtLog = False
        If CB_Map.Checked Then WrtMap = True Else WrtMap = False
        If CB_AstroData.Checked Then WrtAstroData = True Else WrtAstroData = False
        If CB_ImageCatalog.Checked Then WrtCat = True Else WrtCat = False
        If CM_FrameNum.Checked Then WrtFrameNum = True Else WrtFrameNum = False
        If CB_NPix.SelectedItem.ToString() = "HD" Then Resolution = 0.5
        If CB_NPix.SelectedItem.ToString() = "Full HD" Then Resolution = 1
        If CB_NPix.SelectedItem.ToString() = "4K" Then Resolution = 2
        If CB_NPix.SelectedItem.ToString() = "8K" Then Resolution = 4
        MinPix = Convert.ToSingle(CB_NPix.SelectedItem.ToString())
        Frames = CInt(TB_Frames.Text)
        StartGap = CDbl(TB_StartGap.Value) / 100
        Var1 = CDbl(TB_VAR1.Value) / 100
        Var2 = CDbl(TB_VAR2.Value) / 100

        'Dim Source As String = "C:\Users\rober\OneDrive\Documenti\HubbleUltraDeepSpace\Easy_X.bmp"

        Dim Source As String = "C:\Users\rober\OneDrive\Documenti\HubbleUltraDeepSpace\CampioneHUDF.bmp"
        'Dim Source As String = "C:\Users\rober\OneDrive\Documenti\HubbleUltraDeepSpace\HUDS_POST_2000.bmp"
        'Dim Source As String = "C:\Users\rober\OneDrive\Documenti\HubbleUltraDeepSpace\HUDS_POST.bmp"
        'Dim Source As String = "C:\Users\rober\OneDrive\Documenti\HubbleUltraDeepSpace\HUDS_POST_1800_full.bmp"
        PB1.Image = Image.FromFile(Source)
        DoGraphics(Source)
        'PB2.Image = Image.FromFile("C:\Users\rober\OneDrive\Documenti\HubbleUltraDeepSpace\OUTPUT.TIF")

    End Sub



    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.CB_NPix.Text = "12" : MinPix = 12
        Me.CB_Resolution.Text = "Full HD" : Resolution = 1
        Me.ToolStripStatusLabel1.Text = "Ready to RUN"
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs)
        System.Diagnostics.Process.Start(e.Link.LinkData.ToString())
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim Proc As String = "Explorer.exe"
        Dim Args As String =
           ControlChars.Quote &
           IO.Path.Combine("C:\", My.Computer.FileSystem.SpecialDirectories.Temp & "\AstroAnimation") &
           ControlChars.Quote
        Process.Start(Proc, Args)
    End Sub

End Class
