using System;
using XRL.World.Parts;

namespace XRL.World
{
	// Token: 0x020006F7 RID: 1783
	[GameEvent(Cascade = 17, Cache = MinEvent.Cache.Pool)]
	public class DefenderMissileHitEvent : PooledEvent<DefenderMissileHitEvent>
	{
		// Token: 0x06004AA2 RID: 19106 RVA: 0x00174AF4 File Offset: 0x00172CF4
		public override int GetCascadeLevel()
		{
			return DefenderMissileHitEvent.CascadeLevel;
		}

		// Token: 0x06004AA3 RID: 19107 RVA: 0x00174AFB File Offset: 0x00172CFB
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004AA4 RID: 19108 RVA: 0x00174B04 File Offset: 0x00172D04
		public override void Reset()
		{
			base.Reset();
			this.Launcher = null;
			this.Attacker = null;
			this.Defender = null;
			this.Owner = null;
			this.Projectile = null;
			this.ProjectilePart = null;
			this.AimedAt = null;
			this.ApparentTarget = null;
			this.MissilePath = null;
			this.Type = FireType.Normal;
			this.AimLevel = 0;
			this.NaturalHitResult = 0;
			this.HitResult = 0;
			this.PathInvolvesPlayer = false;
			this.MessageAsFrom = null;
			this.Done = false;
			this.PenetrateCreatures = false;
			this.PenetrateWalls = false;
		}

		// Token: 0x06004AA5 RID: 19109 RVA: 0x00174B98 File Offset: 0x00172D98
		public static bool Check(GameObject Launcher, GameObject Attacker, GameObject Defender, GameObject Owner, GameObject Projectile, Projectile ProjectilePart, GameObject AimedAt, GameObject ApparentTarget, MissilePath MissilePath, FireType Type, int AimLevel, int NaturalHitResult, int HitResult, bool PathInvolvesPlayer, GameObject MessageAsFrom, ref bool Done, ref bool PenetrateCreatures, ref bool PenetrateWalls)
		{
			bool flag = true;
			if (flag && GameObject.Validate(ref Defender) && Defender.HasRegisteredEvent("DefenderMissileHit"))
			{
				Event @event = Event.New("DefenderMissileHit", 0, 0, 0);
				@event.SetParameter("Launcher", Launcher);
				@event.SetParameter("Attacker", Attacker);
				@event.SetParameter("Defender", Defender);
				@event.SetParameter("Owner", Owner);
				@event.SetParameter("Projectile", Projectile);
				@event.SetParameter("ProjectilePart", ProjectilePart);
				@event.SetParameter("AimedAt", AimedAt);
				@event.SetParameter("ApparentTarget", ApparentTarget);
				@event.SetParameter("MissilePath", MissilePath);
				@event.SetParameter("Type", Type);
				@event.SetParameter("AimLevel", AimLevel);
				@event.SetParameter("NaturalHitResult", NaturalHitResult);
				@event.SetParameter("HitResult", HitResult);
				@event.SetFlag("PathInvolvesPlayer", PathInvolvesPlayer);
				@event.SetParameter("MessageAsFrom", MessageAsFrom);
				@event.SetFlag("Done", Done);
				@event.SetFlag("PenetrateCreatures", PenetrateCreatures);
				@event.SetFlag("PenetrateWalls", PenetrateWalls);
				flag = Defender.FireEvent(@event);
				Done = @event.HasFlag("Done");
				PenetrateCreatures = @event.HasFlag("PenetrateCreatures");
				PenetrateWalls = @event.HasFlag("PenetrateWalls");
			}
			if (flag && GameObject.Validate(ref Defender) && Defender.WantEvent(PooledEvent<DefenderMissileHitEvent>.ID, DefenderMissileHitEvent.CascadeLevel))
			{
				DefenderMissileHitEvent defenderMissileHitEvent = PooledEvent<DefenderMissileHitEvent>.FromPool();
				defenderMissileHitEvent.Launcher = Launcher;
				defenderMissileHitEvent.Attacker = Attacker;
				defenderMissileHitEvent.Defender = Defender;
				defenderMissileHitEvent.Owner = Owner;
				defenderMissileHitEvent.Projectile = Projectile;
				defenderMissileHitEvent.ProjectilePart = ProjectilePart;
				defenderMissileHitEvent.AimedAt = AimedAt;
				defenderMissileHitEvent.ApparentTarget = ApparentTarget;
				defenderMissileHitEvent.MissilePath = MissilePath;
				defenderMissileHitEvent.Type = Type;
				defenderMissileHitEvent.AimLevel = AimLevel;
				defenderMissileHitEvent.NaturalHitResult = NaturalHitResult;
				defenderMissileHitEvent.HitResult = HitResult;
				defenderMissileHitEvent.PathInvolvesPlayer = PathInvolvesPlayer;
				defenderMissileHitEvent.MessageAsFrom = MessageAsFrom;
				defenderMissileHitEvent.Done = Done;
				defenderMissileHitEvent.PenetrateCreatures = PenetrateCreatures;
				defenderMissileHitEvent.PenetrateWalls = PenetrateWalls;
				flag = Defender.HandleEvent(defenderMissileHitEvent);
				Done = defenderMissileHitEvent.Done;
				PenetrateCreatures = defenderMissileHitEvent.PenetrateCreatures;
				PenetrateWalls = defenderMissileHitEvent.PenetrateWalls;
			}
			return flag;
		}

		// Token: 0x040023FF RID: 9215
		public new static readonly int CascadeLevel = 17;

		// Token: 0x04002400 RID: 9216
		public GameObject Launcher;

		// Token: 0x04002401 RID: 9217
		public GameObject Attacker;

		// Token: 0x04002402 RID: 9218
		public GameObject Defender;

		// Token: 0x04002403 RID: 9219
		public GameObject Owner;

		// Token: 0x04002404 RID: 9220
		public GameObject Projectile;

		// Token: 0x04002405 RID: 9221
		public Projectile ProjectilePart;

		// Token: 0x04002406 RID: 9222
		public GameObject AimedAt;

		// Token: 0x04002407 RID: 9223
		public GameObject ApparentTarget;

		// Token: 0x04002408 RID: 9224
		public MissilePath MissilePath;

		// Token: 0x04002409 RID: 9225
		public FireType Type;

		// Token: 0x0400240A RID: 9226
		public int AimLevel;

		// Token: 0x0400240B RID: 9227
		public int NaturalHitResult;

		// Token: 0x0400240C RID: 9228
		public int HitResult;

		// Token: 0x0400240D RID: 9229
		public bool PathInvolvesPlayer;

		// Token: 0x0400240E RID: 9230
		public GameObject MessageAsFrom;

		// Token: 0x0400240F RID: 9231
		public bool Done;

		// Token: 0x04002410 RID: 9232
		public bool PenetrateCreatures;

		// Token: 0x04002411 RID: 9233
		public bool PenetrateWalls;
	}
}
