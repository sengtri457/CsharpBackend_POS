using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group1_POS.models
{
    internal class PrintReport
    {
        public int m_currentPageIndex;
        public IList<Stream> m_streams;

        public void Print()
        {
            //const string printerName ="HP LaserJet 1020";
            if (m_streams == null || m_streams.Count == 0)
                return;
            PrintDocument printDoc = new PrintDocument();
            printDoc.PrinterSettings.PrintRange = PrintRange.AllPages;
            printDoc.PrinterSettings.PrintRange = PrintRange.SomePages;
            //printDoc.PrinterSettings.FromPage = 1;
            //printDoc.PrinterSettings.ToPage = 1;
            //printDoc.PrinterSettings.PrinterName = printerName;
            if (!printDoc.PrinterSettings.IsValid)
            {
                //string msg = String.Format("Can't find printer \"{0}\".",
                //printerName);
                //MessageBox.Show(msg, "Print Error");
                return;
            }
            printDoc.PrintPage += new PrintPageEventHandler(PrintPage);
            printDoc.Print();
        }


        private void PrintPage(object sender, PrintPageEventArgs ev)
        {
            Metafile pageImage = new Metafile(m_streams[m_currentPageIndex]);
            ev.Graphics.DrawImage(pageImage, ev.PageBounds);
            m_currentPageIndex++;
            ev.HasMorePages = (m_currentPageIndex < m_streams.Count);
        }

        private Stream CreateStream(string name, string fileNameExtension, Encoding
                                                                     encoding, string mimeType, bool willSeek)
        {
            Stream stream = new MemoryStream();
            m_streams.Add(stream);
            return stream;
        }

        public void Export(LocalReport report)
        {
            string deviceInfo =

            "<DeviceInfo>" +
            " <OutputFormat>EMF</OutputFormat>" +
            " <PageWidth>7.2cm</PageWidth>" +
            " <PageHeight>20cm</PageHeight>" +
            " <MarginTop>1cm</MarginTop>" +
            " <MarginLeft>0.5cm</MarginLeft>" +
            " <MarginRight>0.5cm</MarginRight>" +
            " <MarginBottom>1cm</MarginBottom>" +
            "</DeviceInfo>";
            Warning[] warnings;
            m_streams = new List<Stream>();
            report.Render("Image", deviceInfo, CreateStream, out warnings);
            foreach (Stream stream in m_streams)
                stream.Position = 0;
        }

    }
}
