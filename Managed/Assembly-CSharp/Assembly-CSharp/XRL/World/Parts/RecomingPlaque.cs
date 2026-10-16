using System;
using System.Collections.Generic;
using ConsoleLib.Console;
using XRL.UI;

namespace XRL.World.Parts
{
	// Token: 0x02000FB0 RID: 4016
	[Serializable]
	public class RecomingPlaque : IPart
	{
		// Token: 0x06009FB4 RID: 40884 RVA: 0x003A0AF5 File Offset: 0x0039ECF5
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x06009FB5 RID: 40885 RVA: 0x003A0AF8 File Offset: 0x0039ECF8
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == CanSmartUseEvent.ID || ID == CommandSmartUseEvent.ID || ID == GetShortDescriptionEvent.ID;
		}

		// Token: 0x06009FB6 RID: 40886 RVA: 0x003A0B1E File Offset: 0x0039ED1E
		public override bool HandleEvent(CanSmartUseEvent E)
		{
			return !E.Actor.IsPlayer() && base.HandleEvent(E);
		}

		// Token: 0x06009FB7 RID: 40887 RVA: 0x003A0B38 File Offset: 0x0039ED38
		public override bool HandleEvent(CommandSmartUseEvent E)
		{
			if (E.Actor.IsPlayer())
			{
				Cell currentCell = this.ParentObject.CurrentCell;
				if (currentCell != null)
				{
					Look.ShowLooker(0, currentCell.X, currentCell.Y);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009FB8 RID: 40888 RVA: 0x003A0B7C File Offset: 0x0039ED7C
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			if (this.NeedsGeneration)
			{
				this.GeneratePlaque();
			}
			E.Prefix.Append(this.Prefix);
			E.Base.Clear();
			E.Base.Append(this.Inscription);
			E.Postfix.Append(this.Postfix);
			return base.HandleEvent(E);
		}

		// Token: 0x06009FB9 RID: 40889 RVA: 0x003A0BE0 File Offset: 0x0039EDE0
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x06009FBA RID: 40890 RVA: 0x003A0BE4 File Offset: 0x0039EDE4
		public void GeneratePlaque()
		{
			List<string> list = new List<string>(16);
			int num = 28;
			list.Add("");
			list.Add("");
			list.Add("");
			foreach (string text in StringFormat.ClipText("\"Unwrap thyself of thy grave clothes, and go.\"", num, false, true, false).Split('\n', StringSplitOptions.None))
			{
				list.Add(text);
			}
			list.Add("");
			list.Add("");
			list.Add("");
			string text2 = "^k&m";
			string text3 = "&M^m";
			string text4 = "m";
			this.Inscription = Event.NewStringBuilder(null).Append('ÿ').Append('ÿ')
				.Append('ÿ')
				.Append(text3 + "\a" + text2)
				.Append('Ä', 31)
				.Append(text3 + "\a^k&y")
				.ToString();
			for (int j = 0; j < list.Count; j++)
			{
				this.Inscription += "\nÿÿÿ";
				this.Inscription = this.Inscription + text2 + "³^k&y";
				int num2 = ColorUtility.LengthExceptFormatting(list[j]);
				this.Inscription += list[j].PadLeft(16 + num2 / 2, 'ÿ').PadRight(31, 'ÿ');
				this.Inscription = this.Inscription + text2 + "³^k&y";
			}
			this.Inscription += Event.NewStringBuilder(null).Append("\n").Append('ÿ')
				.Append('ÿ')
				.Append('ÿ')
				.Append(text3 + "\a" + text2)
				.Append("&" + text4 + "^k")
				.Append('Ä', 31)
				.Append(text3 + "\a^k&y")
				.ToString();
			this.NeedsGeneration = false;
		}

		// Token: 0x04004249 RID: 16969
		private const string RecomingWords = "\"Unwrap thyself of thy grave clothes, and go.\"";

		// Token: 0x0400424A RID: 16970
		public string Inscription = "";

		// Token: 0x0400424B RID: 16971
		public string Prefix = "";

		// Token: 0x0400424C RID: 16972
		public string Postfix = "";

		// Token: 0x0400424D RID: 16973
		public bool NeedsGeneration = true;
	}
}
