using System;
using XRL.World.Capabilities;

namespace XRL.World.Effects
{
	// Token: 0x02000D38 RID: 3384
	[Serializable]
	public class Flying : Effect
	{
		// Token: 0x06008589 RID: 34185 RVA: 0x0030304E File Offset: 0x0030124E
		public Flying()
		{
			this.DisplayName = "flying";
			this.Duration = 9999;
		}

		// Token: 0x0600858A RID: 34186 RVA: 0x00303073 File Offset: 0x00301273
		public Flying(int Level)
			: this()
		{
			this.Level = Level;
			this.Duration = 9999;
		}

		// Token: 0x0600858B RID: 34187 RVA: 0x0030308D File Offset: 0x0030128D
		public Flying(int Level, GameObject Source)
			: this()
		{
			this.Level = Level;
			this.Source = Source;
			this.Duration = 9999;
		}

		// Token: 0x0600858C RID: 34188 RVA: 0x003030AE File Offset: 0x003012AE
		public override bool UseStandardDurationCountdown()
		{
			return true;
		}

		// Token: 0x0600858D RID: 34189 RVA: 0x003030B1 File Offset: 0x003012B1
		public override int GetEffectType()
		{
			return 16777344;
		}

		// Token: 0x0600858E RID: 34190 RVA: 0x003030B8 File Offset: 0x003012B8
		public override bool SameAs(Effect e)
		{
			return false;
		}

		// Token: 0x0600858F RID: 34191 RVA: 0x003030BB File Offset: 0x003012BB
		public override string GetDescription()
		{
			if (base.Object.GetEffect<Flying>() != this)
			{
				return null;
			}
			return "{{B|flying}}";
		}

		// Token: 0x06008590 RID: 34192 RVA: 0x003030D2 File Offset: 0x003012D2
		public override bool SuppressInLookDisplay()
		{
			return true;
		}

		// Token: 0x06008591 RID: 34193 RVA: 0x003030D5 File Offset: 0x003012D5
		public override string GetDetails()
		{
			return "Can't be attacked in melee by non-flying creatures.\nIsn't affected by terrain.";
		}

		// Token: 0x06008592 RID: 34194 RVA: 0x003030DC File Offset: 0x003012DC
		public override bool Apply(GameObject Object)
		{
			base.FlushNavigationCaches();
			return base.Apply(Object);
		}

		// Token: 0x06008593 RID: 34195 RVA: 0x003030EB File Offset: 0x003012EB
		public override void Remove(GameObject Object)
		{
			base.FlushNavigationCaches();
			base.Remove(Object);
		}

		// Token: 0x06008594 RID: 34196 RVA: 0x003030FA File Offset: 0x003012FA
		public override bool Render(RenderEvent E)
		{
			if (base.Object.GetEffect<Flying>() != this)
			{
				return true;
			}
			if (this.Duration > 0)
			{
				E.RenderEffectIndicator("\u0018", "Tiles2/status_flying.bmp", "&y", "y", 5, 10);
			}
			return true;
		}

		// Token: 0x06008595 RID: 34197 RVA: 0x00303134 File Offset: 0x00301334
		public override bool HandleEvent(GetInventoryActionsEvent E)
		{
			if (E.Object == base.Object && base.Object.IsPlayerLed() && !base.Object.IsPlayer())
			{
				E.AddAction("Stop Flying", "direct to stop flying", "CompanionStopFlying", null, '_', false, 0, 0, false, true, false, false, true, base.Object, false);
			}
			return true;
		}

		// Token: 0x06008596 RID: 34198 RVA: 0x00303192 File Offset: 0x00301392
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetDisplayNameEvent>.ID || ID == PooledEvent<GetElectricalConductivityEvent>.ID || ID == PooledEvent<MovementModeChangedEvent>.ID || ID == GetInventoryActionsEvent.ID || ID == InventoryActionEvent.ID;
		}

		// Token: 0x06008597 RID: 34199 RVA: 0x003031C8 File Offset: 0x003013C8
		public override bool HandleEvent(InventoryActionEvent E)
		{
			if (E.Command == "CompanionStopFlying")
			{
				Flight.StopFlying(base.Object, base.Object, null, false, false);
			}
			return true;
		}

		// Token: 0x06008598 RID: 34200 RVA: 0x003031F2 File Offset: 0x003013F2
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (!E.Reference && base.Object.GetEffect<Flying>() == this)
			{
				E.AddTag("[{{B|flying}}]", 0);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008599 RID: 34201 RVA: 0x0030321D File Offset: 0x0030141D
		public override bool HandleEvent(GetElectricalConductivityEvent E)
		{
			if (E.Pass == 1 && E.Object == base.Object && E.Source != null && !E.Source.IsFlying)
			{
				E.Value = 0;
				return false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600859A RID: 34202 RVA: 0x0030325C File Offset: 0x0030145C
		public override bool HandleEvent(MovementModeChangedEvent E)
		{
			if (this.Duration > 0 && E.Object == base.Object && (E.To == "Engulfing" || E.To == "Engulfed"))
			{
				Flight.Land(E.Object, false);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x040039BC RID: 14780
		public int Level = 1;

		// Token: 0x040039BD RID: 14781
		public GameObject Source;
	}
}
