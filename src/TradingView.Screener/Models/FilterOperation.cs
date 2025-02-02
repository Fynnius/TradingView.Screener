using System.Text.Json.Serialization;

namespace TradingView.Screener.Models;

/// <summary>
/// Represents a filter operation in the TradingView screener API.
/// </summary>
public class FilterOperation
{
    /// <summary>
    /// The left operand of the filter operation (usually a column name).
    /// </summary>
    [JsonPropertyName("left")]
    public string Left { get; set; } = string.Empty;

    /// <summary>
    /// The operation type to perform.
    /// </summary>
    [JsonPropertyName("operation")]
    public string Operation { get; set; } = string.Empty;

    /// <summary>
    /// The right operand of the filter operation (can be a value or another column name).
    /// </summary>
    [JsonPropertyName("right")]
    public object? Right { get; set; }
}

/// <summary>
/// Represents the available operation types for filter operations.
/// </summary>
public static class FilterOperationType
{
    public const string Greater = "greater";
    public const string EGreater = "egreater";
    public const string Less = "less";
    public const string ELess = "eless";
    public const string Equal = "equal";
    public const string NotEqual = "nequal";
    public const string InRange = "in_range";
    public const string NotInRange = "not_in_range";
    public const string Empty = "empty";
    public const string NotEmpty = "nempty";
    public const string Crosses = "crosses";
    public const string CrossesAbove = "crosses_above";
    public const string CrossesBelow = "crosses_below";
    public const string Match = "match";
    public const string NotMatch = "nmatch";
    public const string SimpleMatch = "smatch";
    public const string Has = "has";
    public const string HasNoneOf = "has_none_of";
    public const string AbovePercent = "above%";
    public const string BelowPercent = "below%";
    public const string InRangePercent = "in_range%";
    public const string NotInRangePercent = "not_in_range%";
    public const string InDayRange = "in_day_range";
    public const string InWeekRange = "in_week_range";
    public const string InMonthRange = "in_month_range";
} 