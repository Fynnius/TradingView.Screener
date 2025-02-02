using System.Text.Json.Serialization;

namespace TradingView.Screener.Models;

/// <summary>
/// Represents a query request to the TradingView screener API.
/// </summary>
public class QueryRequest
{
    /// <summary>
    /// The markets to search in (e.g., "america").
    /// </summary>
    [JsonPropertyName("markets")]
    public List<string> Markets { get; set; } = new() { "america" };

    /// <summary>
    /// The symbols to filter by.
    /// </summary>
    [JsonPropertyName("symbols")]
    public SymbolsFilter Symbols { get; set; } = new();

    /// <summary>
    /// Query options.
    /// </summary>
    [JsonPropertyName("options")]
    public Dictionary<string, object> Options { get; set; } = new() { { "lang", "en" } };

    /// <summary>
    /// The columns to return in the result.
    /// </summary>
    [JsonPropertyName("columns")]
    public List<string> Columns { get; set; } = new() { "name", "close", "volume", "market_cap_basic" };

    /// <summary>
    /// The filter operations to apply.
    /// </summary>
    [JsonPropertyName("filter")]
    public List<FilterOperation>? Filter { get; set; }

    /// <summary>
    /// Complex filter operation (AND/OR combinations).
    /// </summary>
    [JsonPropertyName("filter2")]
    public OperationComparison? Filter2 { get; set; }

    /// <summary>
    /// Sort settings for the results.
    /// </summary>
    [JsonPropertyName("sort")]
    public SortBy? Sort { get; set; }

    /// <summary>
    /// Range of results to return [start, end].
    /// </summary>
    [JsonPropertyName("range")]
    public List<int> Range { get; set; } = new() { 0, 50 };

    /// <summary>
    /// Whether to ignore unknown fields in the response.
    /// </summary>
    [JsonPropertyName("ignore_unknown_fields")]
    public bool IgnoreUnknownFields { get; set; }
}

/// <summary>
/// Represents the symbols filter in a query.
/// </summary>
public class SymbolsFilter
{
    /// <summary>
    /// The query parameters for symbol types.
    /// </summary>
    [JsonPropertyName("query")]
    public Dictionary<string, List<string>> Query { get; set; } = new() { { "types", new List<string>() } };

    /// <summary>
    /// List of specific tickers to filter by.
    /// </summary>
    [JsonPropertyName("tickers")]
    public List<string> Tickers { get; set; } = new();
}

/// <summary>
/// Represents a complex operation comparison (AND/OR).
/// </summary>
public class OperationComparison
{
    /// <summary>
    /// The operator type ("and" or "or").
    /// </summary>
    [JsonPropertyName("operator")]
    public string Operator { get; set; } = string.Empty;

    /// <summary>
    /// The operands to combine.
    /// </summary>
    [JsonPropertyName("operands")]
    public List<object> Operands { get; set; } = new();
}

/// <summary>
/// Represents sort settings for the query.
/// </summary>
public class SortBy
{
    /// <summary>
    /// The column to sort by.
    /// </summary>
    [JsonPropertyName("sortBy")]
    public string SortByColumn { get; set; } = string.Empty;

    /// <summary>
    /// The sort order ("asc" or "desc").
    /// </summary>
    [JsonPropertyName("sortOrder")]
    public string SortOrder { get; set; } = "asc";

    /// <summary>
    /// Whether null values should be sorted first.
    /// </summary>
    [JsonPropertyName("nullsFirst")]
    public bool? NullsFirst { get; set; }
}

/// <summary>
/// Represents a row in the screener results.
/// </summary>
public class ScreenerRow
{
    /// <summary>
    /// The symbol (e.g., "NASDAQ:AAPL").
    /// </summary>
    [JsonPropertyName("s")]
    public string Symbol { get; set; } = string.Empty;

    /// <summary>
    /// The data values for the requested columns.
    /// </summary>
    [JsonPropertyName("d")]
    public List<object?> Data { get; set; } = new();
}

/// <summary>
/// Represents the response from the screener API.
/// </summary>
public class ScreenerResponse
{
    /// <summary>
    /// The total count of matching results.
    /// </summary>
    [JsonPropertyName("totalCount")]
    public int TotalCount { get; set; }

    /// <summary>
    /// The rows of data returned.
    /// </summary>
    [JsonPropertyName("data")]
    public List<ScreenerRow> Data { get; set; } = new();
} 