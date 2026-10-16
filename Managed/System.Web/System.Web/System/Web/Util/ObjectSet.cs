using System;
using System.Collections;
using System.Collections.Specialized;

namespace System.Web.Util
{
	// Token: 0x0200011F RID: 287
	internal class ObjectSet : ICollection, IEnumerable
	{
		// Token: 0x06000DFD RID: 3581 RVA: 0x00002050 File Offset: 0x00000250
		internal ObjectSet()
		{
		}

		// Token: 0x170004DC RID: 1244
		// (get) Token: 0x06000DFE RID: 3582 RVA: 0x00008A4D File Offset: 0x00006C4D
		protected virtual bool CaseInsensitive
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000DFF RID: 3583 RVA: 0x00025883 File Offset: 0x00023A83
		public void Add(object o)
		{
			if (this._objects == null)
			{
				this._objects = new HybridDictionary(this.CaseInsensitive);
			}
			this._objects[o] = null;
		}

		// Token: 0x06000E00 RID: 3584 RVA: 0x000258AC File Offset: 0x00023AAC
		public void AddCollection(ICollection c)
		{
			foreach (object obj in c)
			{
				this.Add(obj);
			}
		}

		// Token: 0x06000E01 RID: 3585 RVA: 0x000258FC File Offset: 0x00023AFC
		public void Remove(object o)
		{
			if (this._objects == null)
			{
				return;
			}
			this._objects.Remove(o);
		}

		// Token: 0x06000E02 RID: 3586 RVA: 0x00025913 File Offset: 0x00023B13
		public bool Contains(object o)
		{
			return this._objects != null && this._objects.Contains(o);
		}

		// Token: 0x06000E03 RID: 3587 RVA: 0x0002592B File Offset: 0x00023B2B
		IEnumerator IEnumerable.GetEnumerator()
		{
			if (this._objects == null)
			{
				return ObjectSet._emptyEnumerator;
			}
			return this._objects.Keys.GetEnumerator();
		}

		// Token: 0x170004DD RID: 1245
		// (get) Token: 0x06000E04 RID: 3588 RVA: 0x0002594B File Offset: 0x00023B4B
		public int Count
		{
			get
			{
				if (this._objects == null)
				{
					return 0;
				}
				return this._objects.Keys.Count;
			}
		}

		// Token: 0x170004DE RID: 1246
		// (get) Token: 0x06000E05 RID: 3589 RVA: 0x00025967 File Offset: 0x00023B67
		bool ICollection.IsSynchronized
		{
			get
			{
				return this._objects == null || this._objects.Keys.IsSynchronized;
			}
		}

		// Token: 0x170004DF RID: 1247
		// (get) Token: 0x06000E06 RID: 3590 RVA: 0x00025983 File Offset: 0x00023B83
		object ICollection.SyncRoot
		{
			get
			{
				if (this._objects == null)
				{
					return this;
				}
				return this._objects.Keys.SyncRoot;
			}
		}

		// Token: 0x06000E07 RID: 3591 RVA: 0x0002599F File Offset: 0x00023B9F
		public void CopyTo(Array array, int index)
		{
			if (this._objects != null)
			{
				this._objects.Keys.CopyTo(array, index);
			}
		}

		// Token: 0x040011BA RID: 4538
		private static ObjectSet.EmptyEnumerator _emptyEnumerator = new ObjectSet.EmptyEnumerator();

		// Token: 0x040011BB RID: 4539
		private IDictionary _objects;

		// Token: 0x02000120 RID: 288
		private class EmptyEnumerator : IEnumerator
		{
			// Token: 0x170004E0 RID: 1248
			// (get) Token: 0x06000E09 RID: 3593 RVA: 0x00003BF6 File Offset: 0x00001DF6
			public object Current
			{
				get
				{
					return null;
				}
			}

			// Token: 0x06000E0A RID: 3594 RVA: 0x00008A4D File Offset: 0x00006C4D
			public bool MoveNext()
			{
				return false;
			}

			// Token: 0x06000E0B RID: 3595 RVA: 0x00003946 File Offset: 0x00001B46
			public void Reset()
			{
			}
		}
	}
}
