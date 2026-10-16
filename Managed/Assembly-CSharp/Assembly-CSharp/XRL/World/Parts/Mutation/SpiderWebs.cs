using System;
using XRL.World.Effects;

namespace XRL.World.Parts.Mutation
{
	// Token: 0x02001424 RID: 5156
	[Serializable]
	public class SpiderWebs : BaseMutation
	{
		// Token: 0x0600C8E1 RID: 51425 RVA: 0x00492FBE File Offset: 0x004911BE
		public override void CollectStats(Templates.StatCollector stats, int Level)
		{
			stats.Set("SpinDuration", SpiderWebs.DURATION, false, 0);
			stats.CollectCooldownTurns(base.MyActivatedAbility(this.ActivatedAbilityID, null), SpiderWebs.COOLDOWN, 0);
		}

		// Token: 0x0600C8E2 RID: 51426 RVA: 0x00492FEB File Offset: 0x004911EB
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AIGetMovementAbilityListEvent.ID || ID == AIGetOffensiveAbilityListEvent.ID || ID == LeftCellEvent.ID;
		}

		// Token: 0x0600C8E3 RID: 51427 RVA: 0x00493011 File Offset: 0x00491211
		public override bool HandleEvent(AIGetMovementAbilityListEvent E)
		{
			if (base.IsMyActivatedAbilityAIUsable(this.ActivatedAbilityID, null))
			{
				E.Add("CommandSpinSimpleWeb", 1, null, false, false, null, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C8E4 RID: 51428 RVA: 0x0049303A File Offset: 0x0049123A
		public override bool HandleEvent(AIGetOffensiveAbilityListEvent E)
		{
			if (E.Distance > 1 && base.IsMyActivatedAbilityAIUsable(this.ActivatedAbilityID, null))
			{
				E.Add("CommandSpinSimpleWeb", 1, null, false, false, null, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C8E5 RID: 51429 RVA: 0x0049306C File Offset: 0x0049126C
		public override bool HandleEvent(LeftCellEvent E)
		{
			if (base.IsMyActivatedAbilityToggledOn(this.ActivatedAbilityID, null))
			{
				if (E.Cell.OnWorldMap())
				{
					this.SpinTimer = -1;
				}
				else
				{
					this.SpinTimer--;
				}
				if (this.SpinTimer < 0)
				{
					base.ToggleMyActivatedAbility(this.ActivatedAbilityID, null, false, null);
				}
				else
				{
					GameObject gameObject;
					if (this.ParentObject.HasEffect<Phased>())
					{
						gameObject = GameObject.Create("PhaseWeb");
					}
					else
					{
						gameObject = GameObject.Create("Web");
					}
					gameObject.GetPart<Sticky>().SaveTarget = 15 + base.Level;
					E.Cell.AddObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
					base.DidX("spin", gameObject.a + gameObject.ShortDisplayName, null, null, null, null, null, false, false, null, null, false, false, false, false, false, null);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C8E6 RID: 51430 RVA: 0x0049315E File Offset: 0x0049135E
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("ApplyStuck");
			Registrar.Register("CanApplyStuck");
			Registrar.Register("CommandSpinSimpleWeb");
			Registrar.Register("VillageInit");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600C8E7 RID: 51431 RVA: 0x00493194 File Offset: 0x00491394
		public override string GetLevelText(int Level)
		{
			return "You bear two spinnerets with which you spin a sticky silk.\n";
		}

		// Token: 0x0600C8E8 RID: 51432 RVA: 0x0049319C File Offset: 0x0049139C
		public override bool FireEvent(Event E)
		{
			if (E.ID == "ApplyStuck" || E.ID == "CanApplyStuck")
			{
				return false;
			}
			if (E.ID == "CommandSpinSimpleWeb")
			{
				base.ToggleMyActivatedAbility(this.ActivatedAbilityID, null, false, null);
				if (base.IsMyActivatedAbilityToggledOn(this.ActivatedAbilityID, null))
				{
					this.SpinTimer = SpiderWebs.DURATION;
				}
				base.UseEnergy(1000, "Physical Mutation Spin Web");
				base.CooldownMyActivatedAbility(this.ActivatedAbilityID, SpiderWebs.COOLDOWN, null, null, false);
			}
			else if (E.ID == "VillageInit")
			{
				this.Active = false;
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600C8E9 RID: 51433 RVA: 0x0049325A File Offset: 0x0049145A
		public override bool ChangeLevel(int NewLevel)
		{
			return base.ChangeLevel(NewLevel);
		}

		// Token: 0x0600C8EA RID: 51434 RVA: 0x00493264 File Offset: 0x00491464
		public override bool Mutate(GameObject GO, int Level)
		{
			this.ActivatedAbilityID = base.AddMyActivatedAbility("Spin Webs", "CommandSpinSimpleWeb", "Physical Mutations", null, "#", null, false, false, true, false, false, false, false, false, true, true, false, -1, null, null, null, null, null, null);
			return base.Mutate(GO, Level);
		}

		// Token: 0x0600C8EB RID: 51435 RVA: 0x004932AD File Offset: 0x004914AD
		public override bool Unmutate(GameObject GO)
		{
			base.RemoveMyActivatedAbility(ref this.ActivatedAbilityID, null);
			return base.Unmutate(GO);
		}

		// Token: 0x04004E9D RID: 20125
		public int SpinTimer;

		// Token: 0x04004E9E RID: 20126
		public bool Active;

		// Token: 0x04004E9F RID: 20127
		public static readonly int COOLDOWN = 50;

		// Token: 0x04004EA0 RID: 20128
		public static readonly int DURATION = 4;
	}
}
