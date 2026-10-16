using System;

namespace XRL.World.Parts
{
	// Token: 0x02000E4C RID: 3660
	[Serializable]
	public class AnimatedObject : IPart
	{
		// Token: 0x06009456 RID: 37974 RVA: 0x003565D6 File Offset: 0x003547D6
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x06009457 RID: 37975 RVA: 0x003565D9 File Offset: 0x003547D9
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetDisplayNameEvent>.ID;
		}

		// Token: 0x06009458 RID: 37976 RVA: 0x003565EF File Offset: 0x003547EF
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (!E.Object.HasProperName && !E.Object.HasTagOrProperty("NoAnimatedNamePrefix"))
			{
				E.AddAdjective("animated", 5);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009459 RID: 37977 RVA: 0x00356623 File Offset: 0x00354823
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("HasPowerConnectors");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600945A RID: 37978 RVA: 0x00356638 File Offset: 0x00354838
		public override bool FireEvent(Event E)
		{
			return !(E.ID == "HasPowerConnectors") && base.FireEvent(E);
		}
	}
}
