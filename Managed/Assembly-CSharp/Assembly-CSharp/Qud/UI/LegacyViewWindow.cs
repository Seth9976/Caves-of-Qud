using System;
using QupKit;

namespace Qud.UI
{
	// Token: 0x0200040F RID: 1039
	public class LegacyViewWindow<T> : WindowBase where T : BaseView, new()
	{
		// Token: 0x0600276C RID: 10092 RVA: 0x000CCA10 File Offset: 0x000CAC10
		public override void Init()
		{
			if (!this.AlreadyInit)
			{
				this.AlreadyInit = true;
				this.baseView = new T();
				this.baseView.AttachTo(base.gameObject);
				this.baseView.OnCreate();
			}
		}

		// Token: 0x0600276D RID: 10093 RVA: 0x000CCA5D File Offset: 0x000CAC5D
		public override void Show()
		{
			this.baseView.Enter();
			base.Show();
		}

		// Token: 0x0600276E RID: 10094 RVA: 0x000CCA75 File Offset: 0x000CAC75
		public override void Hide()
		{
			if (this.Previous != null)
			{
				LegacyViewManager.Instance.ActiveView = this.Previous;
				this.Previous.Enter();
			}
			this.baseView.Leave();
			base.Hide();
		}

		// Token: 0x0400137D RID: 4989
		protected T baseView;

		// Token: 0x0400137E RID: 4990
		protected BaseView Previous;

		// Token: 0x0400137F RID: 4991
		private bool AlreadyInit;
	}
}
