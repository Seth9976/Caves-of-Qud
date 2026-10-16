using System;
using System.Collections.Generic;
using System.Text;
using HistoryKit;
using Qud.API;
using XRL.Rules;
using XRL.UI;

namespace XRL.World.Parts
{
	// Token: 0x02000FAD RID: 4013
	[Serializable]
	public class RachelsTombstone : IPart
	{
		// Token: 0x06009FA0 RID: 40864 RVA: 0x003A00B7 File Offset: 0x0039E2B7
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x06009FA1 RID: 40865 RVA: 0x003A00BA File Offset: 0x0039E2BA
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == CanSmartUseEvent.ID || ID == CommandSmartUseEvent.ID || ID == GetShortDescriptionEvent.ID || ID == ObjectCreatedEvent.ID;
		}

		// Token: 0x06009FA2 RID: 40866 RVA: 0x003A00E8 File Offset: 0x0039E2E8
		public override bool HandleEvent(CanSmartUseEvent E)
		{
			return !E.Actor.IsPlayer() && base.HandleEvent(E);
		}

		// Token: 0x06009FA3 RID: 40867 RVA: 0x003A0100 File Offset: 0x0039E300
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

		// Token: 0x06009FA4 RID: 40868 RVA: 0x003A0144 File Offset: 0x0039E344
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			if (this.NeedsGeneration)
			{
				this.GenerateTombstone();
			}
			E.Prefix.Append(this.Prefix);
			E.Base.Append(this.Inscription);
			E.Postfix.Append(this.Postfix);
			return base.HandleEvent(E);
		}

		// Token: 0x06009FA5 RID: 40869 RVA: 0x003A019C File Offset: 0x0039E39C
		public override bool HandleEvent(ObjectCreatedEvent E)
		{
			this.ParentObject.Render.Tile = "terrain/sw_tombstone_" + Stat.Random(1, 4).ToString() + ".bmp";
			return base.HandleEvent(E);
		}

		// Token: 0x06009FA6 RID: 40870 RVA: 0x003A01DE File Offset: 0x0039E3DE
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("AfterLookedAt");
			base.Register(Object, Registrar);
		}

		// Token: 0x06009FA7 RID: 40871 RVA: 0x003A01F3 File Offset: 0x0039E3F3
		public override bool FireEvent(Event E)
		{
			if (E.ID == "AfterLookedAt")
			{
				JournalAPI.RevealObservation("MarkOfDeathSecret", true);
				The.Game.FinishQuestStep("Tomb of the Eaters", "Recover the Mark of Death", -1, true, null);
			}
			return base.FireEvent(E);
		}

		// Token: 0x06009FA8 RID: 40872 RVA: 0x003A0234 File Offset: 0x0039E434
		public void GenerateTombstone()
		{
			this.name = "Rebekah";
			string stringGameState = The.Game.GetStringGameState("MarkOfDeath", "");
			string text = HistoricStringExpander.ExpandString("<spice.tombstones.intro.!random>", null);
			string text2 = "Succumbed to glotrot.";
			List<string> list = new List<string>();
			list.Add("");
			foreach (string text3 in StringFormat.ClipText(text, 28, false, true, false).Split('\n', StringSplitOptions.None))
			{
				list.Add(text3);
			}
			list.Add("");
			list.Add("");
			foreach (string text4 in StringFormat.ClipText(this.name, 28, false, true, false).Split('\n', StringSplitOptions.None))
			{
				list.Add(text4);
			}
			list.Add("");
			list.Add("");
			foreach (string text5 in StringFormat.ClipText(text2, 28, false, true, false).Split('\n', StringSplitOptions.None))
			{
				list.Add(text5);
			}
			list.Add("");
			foreach (string text6 in StringFormat.ClipText(stringGameState, 28, false, true, false).Split('\n', StringSplitOptions.None))
			{
				list.Add(text6);
			}
			list.Add("");
			for (int j = 0; j < list.Count; j++)
			{
				this.Inscription += "\nÿÿÿ";
				if (j % 2 == 0)
				{
					this.Inscription += "º";
				}
				else
				{
					this.Inscription += "¶";
				}
				this.Inscription += list[j].PadLeft(17 + (list[j].Length / 2 - 1), 'ÿ').PadRight(31, 'ÿ');
				if (j % 2 == 0)
				{
					this.Inscription += "º";
				}
				else
				{
					this.Inscription += "Ç";
				}
			}
			this.NeedsGeneration = false;
			StringBuilder stringBuilder = Event.NewStringBuilder(null);
			stringBuilder.Append('ÿ');
			stringBuilder.Append('ÿ');
			stringBuilder.Append('ÿ');
			stringBuilder.Append('É');
			stringBuilder.Append('Í', 31);
			stringBuilder.Append('»');
			this.Prefix = stringBuilder.ToString();
			StringBuilder stringBuilder2 = new StringBuilder();
			stringBuilder2.Append('ÿ');
			stringBuilder2.Append('ÿ');
			stringBuilder2.Append('ÿ');
			stringBuilder2.Append('Ó');
			stringBuilder2.Append('Ä', 31);
			stringBuilder2.Append('½');
			this.Inscription += "\n";
			this.Inscription += stringBuilder2.ToString();
		}

		// Token: 0x0400423E RID: 16958
		public string Inscription = "";

		// Token: 0x0400423F RID: 16959
		public string Prefix = "";

		// Token: 0x04004240 RID: 16960
		public string Postfix = "";

		// Token: 0x04004241 RID: 16961
		public bool NeedsGeneration = true;

		// Token: 0x04004242 RID: 16962
		public string faction;

		// Token: 0x04004243 RID: 16963
		public string name;
	}
}
