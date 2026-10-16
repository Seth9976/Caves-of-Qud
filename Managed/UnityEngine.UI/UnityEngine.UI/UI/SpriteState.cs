using System;
using UnityEngine.Serialization;

namespace UnityEngine.UI
{
	// Token: 0x02000038 RID: 56
	[Serializable]
	public struct SpriteState : IEquatable<SpriteState>
	{
		// Token: 0x1700011F RID: 287
		// (get) Token: 0x0600042F RID: 1071 RVA: 0x00014B72 File Offset: 0x00012D72
		// (set) Token: 0x06000430 RID: 1072 RVA: 0x00014B7A File Offset: 0x00012D7A
		public Sprite highlightedSprite
		{
			get
			{
				return this.m_HighlightedSprite;
			}
			set
			{
				this.m_HighlightedSprite = value;
			}
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x06000431 RID: 1073 RVA: 0x00014B83 File Offset: 0x00012D83
		// (set) Token: 0x06000432 RID: 1074 RVA: 0x00014B8B File Offset: 0x00012D8B
		public Sprite pressedSprite
		{
			get
			{
				return this.m_PressedSprite;
			}
			set
			{
				this.m_PressedSprite = value;
			}
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x06000433 RID: 1075 RVA: 0x00014B94 File Offset: 0x00012D94
		// (set) Token: 0x06000434 RID: 1076 RVA: 0x00014B9C File Offset: 0x00012D9C
		public Sprite selectedSprite
		{
			get
			{
				return this.m_SelectedSprite;
			}
			set
			{
				this.m_SelectedSprite = value;
			}
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x06000435 RID: 1077 RVA: 0x00014BA5 File Offset: 0x00012DA5
		// (set) Token: 0x06000436 RID: 1078 RVA: 0x00014BAD File Offset: 0x00012DAD
		public Sprite disabledSprite
		{
			get
			{
				return this.m_DisabledSprite;
			}
			set
			{
				this.m_DisabledSprite = value;
			}
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x00014BB8 File Offset: 0x00012DB8
		public bool Equals(SpriteState other)
		{
			return this.highlightedSprite == other.highlightedSprite && this.pressedSprite == other.pressedSprite && this.selectedSprite == other.selectedSprite && this.disabledSprite == other.disabledSprite;
		}

		// Token: 0x0400016A RID: 362
		[SerializeField]
		private Sprite m_HighlightedSprite;

		// Token: 0x0400016B RID: 363
		[SerializeField]
		private Sprite m_PressedSprite;

		// Token: 0x0400016C RID: 364
		[FormerlySerializedAs("m_HighlightedSprite")]
		[SerializeField]
		private Sprite m_SelectedSprite;

		// Token: 0x0400016D RID: 365
		[SerializeField]
		private Sprite m_DisabledSprite;
	}
}
