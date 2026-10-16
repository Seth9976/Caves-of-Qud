using System;
using System.Collections.Generic;
using XRL.World.AI;

namespace XRL.World.Parts
{
	// Token: 0x02000E94 RID: 3732
	[Serializable]
	public class TrollKing : IPart
	{
		// Token: 0x06009633 RID: 38451 RVA: 0x003668CB File Offset: 0x00364ACB
		public override bool WantTurnTick()
		{
			return true;
		}

		// Token: 0x06009634 RID: 38452 RVA: 0x003668CE File Offset: 0x00364ACE
		public override void TurnTick(long TimeTick, int Amount)
		{
			this.CheckSpawn(Amount);
		}

		// Token: 0x06009635 RID: 38453 RVA: 0x003668D8 File Offset: 0x00364AD8
		public void CheckSpawn(int Turns = 1)
		{
			Zone currentZone = this.ParentObject.CurrentZone;
			if (currentZone == null || !currentZone.IsActive() || currentZone.IsWorldMap())
			{
				this.StopBudding(Turns);
				return;
			}
			if (this.ParentObject.Physics.Temperature < 20 || currentZone.GetObjectCount(TrollKing.TROLL_FOAL_BLUEPRINT) >= TrollKing.TROLL_FOAL_LIMIT)
			{
				if (this.Budding)
				{
					this.Budding = false;
					if (IComponent<GameObject>.Visible(this.ParentObject))
					{
						Body body = this.ParentObject.Body;
						if (body != null && body.HasPart("Back"))
						{
							IComponent<GameObject>.AddPlayerMessage("The protuberance on " + this.ParentObject.poss("back", true) + " shrinks.", null, true);
						}
						else
						{
							IComponent<GameObject>.AddPlayerMessage("The protuberance on " + this.ParentObject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + " shrinks.", null, true);
						}
					}
				}
				if (this.Counter < this.MaxCounter)
				{
					this.Counter++;
				}
				return;
			}
			if (!this.Budding)
			{
				this.MaxCounter = "1d5+11".RollCached();
				this.Counter = this.MaxCounter;
				this.Budding = true;
				if (IComponent<GameObject>.Visible(this.ParentObject))
				{
					Body body2 = this.ParentObject.Body;
					if (body2 != null && body2.HasPart("Back"))
					{
						IComponent<GameObject>.AddPlayerMessage(string.Concat(new string[]
						{
							"A grotesque protuberance swells from ",
							this.ParentObject.poss("back", true),
							" as ",
							this.ParentObject.does("begin", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, true, true, null, true, null, false),
							" to bud!"
						}), null, true);
					}
					else
					{
						IComponent<GameObject>.AddPlayerMessage(string.Concat(new string[]
						{
							"A grotesque protuberance swells from ",
							this.ParentObject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
							" as ",
							this.ParentObject.does("begin", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, true, true, null, true, null, false),
							" to bud!"
						}), null, true);
					}
				}
			}
			if (this.Counter <= 0)
			{
				this.Spawn();
				this.Budding = false;
				return;
			}
			this.Counter--;
		}

		// Token: 0x06009636 RID: 38454 RVA: 0x00366B84 File Offset: 0x00364D84
		public void StopBudding(int Turns = 1)
		{
			if (this.Budding)
			{
				this.Budding = false;
				if (base.Visible())
				{
					Body body = this.ParentObject.Body;
					if (body != null && body.HasPart("Back"))
					{
						IComponent<GameObject>.AddPlayerMessage("The protuberance on " + this.ParentObject.poss("back", true) + " shrinks.", null, true);
					}
					else
					{
						IComponent<GameObject>.AddPlayerMessage("The protuberance on " + this.ParentObject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + " shrinks.", null, true);
					}
				}
			}
			this.Counter = Math.Min(this.Counter + Turns, this.MaxCounter);
		}

		// Token: 0x06009637 RID: 38455 RVA: 0x00366C50 File Offset: 0x00364E50
		public void Spawn()
		{
			List<Cell> localAdjacentCells = this.ParentObject.CurrentCell.GetLocalAdjacentCells();
			for (int i = 0; i < localAdjacentCells.Count; i++)
			{
				Cell cell = localAdjacentCells[i];
				if (cell.IsEmpty())
				{
					GameObject gameObject = GameObject.Create(TrollKing.TROLL_FOAL_BLUEPRINT);
					gameObject.RemovePart<RandomLoot>();
					gameObject.GetStat("XPValue").BaseValue = 0;
					gameObject.IsTrifling = true;
					gameObject.MakeActive(false);
					cell.AddObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
					IComponent<GameObject>.XDidYToZ(gameObject, "detach", "from", this.ParentObject, null, "!", null, null, this.ParentObject, null, false, false, false, false, false, null, null, null, false, false, false, false, false, null);
					gameObject.SetAlliedLeader<AllyBirth>(this.ParentObject, 0, false);
					this.ParentObject.Bloodsplatter(false);
					return;
				}
			}
		}

		// Token: 0x04003E2F RID: 15919
		public static readonly string TROLL_FOAL_BLUEPRINT = "Troll Foal";

		// Token: 0x04003E30 RID: 15920
		public static readonly int TROLL_FOAL_LIMIT = 18;

		// Token: 0x04003E31 RID: 15921
		private int Counter;

		// Token: 0x04003E32 RID: 15922
		private int MaxCounter = 14;

		// Token: 0x04003E33 RID: 15923
		private bool Budding;
	}
}
