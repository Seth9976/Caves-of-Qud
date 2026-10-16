using System;
using System.Collections.Generic;
using XRL.World.Anatomy;

namespace XRL.World
{
	// Token: 0x02000765 RID: 1893
	[GameEvent(Cascade = 17, Cache = MinEvent.Cache.Pool)]
	public class GetMeleeAttacksEvent : PooledEvent<GetMeleeAttacksEvent>
	{
		// Token: 0x06004E01 RID: 19969 RVA: 0x0017EA3F File Offset: 0x0017CC3F
		public override int GetCascadeLevel()
		{
			return GetMeleeAttacksEvent.CascadeLevel;
		}

		// Token: 0x06004E02 RID: 19970 RVA: 0x0017EA46 File Offset: 0x0017CC46
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004E03 RID: 19971 RVA: 0x0017EA4F File Offset: 0x0017CC4F
		public override void Reset()
		{
			base.Reset();
			this.Actor = null;
			this.Attacks.Clear();
			this.Properties = null;
			this.ChanceModifier = 0;
			this.ChanceMultiplier = 1.0;
		}

		// Token: 0x06004E04 RID: 19972 RVA: 0x0017EA88 File Offset: 0x0017CC88
		public bool AddAttack(int Chance = 0, int HitModifier = 0, int PenModifier = 0, object Source = null, string Type = null, string Properties = null, BodyPart BodyPart = null, Predicate<BodyPart> Filter = null, bool? Primary = null, bool Distinct = true, bool Replace = true)
		{
			if (Distinct && Source != null)
			{
				int i = 0;
				while (i < this.Attacks.Count)
				{
					if (this.Attacks[i].Source == Source)
					{
						if (!Replace || Chance <= this.Attacks[i].Chance)
						{
							return false;
						}
						this.RemoveAttackAt(i);
						break;
					}
					else
					{
						i++;
					}
				}
			}
			MeleeAttack meleeAttack = MeleeAttack.Get(Chance, HitModifier, PenModifier, Source, Type, Properties, null, BodyPart, Filter, false, Primary);
			if (BodyPart != null)
			{
				this.Attacks.Insert(this.Attacks.FindLastIndex((MeleeAttack x) => x.BodyPart != null) + 1, meleeAttack);
			}
			else if (Filter != null)
			{
				this.Attacks.Insert(this.Attacks.FindLastIndex((MeleeAttack x) => x.Filter != null || x.BodyPart != null) + 1, meleeAttack);
			}
			else
			{
				this.Attacks.Add(meleeAttack);
			}
			return true;
		}

		// Token: 0x06004E05 RID: 19973 RVA: 0x0017EB8F File Offset: 0x0017CD8F
		public bool RemoveAttack(MeleeAttack Attack)
		{
			if (this.Attacks.Remove(Attack))
			{
				MeleeAttack.Return(Attack);
				return true;
			}
			return false;
		}

		// Token: 0x06004E06 RID: 19974 RVA: 0x0017EBA8 File Offset: 0x0017CDA8
		public void RemoveAttackAt(int Index)
		{
			MeleeAttack.Return(this.Attacks[Index]);
			this.Attacks.RemoveAt(Index);
		}

		// Token: 0x06004E07 RID: 19975 RVA: 0x0017EBC8 File Offset: 0x0017CDC8
		public static GetMeleeAttacksEvent HandleFrom(GameObject Actor, string Properties = null, int ChanceModifier = 0, double ChanceMultiplier = 1.0)
		{
			GetMeleeAttacksEvent getMeleeAttacksEvent = PooledEvent<GetMeleeAttacksEvent>.FromPool();
			getMeleeAttacksEvent.Properties = Properties;
			getMeleeAttacksEvent.ChanceModifier = ChanceModifier;
			getMeleeAttacksEvent.ChanceMultiplier = ChanceMultiplier;
			Actor.HandleEvent(getMeleeAttacksEvent);
			return getMeleeAttacksEvent;
		}

		// Token: 0x06004E08 RID: 19976 RVA: 0x0017EBFC File Offset: 0x0017CDFC
		public static MeleeAttack GetBestFor(GameObject Actor, BodyPart BodyPart = null, string Properties = null, int ChanceModifier = 0, double ChanceMultiplier = 1.0)
		{
			GetMeleeAttacksEvent getMeleeAttacksEvent = GetMeleeAttacksEvent.HandleFrom(Actor, Properties, ChanceModifier, ChanceMultiplier);
			int num = 0;
			int num2 = -1;
			for (int i = getMeleeAttacksEvent.Attacks.Count - 1; i >= 0; i--)
			{
				if (getMeleeAttacksEvent.Attacks[i].Chance > num && (BodyPart == null || getMeleeAttacksEvent.Attacks[i].IsValidFor(BodyPart)))
				{
					num = getMeleeAttacksEvent.Attacks[i].Chance;
					num2 = i;
				}
			}
			MeleeAttack meleeAttack = ((num2 >= 0) ? getMeleeAttacksEvent.Attacks[num2] : new MeleeAttack());
			meleeAttack.Chance += getMeleeAttacksEvent.ChanceModifier;
			PooledEvent<GetMeleeAttacksEvent>.ResetTo(ref getMeleeAttacksEvent);
			return meleeAttack;
		}

		// Token: 0x040025F6 RID: 9718
		public new static readonly int CascadeLevel = 17;

		// Token: 0x040025F7 RID: 9719
		public GameObject Actor;

		// Token: 0x040025F8 RID: 9720
		public List<MeleeAttack> Attacks = new List<MeleeAttack>();

		// Token: 0x040025F9 RID: 9721
		public string Properties;

		// Token: 0x040025FA RID: 9722
		public int ChanceModifier;

		// Token: 0x040025FB RID: 9723
		public double ChanceMultiplier = 1.0;
	}
}
