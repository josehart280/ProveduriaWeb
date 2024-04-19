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
    public class FacturasComprasController : Controller
    {
        private readonly ProveeduriaPiiContext _context;

        public FacturasComprasController(ProveeduriaPiiContext context)
        {
            _context = context;
        }

        // GET: FacturasCompras
        public async Task<IActionResult> Index()
        {
            var proveeduriaPiiContext = _context.FacturasCompras.Include(f => f.IdProveedorNavigation);
            return View(await proveeduriaPiiContext.ToListAsync());
        }

        // GET: FacturasCompras/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var facturasCompra = await _context.FacturasCompras
                .Include(f => f.IdProveedorNavigation)
                .FirstOrDefaultAsync(m => m.IdFacturaCompra == id);
            if (facturasCompra == null)
            {
                return NotFound();
            }

            return View(facturasCompra);
        }

        // GET: FacturasCompras/Create
        public IActionResult Create()
        {
            ViewData["IdProveedor"] = new SelectList(_context.Proveedores, "IdProveedor", "IdProveedor");
            return View();
        }

        // POST: FacturasCompras/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdFacturaCompra,IdProveedor,FechaFactura,NumeroFactura,Impuesto,MontoTotal,TotalImpuestosPagados")] FacturasCompra facturasCompra)
        {
            if (ModelState.IsValid)
            {
                _context.Add(facturasCompra);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdProveedor"] = new SelectList(_context.Proveedores, "IdProveedor", "IdProveedor", facturasCompra.IdProveedor);
            return View(facturasCompra);
        }

        // GET: FacturasCompras/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var facturasCompra = await _context.FacturasCompras.FindAsync(id);
            if (facturasCompra == null)
            {
                return NotFound();
            }
            ViewData["IdProveedor"] = new SelectList(_context.Proveedores, "IdProveedor", "IdProveedor", facturasCompra.IdProveedor);
            return View(facturasCompra);
        }

        // POST: FacturasCompras/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdFacturaCompra,IdProveedor,FechaFactura,NumeroFactura,Impuesto,MontoTotal,TotalImpuestosPagados")] FacturasCompra facturasCompra)
        {
            if (id != facturasCompra.IdFacturaCompra)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(facturasCompra);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FacturasCompraExists(facturasCompra.IdFacturaCompra))
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
            ViewData["IdProveedor"] = new SelectList(_context.Proveedores, "IdProveedor", "IdProveedor", facturasCompra.IdProveedor);
            return View(facturasCompra);
        }

        // GET: FacturasCompras/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var facturasCompra = await _context.FacturasCompras
                .Include(f => f.IdProveedorNavigation)
                .FirstOrDefaultAsync(m => m.IdFacturaCompra == id);
            if (facturasCompra == null)
            {
                return NotFound();
            }

            return View(facturasCompra);
        }

        // POST: FacturasCompras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var facturasCompra = await _context.FacturasCompras.FindAsync(id);
            if (facturasCompra != null)
            {
                _context.FacturasCompras.Remove(facturasCompra);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FacturasCompraExists(int id)
        {
            return _context.FacturasCompras.Any(e => e.IdFacturaCompra == id);
        }
    }
}
