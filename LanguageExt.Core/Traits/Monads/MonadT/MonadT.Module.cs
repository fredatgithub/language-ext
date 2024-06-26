﻿namespace LanguageExt.Traits;

public static partial class MonadT
{
    public static K<MTran, A> lift<MTran, M, A>(K<M, A> ma)
        where M : Monad<M>
        where MTran : MonadT<MTran, M> =>
        MTran.Lift(ma);
}
