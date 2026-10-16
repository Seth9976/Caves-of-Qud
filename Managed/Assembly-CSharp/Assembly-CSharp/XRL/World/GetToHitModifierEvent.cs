using System;

namespace XRL.World
{
	// Token: 0x0200079A RID: 1946
	[GameEvent(Cascade = 17, Cache = MinEvent.Cache.Pool)]
	public class GetToHitModifierEvent : PooledEvent<GetToHitModifierEvent>
	{
		// Token: 0x06004F6D RID: 20333 RVA: 0x00183B71 File Offset: 0x00181D71
		public override int GetCascadeLevel()
		{
			return GetToHitModifierEvent.CascadeLevel;
		}

		// Token: 0x06004F6E RID: 20334 RVA: 0x00183B78 File Offset: 0x00181D78
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004F6F RID: 20335 RVA: 0x00183B84 File Offset: 0x00181D84
		public override void Reset()
		{
			base.Reset();
			this.Actor = null;
			this.Target = null;
			this.Weapon = null;
			this.Projectile = null;
			this.AimedAt = null;
			this.Modifier = 0;
			this.Prospective = false;
			this.Missile = false;
			this.Thrown = false;
			this.Skill = null;
			this.Stat = null;
			this.Checking = null;
		}

		// Token: 0x170006D8 RID: 1752
		// (get) Token: 0x06004F70 RID: 20336 RVA: 0x00183BEB File Offset: 0x00181DEB
		public bool Melee
		{
			get
			{
				return !this.Missile && !this.Thrown;
			}
		}

		// Token: 0x06004F71 RID: 20337 RVA: 0x00183C00 File Offset: 0x00181E00
		public static int GetFor(GameObject Actor, GameObject Target = null, GameObject Weapon = null, int Bonus = 0, GameObject Projectile = null, GameObject AimedAt = null, string Skill = null, string Stat = null, bool Prospective = false, bool Missile = false, bool Thrown = false)
		{
			int num = Bonus + Actor.StatMod("Agility", 0) + Actor.GetIntProperty("HitBonus", 0) + ((Weapon != null) ? Weapon.GetIntProperty("HitBonus", 0) : 0) + ((Target != null) ? Target.GetIntProperty("IncomingHitBonus", 0) : 0);
			if (Missile)
			{
				num += Actor.GetIntProperty("MissileHitBonus", 0) + ((Weapon != null) ? Weapon.GetIntProperty("MissileHitBonus", 0) : 0) + ((Projectile != null) ? Projectile.GetIntProperty("HitBonus", 0) : 0) + ((Projectile != null) ? Projectile.GetIntProperty("MissileHitBonus", 0) : 0) + ((Target != null) ? Target.GetIntProperty("IncomingMissileHitBonus", 0) : 0);
			}
			else if (Thrown)
			{
				num += Actor.GetIntProperty("ThrownHitBonus", 0) + ((Weapon != null) ? Weapon.GetIntProperty("ThrownHitBonus", 0) : 0) + ((Target != null) ? Target.GetIntProperty("IncomingThrownHitBonus", 0) : 0);
			}
			else
			{
				num += Actor.GetIntProperty("MeleeHitBonus", 0) + ((Weapon != null) ? Weapon.GetIntProperty("MeleeHitBonus", 0) : 0) + ((Target != null) ? Target.GetIntProperty("IncomingMeleeHitBonus", 0) : 0);
			}
			bool flag = true;
			if (flag)
			{
				bool flag2 = GameObject.Validate(ref Actor) && Actor.HasRegisteredEvent("GetToHitModifier");
				bool flag3 = GameObject.Validate(ref Target) && Target.HasRegisteredEvent("GetIncomingToHitModifier");
				if (flag2 || flag3)
				{
					Event @event = Event.New("GetToHitModifier", 0, 0, 0);
					@event.SetParameter("Actor", Actor);
					@event.SetParameter("Target", Target);
					@event.SetParameter("Weapon", Weapon);
					@event.SetParameter("Projectile", Projectile);
					@event.SetParameter("AimedAt", AimedAt);
					@event.SetParameter("Modifier", num);
					@event.SetParameter("Skill", Skill);
					@event.SetParameter("Stat", Stat);
					@event.SetFlag("Prospective", Prospective);
					@event.SetFlag("Missile", Missile);
					@event.SetFlag("Thrown", Thrown);
					if (flag && flag2)
					{
						flag = Actor.FireEvent(@event);
					}
					if (flag && flag3)
					{
						@event.ID = "GetIncomingToHitModifier";
						flag = Target.FireEvent(@event);
					}
					num = @event.GetIntParameter("Modifier", 0);
				}
			}
			if (flag)
			{
				bool flag4 = GameObject.Validate(ref Actor) && Actor.WantEvent(PooledEvent<GetToHitModifierEvent>.ID, GetToHitModifierEvent.CascadeLevel);
				bool flag5 = GameObject.Validate(ref Target) && Target.WantEvent(PooledEvent<GetToHitModifierEvent>.ID, GetToHitModifierEvent.CascadeLevel);
				if (flag4 || flag5)
				{
					GetToHitModifierEvent getToHitModifierEvent = PooledEvent<GetToHitModifierEvent>.FromPool();
					getToHitModifierEvent.Actor = Actor;
					getToHitModifierEvent.Target = Target;
					getToHitModifierEvent.Weapon = Weapon;
					getToHitModifierEvent.Projectile = Projectile;
					getToHitModifierEvent.AimedAt = AimedAt;
					getToHitModifierEvent.Modifier = num;
					getToHitModifierEvent.Skill = Skill;
					getToHitModifierEvent.Stat = Stat;
					getToHitModifierEvent.Prospective = Prospective;
					getToHitModifierEvent.Missile = Missile;
					getToHitModifierEvent.Thrown = Thrown;
					if (flag && flag4)
					{
						getToHitModifierEvent.Checking = "Actor";
						flag = Actor.HandleEvent(getToHitModifierEvent);
					}
					if (flag && flag5)
					{
						getToHitModifierEvent.Checking = "Target";
						flag = Target.HandleEvent(getToHitModifierEvent);
					}
					num = getToHitModifierEvent.Modifier;
				}
			}
			return num;
		}

		// Token: 0x04002721 RID: 10017
		public new static readonly int CascadeLevel = 17;

		// Token: 0x04002722 RID: 10018
		public GameObject Actor;

		// Token: 0x04002723 RID: 10019
		public GameObject Target;

		// Token: 0x04002724 RID: 10020
		public GameObject Weapon;

		// Token: 0x04002725 RID: 10021
		public GameObject Projectile;

		// Token: 0x04002726 RID: 10022
		public GameObject AimedAt;

		// Token: 0x04002727 RID: 10023
		public int Modifier;

		// Token: 0x04002728 RID: 10024
		public bool Prospective;

		// Token: 0x04002729 RID: 10025
		public bool Missile;

		// Token: 0x0400272A RID: 10026
		public bool Thrown;

		// Token: 0x0400272B RID: 10027
		public string Skill;

		// Token: 0x0400272C RID: 10028
		public string Stat;

		// Token: 0x0400272D RID: 10029
		public string Checking;
	}
}
