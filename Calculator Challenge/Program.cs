using Calculator_Challenge;
using Microsoft.Extensions.DependencyInjection;

// Configuración de servicios (DI)
var services = new ServiceCollection();
services.AddSingleton<IStringCalculator, StringCalculator>();

var provider = services.BuildServiceProvider();

// Parseo de flags CLI
var cliArgs = Environment.GetCommandLineArgs();
var showFormula = cliArgs.Any(a => a.Equals("--formula", StringComparison.OrdinalIgnoreCase));
var altDelimArg = GetArgValue(cliArgs, "--alt-delim");
var denyNegArg = GetArgValue(cliArgs, "--deny-negatives");
var upperBoundArg = GetArgValue(cliArgs, "--upper-bound");

var calculator = provider.GetRequiredService<IStringCalculator>();

if (!string.IsNullOrEmpty(altDelimArg))
{
    calculator.AddDefaultDelimiter(altDelimArg);
}

if (!string.IsNullOrEmpty(denyNegArg))
{
    var on = denyNegArg.Equals("on", StringComparison.OrdinalIgnoreCase) || denyNegArg.Equals("true", StringComparison.OrdinalIgnoreCase);
    calculator.SetAllowNegatives(!on);
}

if (!string.IsNullOrEmpty(upperBoundArg) && int.TryParse(upperBoundArg, out var ub) && ub >= 0)
{
    calculator.SetUpperBound(ub);
}

InteractiveMode(calculator, showFormula);

static void InteractiveMode(IStringCalculator calculator, bool showFormula)
{
    Console.WriteLine("=== String Calculator ===");
    Console.WriteLine("Ingresá strings con números y separadores (o 'exit' para salir)");
    Console.WriteLine("Flags: --formula  --alt-delim <d>");
    Console.WriteLine("       --deny-negatives on|off  --upper-bound N");
    Console.WriteLine();

    var cancelled = false;
    Console.CancelKeyPress += (_, e) =>
    {
        e.Cancel = true; // no matar el proceso abruptamente
        cancelled = true;
        Console.WriteLine("\nSaliendo por Ctrl+C...");
    };

    while (!cancelled)
    {
        Console.Write("> ");
        var input = Console.ReadLine();

        if (input?.Equals("exit", StringComparison.OrdinalIgnoreCase) ?? false)
            break;

        if (string.IsNullOrWhiteSpace(input))
            continue;

        try
        {
            if (showFormula)
            {
                var (sum, formula) = calculator.AddWithDetails(input);
                Console.WriteLine(formula);
            }
            else
            {
                var result = calculator.Add(input);
                Console.WriteLine($"Resultado: {result}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}

static string? GetArgValue(string[] args, string key)
{
    for (int i = 0; i < args.Length; i++)
    {
        if (string.Equals(args[i], key, StringComparison.OrdinalIgnoreCase))
        {
            if (i + 1 < args.Length) return args[i + 1];
            return string.Empty;
        }
        // soportar --key=value
        if (args[i].StartsWith(key + "=", StringComparison.OrdinalIgnoreCase))
        {
            var idx = args[i].IndexOf('=');
            return args[i].Substring(idx + 1);
        }
    }
    return null;
}
