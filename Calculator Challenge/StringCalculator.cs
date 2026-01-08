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
    /// Step 7: Soporte para delimitador custom de múltiples caracteres: //[delim]\n
    /// Ejemplo: //[***]\n11***22***33 => 66
    /// </summary>
    public int Add(string input)
    {
        if (string.IsNullOrEmpty(input)) return 0;

        // Detectar delimitador custom //x\n o //[delim]\n
        char[] delimiters = { ',', '\n' };
        
        if (input.StartsWith("//"))
        {
            int newlinePos = input.IndexOf('\n');
            if (newlinePos > 2)
            {
                string delimDef = input.Substring(2, newlinePos - 2);
                
                // Caso: //[delim]\n (multi-char)
                if (delimDef.StartsWith("[") && delimDef.EndsWith("]"))
                {
                    string customDelim = delimDef.Substring(1, delimDef.Length - 2);
                    delimiters = new[] { customDelim[0] }; // Placeholder; procesaremos como string
                    input = input.Substring(newlinePos + 1);
                    return AddWithDelimiterString(input, customDelim);
                }
                // Caso: //x\n (single-char)
                else if (delimDef.Length == 1)
                {
                    delimiters = new[] { delimDef[0] };
                    input = input.Substring(newlinePos + 1);
                }
            }
        }

        var parts = input.Split(delimiters, StringSplitOptions.None);
        return ProcessParts(parts);
    }

    /// <summary>
    /// Procesa números cuando el delimitador es un string (multi-char).
    /// </summary>
    private int AddWithDelimiterString(string input, string delimiter)
    {
        if (string.IsNullOrEmpty(input)) return 0;

        var parts = input.Split(new[] { delimiter }, StringSplitOptions.None);
        return ProcessParts(parts);
    }

    /// <summary>
    /// Procesa un array de partes: suma válidos, valida negativos, respeta límite superior.
    /// </summary>
    private int ProcessParts(string[] parts)
    {
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
            }
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

