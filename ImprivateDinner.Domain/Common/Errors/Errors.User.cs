using ErrorOr;

namespace ImprivateDinner.Domain.Common.Errors;

public static partial class Errors
{
    public static class User
    {
        public static Error DuplicateEmail => Error.Conflict(
            code: "User.DuplicateEmail",
            description: "Email is already in use."
        );

        public static Error Missing => Error.Conflict(
            code: "User.Missing",
            description: "User is not exist"
        );
        public static Error InvalidCredentials => Error.Validation(
            code: "User.InvalidCredentials",
            description: "Credentials are invalid for user"
        );
        
    }
}