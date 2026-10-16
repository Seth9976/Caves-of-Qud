using System;
using System.Collections.Generic;

namespace XRL.World.Parts
{
	// Token: 0x02000E5E RID: 3678
	[Serializable]
	public class CooldownOnStep : IPart
	{
		// Token: 0x060094C8 RID: 38088 RVA: 0x0035C9C4 File Offset: 0x0035ABC4
		public override bool SameAs(IPart Part)
		{
			CooldownOnStep cooldownOnStep = Part as CooldownOnStep;
			return !(cooldownOnStep.ClusterSize != this.ClusterSize) && !(cooldownOnStep.CooldownDamage != this.CooldownDamage) && cooldownOnStep.NeedsToBeHidden == this.NeedsToBeHidden && base.SameAs(Part);
		}

		// Token: 0x060094C9 RID: 38089 RVA: 0x0035CA19 File Offset: 0x0035AC19
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == EnteredCellEvent.ID || ID == ObjectEnteredCellEvent.ID;
		}

		// Token: 0x060094CA RID: 38090 RVA: 0x0035CA38 File Offset: 0x0035AC38
		public override bool HandleEvent(EnteredCellEvent E)
		{
			if (this.ClusterSize != "1")
			{
				List<Cell> list = new List<Cell>(this.ParentObject.CurrentCell.GetLocalEmptyAdjacentCells()).ShuffleInPlace<Cell>(null);
				int num = this.ClusterSize.RollCached();
				int num2 = 0;
				while (num2 < num && num2 < list.Count)
				{
					if (15.in100())
					{
						list[num2].AddObject(this.ParentObject.Blueprint, null, null, null, null);
					}
					else
					{
						GameObject gameObject = GameObject.Create(this.ParentObject.Blueprint);
						CooldownOnStep cooldownOnStep;
						if (gameObject.TryGetPart<CooldownOnStep>(out cooldownOnStep))
						{
							cooldownOnStep.ClusterSize = "1";
						}
						list[num2].AddObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
					}
					num2++;
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x060094CB RID: 38091 RVA: 0x0035CB0C File Offset: 0x0035AD0C
		public override bool HandleEvent(ObjectEnteredCellEvent E)
		{
			GameObject @object = E.Object;
			if (((@object != null) ? @object.ActivatedAbilities : null) != null && this.ParentObject.IsHostileTowards(E.Object) && this.ParentObject.PhaseAndFlightMatches(E.Object))
			{
				Hidden part = this.ParentObject.GetPart<Hidden>();
				if (!this.NeedsToBeHidden || !part.Found)
				{
					if (part != null)
					{
						part.Reveal();
					}
					base.DidXToY("prick", E.Object, "with " + this.ParentObject.its + " neuronal thorns", null, null, null, null, E.Object, false, false, false, false, false, null, null, null, false, false, false, false, false, null);
					ActivatedAbilities activatedAbilities = E.Object.ActivatedAbilities;
					if (((activatedAbilities != null) ? activatedAbilities.AbilityByGuid : null) != null)
					{
						int num = this.CooldownDamage.RollCached();
						foreach (ActivatedAbilityEntry activatedAbilityEntry in activatedAbilities.AbilityByGuid.Values)
						{
							activatedAbilityEntry.AddScaledCooldown(num);
						}
					}
					E.Object.Splatter("^B!");
				}
			}
			else if (E.Object != null && E.Object.IsPlayer() && !this.ParentObject.IsHostileTowards(E.Object))
			{
				Hidden part2 = this.ParentObject.GetPart<Hidden>();
				if (part2 != null)
				{
					part2.Reveal();
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x060094CC RID: 38092 RVA: 0x0035CC90 File Offset: 0x0035AE90
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x04003D8E RID: 15758
		public string ClusterSize = "1";

		// Token: 0x04003D8F RID: 15759
		public string CooldownDamage = "1d10";

		// Token: 0x04003D90 RID: 15760
		public bool NeedsToBeHidden = true;
	}
}
