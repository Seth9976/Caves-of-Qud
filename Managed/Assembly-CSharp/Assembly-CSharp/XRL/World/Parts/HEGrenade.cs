using System;

namespace XRL.World.Parts
{
	// Token: 0x02000FD4 RID: 4052
	[Serializable]
	public class HEGrenade : IGrenade
	{
		// Token: 0x0600A107 RID: 41223 RVA: 0x003A9968 File Offset: 0x003A7B68
		public override bool SameAs(IPart p)
		{
			HEGrenade hegrenade = p as HEGrenade;
			return hegrenade.Force == this.Force && !(hegrenade.Damage != this.Damage) && base.SameAs(p);
		}

		// Token: 0x0600A108 RID: 41224 RVA: 0x003A99A8 File Offset: 0x003A7BA8
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == GetComponentNavigationWeightEvent.ID || ID == GetComponentAdjacentNavigationWeightEvent.ID || ID == PooledEvent<IsExplosiveEvent>.ID;
		}

		// Token: 0x0600A109 RID: 41225 RVA: 0x003A99CE File Offset: 0x003A7BCE
		public override bool HandleEvent(GetComponentNavigationWeightEvent E)
		{
			E.MinWeight(10);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A10A RID: 41226 RVA: 0x003A99DF File Offset: 0x003A7BDF
		public override bool HandleEvent(GetComponentAdjacentNavigationWeightEvent E)
		{
			E.MinWeight(10);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A10B RID: 41227 RVA: 0x003A99F0 File Offset: 0x003A7BF0
		public override bool HandleEvent(IsExplosiveEvent E)
		{
			return false;
		}

		// Token: 0x0600A10C RID: 41228 RVA: 0x003A99F4 File Offset: 0x003A7BF4
		protected override bool DoDetonate(Cell C, GameObject Actor = null, GameObject ApparentTarget = null, bool Indirect = false)
		{
			if (this.detonating)
			{
				return true;
			}
			this.detonating = true;
			try
			{
				base.PlayWorldSound(base.GetPropertyOrTag("DetonatedSound", null), 1f, 0f, true, null, 0f, 1f, 1f, int.MaxValue);
				base.DidX("explode", null, "!", null, null, null, null, false, false, null, null, false, false, false, false, false, null);
				this.ParentObject.Explode(this.Force, Actor, this.Damage, 1f, false, false, Indirect, 0, null);
			}
			finally
			{
				this.detonating = false;
			}
			return true;
		}

		// Token: 0x040042E6 RID: 17126
		public int Force = 10000;

		// Token: 0x040042E7 RID: 17127
		public string Damage = "2d6";

		// Token: 0x040042E8 RID: 17128
		[NonSerialized]
		private bool detonating;
	}
}
