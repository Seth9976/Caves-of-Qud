using System;
using System.Collections.Generic;
using System.Text;
using ConsoleLib.Console;
using Qud.UI;
using UnityEngine;
using XRL.Core;
using XRL.UI;
using XRL.World;

namespace XRL.Messages
{
	// Token: 0x02000575 RID: 1397
	[Serializable]
	public class MessageQueue : IComposite
	{
		// Token: 0x06003C59 RID: 15449 RVA: 0x001270EC File Offset: 0x001252EC
		public void Show()
		{
			if (Options.ModernUI)
			{
				ScreenReturn result = StatusScreensScreen.show(7, The.Game.Player.Body).Result;
				return;
			}
			BookUI.ShowBook(this.GetLines(0, this.Messages.Count).ToString(), "Message Log", "Sounds/Interact/sfx_interact_book_read", null, null);
		}

		// Token: 0x06003C5A RID: 15450 RVA: 0x00127144 File Offset: 0x00125344
		public void EndPlayerTurn()
		{
		}

		// Token: 0x06003C5B RID: 15451 RVA: 0x00127148 File Offset: 0x00125348
		public void BeginPlayerTurn()
		{
			this.LastMessage = this.PreviousMessage;
			this.PreviousMessage = this.Messages.Count;
			if (this.Messages.Count > 2000)
			{
				this.Messages.RemoveRange(0, 100);
				this.LastMessage -= 100;
				this.PreviousMessage -= 100;
			}
		}

		// Token: 0x1700062B RID: 1579
		// (get) Token: 0x06003C5C RID: 15452 RVA: 0x001271B0 File Offset: 0x001253B0
		public static bool Enabled
		{
			get
			{
				return !MessageQueue.Suppress && XRLCore.Core != null && XRLCore.Core.Game != null && XRLCore.Core.Game.Player != null && XRLCore.Core.Game.Player.Messages != null;
			}
		}

		// Token: 0x06003C5D RID: 15453 RVA: 0x0012720C File Offset: 0x0012540C
		public static void AddPlayerMessage(string Message, string Color = null, bool Capitalize = true)
		{
			try
			{
				if (MessageQueue.Enabled && Message != null)
				{
					if (Capitalize)
					{
						Message = ConsoleLib.Console.ColorUtility.CapitalizeExceptFormatting(Message);
					}
					if (!Color.IsNullOrEmpty())
					{
						StringBuilder stringBuilder = XRL.World.Event.NewStringBuilder(null);
						stringBuilder.Append("{{").Append(Color).Append('|')
							.Append(Message)
							.Append("}}");
						Message = stringBuilder.ToString();
					}
					XRLCore.Core.Game.Player.Messages.Add(Message);
				}
			}
			catch (Exception ex)
			{
				Debug.LogException(ex);
			}
		}

		// Token: 0x06003C5E RID: 15454 RVA: 0x001272A0 File Offset: 0x001254A0
		public static void AddPlayerMessage(string Message, char Color, bool Capitalize = true)
		{
			MessageQueue.AddPlayerMessage(Message, (Color == ' ') ? null : (Color.ToString() ?? ""), Capitalize);
		}

		// Token: 0x06003C5F RID: 15455 RVA: 0x001272C1 File Offset: 0x001254C1
		public void Add(string Message)
		{
			if (MessageQueue.Suppress)
			{
				return;
			}
			this.Cache_0_12Valid = false;
			Message = Markup.Transform(Message, false);
			if (Message != "!clear")
			{
				this.Messages.Add(Message);
			}
			XRLCore.CallNewMessageLogEntryCallbacks(Message);
		}

		// Token: 0x06003C60 RID: 15456 RVA: 0x00127300 File Offset: 0x00125500
		public List<string> GetLinesList(int StartingLine, int nLines)
		{
			List<string> list = new List<string>();
			StringBuilder stringBuilder = new StringBuilder();
			int num = 1;
			if (StartingLine < 0)
			{
				StartingLine = 0;
			}
			int num2 = this.Messages.Count - 1 - StartingLine;
			while (num2 >= 0 && num < nLines)
			{
				if (num2 < this.LastMessage)
				{
					stringBuilder.Append("&K^k");
				}
				else
				{
					stringBuilder.Append("&k^Y");
				}
				if (this.Messages[num2].Length > 0 && this.Messages[num2][0] == '#')
				{
					stringBuilder.Append("#^k &y").Append(this.Messages[num2].Substring(1)).Append("\n");
				}
				else
				{
					stringBuilder.Append(">^k &y");
					stringBuilder.Append(this.Messages[num2]);
					list.Add(stringBuilder.ToString());
					stringBuilder.Length = 0;
				}
				num2--;
				num++;
			}
			return list;
		}

		// Token: 0x06003C61 RID: 15457 RVA: 0x001273FC File Offset: 0x001255FC
		public StringBuilder GetLines(int StartingLine, int nLines)
		{
			if (this.Cache_0_12Valid && StartingLine == 0 && nLines == 12)
			{
				return this.Cache_0_12;
			}
			StringBuilder stringBuilder = XRL.World.Event.NewStringBuilder(null);
			int num = 1;
			if (StartingLine < 0)
			{
				StartingLine = 0;
			}
			int num2 = this.Messages.Count - 1 - StartingLine;
			while (num2 >= 0 && num < nLines)
			{
				if (num2 < this.LastMessage)
				{
					stringBuilder.Append("&K^k");
				}
				else
				{
					stringBuilder.Append("&k^Y");
				}
				if (this.Messages[num2].Length > 0 && this.Messages[num2][0] == '#')
				{
					stringBuilder.Append("#^k &y").Append(this.Messages[num2], 1, this.Messages[num2].Length - 1).Append('\n');
				}
				else
				{
					stringBuilder.Append(">^k &y").Append(this.Messages[num2]).Append('\n');
				}
				num2--;
				num++;
			}
			if (StartingLine == 0 && nLines == 12)
			{
				this.Cache_0_12Valid = true;
				this.Cache_0_12 = stringBuilder;
				return this.Cache_0_12;
			}
			return stringBuilder;
		}

		// Token: 0x1700062C RID: 1580
		// (get) Token: 0x06003C62 RID: 15458 RVA: 0x00127521 File Offset: 0x00125721
		public string LastLine
		{
			get
			{
				if (this.Messages.Count <= 0)
				{
					return null;
				}
				return this.Messages[this.Messages.Count - 1];
			}
		}

		// Token: 0x06003C63 RID: 15459 RVA: 0x0012754B File Offset: 0x0012574B
		public override string ToString()
		{
			return this.GetLines(0, this.Messages.Count).ToString();
		}

		// Token: 0x04001E1D RID: 7709
		public static bool Suppress = false;

		// Token: 0x04001E1E RID: 7710
		public List<string> Messages = new List<string>(8192);

		// Token: 0x04001E1F RID: 7711
		public int PreviousMessage;

		// Token: 0x04001E20 RID: 7712
		public int LastMessage;

		// Token: 0x04001E21 RID: 7713
		public int LastTurnMessages = -1;

		// Token: 0x04001E22 RID: 7714
		public bool Terse;

		// Token: 0x04001E23 RID: 7715
		[NonSerialized]
		private static char[] splitterNewline = new char[] { '\n' };

		// Token: 0x04001E24 RID: 7716
		[NonSerialized]
		public static Queue<string> UnityMessages = new Queue<string>(32);

		// Token: 0x04001E25 RID: 7717
		public bool Cache_0_12Valid;

		// Token: 0x04001E26 RID: 7718
		private StringBuilder Cache_0_12 = new StringBuilder(1024);
	}
}
