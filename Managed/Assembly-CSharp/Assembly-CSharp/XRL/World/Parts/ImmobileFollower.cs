using System;
using System.Collections.Generic;

namespace XRL.World.Parts
{
	// Token: 0x020011A9 RID: 4521
	[Serializable]
	public class ImmobileFollower : IPart
	{
		// Token: 0x0600B3B2 RID: 46002 RVA: 0x00418F21 File Offset: 0x00417121
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<JoinPartyLeaderPossibleEvent>.ID;
		}

		// Token: 0x0600B3B3 RID: 46003 RVA: 0x00418F38 File Offset: 0x00417138
		public override bool HandleEvent(JoinPartyLeaderPossibleEvent E)
		{
			if (!E.IsMobile && !E.Result)
			{
				Zone parentZone = E.TargetCell.ParentZone;
				Cell cell = null;
				if (this.JoinAdjacentHostile)
				{
					List<GameObject> objects = parentZone.GetObjects(new Predicate<GameObject>(this.ParentObject.IsHostileTowards));
					Cell cell2;
					if (objects == null)
					{
						cell2 = null;
					}
					else
					{
						GameObject randomElement = objects.GetRandomElement<GameObject>(null);
						cell2 = ((randomElement != null) ? randomElement.CurrentCell : null);
					}
					cell = cell2;
				}
				if (this.JoinRandomInZone && cell == null)
				{
					cell = parentZone.GetSpawnCell();
				}
				E.TargetCell = cell ?? E.TargetCell;
				E.Result = true;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0400487D RID: 18557
		public bool JoinRandomInZone = true;

		// Token: 0x0400487E RID: 18558
		public bool JoinAdjacentHostile;
	}
}
