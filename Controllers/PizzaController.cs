using ContosoPizza.Models;
using ContosoPizza.Services;
using Microsoft.AspNetCore.Mvc;

namespace ContosoPizza.Controllers;

[ApiController]
[Route("[controller]")]
public class PizzaController : ControllerBase
{
    private readonly PizzaService _pizzaService;

    public PizzaController(PizzaService pizzaService)
    {
        _pizzaService = pizzaService;
    }

    [HttpGet]
    public async Task<ActionResult<List<Pizza>>> GetAll() => await _pizzaService.GetAllAsync();

    [HttpGet("{id}")]
    public async Task<ActionResult<Pizza>> Get(int id)
    {
        var pizza = await _pizzaService.GetAsync(id);

        if (pizza is null)
            return NotFound();

        return pizza;
    }

    [HttpPost]
    public async Task<IActionResult> Create(Pizza pizza)
    {
        var created = await _pizzaService.AddAsync(pizza);
        return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Pizza pizza)
    {
        if (id != pizza.Id)
            return BadRequest();

        if (!await _pizzaService.UpdateAsync(pizza))
            return NotFound();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        if (!await _pizzaService.DeleteAsync(id))
            return NotFound();

        return NoContent();
    }
}
