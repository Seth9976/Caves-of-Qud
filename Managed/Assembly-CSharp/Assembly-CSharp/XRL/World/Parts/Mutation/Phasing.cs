using System;
using XRL.UI;
using XRL.World.Effects;

namespace XRL.World.Parts.Mutation
{
	// Token: 0x02001450 RID: 5200
	[Serializable]
	public class Phasing : BaseMutation
	{
		// Token: 0x0600CB3B RID: 52027 RVA: 0x0049E653 File Offset: 0x0049C853
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AIGetDefensiveAbilityListEvent.ID || ID == AIGetOffensiveAbilityListEvent.ID || ID == SingletonEvent<BeforeAbilityManagerOpenEvent>.ID || ID == PooledEvent<GetItemElementsEvent>.ID;
		}

		// Token: 0x0600CB3C RID: 52028 RVA: 0x0049E681 File Offset: 0x0049C881
		public override bool HandleEvent(BeforeAbilityManagerOpenEvent E)
		{
			base.DescribeMyActivatedAbility(this.PhaseOutActivatedAbilityID, new Action<Templates.StatCollector>(this.CollectStats), null);
			return base.HandleEvent(E);
		}

		// Token: 0x0600CB3D RID: 52029 RVA: 0x0049E6A8 File Offset: 0x0049C8A8
		public override bool HandleEvent(AIGetDefensiveAbilityListEvent E)
		{
			if (base.IsMyActivatedAbilityAIUsable(this.PhaseOutActivatedAbilityID, null) && !E.Actor.HasEffect<Phased>() && E.Actor.isDamaged(0.25, false) && base.CheckMyRealityDistortionAdvisability(null, null, null, null, null, null, 0))
			{
				E.Add("CommandPhaseOut", 1, null, false, false, null, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600CB3E RID: 52030 RVA: 0x0049E718 File Offset: 0x0049C918
		public override bool HandleEvent(AIGetOffensiveAbilityListEvent E)
		{
			if (E.Distance > 1 && E.Distance < 5 + base.Level && base.IsMyActivatedAbilityAIUsable(this.PhaseOutActivatedAbilityID, null) && !E.Actor.HasEffect<Phased>() && base.CheckMyRealityDistortionAdvisability(null, null, null, null, null, null, 0))
			{
				E.Add("CommandPhaseOut", 1, null, false, false, null, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600CB3F RID: 52031 RVA: 0x0049E789 File Offset: 0x0049C989
		public override bool HandleEvent(GetItemElementsEvent E)
		{
			if (E.IsRelevantCreature(this.ParentObject))
			{
				E.Add("glass", this.BaseElementWeight);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600CB40 RID: 52032 RVA: 0x0049E7B4 File Offset: 0x0049C9B4
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("AfterPhaseIn");
			Registrar.Register("BeginTakeAction");
			Registrar.Register("CommandPhaseIn");
			Registrar.Register("CommandPhaseOut");
			Registrar.Register("CommandTogglePhase");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600CB41 RID: 52033 RVA: 0x0049E800 File Offset: 0x0049CA00
		public override string GetDescription()
		{
			return "You may phase through solid objects for brief periods of time.";
		}

		// Token: 0x0600CB42 RID: 52034 RVA: 0x0049E807 File Offset: 0x0049CA07
		public int GetDuration(int Level)
		{
			return 6 + Level;
		}

		// Token: 0x0600CB43 RID: 52035 RVA: 0x0049E80C File Offset: 0x0049CA0C
		public int GetBaseCooldown(int Level)
		{
			return 103 - 3 * Level;
		}

		// Token: 0x0600CB44 RID: 52036 RVA: 0x0049E814 File Offset: 0x0049CA14
		public override string GetLevelText(int Level)
		{
			return "Duration: {{rules|" + this.GetDuration(Level).ToString() + "}} rounds\n" + "Cooldown: {{rules|" + this.GetBaseCooldown(Level).ToString() + "}} rounds";
		}

		// Token: 0x0600CB45 RID: 52037 RVA: 0x0049E85D File Offset: 0x0049CA5D
		public override void CollectStats(Templates.StatCollector stats, int Level)
		{
			stats.Set("Duration", this.GetDuration(Level), false, 0);
			stats.CollectCooldownTurns(base.MyActivatedAbility(this.PhaseOutActivatedAbilityID, this.ParentObject), this.GetBaseCooldown(Level), 0);
			base.CollectStats(stats, Level);
		}

		// Token: 0x0600CB46 RID: 52038 RVA: 0x0049E89B File Offset: 0x0049CA9B
		public void SyncAbilities()
		{
			ActivatedAbilityEntry activatedAbility = this.ParentObject.GetActivatedAbility(this.PhaseOutActivatedAbilityID);
			activatedAbility.ToggleState = this.ParentObject.HasEffect<Phased>();
			activatedAbility.Visible = true;
		}

		// Token: 0x0600CB47 RID: 52039 RVA: 0x0049E8C5 File Offset: 0x0049CAC5
		public bool IsPhased()
		{
			GameObject parentObject = this.ParentObject;
			return parentObject != null && parentObject.HasEffect<Phased>();
		}

		// Token: 0x0600CB48 RID: 52040 RVA: 0x0049E8D8 File Offset: 0x0049CAD8
		public override bool FireEvent(Event E)
		{
			if (E.ID == "AfterPhaseIn")
			{
				this.SyncAbilities();
			}
			else if (E.ID == "BeginTakeAction")
			{
				this.SyncAbilities();
			}
			else
			{
				if (E.ID == "CommandTogglePhase")
				{
					return this.ParentObject.FireEvent(Event.New(this.IsPhased() ? "CommandPhaseIn" : "CommandPhaseOut", 0, 0, 0));
				}
				if (E.ID == "CommandPhaseOut")
				{
					if (this.ParentObject.OnWorldMap())
					{
						if (this.ParentObject.IsPlayer())
						{
							Popup.ShowFail("You cannot do that on the world map.", true, true, true);
						}
						return false;
					}
					Event @event = Event.New("InitiateRealityDistortionLocal", "Object", this.ParentObject, "Mutation", this);
					if (!this.ParentObject.FireEvent(@event, E))
					{
						return false;
					}
					this.ParentObject.PlayWorldSound("Sounds/Abilities/sfx_ability_mutation_phase", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
					this.ParentObject.ApplyEffect(new Phased(6 + base.Level + 1), null);
					base.CooldownMyActivatedAbility(this.PhaseOutActivatedAbilityID, 103 - 3 * base.Level, null, null, false);
					this.SyncAbilities();
				}
				else if (E.ID == "CommandPhaseIn")
				{
					this.ParentObject.RemoveEffect<Phased>();
					this.SyncAbilities();
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600CB49 RID: 52041 RVA: 0x0049EA5D File Offset: 0x0049CC5D
		public override bool ChangeLevel(int NewLevel)
		{
			return base.ChangeLevel(NewLevel);
		}

		// Token: 0x0600CB4A RID: 52042 RVA: 0x0049EA68 File Offset: 0x0049CC68
		public override bool Mutate(GameObject GO, int Level)
		{
			this.PhaseOutActivatedAbilityID = base.AddMyActivatedAbility("Phase", "CommandTogglePhase", "Physical Mutations", null, "°", null, true, false, true, false, true, false, false, false, true, true, false, -1, null, null, null, null, null, null);
			this.SyncAbilities();
			return base.Mutate(GO, Level);
		}

		// Token: 0x0600CB4B RID: 52043 RVA: 0x0049EAB7 File Offset: 0x0049CCB7
		public override bool Unmutate(GameObject GO)
		{
			base.RemoveMyActivatedAbility(ref this.PhaseOutActivatedAbilityID, null);
			return base.Unmutate(GO);
		}

		// Token: 0x04004F0C RID: 20236
		public Guid PhaseOutActivatedAbilityID = Guid.Empty;
	}
}
