﻿using System.Runtime.CompilerServices;
using FluentAssertions;
using Xunit;

namespace Core.Enterprise.Plugins.FP;

public class Design_TaskExtensions
{
    private string MapToItself(string itself) => itself;
    private int Parse(string text) => int.Parse(text);
    private DateTime ToDate(int year) => new DateTime(year, 1, 1);

    [Fact]
    public void ToTask_Conver_To_Task()
    {
        var year = 1984;

        var yearTask = year.ToTask();

        yearTask.Should().NotBeNull();
        yearTask.Should().BeOfType<Task<int>>();
        yearTask.Result.Should().Be(year);

    }
    [Fact]
    public async Task FromResult()
    {
        var task = "1984".ToTask();

        var result = await task;

        result.Should().Be("1984");
    }
    [Fact]
    public async Task Select_Lambda()
    {
        var task = "1984".ToTask();

        var result = await task.Select(Parse);

        result.Should().Be(1984);
    }
    [Fact]
    public async Task Select_LinqAsync()
    {
        var task = "1984".ToTask();

        var result = await (
            from subResult in task
            select Parse(subResult));

        result.Should().Be(1984);
    }
    [Fact]
    public async void Map_Functor_1()
    {
        var task = "1984".ToTask();

        var result = await task.Select(MapToItself);

        result.Should().Be("1984");
    }
    [Fact]
    public async void Map_Functor_2()
    {
        var task = "1984".ToTask();

        var sequentialResult = await task.Select(Parse).Select(ToDate);
        var nestedResult = await task.Select(c => ToDate(Parse(c)));

        sequentialResult.Should().Be(nestedResult);
    }
    [Fact]
    public async void Join()
    {
        var task = "1984".ToTask();
        var nestedTask = task.ToTask();

        var result = await nestedTask.Join();

        result.Should().Be("1984");
    }
    [Fact]
    public async void SelectMany_Lambda()
    {
        var task = "1984".ToTask();

        var result = await task.SelectMany(text => Parse(text).ToTask());

        result.Should().Be(1984);
    }
    [Fact]
    public async void SelectMany_Linq()
    {
        var task = "1984".ToTask();

        var result = await (
            from result1 in task
            from result2 in Parse(result1).ToTask()
            select result2);

        result.Should().Be(1984);
    }
}

public static class TaskExtensions
{
    public static Task<T> ToTask<T>(this T t) => Task.FromResult(t);

    public static async Task<T> Join<T>(this Task<Task<T>> nestedTaskT)
    {
        var taskT = await nestedTaskT; 
        var t = await taskT;
        return t;
    }

    public async static Task<R> Select<T, R>(this Task<T> taskT, Func<T, R> mapT2R)
    {
        var t = await taskT;
        var r = mapT2R(t);
        return r;
    }
  

    public static async Task<R> SelectMany<T, R>(this Task<T> taskT, Func<T, Task<R>> mapT2TaskR)
    {
        var t = await taskT;
        var taskR = mapT2TaskR(t);
        var r = await taskR;
        return r;
        //taskT.Select(mapT2TaskR).Join<R>();
    }
    public async static Task<R> SelectMany<T, U, R>(this Task<T> taskT, Func<T, Task<U>> mapT2TaskU, Func<T, U, R> mapTU2R)
    {
        var t = await taskT;
        var u = await mapT2TaskU(t);
        var r = mapTU2R(t, u);
        return r;
    }


    //Map(mapT2BoxU).Flatten<U>().Map(u => mapTU2R(Content, u));  //Bind(t => mapT2BoxU(t).Map(u => mapTU2R(t, u)));

    public async static void FireAndForget(this Task task) =>
     task.FireAndForget<Exception>(returnToCallerTread: false, handleException: ex => { }, retrhrow: true);

    public async static void FireAndForget<TException>(this Task task, bool returnToCallerTread, Action<TException> handleException, bool retrhrow) where TException : Exception
    {
        try
        {
            await task.ConfigureAwait(returnToCallerTread);
        } catch (TException ex)
        {
            handleException(ex);

            if (retrhrow)
                throw;
        }
    }


    public static async IAsyncEnumerable<TResult> Yield<TResult, TFrom>(this IEnumerable<TFrom> list,
        Func<TFrom, CancellationToken, Task<TResult>> factory,
        [EnumeratorCancellation] CancellationToken token)
    {
        var tasks = list.Select(item => factory(item, token)).ToList();
        while (tasks.Count > 0)
        {
            var completedTask = await Task.WhenAny(tasks).ConfigureAwait(false);
            tasks.Remove(completedTask);
            yield return await completedTask.ConfigureAwait(false);
        }
    }
}