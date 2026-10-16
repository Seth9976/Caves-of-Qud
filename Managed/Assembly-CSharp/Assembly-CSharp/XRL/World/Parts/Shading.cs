using System;

namespace XRL.World.Parts
{
	// Token: 0x02001095 RID: 4245
	[Serializable]
	public class Shading : IPart
	{
		// Token: 0x0600A8EF RID: 43247 RVA: 0x003DCC30 File Offset: 0x003DAE30
		public override bool SameAs(IPart p)
		{
			return (p as Shading).Amount == this.Amount && base.SameAs(p);
		}

		// Token: 0x0600A8F0 RID: 43248 RVA: 0x003DCC4E File Offset: 0x003DAE4E
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<BeforeTemperatureChangeEvent>.ID || ID == BeforeApplyDamageEvent.ID || ID == GetShortDescriptionEvent.ID;
		}

		// Token: 0x0600A8F1 RID: 43249 RVA: 0x003DCC74 File Offset: 0x003DAE74
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			if (this.Amount != 1f)
			{
				string text = ((int)((1f - this.Amount) * 100f)).ToString() + "%";
				E.Postfix.AppendRules(string.Concat(new string[] { "Severity of heating effects reduced by ", text, ". Heat damage reduced by ", text, "." }));
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A8F2 RID: 43250 RVA: 0x003DCCF4 File Offset: 0x003DAEF4
		public override bool HandleEvent(BeforeTemperatureChangeEvent E)
		{
			if (E.Amount > 0 && (E.Object == this.ParentObject.Equipped || E.Object == this.ParentObject))
			{
				E.Amount = (int)((float)E.Amount * this.Amount);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A8F3 RID: 43251 RVA: 0x003DCD48 File Offset: 0x003DAF48
		public override bool HandleEvent(BeforeApplyDamageEvent E)
		{
			if ((E.Object == this.ParentObject.Equipped || E.Object == this.ParentObject) && E.Damage.IsHeatDamage())
			{
				E.Damage.Amount = (int)((float)E.Damage.Amount * this.Amount);
				if (E.Damage.Amount <= 0)
				{
					return false;
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A8F4 RID: 43252 RVA: 0x003DCDB9 File Offset: 0x003DAFB9
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x040045C9 RID: 17865
		public float Amount = 0.9f;
	}
}
