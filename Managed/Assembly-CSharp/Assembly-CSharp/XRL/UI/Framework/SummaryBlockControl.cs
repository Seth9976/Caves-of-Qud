using System;
using Kobold;
using UnityEngine;

namespace XRL.UI.Framework
{
	// Token: 0x02001502 RID: 5378
	public class SummaryBlockControl : MonoBehaviour, IFrameworkControl
	{
		// Token: 0x0600D1D6 RID: 53718 RVA: 0x004DED54 File Offset: 0x004DCF54
		public void setData(FrameworkDataElement data)
		{
			SummaryBlockData summaryBlockData = data as SummaryBlockData;
			if (summaryBlockData == null)
			{
				throw new ArgumentException("StartingLocationData expected StartingLocationControl data");
			}
			if (string.IsNullOrEmpty(summaryBlockData.Description))
			{
				this.text.gameObject.SetActive(false);
			}
			else
			{
				this.text.gameObject.SetActive(true);
				this.text.SetText(summaryBlockData.Description);
			}
			if (string.IsNullOrEmpty(summaryBlockData.Title))
			{
				this.titleArea.SetActive(false);
			}
			else
			{
				this.titleArea.SetActive(true);
				this.title.SetText("{{W|" + summaryBlockData.Title + "}}");
			}
			if (string.IsNullOrEmpty(summaryBlockData.IconPath))
			{
				this.image.gameObject.SetActive(false);
			}
			else
			{
				this.image.gameObject.SetActive(true);
				this.image.image.sprite = SpriteManager.GetUnitySprite(summaryBlockData.IconPath);
				this.image.SetHFlip(true);
				this.image.SetColors(summaryBlockData.IconForegroundColor, summaryBlockData.IconDetailColor, Color.clear);
			}
			if (this.GetNavigationContext() != null)
			{
				this.GetNavigationContext().disabled = true;
			}
		}

		// Token: 0x0600D1D7 RID: 53719 RVA: 0x004DEE89 File Offset: 0x004DD089
		public NavigationContext GetNavigationContext()
		{
			FrameworkContext component = base.GetComponent<FrameworkContext>();
			if (component == null)
			{
				return null;
			}
			return component.context;
		}

		// Token: 0x04005305 RID: 21253
		public UIThreeColorProperties image;

		// Token: 0x04005306 RID: 21254
		public GameObject titleArea;

		// Token: 0x04005307 RID: 21255
		public UITextSkin title;

		// Token: 0x04005308 RID: 21256
		public UITextSkin text;
	}
}
