using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace UnityEngine.Rendering
{
	// Token: 0x02000539 RID: 1337
	public struct DrawingSettings : IEquatable<DrawingSettings>
	{
		// Token: 0x06003335 RID: 13109 RVA: 0x000640B4 File Offset: 0x000622B4
		public unsafe DrawingSettings(ShaderTagId shaderPassName, SortingSettings sortingSettings)
		{
			this.m_SortingSettings = sortingSettings;
			this.m_PerObjectData = PerObjectData.None;
			this.m_Flags = DrawRendererFlags.EnableInstancing;
			this.m_OverrideShaderID = 0;
			this.m_OverrideShaderPassIndex = 0;
			this.m_OverrideMaterialInstanceId = 0;
			this.m_OverrideMaterialPassIndex = 0;
			this.m_fallbackMaterialInstanceId = 0;
			this.m_MainLightIndex = -1;
			fixed (int* ptr = &this.shaderPassNames.FixedElementField)
			{
				int* ptr2 = ptr;
				*ptr2 = shaderPassName.id;
				for (int i = 1; i < DrawingSettings.maxShaderPasses; i++)
				{
					ptr2[i] = -1;
				}
			}
			this.m_UseSrpBatcher = 0;
		}

		// Token: 0x1700082D RID: 2093
		// (get) Token: 0x06003336 RID: 13110 RVA: 0x00064144 File Offset: 0x00062344
		// (set) Token: 0x06003337 RID: 13111 RVA: 0x0006415C File Offset: 0x0006235C
		public SortingSettings sortingSettings
		{
			get
			{
				return this.m_SortingSettings;
			}
			set
			{
				this.m_SortingSettings = value;
			}
		}

		// Token: 0x1700082E RID: 2094
		// (get) Token: 0x06003338 RID: 13112 RVA: 0x00064168 File Offset: 0x00062368
		// (set) Token: 0x06003339 RID: 13113 RVA: 0x00064180 File Offset: 0x00062380
		public PerObjectData perObjectData
		{
			get
			{
				return this.m_PerObjectData;
			}
			set
			{
				this.m_PerObjectData = value;
			}
		}

		// Token: 0x1700082F RID: 2095
		// (get) Token: 0x0600333A RID: 13114 RVA: 0x0006418C File Offset: 0x0006238C
		// (set) Token: 0x0600333B RID: 13115 RVA: 0x000641AC File Offset: 0x000623AC
		public bool enableDynamicBatching
		{
			get
			{
				return (this.m_Flags & DrawRendererFlags.EnableDynamicBatching) > DrawRendererFlags.None;
			}
			set
			{
				if (value)
				{
					this.m_Flags |= DrawRendererFlags.EnableDynamicBatching;
				}
				else
				{
					this.m_Flags &= ~DrawRendererFlags.EnableDynamicBatching;
				}
			}
		}

		// Token: 0x17000830 RID: 2096
		// (get) Token: 0x0600333C RID: 13116 RVA: 0x000641E0 File Offset: 0x000623E0
		// (set) Token: 0x0600333D RID: 13117 RVA: 0x00064200 File Offset: 0x00062400
		public bool enableInstancing
		{
			get
			{
				return (this.m_Flags & DrawRendererFlags.EnableInstancing) > DrawRendererFlags.None;
			}
			set
			{
				if (value)
				{
					this.m_Flags |= DrawRendererFlags.EnableInstancing;
				}
				else
				{
					this.m_Flags &= ~DrawRendererFlags.EnableInstancing;
				}
			}
		}

		// Token: 0x17000831 RID: 2097
		// (get) Token: 0x0600333E RID: 13118 RVA: 0x00064234 File Offset: 0x00062434
		// (set) Token: 0x0600333F RID: 13119 RVA: 0x00064261 File Offset: 0x00062461
		public Material overrideMaterial
		{
			get
			{
				return (this.m_OverrideMaterialInstanceId != 0) ? (Object.FindObjectFromInstanceID(this.m_OverrideMaterialInstanceId) as Material) : null;
			}
			set
			{
				this.m_OverrideMaterialInstanceId = ((value != null) ? value.GetInstanceID() : 0);
			}
		}

		// Token: 0x17000832 RID: 2098
		// (get) Token: 0x06003340 RID: 13120 RVA: 0x00064278 File Offset: 0x00062478
		// (set) Token: 0x06003341 RID: 13121 RVA: 0x000642A5 File Offset: 0x000624A5
		public Shader overrideShader
		{
			get
			{
				return (this.m_OverrideShaderID != 0) ? (Object.FindObjectFromInstanceID(this.m_OverrideShaderID) as Shader) : null;
			}
			set
			{
				this.m_OverrideShaderID = ((value != null) ? value.GetInstanceID() : 0);
			}
		}

		// Token: 0x17000833 RID: 2099
		// (get) Token: 0x06003342 RID: 13122 RVA: 0x000642BC File Offset: 0x000624BC
		// (set) Token: 0x06003343 RID: 13123 RVA: 0x000642D4 File Offset: 0x000624D4
		public int overrideMaterialPassIndex
		{
			get
			{
				return this.m_OverrideMaterialPassIndex;
			}
			set
			{
				this.m_OverrideMaterialPassIndex = value;
			}
		}

		// Token: 0x17000834 RID: 2100
		// (get) Token: 0x06003344 RID: 13124 RVA: 0x000642E0 File Offset: 0x000624E0
		// (set) Token: 0x06003345 RID: 13125 RVA: 0x000642F8 File Offset: 0x000624F8
		public int overrideShaderPassIndex
		{
			get
			{
				return this.m_OverrideShaderPassIndex;
			}
			set
			{
				this.m_OverrideShaderPassIndex = value;
			}
		}

		// Token: 0x17000835 RID: 2101
		// (get) Token: 0x06003346 RID: 13126 RVA: 0x00064304 File Offset: 0x00062504
		// (set) Token: 0x06003347 RID: 13127 RVA: 0x00064331 File Offset: 0x00062531
		public Material fallbackMaterial
		{
			get
			{
				return (this.m_fallbackMaterialInstanceId != 0) ? (Object.FindObjectFromInstanceID(this.m_fallbackMaterialInstanceId) as Material) : null;
			}
			set
			{
				this.m_fallbackMaterialInstanceId = ((value != null) ? value.GetInstanceID() : 0);
			}
		}

		// Token: 0x17000836 RID: 2102
		// (get) Token: 0x06003348 RID: 13128 RVA: 0x00064348 File Offset: 0x00062548
		// (set) Token: 0x06003349 RID: 13129 RVA: 0x00064360 File Offset: 0x00062560
		public int mainLightIndex
		{
			get
			{
				return this.m_MainLightIndex;
			}
			set
			{
				this.m_MainLightIndex = value;
			}
		}

		// Token: 0x0600334A RID: 13130 RVA: 0x0006436C File Offset: 0x0006256C
		public unsafe ShaderTagId GetShaderPassName(int index)
		{
			bool flag = index >= DrawingSettings.maxShaderPasses || index < 0;
			if (flag)
			{
				throw new ArgumentOutOfRangeException("index", string.Format("Index should range from 0 to DrawSettings.maxShaderPasses ({0}), was {1}", DrawingSettings.maxShaderPasses, index));
			}
			fixed (int* ptr = &this.shaderPassNames.FixedElementField)
			{
				int* ptr2 = ptr;
				return new ShaderTagId
				{
					id = ptr2[index]
				};
			}
		}

		// Token: 0x0600334B RID: 13131 RVA: 0x000643E0 File Offset: 0x000625E0
		public unsafe void SetShaderPassName(int index, ShaderTagId shaderPassName)
		{
			bool flag = index >= DrawingSettings.maxShaderPasses || index < 0;
			if (flag)
			{
				throw new ArgumentOutOfRangeException("index", string.Format("Index should range from 0 to DrawSettings.maxShaderPasses ({0}), was {1}", DrawingSettings.maxShaderPasses, index));
			}
			fixed (int* ptr = &this.shaderPassNames.FixedElementField)
			{
				int* ptr2 = ptr;
				ptr2[index] = shaderPassName.id;
			}
		}

		// Token: 0x0600334C RID: 13132 RVA: 0x00064448 File Offset: 0x00062648
		public bool Equals(DrawingSettings other)
		{
			for (int i = 0; i < DrawingSettings.maxShaderPasses; i++)
			{
				bool flag = !this.GetShaderPassName(i).Equals(other.GetShaderPassName(i));
				if (flag)
				{
					return false;
				}
			}
			return this.m_SortingSettings.Equals(other.m_SortingSettings) && this.m_PerObjectData == other.m_PerObjectData && this.m_Flags == other.m_Flags && this.m_OverrideMaterialInstanceId == other.m_OverrideMaterialInstanceId && this.m_OverrideMaterialPassIndex == other.m_OverrideMaterialPassIndex && this.m_fallbackMaterialInstanceId == other.m_fallbackMaterialInstanceId && this.m_UseSrpBatcher == other.m_UseSrpBatcher;
		}

		// Token: 0x0600334D RID: 13133 RVA: 0x00064504 File Offset: 0x00062704
		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is DrawingSettings && this.Equals((DrawingSettings)obj);
		}

		// Token: 0x0600334E RID: 13134 RVA: 0x0006453C File Offset: 0x0006273C
		public override int GetHashCode()
		{
			int num = this.m_SortingSettings.GetHashCode();
			num = (num * 397) ^ (int)this.m_PerObjectData;
			num = (num * 397) ^ (int)this.m_Flags;
			num = (num * 397) ^ this.m_OverrideMaterialInstanceId;
			num = (num * 397) ^ this.m_OverrideMaterialPassIndex;
			num = (num * 397) ^ this.m_fallbackMaterialInstanceId;
			return (num * 397) ^ this.m_UseSrpBatcher;
		}

		// Token: 0x0600334F RID: 13135 RVA: 0x000645BC File Offset: 0x000627BC
		public static bool operator ==(DrawingSettings left, DrawingSettings right)
		{
			return left.Equals(right);
		}

		// Token: 0x06003350 RID: 13136 RVA: 0x000645D8 File Offset: 0x000627D8
		public static bool operator !=(DrawingSettings left, DrawingSettings right)
		{
			return !left.Equals(right);
		}

		// Token: 0x0400111B RID: 4379
		private const int kMaxShaderPasses = 16;

		// Token: 0x0400111C RID: 4380
		public static readonly int maxShaderPasses = 16;

		// Token: 0x0400111D RID: 4381
		private SortingSettings m_SortingSettings;

		// Token: 0x0400111E RID: 4382
		[FixedBuffer(typeof(int), 16)]
		internal DrawingSettings.<shaderPassNames>e__FixedBuffer shaderPassNames;

		// Token: 0x0400111F RID: 4383
		private PerObjectData m_PerObjectData;

		// Token: 0x04001120 RID: 4384
		private DrawRendererFlags m_Flags;

		// Token: 0x04001121 RID: 4385
		private int m_OverrideShaderID;

		// Token: 0x04001122 RID: 4386
		private int m_OverrideShaderPassIndex;

		// Token: 0x04001123 RID: 4387
		private int m_OverrideMaterialInstanceId;

		// Token: 0x04001124 RID: 4388
		private int m_OverrideMaterialPassIndex;

		// Token: 0x04001125 RID: 4389
		private int m_fallbackMaterialInstanceId;

		// Token: 0x04001126 RID: 4390
		private int m_MainLightIndex;

		// Token: 0x04001127 RID: 4391
		private int m_UseSrpBatcher;

		// Token: 0x0200053A RID: 1338
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 64)]
		public struct <shaderPassNames>e__FixedBuffer
		{
			// Token: 0x04001128 RID: 4392
			public int FixedElementField;
		}
	}
}
