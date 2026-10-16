using System;
using System.Collections.Generic;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x02001246 RID: 4678
	[Serializable]
	public class Stasisfield : IPart
	{
		// Token: 0x0600BA00 RID: 47616 RVA: 0x0043F153 File Offset: 0x0043D353
		public override bool SameAs(IPart p)
		{
			return (p as Stasisfield).Creator == this.Creator && base.SameAs(p);
		}

		// Token: 0x0600BA01 RID: 47617 RVA: 0x0043F174 File Offset: 0x0043D374
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<BlocksRadarEvent>.ID || ID == SingletonEvent<EndTurnEvent>.ID || ID == EnteredCellEvent.ID || ID == ObjectEnteredCellEvent.ID || ID == PooledEvent<InterruptAutowalkEvent>.ID || ID == PooledEvent<GetMaximumLiquidExposureEvent>.ID || ID == OnDestroyObjectEvent.ID;
		}

		// Token: 0x0600BA02 RID: 47618 RVA: 0x0043F1C5 File Offset: 0x0043D3C5
		public override bool HandleEvent(EndTurnEvent E)
		{
			this.ProcessStasis();
			return base.HandleEvent(E);
		}

		// Token: 0x0600BA03 RID: 47619 RVA: 0x0043F1D4 File Offset: 0x0043D3D4
		public override bool HandleEvent(EnteredCellEvent E)
		{
			this.ProcessStasis();
			return base.HandleEvent(E);
		}

		// Token: 0x0600BA04 RID: 47620 RVA: 0x0043F1E3 File Offset: 0x0043D3E3
		public override bool HandleEvent(ObjectEnteredCellEvent E)
		{
			this.ProcessStasis();
			return base.HandleEvent(E);
		}

		// Token: 0x0600BA05 RID: 47621 RVA: 0x0043F1F2 File Offset: 0x0043D3F2
		public override bool HandleEvent(GetMaximumLiquidExposureEvent E)
		{
			E.PercentageReduction = 100;
			return false;
		}

		// Token: 0x0600BA06 RID: 47622 RVA: 0x0043F1FD File Offset: 0x0043D3FD
		public override bool HandleEvent(InterruptAutowalkEvent E)
		{
			if (E.Actor.HasTagOrProperty("ForcefieldNullifier"))
			{
				E.IndicateObject = this.ParentObject;
				return false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600BA07 RID: 47623 RVA: 0x0043F226 File Offset: 0x0043D426
		public override bool HandleEvent(OnDestroyObjectEvent E)
		{
			this.ShutdownStasis();
			return base.HandleEvent(E);
		}

		// Token: 0x0600BA08 RID: 47624 RVA: 0x0043F235 File Offset: 0x0043D435
		public override bool HandleEvent(BlocksRadarEvent E)
		{
			return false;
		}

		// Token: 0x0600BA09 RID: 47625 RVA: 0x0043F238 File Offset: 0x0043D438
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600BA0A RID: 47626 RVA: 0x0043F23C File Offset: 0x0043D43C
		public void ProcessStasis()
		{
			GameObject.Validate(ref this.Creator);
			Cell currentCell = this.ParentObject.CurrentCell;
			if (currentCell == null)
			{
				this.ParentObject.Obliterate(null, false, null);
				return;
			}
			List<GameObject> list = Event.NewGameObjectList(currentCell.Objects);
			int i = 0;
			int count = list.Count;
			while (i < count)
			{
				GameObject gameObject = list[i];
				if (gameObject != this.ParentObject && currentCell.Objects.Contains(gameObject) && Stasis.EligibleForStasis(gameObject) && !gameObject.IsInStasis() && gameObject.PhaseMatches(this.ParentObject))
				{
					gameObject.ForceApplyEffect(new Stasis(), null);
				}
				i++;
			}
		}

		// Token: 0x0600BA0B RID: 47627 RVA: 0x0043F2E4 File Offset: 0x0043D4E4
		public void ShutdownStasis()
		{
			Cell currentCell = this.ParentObject.CurrentCell;
			if (currentCell == null)
			{
				return;
			}
			int i = 0;
			int count = currentCell.Objects.Count;
			while (i < count)
			{
				Stasis effect = currentCell.Objects[i].GetEffect<Stasis>();
				if (effect != null)
				{
					effect.CheckMaintenance(this.ParentObject);
				}
				i++;
			}
		}

		// Token: 0x04004ABE RID: 19134
		public GameObject Creator;
	}
}
