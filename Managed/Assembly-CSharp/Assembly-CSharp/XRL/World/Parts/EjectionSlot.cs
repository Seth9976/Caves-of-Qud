using System;
using System.Collections.Generic;

namespace XRL.World.Parts
{
	// Token: 0x020012AD RID: 4781
	[Serializable]
	public class EjectionSlot : IPart
	{
		// Token: 0x0600BCE5 RID: 48357 RVA: 0x004524B9 File Offset: 0x004506B9
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == ZoneBuiltEvent.ID || ID == ObjectEnteredCellEvent.ID;
		}

		// Token: 0x0600BCE6 RID: 48358 RVA: 0x004524D7 File Offset: 0x004506D7
		public override bool HandleEvent(ZoneBuiltEvent E)
		{
			this.LockSeats(this.ParentObject.CurrentCell, true);
			return base.HandleEvent(E);
		}

		// Token: 0x0600BCE7 RID: 48359 RVA: 0x004524F2 File Offset: 0x004506F2
		public override bool HandleEvent(ObjectEnteredCellEvent E)
		{
			this.LockSeats(E.Cell, !E.Cell.ParentZone.Built);
			return base.HandleEvent(E);
		}

		// Token: 0x0600BCE8 RID: 48360 RVA: 0x0045251C File Offset: 0x0045071C
		public void LockSeats(Cell Cell, bool Silent = false)
		{
			List<GameObject> objectsWithPartReadonly = Cell.GetObjectsWithPartReadonly("EjectionSeat");
			if (objectsWithPartReadonly.IsNullOrEmpty<GameObject>())
			{
				return;
			}
			using (List<GameObject>.Enumerator enumerator = objectsWithPartReadonly.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (!enumerator.Current.Physics.Takeable)
					{
						return;
					}
				}
			}
			List<GameObject> list = objectsWithPartReadonly;
			GameObject gameObject = list[list.Count - 1];
			gameObject.Physics.Takeable = false;
			if (!Silent)
			{
				gameObject.Physics.DidX("lock", "in place", null, null, null, null, null, false, false, null, null, false, false, false, false, false, null);
				gameObject.PlayWorldSound("Sounds/Robot/sfx_turret_deploy", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
			}
		}
	}
}
