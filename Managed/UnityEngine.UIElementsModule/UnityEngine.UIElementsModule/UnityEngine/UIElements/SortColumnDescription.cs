using System;
using System.Diagnostics;
using Unity.Properties;
using UnityEngine.Internal;

namespace UnityEngine.UIElements
{
	// Token: 0x0200015D RID: 349
	[UxmlObject]
	[Serializable]
	public class SortColumnDescription : INotifyBindablePropertyChanged
	{
		// Token: 0x14000039 RID: 57
		// (add) Token: 0x06000B5A RID: 2906 RVA: 0x0003258C File Offset: 0x0003078C
		// (remove) Token: 0x06000B5B RID: 2907 RVA: 0x000325C4 File Offset: 0x000307C4
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<BindablePropertyChangedEventArgs> propertyChanged;

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x06000B5C RID: 2908 RVA: 0x000325F9 File Offset: 0x000307F9
		// (set) Token: 0x06000B5D RID: 2909 RVA: 0x00032604 File Offset: 0x00030804
		[CreateProperty]
		public string columnName
		{
			get
			{
				return this.m_ColumnName;
			}
			set
			{
				bool flag = this.m_ColumnName == value;
				if (!flag)
				{
					this.m_ColumnName = value;
					Action<SortColumnDescription> action = this.changed;
					if (action != null)
					{
						action(this);
					}
					this.NotifyPropertyChanged(in SortColumnDescription.columnNameProperty);
				}
			}
		}

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x06000B5E RID: 2910 RVA: 0x0003264A File Offset: 0x0003084A
		// (set) Token: 0x06000B5F RID: 2911 RVA: 0x00032654 File Offset: 0x00030854
		[CreateProperty]
		public int columnIndex
		{
			get
			{
				return this.m_ColumnIndex;
			}
			set
			{
				bool flag = this.m_ColumnIndex == value;
				if (!flag)
				{
					this.m_ColumnIndex = value;
					Action<SortColumnDescription> action = this.changed;
					if (action != null)
					{
						action(this);
					}
					this.NotifyPropertyChanged(in SortColumnDescription.columnIndexProperty);
				}
			}
		}

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x06000B60 RID: 2912 RVA: 0x00032697 File Offset: 0x00030897
		// (set) Token: 0x06000B61 RID: 2913 RVA: 0x0003269F File Offset: 0x0003089F
		public Column column { get; internal set; }

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x06000B62 RID: 2914 RVA: 0x000326A8 File Offset: 0x000308A8
		// (set) Token: 0x06000B63 RID: 2915 RVA: 0x000326B0 File Offset: 0x000308B0
		[CreateProperty]
		public SortDirection direction
		{
			get
			{
				return this.m_SortDirection;
			}
			set
			{
				bool flag = this.m_SortDirection == value;
				if (!flag)
				{
					this.m_SortDirection = value;
					Action<SortColumnDescription> action = this.changed;
					if (action != null)
					{
						action(this);
					}
					this.NotifyPropertyChanged(in SortColumnDescription.directionProperty);
				}
			}
		}

		// Token: 0x1400003A RID: 58
		// (add) Token: 0x06000B64 RID: 2916 RVA: 0x000326F4 File Offset: 0x000308F4
		// (remove) Token: 0x06000B65 RID: 2917 RVA: 0x0003272C File Offset: 0x0003092C
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		internal event Action<SortColumnDescription> changed;

		// Token: 0x06000B66 RID: 2918 RVA: 0x00032761 File Offset: 0x00030961
		public SortColumnDescription()
		{
		}

		// Token: 0x06000B67 RID: 2919 RVA: 0x00032772 File Offset: 0x00030972
		public SortColumnDescription(int columnIndex, SortDirection direction)
		{
			this.columnIndex = columnIndex;
			this.direction = direction;
		}

		// Token: 0x06000B68 RID: 2920 RVA: 0x00032793 File Offset: 0x00030993
		public SortColumnDescription(string columnName, SortDirection direction)
		{
			this.columnName = columnName;
			this.direction = direction;
		}

		// Token: 0x06000B69 RID: 2921 RVA: 0x000327B4 File Offset: 0x000309B4
		private void NotifyPropertyChanged(in BindingId property)
		{
			EventHandler<BindablePropertyChangedEventArgs> eventHandler = this.propertyChanged;
			if (eventHandler != null)
			{
				eventHandler(this, new BindablePropertyChangedEventArgs(in property));
			}
		}

		// Token: 0x040006B4 RID: 1716
		private static readonly BindingId columnNameProperty = "columnName";

		// Token: 0x040006B5 RID: 1717
		private static readonly BindingId columnIndexProperty = "columnIndex";

		// Token: 0x040006B6 RID: 1718
		private static readonly BindingId directionProperty = "direction";

		// Token: 0x040006B7 RID: 1719
		[SerializeField]
		private int m_ColumnIndex = -1;

		// Token: 0x040006B8 RID: 1720
		[SerializeField]
		private string m_ColumnName;

		// Token: 0x040006B9 RID: 1721
		[SerializeField]
		private SortDirection m_SortDirection;

		// Token: 0x0200015E RID: 350
		[ExcludeFromDocs]
		[Serializable]
		public class UxmlSerializedData : UnityEngine.UIElements.UxmlSerializedData
		{
			// Token: 0x06000B6B RID: 2923 RVA: 0x00032800 File Offset: 0x00030A00
			[Conditional("UNITY_EDITOR")]
			public new static void Register()
			{
				UxmlDescriptionCache.RegisterType(typeof(SortColumnDescription.UxmlSerializedData), new UxmlAttributeNames[]
				{
					new UxmlAttributeNames("columnName", "column-name", null, Array.Empty<string>()),
					new UxmlAttributeNames("columnIndex", "column-index", null, Array.Empty<string>()),
					new UxmlAttributeNames("direction", "direction", null, Array.Empty<string>())
				});
			}

			// Token: 0x06000B6C RID: 2924 RVA: 0x00032878 File Offset: 0x00030A78
			public override object CreateInstance()
			{
				return new SortColumnDescription();
			}

			// Token: 0x06000B6D RID: 2925 RVA: 0x00032880 File Offset: 0x00030A80
			public override void Deserialize(object obj)
			{
				SortColumnDescription sortColumnDescription = (SortColumnDescription)obj;
				bool flag = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.columnName_UxmlAttributeFlags);
				if (flag)
				{
					sortColumnDescription.columnName = this.columnName;
				}
				bool flag2 = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.columnIndex_UxmlAttributeFlags);
				if (flag2)
				{
					sortColumnDescription.columnIndex = this.columnIndex;
				}
				bool flag3 = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.direction_UxmlAttributeFlags);
				if (flag3)
				{
					sortColumnDescription.direction = this.direction;
				}
			}

			// Token: 0x040006BD RID: 1725
			[SerializeField]
			private string columnName;

			// Token: 0x040006BE RID: 1726
			[SerializeField]
			private int columnIndex;

			// Token: 0x040006BF RID: 1727
			[SerializeField]
			private SortDirection direction;

			// Token: 0x040006C0 RID: 1728
			[SerializeField]
			[UxmlIgnore]
			[HideInInspector]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags columnName_UxmlAttributeFlags;

			// Token: 0x040006C1 RID: 1729
			[UxmlIgnore]
			[HideInInspector]
			[SerializeField]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags columnIndex_UxmlAttributeFlags;

			// Token: 0x040006C2 RID: 1730
			[UxmlIgnore]
			[HideInInspector]
			[SerializeField]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags direction_UxmlAttributeFlags;
		}

		// Token: 0x0200015F RID: 351
		[Obsolete("UxmlObjectFactory<T> is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		internal class UxmlObjectFactory<T> : UxmlObjectFactory<T, SortColumnDescription.UxmlObjectTraits<T>> where T : SortColumnDescription, new()
		{
		}

		// Token: 0x02000160 RID: 352
		[Obsolete("UxmlObjectFactory<T> is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		internal class UxmlObjectFactory : SortColumnDescription.UxmlObjectFactory<SortColumnDescription>
		{
		}

		// Token: 0x02000161 RID: 353
		[Obsolete("UxmlObjectTraits<T> is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		internal class UxmlObjectTraits<T> : UnityEngine.UIElements.UxmlObjectTraits<T> where T : SortColumnDescription
		{
			// Token: 0x06000B71 RID: 2929 RVA: 0x000328FC File Offset: 0x00030AFC
			public override void Init(ref T obj, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ref obj, bag, cc);
				obj.columnName = this.m_ColumnName.GetValueFromBag(bag, cc);
				obj.columnIndex = this.m_ColumnIndex.GetValueFromBag(bag, cc);
				obj.direction = this.m_SortDescription.GetValueFromBag(bag, cc);
			}

			// Token: 0x040006C3 RID: 1731
			private readonly UxmlStringAttributeDescription m_ColumnName = new UxmlStringAttributeDescription
			{
				name = "column-name"
			};

			// Token: 0x040006C4 RID: 1732
			private readonly UxmlIntAttributeDescription m_ColumnIndex = new UxmlIntAttributeDescription
			{
				name = "column-index",
				defaultValue = -1
			};

			// Token: 0x040006C5 RID: 1733
			private readonly UxmlEnumAttributeDescription<SortDirection> m_SortDescription = new UxmlEnumAttributeDescription<SortDirection>
			{
				name = "direction",
				defaultValue = SortDirection.Ascending
			};
		}
	}
}
