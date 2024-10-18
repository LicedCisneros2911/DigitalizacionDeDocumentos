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
    public class Formulario_gestionController : Controller
    {
        private readonly IGenericRepository<Formulario_gestion> _gesRepo;

        public Formulario_gestionController(IGenericRepository <Formulario_gestion> gesRepo)
        {
            _gesRepo = gesRepo;
        }

        // GET: Formulario_gestion
        public async Task<IActionResult> Index()
        {
            return View(_gesRepo.GetAll());
        }

        // GET: Formulario_gestion/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var formulario_gestion = _gesRepo.Find(id.GetValueOrDefault());
            if (formulario_gestion == null)
            {
                return NotFound();
            }

            return View(formulario_gestion);
        }

        // GET: Formulario_gestion/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Formulario_gestion/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,nombre_gestion")] Formulario_gestion formulario_gestion)
        {
            if (ModelState.IsValid)
            {
                _gesRepo.Add(formulario_gestion);
                ViewBag.msg = "Exito";
                ModelState.Clear();
                return View();
            }
            ViewBag.msg = "Error";
            return View(formulario_gestion);
        }

        // GET: Formulario_gestion/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var formulario_gestion = _gesRepo.Find(id.GetValueOrDefault());
            if (formulario_gestion == null)
            {
                return NotFound();
            }
            return View(formulario_gestion);
        }

        // POST: Formulario_gestion/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,nombre_gestion")] Formulario_gestion formulario_gestion)
        {
            if (id != formulario_gestion.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _gesRepo.Update(formulario_gestion);
                return RedirectToAction(nameof(Index));
            }
            return View(formulario_gestion);
        }

        // GET: Formulario_gestion/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            _gesRepo.Remove(id.GetValueOrDefault());

            return RedirectToAction(nameof(Index));
        }
      
    }
}
