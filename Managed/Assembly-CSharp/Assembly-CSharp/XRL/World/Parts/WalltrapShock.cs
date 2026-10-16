using System;
using System.Collections.Generic;
using XRL.Core;
using XRL.Rules;

namespace XRL.World.Parts
{
	// Token: 0x020012A5 RID: 4773
	[Serializable]
	public class WalltrapShock : IPart
	{
		// Token: 0x0600BC8E RID: 48270 RVA: 0x0044EEA4 File Offset: 0x0044D0A4
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x0600BC8F RID: 48271 RVA: 0x0044EEA7 File Offset: 0x0044D0A7
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("WalltrapTrigger");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600BC90 RID: 48272 RVA: 0x0044EEBC File Offset: 0x0044D0BC
		public void Zap(Cell C)
		{
			if (C != null)
			{
				using (List<GameObject>.Enumerator enumerator = C.GetObjectsWithPart("Physics", (GameObject obj) => obj.PhaseMatches(this.ParentObject)).GetEnumerator())
				{
					if (enumerator.MoveNext())
					{
						GameObject gameObject = enumerator.Current;
						this.ParentObject.Discharge(C, this.JetVoltage.RollCached(), 0, this.JetDamageRange, null, this.ParentObject, this.ParentObject, null, null, null, null, null, 0, false, false, false);
					}
				}
			}
		}

		// Token: 0x0600BC91 RID: 48273 RVA: 0x0044EF54 File Offset: 0x0044D154
		public void ZapBolt(string D)
		{
			Cell cell = this.ParentObject.CurrentCell;
			for (int i = 0; i < this.JetLength; i++)
			{
				cell = cell.GetCellFromDirection(D, true);
				if (cell == null)
				{
					break;
				}
				this.Zap(cell);
				if (cell.ParentZone.IsActive() && cell.IsVisible())
				{
					for (int j = 0; j < 3; j++)
					{
						string text = (50.in100() ? "&W" : "&Y");
						XRLCore.ParticleManager.Add(text + ((char)Stat.Random(191, 198)).ToString(), (float)cell.X, (float)cell.Y, 0f, 0f, 10 + 2 * i + (6 - 2 * j));
					}
				}
				if (cell.IsSolid(true))
				{
					break;
				}
			}
		}

		// Token: 0x0600BC92 RID: 48274 RVA: 0x0044F028 File Offset: 0x0044D228
		public override bool FireEvent(Event E)
		{
			if (E.ID == "WalltrapTrigger")
			{
				Cell currentCell = this.ParentObject.CurrentCell;
				if (currentCell != null && !base.IsBroken() && !base.IsRusted() && !base.IsEMPed())
				{
					foreach (string text in Directions.CardinalDirectionList)
					{
						Cell cellFromDirection = currentCell.GetCellFromDirection(text, true);
						if (cellFromDirection != null && !cellFromDirection.IsSolid(true))
						{
							this.ZapBolt(text);
						}
					}
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x04004B94 RID: 19348
		public int JetLength = 3;

		// Token: 0x04004B95 RID: 19349
		public string JetDamageRange = "2d6";

		// Token: 0x04004B96 RID: 19350
		public string JetVoltage = "1d3";
	}
}
