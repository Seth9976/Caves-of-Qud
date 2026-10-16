using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Rendering;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000024 RID: 36
	[NativeHeader("Modules/Terrain/Public/TerrainDataScriptingInterface.h")]
	[UsedByNativeCode]
	[NativeAsStruct]
	[NativeHeader("TerrainScriptingClasses.h")]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class DetailPrototype
	{
		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000102 RID: 258 RVA: 0x000035B8 File Offset: 0x000017B8
		// (set) Token: 0x06000103 RID: 259 RVA: 0x000035D0 File Offset: 0x000017D0
		public GameObject prototype
		{
			get
			{
				return this.m_Prototype;
			}
			set
			{
				this.m_Prototype = value;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000104 RID: 260 RVA: 0x000035DC File Offset: 0x000017DC
		// (set) Token: 0x06000105 RID: 261 RVA: 0x000035F4 File Offset: 0x000017F4
		public Texture2D prototypeTexture
		{
			get
			{
				return this.m_PrototypeTexture;
			}
			set
			{
				this.m_PrototypeTexture = value;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000106 RID: 262 RVA: 0x00003600 File Offset: 0x00001800
		// (set) Token: 0x06000107 RID: 263 RVA: 0x00003618 File Offset: 0x00001818
		public float minWidth
		{
			get
			{
				return this.m_MinWidth;
			}
			set
			{
				this.m_MinWidth = value;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000108 RID: 264 RVA: 0x00003624 File Offset: 0x00001824
		// (set) Token: 0x06000109 RID: 265 RVA: 0x0000363C File Offset: 0x0000183C
		public float maxWidth
		{
			get
			{
				return this.m_MaxWidth;
			}
			set
			{
				this.m_MaxWidth = value;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x0600010A RID: 266 RVA: 0x00003648 File Offset: 0x00001848
		// (set) Token: 0x0600010B RID: 267 RVA: 0x00003660 File Offset: 0x00001860
		public float minHeight
		{
			get
			{
				return this.m_MinHeight;
			}
			set
			{
				this.m_MinHeight = value;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x0600010C RID: 268 RVA: 0x0000366C File Offset: 0x0000186C
		// (set) Token: 0x0600010D RID: 269 RVA: 0x00003684 File Offset: 0x00001884
		public float maxHeight
		{
			get
			{
				return this.m_MaxHeight;
			}
			set
			{
				this.m_MaxHeight = value;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x0600010E RID: 270 RVA: 0x00003690 File Offset: 0x00001890
		// (set) Token: 0x0600010F RID: 271 RVA: 0x000036A8 File Offset: 0x000018A8
		public int noiseSeed
		{
			get
			{
				return this.m_NoiseSeed;
			}
			set
			{
				this.m_NoiseSeed = value;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000110 RID: 272 RVA: 0x000036B4 File Offset: 0x000018B4
		// (set) Token: 0x06000111 RID: 273 RVA: 0x000036CC File Offset: 0x000018CC
		public float noiseSpread
		{
			get
			{
				return this.m_NoiseSpread;
			}
			set
			{
				this.m_NoiseSpread = value;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000112 RID: 274 RVA: 0x000036D8 File Offset: 0x000018D8
		// (set) Token: 0x06000113 RID: 275 RVA: 0x000036F0 File Offset: 0x000018F0
		public float density
		{
			get
			{
				return this.m_Density;
			}
			set
			{
				this.m_Density = value;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000114 RID: 276 RVA: 0x000036FC File Offset: 0x000018FC
		// (set) Token: 0x06000115 RID: 277 RVA: 0x00002E77 File Offset: 0x00001077
		[Obsolete("bendFactor has no effect and is deprecated.", false)]
		public float bendFactor
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000116 RID: 278 RVA: 0x00003714 File Offset: 0x00001914
		// (set) Token: 0x06000117 RID: 279 RVA: 0x0000372C File Offset: 0x0000192C
		public float holeEdgePadding
		{
			get
			{
				return this.m_HoleEdgePadding;
			}
			set
			{
				this.m_HoleEdgePadding = value;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000118 RID: 280 RVA: 0x00003738 File Offset: 0x00001938
		// (set) Token: 0x06000119 RID: 281 RVA: 0x00003750 File Offset: 0x00001950
		public Color healthyColor
		{
			get
			{
				return this.m_HealthyColor;
			}
			set
			{
				this.m_HealthyColor = value;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x0600011A RID: 282 RVA: 0x0000375C File Offset: 0x0000195C
		// (set) Token: 0x0600011B RID: 283 RVA: 0x00003774 File Offset: 0x00001974
		public Color dryColor
		{
			get
			{
				return this.m_DryColor;
			}
			set
			{
				this.m_DryColor = value;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x0600011C RID: 284 RVA: 0x00003780 File Offset: 0x00001980
		// (set) Token: 0x0600011D RID: 285 RVA: 0x00003798 File Offset: 0x00001998
		public DetailRenderMode renderMode
		{
			get
			{
				return (DetailRenderMode)this.m_RenderMode;
			}
			set
			{
				this.m_RenderMode = (int)value;
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x0600011E RID: 286 RVA: 0x000037A4 File Offset: 0x000019A4
		// (set) Token: 0x0600011F RID: 287 RVA: 0x000037BF File Offset: 0x000019BF
		public bool usePrototypeMesh
		{
			get
			{
				return this.m_UsePrototypeMesh != 0;
			}
			set
			{
				this.m_UsePrototypeMesh = (value ? 1 : 0);
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000120 RID: 288 RVA: 0x000037D0 File Offset: 0x000019D0
		// (set) Token: 0x06000121 RID: 289 RVA: 0x000037EB File Offset: 0x000019EB
		public bool useInstancing
		{
			get
			{
				return this.m_UseInstancing != 0;
			}
			set
			{
				this.m_UseInstancing = (value ? 1 : 0);
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000122 RID: 290 RVA: 0x000037FC File Offset: 0x000019FC
		// (set) Token: 0x06000123 RID: 291 RVA: 0x00003814 File Offset: 0x00001A14
		public float targetCoverage
		{
			get
			{
				return this.m_TargetCoverage;
			}
			set
			{
				this.m_TargetCoverage = value;
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000124 RID: 292 RVA: 0x00003820 File Offset: 0x00001A20
		// (set) Token: 0x06000125 RID: 293 RVA: 0x0000383B File Offset: 0x00001A3B
		public bool useDensityScaling
		{
			get
			{
				return this.m_UseDensityScaling != 0;
			}
			set
			{
				this.m_UseDensityScaling = (value ? 1 : 0);
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000126 RID: 294 RVA: 0x0000384C File Offset: 0x00001A4C
		// (set) Token: 0x06000127 RID: 295 RVA: 0x00003864 File Offset: 0x00001A64
		public float alignToGround
		{
			get
			{
				return this.m_AlignToGround;
			}
			set
			{
				this.m_AlignToGround = value;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000128 RID: 296 RVA: 0x00003870 File Offset: 0x00001A70
		// (set) Token: 0x06000129 RID: 297 RVA: 0x00003888 File Offset: 0x00001A88
		public float positionJitter
		{
			get
			{
				return this.m_PositionJitter;
			}
			set
			{
				this.m_PositionJitter = value;
			}
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00003894 File Offset: 0x00001A94
		public DetailPrototype()
		{
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00003960 File Offset: 0x00001B60
		public DetailPrototype(DetailPrototype other)
		{
			this.m_Prototype = other.m_Prototype;
			this.m_PrototypeTexture = other.m_PrototypeTexture;
			this.m_HealthyColor = other.m_HealthyColor;
			this.m_DryColor = other.m_DryColor;
			this.m_MinWidth = other.m_MinWidth;
			this.m_MaxWidth = other.m_MaxWidth;
			this.m_MinHeight = other.m_MinHeight;
			this.m_MaxHeight = other.m_MaxHeight;
			this.m_NoiseSeed = other.m_NoiseSeed;
			this.m_NoiseSpread = other.m_NoiseSpread;
			this.m_Density = other.m_Density;
			this.m_HoleEdgePadding = other.m_HoleEdgePadding;
			this.m_RenderMode = other.m_RenderMode;
			this.m_UsePrototypeMesh = other.m_UsePrototypeMesh;
			this.m_UseInstancing = other.m_UseInstancing;
			this.m_UseDensityScaling = other.m_UseDensityScaling;
			this.m_AlignToGround = other.m_AlignToGround;
			this.m_PositionJitter = other.m_PositionJitter;
			this.m_TargetCoverage = other.m_TargetCoverage;
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00003B10 File Offset: 0x00001D10
		public override bool Equals(object obj)
		{
			return this.Equals(obj as DetailPrototype);
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00003B30 File Offset: 0x00001D30
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00003B48 File Offset: 0x00001D48
		private bool Equals(DetailPrototype other)
		{
			bool flag = other == null;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				bool flag3 = other == this;
				if (flag3)
				{
					flag2 = true;
				}
				else
				{
					bool flag4 = base.GetType() != other.GetType();
					flag2 = !flag4 && (this.m_Prototype == other.m_Prototype && this.m_PrototypeTexture == other.m_PrototypeTexture && this.m_HealthyColor == other.m_HealthyColor && this.m_DryColor == other.m_DryColor && this.m_MinWidth == other.m_MinWidth && this.m_MaxWidth == other.m_MaxWidth && this.m_MinHeight == other.m_MinHeight && this.m_MaxHeight == other.m_MaxHeight && this.m_NoiseSeed == other.m_NoiseSeed && this.m_NoiseSpread == other.m_NoiseSpread && this.m_Density == other.m_Density && this.m_HoleEdgePadding == other.m_HoleEdgePadding && this.m_RenderMode == other.m_RenderMode && this.m_UsePrototypeMesh == other.m_UsePrototypeMesh && this.m_UseInstancing == other.m_UseInstancing && this.m_TargetCoverage == other.m_TargetCoverage) && this.m_UseDensityScaling == other.m_UseDensityScaling;
				}
			}
			return flag2;
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00003CB4 File Offset: 0x00001EB4
		public bool Validate()
		{
			string text;
			return DetailPrototype.ValidateDetailPrototype(this, out text);
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00003CC9 File Offset: 0x00001EC9
		public bool Validate(out string errorMessage)
		{
			return DetailPrototype.ValidateDetailPrototype(this, out errorMessage);
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00003CD4 File Offset: 0x00001ED4
		[FreeFunction("TerrainDataScriptingInterface::ValidateDetailPrototype")]
		internal static bool ValidateDetailPrototype([NotNull] DetailPrototype prototype, out string errorMessage)
		{
			if (prototype == null)
			{
				ThrowHelper.ThrowArgumentNullException(prototype, "prototype");
			}
			bool flag;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				flag = DetailPrototype.ValidateDetailPrototype_Injected(prototype, out managedSpanWrapper);
			}
			finally
			{
				ManagedSpanWrapper managedSpanWrapper;
				errorMessage = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
			}
			return flag;
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00003D18 File Offset: 0x00001F18
		internal static bool IsModeSupportedByRenderPipeline(DetailRenderMode renderMode, bool useInstancing, out string errorMessage)
		{
			bool flag = GraphicsSettings.currentRenderPipeline != null;
			if (flag)
			{
				bool flag2 = renderMode == DetailRenderMode.GrassBillboard && GraphicsSettings.GetDefaultShader(DefaultShaderType.TerrainDetailGrassBillboard) == null;
				if (flag2)
				{
					errorMessage = "The current render pipeline does not support Billboard details. Details will not be rendered.";
					return false;
				}
				bool flag3 = renderMode == DetailRenderMode.VertexLit && !useInstancing && GraphicsSettings.GetDefaultShader(DefaultShaderType.TerrainDetailLit) == null;
				if (flag3)
				{
					errorMessage = "The current render pipeline does not support VertexLit details. Details will be rendered using the default shader.";
					return false;
				}
				bool flag4 = renderMode == DetailRenderMode.Grass && GraphicsSettings.GetDefaultShader(DefaultShaderType.TerrainDetailGrass) == null;
				if (flag4)
				{
					errorMessage = "The current render pipeline does not support Grass details. Details will be rendered using the default shader without alpha test and animation.";
					return false;
				}
			}
			errorMessage = string.Empty;
			return true;
		}

		// Token: 0x06000134 RID: 308
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool ValidateDetailPrototype_Injected(DetailPrototype prototype, out ManagedSpanWrapper errorMessage);

		// Token: 0x0400006C RID: 108
		internal static readonly Color DefaultHealthColor = new Color(0.2627451f, 0.9764706f, 0.16470589f, 1f);

		// Token: 0x0400006D RID: 109
		internal static readonly Color DefaultDryColor = new Color(0.8039216f, 0.7372549f, 0.101960786f, 1f);

		// Token: 0x0400006E RID: 110
		[NativeName("prototype")]
		internal GameObject m_Prototype = null;

		// Token: 0x0400006F RID: 111
		[NativeName("prototypeTexture")]
		internal Texture2D m_PrototypeTexture = null;

		// Token: 0x04000070 RID: 112
		[NativeName("healthyColor")]
		internal Color m_HealthyColor = DetailPrototype.DefaultHealthColor;

		// Token: 0x04000071 RID: 113
		[NativeName("dryColor")]
		internal Color m_DryColor = DetailPrototype.DefaultDryColor;

		// Token: 0x04000072 RID: 114
		[NativeName("minWidth")]
		internal float m_MinWidth = 1f;

		// Token: 0x04000073 RID: 115
		[NativeName("maxWidth")]
		internal float m_MaxWidth = 2f;

		// Token: 0x04000074 RID: 116
		[NativeName("minHeight")]
		internal float m_MinHeight = 1f;

		// Token: 0x04000075 RID: 117
		[NativeName("maxHeight")]
		internal float m_MaxHeight = 2f;

		// Token: 0x04000076 RID: 118
		[NativeName("noiseSeed")]
		internal int m_NoiseSeed = 0;

		// Token: 0x04000077 RID: 119
		[NativeName("noiseSpread")]
		internal float m_NoiseSpread = 0.1f;

		// Token: 0x04000078 RID: 120
		[NativeName("density")]
		internal float m_Density = 1f;

		// Token: 0x04000079 RID: 121
		[NativeName("holeTestRadius")]
		internal float m_HoleEdgePadding = 0f;

		// Token: 0x0400007A RID: 122
		[NativeName("renderMode")]
		internal int m_RenderMode = 2;

		// Token: 0x0400007B RID: 123
		[NativeName("usePrototypeMesh")]
		internal int m_UsePrototypeMesh = 0;

		// Token: 0x0400007C RID: 124
		[NativeName("useInstancing")]
		internal int m_UseInstancing = 0;

		// Token: 0x0400007D RID: 125
		[NativeName("useDensityScaling")]
		internal int m_UseDensityScaling = 0;

		// Token: 0x0400007E RID: 126
		[NativeName("alignToGround")]
		internal float m_AlignToGround = 0f;

		// Token: 0x0400007F RID: 127
		[NativeName("positionJitter")]
		internal float m_PositionJitter = 0f;

		// Token: 0x04000080 RID: 128
		[NativeName("targetCoverage")]
		internal float m_TargetCoverage = 1f;
	}
}
