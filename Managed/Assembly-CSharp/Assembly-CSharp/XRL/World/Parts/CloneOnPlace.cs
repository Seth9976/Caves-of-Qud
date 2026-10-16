using System;
using XRL.World.AI;

namespace XRL.World.Parts
{
	// Token: 0x02000E3F RID: 3647
	[Serializable]
	public class CloneOnPlace : IPart
	{
		// Token: 0x060093E8 RID: 37864 RVA: 0x00353CF0 File Offset: 0x00351EF0
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == EnteredCellEvent.ID;
		}

		// Token: 0x060093E9 RID: 37865 RVA: 0x00353D06 File Offset: 0x00351F06
		public override IPart DeepCopy(GameObject Parent)
		{
			CloneOnPlace cloneOnPlace = (CloneOnPlace)base.DeepCopy(Parent);
			cloneOnPlace.Amount = 0;
			return cloneOnPlace;
		}

		// Token: 0x060093EA RID: 37866 RVA: 0x00353D1C File Offset: 0x00351F1C
		public override bool HandleEvent(EnteredCellEvent E)
		{
			if (this.Force || this.ParentObject.CanBeReplicated(this.ParentObject, this.Context, false))
			{
				for (int i = 0; i < this.Amount; i++)
				{
					GameObject gameObject = this.ParentObject.DeepCopy(false, false, null);
					Cell closestPassableCellFor = E.Cell.getClosestPassableCellFor(gameObject);
					gameObject.RemovePart(typeof(CloneOnPlace));
					closestPassableCellFor.AddObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
					gameObject.MakeActive(false);
					GameObject partyLeader = this.ParentObject.PartyLeader;
					if (partyLeader != null)
					{
						gameObject.Brain.SetPartyLeader(partyLeader, 0, false, false);
						AllegianceSet allegianceSet = this.ParentObject.Brain.FindAllegiance(partyLeader.BaseID);
						Type type;
						if (allegianceSet == null)
						{
							type = null;
						}
						else
						{
							IAllyReason reason = allegianceSet.Reason;
							type = ((reason != null) ? reason.GetType() : null);
						}
						Type type2 = type;
						if (type2 != null)
						{
							IAllyReason allyReason = Activator.CreateInstance(type2) as IAllyReason;
							if (allyReason != null)
							{
								gameObject.Brain.TakeAllegiance(partyLeader, allyReason);
							}
						}
					}
					WasReplicatedEvent.Send(this.ParentObject, this.ParentObject, gameObject, this.Context, false);
					ReplicaCreatedEvent.Send(gameObject, this.ParentObject, this.ParentObject, this.Context, false);
				}
			}
			this.ParentObject.RemovePart(this);
			return base.HandleEvent(E);
		}

		// Token: 0x04003D2D RID: 15661
		public int Amount;

		// Token: 0x04003D2E RID: 15662
		public string Context;

		// Token: 0x04003D2F RID: 15663
		public bool Force;
	}
}
