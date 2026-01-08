using Xunit;
using Calculator_Challenge;

namespace Calculator_Challenge.Tests;

public class StringCalculatorTests
{
    private readonly StringCalculator _calculator = new();

    #region Step 1 - Comma delimiter, max 2 numbers

    [Fact]
    public void Add_EmptyString_ReturnsZero()
    {
        var result = _calculator.Add("");
        Assert.Equal(0, result);
    }

    [Fact]
    public void Add_SingleNumber_ReturnsThatNumber()
    {
        var result = _calculator.Add("20");
        Assert.Equal(20, result);
    }

    [Fact]
    public void Add_TwoNumbersWithComma_ReturnsSum()
    {
        var result = _calculator.Add("1,500");
        Assert.Equal(501, result);
    }

    [Fact]
    public void Add_MissingSecondNumber_TreatsAsZero()
    {
        var result = _calculator.Add("2,");
        Assert.Equal(2, result);
    }

    [Fact]
    public void Add_MissingFirstNumber_TreatsAsZero()
    {
        var result = _calculator.Add(",3");
        Assert.Equal(3, result);
    }

    [Fact]
    public void Add_InvalidSecondNumber_TreatsAsZero()
    {
        var result = _calculator.Add("5,tytyt");
        Assert.Equal(5, result);
    }

    [Fact]
    public void Add_InvalidFirstNumber_TreatsAsZero()
    {
        var result = _calculator.Add("xyz,10");
        Assert.Equal(10, result);
    }

    [Fact]
    public void Add_BothNumbersInvalid_ReturnsZero()
    {
        var result = _calculator.Add("abc,xyz");
        Assert.Equal(0, result);
    }

    [Fact]
    public void Add_ZeroAndPositive_ReturnsSum()
    {
        var result = _calculator.Add("0,5");
        Assert.Equal(5, result);
    }

    #endregion

    #region Step 2 - Unlimited numbers

    [Fact]
    public void Add_ThreeNumbers_ReturnsSum()
    {
        var result = _calculator.Add("1,2,3");
        Assert.Equal(6, result);
    }

    [Fact]
    public void Add_ManyNumbers_ReturnsSum()
    {
        var result = _calculator.Add("1,2,3,4,5,6,7,8,9,10,11,12");
        Assert.Equal(78, result);
    }

    [Fact]
    public void Add_ManyNumbersWithMissing_HandlesAsZero()
    {
        var result = _calculator.Add("1,,3,4,");
        Assert.Equal(8, result); // 1 + 0 + 3 + 4 + 0
    }

    [Fact]
    public void Add_MoreThanTwoNumbers_SumsAll()
    {
        var result = _calculator.Add("1,2,3,4,5");
        Assert.Equal(15, result);
    }

    #endregion

    #region Step 3 - Newline delimiter

    [Fact]
    public void Add_NumbersWithNewlineDelimiter_ReturnsSum()
    {
        var result = _calculator.Add("1\n2,3");
        Assert.Equal(6, result);
    }

    [Fact]
    public void Add_OnlyNewlineDelimiters_ReturnsSum()
    {
        var result = _calculator.Add("10\n20\n30");
        Assert.Equal(60, result);
    }

    [Fact]
    public void Add_MixedNewlineAndComma_ReturnsSum()
    {
        var result = _calculator.Add("5\n10,15\n20");
        Assert.Equal(50, result);
    }

    #endregion

    #region Step 5 - Ignore numbers greater than 1000

    [Fact]
    public void Add_WithNumbersGreaterThan1000_IgnoresThem()
    {
        var result = _calculator.Add("2,1001,6");
        Assert.Equal(8, result); // 2 + 0 + 6
    }

    [Fact]
    public void Add_With1000_IncludesIt()
    {
        var result = _calculator.Add("1000,5");
        Assert.Equal(1005, result);
    }

    [Fact]
    public void Add_With1001_IgnoresIt()
    {
        var result = _calculator.Add("1001,5");
        Assert.Equal(5, result);
    }

    [Fact]
    public void Add_MultipleNumbersGreaterThan1000()
    {
        var result = _calculator.Add("10,1001,2000,5");
        Assert.Equal(15, result); // 10 + 0 + 0 + 5
    }

    #endregion

    #region Step 6 - Custom single-char delimiter

    [Fact]
    public void Add_CustomSingleCharDelimiter_ReturnsSum()
    {
        var result = _calculator.Add("//#\n2#5");
        Assert.Equal(7, result);
    }

    [Fact]
    public void Add_CustomSingleCharDelimiter_WithInvalid_TreatsInvalidAsZero()
    {
        var result = _calculator.Add("//;\n4;abc;6");
        Assert.Equal(10, result);
    }

    [Fact]
    public void Add_CustomSingleCharDelimiter_RespectsGreaterThan1000Rule()
    {
        var result = _calculator.Add("//@\n1001@2");
        Assert.Equal(2, result);
    }

    [Fact]
    public void Add_CustomSingleCharDelimiter_WithNegative_ThrowsWhenDisallowed()
    {
        Assert.Throws<ArgumentException>(() => _calculator.Add("//!\n1!-2!3"));
    }

    #endregion
}

