using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

using WebApplicationTI.Models;
using System.IO;
using ClosedXML.Excel;

using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.Kernel.Geom;
using iText.Kernel.Font;
using iText.IO.Font.Constants;
using iText.Kernel.Colors;
using iText.Layout.Borders;
using iText.IO.Image;
using iText.Kernel.Events;
using iText.Kernel.Pdf.Canvas;

using PagedList;

namespace WebApplicationTI.Controllers
{
    public class UsuariosController : Controller
    {
        private bdTIEntities db = new bdTIEntities();

        // GET: Usuarios
        public ActionResult Index(int? page, string search)
        {
            int pageSize = 5;
            int pageNumber = (page ?? 1);

            List<Usuario> listUsers = db.Usuario.ToList();
            var usuario = db.Usuario.Include(u => u.Depto);

            if (!String.IsNullOrEmpty(search))
            {
                return View(db.Usuario.Where(x => x.Nombre.Contains(search) || x.Nombre.Contains(null)).ToList().ToPagedList(pageNumber , pageSize));
            }
            else
            {
                return View(listUsers.ToList().ToPagedList(pageNumber, pageSize));
            }
        }

        [HttpGet]
        public PartialViewResult Create()
        {
            ViewBag.IdDepto = new SelectList(db.Depto, "IdDepto", "Departamento");
            return PartialView("Create");
        }
        [HttpPost]
        public ActionResult Create(Usuario model)
        {
            if (model != null)
            {
                db.Usuario.Add(model);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public PartialViewResult Edit(int? idUser)
        {
            var model = new Usuario();
            
            model = (from item in db.Usuario
                              where item.IdUsuario == idUser
                              select item).FirstOrDefault();
            ViewBag.IdDepto = new SelectList(db.Depto, "IdDepto", "Departamento", model.IdDepto);

            return PartialView("Edit", model);
        }

        [HttpPost]
        public ActionResult Edit(Usuario model)
        {
            var record = db.Usuario.Where(x => x.IdUsuario == model.IdUsuario);
            if (record != null)
            {
                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public PartialViewResult Delete(int? idUser)
        {
            var model = new Usuario();
            model = (from item in db.Usuario
                              where item.IdUsuario == idUser
                              select item).FirstOrDefault();

            return PartialView("Delete", model);
        }

        [HttpPost]
        public ActionResult Delete(Usuario model)
        {
            if (model != null)
            {
                var obj = db.Usuario.Find(model.IdUsuario);
                db.Usuario.Remove(obj);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public FileResult ExportarExcel()
        {
            XLWorkbook wb = new XLWorkbook();
            DataTable table = new DataTable("Usuarios");
            table.Columns.AddRange(new DataColumn[4] { new DataColumn("Nombre"), new DataColumn("Departamento"), new DataColumn("Correo"), new DataColumn("Extensión"), });
            var users = from usuario in db.Usuario select usuario;
            foreach (var user in users)
            {
                table.Rows.Add(user.Nombre, user.Depto.Departamento, user.Correo, user.Ext);
            }

            wb.Worksheets.Add(table);
            MemoryStream stream = new MemoryStream();
            wb.SaveAs(stream);

            return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Usuarios.xlsx");

        }

        public FileResult ExportarPdf() {
            MemoryStream ms = new MemoryStream();
            string strPDFFileName = string.Format("Reporte usuarios " + DateTime.Now.Date.ToShortDateString() + ".pdf");
            PdfWriter pw = new PdfWriter(ms);
            PdfDocument pdfDocument = new PdfDocument(pw);
            Document doc = new Document(pdfDocument, PageSize.LETTER);
            doc.SetMargins(75, 35, 70, 35);

            string pathImage = Server.MapPath("~/Img/logoimbolsa.png");
            Image image = new Image(ImageDataFactory.Create(pathImage));
            pdfDocument.AddEventHandler(PdfDocumentEvent.START_PAGE, new HeaderEventHandler1(image));
            pdfDocument.AddEventHandler(PdfDocumentEvent.END_PAGE, new FooterEventHandler1());

            Table table = new Table(1).UseAllAvailableWidth();
            Cell cell = new Cell().Add(new Paragraph("Reporte de usarios").SetFontSize(14))
                .SetTextAlignment(TextAlignment.CENTER)
                .SetBorder(Border.NO_BORDER);
            table.AddCell(cell);
            cell = new Cell().Add(new Paragraph("Todos los usuarios"))
                .SetTextAlignment(TextAlignment.CENTER)
                .SetBorder(Border.NO_BORDER);
            table.AddCell(cell);
            doc.Add(table);

            Style styles = new Style()
                .SetBackgroundColor(ColorConstants.BLUE)
                .SetFontColor(ColorConstants.WHITE)
                .SetTextAlignment(TextAlignment.CENTER);

            Table _table = new Table(4).UseAllAvailableWidth();
            Cell _cell = new Cell().Add(new Paragraph("Nombre"));
            _table.AddHeaderCell(_cell.AddStyle(styles));
            _cell = new Cell().Add(new Paragraph("Departamento"));
            _table.AddHeaderCell(_cell.AddStyle(styles));
            _cell = new Cell().Add(new Paragraph("Correo"));
            _table.AddHeaderCell(_cell.AddStyle(styles));
            _cell = new Cell().Add(new Paragraph("Extension"));
            _table.AddHeaderCell(_cell.AddStyle(styles));

            List<Usuario> users = db.Usuario.ToList<Usuario>();
            foreach (var user in users)
            {
                _cell = new Cell().Add(new Paragraph(user.Nombre));
                _table.AddCell(_cell);
                _cell = new Cell().Add(new Paragraph(user.Depto.Departamento));
                _table.AddCell(_cell);
                _cell = new Cell().Add(new Paragraph(user.Correo));
                _table.AddCell(_cell);
                if (user.Ext == null) {
                    _cell = new Cell().Add(new Paragraph(""));
                    _table.AddCell(_cell);
                }
                else {
                    _cell = new Cell().Add(new Paragraph(user.Ext));
                    _table.AddCell(_cell);
                }
            }
            doc.Add(_table);
            doc.Close();

            byte[] byteStream = ms.ToArray();
            ms = new MemoryStream();
            ms.Write(byteStream, 0, byteStream.Length);
            ms.Position = 0;

            return File(ms, "application/pdf", strPDFFileName);
        }      

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }

    public class HeaderEventHandler1 : IEventHandler
    {
        Image image;
        public HeaderEventHandler1(Image img) {
            image = img;
        }
        public void HandleEvent(Event @event)
        {
            PdfDocumentEvent docEvent = (PdfDocumentEvent)@event;
            PdfDocument pdfDoc = docEvent.GetDocument();
            PdfPage page = docEvent.GetPage();

            PdfCanvas canvas1 = new PdfCanvas(page.NewContentStreamBefore(), page.GetResources(), pdfDoc);
            Rectangle rootArea = new Rectangle(35, page.GetPageSize().GetTop() - 75, page.GetPageSize().GetWidth() - 70, 55);
            new Canvas(canvas1, pdfDoc, rootArea).Add(getTable(docEvent));
        }
        public Table getTable(PdfDocumentEvent docEvent) {
            float[] cellWith = { 20f, 80f };
            Table tableEvent = new Table(UnitValue.CreatePercentArray(cellWith)).UseAllAvailableWidth();

            Style Scell = new Style()
                .SetBorder(Border.NO_BORDER);

            Style Stext = new Style()
                .SetTextAlignment(TextAlignment.RIGHT).SetFontSize(10f);

            Cell cell = new Cell().Add(image.SetAutoScale(true));

            tableEvent.AddCell(cell)
                .AddStyle(Scell)
                .SetTextAlignment(TextAlignment.LEFT);

            PdfFont font = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);

            cell = new Cell()
                .Add(new Paragraph("Reprte diario\n").SetFont(font))
                .Add(new Paragraph("Departamento Sistemas\n").SetFont(font))
                .Add(new Paragraph("Fecha emision: " + DateTime.Now.ToShortDateString()))
                .Add(new Paragraph("Reprte diario\n").SetFont(font))
                .AddStyle(Stext).AddStyle(Scell);

            tableEvent.AddCell(cell);
            return tableEvent;
        }
    }

    public class FooterEventHandler1 : IEventHandler
    {
        public void HandleEvent(Event @event)
        {
            PdfDocumentEvent docEvent = (PdfDocumentEvent)@event;
            PdfDocument pdfDoc = docEvent.GetDocument();
            PdfPage page = docEvent.GetPage();

            PdfCanvas canvas1 = new PdfCanvas(page.NewContentStreamBefore(), page.GetResources(), pdfDoc);
            new Canvas(canvas1, pdfDoc, new Rectangle(36,20, page.GetPageSize().GetWidth() - 70, 50)).Add(getTable(docEvent));
        }
        public Table getTable(PdfDocumentEvent docEvent)
        {
            float[] cellWith = { 92f, 8f };
            Table tableEvent = new Table(UnitValue.CreatePercentArray(cellWith)).UseAllAvailableWidth();

            int PagNum = docEvent.GetDocument().GetPageNumber(docEvent.GetPage());

            PdfPage page = docEvent.GetPage();
            int pageNum = docEvent.GetDocument().GetPageNumber(page);
            int TotalPaginas = docEvent.GetDocument().GetNumberOfPages();

            Style Scell = new Style()
                .SetPadding(5)
                .SetBorder(Border.NO_BORDER)
                .SetBorderTop(new SolidBorder(ColorConstants.BLACK,2));

            Cell cell = new Cell().Add(new Paragraph(DateTime.Now.ToLongDateString()));

            tableEvent.AddCell(cell
                .AddStyle(Scell)
                .SetTextAlignment(TextAlignment.RIGHT)
                .SetFontColor(ColorConstants.LIGHT_GRAY));

            cell = new Cell().Add(new Paragraph(PagNum.ToString() +" de "+ TotalPaginas ));
            cell.AddStyle(Scell)
                .SetBackgroundColor(ColorConstants.BLACK)
                .SetFontColor(ColorConstants.WHITE)
                .SetTextAlignment(TextAlignment.CENTER);
            tableEvent.AddCell(cell);

            return tableEvent;
        }

    }
}
