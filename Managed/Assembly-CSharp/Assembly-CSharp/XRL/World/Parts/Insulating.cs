using System;

namespace XRL.World.Parts
{
	// Token: 0x02001052 RID: 4178
	[Serializable]
	public class Insulating : IPart
	{
		// Token: 0x0600A65B RID: 42587 RVA: 0x003CDFB8 File Offset: 0x003CC1B8
		public override bool SameAs(IPart p)
		{
			return (p as Insulating).Amount == this.Amount && base.SameAs(p);
		}

		// Token: 0x0600A65C RID: 42588 RVA: 0x003CDFD6 File Offset: 0x003CC1D6
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<BeforeTemperatureChangeEvent>.ID || ID == BeforeApplyDamageEvent.ID || ID == GetShortDescriptionEvent.ID;
		}

		// Token: 0x0600A65D RID: 42589 RVA: 0x003CDFFC File Offset: 0x003CC1FC
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			if (this.Amount != 1f)
			{
				string text = ((int)((1f - this.Amount) * 100f)).ToString() + "%";
				E.Postfix.AppendRules(string.Concat(new string[] { "Severity of cooling effects reduced by ", text, ". Cold damage reduced by ", text, "." }));
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A65E RID: 42590 RVA: 0x003CE07C File Offset: 0x003CC27C
		public override bool HandleEvent(BeforeTemperatureChangeEvent E)
		{
			if (E.Amount < 0 && (E.Object == this.ParentObject.Equipped || E.Object == this.ParentObject))
			{
				E.Amount = (int)((float)E.Amount * this.Amount);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A65F RID: 42591 RVA: 0x003CE0D0 File Offset: 0x003CC2D0
		public override bool HandleEvent(BeforeApplyDamageEvent E)
		{
			if ((E.Object == this.ParentObject.Equipped || E.Object == this.ParentObject) && E.Damage.IsColdDamage())
			{
				E.Damage.Amount = (int)((float)E.Damage.Amount * this.Amount);
				if (E.Damage.Amount <= 0)
				{
					return false;
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A660 RID: 42592 RVA: 0x003CE141 File Offset: 0x003CC341
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x040044B3 RID: 17587
		public float Amount = 0.9f;
	}
}
