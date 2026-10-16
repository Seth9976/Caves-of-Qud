using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Wintellect.PowerCollections
{
	// Token: 0x02000301 RID: 769
	[DebuggerDisplay("{DebuggerDisplayString()}")]
	[Serializable]
	public abstract class ReadOnlyCollectionBase<T> : ICollection<T>, IEnumerable<T>, IEnumerable, ICollection
	{
		// Token: 0x06001C1D RID: 7197 RVA: 0x0008B623 File Offset: 0x00089823
		private void MethodModifiesCollection()
		{
			throw new NotSupportedException(string.Format(Strings.CannotModifyCollection, Util.SimpleClassName(base.GetType())));
		}

		// Token: 0x06001C1E RID: 7198 RVA: 0x0008B63F File Offset: 0x0008983F
		public override string ToString()
		{
			return Algorithms.ToString<T>(this);
		}

		// Token: 0x06001C1F RID: 7199 RVA: 0x0008B647 File Offset: 0x00089847
		public virtual bool Exists(Predicate<T> predicate)
		{
			if (predicate == null)
			{
				throw new ArgumentNullException("predicate");
			}
			return Algorithms.Exists<T>(this, predicate);
		}

		// Token: 0x06001C20 RID: 7200 RVA: 0x0008B65E File Offset: 0x0008985E
		public virtual bool TrueForAll(Predicate<T> predicate)
		{
			if (predicate == null)
			{
				throw new ArgumentNullException("predicate");
			}
			return Algorithms.TrueForAll<T>(this, predicate);
		}

		// Token: 0x06001C21 RID: 7201 RVA: 0x0008B675 File Offset: 0x00089875
		public virtual int CountWhere(Predicate<T> predicate)
		{
			if (predicate == null)
			{
				throw new ArgumentNullException("predicate");
			}
			return Algorithms.CountWhere<T>(this, predicate);
		}

		// Token: 0x06001C22 RID: 7202 RVA: 0x0008B68C File Offset: 0x0008988C
		public IEnumerable<T> FindAll(Predicate<T> predicate)
		{
			if (predicate == null)
			{
				throw new ArgumentNullException("predicate");
			}
			return Algorithms.FindWhere<T>(this, predicate);
		}

		// Token: 0x06001C23 RID: 7203 RVA: 0x0008B6A3 File Offset: 0x000898A3
		public virtual void ForEach(Action<T> action)
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			Algorithms.ForEach<T>(this, action);
		}

		// Token: 0x06001C24 RID: 7204 RVA: 0x0008B6BA File Offset: 0x000898BA
		public virtual IEnumerable<TOutput> ConvertAll<TOutput>(Converter<T, TOutput> converter)
		{
			if (converter == null)
			{
				throw new ArgumentNullException("converter");
			}
			return Algorithms.Convert<T, TOutput>(this, converter);
		}

		// Token: 0x06001C25 RID: 7205 RVA: 0x0008B6D1 File Offset: 0x000898D1
		void ICollection<T>.Add(T item)
		{
			this.MethodModifiesCollection();
		}

		// Token: 0x06001C26 RID: 7206 RVA: 0x0008B6D9 File Offset: 0x000898D9
		void ICollection<T>.Clear()
		{
			this.MethodModifiesCollection();
		}

		// Token: 0x06001C27 RID: 7207 RVA: 0x0008B6E1 File Offset: 0x000898E1
		bool ICollection<T>.Remove(T item)
		{
			this.MethodModifiesCollection();
			return false;
		}

		// Token: 0x06001C28 RID: 7208 RVA: 0x0008B6EC File Offset: 0x000898EC
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

		// Token: 0x06001C29 RID: 7209 RVA: 0x0008B744 File Offset: 0x00089944
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

		// Token: 0x06001C2A RID: 7210 RVA: 0x0008B80C File Offset: 0x00089A0C
		public virtual T[] ToArray()
		{
			T[] array = new T[this.Count];
			this.CopyTo(array, 0);
			return array;
		}

		// Token: 0x170003B8 RID: 952
		// (get) Token: 0x06001C2B RID: 7211
		public abstract int Count { get; }

		// Token: 0x170003B9 RID: 953
		// (get) Token: 0x06001C2C RID: 7212 RVA: 0x0008B82E File Offset: 0x00089A2E
		bool ICollection<T>.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06001C2D RID: 7213
		public abstract IEnumerator<T> GetEnumerator();

		// Token: 0x06001C2E RID: 7214 RVA: 0x0008B834 File Offset: 0x00089A34
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
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", count, Strings.ArgMustNotBeNegative);
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

		// Token: 0x170003BA RID: 954
		// (get) Token: 0x06001C2F RID: 7215 RVA: 0x0008B90C File Offset: 0x00089B0C
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170003BB RID: 955
		// (get) Token: 0x06001C30 RID: 7216 RVA: 0x0008B90F File Offset: 0x00089B0F
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x06001C31 RID: 7217 RVA: 0x0008B912 File Offset: 0x00089B12
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

		// Token: 0x06001C32 RID: 7218 RVA: 0x0008B924 File Offset: 0x00089B24
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
