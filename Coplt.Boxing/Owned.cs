using System;
using System.Runtime.CompilerServices;
using System.Threading;
using Coplt.Dropping;

namespace Coplt.Boxing;

[Dropping(Unmanaged = true)]
public partial class Owned<T> : Box<T> where T : IDisposable
{
    private int m_disposed;

    public Owned() => m_disposed = 1;
    public Owned(T data) : base(data) { }

    [Drop]
    private void Drop(bool disposing)
    {
        if (Interlocked.Exchange(ref m_disposed, 1) == 1) return;
        Value.Dispose();
        if (disposing && RuntimeHelpers.IsReferenceOrContainsReferences<T>()) Value = default!;
    }

    public T Leak()
    {
        if (Interlocked.Exchange(ref m_disposed, 1) == 1) return default!;
        var r = Value;
        if (RuntimeHelpers.IsReferenceOrContainsReferences<T>()) Value = default!;
        return r;
    }

    public Owned<T> Move()
    {
        if (Interlocked.Exchange(ref m_disposed, 1) == 1) return new();
        var r = Value;
        if (RuntimeHelpers.IsReferenceOrContainsReferences<T>()) Value = default!;
        return new(r);
    }
}
