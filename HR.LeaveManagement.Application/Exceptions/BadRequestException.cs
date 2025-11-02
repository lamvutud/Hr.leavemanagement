namespace HR.LeaveManagement.Application.Exceptions;

public class BadRequestException : Exception
{
    public BadRequestException(string message) : base(message)
    {

    }

    public BadRequestException(string message, FluentValidation.Results.ValidationResult result) : base(message)
    {
        ValidationErrors = [];

        foreach (var error in result.Errors)
        {
            ValidationErrors.Add(error.ErrorMessage);
        }
    }

    public List<string> ValidationErrors { get; set; } = [];
}

