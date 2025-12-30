using ContosoPizza.Data;
using ContosoPizza.Models;
using Microsoft.EntityFrameworkCore;

namespace ContosoPizza.Services;

public class PizzaService
{
    private readonly PizzaDbContext _db;

    public PizzaService(PizzaDbContext db)
    {
        _db = db;
    }

    public async Task<List<Pizza>> GetAllAsync() => await _db.Pizzas.AsNoTracking().ToListAsync();

    public async Task<Pizza?> GetAsync(int id) => await _db.Pizzas.FindAsync(id);

    public async Task<Pizza> AddAsync(Pizza pizza)
    {
        _db.Pizzas.Add(pizza);
        await _db.SaveChangesAsync();
        return pizza;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var pizza = await GetAsync(id);
        if (pizza is null)
            return false;
        _db.Pizzas.Remove(pizza);
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> UpdateAsync(Pizza pizza)
    {
        var exists = await _db.Pizzas.AnyAsync(p => p.Id == pizza.Id);
        if (!exists)
            return false;
        _db.Pizzas.Update(pizza);
        await _db.SaveChangesAsync();
        return true;
    }
}
