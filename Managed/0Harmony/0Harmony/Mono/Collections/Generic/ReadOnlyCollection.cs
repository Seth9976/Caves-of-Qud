using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace Mono.Collections.Generic
{
	// Token: 0x020001AC RID: 428
	internal sealed class ReadOnlyCollection<T> : Collection<T>, ICollection<T>, IEnumerable<T>, IEnumerable, IList, ICollection
	{
		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x0600072D RID: 1837 RVA: 0x0001891C File Offset: 0x00016B1C
		public static ReadOnlyCollection<T> Empty
		{
			get
			{
				if (ReadOnlyCollection<T>.empty != null)
				{
					return ReadOnlyCollection<T>.empty;
				}
				Interlocked.CompareExchange<ReadOnlyCollection<T>>(ref ReadOnlyCollection<T>.empty, new ReadOnlyCollection<T>(), null);
				return ReadOnlyCollection<T>.empty;
			}
		}

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x0600072E RID: 1838 RVA: 0x00018941 File Offset: 0x00016B41
		bool ICollection<T>.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x0600072F RID: 1839 RVA: 0x00018941 File Offset: 0x00016B41
		bool IList.IsFixedSize
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x06000730 RID: 1840 RVA: 0x00018941 File Offset: 0x00016B41
		bool IList.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000731 RID: 1841 RVA: 0x00018944 File Offset: 0x00016B44
		private ReadOnlyCollection()
		{
		}

		// Token: 0x06000732 RID: 1842 RVA: 0x0001894C File Offset: 0x00016B4C
		public ReadOnlyCollection(T[] array)
		{
			if (array == null)
			{
				throw new ArgumentNullException();
			}
			this.Initialize(array, array.Length);
		}

		// Token: 0x06000733 RID: 1843 RVA: 0x00018967 File Offset: 0x00016B67
		public ReadOnlyCollection(Collection<T> collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException();
			}
			this.Initialize(collection.items, collection.size);
		}

		// Token: 0x06000734 RID: 1844 RVA: 0x0001898A File Offset: 0x00016B8A
		private void Initialize(T[] items, int size)
		{
			this.items = new T[size];
			Array.Copy(items, 0, this.items, 0, size);
			this.size = size;
		}

		// Token: 0x06000735 RID: 1845 RVA: 0x000189AE File Offset: 0x00016BAE
		internal override void Grow(int desired)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x06000736 RID: 1846 RVA: 0x000189AE File Offset: 0x00016BAE
		protected override void OnAdd(T item, int index)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x06000737 RID: 1847 RVA: 0x000189AE File Offset: 0x00016BAE
		protected override void OnClear()
		{
			throw new InvalidOperationException();
		}

		// Token: 0x06000738 RID: 1848 RVA: 0x000189AE File Offset: 0x00016BAE
		protected override void OnInsert(T item, int index)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x06000739 RID: 1849 RVA: 0x000189AE File Offset: 0x00016BAE
		protected override void OnRemove(T item, int index)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x0600073A RID: 1850 RVA: 0x000189AE File Offset: 0x00016BAE
		protected override void OnSet(T item, int index)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x04000241 RID: 577
		private static ReadOnlyCollection<T> empty;
	}
}
