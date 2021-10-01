using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using WebApplicationTI.Models;


namespace WebApplicationTI.Controllers
{
    public class LoginController : Controller
    {
        private bdTIEntities db = new bdTIEntities();

        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Validar(Cuenta cuenta) {
            try
            {
                if (cuenta.Usuario != null && cuenta.Contrasenia != null)
                {
                    var userDetalle = (from userlist in db.Cuenta
                                       where userlist.Usuario == cuenta.Usuario && userlist.Contrasenia == cuenta.Contrasenia
                                       select new
                                       {
                                           userlist.IdCuenta,
                                           userlist.Usuario,
                                           userlist.Contrasenia,
                                           userlist.Nombre,
                                       }).ToList();
                    int idUsuerio = userDetalle.FirstOrDefault().IdCuenta;

                    if (userDetalle == null)
                    {
                        ModelState.AddModelError("cuenta", "Usuario o Contraseña incorrecto");
                        return View("Login", cuenta);
                    }
                    else
                    {
                        Session["IdCuenta"] = idUsuerio;
                        Session["Usuario"] = userDetalle.FirstOrDefault().Usuario;
                        Session["Nombre"] = userDetalle.FirstOrDefault().Nombre;
                        return RedirectToAction("Index", "Home");
                    }
                }
                return View();
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Cuenta invalidad CATCH");
                return View();
            }
        }

        // GET: OutLogin
        public ActionResult Salir()
        {
            int userID = (int)Session["IdCuenta"];
            Session.Abandon();
            return RedirectToAction("Login", "Login");
        }
    }
}