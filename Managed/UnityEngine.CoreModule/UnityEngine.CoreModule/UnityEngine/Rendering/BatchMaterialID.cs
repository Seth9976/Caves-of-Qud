using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	// Token: 0x020004FA RID: 1274
	[RequiredByNativeCode(Optional = true, GenerateProxy = true)]
	[NativeClass("BatchMaterialID")]
	[NativeHeader("Runtime/Camera/BatchRendererGroup.h")]
	public struct BatchMaterialID : IEquatable<BatchMaterialID>
	{
		// Token: 0x060031C9 RID: 12745 RVA: 0x00060724 File Offset: 0x0005E924
		public override int GetHashCode()
		{
			return this.value.GetHashCode();
		}

		// Token: 0x060031CA RID: 12746 RVA: 0x00060744 File Offset: 0x0005E944
		public override bool Equals(object obj)
		{
			bool flag = obj is BatchMaterialID;
			return flag && this.Equals((BatchMaterialID)obj);
		}

		// Token: 0x060031CB RID: 12747 RVA: 0x00060774 File Offset: 0x0005E974
		public bool Equals(BatchMaterialID other)
		{
			return this.value == other.value;
		}

		// Token: 0x060031CC RID: 12748 RVA: 0x00060794 File Offset: 0x0005E994
		public int CompareTo(BatchMaterialID other)
		{
			return this.value.CompareTo(other.value);
		}

		// Token: 0x060031CD RID: 12749 RVA: 0x000607B8 File Offset: 0x0005E9B8
		public static bool operator ==(BatchMaterialID a, BatchMaterialID b)
		{
			return a.Equals(b);
		}

		// Token: 0x060031CE RID: 12750 RVA: 0x000607D4 File Offset: 0x0005E9D4
		public static bool operator !=(BatchMaterialID a, BatchMaterialID b)
		{
			return !a.Equals(b);
		}

		// Token: 0x04000F79 RID: 3961
		public static readonly BatchMaterialID Null = new BatchMaterialID
		{
			value = 0U
		};

		// Token: 0x04000F7A RID: 3962
		public uint value;
	}
}
