using System;

namespace XRL.World.Parts
{
	// Token: 0x02000EE8 RID: 3816
	[Serializable]
	public class NoKnockdown : IActivePart
	{
		// Token: 0x06009895 RID: 39061 RVA: 0x0037696F File Offset: 0x00374B6F
		public NoKnockdown()
		{
			this.WorksOnSelf = true;
			this.IsBreakageSensitive = false;
			this.IsRustSensitive = false;
		}

		// Token: 0x06009896 RID: 39062 RVA: 0x0037698C File Offset: 0x00374B8C
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x06009897 RID: 39063 RVA: 0x0037698F File Offset: 0x00374B8F
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("ApplyProne");
			base.Register(Object, Registrar);
		}

		// Token: 0x06009898 RID: 39064 RVA: 0x003769A4 File Offset: 0x00374BA4
		public override bool FireEvent(Event E)
		{
			return (!(E.ID == "ApplyProne") || !base.IsReady(true, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null)) && base.FireEvent(E);
		}
	}
}
