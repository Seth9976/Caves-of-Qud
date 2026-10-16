using System;
using UnityEngine;

namespace TMPro
{
	// Token: 0x02000018 RID: 24
	public struct HighlightState
	{
		// Token: 0x06000079 RID: 121 RVA: 0x00002C45 File Offset: 0x00000E45
		public HighlightState(Color32 color, TMP_Offset padding)
		{
			this.color = color;
			this.padding = padding;
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00002C55 File Offset: 0x00000E55
		public static bool operator ==(HighlightState lhs, HighlightState rhs)
		{
			return lhs.color.Compare(rhs.color) && lhs.padding == rhs.padding;
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00002C7D File Offset: 0x00000E7D
		public static bool operator !=(HighlightState lhs, HighlightState rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00002C89 File Offset: 0x00000E89
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00002C9B File Offset: 0x00000E9B
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00002CAE File Offset: 0x00000EAE
		public bool Equals(HighlightState other)
		{
			return base.Equals(other);
		}

		// Token: 0x0400003E RID: 62
		public Color32 color;

		// Token: 0x0400003F RID: 63
		public TMP_Offset padding;
	}
}
