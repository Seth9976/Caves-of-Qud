using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000238 RID: 568
	internal sealed class FirstAmongEqualsSet<T> : IEnumerable<T>, IEnumerable
	{
		// Token: 0x06002EFD RID: 12029 RVA: 0x000F1BE2 File Offset: 0x000EFDE2
		public FirstAmongEqualsSet(IEnumerable<T> items, IEqualityComparer<T> equalityComparer, Func<T, T, int> canonicalComparer)
		{
			this._canonicalComparer = canonicalComparer;
			this._dictionary = new Dictionary<T, T>(equalityComparer);
			this._hashSet = new HashSet<T>(equalityComparer);
			this.UnionWith(items);
		}

		// Token: 0x06002EFE RID: 12030 RVA: 0x000F1C10 File Offset: 0x000EFE10
		public void UnionWith(IEnumerable<T> items)
		{
			foreach (T t in items)
			{
				T t2;
				if (!this._dictionary.TryGetValue(t, ref t2) || this.IsMoreCanonical(t, t2))
				{
					this._dictionary[t] = t;
				}
			}
		}

		// Token: 0x06002EFF RID: 12031 RVA: 0x000F1C78 File Offset: 0x000EFE78
		private bool IsMoreCanonical(T newItem, T oldItem)
		{
			return this._canonicalComparer.Invoke(newItem, oldItem) > 0;
		}

		// Token: 0x06002F00 RID: 12032 RVA: 0x000F1C8C File Offset: 0x000EFE8C
		public void IntersectWith(IEnumerable<T> items)
		{
			this._hashSet.UnionWith(items);
			foreach (T t in Enumerable.ToList<T>(this._dictionary.Keys))
			{
				if (!this._hashSet.Contains(t))
				{
					this._dictionary.Remove(t);
				}
			}
			foreach (T t2 in this._hashSet)
			{
				T t3;
				if (this._dictionary.TryGetValue(t2, ref t3) && this.IsMoreCanonical(t2, t3))
				{
					this._dictionary[t2] = t2;
				}
			}
			this._hashSet.Clear();
		}

		// Token: 0x06002F01 RID: 12033 RVA: 0x000F1D78 File Offset: 0x000EFF78
		public IEnumerator<T> GetEnumerator()
		{
			return this._dictionary.Values.GetEnumerator();
		}

		// Token: 0x06002F02 RID: 12034 RVA: 0x000F1D8F File Offset: 0x000EFF8F
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0400137A RID: 4986
		private readonly HashSet<T> _hashSet;

		// Token: 0x0400137B RID: 4987
		private readonly Dictionary<T, T> _dictionary;

		// Token: 0x0400137C RID: 4988
		private readonly Func<T, T, int> _canonicalComparer;
	}
}
