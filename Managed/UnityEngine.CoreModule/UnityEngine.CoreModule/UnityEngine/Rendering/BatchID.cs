using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	// Token: 0x020004F9 RID: 1273
	[NativeHeader("Runtime/Camera/BatchRendererGroup.h")]
	[NativeClass("BatchID")]
	[RequiredByNativeCode(Optional = true, GenerateProxy = true)]
	public struct BatchID : IEquatable<BatchID>
	{
		// Token: 0x060031C2 RID: 12738 RVA: 0x00060630 File Offset: 0x0005E830
		public override int GetHashCode()
		{
			return this.value.GetHashCode();
		}

		// Token: 0x060031C3 RID: 12739 RVA: 0x00060650 File Offset: 0x0005E850
		public override bool Equals(object obj)
		{
			bool flag = obj is BatchID;
			return flag && this.Equals((BatchID)obj);
		}

		// Token: 0x060031C4 RID: 12740 RVA: 0x00060680 File Offset: 0x0005E880
		public bool Equals(BatchID other)
		{
			return this.value == other.value;
		}

		// Token: 0x060031C5 RID: 12741 RVA: 0x000606A0 File Offset: 0x0005E8A0
		public int CompareTo(BatchID other)
		{
			return this.value.CompareTo(other.value);
		}

		// Token: 0x060031C6 RID: 12742 RVA: 0x000606C4 File Offset: 0x0005E8C4
		public static bool operator ==(BatchID a, BatchID b)
		{
			return a.Equals(b);
		}

		// Token: 0x060031C7 RID: 12743 RVA: 0x000606E0 File Offset: 0x0005E8E0
		public static bool operator !=(BatchID a, BatchID b)
		{
			return !a.Equals(b);
		}

		// Token: 0x04000F77 RID: 3959
		public static readonly BatchID Null = new BatchID
		{
			value = 0U
		};

		// Token: 0x04000F78 RID: 3960
		public uint value;
	}
}
