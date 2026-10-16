using System;

namespace UnityEngine
{
	// Token: 0x020001F0 RID: 496
	public struct CombineInstance
	{
		// Token: 0x170003DB RID: 987
		// (get) Token: 0x06001779 RID: 6009 RVA: 0x0002894C File Offset: 0x00026B4C
		// (set) Token: 0x0600177A RID: 6010 RVA: 0x00028969 File Offset: 0x00026B69
		public Mesh mesh
		{
			get
			{
				return Mesh.FromInstanceID(this.m_MeshInstanceID);
			}
			set
			{
				this.m_MeshInstanceID = ((value != null) ? value.GetInstanceID() : 0);
			}
		}

		// Token: 0x170003DC RID: 988
		// (get) Token: 0x0600177B RID: 6011 RVA: 0x00028984 File Offset: 0x00026B84
		// (set) Token: 0x0600177C RID: 6012 RVA: 0x0002899C File Offset: 0x00026B9C
		public int subMeshIndex
		{
			get
			{
				return this.m_SubMeshIndex;
			}
			set
			{
				this.m_SubMeshIndex = value;
			}
		}

		// Token: 0x170003DD RID: 989
		// (get) Token: 0x0600177D RID: 6013 RVA: 0x000289A8 File Offset: 0x00026BA8
		// (set) Token: 0x0600177E RID: 6014 RVA: 0x000289C0 File Offset: 0x00026BC0
		public Matrix4x4 transform
		{
			get
			{
				return this.m_Transform;
			}
			set
			{
				this.m_Transform = value;
			}
		}

		// Token: 0x170003DE RID: 990
		// (get) Token: 0x0600177F RID: 6015 RVA: 0x000289CC File Offset: 0x00026BCC
		// (set) Token: 0x06001780 RID: 6016 RVA: 0x000289E4 File Offset: 0x00026BE4
		public Vector4 lightmapScaleOffset
		{
			get
			{
				return this.m_LightmapScaleOffset;
			}
			set
			{
				this.m_LightmapScaleOffset = value;
			}
		}

		// Token: 0x170003DF RID: 991
		// (get) Token: 0x06001781 RID: 6017 RVA: 0x000289F0 File Offset: 0x00026BF0
		// (set) Token: 0x06001782 RID: 6018 RVA: 0x00028A08 File Offset: 0x00026C08
		public Vector4 realtimeLightmapScaleOffset
		{
			get
			{
				return this.m_RealtimeLightmapScaleOffset;
			}
			set
			{
				this.m_RealtimeLightmapScaleOffset = value;
			}
		}

		// Token: 0x040006D7 RID: 1751
		private int m_MeshInstanceID;

		// Token: 0x040006D8 RID: 1752
		private int m_SubMeshIndex;

		// Token: 0x040006D9 RID: 1753
		private Matrix4x4 m_Transform;

		// Token: 0x040006DA RID: 1754
		private Vector4 m_LightmapScaleOffset;

		// Token: 0x040006DB RID: 1755
		private Vector4 m_RealtimeLightmapScaleOffset;
	}
}
