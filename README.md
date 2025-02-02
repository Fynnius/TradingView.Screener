# TradingView.Screener

[![NuGet version](https://img.shields.io/nuget/v/TradingView.Screener.svg)](https://www.nuget.org/packages/TradingView.Screener/)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

A C# port of the [TradingView-Screener Python package](https://github.com/shner-elmo/TradingView-Screener) that allows you to create custom stock screeners using TradingView's API. This package retrieves data directly from TradingView without web scraping or HTML parsing.

## Credits
This project is a C# port of the excellent [TradingView-Screener](https://github.com/shner-elmo/TradingView-Screener) Python package by [shner-elmo](https://github.com/shner-elmo). All credit for the original implementation and API research goes to them.

## Requirements
- .NET 9.0 or later

## Installation

Install via NuGet Package Manager:
```bash
dotnet add package TradingView.Screener
```

For the CLI tool:
```bash
dotnet tool install --global TradingView.Screener.Cli
```

## Features

* **Multiple Markets**: Stocks, crypto, forex, and more
* **Comprehensive Data**: Access to price, volume, market cap, and technical indicators
* **Flexible Filtering**: Filter by price, volume, market cap, and other metrics
* **Sorting & Pagination**: Order results and control result size
* **Async Support**: Built with modern .NET async patterns
* **Command Line Interface**: Easy-to-use CLI tool for quick scans

## Quick Start

Here's a simple example using the library:

```csharp
using TradingView.Screener;
using static TradingView.Screener.Columns;

// Basic query - Get top stocks by volume
var result = await new Query()
    .Select("name", "close", "volume", "market_cap_basic")
    .Limit(5)
    .GetScannerDataRawAsync();

// Print results
foreach (var row in result.Data)
{
    Console.WriteLine($"Symbol: {row.Symbol}, Name: {row.Data[0]}, Close: {row.Data[1]}, Volume: {row.Data[2]}");
}
```

## CLI Usage

The CLI tool provides a simple interface for running stock screens:

```bash
# Basic scan
tvscreener scan

# With filters
tvscreener scan --min-price 10 --min-volume 1000000 --limit 5

# With market filter
tvscreener scan -m crypto --min-volume 1000000 --limit 5

# With JSON output
tvscreener scan --min-price 50 --limit 3 --json
```

Available options:
- `-m, --market`: Market to scan (e.g., america, japan, crypto)
- `--min-price`: Minimum price
- `--max-price`: Maximum price
- `--min-volume`: Minimum volume
- `--max-volume`: Maximum volume
- `--min-market-cap`: Minimum market cap
- `--max-market-cap`: Maximum market cap
- `--exchange`: Exchange (e.g., NASDAQ, NYSE)
- `--sector`: Sector
- `--industry`: Industry
- `-l, --limit`: Maximum number of results
- `--order-by`: Column to sort by
- `--descending`: Sort in descending order
- `--json`: Output in JSON format

## Advanced Examples

### Value Stock Screening
```csharp
// Find value stocks with good fundamentals
var valueStocks = await new Query()
    .Select("name", "close", "volume", "market_cap_basic", "price_earnings_ttm")
    .Where(
        Close > 5,
        Volume > 100000,
        MarketCap > 1_000_000_000  // At least 1B market cap
    )
    .OrderBy("volume", ascending: false)
    .Limit(5)
    .GetScannerDataRawAsync();
```

### Technical Analysis Screening
```csharp
// Find stocks with specific technical indicators
var technicalSignals = await new Query()
    .Select("name", "close", "volume", "EMA20", "EMA50", "RSI")
    .Where(
        Volume > 1000000,
        Close > 10,
        MarketCap > 1_000_000_000
    )
    .OrderBy("volume", ascending: false)
    .Limit(5)
    .GetScannerDataRawAsync();
```

### Crypto Market Screening
```csharp
// Find top cryptocurrencies by volume
var cryptoScreener = await new Query()
    .SetMarkets("crypto")
    .Select("name", "close", "volume", "market_cap_basic", "Volatility.D")
    .Where(
        Volume > 1000000
    )
    .OrderBy("market_cap_basic", ascending: false)
    .Limit(5)
    .GetScannerDataRawAsync();
```

## Known Limitation

1. Complex AND/OR filters using `Where2()` may not work reliably. Use simple filters with `Where()` instead.

## Legal Notice

This package is provided under the MIT License. While it interacts with TradingView's API, it is not affiliated with, endorsed by, or sponsored by TradingView. Users should ensure they comply with TradingView's terms of service when using this package.

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request. For major changes, please open an issue first to discuss what you would like to change.

### Development Setup

1. Clone the repository
2. Install dependencies:
```bash
dotnet restore
```
3. Run tests:
```bash
dotnet test
```

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.
