using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Collections.Internal
{
	// Token: 0x020004B4 RID: 1204
	[NullableContext(1)]
	[Nullable(0)]
	internal static class RoslynUnsafe
	{
		// Token: 0x06003035 RID: 12341 RVA: 0x00089F80 File Offset: 0x00088180
		[MethodImpl(256)]
		public static ref T NullRef<[Nullable(2)] T>()
		{
			return Unsafe.AsRef<T>(null);
		}

		// Token: 0x06003036 RID: 12342 RVA: 0x00089F89 File Offset: 0x00088189
		[MethodImpl(256)]
		public static bool IsNullRef<[Nullable(2)] T>(ref T source)
		{
			return Unsafe.AsPointer<T>(ref source) == null;
		}
	}
}
