using System;
using System.Collections;
using System.Collections.Generic;

namespace Wintellect.PowerCollections
{
	// Token: 0x0200030A RID: 778
	internal static class Util
	{
		// Token: 0x06001CD9 RID: 7385 RVA: 0x0008DD77 File Offset: 0x0008BF77
		public static bool IsCloneableType(Type type, out bool isValue)
		{
			isValue = false;
			if (typeof(ICloneable).IsAssignableFrom(type))
			{
				return true;
			}
			if (type.IsValueType)
			{
				isValue = true;
				return true;
			}
			return false;
		}

		// Token: 0x06001CDA RID: 7386 RVA: 0x0008DDA0 File Offset: 0x0008BFA0
		public static string SimpleClassName(Type type)
		{
			string text = type.Name;
			int num = text.IndexOfAny(new char[] { '<', '{', '`' });
			if (num >= 0)
			{
				text = text.Substring(0, num);
			}
			return text;
		}

		// Token: 0x06001CDB RID: 7387 RVA: 0x0008DDDA File Offset: 0x0008BFDA
		public static IEnumerable<T> CreateEnumerableWrapper<T>(IEnumerable<T> wrapped)
		{
			return new Util.WrapEnumerable<T>(wrapped);
		}

		// Token: 0x06001CDC RID: 7388 RVA: 0x0008DDE2 File Offset: 0x0008BFE2
		public static int GetHashCode<T>(T item, IEqualityComparer<T> equalityComparer)
		{
			if (item == null)
			{
				return 394715708;
			}
			return equalityComparer.GetHashCode(item);
		}

		// Token: 0x0200170F RID: 5903
		[Serializable]
		private class WrapEnumerable<T> : IEnumerable<T>, IEnumerable
		{
			// Token: 0x0600DB9D RID: 56221 RVA: 0x00506C3D File Offset: 0x00504E3D
			public WrapEnumerable(IEnumerable<T> wrapped)
			{
				this.wrapped = wrapped;
			}

			// Token: 0x0600DB9E RID: 56222 RVA: 0x00506C4C File Offset: 0x00504E4C
			public IEnumerator<T> GetEnumerator()
			{
				return this.wrapped.GetEnumerator();
			}

			// Token: 0x0600DB9F RID: 56223 RVA: 0x00506C59 File Offset: 0x00504E59
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.wrapped.GetEnumerator();
			}

			// Token: 0x04005CAE RID: 23726
			private IEnumerable<T> wrapped;
		}
	}
}
