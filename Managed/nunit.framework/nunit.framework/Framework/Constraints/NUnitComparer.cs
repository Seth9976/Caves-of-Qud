using System;
using System.Collections;
using System.Reflection;

namespace NUnit.Framework.Constraints
{
	// Token: 0x02000120 RID: 288
	public class NUnitComparer : IComparer
	{
		// Token: 0x17000216 RID: 534
		// (get) Token: 0x06000906 RID: 2310 RVA: 0x0001565F File Offset: 0x0001385F
		public static NUnitComparer Default
		{
			get
			{
				return new NUnitComparer();
			}
		}

		// Token: 0x06000907 RID: 2311 RVA: 0x00015668 File Offset: 0x00013868
		public int Compare(object x, object y)
		{
			if (x == null)
			{
				if (y != null)
				{
					return -1;
				}
				return 0;
			}
			else
			{
				if (y == null)
				{
					return 1;
				}
				if (x is char && y is char)
				{
					return ((char)x != (char)y) ? 1 : 0;
				}
				if (Numerics.IsNumericType(x) && Numerics.IsNumericType(y))
				{
					return Numerics.Compare(x, y);
				}
				if (x is IComparable)
				{
					return ((IComparable)x).CompareTo(y);
				}
				if (y is IComparable)
				{
					return -((IComparable)y).CompareTo(x);
				}
				Type type = x.GetType();
				Type type2 = y.GetType();
				MethodInfo methodInfo = type.GetMethod("CompareTo", new Type[] { type2 });
				if (methodInfo != null)
				{
					return (int)methodInfo.Invoke(x, new object[] { y });
				}
				methodInfo = type2.GetMethod("CompareTo", new Type[] { type });
				if (methodInfo != null)
				{
					return -(int)methodInfo.Invoke(y, new object[] { x });
				}
				throw new ArgumentException("Neither value implements IComparable or IComparable<T>");
			}
		}
	}
}
