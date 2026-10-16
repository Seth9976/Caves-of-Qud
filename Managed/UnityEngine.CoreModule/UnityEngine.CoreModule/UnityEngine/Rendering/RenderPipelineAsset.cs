using System;

namespace UnityEngine.Rendering
{
	// Token: 0x0200054A RID: 1354
	public abstract class RenderPipelineAsset : ScriptableObject
	{
		// Token: 0x060033BA RID: 13242 RVA: 0x00065538 File Offset: 0x00063738
		internal RenderPipeline InternalCreatePipeline()
		{
			RenderPipeline renderPipeline = null;
			try
			{
				renderPipeline = this.CreatePipeline();
			}
			catch (InvalidImportException)
			{
			}
			catch (Exception ex)
			{
				Debug.LogException(ex);
			}
			return renderPipeline;
		}

		// Token: 0x17000855 RID: 2133
		// (get) Token: 0x060033BB RID: 13243 RVA: 0x0003CDCD File Offset: 0x0003AFCD
		public virtual Material defaultMaterial
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000856 RID: 2134
		// (get) Token: 0x060033BC RID: 13244 RVA: 0x0003CDCD File Offset: 0x0003AFCD
		public virtual Shader autodeskInteractiveShader
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000857 RID: 2135
		// (get) Token: 0x060033BD RID: 13245 RVA: 0x0003CDCD File Offset: 0x0003AFCD
		public virtual Shader autodeskInteractiveTransparentShader
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000858 RID: 2136
		// (get) Token: 0x060033BE RID: 13246 RVA: 0x0003CDCD File Offset: 0x0003AFCD
		public virtual Shader autodeskInteractiveMaskedShader
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000859 RID: 2137
		// (get) Token: 0x060033BF RID: 13247 RVA: 0x0003CDCD File Offset: 0x0003AFCD
		public virtual Shader terrainDetailLitShader
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700085A RID: 2138
		// (get) Token: 0x060033C0 RID: 13248 RVA: 0x0003CDCD File Offset: 0x0003AFCD
		public virtual Shader terrainDetailGrassShader
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700085B RID: 2139
		// (get) Token: 0x060033C1 RID: 13249 RVA: 0x0003CDCD File Offset: 0x0003AFCD
		public virtual Shader terrainDetailGrassBillboardShader
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700085C RID: 2140
		// (get) Token: 0x060033C2 RID: 13250 RVA: 0x0003CDCD File Offset: 0x0003AFCD
		public virtual Material defaultParticleMaterial
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700085D RID: 2141
		// (get) Token: 0x060033C3 RID: 13251 RVA: 0x0003CDCD File Offset: 0x0003AFCD
		public virtual Material defaultLineMaterial
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700085E RID: 2142
		// (get) Token: 0x060033C4 RID: 13252 RVA: 0x0003CDCD File Offset: 0x0003AFCD
		public virtual Material defaultTerrainMaterial
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700085F RID: 2143
		// (get) Token: 0x060033C5 RID: 13253 RVA: 0x0003CDCD File Offset: 0x0003AFCD
		public virtual Material defaultUIMaterial
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000860 RID: 2144
		// (get) Token: 0x060033C6 RID: 13254 RVA: 0x0003CDCD File Offset: 0x0003AFCD
		public virtual Material defaultUIOverdrawMaterial
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000861 RID: 2145
		// (get) Token: 0x060033C7 RID: 13255 RVA: 0x0003CDCD File Offset: 0x0003AFCD
		public virtual Material defaultUIETC1SupportedMaterial
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000862 RID: 2146
		// (get) Token: 0x060033C8 RID: 13256 RVA: 0x0003CDCD File Offset: 0x0003AFCD
		public virtual Material default2DMaterial
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000863 RID: 2147
		// (get) Token: 0x060033C9 RID: 13257 RVA: 0x0003CDCD File Offset: 0x0003AFCD
		public virtual Material default2DMaskMaterial
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000864 RID: 2148
		// (get) Token: 0x060033CA RID: 13258 RVA: 0x0003CDCD File Offset: 0x0003AFCD
		public virtual Shader defaultShader
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000865 RID: 2149
		// (get) Token: 0x060033CB RID: 13259 RVA: 0x0003CDCD File Offset: 0x0003AFCD
		public virtual Shader defaultSpeedTree7Shader
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000866 RID: 2150
		// (get) Token: 0x060033CC RID: 13260 RVA: 0x0003CDCD File Offset: 0x0003AFCD
		public virtual Shader defaultSpeedTree8Shader
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000867 RID: 2151
		// (get) Token: 0x060033CD RID: 13261 RVA: 0x0003CDCD File Offset: 0x0003AFCD
		public virtual Shader defaultSpeedTree9Shader
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000868 RID: 2152
		// (get) Token: 0x060033CE RID: 13262 RVA: 0x00065588 File Offset: 0x00063788
		public virtual string renderPipelineShaderTag
		{
			get
			{
				Debug.LogWarning("The property renderPipelineShaderTag has not been overridden. At build time, any shader variants that use any RenderPipeline tag will be stripped.");
				return string.Empty;
			}
		}

		// Token: 0x060033CF RID: 13263
		protected abstract RenderPipeline CreatePipeline();

		// Token: 0x17000869 RID: 2153
		// (get) Token: 0x060033D0 RID: 13264 RVA: 0x000655AC File Offset: 0x000637AC
		public virtual Type pipelineType
		{
			get
			{
				Debug.LogWarning("You must either inherit from RenderPipelineAsset<TRenderPipeline> or override pipelineType property.");
				return null;
			}
		}

		// Token: 0x1700086A RID: 2154
		// (get) Token: 0x060033D1 RID: 13265 RVA: 0x000655CA File Offset: 0x000637CA
		internal string pipelineTypeFullName
		{
			get
			{
				Type pipelineType = this.pipelineType;
				return ((pipelineType != null) ? pipelineType.FullName : null) ?? string.Empty;
			}
		}

		// Token: 0x060033D2 RID: 13266 RVA: 0x00002925 File Offset: 0x00000B25
		protected virtual void EnsureGlobalSettings()
		{
		}

		// Token: 0x060033D3 RID: 13267 RVA: 0x000655E7 File Offset: 0x000637E7
		protected virtual void OnValidate()
		{
			RenderPipelineManager.RecreateCurrentPipeline(this);
		}

		// Token: 0x060033D4 RID: 13268 RVA: 0x000655F1 File Offset: 0x000637F1
		protected virtual void OnDisable()
		{
			RenderPipelineManager.CleanupRenderPipeline();
		}

		// Token: 0x1700086B RID: 2155
		// (get) Token: 0x060033D5 RID: 13269 RVA: 0x000655FA File Offset: 0x000637FA
		protected internal virtual bool requiresCompatibleRenderPipelineGlobalSettings { get; } = false;

		// Token: 0x1700086C RID: 2156
		// (get) Token: 0x060033D6 RID: 13270 RVA: 0x00065604 File Offset: 0x00063804
		[Obsolete("This property is obsolete. Use pipelineType instead. #from(23.2)", false)]
		protected internal virtual Type renderPipelineType
		{
			get
			{
				Debug.LogWarning("You must either inherit from RenderPipelineAsset<TRenderPipeline> or override renderPipelineType property");
				return null;
			}
		}

		// Token: 0x1700086D RID: 2157
		// (get) Token: 0x060033D7 RID: 13271 RVA: 0x0003CDCD File Offset: 0x0003AFCD
		[Obsolete("This property is obsolete. Use RenderingLayerMask API and Tags & Layers project settings instead. #from(23.3)", false)]
		public virtual string[] renderingLayerMaskNames
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700086E RID: 2158
		// (get) Token: 0x060033D8 RID: 13272 RVA: 0x0003CDCD File Offset: 0x0003AFCD
		[Obsolete("This property is obsolete. Use RenderingLayerMask API and Tags & Layers project settings instead. #from(23.3)", false)]
		public virtual string[] prefixedRenderingLayerMaskNames
		{
			get
			{
				return null;
			}
		}
	}
}
