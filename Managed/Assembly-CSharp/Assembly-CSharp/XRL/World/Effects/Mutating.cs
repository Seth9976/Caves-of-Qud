using System;
using Qud.API;
using XRL.Language;
using XRL.Rules;
using XRL.UI;

namespace XRL.World.Effects
{
	// Token: 0x02000D66 RID: 3430
	[Serializable]
	public class Mutating : Effect, ITierInitialized
	{
		// Token: 0x060087B9 RID: 34745 RVA: 0x0030ACF4 File Offset: 0x00308EF4
		public Mutating()
		{
			this.DisplayName = "{{M|mutating}}";
		}

		// Token: 0x060087BA RID: 34746 RVA: 0x0030AD12 File Offset: 0x00308F12
		public Mutating(int Duration, string Population = "MutatingResults")
			: this()
		{
			this.Duration = Duration;
			this.Population = Population;
			this.PermuteMutationsAt = Duration / 2;
		}

		// Token: 0x060087BB RID: 34747 RVA: 0x0030AD31 File Offset: 0x00308F31
		public Mutating(int Duration, int PermuteMutationsAt, string Population = "MutatingResults")
			: this(Duration, Population)
		{
			this.PermuteMutationsAt = PermuteMutationsAt;
		}

		// Token: 0x060087BC RID: 34748 RVA: 0x0030AD42 File Offset: 0x00308F42
		public void Initialize(int Tier)
		{
			this.Duration = Stat.Roll(100, 200);
			this.PermuteMutationsAt = this.Duration / 2;
		}

		// Token: 0x060087BD RID: 34749 RVA: 0x0030AD64 File Offset: 0x00308F64
		public override int GetEffectType()
		{
			return 100663300;
		}

		// Token: 0x060087BE RID: 34750 RVA: 0x0030AD6C File Offset: 0x00308F6C
		public override bool SameAs(Effect e)
		{
			Mutating mutating = e as Mutating;
			return !(mutating.Population != this.Population) && mutating.Triggered == this.Triggered && mutating.PermuteMutationsAt == this.PermuteMutationsAt && mutating.MutationsPermuted == this.MutationsPermuted && base.SameAs(e);
		}

		// Token: 0x060087BF RID: 34751 RVA: 0x0030ADCC File Offset: 0x00308FCC
		public override string GetDetails()
		{
			return "In the process of mutating.";
		}

		// Token: 0x060087C0 RID: 34752 RVA: 0x0030ADD4 File Offset: 0x00308FD4
		public override bool Apply(GameObject Object)
		{
			if (!Object.IsMutant() && !Object.IsTrueKin())
			{
				return false;
			}
			if (Object.HasEffect<Mutating>())
			{
				return false;
			}
			if (!Object.FireEvent("ApplyMutating"))
			{
				return false;
			}
			Object.PlayWorldSound("Sounds/StatusEffects/sfx_statusEffect_neutral-weirdVitality", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
			if (Object.IsPlayer())
			{
				IComponent<GameObject>.AddPlayerMessage("You start to feel unstable.", 'M', true);
			}
			return base.Apply(Object);
		}

		// Token: 0x060087C1 RID: 34753 RVA: 0x0030AE52 File Offset: 0x00309052
		public override void Remove(GameObject Object)
		{
		}

		// Token: 0x060087C2 RID: 34754 RVA: 0x0030AE54 File Offset: 0x00309054
		public override bool WantEvent(int ID, int Cascade)
		{
			return base.WantEvent(ID, Cascade) || ID == SingletonEvent<EndTurnEvent>.ID;
		}

		// Token: 0x060087C3 RID: 34755 RVA: 0x0030AE6C File Offset: 0x0030906C
		public override bool HandleEvent(EndTurnEvent E)
		{
			if (this.Duration > 0)
			{
				this.Duration--;
				if (this.Duration < this.PermuteMutationsAt && !this.MutationsPermuted)
				{
					this.MutationsPermuted = true;
					base.Object.PermuteRandomMutationBuys();
					if (base.Object.IsPlayer())
					{
						IComponent<GameObject>.AddPlayerMessage("You feel increasingly unstable.", 'M', true);
						base.Object.PlayWorldSound("Sounds/StatusEffects/sfx_statusEffect_neutral-weirdVitality", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
					}
				}
				if (this.Duration <= 0 && !this.Triggered)
				{
					this.Triggered = true;
					string blueprint = PopulationManager.RollOneFrom(this.Population, null, null).Blueprint;
					if (blueprint == "Mutation")
					{
						MutationEntry mutationEntry = MutationsAPI.FindRandomMutationFor(base.Object, (MutationEntry e) => !e.IsDefect(), null, false);
						if (mutationEntry != null)
						{
							base.PlayWorldSound("Sounds/Misc/sfx_characterMod_mutation_positive", 0.5f, 0f, false, null, 0f, 1f, 1f, int.MaxValue);
							if (base.Object.IsPlayer())
							{
								Popup.Show("Your genome destabilizes and you gain a new mutation:\n\n{{W|" + mutationEntry.GetDisplayName(false) + "}}", null, null, true, true, true, true, null);
								Achievement.MUTATION_FROM_GAMMAMOTH.Unlock();
							}
							MutationsAPI.ApplyMutationTo(base.Object, mutationEntry);
						}
					}
					else if (blueprint == "Defect")
					{
						MutationEntry mutationEntry2 = MutationsAPI.FindRandomMutationFor(base.Object, (MutationEntry e) => e.IsDefect(), null, true);
						if (mutationEntry2 != null)
						{
							base.PlayWorldSound("Sounds/Misc/sfx_characterMod_mutation_negative", 0.5f, 0f, false, null, 0f, 1f, 1f, int.MaxValue);
							if (base.Object.IsPlayer())
							{
								Popup.Show("Your genome destabilizes and you gain a new defect:\n\n{{W|" + mutationEntry2.GetDisplayName(false) + "}}", null, null, true, true, true, true, null);
							}
							MutationsAPI.ApplyMutationTo(base.Object, mutationEntry2);
						}
					}
					else if (blueprint.StartsWith("Points:"))
					{
						int num = Stat.Roll(blueprint.Split(':', StringSplitOptions.None)[1], null);
						if ((base.Object.IsMutant() || base.Object.IsTrueKin()) && base.Object.GainMP(num) && base.Object.IsPlayer())
						{
							Popup.Show(string.Concat(new string[]
							{
								"Your genome destabilizes and you gain ",
								Grammar.Cardinal(num),
								" mutation ",
								(num == 1) ? "point" : "points",
								"."
							}), null, "Sounds/UI/ui_notification", true, true, true, true, null);
						}
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x04003A28 RID: 14888
		public string Population = "MutatingResults";

		// Token: 0x04003A29 RID: 14889
		public bool Triggered;

		// Token: 0x04003A2A RID: 14890
		public int PermuteMutationsAt;

		// Token: 0x04003A2B RID: 14891
		public bool MutationsPermuted;
	}
}
