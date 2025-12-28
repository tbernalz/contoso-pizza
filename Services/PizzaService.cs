using ContosoPizza.Models;

namespace ContosoPizza.Services;

public class PizzaService
{
    List<Pizza> Pizzas { get; }
    int nextId = 3;

    public PizzaService()
    {
        Pizzas = new List<Pizza>
        {
            new Pizza
            {
                Id = 1,
                Name = "Margherita",
                IsAvailable = true,
            },
            new Pizza
            {
                Id = 2,
                Name = "Pepperoni",
                IsAvailable = true,
            },
        };
    }

    public List<Pizza> GetAll() => Pizzas;

    public Pizza? Get(int id) => Pizzas.FirstOrDefault(p => p.Id == id);

    public void Add(Pizza pizza)
    {
        pizza.Id = nextId++;
        Pizzas.Add(pizza);
    }

    public void Delete(int id)
    {
        var pizza = Get(id);
        if (pizza is null)
            return;

        Pizzas.Remove(pizza);
    }

    public void Update(Pizza pizza)
    {
        var index = Pizzas.FindIndex(p => p.Id == pizza.Id);
        if (index == -1)
            return;

        Pizzas[index] = pizza;
    }
}
