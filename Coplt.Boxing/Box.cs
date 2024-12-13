using System.Runtime.CompilerServices;

namespace Coplt.Boxing;

public abstract class Box
{
    public abstract ref T TryGet<T>();
}

public class Box<T> : Box
{
    internal T m_data = default!;

    public Box() { }
    public Box(T data) => m_data = data;

    public ref T Value => ref m_data;

    public override ref T1 TryGet<T1>()
    {
        if (typeof(T1) != typeof(T)) return ref Unsafe.NullRef<T1>();
        return ref Unsafe.As<T, T1>(ref Value);
    }
    
    public Boxed<T> Boxed => new(this);
}
