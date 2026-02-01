// See https://aka.ms/new-console-template for more information

using BenchmarkDotNet.Running;

using Shouldly;

using TedToolkit.Assertions;
using TedToolkit.Assertions.Benchmark;
using TedToolkit.Assertions.CustomAssertionItems;
using TedToolkit.Scopes;

Console.WriteLine("Hello, World!");

int? value = 10;
var values = new List<int>();

// using (new AssertionScope().FastPush())
//     value.Must().Not.HaveValue("Silence");

BenchmarkRunner.Run<TestRunner>();