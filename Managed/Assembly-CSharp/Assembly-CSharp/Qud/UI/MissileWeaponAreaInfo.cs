using System;
using System.Collections.Generic;
using ConsoleLib.Console;
using UnityEngine;
using UnityEngine.UI;
using XRL.UI;

namespace Qud.UI
{
	// Token: 0x0200048A RID: 1162
	public class MissileWeaponAreaInfo : MonoBehaviour
	{
		// Token: 0x06002CB3 RID: 11443 RVA: 0x000F196C File Offset: 0x000EFB6C
		private void UpdateImage(IRenderable Source)
		{
			if (Source == null)
			{
				if (this.Tile != null)
				{
					this.Tile = null;
					this.image.FromRenderable(null, true);
				}
				return;
			}
			string text = Source.getTile() ?? Source.getRenderString();
			bool hflip = Source.getHFlip();
			bool vflip = Source.getVFlip();
			ColorChars colorChars = Source.getColorChars();
			if (this.Tile == text && this.Colors.Equals(colorChars) && this.HFlip == hflip && this.VFlip == vflip)
			{
				return;
			}
			this.Tile = text;
			this.Colors = colorChars;
			this.HFlip = hflip;
			this.VFlip = vflip;
			this.image.FromRenderable(Source, true);
		}

		// Token: 0x06002CB4 RID: 11444 RVA: 0x000F1A18 File Offset: 0x000EFC18
		public void UpdateFrom(MissileWeaponArea.MissileWeaponAreaWeaponStatus status)
		{
			this.UpdateImage(status.renderable);
			if (this.totalBars != status.displayAmmoTotalBars)
			{
				if (this.totalBars > status.displayAmmoTotalBars)
				{
					int num = this.totalBars - status.displayAmmoTotalBars;
					for (int i = 0; i < num; i++)
					{
						global::UnityEngine.Object.Destroy(this.bars[0].gameObject);
						this.bars.RemoveAt(0);
					}
				}
				else
				{
					int num2 = status.displayAmmoTotalBars - this.totalBars;
					for (int j = 0; j < num2; j++)
					{
						Image component = global::UnityEngine.Object.Instantiate<GameObject>(this.fullBarPrefab).GetComponent<Image>();
						component.gameObject.SetActive(true);
						component.transform.SetParent(this.barContainer.transform, false);
						component.transform.SetAsLastSibling();
						this.bars.Add(component);
					}
				}
				this.totalBars = status.displayAmmoTotalBars;
			}
			for (int k = 0; k < this.totalBars; k++)
			{
				if (k < status.displayAmmoRemainingBars)
				{
					this.bars[k].color = this.fullColor;
				}
				else
				{
					this.bars[k].color = this.emptyColor;
				}
			}
			bool flag;
			if (status.text != null)
			{
				flag = this.text.SetText(status.text);
			}
			else
			{
				flag = this.text.SetText("");
			}
			if (flag)
			{
				LayoutRebuilder.MarkLayoutForRebuild(base.gameObject.transform as RectTransform);
				Canvas.ForceUpdateCanvases();
			}
		}

		// Token: 0x040018E8 RID: 6376
		public UIThreeColorProperties image;

		// Token: 0x040018E9 RID: 6377
		public GameObject fullBarPrefab;

		// Token: 0x040018EA RID: 6378
		public GameObject barContainer;

		// Token: 0x040018EB RID: 6379
		public UITextSkin text;

		// Token: 0x040018EC RID: 6380
		public Color fullColor;

		// Token: 0x040018ED RID: 6381
		public Color emptyColor;

		// Token: 0x040018EE RID: 6382
		public List<Image> bars = new List<Image>();

		// Token: 0x040018EF RID: 6383
		public int totalBars;

		// Token: 0x040018F0 RID: 6384
		private string Tile;

		// Token: 0x040018F1 RID: 6385
		private string RenderString;

		// Token: 0x040018F2 RID: 6386
		private ColorChars Colors;

		// Token: 0x040018F3 RID: 6387
		private bool HFlip;

		// Token: 0x040018F4 RID: 6388
		private bool VFlip;
	}
}
