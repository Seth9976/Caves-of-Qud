using System;
using System.Diagnostics;

namespace UnityEngine.TextCore.Text
{
	// Token: 0x0200006D RID: 109
	[DebuggerDisplay("Item count = {m_Count}")]
	internal struct TextProcessingStack<T>
	{
		// Token: 0x06000353 RID: 851 RVA: 0x00025A1F File Offset: 0x00023C1F
		public TextProcessingStack(T[] stack)
		{
			this.itemStack = stack;
			this.m_Capacity = stack.Length;
			this.index = 0;
			this.m_RolloverSize = 0;
			this.m_DefaultItem = default(T);
			this.m_Count = 0;
		}

		// Token: 0x06000354 RID: 852 RVA: 0x00025A53 File Offset: 0x00023C53
		public TextProcessingStack(int capacity)
		{
			this.itemStack = new T[capacity];
			this.m_Capacity = capacity;
			this.index = 0;
			this.m_RolloverSize = 0;
			this.m_DefaultItem = default(T);
			this.m_Count = 0;
		}

		// Token: 0x06000355 RID: 853 RVA: 0x00025A8A File Offset: 0x00023C8A
		public TextProcessingStack(int capacity, int rolloverSize)
		{
			this.itemStack = new T[capacity];
			this.m_Capacity = capacity;
			this.index = 0;
			this.m_RolloverSize = rolloverSize;
			this.m_DefaultItem = default(T);
			this.m_Count = 0;
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000356 RID: 854 RVA: 0x00025AC4 File Offset: 0x00023CC4
		public int Count
		{
			get
			{
				return this.m_Count;
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000357 RID: 855 RVA: 0x00025ADC File Offset: 0x00023CDC
		public T current
		{
			get
			{
				bool flag = this.index > 0;
				T t;
				if (flag)
				{
					t = this.itemStack[this.index - 1];
				}
				else
				{
					t = this.itemStack[0];
				}
				return t;
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000358 RID: 856 RVA: 0x00025B20 File Offset: 0x00023D20
		// (set) Token: 0x06000359 RID: 857 RVA: 0x00025B38 File Offset: 0x00023D38
		public int rolloverSize
		{
			get
			{
				return this.m_RolloverSize;
			}
			set
			{
				this.m_RolloverSize = value;
			}
		}

		// Token: 0x0600035A RID: 858 RVA: 0x00025B44 File Offset: 0x00023D44
		internal static void SetDefault(TextProcessingStack<T>[] stack, T item)
		{
			for (int i = 0; i < stack.Length; i++)
			{
				stack[i].SetDefault(item);
			}
		}

		// Token: 0x0600035B RID: 859 RVA: 0x00025B72 File Offset: 0x00023D72
		public void Clear()
		{
			this.index = 0;
			this.m_Count = 0;
		}

		// Token: 0x0600035C RID: 860 RVA: 0x00025B84 File Offset: 0x00023D84
		public void SetDefault(T item)
		{
			bool flag = this.itemStack == null;
			if (flag)
			{
				this.m_Capacity = 4;
				this.itemStack = new T[this.m_Capacity];
				this.m_DefaultItem = default(T);
			}
			this.itemStack[0] = item;
			this.index = 1;
			this.m_Count = 1;
		}

		// Token: 0x0600035D RID: 861 RVA: 0x00025BE0 File Offset: 0x00023DE0
		public void Add(T item)
		{
			bool flag = this.index < this.itemStack.Length;
			if (flag)
			{
				this.itemStack[this.index] = item;
				this.index++;
			}
		}

		// Token: 0x0600035E RID: 862 RVA: 0x00025C24 File Offset: 0x00023E24
		public T Remove()
		{
			this.index--;
			this.m_Count--;
			bool flag = this.index <= 0;
			T t;
			if (flag)
			{
				this.m_Count = 0;
				this.index = 1;
				t = this.itemStack[0];
			}
			else
			{
				t = this.itemStack[this.index - 1];
			}
			return t;
		}

		// Token: 0x0600035F RID: 863 RVA: 0x00025C94 File Offset: 0x00023E94
		public void Push(T item)
		{
			bool flag = this.index == this.m_Capacity;
			if (flag)
			{
				this.m_Capacity *= 2;
				bool flag2 = this.m_Capacity == 0;
				if (flag2)
				{
					this.m_Capacity = 4;
				}
				Array.Resize<T>(ref this.itemStack, this.m_Capacity);
			}
			this.itemStack[this.index] = item;
			bool flag3 = this.m_RolloverSize == 0;
			if (flag3)
			{
				this.index++;
				this.m_Count++;
			}
			else
			{
				this.index = (this.index + 1) % this.m_RolloverSize;
				this.m_Count = ((this.m_Count < this.m_RolloverSize) ? (this.m_Count + 1) : this.m_RolloverSize);
			}
		}

		// Token: 0x06000360 RID: 864 RVA: 0x00025D64 File Offset: 0x00023F64
		public T Pop()
		{
			bool flag = this.index == 0 && this.m_RolloverSize == 0;
			T t;
			if (flag)
			{
				t = default(T);
			}
			else
			{
				bool flag2 = this.m_RolloverSize == 0;
				if (flag2)
				{
					this.index--;
				}
				else
				{
					this.index = (this.index - 1) % this.m_RolloverSize;
					this.index = ((this.index < 0) ? (this.index + this.m_RolloverSize) : this.index);
				}
				T t2 = this.itemStack[this.index];
				this.itemStack[this.index] = this.m_DefaultItem;
				this.m_Count = ((this.m_Count > 0) ? (this.m_Count - 1) : 0);
				t = t2;
			}
			return t;
		}

		// Token: 0x06000361 RID: 865 RVA: 0x00025E3C File Offset: 0x0002403C
		public T Peek()
		{
			bool flag = this.index == 0;
			T t;
			if (flag)
			{
				t = this.m_DefaultItem;
			}
			else
			{
				t = this.itemStack[this.index - 1];
			}
			return t;
		}

		// Token: 0x06000362 RID: 866 RVA: 0x00025E78 File Offset: 0x00024078
		public T CurrentItem()
		{
			bool flag = this.index > 0;
			T t;
			if (flag)
			{
				t = this.itemStack[this.index - 1];
			}
			else
			{
				t = this.itemStack[0];
			}
			return t;
		}

		// Token: 0x06000363 RID: 867 RVA: 0x00025EBC File Offset: 0x000240BC
		public T PreviousItem()
		{
			bool flag = this.index > 1;
			T t;
			if (flag)
			{
				t = this.itemStack[this.index - 2];
			}
			else
			{
				t = this.itemStack[0];
			}
			return t;
		}

		// Token: 0x04000462 RID: 1122
		public T[] itemStack;

		// Token: 0x04000463 RID: 1123
		public int index;

		// Token: 0x04000464 RID: 1124
		private T m_DefaultItem;

		// Token: 0x04000465 RID: 1125
		private int m_Capacity;

		// Token: 0x04000466 RID: 1126
		private int m_RolloverSize;

		// Token: 0x04000467 RID: 1127
		private int m_Count;

		// Token: 0x04000468 RID: 1128
		private const int k_DefaultCapacity = 4;
	}
}
