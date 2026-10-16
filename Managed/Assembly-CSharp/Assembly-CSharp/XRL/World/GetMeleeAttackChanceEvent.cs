using System;
using XRL.Rules;
using XRL.World.Anatomy;

namespace XRL.World
{
	// Token: 0x02000764 RID: 1892
	[GameEvent(Cascade = 17, Cache = MinEvent.Cache.Pool)]
	public class GetMeleeAttackChanceEvent : PooledEvent<GetMeleeAttackChanceEvent>
	{
		// Token: 0x06004DF7 RID: 19959 RVA: 0x0017E828 File Offset: 0x0017CA28
		public override int GetCascadeLevel()
		{
			return GetMeleeAttackChanceEvent.CascadeLevel;
		}

		// Token: 0x06004DF8 RID: 19960 RVA: 0x0017E82F File Offset: 0x0017CA2F
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004DF9 RID: 19961 RVA: 0x0017E838 File Offset: 0x0017CA38
		public override void Reset()
		{
			base.Reset();
			this.Actor = null;
			this.Weapon = null;
			this.BodyPart = null;
			this.Properties = null;
			this.BaseChance = 0;
			this.Chance = 0;
			this.Attempts = 1;
			this.Multiplier = 1.0;
			this.Primary = false;
			this.Intrinsic = false;
			this.Inherited = false;
			this.Consecutive = false;
		}

		// Token: 0x06004DFA RID: 19962 RVA: 0x0017E8A8 File Offset: 0x0017CAA8
		public void HandleFor(GameObject Actor, GameObject Weapon, int BaseChance = -1, int Modifier = 0, double Multiplier = 1.0, string Properties = null, BodyPart BodyPart = null, BodyPart PrimaryBodyPart = null, bool Primary = false, bool Intrinsic = false, bool Consecutive = false)
		{
			this.Reset();
			this.Actor = Actor;
			this.Weapon = Weapon;
			BodyPart bodyPart;
			if ((bodyPart = BodyPart) == null)
			{
				bodyPart = (BodyPart = Weapon.EquippedOn());
			}
			this.BodyPart = bodyPart;
			this.BaseChance = BaseChance;
			this.Multiplier = Multiplier;
			this.Properties = Properties;
			this.Primary = Primary;
			this.Intrinsic = Intrinsic;
			this.Consecutive = Consecutive;
			if (BodyPart.DefaultPrimary && !BodyPart.Primary)
			{
				if (PrimaryBodyPart == null)
				{
					int num;
					Actor.Body.GetMainWeapon(out num, out PrimaryBodyPart, null, true, true, null);
				}
				if (PrimaryBodyPart != null && PrimaryBodyPart != BodyPart && PrimaryBodyPart.Primary)
				{
					this.BodyPart = PrimaryBodyPart;
					this.Inherited = true;
				}
			}
			if (BaseChance == -1)
			{
				this.BaseChance = (Primary ? 100 : Stats.GetSecondaryAttackChance(Actor));
			}
			this.Chance = this.BaseChance + Modifier;
			Actor.HandleEvent(this);
		}

		// Token: 0x06004DFB RID: 19963 RVA: 0x0017E988 File Offset: 0x0017CB88
		public static GetMeleeAttackChanceEvent HandleFrom(GameObject Actor, GameObject Weapon, int BaseChance = -1, int Modifier = 0, double Multiplier = 1.0, string Properties = null, BodyPart BodyPart = null, BodyPart PrimaryBodyPart = null, bool Primary = false, bool Intrinsic = false, bool Consecutive = false)
		{
			GetMeleeAttackChanceEvent getMeleeAttackChanceEvent = PooledEvent<GetMeleeAttackChanceEvent>.FromPool();
			getMeleeAttackChanceEvent.HandleFor(Actor, Weapon, BaseChance, Modifier, Multiplier, Properties, BodyPart, PrimaryBodyPart, Primary, Intrinsic, Consecutive);
			return getMeleeAttackChanceEvent;
		}

		// Token: 0x06004DFC RID: 19964 RVA: 0x0017E9B2 File Offset: 0x0017CBB2
		public void SetFinalizedChance(int Chance)
		{
			this.Chance = Chance;
			this.Multiplier = 1.0;
		}

		// Token: 0x06004DFD RID: 19965 RVA: 0x0017E9CA File Offset: 0x0017CBCA
		public int GetFinalizedChance()
		{
			return (int)Math.Round((double)this.Chance * this.Multiplier);
		}

		// Token: 0x06004DFE RID: 19966 RVA: 0x0017E9E0 File Offset: 0x0017CBE0
		public static int GetFor(GameObject Actor, GameObject Weapon, int BaseChance = -1, int Modifier = 0, double Multiplier = 1.0, string Properties = null, BodyPart BodyPart = null, BodyPart PrimaryBodyPart = null, bool Primary = false, bool Intrinsic = false, bool Consecutive = false)
		{
			GetMeleeAttackChanceEvent getMeleeAttackChanceEvent = PooledEvent<GetMeleeAttackChanceEvent>.FromPool();
			getMeleeAttackChanceEvent.HandleFor(Actor, Weapon, BaseChance, Modifier, Multiplier, Properties, BodyPart, PrimaryBodyPart, Primary, Intrinsic, Consecutive);
			int finalizedChance = getMeleeAttackChanceEvent.GetFinalizedChance();
			PooledEvent<GetMeleeAttackChanceEvent>.ResetTo(ref getMeleeAttackChanceEvent);
			return finalizedChance;
		}

		// Token: 0x040025E9 RID: 9705
		public new static readonly int CascadeLevel = 17;

		// Token: 0x040025EA RID: 9706
		public GameObject Actor;

		// Token: 0x040025EB RID: 9707
		public GameObject Weapon;

		// Token: 0x040025EC RID: 9708
		public BodyPart BodyPart;

		// Token: 0x040025ED RID: 9709
		public string Properties;

		// Token: 0x040025EE RID: 9710
		public int BaseChance;

		// Token: 0x040025EF RID: 9711
		public int Chance;

		// Token: 0x040025F0 RID: 9712
		public int Attempts = 1;

		// Token: 0x040025F1 RID: 9713
		public double Multiplier = 1.0;

		// Token: 0x040025F2 RID: 9714
		public bool Primary;

		// Token: 0x040025F3 RID: 9715
		public bool Intrinsic;

		// Token: 0x040025F4 RID: 9716
		public bool Inherited;

		// Token: 0x040025F5 RID: 9717
		public bool Consecutive;
	}
}
