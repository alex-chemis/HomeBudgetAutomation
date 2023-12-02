using AutoMapper;
using HomeBudgetAutomation.Dtos;
using HomeBudgetAutomation.Dtos.Article;
using HomeBudgetAutomation.Dtos.Operation;
using HomeBudgetAutomation.Models;
using HomeBudgetAutomation.Repositories.Contract;
using HomeBudgetAutomation.ServiceResponder;
using HomeBudgetAutomation.Services.Contract;

namespace HomeBudgetAutomation.Services
{
    public class OperationsService : IOperationsService
    {
        private readonly IOperationsRepository _repository;
        private readonly IMapper _mapper;

        public OperationsService(IOperationsRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public ServiceResponse<OperationDto> Add(CreateOperationDto operation)
        {
            ServiceResponse<OperationDto> response = new();
            try
            {
                var newOperation = _mapper.Map<Operation>(operation);

                if (!_repository.Create(newOperation))
                {
                    response.Data = null;
                    response.Message = ServiceMessageType.InternalServerError;
                    return response;
                }

                response.Data = _mapper.Map<OperationDto>(newOperation);
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

        public ServiceResponse<string> DeleteById(int id)
        {
            ServiceResponse<string> response = new();
            try
            {
                if (!_repository.Delete(id))
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

        public ServiceResponse<List<OperationDto>> GetAll()
        {
            ServiceResponse<List<OperationDto>> response = new();
            try
            {
                var operations = _repository.GetAll();
                response.Data = new List<OperationDto>();

                foreach (var operation in operations)
                {
                    response.Data.Add(_mapper.Map<OperationDto>(operation));
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

        public ServiceResponse<OperationDto> GetById(int id)
        {
            ServiceResponse<OperationDto> response = new();
            try
            {
                response.Data = _mapper.Map<OperationDto>(_repository.Get(id));
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

        public ServiceResponse<OperationDto> Update(int id, UpdateOperationDto operation)
        {
            ServiceResponse<OperationDto> response = new();
            try
            {
                var updatedOperation = _mapper.Map<Operation>(operation);
                updatedOperation.Id = id;

                if (!_repository.Update(updatedOperation))
                {
                    response.Data = null;
                    response.Message = ServiceMessageType.NotFound;
                    return response;
                }

                response.Data = _mapper.Map<OperationDto>(updatedOperation);
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

        public ServiceResponse<List<OperationDto>> GetByDate(FunctionParamsDto functionParams)
        {
            ServiceResponse<List<OperationDto>> response = new();
            try
            {
                var operations = _repository.GetByDate(functionParams);
                response.Data = new List<OperationDto>();

                foreach (var operation in operations)
                {
                    response.Data.Add(_mapper.Map<OperationDto>(operation));
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
    }
}
