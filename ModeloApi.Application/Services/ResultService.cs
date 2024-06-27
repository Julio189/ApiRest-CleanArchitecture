
using FluentValidation.Results;
using System.Text.Json.Serialization;

namespace ModeloApi.Application.Services;
public class ResultService
{
    [JsonIgnore]
    public bool IsValid { get; set; }
    [JsonIgnore]
    public bool IsFound { get; set; }
    public string? Message { get; set; }
    public ICollection<ErrorValidation>? Errors { get; set; }

    public static ResultService RequestError(string message, ValidationResult validationResult)
    {
        return new ResultService
        {
            IsValid = false,
            Message = null,
            Errors = validationResult.Errors.Select(x => new ErrorValidation
            {
                Field = x.PropertyName,
                Message = x.ErrorMessage,
            }).ToList()
        };
    }

    public static ResultService<T> RequestError<T>(string message, ValidationResult validationResult)
    {
        return new ResultService<T>
        {
            IsValid = false,
            Message = null,
            Errors = validationResult.Errors.Select(x => new ErrorValidation
            {
                Field = x.PropertyName,
                Message = x.ErrorMessage,
            }).ToList()
        };
    }

    public static ResultService Fail(string message) => new ResultService { IsValid = false, Message = message };

    public static ResultService<T> Fail<T>(string message) => new ResultService<T> { IsValid = false, Message = message };
    
    public static ResultService NotFound(string message) => new ResultService { IsFound = false, Message = message };

    public static ResultService<T> NotFound<T>(string message) => new ResultService<T> { IsFound = false, Message = message };

    public static ResultService Ok(string message) => new ResultService { IsFound = true, IsValid = true ,Message = message };

    public static ResultService<T> Ok<T>(T data) => new ResultService<T> { IsFound = true, IsValid = true, Data = data };

}

public class ResultService<T> : ResultService
{
    public T? Data { get; set; }
}
