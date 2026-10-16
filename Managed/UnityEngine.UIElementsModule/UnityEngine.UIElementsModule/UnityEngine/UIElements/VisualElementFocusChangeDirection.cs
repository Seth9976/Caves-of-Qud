using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020005C1 RID: 1473
	public class VisualElementFocusChangeDirection : FocusChangeDirection
	{
		// Token: 0x17000AD2 RID: 2770
		// (get) Token: 0x06002B8F RID: 11151 RVA: 0x0009D2EC File Offset: 0x0009B4EC
		public static FocusChangeDirection left
		{
			get
			{
				return VisualElementFocusChangeDirection.s_Left;
			}
		}

		// Token: 0x17000AD3 RID: 2771
		// (get) Token: 0x06002B90 RID: 11152 RVA: 0x0009D2F3 File Offset: 0x0009B4F3
		public static FocusChangeDirection right
		{
			get
			{
				return VisualElementFocusChangeDirection.s_Right;
			}
		}

		// Token: 0x17000AD4 RID: 2772
		// (get) Token: 0x06002B91 RID: 11153 RVA: 0x0009D2FC File Offset: 0x0009B4FC
		protected new static VisualElementFocusChangeDirection lastValue
		{
			get
			{
				return VisualElementFocusChangeDirection.s_Right;
			}
		}

		// Token: 0x06002B92 RID: 11154 RVA: 0x00050ADD File Offset: 0x0004ECDD
		protected VisualElementFocusChangeDirection(int value)
			: base(value)
		{
		}

		// Token: 0x040013D9 RID: 5081
		private static readonly VisualElementFocusChangeDirection s_Left = new VisualElementFocusChangeDirection(FocusChangeDirection.lastValue + 1);

		// Token: 0x040013DA RID: 5082
		private static readonly VisualElementFocusChangeDirection s_Right = new VisualElementFocusChangeDirection(FocusChangeDirection.lastValue + 2);
	}
}
