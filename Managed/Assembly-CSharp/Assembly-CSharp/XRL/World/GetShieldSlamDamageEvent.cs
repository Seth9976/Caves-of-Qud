using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x02000788 RID: 1928
	[GameEvent(Cache = MinEvent.Cache.Pool, Cascade = 17)]
	public class GetShieldSlamDamageEvent : MinEvent
	{
		// Token: 0x06004EEF RID: 20207 RVA: 0x00182004 File Offset: 0x00180204
		public GetShieldSlamDamageEvent()
		{
			this.ID = GetShieldSlamDamageEvent.ID;
		}

		// Token: 0x06004EF0 RID: 20208 RVA: 0x00182017 File Offset: 0x00180217
		public override int GetCascadeLevel()
		{
			return GetShieldSlamDamageEvent.CascadeLevel;
		}

		// Token: 0x06004EF1 RID: 20209 RVA: 0x0018201E File Offset: 0x0018021E
		public static int CountPool()
		{
			if (GetShieldSlamDamageEvent.Pool != null)
			{
				return GetShieldSlamDamageEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x06004EF2 RID: 20210 RVA: 0x00182033 File Offset: 0x00180233
		public static void ResetPool()
		{
			while (GetShieldSlamDamageEvent.PoolCounter > 0)
			{
				GetShieldSlamDamageEvent.Pool[--GetShieldSlamDamageEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x06004EF3 RID: 20211 RVA: 0x0018205B File Offset: 0x0018025B
		public static void ResetTo(ref GetShieldSlamDamageEvent E)
		{
			MinEvent.ResetTo<GetShieldSlamDamageEvent>(E, GetShieldSlamDamageEvent.Pool, ref GetShieldSlamDamageEvent.PoolCounter);
			E = null;
		}

		// Token: 0x06004EF4 RID: 20212 RVA: 0x00182071 File Offset: 0x00180271
		public static GetShieldSlamDamageEvent FromPool()
		{
			return MinEvent.FromPool<GetShieldSlamDamageEvent>(ref GetShieldSlamDamageEvent.Pool, ref GetShieldSlamDamageEvent.PoolCounter, 8192);
		}

		// Token: 0x06004EF5 RID: 20213 RVA: 0x00182087 File Offset: 0x00180287
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004EF6 RID: 20214 RVA: 0x00182090 File Offset: 0x00180290
		public override void Reset()
		{
			base.Reset();
			this.Actor = null;
			this.Target = null;
			this.Shield = null;
			this.Attributes = null;
			this.ExtraDesc = null;
			this.Damage = 0;
			this.ShieldAV = 0;
			this.StrengthMod = 0;
			this.Prospective = false;
		}

		// Token: 0x06004EF7 RID: 20215 RVA: 0x001820E4 File Offset: 0x001802E4
		public static void GetFor(GameObject Actor, GameObject Target, GameObject Shield, ref string Attributes, ref string ExtraDesc, ref int Damage, int ShieldAV, int StrengthMod, bool Prospective = false)
		{
			if (true && GameObject.Validate(ref Actor) && Actor.WantEvent(GetShieldSlamDamageEvent.ID, GetShieldSlamDamageEvent.CascadeLevel))
			{
				GetShieldSlamDamageEvent getShieldSlamDamageEvent = GetShieldSlamDamageEvent.FromPool();
				getShieldSlamDamageEvent.Actor = Actor;
				getShieldSlamDamageEvent.Target = Target;
				getShieldSlamDamageEvent.Shield = Shield;
				getShieldSlamDamageEvent.Attributes = Attributes;
				getShieldSlamDamageEvent.ExtraDesc = ExtraDesc;
				getShieldSlamDamageEvent.Damage = Damage;
				getShieldSlamDamageEvent.ShieldAV = ShieldAV;
				getShieldSlamDamageEvent.StrengthMod = StrengthMod;
				getShieldSlamDamageEvent.Prospective = Prospective;
				Actor.HandleEvent(getShieldSlamDamageEvent);
				Attributes = getShieldSlamDamageEvent.Attributes;
				Damage = getShieldSlamDamageEvent.Damage;
				ExtraDesc = getShieldSlamDamageEvent.ExtraDesc;
			}
		}

		// Token: 0x040026AA RID: 9898
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(GetShieldSlamDamageEvent), null, new MinEvent.EventPoolCount(GetShieldSlamDamageEvent.CountPool), new MinEvent.EventPoolReset(GetShieldSlamDamageEvent.ResetPool));

		// Token: 0x040026AB RID: 9899
		public new static readonly int CascadeLevel = 17;

		// Token: 0x040026AC RID: 9900
		private static List<GetShieldSlamDamageEvent> Pool;

		// Token: 0x040026AD RID: 9901
		private static int PoolCounter;

		// Token: 0x040026AE RID: 9902
		public GameObject Actor;

		// Token: 0x040026AF RID: 9903
		public GameObject Target;

		// Token: 0x040026B0 RID: 9904
		public GameObject Shield;

		// Token: 0x040026B1 RID: 9905
		public string Attributes;

		// Token: 0x040026B2 RID: 9906
		public string ExtraDesc;

		// Token: 0x040026B3 RID: 9907
		public int Damage;

		// Token: 0x040026B4 RID: 9908
		public int ShieldAV;

		// Token: 0x040026B5 RID: 9909
		public int StrengthMod;

		// Token: 0x040026B6 RID: 9910
		public bool Prospective;
	}
}
