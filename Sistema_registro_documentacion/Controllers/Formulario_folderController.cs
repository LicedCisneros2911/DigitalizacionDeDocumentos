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
    public class Formulario_folderController : Controller
    {
        private readonly IGenericRepository<Formulario_folder> _folderRepo;

        public Formulario_folderController(IGenericRepository<Formulario_folder> folderRepo)
        {
            _folderRepo = folderRepo;
        }

        // GET: Formulario_folder
        public async Task<IActionResult> Index()
        {
            return View(_folderRepo.GetAll());
        }

        // GET: Formulario_folder/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var formulario_folder = _folderRepo.Find(id.GetValueOrDefault());
            if (formulario_folder == null)
            {
                return NotFound();
            }

            return View(formulario_folder);
        }

        // GET: Formulario_folder/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Formulario_folder/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,nombre_folder")] Formulario_folder formulario_folder)
        {
            if (ModelState.IsValid)
            {
                if (_folderRepo.Validate(formulario_folder))
                {
                    ModelState.AddModelError(string.Empty, "El folder ya existe");
                    return View();
                }
                _folderRepo.Add(formulario_folder);
                ViewBag.msg = "Exito";
                ModelState.Clear();
                return View();
            }
            ViewBag.msg = "Error";
            return View(formulario_folder);
        }

        // GET: Formulario_folder/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var formulario_folder = _folderRepo.Find(id.GetValueOrDefault());
            if (formulario_folder == null)
            {
                return NotFound();
            }
            return View(formulario_folder);
        }

        // POST: Formulario_folder/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,nombre_folder")] Formulario_folder formulario_folder)
        {
            if (id != formulario_folder.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (_folderRepo.Validate(formulario_folder))
                {
                    ModelState.AddModelError(string.Empty, "El folder ya existe");
                    return View();
                }
                _folderRepo.Update(formulario_folder);
                return RedirectToAction(nameof(Index));
            }
            return View(formulario_folder);
        }

        // GET: Formulario_folder/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            _folderRepo.Remove(id.GetValueOrDefault());
            return RedirectToAction(nameof(Index));
        }       
    }
}
