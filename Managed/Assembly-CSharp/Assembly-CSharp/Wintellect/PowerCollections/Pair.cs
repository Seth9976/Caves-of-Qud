using System;
using System.Collections.Generic;

namespace Wintellect.PowerCollections
{
	// Token: 0x02000300 RID: 768
	[Serializable]
	public struct Pair<TFirst, TSecond> : IComparable, IComparable<Pair<TFirst, TSecond>>
	{
		// Token: 0x06001C0E RID: 7182 RVA: 0x0008B30E File Offset: 0x0008950E
		public Pair(TFirst first, TSecond second)
		{
			this.First = first;
			this.Second = second;
		}

		// Token: 0x06001C0F RID: 7183 RVA: 0x0008B31E File Offset: 0x0008951E
		public Pair(KeyValuePair<TFirst, TSecond> keyAndValue)
		{
			this.First = keyAndValue.Key;
			this.Second = keyAndValue.Value;
		}

		// Token: 0x06001C10 RID: 7184 RVA: 0x0008B33C File Offset: 0x0008953C
		public override bool Equals(object obj)
		{
			if (obj != null && obj is Pair<TFirst, TSecond>)
			{
				Pair<TFirst, TSecond> pair = (Pair<TFirst, TSecond>)obj;
				return this.Equals(pair);
			}
			return false;
		}

		// Token: 0x06001C11 RID: 7185 RVA: 0x0008B364 File Offset: 0x00089564
		public bool Equals(Pair<TFirst, TSecond> other)
		{
			return Pair<TFirst, TSecond>.firstEqualityComparer.Equals(this.First, other.First) && Pair<TFirst, TSecond>.secondEqualityComparer.Equals(this.Second, other.Second);
		}

		// Token: 0x06001C12 RID: 7186 RVA: 0x0008B398 File Offset: 0x00089598
		public override int GetHashCode()
		{
			int num = ((this.First == null) ? 1642088727 : this.First.GetHashCode());
			int num2 = ((this.Second == null) ? 428791459 : this.Second.GetHashCode());
			return num ^ num2;
		}

		// Token: 0x06001C13 RID: 7187 RVA: 0x0008B3F4 File Offset: 0x000895F4
		public int CompareTo(Pair<TFirst, TSecond> other)
		{
			int num2;
			try
			{
				int num = Pair<TFirst, TSecond>.firstComparer.Compare(this.First, other.First);
				if (num != 0)
				{
					num2 = num;
				}
				else
				{
					num2 = Pair<TFirst, TSecond>.secondComparer.Compare(this.Second, other.Second);
				}
			}
			catch (ArgumentException)
			{
				if (!typeof(IComparable<TFirst>).IsAssignableFrom(typeof(TFirst)) && !typeof(IComparable).IsAssignableFrom(typeof(TFirst)))
				{
					throw new NotSupportedException(string.Format(Strings.UncomparableType, typeof(TFirst).FullName));
				}
				if (!typeof(IComparable<TSecond>).IsAssignableFrom(typeof(TSecond)) && !typeof(IComparable).IsAssignableFrom(typeof(TSecond)))
				{
					throw new NotSupportedException(string.Format(Strings.UncomparableType, typeof(TSecond).FullName));
				}
				throw;
			}
			return num2;
		}

		// Token: 0x06001C14 RID: 7188 RVA: 0x0008B4FC File Offset: 0x000896FC
		int IComparable.CompareTo(object obj)
		{
			if (obj is Pair<TFirst, TSecond>)
			{
				return this.CompareTo((Pair<TFirst, TSecond>)obj);
			}
			throw new ArgumentException(Strings.BadComparandType, "obj");
		}

		// Token: 0x06001C15 RID: 7189 RVA: 0x0008B524 File Offset: 0x00089724
		public override string ToString()
		{
			return string.Format("First: {0}, Second: {1}", (this.First == null) ? "null" : this.First.ToString(), (this.Second == null) ? "null" : this.Second.ToString());
		}

		// Token: 0x06001C16 RID: 7190 RVA: 0x0008B585 File Offset: 0x00089785
		public static bool operator ==(Pair<TFirst, TSecond> pair1, Pair<TFirst, TSecond> pair2)
		{
			return Pair<TFirst, TSecond>.firstEqualityComparer.Equals(pair1.First, pair2.First) && Pair<TFirst, TSecond>.secondEqualityComparer.Equals(pair1.Second, pair2.Second);
		}

		// Token: 0x06001C17 RID: 7191 RVA: 0x0008B5B7 File Offset: 0x000897B7
		public static bool operator !=(Pair<TFirst, TSecond> pair1, Pair<TFirst, TSecond> pair2)
		{
			return !(pair1 == pair2);
		}

		// Token: 0x06001C18 RID: 7192 RVA: 0x0008B5C3 File Offset: 0x000897C3
		public static explicit operator KeyValuePair<TFirst, TSecond>(Pair<TFirst, TSecond> pair)
		{
			return new KeyValuePair<TFirst, TSecond>(pair.First, pair.Second);
		}

		// Token: 0x06001C19 RID: 7193 RVA: 0x0008B5D6 File Offset: 0x000897D6
		public KeyValuePair<TFirst, TSecond> ToKeyValuePair()
		{
			return new KeyValuePair<TFirst, TSecond>(this.First, this.Second);
		}

		// Token: 0x06001C1A RID: 7194 RVA: 0x0008B5E9 File Offset: 0x000897E9
		public static explicit operator Pair<TFirst, TSecond>(KeyValuePair<TFirst, TSecond> keyAndValue)
		{
			return new Pair<TFirst, TSecond>(keyAndValue);
		}

		// Token: 0x04000F1F RID: 3871
		private static IComparer<TFirst> firstComparer = Comparer<TFirst>.Default;

		// Token: 0x04000F20 RID: 3872
		private static IComparer<TSecond> secondComparer = Comparer<TSecond>.Default;

		// Token: 0x04000F21 RID: 3873
		private static IEqualityComparer<TFirst> firstEqualityComparer = EqualityComparer<TFirst>.Default;

		// Token: 0x04000F22 RID: 3874
		private static IEqualityComparer<TSecond> secondEqualityComparer = EqualityComparer<TSecond>.Default;

		// Token: 0x04000F23 RID: 3875
		public TFirst First;

		// Token: 0x04000F24 RID: 3876
		public TSecond Second;
	}
}
