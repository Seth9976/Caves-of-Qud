using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using Unity.Properties;
using UnityEngine.Pool;

namespace UnityEngine.UIElements
{
	// Token: 0x02000058 RID: 88
	public abstract class BaseListViewController : CollectionViewController
	{
		// Token: 0x14000006 RID: 6
		// (add) Token: 0x060002E4 RID: 740 RVA: 0x0000DAE4 File Offset: 0x0000BCE4
		// (remove) Token: 0x060002E5 RID: 741 RVA: 0x0000DB1C File Offset: 0x0000BD1C
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action itemsSourceSizeChanged;

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x060002E6 RID: 742 RVA: 0x0000DB54 File Offset: 0x0000BD54
		// (remove) Token: 0x060002E7 RID: 743 RVA: 0x0000DB8C File Offset: 0x0000BD8C
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action<IEnumerable<int>> itemsAdded;

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x060002E8 RID: 744 RVA: 0x0000DBC4 File Offset: 0x0000BDC4
		// (remove) Token: 0x060002E9 RID: 745 RVA: 0x0000DBFC File Offset: 0x0000BDFC
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action<IEnumerable<int>> itemsRemoved;

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060002EA RID: 746 RVA: 0x0000DC31 File Offset: 0x0000BE31
		protected BaseListView baseListView
		{
			get
			{
				return base.view as BaseListView;
			}
		}

		// Token: 0x060002EB RID: 747 RVA: 0x0000DC40 File Offset: 0x0000BE40
		internal override void InvokeMakeItem(ReusableCollectionItem reusableItem)
		{
			ReusableListViewItem reusableListViewItem = reusableItem as ReusableListViewItem;
			bool flag = reusableListViewItem != null;
			if (flag)
			{
				reusableListViewItem.Init(this.MakeItem(), this.baseListView.reorderable && this.baseListView.reorderMode == ListViewReorderMode.Animated);
				this.PostInitRegistration(reusableListViewItem);
			}
		}

		// Token: 0x060002EC RID: 748 RVA: 0x0000DC94 File Offset: 0x0000BE94
		internal void PostInitRegistration(ReusableListViewItem listItem)
		{
			listItem.bindableElement.style.position = Position.Relative;
			listItem.bindableElement.style.flexBasis = StyleKeyword.Initial;
			listItem.bindableElement.style.marginTop = 0f;
			listItem.bindableElement.style.marginBottom = 0f;
			listItem.bindableElement.style.paddingTop = 0f;
			listItem.bindableElement.style.flexGrow = 0f;
			listItem.bindableElement.style.flexShrink = 0f;
		}

		// Token: 0x060002ED RID: 749 RVA: 0x0000DD58 File Offset: 0x0000BF58
		internal override void SetBindingContext(ReusableCollectionItem reusableItem, int index)
		{
			base.SetBindingContext(reusableItem, index);
			bool autoAssignSource = this.baseListView.autoAssignSource;
			if (autoAssignSource)
			{
				reusableItem.rootElement.dataSource = this.itemsSource;
				reusableItem.rootElement.dataSourcePath = PropertyPath.FromIndex(this.GetIdForIndex(index));
			}
		}

		// Token: 0x060002EE RID: 750 RVA: 0x0000DDAC File Offset: 0x0000BFAC
		internal override void InvokeBindItem(ReusableCollectionItem reusableItem, int index)
		{
			ReusableListViewItem reusableListViewItem = reusableItem as ReusableListViewItem;
			bool flag = reusableListViewItem != null;
			if (flag)
			{
				bool flag2 = this.baseListView.reorderable && this.baseListView.reorderMode == ListViewReorderMode.Animated;
				reusableListViewItem.UpdateDragHandle(flag2 && this.NeedsDragHandle(index));
			}
			base.InvokeBindItem(reusableItem, index);
		}

		// Token: 0x060002EF RID: 751 RVA: 0x0000DE08 File Offset: 0x0000C008
		public virtual bool NeedsDragHandle(int index)
		{
			return true;
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x0000DE1C File Offset: 0x0000C01C
		public virtual void AddItems(int itemCount)
		{
			bool flag = itemCount <= 0;
			if (!flag)
			{
				this.EnsureItemSourceCanBeResized();
				int itemsCount = this.GetItemsCount();
				List<int> list = CollectionPool<List<int>, int>.Get();
				try
				{
					bool isFixedSize = this.itemsSource.IsFixedSize;
					if (isFixedSize)
					{
						this.itemsSource = BaseListViewController.AddToArray((Array)this.itemsSource, itemCount);
						for (int i = 0; i < itemCount; i++)
						{
							list.Add(itemsCount + i);
						}
					}
					else
					{
						Type type = this.itemsSource.GetType();
						Type type2 = type.GetInterfaces().FirstOrDefault<Type>(new Func<Type, bool>(BaseListViewController.<AddItems>g__IsGenericList|19_0));
						bool flag2 = type2 != null && type2.GetGenericArguments()[0].IsValueType;
						if (flag2)
						{
							Type type3 = type2.GetGenericArguments()[0];
							for (int j = 0; j < itemCount; j++)
							{
								list.Add(itemsCount + j);
								this.itemsSource.Add(Activator.CreateInstance(type3));
							}
						}
						else
						{
							for (int k = 0; k < itemCount; k++)
							{
								list.Add(itemsCount + k);
								this.itemsSource.Add(null);
							}
						}
					}
					this.RaiseItemsAdded(list);
				}
				finally
				{
					CollectionPool<List<int>, int>.Release(list);
				}
				this.RaiseOnSizeChanged();
			}
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x0000DF9C File Offset: 0x0000C19C
		public virtual void Move(int index, int newIndex)
		{
			bool flag = this.itemsSource == null;
			if (!flag)
			{
				bool flag2 = index == newIndex;
				if (!flag2)
				{
					int num = Mathf.Min(index, newIndex);
					int num2 = Mathf.Max(index, newIndex);
					bool flag3 = num < 0 || num2 >= this.itemsSource.Count;
					if (!flag3)
					{
						int num3 = newIndex;
						int num4 = ((newIndex < index) ? 1 : (-1));
						while (Mathf.Min(index, newIndex) < Mathf.Max(index, newIndex))
						{
							this.Swap(index, newIndex);
							newIndex += num4;
						}
						base.RaiseItemIndexChanged(index, num3);
					}
				}
			}
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x0000E034 File Offset: 0x0000C234
		public virtual void RemoveItem(int index)
		{
			List<int> list;
			using (CollectionPool<List<int>, int>.Get(out list))
			{
				list.Add(index);
				this.RemoveItems(list);
			}
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x0000E080 File Offset: 0x0000C280
		public virtual void RemoveItems(List<int> indices)
		{
			this.EnsureItemSourceCanBeResized();
			bool flag = indices == null;
			if (!flag)
			{
				indices.Sort();
				this.RaiseItemsRemoved(indices);
				bool isFixedSize = this.itemsSource.IsFixedSize;
				if (isFixedSize)
				{
					this.itemsSource = BaseListViewController.RemoveFromArray((Array)this.itemsSource, indices);
				}
				else
				{
					for (int i = indices.Count - 1; i >= 0; i--)
					{
						this.itemsSource.RemoveAt(indices[i]);
					}
				}
				this.RaiseOnSizeChanged();
			}
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x0000E114 File Offset: 0x0000C314
		internal virtual void RemoveItems(int itemCount)
		{
			bool flag = itemCount <= 0;
			if (!flag)
			{
				int itemsCount = this.GetItemsCount();
				List<int> list = CollectionPool<List<int>, int>.Get();
				try
				{
					int num = itemsCount - itemCount;
					for (int i = num; i < itemsCount; i++)
					{
						list.Add(i);
					}
					this.RemoveItems(list);
				}
				finally
				{
					CollectionPool<List<int>, int>.Release(list);
				}
			}
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x0000E188 File Offset: 0x0000C388
		public virtual void ClearItems()
		{
			bool flag = this.itemsSource == null;
			if (!flag)
			{
				this.EnsureItemSourceCanBeResized();
				IEnumerable<int> enumerable = Enumerable.Range(0, this.itemsSource.Count - 1);
				this.itemsSource.Clear();
				this.RaiseItemsRemoved(enumerable);
				this.RaiseOnSizeChanged();
			}
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x0000E1DB File Offset: 0x0000C3DB
		protected void RaiseOnSizeChanged()
		{
			Action action = this.itemsSourceSizeChanged;
			if (action != null)
			{
				action();
			}
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x0000E1F0 File Offset: 0x0000C3F0
		protected void RaiseItemsAdded(IEnumerable<int> indices)
		{
			Action<IEnumerable<int>> action = this.itemsAdded;
			if (action != null)
			{
				action(indices);
			}
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x0000E206 File Offset: 0x0000C406
		protected void RaiseItemsRemoved(IEnumerable<int> indices)
		{
			Action<IEnumerable<int>> action = this.itemsRemoved;
			if (action != null)
			{
				action(indices);
			}
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x0000E21C File Offset: 0x0000C41C
		private static Array AddToArray(Array source, int itemCount)
		{
			Type elementType = source.GetType().GetElementType();
			bool flag = elementType == null;
			if (flag)
			{
				throw new InvalidOperationException("Cannot resize source, because its size is fixed.");
			}
			Array array = Array.CreateInstance(elementType, source.Length + itemCount);
			Array.Copy(source, array, source.Length);
			return array;
		}

		// Token: 0x060002FA RID: 762 RVA: 0x0000E270 File Offset: 0x0000C470
		private static Array RemoveFromArray(Array source, List<int> indicesToRemove)
		{
			int length = source.Length;
			int num = length - indicesToRemove.Count;
			bool flag = num < 0;
			if (flag)
			{
				throw new InvalidOperationException("Cannot remove more items than the current count from source.");
			}
			Type elementType = source.GetType().GetElementType();
			bool flag2 = num == 0;
			Array array;
			if (flag2)
			{
				array = Array.CreateInstance(elementType, 0);
			}
			else
			{
				Array array2 = Array.CreateInstance(elementType, num);
				int num2 = 0;
				int num3 = 0;
				for (int i = 0; i < source.Length; i++)
				{
					bool flag3 = num3 < indicesToRemove.Count && indicesToRemove[num3] == i;
					if (flag3)
					{
						num3++;
					}
					else
					{
						array2.SetValue(source.GetValue(i), num2);
						num2++;
					}
				}
				array = array2;
			}
			return array;
		}

		// Token: 0x060002FB RID: 763 RVA: 0x0000E338 File Offset: 0x0000C538
		private void Swap(int lhs, int rhs)
		{
			IList itemsSource = this.itemsSource;
			IList itemsSource2 = this.itemsSource;
			object obj = this.itemsSource[rhs];
			object obj2 = this.itemsSource[lhs];
			itemsSource[lhs] = obj;
			itemsSource2[rhs] = obj2;
		}

		// Token: 0x060002FC RID: 764 RVA: 0x0000E38C File Offset: 0x0000C58C
		private void EnsureItemSourceCanBeResized()
		{
			bool flag = this.itemsSource == null;
			if (flag)
			{
				throw new InvalidOperationException("Unable to add or remove items because the source is not defined. Please assign a valid items source.");
			}
			bool flag2 = this.itemsSource.IsFixedSize && !this.itemsSource.GetType().IsArray;
			if (flag2)
			{
				throw new InvalidOperationException("Unable to add or remove items because the items source is a fixed size.");
			}
		}

		// Token: 0x060002FE RID: 766 RVA: 0x0000E3ED File Offset: 0x0000C5ED
		[CompilerGenerated]
		internal static bool <AddItems>g__IsGenericList|19_0(Type t)
		{
			return t.IsGenericType && t.GetGenericTypeDefinition() == typeof(IList<>);
		}

		// Token: 0x040001AF RID: 431
		internal const string k_NullItemSourceError = "Unable to add or remove items because the source is not defined. Please assign a valid items source.";

		// Token: 0x040001B0 RID: 432
		internal const string k_FixedItemSourceError = "Unable to add or remove items because the items source is a fixed size.";

		// Token: 0x02000059 RID: 89
		internal class SerializedObjectListControllerImpl
		{
			// Token: 0x17000069 RID: 105
			// (get) Token: 0x060002FF RID: 767 RVA: 0x0000E40F File Offset: 0x0000C60F
			private ISerializedObjectList serializedObjectList
			{
				get
				{
					Func<ISerializedObjectList> serializedObjectListGetter = this.m_SerializedObjectListGetter;
					return (serializedObjectListGetter != null) ? serializedObjectListGetter() : null;
				}
			}

			// Token: 0x06000300 RID: 768 RVA: 0x0000E423 File Offset: 0x0000C623
			internal SerializedObjectListControllerImpl(BaseListViewController baseListViewController, Func<ISerializedObjectList> serializedObjectListGetter)
			{
				this.m_BaseListViewController = baseListViewController;
				this.m_SerializedObjectListGetter = serializedObjectListGetter;
			}

			// Token: 0x06000301 RID: 769 RVA: 0x0000E43C File Offset: 0x0000C63C
			public int GetItemsCount()
			{
				ISerializedObjectList serializedObjectList = this.serializedObjectList;
				return (serializedObjectList != null) ? serializedObjectList.Count : 0;
			}

			// Token: 0x06000302 RID: 770 RVA: 0x0000E460 File Offset: 0x0000C660
			internal int GetItemsMinCount()
			{
				ISerializedObjectList serializedObjectList = this.serializedObjectList;
				return (serializedObjectList != null) ? serializedObjectList.minArraySize : 0;
			}

			// Token: 0x06000303 RID: 771 RVA: 0x0000E484 File Offset: 0x0000C684
			public void AddItems(int itemCount)
			{
				int itemsCount = this.GetItemsCount();
				this.serializedObjectList.arraySize += itemCount;
				this.serializedObjectList.ApplyChanges();
				List<int> list = CollectionPool<List<int>, int>.Get();
				try
				{
					for (int i = 0; i < itemCount; i++)
					{
						list.Add(itemsCount + i);
					}
					this.m_BaseListViewController.RaiseItemsAdded(list);
				}
				finally
				{
					CollectionPool<List<int>, int>.Release(list);
				}
				this.m_BaseListViewController.RaiseOnSizeChanged();
			}

			// Token: 0x06000304 RID: 772 RVA: 0x0000E514 File Offset: 0x0000C714
			internal void RemoveItems(int itemCount)
			{
				int itemsCount = this.GetItemsCount();
				this.serializedObjectList.arraySize -= itemCount;
				List<int> list = CollectionPool<List<int>, int>.Get();
				try
				{
					for (int i = itemsCount - itemCount; i < itemsCount; i++)
					{
						list.Add(i);
					}
					this.m_BaseListViewController.RaiseItemsRemoved(list);
				}
				finally
				{
					CollectionPool<List<int>, int>.Release(list);
				}
				this.serializedObjectList.ApplyChanges();
				this.m_BaseListViewController.RaiseOnSizeChanged();
			}

			// Token: 0x06000305 RID: 773 RVA: 0x0000E5A4 File Offset: 0x0000C7A4
			public void RemoveItems(List<int> indices)
			{
				indices.Sort();
				this.m_BaseListViewController.RaiseItemsRemoved(indices);
				int num = this.serializedObjectList.Count;
				for (int i = indices.Count - 1; i >= 0; i--)
				{
					int num2 = indices[i];
					this.serializedObjectList.RemoveAt(num2, num);
					num--;
				}
				this.serializedObjectList.ApplyChanges();
				this.m_BaseListViewController.RaiseOnSizeChanged();
			}

			// Token: 0x06000306 RID: 774 RVA: 0x0000E620 File Offset: 0x0000C820
			public void RemoveItem(int index)
			{
				List<int> list;
				using (CollectionPool<List<int>, int>.Get(out list))
				{
					list.Add(index);
					this.m_BaseListViewController.RaiseItemsRemoved(list);
				}
				this.serializedObjectList.RemoveAt(index);
				this.serializedObjectList.ApplyChanges();
				this.m_BaseListViewController.RaiseOnSizeChanged();
			}

			// Token: 0x06000307 RID: 775 RVA: 0x0000E694 File Offset: 0x0000C894
			public void ClearItems()
			{
				IEnumerable<int> enumerable = Enumerable.Range(0, this.GetItemsMinCount() - 1);
				this.serializedObjectList.arraySize = 0;
				this.serializedObjectList.ApplyChanges();
				this.m_BaseListViewController.RaiseItemsRemoved(enumerable);
				this.m_BaseListViewController.RaiseOnSizeChanged();
			}

			// Token: 0x06000308 RID: 776 RVA: 0x0000E6E4 File Offset: 0x0000C8E4
			public void Move(int srcIndex, int destIndex)
			{
				bool flag = srcIndex == destIndex;
				if (!flag)
				{
					this.serializedObjectList.Move(srcIndex, destIndex);
					this.serializedObjectList.ApplyChanges();
					this.m_BaseListViewController.RaiseItemIndexChanged(srcIndex, destIndex);
				}
			}

			// Token: 0x040001B4 RID: 436
			private Func<ISerializedObjectList> m_SerializedObjectListGetter;

			// Token: 0x040001B5 RID: 437
			private BaseListViewController m_BaseListViewController;
		}
	}
}
