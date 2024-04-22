using CrudAsp.DTO;
using CrudAsp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace CrudAsp.Controllers
{
    public class HomeController : Controller
    {
        private readonly CrudaspContext _context;

        public HomeController(CrudaspContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var contactos = await _context.Contactos.ToListAsync();
            var contactoDto = contactos                
                .Select(contacto => new ContactoDTO
                {
                    IdContacto = contacto.IdContacto,
                    Nombre = contacto.Nombre,
                    Correo = contacto.Correo,
                    Telefono = contacto.Telefono
                }).ToList();        
        

            return View(contactoDto);
        }
        [HttpGet]
        public IActionResult Crear()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(ContactoDTO contacto)
        {
            if (ModelState.IsValid)
            {
                var nuevoContacto = new Contacto
                {
                    Nombre = contacto.Nombre,
                    Telefono = contacto.Telefono,
                    Correo = contacto.Correo
                };
                _context.Contactos.Add(nuevoContacto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        [HttpGet]
        public IActionResult Editar(int? id, ContactoDTO contactoDto)
        {
            if (id == null)
            {
                return NotFound();
            }
            var contacto = _context.Contactos.Find(id);
            contactoDto.IdContacto= contacto.IdContacto;
            contactoDto.Nombre= contacto.Nombre;
            contactoDto.Correo = contacto.Correo;
            contactoDto.Telefono= contacto.Telefono;

            if (contacto == null)
            {
                return NotFound();
            }
            return View(contactoDto);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Actualizar(ContactoDTO contacto)
        {
            if (ModelState.IsValid)
            {
                var contactoExistente = _context.Contactos.FirstOrDefault(a=>a.IdContacto==contacto.IdContacto);
                {
                   contactoExistente.Nombre=contacto.Nombre;
                    contactoExistente.Correo=contacto.Correo;
                    contactoExistente.Telefono = contacto.Telefono;
                };
                _context.Update(contactoExistente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Eliminar (int id)
        {
            bool registrado = false;
            var contactoExistente = await _context.Contactos.FindAsync(id);
            if (contactoExistente == null)
            {
                
            }
            try
            {
                _context.Contactos.Remove(contactoExistente);
                _context.SaveChanges();
                var registrosActualizados = Index();
                return PartialView("_TablaRegistros", registrosActualizados);

            }
            catch
            {
                throw;

            }            
        }
        [HttpPost]
        public async void EliminarFN(int id)
        {
            bool registrado = false;
            var contactoExistente = await _context.Contactos.FindAsync(id);
            if (contactoExistente == null)
            {
               
            }
            try
            {
                _context.Contactos.Remove(contactoExistente);
                _context.SaveChanges();
               Index();
            }
            catch
            {
                throw;

            }
           
        }

    }
}
