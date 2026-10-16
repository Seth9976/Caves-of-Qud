using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine.Internal;

namespace UnityEngine.UIElements
{
	// Token: 0x02000162 RID: 354
	[UxmlObject]
	public class SortColumnDescriptions : ICollection<SortColumnDescription>, IEnumerable<SortColumnDescription>, IEnumerable
	{
		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x06000B73 RID: 2931 RVA: 0x000329CD File Offset: 0x00030BCD
		private IList<SortColumnDescription> sortColumnDescriptions
		{
			get
			{
				return this.m_Descriptions;
			}
		}

		// Token: 0x1400003B RID: 59
		// (add) Token: 0x06000B74 RID: 2932 RVA: 0x000329D8 File Offset: 0x00030BD8
		// (remove) Token: 0x06000B75 RID: 2933 RVA: 0x00032A10 File Offset: 0x00030C10
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		internal event Action changed;

		// Token: 0x06000B76 RID: 2934 RVA: 0x00032A48 File Offset: 0x00030C48
		public IEnumerator<SortColumnDescription> GetEnumerator()
		{
			return this.m_Descriptions.GetEnumerator();
		}

		// Token: 0x06000B77 RID: 2935 RVA: 0x00032A68 File Offset: 0x00030C68
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000B78 RID: 2936 RVA: 0x00032A80 File Offset: 0x00030C80
		public void Add(SortColumnDescription item)
		{
			this.Insert(this.m_Descriptions.Count, item);
		}

		// Token: 0x06000B79 RID: 2937 RVA: 0x00032A98 File Offset: 0x00030C98
		public void Clear()
		{
			while (this.m_Descriptions.Count > 0)
			{
				this.Remove(this.m_Descriptions[0]);
			}
		}

		// Token: 0x06000B7A RID: 2938 RVA: 0x00032AD0 File Offset: 0x00030CD0
		public bool Contains(SortColumnDescription item)
		{
			return this.m_Descriptions.Contains(item);
		}

		// Token: 0x06000B7B RID: 2939 RVA: 0x00032AEE File Offset: 0x00030CEE
		public void CopyTo(SortColumnDescription[] array, int arrayIndex)
		{
			this.m_Descriptions.CopyTo(array, arrayIndex);
		}

		// Token: 0x06000B7C RID: 2940 RVA: 0x00032B00 File Offset: 0x00030D00
		public bool Remove(SortColumnDescription desc)
		{
			bool flag = desc == null;
			if (flag)
			{
				throw new ArgumentException("Cannot remove null description");
			}
			bool flag2 = this.m_Descriptions.Remove(desc);
			bool flag3;
			if (flag2)
			{
				desc.column = null;
				desc.changed -= this.OnDescriptionChanged;
				Action action = this.changed;
				if (action != null)
				{
					action();
				}
				flag3 = true;
			}
			else
			{
				flag3 = false;
			}
			return flag3;
		}

		// Token: 0x06000B7D RID: 2941 RVA: 0x00032B68 File Offset: 0x00030D68
		private void OnDescriptionChanged(SortColumnDescription desc)
		{
			Action action = this.changed;
			if (action != null)
			{
				action();
			}
		}

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x06000B7E RID: 2942 RVA: 0x00032B7D File Offset: 0x00030D7D
		public int Count
		{
			get
			{
				return this.m_Descriptions.Count;
			}
		}

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x06000B7F RID: 2943 RVA: 0x00032B8A File Offset: 0x00030D8A
		public bool IsReadOnly
		{
			get
			{
				return this.m_Descriptions.IsReadOnly;
			}
		}

		// Token: 0x06000B80 RID: 2944 RVA: 0x00032B98 File Offset: 0x00030D98
		public int IndexOf(SortColumnDescription desc)
		{
			return this.m_Descriptions.IndexOf(desc);
		}

		// Token: 0x06000B81 RID: 2945 RVA: 0x00032BB8 File Offset: 0x00030DB8
		public void Insert(int index, SortColumnDescription desc)
		{
			bool flag = desc == null;
			if (flag)
			{
				throw new ArgumentException("Cannot insert null description");
			}
			bool flag2 = this.Contains(desc);
			if (flag2)
			{
				throw new ArgumentException("Already contains this description");
			}
			this.m_Descriptions.Insert(index, desc);
			desc.changed += this.OnDescriptionChanged;
			Action action = this.changed;
			if (action != null)
			{
				action();
			}
		}

		// Token: 0x06000B82 RID: 2946 RVA: 0x00032C22 File Offset: 0x00030E22
		public void RemoveAt(int index)
		{
			this.Remove(this.m_Descriptions[index]);
		}

		// Token: 0x170001E9 RID: 489
		public SortColumnDescription this[int index]
		{
			get
			{
				return this.m_Descriptions[index];
			}
		}

		// Token: 0x040006C6 RID: 1734
		[SerializeField]
		private readonly IList<SortColumnDescription> m_Descriptions = new List<SortColumnDescription>();

		// Token: 0x02000163 RID: 355
		[ExcludeFromDocs]
		[Serializable]
		public class UxmlSerializedData : UnityEngine.UIElements.UxmlSerializedData
		{
			// Token: 0x06000B85 RID: 2949 RVA: 0x00032C6A File Offset: 0x00030E6A
			[Conditional("UNITY_EDITOR")]
			public new static void Register()
			{
				UxmlDescriptionCache.RegisterType(typeof(SortColumnDescriptions.UxmlSerializedData), new UxmlAttributeNames[]
				{
					new UxmlAttributeNames("sortColumnDescriptions", "sort-column-descriptions", null, Array.Empty<string>())
				});
			}

			// Token: 0x06000B86 RID: 2950 RVA: 0x00032C9F File Offset: 0x00030E9F
			public override object CreateInstance()
			{
				return new SortColumnDescriptions();
			}

			// Token: 0x06000B87 RID: 2951 RVA: 0x00032CA8 File Offset: 0x00030EA8
			public override void Deserialize(object obj)
			{
				bool flag = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.sortColumnDescriptions_UxmlAttributeFlags) && this.sortColumnDescriptions != null;
				if (flag)
				{
					SortColumnDescriptions sortColumnDescriptions = (SortColumnDescriptions)obj;
					foreach (SortColumnDescription.UxmlSerializedData uxmlSerializedData in this.sortColumnDescriptions)
					{
						SortColumnDescription sortColumnDescription = (SortColumnDescription)uxmlSerializedData.CreateInstance();
						uxmlSerializedData.Deserialize(sortColumnDescription);
						sortColumnDescriptions.Add(sortColumnDescription);
					}
				}
			}

			// Token: 0x040006C8 RID: 1736
			[UxmlObjectReference]
			[SerializeReference]
			private List<SortColumnDescription.UxmlSerializedData> sortColumnDescriptions;

			// Token: 0x040006C9 RID: 1737
			[SerializeField]
			[UxmlIgnore]
			[HideInInspector]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags sortColumnDescriptions_UxmlAttributeFlags;
		}

		// Token: 0x02000164 RID: 356
		[Obsolete("UxmlObjectFactory<T> is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		internal class UxmlObjectFactory<T> : UxmlObjectFactory<T, SortColumnDescriptions.UxmlObjectTraits<T>> where T : SortColumnDescriptions, new()
		{
		}

		// Token: 0x02000165 RID: 357
		[Obsolete("UxmlObjectFactory is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		internal class UxmlObjectFactory : SortColumnDescriptions.UxmlObjectFactory<SortColumnDescriptions>
		{
		}

		// Token: 0x02000166 RID: 358
		[Obsolete("UxmlObjectTraits<T> is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		internal class UxmlObjectTraits<T> : UnityEngine.UIElements.UxmlObjectTraits<T> where T : SortColumnDescriptions
		{
			// Token: 0x06000B8B RID: 2955 RVA: 0x00032D54 File Offset: 0x00030F54
			public override void Init(ref T obj, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ref obj, bag, cc);
				List<SortColumnDescription> valueFromBag = this.m_SortColumnDescriptions.GetValueFromBag(bag, cc);
				bool flag = valueFromBag != null;
				if (flag)
				{
					foreach (SortColumnDescription sortColumnDescription in valueFromBag)
					{
						obj.Add(sortColumnDescription);
					}
				}
			}

			// Token: 0x040006CA RID: 1738
			private readonly UxmlObjectListAttributeDescription<SortColumnDescription> m_SortColumnDescriptions = new UxmlObjectListAttributeDescription<SortColumnDescription>();
		}
	}
}
