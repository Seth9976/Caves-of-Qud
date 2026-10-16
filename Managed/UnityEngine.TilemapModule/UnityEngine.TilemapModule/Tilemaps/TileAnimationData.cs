using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Tilemaps
{
	// Token: 0x02000018 RID: 24
	[RequiredByNativeCode]
	[NativeType(Header = "Modules/Tilemap/TilemapScripting.h")]
	public struct TileAnimationData
	{
		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600013E RID: 318 RVA: 0x00003FD4 File Offset: 0x000021D4
		// (set) Token: 0x0600013F RID: 319 RVA: 0x00003FEC File Offset: 0x000021EC
		public Sprite[] animatedSprites
		{
			get
			{
				return this.m_AnimatedSprites;
			}
			set
			{
				this.m_AnimatedSprites = value;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000140 RID: 320 RVA: 0x00003FF8 File Offset: 0x000021F8
		// (set) Token: 0x06000141 RID: 321 RVA: 0x00004010 File Offset: 0x00002210
		public float animationSpeed
		{
			get
			{
				return this.m_AnimationSpeed;
			}
			set
			{
				this.m_AnimationSpeed = value;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000142 RID: 322 RVA: 0x0000401C File Offset: 0x0000221C
		// (set) Token: 0x06000143 RID: 323 RVA: 0x00004034 File Offset: 0x00002234
		public float animationStartTime
		{
			get
			{
				return this.m_AnimationStartTime;
			}
			set
			{
				this.m_AnimationStartTime = value;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000144 RID: 324 RVA: 0x00004040 File Offset: 0x00002240
		// (set) Token: 0x06000145 RID: 325 RVA: 0x00004058 File Offset: 0x00002258
		public TileAnimationFlags flags
		{
			get
			{
				return this.m_Flags;
			}
			set
			{
				this.m_Flags = value;
			}
		}

		// Token: 0x04000060 RID: 96
		private Sprite[] m_AnimatedSprites;

		// Token: 0x04000061 RID: 97
		private float m_AnimationSpeed;

		// Token: 0x04000062 RID: 98
		private float m_AnimationStartTime;

		// Token: 0x04000063 RID: 99
		private TileAnimationFlags m_Flags;
	}
}
