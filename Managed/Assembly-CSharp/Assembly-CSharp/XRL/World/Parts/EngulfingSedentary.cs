using System;

namespace XRL.World.Parts
{
	// Token: 0x02000EAF RID: 3759
	[Serializable]
	public class EngulfingSedentary : IPart
	{
		// Token: 0x060096E0 RID: 38624 RVA: 0x0036BC5F File Offset: 0x00369E5F
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x060096E1 RID: 38625 RVA: 0x0036BC62 File Offset: 0x00369E62
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == SingletonEvent<BeforeTakeActionEvent>.ID;
		}

		// Token: 0x060096E2 RID: 38626 RVA: 0x0036BC78 File Offset: 0x00369E78
		public override bool HandleEvent(BeforeTakeActionEvent E)
		{
			Engulfing part = this.ParentObject.GetPart<Engulfing>();
			if (((part != null) ? part.Engulfed : null) != null && part.CheckEngulfed())
			{
				this.ParentObject.UseEnergy(1000, "Digestion", null, null, false);
				return false;
			}
			return base.HandleEvent(E);
		}
	}
}
