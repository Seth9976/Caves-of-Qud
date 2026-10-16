using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Wintellect.PowerCollections
{
	// Token: 0x020002F4 RID: 756
	[DebuggerDisplay("{DebuggerDisplayString()}")]
	[Serializable]
	public abstract class CollectionBase<T> : ICollection<T>, IEnumerable<T>, IEnumerable, ICollection
	{
		// Token: 0x06001A8E RID: 6798 RVA: 0x000862BD File Offset: 0x000844BD
		public override string ToString()
		{
			return Algorithms.ToString<T>(this);
		}

		// Token: 0x06001A8F RID: 6799 RVA: 0x000862C5 File Offset: 0x000844C5
		public virtual void Add(T item)
		{
			throw new NotImplementedException(Strings.MustOverrideOrReimplement);
		}

		// Token: 0x06001A90 RID: 6800
		public abstract void Clear();

		// Token: 0x06001A91 RID: 6801
		public abstract bool Remove(T item);

		// Token: 0x06001A92 RID: 6802 RVA: 0x000862D4 File Offset: 0x000844D4
		public virtual bool Contains(T item)
		{
			IEqualityComparer<T> @default = EqualityComparer<T>.Default;
			foreach (T t in this)
			{
				if (@default.Equals(t, item))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001A93 RID: 6803 RVA: 0x0008632C File Offset: 0x0008452C
		public virtual void CopyTo(T[] array, int arrayIndex)
		{
			int count = this.Count;
			if (count == 0)
			{
				return;
			}
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", count, Strings.ArgMustNotBeNegative);
			}
			if (arrayIndex < 0)
			{
				throw new ArgumentOutOfRangeException("arrayIndex", arrayIndex, Strings.ArgMustNotBeNegative);
			}
			if (arrayIndex >= array.Length || count > array.Length - arrayIndex)
			{
				throw new ArgumentException("arrayIndex", Strings.ArrayTooSmall);
			}
			int num = arrayIndex;
			int num2 = 0;
			foreach (T t in ((IEnumerable<T>)this))
			{
				if (num2 >= count)
				{
					break;
				}
				array[num] = t;
				num++;
				num2++;
			}
		}

		// Token: 0x06001A94 RID: 6804 RVA: 0x000863F4 File Offset: 0x000845F4
		public virtual T[] ToArray()
		{
			T[] array = new T[this.Count];
			this.CopyTo(array, 0);
			return array;
		}

		// Token: 0x1700038A RID: 906
		// (get) Token: 0x06001A95 RID: 6805
		public abstract int Count { get; }

		// Token: 0x1700038B RID: 907
		// (get) Token: 0x06001A96 RID: 6806 RVA: 0x00086416 File Offset: 0x00084616
		bool ICollection<T>.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001A97 RID: 6807 RVA: 0x00086419 File Offset: 0x00084619
		public virtual ICollection<T> AsReadOnly()
		{
			return Algorithms.ReadOnly<T>(this);
		}

		// Token: 0x06001A98 RID: 6808 RVA: 0x00086421 File Offset: 0x00084621
		public virtual bool Exists(Predicate<T> predicate)
		{
			if (predicate == null)
			{
				throw new ArgumentNullException("predicate");
			}
			return Algorithms.Exists<T>(this, predicate);
		}

		// Token: 0x06001A99 RID: 6809 RVA: 0x00086438 File Offset: 0x00084638
		public virtual bool TrueForAll(Predicate<T> predicate)
		{
			if (predicate == null)
			{
				throw new ArgumentNullException("predicate");
			}
			return Algorithms.TrueForAll<T>(this, predicate);
		}

		// Token: 0x06001A9A RID: 6810 RVA: 0x0008644F File Offset: 0x0008464F
		public virtual int CountWhere(Predicate<T> predicate)
		{
			if (predicate == null)
			{
				throw new ArgumentNullException("predicate");
			}
			return Algorithms.CountWhere<T>(this, predicate);
		}

		// Token: 0x06001A9B RID: 6811 RVA: 0x00086466 File Offset: 0x00084666
		public virtual IEnumerable<T> FindAll(Predicate<T> predicate)
		{
			if (predicate == null)
			{
				throw new ArgumentNullException("predicate");
			}
			return Algorithms.FindWhere<T>(this, predicate);
		}

		// Token: 0x06001A9C RID: 6812 RVA: 0x0008647D File Offset: 0x0008467D
		public virtual ICollection<T> RemoveAll(Predicate<T> predicate)
		{
			if (predicate == null)
			{
				throw new ArgumentNullException("predicate");
			}
			return Algorithms.RemoveWhere<T>(this, predicate);
		}

		// Token: 0x06001A9D RID: 6813 RVA: 0x00086494 File Offset: 0x00084694
		public virtual void ForEach(Action<T> action)
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			Algorithms.ForEach<T>(this, action);
		}

		// Token: 0x06001A9E RID: 6814 RVA: 0x000864AB File Offset: 0x000846AB
		public virtual IEnumerable<TOutput> ConvertAll<TOutput>(Converter<T, TOutput> converter)
		{
			if (converter == null)
			{
				throw new ArgumentNullException("converter");
			}
			return Algorithms.Convert<T, TOutput>(this, converter);
		}

		// Token: 0x06001A9F RID: 6815
		public abstract IEnumerator<T> GetEnumerator();

		// Token: 0x06001AA0 RID: 6816 RVA: 0x000864C4 File Offset: 0x000846C4
		void ICollection.CopyTo(Array array, int index)
		{
			int count = this.Count;
			if (count == 0)
			{
				return;
			}
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", index, Strings.ArgMustNotBeNegative);
			}
			if (index >= array.Length || count > array.Length - index)
			{
				throw new ArgumentException("index", Strings.ArrayTooSmall);
			}
			int num = 0;
			foreach (object obj in ((IEnumerable)this))
			{
				if (num >= count)
				{
					break;
				}
				array.SetValue(obj, index);
				index++;
				num++;
			}
		}

		// Token: 0x1700038C RID: 908
		// (get) Token: 0x06001AA1 RID: 6817 RVA: 0x00086580 File Offset: 0x00084780
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700038D RID: 909
		// (get) Token: 0x06001AA2 RID: 6818 RVA: 0x00086583 File Offset: 0x00084783
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x06001AA3 RID: 6819 RVA: 0x00086586 File Offset: 0x00084786
		IEnumerator IEnumerable.GetEnumerator()
		{
			foreach (T t in this)
			{
				yield return t;
			}
			IEnumerator<T> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06001AA4 RID: 6820 RVA: 0x00086598 File Offset: 0x00084798
		internal string DebuggerDisplayString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append('{');
			bool flag = true;
			foreach (T t in this)
			{
				if (stringBuilder.Length >= 250)
				{
					stringBuilder.Append(",...");
					break;
				}
				if (!flag)
				{
					stringBuilder.Append(',');
				}
				if (t == null)
				{
					stringBuilder.Append("null");
				}
				else
				{
					stringBuilder.Append(t.ToString());
				}
				flag = false;
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}
	}
}
