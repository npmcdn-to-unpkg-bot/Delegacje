using System.IO;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using CrazyAppsStudio.Delegacje.Domain.Entities;
using CrazyAppsStudio.Delegacje.Tasks;
using Tools;
using CrazyAppsStudio.Delegacje.Domain.Extensions;

namespace CrazyAppsStudio.Delegacje.App.Api
{
    [RoutePrefix("api/pdf")]
    public class PdfController : ApiController
    {
        private readonly ITasksRepository tasks;

        private int baseFontSize = 12;
        private Color color = new Color(110, 118, 143);
        private Color colorSummary = new Color(115, 219, 176);

        public PdfController(ITasksRepository tasks)
        {
            this.tasks = tasks;
        }


        [Route("{businessTripId:int}")]
        [HttpGet]
        [AllowAnonymous]
        public HttpResponseMessage Print(int businessTripId)
        {
            BusinessTrip trip = this.tasks.BusinessTripsTasks.GetBusinessTrip(businessTripId);

            Document document = new Document();
            document.Info.Author = "Saffron";
            Section section = document.AddSection();

            DefineStyles(document);
            AddHeader(document, section, trip);
            AddReportIdentifier(document, section, trip);
            AddDetails(document, section, trip);
            AddSummary(document, section, trip);

            PdfDocumentRenderer renderer = new PdfDocumentRenderer();
            renderer.Document = document;
            renderer.RenderDocument();

            using (MemoryStream ms = new MemoryStream())
            {
                renderer.Save(ms, false);

                byte[] res = ms.ToArray();
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new ByteArrayContent(res);
                response.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
                response.Content.Headers.ContentDisposition.FileName = "sample.pdf";
                return response;
            }
        }


        void DefineStyles(Document document)
        {
            // Get the predefined style Normal.
            Style style = document.Styles["Normal"];
            style.Font.Size = baseFontSize;
            style.Font.Color = color;
            style.Font.Name = "Source Sans Pro";

            //section header style
            style = document.Styles.AddStyle("ReportHeader", "Normal");
            style.Font.Size = baseFontSize + 5;
            style.Font.Underline = Underline.Single;
            style.ParagraphFormat.Alignment = ParagraphAlignment.Center;
            style.ParagraphFormat.SpaceAfter = "0cm";

            style = document.Styles.AddStyle("ReportHeaderDate", "Normal");
            style.Font.Size = baseFontSize - 2;
            style.ParagraphFormat.Alignment = ParagraphAlignment.Center;
            style.ParagraphFormat.SpaceAfter = "2cm";

            //section header style
            style = document.Styles.AddStyle("SectionHeader", "Normal");
            style.Font.Size = baseFontSize + 2;
            style.ParagraphFormat.SpaceAfter = "0.6cm";

            //table identifier
            style = document.Styles.AddStyle("TableIdentifier", "Normal");
            style.ParagraphFormat.LeftIndent = "1cm";

            //table style
            style = document.Styles.AddStyle("Table", "Normal");
            style.Font.Size = baseFontSize - 3;

            //summary style
            style = document.Styles.AddStyle("Summary", "Normal");
            style.Font.Size = baseFontSize * 3;
            style.Font.Color = colorSummary;
            style.ParagraphFormat.LeftIndent = "1cm";
        }

        void AddHeader(Document document, Section section, BusinessTrip trip)
        {
            section.AddParagraph("               " + trip.Title + "               ", "ReportHeader");
            section.AddParagraph(trip.Date.Date.ToAppString(), "ReportHeaderDate");
        }

        void AddReportIdentifier(Document document, Section section, BusinessTrip trip)
        {
            section.AddParagraph("1. Indentyfikator Raportu", "SectionHeader");

            // Create the item table
            Table table = section.AddTable();
            table.Style = "TableIdentifier";
            table.Borders.Width = 0;
            table.Rows.Height = Unit.FromPoint(document.Styles["Normal"].Font.Size * 2);

            Column columnHeader = table.AddColumn(Unit.FromCentimeter(5));
            Column columnValue = table.AddColumn(Unit.FromCentimeter(11));

            //Row row1 = table.AddRow();
            //row1.Cells[0].AddParagraph("NAZWA RAPORTU");
            //row1.Cells[1].AddParagraph(trip.);

            Row row2 = table.AddRow();
            row2.Cells[0].AddParagraph("POWÓD PODRÓŻY");
            row2.Cells[1].AddParagraph(trip.BusinessReason);

            Row row3 = table.AddRow();
            row3.Cells[0].AddParagraph("CEL PODÓŻY");
            row3.Cells[1].AddParagraph(trip.BusinessPurpose);

            Row row4 = table.AddRow();
            row4.Cells[0].AddParagraph("KOMENTARZ");
            row4.Cells[1].AddParagraph(trip.Notes);

            AddSectionEndingSpace(section);
        }

        void AddDetails(Document document, Section section, BusinessTrip trip)
        {
            section.AddParagraph("2. Szczegóły Raportu", "SectionHeader");

            AddExpenses(document, section, trip);
            AddMileages(document, section, trip);
            AddDietDays(document, section, trip);
        }

        void AddExpenses(Document document, Section section, BusinessTrip trip)
        {
            if (trip.Expenses.Count == 0)
                return;

            section.AddParagraph("WYDATKI", "SectionHeader");

            // Create the item table
            Table table = section.AddTable();
            table.Style = "Table";
            table.Borders.Width = 0;
            table.Borders.Color = color;
            //table.Rows.Height = Unit.FromPoint(document.Styles["Normal"].Font.Size * 2);

            Column columnSpaceLeft = table.AddColumn(Unit.FromCentimeter(0.25));
            Column columnDate = table.AddColumn(Unit.FromCentimeter(2));
            Column columnCategory = table.AddColumn(Unit.FromCentimeter(2));
            Column columnDestiny = table.AddColumn(Unit.FromCentimeter(2));
            Column columnAmount = table.AddColumn(Unit.FromCentimeter(2));
            Column columnDcoument = table.AddColumn(Unit.FromCentimeter(2));
            Column columnDoNotReturn = table.AddColumn(Unit.FromCentimeter(2));
            Column columnComment = table.AddColumn(Unit.FromCentimeter(3.5));
            Column columnSpaceRight = table.AddColumn(Unit.FromCentimeter(0.25));

            AddTopRow(table, 8);

            Row rowHeader = table.AddRow();
            rowHeader.Cells[0].AddParagraph("");
            rowHeader.Cells[0].Borders.Left.Width = 0.5;
            rowHeader.Cells[1].AddParagraph("DATA");
            rowHeader.Cells[2].AddParagraph("KATEGORIA");
            rowHeader.Cells[3].AddParagraph("DESTYNACJA");
            rowHeader.Cells[4].AddParagraph("KWOTA");
            rowHeader.Cells[5].AddParagraph("DOKUMENT");
            rowHeader.Cells[6].AddParagraph("NIE ZWRACAJ");
            rowHeader.Cells[7].AddParagraph("KOMENTARZ");
            rowHeader.Cells[8].AddParagraph("");
            rowHeader.Cells[8].Borders.Right.Width = 0.5;

            AddEmptyRow(table, 8);

            foreach (Expense e in trip.Expenses)
            {
                Row row = table.AddRow();
                row.Cells[0].AddParagraph("");
                row.Cells[1].AddParagraph(e.Date.ToAppString());
                row.Cells[2].AddParagraph(e.Type.Name);
                row.Cells[3].AddParagraph(e.Country.Name);
                row.Cells[4].AddParagraph(e.Amount + " PLN");
                row.Cells[5].AddParagraph(e.DocumentType.Name);
                row.Cells[6].AddParagraph(e.DoNotReturn ? "TAK" : "NIE");
                row.Cells[7].AddParagraph(e.Notes);
                row.Cells[8].AddParagraph("");

                AddRowBorders(row);
            }

            AddEmptyRow(table, 8);
            AddBottomRow(table, 8);

            AddSectionEndingSpace(section);
        }

        void AddMileages(Document document, Section section, BusinessTrip trip)
        {
            if (trip.MileageAllowances.Count == 0)
                return;

            section.AddParagraph("KILOMETRÓWKA", "SectionHeader");

            // Create the item table
            Table table = section.AddTable();
            table.Style = "Table";
            table.Borders.Width = 0;
            table.Borders.Color = color;
            //table.Rows.Height = Unit.FromPoint(document.Styles["Normal"].Font.Size * 2);

            Column columnSpaceLeft = table.AddColumn(Unit.FromCentimeter(0.25));
            Column columnDate = table.AddColumn(Unit.FromCentimeter(2));
            Column columnType = table.AddColumn(Unit.FromCentimeter(2));
            Column columnDistance = table.AddColumn(Unit.FromCentimeter(2));
            Column columnAmount = table.AddColumn(Unit.FromCentimeter(2));
            Column columnComment = table.AddColumn(Unit.FromCentimeter(7.5));
            Column columnSpaceRight = table.AddColumn(Unit.FromCentimeter(0.25));

            AddTopRow(table, 6);

            Row rowHeader = table.AddRow();
            rowHeader.Cells[0].AddParagraph("");
            rowHeader.Cells[0].Borders.Left.Width = 0.5;
            rowHeader.Cells[1].AddParagraph("DATA");
            rowHeader.Cells[2].AddParagraph("TYP POJAZDU");
            rowHeader.Cells[3].AddParagraph("DYSTANS");
            rowHeader.Cells[4].AddParagraph("KWOTA");
            rowHeader.Cells[5].AddParagraph("KOMENTARZ");
            rowHeader.Cells[6].AddParagraph("");
            rowHeader.Cells[6].Borders.Right.Width = 0.5;

            AddEmptyRow(table, 6);

            foreach (MileageAllowance e in trip.MileageAllowances)
            {
                Row row = table.AddRow();
                row.Cells[0].AddParagraph("");
                row.Cells[1].AddParagraph(e.Date.ToAppString());
                row.Cells[2].AddParagraph(e.Type.Name);
                row.Cells[3].AddParagraph(e.Distance.ToString());
                row.Cells[4].AddParagraph(e.Amount + " PLN");
                row.Cells[5].AddParagraph(e.Notes);
                row.Cells[6].AddParagraph("");

                AddRowBorders(row);
            }

            AddEmptyRow(table, 6);
            AddBottomRow(table, 6);

            AddSectionEndingSpace(section);
        }

        void AddDietDays(Document document, Section section, BusinessTrip trip)
        {
            if (trip.Subsistence == null || trip.Subsistence.Days.Count == 0)
                return;

            section.AddParagraph("DIETA", "SectionHeader");

            // Create the item table
            Table table = section.AddTable();
            table.Style = "Table";
            table.Borders.Width = 0;
            table.Borders.Color = color;
            //table.Rows.Height = Unit.FromPoint(document.Styles["Normal"].Font.Size * 2);

            Column columnSpaceLeft = table.AddColumn(Unit.FromCentimeter(0.25));
            Column columnDate = table.AddColumn(Unit.FromCentimeter(2));
            Column columnType = table.AddColumn(Unit.FromCentimeter(2));
            Column columnDistance = table.AddColumn(Unit.FromCentimeter(2));
            Column columnAmount = table.AddColumn(Unit.FromCentimeter(2));
            Column columnComment = table.AddColumn(Unit.FromCentimeter(7.5));
            Column columnSpaceRight = table.AddColumn(Unit.FromCentimeter(0.25));

            AddTopRow(table, 6);

            Row rowHeader = table.AddRow();
            rowHeader.Cells[0].AddParagraph("");
            rowHeader.Cells[0].Borders.Left.Width = 0.5;
            rowHeader.Cells[1].AddParagraph("DATA");
            rowHeader.Cells[2].AddParagraph("ŚNIADANIE");
            rowHeader.Cells[3].AddParagraph("OBIAD");
            rowHeader.Cells[4].AddParagraph("KOLACJA");
            rowHeader.Cells[5].AddParagraph("KWOTA");
            rowHeader.Cells[6].AddParagraph("");
            rowHeader.Cells[6].Borders.Right.Width = 0.5;

            AddEmptyRow(table, 6);

            foreach (SubsistenceDay e in trip.Subsistence.Days)
            {
                Row row = table.AddRow();
                row.Cells[0].AddParagraph("");
                row.Cells[1].AddParagraph(e.Date.ToAppString());
                row.Cells[2].AddParagraph(e.Breakfast ? "TAK" : "NIE");
                row.Cells[3].AddParagraph(e.Dinner ? "TAK" : "NIE");
                row.Cells[4].AddParagraph(e.Supper ? "TAK" : "NIE");
                row.Cells[5].AddParagraph(e.Amount.ToString() + " PLN");
                row.Cells[6].AddParagraph("");

                AddRowBorders(row);
            }

            AddEmptyRow(table, 6);
            AddBottomRow(table, 6);

            AddSectionEndingSpace(section);
        }

        void AddSummary(Document document, Section section, BusinessTrip trip)
        {
            section.AddParagraph("3. Podsumowanie", "SectionHeader");
            section.AddParagraph(trip.CountTotal() + " PLN", "Summary");
        }


        private static void AddEmptyRow(Table table, int columnsCount)
        {
            Row row = table.AddRow();
            row.Cells[0].AddParagraph("");
            row.Cells[0].Borders.Left.Width = 0.5;
            for (int i = 1; i <= columnsCount - 1; i++)
            {
                row.Cells[i].AddParagraph("");
            }
            row.Cells[columnsCount].AddParagraph("");
            row.Cells[columnsCount].Borders.Right.Width = 0.5;
        }
        private static void AddTopRow(Table table, int columnsCount)
        {
            Row row = table.AddRow();
            row.Cells[0].AddParagraph("");
            row.Cells[0].Borders.Left.Width = 0.5;
            row.Cells[0].Borders.Top.Width = 0.5;
            for (int i = 1; i <= columnsCount - 1; i++)
            {
                row.Cells[i].AddParagraph("");
                row.Cells[i].Borders.Top.Width = 0.5;
            }
            row.Cells[columnsCount].AddParagraph("");
            row.Cells[columnsCount].Borders.Right.Width = 0.5;
            row.Cells[columnsCount].Borders.Top.Width = 0.5;
        }
        private static void AddBottomRow(Table table, int columnsCount)
        {
            Row row = table.AddRow();
            row.Cells[0].AddParagraph("");
            row.Cells[0].Borders.Left.Width = 0.5;
            row.Cells[0].Borders.Bottom.Width = 0.5;
            for (int i = 1; i <= columnsCount - 1; i++)
            {
                row.Cells[i].AddParagraph("");
                row.Cells[i].Borders.Bottom.Width = 0.5;
            }
            row.Cells[columnsCount].AddParagraph("");
            row.Cells[columnsCount].Borders.Right.Width = 0.5;
            row.Cells[columnsCount].Borders.Bottom.Width = 0.5;
        }
        private static void AddSectionEndingSpace(Section section)
        {
            Paragraph p = section.AddParagraph();
            p.AddLineBreak();
            p.AddLineBreak();
            p.AddLineBreak();
        }
        private static void AddRowBorders(Row row)
        {
            row.Cells[0].Borders.Left.Width = 0.5;
            row.Cells[row.Cells.Count - 1].Borders.Right.Width = 0.5;
        }
    }
}
