using System;
using XRL.Messages;

namespace XRL.World.Parts.Mutation
{
	// Token: 0x0200142F RID: 5167
	[Serializable]
	public class ColdAbsorption : BaseMutation
	{
		// Token: 0x0600C984 RID: 51588 RVA: 0x00496326 File Offset: 0x00494526
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("BeforeApplyDamage");
			Registrar.Register("BeforeTemperatureChange");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600C985 RID: 51589 RVA: 0x00496346 File Offset: 0x00494546
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetItemElementsEvent>.ID;
		}

		// Token: 0x0600C986 RID: 51590 RVA: 0x0049635C File Offset: 0x0049455C
		public override bool HandleEvent(GetItemElementsEvent E)
		{
			if (E.IsRelevantCreature(this.ParentObject))
			{
				E.Add("ice", 1);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C987 RID: 51591 RVA: 0x0049637F File Offset: 0x0049457F
		public override string GetDescription()
		{
			return "You regenerate by absorbing cold.";
		}

		// Token: 0x0600C988 RID: 51592 RVA: 0x00496388 File Offset: 0x00494588
		public override string GetLevelText(int Level)
		{
			string text = "Immune to heat damage\n";
			if (Level > 1)
			{
				text = text + "Whenever you would have taken cold damage, you heal for 0." + (Level - 1).ToString() + "% of that damage instead";
			}
			return text;
		}

		// Token: 0x0600C989 RID: 51593 RVA: 0x004963BC File Offset: 0x004945BC
		public override bool FireEvent(Event E)
		{
			if (E.ID == "BeforeApplyDamage")
			{
				Damage damage = E.GetParameter("Damage") as Damage;
				if (damage.HasAttribute("Ice") || damage.HasAttribute("Cold"))
				{
					if (base.Level > 1)
					{
						int num = (int)Math.Ceiling((double)((float)damage.Amount * (0.1f * (float)(base.Level - 1))));
						if (num > 0 && this.ParentObject.Statistics["Hitpoints"].Penalty > 0)
						{
							if (this.ParentObject.IsPlayer())
							{
								MessageQueue.AddPlayerMessage("You are healed for " + num.ToString() + " by the cold.", null, true);
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
					if (intParameter < 0 && (double)this.ParentObject.Physics.Temperature - (double)intParameter * 0.05 < 25.0)
					{
						E.SetParameter("Amount", (int)((double)(25 - this.ParentObject.Physics.Temperature) / 0.05));
					}
				}
				else if (intParameter < 0 && this.ParentObject.Physics.Temperature < 25)
				{
					E.SetParameter("Amount", 25 - this.ParentObject.Physics.Temperature);
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600C98A RID: 51594 RVA: 0x0049655B File Offset: 0x0049475B
		public override bool ChangeLevel(int NewLevel)
		{
			return base.ChangeLevel(NewLevel);
		}

		// Token: 0x0600C98B RID: 51595 RVA: 0x00496564 File Offset: 0x00494764
		public override bool Mutate(GameObject GO, int Level)
		{
			return base.Mutate(GO, Level);
		}

		// Token: 0x0600C98C RID: 51596 RVA: 0x0049656E File Offset: 0x0049476E
		public override bool Unmutate(GameObject GO)
		{
			return base.Unmutate(GO);
		}
	}
}
