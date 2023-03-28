using AutoMapper;
using AutoMapper.QueryableExtensions;
using FinalniTest.Interfaces;
using FinalniTest.Models;
using FinalniTest.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace FinalniTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaketiController : ControllerBase
    {
        private readonly IPaketiRepository _paketiRepository;
        private readonly IMapper _mapper;

        public PaketiController(IPaketiRepository paketioRepository, IMapper mapper)
        {
            _paketiRepository = paketioRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("/api/paketi")]
        public IActionResult GetPaketi()
        {
            return Ok(_paketiRepository.GetAll().ProjectTo<PaketDTO>(_mapper.ConfigurationProvider).ToList());
        }


        [HttpGet("/api/paketi/{id}")]
        public IActionResult GetPaket(int id)
        {
            var kurir = _paketiRepository.GetById(id);
            if (kurir == null)
            {
                return NotFound();
            }
            return Ok(kurir);
        }

        [Authorize]
        [HttpPost]
        [Route("/api/paketi")]
        public IActionResult PostPaket([FromBody] Paket paket)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _paketiRepository.Add(paket);
            return CreatedAtAction("GetPaket", new { id = paket.Id }, paket);
        }

        [Authorize]
        [HttpPut("/api/paketi/{id}")]
        public IActionResult PutPaket(int id, Paket paket)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != paket.Id)
            {
                return BadRequest();
            }

            try
            {
                _paketiRepository.Update(paket);
            }
            catch
            {
                return BadRequest();
            }

            return Ok(paket);
        }

        [Authorize]
        [HttpDelete("/api/paketi/{id}")]
        public IActionResult DeletePaket(int id)
        {
            var paket = _paketiRepository.GetById(id);
            if (paket == null)
            {
                return NotFound();
            }

            _paketiRepository.Delete(paket);
            return NoContent();
        }
        [Authorize]
        [HttpPost]
        [Route("/api/pretraga")]
        public IActionResult GetPaketiByTezina([FromBody] TezineDTO tezine)
        {
            return Ok(_paketiRepository.GetAllByTezina(tezine).ProjectTo<PaketDTO>(_mapper.ConfigurationProvider).ToList());
        }
        [Authorize]
        [HttpGet]
        [Route("/api/paketi/trazi")]
        public IActionResult GetPaketiByPrijem([FromQuery] string prijem)
        {
            return Ok(_paketiRepository.GetAllByPrijem(prijem).ProjectTo<PaketDTO>(_mapper.ConfigurationProvider).ToList());
        }
    }
}
