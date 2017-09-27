using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using WcfService.Persistence;

namespace WcfService.PDF
{
    public class PdfPrinter
    {

        // ------------------------------
        // Class fields
        // ------------------------------

        private static readonly BaseFont font = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, false);


        // ------------------------------
        // Instance fields
        // ------------------------------

        private readonly Profile profile;

        private PdfContentByte _pcb;

        // ------------------------------
        // Constructors
        // ------------------------------

        public PdfPrinter(Profile profile)
        {
            this.profile = profile;
        }

        // ------------------------------
        // Instance methods
        // ------------------------------

        public void Create(Stream output)
        {
            Document document = new Document();
            PdfReader reader = null;

            try
            {
                PdfWriter writer = PdfWriter.GetInstance(document, output);
                document.Open();

                // Load the background image and add it to the document structure
                Stream wantedPDF = Assembly.GetExecutingAssembly().GetManifestResourceStream("WcfService.PDF.wanted.pdf");
                reader = new PdfReader(wantedPDF);
                PdfTemplate background = writer.GetImportedPage(reader, 1);

                // Create a page in the document and add it to the bottom layer
                document.NewPage();
                _pcb = writer.DirectContentUnder;
                _pcb.AddTemplate(background, 0, 0);

                // Get the top layer and write some text
                _pcb = writer.DirectContent;
                _pcb.BeginText();

                int x = 100, y = 300;

                SetFont18();
                PrintText("NAME: "+profile.FirstName.TrimEnd()+" "+profile.LastName.TrimEnd(), x, y);
                y -= 30;
                PrintText("BIRTH: " + profile.Birthday.Value.Date.ToShortDateString(), x, y);
                y -= 30;
                PrintText("GENDER: " + (profile.Gender=='F'?"Female":"Male"), x, y);
                y -= 30;
                PrintText("HEIGHT: " + profile.Height, x, y);
                y -= 30;
                PrintText("WEIGHT: " + profile.Weight, x, y);
                y -= 30;
                PrintText("NATIONALITY: " + profile.Nationality, x, y);

                _pcb.EndText();

                writer.Flush();
            }
            finally
            {
                /*if (readerBicycle != null )
                {
                    readerBicycle.Close();
                }*/
                document.Close();
            }
        }

        private void SetFont7()
        {
            _pcb.SetFontAndSize(font, 7);
        }

        private void SetFont18()
        {
            _pcb.SetFontAndSize(font, 18);
        }

        private void SetFont36()
        {
            _pcb.SetFontAndSize(font, 36);
        }

        private void PrintText(string text, int x, int y)
        {
            _pcb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, text, x, y, 0);
        }

        private void PrintTextCentered(string text, int x, int y)
        {
            _pcb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, text, x, y, 0);
        }

        private void PrintXAxis(int y)
        {
            SetFont7();
            int x = 600;
            while (x >= 0)
            {
                if (x % 20 == 0)
                {
                    PrintTextCentered("" + x, x, y + 8);
                    PrintTextCentered("|", x, y);
                }
                else
                {
                    PrintTextCentered(".", x, y);
                }
                x -= 5;
            }
        }

        private void PrintYAxis(int x)
        {
            SetFont7();
            int y = 800;
            while (y >= 0)
            {
                if (y % 20 == 0)
                {
                    PrintText("__ " + y, x, y);
                }
                else
                {
                    PrintText("_", x, y);
                }
                y = y - 5;
            }
        }
    }
}