using System;
using ConsoleLib.Console;
using UnityEngine;
using XRL.World.Parts.Mutation;

namespace XRL.World.Effects
{
	// Token: 0x02000D2C RID: 3372
	[Serializable]
	public class Dominated : Effect
	{
		// Token: 0x060084E8 RID: 34024 RVA: 0x003001C7 File Offset: 0x002FE3C7
		public Dominated()
		{
			this.DisplayName = "dominated";
		}

		// Token: 0x060084E9 RID: 34025 RVA: 0x003001DA File Offset: 0x002FE3DA
		public Dominated(GameObject Dominator, int Duration, bool RoboDom = false)
			: this()
		{
			this.Dominator = Dominator;
			this.Duration = Duration;
			this.DominatorDebugName = ((Dominator != null) ? Dominator.DebugName : null);
			this.FromOriginalPlayerBody = Dominator.IsOriginalPlayerBody();
			this.RoboDom = RoboDom;
		}

		// Token: 0x060084EA RID: 34026 RVA: 0x00300215 File Offset: 0x002FE415
		public override int GetEffectType()
		{
			return 32770;
		}

		// Token: 0x060084EB RID: 34027 RVA: 0x0030021C File Offset: 0x002FE41C
		public override bool SameAs(Effect e)
		{
			return false;
		}

		// Token: 0x060084EC RID: 34028 RVA: 0x0030021F File Offset: 0x002FE41F
		public override string GetDetails()
		{
			return "Under someone else's control.";
		}

		// Token: 0x060084ED RID: 34029 RVA: 0x00300226 File Offset: 0x002FE426
		public override string GetDescription()
		{
			return "dominated (" + this.Duration.Things("turn", null) + " remaining)";
		}

		// Token: 0x060084EE RID: 34030 RVA: 0x00300248 File Offset: 0x002FE448
		public override bool Apply(GameObject Object)
		{
			if (!GameObject.Validate(ref this.Dominator))
			{
				return false;
			}
			if (this.RoboDom)
			{
				if (!Object.FireEvent("ApplyRoboDomination"))
				{
					return false;
				}
				if (!ApplyEffectEvent.Check(Object, "RoboDomination", this, null))
				{
					return false;
				}
			}
			else
			{
				if (!Object.FireEvent("ApplyDomination"))
				{
					return false;
				}
				if (!ApplyEffectEvent.Check(Object, "Domination", this, null))
				{
					return false;
				}
			}
			if (!this.Dominator.ApplyEffect(new Dominating(Object), null))
			{
				return false;
			}
			try
			{
				Object.FireEvent(Event.New("DominationStarted", "Subject", Object, "Dominator", this.Dominator, "Effect", this));
			}
			catch (Exception ex)
			{
				Debug.LogError("Exception in DominationStarted event: " + ex.ToString());
			}
			this.AAID = Object.AddActivatedAbility("End Domination", "CommandEndDomination", "Mental Mutations", null, "\a", null, false, false, false, false, false, false, false, false, true, true, false, false, -1, null, Renderable.UITile("Abilities/abil_end_domination.bmp", 'm', 'r', " ", '\0'), null, null, null);
			Object.StopFighting(this.Dominator, false);
			this.Dominator.StopFighting(Object, false);
			foreach (GameObject gameObject in The.ZoneManager.FindObjectsReadonly((GameObject o) => o.IsAlliedTowards(this.Dominator)))
			{
				gameObject.StopFighting(Object, false);
				Object.StopFighting(gameObject, false);
			}
			return true;
		}

		// Token: 0x060084EF RID: 34031 RVA: 0x003003D4 File Offset: 0x002FE5D4
		public override void Remove(GameObject Object)
		{
			try
			{
				if (Object != null)
				{
					Object.PlayWorldSound("Sounds/Abilities/sfx_ability_mutation_mental_generic_deactivate", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
				}
				Event @event = Event.New("DominationEnded", 0, 0, 0);
				@event.SetParameter("Subject", Object);
				@event.SetParameter("Dominator", this.Dominator);
				@event.SetParameter("Effect", this);
				if (this.Metempsychosis)
				{
					@event.SetFlag("Metempsychosis", true);
				}
				Object.FireEvent(@event);
			}
			catch (Exception ex)
			{
				MetricsManager.LogError("exception from DominationEnded event", ex);
			}
			try
			{
				Object.RemoveActivatedAbility(ref this.AAID);
			}
			catch (Exception ex2)
			{
				MetricsManager.LogError("exception removing End Domination ability", ex2);
			}
			if (!this.BeingRemovedBySource)
			{
				try
				{
					if (Object.OnWorldMap())
					{
						Object.PullDown(false);
					}
					if (GameObject.Validate(ref this.Dominator) && this.Dominator.HasHitpoints())
					{
						this.Dominator.FireEvent("DominationBroken");
					}
					else if (Object.IsPlayer())
					{
						Domination.Metempsychosis(Object, this.FromOriginalPlayerBody);
					}
				}
				catch (Exception ex3)
				{
					MetricsManager.LogError("exception in Domination cleanupestatus", ex3);
				}
			}
		}

		// Token: 0x060084F0 RID: 34032 RVA: 0x00300524 File Offset: 0x002FE724
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == SingletonEvent<BeginTakeActionEvent>.ID || ID == OnDestroyObjectEvent.ID || ID == PooledEvent<JoinPartyLeaderPossibleEvent>.ID;
		}

		// Token: 0x060084F1 RID: 34033 RVA: 0x0030054C File Offset: 0x002FE74C
		public override bool HandleEvent(BeginTakeActionEvent E)
		{
			if (this.Duration > 0)
			{
				if (!GameObject.Validate(ref this.Dominator))
				{
					MetricsManager.LogError("ending domination because of loss of dominator, was " + (this.DominatorDebugName ?? "null"));
					base.Object.RemoveEffect(this, true);
				}
				else if (this.Duration != 9999)
				{
					this.Duration--;
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x060084F2 RID: 34034 RVA: 0x003005BF File Offset: 0x002FE7BF
		public override bool HandleEvent(OnDestroyObjectEvent E)
		{
			base.Object.RemoveEffect(this, true);
			return base.HandleEvent(E);
		}

		// Token: 0x060084F3 RID: 34035 RVA: 0x003005D8 File Offset: 0x002FE7D8
		public override bool HandleEvent(JoinPartyLeaderPossibleEvent E)
		{
			return E.Result = false;
		}

		// Token: 0x060084F4 RID: 34036 RVA: 0x003005EF File Offset: 0x002FE7EF
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("BeforeDie");
			Registrar.Register("CommandEndDomination");
			Registrar.Register("InterruptDomination");
			base.Register(Object, Registrar);
		}

		// Token: 0x060084F5 RID: 34037 RVA: 0x0030061C File Offset: 0x002FE81C
		public override bool FireEvent(Event E)
		{
			if (E.ID == "BeforeDie" || E.ID == "CommandEndDomination")
			{
				base.Object.RemoveEffect(this, true);
			}
			else if (E.ID == "InterruptDomination" && base.Object.FireEvent("ChainInterruptDomination"))
			{
				base.Object.RemoveEffect(this, true);
				return false;
			}
			return base.FireEvent(E);
		}

		// Token: 0x040039A6 RID: 14758
		public GameObject Dominator;

		// Token: 0x040039A7 RID: 14759
		public bool RoboDom;

		// Token: 0x040039A8 RID: 14760
		public Guid AAID;

		// Token: 0x040039A9 RID: 14761
		public string DominatorDebugName;

		// Token: 0x040039AA RID: 14762
		public bool FromOriginalPlayerBody;

		// Token: 0x040039AB RID: 14763
		[NonSerialized]
		public bool BeingRemovedBySource;

		// Token: 0x040039AC RID: 14764
		[NonSerialized]
		public bool Metempsychosis;
	}
}
