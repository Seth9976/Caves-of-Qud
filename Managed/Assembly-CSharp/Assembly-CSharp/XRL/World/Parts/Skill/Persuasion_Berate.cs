using System;
using System.Threading;
using ConsoleLib.Console;
using XRL.Core;
using XRL.Rules;
using XRL.UI;
using XRL.World.Capabilities;
using XRL.World.Effects;
using XRL.World.Parts.Mutation;

namespace XRL.World.Parts.Skill
{
	// Token: 0x02001359 RID: 4953
	[Serializable]
	public class Persuasion_Berate : BaseSkill
	{
		// Token: 0x0600C0D9 RID: 49369 RVA: 0x00468BC0 File Offset: 0x00466DC0
		public void CollectStats(Templates.StatCollector stats)
		{
			stats.Set("Range", Persuasion_Berate.VOCAL_RANGE, false, 0);
			stats.Set("Duration", Persuasion_Berate.DURATION, false, 0);
			int num = Persuasion_Berate.COOLDOWN;
			if (this.ParentObject.GetIntProperty("Horrifying", 0) > 0)
			{
				num -= 10;
				stats.AddChangePostfix("Cooldown", -10, "your " + PropertyDescription.GetPropertyDisplayName("Horrifying"));
			}
			stats.CollectCooldownTurns(base.MyActivatedAbility(this.ActivatedAbilityID, null), num, num - Persuasion_Berate.COOLDOWN);
		}

		// Token: 0x0600C0DA RID: 49370 RVA: 0x00468C4D File Offset: 0x00466E4D
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AIGetOffensiveAbilityListEvent.ID || ID == PooledEvent<CommandEvent>.ID || ID == SingletonEvent<BeforeAbilityManagerOpenEvent>.ID || ID == PooledEvent<GetItemElementsEvent>.ID;
		}

		// Token: 0x0600C0DB RID: 49371 RVA: 0x00468C7B File Offset: 0x00466E7B
		public override bool HandleEvent(GetItemElementsEvent E)
		{
			if (E.IsRelevantCreature(this.ParentObject))
			{
				E.Add("jewels", 3);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C0DC RID: 49372 RVA: 0x00468C9E File Offset: 0x00466E9E
		public override bool HandleEvent(BeforeAbilityManagerOpenEvent E)
		{
			base.DescribeMyActivatedAbility(this.ActivatedAbilityID, new Action<Templates.StatCollector>(this.CollectStats), null);
			return base.HandleEvent(E);
		}

		// Token: 0x0600C0DD RID: 49373 RVA: 0x00468CC4 File Offset: 0x00466EC4
		public override bool HandleEvent(CommandEvent E)
		{
			if (E.Command == Persuasion_Berate.COMMAND_NAME && this.ParentObject == E.Actor)
			{
				bool flag = E.Actor.IsMissingTongue();
				if (flag && !E.Actor.HasPart<Telepathy>())
				{
					E.Actor.Fail("You cannot berate without a tongue.");
					return false;
				}
				if (!E.Actor.CheckFrozen(true, false, false, null))
				{
					return false;
				}
				Cell cell = base.PickDestinationCell(E.Actor.HasPart<Telepathy>() ? 80 : Persuasion_Berate.VOCAL_RANGE, AllowVis.OnlyVisible, true, false, true, true, PickTarget.PickStyle.EmptyCell, "Berate whom?", true, null);
				if (cell == null)
				{
					return false;
				}
				if (E.Actor.DistanceTo(cell) > Persuasion_Berate.VOCAL_RANGE && !E.Actor.HasPart<Telepathy>())
				{
					E.Actor.Fail("That is out of range! (" + Persuasion_Berate.VOCAL_RANGE.ToString() + " squares)");
					return false;
				}
				int intProperty = E.Actor.GetIntProperty("Horrifying", 0);
				int num = Math.Max(Persuasion_Berate.COOLDOWN - intProperty * 10, 10);
				this.UseEnergy(1000, "Skill Persuasion Berate");
				base.CooldownMyActivatedAbility(this.ActivatedAbilityID, num, null, null, false);
				Persuasion_Berate.ApplyBerate(E.Actor, cell, new bool?(flag), this);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C0DE RID: 49374 RVA: 0x00468E14 File Offset: 0x00467014
		public override bool HandleEvent(AIGetOffensiveAbilityListEvent E)
		{
			if ((E.Distance <= Persuasion_Berate.VOCAL_RANGE || (E.Actor.HasPart<Telepathy>() && E.Actor.CanMakeTelepathicContactWith(E.Target))) && GameObject.Validate(E.Target) && E.Target.Brain != null && !E.Target.HasEffect<Shamed>() && base.IsMyActivatedAbilityAIUsable(this.ActivatedAbilityID, null) && Stat.Random(-80, 80) >= E.Distance && (!E.Actor.IsMissingTongue() || (E.Actor.HasPart<Telepathy>() && E.Actor.CanMakeTelepathicContactWith(E.Target))) && E.Actor.CheckFrozen(true, false, true, E.Target))
			{
				E.Add(Persuasion_Berate.COMMAND_NAME, 1, null, false, false, null, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C0DF RID: 49375 RVA: 0x00468EFC File Offset: 0x004670FC
		public static int ApplyBerate(GameObject Actor, Cell TargetCell, bool? IsMissingTongue = null, object Source = null)
		{
			if (!GameObject.Validate(ref Actor))
			{
				return 0;
			}
			if (TargetCell == null)
			{
				return 0;
			}
			bool flag = IsMissingTongue ?? Actor.IsMissingTongue();
			TextConsole textConsole = Look._TextConsole;
			ScreenBuffer scrapBuffer = TextConsole.ScrapBuffer;
			XRLCore.Core.RenderMapToBuffer(scrapBuffer);
			string text = "1d8";
			int num = Actor.StatMod("Ego", 0);
			if (num != 0)
			{
				text += num.Signed(false);
			}
			int num2 = 0;
			int i = 0;
			int count = TargetCell.Objects.Count;
			while (i < count)
			{
				GameObject gameObject = TargetCell.Objects[i];
				if (gameObject.HasPart<Brain>())
				{
					if (flag && !Actor.CanMakeTelepathicContactWith(gameObject))
					{
						Actor.Fail("{{r|You cannot berate " + gameObject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + " without a tongue.}}");
					}
					else if (!Actor.CheckFrozen(true, false, true, gameObject))
					{
						Actor.Fail("{{r|You cannot berate " + gameObject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + " while frozen.}}");
					}
					else if (Actor.DistanceTo(gameObject) > Persuasion_Berate.VOCAL_RANGE && !Actor.CanMakeTelepathicContactWith(gameObject))
					{
						Actor.Fail("{{r|You cannot make telepathic contact with " + gameObject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + ".}}");
					}
					else if (gameObject.HasEffect<Shamed>())
					{
						IComponent<GameObject>.XDidYToZ(Actor, "attempt", "to shame", gameObject, string.Concat(new string[]
						{
							"with ",
							Actor.its,
							" words, but ",
							gameObject.does("are", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, true, true, null, true, null, false),
							" already shamed"
						}), null, null, null, gameObject, null, false, false, false, false, false, null, null, null, false, false, false, false, false, null);
					}
					else
					{
						int num3 = Persuasion_Berate.DURATION.RollCached();
						if (CanApplyEffectEvent.Check<Shamed>(gameObject, num3) && gameObject.ApplyEffect(new Shamed(num3), null))
						{
							GenericCommandEvent.Send(Actor, Persuasion_Berate.COMMAND_ATTACK, null, gameObject, Source, 0, 0);
							IComponent<GameObject>.XDidYToZ(Actor, "shame", gameObject, "with " + Actor.its + " words", null, null, null, null, gameObject, false, false, false, false, false, null, null, null, false, false, false, false, false, null);
							num2++;
						}
						else
						{
							IComponent<GameObject>.XDidYToZ(Actor, "attempt", "to shame", gameObject, "with " + Actor.its + " words, but they have no effect", null, null, null, gameObject, null, false, false, false, false, false, null, null, null, false, false, false, false, false, null);
						}
					}
				}
				i++;
			}
			if (TargetCell.IsVisible())
			{
				scrapBuffer.WriteAt(TargetCell, (num2 > 0) ? "&Y#" : "&w*", true);
				textConsole.DrawBuffer(scrapBuffer, null, false);
				Thread.Sleep(50);
			}
			return num2;
		}

		// Token: 0x0600C0E0 RID: 49376 RVA: 0x00469228 File Offset: 0x00467428
		public override bool AddSkill(GameObject GO)
		{
			this.ActivatedAbilityID = base.AddMyActivatedAbility("Berate", Persuasion_Berate.COMMAND_NAME, "Skills", null, "\u0014", null, false, false, false, false, false, false, false, false, true, true, false, -1, null, null, null, null, null, null);
			return base.AddSkill(GO);
		}

		// Token: 0x0600C0E1 RID: 49377 RVA: 0x00469270 File Offset: 0x00467470
		public override bool RemoveSkill(GameObject GO)
		{
			base.RemoveMyActivatedAbility(ref this.ActivatedAbilityID, null);
			return base.RemoveSkill(GO);
		}

		// Token: 0x04004D59 RID: 19801
		public static readonly string COMMAND_NAME = "CommandPersuasionBerate";

		// Token: 0x04004D5A RID: 19802
		public static readonly string COMMAND_ATTACK = "Shame Berate";

		// Token: 0x04004D5B RID: 19803
		public static readonly int VOCAL_RANGE = 8;

		// Token: 0x04004D5C RID: 19804
		public static readonly string DURATION = "6d6";

		// Token: 0x04004D5D RID: 19805
		public static readonly int COOLDOWN = 50;

		// Token: 0x04004D5E RID: 19806
		public Guid ActivatedAbilityID = Guid.Empty;
	}
}
