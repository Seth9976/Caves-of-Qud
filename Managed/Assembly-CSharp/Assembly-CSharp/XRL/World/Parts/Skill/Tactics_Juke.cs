using System;
using XRL.Rules;
using XRL.World.Effects;

namespace XRL.World.Parts.Skill
{
	// Token: 0x020013A7 RID: 5031
	[Serializable]
	public class Tactics_Juke : BaseSkill
	{
		// Token: 0x0600C274 RID: 49780 RVA: 0x004708D3 File Offset: 0x0046EAD3
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AIGetOffensiveAbilityListEvent.ID || ID == SingletonEvent<BeforeAbilityManagerOpenEvent>.ID || ID == PooledEvent<CommandEvent>.ID || ID == GetMovementCapabilitiesEvent.ID;
		}

		// Token: 0x0600C275 RID: 49781 RVA: 0x00470901 File Offset: 0x0046EB01
		public override bool HandleEvent(GetMovementCapabilitiesEvent E)
		{
			E.Add("Juke", Tactics_Juke.COMMAND_NAME, 7500, base.MyActivatedAbility(this.ActivatedAbilityID, null), false);
			return base.HandleEvent(E);
		}

		// Token: 0x0600C276 RID: 49782 RVA: 0x0047092D File Offset: 0x0046EB2D
		public void CollectStats(Templates.StatCollector stats)
		{
			stats.CollectCooldownTurns(base.MyActivatedAbility(this.ActivatedAbilityID, null), this.GetCooldown(), 0);
		}

		// Token: 0x0600C277 RID: 49783 RVA: 0x00470949 File Offset: 0x0046EB49
		public override bool HandleEvent(BeforeAbilityManagerOpenEvent E)
		{
			base.DescribeMyActivatedAbility(this.ActivatedAbilityID, new Action<Templates.StatCollector>(this.CollectStats), null);
			return base.HandleEvent(E);
		}

		// Token: 0x0600C278 RID: 49784 RVA: 0x0047096C File Offset: 0x0046EB6C
		public override bool HandleEvent(CommandEvent E)
		{
			if (E.Command == Tactics_Juke.COMMAND_NAME)
			{
				if (this.ParentObject.OnWorldMap())
				{
					return this.ParentObject.Fail("You cannot juke on the world map.");
				}
				if (!this.ParentObject.CheckFrozen(false, false, false, null) || !this.ParentObject.CanChangeBodyPosition("Juking", true, false, false, false) || !this.ParentObject.CanChangeMovementMode("Juking", true, false, false, false))
				{
					return false;
				}
				string text = base.PickDirectionS("Juke where?", null);
				if (text.IsNullOrEmpty())
				{
					return false;
				}
				Cell currentCell = this.ParentObject.CurrentCell;
				Cell cell = ((currentCell != null) ? currentCell.GetCellFromDirection(text, false) : null);
				if (cell == null || cell == this.ParentObject.CurrentCell)
				{
					return false;
				}
				foreach (GameObject gameObject in cell.LoopObjectsWithPart("Physics"))
				{
					if (gameObject.ConsiderSolidFor(this.ParentObject, false))
					{
						return this.ParentObject.Fail("You cannot juke into " + gameObject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + ".");
					}
				}
				Cell currentCell2 = this.ParentObject.CurrentCell;
				if (currentCell2 == null)
				{
					return false;
				}
				GameObject gameObject2 = null;
				int i = 0;
				int count = cell.Objects.Count;
				while (i < count)
				{
					GameObject gameObject3 = cell.Objects[i];
					if (gameObject3.IsCombatObject() && this.ParentObject.PhaseAndFlightMatches(gameObject3) && (!gameObject3.HasPart<FungalVision>() || FungalVisionary.VisionLevel > 0))
					{
						if (gameObject3.GetMatterPhase() >= 3 || IsRootedInPlaceEvent.Check(gameObject3))
						{
							return this.ParentObject.Fail("You cannot juke " + gameObject3.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + " out of your way.");
						}
						if (gameObject2 != null)
						{
							return this.ParentObject.Fail(string.Concat(new string[]
							{
								"You cannot juke both ",
								gameObject3.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
								" and ",
								gameObject2.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
								" out of your way."
							}));
						}
						gameObject2 = gameObject3;
					}
					i++;
				}
				GameObject parentObject = this.ParentObject;
				string text2 = text;
				bool flag = false;
				bool flag2 = false;
				bool flag3 = true;
				bool flag4 = false;
				bool flag5 = false;
				bool flag6 = false;
				GameObject gameObject4 = null;
				GameObject gameObject5 = null;
				bool flag7 = false;
				GameObject gameObject6 = gameObject2;
				if (!parentObject.Move(text2, flag, flag2, flag3, flag4, flag5, flag6, gameObject4, gameObject5, flag7, new int?(0), null, null, false, true, gameObject6, null, 0))
				{
					return false;
				}
				if (GameObject.Validate(ref gameObject2) && currentCell2.Objects.Contains(gameObject2))
				{
					base.DidXToY("juke", Directions.GetDirectionDescription(text) + ", moving", gameObject2, "out of " + this.ParentObject.its + " way", null, null, null, this.ParentObject, null, false, false, false, false, false, null, null, null, false, false, false, false, false, null);
					if (this.ParentObject.HasSkill("ShortBlades_PointedCircle") && gameObject2.IsHostileTowards(this.ParentObject) && this.ParentObject.PhaseAndFlightMatches(gameObject2))
					{
						GameObject primaryWeaponOfType = this.ParentObject.GetPrimaryWeaponOfType("ShortBlades", true);
						if (primaryWeaponOfType != null)
						{
							Combat.MeleeAttackWithWeapon(this.ParentObject, gameObject2, primaryWeaponOfType, this.ParentObject.Body.FindDefaultOrEquippedItem(primaryWeaponOfType), "Juking", 0, 0, 0, 0, 0, true, true);
						}
					}
					GameObject parentObject2 = this.ParentObject;
					if (parentObject2 != null)
					{
						parentObject2.FireEvent(Event.New("JukedTarget", "Defender", gameObject2));
					}
					if (gameObject2 != null)
					{
						gameObject2.FireEvent(Event.New("WasJuked", "Attacker", this.ParentObject));
					}
				}
				else
				{
					base.DidX("juke", Directions.GetDirectionDescription(text), null, null, null, this.ParentObject, null, false, false, null, null, false, false, false, false, false, null);
				}
				this.ParentObject.PlayWorldSound("Sounds/Abilities/sfx_ability_juke", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
				int num = (this.ParentObject.HasPart<Acrobatics_Tumble>() ? 20 : 40);
				base.CooldownMyActivatedAbility(this.ActivatedAbilityID, num, null, "Agility", false);
				this.ParentObject.FireEvent(Event.New("Juked", "FromCell", currentCell2, "ToCell", cell));
				if (gameObject2 != null)
				{
					gameObject2.Gravitate();
				}
			}
			IL_0497:
			return base.HandleEvent(E);
		}

		// Token: 0x0600C279 RID: 49785 RVA: 0x00470E2C File Offset: 0x0046F02C
		public override bool HandleEvent(AIGetOffensiveAbilityListEvent E)
		{
			if (E.Distance == 1 && GameObject.Validate(E.Target) && base.IsMyActivatedAbilityAIUsable(this.ActivatedAbilityID, null) && E.Actor.PhaseAndFlightMatches(E.Target) && E.Target.GetMatterPhase() < 3 && !IsRootedInPlaceEvent.Check(E.Target) && !E.Target.CurrentCell.IsSolidFor(E.Actor, false))
			{
				int num = 10;
				if (E.Actor.HasSkill("ShortBlades_PointedCircle") && E.Actor.GetPrimaryWeaponOfType("ShortBlades", true) != null)
				{
					num += 40;
				}
				if (num.in100())
				{
					E.Add(Tactics_Juke.COMMAND_NAME, 1, null, false, false, null, null);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C27A RID: 49786 RVA: 0x00470EFB File Offset: 0x0046F0FB
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600C27B RID: 49787 RVA: 0x00470EFE File Offset: 0x0046F0FE
		public int GetCooldown()
		{
			if (!this.ParentObject.HasPart<Acrobatics_Tumble>())
			{
				return 40;
			}
			return 20;
		}

		// Token: 0x0600C27C RID: 49788 RVA: 0x00470F14 File Offset: 0x0046F114
		public override bool AddSkill(GameObject GO)
		{
			this.ActivatedAbilityID = base.AddMyActivatedAbility("Juke", Tactics_Juke.COMMAND_NAME, "Skills", null, "\u0012", null, false, false, false, false, false, false, false, false, true, true, false, -1, null, null, null, null, null, null);
			return base.AddSkill(GO);
		}

		// Token: 0x0600C27D RID: 49789 RVA: 0x00470F5C File Offset: 0x0046F15C
		public override bool RemoveSkill(GameObject GO)
		{
			base.RemoveMyActivatedAbility(ref this.ActivatedAbilityID, null);
			return base.RemoveSkill(GO);
		}

		// Token: 0x04004D9F RID: 19871
		public static readonly string COMMAND_NAME = "CommandTacticsJuke";

		// Token: 0x04004DA0 RID: 19872
		public Guid ActivatedAbilityID = Guid.Empty;
	}
}
