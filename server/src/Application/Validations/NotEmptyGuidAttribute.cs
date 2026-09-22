using System.ComponentModel.DataAnnotations;

namespace Application.Validations;

/// <summary>
/// Valida que um <see cref="Guid"/> (ou <see cref="Nullable{Guid}"/>) não seja
/// <see cref="Guid.Empty"/>. A ausência do valor (null) deve continuar sendo
/// tratada por <see cref="RequiredAttribute"/> quando o campo for obrigatório.
/// </summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
public sealed class NotEmptyGuidAttribute : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        // Deixa a validação de obrigatoriedade para [Required]; aqui só barramos o Guid vazio.
        if (value is null) return true;

        return value is Guid guid && guid != Guid.Empty;
    }
}
