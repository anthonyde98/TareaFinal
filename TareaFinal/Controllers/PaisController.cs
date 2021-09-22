using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TareaFinal.Models;

namespace TareaFinal.Controllers
{
    public class PaisController : Controller
    {
        private readonly ILogger<PaisController> _logger;
        Models.TareaFinalContext _db = new TareaFinalContext();

        public PaisController(ILogger<PaisController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View(BuscarPaises());
        }

        [HttpGet]
        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Crear(Pais pais)
        {
            if (ExistePaisByName(pais.Nombre))
                ViewBag.Datos = "Este pais ya existe";
            else
            {
                InsertarPais(pais);
                ViewBag.Datos = "Pais agregado";
            }

            return View();
        }

        public IActionResult Detalle(int id)
        {
            return View(BuscarPaisById(id));
        }

        [HttpGet]
        public IActionResult Editar(int id)
        {
            return View(BuscarPaisById(id));
        }

        [HttpPost]
        public IActionResult Editar(Pais pais)
        {
            if (ExistePaisByName(pais.Nombre))
            {
                ViewBag.Datos = "Este pais ya existe";
                return Editar(pais.Id);
            }
            else
            {
                EditarPais(pais);
                return RedirectToAction("Index");
            }
                
        }

        public IActionResult Eliminar(int id)
        {
            EliminarPais(id);
            return View();
        }

        //------------------------ Metodos CUD (Create, Update, Delete) ------------------------------

        public void InsertarPais(Pais country)
        {
            _db.Pais.Add(country);
            _db.SaveChanges();
        }

        public void EditarPais(Pais country)
        {
            Pais country2 = _db.Pais.Single(pais => pais.Id == country.Id);
            country2.Nombre = country.Nombre;

            _db.SaveChanges();
        }

        public void EliminarPais(int id)
        {
            Pais country = _db.Pais.Single(pais => pais.Id == id);
            _db.Pais.Remove(country);
            _db.SaveChanges();
        }

        //-------------------------------------------------------------------------------------------

        //------------------------------------ Metodos R (Read) -------------------------------------

        public List<Pais> BuscarPaises()
        {
            var paises = _db.Pais.ToList();

            return paises;
        }

        public Pais BuscarPaisById(int id)
        {
            var pais = _db.Pais.Single(country => country.Id == id);

            return pais;
        }

        public bool ExistePaisByName(string nombre)
        {
            bool pais = _db.Pais.Any(country => country.Nombre == nombre);

            return pais;
        }
    }
}
