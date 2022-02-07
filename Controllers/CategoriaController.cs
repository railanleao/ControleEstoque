using ControleEstoque.Connection;
using ControleEstoque.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ControleEstoque.Controllers
{
    public class CategoriaController : Controller
    {
        private readonly ContextDB _contextDB;

        public CategoriaController (ContextDB contextDB)
        {
            _contextDB = contextDB;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _contextDB.Categorias.ToListAsync());
        }

        //GET NovaCategoria
        [HttpGet]
        public IActionResult NovaCategoria()
        {
            return View();
        }
        //POST é o que de fato insere dados no banco
        [HttpPost]
        public async Task<IActionResult> NovaCategoria (Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                await _contextDB.Categorias.AddAsync(categoria);
                await _contextDB.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(categoria);
        }
        [HttpGet]
        //Nesse caso recebemos o catgId para justamente saber qual catg atualizar
        public async Task<IActionResult> AtualizarCategoria (int categoriaId)
        {
            //FindAsync é para encontrar uma catg baseado em um atributo, que é catgId
            return View(await _contextDB.Categorias.FindAsync(categoriaId));
        }

        [HttpPost]
        //Aqui é onde vai mandar as informações ao banco de dados
        public async Task<IActionResult> AtualizarCategoria (Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                _contextDB.Categorias.Update(categoria);
                await _contextDB.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(categoria);
        }
        [HttpPost]
        //Aqui vamos excluir a cat
        public async Task<IActionResult> ExcluirCategoria (int categoriaId)
        {
            Categoria? categoria = await _contextDB.Categorias.FindAsync(categoriaId);
            _contextDB.Categorias.Remove(categoria);
            await _contextDB.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

       
    }
}
