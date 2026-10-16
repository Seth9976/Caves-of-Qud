using System;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x0200018C RID: 396
	[NativeClass("GfxBufferID")]
	[NativeHeader("Runtime/GfxDevice/GfxDeviceTypes.h")]
	public readonly struct GraphicsBufferHandle : IEquatable<GraphicsBufferHandle>
	{
		// Token: 0x06001344 RID: 4932 RVA: 0x00021318 File Offset: 0x0001F518
		public override int GetHashCode()
		{
			return this.value.GetHashCode();
		}

		// Token: 0x06001345 RID: 4933 RVA: 0x00021338 File Offset: 0x0001F538
		public override bool Equals(object obj)
		{
			bool flag = obj is GraphicsBufferHandle;
			return flag && this.Equals((GraphicsBufferHandle)obj);
		}

		// Token: 0x06001346 RID: 4934 RVA: 0x00021368 File Offset: 0x0001F568
		public bool Equals(GraphicsBufferHandle other)
		{
			return this.value == other.value;
		}

		// Token: 0x06001347 RID: 4935 RVA: 0x00021388 File Offset: 0x0001F588
		public int CompareTo(GraphicsBufferHandle other)
		{
			return this.value.CompareTo(other.value);
		}

		// Token: 0x06001348 RID: 4936 RVA: 0x000213AC File Offset: 0x0001F5AC
		public static bool operator ==(GraphicsBufferHandle a, GraphicsBufferHandle b)
		{
			return a.Equals(b);
		}

		// Token: 0x06001349 RID: 4937 RVA: 0x000213C8 File Offset: 0x0001F5C8
		public static bool operator !=(GraphicsBufferHandle a, GraphicsBufferHandle b)
		{
			return !a.Equals(b);
		}

		// Token: 0x04000503 RID: 1283
		public readonly uint value;
	}
}
