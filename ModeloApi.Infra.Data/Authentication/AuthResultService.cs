
using FluentValidation.Results;
using System.Text.Json.Serialization;

namespace ModeloApi.Infra.Data.Authentication;
public class AuthResultService
{
    [JsonIgnore]
    public bool IsSuccess { get; set; }
    public string? Message { get; set; }
    public ICollection<AuthErrorValidation>? Errors { get; set; }
    public object? Data { get; set; }

    public static AuthResultService RequestError(string message, ValidationResult validationResult)
    {
        return new AuthResultService
        {
            IsSuccess = false,
            Message = null,
            Errors = validationResult.Errors.Select(x => new AuthErrorValidation
            {
                Field = x.PropertyName,
                Message = x.ErrorMessage,
            }).ToList()
        };
    }

    public static AuthResultService Fail(string message) => new AuthResultService { IsSuccess = false, Message = message };

    public static AuthResultService Ok(string message) => new AuthResultService { IsSuccess = true, Message = message };

    public static AuthResultService SuccessWithData(object data) => new AuthResultService { IsSuccess = true, Data = data };
}
