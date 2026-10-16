using System;
using System.Collections;
using System.Collections.Generic;

namespace Trivial.Mono.Collections.Generic
{
	// Token: 0x0200000A RID: 10
	public sealed class ReadOnlyCollection<T> : Collection<T>, ICollection<T>, IEnumerable<T>, IEnumerable, IList, ICollection
	{
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000042 RID: 66 RVA: 0x00002AE2 File Offset: 0x00000CE2
		public static ReadOnlyCollection<T> Empty
		{
			get
			{
				ReadOnlyCollection<T> readOnlyCollection;
				if ((readOnlyCollection = ReadOnlyCollection<T>.empty) == null)
				{
					readOnlyCollection = (ReadOnlyCollection<T>.empty = new ReadOnlyCollection<T>());
				}
				return readOnlyCollection;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000043 RID: 67 RVA: 0x00002AF8 File Offset: 0x00000CF8
		bool ICollection<T>.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000044 RID: 68 RVA: 0x00002AF8 File Offset: 0x00000CF8
		bool IList.IsFixedSize
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000045 RID: 69 RVA: 0x00002AF8 File Offset: 0x00000CF8
		bool IList.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002AFB File Offset: 0x00000CFB
		private ReadOnlyCollection()
		{
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002B03 File Offset: 0x00000D03
		public ReadOnlyCollection(T[] array)
		{
			if (array == null)
			{
				throw new ArgumentNullException();
			}
			this.Initialize(array, array.Length);
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002B1E File Offset: 0x00000D1E
		public ReadOnlyCollection(Collection<T> collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException();
			}
			this.Initialize(collection.items, collection.size);
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002B41 File Offset: 0x00000D41
		private void Initialize(T[] items, int size)
		{
			this.items = new T[size];
			Array.Copy(items, 0, this.items, 0, size);
			this.size = size;
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002B65 File Offset: 0x00000D65
		internal override void Grow(int desired)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002B65 File Offset: 0x00000D65
		protected override void OnAdd(T item, int index)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002B65 File Offset: 0x00000D65
		protected override void OnClear()
		{
			throw new InvalidOperationException();
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002B65 File Offset: 0x00000D65
		protected override void OnInsert(T item, int index)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002B65 File Offset: 0x00000D65
		protected override void OnRemove(T item, int index)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002B65 File Offset: 0x00000D65
		protected override void OnSet(T item, int index)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x04000009 RID: 9
		private static ReadOnlyCollection<T> empty;
	}
}
