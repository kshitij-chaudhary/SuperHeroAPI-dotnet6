using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using SuperHeroAPI.Data;
using SuperHeroAPI.Models;

namespace SuperHeroAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        /// <summary>
        /// created a hardcoded data in form of list .....later on this data is stored in database and then all the processes are done on the database 
        /// </summary>
        //private static  List<SuperHero> heroes = new List<SuperHero>
        //    {
        //        //new SuperHero
        //        //{
        //        //    Id = 1,
        //        //    Name = "Spiderman",
        //        //    FirstName = "Peter",
        //        //    LastName = "Parker",
        //        //    Place = "New York"
        //        //},

        //         new SuperHero
        //        {
        //            Id = 2,
        //            Name = "IronMan",
        //            FirstName = "Tony",
        //            LastName = "Stark",
        //            Place = "Long Island"
        //        }
        //    };


        private readonly DataContext context;

        public SuperHeroController(DataContext context)
        {
            
            this.context = context;

            
        }

        /// <summary>
        /// Http get method gives us the all heroes that are present in data base
        /// </summary>
        
        [HttpGet]
        public async  Task<ActionResult<List<SuperHero>>> Get()
        {
            //without database we use this and use this with hard coded data
            // return Ok(heroes);

            //with database
            return (await context.SuperHeros.ToListAsync());
        }


        /// <summary>
        /// this Http method gives us the hero specific to the id 
        /// </summary>
       
        [HttpGet("{id}")]
        public async Task<ActionResult<SuperHero>> Get(int id)
        {
            //without database and with hard coded data
            //var hero = heroes.Find(x => x.Id == id);  
            //if(hero == null)
            //{
            //    return BadRequest("Cannot find hero with this id");
            //}
            //else
            //{
            //    return Ok(hero);
            //}

            //with database

            var hero = await context.SuperHeros.FindAsync(id);
            if (hero == null)
            {
                return BadRequest("Cannot find hero with this id");
            }
            else
            {
                return Ok(hero);
            }
        }


        /// <summary>
        /// Post method of http gives us the freedom to add new hero to our database 
        /// </summary>
        
        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> AddHero(SuperHero hero)
        {
            //without database and with hard coded data
            //heroes.Add(hero);
            //return Ok(heroes);

            //with database
            context.SuperHeros.Add(hero);
            await context.SaveChangesAsync();

            return Ok(await context.SuperHeros.ToListAsync());
        }


        /// <summary>
        /// Put method of Http gives us the freedom to edit a hero that we search based on the id
        /// </summary>

        [HttpPut]
        public async Task<ActionResult<List<SuperHero>>> UpdateHero(SuperHero newHero)
        {
            //with hardcoded data this is used
            //var hero = heroes.Find(h => h.Id == newHero.Id);
            //if (hero == null)
            //{
            //    return BadRequest("no hero available with this id");

            //}
            //else
            //{
            //    hero.Name = newHero.Name;
            //    hero.FirstName = newHero.FirstName;
            //    hero.LastName = newHero.LastName;
            //    hero.Place = newHero.Place;
            //    return Ok(heroes);
            //}

            //with database
            var dbhero = await context.SuperHeros.FindAsync(newHero.Id);
            if (dbhero == null)
            {
                return BadRequest("no hero available with this id");

            }
            else
            {
                dbhero.Name = newHero.Name;
                dbhero.FirstName = newHero.FirstName;
                dbhero.LastName = newHero.LastName;
                dbhero.Place = newHero.Place;
                await context.SaveChangesAsync();
                return Ok(await context.SuperHeros.ToListAsync());
            }
        }

        /// <summary>
        /// Delete method of Http deletes the hero from database acc. to the id we give and then return all the remaining heroes 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<SuperHero>>> Delete(int id) 
        {
            //we use this for hard coded data
            //var hero = heroes.Find(x =>x.Id == id);
            //if(hero == null)
            //{
            //    return NotFound("SuperHero with this id is not available");
            //}
            //else 
            //{
            //    heroes.Remove(hero);
            //    return (heroes);
            //}

            //with database

            var dbhero = await context.SuperHeros.FindAsync(id);
            if (dbhero == null)
            {
                return BadRequest("no hero available with this id");

            }
            else
            {
                 context.SuperHeros.Remove(dbhero);
                await context.SaveChangesAsync();
                return Ok(await context.SuperHeros.ToListAsync());
            }
        }
    }
}
