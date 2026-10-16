using System;

namespace UnityEngine.TextCore.Text
{
	// Token: 0x02000055 RID: 85
	internal struct TextBackingContainer
	{
		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000249 RID: 585 RVA: 0x0001FBF0 File Offset: 0x0001DDF0
		public uint[] Text
		{
			get
			{
				return this.m_Array;
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x0600024A RID: 586 RVA: 0x0001FC08 File Offset: 0x0001DE08
		public int Capacity
		{
			get
			{
				return this.m_Array.Length;
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x0600024B RID: 587 RVA: 0x0001FC24 File Offset: 0x0001DE24
		// (set) Token: 0x0600024C RID: 588 RVA: 0x0001FC3C File Offset: 0x0001DE3C
		public int Count
		{
			get
			{
				return this.m_Count;
			}
			set
			{
				this.m_Count = value;
			}
		}

		// Token: 0x17000069 RID: 105
		public uint this[int index]
		{
			get
			{
				return this.m_Array[index];
			}
			set
			{
				bool flag = index >= this.m_Array.Length;
				if (flag)
				{
					this.Resize(index);
				}
				this.m_Array[index] = value;
			}
		}

		// Token: 0x0600024F RID: 591 RVA: 0x0001FC95 File Offset: 0x0001DE95
		public TextBackingContainer(int size)
		{
			this.m_Array = new uint[size];
			this.m_Count = 0;
		}

		// Token: 0x06000250 RID: 592 RVA: 0x0001FCAB File Offset: 0x0001DEAB
		public void Resize(int size)
		{
			size = Mathf.NextPowerOfTwo(size + 1);
			Array.Resize<uint>(ref this.m_Array, size);
		}

		// Token: 0x04000306 RID: 774
		private uint[] m_Array;

		// Token: 0x04000307 RID: 775
		private int m_Count;
	}
}
