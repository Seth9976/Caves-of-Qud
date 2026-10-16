using System;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000171 RID: 369
	[UsedByNativeCode]
	[NativeHeader("Runtime/Graphics/LightmapData.h")]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class LightmapData
	{
		// Token: 0x1700023F RID: 575
		// (get) Token: 0x06000D38 RID: 3384 RVA: 0x00018CCC File Offset: 0x00016ECC
		// (set) Token: 0x06000D39 RID: 3385 RVA: 0x00018CE4 File Offset: 0x00016EE4
		[Obsolete("Use lightmapColor property (UnityUpgradable) -> lightmapColor", false)]
		public Texture2D lightmapLight
		{
			get
			{
				return this.m_Light;
			}
			set
			{
				this.m_Light = value;
			}
		}

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x06000D3A RID: 3386 RVA: 0x00018CF0 File Offset: 0x00016EF0
		// (set) Token: 0x06000D3B RID: 3387 RVA: 0x00018CE4 File Offset: 0x00016EE4
		public Texture2D lightmapColor
		{
			get
			{
				return this.m_Light;
			}
			set
			{
				this.m_Light = value;
			}
		}

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x06000D3C RID: 3388 RVA: 0x00018D08 File Offset: 0x00016F08
		// (set) Token: 0x06000D3D RID: 3389 RVA: 0x00018D20 File Offset: 0x00016F20
		public Texture2D lightmapDir
		{
			get
			{
				return this.m_Dir;
			}
			set
			{
				this.m_Dir = value;
			}
		}

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x06000D3E RID: 3390 RVA: 0x00018D2C File Offset: 0x00016F2C
		// (set) Token: 0x06000D3F RID: 3391 RVA: 0x00018D44 File Offset: 0x00016F44
		public Texture2D shadowMask
		{
			get
			{
				return this.m_ShadowMask;
			}
			set
			{
				this.m_ShadowMask = value;
			}
		}

		// Token: 0x040004BE RID: 1214
		internal Texture2D m_Light;

		// Token: 0x040004BF RID: 1215
		internal Texture2D m_Dir;

		// Token: 0x040004C0 RID: 1216
		internal Texture2D m_ShadowMask;
	}
}
