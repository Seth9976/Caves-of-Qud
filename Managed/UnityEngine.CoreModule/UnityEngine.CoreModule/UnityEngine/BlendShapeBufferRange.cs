using System;

namespace UnityEngine
{
	// Token: 0x020001ED RID: 493
	[Serializable]
	public struct BlendShapeBufferRange
	{
		// Token: 0x170003CF RID: 975
		// (get) Token: 0x06001757 RID: 5975 RVA: 0x00028470 File Offset: 0x00026670
		// (set) Token: 0x06001758 RID: 5976 RVA: 0x00028488 File Offset: 0x00026688
		public uint startIndex
		{
			get
			{
				return this.m_StartIndex;
			}
			internal set
			{
				this.m_StartIndex = value;
			}
		}

		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x06001759 RID: 5977 RVA: 0x00028494 File Offset: 0x00026694
		// (set) Token: 0x0600175A RID: 5978 RVA: 0x000284AC File Offset: 0x000266AC
		public uint endIndex
		{
			get
			{
				return this.m_EndIndex;
			}
			internal set
			{
				this.m_EndIndex = value;
			}
		}

		// Token: 0x040006CB RID: 1739
		[SerializeField]
		private uint m_StartIndex;

		// Token: 0x040006CC RID: 1740
		[SerializeField]
		private uint m_EndIndex;
	}
}
