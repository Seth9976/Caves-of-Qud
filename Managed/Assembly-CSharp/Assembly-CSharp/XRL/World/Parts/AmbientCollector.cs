using System;
using System.Collections.Generic;

namespace XRL.World.Parts
{
	// Token: 0x020011B3 RID: 4531
	[Serializable]
	public class AmbientCollector : IPoweredPart
	{
		// Token: 0x0600B3EF RID: 46063 RVA: 0x0041A6EA File Offset: 0x004188EA
		public AmbientCollector()
		{
			this.ChargeUse = 0;
			this.WorksOnSelf = true;
		}

		// Token: 0x0600B3F0 RID: 46064 RVA: 0x0041A700 File Offset: 0x00418900
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == QueryChargeProductionEvent.ID || ID == PrimePowerSystemsEvent.ID;
		}

		// Token: 0x0600B3F1 RID: 46065 RVA: 0x0041A720 File Offset: 0x00418920
		public override bool HandleEvent(QueryChargeProductionEvent E)
		{
			if (base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				Zone anyBasisZone = this.GetAnyBasisZone();
				if (anyBasisZone != null)
				{
					AmbientCollector.LiveArtifactCount = 0;
					anyBasisZone.ForeachObject(new Action<GameObject>(this.FindLiveArtifacts));
					int num = AmbientCollector.LiveArtifactCount * E.Multiple;
					if (num >= 500)
					{
						E.Amount += Math.Max(1, Math.Min(num / 1000, E.Multiple));
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B3F2 RID: 46066 RVA: 0x0041A7B8 File Offset: 0x004189B8
		public override bool HandleEvent(PrimePowerSystemsEvent E)
		{
			if (this.ParentObject.HasPropertyOrTag("Furniture"))
			{
				this.ProduceCharge(1);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B3F3 RID: 46067 RVA: 0x0041A7DC File Offset: 0x004189DC
		private void FindLiveArtifacts(GameObject obj)
		{
			if (obj != this.ParentObject)
			{
				if (obj.HasPart<Robot>() || obj.IsImplant)
				{
					AmbientCollector.LiveArtifactCount++;
				}
				else if (obj.HasPartDescendedFrom<IActivePart>())
				{
					bool flag = false;
					using (List<IActivePart>.Enumerator enumerator = obj.GetPartsDescendedFrom<IActivePart>().GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							if (enumerator.Current.ChargeUse > 0)
							{
								flag = true;
								break;
							}
						}
					}
					if (flag && obj.TestCharge(1, false, 0L, true, true))
					{
						AmbientCollector.LiveArtifactCount++;
					}
				}
			}
			obj.ForeachInventoryEquipmentAndCybernetics(new Action<GameObject>(this.FindLiveArtifacts));
		}

		// Token: 0x0600B3F4 RID: 46068 RVA: 0x0041A894 File Offset: 0x00418A94
		public override bool WantTurnTick()
		{
			return true;
		}

		// Token: 0x0600B3F5 RID: 46069 RVA: 0x0041A897 File Offset: 0x00418A97
		public override void TurnTick(long TimeTick, int Amount)
		{
			this.ProduceCharge(Amount);
		}

		// Token: 0x0600B3F6 RID: 46070 RVA: 0x0041A8A0 File Offset: 0x00418AA0
		public void ProduceCharge(int Turns = 1)
		{
			if (base.IsReady(false, false, false, false, false, false, false, false, false, false, Turns, null, false, 0L, null))
			{
				Zone anyBasisZone = this.GetAnyBasisZone();
				if (anyBasisZone != null)
				{
					AmbientCollector.LiveArtifactCount = 0;
					anyBasisZone.ForeachObject(new Action<GameObject>(this.FindLiveArtifacts));
					int num = 0;
					for (int i = 0; i < Turns; i++)
					{
						if (AmbientCollector.LiveArtifactCount.in1000())
						{
							num++;
						}
					}
					if (num > 0)
					{
						this.ParentObject.ChargeAvailable(1, 0L, num, false, null);
						base.ConsumeCharge(num, null, null);
					}
				}
			}
		}

		// Token: 0x0400488E RID: 18574
		[NonSerialized]
		private static int LiveArtifactCount;
	}
}
