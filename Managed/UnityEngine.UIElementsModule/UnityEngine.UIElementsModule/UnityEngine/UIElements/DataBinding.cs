using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using Unity.Properties;
using UnityEngine.Internal;

namespace UnityEngine.UIElements
{
	// Token: 0x02000030 RID: 48
	[UxmlObject]
	public class DataBinding : Binding, IDataSourceProvider
	{
		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060001A0 RID: 416 RVA: 0x00007D6A File Offset: 0x00005F6A
		internal static MethodInfo updateUIMethod
		{
			get
			{
				MethodInfo methodInfo;
				if ((methodInfo = DataBinding.s_UpdateUIMethodInfo) == null)
				{
					methodInfo = (DataBinding.s_UpdateUIMethodInfo = DataBinding.CacheReflectionInfo());
				}
				return methodInfo;
			}
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x00007D80 File Offset: 0x00005F80
		private static MethodInfo CacheReflectionInfo()
		{
			foreach (MethodInfo methodInfo in typeof(DataBinding).GetMethods(BindingFlags.Instance | BindingFlags.NonPublic))
			{
				bool flag = methodInfo.Name != "UpdateUI";
				if (!flag)
				{
					bool flag2 = methodInfo.GetParameters().Length != 2;
					if (!flag2)
					{
						return DataBinding.s_UpdateUIMethodInfo = methodInfo;
					}
				}
			}
			throw new InvalidOperationException("Could not find method UpdateUI by reflection. This is an internal bug. Please report using `Help > Report a Bug...` ");
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060001A2 RID: 418 RVA: 0x00007DF8 File Offset: 0x00005FF8
		// (set) Token: 0x060001A3 RID: 419 RVA: 0x00007E00 File Offset: 0x00006000
		[CreateProperty]
		public object dataSource { get; set; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060001A4 RID: 420 RVA: 0x00007E09 File Offset: 0x00006009
		// (set) Token: 0x060001A5 RID: 421 RVA: 0x00007E11 File Offset: 0x00006011
		public Type dataSourceType { get; set; }

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060001A6 RID: 422 RVA: 0x00007E1A File Offset: 0x0000601A
		// (set) Token: 0x060001A7 RID: 423 RVA: 0x00007E27 File Offset: 0x00006027
		internal string dataSourceTypeString
		{
			get
			{
				return UxmlUtility.TypeToString(this.dataSourceType);
			}
			set
			{
				this.dataSourceType = UxmlUtility.ParseType(value, null);
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060001A8 RID: 424 RVA: 0x00007E37 File Offset: 0x00006037
		// (set) Token: 0x060001A9 RID: 425 RVA: 0x00007E3F File Offset: 0x0000603F
		[CreateProperty]
		public PropertyPath dataSourcePath { get; set; }

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060001AA RID: 426 RVA: 0x00007E48 File Offset: 0x00006048
		// (set) Token: 0x060001AB RID: 427 RVA: 0x00007E69 File Offset: 0x00006069
		internal string dataSourcePathString
		{
			get
			{
				return this.dataSourcePath.ToString();
			}
			set
			{
				this.dataSourcePath = new PropertyPath(value);
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060001AC RID: 428 RVA: 0x00007E78 File Offset: 0x00006078
		// (set) Token: 0x060001AD RID: 429 RVA: 0x00007E80 File Offset: 0x00006080
		[CreateProperty]
		public BindingMode bindingMode
		{
			get
			{
				return this.m_BindingMode;
			}
			set
			{
				bool flag = this.m_BindingMode == value;
				if (!flag)
				{
					this.m_BindingMode = value;
					base.MarkDirty();
				}
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060001AE RID: 430 RVA: 0x00007EAC File Offset: 0x000060AC
		[CreateProperty(ReadOnly = true)]
		public ConverterGroup sourceToUiConverters
		{
			get
			{
				ConverterGroup converterGroup;
				if ((converterGroup = this.m_SourceToUiConverters) == null)
				{
					converterGroup = (this.m_SourceToUiConverters = new ConverterGroup(string.Empty, null, null));
				}
				return converterGroup;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060001AF RID: 431 RVA: 0x00007EE0 File Offset: 0x000060E0
		[CreateProperty(ReadOnly = true)]
		public ConverterGroup uiToSourceConverters
		{
			get
			{
				ConverterGroup converterGroup;
				if ((converterGroup = this.m_UiToSourceConverters) == null)
				{
					converterGroup = (this.m_UiToSourceConverters = new ConverterGroup(string.Empty, null, null));
				}
				return converterGroup;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060001B0 RID: 432 RVA: 0x00007F11 File Offset: 0x00006111
		// (set) Token: 0x060001B1 RID: 433 RVA: 0x00007F30 File Offset: 0x00006130
		internal string sourceToUiConvertersString
		{
			get
			{
				return (this.m_SourceToUIConvertersString != null) ? string.Join(", ", this.m_SourceToUIConvertersString) : null;
			}
			set
			{
				this.m_SourceToUIConvertersString = UxmlUtility.ParseStringListAttribute(value);
				bool flag = this.m_SourceToUIConvertersString != null;
				if (flag)
				{
					foreach (string text in this.m_SourceToUIConvertersString)
					{
						ConverterGroup converterGroup;
						bool flag2 = ConverterGroups.TryGetConverterGroup(text, out converterGroup);
						if (flag2)
						{
							this.ApplyConverterGroupToUI(converterGroup);
						}
					}
				}
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060001B2 RID: 434 RVA: 0x00007FB4 File Offset: 0x000061B4
		// (set) Token: 0x060001B3 RID: 435 RVA: 0x00007FD4 File Offset: 0x000061D4
		internal string uiToSourceConvertersString
		{
			get
			{
				return (this.m_UiToSourceConvertersString != null) ? string.Join(", ", this.m_UiToSourceConvertersString) : null;
			}
			set
			{
				this.m_UiToSourceConvertersString = UxmlUtility.ParseStringListAttribute(value);
				bool flag = this.m_UiToSourceConvertersString != null;
				if (flag)
				{
					foreach (string text in this.m_UiToSourceConvertersString)
					{
						ConverterGroup converterGroup;
						bool flag2 = ConverterGroups.TryGetConverterGroup(text, out converterGroup);
						if (flag2)
						{
							this.ApplyConverterGroupToSource(converterGroup);
						}
					}
				}
			}
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x00008058 File Offset: 0x00006258
		public DataBinding()
		{
			base.updateTrigger = BindingUpdateTrigger.OnSourceChanged;
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x0000806C File Offset: 0x0000626C
		public void ApplyConverterGroupToSource(ConverterGroup group)
		{
			ConverterGroup uiToSourceConverters = this.uiToSourceConverters;
			uiToSourceConverters.registry.Apply(group.registry);
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x00008098 File Offset: 0x00006298
		public void ApplyConverterGroupToUI(ConverterGroup group)
		{
			ConverterGroup sourceToUiConverters = this.sourceToUiConverters;
			sourceToUiConverters.registry.Apply(group.registry);
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x000080C4 File Offset: 0x000062C4
		protected internal virtual BindingResult UpdateUI<TValue>(in BindingContext context, ref TValue value)
		{
			VisualElement targetElement = context.targetElement;
			FocusController focusController = targetElement.focusController;
			bool flag = focusController != null && focusController.IsFocused(targetElement);
			if (flag)
			{
				Focusable leafFocusedElement = focusController.GetLeafFocusedElement();
				TextElement textElement = leafFocusedElement as TextElement;
				bool flag2;
				if (textElement != null && textElement.ClassListContains("unity-text-element--inner-input-field-component"))
				{
					IDelayedField delayedField = targetElement as IDelayedField;
					flag2 = (delayedField != null && delayedField.isDelayed) || textElement.edition.touchScreenKeyboard != null;
				}
				else
				{
					flag2 = false;
				}
				bool flag3 = flag2;
				if (flag3)
				{
					return new BindingResult(BindingStatus.Pending, null);
				}
			}
			ConverterGroup sourceToUiConverters = this.sourceToUiConverters;
			BindingId bindingId = context.bindingId;
			PropertyPath propertyPath = in bindingId;
			VisitReturnCode visitReturnCode;
			bool flag4 = sourceToUiConverters.TrySetValue<VisualElement, TValue>(ref targetElement, in propertyPath, value, out visitReturnCode);
			bool flag5 = flag4;
			BindingResult bindingResult;
			if (flag5)
			{
				bindingResult = default(BindingResult);
			}
			else
			{
				VisitReturnCode visitReturnCode2 = visitReturnCode;
				object dataSource = context.dataSource;
				propertyPath = context.dataSourcePath;
				object obj = targetElement;
				bindingId = context.bindingId;
				string setValueErrorString = DataBinding.GetSetValueErrorString<TValue>(visitReturnCode2, dataSource, in propertyPath, obj, in bindingId, value);
				bindingResult = new BindingResult(BindingStatus.Failure, setValueErrorString);
			}
			return bindingResult;
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x000081CC File Offset: 0x000063CC
		protected internal virtual BindingResult UpdateSource<TValue>(in BindingContext context, ref TValue value)
		{
			object dataSource = context.dataSource;
			ConverterGroup uiToSourceConverters = this.uiToSourceConverters;
			PropertyPath propertyPath = context.dataSourcePath;
			VisitReturnCode visitReturnCode;
			bool flag = uiToSourceConverters.TrySetValue<object, TValue>(ref dataSource, in propertyPath, value, out visitReturnCode);
			bool flag2 = flag;
			BindingResult bindingResult;
			if (flag2)
			{
				bindingResult = default(BindingResult);
			}
			else
			{
				VisitReturnCode visitReturnCode2 = visitReturnCode;
				object targetElement = context.targetElement;
				BindingId bindingId = context.bindingId;
				propertyPath = in bindingId;
				object dataSource2 = context.dataSource;
				PropertyPath dataSourcePath = context.dataSourcePath;
				BindingId bindingId2 = in dataSourcePath;
				string setValueErrorString = DataBinding.GetSetValueErrorString<TValue>(visitReturnCode2, targetElement, in propertyPath, dataSource2, in bindingId2, value);
				bindingResult = new BindingResult(BindingStatus.Failure, setValueErrorString);
			}
			return bindingResult;
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x00008264 File Offset: 0x00006464
		internal static string GetSetValueErrorString<TValue>(VisitReturnCode returnCode, object source, in PropertyPath sourcePath, object target, in BindingId targetPath, TValue extractedValueFromSource)
		{
			string text = string.Format("[UI Toolkit] Could not set value for target of type '<b>{0}</b>' at path '<b>{1}</b>':", target.GetType().Name, targetPath);
			string text2;
			switch (returnCode)
			{
			case VisitReturnCode.Ok:
			case VisitReturnCode.NullContainer:
			case VisitReturnCode.InvalidContainerType:
				throw new InvalidOperationException(text + " internal data binding error. Please report this using the '<b>Help/Report a bug...</b>' menu item.");
			case VisitReturnCode.MissingPropertyBag:
				text2 = text + " the type '" + target.GetType().Name + "' is missing a property bag.";
				break;
			case VisitReturnCode.InvalidPath:
				text2 = text + " the path is either invalid or contains a null value.";
				break;
			case VisitReturnCode.InvalidCast:
			{
				bool isEmpty = sourcePath.IsEmpty;
				if (isEmpty)
				{
					object obj;
					bool flag = PropertyContainer.TryGetValue<object, object>(ref target, in targetPath, out obj) && obj != null;
					if (flag)
					{
						text2 = ((extractedValueFromSource == null) ? (text + " could not convert from '<b>null</b>' to '<b>" + obj.GetType().Name + "</b>'.") : string.Concat(new string[]
						{
							text,
							" could not convert from type '<b>",
							extractedValueFromSource.GetType().Name,
							"</b>' to type '<b>",
							obj.GetType().Name,
							"</b>'."
						}));
						break;
					}
				}
				IProperty property;
				bool flag2 = PropertyContainer.TryGetProperty<object>(ref source, in sourcePath, out property);
				if (flag2)
				{
					object obj2;
					bool flag3 = PropertyContainer.TryGetValue<object, object>(ref target, in targetPath, out obj2) && obj2 != null;
					if (flag3)
					{
						text2 = ((extractedValueFromSource == null) ? string.Concat(new string[]
						{
							text,
							" could not convert from '<b>null (",
							property.DeclaredValueType().Name,
							")</b>' to '<b>",
							obj2.GetType().Name,
							"</b>'."
						}) : string.Concat(new string[]
						{
							text,
							" could not convert from type '<b>",
							extractedValueFromSource.GetType().Name,
							"</b>' to type '<b>",
							obj2.GetType().Name,
							"</b>'."
						}));
						break;
					}
				}
				text2 = text + " conversion failed.";
				break;
			}
			case VisitReturnCode.AccessViolation:
				text2 = text + " the path is read-only.";
				break;
			default:
				throw new ArgumentOutOfRangeException();
			}
			return text2;
		}

		// Token: 0x040000FC RID: 252
		private static MethodInfo s_UpdateUIMethodInfo;

		// Token: 0x040000FD RID: 253
		private BindingMode m_BindingMode;

		// Token: 0x040000FE RID: 254
		private ConverterGroup m_SourceToUiConverters;

		// Token: 0x040000FF RID: 255
		private ConverterGroup m_UiToSourceConverters;

		// Token: 0x04000103 RID: 259
		private List<string> m_SourceToUIConvertersString;

		// Token: 0x04000104 RID: 260
		private List<string> m_UiToSourceConvertersString;

		// Token: 0x04000105 RID: 261
		internal const string k_DataSourceTooltip = "A data source is a collection of information. By default, a binding will inherit the existing data source from the hierarchy. You can instead define another object here as the data source, or define the type of property it may be if the source is not yet available.";

		// Token: 0x04000106 RID: 262
		internal const string k_DataSourcePathTooltip = "The path to the value in the data source used by this binding. To see resolved bindings in the UI Builder, define a path that is compatible with the target source property.";

		// Token: 0x04000107 RID: 263
		internal const string k_BindingModeTooltip = "Controls how a binding is updated, which can include the direction in which data is written.";

		// Token: 0x04000108 RID: 264
		internal const string k_SourceToUiConvertersTooltip = "Define one or more converter groups for this binding that will be used between the data source to the target UI.";

		// Token: 0x04000109 RID: 265
		internal const string k_UiToSourceConvertersTooltip = "Define one or more converter groups for this binding that will be used between the target UI to the data source.";

		// Token: 0x02000031 RID: 49
		[ExcludeFromDocs]
		[Serializable]
		public new class UxmlSerializedData : Binding.UxmlSerializedData
		{
			// Token: 0x060001BA RID: 442 RVA: 0x000084AC File Offset: 0x000066AC
			[Conditional("UNITY_EDITOR")]
			public new static void Register()
			{
				UxmlDescriptionCache.RegisterType(typeof(DataBinding.UxmlSerializedData), new UxmlAttributeNames[]
				{
					new UxmlAttributeNames("dataSourcePathString", "data-source-path", null, Array.Empty<string>()),
					new UxmlAttributeNames("dataSource", "data-source", null, Array.Empty<string>()),
					new UxmlAttributeNames("dataSourceTypeString", "data-source-type", typeof(object), Array.Empty<string>()),
					new UxmlAttributeNames("bindingMode", "binding-mode", null, Array.Empty<string>()),
					new UxmlAttributeNames("sourceToUiConvertersString", "source-to-ui-converters", null, Array.Empty<string>()),
					new UxmlAttributeNames("uiToSourceConvertersString", "ui-to-source-converters", null, Array.Empty<string>())
				});
			}

			// Token: 0x060001BB RID: 443 RVA: 0x00008581 File Offset: 0x00006781
			public override object CreateInstance()
			{
				return new DataBinding();
			}

			// Token: 0x060001BC RID: 444 RVA: 0x00008588 File Offset: 0x00006788
			public override void Deserialize(object obj)
			{
				base.Deserialize(obj);
				DataBinding dataBinding = (DataBinding)obj;
				bool flag = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.dataSourcePathString_UxmlAttributeFlags);
				if (flag)
				{
					dataBinding.dataSourcePathString = this.dataSourcePathString;
				}
				bool flag2 = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.dataSource_UxmlAttributeFlags);
				if (flag2)
				{
					dataBinding.dataSource = (this.dataSource ? this.dataSource : null);
				}
				bool flag3 = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.dataSourceTypeString_UxmlAttributeFlags);
				if (flag3)
				{
					dataBinding.dataSourceTypeString = this.dataSourceTypeString;
				}
				bool flag4 = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.bindingMode_UxmlAttributeFlags);
				if (flag4)
				{
					dataBinding.bindingMode = this.bindingMode;
				}
				bool flag5 = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.uiToSourceConvertersString_UxmlAttributeFlags);
				if (flag5)
				{
					dataBinding.uiToSourceConvertersString = this.uiToSourceConvertersString;
				}
				bool flag6 = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.sourceToUiConvertersString_UxmlAttributeFlags);
				if (flag6)
				{
					dataBinding.sourceToUiConvertersString = this.sourceToUiConvertersString;
				}
			}

			// Token: 0x0400010A RID: 266
			[UxmlAttribute("data-source-path")]
			[HideInInspector]
			[SerializeField]
			[Tooltip("The path to the value in the data source used by this binding. To see resolved bindings in the UI Builder, define a path that is compatible with the target source property.")]
			private string dataSourcePathString;

			// Token: 0x0400010B RID: 267
			[HideInInspector]
			[UxmlIgnore]
			[SerializeField]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags dataSourcePathString_UxmlAttributeFlags;

			// Token: 0x0400010C RID: 268
			[DataSourceDrawer]
			[SerializeField]
			[HideInInspector]
			[Tooltip("A data source is a collection of information. By default, a binding will inherit the existing data source from the hierarchy. You can instead define another object here as the data source, or define the type of property it may be if the source is not yet available.")]
			private Object dataSource;

			// Token: 0x0400010D RID: 269
			[SerializeField]
			[UxmlIgnore]
			[HideInInspector]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags dataSource_UxmlAttributeFlags;

			// Token: 0x0400010E RID: 270
			[UxmlAttribute("data-source-type")]
			[SerializeField]
			[HideInInspector]
			[UxmlTypeReference(typeof(object))]
			[Tooltip("A data source is a collection of information. By default, a binding will inherit the existing data source from the hierarchy. You can instead define another object here as the data source, or define the type of property it may be if the source is not yet available.")]
			private string dataSourceTypeString;

			// Token: 0x0400010F RID: 271
			[HideInInspector]
			[UxmlIgnore]
			[SerializeField]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags dataSourceTypeString_UxmlAttributeFlags;

			// Token: 0x04000110 RID: 272
			[BindingModeDrawer]
			[SerializeField]
			[HideInInspector]
			[Tooltip("Controls how a binding is updated, which can include the direction in which data is written.")]
			private BindingMode bindingMode;

			// Token: 0x04000111 RID: 273
			[HideInInspector]
			[UxmlIgnore]
			[SerializeField]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags bindingMode_UxmlAttributeFlags;

			// Token: 0x04000112 RID: 274
			[SerializeField]
			[UxmlAttribute("source-to-ui-converters")]
			[ConverterDrawer(isConverterToSource = false)]
			[Tooltip("Define one or more converter groups for this binding that will be used between the data source to the target UI.")]
			[HideInInspector]
			[UxmlAttributeBindingPath("uiToSourceConverters")]
			private string sourceToUiConvertersString;

			// Token: 0x04000113 RID: 275
			[HideInInspector]
			[SerializeField]
			[UxmlIgnore]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags sourceToUiConvertersString_UxmlAttributeFlags;

			// Token: 0x04000114 RID: 276
			[UxmlAttribute("ui-to-source-converters")]
			[HideInInspector]
			[ConverterDrawer(isConverterToSource = true)]
			[UxmlAttributeBindingPath("sourceToUiConverters")]
			[Tooltip("Define one or more converter groups for this binding that will be used between the target UI to the data source.")]
			[SerializeField]
			private string uiToSourceConvertersString;

			// Token: 0x04000115 RID: 277
			[SerializeField]
			[HideInInspector]
			[UxmlIgnore]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags uiToSourceConvertersString_UxmlAttributeFlags;
		}
	}
}
