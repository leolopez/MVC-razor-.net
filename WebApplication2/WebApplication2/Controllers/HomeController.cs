using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Net.Http;

namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {
        ExamenLeodegarioEntities3 d = new ExamenLeodegarioEntities3();
        
        public ActionResult Index()
        {
            return View(); 
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        
        public ActionResult CrearEmpleado()
        {
            return View(); 
        
       }
        public ActionResult Iniciar()
        {         
            return View();
        }
        [HttpPost]
        public ActionResult Iniciar(empleados empleado)
        {
          var inicio=  login(empleado.usuario, empleado.passusuario);
          if (inicio.id!=0) {
              if (inicio.tipo.ToLower() == "recursos humanos")
              {
                  return RedirectToAction("Recurso");
              }
              else {
                  return RedirectToAction("EditarEmpleado", "Home", new { model = inicio, id=inicio.id });  
              }
                       
          } else {
              return View();
          }            
        }
        public ActionResult Registrar()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CrearEmpleado(empleados empleado)
        {
            if (ModelState.IsValid)
            {
                d.Entry(empleado).State = EntityState.Added;
                d.SaveChanges();
                return RedirectToAction("Recurso");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Registrar(empleados empleado)
        {
            if (ModelState.IsValid)
            {
                d.Entry(empleado).State = EntityState.Added;
                d.SaveChanges();
                return RedirectToAction("Recurso");
            }
            return View();
        }
        
        public ActionResult EditarEmpleado(int id)
        {                   
            return View(getEmpleadoByID(id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarEmpleado([Bind(Include = "id,nombreCompleto,fechaNacimiento,tipo,domicilio,usuario,passusuario")] empleados empleado)
        {
            if (ModelState.IsValid)
            {
                d.Entry(empleado).State = EntityState.Modified;
                d.SaveChanges();
                return RedirectToAction("Recurso");
            }
            return View(empleado);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }
        public ActionResult DetalleEmpleado(int id)
        {
            return View(getEmpleadoByID(id));
        }

        public ActionResult Recurso()
        {
            List<empleados> Model = d.empleados.ToList();
            return View(Model);
        }
        public empleados getEmpleadoByID(int id){
            var em= d.empleados.ToList().Where(s => s.id == id).FirstOrDefault();
            return em;
        }
        public empleados login(String usuario, String pass)
        {
            var em= d.empleados.ToList().Where(s => s.usuario == usuario && s.passusuario == pass ).FirstOrDefault();
            return em;
        }

    }
}