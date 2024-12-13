using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using InlineIL;
using static InlineIL.IL.Emit;

namespace Coplt.Boxing;

public readonly struct Boxed<T>(object value)
{
    public static bool IsValueType => typeof(T).IsValueType;

    /// <summary>
    /// Try get the value ref, if <see cref="T"/> is class will return null
    /// </summary>
    public ref T TryGetValueRef(out bool success)
    {
        if (IsValueType)
        {
            success = true;
            return ref GetValueTypeRef(value);
        }
        else
        {
            success = false;
            return ref Unsafe.NullRef<T>();
        }
    }

    /// <summary>
    /// Try get the value ref, if <see cref="T"/> is class will return null
    /// </summary>
    public ref T ValueRef => ref IsValueType ? ref GetValueTypeRef(value) : ref Unsafe.NullRef<T>();

    public T Value => IsValueType ? GetValueTypeRef(value) : (T)value;

    private static ref T GetValueTypeRef(object value)
    {
        Ldarg_0();
        Unbox<T>();
        return ref IL.ReturnRef<T>();
    }
}
