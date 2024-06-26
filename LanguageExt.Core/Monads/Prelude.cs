﻿using LanguageExt.Traits;

namespace LanguageExt;

public static partial class Prelude
{
    /// <summary>
    /// Construct identity monad
    /// </summary>
    public static Identity<A> Id<A>(A value) =>
        Identity<A>.Pure(value);

    /// <summary>
    /// Create a new Pure monad.  This monad doesn't do much, but when combined with
    /// other monads, it allows for easier construction of pure lifted values.
    ///
    /// There are various bind operators that make it work with these types:
    ///
    ///     * Option
    ///     * Eff
    ///     * Either
    ///     * Fin
    ///     * IO
    ///     * Validation
    ///     
    /// </summary>
    /// <param name="value">Value to lift</param>
    /// <typeparam name="A">Bound value type</typeparam>
    /// <returns>Pure monad</returns>
    public static Pure<A> Pure<A>(A value) =>
        new(value);
        
    /// <summary>
    /// Create a new Fail monad: the monad that always fails.  This monad doesn't do much,
    /// but when combined with other monads, it allows for easier construction of lifted 
    /// failure values.
    ///
    /// There are various bind operators that make it work with these types:
    ///
    ///     * Option
    ///     * Eff
    ///     * Either
    ///     * Fin
    ///     * IO
    ///     * Validation
    ///     
    /// </summary>
    /// <param name="value">Value to lift</param>
    /// <typeparam name="A">Bound value type</typeparam>
    /// <returns>Pure monad</returns>
    public static Fail<E> Fail<E>(E error) =>
        new(error);
    
    /// <summary>
    /// Lift the IO monad into a transformer-stack with an IO as its innermost monad.
    /// </summary>
    public static K<T, A> liftIO<T, M, A>(IO<A> ma)
        where T : MonadT<T, M>
        where M : Monad<M> => 
        T.Lift(M.LiftIO(ma));
}
