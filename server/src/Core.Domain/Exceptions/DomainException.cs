namespace Core.Domain.Exceptions;

/// <summary>
/// Representa a violação de uma regra de negócio do domínio.
/// Deve ser traduzida pela camada de API em um retorno adequado (ex.: 409 Conflict).
/// </summary>
public class DomainException : Exception
{
    public DomainException(string message) : base(message)
    {
    }
}
