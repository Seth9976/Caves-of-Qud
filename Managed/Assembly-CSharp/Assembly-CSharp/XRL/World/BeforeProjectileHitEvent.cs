using System;
using XRL.World.Parts;

namespace XRL.World
{
	// Token: 0x0200069B RID: 1691
	[GameEvent(Cascade = 17, Cache = MinEvent.Cache.Pool)]
	public class BeforeProjectileHitEvent : PooledEvent<BeforeProjectileHitEvent>
	{
		// Token: 0x06004835 RID: 18485 RVA: 0x0016B87A File Offset: 0x00169A7A
		public override int GetCascadeLevel()
		{
			return BeforeProjectileHitEvent.CascadeLevel;
		}

		// Token: 0x06004836 RID: 18486 RVA: 0x0016B881 File Offset: 0x00169A81
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004837 RID: 18487 RVA: 0x0016B88C File Offset: 0x00169A8C
		public override void Reset()
		{
			base.Reset();
			this.Projectile = null;
			this.Launcher = null;
			this.Attacker = null;
			this.Object = null;
			this.ApparentTarget = null;
			this.Cell = null;
			this.ViaTreatAsSolid = null;
			this.PenetrateCreatures = false;
			this.PenetrateWalls = false;
			this.Hit = false;
			this.LightBased = false;
			this.Prospective = false;
			this.Recheck = false;
			this.RecheckPhase = false;
		}

		// Token: 0x06004838 RID: 18488 RVA: 0x0016B904 File Offset: 0x00169B04
		public static bool Check(GameObject Projectile, GameObject Attacker, GameObject Object, out bool Recheck, out bool RecheckPhase, bool PenetrateCreatures = false, bool PenetrateWalls = false, GameObject Launcher = null, GameObject ApparentTarget = null, Cell Cell = null, TreatAsSolid ViaTreatAsSolid = null, bool LightBased = false, bool Prospective = false)
		{
			if (Cell == null)
			{
				Cell = ((Object != null) ? Object.CurrentCell : null);
			}
			Recheck = false;
			RecheckPhase = false;
			bool flag = true;
			bool flag2 = true;
			if (flag)
			{
				bool flag3 = GameObject.Validate(ref Projectile) && Projectile.HasRegisteredEvent("BeforeProjectileHit");
				bool flag4 = GameObject.Validate(ref Attacker) && Attacker.HasRegisteredEvent("BeforeProjectileHit");
				bool flag5 = GameObject.Validate(ref Object) && Object.HasRegisteredEvent("BeforeProjectileHit");
				if (flag3 || flag4 || flag5)
				{
					Event @event = Event.New("BeforeProjectileHit", 0, 0, 0);
					@event.SetParameter("Projectile", Projectile);
					@event.SetParameter("Launcher", Launcher);
					@event.SetParameter("Attacker", Attacker);
					@event.SetParameter("Object", Object);
					@event.SetParameter("ApparentTarget", ApparentTarget);
					@event.SetParameter("Cell", Cell);
					@event.SetParameter("ViaTreatAsSolid", ViaTreatAsSolid);
					@event.SetFlag("PenetrateCreatures", PenetrateCreatures);
					@event.SetFlag("PenetrateWalls", PenetrateWalls);
					@event.SetFlag("Hit", flag2);
					@event.SetFlag("LightBased", LightBased);
					@event.SetFlag("Prospective", Prospective);
					@event.SetFlag("Recheck", Recheck);
					@event.SetFlag("RecheckPhase", RecheckPhase);
					flag = (!flag3 || Projectile.FireEvent(@event)) && (!flag4 || Attacker.FireEvent(@event)) && (!flag5 || Object.FireEvent(@event));
					flag2 = @event.HasFlag("Hit");
					Recheck = @event.HasFlag("Recheck");
					RecheckPhase = @event.HasFlag("RecheckPhase");
				}
			}
			if (flag)
			{
				bool flag6 = GameObject.Validate(ref Projectile) && Projectile.WantEvent(PooledEvent<BeforeProjectileHitEvent>.ID, BeforeProjectileHitEvent.CascadeLevel);
				bool flag7 = GameObject.Validate(ref Attacker) && Attacker.WantEvent(PooledEvent<BeforeProjectileHitEvent>.ID, BeforeProjectileHitEvent.CascadeLevel);
				bool flag8 = GameObject.Validate(ref Object) && Object.WantEvent(PooledEvent<BeforeProjectileHitEvent>.ID, BeforeProjectileHitEvent.CascadeLevel);
				if (flag6 || flag7 || flag8)
				{
					BeforeProjectileHitEvent beforeProjectileHitEvent = PooledEvent<BeforeProjectileHitEvent>.FromPool();
					beforeProjectileHitEvent.Projectile = Projectile;
					beforeProjectileHitEvent.Launcher = Launcher;
					beforeProjectileHitEvent.Attacker = Attacker;
					beforeProjectileHitEvent.Object = Object;
					beforeProjectileHitEvent.ApparentTarget = ApparentTarget;
					beforeProjectileHitEvent.Cell = Cell;
					beforeProjectileHitEvent.ViaTreatAsSolid = ViaTreatAsSolid;
					beforeProjectileHitEvent.PenetrateCreatures = PenetrateCreatures;
					beforeProjectileHitEvent.PenetrateWalls = PenetrateWalls;
					beforeProjectileHitEvent.Hit = flag2;
					beforeProjectileHitEvent.LightBased = LightBased;
					beforeProjectileHitEvent.Prospective = Prospective;
					beforeProjectileHitEvent.Recheck = Recheck;
					beforeProjectileHitEvent.RecheckPhase = RecheckPhase;
					if ((!flag6 || Projectile.HandleEvent(beforeProjectileHitEvent)) && (!flag7 || Attacker.HandleEvent(beforeProjectileHitEvent)))
					{
						bool flag9 = !flag8 || Object.HandleEvent(beforeProjectileHitEvent);
					}
					flag2 = beforeProjectileHitEvent.Hit;
					Recheck = beforeProjectileHitEvent.Recheck;
					RecheckPhase = beforeProjectileHitEvent.RecheckPhase;
				}
			}
			return flag2;
		}

		// Token: 0x04002271 RID: 8817
		public new static readonly int CascadeLevel = 17;

		// Token: 0x04002272 RID: 8818
		public GameObject Projectile;

		// Token: 0x04002273 RID: 8819
		public GameObject Launcher;

		// Token: 0x04002274 RID: 8820
		public GameObject Attacker;

		// Token: 0x04002275 RID: 8821
		public GameObject Object;

		// Token: 0x04002276 RID: 8822
		public GameObject ApparentTarget;

		// Token: 0x04002277 RID: 8823
		public Cell Cell;

		// Token: 0x04002278 RID: 8824
		public TreatAsSolid ViaTreatAsSolid;

		// Token: 0x04002279 RID: 8825
		public bool PenetrateCreatures;

		// Token: 0x0400227A RID: 8826
		public bool PenetrateWalls;

		// Token: 0x0400227B RID: 8827
		public bool Hit;

		// Token: 0x0400227C RID: 8828
		public bool LightBased;

		// Token: 0x0400227D RID: 8829
		public bool Prospective;

		// Token: 0x0400227E RID: 8830
		public bool Recheck;

		// Token: 0x0400227F RID: 8831
		public bool RecheckPhase;
	}
}
