using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProveduriaWeb;

namespace ProveduriaWeb.Controllers
{
    public class FacturasVentumsController : Controller
    {
        private readonly ProveeduriaPiiContext _context;

        public FacturasVentumsController(ProveeduriaPiiContext context)
        {
            _context = context;
        }

        // GET: FacturasVentums
        public async Task<IActionResult> Index()
        {
            var proveeduriaPiiContext = _context.FacturasVenta.Include(f => f.IdClienteNavigation);
            return View(await proveeduriaPiiContext.ToListAsync());
        }

        // GET: FacturasVentums/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var facturasVentum = await _context.FacturasVenta
                .Include(f => f.IdClienteNavigation)
                .FirstOrDefaultAsync(m => m.IdFacturaVenta == id);
            if (facturasVentum == null)
            {
                return NotFound();
            }

            return View(facturasVentum);
        }

        // GET: FacturasVentums/Create
        public IActionResult Create()
        {
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "IdCliente", "IdCliente");
            return View();
        }

        // POST: FacturasVentums/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdFacturaVenta,IdCliente,FechaFactura,NumeroFactura,Impuesto,MontoTotal,TotalImpuestosCobrados")] FacturasVentum facturasVentum)
        {
            if (ModelState.IsValid)
            {
                _context.Add(facturasVentum);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "IdCliente", "IdCliente", facturasVentum.IdCliente);
            return View(facturasVentum);
        }

        // GET: FacturasVentums/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var facturasVentum = await _context.FacturasVenta.FindAsync(id);
            if (facturasVentum == null)
            {
                return NotFound();
            }
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "IdCliente", "IdCliente", facturasVentum.IdCliente);
            return View(facturasVentum);
        }

        // POST: FacturasVentums/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdFacturaVenta,IdCliente,FechaFactura,NumeroFactura,Impuesto,MontoTotal,TotalImpuestosCobrados")] FacturasVentum facturasVentum)
        {
            if (id != facturasVentum.IdFacturaVenta)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(facturasVentum);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FacturasVentumExists(facturasVentum.IdFacturaVenta))
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
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "IdCliente", "IdCliente", facturasVentum.IdCliente);
            return View(facturasVentum);
        }

        // GET: FacturasVentums/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var facturasVentum = await _context.FacturasVenta
                .Include(f => f.IdClienteNavigation)
                .FirstOrDefaultAsync(m => m.IdFacturaVenta == id);
            if (facturasVentum == null)
            {
                return NotFound();
            }

            return View(facturasVentum);
        }

        // POST: FacturasVentums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var facturasVentum = await _context.FacturasVenta.FindAsync(id);
            if (facturasVentum != null)
            {
                _context.FacturasVenta.Remove(facturasVentum);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FacturasVentumExists(int id)
        {
            return _context.FacturasVenta.Any(e => e.IdFacturaVenta == id);
        }
    }
}
