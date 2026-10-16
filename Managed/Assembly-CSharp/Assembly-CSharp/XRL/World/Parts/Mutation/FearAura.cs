using System;
using System.Collections.Generic;
using XRL.Rules;
using XRL.World.Capabilities;
using XRL.World.Effects;

namespace XRL.World.Parts.Mutation
{
	// Token: 0x02001418 RID: 5144
	[Serializable]
	public class FearAura : BaseMutation
	{
		// Token: 0x0600C7F7 RID: 51191 RVA: 0x0048CBEE File Offset: 0x0048ADEE
		public override string GetDescription()
		{
			return "You scare creatures around you.";
		}

		// Token: 0x0600C7F8 RID: 51192 RVA: 0x0048CBF8 File Offset: 0x0048ADF8
		public override string GetLevelText(int Level)
		{
			return "You cause adjacent creatures to flee in terror.\n" + "Cooldown: " + this.GetCooldown(Level).ToString() + " rounds" + "Additionally, you sometimes scare creatures passively.";
		}

		// Token: 0x0600C7F9 RID: 51193 RVA: 0x0048CC32 File Offset: 0x0048AE32
		public int GetCooldown(int Level)
		{
			return 40;
		}

		// Token: 0x0600C7FA RID: 51194 RVA: 0x0048CC36 File Offset: 0x0048AE36
		public override void CollectStats(Templates.StatCollector stats, int Level)
		{
			stats.CollectCooldownTurns(base.MyActivatedAbility(this.ActivatedAbilityID, null), this.GetCooldown(Level), 0);
		}

		// Token: 0x0600C7FB RID: 51195 RVA: 0x0048CC53 File Offset: 0x0048AE53
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == SingletonEvent<BeginTakeActionEvent>.ID || ID == PooledEvent<CommandEvent>.ID;
		}

		// Token: 0x0600C7FC RID: 51196 RVA: 0x0048CC74 File Offset: 0x0048AE74
		public override bool HandleEvent(BeginTakeActionEvent E)
		{
			GameObject parentObject = this.ParentObject;
			bool? flag;
			if (parentObject == null)
			{
				flag = null;
			}
			else
			{
				Zone currentZone = parentObject.CurrentZone;
				flag = ((currentZone != null) ? new bool?(currentZone.IsWorldMap()) : null);
			}
			bool? flag2 = flag;
			if (flag2.GetValueOrDefault())
			{
				return true;
			}
			this.Cooldown--;
			if (this.Cooldown < 0 && base.IsMyActivatedAbilityVoluntarilyUsable(this.ActivatedAbilityID, null) && this.Chance.in100())
			{
				if (!FearAura.PulseAura(this.ParentObject))
				{
					return false;
				}
				this.Cooldown = Stat.Random(21, 26);
			}
			if (this.EnergyCost > 0 && !this.ParentObject.IsPlayer())
			{
				base.UseEnergy(this.EnergyCost);
				return false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C7FD RID: 51197 RVA: 0x0048CD3C File Offset: 0x0048AF3C
		public override bool HandleEvent(CommandEvent E)
		{
			if (E.Command == FearAura.COMMAND_NAME && base.IsMyActivatedAbilityVoluntarilyUsable(this.ActivatedAbilityID, null))
			{
				if (FearAura.PulseAura(this.ParentObject) && this.EnergyCost > 0 && !this.ParentObject.IsPlayer())
				{
					base.UseEnergy(this.EnergyCost);
				}
				base.CooldownMyActivatedAbility(this.ActivatedAbilityID, this.GetCooldown(base.Level), null, null, false);
				base.DidX("assume", "a menacing pose", null, null, null, this.ParentObject, null, false, false, null, null, false, false, false, false, false, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C7FE RID: 51198 RVA: 0x0048CDE0 File Offset: 0x0048AFE0
		public static bool PulseAura(GameObject Actor)
		{
			List<Cell> list;
			if (Actor == null)
			{
				list = null;
			}
			else
			{
				Cell currentCell = Actor.CurrentCell;
				list = ((currentCell != null) ? currentCell.GetLocalAdjacentCells() : null);
			}
			List<Cell> list2 = list;
			if (list2 == null)
			{
				return false;
			}
			bool flag = false;
			foreach (Cell cell in list2)
			{
				foreach (GameObject gameObject in cell.Objects)
				{
					if (gameObject.Brain != null)
					{
						Mental.PerformAttack(new Mental.Attack(FearAura.ApplyFear), Actor, gameObject, null, "Terrify Aura", "1d8+4", 8388609, "1d3".RollCached(), int.MinValue, 0, 0);
						flag = true;
					}
				}
			}
			if (flag)
			{
				Actor.ParticleBlip("&W!", 10, 0L, false);
			}
			return flag;
		}

		// Token: 0x0600C7FF RID: 51199 RVA: 0x0048CED8 File Offset: 0x0048B0D8
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600C800 RID: 51200 RVA: 0x0048CEDC File Offset: 0x0048B0DC
		public static bool ApplyFear(MentalAttackEvent E)
		{
			if (!E.Defender.FireEvent("CanApplyFear"))
			{
				return false;
			}
			if (!CanApplyEffectEvent.Check<Terrified>(E.Defender, 0))
			{
				return false;
			}
			if (E.Penetrations > 0)
			{
				Terrified terrified = new Terrified(E.Magnitude, E.Attacker, true, false);
				if (E.Defender.ApplyEffect(terrified, null))
				{
					return true;
				}
			}
			if (E.Defender.IsPlayer())
			{
				IComponent<GameObject>.AddPlayerMessage("You feel uneasy.", 'K', true);
			}
			return false;
		}

		// Token: 0x0600C801 RID: 51201 RVA: 0x0048CF58 File Offset: 0x0048B158
		public override bool Mutate(GameObject GO, int Level)
		{
			this.ActivatedAbilityID = base.AddMyActivatedAbility("Fear Aura", FearAura.COMMAND_NAME, "Physical Mutations", null, "®", null, false, false, false, false, false, false, false, false, true, true, false, -1, null, null, null, null, null, null);
			return base.Mutate(GO, Level);
		}

		// Token: 0x0600C802 RID: 51202 RVA: 0x0048CFA1 File Offset: 0x0048B1A1
		public override bool Unmutate(GameObject GO)
		{
			base.RemoveMyActivatedAbility(ref this.ActivatedAbilityID, null);
			return base.Unmutate(GO);
		}

		// Token: 0x04004E6A RID: 20074
		public static readonly string COMMAND_NAME = "CommandFearAura";

		// Token: 0x04004E6B RID: 20075
		public int Chance = 25;

		// Token: 0x04004E6C RID: 20076
		public int EnergyCost = 1000;

		// Token: 0x04004E6D RID: 20077
		public int SpinTimer;

		// Token: 0x04004E6E RID: 20078
		public int Cooldown;
	}
}
