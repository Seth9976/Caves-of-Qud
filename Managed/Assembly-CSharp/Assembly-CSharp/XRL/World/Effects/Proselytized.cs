using System;
using Qud.API;
using XRL.World.AI;
using XRL.World.Parts;
using XRL.World.Parts.Skill;

namespace XRL.World.Effects
{
	// Token: 0x02000D74 RID: 3444
	[Serializable]
	public class Proselytized : Effect
	{
		// Token: 0x0600887E RID: 34942 RVA: 0x0030E169 File Offset: 0x0030C369
		public Proselytized()
		{
			this.DisplayName = "{{Y|proselytized}}";
			this.Duration = 1;
		}

		// Token: 0x0600887F RID: 34943 RVA: 0x0030E183 File Offset: 0x0030C383
		public Proselytized(GameObject Proselytizer)
			: this()
		{
			this.Proselytizer = Proselytizer;
		}

		// Token: 0x06008880 RID: 34944 RVA: 0x0030E192 File Offset: 0x0030C392
		public override int GetEffectType()
		{
			return 2;
		}

		// Token: 0x06008881 RID: 34945 RVA: 0x0030E195 File Offset: 0x0030C395
		public override bool SameAs(Effect e)
		{
			return false;
		}

		// Token: 0x06008882 RID: 34946 RVA: 0x0030E198 File Offset: 0x0030C398
		public override string GetDetails()
		{
			return "Recruited by another creature via proselytization.";
		}

		// Token: 0x06008883 RID: 34947 RVA: 0x0030E19F File Offset: 0x0030C39F
		public override string GetDescription()
		{
			return "{{Y|proselytized}}";
		}

		// Token: 0x06008884 RID: 34948 RVA: 0x0030E1A6 File Offset: 0x0030C3A6
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == SingletonEvent<BeforeBeginTakeActionEvent>.ID || ID == GetInventoryActionsEvent.ID || ID == InventoryActionEvent.ID;
		}

		// Token: 0x06008885 RID: 34949 RVA: 0x0030E1CC File Offset: 0x0030C3CC
		public override bool HandleEvent(BeforeBeginTakeActionEvent E)
		{
			if (!this.IsSupported())
			{
				this.Duration = 0;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008886 RID: 34950 RVA: 0x0030E1E4 File Offset: 0x0030C3E4
		public override bool HandleEvent(GetInventoryActionsEvent E)
		{
			if (E.Actor == this.Proselytizer)
			{
				E.AddAction("Dismiss", "dismiss", "DismissProselyte", null, 'd', false, 0, 0, false, true, false, false, true, null, false);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008887 RID: 34951 RVA: 0x0030E22C File Offset: 0x0030C42C
		public override bool HandleEvent(InventoryActionEvent E)
		{
			if (E.Command == "DismissProselyte" && E.Actor == this.Proselytizer && E.Item == base.Object && this.Proselytizer.CheckCompanionDirection(base.Object))
			{
				IComponent<GameObject>.XDidYToZ(this.Proselytizer, "dismiss", base.Object, "from " + this.Proselytizer.its + " service", null, null, null, null, null, false, false, false, false, false, null, null, null, false, false, false, false, false, null);
				base.Object.RemoveEffect(this, true);
				E.Actor.PlayWorldSound("Sounds/Interact/sfx_interact_follower_dismiss", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
				E.Actor.CompanionDirectionEnergyCost(E.Item, 100, "Dismiss");
				E.Item.FireEvent(Event.New("DismissedFromService", "Object", E.Item, "Leader", E.Actor, "Effect", this));
				E.RequestInterfaceExit();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008888 RID: 34952 RVA: 0x0030E360 File Offset: 0x0030C560
		public override bool Apply(GameObject Object)
		{
			if (!GameObject.Validate(ref this.Proselytizer))
			{
				return false;
			}
			if (Object.Brain == null)
			{
				return false;
			}
			if (!Object.FireEvent("ApplyProselytize"))
			{
				return false;
			}
			if (Object != null)
			{
				Object.PlayWorldSound("Sounds/StatusEffects/sfx_statusEffect_charm", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
			}
			Object.RemoveEffect<Proselytized>();
			IComponent<GameObject>.XDidYToZ(this.Proselytizer, "convince", Object, "to join " + this.Proselytizer.them, "!", null, null, this.Proselytizer, null, false, false, false, false, false, null, null, null, false, false, false, false, false, null);
			if (this.Proselytizer.IsPlayer() && !this.Proselytizer.HasEffect<Dominated>() && !Object.HasIntProperty("TurnsAsPlayerMinion"))
			{
				JournalAPI.AddAccomplishment("You convinced " + Object.an(int.MaxValue, null, null, false, false, false, false, false, false, true, false, false, true, false, null, false, null, false) + " to join your cause.", string.Concat(new string[]
				{
					"Few were possessed of such potent charm as =name=, who -- on the ",
					Calendar.GetDay(),
					" of ",
					Calendar.GetMonth(),
					" -- bent the will of ",
					Object.an(int.MaxValue, null, null, false, false, false, false, true, false, true, false, false, true, false, null, false, null, false),
					" with mere words."
				}), string.Concat(new string[]
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
				}), null, "general", MuralCategory.Treats, MuralWeight.Low, null, -1L, true);
			}
			Object.Heartspray("&M", "&R", "&r", "&Y", '\u0003');
			Object.AddOpinion<OpinionProselytize>(this.Proselytizer, 1f);
			Object.SetAlliedLeader<AllyProselytize>(this.Proselytizer, 0, false);
			Persuasion_Proselytize.SyncTarget(this.Proselytizer, Object, false);
			return true;
		}

		// Token: 0x06008889 RID: 34953 RVA: 0x0030E5B4 File Offset: 0x0030C7B4
		public override void Remove(GameObject Object)
		{
			if (GameObject.Validate(ref this.Proselytizer) && Object.PartyLeader == this.Proselytizer && !this.Proselytizer.SupportsFollower(Object, 14))
			{
				Object.Brain.PartyLeader = null;
				Object.Brain.Goals.Clear();
			}
			Brain brain = Object.Brain;
			GameObject proselytizer = this.Proselytizer;
			brain.RemoveAllegiance<AllyProselytize>((proselytizer != null) ? proselytizer.BaseID : 0);
			this.Proselytizer = null;
			base.Remove(Object);
		}

		// Token: 0x0600888A RID: 34954 RVA: 0x0030E633 File Offset: 0x0030C833
		public bool IsSupported()
		{
			return GameObject.Validate(ref this.Proselytizer) && this.Proselytizer.SupportsFollower(base.Object, 1);
		}

		// Token: 0x04003A54 RID: 14932
		public GameObject Proselytizer;
	}
}
