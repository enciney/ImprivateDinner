namespace ImprivateDinner.Infrastructure.Authentication;

// init keyword is being used to make immutable properties, so they can be set only one time
// ! has another meaning in c# 8 that forgiving operator, so with null! usage you are sayin to the compiler
// "They can be a null so ignore it"
public class JwtSettings
{
    public const string SectionName = nameof(JwtSettings);
    public string Secret { get; init; } = null!; 
    public int ExpiryMinutes { get; init; }  
    public string Issuer { get; init; } = null!;   
    public string Audience { get; init; } = null!;   
}