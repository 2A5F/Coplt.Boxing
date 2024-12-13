using System.Runtime.CompilerServices;
using Coplt.Boxing;

namespace Tests;

public class Tests
{
    [SetUp]
    public void Setup() { }

    [Test]
    public void Test1()
    {
        var box = new Boxed<int>(123);
        Assert.That(box.ValueRef, Is.EqualTo(123));
        box.ValueRef = 456;
        Assert.That(box.ValueRef, Is.EqualTo(456));
    }

    [Test]
    public void Test2()
    {
        var box = new Boxed<string>("asd");
        Assert.That(Unsafe.IsNullRef(ref box.ValueRef), Is.True);
        Assert.That(box.Value, Is.EqualTo("asd"));
    }
}
