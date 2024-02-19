﻿using static LanguageExt.Prelude;
using LanguageExt.TypeClasses;

namespace LanguageExt.ClassInstances;

public struct MCompositions<A> : 
    Monoid<Compositions<A>> 
    where A : Monoid<A>
{
    public Compositions<A> Append(Compositions<A> compy)
    {
        var compx = this;
        Seq<Compositions<A>.Node> go(Seq<Compositions<A>.Node> mx, Seq<Compositions<A>.Node> my)
        {
            if (mx.IsEmpty) return my;
            if (my.IsEmpty) return go(mx.Tail, [mx.Head]);

            var x = mx.Head;
            var sx = mx.Head.Size;
            var cx = mx.Head.Children;
            var vx = mx.Head.Value;
            var xs = mx.Tail;

            var y = my.Head;
            var sy = my.Head.Size;
            var vy = my.Head.Value;
            var ys = my.Tail;

            var ord = sx.CompareTo(sy);
            if (ord      < 0) return go(xs, x.Cons(my));
            else if (ord > 0)
            {
                var (l, r) = ((Compositions<A>.Node, Compositions<A>.Node))cx;
                return go(r.Cons(l.Cons(xs)), my);
            }
            else
            {
                return go(new Compositions<A>.Node(sx + sy, Some((x, y)), vx.Append(vy)).Cons(xs), ys);
            }
        }
        return new Compositions<A>(go(compx.Tree, compy.Tree));
    }

    public static Compositions<A> Empty => Compositions<A>.Empty;
}
