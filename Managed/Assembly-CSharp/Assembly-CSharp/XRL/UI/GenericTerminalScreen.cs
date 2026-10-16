using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConsoleLib.Console;

namespace XRL.UI
{
	// Token: 0x020014D9 RID: 5337
	public class GenericTerminalScreen
	{
		// Token: 0x17000C5B RID: 3163
		// (get) Token: 0x0600D0F2 RID: 53490 RVA: 0x004D9A61 File Offset: 0x004D7C61
		public string RenderedTextForModernUI
		{
			get
			{
				return StringFormat.ClipText(this.mainText, 67, true, true, false);
			}
		}

		// Token: 0x17000C5C RID: 3164
		// (get) Token: 0x0600D0F3 RID: 53491 RVA: 0x004D9A73 File Offset: 0x004D7C73
		public GenericTerminal terminal
		{
			get
			{
				return GenericTerminal.instance;
			}
		}

		// Token: 0x0600D0F4 RID: 53492 RVA: 0x004D9A7A File Offset: 0x004D7C7A
		public virtual void Back()
		{
			this.terminal.currentScreen = null;
		}

		// Token: 0x0600D0F5 RID: 53493 RVA: 0x004D9A88 File Offset: 0x004D7C88
		public virtual void TextComplete()
		{
		}

		// Token: 0x0600D0F6 RID: 53494 RVA: 0x004D9A8A File Offset: 0x004D7C8A
		public virtual void Activate()
		{
		}

		// Token: 0x0600D0F7 RID: 53495 RVA: 0x004D9A8C File Offset: 0x004D7C8C
		protected virtual void OnUpdate()
		{
		}

		// Token: 0x0600D0F8 RID: 53496 RVA: 0x004D9A90 File Offset: 0x004D7C90
		public void Update()
		{
			this.OnUpdate();
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(StringFormat.ClipText("{{|" + this.mainText + "}}", 67, true, true, false)).Append("\n\n");
			int num = stringBuilder.ToString().Split('\n', StringSplitOptions.None).Length;
			StringBuilder stringBuilder2 = new StringBuilder();
			for (int i = 0; i < this.Options.Count; i++)
			{
				if (this.terminal.nSelected == i)
				{
					stringBuilder.Append('>');
				}
				else
				{
					stringBuilder.Append(' ');
				}
				stringBuilder2.Length = 0;
				stringBuilder2.Append((char)(65 + i)).Append(". ").Append(StringFormat.ClipText("{{|" + this.Options[i] + "}}", 60, true, true, false))
					.Append("\n");
				stringBuilder.Append(stringBuilder2);
				this.OptionLines.Add(num);
				num += stringBuilder2.ToString().Count<char>((char ch) => ch == '\n');
			}
			this.renderedText = stringBuilder.ToString();
		}

		// Token: 0x0600D0F9 RID: 53497 RVA: 0x004D9BCC File Offset: 0x004D7DCC
		public virtual void BeforeRender(ScreenBuffer buffer, ref string FooterText)
		{
			FooterText = "";
		}

		// Token: 0x0400526E RID: 21102
		public string renderedText;

		// Token: 0x0400526F RID: 21103
		public string mainText;

		// Token: 0x04005270 RID: 21104
		public List<string> Options = new List<string>();

		// Token: 0x04005271 RID: 21105
		public List<int> OptionLines = new List<int>();
	}
}
