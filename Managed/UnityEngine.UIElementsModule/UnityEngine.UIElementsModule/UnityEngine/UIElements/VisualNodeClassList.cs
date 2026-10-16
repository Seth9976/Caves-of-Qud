using System;
using System.Collections;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x020005E4 RID: 1508
	internal readonly struct VisualNodeClassList : IList<string>, ICollection<string>, IEnumerable<string>, IEnumerable
	{
		// Token: 0x17000B10 RID: 2832
		// (get) Token: 0x06002CA0 RID: 11424 RVA: 0x000A0707 File Offset: 0x0009E907
		public int Count
		{
			get
			{
				return this.m_Manager.GetProperty<VisualNodeClassData>(this.m_Handle).Count;
			}
		}

		// Token: 0x17000B11 RID: 2833
		public string this[int index]
		{
			get
			{
				ref VisualNodeClassData property = ref this.m_Manager.GetProperty<VisualNodeClassData>(this.m_Handle);
				return this.m_Manager.ClassNameStore.GetClassNameManaged(property[index]);
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x06002CA3 RID: 11427 RVA: 0x000A0762 File Offset: 0x0009E962
		public VisualNodeClassList(VisualManager store, VisualNodeHandle handle)
		{
			this.m_Manager = store;
			this.m_Handle = handle;
		}

		// Token: 0x06002CA4 RID: 11428 RVA: 0x000A0773 File Offset: 0x0009E973
		public void Add(string className)
		{
			this.m_Manager.AddToClassList(in this.m_Handle, className);
		}

		// Token: 0x06002CA5 RID: 11429 RVA: 0x000A0788 File Offset: 0x0009E988
		public bool Remove(string className)
		{
			return this.m_Manager.RemoveFromClassList(in this.m_Handle, className);
		}

		// Token: 0x06002CA6 RID: 11430 RVA: 0x000A079C File Offset: 0x0009E99C
		public bool Contains(string className)
		{
			return this.m_Manager.ClassListContains(in this.m_Handle, className);
		}

		// Token: 0x06002CA7 RID: 11431 RVA: 0x000A07B0 File Offset: 0x0009E9B0
		public void Clear()
		{
			this.m_Manager.ClearClassList(in this.m_Handle);
		}

		// Token: 0x17000B12 RID: 2834
		// (get) Token: 0x06002CA8 RID: 11432 RVA: 0x00011DCA File Offset: 0x0000FFCA
		bool ICollection<string>.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06002CA9 RID: 11433 RVA: 0x000A07C4 File Offset: 0x0009E9C4
		void ICollection<string>.CopyTo(string[] array, int arrayIndex)
		{
			int i = 0;
			int num = arrayIndex;
			while (i < this.Count)
			{
				array[num] = this[i];
				i++;
				num++;
			}
		}

		// Token: 0x06002CAA RID: 11434 RVA: 0x000A07F7 File Offset: 0x0009E9F7
		public VisualNodeClassList.Enumerator GetEnumerator()
		{
			return new VisualNodeClassList.Enumerator(this.m_Manager, this.m_Manager.GetProperty<VisualNodeClassData>(this.m_Handle));
		}

		// Token: 0x06002CAB RID: 11435 RVA: 0x000A0815 File Offset: 0x0009EA15
		IEnumerator<string> IEnumerable<string>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06002CAC RID: 11436 RVA: 0x000A0815 File Offset: 0x0009EA15
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06002CAD RID: 11437 RVA: 0x00026C86 File Offset: 0x00024E86
		int IList<string>.IndexOf(string item)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06002CAE RID: 11438 RVA: 0x00026C86 File Offset: 0x00024E86
		void IList<string>.Insert(int index, string item)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06002CAF RID: 11439 RVA: 0x00026C86 File Offset: 0x00024E86
		void IList<string>.RemoveAt(int index)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0400144C RID: 5196
		private readonly VisualManager m_Manager;

		// Token: 0x0400144D RID: 5197
		private readonly VisualNodeHandle m_Handle;

		// Token: 0x020005E5 RID: 1509
		public struct Enumerator : IEnumerator<string>, IEnumerator, IDisposable
		{
			// Token: 0x06002CB0 RID: 11440 RVA: 0x000A0822 File Offset: 0x0009EA22
			internal Enumerator(VisualManager manager, in VisualNodeClassData data)
			{
				this.m_Manager = manager;
				this.m_Data = data;
				this.m_Position = -1;
			}

			// Token: 0x06002CB1 RID: 11441 RVA: 0x000A0840 File Offset: 0x0009EA40
			public bool MoveNext()
			{
				int num = this.m_Position + 1;
				this.m_Position = num;
				return num < this.m_Data.Count;
			}

			// Token: 0x06002CB2 RID: 11442 RVA: 0x000A086B File Offset: 0x0009EA6B
			public void Reset()
			{
				this.m_Position = -1;
			}

			// Token: 0x17000B13 RID: 2835
			// (get) Token: 0x06002CB3 RID: 11443 RVA: 0x000A0874 File Offset: 0x0009EA74
			public string Current
			{
				get
				{
					return this.m_Manager.ClassNameStore.GetClassNameManaged(this.m_Data[this.m_Position]);
				}
			}

			// Token: 0x17000B14 RID: 2836
			// (get) Token: 0x06002CB4 RID: 11444 RVA: 0x000A0897 File Offset: 0x0009EA97
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06002CB5 RID: 11445 RVA: 0x00002152 File Offset: 0x00000352
			public void Dispose()
			{
			}

			// Token: 0x0400144E RID: 5198
			private readonly VisualManager m_Manager;

			// Token: 0x0400144F RID: 5199
			private readonly VisualNodeClassData m_Data;

			// Token: 0x04001450 RID: 5200
			private int m_Position;
		}
	}
}
