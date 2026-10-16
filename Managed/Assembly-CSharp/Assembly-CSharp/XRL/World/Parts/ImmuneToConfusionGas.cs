using System;

namespace XRL.World.Parts
{
	// Token: 0x020011F2 RID: 4594
	[Serializable]
	public class ImmuneToConfusionGas : IPart
	{
		// Token: 0x0600B6F7 RID: 46839 RVA: 0x0042AFA6 File Offset: 0x004291A6
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600B6F8 RID: 46840 RVA: 0x0042AFA9 File Offset: 0x004291A9
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("CanApplyConfusionGas");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600B6F9 RID: 46841 RVA: 0x0042AFBE File Offset: 0x004291BE
		public override bool FireEvent(Event E)
		{
			return !(E.ID == "CanApplyConfusionGas") && base.FireEvent(E);
		}
	}
}
