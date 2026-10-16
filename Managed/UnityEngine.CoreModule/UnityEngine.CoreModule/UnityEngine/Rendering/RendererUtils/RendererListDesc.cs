using System;
using Unity.Collections;

namespace UnityEngine.Rendering.RendererUtils
{
	// Token: 0x02000573 RID: 1395
	public struct RendererListDesc
	{
		// Token: 0x170008F4 RID: 2292
		// (get) Token: 0x06003672 RID: 13938 RVA: 0x0006AC55 File Offset: 0x00068E55
		// (set) Token: 0x06003673 RID: 13939 RVA: 0x0006AC5D File Offset: 0x00068E5D
		public uint batchLayerMask { readonly get; set; }

		// Token: 0x170008F5 RID: 2293
		// (get) Token: 0x06003674 RID: 13940 RVA: 0x0006AC66 File Offset: 0x00068E66
		// (set) Token: 0x06003675 RID: 13941 RVA: 0x0006AC6E File Offset: 0x00068E6E
		internal CullingResults cullingResult { readonly get; private set; }

		// Token: 0x170008F6 RID: 2294
		// (get) Token: 0x06003676 RID: 13942 RVA: 0x0006AC77 File Offset: 0x00068E77
		// (set) Token: 0x06003677 RID: 13943 RVA: 0x0006AC7F File Offset: 0x00068E7F
		internal Camera camera { readonly get; set; }

		// Token: 0x170008F7 RID: 2295
		// (get) Token: 0x06003678 RID: 13944 RVA: 0x0006AC88 File Offset: 0x00068E88
		// (set) Token: 0x06003679 RID: 13945 RVA: 0x0006AC90 File Offset: 0x00068E90
		internal ShaderTagId passName { readonly get; private set; }

		// Token: 0x170008F8 RID: 2296
		// (get) Token: 0x0600367A RID: 13946 RVA: 0x0006AC99 File Offset: 0x00068E99
		// (set) Token: 0x0600367B RID: 13947 RVA: 0x0006ACA1 File Offset: 0x00068EA1
		internal ShaderTagId[] passNames { readonly get; private set; }

		// Token: 0x0600367C RID: 13948 RVA: 0x0006ACAC File Offset: 0x00068EAC
		public RendererListDesc(ShaderTagId passName, CullingResults cullingResult, Camera camera)
		{
			this = default(RendererListDesc);
			this.passName = passName;
			this.passNames = null;
			this.cullingResult = cullingResult;
			this.camera = camera;
			this.layerMask = -1;
			this.renderingLayerMask = uint.MaxValue;
			this.batchLayerMask = uint.MaxValue;
			this.overrideMaterialPassIndex = 0;
			this.overrideShaderPassIndex = 0;
		}

		// Token: 0x0600367D RID: 13949 RVA: 0x0006AD08 File Offset: 0x00068F08
		public RendererListDesc(ShaderTagId[] passNames, CullingResults cullingResult, Camera camera)
		{
			this = default(RendererListDesc);
			this.passNames = passNames;
			this.passName = ShaderTagId.none;
			this.cullingResult = cullingResult;
			this.camera = camera;
			this.layerMask = -1;
			this.renderingLayerMask = uint.MaxValue;
			this.batchLayerMask = uint.MaxValue;
			this.overrideMaterialPassIndex = 0;
		}

		// Token: 0x0600367E RID: 13950 RVA: 0x0006AD60 File Offset: 0x00068F60
		public bool IsValid()
		{
			bool flag = this.camera == null || (this.passName == ShaderTagId.none && (this.passNames == null || this.passNames.Length == 0));
			return !flag;
		}

		// Token: 0x0600367F RID: 13951 RVA: 0x0006ADB8 File Offset: 0x00068FB8
		public static RendererListParams ConvertToParameters(in RendererListDesc desc)
		{
			RendererListDesc rendererListDesc = desc;
			bool flag = !rendererListDesc.IsValid();
			RendererListParams rendererListParams;
			if (flag)
			{
				rendererListParams = RendererListParams.Invalid;
			}
			else
			{
				RendererListParams rendererListParams2 = default(RendererListParams);
				SortingSettings sortingSettings = new SortingSettings(desc.camera)
				{
					criteria = desc.sortingCriteria
				};
				DrawingSettings drawingSettings = new DrawingSettings(RendererListDesc.s_EmptyName, sortingSettings)
				{
					perObjectData = desc.rendererConfiguration
				};
				bool flag2 = desc.passName != ShaderTagId.none;
				if (flag2)
				{
					Debug.Assert(desc.passNames == null);
					drawingSettings.SetShaderPassName(0, desc.passName);
				}
				else
				{
					for (int i = 0; i < desc.passNames.Length; i++)
					{
						drawingSettings.SetShaderPassName(i, desc.passNames[i]);
					}
				}
				bool flag3 = desc.overrideShader != null;
				if (flag3)
				{
					drawingSettings.overrideShader = desc.overrideShader;
					drawingSettings.overrideShaderPassIndex = desc.overrideShaderPassIndex;
				}
				bool flag4 = desc.overrideMaterial != null;
				if (flag4)
				{
					drawingSettings.overrideMaterial = desc.overrideMaterial;
					drawingSettings.overrideMaterialPassIndex = desc.overrideMaterialPassIndex;
				}
				FilteringSettings filteringSettings = new FilteringSettings(new RenderQueueRange?(desc.renderQueueRange), desc.layerMask, desc.renderingLayerMask, 0)
				{
					excludeMotionVectorObjects = desc.excludeObjectMotionVectors,
					batchLayerMask = desc.batchLayerMask
				};
				rendererListParams2.cullingResults = desc.cullingResult;
				rendererListParams2.drawSettings = drawingSettings;
				rendererListParams2.filteringSettings = filteringSettings;
				rendererListParams2.tagName = ShaderTagId.none;
				rendererListParams2.isPassTagName = false;
				bool flag5 = desc.stateBlock != null && desc.stateBlock != null;
				if (flag5)
				{
					NativeArray<RenderStateBlock> nativeArray = new NativeArray<RenderStateBlock>(1, Allocator.Temp, NativeArrayOptions.ClearMemory);
					nativeArray[0] = desc.stateBlock.Value;
					rendererListParams2.stateBlocks = new NativeArray<RenderStateBlock>?(nativeArray);
					NativeArray<ShaderTagId> nativeArray2 = new NativeArray<ShaderTagId>(1, Allocator.Temp, NativeArrayOptions.ClearMemory);
					nativeArray2[0] = ShaderTagId.none;
					rendererListParams2.tagValues = new NativeArray<ShaderTagId>?(nativeArray2);
				}
				rendererListParams = rendererListParams2;
			}
			return rendererListParams;
		}

		// Token: 0x04001251 RID: 4689
		public SortingCriteria sortingCriteria;

		// Token: 0x04001252 RID: 4690
		public PerObjectData rendererConfiguration;

		// Token: 0x04001253 RID: 4691
		public RenderQueueRange renderQueueRange;

		// Token: 0x04001254 RID: 4692
		public RenderStateBlock? stateBlock;

		// Token: 0x04001255 RID: 4693
		public Shader overrideShader;

		// Token: 0x04001256 RID: 4694
		public Material overrideMaterial;

		// Token: 0x04001257 RID: 4695
		public bool excludeObjectMotionVectors;

		// Token: 0x04001258 RID: 4696
		public int layerMask;

		// Token: 0x04001259 RID: 4697
		public uint renderingLayerMask;

		// Token: 0x0400125B RID: 4699
		public int overrideMaterialPassIndex;

		// Token: 0x0400125C RID: 4700
		public int overrideShaderPassIndex;

		// Token: 0x04001261 RID: 4705
		private static readonly ShaderTagId s_EmptyName = new ShaderTagId("");
	}
}
