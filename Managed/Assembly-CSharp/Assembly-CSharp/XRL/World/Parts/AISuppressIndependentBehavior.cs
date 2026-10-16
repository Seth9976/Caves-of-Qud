using System;

namespace XRL.World.Parts
{
	// Token: 0x02000E2D RID: 3629
	[Serializable]
	public class AISuppressIndependentBehavior : AIBehaviorPart
	{
		// Token: 0x0600936C RID: 37740 RVA: 0x0035190E File Offset: 0x0034FB0E
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600936D RID: 37741 RVA: 0x00351911 File Offset: 0x0034FB11
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("CanAIDoIndependentBehavior");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600936E RID: 37742 RVA: 0x00351926 File Offset: 0x0034FB26
		public override bool FireEvent(Event E)
		{
			return !(E.ID == "CanAIDoIndependentBehavior") && base.FireEvent(E);
		}
	}
}
