using System;
using System.Collections.Generic;
using System.Xml;
using ConsoleLib.Console;
using Qud.UI;
using XRL.UI;
using XRL.World;

namespace XRL.Help
{
	// Token: 0x0200054A RID: 1354
	[HasModSensitiveStaticCache]
	public class XRLManual
	{
		// Token: 0x060039AF RID: 14767 RVA: 0x0011D368 File Offset: 0x0011B568
		public XRLManual(TextConsole _Console)
		{
			this.Console = _Console;
			this.Buffer = ScreenBuffer.GetScrapBuffer1(false);
			this.Pages.Clear();
			foreach (XmlDataHelper xmlDataHelper in DataManager.YieldXMLStreamsWithRoot("help", true, true))
			{
				while (xmlDataHelper.Read())
				{
					if (xmlDataHelper.Name == "topic" && xmlDataHelper.NodeType != XmlNodeType.EndElement)
					{
						string text = xmlDataHelper["name"];
						XRLManualPage xrlmanualPage = new XRLManualPage(xmlDataHelper.ReadString());
						xrlmanualPage.Topic = text;
						this.Page.Add(xrlmanualPage);
						this.Pages.Add(text, xrlmanualPage);
					}
				}
			}
		}

		// Token: 0x060039B0 RID: 14768 RVA: 0x0011D44C File Offset: 0x0011B64C
		private void RenderPage(string PageName, int StartingLine)
		{
			int num = 1;
			if (StartingLine > this.Pages[PageName].Lines.Count - 23)
			{
				StartingLine = this.Pages[PageName].Lines.Count - 23;
			}
			if (StartingLine <= 0)
			{
				StartingLine = 0;
			}
			int num2 = StartingLine;
			while (num2 < this.Pages[PageName].Lines.Count && num < 24)
			{
				this.Buffer.Goto(2, num);
				this.Buffer.Write(this.Pages[PageName].LinesStripped[num2], true, false, false, null, -1);
				num++;
				num2++;
			}
			this.Buffer.SingleBox(0, 0, 79, 24, ColorUtility.MakeColor(TextColor.Grey, TextColor.Black));
			this.Buffer.Goto(2, 0);
			this.Buffer.Write("[ &W" + PageName + "&y ]", true, false, false, null, -1);
			this.Buffer.Goto(45, 24);
			this.Buffer.Write(" [&W5&y or &WEnter&y] Back to Index ", true, false, false, null, -1);
			if (this.Pages[PageName].Lines.Count > 23)
			{
				for (int i = 1; i < 24; i++)
				{
					this.Buffer.Goto(79, i);
					this.Buffer.Write(177, ColorUtility.Bright(0), 0);
				}
				int num3 = (int)Math.Ceiling((double)this.Pages[PageName].Lines.Count / 23.0);
				int num4 = (int)Math.Ceiling((double)(this.Pages[PageName].Lines.Count + 23) / 23.0);
				if (num4 <= 0)
				{
					num4 = 1;
				}
				int num5 = 23 / num4;
				if (num5 <= 0)
				{
					num5 = 1;
				}
				int num6 = (int)((double)(23 - num5) * ((double)StartingLine / (double)(this.Pages[PageName].Lines.Count - 23)));
				num6++;
				for (int j = num6; j < num6 + num5; j++)
				{
					this.Buffer.Goto(79, j);
					this.Buffer.Write(219, ColorUtility.Bright(7), 0);
				}
			}
		}

		// Token: 0x060039B1 RID: 14769 RVA: 0x0011D688 File Offset: 0x0011B888
		private void RenderIndex(int ScrollPosition)
		{
			int num = ScrollPosition / 21 * 21;
			if (num > this.Pages.Keys.Count - 21)
			{
				num = this.Pages.Keys.Count - 21;
			}
			if (num <= 0)
			{
				num = 0;
			}
			List<string> list = new List<string>();
			foreach (string text in this.Pages.Keys)
			{
				list.Add(text);
			}
			int num2 = 2;
			int num3 = num;
			while (num3 < this.Pages.Keys.Count && num2 < 23)
			{
				this.Buffer.Goto(2, num2);
				if (num3 == ScrollPosition)
				{
					this.Buffer.Write("&W" + list[num3] + "&k", true, false, false, null, -1);
				}
				else
				{
					this.Buffer.Write(list[num3], true, false, false, null, -1);
				}
				num2++;
				num3++;
			}
			this.Buffer.SingleBox(0, 0, 79, 24, ColorUtility.MakeColor(TextColor.Grey, TextColor.Black));
			this.Buffer.Goto(2, 0);
			this.Buffer.Write("[ &WHelp Index&y ]", true, false, false, null, -1);
			this.Buffer.Goto(45, 24);
			if (ControlManager.activeControllerType == ControlManager.InputDeviceType.Gamepad)
			{
				this.Buffer.Write(" [&W" + ControlManager.getCommandInputDescription("Accept", false, false, ControlManager.InputDeviceType.Unknown, false) + "&y] Select Topic ", true, false, false, null, -1);
			}
			else
			{
				this.Buffer.Write(" [&W>&y or &WEnter&y] Select Topic ", true, false, false, null, -1);
			}
			this.Buffer.Goto(15, 24);
			if (ControlManager.activeControllerType == ControlManager.InputDeviceType.Gamepad)
			{
				this.Buffer.Write(" [&W" + ControlManager.getCommandInputDescription("Cancel", false, false, ControlManager.InputDeviceType.Unknown, false) + "&y] Exit Help ", true, false, false, null, -1);
			}
			else
			{
				this.Buffer.Write(" [&W5&y] Exit Help ", true, false, false, null, -1);
			}
			if (list.Count > 23)
			{
				for (int i = 1; i < 24; i++)
				{
					this.Buffer.Goto(79, i);
					this.Buffer.Write(177, ColorUtility.Bright(0), 0);
				}
				int num4 = (int)Math.Ceiling((double)this.Pages.Keys.Count / 23.0);
				int num5 = (int)Math.Ceiling((double)(this.Pages.Keys.Count + 23) / 23.0);
				if (num5 <= 0)
				{
					num5 = 1;
				}
				int num6 = 23 / num5;
				if (num6 <= 0)
				{
					num6 = 1;
				}
				int num7 = (int)((double)(23 - num6) * ((double)num / (double)(this.Pages.Keys.Count - 23)));
				num7++;
				for (int j = num7; j < num7 + num6; j++)
				{
					this.Buffer.Goto(79, j);
					this.Buffer.Write(219, ColorUtility.Bright(7), 0);
				}
			}
		}

		// Token: 0x060039B2 RID: 14770 RVA: 0x0011D9A4 File Offset: 0x0011BBA4
		public void ShowHelp(string DefaultPage)
		{
			if (Options.ModernUI)
			{
				try
				{
					GameManager.Instance.PushGameView("ModernHelp", true);
					bool result = SingletonWindowBase<HelpScreen>.instance.HelpMenu(DefaultPage).Result;
				}
				catch (Exception ex)
				{
					MetricsManager.LogError("Error showing modern help menu", ex);
				}
				finally
				{
					GameManager.Instance.PopGameView(false);
				}
				return;
			}
			GameManager.Instance.PushGameView("Help", true);
			this.CurrentPage = DefaultPage;
			this.ScrollPosition = 0;
			for (;;)
			{
				Event.ResetPool(true);
				this.Buffer.Clear();
				if (this.CurrentPage != "")
				{
					this.RenderPage(this.CurrentPage, this.ScrollPosition);
				}
				else
				{
					this.RenderIndex(this.IndexPosition);
				}
				this.Console.DrawBuffer(this.Buffer, null, false);
				Keys keys = Keyboard.getvk(true, false, true);
				if (this.CurrentPage == "")
				{
					if (keys == Keys.NumPad2)
					{
						this.IndexPosition++;
					}
					if (keys == Keys.Next || keys == Keys.Next || keys == Keys.NumPad3 || Keyboard.RawCode == Keys.Next || Keyboard.RawCode == Keys.Next)
					{
						this.IndexPosition += 23;
					}
					if (keys == Keys.NumPad8)
					{
						this.IndexPosition--;
					}
					if (keys == Keys.Prior || keys == Keys.Back || keys == Keys.NumPad9 || Keyboard.RawCode == Keys.Back || Keyboard.RawCode == Keys.Prior)
					{
						this.IndexPosition -= 23;
					}
					if (keys == Keys.End)
					{
						this.IndexPosition = this.Pages.Keys.Count - 1;
					}
					if (keys == Keys.Home)
					{
						this.IndexPosition = 0;
					}
				}
				else
				{
					if (keys == Keys.NumPad2)
					{
						this.ScrollPosition++;
					}
					if (keys == Keys.Next || keys == Keys.Next || keys == Keys.NumPad3 || Keyboard.RawCode == Keys.Next || Keyboard.RawCode == Keys.Next)
					{
						this.ScrollPosition += 23;
					}
					if (keys == Keys.NumPad8)
					{
						this.ScrollPosition--;
					}
					if (keys == Keys.Prior || keys == Keys.Back || keys == Keys.NumPad9 || Keyboard.RawCode == Keys.Back || Keyboard.RawCode == Keys.Prior)
					{
						this.ScrollPosition -= 23;
					}
					if (keys == Keys.End)
					{
						this.ScrollPosition = this.Pages[this.CurrentPage].Lines.Count - 23;
					}
					if (keys == Keys.Home)
					{
						this.ScrollPosition = 0;
					}
				}
				if (this.CurrentPage == "" && this.IndexPosition >= this.Pages.Keys.Count)
				{
					this.IndexPosition = this.Pages.Keys.Count - 1;
				}
				if (this.CurrentPage != "" && this.ScrollPosition > this.Pages[this.CurrentPage].Lines.Count - 23)
				{
					this.ScrollPosition = this.Pages[this.CurrentPage].Lines.Count - 23;
				}
				if (this.IndexPosition < 0)
				{
					this.IndexPosition = 0;
				}
				if (this.ScrollPosition < 0)
				{
					this.ScrollPosition = 0;
				}
				if ((keys == Keys.Escape || keys == Keys.NumPad5 || (keys == Keys.MouseEvent && Keyboard.CurrentMouseEvent.Event == "RightClick")) && this.CurrentPage == "")
				{
					break;
				}
				if ((keys == Keys.Escape || keys == Keys.NumPad5 || keys == Keys.Escape || keys == Keys.Enter || (keys == Keys.MouseEvent && Keyboard.CurrentMouseEvent.Event == "RightClick")) && this.CurrentPage != "")
				{
					this.CurrentPage = "";
				}
				else if ((keys == Keys.OemPeriod || keys == Keys.Enter || keys == Keys.Space) && this.CurrentPage == "")
				{
					List<string> list = new List<string>();
					foreach (string text in this.Pages.Keys)
					{
						list.Add(text);
					}
					this.CurrentPage = list[this.IndexPosition];
					this.ScrollPosition = 0;
				}
			}
			GameManager.Instance.PopGameView(false);
		}

		// Token: 0x04001D0C RID: 7436
		private string CurrentPage;

		// Token: 0x04001D0D RID: 7437
		private int ScrollPosition;

		// Token: 0x04001D0E RID: 7438
		private int IndexPosition;

		// Token: 0x04001D0F RID: 7439
		private TextConsole Console;

		// Token: 0x04001D10 RID: 7440
		private ScreenBuffer Buffer;

		// Token: 0x04001D11 RID: 7441
		public List<XRLManualPage> Page = new List<XRLManualPage>();

		// Token: 0x04001D12 RID: 7442
		public Dictionary<string, XRLManualPage> Pages = new Dictionary<string, XRLManualPage>();
	}
}
