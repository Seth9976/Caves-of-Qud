using System;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	// Token: 0x02000520 RID: 1312
	[UsedByNativeCode]
	internal struct GPUDrivenPackedRendererData
	{
		// Token: 0x170007EB RID: 2027
		// (get) Token: 0x0600326F RID: 12911 RVA: 0x00061B38 File Offset: 0x0005FD38
		public bool receiveShadows
		{
			get
			{
				return (this.data & 1U) > 0U;
			}
		}

		// Token: 0x170007EC RID: 2028
		// (get) Token: 0x06003270 RID: 12912 RVA: 0x00061B45 File Offset: 0x0005FD45
		public bool staticShadowCaster
		{
			get
			{
				return (this.data & 2U) > 0U;
			}
		}

		// Token: 0x170007ED RID: 2029
		// (get) Token: 0x06003271 RID: 12913 RVA: 0x00061B52 File Offset: 0x0005FD52
		public byte lodMask
		{
			get
			{
				return (byte)((this.data >> 2) & 255U);
			}
		}

		// Token: 0x170007EE RID: 2030
		// (get) Token: 0x06003272 RID: 12914 RVA: 0x00061B63 File Offset: 0x0005FD63
		public ShadowCastingMode shadowCastingMode
		{
			get
			{
				return (ShadowCastingMode)((this.data >> 10) & 3U);
			}
		}

		// Token: 0x170007EF RID: 2031
		// (get) Token: 0x06003273 RID: 12915 RVA: 0x00061B70 File Offset: 0x0005FD70
		public LightProbeUsage lightProbeUsage
		{
			get
			{
				return (LightProbeUsage)((this.data >> 12) & 7U);
			}
		}

		// Token: 0x170007F0 RID: 2032
		// (get) Token: 0x06003274 RID: 12916 RVA: 0x00061B7D File Offset: 0x0005FD7D
		public MotionVectorGenerationMode motionVecGenMode
		{
			get
			{
				return (MotionVectorGenerationMode)((this.data >> 15) & 3U);
			}
		}

		// Token: 0x170007F1 RID: 2033
		// (get) Token: 0x06003275 RID: 12917 RVA: 0x00061B8A File Offset: 0x0005FD8A
		public bool isPartOfStaticBatch
		{
			get
			{
				return (this.data & 131072U) > 0U;
			}
		}

		// Token: 0x170007F2 RID: 2034
		// (get) Token: 0x06003276 RID: 12918 RVA: 0x00061B9B File Offset: 0x0005FD9B
		public bool movedCurrentFrame
		{
			get
			{
				return (this.data & 262144U) > 0U;
			}
		}

		// Token: 0x170007F3 RID: 2035
		// (get) Token: 0x06003277 RID: 12919 RVA: 0x00061BAC File Offset: 0x0005FDAC
		public bool hasTree
		{
			get
			{
				return (this.data & 524288U) > 0U;
			}
		}

		// Token: 0x170007F4 RID: 2036
		// (get) Token: 0x06003278 RID: 12920 RVA: 0x00061BBD File Offset: 0x0005FDBD
		public bool smallMeshCulling
		{
			get
			{
				return (this.data & 1048576U) > 0U;
			}
		}

		// Token: 0x170007F5 RID: 2037
		// (get) Token: 0x06003279 RID: 12921 RVA: 0x00061BCE File Offset: 0x0005FDCE
		public bool supportsIndirect
		{
			get
			{
				return (this.data & 2097152U) > 0U;
			}
		}

		// Token: 0x0600327A RID: 12922 RVA: 0x00061BDF File Offset: 0x0005FDDF
		public GPUDrivenPackedRendererData()
		{
			this.data = 0U;
		}

		// Token: 0x0600327B RID: 12923 RVA: 0x00061BEC File Offset: 0x0005FDEC
		public GPUDrivenPackedRendererData(bool receiveShadows, bool staticShadowCaster, byte lodMask, ShadowCastingMode shadowCastingMode, LightProbeUsage lightProbeUsage, MotionVectorGenerationMode motionVecGenMode, bool isPartOfStaticBatch, bool movedCurrentFrame, bool hasTree, bool smallMeshCulling, bool supportsIndirect)
		{
			this.data = (receiveShadows ? 1U : 0U);
			this.data |= (staticShadowCaster ? 2U : 0U);
			this.data |= (uint)((uint)lodMask << 2);
			this.data |= (uint)((uint)shadowCastingMode << 10);
			this.data |= (uint)((uint)lightProbeUsage << 12);
			this.data |= (uint)((uint)motionVecGenMode << 15);
			this.data |= (isPartOfStaticBatch ? 131072U : 0U);
			this.data |= (movedCurrentFrame ? 262144U : 0U);
			this.data |= (hasTree ? 524288U : 0U);
			this.data |= (smallMeshCulling ? 1048576U : 0U);
			this.data |= (supportsIndirect ? 2097152U : 0U);
		}

		// Token: 0x04001060 RID: 4192
		private uint data;
	}
}
