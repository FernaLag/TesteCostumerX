using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TesteCostumerX.Data;
using TesteCostumerX.Models;

namespace TesteCostumerX.Controllers
{
    public class ContatosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ContatosController(ApplicationDbContext context)
        {
            _context = context;
        }

        #region Métodos GET

        // GET: Contatos       
        public async Task<IActionResult> Index()
        {
            //Cria uma Lista com todos os Contatos do Banco
            var Contatos = _context.Contato.ToList();

            //Para cada Contato, adiciona o cliente que ele está vinculado
            foreach (var c in Contatos)
            {
                c.Cliente = _context.Cliente.Find(c.FK_Cliente);
            }

            //Retorna para a View Contatos/Index com a Lista de Contatos e seus Clientes vinculados
            return View(await _context.Contato.ToListAsync());
        }

        // GET: Contatos/Details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //Procura o primeiro contato no Banco com o ID desejado
            var contato = await _context.Contato
                .FirstOrDefaultAsync(m => m.ID_Contato == id);
            if (contato == null)
            {
                return NotFound();
            }

            //Adiciona a esse Contato o Cliente que ele está vinculado
            contato.Cliente = _context.Cliente.Find(contato.FK_Cliente);

            //Retorna para a View Contatos/Details
            return View(contato);
        }

        // GET: Contatos/Create
        public IActionResult Create()
        {
            //Salva em ViewBag uma lista de Clientes para ser utilizada no Select dos Clientes
            ViewBag.Clientes = _context.Cliente.ToList();

            //Retorna para a View Contatos/Create
            return View();
        }
               
        // GET: Contatos/Edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //Procura o contato no banco com o ID desejado
            var contato = await _context.Contato.FindAsync(id);
            if (contato == null)
            {
                return NotFound();
            }

            //Salva em ViewBag uma lista de Clientes para ser utilizada no Select dos Clientes
            ViewBag.Clientes = _context.Cliente.ToList();

            //Retorna para a View Contatos/Edit com o Contato desejado
            return View(contato);
        }

        // GET: Contatos/Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //Procura o primeiro Contato no banco com o ID desejado
            var contato = await _context.Contato
                .FirstOrDefaultAsync(m => m.ID_Contato == id);

            if (contato == null)
            {
                return NotFound();
            }

            //Retornar para a View Contatos/Delete com o Contato desejado
            return View(contato);
        }

        #endregion

        #region Métodos POST

        // POST: Contatos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Contato contato)
        {
            //Vincula ao Contato o Cliente Selecionado
            contato.Cliente = _context.Cliente.Find(contato.FK_Cliente);

            //Verifica se o Contato está válido para ser salvo no banco
            if (ModelState.IsValid)
            {
                //Adiciona o contato ao Banco
                _context.Add(contato);

                //Salva alterações
                await _context.SaveChangesAsync();

                //Retorna para a View Contatos/Index
                return RedirectToAction(nameof(Index));
            }
            //Se não for válido para alterações, Retorna para a View Create
            return View(contato);
        }

        // POST: Contatos/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID_Contato,NM_Contato,Email,Telefone,FK_Cliente")] Contato contato)
        {
            if (id != contato.ID_Contato)
            {
                return NotFound();
            }
            else
            {
                //Adiciona oa Contato o Cliente vinculado a ele
                contato.Cliente = _context.Cliente.Find(contato.FK_Cliente);
            }

            //Verifica se o Contato está válido para ser alterado no banco
            if (ModelState.IsValid)
            {
                try
                {
                    //Faz alterações desse contato no Banco
                    _context.Update(contato);

                    //Salva as alterações
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContatoExists(contato.ID_Contato))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                //Retorna para a View Contatos/Index
                return RedirectToAction(nameof(Index));
            }

            //Se o Contato não estiver válido para alterações, retorna para a View Contatos/Create
            return View(contato);
        }

        // POST: Contatos/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //Procura no banco o Contato com o ID desejado
            var contato = await _context.Contato.FindAsync(id);

            //Remove o contato do Banco
            _context.Contato.Remove(contato);

            //Salva as alterações no banco
            await _context.SaveChangesAsync();

            //Retorna para a View Contatos/Delete
            return RedirectToAction(nameof(Index));
        }

        #endregion

        private bool ContatoExists(int id)
        {
            return _context.Contato.Any(e => e.ID_Contato == id);
        }
    }
}
