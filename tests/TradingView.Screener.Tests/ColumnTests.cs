using System.Text.Json;
using TradingView.Screener.Models;

namespace TradingView.Screener.Tests;

public class ColumnTests
{
    [Fact]
    public void GreaterThan_ShouldCreateCorrectFilterOperation()
    {
        // Arrange
        var column = new Column("close");

        // Act
        var filter = column > 100;

        // Assert
        Assert.Equal("close", filter.Left);
        Assert.Equal(FilterOperationType.Greater, filter.Operation);
        Assert.Equal(100, filter.Right);
    }

    [Fact]
    public void GreaterThanOrEqual_ShouldCreateCorrectFilterOperation()
    {
        // Arrange
        var column = new Column("close");

        // Act
        var filter = column >= 100;

        // Assert
        Assert.Equal("close", filter.Left);
        Assert.Equal(FilterOperationType.EGreater, filter.Operation);
        Assert.Equal(100, filter.Right);
    }

    [Fact]
    public void LessThan_ShouldCreateCorrectFilterOperation()
    {
        // Arrange
        var column = new Column("close");

        // Act
        var filter = column < 100;

        // Assert
        Assert.Equal("close", filter.Left);
        Assert.Equal(FilterOperationType.Less, filter.Operation);
        Assert.Equal(100, filter.Right);
    }

    [Fact]
    public void LessThanOrEqual_ShouldCreateCorrectFilterOperation()
    {
        // Arrange
        var column = new Column("close");

        // Act
        var filter = column <= 100;

        // Assert
        Assert.Equal("close", filter.Left);
        Assert.Equal(FilterOperationType.ELess, filter.Operation);
        Assert.Equal(100, filter.Right);
    }

    [Fact]
    public void Equal_ShouldCreateCorrectFilterOperation()
    {
        // Arrange
        var column = new Column("type");

        // Act
        var filter = column == "stock";

        // Assert
        Assert.Equal("type", filter.Left);
        Assert.Equal(FilterOperationType.Equal, filter.Operation);
        Assert.Equal("stock", filter.Right);
    }

    [Fact]
    public void NotEqual_ShouldCreateCorrectFilterOperation()
    {
        // Arrange
        var column = new Column("type");

        // Act
        var filter = column != "etf";

        // Assert
        Assert.Equal("type", filter.Left);
        Assert.Equal(FilterOperationType.NotEqual, filter.Operation);
        Assert.Equal("etf", filter.Right);
    }

    [Fact]
    public void Between_ShouldCreateCorrectFilterOperation()
    {
        // Arrange
        var column = new Column("close");

        // Act
        var filter = column.Between(100, 200);

        // Assert
        Assert.Equal("close", filter.Left);
        Assert.Equal(FilterOperationType.InRange, filter.Operation);
        Assert.Equal(new[] { 100, 200 }, filter.Right);
    }

    [Fact]
    public void NotBetween_ShouldCreateCorrectFilterOperation()
    {
        // Arrange
        var column = new Column("close");

        // Act
        var filter = column.NotBetween(100, 200);

        // Assert
        Assert.Equal("close", filter.Left);
        Assert.Equal(FilterOperationType.NotInRange, filter.Operation);
        Assert.Equal(new[] { 100, 200 }, filter.Right);
    }

    [Fact]
    public void IsIn_ShouldCreateCorrectFilterOperation()
    {
        // Arrange
        var column = new Column("exchange");

        // Act
        var filter = column.IsIn(new[] { "NASDAQ", "NYSE" });

        // Assert
        Assert.Equal("exchange", filter.Left);
        Assert.Equal(FilterOperationType.InRange, filter.Operation);
        Assert.Equal(new[] { "NASDAQ", "NYSE" }, filter.Right);
    }

    [Fact]
    public void Has_ShouldCreateCorrectFilterOperation()
    {
        // Arrange
        var column = new Column("typespecs");

        // Act
        var filter = column.Has(new[] { "common" });

        // Assert
        Assert.Equal("typespecs", filter.Left);
        Assert.Equal(FilterOperationType.Has, filter.Operation);
        Assert.Equal(new[] { "common" }, filter.Right);
    }

    [Fact]
    public void Crosses_ShouldCreateCorrectFilterOperation()
    {
        // Arrange
        var column = new Column("close");
        var other = new Column("sma20");

        // Act
        var filter = column.Crosses(other);

        // Assert
        Assert.Equal("close", filter.Left);
        Assert.Equal(FilterOperationType.Crosses, filter.Operation);
        Assert.Equal("sma20", filter.Right);
    }

    [Fact]
    public void AbovePercent_ShouldCreateCorrectFilterOperation()
    {
        // Arrange
        var column = new Column("close");
        var other = new Column("vwap");

        // Act
        var filter = column.AbovePercent(other, 1.03);

        // Assert
        Assert.Equal("close", filter.Left);
        Assert.Equal(FilterOperationType.AbovePercent, filter.Operation);
        Assert.Equal(new object[] { "vwap", 1.03 }, filter.Right);
    }

    [Fact]
    public void Like_ShouldCreateCorrectFilterOperation()
    {
        // Arrange
        var column = new Column("description");

        // Act
        var filter = column.Like("technology");

        // Assert
        Assert.Equal("description", filter.Left);
        Assert.Equal(FilterOperationType.Match, filter.Operation);
        Assert.Equal("technology", filter.Right);
    }

    [Fact]
    public void Empty_ShouldCreateCorrectFilterOperation()
    {
        // Arrange
        var column = new Column("earnings_date");

        // Act
        var filter = column.Empty();

        // Assert
        Assert.Equal("earnings_date", filter.Left);
        Assert.Equal(FilterOperationType.Empty, filter.Operation);
        Assert.Null(filter.Right);
    }
} 