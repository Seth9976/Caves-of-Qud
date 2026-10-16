using System;
using UnityEngine.Events;

namespace XRL.UI.Framework
{
	// Token: 0x02001514 RID: 5396
	public class FrameworkUnityIndexSensitiveScrollChild : FrameworkUnityScrollChild, FrameworkScroller.IIndexSensitiveScrollChild
	{
		// Token: 0x0600D238 RID: 53816 RVA: 0x004E0C70 File Offset: 0x004DEE70
		public void SetIndex(int index)
		{
			if (this.onScrollIndexChanged != null)
			{
				this.onScrollIndexChanged.Invoke(index);
			}
		}

		// Token: 0x04005353 RID: 21331
		public UnityEvent<int> onScrollIndexChanged;
	}
}
