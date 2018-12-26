using DinkToPdf;

namespace HtmlToPdf
{
    class Program
    {
        static void Main(string[] args)
        {
            // A4 em pixels ==> 595 x 842 (altura x largura)

            string path = @"C:\Users\paulo.gomes\Desktop\HtmlToPdf\TemplateFatura\{0}";
            var html = @"Template_Fatura.html";
            var css = @"styles.css";
            //var faturaId = 12345678;

            string pdfText = System.IO.File.ReadAllText(string.Format(path, html));

            var doc = new HtmlToPdfDocument()
            {
                GlobalSettings = {
                    ColorMode = ColorMode.Color,
                    Orientation = Orientation.Portrait,
                    PaperSize = PaperKind.A4Plus,
                },
                Objects = {
                    new ObjectSettings() {
                        PagesCount = true,
                        HtmlContent = pdfText,
                        WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet = string.Format(path, css) }
                    }
                }
            };

            var pdf = new BasicConverter(new PdfTools()).Convert(doc);

            using (System.IO.FileStream stream = new System.IO.FileStream(string.Format(path, $"output.pdf"), System.IO.FileMode.Create))
            {
                stream.Write(pdf, 0, pdf.Length);
            }
        }
    }
}
