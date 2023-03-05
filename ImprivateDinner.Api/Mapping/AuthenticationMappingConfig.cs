using ImprivateDinner.Application.Authentication.Commands.Register;
using ImprivateDinner.Application.Authentication.Common;
using ImprivateDinner.Application.Authentication.Queries.Login;
using ImprivateDinner.Contracts.Authentication;
using Mapster;

namespace ImprivateDinner.Api.Mapping;

public class AuthenticationMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<RegisterRequest, RegisterCommand>();
        config.NewConfig<LoginRequest, LoginQuery>();

        config.NewConfig<AuthenticationResult, AuthenticationResponse>()
            .Map(dest => dest, src => src.User);
            // above  code is same as below mappings
            // .Map(dest => dest.FirstName, src => src.User.FirstName)
            // .Map(dest => dest.LastName, src => src.User.LastName)
            // .Map(dest => dest.Email, src => src.User.Email)
            // .Map(dest => dest.Id, src => src.User.Id);
    }
}