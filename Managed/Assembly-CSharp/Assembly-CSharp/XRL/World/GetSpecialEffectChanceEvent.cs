using System;

namespace XRL.World
{
	// Token: 0x02000790 RID: 1936
	[GameEvent(Cascade = 17, Cache = MinEvent.Cache.Pool)]
	public class GetSpecialEffectChanceEvent : PooledEvent<GetSpecialEffectChanceEvent>
	{
		// Token: 0x06004F2C RID: 20268 RVA: 0x00182A62 File Offset: 0x00180C62
		public override int GetCascadeLevel()
		{
			return GetSpecialEffectChanceEvent.CascadeLevel;
		}

		// Token: 0x06004F2D RID: 20269 RVA: 0x00182A69 File Offset: 0x00180C69
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004F2E RID: 20270 RVA: 0x00182A72 File Offset: 0x00180C72
		public override void Reset()
		{
			base.Reset();
			this.Actor = null;
			this.Object = null;
			this.Subject = null;
			this.Projectile = null;
			this.Type = null;
			this.BaseChance = 0;
			this.Chance = 0;
		}

		// Token: 0x06004F2F RID: 20271 RVA: 0x00182AAC File Offset: 0x00180CAC
		public static int GetFor(GameObject Actor, GameObject Object, string Type = null, int Chance = 0, GameObject Subject = null, GameObject Projectile = null, bool ConstrainToPercentage = true, bool ConstrainToPermillage = false)
		{
			int num = Chance;
			bool flag = true;
			if (flag)
			{
				bool flag2 = GameObject.Validate(ref Actor) && Actor.HasRegisteredEvent("GetSpecialEffectChance");
				bool flag3 = GameObject.Validate(ref Object) && Object.HasRegisteredEvent("GetSpecialEffectChance");
				bool flag4 = GameObject.Validate(ref Subject) && Subject.HasRegisteredEvent("GetSpecialEffectChance");
				bool flag5 = GameObject.Validate(ref Projectile) && Projectile.HasRegisteredEvent("GetSpecialEffectChance");
				if (flag2 || flag3 || flag4 || flag5)
				{
					Event @event = Event.New("GetSpecialEffectChance", 0, 0, 0);
					@event.SetParameter("Actor", Actor);
					@event.SetParameter("Object", Object);
					@event.SetParameter("Subject", Subject);
					@event.SetParameter("Projectile", Projectile);
					@event.SetParameter("Type", Type);
					@event.SetParameter("BaseChance", num);
					@event.SetParameter("Chance", Chance);
					if (flag && flag2)
					{
						flag = Actor.FireEvent(@event);
					}
					if (flag && flag3)
					{
						flag = Object.FireEvent(@event);
					}
					if (flag && flag4)
					{
						flag = Subject.FireEvent(@event);
					}
					if (flag && flag5)
					{
						flag = Projectile.FireEvent(@event);
					}
					Chance = @event.GetIntParameter("Chance", 0);
				}
			}
			if (flag)
			{
				bool flag6 = GameObject.Validate(ref Actor) && Actor.WantEvent(PooledEvent<GetSpecialEffectChanceEvent>.ID, GetSpecialEffectChanceEvent.CascadeLevel);
				bool flag7 = GameObject.Validate(ref Object) && Object.WantEvent(PooledEvent<GetSpecialEffectChanceEvent>.ID, GetSpecialEffectChanceEvent.CascadeLevel);
				bool flag8 = GameObject.Validate(ref Subject) && Subject.WantEvent(PooledEvent<GetSpecialEffectChanceEvent>.ID, GetSpecialEffectChanceEvent.CascadeLevel);
				bool flag9 = GameObject.Validate(ref Projectile) && Projectile.WantEvent(PooledEvent<GetSpecialEffectChanceEvent>.ID, GetSpecialEffectChanceEvent.CascadeLevel);
				if (flag6 || flag7 || flag8 || flag9)
				{
					GetSpecialEffectChanceEvent getSpecialEffectChanceEvent = PooledEvent<GetSpecialEffectChanceEvent>.FromPool();
					getSpecialEffectChanceEvent.Actor = Actor;
					getSpecialEffectChanceEvent.Object = Object;
					getSpecialEffectChanceEvent.Subject = Subject;
					getSpecialEffectChanceEvent.Projectile = Projectile;
					getSpecialEffectChanceEvent.Type = Type;
					getSpecialEffectChanceEvent.BaseChance = num;
					getSpecialEffectChanceEvent.Chance = Chance;
					if (flag && flag6)
					{
						flag = Actor.HandleEvent(getSpecialEffectChanceEvent);
					}
					if (flag && flag7)
					{
						flag = Object.HandleEvent(getSpecialEffectChanceEvent);
					}
					if (flag && flag8)
					{
						flag = Subject.HandleEvent(getSpecialEffectChanceEvent);
					}
					if (flag && flag9)
					{
						flag = Projectile.HandleEvent(getSpecialEffectChanceEvent);
					}
					Chance = getSpecialEffectChanceEvent.Chance;
				}
			}
			if (ConstrainToPercentage)
			{
				if (Chance > 100)
				{
					Chance = 100;
				}
				else if (Chance < 0)
				{
					Chance = 0;
				}
			}
			else if (ConstrainToPermillage)
			{
				if (Chance > 1000)
				{
					Chance = 1000;
				}
				else if (Chance < 0)
				{
					Chance = 0;
				}
			}
			return Chance;
		}

		// Token: 0x040026DC RID: 9948
		public new static readonly int CascadeLevel = 17;

		// Token: 0x040026DD RID: 9949
		public GameObject Actor;

		// Token: 0x040026DE RID: 9950
		public GameObject Object;

		// Token: 0x040026DF RID: 9951
		public GameObject Subject;

		// Token: 0x040026E0 RID: 9952
		public GameObject Projectile;

		// Token: 0x040026E1 RID: 9953
		public string Type;

		// Token: 0x040026E2 RID: 9954
		public int BaseChance;

		// Token: 0x040026E3 RID: 9955
		public int Chance;
	}
}
