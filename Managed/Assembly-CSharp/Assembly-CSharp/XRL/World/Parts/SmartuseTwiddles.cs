using System;

namespace XRL.World.Parts
{
	// Token: 0x02001288 RID: 4744
	[Serializable]
	public class SmartuseTwiddles : IPart
	{
		// Token: 0x0600BBAC RID: 48044 RVA: 0x0044881E File Offset: 0x00446A1E
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600BBAD RID: 48045 RVA: 0x00448821 File Offset: 0x00446A21
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("CanSmartUse");
			Registrar.Register("CommandSmartUse");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600BBAE RID: 48046 RVA: 0x00448841 File Offset: 0x00446A41
		public override bool FireEvent(Event E)
		{
			if (E.ID == "CanSmartUse")
			{
				return false;
			}
			if (E.ID == "CommandSmartUse")
			{
				this.ParentObject.Twiddle(false);
			}
			return base.FireEvent(E);
		}
	}
}
