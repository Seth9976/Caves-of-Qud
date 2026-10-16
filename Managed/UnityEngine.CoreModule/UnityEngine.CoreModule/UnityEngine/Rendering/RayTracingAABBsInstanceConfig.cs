using System;

namespace UnityEngine.Rendering
{
	// Token: 0x020004E3 RID: 1251
	public struct RayTracingAABBsInstanceConfig
	{
		// Token: 0x06002E5C RID: 11868 RVA: 0x00057DE4 File Offset: 0x00055FE4
		public RayTracingAABBsInstanceConfig()
		{
			this.aabbBuffer = null;
			this.aabbCount = 0;
			this.material = null;
			this.dynamicGeometry = false;
			this.opaqueMaterial = true;
			this.aabbOffset = 0U;
			this.materialProperties = null;
			this.layer = 0;
			this.mask = 255U;
			this.accelerationStructureBuildFlags = RayTracingAccelerationStructureBuildFlags.PreferFastTrace;
			this.accelerationStructureBuildFlagsOverride = false;
		}

		// Token: 0x06002E5D RID: 11869 RVA: 0x00057E50 File Offset: 0x00056050
		public RayTracingAABBsInstanceConfig(GraphicsBuffer aabbBuffer, int aabbCount, bool dynamicGeometry, Material material)
		{
			this.aabbBuffer = aabbBuffer;
			this.aabbCount = aabbCount;
			this.material = material;
			this.dynamicGeometry = dynamicGeometry;
			this.opaqueMaterial = true;
			this.aabbOffset = 0U;
			this.materialProperties = null;
			this.layer = 0;
			this.mask = 255U;
			this.accelerationStructureBuildFlags = RayTracingAccelerationStructureBuildFlags.PreferFastTrace;
			this.accelerationStructureBuildFlagsOverride = false;
		}

		// Token: 0x170007B3 RID: 1971
		// (get) Token: 0x06002E5E RID: 11870 RVA: 0x00057EBB File Offset: 0x000560BB
		// (set) Token: 0x06002E5F RID: 11871 RVA: 0x00057EC3 File Offset: 0x000560C3
		public GraphicsBuffer aabbBuffer { readonly get; set; }

		// Token: 0x170007B4 RID: 1972
		// (get) Token: 0x06002E60 RID: 11872 RVA: 0x00057ECC File Offset: 0x000560CC
		// (set) Token: 0x06002E61 RID: 11873 RVA: 0x00057ED4 File Offset: 0x000560D4
		public int aabbCount { readonly get; set; }

		// Token: 0x170007B5 RID: 1973
		// (get) Token: 0x06002E62 RID: 11874 RVA: 0x00057EDD File Offset: 0x000560DD
		// (set) Token: 0x06002E63 RID: 11875 RVA: 0x00057EE5 File Offset: 0x000560E5
		public uint aabbOffset { readonly get; set; }

		// Token: 0x170007B6 RID: 1974
		// (get) Token: 0x06002E64 RID: 11876 RVA: 0x00057EEE File Offset: 0x000560EE
		// (set) Token: 0x06002E65 RID: 11877 RVA: 0x00057EF6 File Offset: 0x000560F6
		public bool dynamicGeometry { readonly get; set; }

		// Token: 0x170007B7 RID: 1975
		// (get) Token: 0x06002E66 RID: 11878 RVA: 0x00057EFF File Offset: 0x000560FF
		// (set) Token: 0x06002E67 RID: 11879 RVA: 0x00057F07 File Offset: 0x00056107
		public bool opaqueMaterial { readonly get; set; }

		// Token: 0x170007B8 RID: 1976
		// (get) Token: 0x06002E68 RID: 11880 RVA: 0x00057F10 File Offset: 0x00056110
		// (set) Token: 0x06002E69 RID: 11881 RVA: 0x00057F18 File Offset: 0x00056118
		public Material material { readonly get; set; }

		// Token: 0x170007B9 RID: 1977
		// (get) Token: 0x06002E6A RID: 11882 RVA: 0x00057F21 File Offset: 0x00056121
		// (set) Token: 0x06002E6B RID: 11883 RVA: 0x00057F29 File Offset: 0x00056129
		public MaterialPropertyBlock materialProperties { readonly get; set; }

		// Token: 0x170007BA RID: 1978
		// (get) Token: 0x06002E6C RID: 11884 RVA: 0x00057F32 File Offset: 0x00056132
		// (set) Token: 0x06002E6D RID: 11885 RVA: 0x00057F3A File Offset: 0x0005613A
		public int layer { readonly get; set; }

		// Token: 0x170007BB RID: 1979
		// (get) Token: 0x06002E6E RID: 11886 RVA: 0x00057F43 File Offset: 0x00056143
		// (set) Token: 0x06002E6F RID: 11887 RVA: 0x00057F4B File Offset: 0x0005614B
		public uint mask { readonly get; set; }

		// Token: 0x170007BC RID: 1980
		// (get) Token: 0x06002E70 RID: 11888 RVA: 0x00057F54 File Offset: 0x00056154
		// (set) Token: 0x06002E71 RID: 11889 RVA: 0x00057F5C File Offset: 0x0005615C
		public RayTracingAccelerationStructureBuildFlags accelerationStructureBuildFlags { readonly get; set; }

		// Token: 0x170007BD RID: 1981
		// (get) Token: 0x06002E72 RID: 11890 RVA: 0x00057F65 File Offset: 0x00056165
		// (set) Token: 0x06002E73 RID: 11891 RVA: 0x00057F6D File Offset: 0x0005616D
		public bool accelerationStructureBuildFlagsOverride { readonly get; set; }
	}
}
