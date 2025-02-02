using TradingView.Screener.Models;

namespace TradingView.Screener;

/// <summary>
/// Represents a column in the TradingView screener and provides a fluent interface for building filter operations.
/// </summary>
public class Column
{
    private readonly string _name;

    /// <summary>
    /// Initializes a new instance of the Column class.
    /// </summary>
    /// <param name="name">The name of the column.</param>
    public Column(string name)
    {
        _name = name;
    }

    /// <summary>
    /// Returns the column name.
    /// </summary>
    public override string ToString() => _name;

    private static object ExtractName(object value) => value is Column col ? col._name : value;

    /// <summary>
    /// Creates a "greater than" filter operation.
    /// </summary>
    public static FilterOperation operator >(Column left, object right) =>
        new() { Left = left._name, Operation = FilterOperationType.Greater, Right = ExtractName(right) };

    /// <summary>
    /// Creates a "greater than or equal" filter operation.
    /// </summary>
    public static FilterOperation operator >=(Column left, object right) =>
        new() { Left = left._name, Operation = FilterOperationType.EGreater, Right = ExtractName(right) };

    /// <summary>
    /// Creates a "less than" filter operation.
    /// </summary>
    public static FilterOperation operator <(Column left, object right) =>
        new() { Left = left._name, Operation = FilterOperationType.Less, Right = ExtractName(right) };

    /// <summary>
    /// Creates a "less than or equal" filter operation.
    /// </summary>
    public static FilterOperation operator <=(Column left, object right) =>
        new() { Left = left._name, Operation = FilterOperationType.ELess, Right = ExtractName(right) };

    /// <summary>
    /// Creates an "equal" filter operation.
    /// </summary>
    public static FilterOperation operator ==(Column left, object right) =>
        new() { Left = left._name, Operation = FilterOperationType.Equal, Right = ExtractName(right) };

    /// <summary>
    /// Creates a "not equal" filter operation.
    /// </summary>
    public static FilterOperation operator !=(Column left, object right) =>
        new() { Left = left._name, Operation = FilterOperationType.NotEqual, Right = ExtractName(right) };

    /// <summary>
    /// Creates a filter operation checking if the column crosses another value.
    /// </summary>
    public FilterOperation Crosses(object other) =>
        new() { Left = _name, Operation = FilterOperationType.Crosses, Right = ExtractName(other) };

    /// <summary>
    /// Creates a filter operation checking if the column crosses above another value.
    /// </summary>
    public FilterOperation CrossesAbove(object other) =>
        new() { Left = _name, Operation = FilterOperationType.CrossesAbove, Right = ExtractName(other) };

    /// <summary>
    /// Creates a filter operation checking if the column crosses below another value.
    /// </summary>
    public FilterOperation CrossesBelow(object other) =>
        new() { Left = _name, Operation = FilterOperationType.CrossesBelow, Right = ExtractName(other) };

    /// <summary>
    /// Creates a filter operation checking if the column value is between two values.
    /// </summary>
    public FilterOperation Between(object left, object right) =>
        new() { Left = _name, Operation = FilterOperationType.InRange, Right = new[] { ExtractName(left), ExtractName(right) } };

    /// <summary>
    /// Creates a filter operation checking if the column value is not between two values.
    /// </summary>
    public FilterOperation NotBetween(object left, object right) =>
        new() { Left = _name, Operation = FilterOperationType.NotInRange, Right = new[] { ExtractName(left), ExtractName(right) } };

    /// <summary>
    /// Creates a filter operation checking if the column value is in a set of values.
    /// </summary>
    public FilterOperation IsIn(IEnumerable<object> values) =>
        new() { Left = _name, Operation = FilterOperationType.InRange, Right = values.Select(ExtractName).ToList() };

    /// <summary>
    /// Creates a filter operation checking if the column value is not in a set of values.
    /// </summary>
    public FilterOperation NotIn(IEnumerable<object> values) =>
        new() { Left = _name, Operation = FilterOperationType.NotInRange, Right = values.Select(ExtractName).ToList() };

    /// <summary>
    /// Creates a filter operation checking if the column (as a set) contains any of the specified values.
    /// </summary>
    public FilterOperation Has(IEnumerable<string> values) =>
        new() { Left = _name, Operation = FilterOperationType.Has, Right = values.ToList() };

    /// <summary>
    /// Creates a filter operation checking if the column (as a set) contains none of the specified values.
    /// </summary>
    public FilterOperation HasNoneOf(IEnumerable<string> values) =>
        new() { Left = _name, Operation = FilterOperationType.HasNoneOf, Right = values.ToList() };

    /// <summary>
    /// Creates a filter operation checking if the column value is in a day range.
    /// </summary>
    public FilterOperation InDayRange(int start, int end) =>
        new() { Left = _name, Operation = FilterOperationType.InDayRange, Right = new[] { start, end } };

    /// <summary>
    /// Creates a filter operation checking if the column value is in a week range.
    /// </summary>
    public FilterOperation InWeekRange(int start, int end) =>
        new() { Left = _name, Operation = FilterOperationType.InWeekRange, Right = new[] { start, end } };

    /// <summary>
    /// Creates a filter operation checking if the column value is in a month range.
    /// </summary>
    public FilterOperation InMonthRange(int start, int end) =>
        new() { Left = _name, Operation = FilterOperationType.InMonthRange, Right = new[] { start, end } };

    /// <summary>
    /// Creates a filter operation checking if the column value is above another value by a percentage.
    /// </summary>
    public FilterOperation AbovePercent(object column, double percentage) =>
        new() { Left = _name, Operation = FilterOperationType.AbovePercent, Right = new[] { ExtractName(column), percentage } };

    /// <summary>
    /// Creates a filter operation checking if the column value is below another value by a percentage.
    /// </summary>
    public FilterOperation BelowPercent(object column, double percentage) =>
        new() { Left = _name, Operation = FilterOperationType.BelowPercent, Right = new[] { ExtractName(column), percentage } };

    /// <summary>
    /// Creates a filter operation checking if the column value is between two percentage values relative to another column.
    /// </summary>
    public FilterOperation BetweenPercent(object column, double percentage1, double? percentage2 = null) =>
        new() { Left = _name, Operation = FilterOperationType.InRangePercent, Right = new[] { ExtractName(column), percentage1, percentage2 } };

    /// <summary>
    /// Creates a filter operation checking if the column value is not between two percentage values relative to another column.
    /// </summary>
    public FilterOperation NotBetweenPercent(object column, double percentage1, double? percentage2 = null) =>
        new() { Left = _name, Operation = FilterOperationType.NotInRangePercent, Right = new[] { ExtractName(column), percentage1, percentage2 } };

    /// <summary>
    /// Creates a filter operation checking if the column value matches a pattern.
    /// </summary>
    public FilterOperation Like(object pattern) =>
        new() { Left = _name, Operation = FilterOperationType.Match, Right = ExtractName(pattern) };

    /// <summary>
    /// Creates a filter operation checking if the column value does not match a pattern.
    /// </summary>
    public FilterOperation NotLike(object pattern) =>
        new() { Left = _name, Operation = FilterOperationType.NotMatch, Right = ExtractName(pattern) };

    /// <summary>
    /// Creates a filter operation checking if the column value is empty.
    /// </summary>
    public FilterOperation Empty() =>
        new() { Left = _name, Operation = FilterOperationType.Empty };

    /// <summary>
    /// Creates a filter operation checking if the column value is not empty.
    /// </summary>
    public FilterOperation NotEmpty() =>
        new() { Left = _name, Operation = FilterOperationType.NotEmpty };

    /// <summary>
    /// Required for operator overloading.
    /// </summary>
    public override bool Equals(object? obj) => throw new NotSupportedException("Use the == operator to create a filter operation.");

    /// <summary>
    /// Required for operator overloading.
    /// </summary>
    public override int GetHashCode() => _name.GetHashCode();
} 