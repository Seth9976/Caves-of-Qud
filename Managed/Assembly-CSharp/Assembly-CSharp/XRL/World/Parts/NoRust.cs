using System;

namespace XRL.World.Parts
{
	// Token: 0x02001074 RID: 4212
	[Serializable]
	public class NoRust : IPart
	{
		// Token: 0x0600A7C0 RID: 42944 RVA: 0x003D4DF6 File Offset: 0x003D2FF6
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("ApplyRusted");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600A7C1 RID: 42945 RVA: 0x003D4E0B File Offset: 0x003D300B
		public override bool FireEvent(Event E)
		{
			return !(E.ID == "ApplyRusted") && base.FireEvent(E);
		}
	}
}
