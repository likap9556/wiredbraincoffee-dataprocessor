namespace WiredBrainCoffee.DataProcessor.Parsing;

public class CsvLineParserTests
{
    [Fact]
    public void ShouldParseValidLine()
    {
        // Arrange.
        string[] csvLines = new[] { "Espresso;10/10/2022 10:10:16" };

        // Act.
        var machineDataItems = CsvLineParser.Parse(csvLines);

        // Assert.
        Assert.NotNull(machineDataItems);
        Assert.Single(machineDataItems);
        Assert.Equal("Espresso", machineDataItems[0].CoffeeType);
        Assert.Equal(new DateTime(2022, 10, 10, 10, 10, 16), machineDataItems[0].CreatedAt);
    }

    [Fact]
    public void ShouldSkipEmptyLines()
    {
        // Arrange.
        string[] csvLines = new[] { "", " " };

        // Act.
        var machineDataItems = CsvLineParser.Parse(csvLines);

        // Assert.
        Assert.NotNull(machineDataItems);
        Assert.Empty(machineDataItems);
    }

    [InlineData("Espresso", "Invalid csv line")]
    [InlineData("Espresso;InvalidDateTime", "Invalid datetime in csv line")]
    [Theory]
    public void ShouldThrowExceptionForInvalidLine(string csvLine, string expectedMessagePrefix)
    {
        // Arrange.
        var csvLines = new[] { csvLine };

        // Act and Assert.
        var exception = Assert.Throws<Exception>(()=>  CsvLineParser.Parse(csvLines));

        Assert.Equal($"{expectedMessagePrefix}: {csvLine}", exception.Message);
    }
}