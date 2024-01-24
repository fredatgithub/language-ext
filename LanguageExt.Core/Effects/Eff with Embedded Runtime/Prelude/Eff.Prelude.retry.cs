#nullable enable
using System;
using LanguageExt.Common;
using LanguageExt.Effects.Traits;

namespace LanguageExt;

public static partial class Prelude
{
    /// <summary>
    /// Keeps retrying the computation   
    /// </summary>
    /// <param name="ma">Computation to retry</param>
    /// <typeparam name="RT">Runtime</typeparam>
    /// <typeparam name="A">Computation bound value type</typeparam>
    /// <returns>The result of the last invocation of ma</returns>
    public static Eff<RT, A> retry<RT, A>(Eff<RT, A> ma)
        where RT : HasIO<RT, Error> =>
        new(retry(ma.As));

    /// <summary>
    /// Keeps retrying the computation, until the scheduler expires  
    /// </summary>
    /// <param name="schedule">Scheduler strategy for retrying</param>
    /// <param name="ma">Computation to retry</param>
    /// <typeparam name="RT">Runtime</typeparam>
    /// <typeparam name="A">Computation bound value type</typeparam>
    /// <returns>The result of the last invocation of ma</returns>
    public static Eff<RT, A> retry<RT, A>(Schedule schedule, Eff<RT, A> ma)
        where RT : HasIO<RT, Error> =>
        new(retry(schedule, ma.As));

    /// <summary>
    /// Keeps retrying the computation until the predicate returns false
    /// </summary>
    /// <param name="ma">Computation to retry</param>
    /// <typeparam name="RT">Runtime</typeparam>
    /// <typeparam name="A">Computation bound value type</typeparam>
    /// <returns>The result of the last invocation of ma</returns>
    public static Eff<RT, A> retryWhile<RT, A>(
        Eff<RT, A> ma,
        Func<Error, bool> predicate) where RT : HasIO<RT, Error> =>
        new(retryWhile(ma.As, predicate));

    /// <summary>
    /// Keeps retrying the computation, until the scheduler expires, or the predicate returns false
    /// </summary>
    /// <param name="schedule">Scheduler strategy for retrying</param>
    /// <param name="ma">Computation to retry</param>
    /// <typeparam name="RT">Runtime</typeparam>
    /// <typeparam name="A">Computation bound value type</typeparam>
    /// <returns>The result of the last invocation of ma</returns>
    public static Eff<RT, A> retryWhile<RT, A>(
        Schedule schedule,
        Eff<RT, A> ma,
        Func<Error, bool> predicate)
        where RT : HasIO<RT, Error> =>
        new(retryWhile(schedule, ma.As, predicate));

    /// <summary>
    /// Keeps retrying the computation until the predicate returns true
    /// </summary>
    /// <param name="ma">Computation to retry</param>
    /// <typeparam name="RT">Runtime</typeparam>
    /// <typeparam name="A">Computation bound value type</typeparam>
    /// <returns>The result of the last invocation of ma</returns>
    public static Eff<RT, A> retryUntil<RT, A>(
        Eff<RT, A> ma,
        Func<Error, bool> predicate)
        where RT : HasIO<RT, Error> =>
        new(retryUntil(ma.As, predicate));

    /// <summary>
    /// Keeps retrying the computation, until the scheduler expires, or the predicate returns true
    /// </summary>
    /// <param name="schedule">Scheduler strategy for retrying</param>
    /// <param name="ma">Computation to retry</param>
    /// <typeparam name="RT">Runtime</typeparam>
    /// <typeparam name="A">Computation bound value type</typeparam>
    /// <returns>The result of the last invocation of ma</returns>
    public static Eff<RT, A> retryUntil<RT, A>(
        Schedule schedule,
        Eff<RT, A> ma,
        Func<Error, bool> predicate)
        where RT : HasIO<RT, Error> =>
        new(retryUntil(schedule, ma.As, predicate));
}
