using System;
using UnityEngine;

namespace EnhancedUI.EnhancedScroller
{
	// Token: 0x020001DD RID: 477
	public class EnhancedScrollerCellView : MonoBehaviour
	{
		// Token: 0x06001135 RID: 4405 RVA: 0x000684A0 File Offset: 0x000666A0
		public virtual void RefreshCellView()
		{
		}

		// Token: 0x04000B57 RID: 2903
		public string cellIdentifier;

		// Token: 0x04000B58 RID: 2904
		[NonSerialized]
		public int cellIndex;

		// Token: 0x04000B59 RID: 2905
		[NonSerialized]
		public int dataIndex;

		// Token: 0x04000B5A RID: 2906
		[NonSerialized]
		public bool active;
	}
}
