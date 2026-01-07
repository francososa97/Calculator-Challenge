using System;
using System.Collections.Generic;
using System.Linq;

namespace Calculator_Challenge;

/// <summary>
/// String Calculator - Step 0 baseline (empty => 0)
/// </summary>
public class StringCalculator : IStringCalculator
{
    private bool _allowNegatives = true;
    private int _upperBound = 1000;
    private readonly List<string> _baseDelimiters = new() { ",", "\n" };

    /// <summary>
    /// Step 1: Soporte para coma como delimitador, máximo 2 números.
    /// Entradas inválidas o faltantes se tratan como 0.
    /// </summary>
    public int Add(string input)
    {
        if (string.IsNullOrEmpty(input)) return 0;

        // Dividir por coma
        var parts = input.Split(',');

        // Procesar máximo los primeros 2 números
        int sum = 0;
        for (int i = 0; i < parts.Length && i < 2; i++)
        {
            var part = parts[i].Trim();
            if (int.TryParse(part, out var num))
            {
                sum += num;
            }
            // Si no es número válido, se trata como 0 (suma no cambia)
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

