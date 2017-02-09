Imports System.IO

Public Class Form1


    Private DefaultLanguage As String = Application.StartupPath & "\Language\Default.ini"

    Private ini As INIClass

    Public LangToolStrip As ToolStripMenuItem


    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
    Sub New()
        ini = New INIClass(DefaultLanguage)
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Dim LanguagesDir As New DirectoryInfo("Language")
        If LanguagesDir.Exists Then
            For Each lang As FileInfo In LanguagesDir.GetFiles("*.ini")
                LangToolStrip = New ToolStripMenuItem With {.Text = IO.Path.GetFileNameWithoutExtension(lang.FullName), _
                                                     .Image = Image.FromFile(Application.StartupPath & "\Language\LangIcons\" _
                                                                             & IO.Path.GetFileNameWithoutExtension(lang.FullName) & ".Png")}
                languageToolStripMenuItem.DropDownItems.Add(LangToolStrip)
                AddHandler LangToolStrip.Click, AddressOf LangToolStrip_Click
            Next
        End If

    End Sub
    Public Sub LangToolStrip_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim langName As String = DirectCast(sender, ToolStripMenuItem).Text
        Dim newLang As String = DefaultLanguage.Replace("Default", langName)
        ini = New INIClass(newLang)
        fileToolStripMenuItem.Text = GetValue("Menu", "File")
        newToolStripMenuItem.Text = GetValue("Menu", "New")
        openToolStripMenuItem.Text = GetValue("Menu", "Open")
        saveToolStripMenuItem.Text = GetValue("Menu", "Save")
        saveAsToolStripMenuItem.Text = GetValue("Menu", "Save As")
        exitToolStripMenuItem.Text = GetValue("Menu", "Exit")
        editToolStripMenuItem.Text = GetValue("Menu", "Edit")
        undoToolStripMenuItem.Text = GetValue("Menu", "Undo")
        redoToolStripMenuItem.Text = GetValue("Menu", "Redo")
        cutToolStripMenuItem.Text = GetValue("Menu", "Cut")
        copyToolStripMenuItem.Text = GetValue("Menu", "Copy")
        pasteToolStripMenuItem.Text = GetValue("Menu", "Paste")
        selectAllToolStripMenuItem.Text = GetValue("Menu", "Select All")
        formatToolStripMenuItem.Text = GetValue("Menu", "Format")
        fontToolStripMenuItem.Text = GetValue("Menu", "Font")
        helpToolStripMenuItem.Text = GetValue("Menu", "Help")
        languageToolStripMenuItem.Text = GetValue("Menu", "Language")
        aboutToolStripMenuItem.Text = GetValue("Menu", "About")

        toolStripMenuItem1.Text = GetValue("Context", "Cut")
        toolStripMenuItem2.Text = GetValue("Context", "Copy")
        toolStripMenuItem3.Text = GetValue("Context", "Paste")
    End Sub

    Public Function GetValue(ByVal section As String, ByVal key As String)
        Return ini.ReadValueBetween(section, key)
    End Function

    Private Sub newToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles newToolStripMenuItem.Click
        richTextBox1.Clear()
    End Sub

    Private Sub openToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles openToolStripMenuItem.Click
        Using ofd As New OpenFileDialog() With {.Filter = "Text File (*.txt)|*.txt"}
            If ofd.ShowDialog() = Windows.Forms.DialogResult.OK Then
                richTextBox1.Text = IO.File.ReadAllText(ofd.FileName)
            End If
        End Using
    End Sub

    Private Sub saveToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles saveToolStripMenuItem.Click
        Using sfd As New SaveFileDialog() With {.Filter = "Text File (*.txt)|*.txt"}
            If sfd.ShowDialog() = Windows.Forms.DialogResult.OK Then
                IO.File.WriteAllText(sfd.FileName, richTextBox1.Text)
            End If
        End Using
    End Sub

    Private Sub saveAsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles saveAsToolStripMenuItem.Click
        Using sfd As New SaveFileDialog() With {.Filter = "Text File (*.txt)|*.txt"}
            If sfd.ShowDialog() = Windows.Forms.DialogResult.OK Then
                IO.File.WriteAllText(sfd.FileName, richTextBox1.Text)
            End If
        End Using
    End Sub

    Private Sub exitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles exitToolStripMenuItem.Click
        Application.Exit()
    End Sub

    Private Sub undoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles undoToolStripMenuItem.Click
        If richTextBox1.CanUndo Then
            richTextBox1.Undo()
        End If
    End Sub

    Private Sub redoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles redoToolStripMenuItem.Click
        If richTextBox1.CanRedo Then
            richTextBox1.Redo()
        End If
    End Sub

    Private Sub cutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles cutToolStripMenuItem.Click
        If richTextBox1.SelectedText <> "" Then
            Clipboard.SetText(richTextBox1.SelectedText)
            richTextBox1.SelectedText = ""
        Else
            Clipboard.SetText(richTextBox1.Text)
            richTextBox1.SelectedText = ""
        End If
    End Sub

    Private Sub copyToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles copyToolStripMenuItem.Click
        If richTextBox1.SelectedText <> "" Then
            Clipboard.SetText(richTextBox1.SelectedText)
        Else
            Clipboard.SetText(richTextBox1.Text)
        End If
    End Sub

    Private Sub pasteToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles pasteToolStripMenuItem.Click
        If richTextBox1.SelectedText <> "" Then
            richTextBox1.SelectedText = Clipboard.GetText()
        Else
            richTextBox1.Text = Clipboard.GetText()
        End If
    End Sub

    Private Sub selectAllToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles selectAllToolStripMenuItem.Click
        If richTextBox1.CanSelect Then
            richTextBox1.SelectAll()
        End If
    End Sub

    Private Sub fontToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles fontToolStripMenuItem.Click
        Using fd As New FontDialog() With {.ShowHelp = True}
            If fd.ShowDialog() = Windows.Forms.DialogResult.OK Then
                If richTextBox1.SelectedText <> "" Then
                    richTextBox1.SelectionFont = fd.Font
                Else
                    If richTextBox1.Text <> "" Then
                        richTextBox1.SelectAll()
                        richTextBox1.Text = Clipboard.GetText()
                    End If

                End If
            End If
        End Using
    End Sub

    Private Sub aboutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles aboutToolStripMenuItem.Click
        Dim Ab As New AboutBox1
        Ab.Show()
    End Sub

    Private Sub richTextBox1_MouseUp(sender As Object, e As MouseEventArgs) Handles richTextBox1.MouseUp
        If e.Button = System.Windows.Forms.MouseButtons.Right Then
            contextMenuStrip1.Show(richTextBox1, New Point(e.X, e.Y))
        End If
    End Sub

    Private Sub toolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles toolStripMenuItem1.Click
        If richTextBox1.SelectedText <> "" Then
            Clipboard.SetText(richTextBox1.SelectedText)
            richTextBox1.SelectedText = ""
        Else
            Clipboard.SetText(richTextBox1.Text)
            richTextBox1.SelectedText = ""
        End If
    End Sub

    Private Sub toolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles toolStripMenuItem2.Click
        If richTextBox1.SelectedText <> "" Then
            Clipboard.SetText(richTextBox1.SelectedText)
        Else
            Clipboard.SetText(richTextBox1.Text)
        End If
    End Sub

    Private Sub toolStripMenuItem3_Click(sender As Object, e As EventArgs) Handles toolStripMenuItem3.Click
        If richTextBox1.SelectedText <> "" Then
            richTextBox1.SelectedText = Clipboard.GetText()
        Else
            richTextBox1.Text = Clipboard.GetText()
        End If
    End Sub

End Class
