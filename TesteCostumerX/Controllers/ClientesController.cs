using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TesteCostumerX.Data;
using TesteCostumerX.Models;

namespace TesteCostumerX.Controllers
{
    public class ClientesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClientesController(ApplicationDbContext context)
        {
            _context = context;
        }

        #region Métodos GET

        // GET: Clientes
        public async Task<IActionResult> Index()
        {
            //Retorna para a View Index com a Lista de Clientes do Banco
            return View(await _context.Cliente.ToListAsync());
        }

        // GET: Clientes/Details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //Procura no Banco o primeiro Cliente com o ID desejado
            var cliente = await _context.Cliente
                .FirstOrDefaultAsync(m => m.ID_Cliente == id);

            if (cliente == null)
            {
                return NotFound();
            }

            //Adiciona ao cliente os Contatos que estão vinculados a ele
            var contatos = _context.Contato.Where(m => m.FK_Cliente == id).ToList();
            cliente.Contatos_Cliente = contatos;

            //Retorna para a View Details
            return View(cliente);
        }

        // GET: Clientes/Create
        public IActionResult Create()
        {
            return View();
        }

        // GET: Clientes/Edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //Procura no banco o Cliente com o ID desejado
            var cliente = await _context.Cliente.FindAsync(id);

            //Se não encontrar, retorna que não encontrou
            if (cliente == null)
            {
                return NotFound();
            }

            //Retorna para a View Edit com o Cliente para ser Editado
            return View(cliente);
        }

        // GET: Clientes/Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //Procura o primeiro Cliente com o ID desejada no Banco
            var cliente = await _context.Cliente
                .FirstOrDefaultAsync(m => m.ID_Cliente == id);

            if (cliente == null)
            {
                return NotFound();
            }

            //Retorna para a VIew de Delete com o Cliente desejado
            return View(cliente);
        }

        #endregion

        #region Métodos POST

        // POST: Clientes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID_Cliente,NM_Cliente,Email,Telefone")] Cliente cliente)
        {
            //Verifica se o novo Cliente está válido para ser inserido no banco
            if (ModelState.IsValid)
            {
                //Adiciona ao Banco
                _context.Add(cliente);

                //Salva as Alterações
                await _context.SaveChangesAsync();

                //Retorna para a View Cliente/Index
                return RedirectToAction(nameof(Index));
            }
            return View(cliente);
        }

        // POST: Clientes/Edit/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID_Cliente,NM_Cliente,Email,Telefone")] Cliente cliente)
        {
            if (id != cliente.ID_Cliente)
            {
                return NotFound();
            }

            //Verifica se o Cliente é válido para ser alterado no Banco
            if (ModelState.IsValid)
            {
                try
                {
                    //Faz as alterações desse Cliente no Banco
                    _context.Update(cliente);

                    //Não faz alterações na Data de Cadastro do Cliente
                    _context.Entry(cliente).Property(x => x.DT_Cad).IsModified = false;

                    //Salva as alterações
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClienteExists(cliente.ID_Cliente))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                //Retorna para a Cliente/Index
                return RedirectToAction(nameof(Index));
            }

            //Se o Cliente não está válido para alterações, retorna para o Edit
            return View(cliente);
        }

        // POST: Clientes/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //Remove todos os contatos desse Cliente
            var contatos = _context.Contato.Where(m => m.FK_Cliente == id);
            _context.Contato.RemoveRange(contatos);

            //Remove o cliente
            var cliente = await _context.Cliente.FindAsync(id);
            _context.Cliente.Remove(cliente);

            //Salve as alterações
            await _context.SaveChangesAsync();

            //Retorna para a Cliente/Index
            return RedirectToAction(nameof(Index));
        }

        #endregion

        private bool ClienteExists(int id)
        {
            return _context.Cliente.Any(e => e.ID_Cliente == id);
        }
    }
}