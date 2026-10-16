using System;

namespace XRL.World.Effects
{
	// Token: 0x02000D3C RID: 3388
	[Serializable]
	public class Frozen : Effect
	{
		// Token: 0x060085B6 RID: 34230 RVA: 0x00303835 File Offset: 0x00301A35
		public Frozen()
		{
			this.DisplayName = "{{freezing|frozen}}";
		}

		// Token: 0x060085B7 RID: 34231 RVA: 0x00303848 File Offset: 0x00301A48
		public Frozen(int Duration)
			: this()
		{
			this.Duration = Duration;
		}

		// Token: 0x060085B8 RID: 34232 RVA: 0x00303857 File Offset: 0x00301A57
		public override string GetDescription()
		{
			if (base.Object == null || !base.Object.IsPlayer())
			{
				return base.GetDescription();
			}
			return null;
		}

		// Token: 0x060085B9 RID: 34233 RVA: 0x00303876 File Offset: 0x00301A76
		public override int GetEffectType()
		{
			return 33555456;
		}

		// Token: 0x060085BA RID: 34234 RVA: 0x0030387D File Offset: 0x00301A7D
		public override string GetDetails()
		{
			return "Can't take physical actions.";
		}

		// Token: 0x060085BB RID: 34235 RVA: 0x00303884 File Offset: 0x00301A84
		public override bool CanApplyToStack()
		{
			return true;
		}

		// Token: 0x060085BC RID: 34236 RVA: 0x00303887 File Offset: 0x00301A87
		public override bool HandleEvent(EndTurnEvent E)
		{
			if (!base.Object.IsFrozen())
			{
				base.Object.RemoveEffect(this, true);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x060085BD RID: 34237 RVA: 0x003038AB File Offset: 0x00301AAB
		public static bool CanApply(GameObject Object)
		{
			return FrozeEvent.Check(Object, Object.Physics.InflamedBy) && CanApplyEffectEvent.Check<Frozen>(Object, 0) && !Object.HasEffect<Frozen>();
		}

		// Token: 0x060085BE RID: 34238 RVA: 0x003038D8 File Offset: 0x00301AD8
		public static bool ApplyTo(GameObject Object)
		{
			return Frozen.CanApply(Object) && Object.ApplyEffect(new Frozen(1), null);
		}

		// Token: 0x060085BF RID: 34239 RVA: 0x003038F4 File Offset: 0x00301AF4
		public override bool Apply(GameObject Object)
		{
			base.PlayWorldSound("sfx_statusEffect_frozen", 0.5f, 0f, false, null, 0f, 1f, 1f, int.MaxValue);
			Object.MovementModeChanged("Frozen", true);
			if (Object != null)
			{
				Cell currentCell = Object.CurrentCell;
				if (currentCell != null)
				{
					currentCell.FlushNavigationCache();
				}
			}
			return true;
		}

		// Token: 0x060085C0 RID: 34240 RVA: 0x00303950 File Offset: 0x00301B50
		public override void Remove(GameObject Object)
		{
			Object.PlayWorldSound("sfx_statusEffect_thawed", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
			ThawedEvent.Send(Object, Object.Physics.InflamedBy);
			if (Object != null)
			{
				Cell currentCell = Object.CurrentCell;
				if (currentCell == null)
				{
					return;
				}
				currentCell.FlushNavigationCache();
			}
		}

		// Token: 0x060085C1 RID: 34241 RVA: 0x003039AB File Offset: 0x00301BAB
		public override bool Render(RenderEvent E)
		{
			E.ApplyColors("&c", 200);
			E.ApplyBackgroundColor("^C", 200);
			return true;
		}
	}
}
