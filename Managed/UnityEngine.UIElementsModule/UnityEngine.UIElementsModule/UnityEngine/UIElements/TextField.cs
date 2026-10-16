using System;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.Properties;
using UnityEngine.Internal;

namespace UnityEngine.UIElements
{
	// Token: 0x0200011E RID: 286
	public class TextField : TextInputBaseField<string>
	{
		// Token: 0x17000155 RID: 341
		// (get) Token: 0x060008FF RID: 2303 RVA: 0x000279BE File Offset: 0x00025BBE
		private TextField.TextInput textInput
		{
			get
			{
				return (TextField.TextInput)base.textInputBase;
			}
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x06000900 RID: 2304 RVA: 0x000279CC File Offset: 0x00025BCC
		// (set) Token: 0x06000901 RID: 2305 RVA: 0x000279EC File Offset: 0x00025BEC
		[CreateProperty]
		public bool multiline
		{
			get
			{
				return this.textInput.multiline;
			}
			set
			{
				bool multiline = this.multiline;
				this.textInput.multiline = value;
				bool flag = multiline != this.multiline;
				if (flag)
				{
					base.NotifyPropertyChanged(in TextField.multilineProperty);
				}
			}
		}

		// Token: 0x06000902 RID: 2306 RVA: 0x00027A2A File Offset: 0x00025C2A
		public TextField()
			: this(null)
		{
		}

		// Token: 0x06000903 RID: 2307 RVA: 0x00027A35 File Offset: 0x00025C35
		public TextField(int maxLength, bool multiline, bool isPasswordField, char maskChar)
			: this(null, maxLength, multiline, isPasswordField, maskChar)
		{
		}

		// Token: 0x06000904 RID: 2308 RVA: 0x00027A45 File Offset: 0x00025C45
		public TextField(string label)
			: this(label, -1, false, false, '*')
		{
		}

		// Token: 0x06000905 RID: 2309 RVA: 0x00027A58 File Offset: 0x00025C58
		public TextField(string label, int maxLength, bool multiline, bool isPasswordField, char maskChar)
			: base(label, maxLength, maskChar, new TextField.TextInput())
		{
			base.AddToClassList(TextField.ussClassName);
			base.labelElement.AddToClassList(TextField.labelUssClassName);
			base.visualInput.AddToClassList(TextField.inputUssClassName);
			base.pickingMode = PickingMode.Ignore;
			this.SetValueWithoutNotify("");
			this.multiline = multiline;
			base.textEdition.isPassword = isPasswordField;
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x06000906 RID: 2310 RVA: 0x00027AD0 File Offset: 0x00025CD0
		// (set) Token: 0x06000907 RID: 2311 RVA: 0x00027AE8 File Offset: 0x00025CE8
		public override string value
		{
			get
			{
				return base.value;
			}
			set
			{
				base.value = value;
				base.textEdition.UpdateText(base.rawValue);
			}
		}

		// Token: 0x06000908 RID: 2312 RVA: 0x00027B08 File Offset: 0x00025D08
		public override void SetValueWithoutNotify(string newValue)
		{
			base.SetValueWithoutNotify(newValue);
			string text = base.rawValue;
			bool flag = !this.multiline && base.rawValue != null;
			if (flag)
			{
				text = base.rawValue.Replace("\n", "");
			}
			((INotifyValueChanged<string>)this.textInput.textElement).SetValueWithoutNotify(text);
		}

		// Token: 0x06000909 RID: 2313 RVA: 0x00027B65 File Offset: 0x00025D65
		internal override void UpdateTextFromValue()
		{
			this.SetValueWithoutNotify(base.rawValue);
		}

		// Token: 0x0600090A RID: 2314 RVA: 0x00027B78 File Offset: 0x00025D78
		[EventInterest(new Type[] { typeof(FocusOutEvent) })]
		protected override void HandleEventBubbleUp(EventBase evt)
		{
			base.HandleEventBubbleUp(evt);
			bool flag;
			if (base.isDelayed)
			{
				long? num = ((evt != null) ? new long?(evt.eventTypeId) : null);
				long num2 = EventBase<FocusOutEvent>.TypeId();
				flag = (num.GetValueOrDefault() == num2) & (num != null);
			}
			else
			{
				flag = false;
			}
			bool flag2 = flag;
			if (flag2)
			{
				DispatchMode dispatchMode = base.dispatchMode;
				try
				{
					base.dispatchMode = DispatchMode.Immediate;
					this.value = base.text;
				}
				finally
				{
					base.dispatchMode = dispatchMode;
				}
			}
		}

		// Token: 0x0600090B RID: 2315 RVA: 0x00027C10 File Offset: 0x00025E10
		internal override void OnViewDataReady()
		{
			base.OnViewDataReady();
			string fullHierarchicalViewDataKey = base.GetFullHierarchicalViewDataKey();
			base.OverwriteFromViewData(this, fullHierarchicalViewDataKey);
			base.text = base.rawValue;
		}

		// Token: 0x0600090C RID: 2316 RVA: 0x00027C42 File Offset: 0x00025E42
		protected override string ValueToString(string value)
		{
			return value;
		}

		// Token: 0x0600090D RID: 2317 RVA: 0x00027C42 File Offset: 0x00025E42
		protected override string StringToValue(string str)
		{
			return str;
		}

		// Token: 0x04000518 RID: 1304
		internal static readonly BindingId multilineProperty = "multiline";

		// Token: 0x04000519 RID: 1305
		public new static readonly string ussClassName = "unity-text-field";

		// Token: 0x0400051A RID: 1306
		public new static readonly string labelUssClassName = TextField.ussClassName + "__label";

		// Token: 0x0400051B RID: 1307
		public new static readonly string inputUssClassName = TextField.ussClassName + "__input";

		// Token: 0x0200011F RID: 287
		[ExcludeFromDocs]
		[Serializable]
		public new class UxmlSerializedData : TextInputBaseField<string>.UxmlSerializedData, IUxmlSerializedDataCustomAttributeHandler
		{
			// Token: 0x0600090F RID: 2319 RVA: 0x00027C96 File Offset: 0x00025E96
			[Conditional("UNITY_EDITOR")]
			public new static void Register()
			{
				TextInputBaseField<string>.UxmlSerializedData.Register();
				UxmlDescriptionCache.RegisterType(typeof(TextField.UxmlSerializedData), new UxmlAttributeNames[]
				{
					new UxmlAttributeNames("multiline", "multiline", null, Array.Empty<string>())
				});
			}

			// Token: 0x06000910 RID: 2320 RVA: 0x00027CD1 File Offset: 0x00025ED1
			public override object CreateInstance()
			{
				return new TextField();
			}

			// Token: 0x06000911 RID: 2321 RVA: 0x00027CD8 File Offset: 0x00025ED8
			public override void Deserialize(object obj)
			{
				base.Deserialize(obj);
				TextField textField = (TextField)obj;
				bool flag = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.multiline_UxmlAttributeFlags);
				if (flag)
				{
					textField.multiline = this.multiline;
				}
				bool flag2 = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.verticalScrollerVisibility_UxmlAttributeFlags);
				if (flag2)
				{
					textField.verticalScrollerVisibility = this.verticalScrollerVisibility;
				}
			}

			// Token: 0x06000912 RID: 2322 RVA: 0x00027D30 File Offset: 0x00025F30
			void IUxmlSerializedDataCustomAttributeHandler.SerializeCustomAttributes(IUxmlAttributes bag, HashSet<string> handledAttributes)
			{
				string text;
				bool flag = bag.TryGetAttributeValue("text", out text);
				if (flag)
				{
					base.Value = text;
					handledAttributes.Add("value");
					UxmlAsset uxmlAsset = bag as UxmlAsset;
					bool flag2 = uxmlAsset != null;
					if (flag2)
					{
						uxmlAsset.RemoveAttribute("text");
						uxmlAsset.SetAttribute("value", base.Value);
					}
				}
			}

			// Token: 0x0400051C RID: 1308
			[MultilineDecorator]
			[SerializeField]
			private bool multiline;

			// Token: 0x0400051D RID: 1309
			[HideInInspector]
			[UxmlIgnore]
			[SerializeField]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags multiline_UxmlAttributeFlags;
		}

		// Token: 0x02000120 RID: 288
		[Obsolete("UxmlFactory is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlFactory : UxmlFactory<TextField, TextField.UxmlTraits>
		{
		}

		// Token: 0x02000121 RID: 289
		[Obsolete("UxmlTraits is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlTraits : TextInputBaseField<string>.UxmlTraits
		{
			// Token: 0x06000915 RID: 2325 RVA: 0x00027DA8 File Offset: 0x00025FA8
			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				TextField textField = (TextField)ve;
				base.Init(ve, bag, cc);
				string empty = string.Empty;
				bool flag = TextField.UxmlTraits.k_Value.TryGetValueFromBag(bag, cc, ref empty);
				if (flag)
				{
					textField.SetValueWithoutNotify(empty);
				}
				textField.multiline = this.m_Multiline.GetValueFromBag(bag, cc);
			}

			// Token: 0x0400051E RID: 1310
			private static readonly UxmlStringAttributeDescription k_Value = new UxmlStringAttributeDescription
			{
				name = "value",
				obsoleteNames = new string[] { "text" }
			};

			// Token: 0x0400051F RID: 1311
			private UxmlBoolAttributeDescription m_Multiline = new UxmlBoolAttributeDescription
			{
				name = "multiline"
			};
		}

		// Token: 0x02000122 RID: 290
		private class TextInput : TextInputBaseField<string>.TextInputBase
		{
			// Token: 0x17000158 RID: 344
			// (get) Token: 0x06000918 RID: 2328 RVA: 0x00027E5A File Offset: 0x0002605A
			private TextField parentTextField
			{
				get
				{
					return (TextField)base.parent;
				}
			}

			// Token: 0x17000159 RID: 345
			// (get) Token: 0x06000919 RID: 2329 RVA: 0x00027E68 File Offset: 0x00026068
			// (set) Token: 0x0600091A RID: 2330 RVA: 0x00027E88 File Offset: 0x00026088
			public bool multiline
			{
				get
				{
					return base.textEdition.multiline;
				}
				set
				{
					bool flag = value || string.IsNullOrEmpty(base.text) || !base.text.Contains("\n");
					bool flag2 = flag && base.textEdition.multiline == value;
					if (!flag2)
					{
						base.textEdition.multiline = value;
						if (value)
						{
							base.text = this.parentTextField.rawValue;
							base.SetMultiline();
						}
						else
						{
							base.text = base.text.Replace("\n", "");
							base.SetSingleLine();
						}
					}
				}
			}

			// Token: 0x1700015A RID: 346
			// (set) Token: 0x0600091B RID: 2331 RVA: 0x00027F2C File Offset: 0x0002612C
			[Obsolete("isPasswordField is deprecated. Use textEdition.isPassword instead.")]
			public override bool isPasswordField
			{
				set
				{
					base.textEdition.isPassword = value;
					if (value)
					{
						this.multiline = false;
					}
				}
			}

			// Token: 0x0600091C RID: 2332 RVA: 0x00027C42 File Offset: 0x00025E42
			protected override string StringToValue(string str)
			{
				return str;
			}
		}
	}
}
