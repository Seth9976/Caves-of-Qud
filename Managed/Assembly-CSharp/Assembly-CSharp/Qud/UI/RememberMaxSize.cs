using System;
using UnityEngine;
using UnityEngine.UI;

namespace Qud.UI
{
	// Token: 0x02000426 RID: 1062
	[ExecuteAlways]
	[RequireComponent(typeof(RectTransform))]
	public class RememberMaxSize : MonoBehaviour, ILayoutElement
	{
		// Token: 0x17000533 RID: 1331
		// (get) Token: 0x0600282E RID: 10286 RVA: 0x000D0190 File Offset: 0x000CE390
		public float minWidth
		{
			get
			{
				if (!this.RememberWidth)
				{
					return -1f;
				}
				return this.RememberedWidth;
			}
		}

		// Token: 0x17000534 RID: 1332
		// (get) Token: 0x0600282F RID: 10287 RVA: 0x000D01A6 File Offset: 0x000CE3A6
		public float preferredWidth
		{
			get
			{
				if (!this.RememberWidth)
				{
					return -1f;
				}
				return this.RememberedWidth;
			}
		}

		// Token: 0x17000535 RID: 1333
		// (get) Token: 0x06002830 RID: 10288 RVA: 0x000D01BC File Offset: 0x000CE3BC
		public float flexibleWidth
		{
			get
			{
				return -1f;
			}
		}

		// Token: 0x17000536 RID: 1334
		// (get) Token: 0x06002831 RID: 10289 RVA: 0x000D01C3 File Offset: 0x000CE3C3
		public float minHeight
		{
			get
			{
				if (!this.RememberHeight)
				{
					return -1f;
				}
				return this.RememberedHeight;
			}
		}

		// Token: 0x17000537 RID: 1335
		// (get) Token: 0x06002832 RID: 10290 RVA: 0x000D01D9 File Offset: 0x000CE3D9
		public float preferredHeight
		{
			get
			{
				if (!this.RememberHeight)
				{
					return -1f;
				}
				return this.RememberedHeight;
			}
		}

		// Token: 0x17000538 RID: 1336
		// (get) Token: 0x06002833 RID: 10291 RVA: 0x000D01EF File Offset: 0x000CE3EF
		public float flexibleHeight
		{
			get
			{
				return -1f;
			}
		}

		// Token: 0x17000539 RID: 1337
		// (get) Token: 0x06002834 RID: 10292 RVA: 0x000D01F6 File Offset: 0x000CE3F6
		public int layoutPriority
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x06002835 RID: 10293 RVA: 0x000D01FA File Offset: 0x000CE3FA
		public void Awake()
		{
			this.forceReset = true;
		}

		// Token: 0x06002836 RID: 10294 RVA: 0x000D0204 File Offset: 0x000CE404
		public void CalculateLayoutInputHorizontal()
		{
			if (this.lastScreenSize.x != (float)Screen.width || this.lastScreenSize.y != (float)Screen.height || this.forceReset)
			{
				this.lastScreenSize = new Vector2((float)Screen.width, (float)Screen.height);
				this.RememberedHeight = (this.RememberedWidth = -1f);
				this.forceReset = false;
			}
			if (this.RememberWidth)
			{
				this.RememberWidth = false;
				this.RememberedWidth = Mathf.Max(this.RememberedWidth, LayoutUtility.GetPreferredWidth(base.transform as RectTransform));
				this.RememberWidth = true;
			}
		}

		// Token: 0x06002837 RID: 10295 RVA: 0x000D02A8 File Offset: 0x000CE4A8
		public void CalculateLayoutInputVertical()
		{
			if (this.RememberHeight)
			{
				this.RememberHeight = false;
				this.RememberedHeight = Mathf.Max(this.RememberedHeight, LayoutUtility.GetPreferredHeight(base.transform as RectTransform));
				this.RememberHeight = true;
			}
		}

		// Token: 0x040013FF RID: 5119
		public bool RememberHeight = true;

		// Token: 0x04001400 RID: 5120
		public bool RememberWidth;

		// Token: 0x04001401 RID: 5121
		public float RememberedHeight;

		// Token: 0x04001402 RID: 5122
		public float RememberedWidth;

		// Token: 0x04001403 RID: 5123
		public Vector2 lastScreenSize;

		// Token: 0x04001404 RID: 5124
		public bool forceReset;
	}
}
