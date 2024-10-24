using IMDB_API.Repository.Interfaces;
using Microsoft.AspNetCore.Server.IIS.Core;
using IMDB_API.CustomException;
using IMDB_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using IMDB_API.Models.Response_Models;
using IMDB_API.Models.Request_Models;
using IMDB_API.Services.Interfaces;
using AutoMapper;

namespace IMDB_API.Services
{
    public class ActorService : IActorService
    {
        private readonly IActorRepository _actorRepository;
        private readonly IMapper _mapper;

        public ActorService(IActorRepository actorRepository, IMapper mapper)
        {
            _actorRepository = actorRepository;
            _mapper = mapper;
        }
        public List<ActorResponse> GetAll(int? movieId)
        {
            var actors = _actorRepository.GetAll(movieId);
            return _mapper.Map<List<ActorResponse>>(actors);
        }
        public ActorResponse GetById(int id)
        {
            var actor = _actorRepository.GetById(id);
            if (actor == null)
            {
                throw new NotFoundException("Actor not found");
            }
            return _mapper.Map<ActorResponse>(actor);
        }
        public int Create(ActorRequest actorRequest)
        {
            var validationError = Validate(actorRequest);
            if (!string.IsNullOrEmpty(validationError))
            {
                throw new BadRequestException(validationError);
            }

            var actor = _mapper.Map<Actor>(actorRequest);
            actor.DOB = DateTime.Parse(actorRequest.DOB);
            return _actorRepository.Create(actor);
        }
        public void Update(int id, ActorRequest actorRequest)
        {
            GetById(id);
            var validationError = Validate(actorRequest);
            if (!string.IsNullOrEmpty(validationError))
            {
                throw new BadRequestException(validationError);
            }
            var actor = _mapper.Map<Actor>(actorRequest);
            actor.Id = id;
            actor.DOB = DateTime.Parse(actorRequest.DOB);
            _actorRepository.Update(id, actor);
        }
        public void Delete(int id)
        {
            GetById(id);
            _actorRepository.Delete(id);
        }
        private string Validate(ActorRequest actorRequest)
        {
            if (string.IsNullOrEmpty(actorRequest.Name))
                return "Actor name is required.";

            if (!DateTime.TryParse(actorRequest.DOB, out var dob))
                return "Invalid date of birth provided.";

            var year = dob.Year;
            var month = dob.Month;
            var day = dob.Day;
            var daysInMonth = DateTime.DaysInMonth(year, month);

            if (dob == default(DateTime) || (year > DateTime.Now.Year || year < 1600) || (month < 1 || month > 12) || (day < 1 || day > daysInMonth))
                return "Invalid date of birth.";

            if (string.IsNullOrEmpty(actorRequest.Bio))
                return "Bio is required";

            if (string.IsNullOrEmpty(actorRequest.Gender))
                return "Gender is required";

            return null;
        }
    }
}
