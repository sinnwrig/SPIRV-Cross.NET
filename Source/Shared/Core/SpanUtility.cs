
namespace SPIRVCross.NET;

public static class SpanUtility
{
    public static unsafe ReadOnlySpan<T> FromNativePtr<T>(T* ptr, int ptrLength) where T : unmanaged
    {
        // ReadOnlySpan<T> nativeSpan = new(ptr, ptrLength);
        // Span<T> managedSpan = new T[ptrLength];
        // nativeSpan.CopyTo(managedSpan);
        return new ReadOnlySpan<T>(ptr, ptrLength);
    }
}