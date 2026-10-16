using System;
using System.Diagnostics;
using Unity.Properties;
using UnityEngine.Bindings;
using UnityEngine.Internal;

namespace UnityEngine.UIElements
{
	// Token: 0x020000F6 RID: 246
	public class Foldout : BindableElement, INotifyValueChanged<bool>
	{
		// Token: 0x17000122 RID: 290
		// (get) Token: 0x06000802 RID: 2050 RVA: 0x0002309E File Offset: 0x0002129E
		internal Toggle toggle
		{
			[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
			get
			{
				return this.m_Toggle;
			}
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x06000803 RID: 2051 RVA: 0x000230A6 File Offset: 0x000212A6
		public override VisualElement contentContainer
		{
			get
			{
				return this.m_Container;
			}
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x06000804 RID: 2052 RVA: 0x000230AE File Offset: 0x000212AE
		// (set) Token: 0x06000805 RID: 2053 RVA: 0x000230B6 File Offset: 0x000212B6
		public override bool focusable
		{
			get
			{
				return base.focusable;
			}
			set
			{
				base.focusable = value;
				this.m_Toggle.focusable = value;
			}
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x06000806 RID: 2054 RVA: 0x000230CE File Offset: 0x000212CE
		// (set) Token: 0x06000807 RID: 2055 RVA: 0x000230DC File Offset: 0x000212DC
		[CreateProperty]
		public bool toggleOnLabelClick
		{
			get
			{
				return this.m_Toggle.toggleOnTextClick;
			}
			set
			{
				bool flag = this.m_Toggle.toggleOnTextClick == value;
				if (!flag)
				{
					this.m_Toggle.toggleOnTextClick = value;
					base.NotifyPropertyChanged(in Foldout.toggleOnLabelClickProperty);
				}
			}
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x06000808 RID: 2056 RVA: 0x00023117 File Offset: 0x00021317
		// (set) Token: 0x06000809 RID: 2057 RVA: 0x00023124 File Offset: 0x00021324
		[CreateProperty]
		public string text
		{
			get
			{
				return this.m_Toggle.text;
			}
			set
			{
				string text = this.text;
				this.m_Toggle.text = value;
				VisualElement visualElement = this.m_Toggle.visualInput.Q(null, Toggle.textUssClassName);
				if (visualElement != null)
				{
					visualElement.AddToClassList(Foldout.textUssClassName);
				}
				bool flag = string.CompareOrdinal(text, this.text) != 0;
				if (flag)
				{
					base.NotifyPropertyChanged(in Foldout.textProperty);
				}
			}
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x0600080A RID: 2058 RVA: 0x0002318C File Offset: 0x0002138C
		// (set) Token: 0x0600080B RID: 2059 RVA: 0x00023194 File Offset: 0x00021394
		[CreateProperty]
		public bool value
		{
			get
			{
				return this.m_Value;
			}
			set
			{
				bool flag = this.m_Value == value;
				if (!flag)
				{
					using (ChangeEvent<bool> pooled = ChangeEvent<bool>.GetPooled(this.m_Value, value))
					{
						pooled.elementTarget = this;
						this.SetValueWithoutNotify(value);
						this.SendEvent(pooled);
						base.SaveViewData();
						base.NotifyPropertyChanged(in Foldout.valueProperty);
					}
				}
			}
		}

		// Token: 0x0600080C RID: 2060 RVA: 0x00023208 File Offset: 0x00021408
		public void SetValueWithoutNotify(bool newValue)
		{
			this.m_Value = newValue;
			this.m_Toggle.SetValueWithoutNotify(this.m_Value);
			this.contentContainer.style.display = (newValue ? DisplayStyle.Flex : DisplayStyle.None);
			bool value = this.m_Value;
			if (value)
			{
				base.pseudoStates |= PseudoStates.Checked;
			}
			else
			{
				base.pseudoStates &= ~PseudoStates.Checked;
			}
		}

		// Token: 0x0600080D RID: 2061 RVA: 0x0002327C File Offset: 0x0002147C
		internal override void OnViewDataReady()
		{
			base.OnViewDataReady();
			string fullHierarchicalViewDataKey = base.GetFullHierarchicalViewDataKey();
			base.OverwriteFromViewData(this, fullHierarchicalViewDataKey);
			this.SetValueWithoutNotify(this.m_Value);
		}

		// Token: 0x0600080E RID: 2062 RVA: 0x000232B0 File Offset: 0x000214B0
		private void Apply(KeyboardNavigationOperation op, EventBase sourceEvent)
		{
			bool flag = this.Apply(op);
			if (flag)
			{
				sourceEvent.StopPropagation();
				this.focusController.IgnoreEvent(sourceEvent);
			}
		}

		// Token: 0x0600080F RID: 2063 RVA: 0x000232E0 File Offset: 0x000214E0
		private bool Apply(KeyboardNavigationOperation op)
		{
			bool flag;
			switch (op)
			{
			case KeyboardNavigationOperation.SelectAll:
			case KeyboardNavigationOperation.Cancel:
			case KeyboardNavigationOperation.Submit:
			case KeyboardNavigationOperation.Previous:
			case KeyboardNavigationOperation.Next:
			case KeyboardNavigationOperation.PageUp:
			case KeyboardNavigationOperation.PageDown:
			case KeyboardNavigationOperation.Begin:
			case KeyboardNavigationOperation.End:
				flag = false;
				break;
			case KeyboardNavigationOperation.MoveRight:
				this.SetValueWithoutNotify(true);
				flag = true;
				break;
			case KeyboardNavigationOperation.MoveLeft:
				this.SetValueWithoutNotify(false);
				flag = true;
				break;
			default:
				throw new ArgumentOutOfRangeException("op", op, null);
			}
			return flag;
		}

		// Token: 0x06000810 RID: 2064 RVA: 0x0002335C File Offset: 0x0002155C
		public Foldout()
		{
			base.AddToClassList(Foldout.ussClassName);
			base.delegatesFocus = true;
			this.focusable = true;
			base.isEligibleToReceiveFocusFromDisabledChild = false;
			this.m_Container = new VisualElement
			{
				name = "unity-content"
			};
			this.m_Toggle.RegisterValueChangedCallback<bool>(delegate(ChangeEvent<bool> evt)
			{
				this.value = this.m_Toggle.value;
				evt.StopPropagation();
			});
			this.m_Toggle.AddToClassList(Foldout.toggleUssClassName);
			this.m_Toggle.visualInput.AddToClassList(Foldout.inputUssClassName);
			this.m_Toggle.visualInput.Q(null, Toggle.checkmarkUssClassName).AddToClassList(Foldout.checkmarkUssClassName);
			this.m_Toggle.AddManipulator(this.m_NavigationManipulator = new KeyboardNavigationManipulator(new Action<KeyboardNavigationOperation, EventBase>(this.Apply)));
			base.hierarchy.Add(this.m_Toggle);
			this.m_Container.AddToClassList(Foldout.contentUssClassName);
			base.hierarchy.Add(this.m_Container);
			base.RegisterCallback<AttachToPanelEvent>(new EventCallback<AttachToPanelEvent>(this.OnAttachToPanel), TrickleDown.NoTrickleDown);
			this.SetValueWithoutNotify(true);
		}

		// Token: 0x06000811 RID: 2065 RVA: 0x00023494 File Offset: 0x00021694
		private void OnAttachToPanel(AttachToPanelEvent evt)
		{
			for (int i = 0; i <= Foldout.ussFoldoutMaxDepth; i++)
			{
				base.RemoveFromClassList(Foldout.ussFoldoutDepthClassName + i.ToString());
			}
			base.RemoveFromClassList(Foldout.ussFoldoutDepthClassName + "max");
			this.m_Toggle.AssignInspectorStyleIfNecessary(Foldout.toggleInspectorUssClassName);
			int foldoutDepth = this.GetFoldoutDepth();
			bool flag = foldoutDepth > Foldout.ussFoldoutMaxDepth;
			if (flag)
			{
				base.AddToClassList(Foldout.ussFoldoutDepthClassName + "max");
			}
			else
			{
				base.AddToClassList(Foldout.ussFoldoutDepthClassName + foldoutDepth.ToString());
			}
		}

		// Token: 0x04000475 RID: 1141
		internal static readonly BindingId textProperty = "text";

		// Token: 0x04000476 RID: 1142
		internal static readonly BindingId toggleOnLabelClickProperty = "toggleOnLabelClick";

		// Token: 0x04000477 RID: 1143
		internal static readonly BindingId valueProperty = "value";

		// Token: 0x04000478 RID: 1144
		private readonly Toggle m_Toggle = new Toggle();

		// Token: 0x04000479 RID: 1145
		private VisualElement m_Container;

		// Token: 0x0400047A RID: 1146
		[SerializeField]
		[DontCreateProperty]
		private bool m_Value;

		// Token: 0x0400047B RID: 1147
		public static readonly string ussClassName = "unity-foldout";

		// Token: 0x0400047C RID: 1148
		public static readonly string toggleUssClassName = Foldout.ussClassName + "__toggle";

		// Token: 0x0400047D RID: 1149
		public static readonly string contentUssClassName = Foldout.ussClassName + "__content";

		// Token: 0x0400047E RID: 1150
		public static readonly string inputUssClassName = Foldout.ussClassName + "__input";

		// Token: 0x0400047F RID: 1151
		public static readonly string checkmarkUssClassName = Foldout.ussClassName + "__checkmark";

		// Token: 0x04000480 RID: 1152
		public static readonly string textUssClassName = Foldout.ussClassName + "__text";

		// Token: 0x04000481 RID: 1153
		internal static readonly string toggleInspectorUssClassName = Foldout.toggleUssClassName + "--inspector";

		// Token: 0x04000482 RID: 1154
		internal static readonly string ussFoldoutDepthClassName = Foldout.ussClassName + "--depth-";

		// Token: 0x04000483 RID: 1155
		internal static readonly int ussFoldoutMaxDepth = 4;

		// Token: 0x04000484 RID: 1156
		private KeyboardNavigationManipulator m_NavigationManipulator;

		// Token: 0x020000F7 RID: 247
		[ExcludeFromDocs]
		[Serializable]
		public new class UxmlSerializedData : BindableElement.UxmlSerializedData
		{
			// Token: 0x06000814 RID: 2068 RVA: 0x00023638 File Offset: 0x00021838
			[Conditional("UNITY_EDITOR")]
			public new static void Register()
			{
				UxmlDescriptionCache.RegisterType(typeof(Foldout.UxmlSerializedData), new UxmlAttributeNames[]
				{
					new UxmlAttributeNames("text", "text", null, Array.Empty<string>()),
					new UxmlAttributeNames("toggleOnLabelClick", "toggle-on-label-click", null, Array.Empty<string>()),
					new UxmlAttributeNames("value", "value", null, Array.Empty<string>())
				});
			}

			// Token: 0x06000815 RID: 2069 RVA: 0x000236B0 File Offset: 0x000218B0
			public override object CreateInstance()
			{
				return new Foldout();
			}

			// Token: 0x06000816 RID: 2070 RVA: 0x000236B8 File Offset: 0x000218B8
			public override void Deserialize(object obj)
			{
				base.Deserialize(obj);
				Foldout foldout = (Foldout)obj;
				bool flag = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.text_UxmlAttributeFlags);
				if (flag)
				{
					foldout.text = this.text;
				}
				bool flag2 = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.toggleOnLabelClick_UxmlAttributeFlags);
				if (flag2)
				{
					foldout.toggleOnLabelClick = this.toggleOnLabelClick;
				}
				bool flag3 = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.value_UxmlAttributeFlags);
				if (flag3)
				{
					foldout.SetValueWithoutNotify(this.value);
				}
			}

			// Token: 0x04000485 RID: 1157
			[SerializeField]
			[MultilineTextField]
			private string text;

			// Token: 0x04000486 RID: 1158
			[HideInInspector]
			[SerializeField]
			[UxmlIgnore]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags text_UxmlAttributeFlags;

			// Token: 0x04000487 RID: 1159
			[SerializeField]
			private bool toggleOnLabelClick;

			// Token: 0x04000488 RID: 1160
			[SerializeField]
			[UxmlIgnore]
			[HideInInspector]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags toggleOnLabelClick_UxmlAttributeFlags;

			// Token: 0x04000489 RID: 1161
			[SerializeField]
			private bool value;

			// Token: 0x0400048A RID: 1162
			[SerializeField]
			[UxmlIgnore]
			[HideInInspector]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags value_UxmlAttributeFlags;
		}

		// Token: 0x020000F8 RID: 248
		[Obsolete("UxmlFactory is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlFactory : UxmlFactory<Foldout, Foldout.UxmlTraits>
		{
		}

		// Token: 0x020000F9 RID: 249
		[Obsolete("UxmlTraits is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlTraits : BindableElement.UxmlTraits
		{
			// Token: 0x06000819 RID: 2073 RVA: 0x00023734 File Offset: 0x00021934
			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ve, bag, cc);
				Foldout foldout = ve as Foldout;
				bool flag = foldout != null;
				if (flag)
				{
					foldout.text = this.m_Text.GetValueFromBag(bag, cc);
					foldout.SetValueWithoutNotify(this.m_Value.GetValueFromBag(bag, cc));
				}
			}

			// Token: 0x0400048B RID: 1163
			private UxmlStringAttributeDescription m_Text = new UxmlStringAttributeDescription
			{
				name = "text"
			};

			// Token: 0x0400048C RID: 1164
			private UxmlBoolAttributeDescription m_Value = new UxmlBoolAttributeDescription
			{
				name = "value",
				defaultValue = true
			};
		}
	}
}
