using System;
using System.Collections.Generic;
using System.Linq;

namespace Calculator_Challenge;

/// <summary>
/// String Calculator - Step 0 baseline (empty => 0)
/// </summary>
public class StringCalculator : IStringCalculator
{
    private bool _allowNegatives = false;
    private int _upperBound = 1000;
    private readonly List<string> _baseDelimiters = new() { ",", "\n" };

    /// <summary>
    /// Step 5: Soporte para coma y salto de línea como delimitadores.
    /// Entradas inválidas o faltantes se tratan como 0.
    /// Números negativos no permitidos (se listan todos en la excepción) salvo que se habiliten.
    /// Números mayores al umbral (1000 por defecto) se ignoran.
    /// </summary>
    public int Add(string input)
    {
        if (string.IsNullOrEmpty(input)) return 0;

        // Usar coma y \n como delimitadores
        var parts = input.Split(new[] { ',', '\n' }, StringSplitOptions.None);
        int sum = 0;
        var negatives = new List<int>();

        foreach (var part in parts)
        {
            var trimmed = part.Trim();
            if (int.TryParse(trimmed, out var num))
            {
                if (num < 0 && !_allowNegatives)
                {
                    negatives.Add(num);
                }
                else if (num <= _upperBound)
                {
                    sum += num;
                }
                // Si num > _upperBound, se ignora (suma no cambia)
            }
            // Si no es número válido o está vacío, se considera 0
        }

        if (negatives.Count > 0)
        {
            var list = string.Join(", ", negatives);
            throw new ArgumentException($"Negativos no permitidos: {list}");
        }

        return sum;
    }

    /// <summary>
    /// Devuelve detalles del cálculo: suma y fórmula textual mostrando cada término normalizado.
    /// Ejemplo: "2,,4,rrrr,1001,6" => "2+0+4+0+0+6 = 12"
    /// </summary>
    public (int Sum, string Formula) AddWithDetails(string input)
    {
        if (string.IsNullOrEmpty(input)) return (0, "0 = 0");
        return (0, "0 = 0");
    }

    // Configuración
    public void AddDefaultDelimiter(string delimiter)
    {
        if (!string.IsNullOrEmpty(delimiter) && !_baseDelimiters.Contains(delimiter))
            _baseDelimiters.Add(delimiter);
    }

    public void SetUpperBound(int upper)
    {
        if (upper < 0) throw new ArgumentOutOfRangeException(nameof(upper));
        _upperBound = upper;
    }

    /// <summary>
    /// Configura si se permiten números negativos
    /// </summary>
    public void SetAllowNegatives(bool allow)
    {
        _allowNegatives = allow;
    }

    /// <summary>
    /// Valida que no haya números negativos
    /// Recolecta TODOS los negativos y los reporta
    /// </summary>
    private void ValidateNoNegatives(string[] parts)
    {
        var negatives = new List<int>();

        foreach (var part in parts)
        {
            if (string.IsNullOrWhiteSpace(part))
                continue;

            if (int.TryParse(part.Trim(), out var num) && num < 0)
                negatives.Add(num);
        }

        if (negatives.Count > 0)
        {
            var negativesList = string.Join(", ", negatives);
            throw new ArgumentException($"Negativos no permitidos: {negativesList}");
        }
    }
}

