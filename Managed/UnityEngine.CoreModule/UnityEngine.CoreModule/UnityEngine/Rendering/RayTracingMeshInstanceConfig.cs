using System;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.Rendering
{
	// Token: 0x020004E2 RID: 1250
	[MovedFrom("UnityEngine.Experimental.Rendering")]
	public struct RayTracingMeshInstanceConfig
	{
		// Token: 0x06002E56 RID: 11862 RVA: 0x00057CA4 File Offset: 0x00055EA4
		public RayTracingMeshInstanceConfig()
		{
			this.mesh = null;
			this.subMeshIndex = 0U;
			this.material = null;
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
			this.accelerationStructureBuildFlags = RayTracingAccelerationStructureBuildFlags.PreferFastTrace;
			this.accelerationStructureBuildFlagsOverride = false;
		}

		// Token: 0x06002E57 RID: 11863 RVA: 0x00057D34 File Offset: 0x00055F34
		public RayTracingMeshInstanceConfig(Mesh mesh, uint subMeshIndex, Material material)
		{
			this.mesh = mesh;
			this.subMeshIndex = subMeshIndex;
			this.material = material;
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
			this.accelerationStructureBuildFlags = RayTracingAccelerationStructureBuildFlags.PreferFastTrace;
			this.accelerationStructureBuildFlagsOverride = false;
		}

		// Token: 0x170007B1 RID: 1969
		// (get) Token: 0x06002E58 RID: 11864 RVA: 0x00057DC1 File Offset: 0x00055FC1
		// (set) Token: 0x06002E59 RID: 11865 RVA: 0x00057DC9 File Offset: 0x00055FC9
		public RayTracingAccelerationStructureBuildFlags accelerationStructureBuildFlags { readonly get; set; }

		// Token: 0x170007B2 RID: 1970
		// (get) Token: 0x06002E5A RID: 11866 RVA: 0x00057DD2 File Offset: 0x00055FD2
		// (set) Token: 0x06002E5B RID: 11867 RVA: 0x00057DDA File Offset: 0x00055FDA
		public bool accelerationStructureBuildFlagsOverride { readonly get; set; }

		// Token: 0x04000F04 RID: 3844
		public Mesh mesh;

		// Token: 0x04000F05 RID: 3845
		public uint subMeshIndex;

		// Token: 0x04000F06 RID: 3846
		public RayTracingSubMeshFlags subMeshFlags;

		// Token: 0x04000F07 RID: 3847
		public bool dynamicGeometry;

		// Token: 0x04000F08 RID: 3848
		public Material material;

		// Token: 0x04000F09 RID: 3849
		public MaterialPropertyBlock materialProperties;

		// Token: 0x04000F0A RID: 3850
		public bool enableTriangleCulling;

		// Token: 0x04000F0B RID: 3851
		public bool frontTriangleCounterClockwise;

		// Token: 0x04000F0C RID: 3852
		public int layer;

		// Token: 0x04000F0D RID: 3853
		public uint renderingLayerMask;

		// Token: 0x04000F0E RID: 3854
		public uint mask;

		// Token: 0x04000F0F RID: 3855
		public MotionVectorGenerationMode motionVectorMode;

		// Token: 0x04000F10 RID: 3856
		public LightProbeUsage lightProbeUsage;

		// Token: 0x04000F11 RID: 3857
		public LightProbeProxyVolume lightProbeProxyVolume;
	}
}
