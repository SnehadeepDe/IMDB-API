using IMDB_API.CustomException;
using IMDB_API.Models.Request_Models;
using IMDB_API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace IMDB_API.Controllers
{
    [ApiController]
    [Route("/movies/{movieId}/[controller]")]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewService _reviewService;
        public ReviewsController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpGet]
        public IActionResult GetAll([FromRoute] int movieId)
        {
            try
            {
                var reviews = _reviewService.GetAll(movieId);
                if (reviews.Any())
                    return Ok(reviews);
                else
                    return Ok(new { message = "No reviews found" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int movieId, [FromRoute] int id)
        {
            try
            {
                var review = _reviewService.GetById(movieId, id);
                return Ok(review);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPost]
        public IActionResult Create([FromRoute] int movieId, [FromBody] ReviewRequest reviewRequest)
        {
            try
            {
                var id = _reviewService.Create(reviewRequest, movieId);
                return CreatedAtAction(nameof(GetById), new { movieId = movieId, id = id }, new { id = id });
            }
            catch (BadRequestException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPut("/[controller]/{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] ReviewRequest reviewRequest)
        {
            try
            {
                _reviewService.Update(id, reviewRequest);
                return Ok();
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (BadRequestException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpDelete("/[controller]/{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            try
            {
                _reviewService.Delete(id);
                return Ok(new { message = $"Review with Id {id} deleted" });
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}