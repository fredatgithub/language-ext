﻿using System;
using static LanguageExt.Prelude;

namespace LanguageExt.Traits;

public interface StateM<in M, S>  
    where M : StateM<M, S>
{
    public static abstract K<M, Unit> Put(S value);

    public static abstract K<M, Unit> Modify(Func<S, S> modify) ;

    public static abstract K<M, A> Gets<A>(Func<S, A> f);

    public static virtual K<M, S> Get =>
        StateM.gets<M, S, S>(identity);
}
