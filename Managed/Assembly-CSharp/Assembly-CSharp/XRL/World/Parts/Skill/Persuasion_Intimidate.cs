using System;
using System.Threading;
using ConsoleLib.Console;
using XRL.Core;
using XRL.UI;
using XRL.World.Capabilities;
using XRL.World.Effects;

namespace XRL.World.Parts.Skill
{
	// Token: 0x0200135B RID: 4955
	[Serializable]
	public class Persuasion_Intimidate : BaseSkill
	{
		// Token: 0x0600C0EA RID: 49386 RVA: 0x00469390 File Offset: 0x00467590
		public void CollectStats(Templates.StatCollector stats)
		{
			stats.Set("Duration", Persuasion_Intimidate.DURATION, false, 0);
			int num = Persuasion_Intimidate.COOLDOWN;
			if (this.ParentObject.GetIntProperty("Horrifying", 0) > 0)
			{
				num -= 10;
				stats.AddChangePostfix("Cooldown", -10, "your " + PropertyDescription.GetPropertyDisplayName("Horrifying"));
			}
			stats.CollectCooldownTurns(base.MyActivatedAbility(this.ActivatedAbilityID, null), num, num - Persuasion_Intimidate.COOLDOWN);
		}

		// Token: 0x0600C0EB RID: 49387 RVA: 0x0046940B File Offset: 0x0046760B
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AIGetOffensiveAbilityListEvent.ID || ID == PooledEvent<CommandEvent>.ID || ID == SingletonEvent<BeforeAbilityManagerOpenEvent>.ID || ID == PooledEvent<GetItemElementsEvent>.ID;
		}

		// Token: 0x0600C0EC RID: 49388 RVA: 0x00469439 File Offset: 0x00467639
		public override bool HandleEvent(GetItemElementsEvent E)
		{
			if (E.IsRelevantCreature(this.ParentObject))
			{
				E.Add("jewels", 3);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C0ED RID: 49389 RVA: 0x0046945C File Offset: 0x0046765C
		public override bool HandleEvent(BeforeAbilityManagerOpenEvent E)
		{
			base.DescribeMyActivatedAbility(this.ActivatedAbilityID, new Action<Templates.StatCollector>(this.CollectStats), null);
			return base.HandleEvent(E);
		}

		// Token: 0x0600C0EE RID: 49390 RVA: 0x00469480 File Offset: 0x00467680
		public override bool HandleEvent(CommandEvent E)
		{
			if (E.Command == Persuasion_Intimidate.COMMAND_NAME)
			{
				if (this.ParentObject.OnWorldMap())
				{
					return this.ParentObject.Fail("You cannot do that on the world map.");
				}
				int num = Persuasion_Intimidate.COOLDOWN;
				if (this.ParentObject.GetIntProperty("Horrifying", 0) > 0)
				{
					num -= 10;
				}
				Persuasion_Intimidate.ApplyIntimidate(this.ParentObject.GetCurrentCell(), this.ParentObject, false);
				base.CooldownMyActivatedAbility(this.ActivatedAbilityID, num, null, null, false);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C0EF RID: 49391 RVA: 0x0046950C File Offset: 0x0046770C
		public override bool HandleEvent(AIGetOffensiveAbilityListEvent E)
		{
			if (E.Distance <= 1 && GameObject.Validate(E.Target) && E.Target.Brain != null && base.IsMyActivatedAbilityAIUsable(this.ActivatedAbilityID, null) && 10.in100())
			{
				E.Add(Persuasion_Intimidate.COMMAND_NAME, 1, null, false, false, null, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C0F0 RID: 49392 RVA: 0x0046956C File Offset: 0x0046776C
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600C0F1 RID: 49393 RVA: 0x00469570 File Offset: 0x00467770
		public static void ApplyIntimidate(Cell FromCell, GameObject Actor, bool FreeAction = false)
		{
			if (!GameObject.Validate(ref Actor))
			{
				return;
			}
			if (Actor.OnWorldMap())
			{
				return;
			}
			if (Actor != null)
			{
				Actor.PlayWorldSound("Sounds/Abilities/sfx_ability_intimidate", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
			}
			IComponent<GameObject>.XDidY(Actor, "assume", "an intimidating posture", null, null, null, null, null, false, false, null, null, false, false, false, false, false, null);
			foreach (Cell cell in FromCell.GetLocalAdjacentCells())
			{
				TextConsole textConsole = Look._TextConsole;
				ScreenBuffer scrapBuffer = TextConsole.ScrapBuffer;
				XRLCore.Core.RenderMapToBuffer(scrapBuffer);
				if (cell != null)
				{
					foreach (GameObject gameObject in cell.GetObjectsInCell())
					{
						if (gameObject.IsHostileTowards(Actor) || Actor.IsHostileTowards(gameObject))
						{
							if (!FreeAction)
							{
								Actor.UseEnergy(1000, "Persuasion Skill Intimidate", null, null, false);
								FreeAction = true;
							}
							int num = Actor.StatMod("Ego", 0) + Actor.GetIntProperty("Persuasion_Intimidate", 0) * 2;
							Mental.PerformAttack(new Mental.Attack(Terrified.OfAttacker), Actor, gameObject, null, Persuasion_Intimidate.COMMAND_ATTACK, "1d8", 8388610, Persuasion_Intimidate.DURATION.RollCached(), int.MinValue, num, 0);
						}
					}
				}
				if (cell.IsVisible())
				{
					scrapBuffer.WriteAt(cell, "&Y#", true);
					textConsole.DrawBuffer(scrapBuffer, null, false);
					Thread.Sleep(50);
				}
			}
		}

		// Token: 0x0600C0F2 RID: 49394 RVA: 0x0046974C File Offset: 0x0046794C
		public static bool Terrify(MentalAttackEvent E)
		{
			if (E.Penetrations > 0)
			{
				Terrified terrified = new Terrified(Persuasion_Intimidate.DURATION.RollCached(), E.Attacker, false, false);
				if (E.Defender.ApplyEffect(terrified, null))
				{
					return true;
				}
			}
			IComponent<GameObject>.XDidY(E.Defender, "resist", "becoming afraid", null, null, null, E.Defender, null, false, false, null, null, false, false, false, false, false, null);
			return false;
		}

		// Token: 0x0600C0F3 RID: 49395 RVA: 0x004697B8 File Offset: 0x004679B8
		public override bool AddSkill(GameObject GO)
		{
			this.ActivatedAbilityID = base.AddMyActivatedAbility("Intimidate", Persuasion_Intimidate.COMMAND_NAME, "Skills", null, "\u0005", null, false, false, false, false, false, false, false, false, true, true, false, -1, null, null, null, null, null, null);
			return base.AddSkill(GO);
		}

		// Token: 0x0600C0F4 RID: 49396 RVA: 0x00469800 File Offset: 0x00467A00
		public override bool RemoveSkill(GameObject GO)
		{
			base.RemoveMyActivatedAbility(ref this.ActivatedAbilityID, null);
			return base.RemoveSkill(GO);
		}

		// Token: 0x04004D5F RID: 19807
		public static readonly string COMMAND_NAME = "CommandIntimidate";

		// Token: 0x04004D60 RID: 19808
		public static readonly string COMMAND_ATTACK = "Terrify Intimidate";

		// Token: 0x04004D61 RID: 19809
		public static readonly int COOLDOWN = 50;

		// Token: 0x04004D62 RID: 19810
		public static readonly string DURATION = "6d4";

		// Token: 0x04004D63 RID: 19811
		public Guid ActivatedAbilityID = Guid.Empty;
	}
}
