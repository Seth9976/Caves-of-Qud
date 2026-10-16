using System;
using System.Diagnostics;
using Unity.Properties;
using UnityEngine.Bindings;
using UnityEngine.Internal;

namespace UnityEngine.UIElements
{
	// Token: 0x020000FF RID: 255
	public class GroupBox : BindableElement, IGroupBox
	{
		// Token: 0x1700012F RID: 303
		// (get) Token: 0x0600084A RID: 2122 RVA: 0x00024D58 File Offset: 0x00022F58
		internal Label titleLabel
		{
			[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
			get
			{
				return this.m_TitleLabel;
			}
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x0600084B RID: 2123 RVA: 0x00024D60 File Offset: 0x00022F60
		// (set) Token: 0x0600084C RID: 2124 RVA: 0x00024D74 File Offset: 0x00022F74
		[CreateProperty]
		public string text
		{
			get
			{
				Label titleLabel = this.m_TitleLabel;
				return (titleLabel != null) ? titleLabel.text : null;
			}
			set
			{
				string text = this.text;
				bool flag = !string.IsNullOrEmpty(value);
				if (flag)
				{
					bool flag2 = this.m_TitleLabel == null;
					if (flag2)
					{
						this.m_TitleLabel = new Label(value);
						this.m_TitleLabel.AddToClassList(GroupBox.labelUssClassName);
						base.Insert(0, this.m_TitleLabel);
					}
					this.m_TitleLabel.text = value;
				}
				else
				{
					bool flag3 = this.m_TitleLabel != null;
					if (flag3)
					{
						this.m_TitleLabel.RemoveFromHierarchy();
						this.m_TitleLabel = null;
					}
				}
				bool flag4 = string.CompareOrdinal(text, this.text) != 0;
				if (flag4)
				{
					base.NotifyPropertyChanged(in GroupBox.textProperty);
				}
			}
		}

		// Token: 0x0600084D RID: 2125 RVA: 0x00024E24 File Offset: 0x00023024
		public GroupBox()
			: this(null)
		{
		}

		// Token: 0x0600084E RID: 2126 RVA: 0x00024E2F File Offset: 0x0002302F
		public GroupBox(string text)
		{
			base.AddToClassList(GroupBox.ussClassName);
			this.text = text;
		}

		// Token: 0x0600084F RID: 2127 RVA: 0x00002152 File Offset: 0x00000352
		void IGroupBox.OnOptionAdded(IGroupBoxOption option)
		{
		}

		// Token: 0x06000850 RID: 2128 RVA: 0x00002152 File Offset: 0x00000352
		void IGroupBox.OnOptionRemoved(IGroupBoxOption option)
		{
		}

		// Token: 0x040004AF RID: 1199
		internal static readonly BindingId textProperty = "text";

		// Token: 0x040004B0 RID: 1200
		public static readonly string ussClassName = "unity-group-box";

		// Token: 0x040004B1 RID: 1201
		public static readonly string labelUssClassName = GroupBox.ussClassName + "__label";

		// Token: 0x040004B2 RID: 1202
		private Label m_TitleLabel;

		// Token: 0x02000100 RID: 256
		[ExcludeFromDocs]
		[Serializable]
		public new class UxmlSerializedData : BindableElement.UxmlSerializedData
		{
			// Token: 0x06000852 RID: 2130 RVA: 0x00024E7C File Offset: 0x0002307C
			[Conditional("UNITY_EDITOR")]
			public new static void Register()
			{
				UxmlDescriptionCache.RegisterType(typeof(GroupBox.UxmlSerializedData), new UxmlAttributeNames[]
				{
					new UxmlAttributeNames("text", "text", null, Array.Empty<string>())
				});
			}

			// Token: 0x06000853 RID: 2131 RVA: 0x00024EB1 File Offset: 0x000230B1
			public override object CreateInstance()
			{
				return new GroupBox();
			}

			// Token: 0x06000854 RID: 2132 RVA: 0x00024EB8 File Offset: 0x000230B8
			public override void Deserialize(object obj)
			{
				base.Deserialize(obj);
				bool flag = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.text_UxmlAttributeFlags);
				if (flag)
				{
					GroupBox groupBox = (GroupBox)obj;
					groupBox.text = this.text;
				}
			}

			// Token: 0x040004B3 RID: 1203
			[MultilineTextField]
			[SerializeField]
			private string text;

			// Token: 0x040004B4 RID: 1204
			[HideInInspector]
			[SerializeField]
			[UxmlIgnore]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags text_UxmlAttributeFlags;
		}

		// Token: 0x02000101 RID: 257
		[Obsolete("UxmlFactory is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlFactory : UxmlFactory<GroupBox, GroupBox.UxmlTraits>
		{
		}

		// Token: 0x02000102 RID: 258
		[Obsolete("UxmlTraits is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlTraits : BindableElement.UxmlTraits
		{
			// Token: 0x06000857 RID: 2135 RVA: 0x00024EFC File Offset: 0x000230FC
			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ve, bag, cc);
				((GroupBox)ve).text = this.m_Text.GetValueFromBag(bag, cc);
			}

			// Token: 0x040004B5 RID: 1205
			private UxmlStringAttributeDescription m_Text = new UxmlStringAttributeDescription
			{
				name = "text"
			};
		}
	}
}
