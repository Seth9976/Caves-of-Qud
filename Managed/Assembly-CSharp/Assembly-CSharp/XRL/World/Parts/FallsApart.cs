using System;

namespace XRL.World.Parts
{
	// Token: 0x02000EB5 RID: 3765
	[Serializable]
	public class FallsApart : IPart
	{
		// Token: 0x0600970F RID: 38671 RVA: 0x0036D145 File Offset: 0x0036B345
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AfterObjectCreatedEvent.ID;
		}

		// Token: 0x06009710 RID: 38672 RVA: 0x0036D15B File Offset: 0x0036B35B
		public override bool HandleEvent(AfterObjectCreatedEvent E)
		{
			this.TurnsLeft = this.Duration.RollCached();
			return base.HandleEvent(E);
		}

		// Token: 0x06009711 RID: 38673 RVA: 0x0036D175 File Offset: 0x0036B375
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("VillageInit");
			base.Register(Object, Registrar);
		}

		// Token: 0x06009712 RID: 38674 RVA: 0x0036D18A File Offset: 0x0036B38A
		public override bool FireEvent(Event E)
		{
			if (E.ID == "VillageInit")
			{
				this.bActive = false;
			}
			return base.FireEvent(E);
		}

		// Token: 0x06009713 RID: 38675 RVA: 0x0036D1AC File Offset: 0x0036B3AC
		public override bool WantTurnTick()
		{
			return true;
		}

		// Token: 0x06009714 RID: 38676 RVA: 0x0036D1B0 File Offset: 0x0036B3B0
		public override void TurnTick(long TimeTick, int Amount)
		{
			if (!this.bActive)
			{
				return;
			}
			int num = this.TurnsLeft - 1;
			this.TurnsLeft = num;
			if (num <= 0)
			{
				this.ParentObject.Die(null, "impermanence", "You fell apart.", this.ParentObject.It + " @@fell apart.", false, null, null, false, false, this.Message, null, null);
				if (GameObject.Validate(this.ParentObject))
				{
					this.TurnsLeft = this.Duration.RollCached();
				}
			}
		}

		// Token: 0x06009715 RID: 38677 RVA: 0x0036D231 File Offset: 0x0036B431
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x04003EC4 RID: 16068
		public string Duration = "2d4";

		// Token: 0x04003EC5 RID: 16069
		public string Message = "=subject.T= =verb:fall= apart.";

		// Token: 0x04003EC6 RID: 16070
		public int TurnsLeft = -1;

		// Token: 0x04003EC7 RID: 16071
		public bool bActive = true;
	}
}
