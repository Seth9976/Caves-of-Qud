using System;

namespace XRL.World.Effects
{
	// Token: 0x02000D6F RID: 3439
	[Serializable]
	public class PhasedWhileStuck : Effect
	{
		// Token: 0x06008833 RID: 34867 RVA: 0x0030CE48 File Offset: 0x0030B048
		public PhasedWhileStuck()
		{
		}

		// Token: 0x06008834 RID: 34868 RVA: 0x0030CE50 File Offset: 0x0030B050
		public PhasedWhileStuck(int Duration)
			: this()
		{
			this.Duration = Duration;
		}

		// Token: 0x06008835 RID: 34869 RVA: 0x0030CE5F File Offset: 0x0030B05F
		public override int GetEffectType()
		{
			return 33554464;
		}

		// Token: 0x06008836 RID: 34870 RVA: 0x0030CE66 File Offset: 0x0030B066
		public override string GetDescription()
		{
			return null;
		}

		// Token: 0x06008837 RID: 34871 RVA: 0x0030CE69 File Offset: 0x0030B069
		public override bool SameAs(Effect e)
		{
			return false;
		}

		// Token: 0x06008838 RID: 34872 RVA: 0x0030CE6C File Offset: 0x0030B06C
		public override bool Apply(GameObject Object)
		{
			this.PhasedEffect = new Phased(9999);
			return Object.ApplyEffect(this.PhasedEffect, null);
		}

		// Token: 0x06008839 RID: 34873 RVA: 0x0030CE8C File Offset: 0x0030B08C
		public override void Remove(GameObject Object)
		{
			if (this.Duration < 0)
			{
				return;
			}
			this.Duration = -1;
			if (this.PhasedEffect == null || this.PhasedEffect.Object != Object)
			{
				Object.RemoveEffect<Phased>();
			}
			Object.RemoveEffect(this.PhasedEffect, true);
			base.Remove(Object);
		}

		// Token: 0x0600883A RID: 34874 RVA: 0x0030CEDC File Offset: 0x0030B0DC
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == SingletonEvent<EndTurnEvent>.ID;
		}

		// Token: 0x0600883B RID: 34875 RVA: 0x0030CEF2 File Offset: 0x0030B0F2
		public override bool HandleEvent(EndTurnEvent E)
		{
			if (!base.Object.HasEffect<Stuck>())
			{
				this.Duration--;
				if (this.Duration <= 0)
				{
					base.Object.RemoveEffect(this, true);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x04003A43 RID: 14915
		public Phased PhasedEffect;
	}
}
