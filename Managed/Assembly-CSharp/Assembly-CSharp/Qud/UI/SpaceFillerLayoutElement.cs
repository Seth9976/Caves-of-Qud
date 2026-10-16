using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Qud.UI
{
	// Token: 0x02000428 RID: 1064
	[ExecuteAlways]
	public class SpaceFillerLayoutElement : MonoBehaviour, ILayoutElement
	{
		// Token: 0x1700053B RID: 1339
		// (get) Token: 0x0600283E RID: 10302 RVA: 0x000D04D5 File Offset: 0x000CE6D5
		public float minWidth
		{
			get
			{
				return -1f;
			}
		}

		// Token: 0x1700053C RID: 1340
		// (get) Token: 0x0600283F RID: 10303 RVA: 0x000D04DC File Offset: 0x000CE6DC
		public float preferredWidth
		{
			get
			{
				return this._preferredWidth;
			}
		}

		// Token: 0x1700053D RID: 1341
		// (get) Token: 0x06002840 RID: 10304 RVA: 0x000D04E4 File Offset: 0x000CE6E4
		public float flexibleWidth
		{
			get
			{
				return -1f;
			}
		}

		// Token: 0x1700053E RID: 1342
		// (get) Token: 0x06002841 RID: 10305 RVA: 0x000D04EB File Offset: 0x000CE6EB
		public float minHeight
		{
			get
			{
				return -1f;
			}
		}

		// Token: 0x1700053F RID: 1343
		// (get) Token: 0x06002842 RID: 10306 RVA: 0x000D04F2 File Offset: 0x000CE6F2
		public float preferredHeight
		{
			get
			{
				return -1f;
			}
		}

		// Token: 0x17000540 RID: 1344
		// (get) Token: 0x06002843 RID: 10307 RVA: 0x000D04F9 File Offset: 0x000CE6F9
		public float flexibleHeight
		{
			get
			{
				return -1f;
			}
		}

		// Token: 0x17000541 RID: 1345
		// (get) Token: 0x06002844 RID: 10308 RVA: 0x000D0500 File Offset: 0x000CE700
		public int layoutPriority
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x06002845 RID: 10309 RVA: 0x000D0504 File Offset: 0x000CE704
		private IEnumerable<RectTransform> OtherSiblings()
		{
			int count = base.gameObject.transform.parent.childCount;
			int num;
			for (int index = 0; index < count; index = num + 1)
			{
				GameObject gameObject = base.gameObject.transform.parent.GetChild(index).gameObject;
				if (!(gameObject == this.leftSideMirrorTarget.gameObject) && !(gameObject == base.gameObject))
				{
					yield return gameObject.transform as RectTransform;
				}
				num = index;
			}
			yield break;
		}

		// Token: 0x06002846 RID: 10310 RVA: 0x000D0514 File Offset: 0x000CE714
		public void CalculateLayoutInputHorizontal()
		{
			float preferredWidth = LayoutUtility.GetPreferredWidth(this.leftSideMirrorTarget);
			RectTransform rectTransform = base.gameObject.transform.parent as RectTransform;
			float num = this.OtherSiblings().Sum<RectTransform>(delegate(RectTransform s)
			{
				float minWidth = LayoutUtility.GetMinWidth(s);
				if (minWidth > 0f)
				{
					return minWidth;
				}
				return LayoutUtility.GetPreferredWidth(s);
			});
			this._preferredWidth = Math.Max(0f, Math.Min(preferredWidth, rectTransform.rect.width - num - preferredWidth));
		}

		// Token: 0x06002847 RID: 10311 RVA: 0x000D0596 File Offset: 0x000CE796
		public void CalculateLayoutInputVertical()
		{
		}

		// Token: 0x0400140B RID: 5131
		private float _preferredWidth;

		// Token: 0x0400140C RID: 5132
		public RectTransform leftSideMirrorTarget;
	}
}
