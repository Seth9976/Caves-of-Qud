using System;
using XRL.World.Anatomy;

namespace XRL.World.Parts
{
	// Token: 0x02000F3C RID: 3900
	[Serializable]
	public class CyberneticsPalladiumElectrodeposits : IPart
	{
		// Token: 0x06009B8B RID: 39819 RVA: 0x00386A3B File Offset: 0x00384C3B
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x06009B8C RID: 39820 RVA: 0x00386A3E File Offset: 0x00384C3E
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == SingletonEvent<GetAvailableComputePowerEvent>.ID || ID == ImplantedEvent.ID || ID == UnimplantedEvent.ID;
		}

		// Token: 0x06009B8D RID: 39821 RVA: 0x00386A64 File Offset: 0x00384C64
		public override bool HandleEvent(ImplantedEvent E)
		{
			BodyPart part = E.Part;
			if (((part != null) ? part.Type : null) == "Head")
			{
				base.StatShifter.SetStatShift(E.Implantee, "Intelligence", 2, true);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009B8E RID: 39822 RVA: 0x00386AA4 File Offset: 0x00384CA4
		public override bool HandleEvent(UnimplantedEvent E)
		{
			base.StatShifter.RemoveStatShifts(E.Implantee);
			return base.HandleEvent(E);
		}

		// Token: 0x06009B8F RID: 39823 RVA: 0x00386ABE File Offset: 0x00384CBE
		public override bool HandleEvent(GetAvailableComputePowerEvent E)
		{
			if (E.Actor == this.ParentObject.Implantee)
			{
				E.Amount += 20;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009B90 RID: 39824 RVA: 0x00386AE9 File Offset: 0x00384CE9
		public override bool AllowStaticRegistration()
		{
			return true;
		}
	}
}
