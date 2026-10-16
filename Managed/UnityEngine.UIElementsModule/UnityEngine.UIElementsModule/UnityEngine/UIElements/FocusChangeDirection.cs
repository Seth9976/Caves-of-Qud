using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020002B8 RID: 696
	public class FocusChangeDirection : IDisposable
	{
		// Token: 0x170003B0 RID: 944
		// (get) Token: 0x060013DC RID: 5084 RVA: 0x0004FA50 File Offset: 0x0004DC50
		public static FocusChangeDirection unspecified { get; } = new FocusChangeDirection(-1);

		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x060013DD RID: 5085 RVA: 0x0004FA57 File Offset: 0x0004DC57
		public static FocusChangeDirection none { get; } = new FocusChangeDirection(0);

		// Token: 0x170003B2 RID: 946
		// (get) Token: 0x060013DE RID: 5086 RVA: 0x0004FA5E File Offset: 0x0004DC5E
		protected static FocusChangeDirection lastValue { get; } = FocusChangeDirection.none;

		// Token: 0x060013DF RID: 5087 RVA: 0x0004FA65 File Offset: 0x0004DC65
		protected FocusChangeDirection(int value)
		{
			this.m_Value = value;
		}

		// Token: 0x060013E0 RID: 5088 RVA: 0x0004FA78 File Offset: 0x0004DC78
		public static implicit operator int(FocusChangeDirection fcd)
		{
			return (fcd != null) ? fcd.m_Value : 0;
		}

		// Token: 0x060013E1 RID: 5089 RVA: 0x0004FA96 File Offset: 0x0004DC96
		void IDisposable.Dispose()
		{
			this.Dispose();
		}

		// Token: 0x060013E2 RID: 5090 RVA: 0x00002152 File Offset: 0x00000352
		protected virtual void Dispose()
		{
		}

		// Token: 0x060013E3 RID: 5091 RVA: 0x0004FA9F File Offset: 0x0004DC9F
		internal virtual void ApplyTo(FocusController focusController, Focusable f)
		{
			focusController.SwitchFocus(f, this, false, DispatchMode.Default);
		}

		// Token: 0x04000ACB RID: 2763
		private readonly int m_Value;
	}
}
