using AutoMapper;
using HomeBudgetAutomation.Dtos.Article;
using HomeBudgetAutomation.Dtos.Balance;
using HomeBudgetAutomation.Models;
using HomeBudgetAutomation.Repositories.Contract;
using HomeBudgetAutomation.ServiceResponder;
using HomeBudgetAutomation.Services.Contract;
using NuGet.Protocol.Core.Types;

namespace HomeBudgetAutomation.Services
{
    public class BalancesService : IBalancesService
    {
        private readonly IBalancesRepository _repository;
        private readonly IMapper _mapper;

        public BalancesService(IBalancesRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public ServiceResponse<string> SoftDelete(int id)
        {
            ServiceResponse<string> response = new();
            try
            {
                if (!_repository.SoftDelete(id))
                {
                    response.Data = null;
                    response.Message = ServiceMessageType.NotFound;
                    return response;
                }

                response.Message = ServiceMessageType.Ok;
            }
            catch (Exception ex)
            {
                response.Data = null;
                response.Message = ServiceMessageType.InternalServerError;
                response.ErrorMessages = new List<string>() { Convert.ToString(ex.Message) };
            }
            return response;
        }

        public ServiceResponse<BalanceDto> Form(FormBalanceDto balance)
        {
            ServiceResponse<BalanceDto> response = new();
            try
            {
                var newBalance = _mapper.Map<Balance>(balance);

                if (!_repository.Create(newBalance))
                {
                    response.Data = null;
                    response.Message = ServiceMessageType.InternalServerError;
                    return response;
                }

                response.Data = _mapper.Map<BalanceDto>(newBalance);
                response.Message = ServiceMessageType.Ok;
            }
            catch (Exception ex)
            {
                response.Data = null;
                response.Message = ServiceMessageType.InternalServerError;
                response.ErrorMessages = new List<string>() { Convert.ToString(ex.Message) };
            }
            return response;
        }

        public ServiceResponse<List<BalanceDto>> GetAll()
        {
            ServiceResponse<List<BalanceDto>> response = new();
            try
            {
                var balances = _repository.GetAll();
                response.Data = new List<BalanceDto>();

                foreach (var balance in balances)
                {
                    response.Data.Add(_mapper.Map<BalanceDto>(balance));
                }

                response.Message = ServiceMessageType.Ok;
            }
            catch (Exception ex)
            {
                response.Data = null;
                response.Message = ServiceMessageType.InternalServerError;
                response.ErrorMessages = new List<string>() { Convert.ToString(ex.Message) };
            }
            return response;
        }

        public ServiceResponse<BalanceDto> GetById(int id)
        {
            ServiceResponse<BalanceDto> response = new();
            try
            {
                response.Data = _mapper.Map<BalanceDto>(_repository.Get(id));
                response.Message = ServiceMessageType.Ok;
            }
            catch (Exception ex)
            {
                response.Data = null;
                response.Message = ServiceMessageType.InternalServerError;
                response.ErrorMessages = new List<string>() { Convert.ToString(ex.Message) };
            }
            return response;
        }
    }
}
