using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000167 RID: 359
	[UsedByNativeCode]
	[NativeType("Runtime/Graphics/DisplayInfo.h")]
	public struct DisplayInfo : IEquatable<DisplayInfo>
	{
		// Token: 0x06000B89 RID: 2953 RVA: 0x000156DC File Offset: 0x000138DC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(DisplayInfo other)
		{
			return this.handle == other.handle && this.width == other.width && this.height == other.height && this.refreshRate.Equals(other.refreshRate) && this.workArea.Equals(other.workArea) && this.name == other.name;
		}

		// Token: 0x0400049B RID: 1179
		[RequiredMember]
		internal ulong handle;

		// Token: 0x0400049C RID: 1180
		[RequiredMember]
		public int width;

		// Token: 0x0400049D RID: 1181
		[RequiredMember]
		public int height;

		// Token: 0x0400049E RID: 1182
		[RequiredMember]
		public RefreshRate refreshRate;

		// Token: 0x0400049F RID: 1183
		[RequiredMember]
		public RectInt workArea;

		// Token: 0x040004A0 RID: 1184
		[RequiredMember]
		public string name;
	}
}
