using System.Net.Http.Json;
using System.Text.Json;
using TradingView.Screener.Models;

namespace TradingView.Screener;

/// <summary>
/// Provides a fluent interface for building and executing TradingView screener queries.
/// </summary>
public class Query
{
    private const string DefaultUrl = "https://scanner.tradingview.com/{0}/scan";
    private const int DefaultRangeSize = 50;
    private static readonly HttpClient HttpClient = new HttpClient();
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = false
    };
    private QueryRequest _request = new();

    static Query()
    {
        try
        {
            HttpClient.DefaultRequestHeaders.Clear();
            HttpClient.DefaultRequestHeaders.Add("authority", "scanner.tradingview.com");
            HttpClient.DefaultRequestHeaders.Add("accept", "text/plain, */*; q=0.01");
            HttpClient.DefaultRequestHeaders.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/98.0.4758.102 Safari/537.36");
            HttpClient.DefaultRequestHeaders.Add("origin", "https://www.tradingview.com");
            HttpClient.DefaultRequestHeaders.Add("referer", "https://www.tradingview.com/");
            HttpClient.DefaultRequestHeaders.Add("accept-language", "en-US,en;q=0.9");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to initialize HttpClient: {ex.Message}");
            throw;
        }
    }

    /// <summary>
    /// Selects the columns to return in the query results.
    /// </summary>
    /// <param name="columns">The columns to select.</param>
    public Query Select(params object[] columns)
    {
        _request.Columns = columns.Select(c => c switch
        {
            Column col => col.ToString() ?? string.Empty,
            string str => str,
            _ => c?.ToString() ?? string.Empty
        }).ToList();
        return this;
    }

    /// <summary>
    /// Adds filter conditions to the query.
    /// </summary>
    /// <param name="filters">The filter operations to apply.</param>
    public Query Where(params FilterOperation[] filters)
    {
        _request.Filter = filters.ToList();
        return this;
    }

    /// <summary>
    /// Adds a complex filter operation (AND/OR combinations) to the query.
    /// </summary>
    /// <param name="operation">The complex filter operation.</param>
    public Query Where2(OperationComparison operation)
    {
        _request.Filter2 = operation;
        return this;
    }

    /// <summary>
    /// Sets the sort order for the query results.
    /// </summary>
    /// <param name="column">The column to sort by.</param>
    /// <param name="ascending">Whether to sort in ascending order.</param>
    /// <param name="nullsFirst">Whether to place null values first.</param>
    public Query OrderBy(object column, bool ascending = true, bool? nullsFirst = null)
    {
        _request.Sort = new SortBy
        {
            SortByColumn = column switch
            {
                Column col => col.ToString() ?? string.Empty,
                string str => str,
                _ => column?.ToString() ?? string.Empty
            },
            SortOrder = ascending ? "asc" : "desc",
            NullsFirst = nullsFirst
        };
        return this;
    }

    /// <summary>
    /// Sets the maximum number of results to return.
    /// </summary>
    /// <param name="limit">The maximum number of results.</param>
    public Query Limit(int limit)
    {
        _request.Range[1] = _request.Range[0] + limit;
        return this;
    }

    /// <summary>
    /// Sets the number of results to skip.
    /// </summary>
    /// <param name="offset">The number of results to skip.</param>
    public Query Offset(int offset)
    {
        _request.Range[0] = offset;
        _request.Range[1] = offset + DefaultRangeSize;
        return this;
    }

    /// <summary>
    /// Sets the markets to search in.
    /// </summary>
    /// <param name="markets">The markets to include.</param>
    public Query SetMarkets(params string[] markets)
    {
        _request.Markets = markets.ToList();
        return this;
    }

    /// <summary>
    /// Sets specific tickers to filter by.
    /// </summary>
    /// <param name="tickers">The tickers to include.</param>
    public Query SetTickers(params string[] tickers)
    {
        _request.Symbols.Tickers = tickers.ToList();
        return this;
    }

    /// <summary>
    /// Executes the query and returns the raw response.
    /// </summary>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    public async Task<ScreenerResponse> GetScannerDataRawAsync(CancellationToken cancellationToken = default)
    {
        var market = _request.Markets.FirstOrDefault() ?? "america";
        var url = string.Format(DefaultUrl, market);

        using var request = new HttpRequestMessage(HttpMethod.Post, url);
        request.Content = JsonContent.Create(_request, null, JsonOptions);
        if (request.Content.Headers.ContentType != null){
            request.Content.Headers.ContentType.MediaType = "application/x-www-form-urlencoded";
        }

        var response = await HttpClient.SendAsync(request, cancellationToken);
        response.EnsureSuccessStatusCode();
        
        return await response.Content.ReadFromJsonAsync<ScreenerResponse>(JsonOptions, cancellationToken)
            ?? throw new InvalidOperationException("Failed to deserialize response");
    }

    /// <summary>
    /// Creates a copy of the current query.
    /// </summary>
    public Query Copy()
    {
        var query = new Query();
        query._request = JsonSerializer.Deserialize<QueryRequest>(JsonSerializer.Serialize(_request, JsonOptions), JsonOptions)
            ?? throw new InvalidOperationException("Failed to copy query");
        return query;
    }

    /// <summary>
    /// Creates a logical AND operation between filter operations.
    /// </summary>
    public static OperationComparison And(params object[] operands) => new()
    {
        Operator = "and",
        Operands = operands.Select(o => o is FilterOperation f ? new { expression = f } : o).ToList()
    };

    /// <summary>
    /// Creates a logical OR operation between filter operations.
    /// </summary>
    public static OperationComparison Or(params object[] operands) => new()
    {
        Operator = "or",
        Operands = operands.Select(o => o is FilterOperation f ? new { expression = f } : o).ToList()
    };
} 