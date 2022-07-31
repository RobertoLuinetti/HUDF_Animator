<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class HUDFAnimatorForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.PB1 = New System.Windows.Forms.PictureBox()
        Me.PB2 = New System.Windows.Forms.PictureBox()
        Me.Label76 = New System.Windows.Forms.Label()
        Me.TB_Frames = New System.Windows.Forms.TextBox()
        Me.CB_Resolution = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.CB_Log = New System.Windows.Forms.CheckBox()
        Me.CB_Map = New System.Windows.Forms.CheckBox()
        Me.CB_ImageCatalog = New System.Windows.Forms.CheckBox()
        Me.CB_NPix = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.CM_FrameNum = New System.Windows.Forms.CheckBox()
        Me.CB_AstroData = New System.Windows.Forms.CheckBox()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.ToolStripProgressBar1 = New System.Windows.Forms.ToolStripProgressBar()
        Me.ToolStripStatusLabel1 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.TB_StartGap = New System.Windows.Forms.TrackBar()
        Me.TB_VAR1 = New System.Windows.Forms.TrackBar()
        Me.TB_VAR2 = New System.Windows.Forms.TrackBar()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Button2 = New System.Windows.Forms.Button()
        CType(Me.PB1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PB2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.StatusStrip1.SuspendLayout()
        CType(Me.TB_StartGap, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TB_VAR1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TB_VAR2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(12, 491)
        Me.Button1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(172, 29)
        Me.Button1.TabIndex = 0
        Me.Button1.Text = "RUN "
        Me.Button1.UseVisualStyleBackColor = True
        '
        'PB1
        '
        Me.PB1.Location = New System.Drawing.Point(276, 72)
        Me.PB1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.PB1.Name = "PB1"
        Me.PB1.Size = New System.Drawing.Size(571, 988)
        Me.PB1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PB1.TabIndex = 1
        Me.PB1.TabStop = False
        '
        'PB2
        '
        Me.PB2.Location = New System.Drawing.Point(872, 71)
        Me.PB2.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.PB2.Name = "PB2"
        Me.PB2.Size = New System.Drawing.Size(901, 988)
        Me.PB2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PB2.TabIndex = 2
        Me.PB2.TabStop = False
        '
        'Label76
        '
        Me.Label76.AutoSize = True
        Me.Label76.Location = New System.Drawing.Point(5, 421)
        Me.Label76.Name = "Label76"
        Me.Label76.Size = New System.Drawing.Size(130, 20)
        Me.Label76.TabIndex = 12
        Me.Label76.Text = "Number of frames"
        '
        'TB_Frames
        '
        Me.TB_Frames.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TB_Frames.Location = New System.Drawing.Point(12, 444)
        Me.TB_Frames.Name = "TB_Frames"
        Me.TB_Frames.Size = New System.Drawing.Size(125, 27)
        Me.TB_Frames.TabIndex = 11
        Me.TB_Frames.Text = "3000"
        Me.TB_Frames.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'CB_Resolution
        '
        Me.CB_Resolution.FormattingEnabled = True
        Me.CB_Resolution.Items.AddRange(New Object() {"HD", "Full HD", "4K", "8K"})
        Me.CB_Resolution.Location = New System.Drawing.Point(12, 389)
        Me.CB_Resolution.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.CB_Resolution.Name = "CB_Resolution"
        Me.CB_Resolution.Size = New System.Drawing.Size(125, 28)
        Me.CB_Resolution.TabIndex = 13
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(9, 365)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(125, 20)
        Me.Label2.TabIndex = 14
        Me.Label2.Text = "Output resolution"
        '
        'CB_Log
        '
        Me.CB_Log.AutoSize = True
        Me.CB_Log.Checked = True
        Me.CB_Log.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CB_Log.Location = New System.Drawing.Point(7, 28)
        Me.CB_Log.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.CB_Log.Name = "CB_Log"
        Me.CB_Log.Size = New System.Drawing.Size(56, 24)
        Me.CB_Log.TabIndex = 0
        Me.CB_Log.Text = "Log"
        Me.CB_Log.UseVisualStyleBackColor = True
        '
        'CB_Map
        '
        Me.CB_Map.AutoSize = True
        Me.CB_Map.Location = New System.Drawing.Point(7, 60)
        Me.CB_Map.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.CB_Map.Name = "CB_Map"
        Me.CB_Map.Size = New System.Drawing.Size(100, 24)
        Me.CB_Map.TabIndex = 1
        Me.CB_Map.Text = "Astro map"
        Me.CB_Map.UseVisualStyleBackColor = True
        '
        'CB_ImageCatalog
        '
        Me.CB_ImageCatalog.AutoSize = True
        Me.CB_ImageCatalog.Location = New System.Drawing.Point(7, 124)
        Me.CB_ImageCatalog.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.CB_ImageCatalog.Name = "CB_ImageCatalog"
        Me.CB_ImageCatalog.Size = New System.Drawing.Size(129, 24)
        Me.CB_ImageCatalog.TabIndex = 2
        Me.CB_ImageCatalog.Text = "Image Catalog"
        Me.CB_ImageCatalog.UseVisualStyleBackColor = True
        '
        'CB_NPix
        '
        Me.CB_NPix.FormattingEnabled = True
        Me.CB_NPix.Items.AddRange(New Object() {"1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31", "32", "33", "34", "35", "36", "37", "38", "39", "40", "41", "42", "43", "44", "45", "46", "47", "48", "49", "50"})
        Me.CB_NPix.Location = New System.Drawing.Point(11, 333)
        Me.CB_NPix.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.CB_NPix.Name = "CB_NPix"
        Me.CB_NPix.Size = New System.Drawing.Size(54, 28)
        Me.CB_NPix.TabIndex = 9
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 309)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(133, 20)
        Me.Label1.TabIndex = 10
        Me.Label1.Text = "MIN pix per object"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.CM_FrameNum)
        Me.GroupBox1.Controls.Add(Me.CB_AstroData)
        Me.GroupBox1.Controls.Add(Me.CB_ImageCatalog)
        Me.GroupBox1.Controls.Add(Me.CB_Map)
        Me.GroupBox1.Controls.Add(Me.CB_Log)
        Me.GroupBox1.Location = New System.Drawing.Point(14, 72)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupBox1.Size = New System.Drawing.Size(173, 219)
        Me.GroupBox1.TabIndex = 4
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Debug Option"
        '
        'CM_FrameNum
        '
        Me.CM_FrameNum.AutoSize = True
        Me.CM_FrameNum.Location = New System.Drawing.Point(7, 156)
        Me.CM_FrameNum.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.CM_FrameNum.Name = "CM_FrameNum"
        Me.CM_FrameNum.Size = New System.Drawing.Size(127, 24)
        Me.CM_FrameNum.TabIndex = 4
        Me.CM_FrameNum.Text = "Frame number"
        Me.CM_FrameNum.UseVisualStyleBackColor = True
        '
        'CB_AstroData
        '
        Me.CB_AstroData.AutoSize = True
        Me.CB_AstroData.Location = New System.Drawing.Point(7, 92)
        Me.CB_AstroData.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.CB_AstroData.Name = "CB_AstroData"
        Me.CB_AstroData.Size = New System.Drawing.Size(100, 24)
        Me.CB_AstroData.TabIndex = 3
        Me.CB_AstroData.Text = "Astro data"
        Me.CB_AstroData.UseVisualStyleBackColor = True
        '
        'StatusStrip1
        '
        Me.StatusStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripProgressBar1, Me.ToolStripStatusLabel1})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 1139)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(1782, 26)
        Me.StatusStrip1.TabIndex = 15
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'ToolStripProgressBar1
        '
        Me.ToolStripProgressBar1.Name = "ToolStripProgressBar1"
        Me.ToolStripProgressBar1.Size = New System.Drawing.Size(100, 18)
        '
        'ToolStripStatusLabel1
        '
        Me.ToolStripStatusLabel1.Name = "ToolStripStatusLabel1"
        Me.ToolStripStatusLabel1.Size = New System.Drawing.Size(153, 20)
        Me.ToolStripStatusLabel1.Text = "ToolStripStatusLabel1"
        '
        'TB_StartGap
        '
        Me.TB_StartGap.Location = New System.Drawing.Point(12, 609)
        Me.TB_StartGap.Maximum = 100
        Me.TB_StartGap.Minimum = 1
        Me.TB_StartGap.Name = "TB_StartGap"
        Me.TB_StartGap.Size = New System.Drawing.Size(204, 56)
        Me.TB_StartGap.TabIndex = 16
        Me.TB_StartGap.TickFrequency = 5
        Me.TB_StartGap.Value = 50
        '
        'TB_VAR1
        '
        Me.TB_VAR1.Location = New System.Drawing.Point(12, 691)
        Me.TB_VAR1.Maximum = 100
        Me.TB_VAR1.Name = "TB_VAR1"
        Me.TB_VAR1.Size = New System.Drawing.Size(204, 56)
        Me.TB_VAR1.TabIndex = 17
        Me.TB_VAR1.TickFrequency = 5
        Me.TB_VAR1.Value = 75
        '
        'TB_VAR2
        '
        Me.TB_VAR2.Location = New System.Drawing.Point(12, 773)
        Me.TB_VAR2.Maximum = 500
        Me.TB_VAR2.Minimum = 1
        Me.TB_VAR2.Name = "TB_VAR2"
        Me.TB_VAR2.Size = New System.Drawing.Size(204, 56)
        Me.TB_VAR2.TabIndex = 18
        Me.TB_VAR2.TickFrequency = 5
        Me.TB_VAR2.Value = 450
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(34, 586)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(150, 20)
        Me.Label3.TabIndex = 19
        Me.Label3.Text = "GAP between objects"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(34, 668)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(96, 20)
        Me.Label4.TabIndex = 20
        Me.Label4.Text = "Accelleration"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(34, 750)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(125, 20)
        Me.Label5.TabIndex = 21
        Me.Label5.Text = "Images per swipe"
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(12, 538)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(172, 29)
        Me.Button2.TabIndex = 23
        Me.Button2.Text = "Open Output DIR"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'HUDFAnimatorForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1782, 1165)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.TB_VAR2)
        Me.Controls.Add(Me.TB_VAR1)
        Me.Controls.Add(Me.TB_StartGap)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.CB_NPix)
        Me.Controls.Add(Me.Label76)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TB_Frames)
        Me.Controls.Add(Me.CB_Resolution)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.PB2)
        Me.Controls.Add(Me.PB1)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "HUDFAnimatorForm"
        Me.Text = "Form1"
        CType(Me.PB1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PB2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        CType(Me.TB_StartGap, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TB_VAR1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TB_VAR2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Button1 As Button
    Friend WithEvents PB1 As PictureBox
    Friend WithEvents PB2 As PictureBox
    Friend WithEvents TB_Frames As TextBox
    Friend WithEvents Label76 As Label
    Friend WithEvents CB_Resolution As ComboBox
    Friend WithEvents Label2 As Label
    Friend WithEvents CB_Log As CheckBox
    Friend WithEvents CB_Map As CheckBox
    Friend WithEvents CB_ImageCatalog As CheckBox
    Friend WithEvents CB_NPix As ComboBox
    Friend WithEvents Label1 As Label
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents CB_AstroData As CheckBox
    Friend WithEvents StatusStrip1 As StatusStrip
    Friend WithEvents ToolStripStatusLabel1 As ToolStripStatusLabel
    Friend WithEvents ToolStripProgressBar1 As ToolStripProgressBar
    Friend WithEvents TB_StartGap As TrackBar
    Friend WithEvents TB_VAR1 As TrackBar
    Friend WithEvents TB_VAR2 As TrackBar
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Button2 As Button
    Friend WithEvents CM_FrameNum As CheckBox
End Class
