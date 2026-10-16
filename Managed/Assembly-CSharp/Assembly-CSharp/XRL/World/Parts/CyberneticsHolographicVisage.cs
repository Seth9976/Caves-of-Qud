using System;
using System.Collections.Generic;
using XRL.Rules;
using XRL.UI;

namespace XRL.World.Parts
{
	// Token: 0x02000F30 RID: 3888
	[Serializable]
	public class CyberneticsHolographicVisage : IPart
	{
		// Token: 0x06009AF7 RID: 39671 RVA: 0x0038362B File Offset: 0x0038182B
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x06009AF8 RID: 39672 RVA: 0x0038362E File Offset: 0x0038182E
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<CommandEvent>.ID || ID == GetShortDescriptionEvent.ID || ID == ImplantedEvent.ID || ID == UnimplantedEvent.ID || ID == PooledEvent<AfterPlayerBodyChangeEvent>.ID;
		}

		// Token: 0x06009AF9 RID: 39673 RVA: 0x00383664 File Offset: 0x00381864
		public override bool HandleEvent(ImplantedEvent E)
		{
			this.SelectVisage(E.Implantee);
			this.ApplyVisage(E.Implantee);
			this.ActivatedAbilityID = E.Implantee.AddDynamicCommand(out this.CommandID, "AlterHolographicVisage", "Alter Visage", "Cybernetics", null, "\u0002", null, false, false, false, false, false, false, false, false, true, true, false, false, -1, null, null, null, null);
			return base.HandleEvent(E);
		}

		// Token: 0x06009AFA RID: 39674 RVA: 0x003836CE File Offset: 0x003818CE
		public override bool HandleEvent(UnimplantedEvent E)
		{
			this.UnapplyVisage(E.Implantee);
			E.Implantee.RemoveActivatedAbility(ref this.ActivatedAbilityID);
			return base.HandleEvent(E);
		}

		// Token: 0x06009AFB RID: 39675 RVA: 0x003836F8 File Offset: 0x003818F8
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
							playerReputation.Modify(this.Faction, CyberneticsHolographicVisage.AMOUNT, null, null, null, true, true);
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
							playerReputation2.Modify(this.Faction, -CyberneticsHolographicVisage.AMOUNT, null, null, null, true, true);
						}
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009AFC RID: 39676 RVA: 0x00383794 File Offset: 0x00381994
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			if (!this.Faction.IsNullOrEmpty())
			{
				E.Postfix.AppendRules(CyberneticsHolographicVisage.AMOUNT.Signed(false) + " reputation with " + XRL.World.Faction.GetFormattedName(this.Faction));
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009AFD RID: 39677 RVA: 0x003837E4 File Offset: 0x003819E4
		public override bool HandleEvent(CommandEvent E)
		{
			if (E.Command == this.CommandID && E.Actor == this.ParentObject.Implantee)
			{
				this.UnapplyVisage(E.Actor);
				this.SelectVisage(E.Actor);
				this.ApplyVisage(E.Actor);
				E.Actor.UseEnergy(1000, "Cybernetics HolographicVisage", null, null, false);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009AFE RID: 39678 RVA: 0x00383863 File Offset: 0x00381A63
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x06009AFF RID: 39679 RVA: 0x00383868 File Offset: 0x00381A68
		private void ApplyVisage(GameObject Subject)
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
				playerReputation.Modify(this.Faction, CyberneticsHolographicVisage.AMOUNT, null, null, null, true, true);
			}
		}

		// Token: 0x06009B00 RID: 39680 RVA: 0x003838B8 File Offset: 0x00381AB8
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
				playerReputation.Modify(this.Faction, -CyberneticsHolographicVisage.AMOUNT, null, null, null, true, true);
			}
		}

		// Token: 0x06009B01 RID: 39681 RVA: 0x00383908 File Offset: 0x00381B08
		private void SelectVisage(GameObject Subject)
		{
			if (!GameObject.Validate(ref Subject))
			{
				return;
			}
			List<Faction> list = new List<Faction>(Factions.GetFactionCount());
			foreach (Faction faction in Factions.Loop())
			{
				if (faction.Visible)
				{
					list.Add(faction);
				}
			}
			list.Sort((Faction a, Faction b) => a.DisplayName.CompareTo(b.DisplayName));
			List<string> list2 = new List<string>(list.Count + 1);
			list2.Add("none");
			foreach (Faction faction2 in list)
			{
				list2.Add(faction2.DisplayName);
			}
			if (Subject.IsPlayer())
			{
				int num = Popup.PickOption("Choose a model faction for your holographic glamour.", null, "", "Sounds/UI/ui_notification", list2.ToArray(), null, null, null, null, null, null, 0, 60, 0, -1, false, false, false, true, false, null, null);
				if (num == 0)
				{
					this.Faction = null;
				}
				else
				{
					if (num == -1)
					{
						num = Stat.Random(1, list.Count);
					}
					this.Faction = list[num - 1].Name;
				}
			}
			else
			{
				Faction randomElement = list.GetRandomElement<Faction>(null);
				this.Faction = ((randomElement != null) ? randomElement.Name : null);
			}
			if (!this.Faction.IsNullOrEmpty())
			{
				IComponent<GameObject>.EmitMessage(Subject, string.Concat(new string[]
				{
					"In a glissade of light, ",
					Subject.poss("visage", true),
					" morphs into an image pleasing to ",
					XRL.World.Faction.GetFormattedName(this.Faction),
					"."
				}), ' ', true, false, false, null, null);
			}
		}

		// Token: 0x0400405D RID: 16477
		public static readonly int AMOUNT = 200;

		// Token: 0x0400405E RID: 16478
		public string CommandID;

		// Token: 0x0400405F RID: 16479
		public string Faction;

		// Token: 0x04004060 RID: 16480
		public Guid ActivatedAbilityID = Guid.Empty;
	}
}
