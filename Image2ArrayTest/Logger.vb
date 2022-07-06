Module Logger
    Enum LogCMD
        NewFile
        Write
        Close
        Delete
        Init
    End Enum

    Dim file As System.IO.StreamWriter

    Public Sub Flog(Filename As String, Command As LogCMD, Optional Message As String = "")

        Select Case Command
            Case LogCMD.NewFile
                'file = My.Computer.FileSystem.OpenTextFileWriter(My.Computer.FileSystem.SpecialDirectories.Temp & "\" & Filename, True)
            Case LogCMD.Write
                'file = My.Computer.FileSystem.OpenTextFileWriter(My.Computer.FileSystem.SpecialDirectories.Temp & "\" & Filename, True)
                file = My.Computer.FileSystem.OpenTextFileWriter(OutputPath & "\" & Filename, True)
                file.WriteLine(Message)
                file.Close()
            Case LogCMD.Close
                'file.Close()
            Case LogCMD.Delete
                'If System.IO.File.Exists(My.Computer.FileSystem.SpecialDirectories.Temp & "\" & Filename) = True Then
                If System.IO.File.Exists(OutputPath & "\" & Filename) = True Then
                    My.Computer.FileSystem.DeleteFile(OutputPath & "\" & Filename, FileIO.UIOption.AllDialogs, FileIO.RecycleOption.SendToRecycleBin, FileIO.UICancelOption.DoNothing)
                End If
            Case LogCMD.Init
                If Not System.IO.Directory.Exists(OutputPath) Then
                    System.IO.Directory.CreateDirectory(OutputPath)
                End If
            Case Else
        End Select

    End Sub

End Module
