using Microsoft.AspNetCore.Mvc;
using Backend.Models.Database.Entities;
using Backend.Models.Dtos;
using Backend.Models.Interfaces;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewsController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateReview([FromBody] ReviewCreateDTO reviewDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _reviewService.CreateReviewAsync(reviewDto);
                return Ok(new { message = "Reseña creada exitosamente." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllReviews()
        {
            IEnumerable<ReviewDTO> reviews = await _reviewService.GetAllReviewAsync();
            return Ok(reviews);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetReviewById(Guid id)
        {
            Review review = await _reviewService.GetReviewByIdAsync(id);
            if (review == null)
            {
                return NotFound();
            }
            return Ok(review);
        }

        [HttpGet("accommodation/{accommodationId}")]
        public async Task<IActionResult> GetReviewsByAccommodationId(Guid accommodationId)
        {
            var reviewsResponse = await _reviewService.GetReviewsByAccommodationIdAsync(accommodationId);

            if (reviewsResponse == null || !reviewsResponse.Any())
            {
                return Ok(new { message = "No hay reseñas disponibles para este alojamiento." });
            }

            return Ok(reviewsResponse);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReview(Guid id)
        {
            try
            {
                await _reviewService.DeleteReviewAsync(id);
                return Ok(new { message = "Reseña eliminada exitosamente." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}

