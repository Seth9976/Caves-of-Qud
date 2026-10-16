using System;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.Properties;
using UnityEngine.Internal;

namespace UnityEngine.UIElements
{
	// Token: 0x020001B1 RID: 433
	[UxmlElement(null, new Type[] { typeof(Button) })]
	public class ToggleButtonGroup : BaseField<ToggleButtonGroupState>
	{
		// Token: 0x1700023D RID: 573
		// (get) Token: 0x06000D76 RID: 3446 RVA: 0x0003C586 File Offset: 0x0003A786
		// (set) Token: 0x06000D77 RID: 3447 RVA: 0x0003C590 File Offset: 0x0003A790
		[CreateProperty]
		public unsafe bool isMultipleSelection
		{
			get
			{
				return this.m_IsMultipleSelection;
			}
			set
			{
				bool flag = this.m_IsMultipleSelection == value;
				checked
				{
					if (!flag)
					{
						ToggleButtonGroupState value2 = this.value;
						int length = value2.length;
						Span<int> span = new Span<int>(stackalloc byte[unchecked((UIntPtr)length) * 4], length);
						Span<int> activeOptions = value2.GetActiveOptions(span);
						bool flag2 = activeOptions.Length > 1 && this.m_Buttons.Count > 0;
						if (flag2)
						{
							value2.ResetAllOptions();
							value2[*activeOptions[0]] = true;
							this.SetValueWithoutNotify(value2);
						}
						this.m_IsMultipleSelection = value;
						base.NotifyPropertyChanged(in ToggleButtonGroup.isMultipleSelectionProperty);
					}
				}
			}
		}

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x06000D78 RID: 3448 RVA: 0x0003C62C File Offset: 0x0003A82C
		// (set) Token: 0x06000D79 RID: 3449 RVA: 0x0003C634 File Offset: 0x0003A834
		[CreateProperty]
		public unsafe bool allowEmptySelection
		{
			get
			{
				return this.m_AllowEmptySelection;
			}
			set
			{
				bool flag = this.m_AllowEmptySelection == value;
				checked
				{
					if (!flag)
					{
						bool flag2 = !value;
						if (flag2)
						{
							ToggleButtonGroupState value2 = this.value;
							int length = value2.length;
							Span<int> span = new Span<int>(stackalloc byte[unchecked((UIntPtr)length) * 4], length);
							bool flag3 = value2.GetActiveOptions(span).Length == 0 && this.m_Buttons.Count > 0;
							if (flag3)
							{
								value2[0] = true;
								this.SetValueWithoutNotify(value2);
							}
						}
						this.m_AllowEmptySelection = value;
						base.NotifyPropertyChanged(in ToggleButtonGroup.allowEmptySelectionProperty);
					}
				}
			}
		}

		// Token: 0x06000D7A RID: 3450 RVA: 0x0003C6CC File Offset: 0x0003A8CC
		public ToggleButtonGroup()
			: this(null)
		{
		}

		// Token: 0x06000D7B RID: 3451 RVA: 0x0003C6D7 File Offset: 0x0003A8D7
		public ToggleButtonGroup(string label)
			: this(label, new ToggleButtonGroupState(0UL, 64))
		{
		}

		// Token: 0x06000D7C RID: 3452 RVA: 0x0003C6EB File Offset: 0x0003A8EB
		public ToggleButtonGroup(ToggleButtonGroupState toggleButtonGroupState)
			: this(null, toggleButtonGroupState)
		{
		}

		// Token: 0x06000D7D RID: 3453 RVA: 0x0003C6F8 File Offset: 0x0003A8F8
		public ToggleButtonGroup(string label, ToggleButtonGroupState toggleButtonGroupState)
			: base(label)
		{
			base.AddToClassList(ToggleButtonGroup.ussClassName);
			base.visualInput = new VisualElement
			{
				name = ToggleButtonGroup.containerUssClassName,
				classList = { ToggleButtonGroup.buttonGroupClassName },
				focusable = false
			};
			this.m_ButtonGroupContainer = base.visualInput;
			this.m_ButtonGroupContainer.elementAdded += this.OnButtonGroupContainerElementAdded;
			this.m_ButtonGroupContainer.elementRemoved += this.OnButtonGroupContainerElementRemoved;
			this.SetValueWithoutNotify(toggleButtonGroupState);
		}

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x06000D7E RID: 3454 RVA: 0x0003C79A File Offset: 0x0003A99A
		public override VisualElement contentContainer
		{
			get
			{
				return this.m_ButtonGroupContainer ?? this;
			}
		}

		// Token: 0x06000D7F RID: 3455 RVA: 0x0003C7A7 File Offset: 0x0003A9A7
		internal override void OnViewDataReady()
		{
			base.OnViewDataReady();
			this.UpdateButtonStates(this.value);
		}

		// Token: 0x06000D80 RID: 3456 RVA: 0x0003C7C0 File Offset: 0x0003A9C0
		protected override void UpdateMixedValueContent()
		{
			bool showMixedValue = base.showMixedValue;
			if (showMixedValue)
			{
				foreach (Button button in this.m_Buttons)
				{
					button.pseudoStates &= ~PseudoStates.Checked;
					button.IncrementVersion(VersionChangeType.Styles);
				}
			}
			else
			{
				this.SetValueWithoutNotify(this.value);
			}
		}

		// Token: 0x06000D81 RID: 3457 RVA: 0x0003C848 File Offset: 0x0003AA48
		public override void SetValueWithoutNotify(ToggleButtonGroupState newValue)
		{
			bool flag = newValue.length == 0;
			if (flag)
			{
				newValue = new ToggleButtonGroupState(0UL, 0);
				if (this.m_EmptyLabel == null)
				{
					this.m_EmptyLabel = new Label("Group has no buttons.")
					{
						name = ToggleButtonGroup.emptyStateLabelClassName,
						classList = { ToggleButtonGroup.emptyStateLabelClassName }
					};
				}
				base.visualInput.Insert(0, this.m_EmptyLabel);
			}
			else
			{
				VisualElement emptyLabel = this.m_EmptyLabel;
				if (emptyLabel != null)
				{
					emptyLabel.RemoveFromHierarchy();
				}
			}
			base.SetValueWithoutNotify(newValue);
			this.UpdateButtonStates(newValue);
		}

		// Token: 0x06000D82 RID: 3458 RVA: 0x0003C8E0 File Offset: 0x0003AAE0
		private void OnButtonGroupContainerElementAdded(VisualElement ve, int index)
		{
			Button button = ve as Button;
			bool flag = button == null;
			if (flag)
			{
				bool flag2 = ve == this.m_EmptyLabel;
				if (!flag2)
				{
					base.hierarchy.Add(ve);
				}
			}
			else
			{
				bool flag3 = this.m_Buttons.Count + 1 > 64;
				if (flag3)
				{
					Debug.LogWarning(ToggleButtonGroup.k_MaxToggleButtonGroupMessage);
				}
				else
				{
					button.AddToClassList(ToggleButtonGroup.buttonClassName);
					button.clickable.clickedWithEventInfo += this.OnOptionChange;
					this.m_Buttons = this.m_ButtonGroupContainer.Query<Button>(null, null).ToList();
					this.UpdateButtonsStyling();
					bool flag4 = false;
					ToggleButtonGroupState value = this.value;
					bool flag5 = this.m_Buttons.Count >= this.value.length && this.m_Buttons.Count <= 64;
					if (flag5)
					{
						value.length = this.m_Buttons.Count;
						flag4 = true;
					}
					bool flag6 = this.value.data == 0UL && !this.allowEmptySelection;
					if (flag6)
					{
						value[0] = true;
						flag4 = true;
					}
					bool flag7 = flag4;
					if (flag7)
					{
						this.value = value;
					}
				}
			}
		}

		// Token: 0x06000D83 RID: 3459 RVA: 0x0003CA34 File Offset: 0x0003AC34
		private unsafe void OnButtonGroupContainerElementRemoved(VisualElement ve)
		{
			Button button = ve as Button;
			bool flag = button == null;
			checked
			{
				if (!flag)
				{
					ToggleButtonGroupState value = this.value;
					int num = this.m_Buttons.IndexOf(button);
					int length = value.length;
					Span<int> span = new Span<int>(stackalloc byte[unchecked((UIntPtr)length) * 4], length);
					Span<int> activeOptions = value.GetActiveOptions(span);
					bool flag2 = activeOptions.IndexOf(num) != -1;
					button.clickable.clickedWithEventInfo -= this.OnOptionChange;
					bool flag3 = flag2;
					if (flag3)
					{
						this.m_Buttons[num].pseudoStates &= ~PseudoStates.Checked;
					}
					this.m_Buttons.Remove(button);
					this.UpdateButtonsStyling();
					value.length = this.m_Buttons.Count;
					bool flag4 = this.m_Buttons.Count == 0;
					if (flag4)
					{
						value.ResetAllOptions();
						this.SetValueWithoutNotify(value);
					}
					else
					{
						bool flag5 = flag2;
						if (flag5)
						{
							value[num] = false;
							bool flag6 = !this.allowEmptySelection && activeOptions.Length == 1;
							if (flag6)
							{
								value[0] = true;
							}
							this.value = value;
						}
					}
				}
			}
		}

		// Token: 0x06000D84 RID: 3460 RVA: 0x0003CB6C File Offset: 0x0003AD6C
		private unsafe void UpdateButtonStates(ToggleButtonGroupState options)
		{
			int length = this.value.length;
			Span<int> activeOptions;
			checked
			{
				Span<int> span = new Span<int>(stackalloc byte[unchecked((UIntPtr)length) * 4], length);
				activeOptions = options.GetActiveOptions(span);
			}
			for (int i = 0; i < this.m_Buttons.Count; i++)
			{
				bool flag = activeOptions.IndexOf(i) == -1;
				if (flag)
				{
					this.m_Buttons[i].pseudoStates &= ~PseudoStates.Checked;
					this.m_Buttons[i].IncrementVersion(VersionChangeType.Styles);
				}
				else
				{
					this.m_Buttons[i].pseudoStates |= PseudoStates.Checked;
					this.m_Buttons[i].IncrementVersion(VersionChangeType.Styles);
				}
			}
		}

		// Token: 0x06000D85 RID: 3461 RVA: 0x0003CC3C File Offset: 0x0003AE3C
		private unsafe void OnOptionChange(EventBase evt)
		{
			Button button = evt.target as Button;
			int num = this.m_Buttons.IndexOf(button);
			ToggleButtonGroupState value = this.value;
			int length = value.length;
			checked
			{
				Span<int> span = new Span<int>(stackalloc byte[unchecked((UIntPtr)length) * 4], length);
				Span<int> activeOptions = value.GetActiveOptions(span);
				bool showMixedValue = base.showMixedValue;
				if (showMixedValue)
				{
					ToggleButtonGroupState value2 = this.value;
					value2.ResetAllOptions();
					bool flag = this.value != value2;
					if (flag)
					{
						this.SetValueWithoutNotify(value2);
					}
				}
				bool isMultipleSelection = this.isMultipleSelection;
				if (isMultipleSelection)
				{
					bool flag2 = !this.allowEmptySelection && activeOptions.Length == 1 && value[num];
					if (flag2)
					{
						return;
					}
					bool flag3 = value[num];
					if (flag3)
					{
						value[num] = false;
					}
					else
					{
						value[num] = true;
					}
				}
				else
				{
					bool flag4 = this.allowEmptySelection && activeOptions.Length == 1 && value[*activeOptions[0]];
					if (flag4)
					{
						value[*activeOptions[0]] = false;
						bool flag5 = num != *activeOptions[0];
						if (flag5)
						{
							value[num] = true;
						}
					}
					else
					{
						value.ResetAllOptions();
						value[num] = true;
					}
				}
				this.value = value;
			}
		}

		// Token: 0x06000D86 RID: 3462 RVA: 0x0003CDA4 File Offset: 0x0003AFA4
		private void UpdateButtonsStyling()
		{
			int count = this.m_Buttons.Count;
			for (int i = 0; i < count; i++)
			{
				Button button = this.m_Buttons[i];
				bool flag = count == 1;
				bool flag2 = i == 0 && !flag;
				bool flag3 = i == count - 1 && !flag;
				bool flag4 = !flag2 && !flag3 && !flag;
				button.EnableInClassList(ToggleButtonGroup.buttonStandaloneClassName, flag);
				button.EnableInClassList(ToggleButtonGroup.buttonLeftClassName, flag2);
				button.EnableInClassList(ToggleButtonGroup.buttonRightClassName, flag3);
				button.EnableInClassList(ToggleButtonGroup.buttonMidClassName, flag4);
			}
		}

		// Token: 0x04000871 RID: 2161
		private static readonly string k_MaxToggleButtonGroupMessage = string.Format("The number of buttons added to ToggleButtonGroup exceeds the maximum allowed ({0}). The newly added button will not be treated as part of this control.", 64);

		// Token: 0x04000872 RID: 2162
		internal static readonly BindingId isMultipleSelectionProperty = "isMultipleSelection";

		// Token: 0x04000873 RID: 2163
		internal static readonly BindingId allowEmptySelectionProperty = "allowEmptySelection";

		// Token: 0x04000874 RID: 2164
		public new static readonly string ussClassName = "unity-toggle-button-group";

		// Token: 0x04000875 RID: 2165
		public static readonly string containerUssClassName = ToggleButtonGroup.ussClassName + "__container";

		// Token: 0x04000876 RID: 2166
		public static readonly string buttonGroupClassName = "unity-button-group";

		// Token: 0x04000877 RID: 2167
		public static readonly string buttonClassName = ToggleButtonGroup.buttonGroupClassName + "__button";

		// Token: 0x04000878 RID: 2168
		public static readonly string buttonLeftClassName = ToggleButtonGroup.buttonClassName + "--left";

		// Token: 0x04000879 RID: 2169
		public static readonly string buttonMidClassName = ToggleButtonGroup.buttonClassName + "--mid";

		// Token: 0x0400087A RID: 2170
		public static readonly string buttonRightClassName = ToggleButtonGroup.buttonClassName + "--right";

		// Token: 0x0400087B RID: 2171
		public static readonly string buttonStandaloneClassName = ToggleButtonGroup.buttonClassName + "--standalone";

		// Token: 0x0400087C RID: 2172
		public static readonly string emptyStateLabelClassName = ToggleButtonGroup.buttonGroupClassName + "__empty-label";

		// Token: 0x0400087D RID: 2173
		private VisualElement m_ButtonGroupContainer;

		// Token: 0x0400087E RID: 2174
		private List<Button> m_Buttons = new List<Button>();

		// Token: 0x0400087F RID: 2175
		private VisualElement m_EmptyLabel;

		// Token: 0x04000880 RID: 2176
		private const string k_EmptyStateLabel = "Group has no buttons.";

		// Token: 0x04000881 RID: 2177
		private bool m_IsMultipleSelection;

		// Token: 0x04000882 RID: 2178
		private bool m_AllowEmptySelection;

		// Token: 0x020001B2 RID: 434
		[ExcludeFromDocs]
		[Serializable]
		public new class UxmlSerializedData : BaseField<ToggleButtonGroupState>.UxmlSerializedData
		{
			// Token: 0x06000D88 RID: 3464 RVA: 0x0003CF30 File Offset: 0x0003B130
			[Conditional("UNITY_EDITOR")]
			public new static void Register()
			{
				BaseField<ToggleButtonGroupState>.UxmlSerializedData.Register();
				UxmlDescriptionCache.RegisterType(typeof(ToggleButtonGroup.UxmlSerializedData), new UxmlAttributeNames[]
				{
					new UxmlAttributeNames("isMultipleSelection", "is-multiple-selection", null, Array.Empty<string>()),
					new UxmlAttributeNames("allowEmptySelection", "allow-empty-selection", null, Array.Empty<string>())
				});
			}

			// Token: 0x06000D89 RID: 3465 RVA: 0x0003CF92 File Offset: 0x0003B192
			public override object CreateInstance()
			{
				return new ToggleButtonGroup();
			}

			// Token: 0x06000D8A RID: 3466 RVA: 0x0003CF9C File Offset: 0x0003B19C
			public override void Deserialize(object obj)
			{
				base.Deserialize(obj);
				ToggleButtonGroup toggleButtonGroup = (ToggleButtonGroup)obj;
				bool flag = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.isMultipleSelection_UxmlAttributeFlags);
				if (flag)
				{
					toggleButtonGroup.isMultipleSelection = this.isMultipleSelection;
				}
				bool flag2 = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.allowEmptySelection_UxmlAttributeFlags);
				if (flag2)
				{
					toggleButtonGroup.allowEmptySelection = this.allowEmptySelection;
				}
			}

			// Token: 0x04000883 RID: 2179
			[SerializeField]
			private bool isMultipleSelection;

			// Token: 0x04000884 RID: 2180
			[SerializeField]
			[UxmlIgnore]
			[HideInInspector]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags isMultipleSelection_UxmlAttributeFlags;

			// Token: 0x04000885 RID: 2181
			[SerializeField]
			private bool allowEmptySelection;

			// Token: 0x04000886 RID: 2182
			[SerializeField]
			[HideInInspector]
			[UxmlIgnore]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags allowEmptySelection_UxmlAttributeFlags;
		}

		// Token: 0x020001B3 RID: 435
		[Obsolete("UxmlFactory is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlFactory : UxmlFactory<ToggleButtonGroup, ToggleButtonGroup.UxmlTraits>
		{
		}

		// Token: 0x020001B4 RID: 436
		[Obsolete("UxmlTraits is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlTraits : BaseField<ToggleButtonGroupState>.UxmlTraits
		{
			// Token: 0x06000D8D RID: 3469 RVA: 0x0003D004 File Offset: 0x0003B204
			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ve, bag, cc);
				ToggleButtonGroup toggleButtonGroup = (ToggleButtonGroup)ve;
				toggleButtonGroup.isMultipleSelection = this.m_IsMultipleSelection.GetValueFromBag(bag, cc);
				toggleButtonGroup.allowEmptySelection = this.m_AllowEmptySelection.GetValueFromBag(bag, cc);
			}

			// Token: 0x04000887 RID: 2183
			private UxmlBoolAttributeDescription m_IsMultipleSelection = new UxmlBoolAttributeDescription
			{
				name = "is-multiple-selection"
			};

			// Token: 0x04000888 RID: 2184
			private UxmlBoolAttributeDescription m_AllowEmptySelection = new UxmlBoolAttributeDescription
			{
				name = "allow-empty-selection"
			};
		}
	}
}
