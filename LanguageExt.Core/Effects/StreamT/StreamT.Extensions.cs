using System;
using LanguageExt.Traits;

namespace LanguageExt;

/// <summary>
/// StreamT extensions
/// </summary>
public static class IterableTExtensions
{
    public static StreamT<M, A> As<M, A>(this K<StreamT<M>, A> ma)
        where M : Monad<M> =>
        (StreamT<M, A>)ma;

    public static MList<A> As<M, A>(this K<MList, A> ma)
        where M : Monad<M> =>
        (MList<A>)ma;

    public static K<M, Option<(A Head, StreamT<M, A> Tail)>> Run<M, A>(this K<StreamT<M>, A> mma)
        where M : Monad<M> =>
        mma.As().Run();

    /// <summary>
    /// Execute the stream's inner monad `M`, combining the results using
    /// its `Alternative<M>.Combine` operator.
    /// </summary>
    /// <param name="mma">Stream to combine</param>
    /// <returns>Result of the combined effects</returns>
    public static K<M, A> Combine<M, A>(this K<StreamT<M>, A> mma) 
        where M : Monad<M>, Alternative<M> =>
        mma.As().runListT.Combine();

    /// <summary>
    /// Execute the stream's inner monad `M`, combining the results using
    /// its `Alternative<M>.Combine` operator.
    /// </summary>
    /// <param name="mma">Stream to combine</param>
    /// <returns>Result of the combined effects</returns>
    static K<M, A> Combine<M, A>(this K<M, MList<A>> mma)
        where M : Monad<M>, Alternative<M> =>
        mma.Bind(ml => ml switch
                       {
                           MNil<A> =>
                               M.Empty<A>(),

                           MCons<M, A>(var head, var tail) =>
                               M.Combine(M.Pure(head), tail.Combine()),

                           MIter<M, A> iter =>
                               M.Combine(M.Pure(iter.Head), iter.TailM().Combine()),

                           _ => throw new NotSupportedException()
                       });

    public static StreamT<M, A> Flatten<M, A>(this K<StreamT<M>, StreamT<M, A>> mma)
        where M : Monad<M> =>
        new StreamMainT<M, A>(mma.As().runListT.Map(ml => ml.Map(ma => ma.runListT)).Flatten());

    public static StreamT<M, A> Flatten<M, A>(this K<StreamT<M>, K<StreamT<M>, A>> mma)
        where M : Monad<M> =>
        new StreamMainT<M, A>(mma.As().runListT.Map(ml => ml.Map(ma => ma.As().runListT)).Flatten());

    public static K<M, MList<A>> Flatten<M, A>(this K<M, MList<K<M, MList<A>>>> mma)
        where M : Monad<M> =>
        mma.Bind(la => la.Flatten());

    public static K<M, MList<A>> Flatten<M, A>(this MList<K<M, MList<A>>> mma)
        where M : Monad<M> =>
        mma switch
        {
            MNil<K<M, MList<A>>>                     => M.Pure(MNil<A>.Default),
            MCons<M, K<M, MList<A>>> (var h, var t)  => h.Append(t.Flatten()),
            MIter<M, K<M, MList<A>>> (var h, _) iter => h.Append(iter.TailM().Flatten()),
            _                                        => throw new NotSupportedException()
        };

    public static K<M, MList<A>> Append<M, A>(this K<M, MList<A>> xs, K<M, MList<A>> ys)
        where M : Monad<M> =>
        xs.Bind(x => x.Append(ys));

    public static StreamT<M, B> Bind<M, A, B>(this Pure<A> ma, Func<A, StreamT<M, B>> f)
        where M : Monad<M> =>
        StreamT<M>.pure(ma.Value).Bind(f);

    public static StreamT<M, B> Bind<M, A, B>(this Pure<A> ma, Func<A, K<StreamT<M>, B>> f)
        where M : Monad<M> =>
        StreamT<M>.pure(ma.Value).Bind(f);

    public static StreamT<M, B> Bind<M, A, B>(this IO<A> ma, Func<A, StreamT<M, B>> f)
        where M : Monad<M> =>
        StreamT<M>.liftIO(ma).Bind(f);

    public static StreamT<M, B> Bind<M, A, B>(this IO<A> ma, Func<A, K<StreamT<M>, B>> f)
        where M : Monad<M> =>
        StreamT<M>.liftIO(ma).Bind(f);

    public static StreamT<M, C> SelectMany<M, A, B, C>(
        this Pure<A> ma,
        Func<A, StreamT<M, B>> bind,
        Func<A, B, C> project)
        where M : Monad<M> =>
        StreamT<M>.pure(ma.Value).SelectMany(bind, project);

    public static StreamT<M, C> SelectMany<M, A, B, C>(
        this Pure<A> ma,
        Func<A, K<StreamT<M>, B>> bind,
        Func<A, B, C> project)
        where M : Monad<M> =>
        StreamT<M>.pure(ma.Value).SelectMany(bind, project);

    public static StreamT<M, C> SelectMany<M, A, B, C>(
        this IO<A> ma,
        Func<A, StreamT<M, B>> bind,
        Func<A, B, C> project)
        where M : Monad<M> =>
        StreamT<M>.liftIO(ma).SelectMany(bind, project);

    public static StreamT<M, C> SelectMany<M, A, B, C>(
        this IO<A> ma,
        Func<A, K<StreamT<M>, B>> bind,
        Func<A, B, C> project)
        where M : Monad<M> =>
        StreamT<M>.liftIO(ma).SelectMany(bind, project);
}
