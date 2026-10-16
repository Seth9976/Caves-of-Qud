using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.XR
{
	// Token: 0x0200002B RID: 43
	[NativeHeader("Modules/XR/Subsystems/Meshing/XRMeshBindings.h")]
	[UsedByNativeCode]
	public struct MeshId : IEquatable<MeshId>
	{
		// Token: 0x0600018D RID: 397 RVA: 0x00005904 File Offset: 0x00003B04
		public override string ToString()
		{
			return string.Format("{0}-{1}", this.m_SubId1.ToString("X16"), this.m_SubId2.ToString("X16"));
		}

		// Token: 0x0600018E RID: 398 RVA: 0x00005940 File Offset: 0x00003B40
		public override int GetHashCode()
		{
			return this.m_SubId1.GetHashCode() ^ this.m_SubId2.GetHashCode();
		}

		// Token: 0x0600018F RID: 399 RVA: 0x0000596C File Offset: 0x00003B6C
		public override bool Equals(object obj)
		{
			return obj is MeshId && this.Equals((MeshId)obj);
		}

		// Token: 0x06000190 RID: 400 RVA: 0x00005998 File Offset: 0x00003B98
		public bool Equals(MeshId other)
		{
			return this.m_SubId1 == other.m_SubId1 && this.m_SubId2 == other.m_SubId2;
		}

		// Token: 0x06000191 RID: 401 RVA: 0x000059CC File Offset: 0x00003BCC
		public static bool operator ==(MeshId id1, MeshId id2)
		{
			return id1.m_SubId1 == id2.m_SubId1 && id1.m_SubId2 == id2.m_SubId2;
		}

		// Token: 0x06000192 RID: 402 RVA: 0x00005A00 File Offset: 0x00003C00
		public static bool operator !=(MeshId id1, MeshId id2)
		{
			return id1.m_SubId1 != id2.m_SubId1 || id1.m_SubId2 != id2.m_SubId2;
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000193 RID: 403 RVA: 0x00005A34 File Offset: 0x00003C34
		public static MeshId InvalidId
		{
			get
			{
				return MeshId.s_InvalidId;
			}
		}

		// Token: 0x040000F3 RID: 243
		private static MeshId s_InvalidId = default(MeshId);

		// Token: 0x040000F4 RID: 244
		private ulong m_SubId1;

		// Token: 0x040000F5 RID: 245
		private ulong m_SubId2;
	}
}
