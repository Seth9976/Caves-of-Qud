using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000022 RID: 34
	[NativeHeader("Modules/Animation/AnimatorInfo.h")]
	[RequiredByNativeCode]
	public struct AnimatorStateInfo
	{
		// Token: 0x0600010C RID: 268 RVA: 0x00003DF0 File Offset: 0x00001FF0
		public bool IsName(string name)
		{
			int num = Animator.StringToHash(name);
			return num == this.m_FullPath || num == this.m_Name || num == this.m_Path;
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600010D RID: 269 RVA: 0x00003E28 File Offset: 0x00002028
		public int fullPathHash
		{
			get
			{
				return this.m_FullPath;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x0600010E RID: 270 RVA: 0x00003E40 File Offset: 0x00002040
		[Obsolete("AnimatorStateInfo.nameHash has been deprecated. Use AnimatorStateInfo.fullPathHash instead.")]
		public int nameHash
		{
			get
			{
				return this.m_Path;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x0600010F RID: 271 RVA: 0x00003E58 File Offset: 0x00002058
		public int shortNameHash
		{
			get
			{
				return this.m_Name;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000110 RID: 272 RVA: 0x00003E70 File Offset: 0x00002070
		public float normalizedTime
		{
			get
			{
				return this.m_NormalizedTime;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000111 RID: 273 RVA: 0x00003E88 File Offset: 0x00002088
		public float length
		{
			get
			{
				return this.m_Length;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000112 RID: 274 RVA: 0x00003EA0 File Offset: 0x000020A0
		public float speed
		{
			get
			{
				return this.m_Speed;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000113 RID: 275 RVA: 0x00003EB8 File Offset: 0x000020B8
		public float speedMultiplier
		{
			get
			{
				return this.m_SpeedMultiplier;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000114 RID: 276 RVA: 0x00003ED0 File Offset: 0x000020D0
		public int tagHash
		{
			get
			{
				return this.m_Tag;
			}
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00003EE8 File Offset: 0x000020E8
		public bool IsTag(string tag)
		{
			return Animator.StringToHash(tag) == this.m_Tag;
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000116 RID: 278 RVA: 0x00003F08 File Offset: 0x00002108
		public bool loop
		{
			get
			{
				return this.m_Loop != 0;
			}
		}

		// Token: 0x04000066 RID: 102
		private int m_Name;

		// Token: 0x04000067 RID: 103
		private int m_Path;

		// Token: 0x04000068 RID: 104
		private int m_FullPath;

		// Token: 0x04000069 RID: 105
		private float m_NormalizedTime;

		// Token: 0x0400006A RID: 106
		private float m_Length;

		// Token: 0x0400006B RID: 107
		private float m_Speed;

		// Token: 0x0400006C RID: 108
		private float m_SpeedMultiplier;

		// Token: 0x0400006D RID: 109
		private int m_Tag;

		// Token: 0x0400006E RID: 110
		private int m_Loop;
	}
}
