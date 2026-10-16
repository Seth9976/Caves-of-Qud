using System;
using System.Collections.Generic;
using XRL.UI.Framework;

namespace Qud.UI
{
	// Token: 0x02000449 RID: 1097
	[Serializable]
	public abstract class OptionsDataRow : FrameworkDataElement
	{
		// Token: 0x060029A8 RID: 10664 RVA: 0x000DA3ED File Offset: 0x000D85ED
		public bool ValueChangedSinceLastObserved(object obj)
		{
			if (this._observersSeen.Contains(obj))
			{
				return false;
			}
			this._observersSeen.Add(obj);
			return true;
		}

		// Token: 0x060029A9 RID: 10665 RVA: 0x000DA40D File Offset: 0x000D860D
		protected void OnChange()
		{
			this._observersSeen.Clear();
		}

		// Token: 0x04001591 RID: 5521
		public Func<bool> IsEnabled;

		// Token: 0x04001592 RID: 5522
		public string CategoryId;

		// Token: 0x04001593 RID: 5523
		public string Title;

		// Token: 0x04001594 RID: 5524
		public string SearchWords;

		// Token: 0x04001595 RID: 5525
		public string HelpText;

		// Token: 0x04001596 RID: 5526
		protected HashSet<object> _observersSeen = new HashSet<object>();
	}
}
