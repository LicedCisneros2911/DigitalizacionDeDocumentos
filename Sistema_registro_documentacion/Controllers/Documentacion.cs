using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema_registro_documentacion.Controllers
{
    public class Documentacion : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
