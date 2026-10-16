using System;

namespace UnityEngine.TextCore.Text
{
	// Token: 0x02000058 RID: 88
	internal struct HighlightState
	{
		// Token: 0x06000263 RID: 611 RVA: 0x0001FED4 File Offset: 0x0001E0D4
		public HighlightState(Color32 color, Offset padding)
		{
			this.color = color;
			this.padding = padding;
		}

		// Token: 0x06000264 RID: 612 RVA: 0x0001FEE8 File Offset: 0x0001E0E8
		public static bool operator ==(HighlightState lhs, HighlightState rhs)
		{
			return lhs.color.r == rhs.color.r && lhs.color.g == rhs.color.g && lhs.color.b == rhs.color.b && lhs.color.a == rhs.color.a && lhs.padding == rhs.padding;
		}

		// Token: 0x06000265 RID: 613 RVA: 0x0001FF70 File Offset: 0x0001E170
		public static bool operator !=(HighlightState lhs, HighlightState rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x06000266 RID: 614 RVA: 0x0001FF8C File Offset: 0x0001E18C
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000267 RID: 615 RVA: 0x0001FFB0 File Offset: 0x0001E1B0
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x06000268 RID: 616 RVA: 0x0001FFD4 File Offset: 0x0001E1D4
		public bool Equals(HighlightState other)
		{
			return base.Equals(other);
		}

		// Token: 0x0400030F RID: 783
		public Color32 color;

		// Token: 0x04000310 RID: 784
		public Offset padding;
	}
}
