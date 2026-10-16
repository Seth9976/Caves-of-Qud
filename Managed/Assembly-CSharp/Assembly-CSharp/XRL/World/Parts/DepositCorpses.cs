using System;
using System.Collections.Generic;
using XRL.World.AI.GoalHandlers;

namespace XRL.World.Parts
{
	// Token: 0x02000FA0 RID: 4000
	[Serializable]
	public class DepositCorpses : IActivePart
	{
		// Token: 0x06009EC5 RID: 40645 RVA: 0x003985B4 File Offset: 0x003967B4
		public DepositCorpses()
		{
			this.WorksOnSelf = true;
		}

		// Token: 0x06009EC6 RID: 40646 RVA: 0x003985E0 File Offset: 0x003967E0
		public override bool SameAs(IPart Part)
		{
			DepositCorpses depositCorpses = Part as DepositCorpses;
			return !(depositCorpses.Tag != this.Tag) && depositCorpses.MaxNavigationWeight == this.MaxNavigationWeight && depositCorpses.OwnersOnlyIfOwned == this.OwnersOnlyIfOwned && base.SameAs(Part);
		}

		// Token: 0x06009EC7 RID: 40647 RVA: 0x00398630 File Offset: 0x00396830
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<IdleQueryEvent>.ID;
		}

		// Token: 0x06009EC8 RID: 40648 RVA: 0x00398646 File Offset: 0x00396846
		public override bool HandleEvent(IdleQueryEvent E)
		{
			return !this.TryDepositCorpses(E.Actor) && base.HandleEvent(E);
		}

		// Token: 0x06009EC9 RID: 40649 RVA: 0x0039865F File Offset: 0x0039685F
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x06009ECA RID: 40650 RVA: 0x00398664 File Offset: 0x00396864
		public bool TryDepositCorpses(GameObject Actor)
		{
			Zone currentZone = this.ParentObject.CurrentZone;
			if (currentZone == null)
			{
				return false;
			}
			if (Actor.Brain == null)
			{
				return false;
			}
			if (Actor.HasTagOrProperty("NoHauling"))
			{
				return false;
			}
			if (this.OwnersOnlyIfOwned)
			{
				string owner = this.ParentObject.Owner;
				if (!owner.IsNullOrEmpty() && !Actor.IsMemberOfFaction(owner))
				{
					return false;
				}
			}
			List<GameObject> list = Event.NewGameObjectList();
			currentZone.FindObjectsWithTagOrProperty(list, "Corpse");
			if (list.Count <= 0)
			{
				return false;
			}
			GameObject randomElement = list.GetRandomElement<GameObject>(null);
			if (randomElement.HasIntProperty("DepositeCorpsesReserve"))
			{
				randomElement.ModIntProperty("DepositeCorpsesReserve", -1, false);
				if (randomElement.HasIntProperty("DepositeCorpsesReserve"))
				{
					return false;
				}
			}
			if (Actor.WouldBeOverburdened(randomElement))
			{
				return false;
			}
			Cell currentCell = randomElement.CurrentCell;
			if (currentCell == null || currentCell.GetNavigationWeightFor(Actor, false, false, false, false, false) >= this.MaxNavigationWeight)
			{
				return false;
			}
			if (!base.IsReady(true, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				return false;
			}
			randomElement.SetIntProperty("DepositeCorpsesReserve", 50, false);
			Actor.Brain.PushGoal(new DisposeOfCorpse(randomElement, this.ParentObject));
			return true;
		}

		// Token: 0x040041B4 RID: 16820
		private const string RESERVE_PROPERTY = "DepositeCorpsesReserve";

		// Token: 0x040041B5 RID: 16821
		public string Tag = "Corpse";

		// Token: 0x040041B6 RID: 16822
		public int MaxNavigationWeight = 30;

		// Token: 0x040041B7 RID: 16823
		public bool OwnersOnlyIfOwned = true;
	}
}
