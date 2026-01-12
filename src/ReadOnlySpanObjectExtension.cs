using System;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;

namespace Soenneker.Extensions.Spans.Readonly.Objects;

/// <summary>
/// Helpful extension methods surrounding ReadOnlySpan of Object
/// </summary>
public static class ReadOnlySpanObjectExtension
{
    /// <summary>
    /// Converts a span of objects to an array of their corresponding types.
    /// </summary>
    [Pure]
    public static Type[] ToTypes(this ReadOnlySpan<object> objects)
    {
        int length = objects.Length;

        if (length == 0)
            return [];

        if (length == 1)
            return [objects[0].GetType()];

        Type[] result = GC.AllocateUninitializedArray<Type>(length);

        for (var i = 0; i < length; i++)
            result[i] = objects[i].GetType();

        return result;
    }

    /// <summary>
    /// Fills the destination span with the corresponding types of the input objects (no allocations).
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void FillTypes(this ReadOnlySpan<object> objects, Span<Type> destination)
    {
        if (destination.Length < objects.Length)
            throw new ArgumentException("Destination span is too small.", nameof(destination));

        for (var i = 0; i < objects.Length; i++)
            destination[i] = objects[i].GetType();
    }
}
