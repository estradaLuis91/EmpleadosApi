using System.Collections.Generic;
using System.Threading.Tasks;
using EmpleadosApi.Domain.Entities;
using EmpleadosApi.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EmpleadosApi.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmpleadoController : ControllerBase
    {
        private readonly IEmpleadoRepository _repository;

        public EmpleadoController(IEmpleadoRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Empleado>>> GetAll(int? numeroEmp)
        {
            if (numeroEmp.HasValue)
            {
                var empleado = await _repository.GetByIdAsync(numeroEmp.Value);
                if (empleado == null) return NotFound();
                return Ok(empleado);
            }

            var empleados = await _repository.GetAllAsync();
            return Ok(new { empleados });
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Empleado empleado)
        {
            await _repository.AddAsync(empleado);
            return CreatedAtAction(nameof(GetAll), new { id = empleado.NumeroEmp }, empleado);
        }

        [HttpPut("{numEmpleado}")]
        public async Task<IActionResult> Update(int numEmpleado, [FromBody] Empleados empleado)
        {
            var existingEmpleado = await _repository.GetByIdAsync(numEmpleado);
            if (existingEmpleado == null)
                return NotFound($"No se encontró un empleado con el número {numEmpleado}.");

            await _repository.UpdateAsync(empleado);
            return NoContent();
        }

        [HttpDelete("{numEmpleado}")]
        public async Task<IActionResult> Delete(int numEmpleado)
        {
            var existingEmpleado = await _repository.GetByIdAsync(numEmpleado);
            if (existingEmpleado == null)
                return NotFound($"No se encontró un empleado con el número {numEmpleado}.");

            await _repository.DeleteAsync(numEmpleado);
            return NoContent();
        }
    }
}
