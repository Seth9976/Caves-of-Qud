using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.Properties;
using UnityEngine.Internal;
using UnityEngine.Pool;

namespace UnityEngine.UIElements
{
	// Token: 0x02000178 RID: 376
	public class RadioButtonGroup : BaseField<int>, IGroupBox
	{
		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x06000BE0 RID: 3040 RVA: 0x00033F00 File Offset: 0x00032100
		// (set) Token: 0x06000BE1 RID: 3041 RVA: 0x00033F20 File Offset: 0x00032120
		[CreateProperty]
		public IEnumerable<string> choices
		{
			get
			{
				RadioButtonGroup.<get_choices>d__17 <get_choices>d__ = new RadioButtonGroup.<get_choices>d__17(-2);
				<get_choices>d__.<>4__this = this;
				return <get_choices>d__;
			}
			set
			{
				bool flag = (value != null && RadioButtonGroup.<set_choices>g__AreListEqual|18_0(this.m_Choices, value)) || (value == null && this.m_Choices.Count == 0);
				if (!flag)
				{
					this.m_Choices.Clear();
					bool flag2 = value != null;
					if (flag2)
					{
						this.m_Choices.AddRange(value);
					}
					this.RebuildRadioButtonsFromChoices();
					base.NotifyPropertyChanged(in RadioButtonGroup.choicesProperty);
				}
			}
		}

		// Token: 0x06000BE2 RID: 3042 RVA: 0x00033F94 File Offset: 0x00032194
		private void RebuildRadioButtonsFromChoices()
		{
			bool flag = this.m_Choices.Count == 0;
			if (flag)
			{
				this.m_ChoiceRadioButtonContainer.Clear();
			}
			else
			{
				int num = 0;
				foreach (string text in this.m_Choices)
				{
					bool flag2 = num < this.m_ChoiceRadioButtonContainer.childCount;
					if (flag2)
					{
						(this.m_ChoiceRadioButtonContainer[num] as RadioButton).text = text;
						this.ScheduleRadioButtons();
					}
					else
					{
						RadioButton radioButton = new RadioButton
						{
							text = text
						};
						this.m_ChoiceRadioButtonContainer.Add(radioButton);
					}
					num++;
				}
				int num2 = this.m_ChoiceRadioButtonContainer.childCount - 1;
				for (int i = num2; i >= num; i--)
				{
					this.m_ChoiceRadioButtonContainer[i].RemoveFromHierarchy();
				}
			}
		}

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x06000BE3 RID: 3043 RVA: 0x000340A8 File Offset: 0x000322A8
		// (set) Token: 0x06000BE4 RID: 3044 RVA: 0x000340B0 File Offset: 0x000322B0
		internal List<string> choicesList
		{
			get
			{
				return this.m_Choices;
			}
			set
			{
				this.choices = value;
			}
		}

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x06000BE5 RID: 3045 RVA: 0x000340BA File Offset: 0x000322BA
		public override VisualElement contentContainer
		{
			get
			{
				return this.m_ContentContainer ?? this;
			}
		}

		// Token: 0x06000BE6 RID: 3046 RVA: 0x000340C7 File Offset: 0x000322C7
		public RadioButtonGroup()
			: this(null, null)
		{
		}

		// Token: 0x06000BE7 RID: 3047 RVA: 0x000340D4 File Offset: 0x000322D4
		public RadioButtonGroup(string label, List<string> radioButtonChoices = null)
			: base(label, null)
		{
			base.AddToClassList(RadioButtonGroup.ussClassName);
			VisualElement visualInput = base.visualInput;
			VisualElement visualElement = new VisualElement();
			visualElement.name = RadioButtonGroup.choicesContainerName;
			VisualElement visualElement2 = visualElement;
			this.m_ChoiceRadioButtonContainer = visualElement;
			visualInput.Add(visualElement2);
			this.m_ChoiceRadioButtonContainer.AddToClassList(RadioButtonGroup.containerUssClassName);
			VisualElement visualInput2 = base.visualInput;
			VisualElement visualElement3 = new VisualElement();
			visualElement3.name = RadioButtonGroup.containerName;
			visualElement2 = visualElement3;
			this.m_ContentContainer = visualElement3;
			visualInput2.Add(visualElement2);
			this.m_ContentContainer.AddToClassList(RadioButtonGroup.containerUssClassName);
			this.m_GetAllRadioButtonsQuery = this.Query<RadioButton>(null, null);
			this.m_RadioButtonValueChangedCallback = new EventCallback<ChangeEvent<bool>>(this.RadioButtonValueChangedCallback);
			this.choices = radioButtonChoices;
			this.value = -1;
			base.visualInput.focusable = false;
			base.delegatesFocus = true;
		}

		// Token: 0x06000BE8 RID: 3048 RVA: 0x000341C0 File Offset: 0x000323C0
		private void RadioButtonValueChangedCallback(ChangeEvent<bool> evt)
		{
			bool newValue = evt.newValue;
			if (newValue)
			{
				RadioButton radioButton = evt.target as RadioButton;
				List<RadioButton> list;
				using (CollectionPool<List<RadioButton>, RadioButton>.Get(out list))
				{
					this.GetAllRadioButtons(list);
					this.value = list.IndexOf(radioButton);
					evt.StopPropagation();
				}
			}
		}

		// Token: 0x06000BE9 RID: 3049 RVA: 0x0003422C File Offset: 0x0003242C
		public override void SetValueWithoutNotify(int newValue)
		{
			base.SetValueWithoutNotify(newValue);
			this.UpdateRadioButtons(true);
		}

		// Token: 0x06000BEA RID: 3050 RVA: 0x0003423F File Offset: 0x0003243F
		private void GetAllRadioButtons(List<RadioButton> radioButtons)
		{
			radioButtons.Clear();
			this.m_GetAllRadioButtonsQuery.ForEach(new Action<RadioButton>(radioButtons.Add));
		}

		// Token: 0x06000BEB RID: 3051 RVA: 0x00034264 File Offset: 0x00032464
		private void UpdateRadioButtons(bool notify)
		{
			bool flag = base.panel == null;
			if (!flag)
			{
				List<RadioButton> list;
				using (CollectionPool<List<RadioButton>, RadioButton>.Get(out list))
				{
					this.GetAllRadioButtons(list);
					bool flag2 = this.value >= 0 && this.value < list.Count;
					if (flag2)
					{
						this.m_SelectedRadioButton = list[this.value];
						if (notify)
						{
							this.m_SelectedRadioButton.value = true;
						}
						else
						{
							this.m_SelectedRadioButton.SetValueWithoutNotify(true);
						}
						foreach (RadioButton radioButton in list)
						{
							bool flag3 = radioButton != this.m_SelectedRadioButton;
							if (flag3)
							{
								if (notify)
								{
									radioButton.value = false;
								}
								else
								{
									radioButton.SetValueWithoutNotify(false);
								}
							}
						}
					}
					else
					{
						foreach (RadioButton radioButton2 in this.m_RegisteredRadioButtons)
						{
							if (notify)
							{
								radioButton2.value = false;
							}
							else
							{
								radioButton2.SetValueWithoutNotify(false);
							}
						}
					}
					this.m_UpdatingButtons = false;
				}
			}
		}

		// Token: 0x06000BEC RID: 3052 RVA: 0x00034404 File Offset: 0x00032604
		private void ScheduleRadioButtons()
		{
			bool updatingButtons = this.m_UpdatingButtons;
			if (!updatingButtons)
			{
				base.schedule.Execute(delegate
				{
					this.UpdateRadioButtons(false);
				});
				this.m_UpdatingButtons = true;
			}
		}

		// Token: 0x06000BED RID: 3053 RVA: 0x00034440 File Offset: 0x00032640
		private void RegisterRadioButton(RadioButton radioButton)
		{
			bool flag = this.m_RegisteredRadioButtons.Contains(radioButton);
			if (!flag)
			{
				this.m_RegisteredRadioButtons.Add(radioButton);
				radioButton.RegisterValueChangedCallback<bool>(this.m_RadioButtonValueChangedCallback);
				bool flag2 = this.value == -1 && radioButton.value;
				if (flag2)
				{
					List<RadioButton> list;
					using (CollectionPool<List<RadioButton>, RadioButton>.Get(out list))
					{
						this.GetAllRadioButtons(list);
						this.SetValueWithoutNotify(list.IndexOf(radioButton));
					}
				}
				this.ScheduleRadioButtons();
			}
		}

		// Token: 0x06000BEE RID: 3054 RVA: 0x000344D8 File Offset: 0x000326D8
		private void UnregisterRadioButton(RadioButton radioButton)
		{
			bool flag = !this.m_RegisteredRadioButtons.Contains(radioButton);
			if (!flag)
			{
				this.m_RegisteredRadioButtons.Remove(radioButton);
				radioButton.UnregisterValueChangedCallback<bool>(this.m_RadioButtonValueChangedCallback);
				this.UpdateRadioButtons(false);
			}
		}

		// Token: 0x06000BEF RID: 3055 RVA: 0x00034520 File Offset: 0x00032720
		void IGroupBox.OnOptionAdded(IGroupBoxOption option)
		{
			RadioButton radioButton = option as RadioButton;
			bool flag = radioButton == null;
			if (flag)
			{
				throw new ArgumentException("[UI Toolkit] Internal group box error. Expected a radio button element. Please report this using Help -> Report a bug...");
			}
			this.RegisterRadioButton(radioButton);
		}

		// Token: 0x06000BF0 RID: 3056 RVA: 0x00034554 File Offset: 0x00032754
		void IGroupBox.OnOptionRemoved(IGroupBoxOption option)
		{
			RadioButton radioButton = option as RadioButton;
			bool flag = radioButton == null;
			if (flag)
			{
				throw new ArgumentException("[UI Toolkit] Internal group box error. Expected a radio button element. Please report this using Help -> Report a bug...");
			}
			this.UnregisterRadioButton(radioButton);
			bool flag2 = this.m_SelectedRadioButton == radioButton;
			if (flag2)
			{
				this.m_SelectedRadioButton = null;
				this.value = -1;
			}
		}

		// Token: 0x06000BF2 RID: 3058 RVA: 0x000345F8 File Offset: 0x000327F8
		[CompilerGenerated]
		internal static bool <set_choices>g__AreListEqual|18_0(List<string> list1, IEnumerable<string> list2)
		{
			int num = 0;
			using (IEnumerator<string> enumerator = list2.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					num++;
				}
			}
			bool flag = list1.Count != num;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				int num2 = 0;
				foreach (string text in list2)
				{
					bool flag3 = !string.Equals(list1[num2], text);
					if (flag3)
					{
						return false;
					}
					num2++;
				}
				flag2 = true;
			}
			return flag2;
		}

		// Token: 0x04000702 RID: 1794
		internal static readonly BindingId choicesProperty = "choices";

		// Token: 0x04000703 RID: 1795
		public new static readonly string ussClassName = "unity-radio-button-group";

		// Token: 0x04000704 RID: 1796
		public static readonly string containerUssClassName = RadioButtonGroup.ussClassName + "__container";

		// Token: 0x04000705 RID: 1797
		internal static readonly string containerName = "contentContainer";

		// Token: 0x04000706 RID: 1798
		internal static readonly string choicesContainerName = "choicesContentContainer";

		// Token: 0x04000707 RID: 1799
		private VisualElement m_ChoiceRadioButtonContainer;

		// Token: 0x04000708 RID: 1800
		private VisualElement m_ContentContainer;

		// Token: 0x04000709 RID: 1801
		private UQueryBuilder<RadioButton> m_GetAllRadioButtonsQuery;

		// Token: 0x0400070A RID: 1802
		private readonly List<RadioButton> m_RegisteredRadioButtons = new List<RadioButton>();

		// Token: 0x0400070B RID: 1803
		private RadioButton m_SelectedRadioButton;

		// Token: 0x0400070C RID: 1804
		private EventCallback<ChangeEvent<bool>> m_RadioButtonValueChangedCallback;

		// Token: 0x0400070D RID: 1805
		private bool m_UpdatingButtons;

		// Token: 0x0400070E RID: 1806
		private List<string> m_Choices = new List<string>();

		// Token: 0x02000179 RID: 377
		[ExcludeFromDocs]
		[Serializable]
		public new class UxmlSerializedData : BaseField<int>.UxmlSerializedData
		{
			// Token: 0x06000BF4 RID: 3060 RVA: 0x000346C6 File Offset: 0x000328C6
			[Conditional("UNITY_EDITOR")]
			public new static void Register()
			{
				BaseField<int>.UxmlSerializedData.Register();
				UxmlDescriptionCache.RegisterType(typeof(RadioButtonGroup.UxmlSerializedData), new UxmlAttributeNames[]
				{
					new UxmlAttributeNames("choicesList", "choices", null, Array.Empty<string>())
				});
			}

			// Token: 0x06000BF5 RID: 3061 RVA: 0x00034701 File Offset: 0x00032901
			public override object CreateInstance()
			{
				return new RadioButtonGroup();
			}

			// Token: 0x06000BF6 RID: 3062 RVA: 0x00034708 File Offset: 0x00032908
			public override void Deserialize(object obj)
			{
				base.Deserialize(obj);
				bool flag = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.choicesList_UxmlAttributeFlags);
				if (flag)
				{
					RadioButtonGroup radioButtonGroup = (RadioButtonGroup)obj;
					radioButtonGroup.choicesList = this.choicesList;
				}
			}

			// Token: 0x0400070F RID: 1807
			[SerializeField]
			[UxmlAttribute("choices")]
			[UxmlAttributeBindingPath("choices")]
			private List<string> choicesList;

			// Token: 0x04000710 RID: 1808
			[UxmlIgnore]
			[SerializeField]
			[HideInInspector]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags choicesList_UxmlAttributeFlags;
		}

		// Token: 0x0200017A RID: 378
		[Obsolete("UxmlFactory is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlFactory : UxmlFactory<RadioButtonGroup, RadioButtonGroup.UxmlTraits>
		{
		}

		// Token: 0x0200017B RID: 379
		[Obsolete("UxmlTraits is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlTraits : BaseFieldTraits<int, UxmlIntAttributeDescription>
		{
			// Token: 0x06000BF9 RID: 3065 RVA: 0x0003474C File Offset: 0x0003294C
			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ve, bag, cc);
				RadioButtonGroup radioButtonGroup = (RadioButtonGroup)ve;
				radioButtonGroup.choicesList = UxmlUtility.ParseStringListAttribute(this.m_Choices.GetValueFromBag(bag, cc));
			}

			// Token: 0x04000711 RID: 1809
			private UxmlStringAttributeDescription m_Choices = new UxmlStringAttributeDescription
			{
				name = "choices"
			};
		}
	}
}
