using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AgendaTelefonica.Models;
using AgendaTelefonica.Models.ViewModels;

namespace AgendaTelefonica.Controllers
{
    public class ContactosController : Controller
    {
        // GET: Contactos
        public ActionResult Index()
        {
            // se crea el objeto listado
            List<ListContactsViewModel> listado;

            // Se usa using para comunicarse con el modelo y recuperar información de la BD
            using (AgendaEntities1 db = new AgendaEntities1())
            {
                // Se hace una consulta utilizando el ListContactsViewModel y lo convierte en una lista
                listado = (from d in db.contactos
                               select new ListContactsViewModel
                               {
                                   Id_contacto = d.id_contacto,
                                   Nombre = d.nombre,
                                   Telefono = d.telefono,
                                   Email = d.email
                               }).ToList();
            }
            return View(listado);
        }

        // REGISTRAR
        public ActionResult Nuevo()
        {
            // Sólo para mostrar la vista Nuevo
            return View();
        }

        // Para registrar un nuevo Contacto
        [HttpPost]
        public ActionResult Nuevo(NewContactsViewModel model)
        {
            try
            {
                // El if verifica que los datos sean correctos como se determinó en NewContactsViewModel
                if (ModelState.IsValid)
                {
                    using (AgendaEntities1 db = new AgendaEntities1())
                    {
                        // Se crea un objeto de tipo contactos para realizar el registro
                        var objNuevo = new contactos();
                        objNuevo.nombre = model.Nombre;
                        objNuevo.telefono = model.Telefono;
                        objNuevo.email = model.Email;

                        // Se agrega el objeto a la base de datos y se guardan los cambios
                        db.contactos.Add(objNuevo);
                        db.SaveChanges();
                    }

                    //MessagesBox
                    return Redirect("~/Contactos");
                }
                return View(model);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // EDITAR REGISTROS
        public ActionResult Editar(int Id)
        {
            // Obtiene la información del registro a Editar y la envía a la vista
            NewContactsViewModel model = new NewContactsViewModel();
            using (AgendaEntities1 db = new AgendaEntities1())
            {
                var objContact = db.contactos.Find(Id);
                model.Id_contacto = objContact.id_contacto;
                model.Nombre = objContact.nombre;
                model.Telefono = objContact.telefono;
                model.Email = objContact.email;
            }
            return View(model);
        }

        // Para actualiza/editar un nuevo Contacto
        [HttpPost]
        public ActionResult Editar(NewContactsViewModel model)
        {
            try
            {
                // El if verifica que los datos sean correctos como se determinó en NewContactsViewModel
                if (ModelState.IsValid)
                {
                    using (AgendaEntities1 db = new AgendaEntities1())
                    {
                        // Se crea un objeto de tipo contactos para realizar el registro
                        var objContact = db.contactos.Find(model.Id_contacto);
                        objContact.nombre = model.Nombre;
                        objContact.telefono = model.Telefono;
                        objContact.email = model.Email;

                        // Se agrega el objeto a la base de datos y se guardan los cambios
                        db.Entry(objContact).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }

                    //MessagesBox
                    return Redirect("~/Contactos");
                }
                return View(model);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // ELIMINAR REGISTROS
        public ActionResult Eliminar(int Id)
        {
            // Obtiene la información del registro a Eliminar
            using (AgendaEntities1 db = new AgendaEntities1())
            {
                var objContact = db.contactos.Find(Id);
                db.contactos.Remove(objContact);
                db.SaveChanges();
            }
            return Redirect("~/Contactos");
        }


        // BUSCAR REGISTROS
        public ActionResult Buscar(string stringBuscar)
        {
            List<ListContactsViewModel> contactos;

            // Se usa using para comunicarse con el modelo y recuperar información de la BD
            using (AgendaEntities1 db = new AgendaEntities1())
            {
                // Se hace una consulta utilizando el ListContactsViewModel y lo convierte en una lista
                // esto porque la busqueda puede contener varios elementos
                contactos = (from d in db.contactos
                             where d.nombre.Contains(stringBuscar) || d.telefono.Contains(stringBuscar) || d.email.Contains(stringBuscar)
                             select new ListContactsViewModel
                             {
                                 Id_contacto = d.id_contacto,
                                 Nombre = d.nombre,
                                 Telefono = d.telefono,
                                 Email = d.email
                             }).OrderBy(d => d.Nombre).ToList();
            }
            // Envía un mensaje a la vista, sólo para dar referencia de lo que se buscó
            ViewData["Mensaje"] = stringBuscar;
            return View(contactos);
        }
    }
}