using System;
using XRL.World.Effects;

namespace XRL.World.Parts.Skill
{
	// Token: 0x020013A3 RID: 5027
	[Serializable]
	public class Tactics_Camouflage : BaseSkill
	{
		// Token: 0x0600C24D RID: 49741 RVA: 0x0046F19A File Offset: 0x0046D39A
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("EndTurn");
			Registrar.Register("EnteredCell");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600C24E RID: 49742 RVA: 0x0046F1BC File Offset: 0x0046D3BC
		private void CheckCamouflage(bool ForceRemove = false)
		{
			FoliageCamouflaged foliageCamouflaged = this.ParentObject.GetEffect<FoliageCamouflaged>();
			bool flag = foliageCamouflaged != null;
			if (!flag)
			{
				foliageCamouflaged = new FoliageCamouflaged();
			}
			if (!ForceRemove && this.ParentObject.CurrentCell != null && this.ParentObject.CurrentCell.HasObjectOtherThan(new Predicate<GameObject>(foliageCamouflaged.EnablesCamouflage), this.ParentObject))
			{
				if (!flag)
				{
					this.ParentObject.ApplyEffect(foliageCamouflaged, null);
				}
				foliageCamouflaged.SetContribution(this.ParentObject, this.Level);
				return;
			}
			if (flag)
			{
				foliageCamouflaged.RemoveContribution(this.ParentObject);
			}
		}

		// Token: 0x0600C24F RID: 49743 RVA: 0x0046F24B File Offset: 0x0046D44B
		public override bool FireEvent(Event E)
		{
			if (E.ID == "EndTurn" || E.ID == "EnteredCell")
			{
				this.CheckCamouflage(false);
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600C250 RID: 49744 RVA: 0x0046F27F File Offset: 0x0046D47F
		public override bool AddSkill(GameObject GO)
		{
			this.CheckCamouflage(false);
			return base.AddSkill(GO);
		}

		// Token: 0x0600C251 RID: 49745 RVA: 0x0046F28F File Offset: 0x0046D48F
		public override bool RemoveSkill(GameObject GO)
		{
			this.CheckCamouflage(true);
			return base.AddSkill(GO);
		}

		// Token: 0x04004D98 RID: 19864
		public int Level = 4;
	}
}
