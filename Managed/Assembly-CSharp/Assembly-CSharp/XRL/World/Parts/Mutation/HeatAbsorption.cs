using System;
using XRL.Messages;

namespace XRL.World.Parts.Mutation
{
	// Token: 0x02001443 RID: 5187
	[Serializable]
	public class HeatAbsorption : BaseMutation
	{
		// Token: 0x0600CA86 RID: 51846 RVA: 0x0049AD72 File Offset: 0x00498F72
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("BeforeApplyDamage");
			Registrar.Register("BeforeTemperatureChange");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600CA87 RID: 51847 RVA: 0x0049AD92 File Offset: 0x00498F92
		public override string GetDescription()
		{
			return "You regenerate by absorbing heat.";
		}

		// Token: 0x0600CA88 RID: 51848 RVA: 0x0049AD9C File Offset: 0x00498F9C
		public override string GetLevelText(int Level)
		{
			string text = "Immune to heat damage\n";
			if (Level > 1)
			{
				text = text + "Whenever you would have taken heat damage, you heal for " + ((Level - 1) * 10).ToString() + "% of that damage instead";
			}
			return text;
		}

		// Token: 0x0600CA89 RID: 51849 RVA: 0x0049ADD4 File Offset: 0x00498FD4
		public override bool FireEvent(Event E)
		{
			if (E.ID == "BeforeApplyDamage")
			{
				Damage damage = E.GetParameter("Damage") as Damage;
				if (damage.HasAttribute("Fire") || damage.HasAttribute("Heat"))
				{
					if (base.Level > 1)
					{
						int num = (int)Math.Ceiling((double)((float)damage.Amount * (0.1f * (float)(base.Level - 1))));
						if (num > 0 && this.ParentObject.Statistics["Hitpoints"].Penalty > 0)
						{
							if (this.ParentObject.IsPlayer())
							{
								MessageQueue.AddPlayerMessage("You are healed for " + num.ToString() + " by the heat.", null, true);
							}
							this.ParentObject.Heal(num, false, false, false);
						}
					}
					return false;
				}
			}
			else if (E.ID == "BeforeTemperatureChange")
			{
				int intParameter = E.GetIntParameter("Amount", 0);
				if (E.HasFlag("Radiant"))
				{
					if (intParameter > 0 && this.ParentObject.Physics.Temperature > 0)
					{
						E.SetParameter("Amount", (int)((double)(25 - this.ParentObject.Physics.Temperature) / 0.05));
					}
				}
				else if (intParameter > 0 && this.ParentObject.Physics.Temperature > 0)
				{
					E.SetParameter("Amount", 25 - this.ParentObject.Physics.Temperature);
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600CA8A RID: 51850 RVA: 0x0049AF59 File Offset: 0x00499159
		public override bool ChangeLevel(int NewLevel)
		{
			return base.ChangeLevel(NewLevel);
		}

		// Token: 0x0600CA8B RID: 51851 RVA: 0x0049AF62 File Offset: 0x00499162
		public override bool Mutate(GameObject GO, int Level)
		{
			return base.Mutate(GO, Level);
		}

		// Token: 0x0600CA8C RID: 51852 RVA: 0x0049AF6C File Offset: 0x0049916C
		public override bool Unmutate(GameObject GO)
		{
			return base.Unmutate(GO);
		}
	}
}
