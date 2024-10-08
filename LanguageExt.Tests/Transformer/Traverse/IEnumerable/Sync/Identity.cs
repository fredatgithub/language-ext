using System.Linq;
using Xunit;

namespace LanguageExt.Tests.Transformer.Traverse.IEnumerableT.Sync;

public class IdentityIEnumerable
{
    [Fact]
    public void IdEmptyIsEmpty()
    {
        var ma = Id(Iterable.empty<int>());
        var mb = ma.Traverse(identity).As();
        var mc = Iterable.empty<Identity<int>>();

        Assert.True(mb.ToSeq() == mc.ToSeq());
    }

    [Fact]
    public void IdNonEmptyIEnumerableIsIEnumerableId()
    {
        var ma = Id(Iterable.create([1, 2, 3]));
        var mb = ma.Traverse(identity).As();
        var mc = new[] { Id(1), Id(2), Id(3) }.AsEnumerable();

        Assert.True(mb.ToSeq() == mc.AsIterable().ToSeq());
    }
}
