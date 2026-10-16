using System;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.Properties;
using UnityEngine.Internal;

namespace UnityEngine.UIElements
{
	// Token: 0x02000140 RID: 320
	[UxmlObject]
	public class Column : INotifyBindablePropertyChanged
	{
		// Token: 0x14000028 RID: 40
		// (add) Token: 0x06000A2F RID: 2607 RVA: 0x0002C9E8 File Offset: 0x0002ABE8
		// (remove) Token: 0x06000A30 RID: 2608 RVA: 0x0002CA20 File Offset: 0x0002AC20
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<BindablePropertyChangedEventArgs> propertyChanged;

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x06000A31 RID: 2609 RVA: 0x0002CA55 File Offset: 0x0002AC55
		// (set) Token: 0x06000A32 RID: 2610 RVA: 0x0002CA60 File Offset: 0x0002AC60
		[CreateProperty]
		public string name
		{
			get
			{
				return this.m_Name;
			}
			set
			{
				bool flag = this.m_Name == value;
				if (!flag)
				{
					this.m_Name = value;
					this.NotifyChange(ColumnDataType.Name);
					this.NotifyPropertyChanged(in Column.nameProperty);
				}
			}
		}

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x06000A33 RID: 2611 RVA: 0x0002CA9B File Offset: 0x0002AC9B
		// (set) Token: 0x06000A34 RID: 2612 RVA: 0x0002CAA4 File Offset: 0x0002ACA4
		[CreateProperty]
		public string title
		{
			get
			{
				return this.m_Title;
			}
			set
			{
				bool flag = this.m_Title == value;
				if (!flag)
				{
					this.m_Title = value;
					this.NotifyChange(ColumnDataType.Title);
					this.NotifyPropertyChanged(in Column.titleProperty);
				}
			}
		}

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x06000A35 RID: 2613 RVA: 0x0002CADF File Offset: 0x0002ACDF
		// (set) Token: 0x06000A36 RID: 2614 RVA: 0x0002CAE8 File Offset: 0x0002ACE8
		[CreateProperty]
		public Background icon
		{
			get
			{
				return this.m_Icon;
			}
			set
			{
				bool flag = this.m_Icon == value;
				if (!flag)
				{
					this.m_Icon = value;
					this.NotifyChange(ColumnDataType.Icon);
					this.NotifyPropertyChanged(in Column.iconProperty);
				}
			}
		}

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x06000A37 RID: 2615 RVA: 0x0002CB23 File Offset: 0x0002AD23
		// (set) Token: 0x06000A38 RID: 2616 RVA: 0x0002CB2B File Offset: 0x0002AD2B
		public Comparison<int> comparison { get; set; }

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x06000A39 RID: 2617 RVA: 0x0002CB34 File Offset: 0x0002AD34
		internal int index
		{
			get
			{
				Columns collection = this.collection;
				return (collection != null) ? collection.IndexOf(this) : (-1);
			}
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x06000A3A RID: 2618 RVA: 0x0002CB49 File Offset: 0x0002AD49
		internal int displayIndex
		{
			get
			{
				Columns collection = this.collection;
				List<Column> list = ((collection != null) ? collection.displayList : null) as List<Column>;
				return (list != null) ? list.IndexOf(this) : (-1);
			}
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x06000A3B RID: 2619 RVA: 0x0002CB6F File Offset: 0x0002AD6F
		internal int visibleIndex
		{
			get
			{
				Columns collection = this.collection;
				List<Column> list = ((collection != null) ? collection.visibleList : null) as List<Column>;
				return (list != null) ? list.IndexOf(this) : (-1);
			}
		}

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x06000A3C RID: 2620 RVA: 0x0002CB95 File Offset: 0x0002AD95
		// (set) Token: 0x06000A3D RID: 2621 RVA: 0x0002CBA0 File Offset: 0x0002ADA0
		[CreateProperty]
		public bool visible
		{
			get
			{
				return this.m_Visible;
			}
			set
			{
				bool flag = this.m_Visible == value;
				if (!flag)
				{
					this.m_Visible = value;
					this.NotifyChange(ColumnDataType.Visibility);
					this.NotifyPropertyChanged(in Column.visibleProperty);
				}
			}
		}

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x06000A3E RID: 2622 RVA: 0x0002CBD8 File Offset: 0x0002ADD8
		// (set) Token: 0x06000A3F RID: 2623 RVA: 0x0002CBE0 File Offset: 0x0002ADE0
		[CreateProperty]
		public Length width
		{
			get
			{
				return this.m_Width;
			}
			set
			{
				bool flag = this.m_Width == value;
				if (!flag)
				{
					this.m_Width = value;
					this.desiredWidth = float.NaN;
					this.NotifyChange(ColumnDataType.Width);
					this.NotifyPropertyChanged(in Column.widthProperty);
				}
			}
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x06000A40 RID: 2624 RVA: 0x0002CC27 File Offset: 0x0002AE27
		// (set) Token: 0x06000A41 RID: 2625 RVA: 0x0002CC30 File Offset: 0x0002AE30
		[CreateProperty]
		public Length minWidth
		{
			get
			{
				return this.m_MinWidth;
			}
			set
			{
				bool flag = this.m_MinWidth == value;
				if (!flag)
				{
					this.m_MinWidth = value;
					this.NotifyChange(ColumnDataType.MinWidth);
					this.NotifyPropertyChanged(in Column.minWidthProperty);
				}
			}
		}

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x06000A42 RID: 2626 RVA: 0x0002CC6B File Offset: 0x0002AE6B
		// (set) Token: 0x06000A43 RID: 2627 RVA: 0x0002CC74 File Offset: 0x0002AE74
		[CreateProperty]
		public Length maxWidth
		{
			get
			{
				return this.m_MaxWidth;
			}
			set
			{
				bool flag = this.m_MaxWidth == value;
				if (!flag)
				{
					this.m_MaxWidth = value;
					this.NotifyChange(ColumnDataType.MaxWidth);
					this.NotifyPropertyChanged(in Column.maxWidthProperty);
				}
			}
		}

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x06000A44 RID: 2628 RVA: 0x0002CCAF File Offset: 0x0002AEAF
		// (set) Token: 0x06000A45 RID: 2629 RVA: 0x0002CCB8 File Offset: 0x0002AEB8
		internal float desiredWidth
		{
			get
			{
				return this.m_DesiredWidth;
			}
			set
			{
				bool flag = this.m_DesiredWidth == value;
				if (!flag)
				{
					this.m_DesiredWidth = value;
					Action<Column> action = this.resized;
					if (action != null)
					{
						action(this);
					}
				}
			}
		}

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x06000A46 RID: 2630 RVA: 0x0002CCEF File Offset: 0x0002AEEF
		// (set) Token: 0x06000A47 RID: 2631 RVA: 0x0002CCF8 File Offset: 0x0002AEF8
		[CreateProperty]
		public bool sortable
		{
			get
			{
				return this.m_Sortable;
			}
			set
			{
				bool flag = this.m_Sortable == value;
				if (!flag)
				{
					this.m_Sortable = value;
					this.NotifyChange(ColumnDataType.Sortable);
					this.NotifyPropertyChanged(in Column.sortableProperty);
				}
			}
		}

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x06000A48 RID: 2632 RVA: 0x0002CD30 File Offset: 0x0002AF30
		// (set) Token: 0x06000A49 RID: 2633 RVA: 0x0002CD38 File Offset: 0x0002AF38
		[CreateProperty]
		public bool stretchable
		{
			get
			{
				return this.m_Stretchable;
			}
			set
			{
				bool flag = this.m_Stretchable == value;
				if (!flag)
				{
					this.m_Stretchable = value;
					this.NotifyChange(ColumnDataType.Stretchable);
					this.NotifyPropertyChanged(in Column.stretchableProperty);
				}
			}
		}

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x06000A4A RID: 2634 RVA: 0x0002CD70 File Offset: 0x0002AF70
		// (set) Token: 0x06000A4B RID: 2635 RVA: 0x0002CD78 File Offset: 0x0002AF78
		[CreateProperty]
		public bool optional
		{
			get
			{
				return this.m_Optional;
			}
			set
			{
				bool flag = this.m_Optional == value;
				if (!flag)
				{
					this.m_Optional = value;
					this.NotifyChange(ColumnDataType.Optional);
					this.NotifyPropertyChanged(in Column.optionalProperty);
				}
			}
		}

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x06000A4C RID: 2636 RVA: 0x0002CDB1 File Offset: 0x0002AFB1
		// (set) Token: 0x06000A4D RID: 2637 RVA: 0x0002CDBC File Offset: 0x0002AFBC
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
					this.NotifyChange(ColumnDataType.Resizable);
					this.NotifyPropertyChanged(in Column.resizableProperty);
				}
			}
		}

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x06000A4E RID: 2638 RVA: 0x0002CDF5 File Offset: 0x0002AFF5
		// (set) Token: 0x06000A4F RID: 2639 RVA: 0x0002CDFD File Offset: 0x0002AFFD
		public string bindingPath { get; set; }

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x06000A50 RID: 2640 RVA: 0x0002CE06 File Offset: 0x0002B006
		// (set) Token: 0x06000A51 RID: 2641 RVA: 0x0002CE10 File Offset: 0x0002B010
		[CreateProperty]
		public VisualTreeAsset headerTemplate
		{
			get
			{
				return this.m_HeaderTemplate;
			}
			set
			{
				bool flag = this.m_HeaderTemplate == value;
				if (!flag)
				{
					this.m_HeaderTemplate = value;
					this.NotifyChange(ColumnDataType.HeaderTemplate);
					this.NotifyPropertyChanged(in Column.headerTemplateProperty);
				}
			}
		}

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x06000A52 RID: 2642 RVA: 0x0002CE4C File Offset: 0x0002B04C
		// (set) Token: 0x06000A53 RID: 2643 RVA: 0x0002CE54 File Offset: 0x0002B054
		[CreateProperty]
		public VisualTreeAsset cellTemplate
		{
			get
			{
				return this.m_CellTemplate;
			}
			set
			{
				bool flag = this.m_CellTemplate == value;
				if (!flag)
				{
					this.m_CellTemplate = value;
					this.NotifyChange(ColumnDataType.CellTemplate);
					this.NotifyPropertyChanged(in Column.cellTemplateProperty);
				}
			}
		}

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x06000A54 RID: 2644 RVA: 0x0002CE90 File Offset: 0x0002B090
		// (set) Token: 0x06000A55 RID: 2645 RVA: 0x0002CE98 File Offset: 0x0002B098
		public Func<VisualElement> makeHeader
		{
			get
			{
				return this.m_MakeHeader;
			}
			set
			{
				bool flag = this.m_MakeHeader == value;
				if (!flag)
				{
					this.m_MakeHeader = value;
					this.NotifyChange(ColumnDataType.HeaderTemplate);
				}
			}
		}

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x06000A56 RID: 2646 RVA: 0x0002CEC8 File Offset: 0x0002B0C8
		// (set) Token: 0x06000A57 RID: 2647 RVA: 0x0002CED0 File Offset: 0x0002B0D0
		public Action<VisualElement> bindHeader
		{
			get
			{
				return this.m_BindHeader;
			}
			set
			{
				bool flag = this.m_BindHeader == value;
				if (!flag)
				{
					this.m_BindHeader = value;
					this.NotifyChange(ColumnDataType.HeaderTemplate);
				}
			}
		}

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x06000A58 RID: 2648 RVA: 0x0002CF00 File Offset: 0x0002B100
		// (set) Token: 0x06000A59 RID: 2649 RVA: 0x0002CF08 File Offset: 0x0002B108
		public Action<VisualElement> unbindHeader
		{
			get
			{
				return this.m_UnbindHeader;
			}
			set
			{
				bool flag = this.m_UnbindHeader == value;
				if (!flag)
				{
					this.m_UnbindHeader = value;
					this.NotifyChange(ColumnDataType.HeaderTemplate);
				}
			}
		}

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x06000A5A RID: 2650 RVA: 0x0002CF38 File Offset: 0x0002B138
		// (set) Token: 0x06000A5B RID: 2651 RVA: 0x0002CF40 File Offset: 0x0002B140
		public Action<VisualElement> destroyHeader
		{
			get
			{
				return this.m_DestroyHeader;
			}
			set
			{
				bool flag = this.m_DestroyHeader == value;
				if (!flag)
				{
					this.m_DestroyHeader = value;
					this.NotifyChange(ColumnDataType.HeaderTemplate);
				}
			}
		}

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x06000A5C RID: 2652 RVA: 0x0002CF70 File Offset: 0x0002B170
		// (set) Token: 0x06000A5D RID: 2653 RVA: 0x0002CF78 File Offset: 0x0002B178
		public Func<VisualElement> makeCell
		{
			get
			{
				return this.m_MakeCell;
			}
			set
			{
				bool flag = this.m_MakeCell == value;
				if (!flag)
				{
					this.m_MakeCell = value;
					this.NotifyChange(ColumnDataType.CellTemplate);
				}
			}
		}

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x06000A5E RID: 2654 RVA: 0x0002CFA8 File Offset: 0x0002B1A8
		// (set) Token: 0x06000A5F RID: 2655 RVA: 0x0002CFB0 File Offset: 0x0002B1B0
		public Action<VisualElement, int> bindCell
		{
			get
			{
				return this.m_BindCell;
			}
			set
			{
				bool flag = this.m_BindCell == value;
				if (!flag)
				{
					this.m_BindCell = value;
					this.NotifyChange(ColumnDataType.CellTemplate);
				}
			}
		}

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x06000A60 RID: 2656 RVA: 0x0002CFE0 File Offset: 0x0002B1E0
		// (set) Token: 0x06000A61 RID: 2657 RVA: 0x0002CFE8 File Offset: 0x0002B1E8
		public Action<VisualElement, int> unbindCell
		{
			get
			{
				return this.m_UnbindCellItem;
			}
			set
			{
				bool flag = this.m_UnbindCellItem == value;
				if (!flag)
				{
					this.m_UnbindCellItem = value;
					this.NotifyChange(ColumnDataType.CellTemplate);
				}
			}
		}

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x06000A62 RID: 2658 RVA: 0x0002D018 File Offset: 0x0002B218
		// (set) Token: 0x06000A63 RID: 2659 RVA: 0x0002D020 File Offset: 0x0002B220
		public Action<VisualElement> destroyCell { get; set; }

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x06000A64 RID: 2660 RVA: 0x0002D029 File Offset: 0x0002B229
		// (set) Token: 0x06000A65 RID: 2661 RVA: 0x0002D031 File Offset: 0x0002B231
		public Columns collection { get; internal set; }

		// Token: 0x14000029 RID: 41
		// (add) Token: 0x06000A66 RID: 2662 RVA: 0x0002D03C File Offset: 0x0002B23C
		// (remove) Token: 0x06000A67 RID: 2663 RVA: 0x0002D074 File Offset: 0x0002B274
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		internal event Action<Column, ColumnDataType> changed;

		// Token: 0x1400002A RID: 42
		// (add) Token: 0x06000A68 RID: 2664 RVA: 0x0002D0AC File Offset: 0x0002B2AC
		// (remove) Token: 0x06000A69 RID: 2665 RVA: 0x0002D0E4 File Offset: 0x0002B2E4
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		internal event Action<Column> resized;

		// Token: 0x06000A6A RID: 2666 RVA: 0x0002D119 File Offset: 0x0002B319
		private void NotifyChange(ColumnDataType type)
		{
			Action<Column, ColumnDataType> action = this.changed;
			if (action != null)
			{
				action(this, type);
			}
		}

		// Token: 0x06000A6B RID: 2667 RVA: 0x0002D130 File Offset: 0x0002B330
		private void NotifyPropertyChanged(in BindingId property)
		{
			EventHandler<BindablePropertyChangedEventArgs> eventHandler = this.propertyChanged;
			if (eventHandler != null)
			{
				eventHandler(this, new BindablePropertyChangedEventArgs(in property));
			}
		}

		// Token: 0x06000A6C RID: 2668 RVA: 0x0002D14C File Offset: 0x0002B34C
		internal float GetWidth(float layoutWidth)
		{
			return (this.width.unit == LengthUnit.Pixel) ? this.width.value : (this.width.value * layoutWidth / 100f);
		}

		// Token: 0x06000A6D RID: 2669 RVA: 0x0002D194 File Offset: 0x0002B394
		internal float GetMaxWidth(float layoutWidth)
		{
			return (this.maxWidth.unit == LengthUnit.Pixel) ? this.maxWidth.value : (this.maxWidth.value * layoutWidth / 100f);
		}

		// Token: 0x06000A6E RID: 2670 RVA: 0x0002D1DC File Offset: 0x0002B3DC
		internal float GetMinWidth(float layoutWidth)
		{
			return (this.minWidth.unit == LengthUnit.Pixel) ? this.minWidth.value : (this.minWidth.value * layoutWidth / 100f);
		}

		// Token: 0x040005D3 RID: 1491
		private static readonly BindingId nameProperty = "name";

		// Token: 0x040005D4 RID: 1492
		private static readonly BindingId titleProperty = "title";

		// Token: 0x040005D5 RID: 1493
		private static readonly BindingId iconProperty = "icon";

		// Token: 0x040005D6 RID: 1494
		private static readonly BindingId visibleProperty = "visible";

		// Token: 0x040005D7 RID: 1495
		private static readonly BindingId widthProperty = "width";

		// Token: 0x040005D8 RID: 1496
		private static readonly BindingId minWidthProperty = "minWidth";

		// Token: 0x040005D9 RID: 1497
		private static readonly BindingId maxWidthProperty = "maxWidth";

		// Token: 0x040005DA RID: 1498
		private static readonly BindingId sortableProperty = "sortable";

		// Token: 0x040005DB RID: 1499
		private static readonly BindingId stretchableProperty = "stretchable";

		// Token: 0x040005DC RID: 1500
		private static readonly BindingId optionalProperty = "optional";

		// Token: 0x040005DD RID: 1501
		private static readonly BindingId resizableProperty = "resizable";

		// Token: 0x040005DE RID: 1502
		private static readonly BindingId headerTemplateProperty = "headerTemplate";

		// Token: 0x040005DF RID: 1503
		private static readonly BindingId cellTemplateProperty = "cellTemplate";

		// Token: 0x040005E0 RID: 1504
		internal const string k_HeaderTemplateAttributeName = "header-template";

		// Token: 0x040005E1 RID: 1505
		internal const string k_CellTemplateAttributeName = "cell-template";

		// Token: 0x040005E2 RID: 1506
		internal const float kDefaultMinWidth = 35f;

		// Token: 0x040005E3 RID: 1507
		private string m_Name;

		// Token: 0x040005E4 RID: 1508
		private string m_Title;

		// Token: 0x040005E5 RID: 1509
		private Background m_Icon;

		// Token: 0x040005E6 RID: 1510
		private bool m_Visible = true;

		// Token: 0x040005E7 RID: 1511
		private Length m_Width = 0f;

		// Token: 0x040005E8 RID: 1512
		private Length m_MinWidth = 35f;

		// Token: 0x040005E9 RID: 1513
		private Length m_MaxWidth = 8388608f;

		// Token: 0x040005EA RID: 1514
		private float m_DesiredWidth = float.NaN;

		// Token: 0x040005EB RID: 1515
		private bool m_Stretchable;

		// Token: 0x040005EC RID: 1516
		private bool m_Sortable = true;

		// Token: 0x040005ED RID: 1517
		private bool m_Optional = true;

		// Token: 0x040005EE RID: 1518
		private bool m_Resizable = true;

		// Token: 0x040005EF RID: 1519
		private VisualTreeAsset m_HeaderTemplate;

		// Token: 0x040005F0 RID: 1520
		private VisualTreeAsset m_CellTemplate;

		// Token: 0x040005F1 RID: 1521
		private Func<VisualElement> m_MakeHeader;

		// Token: 0x040005F2 RID: 1522
		private Action<VisualElement> m_BindHeader;

		// Token: 0x040005F3 RID: 1523
		private Action<VisualElement> m_UnbindHeader;

		// Token: 0x040005F4 RID: 1524
		private Action<VisualElement> m_DestroyHeader;

		// Token: 0x040005F5 RID: 1525
		private Func<VisualElement> m_MakeCell;

		// Token: 0x040005F6 RID: 1526
		private Action<VisualElement, int> m_BindCell;

		// Token: 0x040005F7 RID: 1527
		private Action<VisualElement, int> m_UnbindCellItem;

		// Token: 0x02000141 RID: 321
		[ExcludeFromDocs]
		[Serializable]
		public class UxmlSerializedData : UnityEngine.UIElements.UxmlSerializedData
		{
			// Token: 0x06000A71 RID: 2673 RVA: 0x0002D360 File Offset: 0x0002B560
			[Conditional("UNITY_EDITOR")]
			public new static void Register()
			{
				UxmlDescriptionCache.RegisterType(typeof(Column.UxmlSerializedData), new UxmlAttributeNames[]
				{
					new UxmlAttributeNames("name", "name", null, Array.Empty<string>()),
					new UxmlAttributeNames("title", "title", null, Array.Empty<string>()),
					new UxmlAttributeNames("visible", "visible", null, Array.Empty<string>()),
					new UxmlAttributeNames("width", "width", null, Array.Empty<string>()),
					new UxmlAttributeNames("minWidth", "min-width", null, Array.Empty<string>()),
					new UxmlAttributeNames("maxWidth", "max-width", null, Array.Empty<string>()),
					new UxmlAttributeNames("stretchable", "stretchable", null, Array.Empty<string>()),
					new UxmlAttributeNames("sortable", "sortable", null, Array.Empty<string>()),
					new UxmlAttributeNames("optional", "optional", null, Array.Empty<string>()),
					new UxmlAttributeNames("resizable", "resizable", null, Array.Empty<string>()),
					new UxmlAttributeNames("headerTemplate", "header-template", null, Array.Empty<string>()),
					new UxmlAttributeNames("cellTemplate", "cell-template", null, Array.Empty<string>()),
					new UxmlAttributeNames("bindingPath", "binding-path", null, Array.Empty<string>())
				});
			}

			// Token: 0x06000A72 RID: 2674 RVA: 0x0002D4F5 File Offset: 0x0002B6F5
			public override object CreateInstance()
			{
				return new Column();
			}

			// Token: 0x06000A73 RID: 2675 RVA: 0x0002D4FC File Offset: 0x0002B6FC
			public override void Deserialize(object obj)
			{
				Column column = (Column)obj;
				bool flag = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.name_UxmlAttributeFlags);
				if (flag)
				{
					column.name = this.name;
				}
				bool flag2 = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.title_UxmlAttributeFlags);
				if (flag2)
				{
					column.title = this.title;
				}
				bool flag3 = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.visible_UxmlAttributeFlags);
				if (flag3)
				{
					column.visible = this.visible;
				}
				bool flag4 = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.width_UxmlAttributeFlags);
				if (flag4)
				{
					column.width = this.width;
				}
				bool flag5 = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.minWidth_UxmlAttributeFlags);
				if (flag5)
				{
					column.minWidth = this.minWidth;
				}
				bool flag6 = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.maxWidth_UxmlAttributeFlags);
				if (flag6)
				{
					column.maxWidth = this.maxWidth;
				}
				bool flag7 = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.sortable_UxmlAttributeFlags);
				if (flag7)
				{
					column.sortable = this.sortable;
				}
				bool flag8 = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.stretchable_UxmlAttributeFlags);
				if (flag8)
				{
					column.stretchable = this.stretchable;
				}
				bool flag9 = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.optional_UxmlAttributeFlags);
				if (flag9)
				{
					column.optional = this.optional;
				}
				bool flag10 = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.resizable_UxmlAttributeFlags);
				if (flag10)
				{
					column.resizable = this.resizable;
				}
				bool flag11 = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.bindingPath_UxmlAttributeFlags);
				if (flag11)
				{
					column.bindingPath = this.bindingPath;
				}
				bool flag12 = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.headerTemplate_UxmlAttributeFlags) && this.headerTemplate != null;
				if (flag12)
				{
					column.headerTemplate = this.headerTemplate;
					column.makeHeader = () => this.headerTemplate.Instantiate();
				}
				bool flag13 = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.cellTemplate_UxmlAttributeFlags) && this.cellTemplate != null;
				if (flag13)
				{
					column.cellTemplate = this.cellTemplate;
					column.makeCell = () => this.cellTemplate.Instantiate();
				}
			}

			// Token: 0x040005FF RID: 1535
			[SerializeField]
			private VisualTreeAsset headerTemplate;

			// Token: 0x04000600 RID: 1536
			[SerializeField]
			private VisualTreeAsset cellTemplate;

			// Token: 0x04000601 RID: 1537
			[SerializeField]
			private string name;

			// Token: 0x04000602 RID: 1538
			[SerializeField]
			private string title;

			// Token: 0x04000603 RID: 1539
			[SerializeField]
			private string bindingPath;

			// Token: 0x04000604 RID: 1540
			[SerializeField]
			private Length width;

			// Token: 0x04000605 RID: 1541
			[SerializeField]
			private Length minWidth;

			// Token: 0x04000606 RID: 1542
			[SerializeField]
			private Length maxWidth;

			// Token: 0x04000607 RID: 1543
			[SerializeField]
			private bool visible;

			// Token: 0x04000608 RID: 1544
			[SerializeField]
			private bool stretchable;

			// Token: 0x04000609 RID: 1545
			[SerializeField]
			private bool sortable;

			// Token: 0x0400060A RID: 1546
			[SerializeField]
			private bool optional;

			// Token: 0x0400060B RID: 1547
			[SerializeField]
			private bool resizable;

			// Token: 0x0400060C RID: 1548
			[UxmlIgnore]
			[SerializeField]
			[HideInInspector]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags name_UxmlAttributeFlags;

			// Token: 0x0400060D RID: 1549
			[HideInInspector]
			[SerializeField]
			[UxmlIgnore]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags title_UxmlAttributeFlags;

			// Token: 0x0400060E RID: 1550
			[SerializeField]
			[UxmlIgnore]
			[HideInInspector]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags visible_UxmlAttributeFlags;

			// Token: 0x0400060F RID: 1551
			[SerializeField]
			[UxmlIgnore]
			[HideInInspector]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags width_UxmlAttributeFlags;

			// Token: 0x04000610 RID: 1552
			[UxmlIgnore]
			[HideInInspector]
			[SerializeField]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags minWidth_UxmlAttributeFlags;

			// Token: 0x04000611 RID: 1553
			[SerializeField]
			[UxmlIgnore]
			[HideInInspector]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags maxWidth_UxmlAttributeFlags;

			// Token: 0x04000612 RID: 1554
			[SerializeField]
			[UxmlIgnore]
			[HideInInspector]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags stretchable_UxmlAttributeFlags;

			// Token: 0x04000613 RID: 1555
			[SerializeField]
			[HideInInspector]
			[UxmlIgnore]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags sortable_UxmlAttributeFlags;

			// Token: 0x04000614 RID: 1556
			[UxmlIgnore]
			[HideInInspector]
			[SerializeField]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags optional_UxmlAttributeFlags;

			// Token: 0x04000615 RID: 1557
			[HideInInspector]
			[SerializeField]
			[UxmlIgnore]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags resizable_UxmlAttributeFlags;

			// Token: 0x04000616 RID: 1558
			[HideInInspector]
			[UxmlIgnore]
			[SerializeField]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags headerTemplate_UxmlAttributeFlags;

			// Token: 0x04000617 RID: 1559
			[HideInInspector]
			[UxmlIgnore]
			[SerializeField]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags cellTemplate_UxmlAttributeFlags;

			// Token: 0x04000618 RID: 1560
			[SerializeField]
			[HideInInspector]
			[UxmlIgnore]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags bindingPath_UxmlAttributeFlags;
		}

		// Token: 0x02000142 RID: 322
		[Obsolete("UxmlObjectFactory<T> is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		internal class UxmlObjectFactory<T> : UxmlObjectFactory<T, Column.UxmlObjectTraits<T>> where T : Column, new()
		{
		}

		// Token: 0x02000143 RID: 323
		[Obsolete("UxmlObjectFactory<T> is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		internal class UxmlObjectFactory : Column.UxmlObjectFactory<Column>
		{
		}

		// Token: 0x02000144 RID: 324
		[Obsolete("UxmlObjectTraits<T> is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		internal class UxmlObjectTraits<T> : UnityEngine.UIElements.UxmlObjectTraits<T> where T : Column
		{
			// Token: 0x06000A79 RID: 2681 RVA: 0x0002D70C File Offset: 0x0002B90C
			private static Length ParseLength(string str, Length defaultValue)
			{
				float num = defaultValue.value;
				LengthUnit lengthUnit = defaultValue.unit;
				int num2 = 0;
				int num3 = -1;
				for (int i = 0; i < str.Length; i++)
				{
					char c = str[i];
					bool flag = char.IsLetter(c) || c == '%';
					if (flag)
					{
						num3 = i;
						break;
					}
					num2++;
				}
				string text = str.Substring(0, num2);
				string text2 = string.Empty;
				bool flag2 = num3 > 0;
				if (flag2)
				{
					text2 = str.Substring(num3, str.Length - num3).ToLowerInvariant();
				}
				float num4;
				bool flag3 = float.TryParse(text, out num4);
				if (flag3)
				{
					num = num4;
				}
				string text3 = text2;
				string text4 = text3;
				if (!(text4 == "px"))
				{
					if (text4 == "%")
					{
						lengthUnit = LengthUnit.Percent;
					}
				}
				else
				{
					lengthUnit = LengthUnit.Pixel;
				}
				return new Length(num, lengthUnit);
			}

			// Token: 0x06000A7A RID: 2682 RVA: 0x0002D7F8 File Offset: 0x0002B9F8
			public override void Init(ref T obj, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ref obj, bag, cc);
				obj.name = this.m_Name.GetValueFromBag(bag, cc);
				obj.title = this.m_Text.GetValueFromBag(bag, cc);
				obj.visible = this.m_Visible.GetValueFromBag(bag, cc);
				obj.width = Column.UxmlObjectTraits<T>.ParseLength(this.m_Width.GetValueFromBag(bag, cc), default(Length));
				obj.maxWidth = Column.UxmlObjectTraits<T>.ParseLength(this.m_MaxWidth.GetValueFromBag(bag, cc), new Length(8388608f));
				obj.minWidth = Column.UxmlObjectTraits<T>.ParseLength(this.m_MinWidth.GetValueFromBag(bag, cc), new Length(35f));
				obj.sortable = this.m_Sortable.GetValueFromBag(bag, cc);
				obj.stretchable = this.m_Stretch.GetValueFromBag(bag, cc);
				obj.optional = this.m_Optional.GetValueFromBag(bag, cc);
				obj.resizable = this.m_Resizable.GetValueFromBag(bag, cc);
				obj.bindingPath = this.m_BindingPath.GetValueFromBag(bag, cc);
				string valueFromBag = this.m_HeaderTemplateId.GetValueFromBag(bag, cc);
				bool flag = !string.IsNullOrEmpty(valueFromBag);
				if (flag)
				{
					Column.UxmlObjectTraits<T>.<>c__DisplayClass14_0 CS$<>8__locals1 = new Column.UxmlObjectTraits<T>.<>c__DisplayClass14_0();
					Column.UxmlObjectTraits<T>.<>c__DisplayClass14_0 CS$<>8__locals2 = CS$<>8__locals1;
					VisualTreeAsset visualTreeAsset = cc.visualTreeAsset;
					CS$<>8__locals2.asset = ((visualTreeAsset != null) ? visualTreeAsset.ResolveTemplate(valueFromBag) : null);
					obj.makeHeader = delegate
					{
						bool flag3 = CS$<>8__locals1.asset != null;
						VisualElement visualElement;
						if (flag3)
						{
							visualElement = CS$<>8__locals1.asset.Instantiate();
						}
						else
						{
							visualElement = new Label(BaseVerticalCollectionView.k_InvalidTemplateError);
						}
						return visualElement;
					};
				}
				string valueFromBag2 = this.m_CellTemplateId.GetValueFromBag(bag, cc);
				bool flag2 = !string.IsNullOrEmpty(valueFromBag2);
				if (flag2)
				{
					Column.UxmlObjectTraits<T>.<>c__DisplayClass14_1 CS$<>8__locals3 = new Column.UxmlObjectTraits<T>.<>c__DisplayClass14_1();
					Column.UxmlObjectTraits<T>.<>c__DisplayClass14_1 CS$<>8__locals4 = CS$<>8__locals3;
					VisualTreeAsset visualTreeAsset2 = cc.visualTreeAsset;
					CS$<>8__locals4.asset = ((visualTreeAsset2 != null) ? visualTreeAsset2.ResolveTemplate(valueFromBag2) : null);
					obj.makeCell = delegate
					{
						bool flag4 = CS$<>8__locals3.asset != null;
						VisualElement visualElement2;
						if (flag4)
						{
							visualElement2 = CS$<>8__locals3.asset.Instantiate();
						}
						else
						{
							visualElement2 = new Label(BaseVerticalCollectionView.k_InvalidTemplateError);
						}
						return visualElement2;
					};
				}
			}

			// Token: 0x04000619 RID: 1561
			private UxmlStringAttributeDescription m_Name = new UxmlStringAttributeDescription
			{
				name = "name"
			};

			// Token: 0x0400061A RID: 1562
			private UxmlStringAttributeDescription m_Text = new UxmlStringAttributeDescription
			{
				name = "title"
			};

			// Token: 0x0400061B RID: 1563
			private UxmlBoolAttributeDescription m_Visible = new UxmlBoolAttributeDescription
			{
				name = "visible",
				defaultValue = true
			};

			// Token: 0x0400061C RID: 1564
			private UxmlStringAttributeDescription m_Width = new UxmlStringAttributeDescription
			{
				name = "width"
			};

			// Token: 0x0400061D RID: 1565
			private UxmlStringAttributeDescription m_MinWidth = new UxmlStringAttributeDescription
			{
				name = "min-width"
			};

			// Token: 0x0400061E RID: 1566
			private UxmlStringAttributeDescription m_MaxWidth = new UxmlStringAttributeDescription
			{
				name = "max-width"
			};

			// Token: 0x0400061F RID: 1567
			private UxmlBoolAttributeDescription m_Stretch = new UxmlBoolAttributeDescription
			{
				name = "stretchable"
			};

			// Token: 0x04000620 RID: 1568
			private UxmlBoolAttributeDescription m_Sortable = new UxmlBoolAttributeDescription
			{
				name = "sortable",
				defaultValue = true
			};

			// Token: 0x04000621 RID: 1569
			private UxmlBoolAttributeDescription m_Optional = new UxmlBoolAttributeDescription
			{
				name = "optional",
				defaultValue = true
			};

			// Token: 0x04000622 RID: 1570
			private UxmlBoolAttributeDescription m_Resizable = new UxmlBoolAttributeDescription
			{
				name = "resizable",
				defaultValue = true
			};

			// Token: 0x04000623 RID: 1571
			private UxmlStringAttributeDescription m_HeaderTemplateId = new UxmlStringAttributeDescription
			{
				name = "header-template"
			};

			// Token: 0x04000624 RID: 1572
			private UxmlStringAttributeDescription m_CellTemplateId = new UxmlStringAttributeDescription
			{
				name = "cell-template"
			};

			// Token: 0x04000625 RID: 1573
			private UxmlStringAttributeDescription m_BindingPath = new UxmlStringAttributeDescription
			{
				name = "binding-path"
			};
		}
	}
}
