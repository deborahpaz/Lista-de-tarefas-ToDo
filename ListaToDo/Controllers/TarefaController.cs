using Microsoft.AspNetCore.Mvc;
using ListaToDo.Models;

namespace ListaToDo.Controllers;

public class TarefaController : Controller
{
    private static List<Tarefa> _tarefa = new List<Tarefa>()
    {
        new Tarefa {TarefaId = 1, NomeTarefa = "Entregar exercício do treinamento", DataInicioTarefa = DateTime.Parse("09/05/2024"), StatusTarefa = true, DataFinalTarefa = DateTime.Parse("10/05/2024") },
        new Tarefa {TarefaId = 2, NomeTarefa = "Estudar MVC C# utilizando CRUD", DataInicioTarefa = DateTime.Parse("03/05/2024")},
        new Tarefa {TarefaId = 3, NomeTarefa = "Terminar projeto final do treinamento", DataInicioTarefa = DateTime.Parse("01/05/2024")},
        new Tarefa {TarefaId = 4, NomeTarefa = "Responder formulário avaliativo", DataInicioTarefa = DateTime.Parse("10/05/2024"), StatusTarefa = true, DataFinalTarefa = DateTime.Parse("10/05/2024")} 

    };

    public IActionResult Index()
    {
        return View(_tarefa);
    }

    [HttpGet] // anotação para PEGAR
    public IActionResult Create() // chama o form de cadastro
    {
        return View();
    }

    [HttpPost] // anotação para ENVIAR
    public IActionResult Create(Tarefa tarefa) //recebe os dados do form
    {
        if (ModelState.IsValid)
        {
            /* Tem cliente? Se sim, no maior Id (o último) o novo cliente vai
        receber o maior mais um. Se não, o id vai ser 1 (meio que vazio)
        */
            tarefa.TarefaId = _tarefa.Count > 0 ? _tarefa.Max(p => p.TarefaId) + 1 : 1;
            _tarefa.Add(tarefa);
        }
        return RedirectToAction("Index");
    
    }

    public IActionResult Delete(int id)
    {
        var tarefa = _tarefa.FirstOrDefault(t => t.TarefaId == id);
        if (tarefa == null)
        {
            return NotFound();
        }
        _tarefa.Remove(tarefa);
        return RedirectToAction("Index");
    }

    public IActionResult Finalizar(int id)
    {
        var tarefa = _tarefa.FirstOrDefault(t => t.TarefaId == id);
        if (tarefa == null)
        {
            return NotFound();
        }

        tarefa.StatusTarefa = true;
        tarefa.DataFinalTarefa = DateTime.Now;
        return RedirectToAction("Index");
    }

    public IActionResult Iniciar(int id)
    {
        var tarefa = _tarefa.FirstOrDefault(t => t.TarefaId == id);
        if (tarefa == null)
        {
            return NotFound();
        }

        tarefa.StatusTarefa = false;
        tarefa.DataFinalTarefa = null;
        return RedirectToAction("Index");
    }

    public IActionResult Edit(int id)
    {
        var tarefa = _tarefa.FirstOrDefault(t => t.TarefaId == id);
        if (tarefa == null)
        {
            return NotFound();
        }

        return View(tarefa);
    }

    [HttpPost]
    public IActionResult Edit(Tarefa tarefa)
    {
        if (ModelState.IsValid)
        {
            var existingTarefa = _tarefa.FirstOrDefault(t => t.TarefaId == tarefa.TarefaId);
            if (existingTarefa != null)
            {
                existingTarefa.NomeTarefa = tarefa.NomeTarefa;
            }
            return RedirectToAction("Index");
        }
        return View(tarefa);
    }

    public IActionResult Concluida()
    {
        var _tarefaConcluida = _tarefa.Where(t => t.StatusTarefa == true).ToList();
        return View(_tarefaConcluida);
    }

    public IActionResult AFazer()
    {
        var _tarefaAFazer = _tarefa.Where(t => t.StatusTarefa == false).ToList();
        return View(_tarefaAFazer);
    }


}
