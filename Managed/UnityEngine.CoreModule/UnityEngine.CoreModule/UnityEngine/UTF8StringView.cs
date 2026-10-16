using System;
using System.Text;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x020002EF RID: 751
	[NativeHeader("Runtime/Export/Unsafe/UTF8StringView.bindings.h")]
	internal readonly struct UTF8StringView
	{
		// Token: 0x060024DE RID: 9438 RVA: 0x00047308 File Offset: 0x00045508
		public unsafe UTF8StringView(ReadOnlySpan<byte> prefixUtf8Span)
		{
			fixed (byte* ptr = prefixUtf8Span[0])
			{
				byte* ptr2 = ptr;
				this.utf8Ptr = new IntPtr((void*)ptr2);
			}
			this.utf8Length = prefixUtf8Span.Length;
		}

		// Token: 0x060024DF RID: 9439 RVA: 0x00047340 File Offset: 0x00045540
		public UTF8StringView(IntPtr ptr, int lengthUtf8)
		{
			this.utf8Ptr = ptr;
			this.utf8Length = lengthUtf8;
		}

		// Token: 0x060024E0 RID: 9440 RVA: 0x00047351 File Offset: 0x00045551
		public unsafe UTF8StringView(byte* ptr, int lengthUtf8)
		{
			this.utf8Ptr = new IntPtr((void*)ptr);
			this.utf8Length = lengthUtf8;
		}

		// Token: 0x060024E1 RID: 9441 RVA: 0x00047368 File Offset: 0x00045568
		public unsafe override string ToString()
		{
			return Encoding.UTF8.GetString((byte*)this.utf8Ptr.ToPointer(), this.utf8Length);
		}

		// Token: 0x04000A77 RID: 2679
		public readonly IntPtr utf8Ptr;

		// Token: 0x04000A78 RID: 2680
		public readonly int utf8Length;
	}
}
