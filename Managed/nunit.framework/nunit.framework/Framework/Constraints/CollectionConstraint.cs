using System;
using System.Collections;

namespace NUnit.Framework.Constraints
{
	// Token: 0x020000EE RID: 238
	public abstract class CollectionConstraint : Constraint
	{
		// Token: 0x06000754 RID: 1876 RVA: 0x00011D90 File Offset: 0x0000FF90
		protected CollectionConstraint()
			: base(new object[0])
		{
		}

		// Token: 0x06000755 RID: 1877 RVA: 0x00011D9E File Offset: 0x0000FF9E
		protected CollectionConstraint(object arg)
			: base(new object[] { arg })
		{
		}

		// Token: 0x06000756 RID: 1878 RVA: 0x00011DB0 File Offset: 0x0000FFB0
		protected static bool IsEmpty(IEnumerable enumerable)
		{
			ICollection collection = enumerable as ICollection;
			if (collection != null)
			{
				return collection.Count == 0;
			}
			using (IEnumerator enumerator = enumerable.GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000757 RID: 1879 RVA: 0x00011E14 File Offset: 0x00010014
		public override ConstraintResult ApplyTo(object actual)
		{
			IEnumerable enumerable = actual as IEnumerable;
			if (enumerable == null)
			{
				throw new ArgumentException("The actual value must be an IEnumerable", "actual");
			}
			return new ConstraintResult(this, actual, this.Matches(enumerable));
		}

		// Token: 0x06000758 RID: 1880
		protected abstract bool Matches(IEnumerable collection);
	}
}
