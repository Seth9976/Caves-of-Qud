using System;
using System.Collections.Generic;
using XRL.World.AI.GoalHandlers;

namespace XRL.World.Parts
{
	// Token: 0x02000E83 RID: 3715
	[Serializable]
	public class Mindrone : IPart
	{
		// Token: 0x060095CA RID: 38346 RVA: 0x00362D25 File Offset: 0x00360F25
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == SingletonEvent<BeginTakeActionEvent>.ID;
		}

		// Token: 0x060095CB RID: 38347 RVA: 0x00362D3C File Offset: 0x00360F3C
		public override bool HandleEvent(BeginTakeActionEvent E)
		{
			if (this.HealCooldown <= 0 && !this.ParentObject.HasGoal("MindroneGoal"))
			{
				List<GameObject> list = this.ParentObject.CurrentZone.FastSquareVisibility(this.ParentObject.CurrentCell.X, this.ParentObject.CurrentCell.Y, 12, "Combat", this.ParentObject, false, false, null);
				List<GameObject> list2 = Event.NewGameObjectList();
				foreach (GameObject gameObject in list)
				{
					if (!gameObject.IsPlayer() && gameObject.HasPart<Metal>() && gameObject.isDamaged())
					{
						list2.Add(gameObject);
					}
				}
				if (list2.Count == 0)
				{
					if (The.Player != null)
					{
						this.ParentObject.Brain.PushGoal(new Flee(The.Player, 1, false, true));
					}
				}
				else
				{
					this.ParentObject.Brain.Goals.Clear();
					this.ParentObject.Brain.PushGoal(new MindroneGoal(list2.GetRandomElement<GameObject>(null)));
					this.HealCooldown = 20;
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x04003DEE RID: 15854
		public int HealCooldown;
	}
}
