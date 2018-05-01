using PdfFileWriter;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows;

namespace Silent_Update.Utilities
{
    public class SavePDF
    {
        private string FilePath;
        private PdfDocument Document;
        private PdfPage Page;
        private PdfContents Contents;
        private PdfFont NormalFont;
        private PdfFont TableTitleFont;

        public SavePDF(string filePath)
        {
            this.FilePath = filePath;
            // Step 1: Create one document object PdfDocument.
            Document = new PdfDocument(PaperType.Letter, false, UnitOfMeasure.Inch, this.FilePath);
            Document.Debug = false;
            // define font resource
            NormalFont = PdfFont.CreatePdfFont(Document, "Arial", System.Drawing.FontStyle.Regular, true);
            TableTitleFont = PdfFont.CreatePdfFont(Document, "Times New Roman", System.Drawing.FontStyle.Bold, true);
        }

        public void CreateFile(List<string> Data)
        {
            // Add new page
            Page = new PdfPage(Document);

            // Add contents to page
            Contents = new PdfContents(Page);
            PdfFont TitleFont = PdfFont.CreatePdfFont(Document, "Verdana", System.Drawing.FontStyle.Bold);
            PdfFont AuthorFont = PdfFont.CreatePdfFont(Document, "Verdana", System.Drawing.FontStyle.Italic);

            // create table
            PdfTable ClientList = new PdfTable(Page, Contents, NormalFont, 9.0);

            // divide columns width in proportion to following values
            ClientList.SetColumnWidth(6.0, 3.5, 5.0, 5.0, 5.0);

            // event handlers
            ClientList.TableStartEvent += BookListTableStart;
            ClientList.TableEndEvent += BookListTableEnd;

            // set display header at the top of each additional page
            ClientList.HeaderOnEachPage = true;

            // headers
            ClientList.Header[0].Value = Application.Current.FindResource("NameHeaderLbl").ToString();
            ClientList.Header[1].Value = Application.Current.FindResource("PDFAction").ToString();
            ClientList.Header[2].Value = Application.Current.FindResource("PDFResult").ToString();
            ClientList.Header[3].Value = Application.Current.FindResource("PDFStart").ToString();
            ClientList.Header[4].Value = Application.Current.FindResource("PDFEnd").ToString();

            // make some changes to default header style
            ClientList.DefaultHeaderStyle.Alignment = ContentAlignment.MiddleCenter;
            ClientList.DefaultHeaderStyle.MultiLineText = true;
            ClientList.DefaultHeaderStyle.TextBoxTextJustify = TextBoxJustify.Center;

            for (int i = 0; i < 5; i++)
            {
                ClientList.Cell[i].Style = ClientList.CellStyle;
                ClientList.Cell[i].Style.MultiLineText = true;
                ClientList.Cell[i].Style.TextBoxPageBreakLines = 4;
            }
            // default cell style
            ClientList.DefaultCellStyle.Alignment = ContentAlignment.MiddleLeft;

            int DataLength = Data.Count;
            if (DataLength > 0)
            {
                for (int i = 0; i < DataLength; i++)
                {
                    string[] ClientData = Data.ElementAt(i).Split(':');
                    ClientList.Cell[0].Value = ClientData[1];
                    ClientList.Cell[1].Value = ClientData[0];
                    ClientList.Cell[2].Value = ClientData[2];
                    ClientList.Cell[3].Value = ClientData[3];
                    ClientList.Cell[4].Value = ClientData[4];
                    // draw it
                    ClientList.DrawRow();
                }
            }
            // close book list
            ClientList.Close();

            // argument: PDF file name
            Document.CreateFile();

            // start default PDF reader and display the file
            Process Proc = new Process();
            Proc.StartInfo = new ProcessStartInfo(this.FilePath);
            Proc.Start();
            // exit            
        }

        private void BookListTableStart(PdfTable BookList, Double TableStartPos)
        {
            Double PosX = 0.5 * (BookList.TableArea.Left + BookList.TableArea.Right);
            Double PosY = TableStartPos + TableTitleFont.Descent(16.0) + 0.05;
            BookList.Contents.DrawText(TableTitleFont, 16.0, PosX, PosY, TextJustify.Center, DrawStyle.Normal, Color.Chocolate, Application.Current.FindResource("PDFTitle").ToString());
            return;
        }

        private void BookListTableEnd(PdfTable BookList, Double TableEndPos)
        {
            Double PosX = BookList.TableArea.Left;
            Double PosY = TableEndPos - TableTitleFont.Ascent(12.0) - 0.05;
            BookList.Contents.DrawText(TableTitleFont, 12.0, PosX, PosY, TextJustify.Left, DrawStyle.Normal, Color.Chocolate, Application.Current.FindResource("PDFCopyRight").ToString());
            return;
        }
    }
}
