using System;
using System.Collections.Generic;
using System.Text;
using XRL.Language;
using XRL.Rules;

namespace XRL.World.Parts
{
	// Token: 0x02000F5D RID: 3933
	[WantLoadBlueprint]
	[Serializable]
	public class MakersMark : IPart
	{
		// Token: 0x06009CCB RID: 40139 RVA: 0x0038D038 File Offset: 0x0038B238
		public static void LoadBlueprint(GameObjectBlueprint Blueprint)
		{
			string text;
			if (Blueprint.TryGetPartParameter<string>("MakersMark", "Mark", out text) && !text.IsNullOrEmpty())
			{
				MakersMark.RecordUsage(text);
			}
		}

		// Token: 0x06009CCC RID: 40140 RVA: 0x0038D068 File Offset: 0x0038B268
		public override bool SameAs(IPart p)
		{
			MakersMark makersMark = p as MakersMark;
			return !(makersMark.Mark != this.Mark) && !(makersMark.CrafterName != this.CrafterName) && !(makersMark.Color != this.Color) && base.SameAs(p);
		}

		// Token: 0x06009CCD RID: 40141 RVA: 0x0038D0C2 File Offset: 0x0038B2C2
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == SingletonEvent<GetDebugInternalsEvent>.ID || ID == PooledEvent<GetDisplayNameEvent>.ID || ID == GetShortDescriptionEvent.ID || ID == GetUnknownShortDescriptionEvent.ID;
		}

		// Token: 0x06009CCE RID: 40142 RVA: 0x0038D0F0 File Offset: 0x0038B2F0
		public override bool HandleEvent(GetDebugInternalsEvent E)
		{
			E.AddEntry(this, "Mark", this.Mark);
			E.AddEntry(this, "Mark.Length", this.Mark.Length);
			E.AddEntry(this, "CrafterName", this.CrafterName);
			E.AddEntry(this, "Color", this.Color);
			E.AddEntry(this, "MarkText", this.GetMarkText());
			return base.HandleEvent(E);
		}

		// Token: 0x06009CCF RID: 40143 RVA: 0x0038D163 File Offset: 0x0038B363
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			E.AddMark(this.GetMarkText(), 0);
			return base.HandleEvent(E);
		}

		// Token: 0x06009CD0 RID: 40144 RVA: 0x0038D179 File Offset: 0x0038B379
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			E.Postfix.Compound(this.GetDescText(), '\n');
			return base.HandleEvent(E);
		}

		// Token: 0x06009CD1 RID: 40145 RVA: 0x0038D196 File Offset: 0x0038B396
		public override bool HandleEvent(GetUnknownShortDescriptionEvent E)
		{
			E.Postfix.Compound(this.GetDescText(), '\n');
			return base.HandleEvent(E);
		}

		// Token: 0x06009CD2 RID: 40146 RVA: 0x0038D1B4 File Offset: 0x0038B3B4
		private string GetMarkText()
		{
			string text;
			if ((text = this.MarkText) == null)
			{
				text = (this.MarkText = this.BuildMarkText());
			}
			return text;
		}

		// Token: 0x06009CD3 RID: 40147 RVA: 0x0038D1DC File Offset: 0x0038B3DC
		private string BuildMarkText()
		{
			if (this.Mark.Length <= 1)
			{
				return this.Mark.Color(this.Color);
			}
			List<string> list = this.Color.CachedDoubleSemicolonExpansion();
			if (list.Count != this.Mark.Length)
			{
				return this.Mark;
			}
			StringBuilder stringBuilder = Event.NewStringBuilder(null);
			int i = 0;
			int length = this.Mark.Length;
			while (i < length)
			{
				stringBuilder.Append("{{").Append(list[i]).Append('|')
					.Append(this.Mark[i])
					.Append("}}");
				i++;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06009CD4 RID: 40148 RVA: 0x0038D290 File Offset: 0x0038B490
		private string GetDescText()
		{
			StringBuilder stringBuilder = Event.NewStringBuilder(null);
			stringBuilder.Append(this.GetMarkText()).Append("{{C|: ").Append(MakersMark.GetDescriptionFor(this.ParentObject, this.CrafterName, this.Mark))
				.Append("}}");
			return stringBuilder.ToString();
		}

		// Token: 0x06009CD5 RID: 40149 RVA: 0x0038D2E8 File Offset: 0x0038B4E8
		public static string GetDescriptionFor(GameObject Object, string CrafterName, string Mark = null)
		{
			string text = CrafterName ?? "a crafter";
			if (text.Contains(";;"))
			{
				text = Grammar.MakeAndList(text.CachedDoubleSemicolonExpansion(), true);
			}
			return string.Concat(new string[]
			{
				Object.IndicativeProximal,
				" ",
				Object.IsPlural ? Grammar.Pluralize(Object.GetDescriptiveCategory()) : Object.GetDescriptiveCategory(),
				Object.GetVerb("bear", true, false, false, true, false),
				" the ",
				(Mark != null && Mark.Length > 1) ? "marks" : "mark",
				" of ",
				text,
				"."
			});
		}

		// Token: 0x06009CD6 RID: 40150 RVA: 0x0038D39F File Offset: 0x0038B59F
		public static string GetDescriptionFor(GameObject Object, GameObject Actor)
		{
			return MakersMark.GetDescriptionFor(Object, MakersMark.NameForMark(Actor), null);
		}

		// Token: 0x06009CD7 RID: 40151 RVA: 0x0038D3B0 File Offset: 0x0038B5B0
		public static string NameForMark(GameObject Object)
		{
			if (Object == null)
			{
				return null;
			}
			return Object.GetReferenceDisplayName(int.MaxValue, null, "MakersMark", false, true, false, false, false, false, false, false, null, false);
		}

		// Token: 0x06009CD8 RID: 40152 RVA: 0x0038D3E0 File Offset: 0x0038B5E0
		public bool AddCrafter(GameObject Crafter, string CrafterMark, string MarkColor)
		{
			if (!GameObject.Validate(ref Crafter) || CrafterMark.IsNullOrEmpty() || MarkColor.IsNullOrEmpty())
			{
				return false;
			}
			if (this.Mark.IsNullOrEmpty())
			{
				this.Mark = CrafterMark;
			}
			else
			{
				if (this.Mark.Contains(CrafterMark))
				{
					return false;
				}
				this.Mark += CrafterMark;
			}
			if (this.Color.IsNullOrEmpty())
			{
				this.Color = MarkColor;
			}
			else
			{
				this.Color = this.Color + ";;" + MarkColor;
			}
			string text = MakersMark.NameForMark(Crafter);
			if (this.CrafterName.IsNullOrEmpty())
			{
				this.CrafterName = text;
			}
			else
			{
				this.CrafterName = this.CrafterName + ";;" + text;
			}
			this.MarkText = null;
			return true;
		}

		// Token: 0x06009CD9 RID: 40153 RVA: 0x0038D4AC File Offset: 0x0038B6AC
		public static string Generate(bool RecordUse = true)
		{
			int num = 0;
			int num2;
			do
			{
				num2 = Stat.Random(1, 254);
			}
			while (num2 == 94 || num2 == 38 || num2 == 124 || num2 == 44 || num2 == 123 || num2 == 125 || num2 == 32 || num2 == 10 || char.IsLetter((char)num2) || char.IsDigit((char)num2) || (The.Game.GetBooleanGameState("MakersMarkUsed_" + ((char)num2).ToString(), false) && ++num < 32));
			string text = ((char)num2).ToString() ?? "";
			if (RecordUse)
			{
				MakersMark.RecordUsage(text);
			}
			return text;
		}

		// Token: 0x06009CDA RID: 40154 RVA: 0x0038D54C File Offset: 0x0038B74C
		public static List<string> GetUsable(bool SkipUsed = true, bool IncludeUsedIfNoneLeft = true)
		{
			List<string> list = new List<string>(256);
			for (;;)
			{
				for (int i = 1; i < 255; i++)
				{
					if (i != 94 && i != 38 && i != 124 && i != 44 && i != 123 && i != 125 && i != 32 && i != 10 && !char.IsLetter((char)i) && !char.IsDigit((char)i) && (!SkipUsed || !The.Game.GetBooleanGameState("MakersMarkUsed_" + ((char)i).ToString(), false)))
					{
						list.Add(((char)i).ToString() ?? "");
					}
				}
				if (!SkipUsed || !IncludeUsedIfNoneLeft || list.Count != 0)
				{
					break;
				}
				SkipUsed = false;
			}
			return list;
		}

		// Token: 0x06009CDB RID: 40155 RVA: 0x0038D604 File Offset: 0x0038B804
		public static void RecordUsage(string Mark)
		{
			The.Game.SetBooleanGameState("MakersMarkUsed_" + Mark, true);
		}

		// Token: 0x040040D4 RID: 16596
		public string Mark;

		// Token: 0x040040D5 RID: 16597
		public string CrafterName;

		// Token: 0x040040D6 RID: 16598
		public string Color;

		// Token: 0x040040D7 RID: 16599
		[NonSerialized]
		private string MarkText;
	}
}
