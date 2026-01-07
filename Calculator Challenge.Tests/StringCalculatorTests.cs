using Xunit;
using Calculator_Challenge;

namespace Calculator_Challenge.Tests;

public class StringCalculatorTests
{
    [Fact]
    public void Add_EmptyString_ReturnsZero()
    {
        var calc = new StringCalculator();
        var result = calc.Add("");
        Assert.Equal(0, result);
    }
}
