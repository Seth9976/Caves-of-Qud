using System;
using XRL.UI;
using XRL.World.Effects;

namespace XRL.World.Parts.Mutation
{
	// Token: 0x020013F0 RID: 5104
	[Serializable]
	public class LifeDrain : BaseMutation
	{
		// Token: 0x0600C593 RID: 50579 RVA: 0x0047E8DC File Offset: 0x0047CADC
		public LifeDrain()
		{
			base.Type = "Mental";
		}

		// Token: 0x0600C594 RID: 50580 RVA: 0x0047E8F6 File Offset: 0x0047CAF6
		public override string GetDescription()
		{
			return "You bond with a nearby organic creature and leech its life force.";
		}

		// Token: 0x0600C595 RID: 50581 RVA: 0x0047E900 File Offset: 0x0047CB00
		public override string GetLevelText(int Level)
		{
			string text = "Mental attack versus an organic creature\n";
			text = string.Concat(new string[]
			{
				text,
				"Drains {{rules|",
				Level.ToString(),
				"}} hit ",
				(Level == 1) ? "point" : "points",
				" per round\n"
			});
			text += "Target gets a mental save to resist damage each round\n";
			text += "Duration: 20 rounds\n";
			return text + "Cooldown: 200 rounds\n";
		}

		// Token: 0x0600C596 RID: 50582 RVA: 0x0047E97D File Offset: 0x0047CB7D
		public int GetCooldown(int Level)
		{
			return 200;
		}

		// Token: 0x0600C597 RID: 50583 RVA: 0x0047E984 File Offset: 0x0047CB84
		public int GetDuration(int Level)
		{
			return 20;
		}

		// Token: 0x0600C598 RID: 50584 RVA: 0x0047E988 File Offset: 0x0047CB88
		public override void CollectStats(Templates.StatCollector stats, int Level)
		{
			if (Level == 1)
			{
				stats.Set("HP", "1 hit point", false, 0);
			}
			else
			{
				stats.Set("HP", Level.ToString() + " hit points", false, 0);
			}
			stats.Set("Duration", this.GetDuration(Level), false, 0);
			stats.CollectCooldownTurns(base.MyActivatedAbility(this.ActivatedAbilityID, null), this.GetCooldown(Level), 0);
		}

		// Token: 0x0600C599 RID: 50585 RVA: 0x0047E9FA File Offset: 0x0047CBFA
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AIGetOffensiveAbilityListEvent.ID;
		}

		// Token: 0x0600C59A RID: 50586 RVA: 0x0047EA10 File Offset: 0x0047CC10
		public override bool HandleEvent(AIGetOffensiveAbilityListEvent E)
		{
			if (E.Distance <= 1 && base.IsMyActivatedAbilityAIUsable(this.ActivatedAbilityID, null) && LifeDrain.IsValidTarget(E.Target) && (!this.RealityDistortionBased || (base.CheckMyRealityDistortionAdvisability(null, null, null, null, null, null, 0) && IComponent<GameObject>.CheckRealityDistortionAdvisability(E.Target, null, E.Actor, null, this, null, 0))))
			{
				E.Add("CommandLifeDrain", 1, null, false, false, null, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C59B RID: 50587 RVA: 0x0047EA99 File Offset: 0x0047CC99
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("CommandLifeDrain");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600C59C RID: 50588 RVA: 0x0047EAB0 File Offset: 0x0047CCB0
		public override bool FireEvent(Event E)
		{
			if (E.ID == "CommandLifeDrain")
			{
				if (this.RealityDistortionBased && !this.ParentObject.IsRealityDistortionUsable(null, null))
				{
					RealityStabilized.ShowGenericInterdictMessage(this.ParentObject, null);
					return false;
				}
				Cell cell = base.PickDirection("Syphon Vim", null);
				if (cell != null)
				{
					GameObject combatTarget = cell.GetCombatTarget(this.ParentObject, true, false, false, 5, null, null, null, null, null, false, false, null);
					if (combatTarget == this.ParentObject)
					{
						if (this.ParentObject.IsPlayer())
						{
							Popup.ShowFail("You cannot syphon vim from " + this.ParentObject.itself + ".", true, true, true);
						}
						return false;
					}
					if (combatTarget == null)
					{
						if (this.ParentObject.IsPlayer())
						{
							Popup.ShowFail("No one is there for you to syphon vim from.", true, true, true);
						}
						return false;
					}
					if (!LifeDrain.IsValidTarget(combatTarget))
					{
						if (this.ParentObject.IsPlayer())
						{
							Popup.ShowFail("You cannot syphon vim from " + combatTarget.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + ".", true, true, true);
						}
						return false;
					}
					if (this.RealityDistortionBased)
					{
						Event @event = Event.New("InitiateRealityDistortionTransit", "Object", this.ParentObject, "Mutation", this, "Cell", cell);
						if (!this.ParentObject.FireEvent(@event, E) || !cell.FireEvent(@event, E))
						{
							return false;
						}
					}
					combatTarget.ApplyEffect(new LifeDrain(20, base.Level, base.Level.ToString(), this.ParentObject, this.RealityDistortionBased), null);
					base.UseEnergy(1000, "Mental Mutation SyphonVim");
					base.CooldownMyActivatedAbility(this.ActivatedAbilityID, 200, null, null, false);
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600C59D RID: 50589 RVA: 0x0047EC70 File Offset: 0x0047CE70
		public override bool ChangeLevel(int NewLevel)
		{
			return base.ChangeLevel(NewLevel);
		}

		// Token: 0x0600C59E RID: 50590 RVA: 0x0047EC7C File Offset: 0x0047CE7C
		public override bool Mutate(GameObject GO, int Level)
		{
			this.ActivatedAbilityID = base.AddMyActivatedAbility("Syphon Vim", "CommandLifeDrain", "Mental Mutations", null, "í", null, false, false, false, false, this.RealityDistortionBased, false, false, false, true, true, false, -1, null, null, null, null, null, null);
			return base.Mutate(GO, Level);
		}

		// Token: 0x0600C59F RID: 50591 RVA: 0x0047ECCA File Offset: 0x0047CECA
		public override bool Unmutate(GameObject GO)
		{
			base.RemoveMyActivatedAbility(ref this.ActivatedAbilityID, null);
			return base.Unmutate(GO);
		}

		// Token: 0x0600C5A0 RID: 50592 RVA: 0x0047ECE1 File Offset: 0x0047CEE1
		public static bool IsValidTarget(GameObject Object)
		{
			return GameObject.Validate(ref Object) && Object.IsCombatObject() && Object.HasHitpoints() && Object.IsOrganic;
		}

		// Token: 0x04004E02 RID: 19970
		public bool RealityDistortionBased = true;
	}
}
