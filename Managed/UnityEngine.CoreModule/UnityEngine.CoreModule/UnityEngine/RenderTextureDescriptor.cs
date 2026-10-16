using System;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Internal;
using UnityEngine.Rendering;

namespace UnityEngine
{
	// Token: 0x020001FC RID: 508
	public struct RenderTextureDescriptor
	{
		// Token: 0x1700045C RID: 1116
		// (get) Token: 0x06001ACE RID: 6862 RVA: 0x0002EE05 File Offset: 0x0002D005
		// (set) Token: 0x06001ACF RID: 6863 RVA: 0x0002EE0D File Offset: 0x0002D00D
		public int width { readonly get; set; }

		// Token: 0x1700045D RID: 1117
		// (get) Token: 0x06001AD0 RID: 6864 RVA: 0x0002EE16 File Offset: 0x0002D016
		// (set) Token: 0x06001AD1 RID: 6865 RVA: 0x0002EE1E File Offset: 0x0002D01E
		public int height { readonly get; set; }

		// Token: 0x1700045E RID: 1118
		// (get) Token: 0x06001AD2 RID: 6866 RVA: 0x0002EE27 File Offset: 0x0002D027
		// (set) Token: 0x06001AD3 RID: 6867 RVA: 0x0002EE2F File Offset: 0x0002D02F
		public int msaaSamples { readonly get; set; }

		// Token: 0x1700045F RID: 1119
		// (get) Token: 0x06001AD4 RID: 6868 RVA: 0x0002EE38 File Offset: 0x0002D038
		// (set) Token: 0x06001AD5 RID: 6869 RVA: 0x0002EE40 File Offset: 0x0002D040
		public int volumeDepth { readonly get; set; }

		// Token: 0x17000460 RID: 1120
		// (get) Token: 0x06001AD6 RID: 6870 RVA: 0x0002EE49 File Offset: 0x0002D049
		// (set) Token: 0x06001AD7 RID: 6871 RVA: 0x0002EE51 File Offset: 0x0002D051
		public int mipCount { readonly get; set; }

		// Token: 0x17000461 RID: 1121
		// (get) Token: 0x06001AD8 RID: 6872 RVA: 0x0002EE5C File Offset: 0x0002D05C
		// (set) Token: 0x06001AD9 RID: 6873 RVA: 0x0002EE74 File Offset: 0x0002D074
		public GraphicsFormat graphicsFormat
		{
			get
			{
				return this._graphicsFormat;
			}
			set
			{
				this._graphicsFormat = value;
				this.SetOrClearRenderTextureCreationFlag(GraphicsFormatUtility.IsSRGBFormat(value), RenderTextureCreationFlags.SRGB);
			}
		}

		// Token: 0x17000462 RID: 1122
		// (get) Token: 0x06001ADA RID: 6874 RVA: 0x0002EE8C File Offset: 0x0002D08C
		// (set) Token: 0x06001ADB RID: 6875 RVA: 0x0002EE94 File Offset: 0x0002D094
		public GraphicsFormat stencilFormat { readonly get; set; }

		// Token: 0x17000463 RID: 1123
		// (get) Token: 0x06001ADC RID: 6876 RVA: 0x0002EE9D File Offset: 0x0002D09D
		// (set) Token: 0x06001ADD RID: 6877 RVA: 0x0002EEA5 File Offset: 0x0002D0A5
		public GraphicsFormat depthStencilFormat { readonly get; set; }

		// Token: 0x17000464 RID: 1124
		// (get) Token: 0x06001ADE RID: 6878 RVA: 0x0002EEB0 File Offset: 0x0002D0B0
		// (set) Token: 0x06001ADF RID: 6879 RVA: 0x0002EEEC File Offset: 0x0002D0EC
		public RenderTextureFormat colorFormat
		{
			get
			{
				bool flag = this.graphicsFormat > GraphicsFormat.None;
				RenderTextureFormat renderTextureFormat;
				if (flag)
				{
					renderTextureFormat = GraphicsFormatUtility.GetRenderTextureFormat(this.graphicsFormat);
				}
				else
				{
					renderTextureFormat = ((this.shadowSamplingMode != ShadowSamplingMode.None) ? RenderTextureFormat.Shadowmap : RenderTextureFormat.Depth);
				}
				return renderTextureFormat;
			}
			set
			{
				this.shadowSamplingMode = RenderTexture.GetShadowSamplingModeForFormat(value);
				GraphicsFormat graphicsFormat = GraphicsFormatUtility.GetGraphicsFormat(value, this.sRGB);
				this.graphicsFormat = SystemInfo.GetCompatibleFormat(graphicsFormat, GraphicsFormatUsage.Render);
				this.depthStencilFormat = RenderTexture.GetDepthStencilFormatLegacy(this.depthBufferBits, this.shadowSamplingMode);
			}
		}

		// Token: 0x17000465 RID: 1125
		// (get) Token: 0x06001AE0 RID: 6880 RVA: 0x0002EF3C File Offset: 0x0002D13C
		// (set) Token: 0x06001AE1 RID: 6881 RVA: 0x0002EF59 File Offset: 0x0002D159
		public bool sRGB
		{
			get
			{
				return GraphicsFormatUtility.IsSRGBFormat(this.graphicsFormat);
			}
			set
			{
				this.graphicsFormat = ((value && QualitySettings.activeColorSpace == ColorSpace.Linear) ? GraphicsFormatUtility.GetSRGBFormat(this.graphicsFormat) : GraphicsFormatUtility.GetLinearFormat(this.graphicsFormat));
			}
		}

		// Token: 0x17000466 RID: 1126
		// (get) Token: 0x06001AE2 RID: 6882 RVA: 0x0002EF88 File Offset: 0x0002D188
		// (set) Token: 0x06001AE3 RID: 6883 RVA: 0x0002EFA5 File Offset: 0x0002D1A5
		public int depthBufferBits
		{
			get
			{
				return GraphicsFormatUtility.GetDepthBits(this.depthStencilFormat);
			}
			set
			{
				this.depthStencilFormat = RenderTexture.GetDepthStencilFormatLegacy(value, this.shadowSamplingMode);
			}
		}

		// Token: 0x17000467 RID: 1127
		// (get) Token: 0x06001AE4 RID: 6884 RVA: 0x0002EFBB File Offset: 0x0002D1BB
		// (set) Token: 0x06001AE5 RID: 6885 RVA: 0x0002EFC3 File Offset: 0x0002D1C3
		public TextureDimension dimension { readonly get; set; }

		// Token: 0x17000468 RID: 1128
		// (get) Token: 0x06001AE6 RID: 6886 RVA: 0x0002EFCC File Offset: 0x0002D1CC
		// (set) Token: 0x06001AE7 RID: 6887 RVA: 0x0002EFD4 File Offset: 0x0002D1D4
		public ShadowSamplingMode shadowSamplingMode { readonly get; set; }

		// Token: 0x17000469 RID: 1129
		// (get) Token: 0x06001AE8 RID: 6888 RVA: 0x0002EFDD File Offset: 0x0002D1DD
		// (set) Token: 0x06001AE9 RID: 6889 RVA: 0x0002EFE5 File Offset: 0x0002D1E5
		public VRTextureUsage vrUsage { readonly get; set; }

		// Token: 0x1700046A RID: 1130
		// (get) Token: 0x06001AEA RID: 6890 RVA: 0x0002EFF0 File Offset: 0x0002D1F0
		public RenderTextureCreationFlags flags
		{
			get
			{
				return this._flags;
			}
		}

		// Token: 0x1700046B RID: 1131
		// (get) Token: 0x06001AEB RID: 6891 RVA: 0x0002F008 File Offset: 0x0002D208
		// (set) Token: 0x06001AEC RID: 6892 RVA: 0x0002F010 File Offset: 0x0002D210
		public RenderTextureMemoryless memoryless { readonly get; set; }

		// Token: 0x06001AED RID: 6893 RVA: 0x0002F019 File Offset: 0x0002D219
		[ExcludeFromDocs]
		public RenderTextureDescriptor(int width, int height)
		{
			this = new RenderTextureDescriptor(width, height, RenderTextureFormat.Default);
		}

		// Token: 0x06001AEE RID: 6894 RVA: 0x0002F026 File Offset: 0x0002D226
		[ExcludeFromDocs]
		public RenderTextureDescriptor(int width, int height, RenderTextureFormat colorFormat)
		{
			this = new RenderTextureDescriptor(width, height, colorFormat, 0);
		}

		// Token: 0x06001AEF RID: 6895 RVA: 0x0002F034 File Offset: 0x0002D234
		[ExcludeFromDocs]
		public RenderTextureDescriptor(int width, int height, RenderTextureFormat colorFormat, int depthBufferBits)
		{
			this = new RenderTextureDescriptor(width, height, colorFormat, depthBufferBits, Texture.GenerateAllMips);
		}

		// Token: 0x06001AF0 RID: 6896 RVA: 0x0002F048 File Offset: 0x0002D248
		[ExcludeFromDocs]
		public RenderTextureDescriptor(int width, int height, GraphicsFormat colorFormat, int depthBufferBits)
		{
			this = new RenderTextureDescriptor(width, height, colorFormat, depthBufferBits, Texture.GenerateAllMips);
		}

		// Token: 0x06001AF1 RID: 6897 RVA: 0x0002F05C File Offset: 0x0002D25C
		[ExcludeFromDocs]
		public RenderTextureDescriptor(int width, int height, RenderTextureFormat colorFormat, int depthBufferBits, int mipCount)
		{
			this = new RenderTextureDescriptor(width, height, colorFormat, depthBufferBits, mipCount, RenderTextureReadWrite.Linear);
		}

		// Token: 0x06001AF2 RID: 6898 RVA: 0x0002F070 File Offset: 0x0002D270
		public RenderTextureDescriptor(int width, int height, [DefaultValue("RenderTextureFormat.Default")] RenderTextureFormat colorFormat, [DefaultValue("0")] int depthBufferBits, [DefaultValue("Texture.GenerateAllMips")] int mipCount, [DefaultValue("RenderTextureReadWrite.Linear")] RenderTextureReadWrite readWrite)
		{
			GraphicsFormat compatibleFormat = RenderTexture.GetCompatibleFormat(colorFormat, readWrite);
			this = new RenderTextureDescriptor(width, height, compatibleFormat, RenderTexture.GetDepthStencilFormatLegacy(depthBufferBits, colorFormat, false), mipCount);
			this.shadowSamplingMode = RenderTexture.GetShadowSamplingModeForFormat(colorFormat);
		}

		// Token: 0x06001AF3 RID: 6899 RVA: 0x0002F0B0 File Offset: 0x0002D2B0
		[ExcludeFromDocs]
		public RenderTextureDescriptor(int width, int height, GraphicsFormat colorFormat, int depthBufferBits, int mipCount)
		{
			this = default(RenderTextureDescriptor);
			this._flags = RenderTextureCreationFlags.AutoGenerateMips | RenderTextureCreationFlags.AllowVerticalFlip;
			this.width = width;
			this.height = height;
			this.volumeDepth = 1;
			this.msaaSamples = 1;
			this.graphicsFormat = colorFormat;
			this.depthStencilFormat = RenderTexture.GetDepthStencilFormatLegacy(depthBufferBits, colorFormat);
			this.mipCount = mipCount;
			this.dimension = TextureDimension.Tex2D;
			this.shadowSamplingMode = ShadowSamplingMode.None;
			this.vrUsage = VRTextureUsage.None;
			this.memoryless = RenderTextureMemoryless.None;
		}

		// Token: 0x06001AF4 RID: 6900 RVA: 0x0002F130 File Offset: 0x0002D330
		[ExcludeFromDocs]
		public RenderTextureDescriptor(int width, int height, GraphicsFormat colorFormat, GraphicsFormat depthStencilFormat)
		{
			this = new RenderTextureDescriptor(width, height, colorFormat, depthStencilFormat, Texture.GenerateAllMips);
		}

		// Token: 0x06001AF5 RID: 6901 RVA: 0x0002F144 File Offset: 0x0002D344
		[ExcludeFromDocs]
		public RenderTextureDescriptor(int width, int height, GraphicsFormat colorFormat, GraphicsFormat depthStencilFormat, int mipCount)
		{
			this = default(RenderTextureDescriptor);
			this._flags = RenderTextureCreationFlags.AutoGenerateMips | RenderTextureCreationFlags.AllowVerticalFlip;
			this.width = width;
			this.height = height;
			this.volumeDepth = 1;
			this.msaaSamples = 1;
			this.graphicsFormat = colorFormat;
			this.depthStencilFormat = depthStencilFormat;
			this.mipCount = mipCount;
			this.dimension = TextureDimension.Tex2D;
			this.shadowSamplingMode = ShadowSamplingMode.None;
			this.vrUsage = VRTextureUsage.None;
			this.memoryless = RenderTextureMemoryless.None;
		}

		// Token: 0x06001AF6 RID: 6902 RVA: 0x0002F1C0 File Offset: 0x0002D3C0
		private void SetOrClearRenderTextureCreationFlag(bool value, RenderTextureCreationFlags flag)
		{
			if (value)
			{
				this._flags |= flag;
			}
			else
			{
				this._flags &= ~flag;
			}
		}

		// Token: 0x1700046C RID: 1132
		// (get) Token: 0x06001AF7 RID: 6903 RVA: 0x0002F1F8 File Offset: 0x0002D3F8
		// (set) Token: 0x06001AF8 RID: 6904 RVA: 0x0002F215 File Offset: 0x0002D415
		public bool useMipMap
		{
			get
			{
				return (this._flags & RenderTextureCreationFlags.MipMap) > (RenderTextureCreationFlags)0;
			}
			set
			{
				this.SetOrClearRenderTextureCreationFlag(value, RenderTextureCreationFlags.MipMap);
			}
		}

		// Token: 0x1700046D RID: 1133
		// (get) Token: 0x06001AF9 RID: 6905 RVA: 0x0002F224 File Offset: 0x0002D424
		// (set) Token: 0x06001AFA RID: 6906 RVA: 0x0002F241 File Offset: 0x0002D441
		public bool autoGenerateMips
		{
			get
			{
				return (this._flags & RenderTextureCreationFlags.AutoGenerateMips) > (RenderTextureCreationFlags)0;
			}
			set
			{
				this.SetOrClearRenderTextureCreationFlag(value, RenderTextureCreationFlags.AutoGenerateMips);
			}
		}

		// Token: 0x1700046E RID: 1134
		// (get) Token: 0x06001AFB RID: 6907 RVA: 0x0002F250 File Offset: 0x0002D450
		// (set) Token: 0x06001AFC RID: 6908 RVA: 0x0002F26E File Offset: 0x0002D46E
		public bool enableRandomWrite
		{
			get
			{
				return (this._flags & RenderTextureCreationFlags.EnableRandomWrite) > (RenderTextureCreationFlags)0;
			}
			set
			{
				this.SetOrClearRenderTextureCreationFlag(value, RenderTextureCreationFlags.EnableRandomWrite);
			}
		}

		// Token: 0x1700046F RID: 1135
		// (get) Token: 0x06001AFD RID: 6909 RVA: 0x0002F27C File Offset: 0x0002D47C
		// (set) Token: 0x06001AFE RID: 6910 RVA: 0x0002F29D File Offset: 0x0002D49D
		public bool bindMS
		{
			get
			{
				return (this._flags & RenderTextureCreationFlags.BindMS) > (RenderTextureCreationFlags)0;
			}
			set
			{
				this.SetOrClearRenderTextureCreationFlag(value, RenderTextureCreationFlags.BindMS);
			}
		}

		// Token: 0x17000470 RID: 1136
		// (get) Token: 0x06001AFF RID: 6911 RVA: 0x0002F2B0 File Offset: 0x0002D4B0
		// (set) Token: 0x06001B00 RID: 6912 RVA: 0x0002F2CE File Offset: 0x0002D4CE
		internal bool createdFromScript
		{
			get
			{
				return (this._flags & RenderTextureCreationFlags.CreatedFromScript) > (RenderTextureCreationFlags)0;
			}
			set
			{
				this.SetOrClearRenderTextureCreationFlag(value, RenderTextureCreationFlags.CreatedFromScript);
			}
		}

		// Token: 0x17000471 RID: 1137
		// (get) Token: 0x06001B01 RID: 6913 RVA: 0x0002F2DC File Offset: 0x0002D4DC
		// (set) Token: 0x06001B02 RID: 6914 RVA: 0x0002F2FD File Offset: 0x0002D4FD
		public bool useDynamicScale
		{
			get
			{
				return (this._flags & RenderTextureCreationFlags.DynamicallyScalable) > (RenderTextureCreationFlags)0;
			}
			set
			{
				this.SetOrClearRenderTextureCreationFlag(value, RenderTextureCreationFlags.DynamicallyScalable);
			}
		}

		// Token: 0x17000472 RID: 1138
		// (get) Token: 0x06001B03 RID: 6915 RVA: 0x0002F310 File Offset: 0x0002D510
		// (set) Token: 0x06001B04 RID: 6916 RVA: 0x0002F331 File Offset: 0x0002D531
		public bool useDynamicScaleExplicit
		{
			get
			{
				return (this._flags & RenderTextureCreationFlags.DynamicallyScalableExplicit) > (RenderTextureCreationFlags)0;
			}
			set
			{
				this.SetOrClearRenderTextureCreationFlag(value, RenderTextureCreationFlags.DynamicallyScalableExplicit);
			}
		}

		// Token: 0x040006EF RID: 1775
		private GraphicsFormat _graphicsFormat;

		// Token: 0x040006F5 RID: 1781
		private RenderTextureCreationFlags _flags;
	}
}
