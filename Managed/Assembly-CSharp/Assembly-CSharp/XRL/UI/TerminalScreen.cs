using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConsoleLib.Console;
using XRL.Language;

namespace XRL.UI
{
	// Token: 0x02001482 RID: 5250
	public class TerminalScreen
	{
		// Token: 0x17000C09 RID: 3081
		// (get) Token: 0x0600CEB0 RID: 52912 RVA: 0x004B3662 File Offset: 0x004B1862
		public string RenderedTextForModernUI
		{
			get
			{
				return StringFormat.ClipText(this.MainText, 67, true, true, false);
			}
		}

		// Token: 0x17000C0A RID: 3082
		// (get) Token: 0x0600CEB1 RID: 52913 RVA: 0x004B3674 File Offset: 0x004B1874
		public int LicensesRemaining
		{
			get
			{
				return this.Terminal.LicensesRemaining;
			}
		}

		// Token: 0x17000C0B RID: 3083
		// (get) Token: 0x0600CEB2 RID: 52914 RVA: 0x004B3681 File Offset: 0x004B1881
		public CyberneticsTerminal Terminal
		{
			get
			{
				return CyberneticsTerminal.Instance;
			}
		}

		// Token: 0x0600CEB3 RID: 52915 RVA: 0x004B3688 File Offset: 0x004B1888
		public virtual void Back()
		{
			this.Terminal.CurrentScreen = null;
		}

		// Token: 0x0600CEB4 RID: 52916 RVA: 0x004B3696 File Offset: 0x004B1896
		public virtual void TextComplete()
		{
		}

		// Token: 0x0600CEB5 RID: 52917 RVA: 0x004B3698 File Offset: 0x004B1898
		public virtual void Activate()
		{
		}

		// Token: 0x0600CEB6 RID: 52918 RVA: 0x004B369A File Offset: 0x004B189A
		protected virtual void OnUpdate()
		{
		}

		// Token: 0x0600CEB7 RID: 52919 RVA: 0x004B369C File Offset: 0x004B189C
		protected virtual void ClearOptions()
		{
			this.Options.Clear();
			this.HackOption = -1;
		}

		// Token: 0x0600CEB8 RID: 52920 RVA: 0x004B36B0 File Offset: 0x004B18B0
		public void Update()
		{
			this.OnUpdate();
			if (this.Terminal.HackActive)
			{
				if (this.MainText != null && this.MainText != this.HackText && this.Terminal.LowLevelHack)
				{
					this.MainText = TextFilters.Leet(this.MainText, true);
					this.HackText = this.MainText;
				}
				bool flag = false;
				int i = 0;
				int count = this.Options.Count;
				while (i < count)
				{
					if (i != this.HackOption && this.Options[i] != null && (this.HackOptions == null || i >= this.HackOptions.Count || this.HackOptions[i] != this.Options[i]))
					{
						if (this.Terminal.LowLevelHack)
						{
							this.Options[i] = TextFilters.Leet(this.Options[i], true);
						}
						flag = true;
					}
					i++;
				}
				if (flag)
				{
					this.HackOptions = new List<string>(this.Options);
				}
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(StringFormat.ClipText(this.MainText, 67, true, true, false)).Append("\n\n");
			int num = stringBuilder.ToString().Split('\n', StringSplitOptions.None).Length;
			StringBuilder stringBuilder2 = new StringBuilder();
			int num2 = 65;
			for (int j = 0; j < this.Options.Count; j++)
			{
				if (this.Terminal.Selected == j)
				{
					stringBuilder.Append('>');
				}
				else
				{
					stringBuilder.Append(' ');
				}
				stringBuilder2.Clear();
				if (j == this.HackOption)
				{
					stringBuilder2.Append("{{R|CTRL-ENTER. ").Append(StringFormat.ClipText(this.Options[j], 60, true, true, false)).Append("}}");
					Markup.Transform(stringBuilder2, false);
				}
				else
				{
					stringBuilder2.Append((char)num2++).Append(". ").Append(StringFormat.ClipText("{{|" + this.Options[j] + "}}", 60, true, true, false));
				}
				stringBuilder2.Append('\n');
				stringBuilder.Append(stringBuilder2);
				this.OptionLines.Add(num);
				num += stringBuilder2.ToString().Count<char>((char ch) => ch == '\n');
			}
			this.RenderedText = stringBuilder.ToString();
		}

		// Token: 0x0600CEB9 RID: 52921 RVA: 0x004B393E File Offset: 0x004B1B3E
		public virtual void BeforeRender(ScreenBuffer buffer, ref string footerText)
		{
		}

		// Token: 0x04005045 RID: 20549
		public string RenderedText;

		// Token: 0x04005046 RID: 20550
		public string MainText;

		// Token: 0x04005047 RID: 20551
		public List<string> Options = new List<string>();

		// Token: 0x04005048 RID: 20552
		public List<int> OptionLines = new List<int>();

		// Token: 0x04005049 RID: 20553
		public int HackOption = -1;

		// Token: 0x0400504A RID: 20554
		public string HackText;

		// Token: 0x0400504B RID: 20555
		public List<string> HackOptions;
	}
}
