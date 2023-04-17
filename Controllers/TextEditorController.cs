using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TextEditorMVC.Models;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using System.Drawing;

namespace TextEditorMVC.Controllers
{
    public class TextEditorController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TextEditorController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TextEditor
        public async Task<IActionResult> Index()
        {
              return _context.TestEditor != null ? 
                          View(await _context.TestEditor.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.TestEditor'  is null.");
        }

        // GET: TextEditor/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TestEditor == null)
            {
                return NotFound();
            }

            var textEditorModel = await _context.TestEditor
                .FirstOrDefaultAsync(m => m.Id == id);
            if (textEditorModel == null)
            {
                return NotFound();
            }

            return View(textEditorModel);
        }

        // GET: TextEditor/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TextEditor/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Author,Title,Body")] TextEditorModel textEditorModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(textEditorModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(textEditorModel);
        }

        // GET: TextEditor/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TestEditor == null)
            {
                return NotFound();
            }

            var textEditorModel = await _context.TestEditor.FindAsync(id);
            if (textEditorModel == null)
            {
                return NotFound();
            }
            return View(textEditorModel);
        }

        // POST: TextEditor/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Author,Title,Body")] TextEditorModel textEditorModel)
        {
            if (id != textEditorModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(textEditorModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TextEditorModelExists(textEditorModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(textEditorModel);
        }

        // GET: TextEditor/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TestEditor == null)
            {
                return NotFound();
            }

            var textEditorModel = await _context.TestEditor
                .FirstOrDefaultAsync(m => m.Id == id);
            if (textEditorModel == null)
            {
                return NotFound();
            }

            return View(textEditorModel);
        }

        // POST: TextEditor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TestEditor == null)
            {
                return Problem("Entity set 'ApplicationDbContext.TestEditor'  is null.");
            }
            var textEditorModel = await _context.TestEditor.FindAsync(id);
            if (textEditorModel != null)
            {
                _context.TestEditor.Remove(textEditorModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        private bool TextEditorModelExists(int id)
        {
          return (_context.TestEditor?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        /*public void CreatePDFDocument()
        {
            using (Syncfusion.Pdf.PdfDocument document = new Syncfusion.Pdf.PdfDocument())
            {
                //Add a page to the document.
                PdfPage page = document.Pages.Add();
                //Create PDF graphics for the page.
                PdfGraphics graphics = page.Graphics;
                //Set the standard font.
                PdfFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 20);
                //Draw the text.
                graphics.DrawString("Hello World!!!", font, PdfBrushes.Black, new PointF(0, 0));
                //Open the document in browser after saving it. 
                document.Save("Output.pdf", HttpContext.ApplicationInstance.Response, HttpReadType.Save);
            }
        }*/
    }
}
