using System;
using System.Collections.Generic;

namespace XRL.World.Parts
{
	// Token: 0x020010FF RID: 4351
	[Serializable]
	public class AIShootRound : AIBehaviorPart
	{
		// Token: 0x0600AC23 RID: 44067 RVA: 0x003F169E File Offset: 0x003EF89E
		public void SetTarget(Cell Cell)
		{
			this.X = Cell.X;
			this.Y = Cell.Y;
		}

		// Token: 0x0600AC24 RID: 44068 RVA: 0x003F16B8 File Offset: 0x003EF8B8
		public override bool WantTurnTick()
		{
			return true;
		}

		// Token: 0x0600AC25 RID: 44069 RVA: 0x003F16BC File Offset: 0x003EF8BC
		public override void TurnTick(long TimeTick, int Amount)
		{
			if (this.Cooldown > 0)
			{
				this.Cooldown = Math.Max(this.Cooldown - Amount, 0);
				return;
			}
			List<GameObject> missileWeapons = this.ParentObject.GetMissileWeapons(null, null);
			GameObject gameObject = ((missileWeapons != null) ? missileWeapons.GetRandomElement<GameObject>(null) : null);
			Zone anyBasisZone = this.GetAnyBasisZone();
			Cell cell = ((anyBasisZone != null) ? anyBasisZone.GetCell(this.X, this.Y) : null);
			if (gameObject != null && cell != null)
			{
				Event @event = Event.New("CommandFireMissile", 0, 0, 0);
				@event.SetParameter("Owner", this.ParentObject);
				@event.SetParameter("TargetCell", cell);
				@event.SetParameter("EnergyMultiplier", 0f);
				gameObject.FireEvent(@event);
			}
		}

		// Token: 0x04004742 RID: 18242
		public int X;

		// Token: 0x04004743 RID: 18243
		public int Y;

		// Token: 0x04004744 RID: 18244
		public int Cooldown;
	}
}
