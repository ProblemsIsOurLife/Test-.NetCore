
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Testquest.entities;
using Testquest.Models;
using static Testquest.Models.ApplicationContext;

namespace Testquest.Controllers
{
    [Route("api/Peoples")]
    [ApiController]
    public class PeopleController : Controller
    {

        List<People> peoples;
        ApplicationContext db ;
        public PeopleController(ApplicationContext context)
        {
            //create data in db if its empty
            db = context;
            if (!db.Specs.Any())
            {
                db.Specs.Add(new Specs {  Name = "Программист" });
                db.Specs.Add(new Specs { Name = "Учитель" });
                db.Specs.Add(new Specs { Name = "Врач" });
                db.Specs.Add(new Specs {  Name = "Водитель" });
               
            }
            if (!db.Peoples.Any())
            {
                db.Peoples.Add(new People {  Name = "Andrew", Surname = "Marrv", Birthday = "11.11", gender = "M",Spec = "Программист" });
                db.Peoples.Add(new People { Name = "Kail", Surname = "Spel", Birthday = "5.4", gender = "M", Spec = "Учитель" });
                db.Peoples.Add(new People {  Name = "Karin", Surname = "Apol", Birthday = "8.6", gender = "F", Spec = "Врач" });
                db.Peoples.Add(new People { Name = "Karin2", Surname = "Apol", Birthday = "8.6", gender = "F", Spec = "Врач" });
                db.Peoples.Add(new People { Name = "Karin3", Surname = "Apol", Birthday = "8.6", gender = "F", Spec = "Врач" });
            }
            db.SaveChanges();

        }

        



      
        // GET api/People
        [HttpGet]
        public IEnumerable<People> Get()
        {
            return db.Peoples.ToList();
        }
        

        // GET api/values/5
        [HttpGet("{id}")]
        public People Get(int id)
        {
            People people = db.Peoples.FirstOrDefault(x => x.ID == id);
            return people;
        }
        //Get api/People/pagenumber
        [HttpGet("page/{pageid}")]
        public IEnumerable<People> GetPage(int pageid)
        {
            List<People> people = db.Peoples.ToList();
            int count = 0;
            if (db.Peoples.Count() > 3)
            {
                count = db.Peoples.Count() / 3;
                if (count != 3 && people.Count % 3 ==1)
                {
                    count++;
                }
            }
            else
                count = 1;

            if (pageid <= count)
            {
                int vcount = 0;
                List<People> temppeople = new List<People>();
                if (pageid != 1)
                {
                    pageid = pageid * 3 - 2;
                }
                foreach (People people1 in db.Peoples)
                {
                    if (count == 1)
                    {
                        temppeople.Add(people[(pageid - 1) * 3]);
                        pageid++;
                        if (pageid == 3)
                        {
                            break;
                        }
                    }
                    else
                    {
                        try
                        {
                            temppeople.Add(people[(pageid - 1)]);
                        }
                        catch (Exception) { break; }


                        pageid++;
                        vcount++;
                        if (vcount == 3)
                        {
                            vcount = 0;
                            break;
                        }
                    }

                }
                return temppeople;
            }

            else
                return new List<People>();
        }
        //filtration of List
        public IEnumerable<People> filtration(string filter, string filtrdata)
        {
            int k = 0;//schetchik
            int kolv = db.Specs.Count();

            List<People> temppeople = new List<People>();
            switch (filter)
            {
                case "Birthday":
                    {
                        foreach (People peoples in db.Peoples)
                        {
                            if (peoples.Birthday == filtrdata)
                            {
                                temppeople.Add(peoples);
                            }

                        }
                        if (temppeople.Count != 0)
                            return temppeople;
                        else
                            return temppeople.DefaultIfEmpty();
                    }
                case "gender":
                    {
                        foreach (People peoples in db.Peoples)
                        {
                            if (peoples.gender == filtrdata)
                            {
                                temppeople.Add(peoples);
                            }

                        }
                        return temppeople;
                    }
                case "Spec":
                    {
                        foreach (People peoples in db.Peoples)
                        {
                            if (peoples.gender == filtrdata)
                            {
                                temppeople.Add(peoples);
                            }

                        }
                        return temppeople;
                    }
                default:
                    {
                        return temppeople.DefaultIfEmpty();
                    }

            }
        }
        //GET api/filter/filtername:typefilter on one page
        [HttpGet("filter/{filter}:{filtrdata}")]
        public IEnumerable<People> Get(string filter, string filtrdata)
        {
            return filtration(filter, filtrdata);
        }
        //GET api/filter/filtername:typefilter/pagenumber
        [HttpGet("filter/{filter}:{filtrdata}/{id}")]
        public IEnumerable<People> Get(string filter,string filtrdata, int id)
        {
            List <People> people = filtration(filter, filtrdata).ToList();
            int count = 0;
            if (people.Count > 3)
            {
                count = people.Count / 3;
                if (count != 3 && people.Count % 3 ==1)
                {
                    count++;
                }
            }
            else
                count = 1;

            if(id<=count)
            {
                int vcount = 0;
                List<People> temppeople = new List<People>();
                if (id != 1)
                {
                    id = id*3-2;
                }
                foreach (People people1 in people)
                {
                    if(count ==1)
                    {
                        temppeople.Add(people[(id - 1) * 3]);
                        id++;
                        if(id == 3)
                        {
                            break;
                        }
                    }
                    else
                    {
                        try
                        {
                            temppeople.Add(people[(id - 1)]);
                        }
                        catch (Exception) { break; }


                        id++;
                        vcount++;
                        if (vcount == 3)
                        {
                            vcount = 0;
                            break;
                        }
                        }
                 
                }
                return temppeople;
            }
                    
             else
            return new List<People>();
        }
        
        // POST api/People
        [HttpPost]
        public IActionResult Post(People people)
        {
            if (ModelState.IsValid)
            {
                db.Peoples.Add(people);
                db.SaveChanges();
                return Ok(people);
            }
            return BadRequest(ModelState);
        }

        // PUT api/People/id
        [HttpPut("{id}")]
        public IActionResult Put(People people)
        {
            if (ModelState.IsValid)
            {
                db.Update(people);
                db.SaveChanges();
                return Ok(people);
            }
            return BadRequest(ModelState);
        }

        // DELETE api/People/id
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            People product = db.Peoples.FirstOrDefault(x => x.ID == id);
            if (product != null)
            {
                db.Peoples.Remove(product);
                db.SaveChanges();
            }
            return Ok(product);
        }
    }
}
