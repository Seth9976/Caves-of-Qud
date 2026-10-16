using System;
using System.Collections.Generic;
using Unity.Properties;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.UIElements
{
	// Token: 0x020001AA RID: 426
	[MovedFrom(true, "UnityEditor.UIElements", "UnityEditor.UIElementsModule", null)]
	public abstract class TextValueField<TValueType> : TextInputBaseField<TValueType>, IValueField<TValueType>
	{
		// Token: 0x17000237 RID: 567
		// (get) Token: 0x06000D47 RID: 3399 RVA: 0x0003BCDF File Offset: 0x00039EDF
		private TextValueField<TValueType>.TextValueInput textValueInput
		{
			get
			{
				return (TextValueField<TValueType>.TextValueInput)base.textInputBase;
			}
		}

		// Token: 0x17000238 RID: 568
		// (set) Token: 0x06000D48 RID: 3400 RVA: 0x0003BCEC File Offset: 0x00039EEC
		internal bool forceUpdateDisplay
		{
			set
			{
				this.m_ForceUpdateDisplay = value;
			}
		}

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x06000D49 RID: 3401 RVA: 0x0003BCF5 File Offset: 0x00039EF5
		// (set) Token: 0x06000D4A RID: 3402 RVA: 0x0003BD04 File Offset: 0x00039F04
		[CreateProperty]
		public string formatString
		{
			get
			{
				return this.textValueInput.formatString;
			}
			set
			{
				bool flag = this.textValueInput.formatString != value;
				if (flag)
				{
					this.textValueInput.formatString = value;
					base.textEdition.UpdateText(this.ValueToString(base.rawValue));
					base.NotifyPropertyChanged(in TextValueField<TValueType>.formatStringProperty);
				}
			}
		}

		// Token: 0x06000D4B RID: 3403 RVA: 0x0003BD5A File Offset: 0x00039F5A
		protected TextValueField(int maxLength, TextValueField<TValueType>.TextValueInput textValueInput)
			: this(null, maxLength, textValueInput)
		{
		}

		// Token: 0x06000D4C RID: 3404 RVA: 0x0003BD68 File Offset: 0x00039F68
		protected TextValueField(string label, int maxLength, TextValueField<TValueType>.TextValueInput textValueInput)
			: base(label, maxLength, '\0', textValueInput)
		{
			base.textEdition.UpdateText(this.ValueToString(base.rawValue));
			base.onIsReadOnlyChanged = (Action<bool>)Delegate.Combine(base.onIsReadOnlyChanged, new Action<bool>(this.OnIsReadOnlyChanged));
		}

		// Token: 0x06000D4D RID: 3405
		public abstract void ApplyInputDeviceDelta(Vector3 delta, DeltaSpeed speed, TValueType startValue);

		// Token: 0x06000D4E RID: 3406 RVA: 0x0003BDBC File Offset: 0x00039FBC
		public void StartDragging()
		{
			bool showMixedValue = base.showMixedValue;
			if (showMixedValue)
			{
				this.value = default(TValueType);
			}
			this.textValueInput.StartDragging();
		}

		// Token: 0x06000D4F RID: 3407 RVA: 0x0003BDF2 File Offset: 0x00039FF2
		public void StopDragging()
		{
			this.textValueInput.StopDragging();
		}

		// Token: 0x06000D50 RID: 3408 RVA: 0x0003BE04 File Offset: 0x0003A004
		internal override void UpdateValueFromText()
		{
			base.UpdatePlaceholderClassList(null);
			this.m_UpdateTextFromValue = false;
			try
			{
				this.value = this.StringToValue(base.text);
			}
			finally
			{
				this.m_UpdateTextFromValue = true;
			}
		}

		// Token: 0x06000D51 RID: 3409 RVA: 0x0003BE54 File Offset: 0x0003A054
		internal override void UpdateTextFromValue()
		{
			bool updateTextFromValue = this.m_UpdateTextFromValue;
			if (updateTextFromValue)
			{
				base.text = this.ValueToString(base.rawValue);
			}
		}

		// Token: 0x06000D52 RID: 3410 RVA: 0x0003BE81 File Offset: 0x0003A081
		private void OnIsReadOnlyChanged(bool newValue)
		{
			this.EnableLabelDragger(!newValue);
		}

		// Token: 0x06000D53 RID: 3411 RVA: 0x00011DCA File Offset: 0x0000FFCA
		internal virtual bool CanTryParse(string textString)
		{
			return false;
		}

		// Token: 0x06000D54 RID: 3412 RVA: 0x0003BE8F File Offset: 0x0003A08F
		protected void AddLabelDragger<TDraggerType>()
		{
			this.m_Dragger = new FieldMouseDragger<TDraggerType>((IValueField<TDraggerType>)this);
			this.EnableLabelDragger(!base.isReadOnly);
		}

		// Token: 0x06000D55 RID: 3413 RVA: 0x0003BEB4 File Offset: 0x0003A0B4
		private void EnableLabelDragger(bool enable)
		{
			bool flag = this.m_Dragger != null;
			if (flag)
			{
				this.m_Dragger.SetDragZone(enable ? base.labelElement : null);
				base.labelElement.EnableInClassList(BaseField<TValueType>.labelDraggerVariantUssClassName, enable);
			}
		}

		// Token: 0x06000D56 RID: 3414 RVA: 0x0003BEFC File Offset: 0x0003A0FC
		public override void SetValueWithoutNotify(TValueType newValue)
		{
			bool flag = this.m_ForceUpdateDisplay || (this.m_UpdateTextFromValue && !EqualityComparer<TValueType>.Default.Equals(base.rawValue, newValue));
			base.SetValueWithoutNotify(newValue);
			bool flag2 = flag;
			if (flag2)
			{
				base.textEdition.UpdateText(this.ValueToString(base.rawValue));
			}
			this.m_ForceUpdateDisplay = false;
		}

		// Token: 0x06000D57 RID: 3415 RVA: 0x0003BF63 File Offset: 0x0003A163
		public void ClearValue()
		{
			base.text = string.Empty;
			this.UpdateValueFromText();
		}

		// Token: 0x06000D58 RID: 3416 RVA: 0x0003BF7C File Offset: 0x0003A17C
		[EventInterest(new Type[]
		{
			typeof(BlurEvent),
			typeof(FocusEvent)
		})]
		protected override void HandleEventBubbleUp(EventBase evt)
		{
			base.HandleEventBubbleUp(evt);
			bool flag = string.IsNullOrEmpty(base.text) && !string.IsNullOrEmpty(base.textEdition.placeholder);
			bool flag2 = flag;
			if (!flag2)
			{
				bool flag3 = evt.eventTypeId == EventBase<BlurEvent>.TypeId();
				if (flag3)
				{
					bool showMixedValue = base.showMixedValue;
					if (showMixedValue)
					{
						this.UpdateMixedValueContent();
					}
					else
					{
						bool flag4 = string.IsNullOrEmpty(base.text);
						if (flag4)
						{
							base.textInputBase.UpdateTextFromValue();
						}
						else
						{
							base.textInputBase.UpdateValueFromText();
							base.textInputBase.UpdateTextFromValue();
						}
					}
				}
				else
				{
					bool flag5 = evt.eventTypeId == EventBase<FocusEvent>.TypeId();
					if (flag5)
					{
						bool flag6 = base.showMixedValue && base.textInputBase.textElement.hasFocus;
						if (flag6)
						{
							base.textInputBase.text = "";
						}
					}
				}
			}
		}

		// Token: 0x06000D59 RID: 3417 RVA: 0x0003C06F File Offset: 0x0003A26F
		internal override void OnViewDataReady()
		{
			this.m_ForceUpdateDisplay = true;
			base.OnViewDataReady();
		}

		// Token: 0x06000D5A RID: 3418 RVA: 0x0003C080 File Offset: 0x0003A280
		internal override void RegisterEditingCallbacks()
		{
			base.RegisterEditingCallbacks();
			base.labelElement.RegisterCallback<PointerDownEvent>(new EventCallback<PointerDownEvent>(base.StartEditing), TrickleDown.TrickleDown);
			base.labelElement.RegisterCallback<PointerUpEvent>(new EventCallback<PointerUpEvent>(base.EndEditing), TrickleDown.NoTrickleDown);
		}

		// Token: 0x06000D5B RID: 3419 RVA: 0x0003C0BC File Offset: 0x0003A2BC
		internal override void UnregisterEditingCallbacks()
		{
			base.UnregisterEditingCallbacks();
			base.labelElement.UnregisterCallback<PointerDownEvent>(new EventCallback<PointerDownEvent>(base.StartEditing), TrickleDown.TrickleDown);
			base.labelElement.UnregisterCallback<PointerUpEvent>(new EventCallback<PointerUpEvent>(base.EndEditing), TrickleDown.NoTrickleDown);
		}

		// Token: 0x0400085E RID: 2142
		internal static readonly BindingId formatStringProperty = "formatString";

		// Token: 0x0400085F RID: 2143
		private BaseFieldMouseDragger m_Dragger;

		// Token: 0x04000860 RID: 2144
		private bool m_ForceUpdateDisplay;

		// Token: 0x04000861 RID: 2145
		internal const int kMaxValueFieldLength = 1000;

		// Token: 0x020001AB RID: 427
		protected abstract class TextValueInput : TextInputBaseField<TValueType>.TextInputBase
		{
			// Token: 0x1700023A RID: 570
			// (get) Token: 0x06000D5D RID: 3421 RVA: 0x0003C109 File Offset: 0x0003A309
			private TextValueField<TValueType> textValueFieldParent
			{
				get
				{
					return (TextValueField<TValueType>)base.parent;
				}
			}

			// Token: 0x06000D5E RID: 3422 RVA: 0x0003C116 File Offset: 0x0003A316
			protected TextValueInput()
			{
				base.textEdition.AcceptCharacter = new Func<char, bool>(this.AcceptCharacter);
			}

			// Token: 0x06000D5F RID: 3423 RVA: 0x0003C13C File Offset: 0x0003A33C
			internal override bool AcceptCharacter(char c)
			{
				return base.AcceptCharacter(c) && c != '\0' && this.allowedCharacters.IndexOf(c) != -1;
			}

			// Token: 0x1700023B RID: 571
			// (get) Token: 0x06000D60 RID: 3424
			protected abstract string allowedCharacters { get; }

			// Token: 0x1700023C RID: 572
			// (get) Token: 0x06000D61 RID: 3425 RVA: 0x0003C16F File Offset: 0x0003A36F
			// (set) Token: 0x06000D62 RID: 3426 RVA: 0x0003C177 File Offset: 0x0003A377
			public string formatString { get; set; }

			// Token: 0x06000D63 RID: 3427
			public abstract void ApplyInputDeviceDelta(Vector3 delta, DeltaSpeed speed, TValueType startValue);

			// Token: 0x06000D64 RID: 3428 RVA: 0x0003C180 File Offset: 0x0003A380
			public void StartDragging()
			{
				base.isDragging = true;
				base.textSelection.SelectNone();
				base.MarkDirtyRepaint();
			}

			// Token: 0x06000D65 RID: 3429 RVA: 0x0003C1A0 File Offset: 0x0003A3A0
			public void StopDragging()
			{
				bool isDelayed = this.textValueFieldParent.isDelayed;
				if (isDelayed)
				{
					base.UpdateValueFromText();
				}
				base.isDragging = false;
				base.textSelection.SelectAll();
				base.MarkDirtyRepaint();
			}

			// Token: 0x06000D66 RID: 3430
			protected abstract string ValueToString(TValueType value);

			// Token: 0x06000D67 RID: 3431 RVA: 0x0003C1E4 File Offset: 0x0003A3E4
			protected override TValueType StringToValue(string str)
			{
				return base.StringToValue(str);
			}
		}
	}
}
