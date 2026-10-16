using System;
using XRL.World.Parts;

namespace XRL.World
{
	// Token: 0x02000804 RID: 2052
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class MissilePenetrateEvent : PooledEvent<MissilePenetrateEvent>
	{
		// Token: 0x0600520F RID: 21007 RVA: 0x0018B8DD File Offset: 0x00189ADD
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06005210 RID: 21008 RVA: 0x0018B8E8 File Offset: 0x00189AE8
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
			this.PathInvolvesPlayer = false;
			this.MessageAsFrom = null;
			this.Penetrations = 0;
			this.OutcomeMessageFragment = null;
		}

		// Token: 0x06005211 RID: 21009 RVA: 0x0018B96C File Offset: 0x00189B6C
		public static void Process(GameObject Launcher, GameObject Attacker, GameObject Defender, GameObject Owner, GameObject Projectile, Projectile ProjectilePart, GameObject AimedAt, GameObject ApparentTarget, MissilePath MissilePath, FireType Type, int AimLevel, int NaturalHitResult, bool PathInvolvesPlayer, GameObject MessageAsFrom, ref int Penetrations, ref string OutcomeMessageFragment)
		{
			bool flag = true;
			if (flag && GameObject.Validate(ref Launcher) && Launcher.HasRegisteredEvent("MissilePenetrate"))
			{
				Event @event = Event.New("MissilePenetrate", 0, 0, 0);
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
				@event.SetParameter("PathInvolvesPlayer", PathInvolvesPlayer);
				@event.SetParameter("MessageAsFrom", MessageAsFrom);
				@event.SetParameter("Penetrations", Penetrations);
				@event.SetParameter("OutcomeMessageFragment", OutcomeMessageFragment);
				flag = Launcher.FireEvent(@event);
				Penetrations = @event.GetIntParameter("Penetrations", 0);
				OutcomeMessageFragment = @event.GetStringParameter("OutcomeMessageFragment", null);
			}
			if (flag && GameObject.Validate(ref Launcher) && Launcher.WantEvent(PooledEvent<MissilePenetrateEvent>.ID, MinEvent.CascadeLevel))
			{
				MissilePenetrateEvent missilePenetrateEvent = PooledEvent<MissilePenetrateEvent>.FromPool();
				missilePenetrateEvent.Launcher = Launcher;
				missilePenetrateEvent.Attacker = Attacker;
				missilePenetrateEvent.Defender = Defender;
				missilePenetrateEvent.Owner = Owner;
				missilePenetrateEvent.Projectile = Projectile;
				missilePenetrateEvent.ProjectilePart = ProjectilePart;
				missilePenetrateEvent.AimedAt = AimedAt;
				missilePenetrateEvent.ApparentTarget = ApparentTarget;
				missilePenetrateEvent.MissilePath = MissilePath;
				missilePenetrateEvent.Type = Type;
				missilePenetrateEvent.AimLevel = AimLevel;
				missilePenetrateEvent.NaturalHitResult = NaturalHitResult;
				missilePenetrateEvent.PathInvolvesPlayer = PathInvolvesPlayer;
				missilePenetrateEvent.MessageAsFrom = MessageAsFrom;
				missilePenetrateEvent.Penetrations = Penetrations;
				missilePenetrateEvent.OutcomeMessageFragment = OutcomeMessageFragment;
				flag = Launcher.HandleEvent(missilePenetrateEvent);
				Penetrations = missilePenetrateEvent.Penetrations;
				OutcomeMessageFragment = missilePenetrateEvent.OutcomeMessageFragment;
			}
		}

		// Token: 0x04002932 RID: 10546
		public GameObject Launcher;

		// Token: 0x04002933 RID: 10547
		public GameObject Attacker;

		// Token: 0x04002934 RID: 10548
		public GameObject Defender;

		// Token: 0x04002935 RID: 10549
		public GameObject Owner;

		// Token: 0x04002936 RID: 10550
		public GameObject Projectile;

		// Token: 0x04002937 RID: 10551
		public Projectile ProjectilePart;

		// Token: 0x04002938 RID: 10552
		public GameObject AimedAt;

		// Token: 0x04002939 RID: 10553
		public GameObject ApparentTarget;

		// Token: 0x0400293A RID: 10554
		public MissilePath MissilePath;

		// Token: 0x0400293B RID: 10555
		public FireType Type;

		// Token: 0x0400293C RID: 10556
		public int AimLevel;

		// Token: 0x0400293D RID: 10557
		public int NaturalHitResult;

		// Token: 0x0400293E RID: 10558
		public bool PathInvolvesPlayer;

		// Token: 0x0400293F RID: 10559
		public GameObject MessageAsFrom;

		// Token: 0x04002940 RID: 10560
		public int Penetrations;

		// Token: 0x04002941 RID: 10561
		public string OutcomeMessageFragment;
	}
}
