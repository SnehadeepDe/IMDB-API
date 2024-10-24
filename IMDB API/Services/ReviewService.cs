using AutoMapper;
using IMDB_API.CustomException;
using IMDB_API.Models;
using IMDB_API.Models.Request_Models;
using IMDB_API.Models.Response_Models;
using IMDB_API.Repository.Interfaces;
using IMDB_API.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IMDB_API.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IMovieRepository _movieRepository;
        private readonly IMapper _mapper;

        public ReviewService(IReviewRepository reviewRepository, IMovieRepository movieRepository, IMapper mapper)
        {
            _reviewRepository = reviewRepository;
            _movieRepository = movieRepository;
            _mapper = mapper;
        }

        public List<ReviewResponse> GetAll(int movieId)
        {
            var reviews = _reviewRepository.GetAll(movieId);
            return _mapper.Map<List<ReviewResponse>>(reviews);
        }

        public ReviewResponse GetById(int movieId, int id)
        {
            var review = _reviewRepository.GetById(id);
            var movie = _movieRepository.GetById(movieId);
            if (review == null)
            {
                throw new NotFoundException("Review not found");
            }
            if (movie == null)
            {
                throw new NotFoundException("Movie not found");
            }
            if (review.MovieId != movieId)
            {
                throw new NotFoundException($"No review with Review Id = {id} under Movie Id = {movieId}");
            }
            return _mapper.Map<ReviewResponse>(review);
        }

        public int Create(ReviewRequest reviewRequest, int movieId)
        {
            var validationError = Validate(reviewRequest, movieId);
            if (!string.IsNullOrEmpty(validationError))
            {
                throw new BadRequestException(validationError);
            }
            var review = _mapper.Map<Review>(reviewRequest);
            review.MovieId = movieId;
            return _reviewRepository.Create(review);
        }
        public void Update(int id, ReviewRequest reviewRequest)
        {
            var reviewById = _reviewRepository.GetById(id);
            if (reviewById == null)
            {
                throw new NotFoundException("Review not found");
            }
            var validationError = Validate(reviewRequest, reviewRequest.MovieId);
            if (!string.IsNullOrEmpty(validationError))
            {
                throw new BadRequestException(validationError);
            }
            var review = _mapper.Map<Review>(reviewRequest);
            review.Id = id;
            _reviewRepository.Update(id, review);
        }
        public void Delete(int id)
        {
            var review = _reviewRepository.GetById(id);
            if (review == null)
            {
                throw new NotFoundException("Review not found");
            }
            _reviewRepository.Delete(id);
        }
        private string Validate(ReviewRequest reviewRequest, int movieId)
        {
            if (string.IsNullOrEmpty(reviewRequest.Message))
                return "Review message is required.";

            var movie = _movieRepository.GetById(movieId);
            if (movie == null)
                return "Movie not found.";

            return null;
        }
    }
}