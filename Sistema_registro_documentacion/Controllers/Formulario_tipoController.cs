using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Sistema_registro_documentacion.Data;
using Sistema_registro_documentacion.Models;
using Sistema_registro_documentacion.Repository;

namespace Sistema_registro_documentacion.Controllers
{
    [Authorize(Roles = "Administrador, Editor")]
    public class Formulario_tipoController : Controller
    {
        private readonly IGenericRepository<Formulario_tipo> _tipoRepo;

        public Formulario_tipoController(IGenericRepository<Formulario_tipo> tipoRepo)
        {
            _tipoRepo = tipoRepo;
        }

        // GET: Formulario_tipo
        public async Task<IActionResult> Index()
        {
            return View(_tipoRepo.GetAll());
        }

        // GET: Formulario_tipo/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var formulario_tipo = _tipoRepo.Find(id.GetValueOrDefault());
            if (formulario_tipo == null)
            {
                return NotFound();
            }

            return View(formulario_tipo);
        }

        // GET: Formulario_tipo/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Formulario_tipo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,nombre_tipo")] Formulario_tipo formulario_tipo)
        {
            if (ModelState.IsValid)
            {
                _tipoRepo.Add(formulario_tipo);
                ViewBag.msg = "Exito";
                ModelState.Clear();
                return View();
                //return RedirectToAction(nameof(Index));
            }
            ViewBag.msg = "Error";
            return View(formulario_tipo);
        }

        // GET: Formulario_tipo/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var formulario_tipo = _tipoRepo.Find(id.GetValueOrDefault());
            if (formulario_tipo == null)
            {
                return NotFound();
            }
            return View(formulario_tipo);
        }

        // POST: Formulario_tipo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,nombre_tipo")] Formulario_tipo formulario_tipo)
        {
            if (id != formulario_tipo.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _tipoRepo.Update(formulario_tipo);
                return RedirectToAction(nameof(Index));
            }
            return View(formulario_tipo);
        }

        // GET: Formulario_tipo/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            _tipoRepo.Remove(id.GetValueOrDefault());
           

            return RedirectToAction(nameof(Index));
        }
    }
}
