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
        Models.TareaFinalContext db = new TareaFinalContext();

        public PaisController(ILogger<PaisController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View(BuscarPaises());
        }


        public List<Pais> BuscarPaises()
        {
            var paises = this.db.Pais.ToList();

            return paises;
        }

        [HttpGet]
        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Crear(Pais pais)
        {
            if (ExistePaisPorNombre(pais.Nombre))
                ViewBag.Datos = "Este paisBuscado ya existe";
            else
            {
                InsertarPais(pais);
                ViewBag.Datos = "Pais agregado";
            }

            return View();
        }

        public bool ExistePaisPorNombre(string nombre)
        {
            bool paisExiste = this.db.Pais.Any(country => country.Nombre == nombre);

            return paisExiste;
        }

        public void InsertarPais(Pais pais)
        {
            this.db.Pais.Add(pais);
            this.db.SaveChanges();
        }


        public IActionResult Detalle(int idPais)
        {
            return View(BuscarPaisPorId(idPais));
        }

        public Pais BuscarPaisPorId(int idPais)
        {
            var paisBuscado = this.db.Pais.Single(pais => pais.IdPais == idPais);

            return paisBuscado;
        }

        [HttpGet]
        public IActionResult Editar(int idPais)
        {
            return View(BuscarPaisPorId(idPais));
        }

        [HttpPost]
        public IActionResult Editar(Pais pais)
        {
            if (ExistePaisPorNombre(pais.Nombre))
            {
                ViewBag.Datos = "Este paisBuscado ya existe";
                return Editar(pais.IdPais);
            }
            else
            {
                EditarPais(pais);
                return RedirectToAction("Index");
            }
                
        }

        public void EditarPais(Pais pais)
        {
            Pais paisPorEditar = BuscarPaisPorId(pais.IdPais);
            paisPorEditar.Nombre = pais.Nombre;

            this.db.SaveChanges();
        }


        public IActionResult Eliminar(int idPais)
        {
            EliminarPais(idPais);
            return View();
        }


        
        public void EliminarPais(int idPais)
        {
            Pais paisPorEliminar = this.db.Pais.Single(pais => pais.IdPais == idPais);
            this.db.Pais.Remove(paisPorEliminar);
            this.db.SaveChanges();
        }

      
        
    }
}
