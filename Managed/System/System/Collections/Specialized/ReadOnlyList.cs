using System;

namespace System.Collections.Specialized
{
	// Token: 0x020004BA RID: 1210
	internal sealed class ReadOnlyList : IList, ICollection, IEnumerable
	{
		// Token: 0x0600270D RID: 9997 RVA: 0x00087AA6 File Offset: 0x00085CA6
		internal ReadOnlyList(IList list)
		{
			this._list = list;
		}

		// Token: 0x170007EF RID: 2031
		// (get) Token: 0x0600270E RID: 9998 RVA: 0x00087AB5 File Offset: 0x00085CB5
		public int Count
		{
			get
			{
				return this._list.Count;
			}
		}

		// Token: 0x170007F0 RID: 2032
		// (get) Token: 0x0600270F RID: 9999 RVA: 0x0000390E File Offset: 0x00001B0E
		public bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170007F1 RID: 2033
		// (get) Token: 0x06002710 RID: 10000 RVA: 0x0000390E File Offset: 0x00001B0E
		public bool IsFixedSize
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170007F2 RID: 2034
		// (get) Token: 0x06002711 RID: 10001 RVA: 0x00087AC2 File Offset: 0x00085CC2
		public bool IsSynchronized
		{
			get
			{
				return this._list.IsSynchronized;
			}
		}

		// Token: 0x170007F3 RID: 2035
		public object this[int index]
		{
			get
			{
				return this._list[index];
			}
			set
			{
				throw new NotSupportedException("Collection is read-only.");
			}
		}

		// Token: 0x170007F4 RID: 2036
		// (get) Token: 0x06002714 RID: 10004 RVA: 0x00087ADD File Offset: 0x00085CDD
		public object SyncRoot
		{
			get
			{
				return this._list.SyncRoot;
			}
		}

		// Token: 0x06002715 RID: 10005 RVA: 0x00035761 File Offset: 0x00033961
		public int Add(object value)
		{
			throw new NotSupportedException("Collection is read-only.");
		}

		// Token: 0x06002716 RID: 10006 RVA: 0x00035761 File Offset: 0x00033961
		public void Clear()
		{
			throw new NotSupportedException("Collection is read-only.");
		}

		// Token: 0x06002717 RID: 10007 RVA: 0x00087AEA File Offset: 0x00085CEA
		public bool Contains(object value)
		{
			return this._list.Contains(value);
		}

		// Token: 0x06002718 RID: 10008 RVA: 0x00087AF8 File Offset: 0x00085CF8
		public void CopyTo(Array array, int index)
		{
			this._list.CopyTo(array, index);
		}

		// Token: 0x06002719 RID: 10009 RVA: 0x00087B07 File Offset: 0x00085D07
		public IEnumerator GetEnumerator()
		{
			return this._list.GetEnumerator();
		}

		// Token: 0x0600271A RID: 10010 RVA: 0x00087B14 File Offset: 0x00085D14
		public int IndexOf(object value)
		{
			return this._list.IndexOf(value);
		}

		// Token: 0x0600271B RID: 10011 RVA: 0x00035761 File Offset: 0x00033961
		public void Insert(int index, object value)
		{
			throw new NotSupportedException("Collection is read-only.");
		}

		// Token: 0x0600271C RID: 10012 RVA: 0x00035761 File Offset: 0x00033961
		public void Remove(object value)
		{
			throw new NotSupportedException("Collection is read-only.");
		}

		// Token: 0x0600271D RID: 10013 RVA: 0x00035761 File Offset: 0x00033961
		public void RemoveAt(int index)
		{
			throw new NotSupportedException("Collection is read-only.");
		}

		// Token: 0x04001542 RID: 5442
		private readonly IList _list;
	}
}
