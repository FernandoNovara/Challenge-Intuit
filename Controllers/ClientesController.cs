using Challenge.Backend.DBContext;
using Challenge.Backend.Logs;
using Challenge.Backend.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Challenge.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly ChallengeDBContext _context;
        private readonly ILogServices<LogServices> _log;

        public ClientsController(ChallengeDBContext context,ILogServices<LogServices> log)
        {
            _context = context;
            _log = log;
        }

        /// <summary>
        /// Retrieves a list of all clients.
        /// </summary>
        [HttpGet("getClients")]
        public async Task<ActionResult<IEnumerable<Clients>>> GetClients()
        {
            try
            {
                return Ok(await _context.Clients.ToListAsync());
            }
            catch (Exception ex)
            {
                _log.LogError("Error retrieving clients: " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Error retrieving clients", error = ex.Message });
            }
        }

        /// <summary>
        /// Retrieves client data by their unique identifier.
        /// </summary>
        /// <param name="id">The ID of the client to retrieve.</param>
        /// <returns></returns>
        [HttpGet("getClient/{id}")]
        public async Task<ActionResult<Clients>> GetClient(int id)
        {
            try
            {
                var Clients = await _context.Clients.FindAsync(id);

                if (Clients == null)
                {
                    return NotFound(new { message = $"Client with ID {id} was not found." });
                }

                return Ok(Clients);
            }
            catch (Exception ex)
            {
                _log.LogError("Error retrieving client: " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError,new { message = "Error retrieving client", error = ex.Message });
            }
        }

        /// <summary>
        /// Retrieves client data by their name.
        /// </summary>
        /// <param name="name">The name of the client to retrieve.</param>
        /// <returns></returns>
        [HttpGet("getClientByName/{name}")]
        public async Task<ActionResult<Clients>> GetClientByName(string name)
        {
            try
            {
                
                var Clients = await _context.Clients.Where(e => e.Name!.Contains(name)).ToListAsync();

                if (Clients == null)
                {
                    return NotFound(new { message = $"Clients with name {name} was not found." });
                }
                _log.LogInfo("Successful client retrieve.");
                return Ok(Clients);
            }
            catch (Exception ex)
            {
                _log.LogError("Error retrieving clients: " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Error retrieving clients", error = ex.Message });
            }
        }

        /// <summary>
        /// Updates an existing client's information in the database.
        /// </summary>
        /// <param name="Clients">The client data to update, including a valid ClientId.</param>
        /// <returns></returns>
        [HttpPut("updateClient")]
        public async Task<IActionResult> UpdateClient(Clients Client)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { message = "Validation failed for the client data.", errors = ModelState });

            if (!ClientsExists(Client.ClientId))
            {
                return NotFound(new { message = $"Client with ID {Client.ClientId} was not found." });
            }
            
            _context.Entry(Client).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                
                return Ok(new { message = "Successful update client" });
            }
            catch (Exception ex)
            {
                _log.LogError("Error update client - " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Error update client", error = ex.Message });
            }
        }

        /// <summary>
        /// Adds a new client to the database.
        /// </summary>
        /// <param name="Clients">The client data to be added.</param>
        /// <returns></returns>
        [HttpPost("addClient")]
        public async Task<ActionResult<Clients>> AddClient(Clients Client)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { message = "Validation failed for the client data.", errors = ModelState });

            if (string.IsNullOrWhiteSpace(Client.Name) ||
                string.IsNullOrWhiteSpace(Client.LastName) ||
                string.IsNullOrWhiteSpace(Client.CUIT) ||
                string.IsNullOrWhiteSpace(Client.Email))
            {
                return BadRequest(new { message = "Required fields cannot be empty or whitespace." });
            }

            try
            {
                if (ClientsExists(Client.ClientId))
                {
                    return BadRequest(new { message = $"Client with ID {Client.ClientId} already exists." });
                }

                _context.Clients.Add(Client);
                await _context.SaveChangesAsync();

                return Ok(new { message = "Successful adding client" });
            }
            catch (Exception ex)
            {
                _log.LogError("Error adding client - " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Error adding client", error = ex.Message });
            }
        }

        /// <summary>
        /// Deletes a client from the database by their unique identifier.
        /// </summary>
        /// <param name="id">The ID of the client to delete.</param>
        /// <returns></returns>
        [HttpDelete("deleteClient/{id}")]
        public async Task<IActionResult> DeleteClient(int id)
        {
            try
            {
                var Client = await _context.Clients.FindAsync(id);
                if (Client == null)
                {
                    return NotFound(new { message = $"Client with ID {id} was not found." });
                }

                _context.Clients.Remove(Client);
                await _context.SaveChangesAsync();

                return Ok(new { message = "Client deleted successfully." });
            }
            catch (Exception ex)
            {
                _log.LogError("Error delete client - " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Error delete client", error = ex.Message });
            }
        }

        private bool ClientsExists(int id)
        {
            return _context.Clients.Any(e => e.ClientId == id);
        }
    }
}
