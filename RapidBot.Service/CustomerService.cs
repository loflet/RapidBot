using RapidBot.Data;
using RapidBot.Data.Infrastructure;
using RapidBot.Data.Repositories;
using RapidBot.Service.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
namespace RapidBot.Service
{
    public interface ICustomerService
    {
        IEnumerable<CustomerDto> GetAllCustomers();
        CustomerDto GetCustomerById(int id);
        CustomerDto GetCustomerByEmail(string email);
    }
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository customerRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public CustomerService(ICustomerRepository _customerRepository, IUnitOfWork _unitOfWork, IMapper _mapper)
        {
            customerRepository = _customerRepository;
            unitOfWork = _unitOfWork;
            mapper = _mapper;
        }

        public IEnumerable<CustomerDto> GetAllCustomers()
        {
            IEnumerable<Customer> customersBO = customerRepository.GetAll();
            return mapper.Map<IEnumerable<Customer>, IEnumerable<CustomerDto>>(customersBO);
        }

        public CustomerDto GetCustomerById(int id)
        {
            Customer customerBO = customerRepository.GetById(id);
            return mapper.Map<Customer, CustomerDto>(customerBO);
        }

        public CustomerDto GetCustomerByEmail(string email)
        {
            Customer customerBO = customerRepository.Get(c => c.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
            return mapper.Map<Customer, CustomerDto>(customerBO);
        }
    }
}
