using System;
using System.Collections.Generic;
using System.Linq;
using XRL.Rules;
using XRL.World.Capabilities;
using XRL.World.Effects;

namespace XRL.World.Parts.Skill
{
	/// This part is not used in the base game.
	// Token: 0x020013A5 RID: 5029
	[Serializable]
	public class Tactics_DeathFromAbove : BaseSkill
	{
		// Token: 0x0600C263 RID: 49763 RVA: 0x0046FFC9 File Offset: 0x0046E1C9
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AIGetOffensiveAbilityListEvent.ID || ID == PooledEvent<CommandEvent>.ID || ID == GetAttackerMeleePenetrationEvent.ID || ID == GetMovementCapabilitiesEvent.ID;
		}

		// Token: 0x0600C264 RID: 49764 RVA: 0x0046FFF7 File Offset: 0x0046E1F7
		public override bool HandleEvent(GetMovementCapabilitiesEvent E)
		{
			E.Add("Death From Above", Tactics_DeathFromAbove.COMMAND_NAME, 8500, base.MyActivatedAbility(this.ActivatedAbilityID, null), true);
			return base.HandleEvent(E);
		}

		// Token: 0x0600C265 RID: 49765 RVA: 0x00470024 File Offset: 0x0046E224
		public override bool HandleEvent(AIGetOffensiveAbilityListEvent E)
		{
			if (GameObject.Validate(E.Target) && base.IsMyActivatedAbilityAIUsable(this.ActivatedAbilityID, null) && E.Actor.CanChangeBodyPosition(null, false, false, false, false) && E.Actor.CanChangeMovementMode(null, false, false, false, false) && !E.Actor.IsOverburdened())
			{
				bool flag = false;
				if (E.Actor.IsFlying)
				{
					if (E.Distance <= 1)
					{
						flag = true;
					}
				}
				else
				{
					int num;
					int num2;
					string text;
					string text2;
					string text3;
					bool flag2;
					GetJumpingBehaviorEvent.Retrieve(E.Actor, out num, out num2, out text, out text2, out text3, out flag2, null);
					int num3 = Acrobatics_Jump.GetBaseRange(E.Actor) + num;
					if (E.Distance >= num2 && E.Distance <= num3 && Acrobatics_Jump.CheckPath(E.Actor, E.Target.CurrentCell, flag2, true, true, "jump"))
					{
						flag = true;
					}
				}
				if (flag)
				{
					E.Add(Tactics_DeathFromAbove.COMMAND_NAME, 1, null, false, false, E.Target, null);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C266 RID: 49766 RVA: 0x00470128 File Offset: 0x0046E328
		public override bool HandleEvent(GetAttackerMeleePenetrationEvent E)
		{
			string properties = E.Properties;
			if (properties != null && properties.HasDelimitedSubstring(',', "DeathFromAbove", StringComparison.Ordinal))
			{
				E.PenetrationBonus += 2;
				E.MaxPenetrationBonus += 2;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C267 RID: 49767 RVA: 0x00470174 File Offset: 0x0046E374
		public override bool HandleEvent(GetMeleeAttackChanceEvent E)
		{
			if (E.Properties.HasDelimitedSubstring(',', "DeathFromAbove", StringComparison.Ordinal))
			{
				E.SetFinalizedChance((E.Intrinsic && !E.Consecutive) ? 100 : 0);
				return false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C268 RID: 49768 RVA: 0x004701B0 File Offset: 0x0046E3B0
		public override bool HandleEvent(CommandEvent E)
		{
			if (E.Command == Tactics_DeathFromAbove.COMMAND_NAME)
			{
				if (!Tactics_DeathFromAbove.PerformDeathFromAbove(this.ParentObject, E.Target, null))
				{
					return false;
				}
				base.CooldownMyActivatedAbility(this.ActivatedAbilityID, 40, null, null, false);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C269 RID: 49769 RVA: 0x00470200 File Offset: 0x0046E400
		private static bool ValidDeathFromAboveTarget(GameObject obj)
		{
			return obj != null && obj.IsCombat;
		}

		// Token: 0x0600C26A RID: 49770 RVA: 0x0047020D File Offset: 0x0046E40D
		public int GetMinimumRange()
		{
			return 2;
		}

		// Token: 0x0600C26B RID: 49771 RVA: 0x00470210 File Offset: 0x0046E410
		public int GetMaximumRange()
		{
			return 3 + this.ParentObject.GetIntProperty("ChargeRangeModifier", 0);
		}

		// Token: 0x0600C26C RID: 49772 RVA: 0x00470228 File Offset: 0x0046E428
		public static bool PerformDeathFromAbove(GameObject Actor, GameObject Target = null, string SourceKey = null)
		{
			if (Actor.OnWorldMap())
			{
				Actor.Fail("You cannot perform Death From Above on the world map.");
				return false;
			}
			if (Actor.IsOverburdened())
			{
				Actor.Fail("You cannot perform Death From Above while overburdened.");
				return false;
			}
			if (!Actor.CanChangeBodyPosition("Charging", true, false, false, false))
			{
				return false;
			}
			if (!Actor.CanChangeMovementMode("Charging", true, false, false, false))
			{
				return false;
			}
			bool isFlying = Actor.IsFlying;
			int num = 0;
			int num2 = 1;
			bool flag = false;
			string text = null;
			string text2 = null;
			string text3 = null;
			if (!isFlying)
			{
				int num3;
				GetJumpingBehaviorEvent.Retrieve(Actor, out num3, out num, out text, out text2, out text3, out flag, null);
				num2 = Acrobatics_Jump.GetBaseRange(Actor) + num3;
			}
			Cell cell = null;
			if (Target == null)
			{
				if (Actor.IsPlayer())
				{
					if (isFlying)
					{
						cell = Actor.Physics.PickDirection(true, null, null);
					}
					else
					{
						List<Cell> list = Actor.Physics.PickLine(num2 + 1, AllowVis.OnlyVisible, new Predicate<GameObject>(Tactics_DeathFromAbove.ValidDeathFromAboveTarget), false, true, true, false, null, null, "Death From Above", true, true);
						cell = ((list != null) ? list.Last<Cell>() : null);
					}
					if (cell == null)
					{
						return false;
					}
					Target = cell.GetCombatTarget(Actor, true, false, true, 0, null, null, null, null, null, true, true, null);
					if (Target == null)
					{
						Actor.Fail("There is nobody there to perform Death From Above on.");
						return false;
					}
				}
				else
				{
					Target = Actor.Target;
				}
				if (Target == null)
				{
					return false;
				}
			}
			cell = Target.CurrentCell;
			if (cell == null)
			{
				return false;
			}
			int num4 = Actor.DistanceTo(Target);
			List<Point> list2 = null;
			if (isFlying)
			{
				if (num4 > 1)
				{
					Actor.Fail("While flying, you can only perform Death From Above on nearby targets.");
					return false;
				}
			}
			else
			{
				if (num4 < num)
				{
					Actor.Fail("To perform Death From Above from the ground, you must select a target at least " + num.Things("square", null) + " away.");
					return false;
				}
				if (num4 > num2)
				{
					Actor.Fail("To perform Death From Above from the ground, you must select a target no more than " + num2.Things("square", null) + " away.");
					return false;
				}
				if (!Acrobatics_Jump.CheckPath(Actor, cell, out list2, false, flag, true, "jump"))
				{
					return false;
				}
			}
			if (Target == Actor)
			{
				Actor.Fail("You cannot perform Death From Above on " + Actor.itself + ".");
				return false;
			}
			if (!Target.IsCombatObject())
			{
				if (Target.IsWall())
				{
					Actor.Fail("You cannot perform Death From Above on a wall.");
					return false;
				}
				if (Target.IsDoor())
				{
					Actor.Fail("You cannot perform Death From Above on a door.");
					return false;
				}
			}
			Cell currentCell = Actor.CurrentCell;
			IComponent<GameObject>.XDidYToZ(Actor, isFlying ? "dive" : (text2 ?? "leap"), "at", Target, null, "!", null, null, null, Target, false, false, false, false, false, null, null, null, false, false, false, false, false, null);
			Acrobatics_Jump.PlayAnimation(Actor, cell);
			bool flag2 = false;
			if (!isFlying)
			{
				Cell cell2 = null;
				Point point = null;
				if (list2 != null && list2.Count >= 2)
				{
					point = list2[list2.Count - 2];
				}
				if (point != null)
				{
					cell2 = cell.ParentZone.GetCell(point.X, point.Y);
				}
				if (cell2 == null)
				{
					cell2 = cell;
				}
				bool terse = The.Game.Player.Messages.Terse;
				try
				{
					if (!terse)
					{
						The.Game.Player.Messages.Terse = true;
					}
					flag2 = Actor.DirectMoveTo(cell2, 0, false, true, true, null);
				}
				catch (Exception ex)
				{
					MetricsManager.LogError("DeathFromAbove tempCell move", ex);
				}
				finally
				{
					if (!terse)
					{
						The.Game.Player.Messages.Terse = false;
					}
				}
			}
			bool isFlying2 = Target.IsFlying;
			if (Actor.InSameOrAdjacentCellTo(Target))
			{
				Combat.PerformMeleeAttack(Actor, Target, 0, 0, 0, 0, "Charging,DeathFromAbove", true);
				Actor.FireEvent(Event.New("ChargedTarget", "Defender", Target).SetFlag("DeathFromAbove", true));
				Target.FireEvent(Event.New("WasCharged", "Attacker", Actor).SetFlag("DeathFromAbove", true));
			}
			object obj;
			if ((obj = ((GameObject.Validate(Target) && !Target.IsNowhere()) ? null : cell)) == null && (obj = cell.GetRandomLocalAdjacentCell((Cell c) => c.IsEmptyOfSolidFor(Actor, true))) == null && (obj = cell.GetRandomLocalAdjacentCell(2, (Cell c) => c.IsEmptyOfSolidFor(Actor, true), false)) == null)
			{
				obj = cell.GetRandomLocalAdjacentCell() ?? cell;
			}
			Cell cell3 = obj;
			Actor.DirectMoveTo(cell3, 0, true, true, false, null);
			if (isFlying)
			{
				Flight.Land(Actor, true);
			}
			if (flag2)
			{
				JumpedEvent.Send(Actor, currentCell, cell, list2, num2, text, text3, SourceKey);
			}
			Actor.PlayWorldSound("Sounds/Foley/fly_generic_fall", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
			if (!Actor.HasEffect<Prone>() && !Actor.MakeSave("Agility", 20, null, null, "DeathFromAbove Knockdown", false, false, false, false, Actor))
			{
				Actor.ApplyEffect(new Prone(), null);
			}
			if (GameObject.Validate(Target) && !Target.IsNowhere())
			{
				if (isFlying2 && Actor.PhaseMatches(Target))
				{
					Target.ApplyEffect(new Grounded(Stat.Random(3, 5)), null);
				}
				if (!Target.HasEffect<Prone>() && !Target.MakeSave("Agility", 20, null, null, "DeathFromAbove Knockdown", false, false, false, false, Actor))
				{
					Target.ApplyEffect(new Prone(), null);
				}
			}
			Actor.UseEnergy(1000, "DeathFromAbove", null, null, false);
			return true;
		}

		// Token: 0x0600C26D RID: 49773 RVA: 0x00470820 File Offset: 0x0046EA20
		public override bool AddSkill(GameObject GO)
		{
			this.ActivatedAbilityID = base.AddMyActivatedAbility("Death From Above", Tactics_DeathFromAbove.COMMAND_NAME, "Skills", null, "\u0019", null, false, false, false, true, false, false, false, false, true, true, false, -1, null, null, null, null, null, null);
			return true;
		}

		// Token: 0x0600C26E RID: 49774 RVA: 0x00470862 File Offset: 0x0046EA62
		public override bool RemoveSkill(GameObject GO)
		{
			base.RemoveMyActivatedAbility(ref this.ActivatedAbilityID, null);
			return true;
		}

		// Token: 0x04004D9D RID: 19869
		public static readonly string COMMAND_NAME = "DeathFromAbove";

		// Token: 0x04004D9E RID: 19870
		public Guid ActivatedAbilityID = Guid.Empty;
	}
}
