using System;
using ConsoleLib.Console;
using TMPro;
using UnityEngine;
using XRL.World.Text;

namespace XRL.UI
{
	// Token: 0x02001477 RID: 5239
	[ExecuteAlways]
	[RequireComponent(typeof(TextMeshProUGUI))]
	public class UITextSkin : MonoBehaviour
	{
		// Token: 0x0600CE37 RID: 52791 RVA: 0x004AE335 File Offset: 0x004AC535
		private static void SwapBuffers()
		{
			TextBuilder buffer = UITextSkin.Buffer1;
			UITextSkin.Buffer1 = UITextSkin.Buffer2;
			UITextSkin.Buffer2 = buffer;
		}

		// Token: 0x0600CE38 RID: 52792 RVA: 0x004AE34C File Offset: 0x004AC54C
		private void SetTheText(ReadOnlySpan<char> Text)
		{
			if (this.StripFormatting)
			{
				ConsoleLib.Console.ColorUtility.StripFormatting(Text, UITextSkin.Buffer1);
			}
			else
			{
				UITextSkin.Buffer1.Rebase(Text);
			}
			ZMarkup.Transform(UITextSkin.Buffer1, UITextSkin.Buffer2, false);
			if (this.useBlockWrap && this.blockWrap > 0)
			{
				TextBlock.ParseSpan(UITextSkin.Buffer2.AsSpan(), UITextSkin.Buffer1, this.blockWrap, 5000, 0);
			}
			else
			{
				UITextSkin.SwapBuffers();
			}
			Sidebar.FormatToRTF(UITextSkin.Buffer1, UITextSkin.Buffer2, "FF");
			UITextSkin.Buffer2.SetToTMP(this.tmp);
			if (UITextSkin.LastCapacity != Math.Max(UITextSkin.Buffer1.Capacity, UITextSkin.Buffer2.Capacity))
			{
				UITextSkin.LastCapacity = Math.Max(UITextSkin.Buffer1.Capacity, UITextSkin.Buffer2.Capacity);
				MetricsManager.LogWarning(string.Format("Expanded buffer size, now {0}", UITextSkin.LastCapacity));
			}
		}

		// Token: 0x17000BEF RID: 3055
		// (get) Token: 0x0600CE39 RID: 52793 RVA: 0x004AE444 File Offset: 0x004AC644
		public RectTransform rectTransform
		{
			get
			{
				RectTransform rectTransform;
				if ((rectTransform = this._rt) == null)
				{
					rectTransform = (this._rt = base.GetComponent<RectTransform>());
				}
				return rectTransform;
			}
		}

		// Token: 0x17000BF0 RID: 3056
		// (get) Token: 0x0600CE3A RID: 52794 RVA: 0x004AE46A File Offset: 0x004AC66A
		private TextMeshProUGUI tmp
		{
			get
			{
				if (this._tmp == null)
				{
					this._tmp = base.GetComponent<TextMeshProUGUI>();
				}
				return this._tmp;
			}
		}

		// Token: 0x0600CE3B RID: 52795 RVA: 0x004AE48C File Offset: 0x004AC68C
		private TextMeshProUGUI Updated()
		{
			this.Apply();
			return this.tmp;
		}

		// Token: 0x17000BF1 RID: 3057
		// (get) Token: 0x0600CE3C RID: 52796 RVA: 0x004AE49A File Offset: 0x004AC69A
		public float preferredHeight
		{
			get
			{
				return this.Updated().preferredHeight;
			}
		}

		// Token: 0x17000BF2 RID: 3058
		// (get) Token: 0x0600CE3D RID: 52797 RVA: 0x004AE4A7 File Offset: 0x004AC6A7
		public float preferredWidth
		{
			get
			{
				return this.Updated().preferredWidth;
			}
		}

		// Token: 0x0600CE3E RID: 52798 RVA: 0x004AE4B4 File Offset: 0x004AC6B4
		public Vector2 GetPreferredValues(float MaxWidth, float MaxHeight)
		{
			return this.Updated().GetPreferredValues(MaxWidth, MaxHeight);
		}

		// Token: 0x0600CE3F RID: 52799 RVA: 0x004AE4C3 File Offset: 0x004AC6C3
		public Vector2 GetPreferredValues(string Text, float MaxWidth, float MaxHeight)
		{
			return this.Updated().GetPreferredValues(Text, MaxWidth, MaxHeight);
		}

		/// <summary>
		/// shortcut to set .<paramref name="text" /> then call .Apply()
		/// </summary>
		/// <param name="text" />
		// Token: 0x0600CE40 RID: 52800 RVA: 0x004AE4D3 File Offset: 0x004AC6D3
		public bool SetText(string text)
		{
			if (this.text == text)
			{
				return false;
			}
			this.text = text;
			this.formattedText = null;
			this.Apply();
			return true;
		}

		// Token: 0x17000BF3 RID: 3059
		// (get) Token: 0x0600CE41 RID: 52801 RVA: 0x004AE4FA File Offset: 0x004AC6FA
		// (set) Token: 0x0600CE42 RID: 52802 RVA: 0x004AE502 File Offset: 0x004AC702
		public bool StripFormatting
		{
			get
			{
				return this._StripFormatting;
			}
			set
			{
				this._StripFormatting = value;
				this.formattedText = null;
			}
		}

		// Token: 0x0600CE43 RID: 52803 RVA: 0x004AE514 File Offset: 0x004AC714
		public void Apply()
		{
			if (!this.autoSizeSet)
			{
				this.autoSizeSet = true;
				this.tmp.vertexBufferAutoSizeReduction = false;
			}
			int num = 16;
			if (this.style == UITextSkin.Size.xsmall)
			{
				num = 12;
			}
			if (this.style == UITextSkin.Size.small)
			{
				num = 14;
			}
			if (this.style == UITextSkin.Size.normal)
			{
				num = 16;
			}
			if (this.style == UITextSkin.Size.header)
			{
				num = 24;
			}
			if (this.style == UITextSkin.Size.subscript)
			{
				num = 8;
			}
			if (this.style == UITextSkin.Size.topstatusbar)
			{
				num = 16;
			}
			if (this.style == UITextSkin.Size.bottomstatusbar)
			{
				num = 14;
			}
			if (this.adjustmentFactor == UITextSkin.AdjustmentFactor.messageLog)
			{
				num += Options.MessageLogLineSizeAdjustment;
			}
			this.SetTheText(this.text);
			if (this.style != UITextSkin.Size.unset && this.tmp.fontSize != (float)num)
			{
				this.tmp.fontSize = (float)num;
			}
			if (this.tmp.color != this.color)
			{
				this.tmp.color = this.color;
			}
			if (this.bold)
			{
				if ((this.tmp.fontStyle & FontStyles.Bold) == FontStyles.Normal)
				{
					this.tmp.fontStyle = this.tmp.fontStyle + 1;
				}
			}
			else if ((this.tmp.fontStyle & FontStyles.Bold) != FontStyles.Normal)
			{
				this.tmp.fontStyle = this.tmp.fontStyle - 1;
			}
			if (this.textWrappingMode == UITextSkin.TextWrappingModeType.LegacyPaddingUpgradeMode && this.tmp.textWrappingMode == TextWrappingModes.Normal)
			{
				this.tmp.textWrappingMode = TextWrappingModes.PreserveWhitespace;
			}
		}

		// Token: 0x0600CE44 RID: 52804 RVA: 0x004AE67B File Offset: 0x004AC87B
		public void Start()
		{
			this.Apply();
		}

		// Token: 0x04004FE1 RID: 20449
		private static int LastCapacity = 32768;

		// Token: 0x04004FE2 RID: 20450
		private static TextBuilder Buffer1 = new StaticTextBuilder(32768);

		// Token: 0x04004FE3 RID: 20451
		private static TextBuilder Buffer2 = new StaticTextBuilder(32768);

		// Token: 0x04004FE4 RID: 20452
		private RectTransform _rt;

		// Token: 0x04004FE5 RID: 20453
		private TextMeshProUGUI _tmp;

		// Token: 0x04004FE6 RID: 20454
		public UITextSkin.Size style;

		// Token: 0x04004FE7 RID: 20455
		public UITextSkin.AdjustmentFactor adjustmentFactor;

		/// <summary>
		/// Call Apply() after setting this.
		/// </summary>
		// Token: 0x04004FE8 RID: 20456
		[Multiline]
		public string text = "";

		// Token: 0x04004FE9 RID: 20457
		public bool useBlockWrap = true;

		// Token: 0x04004FEA RID: 20458
		public int blockWrap = 72;

		// Token: 0x04004FEB RID: 20459
		protected string formattedText;

		// Token: 0x04004FEC RID: 20460
		public bool bold;

		// Token: 0x04004FED RID: 20461
		public Color color = new Color(0.69f, 0.78f, 0.76f);

		// Token: 0x04004FEE RID: 20462
		public UITextSkin.TextWrappingModeType textWrappingMode;

		// Token: 0x04004FEF RID: 20463
		protected bool _StripFormatting;

		// Token: 0x04004FF0 RID: 20464
		private bool autoSizeSet;

		// Token: 0x02001D91 RID: 7569
		public enum Size
		{
			// Token: 0x04007351 RID: 29521
			normal,
			// Token: 0x04007352 RID: 29522
			header,
			// Token: 0x04007353 RID: 29523
			subscript,
			// Token: 0x04007354 RID: 29524
			topstatusbar,
			// Token: 0x04007355 RID: 29525
			bottomstatusbar,
			// Token: 0x04007356 RID: 29526
			xsmall,
			// Token: 0x04007357 RID: 29527
			small,
			// Token: 0x04007358 RID: 29528
			unset
		}

		// Token: 0x02001D92 RID: 7570
		public enum AdjustmentFactor
		{
			// Token: 0x0400735A RID: 29530
			none,
			// Token: 0x0400735B RID: 29531
			messageLog
		}

		// Token: 0x02001D93 RID: 7571
		public enum TextWrappingModeType
		{
			// Token: 0x0400735D RID: 29533
			LegacyPaddingUpgradeMode
		}
	}
}
