using System;
using System.Collections.Generic;
using Qud.API;
using XRL.UI;
using XRL.World.Parts;
using XRL.World.Skills;

namespace XRL.World.Effects
{
	// Token: 0x02000C40 RID: 3136
	[Serializable]
	public class BrainBrineCurse : Effect, ITierInitialized
	{
		// Token: 0x06007FF4 RID: 32756 RVA: 0x002F22A5 File Offset: 0x002F04A5
		public BrainBrineCurse()
		{
			this.Duration = 1;
		}

		// Token: 0x06007FF5 RID: 32757 RVA: 0x002F22B4 File Offset: 0x002F04B4
		public override string GetDescription()
		{
			return null;
		}

		// Token: 0x06007FF6 RID: 32758 RVA: 0x002F22B7 File Offset: 0x002F04B7
		public override int GetEffectType()
		{
			return 67117056;
		}

		// Token: 0x06007FF7 RID: 32759 RVA: 0x002F22BE File Offset: 0x002F04BE
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("EndTurn");
			base.Register(Object, Registrar);
		}

		// Token: 0x06007FF8 RID: 32760 RVA: 0x002F22D4 File Offset: 0x002F04D4
		public void GainChoice(string Choice)
		{
			if (Choice == "skills")
			{
				List<PowerEntry> powerPool = SkillFactory.GetPowerPool(base.Object, null, 0, int.MaxValue, false, true, true);
				if (powerPool.Count > 0)
				{
					powerPool.ShuffleInPlace<PowerEntry>(null);
					int i = 0;
					int num = base.Object.GetSeededRange("brainbrine", 2, 3);
					while (i < num)
					{
						if (i >= powerPool.Count)
						{
							return;
						}
						PowerEntry powerEntry = powerPool[i];
						string text = powerEntry.Class;
						string text2 = powerEntry.Name;
						if (powerEntry.Cost != 0 || powerEntry.ParentSkill == null)
						{
							goto IL_00B4;
						}
						if (!base.Object.HasSkill(powerEntry.ParentSkill.Class))
						{
							text = powerEntry.ParentSkill.Class;
							text2 = powerEntry.ParentSkill.Name;
							goto IL_00B4;
						}
						num++;
						IL_00F2:
						i++;
						continue;
						IL_00B4:
						if (base.Object.IsPlayer())
						{
							Popup.Show("You learn the skill {{C|" + text2 + "}}!", null, "Sounds/UI/ui_notification", true, true, true, true, null);
						}
						base.Object.AddSkill(text, null, null);
						goto IL_00F2;
					}
				}
			}
			else
			{
				if (Choice == "sp")
				{
					base.Object.GainSP(base.Object.GetSeededRange("brainbrine", 400, 500), true);
					return;
				}
				if (Choice == "ego")
				{
					base.Object.GainEgo(base.Object.GetSeededRange("brainbrine", 1, 2), true);
					return;
				}
				if (Choice == "int")
				{
					base.Object.GainIntelligence(base.Object.GetSeededRange("brainbrine", 2, 3), true);
					return;
				}
				if (Choice == "wis")
				{
					base.Object.GainWillpower(base.Object.GetSeededRange("brainbrine", 2, 3), true);
					return;
				}
				if (Choice == "secrets")
				{
					int j = 0;
					int seededRange = base.Object.GetSeededRange("brainbrine", 6, 8);
					while (j < seededRange)
					{
						JournalAPI.RevealRandomSecret();
						j++;
					}
					return;
				}
				if (Choice == "+mutation")
				{
					MutationEntry mutationEntry = MutationsAPI.RandomlyMutate(base.Object, (MutationEntry e) => e.IsMental() && !e.IsDefect(), base.Object.GetSeededRandom("brainbrine"), false, null);
					if (mutationEntry != null)
					{
						Popup.Show("You gained the mutation {{G|" + mutationEntry.GetDisplayName(false) + "}}!", null, "Sounds/UI/ui_notification", true, true, true, true, null);
						return;
					}
					Popup.Show("Your mind begins to morph but the physiology of your brain restricts it.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
					return;
				}
				else if (Choice == "-mutation")
				{
					MutationEntry mutationEntry2 = MutationsAPI.RandomlyMutate(base.Object, (MutationEntry e) => e.IsMental() && e.IsDefect(), base.Object.GetSeededRandom("brainbrine"), true, null);
					if (mutationEntry2 != null)
					{
						Popup.Show("You gained the defect {{R|" + mutationEntry2.GetDisplayName(false) + "}}!", null, "Sounds/UI/ui_notification", true, true, true, true, null);
						return;
					}
					Popup.Show("Your mind begins to morph but the physiology of your brain restricts it.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
				}
			}
		}

		// Token: 0x06007FF9 RID: 32761 RVA: 0x002F25F7 File Offset: 0x002F07F7
		public bool IsValidTarget(GameObject Object)
		{
			return this.Duration > 0 && Object != null && Object.IsPlayer() && !Object.HasEffect<Confused>();
		}

		// Token: 0x06007FFA RID: 32762 RVA: 0x002F2618 File Offset: 0x002F0818
		public override bool FireEvent(Event E)
		{
			if (E.ID == "EndTurn" && this.IsValidTarget(base.Object))
			{
				this.Duration = 0;
				Popup.Show("You shake the water from your addled brain, but someone else's thoughts have already taken root.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
				BallBag<string> ballBag = new BallBag<string>(base.Object.GetSeededRandom("brainbrine"), 0);
				if (SkillFactory.GetPowerPool(base.Object, null, 0, 2147483647, false, true, true).Count > 0)
				{
					ballBag.Add("skills", 20);
					ballBag.Add("sp", 20);
				}
				ballBag.Add("int", 10);
				ballBag.Add("wis", 10);
				ballBag.Add("secrets", 30);
				ballBag.Add("+mutation", 30);
				ballBag.Add("-mutation", 15);
				int num = DrinkMagnifier.Magnify(base.Object, 1);
				for (int i = 0; i < num; i++)
				{
					this.GainChoice(ballBag.PeekOne());
				}
				if (Options.AnySifrah)
				{
					if (50.in100())
					{
						TinkeringSifrah.AwardInsight();
					}
					if (50.in100())
					{
						SocialSifrah.AwardInsight();
					}
					if (50.in100())
					{
						RitualSifrah.AwardInsight();
					}
					if (50.in100())
					{
						PsionicSifrah.AwardInsight();
					}
				}
				base.Object.LoseEgo(base.Object.GetSeededRange("brainbrine", num, num), true);
			}
			return base.FireEvent(E);
		}
	}
}
