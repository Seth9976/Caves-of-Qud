using System;
using System.Collections.Generic;
using Unity.Properties;
using UnityEngine.Bindings;

namespace UnityEngine.UIElements
{
	// Token: 0x020001BE RID: 446
	public readonly struct TreeViewItemData<T>
	{
		// Token: 0x1700024E RID: 590
		// (get) Token: 0x06000DD9 RID: 3545 RVA: 0x0003E073 File Offset: 0x0003C273
		public int id { get; }

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x06000DDA RID: 3546 RVA: 0x0003E07B File Offset: 0x0003C27B
		public T data
		{
			get
			{
				return this.m_Data;
			}
		}

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x06000DDB RID: 3547 RVA: 0x0003E083 File Offset: 0x0003C283
		public IEnumerable<TreeViewItemData<T>> children
		{
			get
			{
				return this.m_Children;
			}
		}

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x06000DDC RID: 3548 RVA: 0x0003E08B File Offset: 0x0003C28B
		public bool hasChildren
		{
			get
			{
				return this.m_Children != null && this.m_Children.Count > 0;
			}
		}

		// Token: 0x06000DDD RID: 3549 RVA: 0x0003E0A6 File Offset: 0x0003C2A6
		public TreeViewItemData(int id, T data, List<TreeViewItemData<T>> children = null)
		{
			this.id = id;
			this.m_Data = data;
			this.m_Children = children ?? new List<TreeViewItemData<T>>();
		}

		// Token: 0x06000DDE RID: 3550 RVA: 0x0003E0C7 File Offset: 0x0003C2C7
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal void AddChild(TreeViewItemData<T> child)
		{
			this.m_Children.Add(child);
		}

		// Token: 0x06000DDF RID: 3551 RVA: 0x0003E0D8 File Offset: 0x0003C2D8
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal void AddChildren(IList<TreeViewItemData<T>> children)
		{
			foreach (TreeViewItemData<T> treeViewItemData in children)
			{
				this.AddChild(treeViewItemData);
			}
		}

		// Token: 0x06000DE0 RID: 3552 RVA: 0x0003E124 File Offset: 0x0003C324
		internal void InsertChild(TreeViewItemData<T> child, int index)
		{
			bool flag = index < 0 || index >= this.m_Children.Count;
			if (flag)
			{
				this.m_Children.Add(child);
			}
			else
			{
				this.m_Children.Insert(index, child);
			}
		}

		// Token: 0x06000DE1 RID: 3553 RVA: 0x0003E16C File Offset: 0x0003C36C
		internal void RemoveChild(int childId)
		{
			bool flag = this.m_Children == null;
			if (!flag)
			{
				for (int i = 0; i < this.m_Children.Count; i++)
				{
					bool flag2 = childId == this.m_Children[i].id;
					if (flag2)
					{
						this.m_Children.RemoveAt(i);
						break;
					}
				}
			}
		}

		// Token: 0x06000DE2 RID: 3554 RVA: 0x0003E1D4 File Offset: 0x0003C3D4
		internal int GetChildIndex(int itemId)
		{
			int num = 0;
			foreach (TreeViewItemData<T> treeViewItemData in this.m_Children)
			{
				bool flag = treeViewItemData.id == itemId;
				if (flag)
				{
					return num;
				}
				num++;
			}
			return -1;
		}

		// Token: 0x06000DE3 RID: 3555 RVA: 0x0003E240 File Offset: 0x0003C440
		internal void ReplaceChild(TreeViewItemData<T> newChild)
		{
			bool flag = !this.hasChildren;
			if (!flag)
			{
				int num = 0;
				foreach (TreeViewItemData<T> treeViewItemData in this.m_Children)
				{
					bool flag2 = treeViewItemData.id == newChild.id;
					if (flag2)
					{
						this.m_Children.RemoveAt(num);
						this.m_Children.Insert(num, newChild);
						break;
					}
					num++;
				}
			}
		}

		// Token: 0x040008A9 RID: 2217
		[CreateProperty]
		private readonly T m_Data;

		// Token: 0x040008AA RID: 2218
		private readonly IList<TreeViewItemData<T>> m_Children;
	}
}
