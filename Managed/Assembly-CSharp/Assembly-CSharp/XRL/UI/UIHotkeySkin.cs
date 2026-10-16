using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Qud.UI;
using TMPro;
using UnityEngine;

namespace XRL.UI
{
	// Token: 0x02001475 RID: 5237
	[ExecuteAlways]
	[RequireComponent(typeof(TextMeshProUGUI))]
	public class UIHotkeySkin : MonoBehaviour
	{
		// Token: 0x17000BE7 RID: 3047
		// (get) Token: 0x0600CE21 RID: 52769 RVA: 0x004ADD68 File Offset: 0x004ABF68
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

		// Token: 0x17000BE8 RID: 3048
		// (get) Token: 0x0600CE22 RID: 52770 RVA: 0x004ADD8E File Offset: 0x004ABF8E
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

		// Token: 0x0600CE23 RID: 52771 RVA: 0x004ADDB0 File Offset: 0x004ABFB0
		private TextMeshProUGUI Updated()
		{
			this.Apply();
			return this.tmp;
		}

		// Token: 0x17000BE9 RID: 3049
		// (get) Token: 0x0600CE24 RID: 52772 RVA: 0x004ADDBE File Offset: 0x004ABFBE
		public float preferredHeight
		{
			get
			{
				return this.Updated().preferredHeight;
			}
		}

		// Token: 0x17000BEA RID: 3050
		// (get) Token: 0x0600CE25 RID: 52773 RVA: 0x004ADDCB File Offset: 0x004ABFCB
		public float preferredWidth
		{
			get
			{
				return this.Updated().preferredWidth;
			}
		}

		// Token: 0x0600CE26 RID: 52774 RVA: 0x004ADDD8 File Offset: 0x004ABFD8
		public Vector2 GetPreferredValues(float MaxWidth, float MaxHeight)
		{
			return this.Updated().GetPreferredValues(MaxWidth, MaxHeight);
		}

		// Token: 0x0600CE27 RID: 52775 RVA: 0x004ADDE7 File Offset: 0x004ABFE7
		public Vector2 GetPreferredValues(string Text, float MaxWidth, float MaxHeight)
		{
			return this.Updated().GetPreferredValues(Text, MaxWidth, MaxHeight);
		}

		/// <summary>
		/// shortcut to set .<paramref name="text" /> then call .Apply()
		/// </summary>
		/// <param name="text" />
		// Token: 0x0600CE28 RID: 52776 RVA: 0x004ADDF7 File Offset: 0x004ABFF7
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

		// Token: 0x17000BEB RID: 3051
		// (get) Token: 0x0600CE29 RID: 52777 RVA: 0x004ADE1E File Offset: 0x004AC01E
		// (set) Token: 0x0600CE2A RID: 52778 RVA: 0x004ADE26 File Offset: 0x004AC026
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

		// Token: 0x0600CE2B RID: 52779 RVA: 0x004ADE38 File Offset: 0x004AC038
		public void Apply()
		{
			int num = 16;
			if (this.style == UIHotkeySkin.Size.xsmall)
			{
				num = 12;
			}
			if (this.style == UIHotkeySkin.Size.small)
			{
				num = 14;
			}
			if (this.style == UIHotkeySkin.Size.normal)
			{
				num = 16;
			}
			if (this.style == UIHotkeySkin.Size.header)
			{
				num = 24;
			}
			if (this.style == UIHotkeySkin.Size.subscript)
			{
				num = 8;
			}
			if (this.style == UIHotkeySkin.Size.topstatusbar)
			{
				num = 16;
			}
			if (this.style == UIHotkeySkin.Size.bottomstatusbar)
			{
				num = 14;
			}
			this.lasttext = this.text;
			this.textProcess.Length = 0;
			this.textProcess.Append(this.text);
			if (UIHotkeySkin.keysByLength == null)
			{
				Dictionary<string, CommandBinding> commandBindings = CommandBindingManager.CommandBindings;
				List<string> list;
				if (commandBindings == null)
				{
					list = null;
				}
				else
				{
					Dictionary<string, CommandBinding>.KeyCollection keys = commandBindings.Keys;
					list = ((keys != null) ? keys.ToList<string>() : null);
				}
				UIHotkeySkin.keysByLength = list;
				List<string> list2 = UIHotkeySkin.keysByLength;
				if (list2 != null)
				{
					list2.Sort((string a, string b) => b.Length - a.Length);
				}
			}
			if (UIHotkeySkin.keysByLength != null && this.textProcess.Contains("~"))
			{
				for (int i = 0; i < UIHotkeySkin.keysByLength.Count; i++)
				{
					string text = UIHotkeySkin.keysByLength[i];
					if (this.textProcess.Contains("~" + text))
					{
						this.textProcess.Replace("~" + text, ControlManager.getCommandInputDescription(text, true, false, ControlManager.InputDeviceType.Unknown, false));
						if (!this.textProcess.Contains("~"))
						{
							break;
						}
					}
				}
			}
			this.formattedText = RTF.FormatToRTF(GameText.VariableReplace(this.textProcess, false), "FF", this.useBlockWrap ? this.blockWrap : (-1), this.StripFormatting);
			if (this.style != UIHotkeySkin.Size.unset && this.tmp.fontSize != (float)num)
			{
				this.tmp.fontSize = (float)num;
			}
			if (this.tmp.text != this.formattedText)
			{
				this.tmp.text = this.formattedText;
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
					return;
				}
			}
			else if ((this.tmp.fontStyle & FontStyles.Bold) != FontStyles.Normal)
			{
				this.tmp.fontStyle = this.tmp.fontStyle - 1;
			}
		}

		// Token: 0x0600CE2C RID: 52780 RVA: 0x004AE0A2 File Offset: 0x004AC2A2
		public void Start()
		{
			this.Apply();
		}

		// Token: 0x0600CE2D RID: 52781 RVA: 0x004AE0AC File Offset: 0x004AC2AC
		public void Update()
		{
			if (this.lastDeviceType != ControlManager.activeControllerType || this.lasttext != this.text || this.lastRefreshIndex != CommandBindingManager.BindingRefreshIndex)
			{
				this.Apply();
				this.lastDeviceType = ControlManager.activeControllerType;
				this.lastRefreshIndex = CommandBindingManager.BindingRefreshIndex;
			}
		}

		// Token: 0x04004FCB RID: 20427
		private RectTransform _rt;

		// Token: 0x04004FCC RID: 20428
		private TextMeshProUGUI _tmp;

		// Token: 0x04004FCD RID: 20429
		public UIHotkeySkin.Size style;

		/// <summary>
		/// Call Apply() after setting this.
		/// </summary>
		// Token: 0x04004FCE RID: 20430
		[Multiline]
		public string text = "";

		// Token: 0x04004FCF RID: 20431
		public bool useBlockWrap = true;

		// Token: 0x04004FD0 RID: 20432
		public int blockWrap = 72;

		// Token: 0x04004FD1 RID: 20433
		protected string formattedText;

		// Token: 0x04004FD2 RID: 20434
		public bool bold;

		// Token: 0x04004FD3 RID: 20435
		public Color color = new Color(0.69f, 0.78f, 0.76f);

		// Token: 0x04004FD4 RID: 20436
		protected bool _StripFormatting;

		// Token: 0x04004FD5 RID: 20437
		private static List<string> keysByLength;

		// Token: 0x04004FD6 RID: 20438
		private StringBuilder textProcess = new StringBuilder();

		// Token: 0x04004FD7 RID: 20439
		private string lasttext;

		// Token: 0x04004FD8 RID: 20440
		private int lastRefreshIndex = -1;

		// Token: 0x04004FD9 RID: 20441
		private ControlManager.InputDeviceType lastDeviceType = ControlManager.InputDeviceType.Unknown;

		// Token: 0x02001D8D RID: 7565
		public enum Size
		{
			// Token: 0x04007338 RID: 29496
			normal,
			// Token: 0x04007339 RID: 29497
			header,
			// Token: 0x0400733A RID: 29498
			subscript,
			// Token: 0x0400733B RID: 29499
			topstatusbar,
			// Token: 0x0400733C RID: 29500
			bottomstatusbar,
			// Token: 0x0400733D RID: 29501
			xsmall,
			// Token: 0x0400733E RID: 29502
			small,
			// Token: 0x0400733F RID: 29503
			unset
		}
	}
}
