using Microsoft.AspNetCore.Mvc;
using ShopMvcApp_PV212.Models;

namespace ShopMvcApp_PV212.Controllers
{
    public class ContactsController : Controller
    {
        static List<Contact> contacts;

        public ContactsController()
        {
            contacts = new List<Contact>()
            {
                new Contact() { Id = 100, Name = "Vova", Phone = "+3943-535-35" },
                new Contact() { Id = 103, Name = "Olga", Phone = "+45-4-35" },
                new Contact() { Id = 130, Name = "Sofia", Phone = "+453-666-3533" }
            };  
        }

        public IActionResult Index()
        {
            // load data from db...

            return View(contacts);
        }
    }
}
