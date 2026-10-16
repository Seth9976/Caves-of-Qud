using System;
using System.Collections.Generic;
using XRL.World.Anatomy;

namespace XRL.World
{
	// Token: 0x020007D0 RID: 2000
	[GameEvent(Base = true)]
	public abstract class IMeleeAttackEvent : MinEvent
	{
		// Token: 0x0600509C RID: 20636 RVA: 0x001872EB File Offset: 0x001854EB
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x0600509D RID: 20637 RVA: 0x001872F4 File Offset: 0x001854F4
		public override void Reset()
		{
			base.Reset();
			this.Actor = null;
			this.Attacks.Clear();
		}

		// Token: 0x0600509E RID: 20638 RVA: 0x00187310 File Offset: 0x00185510
		public bool AddAttack(int Chance = 0, int HitModifier = 0, int PenModifier = 0, object Source = null, string Type = null, string Properties = null, GameObject Weapon = null, BodyPart BodyPart = null, Predicate<BodyPart> Filter = null, bool? Primary = null, bool Instrinsic = false, bool Distinct = true, bool Replace = true)
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

		// Token: 0x0600509F RID: 20639 RVA: 0x00187418 File Offset: 0x00185618
		public bool HasAttackByWeapon(GameObject Weapon)
		{
			int i = 0;
			int count = this.Attacks.Count;
			while (i < count)
			{
				if (this.Attacks[i].Weapon == Weapon)
				{
					return true;
				}
				i++;
			}
			return false;
		}

		// Token: 0x060050A0 RID: 20640 RVA: 0x00187454 File Offset: 0x00185654
		public bool RemoveAttack(MeleeAttack Attack)
		{
			if (this.Attacks.Remove(Attack))
			{
				MeleeAttack.Return(Attack);
				return true;
			}
			return false;
		}

		// Token: 0x060050A1 RID: 20641 RVA: 0x0018746D File Offset: 0x0018566D
		public void RemoveAttackAt(int Index)
		{
			MeleeAttack.Return(this.Attacks[Index]);
			this.Attacks.RemoveAt(Index);
		}

		// Token: 0x0400282C RID: 10284
		public GameObject Actor;

		// Token: 0x0400282D RID: 10285
		public List<MeleeAttack> Attacks = new List<MeleeAttack>();
	}
}
