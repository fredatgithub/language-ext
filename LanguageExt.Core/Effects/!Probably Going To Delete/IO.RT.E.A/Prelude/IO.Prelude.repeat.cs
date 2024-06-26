/*
using System;
using LanguageExt.Effects.Traits;

namespace LanguageExt;

public static partial class Prelude
{
    /// <summary>
    /// Keeps repeating the computation   
    /// </summary>
    /// <param name="ma">Computation to repeat</param>
    /// <typeparam name="RT">Runtime</typeparam>
    /// <typeparam name="A">Computation bound value type</typeparam>
    /// <returns>The result of the last invocation of `ma`</returns>
    public static IO<RT, E, A> repeat<RT, E, A>(IO<RT, E, A> ma)
        where RT : HasIO<RT, E> =>
        new(Transducer.repeat(Schedule.Forever, ma.Morphism));

    /// <summary>
    /// Keeps repeating the computation, until the scheduler expires  
    /// </summary>
    /// <param name="schedule">Scheduler strategy for repeating</param>
    /// <param name="ma">Computation to repeat</param>
    /// <typeparam name="RT">Runtime</typeparam>
    /// <typeparam name="A">Computation bound value type</typeparam>
    /// <returns>The result of the last invocation of `ma`</returns>
    public static IO<RT, E, A> repeat<RT, E, A>(Schedule schedule, IO<RT, E, A> ma)
        where RT : HasIO<RT, E> =>
        new(Transducer.repeat(schedule, ma.Morphism));

    /// <summary>
    /// Keeps repeating the computation until the predicate returns false
    /// </summary>
    /// <param name="ma">Computation to repeat</param>
    /// <typeparam name="RT">Runtime</typeparam>
    /// <typeparam name="A">Computation bound value type</typeparam>
    /// <returns>The result of the last invocation of `ma`</returns>
    public static IO<RT, E, A> repeatWhile<RT, E, A>(
        IO<RT, E, A> ma,
        Func<A, bool> predicate) where RT : HasIO<RT, E> =>
        new(Transducer.repeatWhile(Schedule.Forever, ma.Morphism, predicate));

    /// <summary>
    /// Keeps repeating the computation, until the scheduler expires, or the predicate returns false
    /// </summary>
    /// <param name="schedule">Scheduler strategy for repeating</param>
    /// <param name="ma">Computation to repeat</param>
    /// <typeparam name="RT">Runtime</typeparam>
    /// <typeparam name="A">Computation bound value type</typeparam>
    /// <returns>The result of the last invocation of `ma`</returns>
    public static IO<RT, E, A> repeatWhile<RT, E, A>(
        Schedule schedule,
        IO<RT, E, A> ma,
        Func<A, bool> predicate)
        where RT : HasIO<RT, E> =>
        new(Transducer.repeatWhile(schedule, ma.Morphism, predicate));

    /// <summary>
    /// Keeps repeating the computation until the predicate returns true
    /// </summary>
    /// <param name="ma">Computation to repeat</param>
    /// <typeparam name="RT">Runtime</typeparam>
    /// <typeparam name="A">Computation bound value type</typeparam>
    /// <returns>The result of the last invocation of `ma`</returns>
    public static IO<RT, E, A> repeatUntil<RT, E, A>(
        IO<RT, E, A> ma,
        Func<A, bool> predicate)
        where RT : HasIO<RT, E> =>
        new(Transducer.repeatUntil(Schedule.Forever, ma.Morphism, predicate));

    /// <summary>
    /// Keeps repeating the computation, until the scheduler expires, or the predicate returns true
    /// </summary>
    /// <param name="schedule">Scheduler strategy for repeating</param>
    /// <param name="ma">Computation to repeat</param>
    /// <typeparam name="RT">Runtime</typeparam>
    /// <typeparam name="A">Computation bound value type</typeparam>
    /// <returns>The result of the last invocation of `ma`</returns>
    public static IO<RT, E, A> repeatUntil<RT, E, A>(
        Schedule schedule,
        IO<RT, E, A> ma,
        Func<A, bool> predicate)
        where RT : HasIO<RT, E> =>
        new(Transducer.repeatUntil(schedule, ma.Morphism, predicate));
}
*/
