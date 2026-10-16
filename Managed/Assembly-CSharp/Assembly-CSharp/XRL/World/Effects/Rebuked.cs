using System;
using HistoryKit;
using Qud.API;
using XRL.World.AI;
using XRL.World.Parts;
using XRL.World.Parts.Skill;

namespace XRL.World.Effects
{
	// Token: 0x02000D77 RID: 3447
	[Serializable]
	public class Rebuked : Effect
	{
		// Token: 0x060088C1 RID: 35009 RVA: 0x0030FF56 File Offset: 0x0030E156
		public Rebuked()
		{
			this.DisplayName = "{{C|rebuked}}";
			this.Duration = 1;
		}

		// Token: 0x060088C2 RID: 35010 RVA: 0x0030FF70 File Offset: 0x0030E170
		public Rebuked(GameObject Rebuker)
			: this()
		{
			this.Rebuker = Rebuker;
		}

		// Token: 0x060088C3 RID: 35011 RVA: 0x0030FF7F File Offset: 0x0030E17F
		public override int GetEffectType()
		{
			return 2;
		}

		// Token: 0x060088C4 RID: 35012 RVA: 0x0030FF82 File Offset: 0x0030E182
		public override bool SameAs(Effect e)
		{
			return false;
		}

		// Token: 0x060088C5 RID: 35013 RVA: 0x0030FF85 File Offset: 0x0030E185
		public override string GetDetails()
		{
			return "Rebuked into obedience by another creature.";
		}

		// Token: 0x060088C6 RID: 35014 RVA: 0x0030FF8C File Offset: 0x0030E18C
		public override string GetDescription()
		{
			return "{{C|rebuked}}";
		}

		// Token: 0x060088C7 RID: 35015 RVA: 0x0030FF93 File Offset: 0x0030E193
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == SingletonEvent<BeforeBeginTakeActionEvent>.ID || ID == GetInventoryActionsEvent.ID || ID == InventoryActionEvent.ID;
		}

		// Token: 0x060088C8 RID: 35016 RVA: 0x0030FFB9 File Offset: 0x0030E1B9
		public override bool HandleEvent(BeforeBeginTakeActionEvent E)
		{
			if (!this.IsSupported())
			{
				this.Duration = 0;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x060088C9 RID: 35017 RVA: 0x0030FFD4 File Offset: 0x0030E1D4
		public override bool HandleEvent(GetInventoryActionsEvent E)
		{
			if (E.Actor == this.Rebuker)
			{
				E.AddAction("Dismiss", "dismiss", "DismissServitor", null, 'd', false, 0, 0, false, true, false, false, true, null, false);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x060088CA RID: 35018 RVA: 0x0031001C File Offset: 0x0030E21C
		public override bool HandleEvent(InventoryActionEvent E)
		{
			if (E.Command == "DismissServitor" && E.Actor == this.Rebuker && E.Item == base.Object && this.Rebuker.CheckCompanionDirection(base.Object))
			{
				IComponent<GameObject>.XDidYToZ(this.Rebuker, "dismiss", base.Object, "from " + this.Rebuker.its + " service", null, null, null, null, null, false, false, false, false, false, null, null, null, false, false, false, false, false, null);
				base.Object.RemoveEffect(this, true);
				E.Actor.PlayWorldSound("Sounds/Interact/sfx_interact_follower_dismiss", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
				E.Actor.CompanionDirectionEnergyCost(E.Item, 100, "Dismiss");
				E.Item.FireEvent(Event.New("DismissedFromService", "Object", E.Item, "Leader", E.Actor, "Effect", this));
				E.RequestInterfaceExit();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x060088CB RID: 35019 RVA: 0x00310150 File Offset: 0x0030E350
		public override bool Apply(GameObject Object)
		{
			if (!GameObject.Validate(ref this.Rebuker))
			{
				return false;
			}
			if (Object.Brain == null)
			{
				return false;
			}
			Object.RemoveEffect<Rebuked>();
			if (Object != null)
			{
				Object.PlayWorldSound("Sounds/StatusEffects/sfx_statusEffect_charm", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
			}
			IComponent<GameObject>.XDidYToZ(this.Rebuker, "rebuke", Object, "into submission", null, null, null, this.Rebuker, null, false, false, false, false, false, null, null, null, false, false, false, false, false, null);
			if (this.Rebuker.IsPlayer() && !this.Rebuker.HasEffect<Dominated>())
			{
				JournalAPI.AddAccomplishment("You rebuked " + Object.an(int.MaxValue, null, null, false, false, false, false, false, false, true, false, false, true, false, null, false, null, false) + " into submission.", HistoricStringExpander.ExpandString(string.Concat(new string[]
				{
					"<spice.commonPhrases.onlooker.!random.capitalize>! <spice.commonPhrases.remember.!random.capitalize> the admonishment =name= gave ",
					Object.an(int.MaxValue, null, null, false, false, false, false, true, true, true, false, false, true, false, null, false, null, false),
					" when ",
					Object.it,
					" presumed to speak the sacred tongue!"
				}), null), string.Concat(new string[]
				{
					"<spice.elements.",
					The.Player.GetMythicDomain(5, null),
					".weddingConditions.!random.capitalize>, =name= cemented ",
					The.Player.GetPronounProvider(false).PossessiveAdjective,
					" friendship with ",
					Object.GetPrimaryFactionName(true, true, true),
					" by marrying ",
					Object.an(int.MaxValue, null, null, false, false, false, false, false, false, true, false, false, true, false, null, false, null, false),
					"."
				}), null, "general", MuralCategory.Trysts, MuralWeight.Low, null, -1L, true);
			}
			Object.Heartspray("&C", "&c", "&B", "&Y", '\u0003');
			Object.SetAlliedLeader<AllyRebuke>(this.Rebuker, 0, false);
			Persuasion_RebukeRobot.SyncTarget(this.Rebuker, Object, false);
			return true;
		}

		// Token: 0x060088CC RID: 35020 RVA: 0x00310358 File Offset: 0x0030E558
		public override void Remove(GameObject Object)
		{
			if (GameObject.Validate(ref this.Rebuker) && Object.PartyLeader == this.Rebuker && !this.Rebuker.SupportsFollower(Object, 7))
			{
				Object.Brain.PartyLeader = null;
				Persuasion_RebukeRobot.Neutralize(this.Rebuker, Object);
				IComponent<GameObject>.XDidY(Object, "wander", "away disinterestedly", null, null, null, null, null, false, false, null, null, false, false, false, false, false, null);
			}
			Brain brain = Object.Brain;
			GameObject rebuker = this.Rebuker;
			brain.RemoveAllegiance<AllyRebuke>((rebuker != null) ? rebuker.BaseID : 0);
			this.Rebuker = null;
			base.Remove(Object);
		}

		// Token: 0x060088CD RID: 35021 RVA: 0x003103F1 File Offset: 0x0030E5F1
		public bool IsSupported()
		{
			return GameObject.Validate(ref this.Rebuker) && this.Rebuker.SupportsFollower(base.Object, 8);
		}

		// Token: 0x04003A60 RID: 14944
		public GameObject Rebuker;
	}
}
