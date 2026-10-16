using System;
using System.Collections.Generic;
using XRL.World.Parts;

namespace XRL.World
{
	// Token: 0x02000752 RID: 1874
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class GetHostileWalkRadiusEvent : IActOnItemEvent
	{
		// Token: 0x06004D6C RID: 19820 RVA: 0x0017CF2B File Offset: 0x0017B12B
		public GetHostileWalkRadiusEvent()
		{
			this.ID = GetHostileWalkRadiusEvent.ID;
		}

		// Token: 0x06004D6D RID: 19821 RVA: 0x0017CF3E File Offset: 0x0017B13E
		public static int CountPool()
		{
			if (GetHostileWalkRadiusEvent.Pool != null)
			{
				return GetHostileWalkRadiusEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x06004D6E RID: 19822 RVA: 0x0017CF53 File Offset: 0x0017B153
		public static void ResetPool()
		{
			while (GetHostileWalkRadiusEvent.PoolCounter > 0)
			{
				GetHostileWalkRadiusEvent.Pool[--GetHostileWalkRadiusEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x06004D6F RID: 19823 RVA: 0x0017CF7B File Offset: 0x0017B17B
		public static void ResetTo(ref GetHostileWalkRadiusEvent E)
		{
			MinEvent.ResetTo<GetHostileWalkRadiusEvent>(E, GetHostileWalkRadiusEvent.Pool, ref GetHostileWalkRadiusEvent.PoolCounter);
			E = null;
		}

		// Token: 0x06004D70 RID: 19824 RVA: 0x0017CF91 File Offset: 0x0017B191
		public static GetHostileWalkRadiusEvent FromPool()
		{
			return MinEvent.FromPool<GetHostileWalkRadiusEvent>(ref GetHostileWalkRadiusEvent.Pool, ref GetHostileWalkRadiusEvent.PoolCounter, 8192);
		}

		// Token: 0x06004D71 RID: 19825 RVA: 0x0017CFA7 File Offset: 0x0017B1A7
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x06004D72 RID: 19826 RVA: 0x0017CFBB File Offset: 0x0017B1BB
		public override void Reset()
		{
			base.Reset();
			this.Radius = 0;
		}

		// Token: 0x06004D73 RID: 19827 RVA: 0x0017CFCA File Offset: 0x0017B1CA
		public static GetHostileWalkRadiusEvent FromPool(GameObject Actor, GameObject Item, int Radius)
		{
			GetHostileWalkRadiusEvent getHostileWalkRadiusEvent = GetHostileWalkRadiusEvent.FromPool();
			getHostileWalkRadiusEvent.Actor = Actor;
			getHostileWalkRadiusEvent.Item = Item;
			getHostileWalkRadiusEvent.Radius = Radius;
			return getHostileWalkRadiusEvent;
		}

		// Token: 0x06004D74 RID: 19828 RVA: 0x0017CFE6 File Offset: 0x0017B1E6
		public void MaxRadius(int Radius)
		{
			if (this.Radius < Radius)
			{
				this.Radius = Radius;
			}
		}

		// Token: 0x06004D75 RID: 19829 RVA: 0x0017CFF8 File Offset: 0x0017B1F8
		public static int GetFor(GameObject Actor, GameObject Item)
		{
			int num = Brain.DEFAULT_HOSTILE_WALK_RADIUS;
			if (Item.Brain != null)
			{
				bool flag;
				bool flag2;
				bool flag3;
				Item.Brain.CheckMobility(out flag, out flag2, out flag3);
				if ((flag || flag2 || flag3) && !Item.HasReadyMissileWeapon())
				{
					if (flag)
					{
						if (Item.IsCombat)
						{
							num = 2;
						}
						else
						{
							num = 1;
						}
					}
					else if (flag2 && flag3)
					{
						num = 1;
						if (Actor != null)
						{
							List<Tuple<Cell, char>> lineTo = Item.GetLineTo(Actor, true, false);
							if (lineTo != null)
							{
								int num2 = 1;
								int num3 = Math.Min(lineTo.Count, Item.Brain.HostileWalkRadius);
								while (num2 < num3 && (lineTo[num2].Item1.HasAquaticSupportFor(Item) || lineTo[num2].Item1.HasWalkableWallFor(Item)))
								{
									num++;
									num2++;
								}
							}
							if (num < Item.Brain.HostileWalkRadius)
							{
								num++;
							}
						}
					}
					else if (flag2)
					{
						num = 1;
						if (Actor != null)
						{
							List<Tuple<Cell, char>> lineTo2 = Item.GetLineTo(Actor, true, false);
							if (lineTo2 != null)
							{
								int num4 = 1;
								int num5 = Math.Min(lineTo2.Count, Item.Brain.HostileWalkRadius);
								while (num4 < num5 && lineTo2[num4].Item1.HasAquaticSupportFor(Item))
								{
									num++;
									num4++;
								}
							}
							if (num < Item.Brain.HostileWalkRadius)
							{
								num++;
							}
						}
					}
					else if (flag3)
					{
						num = 1;
						if (Actor != null)
						{
							List<Tuple<Cell, char>> lineTo3 = Item.GetLineTo(Actor, true, false);
							if (lineTo3 != null)
							{
								int num6 = 1;
								int num7 = Math.Min(lineTo3.Count, Item.Brain.HostileWalkRadius);
								while (num6 < num7 && lineTo3[num6].Item1.HasWalkableWallFor(Item))
								{
									num++;
									num6++;
								}
							}
							if (num < Item.Brain.HostileWalkRadius)
							{
								num++;
							}
						}
					}
					else
					{
						num = Item.Brain.HostileWalkRadius;
					}
				}
			}
			if (Item.WantEvent(GetHostileWalkRadiusEvent.ID, MinEvent.CascadeLevel))
			{
				GetHostileWalkRadiusEvent getHostileWalkRadiusEvent = GetHostileWalkRadiusEvent.FromPool(Actor, Item, num);
				Item.HandleEvent(getHostileWalkRadiusEvent);
				num = getHostileWalkRadiusEvent.Radius;
			}
			return num;
		}

		// Token: 0x04002597 RID: 9623
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(GetHostileWalkRadiusEvent), null, new MinEvent.EventPoolCount(GetHostileWalkRadiusEvent.CountPool), new MinEvent.EventPoolReset(GetHostileWalkRadiusEvent.ResetPool));

		// Token: 0x04002598 RID: 9624
		private static List<GetHostileWalkRadiusEvent> Pool;

		// Token: 0x04002599 RID: 9625
		private static int PoolCounter;

		// Token: 0x0400259A RID: 9626
		public int Radius;
	}
}
