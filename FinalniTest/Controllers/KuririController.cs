using AutoMapper;
using AutoMapper.QueryableExtensions;
using FinalniTest.Interfaces;
using FinalniTest.Models;
using FinalniTest.Models.DTO;
using FinalniTest.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace FinalniTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KuririController : ControllerBase
    {
        private readonly IKuririRepository _kuririRepository;

        public KuririController(IKuririRepository kuririoRepository)
        {
            _kuririRepository = kuririoRepository;
        }

        [HttpGet]
        [Route("/api/kuriri")]
        public IActionResult GetKuriri()
        {
            return Ok(_kuririRepository.GetAll());
        }

        [HttpGet("/api/kuriri/{id}")]
        public IActionResult GetKurir(int id)
        {
            var kurir = _kuririRepository.GetById(id);
            if (kurir == null)
            {
                return NotFound();
            }
            return Ok(kurir);
        }

        [Authorize]
        [HttpGet]
        [Route("/api/dostave")]
        public IActionResult GetDostave(decimal granica)
        {
            return Ok(_kuririRepository.GetDostave(granica));
        }

        [Authorize]
        [HttpGet]
        [Route("/api/kuriri/nadji")]
        public IActionResult GetKuririByIme(string ime)
        {
            return Ok(_kuririRepository.GetAllByIme(ime));
        }

        [Authorize]
        [HttpGet]
        [Route("/api/stanje")]
        public IActionResult GetStanje()
        {
            return Ok(_kuririRepository.GetBrojPaketa());
        }

        [Authorize]
        [HttpDelete]
        [Route("/api/kuriri/{id}")]
        public IActionResult DeleteKurir(int id)
        {
            var kurir = _kuririRepository.GetById(id);
            if (kurir == null)
            {
                return NotFound();
            }

            _kuririRepository.Delete(kurir);
            return NoContent();
        }

        [Authorize]
        [HttpPost]
        [Route("/api/kuriri")]
        public IActionResult PostKurir([FromBody] Kurir kurir)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _kuririRepository.Add(kurir);
            return CreatedAtAction("GetKurir", new { id = kurir.Id }, kurir);
        }

        [Authorize]
        [HttpPut("/api/kuriri/{id}")]
        public IActionResult PutKurir(int id, Kurir kurir)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != kurir.Id)
            {
                return BadRequest();
            }

            try
            {
                _kuririRepository.Update(kurir);
            }
            catch
            {
                return BadRequest();
            }

            return Ok(kurir);
        }
    }
}
