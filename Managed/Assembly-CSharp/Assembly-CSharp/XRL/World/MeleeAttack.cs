using System;
using System.Collections.Generic;
using XRL.World.Anatomy;

namespace XRL.World
{
	// Token: 0x02000935 RID: 2357
	public class MeleeAttack : IDisposable
	{
		// Token: 0x06005B16 RID: 23318 RVA: 0x001B8CD8 File Offset: 0x001B6ED8
		public bool IsValidFor(BodyPart BodyPart)
		{
			return (this.BodyPart == null || BodyPart == this.BodyPart) && (this.Filter == null || this.Filter(BodyPart));
		}

		// Token: 0x06005B17 RID: 23319 RVA: 0x001B8D08 File Offset: 0x001B6F08
		public void Reset()
		{
			this.Chance = 0;
			this.HitModifier = 0;
			this.PenModifier = 0;
			this.Type = null;
			this.Properties = null;
			this.BodyPart = null;
			this.Source = null;
			this.Filter = null;
			this.Primary = null;
		}

		// Token: 0x06005B18 RID: 23320 RVA: 0x001B8D59 File Offset: 0x001B6F59
		private static MeleeAttack GetInternal()
		{
			if (MeleeAttack.Pool.Count <= 0)
			{
				return new MeleeAttack();
			}
			return MeleeAttack.Pool.Pop();
		}

		// Token: 0x06005B19 RID: 23321 RVA: 0x001B8D78 File Offset: 0x001B6F78
		public static MeleeAttack Get(int Chance = 0, int HitModifier = 0, int PenModifier = 0, object Source = null, string Type = null, string Properties = null, GameObject Weapon = null, BodyPart BodyPart = null, Predicate<BodyPart> Filter = null, bool Intrinsic = false, bool? Primary = null)
		{
			MeleeAttack @internal = MeleeAttack.GetInternal();
			@internal.Chance = Chance;
			@internal.HitModifier = HitModifier;
			@internal.PenModifier = PenModifier;
			@internal.Source = Source;
			@internal.Type = Type;
			@internal.Properties = Properties;
			@internal.BodyPart = BodyPart;
			@internal.Filter = Filter;
			@internal.Primary = Primary;
			return @internal;
		}

		// Token: 0x06005B1A RID: 23322 RVA: 0x001B8DCE File Offset: 0x001B6FCE
		public static void Return(MeleeAttack Attack)
		{
			Attack.Reset();
			MeleeAttack.Pool.Push(Attack);
		}

		// Token: 0x06005B1B RID: 23323 RVA: 0x001B8DE1 File Offset: 0x001B6FE1
		public void Dispose()
		{
			MeleeAttack.Return(this);
		}

		/// <summary>The chance that this melee attack will be attempted.</summary>
		// Token: 0x04002CE7 RID: 11495
		public int Chance;

		/// <summary>A to-hit modifier applied if the attack is attempted.</summary>
		// Token: 0x04002CE8 RID: 11496
		public int HitModifier;

		/// <summary>A penetration modifier applied if the attack is attempted.</summary>
		// Token: 0x04002CE9 RID: 11497
		public int PenModifier;

		/// <summary>The object or part that added this attack.</summary>
		// Token: 0x04002CEA RID: 11498
		public object Source;

		/// <summary>Comma delimited identifiers.</summary>
		// Token: 0x04002CEB RID: 11499
		public string Type;

		/// <summary>Comma delimited properties added to the attack.</summary>
		// Token: 0x04002CEC RID: 11500
		public string Properties;

		/// <summary>The weapon this attack is using.</summary>
		// Token: 0x04002CED RID: 11501
		public GameObject Weapon;

		/// <summary>The body part this weapon is attack from.</summary>
		// Token: 0x04002CEE RID: 11502
		public BodyPart BodyPart;

		/// <summary>Filter applicable body parts for this attack.</summary>
		// Token: 0x04002CEF RID: 11503
		public Predicate<BodyPart> Filter;

		/// <summary>Whether this attack is a basic natural attack of the weapon, not added via skills or mutations.</summary>
		// Token: 0x04002CF0 RID: 11504
		public bool Intrinsic;

		/// <summary>Whether this attack can be (or is being) executed from a primary (true), secondary (false), or any limb (null).</summary>
		// Token: 0x04002CF1 RID: 11505
		public bool? Primary;

		// Token: 0x04002CF2 RID: 11506
		private static Stack<MeleeAttack> Pool = new Stack<MeleeAttack>();
	}
}
