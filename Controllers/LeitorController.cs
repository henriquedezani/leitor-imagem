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
            using(LeitorData data = new LeitorData())
            {
                return data.Read();
            }
        }

        // GET api/values/5
        [HttpGet("search")]
        public ActionResult<IEnumerable<Erro>> Get([FromQuery]string frase)
        {
            string[] palavras = frase.Split(' '); 

            using(LeitorData data = new LeitorData())
            {
                return data.Read(palavras);
            }
        }
    }
}
