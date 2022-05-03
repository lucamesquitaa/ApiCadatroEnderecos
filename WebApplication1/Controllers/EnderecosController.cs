#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Correios;
using Correios.CorreiosServiceReference;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("v1/endereco")]
    [ApiController]
    public class EnderecosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EnderecosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Enderecos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Endereco>>> GetEnderecos()
        {
            return await _context.Enderecos.ToListAsync();
        }

        // GET: api/Enderecos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Endereco>> GetEndereco(int id)
        {
            var endereco = await _context.Enderecos.FindAsync(id);

            if (endereco == null)
            {
                return NotFound();
            }

            return endereco;
        }

        // PUT: api/Enderecos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEndereco(int id, Endereco endereco)
        {
            if (id != endereco.Id)
            {
                return BadRequest();
            }

            _context.Entry(endereco).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EnderecoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Enderecos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Endereco>> PostEndereco(Endereco endereco)
        {
            /*
             if(endereco.Cep != null)
             {
                using (var ws = new AtendeClienteClient())
                {
                    var retorno = ws.consultaCEP(endereco.Cep);

                    if (endereco.Rua == null)
                    {
                        string rua = retorno.end;
                    }
                    if (endereco.Bairro == null)
                    {
                        string bairro = retorno.bairro;
                    }
                    if (endereco.Cidade == null)
                    {
                        string cidade = retorno.cidade;
                    }
                    if (endereco.Uf == null)
                    {
                        string uf = retorno.uf;
                    }
                    var enderecoCompleto = new Endereco
                    {
                        Cep = endereco.Cep,
                        Rua = endereco.Rua != null ? endereco.Rua : retorno.end,
                        Bairro = endereco.Bairro != null ? endereco.Bairro : retorno.bairro,
                        Cidade = endereco.Cidade != null ? endereco.Cidade : retorno.cidade,
                        Uf = endereco.Uf != null ? endereco.Uf : retorno.uf
                    };
                    _context.Enderecos.Add(enderecoCompleto);
                    await _context.SaveChangesAsync();

                    return CreatedAtAction("GetEndereco", new { id = endereco.Id }, endereco);
                }*/
            if (endereco.Cep != null)
            {
                _context.Enderecos.Add(endereco);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetEndereco", new { id = endereco.Id }, endereco);
            }
             else
             {
                 return BadRequest();
             }
           
        }

        // DELETE: api/Enderecos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEndereco(int id)
        {
            var endereco = await _context.Enderecos.FindAsync(id);
            if (endereco == null)
            {
                return NotFound();
            }

            _context.Enderecos.Remove(endereco);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EnderecoExists(int id)
        {
            return _context.Enderecos.Any(e => e.Id == id);
        }
    }
}
