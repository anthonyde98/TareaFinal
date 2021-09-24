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
        Models.TareaFinalContext db = new TareaFinalContext();

        public ClienteController(ILogger<ClienteController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View(BuscarCliente());
        }

        public IQueryable<ClienteViewModel> BuscarCliente()
        {
            var cliente = from c in db.Clientes
                          from p in db.Pais
                          where c.IdPais == p.IdPais
                          select new ClienteViewModel()
                          {
                              IdCliente = c.IdCliente,
                              Nombre = c.Nombre,
                              FechaNacimiento = c.FechaNacimiento,
                              NombrePais = p.Nombre
                          };

            return cliente;
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
            ViewBag.Datos = "cliente agregado";

            return View(BuscarPaises());
        }

        public List<Pais> BuscarPaises()
        {
            var paises = this.db.Pais.ToList();

            return paises;
        }


        public void InsertarCliente(Cliente cliente)
        {
            this.db.Clientes.Add(cliente);
            this.db.SaveChanges();
        }

        public IActionResult Detalle(int idCliente)
        {
            return View(BuscarClientePorId(idCliente));
        }

        public ClienteViewModel BuscarClientePorId(int idCliente)
        {



            var cliente = from c in this.db.Clientes
                          from p in this.db.Pais
                          where c.IdPais == p.IdPais && c.IdCliente == idCliente
                          select new ClienteViewModel()
                          {
                              IdCliente = c.IdCliente,
                              Nombre = c.Nombre,
                              FechaNacimiento = c.FechaNacimiento,
                              NombrePais = p.Nombre
                          };

            return cliente.Single();
        }

        [HttpGet]
        public IActionResult Editar(int idCliente)
        {
            return View(ConfigEditarCliente(idCliente));
        }

        [HttpPost]
        public IActionResult Editar(Cliente cliente)
        {
            EditarCliente(cliente);
            return RedirectToAction("Index");
        }

        public void EditarCliente(Cliente clienteActual)
        {
            Cliente clientePorEditar = db.Clientes.Single(cliente => cliente.IdCliente == clienteActual.IdCliente);
            clientePorEditar.Nombre = clienteActual.Nombre;
            clientePorEditar.IdPais = clienteActual.IdPais;
            clientePorEditar.FechaNacimiento = clienteActual.FechaNacimiento;
            this.db.SaveChanges();
        }

        public IActionResult Eliminar(int idCliente)
        {
            
            EliminarCliente(idCliente);
            return View();
        }

       
        public void EliminarCliente(int id)
        {
            Cliente clientePorEliminar = db.Clientes.Single(cliente => cliente.IdCliente == id);
            this.db.Clientes.Remove(clientePorEliminar);
            this.db.SaveChanges();
        }

       

        //---------------------------------------------------------------------------------------------

        //------------------------------------- Metodos Extras ----------------------------------------

        public EditarClienteViewModel ConfigEditarCliente(int idCliente)
        {
            EditarClienteViewModel editarClienteViewModel = new EditarClienteViewModel();

            editarClienteViewModel.Cliente = this.db.Clientes.Single(cliente => cliente.IdCliente == idCliente);

            editarClienteViewModel.Paises = BuscarPaises();

            return editarClienteViewModel;
        }
    }
}
