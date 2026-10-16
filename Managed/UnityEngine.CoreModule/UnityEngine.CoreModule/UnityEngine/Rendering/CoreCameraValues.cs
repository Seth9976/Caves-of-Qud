using System;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	// Token: 0x0200052B RID: 1323
	[UsedByNativeCode]
	internal struct CoreCameraValues : IEquatable<CoreCameraValues>
	{
		// Token: 0x060032C2 RID: 12994 RVA: 0x00062884 File Offset: 0x00060A84
		public bool Equals(CoreCameraValues other)
		{
			return this.filterMode == other.filterMode && this.cullingMask == other.cullingMask && this.instanceID == other.instanceID;
		}

		// Token: 0x060032C3 RID: 12995 RVA: 0x000628C4 File Offset: 0x00060AC4
		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is CoreCameraValues && this.Equals((CoreCameraValues)obj);
		}

		// Token: 0x060032C4 RID: 12996 RVA: 0x000628FC File Offset: 0x00060AFC
		public override int GetHashCode()
		{
			int num = this.filterMode;
			num = (num * 397) ^ (int)this.cullingMask;
			return (num * 397) ^ this.instanceID;
		}

		// Token: 0x060032C5 RID: 12997 RVA: 0x00062938 File Offset: 0x00060B38
		public static bool operator ==(CoreCameraValues left, CoreCameraValues right)
		{
			return left.Equals(right);
		}

		// Token: 0x060032C6 RID: 12998 RVA: 0x00062954 File Offset: 0x00060B54
		public static bool operator !=(CoreCameraValues left, CoreCameraValues right)
		{
			return !left.Equals(right);
		}

		// Token: 0x040010BB RID: 4283
		private int filterMode;

		// Token: 0x040010BC RID: 4284
		private uint cullingMask;

		// Token: 0x040010BD RID: 4285
		private int instanceID;
	}
}
