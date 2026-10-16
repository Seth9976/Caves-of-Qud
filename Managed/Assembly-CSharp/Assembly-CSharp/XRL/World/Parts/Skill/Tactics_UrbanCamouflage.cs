using System;
using XRL.World.Effects;

namespace XRL.World.Parts.Skill
{
	// Token: 0x020013AC RID: 5036
	[Serializable]
	public class Tactics_UrbanCamouflage : BaseSkill
	{
		// Token: 0x0600C293 RID: 49811 RVA: 0x00471839 File Offset: 0x0046FA39
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("EndTurn");
			Registrar.Register("EnteredCell");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600C294 RID: 49812 RVA: 0x0047185C File Offset: 0x0046FA5C
		private void CheckCamouflage(bool ForceRemove = false)
		{
			UrbanCamouflaged urbanCamouflaged = this.ParentObject.GetEffect<UrbanCamouflaged>();
			bool flag = urbanCamouflaged != null;
			if (!flag)
			{
				urbanCamouflaged = new UrbanCamouflaged();
			}
			if (!ForceRemove && this.ParentObject.CurrentCell != null && this.ParentObject.CurrentCell.HasObjectOtherThan(new Predicate<GameObject>(urbanCamouflaged.EnablesCamouflage), this.ParentObject))
			{
				if (!flag)
				{
					this.ParentObject.ApplyEffect(urbanCamouflaged, null);
				}
				urbanCamouflaged.SetContribution(this.ParentObject, this.Level);
				return;
			}
			if (flag)
			{
				urbanCamouflaged.RemoveContribution(this.ParentObject);
			}
		}

		// Token: 0x0600C295 RID: 49813 RVA: 0x004718EB File Offset: 0x0046FAEB
		public override bool FireEvent(Event E)
		{
			if (E.ID == "EndTurn" || E.ID == "EnteredCell")
			{
				this.CheckCamouflage(false);
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600C296 RID: 49814 RVA: 0x0047191F File Offset: 0x0046FB1F
		public override bool AddSkill(GameObject GO)
		{
			this.CheckCamouflage(false);
			return true;
		}

		// Token: 0x0600C297 RID: 49815 RVA: 0x00471929 File Offset: 0x0046FB29
		public override bool RemoveSkill(GameObject GO)
		{
			this.CheckCamouflage(true);
			return true;
		}

		// Token: 0x04004DA1 RID: 19873
		public int Level = 4;
	}
}
