using System;

namespace XRL.World.Parts
{
	// Token: 0x02001047 RID: 4167
	[Serializable]
	public class HUD : IPoweredPart
	{
		// Token: 0x0600A5DD RID: 42461 RVA: 0x003CB0B1 File Offset: 0x003C92B1
		public HUD()
		{
			this.WorksOnWearer = true;
		}

		// Token: 0x0600A5DE RID: 42462 RVA: 0x003CB0CB File Offset: 0x003C92CB
		public override bool SameAs(IPart p)
		{
			return !((p as HUD).EventHandled != this.EventHandled) && base.SameAs(p);
		}

		// Token: 0x0600A5DF RID: 42463 RVA: 0x003CB0EE File Offset: 0x003C92EE
		public override bool AllowStaticRegistration()
		{
			return false;
		}

		// Token: 0x0600A5E0 RID: 42464 RVA: 0x003CB0F1 File Offset: 0x003C92F1
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("Equipped");
			Registrar.Register("Unequipped");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600A5E1 RID: 42465 RVA: 0x003CB114 File Offset: 0x003C9314
		public override bool FireEvent(Event E)
		{
			if (E.ID == "EndTurn")
			{
				base.ConsumeChargeIfOperational(false, false, false, false, false, false, false, false, false, 1, null, false, 0, false, null);
			}
			else if (E.ID == this.EventHandled)
			{
				if (base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
				{
					return false;
				}
			}
			else if (E.ID == "Equipped")
			{
				E.GetGameObjectParameter("EquippingObject").RegisterPartEvent(this, this.EventHandled);
			}
			else if (E.ID == "Unequipped")
			{
				E.GetGameObjectParameter("UnequippingObject").UnregisterPartEvent(this, this.EventHandled);
			}
			return base.FireEvent(E);
		}

		// Token: 0x0400447C RID: 17532
		public string EventHandled = "HandleSmartData";
	}
}
