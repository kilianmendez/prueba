using Backend.Models.Dtos;
using Backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly ReservationService _reservationService;

        public ReservationController(ReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        [HttpPost("CreateReservation")]
        public async Task<IActionResult> CreateReservation([FromBody] ReservationCreateRequest request)
        {
            var result = await _reservationService.CreateReservationAsync(request);
            if (result == null)
                return StatusCode(500, "Error al crear la reserva.");
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetReservationById(Guid id)
        {
            var reservation = await _reservationService.GetReservationByIdAsync(id);
            if (reservation == null)
                return NotFound("Reserva no encontrada.");
            return Ok(reservation);
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllReservations()
        {
            var reservations = await _reservationService.GetAllReservationsAsync();
            return Ok(reservations);
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> UpdateReservation(Guid id, ReservationUpdateRequest request)
        {
            var updated = await _reservationService.UpdateReservationAsync(id, request);
            if (updated == null)
                return NotFound("Reserva no encontrada o no se pudo actualizar.");
            return Ok(updated);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteReservation(Guid id)
        {
            bool deleted = await _reservationService.DeleteReservationAsync(id);
            if (!deleted)
                return NotFound("Reserva no encontrada.");
            return Ok("Reserva eliminada.");
        }


    }
}
