using System;
using UnityEngine.Bindings;

namespace UnityEngine.U2D
{
	// Token: 0x020005B6 RID: 1462
	[VisibleToOtherModules]
	internal struct SpriteChannelInfo
	{
		// Token: 0x17000916 RID: 2326
		// (get) Token: 0x060037ED RID: 14317 RVA: 0x0006DD2C File Offset: 0x0006BF2C
		// (set) Token: 0x060037EE RID: 14318 RVA: 0x0006DD49 File Offset: 0x0006BF49
		public unsafe void* buffer
		{
			get
			{
				return (void*)this.m_Buffer;
			}
			set
			{
				this.m_Buffer = (IntPtr)value;
			}
		}

		// Token: 0x17000917 RID: 2327
		// (get) Token: 0x060037EF RID: 14319 RVA: 0x0006DD58 File Offset: 0x0006BF58
		// (set) Token: 0x060037F0 RID: 14320 RVA: 0x0006DD70 File Offset: 0x0006BF70
		public int count
		{
			get
			{
				return this.m_Count;
			}
			set
			{
				this.m_Count = value;
			}
		}

		// Token: 0x17000918 RID: 2328
		// (get) Token: 0x060037F1 RID: 14321 RVA: 0x0006DD7C File Offset: 0x0006BF7C
		// (set) Token: 0x060037F2 RID: 14322 RVA: 0x0006DD94 File Offset: 0x0006BF94
		public int offset
		{
			get
			{
				return this.m_Offset;
			}
			set
			{
				this.m_Offset = value;
			}
		}

		// Token: 0x17000919 RID: 2329
		// (get) Token: 0x060037F3 RID: 14323 RVA: 0x0006DDA0 File Offset: 0x0006BFA0
		// (set) Token: 0x060037F4 RID: 14324 RVA: 0x0006DDB8 File Offset: 0x0006BFB8
		public int stride
		{
			get
			{
				return this.m_Stride;
			}
			set
			{
				this.m_Stride = value;
			}
		}

		// Token: 0x0400140F RID: 5135
		[NativeName("buffer")]
		private IntPtr m_Buffer;

		// Token: 0x04001410 RID: 5136
		[NativeName("count")]
		private int m_Count;

		// Token: 0x04001411 RID: 5137
		[NativeName("offset")]
		private int m_Offset;

		// Token: 0x04001412 RID: 5138
		[NativeName("stride")]
		private int m_Stride;
	}
}
