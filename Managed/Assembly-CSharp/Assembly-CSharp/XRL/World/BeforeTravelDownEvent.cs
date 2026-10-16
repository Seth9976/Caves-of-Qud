using System;
using System.Diagnostics.CodeAnalysis;
using Genkit;

namespace XRL.World
{
	// Token: 0x020006A5 RID: 1701
	[GameEvent(Cascade = 17)]
	public class BeforeTravelDownEvent : PooledEvent<BeforeTravelDownEvent>
	{
		// Token: 0x06004876 RID: 18550 RVA: 0x0016C6E6 File Offset: 0x0016A8E6
		public override int GetCascadeLevel()
		{
			return BeforeTravelDownEvent.CascadeLevel;
		}

		// Token: 0x06004877 RID: 18551 RVA: 0x0016C6ED File Offset: 0x0016A8ED
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004878 RID: 18552 RVA: 0x0016C6F6 File Offset: 0x0016A8F6
		public override void Reset()
		{
			base.Reset();
			this.Actor = null;
			this.World = null;
			this.Cell = null;
			this.Terrain = null;
			this.Landing = default(Point3D);
		}

		// Token: 0x06004879 RID: 18553 RVA: 0x0016C728 File Offset: 0x0016A928
		public static bool Check(GameObject Actor, string World, Cell Cell, GameObject Terrain, Point3D? Landing)
		{
			bool flag;
			using (BeforeTravelDownEvent beforeTravelDownEvent = PooledEvent<BeforeTravelDownEvent>.FromPool())
			{
				beforeTravelDownEvent.Actor = Actor;
				beforeTravelDownEvent.World = World;
				beforeTravelDownEvent.Cell = Cell;
				beforeTravelDownEvent.Terrain = Terrain;
				beforeTravelDownEvent.Landing = Landing ?? new Point3D(1, 1, 10);
				if (!Actor.HandleEvent(beforeTravelDownEvent))
				{
					flag = false;
				}
				else if (Terrain != null && !Terrain.HandleEvent(beforeTravelDownEvent))
				{
					flag = false;
				}
				else
				{
					flag = The.Game.HandleEvent(beforeTravelDownEvent);
				}
			}
			return flag;
		}

		// Token: 0x040022B1 RID: 8881
		public new static readonly int CascadeLevel = 17;

		// Token: 0x040022B2 RID: 8882
		[NotNull]
		public GameObject Actor;

		// Token: 0x040022B3 RID: 8883
		public string World;

		// Token: 0x040022B4 RID: 8884
		[NotNull]
		public Cell Cell;

		// Token: 0x040022B5 RID: 8885
		public GameObject Terrain;

		// Token: 0x040022B6 RID: 8886
		public Point3D Landing;
	}
}
