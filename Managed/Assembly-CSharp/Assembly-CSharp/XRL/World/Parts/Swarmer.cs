using System;
using System.Collections.Generic;

namespace XRL.World.Parts
{
	// Token: 0x02000F0A RID: 3850
	[Serializable]
	public class Swarmer : IPart
	{
		// Token: 0x06009994 RID: 39316 RVA: 0x0037C734 File Offset: 0x0037A934
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x06009995 RID: 39317 RVA: 0x0037C737 File Offset: 0x0037A937
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == GetShortDescriptionEvent.ID || ID == GetAttackerMeleePenetrationEvent.ID || ID == PooledEvent<GetToHitModifierEvent>.ID;
		}

		// Token: 0x06009996 RID: 39318 RVA: 0x0037C760 File Offset: 0x0037A960
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			if (this.ExtraBonus > 0)
			{
				E.Postfix.AppendRules(string.Concat(new string[]
				{
					"Swarm Alpha: As long as this creature is adjacent to ",
					this.ParentObject.its,
					" target, ",
					this.ParentObject.it,
					this.ParentObject.GetVerb("grant", true, true, false, true, false),
					" ",
					this.ExtraBonus.ToString(),
					" to the swarm bonuses of each other swarmer who is adjacent to ",
					this.ParentObject.its,
					" target."
				}));
			}
			int swarmBonus = this.GetSwarmBonus();
			E.Postfix.AppendRules(string.Concat(new string[]
			{
				"Swarmer: This creature receives +1 to hit in melee and +1 to penetration rolls for each other hostile swarmer beyond the first who is in another square adjacent to ",
				this.ParentObject.its,
				" target. (currently ",
				swarmBonus.Signed(false),
				")"
			}));
			return base.HandleEvent(E);
		}

		// Token: 0x06009997 RID: 39319 RVA: 0x0037C85F File Offset: 0x0037AA5F
		public override bool HandleEvent(GetAttackerMeleePenetrationEvent E)
		{
			E.PenetrationBonus += this.GetSwarmBonus();
			return base.HandleEvent(E);
		}

		// Token: 0x06009998 RID: 39320 RVA: 0x0037C87C File Offset: 0x0037AA7C
		public override bool HandleEvent(GetToHitModifierEvent E)
		{
			if (E.Actor == this.ParentObject && E.Checking == "Actor" && E.Melee)
			{
				E.Modifier += this.GetSwarmBonus();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009999 RID: 39321 RVA: 0x0037C8CB File Offset: 0x0037AACB
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600999A RID: 39322 RVA: 0x0037C8D0 File Offset: 0x0037AAD0
		private void FindSwarmers(Cell C, GameObject Target, ref int swarmers, ref int extra)
		{
			bool flag = false;
			int i = 0;
			int count = C.Objects.Count;
			while (i < count)
			{
				GameObject gameObject = C.Objects[i];
				if (gameObject != this.ParentObject)
				{
					Swarmer part = gameObject.GetPart<Swarmer>();
					if (part != null && gameObject.IsHostileTowards(Target))
					{
						flag = true;
						if (part.ExtraBonus > extra)
						{
							extra = part.ExtraBonus;
						}
					}
				}
				i++;
			}
			if (flag)
			{
				swarmers++;
			}
		}

		// Token: 0x0600999B RID: 39323 RVA: 0x0037C944 File Offset: 0x0037AB44
		public int GetSwarmBonus()
		{
			Cell currentCell = this.ParentObject.CurrentCell;
			GameObject gameObject = this.ParentObject.Target ?? IComponent<GameObject>.ThePlayer;
			if (gameObject != null && this.ParentObject.DistanceTo(gameObject) <= 1)
			{
				Cell currentCell2 = gameObject.CurrentCell;
				if (currentCell2 != null)
				{
					int num = -1;
					int num2 = 0;
					if (currentCell2 != currentCell)
					{
						this.FindSwarmers(currentCell2, gameObject, ref num, ref num2);
					}
					List<Cell> adjacentCells = currentCell2.GetAdjacentCells(true);
					int i = 0;
					int count = adjacentCells.Count;
					while (i < count)
					{
						if (adjacentCells[i] != currentCell)
						{
							this.FindSwarmers(adjacentCells[i], gameObject, ref num, ref num2);
						}
						i++;
					}
					if (num <= 0 && num2 > 0)
					{
						num = num2;
					}
					else
					{
						num += num2;
					}
					if (num > 0)
					{
						return num;
					}
				}
			}
			return 0;
		}

		// Token: 0x04004002 RID: 16386
		public int ExtraBonus;
	}
}
