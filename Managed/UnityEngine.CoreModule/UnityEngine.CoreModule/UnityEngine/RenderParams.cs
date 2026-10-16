using System;
using UnityEngine.Rendering;

namespace UnityEngine
{
	// Token: 0x02000179 RID: 377
	public struct RenderParams
	{
		// Token: 0x06000DB4 RID: 3508 RVA: 0x0001982C File Offset: 0x00017A2C
		public RenderParams(Material mat)
		{
			this.layer = 0;
			this.renderingLayerMask = RenderingLayerMask.defaultRenderingLayerMask;
			this.rendererPriority = 0;
			this.worldBounds = new Bounds(Vector3.zero, Vector3.zero);
			this.camera = null;
			this.motionVectorMode = MotionVectorGenerationMode.Camera;
			this.reflectionProbeUsage = ReflectionProbeUsage.Off;
			this.material = mat;
			this.matProps = null;
			this.shadowCastingMode = ShadowCastingMode.Off;
			this.receiveShadows = false;
			this.lightProbeUsage = LightProbeUsage.Off;
			this.lightProbeProxyVolume = null;
			this.overrideSceneCullingMask = false;
			this.sceneCullingMask = 0UL;
			this.instanceID = 0;
		}

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x06000DB5 RID: 3509 RVA: 0x000198D2 File Offset: 0x00017AD2
		// (set) Token: 0x06000DB6 RID: 3510 RVA: 0x000198DA File Offset: 0x00017ADA
		public int layer { readonly get; set; }

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x06000DB7 RID: 3511 RVA: 0x000198E3 File Offset: 0x00017AE3
		// (set) Token: 0x06000DB8 RID: 3512 RVA: 0x000198EB File Offset: 0x00017AEB
		public uint renderingLayerMask { readonly get; set; }

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x06000DB9 RID: 3513 RVA: 0x000198F4 File Offset: 0x00017AF4
		// (set) Token: 0x06000DBA RID: 3514 RVA: 0x000198FC File Offset: 0x00017AFC
		public int rendererPriority { readonly get; set; }

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x06000DBB RID: 3515 RVA: 0x00019905 File Offset: 0x00017B05
		// (set) Token: 0x06000DBC RID: 3516 RVA: 0x0001990D File Offset: 0x00017B0D
		public int instanceID { readonly get; set; }

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x06000DBD RID: 3517 RVA: 0x00019916 File Offset: 0x00017B16
		// (set) Token: 0x06000DBE RID: 3518 RVA: 0x0001991E File Offset: 0x00017B1E
		public Bounds worldBounds { readonly get; set; }

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x06000DBF RID: 3519 RVA: 0x00019927 File Offset: 0x00017B27
		// (set) Token: 0x06000DC0 RID: 3520 RVA: 0x0001992F File Offset: 0x00017B2F
		public Camera camera { readonly get; set; }

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x06000DC1 RID: 3521 RVA: 0x00019938 File Offset: 0x00017B38
		// (set) Token: 0x06000DC2 RID: 3522 RVA: 0x00019940 File Offset: 0x00017B40
		public MotionVectorGenerationMode motionVectorMode { readonly get; set; }

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x06000DC3 RID: 3523 RVA: 0x00019949 File Offset: 0x00017B49
		// (set) Token: 0x06000DC4 RID: 3524 RVA: 0x00019951 File Offset: 0x00017B51
		public ReflectionProbeUsage reflectionProbeUsage { readonly get; set; }

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x06000DC5 RID: 3525 RVA: 0x0001995A File Offset: 0x00017B5A
		// (set) Token: 0x06000DC6 RID: 3526 RVA: 0x00019962 File Offset: 0x00017B62
		public Material material { readonly get; set; }

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x06000DC7 RID: 3527 RVA: 0x0001996B File Offset: 0x00017B6B
		// (set) Token: 0x06000DC8 RID: 3528 RVA: 0x00019973 File Offset: 0x00017B73
		public MaterialPropertyBlock matProps { readonly get; set; }

		// Token: 0x17000269 RID: 617
		// (get) Token: 0x06000DC9 RID: 3529 RVA: 0x0001997C File Offset: 0x00017B7C
		// (set) Token: 0x06000DCA RID: 3530 RVA: 0x00019984 File Offset: 0x00017B84
		public ShadowCastingMode shadowCastingMode { readonly get; set; }

		// Token: 0x1700026A RID: 618
		// (get) Token: 0x06000DCB RID: 3531 RVA: 0x0001998D File Offset: 0x00017B8D
		// (set) Token: 0x06000DCC RID: 3532 RVA: 0x00019995 File Offset: 0x00017B95
		public bool receiveShadows { readonly get; set; }

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x06000DCD RID: 3533 RVA: 0x0001999E File Offset: 0x00017B9E
		// (set) Token: 0x06000DCE RID: 3534 RVA: 0x000199A6 File Offset: 0x00017BA6
		public LightProbeUsage lightProbeUsage { readonly get; set; }

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x06000DCF RID: 3535 RVA: 0x000199AF File Offset: 0x00017BAF
		// (set) Token: 0x06000DD0 RID: 3536 RVA: 0x000199B7 File Offset: 0x00017BB7
		public LightProbeProxyVolume lightProbeProxyVolume { readonly get; set; }

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x06000DD1 RID: 3537 RVA: 0x000199C0 File Offset: 0x00017BC0
		// (set) Token: 0x06000DD2 RID: 3538 RVA: 0x000199C8 File Offset: 0x00017BC8
		public bool overrideSceneCullingMask { readonly get; set; }

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x06000DD3 RID: 3539 RVA: 0x000199D1 File Offset: 0x00017BD1
		// (set) Token: 0x06000DD4 RID: 3540 RVA: 0x000199D9 File Offset: 0x00017BD9
		public ulong sceneCullingMask { readonly get; set; }
	}
}
