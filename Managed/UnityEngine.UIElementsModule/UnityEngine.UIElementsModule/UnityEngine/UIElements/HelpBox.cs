using System;
using System.Diagnostics;
using Unity.Properties;
using UnityEngine.Internal;

namespace UnityEngine.UIElements
{
	// Token: 0x02000109 RID: 265
	public class HelpBox : VisualElement
	{
		// Token: 0x17000136 RID: 310
		// (get) Token: 0x06000873 RID: 2163 RVA: 0x000252A8 File Offset: 0x000234A8
		// (set) Token: 0x06000874 RID: 2164 RVA: 0x000252C8 File Offset: 0x000234C8
		[CreateProperty]
		public string text
		{
			get
			{
				return this.m_Label.text;
			}
			set
			{
				string text = this.text;
				this.m_Label.text = value;
				bool flag = string.CompareOrdinal(text, this.text) != 0;
				if (flag)
				{
					base.NotifyPropertyChanged(in HelpBox.textProperty);
				}
			}
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x06000875 RID: 2165 RVA: 0x0002530C File Offset: 0x0002350C
		// (set) Token: 0x06000876 RID: 2166 RVA: 0x00025324 File Offset: 0x00023524
		[CreateProperty]
		public HelpBoxMessageType messageType
		{
			get
			{
				return this.m_HelpBoxMessageType;
			}
			set
			{
				bool flag = value != this.m_HelpBoxMessageType;
				if (flag)
				{
					this.m_HelpBoxMessageType = value;
					this.UpdateIcon(value);
					base.NotifyPropertyChanged(in HelpBox.messageTypeProperty);
				}
			}
		}

		// Token: 0x06000877 RID: 2167 RVA: 0x0002535F File Offset: 0x0002355F
		public HelpBox()
			: this(string.Empty, HelpBoxMessageType.None)
		{
		}

		// Token: 0x06000878 RID: 2168 RVA: 0x00025370 File Offset: 0x00023570
		public HelpBox(string text, HelpBoxMessageType messageType)
		{
			base.AddToClassList(HelpBox.ussClassName);
			this.m_HelpBoxMessageType = messageType;
			this.m_Label = new Label(text);
			this.m_Label.AddToClassList(HelpBox.labelUssClassName);
			base.Add(this.m_Label);
			this.m_Icon = new VisualElement();
			this.m_Icon.AddToClassList(HelpBox.iconUssClassName);
			this.UpdateIcon(messageType);
		}

		// Token: 0x06000879 RID: 2169 RVA: 0x000253E8 File Offset: 0x000235E8
		private string GetIconClass(HelpBoxMessageType messageType)
		{
			string text;
			switch (messageType)
			{
			case HelpBoxMessageType.Info:
				text = HelpBox.iconInfoUssClassName;
				break;
			case HelpBoxMessageType.Warning:
				text = HelpBox.iconwarningUssClassName;
				break;
			case HelpBoxMessageType.Error:
				text = HelpBox.iconErrorUssClassName;
				break;
			default:
				text = null;
				break;
			}
			return text;
		}

		// Token: 0x0600087A RID: 2170 RVA: 0x00025430 File Offset: 0x00023630
		private void UpdateIcon(HelpBoxMessageType messageType)
		{
			bool flag = !string.IsNullOrEmpty(this.m_IconClass);
			if (flag)
			{
				this.m_Icon.RemoveFromClassList(this.m_IconClass);
			}
			this.m_IconClass = this.GetIconClass(messageType);
			bool flag2 = this.m_IconClass == null;
			if (flag2)
			{
				this.m_Icon.RemoveFromHierarchy();
			}
			else
			{
				this.m_Icon.AddToClassList(this.m_IconClass);
				bool flag3 = this.m_Icon.parent == null;
				if (flag3)
				{
					base.Insert(0, this.m_Icon);
				}
			}
		}

		// Token: 0x040004BE RID: 1214
		internal static readonly BindingId textProperty = "text";

		// Token: 0x040004BF RID: 1215
		internal static readonly BindingId messageTypeProperty = "messageType";

		// Token: 0x040004C0 RID: 1216
		public static readonly string ussClassName = "unity-help-box";

		// Token: 0x040004C1 RID: 1217
		public static readonly string labelUssClassName = HelpBox.ussClassName + "__label";

		// Token: 0x040004C2 RID: 1218
		public static readonly string iconUssClassName = HelpBox.ussClassName + "__icon";

		// Token: 0x040004C3 RID: 1219
		public static readonly string iconInfoUssClassName = HelpBox.iconUssClassName + "--info";

		// Token: 0x040004C4 RID: 1220
		public static readonly string iconwarningUssClassName = HelpBox.iconUssClassName + "--warning";

		// Token: 0x040004C5 RID: 1221
		public static readonly string iconErrorUssClassName = HelpBox.iconUssClassName + "--error";

		// Token: 0x040004C6 RID: 1222
		private HelpBoxMessageType m_HelpBoxMessageType;

		// Token: 0x040004C7 RID: 1223
		private VisualElement m_Icon;

		// Token: 0x040004C8 RID: 1224
		private string m_IconClass;

		// Token: 0x040004C9 RID: 1225
		private Label m_Label;

		// Token: 0x0200010A RID: 266
		[ExcludeFromDocs]
		[Serializable]
		public new class UxmlSerializedData : VisualElement.UxmlSerializedData
		{
			// Token: 0x0600087C RID: 2172 RVA: 0x00025560 File Offset: 0x00023760
			[Conditional("UNITY_EDITOR")]
			public new static void Register()
			{
				UxmlDescriptionCache.RegisterType(typeof(HelpBox.UxmlSerializedData), new UxmlAttributeNames[]
				{
					new UxmlAttributeNames("text", "text", null, Array.Empty<string>()),
					new UxmlAttributeNames("messageType", "message-type", null, Array.Empty<string>())
				});
			}

			// Token: 0x0600087D RID: 2173 RVA: 0x000255BC File Offset: 0x000237BC
			public override object CreateInstance()
			{
				return new HelpBox();
			}

			// Token: 0x0600087E RID: 2174 RVA: 0x000255C4 File Offset: 0x000237C4
			public override void Deserialize(object obj)
			{
				base.Deserialize(obj);
				HelpBox helpBox = (HelpBox)obj;
				bool flag = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.text_UxmlAttributeFlags);
				if (flag)
				{
					helpBox.text = this.text;
				}
				bool flag2 = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.messageType_UxmlAttributeFlags);
				if (flag2)
				{
					helpBox.messageType = this.messageType;
				}
			}

			// Token: 0x040004CA RID: 1226
			[MultilineTextField]
			[SerializeField]
			private string text;

			// Token: 0x040004CB RID: 1227
			[HideInInspector]
			[UxmlIgnore]
			[SerializeField]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags text_UxmlAttributeFlags;

			// Token: 0x040004CC RID: 1228
			[SerializeField]
			private HelpBoxMessageType messageType;

			// Token: 0x040004CD RID: 1229
			[SerializeField]
			[UxmlIgnore]
			[HideInInspector]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags messageType_UxmlAttributeFlags;
		}

		// Token: 0x0200010B RID: 267
		[Obsolete("UxmlFactory is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlFactory : UxmlFactory<HelpBox, HelpBox.UxmlTraits>
		{
		}

		// Token: 0x0200010C RID: 268
		[Obsolete("UxmlTraits is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlTraits : VisualElement.UxmlTraits
		{
			// Token: 0x06000881 RID: 2177 RVA: 0x00025624 File Offset: 0x00023824
			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ve, bag, cc);
				HelpBox helpBox = ve as HelpBox;
				helpBox.text = this.m_Text.GetValueFromBag(bag, cc);
				helpBox.messageType = this.m_MessageType.GetValueFromBag(bag, cc);
			}

			// Token: 0x040004CE RID: 1230
			private UxmlStringAttributeDescription m_Text = new UxmlStringAttributeDescription
			{
				name = "text"
			};

			// Token: 0x040004CF RID: 1231
			private UxmlEnumAttributeDescription<HelpBoxMessageType> m_MessageType = new UxmlEnumAttributeDescription<HelpBoxMessageType>
			{
				name = "message-type",
				defaultValue = HelpBoxMessageType.None
			};
		}
	}
}
