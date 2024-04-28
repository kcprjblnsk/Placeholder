using FluentValidation;
using URLStatus.Application.Exceptions;
using ValidationException = URLStatus.Application.Exceptions.ValidationException;

namespace URLStatus.WebAPI.Application.Response
{
    public class ValidationErrorResponse
    {
        public ValidationErrorResponse()
        {

        }

        public ValidationErrorResponse(ValidationException validationException)
        {
            if (validationException != null)
            {
                if (validationException.Errors != null)
                {
                    Errors = validationException.Errors.Select(e => new FieldValidationError()
                    {
                        Error = e.Error,
                        FieldName = e.FieldName
                    }).ToList();
                }
            };
        }

        public List<FieldValidationError> Errors { get; set; }= new List<FieldValidationError>();
        public class FieldValidationError
        {
            public required string FieldName { get; set; }
            public required string Error { get; set; }

        }
    }
}