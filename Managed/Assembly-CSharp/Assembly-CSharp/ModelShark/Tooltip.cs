using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ModelShark
{
	// Token: 0x020001A0 RID: 416
	public class Tooltip
	{
		// Token: 0x17000185 RID: 389
		// (get) Token: 0x06000FA1 RID: 4001 RVA: 0x00062FC1 File Offset: 0x000611C1
		// (set) Token: 0x06000FA2 RID: 4002 RVA: 0x00062FC9 File Offset: 0x000611C9
		public RectTransform RectTransform { get; set; }

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x06000FA3 RID: 4003 RVA: 0x00062FD2 File Offset: 0x000611D2
		// (set) Token: 0x06000FA4 RID: 4004 RVA: 0x00062FDA File Offset: 0x000611DA
		public TooltipStyle TooltipStyle { get; set; }

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x06000FA5 RID: 4005 RVA: 0x00062FE3 File Offset: 0x000611E3
		// (set) Token: 0x06000FA6 RID: 4006 RVA: 0x00062FEB File Offset: 0x000611EB
		public GameObject GameObject { get; set; }

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x06000FA7 RID: 4007 RVA: 0x00062FF4 File Offset: 0x000611F4
		// (set) Token: 0x06000FA8 RID: 4008 RVA: 0x00062FFC File Offset: 0x000611FC
		public List<TextField> TextFields { get; set; }

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x06000FA9 RID: 4009 RVA: 0x00063005 File Offset: 0x00061205
		// (set) Token: 0x06000FAA RID: 4010 RVA: 0x0006300D File Offset: 0x0006120D
		public List<TMPField> TMPFields { get; set; }

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x06000FAB RID: 4011 RVA: 0x00063016 File Offset: 0x00061216
		// (set) Token: 0x06000FAC RID: 4012 RVA: 0x0006301E File Offset: 0x0006121E
		public List<ImageField> ImageFields { get; set; }

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x06000FAD RID: 4013 RVA: 0x00063027 File Offset: 0x00061227
		// (set) Token: 0x06000FAE RID: 4014 RVA: 0x0006302F File Offset: 0x0006122F
		public List<SectionField> SectionFields { get; set; }

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x06000FAF RID: 4015 RVA: 0x00063038 File Offset: 0x00061238
		// (set) Token: 0x06000FB0 RID: 4016 RVA: 0x00063040 File Offset: 0x00061240
		public Image BackgroundImage { get; set; }

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x06000FB1 RID: 4017 RVA: 0x00063049 File Offset: 0x00061249
		// (set) Token: 0x06000FB2 RID: 4018 RVA: 0x00063051 File Offset: 0x00061251
		public CanvasRenderer[] CanvasRenderers { get; set; }

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x06000FB3 RID: 4019 RVA: 0x0006305A File Offset: 0x0006125A
		// (set) Token: 0x06000FB4 RID: 4020 RVA: 0x00063062 File Offset: 0x00061262
		public Graphic[] Graphics { get; set; }

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x06000FB5 RID: 4021 RVA: 0x0006306B File Offset: 0x0006126B
		// (set) Token: 0x06000FB6 RID: 4022 RVA: 0x00063073 File Offset: 0x00061273
		public bool StaysOpen { get; set; }

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x06000FB7 RID: 4023 RVA: 0x0006307C File Offset: 0x0006127C
		// (set) Token: 0x06000FB8 RID: 4024 RVA: 0x00063084 File Offset: 0x00061284
		public bool NeverRotate { get; set; }

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x06000FB9 RID: 4025 RVA: 0x0006308D File Offset: 0x0006128D
		// (set) Token: 0x06000FBA RID: 4026 RVA: 0x00063095 File Offset: 0x00061295
		public bool IsBlocking { get; set; }

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x06000FBB RID: 4027 RVA: 0x0006309E File Offset: 0x0006129E
		// (set) Token: 0x06000FBC RID: 4028 RVA: 0x000630A5 File Offset: 0x000612A5
		public static string Delimiter { get; set; }

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x06000FBD RID: 4029 RVA: 0x000630AD File Offset: 0x000612AD
		// (set) Token: 0x06000FBE RID: 4030 RVA: 0x000630B5 File Offset: 0x000612B5
		public List<Tooltip.SetupHelper> SetupHelpers { get; set; }

		// Token: 0x06000FBF RID: 4031 RVA: 0x000630C0 File Offset: 0x000612C0
		public void Initialize()
		{
			if (string.IsNullOrEmpty(Tooltip.Delimiter))
			{
				Tooltip.Delimiter = TooltipManager.Instance.textFieldDelimiter;
			}
			this.RectTransform = this.GameObject.GetComponent<RectTransform>();
			this.TooltipStyle = this.GameObject.GetComponent<TooltipStyle>();
			this.BackgroundImage = this.GameObject.GetComponent<Image>();
			this.CanvasRenderers = this.GameObject.GetComponentsInChildren<CanvasRenderer>(true);
			this.Graphics = this.GameObject.GetComponentsInChildren<Graphic>(true);
			Text[] componentsInChildren = this.GameObject.GetComponentsInChildren<Text>(true);
			this.TextFields = new List<TextField>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				if (componentsInChildren[i].text.Contains(Tooltip.Delimiter))
				{
					this.TextFields.Add(new TextField
					{
						Text = componentsInChildren[i],
						Original = componentsInChildren[i].text
					});
				}
			}
			TextMeshProUGUI[] componentsInChildren2 = this.GameObject.GetComponentsInChildren<TextMeshProUGUI>(true);
			this.TMPFields = new List<TMPField>();
			for (int j = 0; j < componentsInChildren2.Length; j++)
			{
				if (componentsInChildren2[j].text.Contains(Tooltip.Delimiter))
				{
					this.TMPFields.Add(new TMPField
					{
						Text = componentsInChildren2[j],
						Original = componentsInChildren2[j].text
					});
				}
			}
			List<DynamicImage> list = this.GameObject.GetComponentsInChildren<DynamicImage>(true).ToList<DynamicImage>();
			this.ImageFields = new List<ImageField>();
			for (int k = 0; k < list.Count; k++)
			{
				Image component = list[k].GetComponent<Image>();
				this.ImageFields.Add(new ImageField
				{
					Image = component,
					Name = list[k].placeholderName.Trim(Tooltip.Delimiter.ToCharArray()),
					Original = component.sprite
				});
			}
			List<DynamicSection> list2 = this.GameObject.GetComponentsInChildren<DynamicSection>(true).ToList<DynamicSection>();
			this.SectionFields = new List<SectionField>();
			for (int l = 0; l < list2.Count; l++)
			{
				GameObject gameObject = list2[l].gameObject;
				this.SectionFields.Add(new SectionField
				{
					GameObject = gameObject,
					Name = list2[l].placeholderName.Trim(Tooltip.Delimiter.ToCharArray()),
					Original = gameObject.activeSelf
				});
			}
			this.SetupHelpers = this.GameObject.GetComponentsInChildren<Tooltip.SetupHelper>(true).ToList<Tooltip.SetupHelper>();
		}

		// Token: 0x06000FC0 RID: 4032 RVA: 0x00063338 File Offset: 0x00061538
		public void WarmUp()
		{
			this.GameObject.SetActive(true);
			for (int i = 0; i < this.CanvasRenderers.Length; i++)
			{
				this.CanvasRenderers[i].SetAlpha(0f);
			}
		}

		// Token: 0x06000FC1 RID: 4033 RVA: 0x00063378 File Offset: 0x00061578
		public void Deactivate()
		{
			if (TooltipManager.Instance == null)
			{
				return;
			}
			for (int i = 0; i < this.TextFields.Count; i++)
			{
				this.TextFields[i].Text.text = this.TextFields[i].Original;
			}
			for (int j = 0; j < this.TMPFields.Count; j++)
			{
				this.TMPFields[j].Text.text = this.TMPFields[j].Original;
			}
			for (int k = 0; k < this.ImageFields.Count; k++)
			{
				this.ImageFields[k].Image.sprite = this.ImageFields[k].Original;
			}
			if (TooltipManager.Instance.BlockingTooltip == this)
			{
				TooltipManager.Instance.BlockingTooltip = null;
			}
			this.GameObject.SetActive(false);
			this.GameObject.transform.SetParent(TooltipManager.Instance.TooltipContainer.transform, false);
		}

		// Token: 0x06000FC2 RID: 4034 RVA: 0x00063490 File Offset: 0x00061690
		public void Display(float fadeDuration)
		{
			if (fadeDuration > 0f)
			{
				for (int i = 0; i < this.Graphics.Length; i++)
				{
					this.Graphics[i].CrossFadeAlpha(1f, fadeDuration, true);
				}
				return;
			}
			for (int j = 0; j < this.CanvasRenderers.Length; j++)
			{
				this.CanvasRenderers[j].SetAlpha(1f);
			}
		}

		// Token: 0x02001623 RID: 5667
		public interface SetupHelper
		{
			// Token: 0x0600D6F2 RID: 55026
			void BeforeShow(TooltipTrigger trigger, Tooltip tooltip);
		}
	}
}
