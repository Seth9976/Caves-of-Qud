using System;
using System.Collections.Generic;
using XRL.Rules;
using XRL.World.Capabilities;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x02000E7A RID: 3706
	[Serializable]
	public class HolographicIvory : IPart
	{
		// Token: 0x06009596 RID: 38294 RVA: 0x00361FE9 File Offset: 0x003601E9
		public override void AddedAfterCreation()
		{
			base.AddedAfterCreation();
			this.ParentObject.MakeNonflammable();
		}

		// Token: 0x06009597 RID: 38295 RVA: 0x00362000 File Offset: 0x00360200
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<CanBeDismemberedEvent>.ID || ID == PooledEvent<CanBeInvoluntarilyMovedEvent>.ID || ID == EnteredCellEvent.ID || ID == PooledEvent<GetElectricalConductivityEvent>.ID || ID == PooledEvent<GetMatterPhaseEvent>.ID || ID == PooledEvent<GetMaximumLiquidExposureEvent>.ID || ID == PooledEvent<GetScanTypeEvent>.ID || ID == ObjectCreatedEvent.ID || ID == ObjectEnteredCellEvent.ID || ID == PooledEvent<RespiresEvent>.ID;
		}

		// Token: 0x06009598 RID: 38296 RVA: 0x00362069 File Offset: 0x00360269
		public override bool HandleEvent(ObjectCreatedEvent E)
		{
			E.Object.MakeImperviousToHeat();
			return base.HandleEvent(E);
		}

		// Token: 0x06009599 RID: 38297 RVA: 0x0036207E File Offset: 0x0036027E
		public override bool HandleEvent(GetElectricalConductivityEvent E)
		{
			if (E.Pass == 1 && E.Object == this.ParentObject)
			{
				E.Value = 0;
				return false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600959A RID: 38298 RVA: 0x003620A7 File Offset: 0x003602A7
		public override bool HandleEvent(CanBeDismemberedEvent E)
		{
			return E.Object != this.ParentObject && base.HandleEvent(E);
		}

		// Token: 0x0600959B RID: 38299 RVA: 0x003620C0 File Offset: 0x003602C0
		public override bool HandleEvent(CanBeInvoluntarilyMovedEvent E)
		{
			return E.Object != this.ParentObject && base.HandleEvent(E);
		}

		// Token: 0x0600959C RID: 38300 RVA: 0x003620D9 File Offset: 0x003602D9
		public override bool HandleEvent(GetMatterPhaseEvent E)
		{
			E.MinMatterPhase(4);
			return false;
		}

		// Token: 0x0600959D RID: 38301 RVA: 0x003620E3 File Offset: 0x003602E3
		public override bool HandleEvent(GetMaximumLiquidExposureEvent E)
		{
			E.PercentageReduction = 100;
			return false;
		}

		// Token: 0x0600959E RID: 38302 RVA: 0x003620EE File Offset: 0x003602EE
		public override bool HandleEvent(GetScanTypeEvent E)
		{
			if (E.Object == this.ParentObject)
			{
				E.ScanType = Scanning.Scan.Tech;
				return false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600959F RID: 38303 RVA: 0x0036210E File Offset: 0x0036030E
		public override bool HandleEvent(RespiresEvent E)
		{
			return false;
		}

		// Token: 0x060095A0 RID: 38304 RVA: 0x00362114 File Offset: 0x00360314
		public override bool HandleEvent(EnteredCellEvent E)
		{
			if (this.ClusterSize != "1")
			{
				List<Cell> list = new List<Cell>(this.ParentObject.CurrentCell.GetLocalEmptyAdjacentCells());
				list.ShuffleInPlace<Cell>(null);
				int num = Stat.Roll(this.ClusterSize, null);
				int num2 = 0;
				while (num2 < num && num2 < list.Count)
				{
					if (15.in100())
					{
						list[num2].AddObject(this.ParentObject.Blueprint, null, null, null, null);
					}
					else
					{
						GameObject gameObject = GameObject.Create(this.ParentObject.Blueprint);
						Impaler part = gameObject.GetPart<Impaler>();
						if (part != null)
						{
							part.ClusterSize = "1";
						}
						list[num2].AddObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
					}
					num2++;
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x060095A1 RID: 38305 RVA: 0x003621EC File Offset: 0x003603EC
		public override bool HandleEvent(ObjectEnteredCellEvent E)
		{
			if (E.Object.IsCombatObject() && this.ParentObject.IsHostileTowards(E.Object) && E.Object.PhaseAndFlightMatches(this.ParentObject))
			{
				Hidden part = this.ParentObject.GetPart<Hidden>();
				if (!part.Found)
				{
					part.Found = true;
					base.DidX("strike", null, "!", null, null, null, E.Object, false, false, null, null, false, false, false, false, false, null);
					if (E.Object.TakeDamage(this.Damage.RollCached(), "from %t impalement.", "Stabbing Illusion", null, null, this.ParentObject, null, null, null, null, false, false, false, false, false, false, false, false, false, 5, null))
					{
						E.Object.Bloodsplatter();
						E.Object.ApplyEffect(new HolographicBleeding(this.BleedDamage, this.BleedSave, this.ParentObject, false, false, false, false), null);
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x04003DE6 RID: 15846
		public string ClusterSize = "1";

		// Token: 0x04003DE7 RID: 15847
		public string Damage = "1d10";

		// Token: 0x04003DE8 RID: 15848
		public string BleedDamage = "1d2";

		// Token: 0x04003DE9 RID: 15849
		public int BleedSave = 20;
	}
}
