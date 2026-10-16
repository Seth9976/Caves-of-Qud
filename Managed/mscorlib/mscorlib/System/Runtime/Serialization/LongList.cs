using System;

namespace System.Runtime.Serialization
{
	// Token: 0x02000665 RID: 1637
	[Serializable]
	internal class LongList
	{
		// Token: 0x06003D3F RID: 15679 RVA: 0x000D43F2 File Offset: 0x000D25F2
		internal LongList()
			: this(2)
		{
		}

		// Token: 0x06003D40 RID: 15680 RVA: 0x000D43FB File Offset: 0x000D25FB
		internal LongList(int startingSize)
		{
			this.m_count = 0;
			this.m_totalItems = 0;
			this.m_values = new long[startingSize];
		}

		// Token: 0x06003D41 RID: 15681 RVA: 0x000D4420 File Offset: 0x000D2620
		internal void Add(long value)
		{
			if (this.m_totalItems == this.m_values.Length)
			{
				this.EnlargeArray();
			}
			long[] values = this.m_values;
			int totalItems = this.m_totalItems;
			this.m_totalItems = totalItems + 1;
			values[totalItems] = value;
			this.m_count++;
		}

		// Token: 0x17000965 RID: 2405
		// (get) Token: 0x06003D42 RID: 15682 RVA: 0x000D446A File Offset: 0x000D266A
		internal int Count
		{
			get
			{
				return this.m_count;
			}
		}

		// Token: 0x06003D43 RID: 15683 RVA: 0x000D4472 File Offset: 0x000D2672
		internal void StartEnumeration()
		{
			this.m_currentItem = -1;
		}

		// Token: 0x06003D44 RID: 15684 RVA: 0x000D447C File Offset: 0x000D267C
		internal bool MoveNext()
		{
			int num;
			do
			{
				num = this.m_currentItem + 1;
				this.m_currentItem = num;
			}
			while (num < this.m_totalItems && this.m_values[this.m_currentItem] == -1L);
			return this.m_currentItem != this.m_totalItems;
		}

		// Token: 0x17000966 RID: 2406
		// (get) Token: 0x06003D45 RID: 15685 RVA: 0x000D44C4 File Offset: 0x000D26C4
		internal long Current
		{
			get
			{
				return this.m_values[this.m_currentItem];
			}
		}

		// Token: 0x06003D46 RID: 15686 RVA: 0x000D44D4 File Offset: 0x000D26D4
		internal bool RemoveElement(long value)
		{
			int num = 0;
			while (num < this.m_totalItems && this.m_values[num] != value)
			{
				num++;
			}
			if (num == this.m_totalItems)
			{
				return false;
			}
			this.m_values[num] = -1L;
			return true;
		}

		// Token: 0x06003D47 RID: 15687 RVA: 0x000D4514 File Offset: 0x000D2714
		private void EnlargeArray()
		{
			int num = this.m_values.Length * 2;
			if (num < 0)
			{
				if (num == 2147483647)
				{
					throw new SerializationException(Environment.GetResourceString("The internal array cannot expand to greater than Int32.MaxValue elements."));
				}
				num = int.MaxValue;
			}
			long[] array = new long[num];
			Array.Copy(this.m_values, array, this.m_count);
			this.m_values = array;
		}

		// Token: 0x0400276C RID: 10092
		private const int InitialSize = 2;

		// Token: 0x0400276D RID: 10093
		private long[] m_values;

		// Token: 0x0400276E RID: 10094
		private int m_count;

		// Token: 0x0400276F RID: 10095
		private int m_totalItems;

		// Token: 0x04002770 RID: 10096
		private int m_currentItem;
	}
}
