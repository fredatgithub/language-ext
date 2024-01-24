﻿using LanguageExt.TypeClasses;
using static LanguageExt.Prelude;
using System;
using System.Linq;
using System.Diagnostics.Contracts;

namespace LanguageExt.ClassInstances;

/// <summary>
/// Hash set trait instance
/// </summary>
/// <typeparam name="A"></typeparam>
public struct MHashSet<A> :
    Monad<HashSet<A>, A>,
    Eq<HashSet<A>>,
    Monoid<HashSet<A>>
{
    [Pure]
    public static HashSet<A> Append(HashSet<A> x, HashSet<A> y) =>
        HashSet.createRange(x.ConcatFast(y));

    [Pure]
    public static MB Bind<MONADB, MB, B>(HashSet<A> ma, Func<A, MB> f) where MONADB : Monad<Unit, Unit, MB, B> =>
        ma.Fold(MONADB.Zero(), (s, a) => MONADB.Plus(s, f(a)));

    [Pure]
    public static Func<Unit, int> Count(HashSet<A> fa) =>
        _ => fa.Count();

    [Pure]
    public static HashSet<A> Subtract(HashSet<A> x, HashSet<A> y) =>
        HashSet.createRange(Enumerable.Except(x, y));

    [Pure]
    public static HashSet<A> Empty() =>
        HashSet.empty<A>();

    [Pure]
    public static bool Equals(HashSet<A> x, HashSet<A> y) =>
        x == y;

    [Pure]
    public static HashSet<A> Fail(object? err = null) =>
        Empty();

    [Pure]
    public static Func<Unit, S> Fold<S>(HashSet<A> fa, S state, Func<S, A, S> f) =>
        _ => fa.Fold(state, f);

    [Pure]
    public static Func<Unit, S> FoldBack<S>(HashSet<A> fa, S state, Func<S, A, S> f) =>
        _ => fa.FoldBack(state, f);

    [Pure]
    public static HashSet<A> Plus(HashSet<A> ma, HashSet<A> mb) =>
        ma + mb;

    [Pure]
    public static HashSet<A> Return(Func<Unit, A> f) =>
        HashSet(f(unit));

    [Pure]
    public static HashSet<A> Zero() =>
        Empty();

    [Pure]
    public static int GetHashCode(HashSet<A> x) =>
        x.GetHashCode();

    [Pure]
    public static HashSet<A> Run(Func<Unit, HashSet<A>> ma) =>
        ma(unit);

    [Pure]
    public static HashSet<A> BindReturn(Unit maOutput, HashSet<A> mb) =>
        mb;

    [Pure]
    public static HashSet<A> Return(A x) =>
        Return(_ => x);

    [Pure]
    public static HashSet<A> Apply(Func<A, A, A> f, HashSet<A> fa, HashSet<A> fb) =>
        from a in fa
        from b in fb
        select f(a, b);
}
