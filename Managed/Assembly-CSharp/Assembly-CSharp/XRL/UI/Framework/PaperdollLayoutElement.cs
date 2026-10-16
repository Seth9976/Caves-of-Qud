using System;
using Genkit;
using Qud.UI;
using UnityEngine;
using UnityEngine.UI;

namespace XRL.UI.Framework
{
	// Token: 0x020014EE RID: 5358
	public class PaperdollLayoutElement : LayoutElement
	{
		// Token: 0x0600D146 RID: 53574 RVA: 0x004DAC27 File Offset: 0x004D8E27
		public void setSize(int x, int y)
		{
			this.backdrop.sizeDelta = new Vector2((float)(x * 90 / 32) * 32f + 64f, (float)(y * 120 / 32) * 32f + 64f);
		}

		// Token: 0x0600D147 RID: 53575 RVA: 0x004DAC64 File Offset: 0x004D8E64
		public void clearConnector()
		{
			if (this.connectorImage.transform.parent != base.gameObject.transform)
			{
				this.connectorImage.transform.SetParent(base.gameObject.transform, false);
			}
			this.connectorImage.sizeDelta = new Vector2(0f, 0f);
			this.connectorImage.anchoredPosition = new Vector2(0f, 0f);
			if (this.connectorDirection == "L")
			{
				this.connectLeft();
			}
			if (this.connectorDirection == "R")
			{
				this.connectRight();
			}
			if (this.connectorDirection == "U")
			{
				this.connectUp();
			}
			if (this.connectorDirection == "D")
			{
				this.connectDown();
			}
		}

		// Token: 0x0600D148 RID: 53576 RVA: 0x004DAD43 File Offset: 0x004D8F43
		public void connectLeft()
		{
			this.connectorImage.sizeDelta = new Vector2(90f, 2f);
			this.connectorImage.anchoredPosition = new Vector2(-90f, 0f);
		}

		// Token: 0x0600D149 RID: 53577 RVA: 0x004DAD79 File Offset: 0x004D8F79
		public void connectRight()
		{
			this.connectorImage.sizeDelta = new Vector2(90f, 2f);
			this.connectorImage.anchoredPosition = new Vector2(0f, 0f);
		}

		// Token: 0x0600D14A RID: 53578 RVA: 0x004DADAF File Offset: 0x004D8FAF
		public void connectUp()
		{
			this.connectorImage.sizeDelta = new Vector2(2f, 120f);
			this.connectorImage.anchoredPosition = new Vector2(0f, -120f);
		}

		// Token: 0x0600D14B RID: 53579 RVA: 0x004DADE5 File Offset: 0x004D8FE5
		public void connectDown()
		{
			this.connectorImage.sizeDelta = new Vector2(2f, 120f);
			this.connectorImage.anchoredPosition = new Vector2(0f, 0f);
		}

		// Token: 0x04005295 RID: 21141
		public ScrollChildContext context;

		// Token: 0x04005296 RID: 21142
		public FrameworkUnityScrollChild scrollChild;

		// Token: 0x04005297 RID: 21143
		public EquipmentLineData data;

		// Token: 0x04005298 RID: 21144
		public Rect2D gridLocation;

		// Token: 0x04005299 RID: 21145
		public RectTransform connectorImage;

		// Token: 0x0400529A RID: 21146
		public RectTransform backdrop;

		// Token: 0x0400529B RID: 21147
		public string connectorDirection = ".";

		// Token: 0x0400529C RID: 21148
		public const int CONNECTOR_WIDTH = 90;

		// Token: 0x0400529D RID: 21149
		public const int CONNECTOR_HEIGHT = 120;

		// Token: 0x0400529E RID: 21150
		public const int CONNECTOR_X = 0;

		// Token: 0x0400529F RID: 21151
		public const int CONNECTOR_Y = 0;
	}
}
