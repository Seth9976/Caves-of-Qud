using System;
using System.Collections.Generic;
using XRL.Collections;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x02000EAE RID: 3758
	[Serializable]
	public class EngulfingHandOff : IPart
	{
		// Token: 0x060096D9 RID: 38617 RVA: 0x0036BA21 File Offset: 0x00369C21
		public override bool SameAs(IPart Part)
		{
			return false;
		}

		// Token: 0x060096DA RID: 38618 RVA: 0x0036BA24 File Offset: 0x00369C24
		public override bool WantEvent(int ID, int Cascade)
		{
			return base.WantEvent(ID, Cascade) || ID == SingletonEvent<BeginTakeActionEvent>.ID;
		}

		// Token: 0x060096DB RID: 38619 RVA: 0x0036BA3C File Offset: 0x00369C3C
		public override bool HandleEvent(BeginTakeActionEvent E)
		{
			Engulfing engulfing;
			if (this.ParentObject.TryGetPart<Engulfing>(out engulfing))
			{
				if (engulfing.Engulfed == null)
				{
					return true;
				}
				if (!engulfing.CheckEngulfed())
				{
					return true;
				}
				if (!engulfing.Engulfed.CanBeInvoluntarilyMoved())
				{
					return true;
				}
				List<Engulfing> adjacentReceivers = this.GetAdjacentReceivers();
				if (adjacentReceivers.Count > 0)
				{
					this.AttemptHandOff(engulfing, adjacentReceivers.GetRandomElement<Engulfing>(null), engulfing.Engulfed);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x060096DC RID: 38620 RVA: 0x0036BAA8 File Offset: 0x00369CA8
		public List<Engulfing> GetAdjacentReceivers()
		{
			List<Engulfing> list = new List<Engulfing>();
			Cell currentCell = this.ParentObject.CurrentCell;
			if (currentCell != null)
			{
				currentCell.ForeachAdjacentCell(delegate(Cell Cell)
				{
					using (Container<GameObject>.Enumerator enumerator = Cell.Objects.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							Engulfing engulfing;
							if (enumerator.Current.TryGetPart<Engulfing>(out engulfing))
							{
								if (engulfing.Engulfed == null)
								{
									list.Add(engulfing);
									break;
								}
								break;
							}
						}
					}
				}, true);
			}
			return list;
		}

		// Token: 0x060096DD RID: 38621 RVA: 0x0036BAF0 File Offset: 0x00369CF0
		public bool HandOffSave(GameObject Engulfed)
		{
			return Engulfed.MakeSave(this.SaveStat, this.SaveTarget, this.ParentObject, this.SaveDifficultyStat, "Engulfment Move", false, false, false, false, null);
		}

		// Token: 0x060096DE RID: 38622 RVA: 0x0036BB28 File Offset: 0x00369D28
		public void AttemptHandOff(Engulfing Giver, Engulfing Receiver, GameObject Engulfed)
		{
			this.ParentObject.UseEnergy(1000, "HandOff", null, null, false);
			Engulfed engulfed;
			if (!this.HandOffSave(Engulfed) && Engulfed.TryGetEffect<Engulfed>(out engulfed))
			{
				Giver.Engulfed = null;
				engulfed.EngulfedBy = Receiver.ParentObject;
				Engulfed.ApplyEffect(new AnemoneEffect(this.BleedingDamageBonus, this.BleedingSavePenalty, 1), null);
				Engulfed.CurrentCell.RemoveObject(Engulfed, false, false, false, false, false, true, true, null, null, null, null, null, null, null);
				Receiver.ParentObject.CurrentCell.AddObject(Engulfed, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
				Receiver.Engulfed = Engulfed;
				Receiver.ParentObject.UseEnergy(1000, "HandOff", null, null, false);
				if (this.ParentObject.IsPlayer() || Engulfed.IsPlayer())
				{
					base.DidXToY("hand", Engulfed, "off", null, null, null, null, Engulfed, false, false, false, false, false, null, null, null, false, false, false, false, false, null);
				}
			}
		}

		// Token: 0x04003EA4 RID: 16036
		public string SaveStat = "Strength";

		// Token: 0x04003EA5 RID: 16037
		public string SaveDifficultyStat = "Strength";

		// Token: 0x04003EA6 RID: 16038
		public int SaveTarget = 30;

		// Token: 0x04003EA7 RID: 16039
		public string BleedingDamageBonus;

		// Token: 0x04003EA8 RID: 16040
		public int BleedingSavePenalty;
	}
}
