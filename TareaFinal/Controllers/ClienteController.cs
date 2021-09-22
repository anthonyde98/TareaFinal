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
    public class ClienteController : Controller
    {
        private readonly ILogger<ClienteController> _logger;
        Models.TareaFinalContext _db = new TareaFinalContext();

        public ClienteController(ILogger<ClienteController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View(BuscarCliente());
        }

        [HttpGet]
        public IActionResult Crear()
        {     
            return View(BuscarPaises());
        }

        [HttpPost]
        public IActionResult Crear(Cliente cliente)
        {
            InsertarCliente(cliente);
            ViewBag.Datos = "Cliente agregado";

            return View(BuscarPaises());
        }

        public IActionResult Detalle(int id)
        {
            return View(BuscarClienteById(id));
        }

        [HttpGet]
        public IActionResult Editar(int id)
        {
            return View(ConfigEditarCliente(id));
        }

        [HttpPost]
        public IActionResult Editar(Cliente Cliente)
        {
            EditarCliente(Cliente);
            return RedirectToAction("Index");
        }

        public IActionResult Eliminar(int id)
        {
            EliminarCliente(id);
            return View();
        }

        //------------------------ Metodos CUD (Create, Update, Delete) ------------------------------

        public void InsertarCliente(Cliente costumer)
        {

            _db.Clientes.Add(costumer);
            _db.SaveChanges();
        }

        public void EditarCliente(Cliente costumer)
        {
            Cliente costumer2 = _db.Clientes.Single(cliente => cliente.Id == costumer.Id);
            costumer2.Nombre = costumer.Nombre;
            costumer2.NombrePais = costumer.NombrePais;
            costumer2.FechaNacimiento = costumer.FechaNacimiento;
            _db.SaveChanges();
        }

        public void EliminarCliente(int id)
        {
            Cliente costumer = _db.Clientes.Single(cliente => cliente.Id == id);
            _db.Clientes.Remove(costumer);
            _db.SaveChanges();
        }

        //-------------------------------------------------------------------------------------------

        //------------------------------------ Metodos R (Read) -------------------------------------

        public IQueryable<ClienteViewModel> BuscarCliente()
        {
            var Cliente = from c in _db.Clientes 
                          from p in _db.Pais 
                          where c.NombrePais == p.Id 
                          select new ClienteViewModel() 
                          { 
                              Id = c.Id, 
                              Nombre = c.Nombre, 
                              FechaNacimiento = c.FechaNacimiento, 
                              NombrePais = p.Nombre 
                          };

            return Cliente;
        }

        public List<Pais> BuscarPaises()
        {
            var paises = _db.Pais.ToList();

            return paises;
        }

        public ClienteViewModel BuscarClienteById(int id)
        {
            var Cliente = from c in _db.Clientes
                          from p in _db.Pais
                          where c.NombrePais == p.Id && c.Id == id
                          select new ClienteViewModel()
                          {
                              Id = c.Id,
                              Nombre = c.Nombre,
                              FechaNacimiento = c.FechaNacimiento,
                              NombrePais = p.Nombre
                          };

            return Cliente.Single();
        }

        //---------------------------------------------------------------------------------------------

        //------------------------------------- Metodos Extras ----------------------------------------

        public EditarClienteViewModel ConfigEditarCliente(int id)
        {
            EditarClienteViewModel CPs = new EditarClienteViewModel();

            CPs.Cliente = _db.Clientes.Single(costumer => costumer.Id == id);

            CPs.Paises = BuscarPaises();

            return CPs;
        }
    }
}
