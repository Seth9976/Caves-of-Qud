using System;
using System.Collections.Generic;
using UnityEngine;
using XRL.Rules;

namespace XRL.World.Parts
{
	// Token: 0x02000F0D RID: 3853
	[Serializable]
	public class TemperatureVenting : IPart
	{
		// Token: 0x060099A2 RID: 39330 RVA: 0x0037CB22 File Offset: 0x0037AD22
		public override bool WantTurnTick()
		{
			return true;
		}

		// Token: 0x060099A3 RID: 39331 RVA: 0x0037CB28 File Offset: 0x0037AD28
		public override void TurnTick(long TimeTick, int Amount)
		{
			int temperature = this.ParentObject.Physics.Temperature;
			if (temperature > this.TemperatureCeiling || temperature < this.TemperatureFloor)
			{
				this.CurrentWarmup += Amount;
				if (this.CurrentWarmup >= this.Warmup)
				{
					this.Trigger();
					this.CurrentWarmup = 0;
					return;
				}
			}
			else if (this.CurrentWarmup > 0)
			{
				this.CurrentWarmup = Math.Max(0, this.CurrentWarmup - Amount);
			}
		}

		// Token: 0x060099A4 RID: 39332 RVA: 0x0037CBA0 File Offset: 0x0037ADA0
		public void Trigger()
		{
			int temperature = this.ParentObject.Physics.Temperature;
			float num = (float)this.DifferentialPercent / 100f;
			int num2 = temperature - this.TemperatureTarget;
			this.ParentObject.TemperatureChange(-Mathf.RoundToInt((float)num2 * num), null, false, false, false, true, 0, null, null);
			base.PlayWorldSound("sfx_ability_gasMutation_activeRelease", 0.5f, 0f, false, null, 0f, 1f, 1f, int.MaxValue);
			base.DidX("release", (num2 > 0) ? "a cloud of steam to cool off" : "a cloud of cryogenic mist to heat up", "!", null, null, this.ParentObject, null, false, false, null, null, false, false, false, false, false, null);
			Cell currentCell = this.ParentObject.CurrentCell;
			List<GameObject> gasObjects = this.GasObjects;
			try
			{
				foreach (Cell cell in currentCell.IterateAdjacent(this.CloudRadius, true, true, true))
				{
					foreach (GameObject gameObject in cell.Objects)
					{
						if (gameObject.HasPart<Gas>())
						{
							gasObjects.Add(gameObject);
						}
					}
				}
				foreach (GameObject gameObject2 in gasObjects)
				{
					string text = this.ParentObject.GetDirectionToward(gameObject2, false);
					if (text == "." || text == "?")
					{
						text = Directions.GetRandomDirection();
					}
					Cell cellFromDirection = gameObject2.CurrentCell.GetCellFromDirection(text, true);
					if (cellFromDirection != null)
					{
						cellFromDirection.AddObject(gameObject2, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
					}
				}
			}
			finally
			{
				gasObjects.Clear();
			}
			foreach (Cell cell2 in currentCell.IterateAdjacent(this.CloudRadius, false, false, true))
			{
				cell2.AddObject((num2 > 0) ? "SteamGas80" : "CryoGas80", null, null, null, null);
			}
		}

		// Token: 0x04004005 RID: 16389
		public int TemperatureTarget = 25;

		// Token: 0x04004006 RID: 16390
		public int TemperatureCeiling = 2000;

		// Token: 0x04004007 RID: 16391
		public int TemperatureFloor = -100;

		// Token: 0x04004008 RID: 16392
		public int DifferentialPercent = 20;

		// Token: 0x04004009 RID: 16393
		public int CloudRadius = 1;

		// Token: 0x0400400A RID: 16394
		public int Warmup = 3;

		// Token: 0x0400400B RID: 16395
		public int CurrentWarmup;

		// Token: 0x0400400C RID: 16396
		[NonSerialized]
		private List<GameObject> GasObjects = new List<GameObject>();
	}
}
