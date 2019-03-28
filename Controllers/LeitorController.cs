using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LeitorImagem.Data;
using LeitorImagem.Models;
using Microsoft.AspNetCore.Mvc;

namespace LeitorImagem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeitorController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<Erro>> Get()
        {
            return null;
        }

        // GET api/values/5
        [HttpGet("/search")]
        public ActionResult<IEnumerable<Erro>> Get([FromQuery]string frase)
        {
            
            using(LeitorData data = new LeitorData())
            {
                return data.Read(frase);
            }
        }
    }
}
