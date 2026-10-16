using System;

namespace XRL.World.Parts
{
	// Token: 0x02000FC5 RID: 4037
	[Serializable]
	public class GasGlitter : IGasBehavior
	{
		// Token: 0x0600A08F RID: 41103 RVA: 0x003A7263 File Offset: 0x003A5463
		public override bool SameAs(IPart p)
		{
			return !((p as GasGlitter).GasType != this.GasType) && base.SameAs(p);
		}

		// Token: 0x0600A090 RID: 41104 RVA: 0x003A7286 File Offset: 0x003A5486
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("RefractLight");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600A091 RID: 41105 RVA: 0x003A729C File Offset: 0x003A549C
		public override bool FireEvent(Event E)
		{
			if (E.ID == "RefractLight")
			{
				Gas part = this.ParentObject.GetPart<Gas>();
				if (part != null && part.Density.in100())
				{
					E.SetParameter("By", this.ParentObject);
					return false;
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x040042B7 RID: 17079
		public string GasType = "Glitter";
	}
}
