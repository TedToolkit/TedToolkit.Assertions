using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;

using Shouldly;

namespace TedToolkit.Assertions.Benchmark;

/// <summary>
///
/// </summary>
[MemoryDiagnoser]
[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
[CategoriesColumn]
public class TestRunner
{
    private static readonly int _value = 10;

    /// <summary>
    ///
    /// </summary>
    [Benchmark]
    [BenchmarkCategory("Succeed")]
    public void Shouldly()
    {
        _value.ShouldBe(10);
    }

    /// <summary>
    ///
    /// </summary>
    [Benchmark]
    [BenchmarkCategory("Succeed")]
    public void FluentAssertion()
    {
        FluentAssertions.AssertionExtensions.Should(_value).Be(10);
    }

    /// <summary>
    ///
    /// </summary>
    [Benchmark]
    [BenchmarkCategory("Succeed")]
    public void TedToolkit()
    {
        _value.Must().Be(10);
    }

    /// <summary>
    ///
    /// </summary>
    [Benchmark]
    [BenchmarkCategory("Failed")]
    public void ShouldlyFailed()
    {
        try
        {
            _value.ShouldBe(0);
        }
        catch
        {
            //Ignore
        }
    }

    /// <summary>
    ///
    /// </summary>
    [Benchmark]
    [BenchmarkCategory("Failed")]
    public void FluentAssertionFailed()
    {
        try
        {
            FluentAssertions.AssertionExtensions.Should(_value).Be(0);
        }
        catch
        {
            //Ignore
        }
    }

    /// <summary>
    ///
    /// </summary>
    [Benchmark]
    [BenchmarkCategory("Failed")]
    public void TedToolkitFailed()
    {
        try
        {
            _value.Must().Be(0);
        }
        catch
        {
            //Ignore
        }
    }
}