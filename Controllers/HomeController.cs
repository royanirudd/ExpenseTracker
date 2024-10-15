using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SpendSmart.Models;

namespace SpendSmart.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    private readonly SpendSmartDbContext _context;

    public HomeController(ILogger<HomeController> logger, SpendSmartDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult Expenses()
    {
        var allExpenses = _context.Expenses.ToList();

        var totalExpenses = allExpenses.Sum(x => x.Value);

        ViewBag.Expenses = totalExpenses;

        return View(allExpenses);
    }

    public IActionResult CreateEditExpense(int? id)
    {

        if (id != null)
        {
            //editing by preloading 

            var expenseInDb = _context.Expenses.SingleOrDefault(expense => expense.Id == id);
            return View(expenseInDb);
        }
        return View();
    }

    public IActionResult DeleteExpense(int id)
    {
        var expenseInDb = _context.Expenses.SingleOrDefault(expense => expense.Id == id);

        if (expenseInDb != null)
        {
            _context.Expenses.Remove(expenseInDb);
            _context.SaveChanges();
            TempData["SuccessMessage"] = "Expense deleted successfully!";
        }
        else
        {
            TempData["ErrorMessage"] = "Expense not found!";
        }

        return RedirectToAction("Expenses");
    }



    public IActionResult CreateEditExpenseForm(Expense model)
    {
        if (model.Id == 0)
        {
            //Create
            _context.Expenses.Add(model);
        }
        else
        {
            //Edit
            _context.Expenses.Update(model);
        }

        _context.SaveChanges(); //This step needs to be done everytime any changes are done

        return RedirectToAction("Expenses");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
