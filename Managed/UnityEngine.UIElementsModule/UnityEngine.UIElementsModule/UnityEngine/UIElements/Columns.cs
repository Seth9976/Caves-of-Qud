using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.Properties;
using UnityEngine.Internal;

namespace UnityEngine.UIElements
{
	// Token: 0x0200014B RID: 331
	[UxmlObject]
	public class Columns : ICollection<Column>, IEnumerable<Column>, IEnumerable, INotifyBindablePropertyChanged
	{
		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x06000AB0 RID: 2736 RVA: 0x0002F823 File Offset: 0x0002DA23
		internal IList<Column> columns
		{
			get
			{
				return this.m_Columns;
			}
		}

		// Token: 0x1400002C RID: 44
		// (add) Token: 0x06000AB1 RID: 2737 RVA: 0x0002F82C File Offset: 0x0002DA2C
		// (remove) Token: 0x06000AB2 RID: 2738 RVA: 0x0002F864 File Offset: 0x0002DA64
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<BindablePropertyChangedEventArgs> propertyChanged;

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x06000AB3 RID: 2739 RVA: 0x0002F899 File Offset: 0x0002DA99
		// (set) Token: 0x06000AB4 RID: 2740 RVA: 0x0002F8A4 File Offset: 0x0002DAA4
		[CreateProperty]
		public string primaryColumnName
		{
			get
			{
				return this.m_PrimaryColumnName;
			}
			set
			{
				bool flag = this.m_PrimaryColumnName == value;
				if (!flag)
				{
					this.m_PrimaryColumnName = value;
					this.NotifyChange(ColumnsDataType.PrimaryColumn);
					this.NotifyPropertyChanged(in Columns.primaryColumnNameProperty);
				}
			}
		}

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x06000AB5 RID: 2741 RVA: 0x0002F8DF File Offset: 0x0002DADF
		// (set) Token: 0x06000AB6 RID: 2742 RVA: 0x0002F8E8 File Offset: 0x0002DAE8
		[CreateProperty]
		public bool reorderable
		{
			get
			{
				return this.m_Reorderable;
			}
			set
			{
				bool flag = this.m_Reorderable == value;
				if (!flag)
				{
					this.m_Reorderable = value;
					this.NotifyChange(ColumnsDataType.Reorderable);
					this.NotifyPropertyChanged(in Columns.reorderableProperty);
				}
			}
		}

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x06000AB7 RID: 2743 RVA: 0x0002F920 File Offset: 0x0002DB20
		// (set) Token: 0x06000AB8 RID: 2744 RVA: 0x0002F928 File Offset: 0x0002DB28
		[CreateProperty]
		public bool resizable
		{
			get
			{
				return this.m_Resizable;
			}
			set
			{
				bool flag = this.m_Resizable == value;
				if (!flag)
				{
					this.m_Resizable = value;
					this.NotifyChange(ColumnsDataType.Resizable);
					this.NotifyPropertyChanged(in Columns.resizableProperty);
				}
			}
		}

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x06000AB9 RID: 2745 RVA: 0x0002F960 File Offset: 0x0002DB60
		// (set) Token: 0x06000ABA RID: 2746 RVA: 0x0002F968 File Offset: 0x0002DB68
		[CreateProperty]
		public bool resizePreview
		{
			get
			{
				return this.m_ResizePreview;
			}
			set
			{
				bool flag = this.m_ResizePreview == value;
				if (!flag)
				{
					this.m_ResizePreview = value;
					this.NotifyChange(ColumnsDataType.ResizePreview);
					this.NotifyPropertyChanged(in Columns.resizePreviewProperty);
				}
			}
		}

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x06000ABB RID: 2747 RVA: 0x0002F9A0 File Offset: 0x0002DBA0
		internal IEnumerable<Column> displayList
		{
			get
			{
				this.InitOrderColumns();
				return this.m_DisplayColumns;
			}
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x06000ABC RID: 2748 RVA: 0x0002F9C0 File Offset: 0x0002DBC0
		internal IEnumerable<Column> visibleList
		{
			get
			{
				this.UpdateVisibleColumns();
				return this.m_VisibleColumns;
			}
		}

		// Token: 0x1400002D RID: 45
		// (add) Token: 0x06000ABD RID: 2749 RVA: 0x0002F9E0 File Offset: 0x0002DBE0
		// (remove) Token: 0x06000ABE RID: 2750 RVA: 0x0002FA18 File Offset: 0x0002DC18
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		internal event Action<ColumnsDataType> changed;

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x06000ABF RID: 2751 RVA: 0x0002FA4D File Offset: 0x0002DC4D
		// (set) Token: 0x06000AC0 RID: 2752 RVA: 0x0002FA58 File Offset: 0x0002DC58
		[CreateProperty]
		public Columns.StretchMode stretchMode
		{
			get
			{
				return this.m_StretchMode;
			}
			set
			{
				bool flag = this.m_StretchMode == value;
				if (!flag)
				{
					this.m_StretchMode = value;
					this.NotifyChange(ColumnsDataType.StretchMode);
					this.NotifyPropertyChanged(in Columns.stretchModeProperty);
				}
			}
		}

		// Token: 0x1400002E RID: 46
		// (add) Token: 0x06000AC1 RID: 2753 RVA: 0x0002FA90 File Offset: 0x0002DC90
		// (remove) Token: 0x06000AC2 RID: 2754 RVA: 0x0002FAC8 File Offset: 0x0002DCC8
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		internal event Action<Column, int> columnAdded;

		// Token: 0x1400002F RID: 47
		// (add) Token: 0x06000AC3 RID: 2755 RVA: 0x0002FB00 File Offset: 0x0002DD00
		// (remove) Token: 0x06000AC4 RID: 2756 RVA: 0x0002FB38 File Offset: 0x0002DD38
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		internal event Action<Column> columnRemoved;

		// Token: 0x14000030 RID: 48
		// (add) Token: 0x06000AC5 RID: 2757 RVA: 0x0002FB70 File Offset: 0x0002DD70
		// (remove) Token: 0x06000AC6 RID: 2758 RVA: 0x0002FBA8 File Offset: 0x0002DDA8
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		internal event Action<Column, ColumnDataType> columnChanged;

		// Token: 0x14000031 RID: 49
		// (add) Token: 0x06000AC7 RID: 2759 RVA: 0x0002FBE0 File Offset: 0x0002DDE0
		// (remove) Token: 0x06000AC8 RID: 2760 RVA: 0x0002FC18 File Offset: 0x0002DE18
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		internal event Action<Column> columnResized;

		// Token: 0x14000032 RID: 50
		// (add) Token: 0x06000AC9 RID: 2761 RVA: 0x0002FC50 File Offset: 0x0002DE50
		// (remove) Token: 0x06000ACA RID: 2762 RVA: 0x0002FC88 File Offset: 0x0002DE88
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		internal event Action<Column, int, int> columnReordered;

		// Token: 0x06000ACB RID: 2763 RVA: 0x0002FCC0 File Offset: 0x0002DEC0
		public bool IsPrimary(Column column)
		{
			return this.primaryColumnName == column.name || (string.IsNullOrEmpty(this.primaryColumnName) && column.visibleIndex == 0);
		}

		// Token: 0x06000ACC RID: 2764 RVA: 0x0002FD04 File Offset: 0x0002DF04
		public IEnumerator<Column> GetEnumerator()
		{
			return this.m_Columns.GetEnumerator();
		}

		// Token: 0x06000ACD RID: 2765 RVA: 0x0002FD24 File Offset: 0x0002DF24
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000ACE RID: 2766 RVA: 0x0002FD3C File Offset: 0x0002DF3C
		public void Add(Column item)
		{
			this.Insert(this.m_Columns.Count, item);
		}

		// Token: 0x06000ACF RID: 2767 RVA: 0x0002FD54 File Offset: 0x0002DF54
		public void Clear()
		{
			while (this.m_Columns.Count > 0)
			{
				this.Remove(this.m_Columns[this.m_Columns.Count - 1]);
			}
		}

		// Token: 0x06000AD0 RID: 2768 RVA: 0x0002FD98 File Offset: 0x0002DF98
		public bool Contains(Column item)
		{
			return this.m_Columns.Contains(item);
		}

		// Token: 0x06000AD1 RID: 2769 RVA: 0x0002FDB8 File Offset: 0x0002DFB8
		public bool Contains(string name)
		{
			foreach (Column column in this.m_Columns)
			{
				bool flag = column.name == name;
				if (flag)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000AD2 RID: 2770 RVA: 0x0002FE1C File Offset: 0x0002E01C
		public void CopyTo(Column[] array, int arrayIndex)
		{
			this.m_Columns.CopyTo(array, arrayIndex);
		}

		// Token: 0x06000AD3 RID: 2771 RVA: 0x0002FE30 File Offset: 0x0002E030
		public bool Remove(Column column)
		{
			bool flag = column == null;
			if (flag)
			{
				throw new ArgumentException("Cannot remove null column");
			}
			bool flag2 = this.m_Columns.Remove(column);
			bool flag3;
			if (flag2)
			{
				List<Column> displayColumns = this.m_DisplayColumns;
				if (displayColumns != null)
				{
					displayColumns.Remove(column);
				}
				List<Column> visibleColumns = this.m_VisibleColumns;
				if (visibleColumns != null)
				{
					visibleColumns.Remove(column);
				}
				column.collection = null;
				column.propertyChanged -= this.OnColumnsPropertyChanged;
				column.changed -= this.OnColumnChanged;
				column.resized -= this.OnColumnResized;
				Action<Column> action = this.columnRemoved;
				if (action != null)
				{
					action(column);
				}
				flag3 = true;
			}
			else
			{
				flag3 = false;
			}
			return flag3;
		}

		// Token: 0x06000AD4 RID: 2772 RVA: 0x0002FEE8 File Offset: 0x0002E0E8
		private void OnColumnsPropertyChanged(object sender, BindablePropertyChangedEventArgs args)
		{
			Column column = (Column)sender;
			int num = this.m_Columns.IndexOf(column);
			bool flag = num > 0;
			if (flag)
			{
				string text = string.Format("columns[{0}].{1}", num, args.propertyName);
				BindingId bindingId = text;
				this.NotifyPropertyChanged(in bindingId);
			}
		}

		// Token: 0x06000AD5 RID: 2773 RVA: 0x0002FF44 File Offset: 0x0002E144
		private void OnColumnChanged(Column column, ColumnDataType type)
		{
			bool flag = type == ColumnDataType.Visibility;
			if (flag)
			{
				this.DirtyVisibleColumns();
			}
			Action<Column, ColumnDataType> action = this.columnChanged;
			if (action != null)
			{
				action(column, type);
			}
		}

		// Token: 0x06000AD6 RID: 2774 RVA: 0x0002FF75 File Offset: 0x0002E175
		private void OnColumnResized(Column column)
		{
			Action<Column> action = this.columnResized;
			if (action != null)
			{
				action(column);
			}
		}

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x06000AD7 RID: 2775 RVA: 0x0002FF8B File Offset: 0x0002E18B
		public int Count
		{
			get
			{
				return this.m_Columns.Count;
			}
		}

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x06000AD8 RID: 2776 RVA: 0x0002FF98 File Offset: 0x0002E198
		public bool IsReadOnly
		{
			get
			{
				return this.m_Columns.IsReadOnly;
			}
		}

		// Token: 0x06000AD9 RID: 2777 RVA: 0x0002FFA8 File Offset: 0x0002E1A8
		public int IndexOf(Column column)
		{
			return this.m_Columns.IndexOf(column);
		}

		// Token: 0x06000ADA RID: 2778 RVA: 0x0002FFC8 File Offset: 0x0002E1C8
		public void Insert(int index, Column column)
		{
			bool flag = column == null;
			if (flag)
			{
				throw new ArgumentException("Cannot insert null column");
			}
			bool flag2 = column.collection == this;
			if (flag2)
			{
				throw new ArgumentException("Already contains this column");
			}
			bool flag3 = column.collection != null;
			if (flag3)
			{
				column.collection.Remove(column);
			}
			this.m_Columns.Insert(index, column);
			bool flag4 = this.m_DisplayColumns != null;
			if (flag4)
			{
				this.m_DisplayColumns.Insert(index, column);
				this.DirtyVisibleColumns();
			}
			column.collection = this;
			column.propertyChanged += this.OnColumnsPropertyChanged;
			column.changed += this.OnColumnChanged;
			column.resized += this.OnColumnResized;
			Action<Column, int> action = this.columnAdded;
			if (action != null)
			{
				action(column, index);
			}
		}

		// Token: 0x06000ADB RID: 2779 RVA: 0x000300A2 File Offset: 0x0002E2A2
		public void RemoveAt(int index)
		{
			this.Remove(this.m_Columns[index]);
		}

		// Token: 0x170001D2 RID: 466
		public Column this[int index]
		{
			get
			{
				return this.m_Columns[index];
			}
		}

		// Token: 0x170001D3 RID: 467
		public Column this[string name]
		{
			get
			{
				foreach (Column column in this.m_Columns)
				{
					bool flag = column.name == name;
					if (flag)
					{
						return column;
					}
				}
				return null;
			}
		}

		// Token: 0x06000ADE RID: 2782 RVA: 0x0003013C File Offset: 0x0002E33C
		public void ReorderDisplay(int from, int to)
		{
			this.InitOrderColumns();
			Column column = this.m_DisplayColumns[from];
			this.m_DisplayColumns.RemoveAt(from);
			this.m_DisplayColumns.Insert(to, column);
			this.DirtyVisibleColumns();
			Action<Column, int, int> action = this.columnReordered;
			if (action != null)
			{
				action(column, from, to);
			}
		}

		// Token: 0x06000ADF RID: 2783 RVA: 0x00030198 File Offset: 0x0002E398
		private void InitOrderColumns()
		{
			bool flag = this.m_DisplayColumns == null;
			if (flag)
			{
				this.m_DisplayColumns = new List<Column>(this);
			}
		}

		// Token: 0x06000AE0 RID: 2784 RVA: 0x000301C4 File Offset: 0x0002E3C4
		private void DirtyVisibleColumns()
		{
			this.m_VisibleColumnsDirty = true;
			bool flag = this.m_VisibleColumns != null;
			if (flag)
			{
				this.m_VisibleColumns.Clear();
			}
		}

		// Token: 0x06000AE1 RID: 2785 RVA: 0x000301F4 File Offset: 0x0002E3F4
		private void UpdateVisibleColumns()
		{
			bool flag = !this.m_VisibleColumnsDirty;
			if (!flag)
			{
				this.InitOrderColumns();
				bool flag2 = this.m_VisibleColumns == null;
				if (flag2)
				{
					this.m_VisibleColumns = new List<Column>(this.m_Columns.Count);
				}
				this.m_VisibleColumns.AddRange(this.m_DisplayColumns.FindAll((Column c) => c.visible));
				this.m_VisibleColumnsDirty = false;
			}
		}

		// Token: 0x06000AE2 RID: 2786 RVA: 0x0003027A File Offset: 0x0002E47A
		private void NotifyChange(ColumnsDataType type)
		{
			Action<ColumnsDataType> action = this.changed;
			if (action != null)
			{
				action(type);
			}
		}

		// Token: 0x06000AE3 RID: 2787 RVA: 0x00030290 File Offset: 0x0002E490
		private void NotifyPropertyChanged(in BindingId property)
		{
			EventHandler<BindablePropertyChangedEventArgs> eventHandler = this.propertyChanged;
			if (eventHandler != null)
			{
				eventHandler(this, new BindablePropertyChangedEventArgs(in property));
			}
		}

		// Token: 0x04000648 RID: 1608
		private static readonly BindingId primaryColumnNameProperty = "primaryColumnName";

		// Token: 0x04000649 RID: 1609
		private static readonly BindingId reorderableProperty = "reorderable";

		// Token: 0x0400064A RID: 1610
		private static readonly BindingId resizableProperty = "resizable";

		// Token: 0x0400064B RID: 1611
		private static readonly BindingId resizePreviewProperty = "resizePreview";

		// Token: 0x0400064C RID: 1612
		private static readonly BindingId stretchModeProperty = "stretchMode";

		// Token: 0x0400064D RID: 1613
		private IList<Column> m_Columns = new List<Column>();

		// Token: 0x0400064E RID: 1614
		private List<Column> m_DisplayColumns;

		// Token: 0x0400064F RID: 1615
		private List<Column> m_VisibleColumns;

		// Token: 0x04000650 RID: 1616
		private bool m_VisibleColumnsDirty = true;

		// Token: 0x04000651 RID: 1617
		private Columns.StretchMode m_StretchMode = Columns.StretchMode.GrowAndFill;

		// Token: 0x04000652 RID: 1618
		private bool m_Reorderable = true;

		// Token: 0x04000653 RID: 1619
		private bool m_Resizable = true;

		// Token: 0x04000654 RID: 1620
		private bool m_ResizePreview;

		// Token: 0x04000655 RID: 1621
		private string m_PrimaryColumnName;

		// Token: 0x0200014C RID: 332
		public enum StretchMode
		{
			// Token: 0x0400065E RID: 1630
			Grow,
			// Token: 0x0400065F RID: 1631
			GrowAndFill
		}

		// Token: 0x0200014D RID: 333
		[ExcludeFromDocs]
		[Serializable]
		public class UxmlSerializedData : UnityEngine.UIElements.UxmlSerializedData
		{
			// Token: 0x06000AE6 RID: 2790 RVA: 0x00030334 File Offset: 0x0002E534
			[Conditional("UNITY_EDITOR")]
			public new static void Register()
			{
				UxmlDescriptionCache.RegisterType(typeof(Columns.UxmlSerializedData), new UxmlAttributeNames[]
				{
					new UxmlAttributeNames("primaryColumnName", "primary-column-name", null, Array.Empty<string>()),
					new UxmlAttributeNames("stretchMode", "stretch-mode", null, Array.Empty<string>()),
					new UxmlAttributeNames("reorderable", "reorderable", null, Array.Empty<string>()),
					new UxmlAttributeNames("resizable", "resizable", null, Array.Empty<string>()),
					new UxmlAttributeNames("resizePreview", "resize-preview", null, Array.Empty<string>()),
					new UxmlAttributeNames("columns", "columns", null, Array.Empty<string>())
				});
			}

			// Token: 0x06000AE7 RID: 2791 RVA: 0x00030400 File Offset: 0x0002E600
			public override object CreateInstance()
			{
				return new Columns();
			}

			// Token: 0x06000AE8 RID: 2792 RVA: 0x00030408 File Offset: 0x0002E608
			public override void Deserialize(object obj)
			{
				Columns columns = (Columns)obj;
				bool flag = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.primaryColumnName_UxmlAttributeFlags);
				if (flag)
				{
					columns.primaryColumnName = this.primaryColumnName;
				}
				bool flag2 = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.stretchMode_UxmlAttributeFlags);
				if (flag2)
				{
					columns.stretchMode = this.stretchMode;
				}
				bool flag3 = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.reorderable_UxmlAttributeFlags);
				if (flag3)
				{
					columns.reorderable = this.reorderable;
				}
				bool flag4 = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.resizable_UxmlAttributeFlags);
				if (flag4)
				{
					columns.resizable = this.resizable;
				}
				bool flag5 = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.resizePreview_UxmlAttributeFlags);
				if (flag5)
				{
					columns.resizePreview = this.resizePreview;
				}
				bool flag6 = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.columns_UxmlAttributeFlags) && this.columns != null;
				if (flag6)
				{
					foreach (Column.UxmlSerializedData uxmlSerializedData in this.columns)
					{
						Column column = (Column)uxmlSerializedData.CreateInstance();
						uxmlSerializedData.Deserialize(column);
						columns.Add(column);
					}
				}
			}

			// Token: 0x04000660 RID: 1632
			[SerializeField]
			private string primaryColumnName;

			// Token: 0x04000661 RID: 1633
			[SerializeField]
			[UxmlIgnore]
			[HideInInspector]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags primaryColumnName_UxmlAttributeFlags;

			// Token: 0x04000662 RID: 1634
			[SerializeField]
			private Columns.StretchMode stretchMode;

			// Token: 0x04000663 RID: 1635
			[UxmlIgnore]
			[HideInInspector]
			[SerializeField]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags stretchMode_UxmlAttributeFlags;

			// Token: 0x04000664 RID: 1636
			[SerializeField]
			private bool reorderable;

			// Token: 0x04000665 RID: 1637
			[SerializeField]
			[UxmlIgnore]
			[HideInInspector]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags reorderable_UxmlAttributeFlags;

			// Token: 0x04000666 RID: 1638
			[SerializeField]
			private bool resizable;

			// Token: 0x04000667 RID: 1639
			[SerializeField]
			[UxmlIgnore]
			[HideInInspector]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags resizable_UxmlAttributeFlags;

			// Token: 0x04000668 RID: 1640
			[SerializeField]
			private bool resizePreview;

			// Token: 0x04000669 RID: 1641
			[HideInInspector]
			[UxmlIgnore]
			[SerializeField]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags resizePreview_UxmlAttributeFlags;

			// Token: 0x0400066A RID: 1642
			[UxmlObjectReference]
			[SerializeReference]
			private List<Column.UxmlSerializedData> columns;

			// Token: 0x0400066B RID: 1643
			[UxmlIgnore]
			[HideInInspector]
			[SerializeField]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags columns_UxmlAttributeFlags;
		}

		// Token: 0x0200014E RID: 334
		[Obsolete("UxmlObjectFactory<T> is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		internal class UxmlObjectFactory<T> : UxmlObjectFactory<T, Columns.UxmlObjectTraits<T>> where T : Columns, new()
		{
		}

		// Token: 0x0200014F RID: 335
		[Obsolete("UxmlObjectFactory is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		internal class UxmlObjectFactory : Columns.UxmlObjectFactory<Columns>
		{
		}

		// Token: 0x02000150 RID: 336
		[Obsolete("UxmlObjectTraits<T> is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		internal class UxmlObjectTraits<T> : UnityEngine.UIElements.UxmlObjectTraits<T> where T : Columns
		{
			// Token: 0x06000AEC RID: 2796 RVA: 0x00030548 File Offset: 0x0002E748
			public override void Init(ref T obj, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ref obj, bag, cc);
				obj.primaryColumnName = this.m_PrimaryColumnName.GetValueFromBag(bag, cc);
				obj.stretchMode = this.m_StretchMode.GetValueFromBag(bag, cc);
				obj.reorderable = this.m_Reorderable.GetValueFromBag(bag, cc);
				obj.resizable = this.m_Resizable.GetValueFromBag(bag, cc);
				obj.resizePreview = this.m_ResizePreview.GetValueFromBag(bag, cc);
				List<Column> valueFromBag = this.m_Columns.GetValueFromBag(bag, cc);
				bool flag = valueFromBag != null;
				if (flag)
				{
					foreach (Column column in valueFromBag)
					{
						obj.Add(column);
					}
				}
			}

			// Token: 0x0400066C RID: 1644
			private readonly UxmlStringAttributeDescription m_PrimaryColumnName = new UxmlStringAttributeDescription
			{
				name = "primary-column-name"
			};

			// Token: 0x0400066D RID: 1645
			private readonly UxmlEnumAttributeDescription<Columns.StretchMode> m_StretchMode = new UxmlEnumAttributeDescription<Columns.StretchMode>
			{
				name = "stretch-mode",
				defaultValue = Columns.StretchMode.GrowAndFill
			};

			// Token: 0x0400066E RID: 1646
			private readonly UxmlBoolAttributeDescription m_Reorderable = new UxmlBoolAttributeDescription
			{
				name = "reorderable",
				defaultValue = true
			};

			// Token: 0x0400066F RID: 1647
			private readonly UxmlBoolAttributeDescription m_Resizable = new UxmlBoolAttributeDescription
			{
				name = "resizable",
				defaultValue = true
			};

			// Token: 0x04000670 RID: 1648
			private readonly UxmlBoolAttributeDescription m_ResizePreview = new UxmlBoolAttributeDescription
			{
				name = "resize-preview"
			};

			// Token: 0x04000671 RID: 1649
			private readonly UxmlObjectListAttributeDescription<Column> m_Columns = new UxmlObjectListAttributeDescription<Column>();
		}
	}
}
