using System;

// Token: 0x020000F2 RID: 242
public class exList<T> where T : struct
{
	// Token: 0x06000942 RID: 2370 RVA: 0x0003AE1C File Offset: 0x0003901C
	public static exList<T> GetTempList()
	{
		if (exList<T>.tempList_ == null)
		{
			exList<T>.tempList_ = new exList<T>();
		}
		exList<T>.tempList_.Clear();
		return exList<T>.tempList_;
	}

	// Token: 0x1700012F RID: 303
	// (get) Token: 0x06000943 RID: 2371 RVA: 0x0003AE3E File Offset: 0x0003903E
	// (set) Token: 0x06000944 RID: 2372 RVA: 0x0003AE48 File Offset: 0x00039048
	public int Capacity
	{
		get
		{
			return this.buffer.Length;
		}
		set
		{
			if (value < this.Count)
			{
				throw new ArgumentOutOfRangeException();
			}
			Array.Resize<T>(ref this.buffer, value);
		}
	}

	// Token: 0x06000945 RID: 2373 RVA: 0x0003AE65 File Offset: 0x00039065
	public exList()
	{
		this.buffer = exList<T>.emptyArray;
	}

	// Token: 0x06000946 RID: 2374 RVA: 0x0003AE78 File Offset: 0x00039078
	public exList(int capacity)
	{
		if (capacity < 0)
		{
			throw new ArgumentOutOfRangeException("capacity");
		}
		this.buffer = new T[capacity];
	}

	// Token: 0x06000947 RID: 2375 RVA: 0x0003AE9C File Offset: 0x0003909C
	public void Add(T _item)
	{
		if (this.Count == this.buffer.Length)
		{
			this.GrowIfNeeded(1);
		}
		T[] array = this.buffer;
		int count = this.Count;
		this.Count = count + 1;
		array[count] = _item;
	}

	// Token: 0x06000948 RID: 2376 RVA: 0x0003AEE0 File Offset: 0x000390E0
	public void AddRange(int _count)
	{
		int num = this.Count + _count;
		if (num > this.buffer.Length)
		{
			this.Capacity = Math.Max(Math.Max(this.Capacity * 2, 4), num);
		}
		this.Count = num;
	}

	// Token: 0x06000949 RID: 2377 RVA: 0x0003AF22 File Offset: 0x00039122
	public void RemoveRange(int _index, int _count)
	{
		if (_count > 0)
		{
			this.Shift(_index, -_count);
		}
	}

	// Token: 0x0600094A RID: 2378 RVA: 0x0003AF31 File Offset: 0x00039131
	public void Clear()
	{
		this.Count = 0;
	}

	// Token: 0x0600094B RID: 2379 RVA: 0x0003AF3C File Offset: 0x0003913C
	public void TrimExcess()
	{
		if (this.Count > 0)
		{
			if (this.Count < this.buffer.Length)
			{
				T[] array = new T[this.Count];
				Array.Copy(this.buffer, array, this.Count);
				this.buffer = array;
				return;
			}
		}
		else
		{
			this.buffer = exList<T>.emptyArray;
		}
	}

	// Token: 0x0600094C RID: 2380 RVA: 0x0003AF93 File Offset: 0x00039193
	public T[] FastToArray()
	{
		this.TrimExcess();
		return this.buffer;
	}

	// Token: 0x0600094D RID: 2381 RVA: 0x0003AFA4 File Offset: 0x000391A4
	public T[] ToArray()
	{
		T[] array = new T[this.Count];
		Array.Copy(this.buffer, array, this.Count);
		return array;
	}

	// Token: 0x0600094E RID: 2382 RVA: 0x0003AFD0 File Offset: 0x000391D0
	public void FromArray(ref T[] _array)
	{
		this.buffer = _array;
		this.Count = _array.Length;
		_array = null;
	}

	// Token: 0x0600094F RID: 2383 RVA: 0x0003AFE8 File Offset: 0x000391E8
	private void GrowIfNeeded(int _newCount)
	{
		int num = this.Count + _newCount;
		if (num > this.buffer.Length)
		{
			this.Capacity = Math.Max(Math.Max(this.Capacity * 2, 4), num);
		}
	}

	// Token: 0x06000950 RID: 2384 RVA: 0x0003B024 File Offset: 0x00039224
	private void Shift(int start, int delta)
	{
		if (delta < 0)
		{
			start -= delta;
		}
		if (start < this.Count)
		{
			Array.Copy(this.buffer, start, this.buffer, start + delta, this.Count - start);
		}
		this.Count += delta;
		if (delta < 0)
		{
			Array.Clear(this.buffer, this.Count, -delta);
		}
	}

	// Token: 0x0400061E RID: 1566
	private static readonly T[] emptyArray = new T[0];

	// Token: 0x0400061F RID: 1567
	private static exList<T> tempList_;

	// Token: 0x04000620 RID: 1568
	public T[] buffer;

	// Token: 0x04000621 RID: 1569
	public int Count;
}
