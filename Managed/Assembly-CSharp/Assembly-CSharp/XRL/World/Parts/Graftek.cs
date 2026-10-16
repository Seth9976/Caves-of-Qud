using System;
using System.Collections.Generic;
using XRL.World.AI.GoalHandlers;

namespace XRL.World.Parts
{
	// Token: 0x02000E77 RID: 3703
	[Serializable]
	public class Graftek : IPart
	{
		// Token: 0x06009583 RID: 38275 RVA: 0x003615CF File Offset: 0x0035F7CF
		public override bool SameAs(IPart p)
		{
			return (p as Graftek).GraftCooldown == this.GraftCooldown && base.SameAs(p);
		}

		// Token: 0x06009584 RID: 38276 RVA: 0x003615ED File Offset: 0x0035F7ED
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x06009585 RID: 38277 RVA: 0x003615F0 File Offset: 0x0035F7F0
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("BeginTakeAction");
			base.Register(Object, Registrar);
		}

		// Token: 0x06009586 RID: 38278 RVA: 0x00361608 File Offset: 0x0035F808
		public override bool FireEvent(Event E)
		{
			if (E.ID == "BeginTakeAction")
			{
				this.GraftCooldown--;
				if (this.GraftCooldown <= 0)
				{
					for (int i = 0; i < this.ParentObject.Brain.Goals.Items.Count; i++)
					{
						if (this.ParentObject.Brain.Goals.Items[i].GetType().FullName.Contains("Graftek"))
						{
							return true;
						}
					}
					Cell currentCell = this.ParentObject.CurrentCell;
					List<GameObject> list = currentCell.ParentZone.FastSquareVisibility(currentCell.X, currentCell.Y, 12, "Combat", this.ParentObject, false, false, null);
					List<GameObject> list2 = Event.NewGameObjectList();
					foreach (GameObject gameObject in list)
					{
						if (!gameObject.IsPlayer() && !gameObject.HasPart<Graftek>() && !gameObject.HasPart<GraftekGraft>() && !gameObject.HasPart<Metal>() && !gameObject.Statistics.ContainsKey("Noclone") && (gameObject.IsOrganic || gameObject.HasPart<Brain>()))
						{
							list2.Add(gameObject);
						}
					}
					if (list2.Count == 0)
					{
						this.ParentObject.Brain.PushGoal(new WanderRandomly(5));
					}
					else
					{
						this.ParentObject.Brain.Goals.Clear();
						this.ParentObject.Brain.PushGoal(new GraftekGoal(list2.GetRandomElement<GameObject>(null)));
					}
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x04003DDD RID: 15837
		public int GraftCooldown;
	}
}
