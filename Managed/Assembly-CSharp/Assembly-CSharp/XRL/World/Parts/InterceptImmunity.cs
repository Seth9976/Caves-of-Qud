using System;

namespace XRL.World.Parts
{
	// Token: 0x020011F4 RID: 4596
	[Serializable]
	public class InterceptImmunity : IPart
	{
		// Token: 0x0600B6FF RID: 46847 RVA: 0x0042B020 File Offset: 0x00429220
		public override bool SameAs(IPart p)
		{
			return !((p as InterceptImmunity).Except == this.Except) && base.SameAs(p);
		}

		// Token: 0x0600B700 RID: 46848 RVA: 0x0042B043 File Offset: 0x00429243
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<PointDefenseInterceptEvent>.ID;
		}

		// Token: 0x0600B701 RID: 46849 RVA: 0x0042B059 File Offset: 0x00429259
		public override bool HandleEvent(PointDefenseInterceptEvent E)
		{
			return (E.TargetProjectile != this.ParentObject || !this.ImmuneTo(E.InterceptProjectile)) && base.HandleEvent(E);
		}

		// Token: 0x0600B702 RID: 46850 RVA: 0x0042B080 File Offset: 0x00429280
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600B703 RID: 46851 RVA: 0x0042B084 File Offset: 0x00429284
		public bool ImmuneTo(GameObject Projectile)
		{
			if (this.Except.IsNullOrEmpty())
			{
				return true;
			}
			Projectile projectile = ((Projectile != null) ? Projectile.GetPart<Projectile>() : null);
			return projectile == null || projectile.Attributes == null || !projectile.Attributes.HasDelimitedSubstring(' ', this.Except, StringComparison.Ordinal);
		}

		// Token: 0x040049BC RID: 18876
		public string Except;
	}
}
