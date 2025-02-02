using System.Text.Json;
using TradingView.Screener.Models;
using static TradingView.Screener.Columns;

namespace TradingView.Screener.Tests;

public class QueryTests
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = false
    };

    [Fact]
    public void Select_ShouldAddColumnsToRequest()
    {
        // Arrange
        var query = new Query();

        // Act
        query.Select(Name, Close, Volume, MarketCap);

        // Assert
        var request = GetQueryRequest(query);
        Assert.Equal(new[] { "name", "close", "volume", "market_cap_basic" }, request.Columns);
    }

    [Fact]
    public void Where_ShouldAddFiltersToRequest()
    {
        // Arrange
        var query = new Query();

        // Act
        query.Where(Close > 100, Volume > 1000000);

        // Assert
        var request = GetQueryRequest(query);
        Assert.Collection(request.Filter!,
            filter =>
            {
                Assert.Equal("close", filter.Left);
                Assert.Equal("greater", filter.Operation);
                Assert.Equal(100, filter.Right);
            },
            filter =>
            {
                Assert.Equal("volume", filter.Left);
                Assert.Equal("greater", filter.Operation);
                Assert.Equal(1000000, filter.Right);
            });
    }

    [Fact]
    public void Where2_ShouldAddComplexFiltersToRequest()
    {
        // Arrange
        var query = new Query();

        // Act
        query.Where2(Query.And(
            Close.Between(EMA5, EMA20),
            Volume > AverageVolume10D
        ));

        // Assert
        var request = GetQueryRequest(query);
        Assert.NotNull(request.Filter2);
        Assert.Equal("and", request.Filter2.Operator);
        Assert.Equal(2, request.Filter2.Operands.Count);
    }

    [Fact]
    public void OrderBy_ShouldAddSortToRequest()
    {
        // Arrange
        var query = new Query();

        // Act
        query.OrderBy(Volume, ascending: false);

        // Assert
        var request = GetQueryRequest(query);
        Assert.NotNull(request.Sort);
        Assert.Equal("volume", request.Sort.SortByColumn);
        Assert.Equal("desc", request.Sort.SortOrder);
    }

    [Fact]
    public void Limit_ShouldSetRangeInRequest()
    {
        // Arrange
        var query = new Query();

        // Act
        query.Limit(10);

        // Assert
        var request = GetQueryRequest(query);
        Assert.Equal(new[] { 0, 10 }, request.Range);
    }

    [Fact]
    public void Offset_ShouldSetRangeInRequest()
    {
        // Arrange
        var query = new Query();

        // Act
        query.Offset(20);

        // Assert
        var request = GetQueryRequest(query);
        Assert.Equal(new[] { 20, 70 }, request.Range);
    }

    [Fact]
    public void SetMarkets_ShouldSetMarketsInRequest()
    {
        // Arrange
        var query = new Query();

        // Act
        query.SetMarkets("nasdaq", "nyse");

        // Assert
        var request = GetQueryRequest(query);
        Assert.Equal(new[] { "nasdaq", "nyse" }, request.Markets);
    }

    [Fact]
    public void SetTickers_ShouldSetTickersInRequest()
    {
        // Arrange
        var query = new Query();

        // Act
        query.SetTickers("AAPL", "MSFT", "GOOGL");

        // Assert
        var request = GetQueryRequest(query);
        Assert.Equal(new[] { "AAPL", "MSFT", "GOOGL" }, request.Symbols.Tickers);
    }

    [Fact]
    public void ComplexQuery_ShouldGenerateCorrectRequest()
    {
        // Arrange & Act
        var query = new Query()
            .Select(Name, Close, Volume, MarketCap, RSI)
            .Where2(Query.And(
                Close.Between(EMA5, EMA20),
                Volume > AverageVolume10D,
                RSI < 70,
                MarketCap > 1_000_000_000
            ))
            .OrderBy(Volume, ascending: false)
            .Limit(10);

        // Assert
        var request = GetQueryRequest(query);
        Assert.Equal(new[] { "name", "close", "volume", "market_cap_basic", "RSI" }, request.Columns);
        Assert.NotNull(request.Filter2);
        Assert.Equal("and", request.Filter2.Operator);
        Assert.Equal(4, request.Filter2.Operands.Count);
        Assert.NotNull(request.Sort);
        Assert.Equal("volume", request.Sort.SortByColumn);
        Assert.Equal("desc", request.Sort.SortOrder);
        Assert.Equal(new[] { 0, 10 }, request.Range);
    }

    private static QueryRequest GetQueryRequest(Query query)
    {
        var requestField = query.GetType().GetField("_request", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        return (QueryRequest)(requestField?.GetValue(query) ?? throw new InvalidOperationException("Could not get request field"));
    }
} 