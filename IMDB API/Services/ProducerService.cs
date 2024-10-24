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
    public class ProducerService : IProducerService
    {
        private readonly IProducerRepository _producerRepository;
        private readonly IMapper _mapper;

        public ProducerService(IProducerRepository producerRepository, IMapper mapper)
        {
            _producerRepository = producerRepository;
            _mapper = mapper;
        }

        public List<ProducerResponse> GetAll()
        {
            var producers = _producerRepository.GetAll();
            return _mapper.Map<List<ProducerResponse>>(producers);
        }

        public ProducerResponse GetById(int id)
        {
            var producer = _producerRepository.GetById(id);
            if (producer == null)
            {
                throw new NotFoundException("Producer not found");
            }
            return _mapper.Map<ProducerResponse>(producer);
        }

        public int Create(ProducerRequest producerRequest)
        {
            var validationError = Validate(producerRequest);
            if (!string.IsNullOrEmpty(validationError))
            {
                throw new BadRequestException(validationError);
            }

            var producer = _mapper.Map<Producer>(producerRequest);
            producer.DOB = DateTime.Parse(producerRequest.DOB);
            return _producerRepository.Create(producer);
        }

        public void Update(int id, ProducerRequest producerRequest)
        {
            GetById(id);
            var validationError = Validate(producerRequest);
            if (!string.IsNullOrEmpty(validationError))
            {
                throw new BadRequestException(validationError);
            }

            var producer = _mapper.Map<Producer>(producerRequest);
            producer.Id = id;
            producer.DOB = DateTime.Parse(producerRequest.DOB);
            _producerRepository.Update(id, producer);
        }

        public void Delete(int id)
        {
            GetById(id);
            _producerRepository.Delete(id);
        }

        private string Validate(ProducerRequest producerRequest)
        {
            if (string.IsNullOrEmpty(producerRequest.Name))
                return "Producer name is required.";

            if (!DateTime.TryParse(producerRequest.DOB, out var dob))
                return "Invalid date of birth provided.";

            var year = dob.Year;
            var month = dob.Month;
            var day = dob.Day;
            var daysInMonth = DateTime.DaysInMonth(year, month);

            if (dob == default(DateTime) || (year > DateTime.Now.Year || year < 1600) || (month < 1 || month > 12) || (day < 1 || day > daysInMonth))
                return "Invalid date of birth.";

            if (string.IsNullOrEmpty(producerRequest.Bio))
                return "Bio is required";

            if (string.IsNullOrEmpty(producerRequest.Gender))
                return "Gender is required";

            return null;
        }
    }
}