using System;
using System.Collections.Generic;
using XRL.Language;
using XRL.UI;
using XRL.World.Effects;

namespace XRL.World.Parts.Skill
{
	// Token: 0x020013A4 RID: 5028
	[Serializable]
	public class Tactics_Charge : BaseSkill
	{
		// Token: 0x0600C253 RID: 49747 RVA: 0x0046F2AE File Offset: 0x0046D4AE
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AIGetOffensiveAbilityListEvent.ID || ID == SingletonEvent<BeforeAbilityManagerOpenEvent>.ID || ID == PooledEvent<CommandEvent>.ID || ID == EnteredCellEvent.ID || ID == GetAttackerMeleePenetrationEvent.ID || ID == GetMovementCapabilitiesEvent.ID;
		}

		// Token: 0x0600C254 RID: 49748 RVA: 0x0046F2EC File Offset: 0x0046D4EC
		public override bool HandleEvent(GetMovementCapabilitiesEvent E)
		{
			E.Add("Charge", Tactics_Charge.COMMAND_NAME, 8000, base.MyActivatedAbility(this.ActivatedAbilityID, null), true);
			return base.HandleEvent(E);
		}

		// Token: 0x0600C255 RID: 49749 RVA: 0x0046F318 File Offset: 0x0046D518
		public override bool HandleEvent(CommandEvent E)
		{
			return (!(E.Command == Tactics_Charge.COMMAND_NAME) || this.PerformCharge()) && base.HandleEvent(E);
		}

		// Token: 0x0600C256 RID: 49750 RVA: 0x0046F340 File Offset: 0x0046D540
		public void CollectStats(Templates.StatCollector stats)
		{
			GetPropertyModDescription.GetFor(this.ParentObject, "ChargeRangeModifier", "Range", stats);
			int num = stats.CollectBonusModifiers("Range", 0, null, true);
			stats.Set("Range", this.GetMinimumRange().ToString() + "-" + this.GetMaximumRange().ToString(), num != 0, num);
			stats.CollectCooldownTurns(base.MyActivatedAbility(this.ActivatedAbilityID, null), Tactics_Charge.COOLDOWN, 0);
		}

		// Token: 0x0600C257 RID: 49751 RVA: 0x0046F3C2 File Offset: 0x0046D5C2
		public override bool HandleEvent(BeforeAbilityManagerOpenEvent E)
		{
			base.DescribeMyActivatedAbility(this.ActivatedAbilityID, new Action<Templates.StatCollector>(this.CollectStats), null);
			return base.HandleEvent(E);
		}

		// Token: 0x0600C258 RID: 49752 RVA: 0x0046F3E8 File Offset: 0x0046D5E8
		public override bool HandleEvent(AIGetOffensiveAbilityListEvent E)
		{
			int minimumRange = this.GetMinimumRange();
			int maximumRange = this.GetMaximumRange();
			if (E.Distance >= minimumRange && E.Distance <= maximumRange + 1 && GameObject.Validate(E.Target) && base.IsMyActivatedAbilityAIUsable(this.ActivatedAbilityID, null) && !E.Actor.IsFlying && E.Actor.CanChangeBodyPosition(null, false, false, false, false) && E.Actor.CanChangeMovementMode(null, false, false, false, false) && !E.Actor.HasEffect<Terrified>() && !E.Actor.IsOverburdened() && !E.Actor.AreViableHostilesAdjacent(false, false))
			{
				List<Cell> list = base.PickLine(maximumRange + 1, AllowVis.OnlyVisible, new Predicate<GameObject>(this.ValidChargeTarget), false, false, true, false, null, null, null, false, true);
				if (list != null)
				{
					int num = list.Count - 1;
					if (num >= minimumRange && num <= maximumRange)
					{
						int num2 = 268435456;
						int i = 0;
						int count = list.Count;
						while (i < count)
						{
							Cell cell = list[i];
							GameObject combatTarget = cell.GetCombatTarget(E.Actor, false, false, false, 5, null, null, null, null, null, true, true, null);
							if (combatTarget == E.Target)
							{
								E.Add(Tactics_Charge.COMMAND_NAME, 1, null, false, false, null, null);
								break;
							}
							if (combatTarget != null && !E.Actor.IsHostileTowards(combatTarget))
							{
								break;
							}
							int num3 = cell.NavigationWeight(E.Actor, ref num2);
							if (num3 > 40 && (i == count - 1 || num3 > 80))
							{
								break;
							}
							i++;
						}
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C259 RID: 49753 RVA: 0x0046F58F File Offset: 0x0046D78F
		public override bool HandleEvent(EnteredCellEvent E)
		{
			if (E.Forced && E.Type != "Teleporting")
			{
				this.ForcedMoveDirection = E.Direction ?? "?";
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C25A RID: 49754 RVA: 0x0046F5C8 File Offset: 0x0046D7C8
		public override bool HandleEvent(GetAttackerMeleePenetrationEvent E)
		{
			if (!E.Properties.IsNullOrEmpty() && E.Hand == "Primary" && E.Properties.HasDelimitedSubstring(',', "Charging", StringComparison.Ordinal) && !E.Properties.HasDelimitedSubstring(',', "DeathFromAbove", StringComparison.Ordinal))
			{
				E.PenetrationBonus++;
				E.MaxPenetrationBonus++;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C25B RID: 49755 RVA: 0x0046F641 File Offset: 0x0046D841
		private bool ValidChargeTarget(GameObject obj)
		{
			return obj != null && obj.IsCombat && obj.FlightMatches(this.ParentObject);
		}

		// Token: 0x0600C25C RID: 49756 RVA: 0x0046F65C File Offset: 0x0046D85C
		public int GetMinimumRange()
		{
			return 2;
		}

		// Token: 0x0600C25D RID: 49757 RVA: 0x0046F65F File Offset: 0x0046D85F
		public int GetMaximumRange()
		{
			return 3 + this.ParentObject.GetIntProperty("ChargeRangeModifier", 0);
		}

		// Token: 0x0600C25E RID: 49758 RVA: 0x0046F674 File Offset: 0x0046D874
		public bool PerformCharge()
		{
			if (this.ParentObject.OnWorldMap())
			{
				this.ParentObject.Fail("You cannot charge on the world map.");
				return false;
			}
			if (this.ParentObject.IsFlying)
			{
				this.ParentObject.Fail("You cannot charge while flying.");
				return false;
			}
			if (this.ParentObject.IsOverburdened())
			{
				this.ParentObject.Fail("You cannot charge while overburdened.");
				return false;
			}
			if (!this.ParentObject.CanChangeBodyPosition("Charging", true, false, false, false))
			{
				return false;
			}
			if (!this.ParentObject.CanChangeMovementMode("Charging", true, false, false, false))
			{
				return false;
			}
			int minimumRange = this.GetMinimumRange();
			int maximumRange = this.GetMaximumRange();
			List<Cell> list = base.PickLine(maximumRange + 1, AllowVis.OnlyVisible, new Predicate<GameObject>(this.ValidChargeTarget), false, true, true, false, null, null, "Charge", true, true);
			if (list == null || list.Count <= 0)
			{
				return false;
			}
			if (this.ParentObject.IsPlayer())
			{
				list.RemoveAt(0);
			}
			int num = list.Count - 1;
			if (num < minimumRange)
			{
				this.ParentObject.Fail(string.Concat(new string[]
				{
					"You must charge at least ",
					Grammar.Cardinal(minimumRange),
					" ",
					(minimumRange == 1) ? "space" : "spaces",
					"."
				}));
				return false;
			}
			if (num > maximumRange)
			{
				this.ParentObject.Fail(string.Concat(new string[]
				{
					"You can't charge more than ",
					Grammar.Cardinal(maximumRange),
					" ",
					(maximumRange == 1) ? "space" : "spaces",
					"."
				}));
				return false;
			}
			if (this.ParentObject.AreViableHostilesAdjacent(false, false))
			{
				this.ParentObject.Fail("You cannot charge while in melee combat.");
				return false;
			}
			Cell cell = list[list.Count - 1];
			GameObject gameObject;
			if (this.ParentObject.IsPlayer())
			{
				gameObject = cell.GetCombatTarget(this.ParentObject, false, false, false, 5, null, null, null, null, null, true, false, null);
			}
			else
			{
				gameObject = this.ParentObject.Target;
			}
			if (gameObject == null)
			{
				if (base.IsPlayer())
				{
					gameObject = cell.GetCombatTarget(this.ParentObject, true, false, false, 5, null, null, null, null, null, true, false, null);
					if (gameObject != null)
					{
						this.ParentObject.Fail("You cannot charge a flying target.");
					}
					else
					{
						this.ParentObject.Fail("You must charge at a target!");
					}
				}
				return false;
			}
			string text = null;
			string text2 = null;
			string text3 = null;
			string text4 = null;
			int num2 = 10;
			Disguised effect = this.ParentObject.GetEffect<Disguised>();
			if (effect != null)
			{
				if (!effect.Tile.IsNullOrEmpty() && Options.UseTiles)
				{
					text2 = effect.Tile;
					if (!effect.TileColor.IsNullOrEmpty())
					{
						text3 = effect.TileColor;
					}
					else
					{
						text3 = effect.ColorString;
					}
					text4 = effect.DetailColor;
				}
				else
				{
					text = effect.ColorString + effect.RenderString;
				}
			}
			else if (!this.ParentObject.Render.Tile.IsNullOrEmpty() && Options.UseTiles)
			{
				text2 = this.ParentObject.Render.Tile;
				if (!this.ParentObject.Render.TileColor.IsNullOrEmpty())
				{
					text3 = this.ParentObject.Render.TileColor;
				}
				else
				{
					text3 = this.ParentObject.Render.ColorString;
				}
				text4 = this.ParentObject.Render.DetailColor;
			}
			else
			{
				text = this.ParentObject.Render.ColorString + this.ParentObject.Render.RenderString;
			}
			if (base.Visible())
			{
				if (text2 != null)
				{
					this.ParentObject.TileParticleBlip(text2, text3, text4, num2, false, this.ParentObject.Render.getHFlip(), this.ParentObject.Render.getVFlip(), 0L);
				}
				else
				{
					this.ParentObject.ParticleBlip(text, num2, 0L, false);
				}
			}
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			bool flag4 = false;
			bool flag5 = false;
			Cell cell2 = this.ParentObject.CurrentCell;
			string text5 = null;
			List<string> list2 = new List<string>(maximumRange + 2);
			int i = 0;
			int num3 = maximumRange + 2;
			while (i < num3)
			{
				if (i >= list.Count)
				{
					list2.Add(text5);
				}
				else
				{
					Cell cell3 = list[i];
					string directionFromCell = cell2.GetDirectionFromCell(cell3, false);
					list2.Add(directionFromCell);
					text5 = directionFromCell;
					cell2 = cell3;
				}
				i++;
			}
			bool flag6 = this.ParentObject.HasPart<Robot>();
			GameObject gameObject2 = null;
			int j = 0;
			int count = list2.Count;
			while (j < count)
			{
				string text6 = list2[j];
				Cell cellFromDirection = this.ParentObject.CurrentCell.GetCellFromDirection(text6, false);
				if (cellFromDirection == null)
				{
					break;
				}
				if (flag6)
				{
					gameObject2 = cellFromDirection.GetFirstObjectWithPropertyOrTag("RobotStop");
					if (gameObject2 != null)
					{
						break;
					}
				}
				bool flag7 = cellFromDirection.Objects.Contains(gameObject);
				GameObject combatTarget = cellFromDirection.GetCombatTarget(this.ParentObject, false, false, false, 0, null, null, null, null, null, true, true, null);
				if (combatTarget != null)
				{
					base.DidXToY("charge", combatTarget, null, "!", null, null, null, combatTarget.IsPlayer() ? combatTarget : null, false, false, false, false, false, null, null, null, false, false, false, false, false, null);
					if (this.ParentObject.DistanceTo(cellFromDirection) <= 1)
					{
						Combat.AttackCell(this.ParentObject, cellFromDirection, "Charging", 0, 0, 0, false);
					}
					else
					{
						this.ParentObject.UseEnergy(1000, "Charging", null, null, false);
					}
					this.ParentObject.FireEvent(Event.New("ChargedTarget", "Defender", combatTarget));
					combatTarget.FireEvent(Event.New("WasCharged", "Attacker", this.ParentObject));
					IL_0887:
					this.ParentObject.PlayWorldSound("Sounds/Abilities/sfx_ability_charge", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
					base.CooldownMyActivatedAbility(this.ActivatedAbilityID, Tactics_Charge.COOLDOWN, null, null, false);
					return true;
				}
				if (flag7)
				{
					flag3 = true;
				}
				else if (flag3)
				{
					flag4 = true;
					flag3 = false;
				}
				if (this.ParentObject.DistanceTo(gameObject) == 1)
				{
					flag = true;
				}
				else if (flag)
				{
					flag2 = true;
				}
				if (j >= maximumRange)
				{
					flag5 = true;
				}
				this.ForcedMoveDirection = null;
				if (!this.ParentObject.Move(text6, false, false, false, false, true, false, null, null, false, new int?(0), "Charge", null, false, false, null, null, 0))
				{
					break;
				}
				if (this.ForcedMoveDirection != null)
				{
					if (this.ForcedMoveDirection == "U" || this.ForcedMoveDirection == "D" || this.ForcedMoveDirection == "?")
					{
						break;
					}
					if (this.ForcedMoveDirection != text6)
					{
						int num4 = j + 1;
						while (j < count)
						{
							list2[num4] = this.ForcedMoveDirection;
							j++;
						}
					}
				}
				num2 += 5;
				if (base.Visible())
				{
					if (text2 != null)
					{
						this.ParentObject.TileParticleBlip(text2, text3, text4, num2, false, this.ParentObject.Render.getHFlip(), this.ParentObject.Render.getVFlip(), 0L);
					}
					else
					{
						this.ParentObject.ParticleBlip(text, num2, 0L, false);
					}
				}
				j++;
			}
			this.ForcedMoveDirection = null;
			if (gameObject2 != null)
			{
				base.DidXToY("are", "stopped in " + this.ParentObject.its + " tracks by", gameObject2, null, "!", null, null, null, this.ParentObject, false, false, false, false, false, null, null, null, false, false, false, false, false, null);
			}
			else if (flag4)
			{
				base.DidXToY("charge", "right through", gameObject, null, "!", null, null, null, this.ParentObject, false, false, false, false, false, null, null, null, false, false, false, false, false, null);
			}
			else if (flag2)
			{
				base.DidXToY("charge", "right past", gameObject, null, "!", null, null, null, this.ParentObject, false, false, false, false, false, null, null, null, false, false, false, false, false, null);
			}
			else if (flag3 || flag || flag5)
			{
				base.DidXToY("charge", gameObject, ", but" + this.ParentObject.GetVerb("fail", true, false, false, true, false) + " to make contact", "!", null, null, null, this.ParentObject, false, false, false, false, false, null, null, null, false, false, false, false, false, null);
			}
			else
			{
				base.DidX("charge", ", but" + this.ParentObject.Is + " brought up short", "!", null, null, null, this.ParentObject, false, false, null, null, false, false, false, false, false, null);
			}
			if (flag5)
			{
				this.ParentObject.ApplyEffect(new Dazed(1), null);
			}
			this.ParentObject.UseEnergy(1000, "Charging", null, null, false);
			goto IL_0887;
		}

		// Token: 0x0600C25F RID: 49759 RVA: 0x0046FF50 File Offset: 0x0046E150
		public override bool AddSkill(GameObject GO)
		{
			this.ActivatedAbilityID = base.AddMyActivatedAbility("Charge", Tactics_Charge.COMMAND_NAME, "Skills", null, "\u0010", null, false, false, false, true, false, false, false, false, true, true, false, -1, null, null, null, null, null, null);
			return true;
		}

		// Token: 0x0600C260 RID: 49760 RVA: 0x0046FF92 File Offset: 0x0046E192
		public override bool RemoveSkill(GameObject GO)
		{
			base.RemoveMyActivatedAbility(ref this.ActivatedAbilityID, null);
			return true;
		}

		// Token: 0x04004D99 RID: 19865
		public static readonly string COMMAND_NAME = "CommandMeleeCharge";

		// Token: 0x04004D9A RID: 19866
		public static readonly int COOLDOWN = 15;

		// Token: 0x04004D9B RID: 19867
		public Guid ActivatedAbilityID = Guid.Empty;

		// Token: 0x04004D9C RID: 19868
		[NonSerialized]
		private string ForcedMoveDirection;
	}
}
