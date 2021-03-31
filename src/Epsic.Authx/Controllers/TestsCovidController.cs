using System;
using System.Linq;
using System.Threading.Tasks;
using Epsic.Authx.Data;
using Epsic.Authx.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Epsic.Authx.Controllers
{
    [ApiController]
    public class TestsCovidController : ControllerBase
    {
        private readonly CovidDbContext _context;

        public TestsCovidController(CovidDbContext context)
        {
            _context = context;
        }


        // GET: testsCovid/E04832F9-6006-4E2F-8816-64E709BE38C0
        [HttpGet("testsCovid/{id}")]
        public async Task<IActionResult> Details(Guid id)
        {
            var testCovid = await _context.TestsCovid.FirstOrDefaultAsync(m => m.Id == id);
            if (testCovid == null) return NotFound();
            return Ok(testCovid);
        }

        // DELETE: testsCovid/E04832F9-6006-4E2F-8816-64E709BE38C0
        [HttpDelete("testsCovid/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var testCovid = await _context.TestsCovid.FirstOrDefaultAsync(m => m.Id == id);
            _context.Remove(testCovid);
            return NoContent();
        }

        // GET: testsCovid
        [HttpGet("testsCovid")]
        public async Task<IActionResult> Get()
        {
            var testsCovid = await _context.TestsCovid.Select(t => new { t.Id, t.DateTest, t.Resultat }).ToListAsync();
            return Ok(testsCovid);
        }

        // GET: testsCovid/stats
        [HttpGet("testsCovid/stats")]
        public async Task<IActionResult> GetStats([FromQuery] DateTime? dateFrom, [FromQuery] DateTime? dateTo)
        {
            var stats = await _context.TestsCovid.Where(t => dateFrom == null || t.DateTest >= dateFrom)
                .Where(t => dateTo == null || t.DateTest <= dateTo)
                .GroupBy(t => new
                {
                    t.TypeDeTest,
                    t.Resultat
                })
                .Select(t => new
                {
                    TypeDeTest = t.Key.TypeDeTest.ToString(),
                    t.Key.Resultat,
                    Nombre = t.Count()
                })
                .ToListAsync();

            return Ok(stats);
        }

        // POST: testsCovid
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("testsCovid")]
        [Authorize("MedecinOnly")]
        public async Task<IActionResult> Create(TestCovidDto testCovid)
        {
            var testCovidDb = new TestCovid
            {
                Id = Guid.NewGuid(),
                DateTest = testCovid.DateTest,
                Resultat = testCovid.Resultat,
                TypeDeTest = testCovid.TypeDeTest,
            };
            _context.Add(testCovidDb);
            await _context.SaveChangesAsync();
            return Created($"TestsCovid/{testCovidDb.Id}", testCovidDb);
        }
    }
}