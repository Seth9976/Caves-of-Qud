using System;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x02000EC2 RID: 3778
	[Serializable]
	public class Grabber : IPart
	{
		// Token: 0x06009783 RID: 38787 RVA: 0x00370706 File Offset: 0x0036E906
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("WeaponHit");
			base.Register(Object, Registrar);
		}

		// Token: 0x06009784 RID: 38788 RVA: 0x0037071C File Offset: 0x0036E91C
		public override bool SameAs(IPart Part)
		{
			Grabber grabber = Part as Grabber;
			return grabber.Chance == this.Chance && grabber.StuckDuration == this.StuckDuration && grabber.SaveTarget == this.SaveTarget && !(grabber.StuckAdjective != this.StuckAdjective) && !(grabber.StuckPreposition != this.StuckPreposition) && !(grabber.StuckSaveVs != this.StuckSaveVs) && !(grabber.CheckEvent != this.CheckEvent) && base.SameAs(Part);
		}

		// Token: 0x06009785 RID: 38789 RVA: 0x003707BC File Offset: 0x0036E9BC
		public override bool FireEvent(Event E)
		{
			if (E.ID == "WeaponHit" && this.ParentObject.hitpoints > 0)
			{
				GameObject equipped = this.ParentObject.Equipped;
				if (((equipped != null) ? equipped.CurrentCell : null) != null && this.Chance.in100())
				{
					GameObject gameObjectParameter = E.GetGameObjectParameter("Defender");
					if (this.CheckEvent.IsNullOrEmpty() || gameObjectParameter.FireEvent(this.CheckEvent))
					{
						Stuck stuck = new Stuck(this.StuckDuration, this.SaveTarget, this.StuckSaveVs, null, this.StuckAdjective, this.StuckPreposition, this.ParentObject.Equipped.ID, "Move", 0.25f, 1f, 1f, false, false);
						gameObjectParameter.ApplyEffect(stuck, null);
					}
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x04003EF9 RID: 16121
		public int Chance = 15;

		// Token: 0x04003EFA RID: 16122
		public int StuckDuration = 10;

		// Token: 0x04003EFB RID: 16123
		public int SaveTarget = 20;

		// Token: 0x04003EFC RID: 16124
		public string StuckAdjective = "grabbed";

		// Token: 0x04003EFD RID: 16125
		public string StuckPreposition = "by";

		// Token: 0x04003EFE RID: 16126
		public string StuckSaveVs = "Grab Stuck Restraint";

		// Token: 0x04003EFF RID: 16127
		public string CheckEvent = "BeforeGrabbed";
	}
}
