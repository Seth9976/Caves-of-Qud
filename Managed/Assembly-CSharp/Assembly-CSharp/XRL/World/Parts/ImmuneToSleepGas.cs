using System;

namespace XRL.World.Parts
{
	// Token: 0x020011F3 RID: 4595
	[Serializable]
	public class ImmuneToSleepGas : IPart
	{
		// Token: 0x0600B6FB RID: 46843 RVA: 0x0042AFE3 File Offset: 0x004291E3
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600B6FC RID: 46844 RVA: 0x0042AFE6 File Offset: 0x004291E6
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("CanApplySleepGas");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600B6FD RID: 46845 RVA: 0x0042AFFB File Offset: 0x004291FB
		public override bool FireEvent(Event E)
		{
			return !(E.ID == "CanApplySleepGas") && base.FireEvent(E);
		}
	}
}
