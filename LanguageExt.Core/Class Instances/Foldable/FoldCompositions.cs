﻿using static LanguageExt.Prelude;
using LanguageExt.TypeClasses;
using System;

namespace LanguageExt.ClassInstances;

public struct FoldCompositions<A> : Foldable<Compositions<A>, A>
{
    static S FoldNode<S>(S state, Func<S, A, S> f, Compositions<A>.Node node)
    {
        if (node.Children.IsNone) return f(state, node.Value);
        var (l, r) = ((Compositions<A>.Node, Compositions<A>.Node))node.Children;
        state      = FoldNode(state, f, l);
        state      = FoldNode(state, f, r);
        return state;
    }

    static S FoldNodes<S>(S state, Func<S, A, S> f, Seq<Compositions<A>.Node> nodes) =>
        nodes.Fold(state, (s, n) => FoldNode(s, f, n));

    static S FoldNodeBack<S>(S state, Func<S, A, S> f, Compositions<A>.Node node)
    {
        if (node.Children.IsNone) return f(state, node.Value);
        var (l, r) = ((Compositions<A>.Node, Compositions<A>.Node))node.Children;
        state      = FoldNode(state, f, r);
        state      = FoldNode(state, f, l);
        return state;
    }

    static S FoldNodesBack<S>(S state, Func<S, A, S> f, Seq<Compositions<A>.Node> nodes) =>
        nodes.FoldBack(state, (s, n) => FoldNode(s, f, n));

    internal static Seq<B> FoldMap<S, B>(Func<A, B> f, Seq<Compositions<A>.Node> nodes) =>
        FoldNodes(Seq<B>(), (s, n) => f(n).Cons(s), nodes);

    internal static Seq<B> FoldMapBack<S, B>(Func<A, B> f, Seq<Compositions<A>.Node> nodes) =>
        FoldNodesBack(Seq<B>(), (s, n) => f(n).Cons(s), nodes);

    public static Func<Unit, int> Count(Compositions<A> fa) => _ =>
        FoldNodes(0, (s, n) => s + 1, fa.Tree);

    public static Func<Unit, S> Fold<S>(Compositions<A> fa, S state, Func<S, A, S> f) => _ =>
        FoldNodes(state, f, fa.Tree);

    public static Func<Unit, S> FoldBack<S>(Compositions<A> fa, S state, Func<S, A, S> f) => _ =>
        FoldNodesBack(state, f, fa.Tree);
}
