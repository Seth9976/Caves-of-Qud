using System;
using System.Diagnostics;
using Unity.Properties;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.UIElements
{
	// Token: 0x020000EC RID: 236
	[MovedFrom(true, "UnityEditor.UIElements", "UnityEditor.UIElementsModule", null)]
	public class EnumField : BaseField<Enum>
	{
		// Token: 0x1700011A RID: 282
		// (get) Token: 0x060007C8 RID: 1992 RVA: 0x0002254B File Offset: 0x0002074B
		internal Type type
		{
			[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
			get
			{
				return this.m_EnumType;
			}
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x060007C9 RID: 1993 RVA: 0x00022553 File Offset: 0x00020753
		// (set) Token: 0x060007CA RID: 1994 RVA: 0x0002255B File Offset: 0x0002075B
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal bool includeObsoleteValues
		{
			get
			{
				return this.m_IncludeObsoleteValues;
			}
			set
			{
				this.m_IncludeObsoleteValues = value;
			}
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x060007CB RID: 1995 RVA: 0x00022564 File Offset: 0x00020764
		// (set) Token: 0x060007CC RID: 1996 RVA: 0x00022574 File Offset: 0x00020774
		internal string typeAsString
		{
			get
			{
				return UxmlUtility.TypeToString(this.m_EnumType);
			}
			[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
			set
			{
				this.m_EnumType = UxmlUtility.ParseType(value, null);
				bool flag = this.m_EnumType == null;
				if (flag)
				{
					this.value = null;
					this.m_TextElement.text = string.Empty;
				}
			}
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x060007CD RID: 1997 RVA: 0x000225BA File Offset: 0x000207BA
		// (set) Token: 0x060007CE RID: 1998 RVA: 0x000225D0 File Offset: 0x000207D0
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal string valueAsString
		{
			get
			{
				Enum value = this.value;
				return (value != null) ? value.ToString() : null;
			}
			set
			{
				bool flag = this.type != null;
				if (flag)
				{
					bool flag2 = !string.IsNullOrEmpty(value);
					if (flag2)
					{
						object obj;
						Enum @enum;
						bool flag3;
						if (Enum.TryParse(this.type, value, false, out obj))
						{
							@enum = obj as Enum;
							flag3 = @enum != null;
						}
						else
						{
							flag3 = false;
						}
						bool flag4 = flag3;
						if (flag4)
						{
							this.Init(@enum, this.includeObsoleteValues);
						}
						else
						{
							this.PopulateDataFromType(this.type);
							this.value = null;
						}
					}
					else
					{
						Enum enum2 = (Enum)Enum.ToObject(this.type, 0);
						this.Init(enum2, this.includeObsoleteValues);
					}
				}
				else
				{
					this.value = null;
				}
			}
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x060007CF RID: 1999 RVA: 0x00022684 File Offset: 0x00020884
		[CreateProperty(ReadOnly = true)]
		public string text
		{
			get
			{
				return this.m_TextElement.text;
			}
		}

		// Token: 0x060007D0 RID: 2000 RVA: 0x000226A4 File Offset: 0x000208A4
		private void Initialize(Enum defaultValue)
		{
			this.m_TextElement = new TextElement();
			this.m_TextElement.AddToClassList(EnumField.textUssClassName);
			this.m_TextElement.pickingMode = PickingMode.Ignore;
			base.visualInput.Add(this.m_TextElement);
			this.m_ArrowElement = new VisualElement();
			this.m_ArrowElement.AddToClassList(EnumField.arrowUssClassName);
			this.m_ArrowElement.pickingMode = PickingMode.Ignore;
			base.visualInput.Add(this.m_ArrowElement);
			bool flag = defaultValue != null;
			if (flag)
			{
				this.Init(defaultValue);
			}
		}

		// Token: 0x060007D1 RID: 2001 RVA: 0x0002273A File Offset: 0x0002093A
		public EnumField()
			: this(null, null)
		{
		}

		// Token: 0x060007D2 RID: 2002 RVA: 0x00022746 File Offset: 0x00020946
		public EnumField(Enum defaultValue)
			: this(null, defaultValue)
		{
		}

		// Token: 0x060007D3 RID: 2003 RVA: 0x00022754 File Offset: 0x00020954
		public EnumField(string label, Enum defaultValue = null)
			: base(label, null)
		{
			base.AddToClassList(EnumField.ussClassName);
			base.labelElement.AddToClassList(EnumField.labelUssClassName);
			base.visualInput.AddToClassList(EnumField.inputUssClassName);
			this.Initialize(defaultValue);
			base.RegisterCallback<PointerDownEvent>(new EventCallback<PointerDownEvent>(this.OnPointerDownEvent), TrickleDown.NoTrickleDown);
			base.RegisterCallback<PointerMoveEvent>(new EventCallback<PointerMoveEvent>(this.OnPointerMoveEvent), TrickleDown.NoTrickleDown);
			base.RegisterCallback<MouseDownEvent>(delegate(MouseDownEvent e)
			{
				bool flag = e.button == 0;
				if (flag)
				{
					e.StopPropagation();
				}
			}, TrickleDown.NoTrickleDown);
			base.RegisterCallback<NavigationSubmitEvent>(new EventCallback<NavigationSubmitEvent>(this.OnNavigationSubmit), TrickleDown.NoTrickleDown);
		}

		// Token: 0x060007D4 RID: 2004 RVA: 0x00022804 File Offset: 0x00020A04
		public void Init(Enum defaultValue)
		{
			this.Init(defaultValue, false);
		}

		// Token: 0x060007D5 RID: 2005 RVA: 0x00022810 File Offset: 0x00020A10
		public void Init(Enum defaultValue, bool includeObsoleteValues)
		{
			bool flag = defaultValue == null;
			if (flag)
			{
				throw new ArgumentNullException("defaultValue");
			}
			this.m_IncludeObsoleteValues = includeObsoleteValues;
			this.PopulateDataFromType(defaultValue.GetType());
			bool flag2 = !object.Equals(base.rawValue, defaultValue);
			if (flag2)
			{
				this.SetValueWithoutNotify(defaultValue);
			}
			else
			{
				this.UpdateValueLabel(defaultValue);
			}
		}

		// Token: 0x060007D6 RID: 2006 RVA: 0x0002286B File Offset: 0x00020A6B
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal void PopulateDataFromType(Type enumType)
		{
			this.m_EnumType = enumType;
			this.m_EnumData = EnumDataUtility.GetCachedEnumData(this.m_EnumType, this.includeObsoleteValues ? EnumDataUtility.CachedType.IncludeObsoleteExceptErrors : EnumDataUtility.CachedType.ExcludeObsolete, new Func<string, string>(NameFormatter.FormatVariableName));
		}

		// Token: 0x060007D7 RID: 2007 RVA: 0x000228A0 File Offset: 0x00020AA0
		public override void SetValueWithoutNotify(Enum newValue)
		{
			bool flag = !object.Equals(base.rawValue, newValue);
			if (flag)
			{
				base.SetValueWithoutNotify(newValue);
				bool flag2 = this.m_EnumType == null;
				if (!flag2)
				{
					this.UpdateValueLabel(newValue);
				}
			}
		}

		// Token: 0x060007D8 RID: 2008 RVA: 0x000228E8 File Offset: 0x00020AE8
		private void UpdateValueLabel(Enum value)
		{
			int num = Array.IndexOf<Enum>(this.m_EnumData.values, value);
			bool flag = (num >= 0) & (num < this.m_EnumData.values.Length);
			if (flag)
			{
				this.m_TextElement.text = this.m_EnumData.displayNames[num];
			}
			else
			{
				this.m_TextElement.text = string.Empty;
			}
		}

		// Token: 0x060007D9 RID: 2009 RVA: 0x00022954 File Offset: 0x00020B54
		private void OnPointerDownEvent(PointerDownEvent evt)
		{
			this.ProcessPointerDown<PointerDownEvent>(evt);
		}

		// Token: 0x060007DA RID: 2010 RVA: 0x00022960 File Offset: 0x00020B60
		private void OnPointerMoveEvent(PointerMoveEvent evt)
		{
			bool flag = evt.button == 0;
			if (flag)
			{
				bool flag2 = (evt.pressedButtons & 1) != 0;
				if (flag2)
				{
					this.ProcessPointerDown<PointerMoveEvent>(evt);
				}
			}
		}

		// Token: 0x060007DB RID: 2011 RVA: 0x00022998 File Offset: 0x00020B98
		private bool ContainsPointer(int pointerId)
		{
			VisualElement topElementUnderPointer = base.elementPanel.GetTopElementUnderPointer(pointerId);
			return this == topElementUnderPointer || base.visualInput == topElementUnderPointer;
		}

		// Token: 0x060007DC RID: 2012 RVA: 0x000229C8 File Offset: 0x00020BC8
		private void ProcessPointerDown<T>(PointerEventBase<T> evt) where T : PointerEventBase<T>, new()
		{
			bool flag = evt.button == 0;
			if (flag)
			{
				bool flag2 = this.ContainsPointer(evt.pointerId);
				if (flag2)
				{
					base.schedule.Execute(new Action(this.ShowMenu));
					evt.StopPropagation();
				}
			}
		}

		// Token: 0x060007DD RID: 2013 RVA: 0x00022A16 File Offset: 0x00020C16
		private void OnNavigationSubmit(NavigationSubmitEvent evt)
		{
			this.ShowMenu();
			evt.StopPropagation();
		}

		// Token: 0x060007DE RID: 2014 RVA: 0x00022A28 File Offset: 0x00020C28
		internal void ShowMenu()
		{
			bool flag = this.m_EnumType == null;
			if (!flag)
			{
				IGenericMenu genericMenu = ((this.createMenuCallback != null) ? this.createMenuCallback() : base.elementPanel.CreateMenu());
				int num = Array.IndexOf<Enum>(this.m_EnumData.values, this.value);
				for (int i = 0; i < this.m_EnumData.values.Length; i++)
				{
					bool flag2 = num == i;
					genericMenu.AddItem(this.m_EnumData.displayNames[i], flag2, delegate(object contentView)
					{
						this.ChangeValueFromMenu(contentView);
					}, this.m_EnumData.values[i]);
				}
				genericMenu.DropDown(base.visualInput.worldBound, this, true);
			}
		}

		// Token: 0x060007DF RID: 2015 RVA: 0x00022AEC File Offset: 0x00020CEC
		private void ChangeValueFromMenu(object menuItem)
		{
			this.value = menuItem as Enum;
		}

		// Token: 0x060007E0 RID: 2016 RVA: 0x00022AFC File Offset: 0x00020CFC
		protected override void UpdateMixedValueContent()
		{
			bool showMixedValue = base.showMixedValue;
			if (showMixedValue)
			{
				this.m_TextElement.text = BaseField<Enum>.mixedValueString;
			}
			else
			{
				this.UpdateValueLabel(this.value);
			}
			this.m_TextElement.EnableInClassList(EnumField.labelUssClassName, base.showMixedValue);
			this.m_TextElement.EnableInClassList(BaseField<Enum>.mixedValueLabelUssClassName, base.showMixedValue);
		}

		// Token: 0x0400045B RID: 1115
		internal static readonly BindingId textProperty = "text";

		// Token: 0x0400045C RID: 1116
		private Type m_EnumType;

		// Token: 0x0400045D RID: 1117
		private bool m_IncludeObsoleteValues;

		// Token: 0x0400045E RID: 1118
		private TextElement m_TextElement;

		// Token: 0x0400045F RID: 1119
		private VisualElement m_ArrowElement;

		// Token: 0x04000460 RID: 1120
		private EnumData m_EnumData;

		// Token: 0x04000461 RID: 1121
		internal Func<IGenericMenu> createMenuCallback;

		// Token: 0x04000462 RID: 1122
		public new static readonly string ussClassName = "unity-enum-field";

		// Token: 0x04000463 RID: 1123
		public static readonly string textUssClassName = EnumField.ussClassName + "__text";

		// Token: 0x04000464 RID: 1124
		public static readonly string arrowUssClassName = EnumField.ussClassName + "__arrow";

		// Token: 0x04000465 RID: 1125
		public new static readonly string labelUssClassName = EnumField.ussClassName + "__label";

		// Token: 0x04000466 RID: 1126
		public new static readonly string inputUssClassName = EnumField.ussClassName + "__input";

		// Token: 0x020000ED RID: 237
		[ExcludeFromDocs]
		[Serializable]
		public new class UxmlSerializedData : BaseField<Enum>.UxmlSerializedData
		{
			// Token: 0x060007E3 RID: 2019 RVA: 0x00022BE8 File Offset: 0x00020DE8
			[Conditional("UNITY_EDITOR")]
			public new static void Register()
			{
				BaseField<Enum>.UxmlSerializedData.Register();
				UxmlDescriptionCache.RegisterType(typeof(EnumField.UxmlSerializedData), new UxmlAttributeNames[]
				{
					new UxmlAttributeNames("typeAsString", "type", typeof(Enum), Array.Empty<string>()),
					new UxmlAttributeNames("valueAsString", "value", null, Array.Empty<string>()),
					new UxmlAttributeNames("includeObsoleteValues", "include-obsolete-values", null, Array.Empty<string>())
				});
			}

			// Token: 0x060007E4 RID: 2020 RVA: 0x00022C6F File Offset: 0x00020E6F
			public override object CreateInstance()
			{
				return new EnumField();
			}

			// Token: 0x060007E5 RID: 2021 RVA: 0x00022C78 File Offset: 0x00020E78
			public override void Deserialize(object obj)
			{
				base.Deserialize(obj);
				EnumField enumField = (EnumField)obj;
				bool flag = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.includeObsoleteValues_UxmlAttributeFlags);
				if (flag)
				{
					enumField.includeObsoleteValues = this.includeObsoleteValues;
				}
				bool flag2 = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.typeAsString_UxmlAttributeFlags);
				if (flag2)
				{
					enumField.typeAsString = this.typeAsString;
				}
				bool flag3 = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.valueAsString_UxmlAttributeFlags);
				if (flag3)
				{
					enumField.valueAsString = this.valueAsString;
				}
				else
				{
					enumField.valueAsString = null;
				}
			}

			// Token: 0x04000467 RID: 1127
			[UxmlAttribute("type")]
			[SerializeField]
			[UxmlTypeReference(typeof(Enum))]
			private string typeAsString;

			// Token: 0x04000468 RID: 1128
			[UxmlAttribute("value")]
			[SerializeField]
			[EnumFieldValueDecorator]
			private string valueAsString;

			// Token: 0x04000469 RID: 1129
			[SerializeField]
			private bool includeObsoleteValues;

			// Token: 0x0400046A RID: 1130
			[HideInInspector]
			[SerializeField]
			[UxmlIgnore]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags typeAsString_UxmlAttributeFlags;

			// Token: 0x0400046B RID: 1131
			[SerializeField]
			[UxmlIgnore]
			[HideInInspector]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags valueAsString_UxmlAttributeFlags;

			// Token: 0x0400046C RID: 1132
			[HideInInspector]
			[UxmlIgnore]
			[SerializeField]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags includeObsoleteValues_UxmlAttributeFlags;
		}

		// Token: 0x020000EE RID: 238
		[Obsolete("UxmlFactory is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlFactory : UxmlFactory<EnumField, EnumField.UxmlTraits>
		{
		}

		// Token: 0x020000EF RID: 239
		[Obsolete("UxmlTraits is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlTraits : BaseField<Enum>.UxmlTraits
		{
			// Token: 0x060007E8 RID: 2024 RVA: 0x00022D08 File Offset: 0x00020F08
			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ve, bag, cc);
				Type type;
				Enum @enum;
				bool flag2;
				bool flag = EnumFieldHelpers.ExtractValue(bag, cc, out type, out @enum, out flag2);
				if (flag)
				{
					EnumField enumField = (EnumField)ve;
					enumField.Init(@enum, flag2);
				}
				else
				{
					bool flag3 = null != type;
					if (flag3)
					{
						EnumField enumField2 = (EnumField)ve;
						enumField2.m_EnumType = type;
						bool flag4 = enumField2.m_EnumType != null;
						if (flag4)
						{
							enumField2.PopulateDataFromType(enumField2.m_EnumType);
						}
						enumField2.value = null;
					}
					else
					{
						EnumField enumField3 = (EnumField)ve;
						enumField3.m_EnumType = null;
						enumField3.value = null;
					}
				}
			}

			// Token: 0x0400046D RID: 1133
			private UxmlTypeAttributeDescription<Enum> m_Type = EnumFieldHelpers.type;

			// Token: 0x0400046E RID: 1134
			private UxmlStringAttributeDescription m_Value = EnumFieldHelpers.value;

			// Token: 0x0400046F RID: 1135
			private UxmlBoolAttributeDescription m_IncludeObsoleteValues = EnumFieldHelpers.includeObsoleteValues;
		}
	}
}
