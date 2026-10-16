using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;

namespace Roslyn.Utilities
{
	// Token: 0x02000037 RID: 55
	[NullableContext(1)]
	[Nullable(0)]
	internal class ConsList<[Nullable(2)] T> : IEnumerable<T>, IEnumerable
	{
		// Token: 0x06000101 RID: 257 RVA: 0x00004B16 File Offset: 0x00002D16
		private ConsList()
		{
			this._head = default(T);
			this._tail = null;
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00004B31 File Offset: 0x00002D31
		public ConsList(T head, ConsList<T> tail)
		{
			this._head = head;
			this._tail = tail;
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000103 RID: 259 RVA: 0x00004B47 File Offset: 0x00002D47
		[DebuggerBrowsable(0)]
		public T Head
		{
			get
			{
				return this._head;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000104 RID: 260 RVA: 0x00004B4F File Offset: 0x00002D4F
		[DebuggerBrowsable(0)]
		public ConsList<T> Tail
		{
			get
			{
				return this._tail;
			}
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00004B57 File Offset: 0x00002D57
		public bool Any()
		{
			return this != ConsList<T>.Empty;
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00004B64 File Offset: 0x00002D64
		public ConsList<T> Push(T value)
		{
			return new ConsList<T>(value, this);
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00004B6D File Offset: 0x00002D6D
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00004B7A File Offset: 0x00002D7A
		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00004B87 File Offset: 0x00002D87
		[NullableContext(0)]
		public ConsList<T>.Enumerator GetEnumerator()
		{
			return new ConsList<T>.Enumerator(this);
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00004B90 File Offset: 0x00002D90
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder("ConsList[");
			bool flag = false;
			ConsList<T> consList = this;
			while (consList._tail != null)
			{
				if (flag)
				{
					stringBuilder.Append(", ");
				}
				stringBuilder.Append(consList.Head);
				flag = true;
				consList = consList._tail;
			}
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}

		// Token: 0x0400003B RID: 59
		public static readonly ConsList<T> Empty = new ConsList<T>();

		// Token: 0x0400003C RID: 60
		[Nullable(2)]
		private readonly T _head;

		// Token: 0x0400003D RID: 61
		[Nullable(new byte[] { 2, 1 })]
		private readonly ConsList<T> _tail;

		// Token: 0x020004F3 RID: 1267
		[Nullable(0)]
		internal struct Enumerator : IEnumerator<T>, IEnumerator, IDisposable
		{
			// Token: 0x060032BC RID: 12988 RVA: 0x000900A0 File Offset: 0x0008E2A0
			internal Enumerator(ConsList<T> list)
			{
				this._current = default(T);
				this._tail = list;
			}

			// Token: 0x17000EA8 RID: 3752
			// (get) Token: 0x060032BD RID: 12989 RVA: 0x000900B5 File Offset: 0x0008E2B5
			public T Current
			{
				get
				{
					return this._current;
				}
			}

			// Token: 0x060032BE RID: 12990 RVA: 0x000900C0 File Offset: 0x0008E2C0
			public bool MoveNext()
			{
				ConsList<T> tail = this._tail;
				ConsList<T> tail2 = tail._tail;
				if (tail2 != null)
				{
					this._current = tail._head;
					this._tail = tail2;
					return true;
				}
				this._current = default(T);
				return false;
			}

			// Token: 0x060032BF RID: 12991 RVA: 0x00090100 File Offset: 0x0008E300
			public void Dispose()
			{
			}

			// Token: 0x17000EA9 RID: 3753
			// (get) Token: 0x060032C0 RID: 12992 RVA: 0x00090102 File Offset: 0x0008E302
			[Nullable(2)]
			object IEnumerator.Current
			{
				[NullableContext(2)]
				get
				{
					return this.Current;
				}
			}

			// Token: 0x060032C1 RID: 12993 RVA: 0x0009010F File Offset: 0x0008E30F
			public void Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x040013DF RID: 5087
			[Nullable(2)]
			private T _current;

			// Token: 0x040013E0 RID: 5088
			private ConsList<T> _tail;
		}
	}
}
