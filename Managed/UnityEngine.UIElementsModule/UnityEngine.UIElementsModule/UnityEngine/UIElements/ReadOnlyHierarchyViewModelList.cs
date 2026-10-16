using System;
using System.Collections;
using Unity.Hierarchy;

namespace UnityEngine.UIElements
{
	// Token: 0x0200005A RID: 90
	internal class ReadOnlyHierarchyViewModelList : IList, ICollection, IEnumerable
	{
		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000309 RID: 777 RVA: 0x0000E724 File Offset: 0x0000C924
		public bool IsFixedSize
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x0600030A RID: 778 RVA: 0x0000E724 File Offset: 0x0000C924
		public bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x0600030B RID: 779 RVA: 0x0000E727 File Offset: 0x0000C927
		public int Count
		{
			get
			{
				return this.m_HierarchyViewModel.Count;
			}
		}

		// Token: 0x0600030C RID: 780 RVA: 0x0000E734 File Offset: 0x0000C934
		public bool Contains(object value)
		{
			bool flag;
			if (value is HierarchyNode)
			{
				HierarchyNode hierarchyNode = (HierarchyNode)value;
				flag = this.m_HierarchyViewModel.Contains(in hierarchyNode);
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x0600030D RID: 781 RVA: 0x0000E760 File Offset: 0x0000C960
		public int IndexOf(object value)
		{
			int num;
			if (value is HierarchyNode)
			{
				HierarchyNode hierarchyNode = (HierarchyNode)value;
				num = this.m_HierarchyViewModel.IndexOf(in hierarchyNode);
			}
			else
			{
				num = BaseTreeView.invalidId;
			}
			return num;
		}

		// Token: 0x0600030E RID: 782 RVA: 0x0000E792 File Offset: 0x0000C992
		public ReadOnlyHierarchyViewModelList(HierarchyViewModel viewModel)
		{
			this.m_HierarchyViewModel = viewModel;
		}

		// Token: 0x1700006D RID: 109
		public unsafe object this[int index]
		{
			get
			{
				return *this.m_HierarchyViewModel[index];
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06000311 RID: 785 RVA: 0x0000E7BC File Offset: 0x0000C9BC
		public unsafe void CopyTo(Array array, int index)
		{
			for (int i = index; i < this.m_HierarchyViewModel.Count; i++)
			{
				array.SetValue(*this.m_HierarchyViewModel[i], i - index);
			}
		}

		// Token: 0x06000312 RID: 786 RVA: 0x0000E804 File Offset: 0x0000CA04
		public IEnumerator GetEnumerator()
		{
			return new ReadOnlyHierarchyViewModelList.Enumerator(this.m_HierarchyViewModel);
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000313 RID: 787 RVA: 0x0000A6E3 File Offset: 0x000088E3
		public bool IsSynchronized
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000314 RID: 788 RVA: 0x0000A6E3 File Offset: 0x000088E3
		public object SyncRoot
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06000315 RID: 789 RVA: 0x0000A6E3 File Offset: 0x000088E3
		public int Add(object value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000316 RID: 790 RVA: 0x0000A6E3 File Offset: 0x000088E3
		public void Clear()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000317 RID: 791 RVA: 0x0000A6E3 File Offset: 0x000088E3
		public void Insert(int index, object value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000318 RID: 792 RVA: 0x0000A6E3 File Offset: 0x000088E3
		public void Remove(object value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000319 RID: 793 RVA: 0x0000A6E3 File Offset: 0x000088E3
		public void RemoveAt(int index)
		{
			throw new NotSupportedException();
		}

		// Token: 0x040001B6 RID: 438
		private readonly HierarchyViewModel m_HierarchyViewModel;

		// Token: 0x0200005B RID: 91
		private struct Enumerator : IEnumerator
		{
			// Token: 0x0600031A RID: 794 RVA: 0x0000E816 File Offset: 0x0000CA16
			public Enumerator(HierarchyViewModel hierarchyViewModel)
			{
				this.m_HierarchyViewModel = hierarchyViewModel;
				this.m_Enumerator = hierarchyViewModel.GetEnumerator();
			}

			// Token: 0x17000070 RID: 112
			// (get) Token: 0x0600031B RID: 795 RVA: 0x0000E82C File Offset: 0x0000CA2C
			public unsafe object Current
			{
				get
				{
					return *this.m_Enumerator.Current;
				}
			}

			// Token: 0x0600031C RID: 796 RVA: 0x0000E843 File Offset: 0x0000CA43
			public bool MoveNext()
			{
				return this.m_Enumerator.MoveNext();
			}

			// Token: 0x0600031D RID: 797 RVA: 0x0000E850 File Offset: 0x0000CA50
			public void Reset()
			{
				this.m_Enumerator = this.m_HierarchyViewModel.GetEnumerator();
			}

			// Token: 0x040001B7 RID: 439
			private readonly HierarchyViewModel m_HierarchyViewModel;

			// Token: 0x040001B8 RID: 440
			private HierarchyViewModel.Enumerator m_Enumerator;
		}
	}
}
