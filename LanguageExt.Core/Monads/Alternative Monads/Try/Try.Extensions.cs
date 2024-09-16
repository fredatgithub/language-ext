﻿using System;
using static LanguageExt.Prelude;
using System.Diagnostics.Contracts;
using LanguageExt.Traits;

namespace LanguageExt;

/// <summary>
/// Either monad extensions
/// </summary>
public static class TryExt
{
    public static Try<A> As<A>(this K<Try, A> ma) =>
        (Try<A>)ma;

    /// <summary>
    /// Run the `Try`
    /// </summary>
    public static Fin<A> Run<A>(this K<Try, A> ma)
    {
        try
        {
            return ma.As().runTry();
        }
        catch (Exception e)
        {
            return Fin<A>.Fail(e);
        }
    }

    /// <summary>
    /// Monadic join
    /// </summary>
    [Pure]
    public static Try<A> Flatten<A>(this K<Try, Try<A>> mma) =>
        new(() =>
                mma.As().Run() switch
                {
                    Fin.Succ<Try<A>> (var succ) => succ.Run(),
                    Fin.Fail<Try<A>> (var fail) => FinFail<A>(fail),
                    _                           => throw new NotSupportedException()
                });

    /// <summary>
    /// Monadic join
    /// </summary>
    [Pure]
    public static Try<A> Flatten<A>(this K<Try, K<Try, A>> mma) =>
        new(() =>
                mma.Run() switch
                {
                    Fin.Succ<K<Try, A>> (var succ) => succ.Run(),
                    Fin.Fail<K<Try, A>> (var fail) => FinFail<A>(fail),
                    _                              => throw new NotSupportedException()
                });

    /// <summary>
    /// Applicative apply
    /// </summary>
    [Pure]
    public static Try<B> Apply<A, B>(this Try<Func<A, B>> mf, Try<A> ma) => 
        mf.Kind().Apply(ma).As();

    /// <summary>
    /// Applicative action
    /// </summary>
    [Pure]
    public static Try<B> Action<A, B>(this Try<A> ma, Try<B> mb) => 
        ma.Kind().Action(mb).As();
}