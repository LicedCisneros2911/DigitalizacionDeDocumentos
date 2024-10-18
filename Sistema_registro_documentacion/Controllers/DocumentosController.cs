using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sistema_registro_documentacion.Data;
using Sistema_registro_documentacion.Models;
using Sistema_registro_documentacion.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Sistema_registro_documentacion.Controllers
{
    [Authorize]
    public class DocumentosController : Controller
    {
        private readonly IGenericRepository<Documento> _docRepo;
        private readonly IWebHostEnvironment _env;
        private readonly ApplicationDBContext _context;
        public DocumentosController(ApplicationDBContext context, IGenericRepository<Documento> docRepo, IWebHostEnvironment env)
        {
            _docRepo = docRepo;
            _env = env;
            _context = context;
        }

        // GET: Documentos
        public async Task<IActionResult> Index(int folder = 0, int tipo = 0, string fecha = "2024")
        {
            List<Documento> documento = new List<Documento>();
            if (!String.IsNullOrEmpty(fecha))
            {
                List<string> param = new List<string>();
                param.Add(folder.ToString());
                param.Add(tipo.ToString());
                param.Add(fecha);
                documento = _docRepo.Filter(param);
                ViewBag.Cantidad = documento.Count;
                ViewBag.Folder = fecha;
                if (tipo == -1)
                {
                    ViewBag.msg = "Exito";
                }
                else if(tipo == -2)
                {
                    ViewBag.msg = "Error";
                }
                ViewData["tipoList"] = new SelectList(_context.formulario_tipo, "id", "nombre_tipo");
                ViewData["gestionList"] = new SelectList(_context.formulario_gestion, "nombre_gestion", "nombre_gestion");
                ViewData["folderList"] = new SelectList(_context.formulario_folder, "id", "nombre_folder");
            }
            else
            {
                return View(_docRepo.GetAll());
            }
            return View(documento);
        }

        [Authorize(Roles = "Administrador, Editor")]
        // GET: Documentos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var documento = _docRepo.Find(id.GetValueOrDefault());
            if (documento == null)
            {
                return NotFound();
            }
            return View(documento);
        }

        // GET: Documentos/Create
        [Authorize(Roles = "Administrador, Editor")]
        public IActionResult Create()
        {
            ViewData["tipoList"] = new SelectList(_context.formulario_tipo, "id", "nombre_tipo");
            ViewData["gestionList"] = new SelectList(_context.formulario_gestion, "nombre_gestion", "nombre_gestion");
            ViewData["folderList"] = new SelectList(_context.formulario_folder, "id", "nombre_folder");
            return View();
        }

        // POST: Documentos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador, Editor")]
        public async Task<IActionResult> Create([Bind("id,tipo,folder,titulo,cite,de,via,a,Ref,fecha,descripcion,filefoto")] Documento documento)
        {
            if (ModelState.IsValid)
            {
                if (_docRepo.Validate(documento))
                {
                    ModelState.AddModelError(string.Empty, "El cite ya existe");
                    ViewData["tipoList"] = new SelectList(_context.formulario_tipo, "id", "nombre_tipo");
                    ViewData["gestionList"] = new SelectList(_context.formulario_gestion, "nombre_gestion", "nombre_gestion");
                    ViewData["folderList"] = new SelectList(_context.formulario_folder, "id", "nombre_folder");
                    return View();
                }

                else
                {
                    if (documento.filefoto != null)
                    {
                        documento.foto = UploadFileBytes(documento);
                    }
                    _docRepo.Add(documento);
                    ViewData["tipoList"] = new SelectList(_context.formulario_tipo, "id", "nombre_tipo");
                    ViewData["gestionList"] = new SelectList(_context.formulario_gestion, "nombre_gestion", "nombre_gestion");
                    ViewData["folderList"] = new SelectList(_context.formulario_folder, "id", "nombre_folder");
                    ViewBag.msg = "Exito";
                    ModelState.Clear();
                    return View();
                }
            }
            ViewData["tipoList"] = new SelectList(_context.formulario_tipo, "id", "nombre_tipo");
            ViewData["gestionList"] = new SelectList(_context.formulario_gestion, "nombre_gestion", "nombre_gestion");
            ViewData["folderList"] = new SelectList(_context.formulario_folder, "id", "nombre_folder");
            ViewBag.msg = "Error";
            return View(documento);
        }

        // GET: Documentos/Edit/5
        [Authorize(Roles = "Administrador, Editor")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var documento = _docRepo.Find(id.GetValueOrDefault());
            if (documento == null)
            {
                return NotFound();
            }
            ViewData["tipoList"] = new SelectList(_context.formulario_tipo, "id", "nombre_tipo");
            ViewData["gestionList"] = new SelectList(_context.formulario_gestion, "nombre_gestion", "nombre_gestion");
            ViewData["folderList"] = new SelectList(_context.formulario_folder, "id", "nombre_folder");
            return View(documento);
        }

        // POST: Documentos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador, Editor")]
        public async Task<IActionResult> Edit(int id, [Bind("id,tipo,folder,titulo,cite,de,via,a,Ref,fecha,descripcion,filefoto")] Documento documento)
        {
            if (id != documento.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (_docRepo.Validate(documento))
                {
                    ModelState.AddModelError(string.Empty, "El cite ya existe");
                    ViewData["tipoList"] = new SelectList(_context.formulario_tipo, "id", "nombre_tipo");
                    ViewData["gestionList"] = new SelectList(_context.formulario_gestion, "nombre_gestion", "nombre_gestion");
                    ViewData["folderList"] = new SelectList(_context.formulario_folder, "id", "nombre_folder");
                    return View();
                }
                else
                {
                    if (documento.filefoto != null)
                    {
                        documento.foto = UploadFileBytes(documento);
                    }
                    _docRepo.Update(documento);
                    return RedirectToAction("Index", new { folder = 0, tipo = -1, fecha = ""});
                }
            }
            ViewData["tipoList"] = new SelectList(_context.formulario_tipo, "id", "nombre_tipo");
            ViewData["gestionList"] = new SelectList(_context.formulario_gestion, "nombre_gestion", "nombre_gestion");
            ViewData["folderList"] = new SelectList(_context.formulario_folder, "id", "nombre_folder");
            ViewBag.msg = "Error";
            return RedirectToAction("Index", new { folder = 0, tipo = -2, fecha = "" });
        }

        // GET: Documentos/Delete/5
        [Authorize(Roles = "Administrador, Editor")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            _docRepo.Remove(id.GetValueOrDefault());
            return RedirectToAction(nameof(Index));
        }
        private byte[] UploadFileBytes(Documento documento)
        {
            byte[] imagebytes = null;
            if (documento.filefoto != null)
            {
                using (var s = new MemoryStream())
                {
                    documento.filefoto.CopyTo(s);
                    var filebytes = s.ToArray();
                    imagebytes = filebytes;
                }
            }
            return imagebytes;
        }

        public ActionResult ShowPDF(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var documento = _docRepo.Find(id.GetValueOrDefault());
            if (documento == null || documento.foto == null)
            {
                return NotFound();
            }
            MemoryStream ms = new MemoryStream(documento.foto);
            return new FileStreamResult(ms, "application/pdf");
        }

        private string UploadImage(Documento documento)
        {
            string filename = null;
            if (documento.filefoto != null)
            {
                string dir = Path.Combine(_env.WebRootPath, "imagenes");
                filename = Guid.NewGuid().ToString() + "-" + documento.filefoto.FileName;
                string path = Path.Combine(dir, filename);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    documento.filefoto.CopyTo(stream);
                }
            }
            return filename;
        }
    }
}

