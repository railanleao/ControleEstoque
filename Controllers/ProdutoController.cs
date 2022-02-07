using ControleEstoque.Connection;
using ControleEstoque.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;


namespace ControleEstoque.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly ContextDB _contextDB;

        public ProdutoController (ContextDB contextDB)
        {
            _contextDB = contextDB;
        }
        public async Task<IActionResult> Index()
        {//Include( p => p.Categoria)
            return View(await _contextDB.Produtos.Include(p => p.Categoria).ToListAsync());
        }
        [HttpGet]  
        public async Task<IActionResult> DetalhesProduto (int produtoId)
        {
            Produto? produto = await _contextDB.Produtos.Include(p => p.Categoria).FirstOrDefaultAsync(p => p.ProdutoId ==produtoId);
            return View(produto);
        }

        //GET NovaCategoria
        [HttpGet]
        public async Task<IActionResult> NovoProduto()
        {
            //Lista de SELEÇÃO/Para o usuário selecinar a qual categoria esse prod pertence
            ViewData ["CategoriaId"] = new SelectList(await _contextDB.Categorias.ToListAsync(), "CategoriaId", "Nome");
            return View();
        }
        //POST é o que de fato insere dados no banco
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> NovoProduto(Produto produto)
        {
            
            if (ModelState.IsValid)
            { 
                await _contextDB.Produtos.AddAsync(produto);
                await _contextDB.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //Para validar erro no ModelState
            //var errors = ModelState.Values.SelectMany(v => v.Errors);
            ViewData["CategoriaId"] = new SelectList(await _contextDB.Categorias.ToListAsync(), "CategoriaId", "Nome", produto.CategoriaId);
            return View(produto);            
        }
        
        [HttpGet]
        //Nesse caso recebemos o catgId para justamente saber qual catg atualizar
        public async Task<IActionResult> AtualizarProduto(int produtoId)
        {
            //FindAsync é para encontrar uma catg baseado em um atributo, que é catgId
            Produto? produto = await _contextDB.Produtos.FindAsync(produtoId);
            ViewData["CategoriaId"] = new SelectList(await _contextDB.Categorias.ToListAsync(), "CategoriaId", "Nome", produto.CategoriaId);
            return View(produto);
        }

        [HttpPost]
        //Aqui é onde vai mandar as informações ao banco de dados
        public async Task<IActionResult> AtualizarProduto(Produto produto)
        {
            if (ModelState.IsValid)
            {
                _contextDB.Produtos.Update(produto);
                await _contextDB.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoriaId"] = new SelectList(await _contextDB.Categorias.ToListAsync(), "CategoriaId", "Nome", produto.CategoriaId);
            return View(produto);
        }

        [HttpPost]
        //Aqui vamos excluir a cat
        public async Task<IActionResult> ExcluirProduto(int produtoId)
        {
            Produto? produto = await _contextDB.Produtos.FindAsync(produtoId);
            _contextDB.Produtos.Remove(produto);
            await _contextDB.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
