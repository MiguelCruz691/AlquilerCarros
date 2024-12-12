using AlquilerCarros.DTO;
using AlquilerCarros.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace AlquilerCarros.Controllers
{
    public class AlquilerController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AlquilerController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AlquilerController
        public IActionResult Alquiler(DateOnly? fechaInicio, DateOnly? fechaFin)
        {
            var alquileres = _context.Alquilers
                .Where(a => a.FechaInicio >= fechaInicio && a.FechaFin <= fechaFin)
                .Join(_context.Clientes, a => a.IdClienteNavigation.Cedula, c => c.Cedula, (a, c) => new { a, c })
                .Join(_context.Carros, ac => ac.a.IdCarroNavigation.Placa, ca => ca.Placa, (ac, ca) => new ClienteCarroAlquilerDTO
                {
                    Cedula = ac.c.Cedula,
                    Nombre = ac.c.Nombre,
                    FechaInicio = ac.a.FechaInicio,
                    FechaFin = ac.a.FechaFin,
                    Tiempo = ac.a.Tiempo,
                    Saldo = ac.a.Saldo,
                    Placa = ac.a.IdCarroNavigation.Placa,
                    Marca = ac.a.IdCarroNavigation.Marca
                })
                .ToList();

            Console.WriteLine($"Se encontraron {alquileres.Count} alquileres.");
            Console.WriteLine($"Fecha de inicio: {fechaInicio}");
            Console.WriteLine($"Fecha final: {fechaFin}");

            return View("Alquiler", alquileres);
        }

        //// GET: AlquilerController/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        //// GET: AlquilerController/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: AlquilerController/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: AlquilerController/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: AlquilerController/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: AlquilerController/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: AlquilerController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
