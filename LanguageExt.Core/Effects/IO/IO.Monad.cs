using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LanguageExt.Common;
using LanguageExt.Traits;

namespace LanguageExt;

public partial class IO : 
    Monad<IO>, 
    Fallible<IO>,
    Alternative<IO>
{
    public static IO<A> pure<A>(A value) => 
        IO<A>.Pure(value);
    
    static K<IO, B> Monad<IO>.Bind<A, B>(K<IO, A> ma, Func<A, K<IO, B>> f) =>
        ma.As().Bind(f);

    static K<IO, B> Functor<IO>.Map<A, B>(Func<A, B> f, K<IO, A> ma) => 
        ma.As().Map(f);

    static K<IO, A> Applicative<IO>.Pure<A>(A value) => 
        IO<A>.Pure(value);

    static K<IO, B> Applicative<IO>.Apply<A, B>(K<IO, Func<A, B>> mf, K<IO, A> ma) => 
        mf.As().Bind(ma.As().Map);

    static K<IO, B> Applicative<IO>.Action<A, B>(K<IO, A> ma, K<IO, B> mb) =>
        ma.As().Bind(_ => mb);
    
    static K<IO, A> Applicative<IO>.Actions<A>(IEnumerable<K<IO, A>> fas) =>
        IO<A>.LiftAsync(
            async envIO =>
            {
                A? rs = default;
                foreach (var kfa in fas)
                {
                    var fa = kfa.As();
                    rs = await fa.RunAsync(envIO);
                }
                if (rs is null) throw Exceptions.SequenceEmpty;
                return rs;
            });    

    static K<IO, A> MonoidK<IO>.Empty<A>() =>
        IO<A>.Empty;

    static K<IO, A> SemigroupK<IO>.Combine<A>(K<IO, A> ma, K<IO, A> mb) => 
        ma.As() | mb.As();

    static K<IO, A> MonadIO<IO>.LiftIO<A>(IO<A> ma) => 
        ma;

    static K<IO, IO<A>> MonadIO<IO>.ToIO<A>(K<IO, A> ma) => 
        pure(ma.As());

    static K<IO, B> MonadIO<IO>.MapIO<A, B>(K<IO, A> ma, Func<IO<A>, IO<B>> f) =>
        f(ma.As());

    static K<IO, A> Fallible<Error, IO>.Fail<A>(Error error) =>
        fail<A>(error);

    static K<IO, A> Fallible<Error, IO>.Catch<A>(K<IO, A> fa, Func<Error, bool> Predicate, Func<Error, K<IO, A>> Fail) =>
        fa.As().Catch(Predicate, Fail);
}
