using System;

namespace IFBOptLib.IFBGraphics
{
    public static partial class IFBMesh
    {
        internal struct DisposableIntPtr : IDisposable
        {
            public readonly IntPtr Ptr;
            private DisposableIntPtr(IntPtr ptr)
            {
                Ptr = ptr;
            }
            public void Dispose()
            {
                FreePointerSafe(Ptr);
            }

            public static DisposableIntPtr Create(IntPtr ptr)
            {
                return new DisposableIntPtr(ptr);
            }
        }
    }
}
