using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020001EC RID: 492
	[UsedByNativeCode]
	[Serializable]
	internal struct BlendShape
	{
		// Token: 0x170003CB RID: 971
		// (get) Token: 0x0600174F RID: 5967 RVA: 0x000283E0 File Offset: 0x000265E0
		// (set) Token: 0x06001750 RID: 5968 RVA: 0x000283F8 File Offset: 0x000265F8
		public uint firstVertex
		{
			get
			{
				return this.m_FirstVertex;
			}
			set
			{
				this.m_FirstVertex = value;
			}
		}

		// Token: 0x170003CC RID: 972
		// (get) Token: 0x06001751 RID: 5969 RVA: 0x00028404 File Offset: 0x00026604
		// (set) Token: 0x06001752 RID: 5970 RVA: 0x0002841C File Offset: 0x0002661C
		public uint vertexCount
		{
			get
			{
				return this.m_VertexCount;
			}
			set
			{
				this.m_VertexCount = value;
			}
		}

		// Token: 0x170003CD RID: 973
		// (get) Token: 0x06001753 RID: 5971 RVA: 0x00028428 File Offset: 0x00026628
		// (set) Token: 0x06001754 RID: 5972 RVA: 0x00028440 File Offset: 0x00026640
		public bool hasNormals
		{
			get
			{
				return this.m_HasNormals;
			}
			set
			{
				this.m_HasNormals = value;
			}
		}

		// Token: 0x170003CE RID: 974
		// (get) Token: 0x06001755 RID: 5973 RVA: 0x0002844C File Offset: 0x0002664C
		// (set) Token: 0x06001756 RID: 5974 RVA: 0x00028464 File Offset: 0x00026664
		public bool hasTangents
		{
			get
			{
				return this.m_HasTangents;
			}
			set
			{
				this.m_HasTangents = value;
			}
		}

		// Token: 0x040006C7 RID: 1735
		[SerializeField]
		private uint m_FirstVertex;

		// Token: 0x040006C8 RID: 1736
		[SerializeField]
		private uint m_VertexCount;

		// Token: 0x040006C9 RID: 1737
		[SerializeField]
		private bool m_HasNormals;

		// Token: 0x040006CA RID: 1738
		[SerializeField]
		private bool m_HasTangents;
	}
}
