using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

[ApiController]
[Route("[controller]")]
public class ClientsController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly ILogger<ClientsController> _logger;

    public ClientsController(AppDbContext context, ILogger<ClientsController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Client>> GetClients()
    {
        _logger.LogInformation("Getting all clients");
        return _context.Clients.ToList();
    }

    [HttpGet("{id}")]
    public ActionResult<Client> GetClient(int id)
    {
        _logger.LogInformation($"Getting client with id {id}");
        var client = _context.Clients.Find(id);

        if (client == null)
        {
            _logger.LogWarning($"Client with id {id} not found");
            return NotFound();
        }

        return client;
    }

    [HttpPost]
    public ActionResult<Client> PostClient(Client client)
    {
        _logger.LogInformation("Creating a new client");
        _context.Clients.Add(client);
        _context.SaveChanges();

        return CreatedAtAction(nameof(GetClient), new { id = client.Id }, client);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateClient(int id, [FromBody] Client client)
    {
        _logger.LogInformation($"Updating client with id {id}");
        var existingClient = await _context.Clients.FindAsync(id);
        if (existingClient == null)
        {
            _logger.LogWarning($"Client with id {id} not found");
            return NotFound();
        }

        existingClient.Nombre = client.Nombre;
        existingClient.Edad = client.Edad;
        existingClient.Correo = client.Correo;
        existingClient.Numero = client.Numero;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ClientExists(id))
            {
                _logger.LogWarning($"Client with id {id} not found");
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    private bool ClientExists(int id)
    {
        return _context.Clients.Any(e => e.Id == id);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteClient(int id)
    {
        _logger.LogInformation($"Deleting client with id {id}");
        var client = _context.Clients.Find(id);

        if (client == null)
        {
            _logger.LogWarning($"Client with id {id} not found");
            return NotFound();
        }

        _context.Clients.Remove(client);
        _logger.LogInformation($"Client with id {id} deleted");

        _context.SaveChanges();

        return NoContent();
    }
}

