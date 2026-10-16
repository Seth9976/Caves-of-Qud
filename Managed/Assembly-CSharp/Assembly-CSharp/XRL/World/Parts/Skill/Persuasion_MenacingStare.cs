using System;
using System.Collections.Generic;
using System.Threading;
using ConsoleLib.Console;
using XRL.Core;
using XRL.Language;
using XRL.UI;
using XRL.World.Capabilities;
using XRL.World.Effects;

namespace XRL.World.Parts.Skill
{
	// Token: 0x0200135C RID: 4956
	[Serializable]
	public class Persuasion_MenacingStare : BaseSkill
	{
		// Token: 0x0600C0F7 RID: 49399 RVA: 0x00469854 File Offset: 0x00467A54
		public void CollectStats(Templates.StatCollector stats)
		{
			stats.Set("Range", this.MaxRange, false, 0);
			int num = Persuasion_MenacingStare.COOLDOWN;
			if (this.ParentObject.GetIntProperty("Horrifying", 0) > 0)
			{
				num -= 10;
				stats.AddChangePostfix("Cooldown", -10, "your " + PropertyDescription.GetPropertyDisplayName("Horrifying"));
			}
			stats.CollectCooldownTurns(base.MyActivatedAbility(this.ActivatedAbilityID, null), num, num - Persuasion_MenacingStare.COOLDOWN);
		}

		// Token: 0x0600C0F8 RID: 49400 RVA: 0x004698D0 File Offset: 0x00467AD0
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AIGetOffensiveAbilityListEvent.ID || ID == PooledEvent<CommandEvent>.ID || ID == SingletonEvent<BeforeAbilityManagerOpenEvent>.ID || ID == PooledEvent<GetItemElementsEvent>.ID;
		}

		// Token: 0x0600C0F9 RID: 49401 RVA: 0x004698FE File Offset: 0x00467AFE
		public override bool HandleEvent(GetItemElementsEvent E)
		{
			if (E.IsRelevantCreature(this.ParentObject))
			{
				E.Add("jewels", 3);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C0FA RID: 49402 RVA: 0x00469921 File Offset: 0x00467B21
		public override bool HandleEvent(BeforeAbilityManagerOpenEvent E)
		{
			base.DescribeMyActivatedAbility(this.ActivatedAbilityID, new Action<Templates.StatCollector>(this.CollectStats), null);
			return base.HandleEvent(E);
		}

		// Token: 0x0600C0FB RID: 49403 RVA: 0x00469944 File Offset: 0x00467B44
		public override bool HandleEvent(CommandEvent E)
		{
			if (E.Command == Persuasion_MenacingStare.COMMAND_NAME)
			{
				Persuasion_MenacingStare.PickStare(this.ParentObject, this.RatingBase, this.RatingOffset, this.MaxRange, new Guid?(this.ActivatedAbilityID));
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C0FC RID: 49404 RVA: 0x00469994 File Offset: 0x00467B94
		public override bool HandleEvent(AIGetOffensiveAbilityListEvent E)
		{
			if (E.Distance <= this.MaxRange && GameObject.Validate(E.Target) && E.Target.Brain != null && base.IsMyActivatedAbilityAIUsable(this.ActivatedAbilityID, null) && 50.in100())
			{
				E.Add(Persuasion_MenacingStare.COMMAND_NAME, 1, null, false, false, null, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C0FD RID: 49405 RVA: 0x004699F9 File Offset: 0x00467BF9
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600C0FE RID: 49406 RVA: 0x004699FC File Offset: 0x00467BFC
		public static bool PickStare(GameObject Actor, string RatingBase, int RatingOffset, int MaxRange, Guid? AbilityID = null)
		{
			Cell cell = Actor.Physics.PickDestinationCell(80, AllowVis.OnlyVisible, true, false, true, true, PickTarget.PickStyle.EmptyCell, "Menacing Stare", true, null);
			if (cell == null)
			{
				return false;
			}
			if (cell.DistanceTo(Actor) > MaxRange)
			{
				Actor.Fail(string.Concat(new string[]
				{
					"That is out of range! (",
					MaxRange.ToString(),
					" ",
					(MaxRange == 1) ? "square" : "squares",
					")"
				}));
				return false;
			}
			int num = Persuasion_MenacingStare.COOLDOWN;
			if (Actor.GetIntProperty("Horrifying", 0) > 0)
			{
				num -= 10;
			}
			if (AbilityID != null)
			{
				Actor.CooldownActivatedAbility(AbilityID.Value, num, null, false);
			}
			Persuasion_MenacingStare.ApplyStare(Actor, cell, RatingBase, RatingOffset);
			return true;
		}

		// Token: 0x0600C0FF RID: 49407 RVA: 0x00469ABC File Offset: 0x00467CBC
		public static int ApplyStare(GameObject Actor, Cell Cell, string RatingBase, int RatingOffset)
		{
			if (Cell == null)
			{
				return 0;
			}
			TextConsole textConsole = Look._TextConsole;
			ScreenBuffer scrapBuffer = TextConsole.ScrapBuffer;
			XRLCore.Core.RenderMapToBuffer(scrapBuffer);
			int num = 0;
			List<GameObject> list = Event.NewGameObjectList();
			foreach (GameObject gameObject in Cell.GetObjectsInCell())
			{
				if ((gameObject != Actor || gameObject.GetBodyPartCount("Head") >= 2) && gameObject.Brain != null)
				{
					list.Add(gameObject);
				}
			}
			if (list.Count > 0)
			{
				if (list.Count > 1)
				{
					Actor.Physics.DidX("stare", "at " + Grammar.MakeAndList(list, true, true, true, true, false, null) + " menacingly", null, null, null, null, null, false, false, null, null, false, false, false, false, false, null);
				}
				else
				{
					Actor.Physics.DidXToY("stare", "at", list[0], "menacingly", null, null, null, null, null, false, false, false, false, false, null, null, null, false, false, false, false, false, null);
				}
			}
			foreach (GameObject gameObject2 in list)
			{
				if (Actor.Physics.PerformMentalAttack(new Mental.Attack(Terrified.OfAttacker), Actor, gameObject2, null, Persuasion_MenacingStare.COMMAND_ATTACK, RatingBase, 8388612, "6d4".RollCached(), -2147483648, Actor.StatMod("Ego", 0) + RatingOffset, 0))
				{
					num++;
				}
			}
			if (Cell.IsVisible())
			{
				scrapBuffer.WriteAt(Cell, (num > 0) ? "&Y#" : "&w*", true);
				textConsole.DrawBuffer(scrapBuffer, null, false);
				Thread.Sleep(50);
			}
			return num;
		}

		// Token: 0x0600C100 RID: 49408 RVA: 0x00469C8C File Offset: 0x00467E8C
		public static Guid AddAbility(GameObject Object)
		{
			return Object.AddActivatedAbility("Menacing Stare", Persuasion_MenacingStare.COMMAND_NAME, "Skills", null, "ì", null, false, false, false, false, false, false, false, false, true, true, false, false, -1, null, Renderable.UITile("Abilities/abil_menacing_stare.bmp", 'w', 'M', " ", '\0'), null, null, null);
		}

		// Token: 0x0600C101 RID: 49409 RVA: 0x00469CDA File Offset: 0x00467EDA
		public override bool AddSkill(GameObject GO)
		{
			this.ActivatedAbilityID = Persuasion_MenacingStare.AddAbility(GO);
			return base.AddSkill(GO);
		}

		// Token: 0x0600C102 RID: 49410 RVA: 0x00469CEF File Offset: 0x00467EEF
		public override bool RemoveSkill(GameObject GO)
		{
			base.RemoveMyActivatedAbility(ref this.ActivatedAbilityID, null);
			return base.RemoveSkill(GO);
		}

		// Token: 0x04004D64 RID: 19812
		public static readonly string COMMAND_NAME = "CommandPersuasionMenacingStare";

		// Token: 0x04004D65 RID: 19813
		public static readonly string COMMAND_ATTACK = "Terrify MenacingStare";

		// Token: 0x04004D66 RID: 19814
		public static readonly int COOLDOWN = 50;

		// Token: 0x04004D67 RID: 19815
		public string RatingBase = "1d8";

		// Token: 0x04004D68 RID: 19816
		public int RatingOffset = 2;

		// Token: 0x04004D69 RID: 19817
		public int MaxRange = 5;

		// Token: 0x04004D6A RID: 19818
		public Guid ActivatedAbilityID = Guid.Empty;
	}
}
