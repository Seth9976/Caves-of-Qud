using System;

namespace XRL.World.Parts
{
	// Token: 0x02000F90 RID: 3984
	[Serializable]
	public class TemperatureOnEat : IPart
	{
		// Token: 0x06009E14 RID: 40468 RVA: 0x00393989 File Offset: 0x00391B89
		public override bool SameAs(IPart p)
		{
			return true;
		}

		// Token: 0x06009E15 RID: 40469 RVA: 0x0039398C File Offset: 0x00391B8C
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("OnEat");
			base.Register(Object, Registrar);
		}

		// Token: 0x06009E16 RID: 40470 RVA: 0x003939A1 File Offset: 0x00391BA1
		public override bool FireEvent(Event E)
		{
			if (E.ID == "OnEat")
			{
				E.GetGameObjectParameter("Eater").Physics.Temperature += 100;
			}
			return base.FireEvent(E);
		}
	}
}
