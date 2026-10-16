using System;
using XRL.Messages;

namespace XRL.World.Parts.Mutation
{
	// Token: 0x020013CB RID: 5067
	[Serializable]
	public class BrittleBones : BaseMutation
	{
		// Token: 0x0600C3C4 RID: 50116 RVA: 0x00476764 File Offset: 0x00474964
		public override bool CanLevel()
		{
			return false;
		}

		// Token: 0x0600C3C5 RID: 50117 RVA: 0x00476767 File Offset: 0x00474967
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("BeforeApplyDamage");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600C3C6 RID: 50118 RVA: 0x0047677C File Offset: 0x0047497C
		public override string GetDescription()
		{
			return "Your bones are brittle.\n\nYou suffer 50% more damage from bludgeoning attacks, falling, and other sources of concussive damage.";
		}

		// Token: 0x0600C3C7 RID: 50119 RVA: 0x00476783 File Offset: 0x00474983
		public override string GetLevelText(int Level)
		{
			return "";
		}

		// Token: 0x0600C3C8 RID: 50120 RVA: 0x0047678C File Offset: 0x0047498C
		public override bool FireEvent(Event E)
		{
			if (E.ID == "BeforeApplyDamage")
			{
				Damage damage = E.GetParameter("Damage") as Damage;
				if ((damage.HasAttribute("Crushing") || damage.HasAttribute("Cudgel") || damage.HasAttribute("Concussion")) && damage.Amount > 0)
				{
					if (this.ParentObject.IsPlayer())
					{
						MessageQueue.AddPlayerMessage("You feel your bones fracture.", 'r', true);
					}
					damage.Amount = (int)((double)damage.Amount * 1.5);
					E.SetParameter("Damage", damage);
				}
			}
			return true;
		}

		// Token: 0x0600C3C9 RID: 50121 RVA: 0x0047682F File Offset: 0x00474A2F
		public override bool ChangeLevel(int NewLevel)
		{
			return base.ChangeLevel(NewLevel);
		}

		// Token: 0x0600C3CA RID: 50122 RVA: 0x00476838 File Offset: 0x00474A38
		public override bool Mutate(GameObject GO, int Level)
		{
			return base.Mutate(GO, Level);
		}

		// Token: 0x0600C3CB RID: 50123 RVA: 0x00476842 File Offset: 0x00474A42
		public override bool Unmutate(GameObject GO)
		{
			return base.Unmutate(GO);
		}
	}
}
