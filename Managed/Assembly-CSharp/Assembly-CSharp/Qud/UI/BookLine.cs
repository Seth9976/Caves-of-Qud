using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using XRL.UI;
using XRL.UI.Framework;

namespace Qud.UI
{
	// Token: 0x02000430 RID: 1072
	public class BookLine : MonoBehaviour, IFrameworkControl, IFrameworkControlSubcontexts
	{
		// Token: 0x17000545 RID: 1349
		// (get) Token: 0x060028A2 RID: 10402 RVA: 0x000D2B23 File Offset: 0x000D0D23
		public bool selected
		{
			get
			{
				BookLine.Context context = this.context;
				return context != null && context.IsActive(true);
			}
		}

		// Token: 0x060028A3 RID: 10403 RVA: 0x000D2B37 File Offset: 0x000D0D37
		public NavigationContext GetNavigationContext()
		{
			return this.context;
		}

		// Token: 0x060028A4 RID: 10404 RVA: 0x000D2B3F File Offset: 0x000D0D3F
		public void SetupContexts(ScrollChildContext scrollContext)
		{
			scrollContext.proxyTo = this.context;
		}

		// Token: 0x060028A5 RID: 10405 RVA: 0x000D2B50 File Offset: 0x000D0D50
		public void setData(FrameworkDataElement data)
		{
			BookLineData bookLineData = data as BookLineData;
			if (bookLineData != null)
			{
				this.text.SetText(bookLineData.text);
			}
		}

		// Token: 0x060028A6 RID: 10406 RVA: 0x000D2B7C File Offset: 0x000D0D7C
		public int HighlightClosestSpacer(Vector2 screenPosition)
		{
			AbilityManagerSpacer abilityManagerSpacer = ((this.spacers.Count > 0) ? this.spacers[0] : null);
			float num = float.MaxValue;
			int num2 = -1;
			Vector3[] array = new Vector3[4];
			int num3 = 0;
			foreach (AbilityManagerSpacer abilityManagerSpacer2 in this.spacers)
			{
				(abilityManagerSpacer2.transform as RectTransform).GetWorldCorners(array);
				float magnitude = (new Vector2((array[0].x + array[2].x) / 2f, (array[0].y + array[2].y) / 2f) - screenPosition).magnitude;
				if (magnitude < num)
				{
					abilityManagerSpacer = abilityManagerSpacer2;
					num = magnitude;
					num2 = num3;
				}
				abilityManagerSpacer2.image.enabled = false;
				num3++;
			}
			if (abilityManagerSpacer != null)
			{
				abilityManagerSpacer.image.enabled = true;
			}
			return num2;
		}

		// Token: 0x04001454 RID: 5204
		private BookLine.Context context = new BookLine.Context();

		// Token: 0x04001455 RID: 5205
		public UITextSkin text;

		// Token: 0x04001456 RID: 5206
		public StringBuilder SB = new StringBuilder();

		// Token: 0x04001457 RID: 5207
		private List<AbilityManagerSpacer> spacers = new List<AbilityManagerSpacer>();

		// Token: 0x02001802 RID: 6146
		public class Context : NavigationContext
		{
			// Token: 0x040060AA RID: 24746
			public BookLineData data;
		}
	}
}
