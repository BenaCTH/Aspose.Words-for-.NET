' For complete examples and data files, please go to https://github.com/asposewords/Aspose_Words_NET
' The path to the documents directory.
Dim dataDir As String = RunExamples.GetDataDir_WorkingWithTables()
Dim fileName As String = "TestFile.doc"
' Open the document
Dim doc As New Document(dataDir & fileName)

Dim table As Table = CType(doc.GetChild(NodeType.Table, 0, True), Table)

' Auto fit the table to the cell contents
table.AutoFit(AutoFitBehavior.AutoFitToContents)

dataDir = dataDir & RunExamples.GetOutputFilePath(fileName)
' Save the document to disk.
doc.Save(dataDir)

Debug.Assert(doc.FirstSection.Body.Tables(0).PreferredWidth.Type = PreferredWidthType.Auto, "PreferredWidth type is not auto")
Debug.Assert(doc.FirstSection.Body.Tables(0).FirstRow.FirstCell.CellFormat.PreferredWidth.Type = PreferredWidthType.Auto, "PrefferedWidth on cell is not auto")
Debug.Assert(doc.FirstSection.Body.Tables(0).FirstRow.FirstCell.CellFormat.PreferredWidth.Value = 0, "PreferredWidth value is not 0")