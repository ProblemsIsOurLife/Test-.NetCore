using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Testquest.entities;
using Testquest.Models;


namespace Testquest.Controllers
{
    [Route("api/Specs")]
    [ApiController]
    public class SpecsController : Controller
    {

        List<Specs> specs;
        ApplicationContext db;
        public SpecsController(ApplicationContext context)
        {
            db = context;
            if (!db.Specs.Any())
            {
                db.Specs.Add(new Specs { Name = "Программист" });
                db.Specs.Add(new Specs { Name = "Учитель" });
                db.Specs.Add(new Specs { Name = "Врач" });
                db.Specs.Add(new Specs { Name = "Водитель" });

            }
            db.SaveChanges();

        }
       
        // GET api/values
        [HttpGet]
        public IEnumerable<Specs> Get()
        {
            return db.Specs.ToList();
        }


        // GET api/Specs/id
        [HttpGet("{id}")]
        public Specs Get(int id)
        {
            Specs specs = db.Specs.FirstOrDefault(x => x.ID == id);
            return specs;
        }


        // POST api/Specs/id
        [HttpPost]
        public IActionResult Post(Specs people)
        {
            if (ModelState.IsValid)
            {
                db.Specs.Add(people);
                db.SaveChanges();
                return Ok(people);
            }
            return BadRequest(ModelState);
        }

        // PUT api/Specs/id
        [HttpPut("{id}")]
        public IActionResult Put(Specs people)
        {
            if (ModelState.IsValid)
            {
                db.Update(people);
                db.SaveChanges();
                return Ok(people);
            }
            return BadRequest(ModelState);
        }

        // DELETE api/Specs/id
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Specs product = db.Specs.FirstOrDefault(x => x.ID == id);
            if (product != null)
            {
                db.Specs.Remove(product);
                db.SaveChanges();
            }
            return Ok(product);
        }
    }
}
