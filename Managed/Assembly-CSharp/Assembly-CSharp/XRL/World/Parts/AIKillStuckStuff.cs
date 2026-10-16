using System;
using System.Collections.Generic;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x02000E21 RID: 3617
	[Serializable]
	public class AIKillStuckStuff : AIBehaviorPart
	{
		// Token: 0x06009335 RID: 37685 RVA: 0x003509C5 File Offset: 0x0034EBC5
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == SingletonEvent<BeginTakeActionEvent>.ID || ID == PooledEvent<TakeOnRoleEvent>.ID;
		}

		// Token: 0x06009336 RID: 37686 RVA: 0x003509E3 File Offset: 0x0034EBE3
		public override bool HandleEvent(TakeOnRoleEvent E)
		{
			this.ParentObject.RemovePart(this);
			return base.HandleEvent(E);
		}

		// Token: 0x06009337 RID: 37687 RVA: 0x003509F8 File Offset: 0x0034EBF8
		public override bool HandleEvent(BeginTakeActionEvent E)
		{
			Cell currentCell = this.ParentObject.CurrentCell;
			if (currentCell != null && this.ParentObject.Target == null && !this.ParentObject.IsPlayerControlled() && this.ParentObject.FireEvent("CanAIDoIndependentBehavior"))
			{
				List<GameObject> list = currentCell.ParentZone.FastFloodVisibility(currentCell.X, currentCell.Y, 15, typeof(Combat), this.ParentObject, null, null);
				if (list.Count > 1)
				{
					list.ShuffleInPlace<GameObject>(null);
				}
				int i = 0;
				int count = list.Count;
				while (i < count)
				{
					GameObject gameObject = list[i];
					if (gameObject.HasEffect<Stuck>() && !this.ParentObject.IsAlliedTowards(gameObject))
					{
						this.ParentObject.Target = gameObject;
						break;
					}
					i++;
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009338 RID: 37688 RVA: 0x00350AD3 File Offset: 0x0034ECD3
		public override bool AllowStaticRegistration()
		{
			return true;
		}
	}
}
