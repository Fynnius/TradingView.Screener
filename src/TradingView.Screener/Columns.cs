namespace TradingView.Screener;

/// <summary>
/// Provides easy access to commonly used columns in the TradingView screener.
/// </summary>
public static class Columns
{
    // Basic Information
    public static Column Name => new("name");
    public static Column Description => new("description");
    public static Column Type => new("type");
    public static Column Subtype => new("subtype");
    public static Column Exchange => new("exchange");
    public static Column Sector => new("sector");
    public static Column Industry => new("industry");

    // Price Information
    public static Column Close => new("close");
    public static Column Open => new("open");
    public static Column High => new("high");
    public static Column Low => new("low");
    public static Column PreviousClose => new("previous_close");
    public static Column Volume => new("volume");
    public static Column Price => new("price");
    public static Column Change => new("change");
    public static Column ChangePercent => new("change_percent");

    // Market Data
    public static Column MarketCap => new("market_cap_basic");
    public static Column FloatingMarketCap => new("floating_market_cap");
    public static Column SharesOutstanding => new("shares_outstanding");
    public static Column FloatingShares => new("floating_shares");

    // Technical Indicators
    public static Column RSI => new("RSI");
    public static Column MACD => new("MACD.macd");
    public static Column MACDSignal => new("MACD.signal");
    public static Column MACDHist => new("MACD.hist");
    public static Column EMA5 => new("EMA5");
    public static Column EMA10 => new("EMA10");
    public static Column EMA20 => new("EMA20");
    public static Column EMA50 => new("EMA50");
    public static Column EMA100 => new("EMA100");
    public static Column EMA200 => new("EMA200");
    public static Column SMA5 => new("SMA5");
    public static Column SMA10 => new("SMA10");
    public static Column SMA20 => new("SMA20");
    public static Column SMA50 => new("SMA50");
    public static Column SMA100 => new("SMA100");
    public static Column SMA200 => new("SMA200");
    public static Column VWAP => new("VWAP");

    // Price Statistics
    public static Column High52Week => new("High.52Week");
    public static Column Low52Week => new("Low.52Week");
    public static Column HighAllTime => new("High.All");
    public static Column LowAllTime => new("Low.All");
    public static Column AverageVolume10D => new("average_volume_10d_calc");
    public static Column RelativeVolume10D => new("relative_volume_10d_calc");

    // Fundamental Data
    public static Column PERatio => new("price_earnings_ttm");
    public static Column PEGRatio => new("price_earnings_growth_ttm");
    public static Column PSRatio => new("price_sales_ttm");
    public static Column PBRatio => new("price_book_ttm");
    public static Column DividendYield => new("dividend_yield_ttm");
    public static Column EPS => new("earnings_per_share_basic_ttm");
    public static Column Revenue => new("total_revenue_ttm");
    public static Column GrossProfit => new("gross_profit_ttm");
    public static Column NetIncome => new("net_income_ttm");
    public static Column OperatingMargin => new("operating_margin_ttm");
    public static Column ProfitMargin => new("profit_margin_ttm");
    public static Column ROE => new("return_on_equity_ttm");
    public static Column ROA => new("return_on_assets_ttm");
    public static Column DebtToEquity => new("debt_to_equity_ttm");
    public static Column CurrentRatio => new("current_ratio_ttm");

    // Options Data
    public static Column ImpliedVolatility => new("implied_volatility_30d");
    public static Column HistoricalVolatility => new("historical_volatility_30d");
    public static Column OptionVolume => new("option_volume");
    public static Column PutCallRatio => new("put_call_ratio");

    // Earnings and Events
    public static Column EarningsDate => new("earnings_release_next_date");
    public static Column EarningsSurprise => new("earnings_surprise_ttm");
    public static Column EarningsGrowth => new("earnings_growth_ttm");
    public static Column RevenueGrowth => new("revenue_growth_ttm");

    // Analyst Ratings
    public static Column AnalystRating => new("analyst_rating");
    public static Column AnalystCount => new("analyst_count");
    public static Column PriceTarget => new("price_target");
    public static Column RecommendationMean => new("recommendation_mean");

    // Short Interest
    public static Column ShortInterest => new("short_interest");
    public static Column ShortInterestRatio => new("short_interest_ratio");
    public static Column ShortPercentFloat => new("short_percent_float");
    public static Column DaysTocover => new("days_to_cover");
} 