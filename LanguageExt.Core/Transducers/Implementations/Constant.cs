﻿#nullable enable
namespace LanguageExt;

record ConstantTransducer<A, B>(B Value) : Transducer<A, B>
{
    public override Reducer<A, S> Transform<S>(Reducer<B, S> reduce) =>
        new Reduce<S>(Value, reduce);

    record Reduce<S>(B Value, Reducer<B, S> Reducer) : Reducer<A, S>
    {
        public override TResult<S> Run(TState st, S s, A _) =>
            Reducer.Run(st, s, Value);
    }

    public override string ToString() =>
        "const";
}
