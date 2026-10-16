using System;
using UnityEngine;

namespace EnhancedUI
{
	// Token: 0x020001D5 RID: 469
	public class SmallList<T>
	{
		// Token: 0x170001C3 RID: 451
		public T this[int i]
		{
			get
			{
				return this.data[i];
			}
			set
			{
				this.data[i] = value;
			}
		}

		// Token: 0x060010B5 RID: 4277 RVA: 0x000660EC File Offset: 0x000642EC
		private void ResizeArray()
		{
			T[] array;
			if (this.data != null)
			{
				array = new T[Mathf.Max(this.data.Length << 1, 64)];
			}
			else
			{
				array = new T[64];
			}
			if (this.data != null && this.Count > 0)
			{
				this.data.CopyTo(array, 0);
			}
			this.data = array;
		}

		// Token: 0x060010B6 RID: 4278 RVA: 0x00066147 File Offset: 0x00064347
		public void Clear()
		{
			this.Count = 0;
		}

		// Token: 0x060010B7 RID: 4279 RVA: 0x00066150 File Offset: 0x00064350
		public T First()
		{
			if (this.data == null || this.Count == 0)
			{
				return default(T);
			}
			return this.data[0];
		}

		// Token: 0x060010B8 RID: 4280 RVA: 0x00066184 File Offset: 0x00064384
		public T Last()
		{
			if (this.data == null || this.Count == 0)
			{
				return default(T);
			}
			return this.data[this.Count - 1];
		}

		// Token: 0x060010B9 RID: 4281 RVA: 0x000661BE File Offset: 0x000643BE
		public void Add(T item)
		{
			if (this.data == null || this.Count == this.data.Length)
			{
				this.ResizeArray();
			}
			this.data[this.Count] = item;
			this.Count++;
		}

		// Token: 0x060010BA RID: 4282 RVA: 0x000661FE File Offset: 0x000643FE
		public void AddStart(T item)
		{
			this.Insert(item, 0);
		}

		// Token: 0x060010BB RID: 4283 RVA: 0x00066208 File Offset: 0x00064408
		public void Insert(T item, int index)
		{
			if (this.data == null || this.Count == this.data.Length)
			{
				this.ResizeArray();
			}
			for (int i = this.Count; i > index; i--)
			{
				this.data[i] = this.data[i - 1];
			}
			this.data[index] = item;
			this.Count++;
		}

		// Token: 0x060010BC RID: 4284 RVA: 0x00066279 File Offset: 0x00064479
		public T RemoveStart()
		{
			return this.RemoveAt(0);
		}

		// Token: 0x060010BD RID: 4285 RVA: 0x00066284 File Offset: 0x00064484
		public T RemoveAt(int index)
		{
			if (this.data != null && this.Count != 0)
			{
				T t = this.data[index];
				for (int i = index; i < this.Count - 1; i++)
				{
					this.data[i] = this.data[i + 1];
				}
				this.Count--;
				this.data[this.Count] = default(T);
				return t;
			}
			return default(T);
		}

		// Token: 0x060010BE RID: 4286 RVA: 0x00066310 File Offset: 0x00064510
		public T Remove(T item)
		{
			if (this.data != null && this.Count != 0)
			{
				for (int i = 0; i < this.Count; i++)
				{
					if (this.data[i].Equals(item))
					{
						return this.RemoveAt(i);
					}
				}
			}
			return default(T);
		}

		// Token: 0x060010BF RID: 4287 RVA: 0x00066370 File Offset: 0x00064570
		public T RemoveEnd()
		{
			if (this.data != null && this.Count != 0)
			{
				this.Count--;
				T t = this.data[this.Count];
				this.data[this.Count] = default(T);
				return t;
			}
			return default(T);
		}

		// Token: 0x060010C0 RID: 4288 RVA: 0x000663D0 File Offset: 0x000645D0
		public bool Contains(T item)
		{
			if (this.data == null)
			{
				return false;
			}
			for (int i = 0; i < this.Count; i++)
			{
				if (this.data[i].Equals(item))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x04000B20 RID: 2848
		public T[] data;

		// Token: 0x04000B21 RID: 2849
		public int Count;
	}
}
