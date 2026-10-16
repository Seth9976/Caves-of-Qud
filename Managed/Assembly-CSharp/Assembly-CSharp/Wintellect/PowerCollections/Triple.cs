using System;
using System.Collections.Generic;

namespace Wintellect.PowerCollections
{
	// Token: 0x02000309 RID: 777
	[Serializable]
	public struct Triple<TFirst, TSecond, TThird> : IComparable, IComparable<Triple<TFirst, TSecond, TThird>>
	{
		// Token: 0x06001CCF RID: 7375 RVA: 0x0008D9CB File Offset: 0x0008BBCB
		public Triple(TFirst first, TSecond second, TThird third)
		{
			this.First = first;
			this.Second = second;
			this.Third = third;
		}

		// Token: 0x06001CD0 RID: 7376 RVA: 0x0008D9E4 File Offset: 0x0008BBE4
		public override bool Equals(object obj)
		{
			if (obj != null && obj is Triple<TFirst, TSecond, TThird>)
			{
				Triple<TFirst, TSecond, TThird> triple = (Triple<TFirst, TSecond, TThird>)obj;
				return this.Equals(triple);
			}
			return false;
		}

		// Token: 0x06001CD1 RID: 7377 RVA: 0x0008DA0C File Offset: 0x0008BC0C
		public bool Equals(Triple<TFirst, TSecond, TThird> other)
		{
			return Triple<TFirst, TSecond, TThird>.firstEqualityComparer.Equals(this.First, other.First) && Triple<TFirst, TSecond, TThird>.secondEqualityComparer.Equals(this.Second, other.Second) && Triple<TFirst, TSecond, TThird>.thirdEqualityComparer.Equals(this.Third, other.Third);
		}

		// Token: 0x06001CD2 RID: 7378 RVA: 0x0008DA64 File Offset: 0x0008BC64
		public override int GetHashCode()
		{
			int num = ((this.First == null) ? 1642088727 : this.First.GetHashCode());
			int num2 = ((this.Second == null) ? 428791459 : this.Second.GetHashCode());
			int num3 = ((this.Third == null) ? 1090263159 : this.Third.GetHashCode());
			return num ^ num2 ^ num3;
		}

		// Token: 0x06001CD3 RID: 7379 RVA: 0x0008DAE8 File Offset: 0x0008BCE8
		public int CompareTo(Triple<TFirst, TSecond, TThird> other)
		{
			int num2;
			try
			{
				int num = Triple<TFirst, TSecond, TThird>.firstComparer.Compare(this.First, other.First);
				if (num != 0)
				{
					num2 = num;
				}
				else
				{
					int num3 = Triple<TFirst, TSecond, TThird>.secondComparer.Compare(this.Second, other.Second);
					if (num3 != 0)
					{
						num2 = num3;
					}
					else
					{
						num2 = Triple<TFirst, TSecond, TThird>.thirdComparer.Compare(this.Third, other.Third);
					}
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
				if (!typeof(IComparable<TThird>).IsAssignableFrom(typeof(TThird)) && !typeof(IComparable).IsAssignableFrom(typeof(TThird)))
				{
					throw new NotSupportedException(string.Format(Strings.UncomparableType, typeof(TThird).FullName));
				}
				throw;
			}
			return num2;
		}

		// Token: 0x06001CD4 RID: 7380 RVA: 0x0008DC74 File Offset: 0x0008BE74
		int IComparable.CompareTo(object obj)
		{
			if (obj is Triple<TFirst, TSecond, TThird>)
			{
				return this.CompareTo((Triple<TFirst, TSecond, TThird>)obj);
			}
			throw new ArgumentException(Strings.BadComparandType, "obj");
		}

		// Token: 0x06001CD5 RID: 7381 RVA: 0x0008DC9C File Offset: 0x0008BE9C
		public override string ToString()
		{
			return string.Format("First: {0}, Second: {1}, Third: {2}", (this.First == null) ? "null" : this.First.ToString(), (this.Second == null) ? "null" : this.Second.ToString(), (this.Third == null) ? "null" : this.Third.ToString());
		}

		// Token: 0x06001CD6 RID: 7382 RVA: 0x0008DD22 File Offset: 0x0008BF22
		public static bool operator ==(Triple<TFirst, TSecond, TThird> pair1, Triple<TFirst, TSecond, TThird> pair2)
		{
			return pair1.Equals(pair2);
		}

		// Token: 0x06001CD7 RID: 7383 RVA: 0x0008DD2C File Offset: 0x0008BF2C
		public static bool operator !=(Triple<TFirst, TSecond, TThird> pair1, Triple<TFirst, TSecond, TThird> pair2)
		{
			return !pair1.Equals(pair2);
		}

		// Token: 0x04000F4A RID: 3914
		private static IComparer<TFirst> firstComparer = Comparer<TFirst>.Default;

		// Token: 0x04000F4B RID: 3915
		private static IComparer<TSecond> secondComparer = Comparer<TSecond>.Default;

		// Token: 0x04000F4C RID: 3916
		private static IComparer<TThird> thirdComparer = Comparer<TThird>.Default;

		// Token: 0x04000F4D RID: 3917
		private static IEqualityComparer<TFirst> firstEqualityComparer = EqualityComparer<TFirst>.Default;

		// Token: 0x04000F4E RID: 3918
		private static IEqualityComparer<TSecond> secondEqualityComparer = EqualityComparer<TSecond>.Default;

		// Token: 0x04000F4F RID: 3919
		private static IEqualityComparer<TThird> thirdEqualityComparer = EqualityComparer<TThird>.Default;

		// Token: 0x04000F50 RID: 3920
		public TFirst First;

		// Token: 0x04000F51 RID: 3921
		public TSecond Second;

		// Token: 0x04000F52 RID: 3922
		public TThird Third;
	}
}
