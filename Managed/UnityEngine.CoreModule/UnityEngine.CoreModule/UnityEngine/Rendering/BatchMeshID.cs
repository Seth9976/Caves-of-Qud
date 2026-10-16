using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	// Token: 0x020004FB RID: 1275
	[NativeHeader("Runtime/Camera/BatchRendererGroup.h")]
	[RequiredByNativeCode(Optional = true, GenerateProxy = true)]
	[NativeClass("BatchMeshID")]
	public struct BatchMeshID : IEquatable<BatchMeshID>
	{
		// Token: 0x060031D0 RID: 12752 RVA: 0x00060818 File Offset: 0x0005EA18
		public override int GetHashCode()
		{
			return this.value.GetHashCode();
		}

		// Token: 0x060031D1 RID: 12753 RVA: 0x00060838 File Offset: 0x0005EA38
		public override bool Equals(object obj)
		{
			bool flag = obj is BatchMeshID;
			return flag && this.Equals((BatchMeshID)obj);
		}

		// Token: 0x060031D2 RID: 12754 RVA: 0x00060868 File Offset: 0x0005EA68
		public bool Equals(BatchMeshID other)
		{
			return this.value == other.value;
		}

		// Token: 0x060031D3 RID: 12755 RVA: 0x00060888 File Offset: 0x0005EA88
		public int CompareTo(BatchMeshID other)
		{
			return this.value.CompareTo(other.value);
		}

		// Token: 0x060031D4 RID: 12756 RVA: 0x000608AC File Offset: 0x0005EAAC
		public static bool operator ==(BatchMeshID a, BatchMeshID b)
		{
			return a.Equals(b);
		}

		// Token: 0x060031D5 RID: 12757 RVA: 0x000608C8 File Offset: 0x0005EAC8
		public static bool operator !=(BatchMeshID a, BatchMeshID b)
		{
			return !a.Equals(b);
		}

		// Token: 0x04000F7B RID: 3963
		public static readonly BatchMeshID Null = new BatchMeshID
		{
			value = 0U
		};

		// Token: 0x04000F7C RID: 3964
		public uint value;
	}
}
