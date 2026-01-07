namespace Calculator_Challenge;

public interface IStringCalculator
{
    int Add(string input);
    (int Sum, string Formula) AddWithDetails(string input);

    // Opciones de configuración
    void SetAllowNegatives(bool allow);
    void SetUpperBound(int upper);
    void AddDefaultDelimiter(string delimiter);
}
