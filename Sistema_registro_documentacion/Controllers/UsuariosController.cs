using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sistema_registro_documentacion.Models;
using Sistema_registro_documentacion.Repository;
using System.Threading.Tasks;

namespace CoreWebApplicationTrial.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class UsuariosController : Controller
    {
        private readonly IGenericRepository<Usuario> _usuarioRepo;


        public UsuariosController(IGenericRepository<Usuario> usuarioRepo)
        {
            _usuarioRepo = usuarioRepo;
        }


        // GET: Usuarios
        public async Task<IActionResult> Index()
        {


            return View(_usuarioRepo.GetAll());
        }

        // GET: Usuarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = _usuarioRepo.Find(id.GetValueOrDefault());
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // GET: Usuarios/Create
        public IActionResult Create()
        {
            return View();
        }



        // POST: Usuarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,nombre,apppaterno,appmaterno,telefono,estado,rol,usuario,password")] Usuario usu)
        {
            if (ModelState.IsValid)
            {
                if (_usuarioRepo.Validate(usu))
                {
                    ModelState.AddModelError(string.Empty, "El usuario ya existe");
                    return View();
                }
                else
                {
                    _usuarioRepo.Add(usu);
                    ViewBag.msg = "Exito";
                    ModelState.Clear();
                    return View();
                }
            }
            ViewBag.msg = "Error";
            return View(usu);
        }


        // GET: Usuarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = _usuarioRepo.Find(id.GetValueOrDefault());
            if (usuario == null)
            {
                return NotFound();
            }
            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,nombre,apppaterno,appmaterno,telefono,estado,rol,usuario,password")] Usuario usu)
        {
            if (id != usu.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (_usuarioRepo.Validate(usu))
                {
                    ModelState.AddModelError(string.Empty, "El usuario ya existe");
                    return View();
                }
                else
                {
                    _usuarioRepo.Update(usu);
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(usu);
        }

        // GET: Usuarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            _usuarioRepo.Remove(id.GetValueOrDefault());
            return RedirectToAction(nameof(Index));
        }


        //FILTER
        public async Task<IActionResult> Filter(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
