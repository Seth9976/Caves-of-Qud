using System;
using System.Collections.Generic;

namespace XRL.World.Parts
{
	// Token: 0x02000EFD RID: 3837
	[Serializable]
	public class SacralTracking : IPart
	{
		// Token: 0x06009928 RID: 39208 RVA: 0x00379830 File Offset: 0x00377A30
		public override void Write(GameObject Basis, SerializationWriter Writer)
		{
			Writer.WriteComposite<WorshipTracking>(this.WorshipTracking);
			Writer.WriteComposite<WorshipTracking>(this.BlasphemyTracking);
		}

		// Token: 0x06009929 RID: 39209 RVA: 0x0037984A File Offset: 0x00377A4A
		public override void Read(GameObject Basis, SerializationReader Reader)
		{
			this.WorshipTracking = Reader.ReadCompositeList<WorshipTracking>();
			this.BlasphemyTracking = Reader.ReadCompositeList<WorshipTracking>();
		}

		// Token: 0x0600992A RID: 39210 RVA: 0x00379864 File Offset: 0x00377A64
		public override bool SameAs(IPart Part)
		{
			return false;
		}

		// Token: 0x0600992B RID: 39211 RVA: 0x00379867 File Offset: 0x00377A67
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == BlasphemyPerformedEvent.ID || ID == WorshipPerformedEvent.ID;
		}

		// Token: 0x0600992C RID: 39212 RVA: 0x00379885 File Offset: 0x00377A85
		public override bool HandleEvent(WorshipPerformedEvent E)
		{
			this.Track(ref this.WorshipTracking, E.Being);
			return base.HandleEvent(E);
		}

		// Token: 0x0600992D RID: 39213 RVA: 0x003798A0 File Offset: 0x00377AA0
		public override bool HandleEvent(BlasphemyPerformedEvent E)
		{
			this.Track(ref this.BlasphemyTracking, E.Being);
			return base.HandleEvent(E);
		}

		// Token: 0x0600992E RID: 39214 RVA: 0x003798BC File Offset: 0x00377ABC
		private void Track(ref List<WorshipTracking> List, Worshippable Being)
		{
			if (Being == null)
			{
				return;
			}
			if (List == null)
			{
				List = new List<WorshipTracking>();
			}
			WorshipTracking worshipTracking = null;
			foreach (WorshipTracking worshipTracking2 in List)
			{
				if (worshipTracking2.Faction == Being.Faction && worshipTracking == null && worshipTracking2.Name == Being.Name)
				{
					worshipTracking = worshipTracking2;
				}
			}
			if (worshipTracking == null)
			{
				worshipTracking = new WorshipTracking();
				worshipTracking.Name = Being.Name;
				worshipTracking.Faction = Being.Faction;
				worshipTracking.First = The.CurrentTurn;
				List.Add(worshipTracking);
			}
			worshipTracking.Times++;
			worshipTracking.Last = The.CurrentTurn;
		}

		// Token: 0x0600992F RID: 39215 RVA: 0x00379990 File Offset: 0x00377B90
		public bool HasWorshipped(string Faction, int WithinTurns = 0)
		{
			foreach (WorshipTracking worshipTracking in this.WorshipTracking)
			{
				if (worshipTracking.Faction == Faction && (WithinTurns == 0 || worshipTracking.Last >= The.CurrentTurn - (long)WithinTurns))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06009930 RID: 39216 RVA: 0x00379A04 File Offset: 0x00377C04
		public bool HasWorshipped(Worshippable Being, int WithinTurns = 0)
		{
			foreach (WorshipTracking worshipTracking in this.WorshipTracking)
			{
				if (worshipTracking.Name == Being.Name && (WithinTurns == 0 || worshipTracking.Last >= The.CurrentTurn - (long)WithinTurns))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06009931 RID: 39217 RVA: 0x00379A80 File Offset: 0x00377C80
		public bool HasWorshippedInName(string Name, string Faction = null, int WithinTurns = 0)
		{
			foreach (WorshipTracking worshipTracking in this.WorshipTracking)
			{
				if (worshipTracking.Name == Name && (Faction.IsNullOrEmpty() || worshipTracking.Faction == Faction) && (WithinTurns == 0 || worshipTracking.Last >= The.CurrentTurn - (long)WithinTurns))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06009932 RID: 39218 RVA: 0x00379B0C File Offset: 0x00377D0C
		public bool HasWorshippedBySpec(string Spec, string ContextFaction = null)
		{
			if (Spec.IsNullOrEmpty())
			{
				return this.HasWorshipped(ContextFaction, 0);
			}
			string text = null;
			string text2 = null;
			int num = 0;
			if (Spec.Contains("=") && Spec.Contains(";"))
			{
				Dictionary<string, string> dictionary = Spec.CachedDictionaryExpansion();
				dictionary.TryGetValue("Name", out text);
				dictionary.TryGetValue("Factions", out text2);
				if (text2 == "*context*")
				{
					text2 = ContextFaction;
				}
				string text3;
				if (dictionary.TryGetValue("WithinTurns", out text3))
				{
					int.TryParse(text3, out num);
				}
			}
			else
			{
				text = Spec;
			}
			if (!text.IsNullOrEmpty())
			{
				return this.HasWorshippedInName(text, text2, num);
			}
			return this.HasWorshipped(text2, num);
		}

		// Token: 0x06009933 RID: 39219 RVA: 0x00379BB0 File Offset: 0x00377DB0
		public List<WorshipTracking> GetWorshipTracking()
		{
			return this.WorshipTracking;
		}

		// Token: 0x06009934 RID: 39220 RVA: 0x00379BB8 File Offset: 0x00377DB8
		public bool HasBlasphemed(string Faction, int WithinTurns = 0)
		{
			foreach (WorshipTracking worshipTracking in this.BlasphemyTracking)
			{
				if (worshipTracking.Faction == Faction && (WithinTurns == 0 || worshipTracking.Last >= The.CurrentTurn - (long)WithinTurns))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06009935 RID: 39221 RVA: 0x00379C2C File Offset: 0x00377E2C
		public bool HasBlasphemed(Worshippable Being, int WithinTurns = 0)
		{
			foreach (WorshipTracking worshipTracking in this.BlasphemyTracking)
			{
				if (worshipTracking.Name == Being.Name && (WithinTurns == 0 || worshipTracking.Last >= The.CurrentTurn - (long)WithinTurns))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06009936 RID: 39222 RVA: 0x00379CA8 File Offset: 0x00377EA8
		public bool HasBlasphemedAgainstName(string Name, string Faction = null, int WithinTurns = 0)
		{
			foreach (WorshipTracking worshipTracking in this.BlasphemyTracking)
			{
				if (worshipTracking.Name == Name && (Faction.IsNullOrEmpty() || worshipTracking.Faction == Faction) && (WithinTurns == 0 || worshipTracking.Last >= The.CurrentTurn - (long)WithinTurns))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06009937 RID: 39223 RVA: 0x00379D34 File Offset: 0x00377F34
		public bool HasBlasphemedBySpec(string Spec, string ContextFaction = null)
		{
			if (Spec.IsNullOrEmpty())
			{
				return this.HasBlasphemed(ContextFaction, 0);
			}
			string text = null;
			string text2 = null;
			int num = 0;
			if (Spec.Contains("=") && Spec.Contains(";"))
			{
				Dictionary<string, string> dictionary = Spec.CachedDictionaryExpansion();
				dictionary.TryGetValue("Name", out text);
				dictionary.TryGetValue("Factions", out text2);
				if (text2 == "*context*")
				{
					text2 = ContextFaction;
				}
				string text3;
				if (dictionary.TryGetValue("WithinTurns", out text3))
				{
					int.TryParse(text3, out num);
				}
			}
			else
			{
				text = Spec;
			}
			if (!text.IsNullOrEmpty())
			{
				return this.HasBlasphemedAgainstName(text, text2, num);
			}
			return this.HasBlasphemed(text2, num);
		}

		// Token: 0x06009938 RID: 39224 RVA: 0x00379DD8 File Offset: 0x00377FD8
		public List<WorshipTracking> GetBlasphemyTracking()
		{
			return this.BlasphemyTracking;
		}

		// Token: 0x04003FD3 RID: 16339
		[NonSerialized]
		private List<WorshipTracking> WorshipTracking;

		// Token: 0x04003FD4 RID: 16340
		[NonSerialized]
		private List<WorshipTracking> BlasphemyTracking;
	}
}
