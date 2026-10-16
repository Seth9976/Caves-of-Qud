using System;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000025 RID: 37
	[Obsolete("SplatPrototype is obsolete. Use TerrainLayer instead.", false)]
	[UsedByNativeCode]
	[NativeAsStruct]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class SplatPrototype
	{
		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000135 RID: 309 RVA: 0x00003DF0 File Offset: 0x00001FF0
		// (set) Token: 0x06000136 RID: 310 RVA: 0x00003E08 File Offset: 0x00002008
		public Texture2D texture
		{
			get
			{
				return this.m_Texture;
			}
			set
			{
				this.m_Texture = value;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000137 RID: 311 RVA: 0x00003E14 File Offset: 0x00002014
		// (set) Token: 0x06000138 RID: 312 RVA: 0x00003E2C File Offset: 0x0000202C
		public Texture2D normalMap
		{
			get
			{
				return this.m_NormalMap;
			}
			set
			{
				this.m_NormalMap = value;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000139 RID: 313 RVA: 0x00003E38 File Offset: 0x00002038
		// (set) Token: 0x0600013A RID: 314 RVA: 0x00003E50 File Offset: 0x00002050
		public Vector2 tileSize
		{
			get
			{
				return this.m_TileSize;
			}
			set
			{
				this.m_TileSize = value;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x0600013B RID: 315 RVA: 0x00003E5C File Offset: 0x0000205C
		// (set) Token: 0x0600013C RID: 316 RVA: 0x00003E74 File Offset: 0x00002074
		public Vector2 tileOffset
		{
			get
			{
				return this.m_TileOffset;
			}
			set
			{
				this.m_TileOffset = value;
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x0600013D RID: 317 RVA: 0x00003E80 File Offset: 0x00002080
		// (set) Token: 0x0600013E RID: 318 RVA: 0x00003EB8 File Offset: 0x000020B8
		public Color specular
		{
			get
			{
				return new Color(this.m_SpecularMetallic.x, this.m_SpecularMetallic.y, this.m_SpecularMetallic.z);
			}
			set
			{
				this.m_SpecularMetallic.x = value.r;
				this.m_SpecularMetallic.y = value.g;
				this.m_SpecularMetallic.z = value.b;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x0600013F RID: 319 RVA: 0x00003EF0 File Offset: 0x000020F0
		// (set) Token: 0x06000140 RID: 320 RVA: 0x00003F0D File Offset: 0x0000210D
		public float metallic
		{
			get
			{
				return this.m_SpecularMetallic.w;
			}
			set
			{
				this.m_SpecularMetallic.w = value;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000141 RID: 321 RVA: 0x00003F1C File Offset: 0x0000211C
		// (set) Token: 0x06000142 RID: 322 RVA: 0x00003F34 File Offset: 0x00002134
		public float smoothness
		{
			get
			{
				return this.m_Smoothness;
			}
			set
			{
				this.m_Smoothness = value;
			}
		}

		// Token: 0x04000081 RID: 129
		[NativeName("texture")]
		internal Texture2D m_Texture;

		// Token: 0x04000082 RID: 130
		[NativeName("normalMap")]
		internal Texture2D m_NormalMap;

		// Token: 0x04000083 RID: 131
		[NativeName("tileSize")]
		internal Vector2 m_TileSize = new Vector2(15f, 15f);

		// Token: 0x04000084 RID: 132
		[NativeName("tileOffset")]
		internal Vector2 m_TileOffset = new Vector2(0f, 0f);

		// Token: 0x04000085 RID: 133
		[NativeName("specularMetallic")]
		internal Vector4 m_SpecularMetallic = new Vector4(0f, 0f, 0f, 0f);

		// Token: 0x04000086 RID: 134
		[NativeName("smoothness")]
		internal float m_Smoothness = 0f;
	}
}
