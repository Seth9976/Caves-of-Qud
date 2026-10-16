using System;
using System.Collections.Generic;
using XRL.Rules;
using XRL.UI;

namespace XRL.World.Parts
{
	// Token: 0x02000F24 RID: 3876
	[Serializable]
	public class CyberneticsCustomVisage : IPart
	{
		// Token: 0x06009A90 RID: 39568 RVA: 0x003812E8 File Offset: 0x0037F4E8
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x06009A91 RID: 39569 RVA: 0x003812EB File Offset: 0x0037F4EB
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == GetShortDescriptionEvent.ID || ID == ImplantedEvent.ID || ID == UnimplantedEvent.ID || ID == PooledEvent<AfterPlayerBodyChangeEvent>.ID;
		}

		// Token: 0x06009A92 RID: 39570 RVA: 0x00381319 File Offset: 0x0037F519
		public override bool HandleEvent(ImplantedEvent E)
		{
			this.ApplyVisage(E.Implantee);
			return base.HandleEvent(E);
		}

		// Token: 0x06009A93 RID: 39571 RVA: 0x0038132E File Offset: 0x0037F52E
		public override bool HandleEvent(UnimplantedEvent E)
		{
			this.UnapplyVisage(E.Implantee);
			return base.HandleEvent(E);
		}

		// Token: 0x06009A94 RID: 39572 RVA: 0x00381344 File Offset: 0x0037F544
		public override bool HandleEvent(AfterPlayerBodyChangeEvent E)
		{
			if (!this.Faction.IsNullOrEmpty())
			{
				GameObject implantee = this.ParentObject.Implantee;
				if (E.NewBody == implantee)
				{
					XRLGame game = The.Game;
					if (game != null)
					{
						Reputation playerReputation = game.PlayerReputation;
						if (playerReputation != null)
						{
							playerReputation.Modify(this.Faction, CyberneticsCustomVisage.AMOUNT, null, null, null, true, true);
						}
					}
				}
				else if (E.OldBody == implantee)
				{
					XRLGame game2 = The.Game;
					if (game2 != null)
					{
						Reputation playerReputation2 = game2.PlayerReputation;
						if (playerReputation2 != null)
						{
							playerReputation2.Modify(this.Faction, -CyberneticsCustomVisage.AMOUNT, null, null, null, true, true);
						}
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009A95 RID: 39573 RVA: 0x003813E0 File Offset: 0x0037F5E0
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			if (!this.Faction.IsNullOrEmpty())
			{
				E.Postfix.AppendRules(CyberneticsCustomVisage.AMOUNT.Signed(false) + " reputation with " + XRL.World.Faction.GetFormattedName(this.Faction));
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009A96 RID: 39574 RVA: 0x0038142D File Offset: 0x0037F62D
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x06009A97 RID: 39575 RVA: 0x00381430 File Offset: 0x0037F630
		private void ApplyVisage(GameObject Subject)
		{
			if (this.Faction.IsNullOrEmpty())
			{
				List<Faction> list = new List<Faction>(Factions.GetFactionCount());
				foreach (Faction faction in Factions.Loop())
				{
					if (faction.Visible)
					{
						list.Add(faction);
					}
				}
				list.Sort((Faction a, Faction b) => a.DisplayName.CompareTo(b.DisplayName));
				List<string> list2 = new List<string>(list.Count);
				foreach (Faction faction2 in list)
				{
					list2.Add(faction2.DisplayName);
				}
				if (Subject.IsPlayer())
				{
					int num = Popup.PickOption("Choose a model faction for your facial reconstruction.", null, "", "Sounds/UI/ui_notification", list2.ToArray(), null, null, null, null, null, null, 0, 60, 0, -1, false, false, false, true, false, null, null);
					if (num == -1)
					{
						num = Stat.Random(0, list.Count - 1);
					}
					this.Faction = list[num].Name;
				}
				else
				{
					Faction randomElement = list.GetRandomElement<Faction>(null);
					this.Faction = ((randomElement != null) ? randomElement.Name : null);
				}
			}
			if (!this.Faction.IsNullOrEmpty() && Subject != null && Subject.IsPlayer())
			{
				XRLGame game = The.Game;
				if (game == null)
				{
					return;
				}
				Reputation playerReputation = game.PlayerReputation;
				if (playerReputation == null)
				{
					return;
				}
				playerReputation.Modify(this.Faction, CyberneticsCustomVisage.AMOUNT, null, null, null, true, true);
			}
		}

		// Token: 0x06009A98 RID: 39576 RVA: 0x003815D4 File Offset: 0x0037F7D4
		private void UnapplyVisage(GameObject Subject)
		{
			if (!this.Faction.IsNullOrEmpty() && Subject != null && Subject.IsPlayer())
			{
				XRLGame game = The.Game;
				if (game == null)
				{
					return;
				}
				Reputation playerReputation = game.PlayerReputation;
				if (playerReputation == null)
				{
					return;
				}
				playerReputation.Modify(this.Faction, -CyberneticsCustomVisage.AMOUNT, null, null, null, true, true);
			}
		}

		// Token: 0x04004048 RID: 16456
		public static readonly int AMOUNT = 300;

		// Token: 0x04004049 RID: 16457
		public string Faction;
	}
}
