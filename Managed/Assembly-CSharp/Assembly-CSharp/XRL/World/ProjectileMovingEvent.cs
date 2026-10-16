using System;
using System.Collections.Generic;
using ConsoleLib.Console;

namespace XRL.World
{
	// Token: 0x0200082E RID: 2094
	[GameEvent(Cascade = 17, Cache = MinEvent.Cache.Pool)]
	public class ProjectileMovingEvent : PooledEvent<ProjectileMovingEvent>
	{
		// Token: 0x0600533B RID: 21307 RVA: 0x0018EDC8 File Offset: 0x0018CFC8
		public override int GetCascadeLevel()
		{
			return ProjectileMovingEvent.CascadeLevel;
		}

		// Token: 0x0600533C RID: 21308 RVA: 0x0018EDCF File Offset: 0x0018CFCF
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x0600533D RID: 21309 RVA: 0x0018EDD8 File Offset: 0x0018CFD8
		public override void Reset()
		{
			base.Reset();
			this.Attacker = null;
			this.Launcher = null;
			this.Projectile = null;
			this.Defender = null;
			this.ApparentTarget = null;
			this.HitOverride = null;
			this.Cell = null;
			this.TargetCell = null;
			this.Path = null;
			this.PathIndex = -1;
			this.ScreenBuffer = null;
			this.Throw = false;
			this.ActivateShowUninvolved = false;
			this.RecheckPhase = false;
		}

		// Token: 0x040029E2 RID: 10722
		public new static readonly int CascadeLevel = 17;

		// Token: 0x040029E3 RID: 10723
		public GameObject Attacker;

		// Token: 0x040029E4 RID: 10724
		public GameObject Launcher;

		// Token: 0x040029E5 RID: 10725
		public GameObject Projectile;

		// Token: 0x040029E6 RID: 10726
		public GameObject Defender;

		// Token: 0x040029E7 RID: 10727
		public GameObject ApparentTarget;

		// Token: 0x040029E8 RID: 10728
		public GameObject HitOverride;

		// Token: 0x040029E9 RID: 10729
		public Cell Cell;

		// Token: 0x040029EA RID: 10730
		public Cell TargetCell;

		// Token: 0x040029EB RID: 10731
		public List<Point> Path;

		// Token: 0x040029EC RID: 10732
		public int PathIndex = -1;

		// Token: 0x040029ED RID: 10733
		public ScreenBuffer ScreenBuffer;

		// Token: 0x040029EE RID: 10734
		public bool Throw;

		// Token: 0x040029EF RID: 10735
		public bool ActivateShowUninvolved;

		// Token: 0x040029F0 RID: 10736
		public bool RecheckPhase;
	}
}
