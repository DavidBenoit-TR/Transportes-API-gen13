using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Transportes_API_gen13.Models;
using Transportes_API_gen13.Services;

namespace Transportes_API_gen13.Controllers
{
    [Route("api/[controller]")]//Se declara el espacio de nombre
    [ApiController]//establece el trato del controlador
    public class CamionesController : ControllerBase
    {
        //variables para interfaz y el contexto
        private readonly ICamiones _service;
        private readonly TransportesContext _context;
        //constructor par ainicializar mi servicio y mi contexto
        //Dependency inyection
        public CamionesController(ICamiones service, TransportesContext context)
        {
            _service = service;
            _context = context;
        }

        //GET
        [HttpGet]
        [Route("getCamiones")]
        public List<Camiones_DTO> getCamiones()
        {
            //creo una lista de objeto DTO y la lleno con mi servicio
            List<Camiones_DTO> lista = _service.GetCamiones();
            return lista; //retorno la lista al exterior
        }

        //GET by ID
        [HttpGet]
        [Route("getCamion/{id}")]
        public Camiones_DTO getCamion(int id)
        {
            //creo un objeto DTO y lo lleno con mi servicio
            Camiones_DTO camion = _service.GetCamion(id);
            return camion; //retorno el objeto al exterior
        }

        //POST (insertar)
        [HttpPost]
        [Route("insertCamion")]
        //Los métodos IActionResult retornan una respuesta API en un formato establecido, capáz de ser leído por cualquier CLiente HTTP
        //por otro lado, la sentencia [FromBody] determina que existe contenido en el cuerpo de la petición
        public IActionResult insertCamion([FromBody] Camiones_DTO camion)
        {
            //consumo mi servicio
            string respuesta = _service.InsertCamion(camion);
            //retorno un nuevo objeto del tipo OK, siendo este un tipo de respuesta HTTP
            //se genera una nuevo objeto con la respuesta (new { respuesta }) para que esta tenga un formato de salida JSON y no texto plano
            return Ok(new { respuesta });
        }

        //PUT (actualizar)
        [HttpPut]
        [Route("updateCamion")]
        public IActionResult updateCamion([FromBody] Camiones_DTO camion)
        {
            string respuesta = _service.UpdateCamion(camion);
            return Ok(new { respuesta });
        }

        //DELETE
        [HttpDelete]
        [Route("delete/{id}")]
        public IActionResult deleteCamion(int id)
        {
            string respuesta = _service.DeleteCamion(id);
            return Ok(new { respuesta });
        }
    }
}
