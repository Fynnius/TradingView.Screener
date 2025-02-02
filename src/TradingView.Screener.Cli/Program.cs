using Microsoft.Extensions.CommandLineUtils;
using TradingView.Screener;
using TradingView.Screener.Models;
using System.Text.Json;
using static TradingView.Screener.Columns;

namespace TradingView.Screener.Cli;

public class Program
{
    public static int Main(string[] args)
    {
        var app = new CommandLineApplication();
        app.Name = "tvscreener";
        app.Description = "TradingView Screener CLI Tool";
        app.HelpOption("-h|--help");

        app.Command("scan", (command) =>
        {
            command.Description = "Scan stocks based on criteria";
            command.HelpOption("-h|--help");

            var marketOption = command.Option("-m|--market",
                "Market to scan (e.g., america, japan)",
                CommandOptionType.SingleValue);

            var minPriceOption = command.Option("--min-price",
                "Minimum price",
                CommandOptionType.SingleValue);

            var maxPriceOption = command.Option("--max-price",
                "Maximum price",
                CommandOptionType.SingleValue);

            var minVolumeOption = command.Option("--min-volume",
                "Minimum volume",
                CommandOptionType.SingleValue);

            var maxVolumeOption = command.Option("--max-volume",
                "Maximum volume",
                CommandOptionType.SingleValue);

            var minMarketCapOption = command.Option("--min-market-cap",
                "Minimum market cap",
                CommandOptionType.SingleValue);

            var maxMarketCapOption = command.Option("--max-market-cap",
                "Maximum market cap",
                CommandOptionType.SingleValue);

            var exchangeOption = command.Option("--exchange",
                "Exchange (e.g., NASDAQ, NYSE)",
                CommandOptionType.SingleValue);

            var sectorOption = command.Option("--sector",
                "Sector",
                CommandOptionType.SingleValue);

            var industryOption = command.Option("--industry",
                "Industry",
                CommandOptionType.SingleValue);

            var limitOption = command.Option("-l|--limit",
                "Maximum number of results",
                CommandOptionType.SingleValue);

            var orderByOption = command.Option("--order-by",
                "Column to sort by",
                CommandOptionType.SingleValue);

            var descendingOption = command.Option("--descending",
                "Sort in descending order",
                CommandOptionType.NoValue);

            var jsonOption = command.Option("--json",
                "Output in JSON format",
                CommandOptionType.NoValue);

            command.OnExecute(async () =>
            {
                try
                {
                    var query = new Query();

                    // Apply market filter
                    if (marketOption.HasValue())
                    {
                        query.SetMarkets(marketOption.Value());
                    }

                    // Build where clause
                    var filters = new List<FilterOperation>();

                    if (minPriceOption.HasValue() && decimal.TryParse(minPriceOption.Value(), out var minPrice))
                    {
                        filters.Add(new FilterOperation { Left = "close", Operation = "greater", Right = minPrice });
                    }

                    if (maxPriceOption.HasValue() && decimal.TryParse(maxPriceOption.Value(), out var maxPrice))
                    {
                        filters.Add(new FilterOperation { Left = "close", Operation = "less", Right = maxPrice });
                    }

                    if (minVolumeOption.HasValue() && decimal.TryParse(minVolumeOption.Value(), out var minVolume))
                    {
                        filters.Add(new FilterOperation { Left = "volume", Operation = "greater", Right = minVolume });
                    }

                    if (maxVolumeOption.HasValue() && decimal.TryParse(maxVolumeOption.Value(), out var maxVolume))
                    {
                        filters.Add(new FilterOperation { Left = "volume", Operation = "less", Right = maxVolume });
                    }

                    if (minMarketCapOption.HasValue() && decimal.TryParse(minMarketCapOption.Value(), out var minMarketCap))
                    {
                        filters.Add(new FilterOperation { Left = "market_cap_basic", Operation = "greater", Right = minMarketCap });
                    }

                    if (maxMarketCapOption.HasValue() && decimal.TryParse(maxMarketCapOption.Value(), out var maxMarketCap))
                    {
                        filters.Add(new FilterOperation { Left = "market_cap_basic", Operation = "less", Right = maxMarketCap });
                    }

                    if (exchangeOption.HasValue())
                    {
                        filters.Add(new FilterOperation { Left = "exchange", Operation = "equal", Right = exchangeOption.Value() });
                    }

                    if (sectorOption.HasValue())
                    {
                        filters.Add(new FilterOperation { Left = "sector", Operation = "equal", Right = sectorOption.Value() });
                    }

                    if (industryOption.HasValue())
                    {
                        filters.Add(new FilterOperation { Left = "industry", Operation = "equal", Right = industryOption.Value() });
                    }

                    // Apply filters
                    if (filters.Any())
                    {
                        query.Where(filters.ToArray());
                    }

                    // Apply limit
                    if (limitOption.HasValue() && int.TryParse(limitOption.Value(), out var limit))
                    {
                        query.Limit(limit);
                    }

                    // Apply sorting
                    if (orderByOption.HasValue())
                    {
                        query.OrderBy(orderByOption.Value().ToLowerInvariant(), !descendingOption.HasValue());
                    }

                    // Execute query
                    var result = await query
                        .Select("name", "close", "volume", "market_cap_basic")
                        .GetScannerDataRawAsync();

                    // Print results
                    if (jsonOption.HasValue())
                    {
                        Console.WriteLine(JsonSerializer.Serialize(result, new JsonSerializerOptions
                        {
                            WriteIndented = true
                        }));
                    }
                    else
                    {
                        Console.WriteLine($"Found {result.TotalCount} results:");
                        Console.WriteLine();
                        Console.WriteLine("Symbol\t\tName\t\tClose\t\tVolume\t\tMarket Cap");
                        Console.WriteLine(new string('-', 80));

                        foreach (var row in result.Data)
                        {
                            Console.WriteLine($"{row.Symbol,-15}{row.Data[0],-15}{row.Data[1],-15}{row.Data[2],-15}{row.Data[3],-15}");
                        }
                    }

                    return 0;
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Error.WriteLine($"Error: {ex.Message}");
                    Console.ResetColor();
                    return 1;
                }
            });
        });

        app.OnExecute(() =>
        {
            app.ShowHelp();
            return 0;
        });

        try
        {
            return app.Execute(args);
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Error.WriteLine($"Fatal error: {ex.Message}");
            Console.ResetColor();
            return 1;
        }
    }
}
