﻿using System;

namespace LanguageExt;

record MapTransducer<A, B, C>(Transducer<A, B> F, Func<B, C> G) : 
    Transducer<A, C>
{
    public override Reducer<A, S> Transform<S>(Reducer<C, S> reduce) =>
        F.Transform(new Mapper<S>(G, reduce));
    
    record Mapper<S>(Func<B, C> G, Reducer<C, S> Reducer) : Reducer<B, S>
    {
        public override TResult<S> Run(TState st, S s, B b) =>
            TResult.Recursive(st, s, G(b), Reducer);
    }
            
    public override string ToString() =>  
        "map";
}
