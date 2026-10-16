using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020005C2 RID: 1474
	internal class VisualElementFocusChangeTarget : FocusChangeDirection
	{
		// Token: 0x06002B94 RID: 11156 RVA: 0x0009D344 File Offset: 0x0009B544
		public static VisualElementFocusChangeTarget GetPooled(Focusable target)
		{
			VisualElementFocusChangeTarget visualElementFocusChangeTarget = VisualElementFocusChangeTarget.Pool.Get();
			visualElementFocusChangeTarget.target = target;
			return visualElementFocusChangeTarget;
		}

		// Token: 0x06002B95 RID: 11157 RVA: 0x0009D36A File Offset: 0x0009B56A
		protected override void Dispose()
		{
			this.target = null;
			VisualElementFocusChangeTarget.Pool.Release(this);
		}

		// Token: 0x06002B96 RID: 11158 RVA: 0x0009D381 File Offset: 0x0009B581
		internal override void ApplyTo(FocusController focusController, Focusable f)
		{
			focusController.selectedTextElement = null;
			f.Focus();
		}

		// Token: 0x06002B97 RID: 11159 RVA: 0x0009D393 File Offset: 0x0009B593
		public VisualElementFocusChangeTarget()
			: base(FocusChangeDirection.unspecified)
		{
		}

		// Token: 0x17000AD5 RID: 2773
		// (get) Token: 0x06002B98 RID: 11160 RVA: 0x0009D3A7 File Offset: 0x0009B5A7
		// (set) Token: 0x06002B99 RID: 11161 RVA: 0x0009D3AF File Offset: 0x0009B5AF
		public Focusable target { get; private set; }

		// Token: 0x040013DB RID: 5083
		private static readonly ObjectPool<VisualElementFocusChangeTarget> Pool = new ObjectPool<VisualElementFocusChangeTarget>(() => new VisualElementFocusChangeTarget(), 100);
	}
}
