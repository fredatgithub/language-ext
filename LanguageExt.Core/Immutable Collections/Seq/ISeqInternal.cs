﻿using System;
using System.Collections.Generic;

namespace LanguageExt;

internal enum SeqType
{
    Empty,
    Lazy,
    Strict,
    Concat
}

internal interface ISeqInternal<A> : IEnumerable<A>
{
    SeqType Type { get; }
    A this[int index] { get; }
    Option<A> At(int index);
    ISeqInternal<A> Add(A value);
    ISeqInternal<A> Cons(A value);
    A Head { get; }
    ISeqInternal<A> Tail { get; }
    bool IsEmpty { get; }
    ISeqInternal<A> Init { get; }
    A Last { get; }
    int Count { get; }
    S Fold<S>(S state, Func<S, A, S> f);
    S FoldBack<S>(S state, Func<S, A, S> f);
    ISeqInternal<A> Skip(int amount);
    ISeqInternal<A> Take(int amount);
    ISeqInternal<A> Strict();
    Unit Iter(Action<A> f);
    bool Exists(Func<A, bool> f);
    bool ForAll(Func<A, bool> f);
    int GetHashCode(int offsetBasis);
}
