using System;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x0200126F RID: 4719
	[Serializable]
	public class GraveMoss : IPart
	{
		// Token: 0x0600BAF9 RID: 47865 RVA: 0x00444DC0 File Offset: 0x00442FC0
		public override bool WantEvent(int ID, int Cascade)
		{
			return base.WantEvent(ID, Cascade) || ID == EffectAppliedEvent.ID || (ID == SingletonEvent<EndTurnEvent>.ID && this.Triggered) || ID == ObjectEnteredCellEvent.ID;
		}

		// Token: 0x0600BAFA RID: 47866 RVA: 0x00444DEE File Offset: 0x00442FEE
		public override bool HandleEvent(EffectAppliedEvent E)
		{
			if (this.IsBlood(E.Effect))
			{
				this.Trigger();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600BAFB RID: 47867 RVA: 0x00444E0B File Offset: 0x0044300B
		public override bool HandleEvent(ObjectEnteredCellEvent E)
		{
			if (GameObject.Validate(E.Object) && E.Object.HasPropertyOrTag("Corpse"))
			{
				this.Trigger();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600BAFC RID: 47868 RVA: 0x00444E3C File Offset: 0x0044303C
		public override bool HandleEvent(EndTurnEvent E)
		{
			if (this.Triggered && this.Turns > 0)
			{
				this.Turns--;
				if (this.Turns <= 0)
				{
					Cell currentCell = this.ParentObject.CurrentCell;
					GameObject firstObjectWithPropertyOrTag = currentCell.GetFirstObjectWithPropertyOrTag("Corpse");
					if (firstObjectWithPropertyOrTag != null)
					{
						firstObjectWithPropertyOrTag.Destroy(null, false, false, null);
					}
					currentCell.AddObject("Gorged Growth", null, null, null, null).MakeActive(false);
					this.ParentObject.Destroy(null, false, false, null);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600BAFD RID: 47869 RVA: 0x00444EC4 File Offset: 0x004430C4
		public void Trigger()
		{
			if (!this.Triggered)
			{
				if (base.Visible())
				{
					IComponent<GameObject>.AddPlayerMessage(this.ParentObject.Does("start", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " to fizz hungrily.", null, true);
				}
				this.Triggered = true;
			}
		}

		// Token: 0x0600BAFE RID: 47870 RVA: 0x00444F2A File Offset: 0x0044312A
		public bool IsBlood(LiquidVolume Liquid)
		{
			return Liquid != null && Liquid.HasPrimaryOrSecondaryLiquid("blood");
		}

		// Token: 0x0600BAFF RID: 47871 RVA: 0x00444F3C File Offset: 0x0044313C
		public bool IsBlood(Effect FX)
		{
			LiquidCovered liquidCovered = FX as LiquidCovered;
			if (liquidCovered != null && this.IsBlood(liquidCovered.Liquid))
			{
				return true;
			}
			LiquidStained liquidStained = FX as LiquidStained;
			return liquidStained != null && this.IsBlood(liquidStained.Liquid);
		}

		// Token: 0x04004B12 RID: 19218
		public bool Triggered;

		// Token: 0x04004B13 RID: 19219
		public int Turns = 20;
	}
}
