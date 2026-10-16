using System;

namespace UnityEngine.Rendering
{
	// Token: 0x020004E4 RID: 1252
	public struct RayTracingGeometryInstanceConfig
	{
		// Token: 0x06002E74 RID: 11892 RVA: 0x00057F78 File Offset: 0x00056178
		public RayTracingGeometryInstanceConfig()
		{
			this.material = null;
			this.vertexBuffer = null;
			this.indexBuffer = null;
			this.vertexAttributes = null;
			this.vertexStart = 0U;
			this.indexStart = 0U;
			this.vertexCount = -1;
			this.indexCount = -1;
			this.subMeshFlags = RayTracingSubMeshFlags.Enabled | RayTracingSubMeshFlags.ClosestHitOnly;
			this.dynamicGeometry = false;
			this.materialProperties = null;
			this.enableTriangleCulling = true;
			this.frontTriangleCounterClockwise = false;
			this.layer = 0;
			this.renderingLayerMask = RenderingLayerMask.defaultRenderingLayerMask;
			this.mask = 255U;
			this.motionVectorMode = MotionVectorGenerationMode.Camera;
			this.lightProbeUsage = LightProbeUsage.Off;
			this.lightProbeProxyVolume = null;
			this.accelerationStructureBuildFlagsOverride = false;
			this.accelerationStructureBuildFlags = RayTracingAccelerationStructureBuildFlags.PreferFastTrace;
		}

		// Token: 0x06002E75 RID: 11893 RVA: 0x0005803C File Offset: 0x0005623C
		public RayTracingGeometryInstanceConfig(GraphicsBuffer vertexBuffer, VertexAttributeDescriptor[] vertexAttributes, GraphicsBuffer indexBuffer, Material material)
		{
			this.material = material;
			this.vertexBuffer = vertexBuffer;
			this.indexBuffer = indexBuffer;
			this.vertexAttributes = vertexAttributes;
			this.vertexStart = 0U;
			this.indexStart = 0U;
			this.vertexCount = -1;
			this.indexCount = -1;
			this.subMeshFlags = RayTracingSubMeshFlags.Enabled | RayTracingSubMeshFlags.ClosestHitOnly;
			this.dynamicGeometry = false;
			this.materialProperties = null;
			this.enableTriangleCulling = true;
			this.frontTriangleCounterClockwise = false;
			this.layer = 0;
			this.renderingLayerMask = RenderingLayerMask.defaultRenderingLayerMask;
			this.mask = 255U;
			this.motionVectorMode = MotionVectorGenerationMode.Camera;
			this.lightProbeUsage = LightProbeUsage.Off;
			this.lightProbeProxyVolume = null;
			this.accelerationStructureBuildFlagsOverride = false;
			this.accelerationStructureBuildFlags = RayTracingAccelerationStructureBuildFlags.PreferFastTrace;
		}

		// Token: 0x170007BE RID: 1982
		// (get) Token: 0x06002E76 RID: 11894 RVA: 0x00058100 File Offset: 0x00056300
		// (set) Token: 0x06002E77 RID: 11895 RVA: 0x00058108 File Offset: 0x00056308
		public GraphicsBuffer vertexBuffer { readonly get; set; }

		// Token: 0x170007BF RID: 1983
		// (get) Token: 0x06002E78 RID: 11896 RVA: 0x00058111 File Offset: 0x00056311
		// (set) Token: 0x06002E79 RID: 11897 RVA: 0x00058119 File Offset: 0x00056319
		public VertexAttributeDescriptor[] vertexAttributes { readonly get; set; }

		// Token: 0x170007C0 RID: 1984
		// (get) Token: 0x06002E7A RID: 11898 RVA: 0x00058122 File Offset: 0x00056322
		// (set) Token: 0x06002E7B RID: 11899 RVA: 0x0005812A File Offset: 0x0005632A
		public uint vertexStart { readonly get; set; }

		// Token: 0x170007C1 RID: 1985
		// (get) Token: 0x06002E7C RID: 11900 RVA: 0x00058133 File Offset: 0x00056333
		// (set) Token: 0x06002E7D RID: 11901 RVA: 0x0005813B File Offset: 0x0005633B
		public int vertexCount { readonly get; set; }

		// Token: 0x170007C2 RID: 1986
		// (get) Token: 0x06002E7E RID: 11902 RVA: 0x00058144 File Offset: 0x00056344
		// (set) Token: 0x06002E7F RID: 11903 RVA: 0x0005814C File Offset: 0x0005634C
		public GraphicsBuffer indexBuffer { readonly get; set; }

		// Token: 0x170007C3 RID: 1987
		// (get) Token: 0x06002E80 RID: 11904 RVA: 0x00058155 File Offset: 0x00056355
		// (set) Token: 0x06002E81 RID: 11905 RVA: 0x0005815D File Offset: 0x0005635D
		public uint indexStart { readonly get; set; }

		// Token: 0x170007C4 RID: 1988
		// (get) Token: 0x06002E82 RID: 11906 RVA: 0x00058166 File Offset: 0x00056366
		// (set) Token: 0x06002E83 RID: 11907 RVA: 0x0005816E File Offset: 0x0005636E
		public int indexCount { readonly get; set; }

		// Token: 0x170007C5 RID: 1989
		// (get) Token: 0x06002E84 RID: 11908 RVA: 0x00058177 File Offset: 0x00056377
		// (set) Token: 0x06002E85 RID: 11909 RVA: 0x0005817F File Offset: 0x0005637F
		public RayTracingSubMeshFlags subMeshFlags { readonly get; set; }

		// Token: 0x170007C6 RID: 1990
		// (get) Token: 0x06002E86 RID: 11910 RVA: 0x00058188 File Offset: 0x00056388
		// (set) Token: 0x06002E87 RID: 11911 RVA: 0x00058190 File Offset: 0x00056390
		public bool dynamicGeometry { readonly get; set; }

		// Token: 0x170007C7 RID: 1991
		// (get) Token: 0x06002E88 RID: 11912 RVA: 0x00058199 File Offset: 0x00056399
		// (set) Token: 0x06002E89 RID: 11913 RVA: 0x000581A1 File Offset: 0x000563A1
		public Material material { readonly get; set; }

		// Token: 0x170007C8 RID: 1992
		// (get) Token: 0x06002E8A RID: 11914 RVA: 0x000581AA File Offset: 0x000563AA
		// (set) Token: 0x06002E8B RID: 11915 RVA: 0x000581B2 File Offset: 0x000563B2
		public MaterialPropertyBlock materialProperties { readonly get; set; }

		// Token: 0x170007C9 RID: 1993
		// (get) Token: 0x06002E8C RID: 11916 RVA: 0x000581BB File Offset: 0x000563BB
		// (set) Token: 0x06002E8D RID: 11917 RVA: 0x000581C3 File Offset: 0x000563C3
		public bool enableTriangleCulling { readonly get; set; }

		// Token: 0x170007CA RID: 1994
		// (get) Token: 0x06002E8E RID: 11918 RVA: 0x000581CC File Offset: 0x000563CC
		// (set) Token: 0x06002E8F RID: 11919 RVA: 0x000581D4 File Offset: 0x000563D4
		public bool frontTriangleCounterClockwise { readonly get; set; }

		// Token: 0x170007CB RID: 1995
		// (get) Token: 0x06002E90 RID: 11920 RVA: 0x000581DD File Offset: 0x000563DD
		// (set) Token: 0x06002E91 RID: 11921 RVA: 0x000581E5 File Offset: 0x000563E5
		public int layer { readonly get; set; }

		// Token: 0x170007CC RID: 1996
		// (get) Token: 0x06002E92 RID: 11922 RVA: 0x000581EE File Offset: 0x000563EE
		// (set) Token: 0x06002E93 RID: 11923 RVA: 0x000581F6 File Offset: 0x000563F6
		public uint renderingLayerMask { readonly get; set; }

		// Token: 0x170007CD RID: 1997
		// (get) Token: 0x06002E94 RID: 11924 RVA: 0x000581FF File Offset: 0x000563FF
		// (set) Token: 0x06002E95 RID: 11925 RVA: 0x00058207 File Offset: 0x00056407
		public uint mask { readonly get; set; }

		// Token: 0x170007CE RID: 1998
		// (get) Token: 0x06002E96 RID: 11926 RVA: 0x00058210 File Offset: 0x00056410
		// (set) Token: 0x06002E97 RID: 11927 RVA: 0x00058218 File Offset: 0x00056418
		public MotionVectorGenerationMode motionVectorMode { readonly get; set; }

		// Token: 0x170007CF RID: 1999
		// (get) Token: 0x06002E98 RID: 11928 RVA: 0x00058221 File Offset: 0x00056421
		// (set) Token: 0x06002E99 RID: 11929 RVA: 0x00058229 File Offset: 0x00056429
		public LightProbeUsage lightProbeUsage { readonly get; set; }

		// Token: 0x170007D0 RID: 2000
		// (get) Token: 0x06002E9A RID: 11930 RVA: 0x00058232 File Offset: 0x00056432
		// (set) Token: 0x06002E9B RID: 11931 RVA: 0x0005823A File Offset: 0x0005643A
		public LightProbeProxyVolume lightProbeProxyVolume { readonly get; set; }

		// Token: 0x170007D1 RID: 2001
		// (get) Token: 0x06002E9C RID: 11932 RVA: 0x00058243 File Offset: 0x00056443
		// (set) Token: 0x06002E9D RID: 11933 RVA: 0x0005824B File Offset: 0x0005644B
		public RayTracingAccelerationStructureBuildFlags accelerationStructureBuildFlags { readonly get; set; }

		// Token: 0x170007D2 RID: 2002
		// (get) Token: 0x06002E9E RID: 11934 RVA: 0x00058254 File Offset: 0x00056454
		// (set) Token: 0x06002E9F RID: 11935 RVA: 0x0005825C File Offset: 0x0005645C
		public bool accelerationStructureBuildFlagsOverride { readonly get; set; }
	}
}
