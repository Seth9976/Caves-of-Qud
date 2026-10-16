using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.Layouts
{
	// Token: 0x02000110 RID: 272
	public class InputControlLayout
	{
		// Token: 0x17000478 RID: 1144
		// (get) Token: 0x06000F87 RID: 3975 RVA: 0x0004AEE0 File Offset: 0x000490E0
		public static InternedString DefaultVariant
		{
			get
			{
				return InputControlLayout.s_DefaultVariant;
			}
		}

		// Token: 0x17000479 RID: 1145
		// (get) Token: 0x06000F88 RID: 3976 RVA: 0x0004AEE7 File Offset: 0x000490E7
		public InternedString name
		{
			get
			{
				return this.m_Name;
			}
		}

		// Token: 0x1700047A RID: 1146
		// (get) Token: 0x06000F89 RID: 3977 RVA: 0x0004AEEF File Offset: 0x000490EF
		public string displayName
		{
			get
			{
				return this.m_DisplayName ?? this.m_Name;
			}
		}

		// Token: 0x1700047B RID: 1147
		// (get) Token: 0x06000F8A RID: 3978 RVA: 0x0004AF06 File Offset: 0x00049106
		public Type type
		{
			get
			{
				return this.m_Type;
			}
		}

		// Token: 0x1700047C RID: 1148
		// (get) Token: 0x06000F8B RID: 3979 RVA: 0x0004AF0E File Offset: 0x0004910E
		public InternedString variants
		{
			get
			{
				return this.m_Variants;
			}
		}

		// Token: 0x1700047D RID: 1149
		// (get) Token: 0x06000F8C RID: 3980 RVA: 0x0004AF16 File Offset: 0x00049116
		public FourCC stateFormat
		{
			get
			{
				return this.m_StateFormat;
			}
		}

		// Token: 0x1700047E RID: 1150
		// (get) Token: 0x06000F8D RID: 3981 RVA: 0x0004AF1E File Offset: 0x0004911E
		public int stateSizeInBytes
		{
			get
			{
				return this.m_StateSizeInBytes;
			}
		}

		// Token: 0x1700047F RID: 1151
		// (get) Token: 0x06000F8E RID: 3982 RVA: 0x0004AF26 File Offset: 0x00049126
		public IEnumerable<InternedString> baseLayouts
		{
			get
			{
				return this.m_BaseLayouts;
			}
		}

		// Token: 0x17000480 RID: 1152
		// (get) Token: 0x06000F8F RID: 3983 RVA: 0x0004AF33 File Offset: 0x00049133
		public IEnumerable<InternedString> appliedOverrides
		{
			get
			{
				return this.m_AppliedOverrides;
			}
		}

		// Token: 0x17000481 RID: 1153
		// (get) Token: 0x06000F90 RID: 3984 RVA: 0x0004AF40 File Offset: 0x00049140
		public ReadOnlyArray<InternedString> commonUsages
		{
			get
			{
				return new ReadOnlyArray<InternedString>(this.m_CommonUsages);
			}
		}

		// Token: 0x17000482 RID: 1154
		// (get) Token: 0x06000F91 RID: 3985 RVA: 0x0004AF4D File Offset: 0x0004914D
		public ReadOnlyArray<InputControlLayout.ControlItem> controls
		{
			get
			{
				return new ReadOnlyArray<InputControlLayout.ControlItem>(this.m_Controls);
			}
		}

		// Token: 0x17000483 RID: 1155
		// (get) Token: 0x06000F92 RID: 3986 RVA: 0x0004AF5A File Offset: 0x0004915A
		public bool updateBeforeRender
		{
			get
			{
				return this.m_UpdateBeforeRender.GetValueOrDefault();
			}
		}

		// Token: 0x17000484 RID: 1156
		// (get) Token: 0x06000F93 RID: 3987 RVA: 0x0004AF67 File Offset: 0x00049167
		public bool isDeviceLayout
		{
			get
			{
				return typeof(InputDevice).IsAssignableFrom(this.m_Type);
			}
		}

		// Token: 0x17000485 RID: 1157
		// (get) Token: 0x06000F94 RID: 3988 RVA: 0x0004AF7E File Offset: 0x0004917E
		public bool isControlLayout
		{
			get
			{
				return !this.isDeviceLayout;
			}
		}

		// Token: 0x17000486 RID: 1158
		// (get) Token: 0x06000F95 RID: 3989 RVA: 0x0004AF89 File Offset: 0x00049189
		// (set) Token: 0x06000F96 RID: 3990 RVA: 0x0004AF96 File Offset: 0x00049196
		public bool isOverride
		{
			get
			{
				return (this.m_Flags & InputControlLayout.Flags.IsOverride) > (InputControlLayout.Flags)0;
			}
			internal set
			{
				if (value)
				{
					this.m_Flags |= InputControlLayout.Flags.IsOverride;
					return;
				}
				this.m_Flags &= ~InputControlLayout.Flags.IsOverride;
			}
		}

		// Token: 0x17000487 RID: 1159
		// (get) Token: 0x06000F97 RID: 3991 RVA: 0x0004AFB9 File Offset: 0x000491B9
		// (set) Token: 0x06000F98 RID: 3992 RVA: 0x0004AFC6 File Offset: 0x000491C6
		public bool isGenericTypeOfDevice
		{
			get
			{
				return (this.m_Flags & InputControlLayout.Flags.IsGenericTypeOfDevice) > (InputControlLayout.Flags)0;
			}
			internal set
			{
				if (value)
				{
					this.m_Flags |= InputControlLayout.Flags.IsGenericTypeOfDevice;
					return;
				}
				this.m_Flags &= ~InputControlLayout.Flags.IsGenericTypeOfDevice;
			}
		}

		// Token: 0x17000488 RID: 1160
		// (get) Token: 0x06000F99 RID: 3993 RVA: 0x0004AFE9 File Offset: 0x000491E9
		// (set) Token: 0x06000F9A RID: 3994 RVA: 0x0004AFF6 File Offset: 0x000491F6
		public bool hideInUI
		{
			get
			{
				return (this.m_Flags & InputControlLayout.Flags.HideInUI) > (InputControlLayout.Flags)0;
			}
			internal set
			{
				if (value)
				{
					this.m_Flags |= InputControlLayout.Flags.HideInUI;
					return;
				}
				this.m_Flags &= ~InputControlLayout.Flags.HideInUI;
			}
		}

		// Token: 0x17000489 RID: 1161
		// (get) Token: 0x06000F9B RID: 3995 RVA: 0x0004B019 File Offset: 0x00049219
		// (set) Token: 0x06000F9C RID: 3996 RVA: 0x0004B027 File Offset: 0x00049227
		public bool isNoisy
		{
			get
			{
				return (this.m_Flags & InputControlLayout.Flags.IsNoisy) > (InputControlLayout.Flags)0;
			}
			internal set
			{
				if (value)
				{
					this.m_Flags |= InputControlLayout.Flags.IsNoisy;
					return;
				}
				this.m_Flags &= ~InputControlLayout.Flags.IsNoisy;
			}
		}

		// Token: 0x1700048A RID: 1162
		// (get) Token: 0x06000F9D RID: 3997 RVA: 0x0004B04C File Offset: 0x0004924C
		// (set) Token: 0x06000F9E RID: 3998 RVA: 0x0004B080 File Offset: 0x00049280
		public bool? canRunInBackground
		{
			get
			{
				if ((this.m_Flags & InputControlLayout.Flags.CanRunInBackgroundIsSet) == (InputControlLayout.Flags)0)
				{
					return null;
				}
				return new bool?((this.m_Flags & InputControlLayout.Flags.CanRunInBackground) > (InputControlLayout.Flags)0);
			}
			internal set
			{
				if (value == null)
				{
					this.m_Flags &= ~InputControlLayout.Flags.CanRunInBackgroundIsSet;
					return;
				}
				this.m_Flags |= InputControlLayout.Flags.CanRunInBackgroundIsSet;
				if (value.Value)
				{
					this.m_Flags |= InputControlLayout.Flags.CanRunInBackground;
					return;
				}
				this.m_Flags &= ~InputControlLayout.Flags.CanRunInBackground;
			}
		}

		// Token: 0x1700048B RID: 1163
		public InputControlLayout.ControlItem this[string path]
		{
			get
			{
				if (string.IsNullOrEmpty(path))
				{
					throw new ArgumentNullException("path");
				}
				if (this.m_Controls != null)
				{
					for (int i = 0; i < this.m_Controls.Length; i++)
					{
						if (this.m_Controls[i].name == path)
						{
							return this.m_Controls[i];
						}
					}
				}
				throw new KeyNotFoundException(string.Format("Cannot find control '{0}' in layout '{1}'", path, this.name));
			}
		}

		// Token: 0x06000FA0 RID: 4000 RVA: 0x0004B158 File Offset: 0x00049358
		public InputControlLayout.ControlItem? FindControl(InternedString path)
		{
			if (string.IsNullOrEmpty(path))
			{
				throw new ArgumentNullException("path");
			}
			if (this.m_Controls == null)
			{
				return null;
			}
			for (int i = 0; i < this.m_Controls.Length; i++)
			{
				if (this.m_Controls[i].name == path)
				{
					return new InputControlLayout.ControlItem?(this.m_Controls[i]);
				}
			}
			return null;
		}

		// Token: 0x06000FA1 RID: 4001 RVA: 0x0004B1D8 File Offset: 0x000493D8
		public InputControlLayout.ControlItem? FindControlIncludingArrayElements(string path, out int arrayIndex)
		{
			if (string.IsNullOrEmpty(path))
			{
				throw new ArgumentNullException("path");
			}
			arrayIndex = -1;
			if (this.m_Controls == null)
			{
				return null;
			}
			int num = 0;
			int num2 = path.Length;
			while (num2 > 0 && char.IsDigit(path[num2 - 1]))
			{
				num2--;
				num *= 10;
				num += (int)(path[num2] - '0');
			}
			int num3 = 0;
			if (num2 < path.Length && num2 > 0)
			{
				num3 = num2;
			}
			for (int i = 0; i < this.m_Controls.Length; i++)
			{
				ref InputControlLayout.ControlItem ptr = ref this.m_Controls[i];
				if (string.Compare(ptr.name, path, StringComparison.InvariantCultureIgnoreCase) == 0)
				{
					return new InputControlLayout.ControlItem?(ptr);
				}
				if (ptr.isArray && num3 > 0 && num3 == ptr.name.length && string.Compare(ptr.name.ToString(), 0, path, 0, num3, StringComparison.InvariantCultureIgnoreCase) == 0)
				{
					arrayIndex = num;
					return new InputControlLayout.ControlItem?(ptr);
				}
			}
			return null;
		}

		// Token: 0x06000FA2 RID: 4002 RVA: 0x0004B2FC File Offset: 0x000494FC
		public Type GetValueType()
		{
			return TypeHelpers.GetGenericTypeArgumentFromHierarchy(this.type, typeof(InputControl<>), 0);
		}

		// Token: 0x06000FA3 RID: 4003 RVA: 0x0004B314 File Offset: 0x00049514
		public static InputControlLayout FromType(string name, Type type)
		{
			List<InputControlLayout.ControlItem> list = new List<InputControlLayout.ControlItem>();
			InputControlLayoutAttribute customAttribute = type.GetCustomAttribute<InputControlLayoutAttribute>(true);
			FourCC fourCC = default(FourCC);
			if (customAttribute != null && customAttribute.stateType != null)
			{
				InputControlLayout.AddControlItems(customAttribute.stateType, list, name);
				if (typeof(IInputStateTypeInfo).IsAssignableFrom(customAttribute.stateType))
				{
					fourCC = ((IInputStateTypeInfo)Activator.CreateInstance(customAttribute.stateType)).format;
				}
			}
			else
			{
				InputControlLayout.AddControlItems(type, list, name);
			}
			if (customAttribute != null && !string.IsNullOrEmpty(customAttribute.stateFormat))
			{
				fourCC = new FourCC(customAttribute.stateFormat);
			}
			InternedString internedString = default(InternedString);
			if (customAttribute != null)
			{
				internedString = new InternedString(customAttribute.variants);
			}
			InputControlLayout inputControlLayout = new InputControlLayout(name, type)
			{
				m_Controls = list.ToArray(),
				m_StateFormat = fourCC,
				m_Variants = internedString,
				m_UpdateBeforeRender = ((customAttribute != null) ? customAttribute.updateBeforeRenderInternal : null),
				isGenericTypeOfDevice = (customAttribute != null && customAttribute.isGenericTypeOfDevice),
				hideInUI = (customAttribute != null && customAttribute.hideInUI),
				m_Description = ((customAttribute != null) ? customAttribute.description : null),
				m_DisplayName = ((customAttribute != null) ? customAttribute.displayName : null),
				canRunInBackground = ((customAttribute != null) ? customAttribute.canRunInBackgroundInternal : null),
				isNoisy = (customAttribute != null && customAttribute.isNoisy)
			};
			if (((customAttribute != null) ? customAttribute.commonUsages : null) != null)
			{
				inputControlLayout.m_CommonUsages = ArrayHelpers.Select<string, InternedString>(customAttribute.commonUsages, (string x) => new InternedString(x));
			}
			return inputControlLayout;
		}

		// Token: 0x06000FA4 RID: 4004 RVA: 0x0004B4B5 File Offset: 0x000496B5
		public string ToJson()
		{
			return JsonUtility.ToJson(InputControlLayout.LayoutJson.FromLayout(this), true);
		}

		// Token: 0x06000FA5 RID: 4005 RVA: 0x0004B4C8 File Offset: 0x000496C8
		public static InputControlLayout FromJson(string json)
		{
			return JsonUtility.FromJson<InputControlLayout.LayoutJson>(json).ToLayout();
		}

		// Token: 0x06000FA6 RID: 4006 RVA: 0x0004B4E3 File Offset: 0x000496E3
		private InputControlLayout(string name, Type type)
		{
			this.m_Name = new InternedString(name);
			this.m_Type = type;
		}

		// Token: 0x06000FA7 RID: 4007 RVA: 0x0004B4FE File Offset: 0x000496FE
		private static void AddControlItems(Type type, List<InputControlLayout.ControlItem> controlLayouts, string layoutName)
		{
			InputControlLayout.AddControlItemsFromFields(type, controlLayouts, layoutName);
			InputControlLayout.AddControlItemsFromProperties(type, controlLayouts, layoutName);
		}

		// Token: 0x06000FA8 RID: 4008 RVA: 0x0004B510 File Offset: 0x00049710
		private static void AddControlItemsFromFields(Type type, List<InputControlLayout.ControlItem> controlLayouts, string layoutName)
		{
			MemberInfo[] fields = type.GetFields(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public);
			InputControlLayout.AddControlItemsFromMembers(fields, controlLayouts, layoutName);
		}

		// Token: 0x06000FA9 RID: 4009 RVA: 0x0004B530 File Offset: 0x00049730
		private static void AddControlItemsFromProperties(Type type, List<InputControlLayout.ControlItem> controlLayouts, string layoutName)
		{
			MemberInfo[] properties = type.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public);
			InputControlLayout.AddControlItemsFromMembers(properties, controlLayouts, layoutName);
		}

		// Token: 0x06000FAA RID: 4010 RVA: 0x0004B550 File Offset: 0x00049750
		private static void AddControlItemsFromMembers(MemberInfo[] members, List<InputControlLayout.ControlItem> controlItems, string layoutName)
		{
			foreach (MemberInfo memberInfo in members)
			{
				if (!(memberInfo.DeclaringType == typeof(InputControl)))
				{
					Type valueType = TypeHelpers.GetValueType(memberInfo);
					if (valueType != null && valueType.IsValueType && typeof(IInputStateTypeInfo).IsAssignableFrom(valueType))
					{
						int count = controlItems.Count;
						InputControlLayout.AddControlItems(valueType, controlItems, layoutName);
						if (memberInfo as FieldInfo != null)
						{
							int num = Marshal.OffsetOf(memberInfo.DeclaringType, memberInfo.Name).ToInt32();
							int count2 = controlItems.Count;
							for (int j = count; j < count2; j++)
							{
								InputControlLayout.ControlItem controlItem = controlItems[j];
								if (controlItems[j].offset != 4294967295U)
								{
									controlItem.offset += (uint)num;
									controlItems[j] = controlItem;
								}
							}
						}
					}
					InputControlAttribute[] array = memberInfo.GetCustomAttributes<InputControlAttribute>(false).ToArray<InputControlAttribute>();
					if (array.Length != 0 || (!(valueType == null) && typeof(InputControl).IsAssignableFrom(valueType) && !(memberInfo is PropertyInfo)))
					{
						InputControlLayout.AddControlItemsFromMember(memberInfo, array, controlItems);
					}
				}
			}
		}

		// Token: 0x06000FAB RID: 4011 RVA: 0x0004B690 File Offset: 0x00049890
		private static void AddControlItemsFromMember(MemberInfo member, InputControlAttribute[] attributes, List<InputControlLayout.ControlItem> controlItems)
		{
			if (attributes.Length == 0)
			{
				InputControlLayout.ControlItem controlItem = InputControlLayout.CreateControlItemFromMember(member, null);
				controlItems.Add(controlItem);
				return;
			}
			foreach (InputControlAttribute inputControlAttribute in attributes)
			{
				InputControlLayout.ControlItem controlItem2 = InputControlLayout.CreateControlItemFromMember(member, inputControlAttribute);
				controlItems.Add(controlItem2);
			}
		}

		// Token: 0x06000FAC RID: 4012 RVA: 0x0004B6D8 File Offset: 0x000498D8
		private static InputControlLayout.ControlItem CreateControlItemFromMember(MemberInfo member, InputControlAttribute attribute)
		{
			string text = ((attribute != null) ? attribute.name : null);
			if (string.IsNullOrEmpty(text))
			{
				text = member.Name;
			}
			bool flag = text.IndexOf('/') != -1;
			string text2 = ((attribute != null) ? attribute.displayName : null);
			string text3 = ((attribute != null) ? attribute.shortDisplayName : null);
			string text4 = ((attribute != null) ? attribute.layout : null);
			if (string.IsNullOrEmpty(text4) && !flag && (!(member is FieldInfo) || member.GetCustomAttribute<FixedBufferAttribute>(false) == null))
			{
				text4 = InputControlLayout.InferLayoutFromValueType(TypeHelpers.GetValueType(member));
			}
			string text5 = null;
			if (attribute != null && !string.IsNullOrEmpty(attribute.variants))
			{
				text5 = attribute.variants;
			}
			uint num = uint.MaxValue;
			if (attribute != null && attribute.offset != 4294967295U)
			{
				num = attribute.offset;
			}
			else if (member is FieldInfo && !flag)
			{
				num = (uint)Marshal.OffsetOf(member.DeclaringType, member.Name).ToInt32();
			}
			uint num2 = uint.MaxValue;
			if (attribute != null)
			{
				num2 = attribute.bit;
			}
			uint num3 = 0U;
			if (attribute != null)
			{
				num3 = attribute.sizeInBits;
			}
			FourCC fourCC = default(FourCC);
			if (attribute != null && !string.IsNullOrEmpty(attribute.format))
			{
				fourCC = new FourCC(attribute.format);
			}
			else if (!flag && num2 == 4294967295U)
			{
				fourCC = InputStateBlock.GetPrimitiveFormatFromType(TypeHelpers.GetValueType(member));
			}
			InternedString[] array = null;
			if (attribute != null)
			{
				string[] array2 = ArrayHelpers.Join<string>(attribute.alias, attribute.aliases);
				if (array2 != null)
				{
					array = array2.Select<string, InternedString>((string x) => new InternedString(x)).ToArray<InternedString>();
				}
			}
			InternedString[] array3 = null;
			if (attribute != null)
			{
				string[] array4 = ArrayHelpers.Join<string>(attribute.usage, attribute.usages);
				if (array4 != null)
				{
					array3 = array4.Select<string, InternedString>((string x) => new InternedString(x)).ToArray<InternedString>();
				}
			}
			NamedValue[] array5 = null;
			if (attribute != null && !string.IsNullOrEmpty(attribute.parameters))
			{
				array5 = NamedValue.ParseMultiple(attribute.parameters);
			}
			NameAndParameters[] array6 = null;
			if (attribute != null && !string.IsNullOrEmpty(attribute.processors))
			{
				array6 = NameAndParameters.ParseMultiple(attribute.processors).ToArray<NameAndParameters>();
			}
			string text6 = null;
			if (attribute != null && !string.IsNullOrEmpty(attribute.useStateFrom))
			{
				text6 = attribute.useStateFrom;
			}
			bool flag2 = false;
			if (attribute != null)
			{
				flag2 = attribute.noisy;
			}
			bool flag3 = false;
			if (attribute != null)
			{
				flag3 = attribute.dontReset;
			}
			bool flag4 = false;
			if (attribute != null)
			{
				flag4 = attribute.synthetic;
			}
			int num4 = 0;
			if (attribute != null)
			{
				num4 = attribute.arraySize;
			}
			PrimitiveValue primitiveValue = default(PrimitiveValue);
			if (attribute != null)
			{
				primitiveValue = PrimitiveValue.FromObject(attribute.defaultState);
			}
			PrimitiveValue primitiveValue2 = default(PrimitiveValue);
			PrimitiveValue primitiveValue3 = default(PrimitiveValue);
			if (attribute != null)
			{
				primitiveValue2 = PrimitiveValue.FromObject(attribute.minValue);
				primitiveValue3 = PrimitiveValue.FromObject(attribute.maxValue);
			}
			return new InputControlLayout.ControlItem
			{
				name = new InternedString(text),
				displayName = text2,
				shortDisplayName = text3,
				layout = new InternedString(text4),
				variants = new InternedString(text5),
				useStateFrom = text6,
				format = fourCC,
				offset = num,
				bit = num2,
				sizeInBits = num3,
				parameters = new ReadOnlyArray<NamedValue>(array5),
				processors = new ReadOnlyArray<NameAndParameters>(array6),
				usages = new ReadOnlyArray<InternedString>(array3),
				aliases = new ReadOnlyArray<InternedString>(array),
				isModifyingExistingControl = flag,
				isFirstDefinedInThisLayout = true,
				isNoisy = flag2,
				dontReset = flag3,
				isSynthetic = flag4,
				arraySize = num4,
				defaultState = primitiveValue,
				minValue = primitiveValue2,
				maxValue = primitiveValue3
			};
		}

		// Token: 0x06000FAD RID: 4013 RVA: 0x0004BA80 File Offset: 0x00049C80
		private static string InferLayoutFromValueType(Type type)
		{
			InternedString internedString = InputControlLayout.s_Layouts.TryFindLayoutForType(type);
			if (internedString.IsEmpty())
			{
				InternedString internedString2 = new InternedString(type.Name);
				if (InputControlLayout.s_Layouts.HasLayout(internedString2))
				{
					internedString = internedString2;
				}
				else if (type.Name.EndsWith("Control"))
				{
					internedString2 = new InternedString(type.Name.Substring(0, type.Name.Length - "Control".Length));
					if (InputControlLayout.s_Layouts.HasLayout(internedString2))
					{
						internedString = internedString2;
					}
				}
			}
			return internedString;
		}

		// Token: 0x06000FAE RID: 4014 RVA: 0x0004BB10 File Offset: 0x00049D10
		public void MergeLayout(InputControlLayout other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			bool? updateBeforeRender = this.m_UpdateBeforeRender;
			this.m_UpdateBeforeRender = ((updateBeforeRender != null) ? updateBeforeRender : other.m_UpdateBeforeRender);
			if (this.m_Variants.IsEmpty())
			{
				this.m_Variants = other.m_Variants;
			}
			if (this.m_Type == null)
			{
				this.m_Type = other.m_Type;
			}
			else if (this.m_Type.IsAssignableFrom(other.m_Type))
			{
				this.m_Type = other.m_Type;
			}
			bool flag = !this.m_Variants.IsEmpty();
			if (this.m_StateFormat == default(FourCC))
			{
				this.m_StateFormat = other.m_StateFormat;
			}
			this.m_CommonUsages = ArrayHelpers.Merge<InternedString>(other.m_CommonUsages, this.m_CommonUsages);
			this.m_AppliedOverrides.Merge(other.m_AppliedOverrides);
			if (string.IsNullOrEmpty(this.m_DisplayName))
			{
				this.m_DisplayName = other.m_DisplayName;
			}
			if (this.m_Controls == null)
			{
				this.m_Controls = other.m_Controls;
				return;
			}
			if (other.m_Controls != null)
			{
				InputControlLayout.ControlItem[] controls = other.m_Controls;
				List<InputControlLayout.ControlItem> list = new List<InputControlLayout.ControlItem>();
				List<string> list2 = new List<string>();
				Dictionary<string, InputControlLayout.ControlItem> dictionary = InputControlLayout.CreateLookupTableForControls(controls, list2);
				foreach (KeyValuePair<string, InputControlLayout.ControlItem> keyValuePair in InputControlLayout.CreateLookupTableForControls(this.m_Controls, null))
				{
					InputControlLayout.ControlItem controlItem;
					if (dictionary.TryGetValue(keyValuePair.Key, out controlItem))
					{
						InputControlLayout.ControlItem controlItem2 = keyValuePair.Value.Merge(controlItem);
						list.Add(controlItem2);
						dictionary.Remove(keyValuePair.Key);
					}
					else if (keyValuePair.Value.variants.IsEmpty() || keyValuePair.Value.variants == InputControlLayout.DefaultVariant)
					{
						bool flag2 = false;
						if (flag)
						{
							for (int i = 0; i < list2.Count; i++)
							{
								if (InputControlLayout.VariantsMatch(this.m_Variants.ToLower(), list2[i]))
								{
									string text = keyValuePair.Key + "@" + list2[i];
									if (dictionary.TryGetValue(text, out controlItem))
									{
										InputControlLayout.ControlItem controlItem3 = keyValuePair.Value.Merge(controlItem);
										list.Add(controlItem3);
										dictionary.Remove(text);
										flag2 = true;
									}
								}
							}
						}
						else
						{
							foreach (string text2 in list2)
							{
								string text3 = keyValuePair.Key + "@" + text2;
								if (dictionary.TryGetValue(text3, out controlItem))
								{
									InputControlLayout.ControlItem controlItem4 = keyValuePair.Value.Merge(controlItem);
									list.Add(controlItem4);
									dictionary.Remove(text3);
									flag2 = true;
								}
							}
						}
						if (!flag2)
						{
							list.Add(keyValuePair.Value);
						}
					}
					else if (dictionary.TryGetValue(keyValuePair.Value.name.ToLower(), out controlItem))
					{
						InputControlLayout.ControlItem controlItem5 = keyValuePair.Value.Merge(controlItem);
						list.Add(controlItem5);
						dictionary.Remove(keyValuePair.Value.name.ToLower());
					}
					else if (InputControlLayout.VariantsMatch(this.m_Variants, keyValuePair.Value.variants))
					{
						list.Add(keyValuePair.Value);
					}
				}
				if (!flag)
				{
					int count = list.Count;
					list.AddRange(dictionary.Values);
					for (int j = count; j < list.Count; j++)
					{
						InputControlLayout.ControlItem controlItem6 = list[j];
						controlItem6.isFirstDefinedInThisLayout = false;
						list[j] = controlItem6;
					}
				}
				else
				{
					int count2 = list.Count;
					list.AddRange(dictionary.Values.Where<InputControlLayout.ControlItem>((InputControlLayout.ControlItem x) => InputControlLayout.VariantsMatch(this.m_Variants, x.variants)));
					for (int k = count2; k < list.Count; k++)
					{
						InputControlLayout.ControlItem controlItem7 = list[k];
						controlItem7.isFirstDefinedInThisLayout = false;
						list[k] = controlItem7;
					}
				}
				this.m_Controls = list.ToArray();
			}
		}

		// Token: 0x06000FAF RID: 4015 RVA: 0x0004BF90 File Offset: 0x0004A190
		private static Dictionary<string, InputControlLayout.ControlItem> CreateLookupTableForControls(InputControlLayout.ControlItem[] controlItems, List<string> variants = null)
		{
			Dictionary<string, InputControlLayout.ControlItem> dictionary = new Dictionary<string, InputControlLayout.ControlItem>();
			int i = 0;
			while (i < controlItems.Length)
			{
				string text = controlItems[i].name.ToLower();
				InternedString variants2 = controlItems[i].variants;
				if (variants2.IsEmpty() || !(variants2 != InputControlLayout.DefaultVariant))
				{
					goto IL_00EC;
				}
				if (variants2.ToString().IndexOf(";"[0]) != -1)
				{
					foreach (string text2 in variants2.ToLower().Split(";"[0], StringSplitOptions.None))
					{
						if (variants != null)
						{
							variants.Add(text2);
						}
						text = text + "@" + text2;
						dictionary[text] = controlItems[i];
					}
				}
				else
				{
					text = text + "@" + variants2.ToLower();
					if (variants != null)
					{
						variants.Add(variants2.ToLower());
						goto IL_00EC;
					}
					goto IL_00EC;
				}
				IL_00FA:
				i++;
				continue;
				IL_00EC:
				dictionary[text] = controlItems[i];
				goto IL_00FA;
			}
			return dictionary;
		}

		// Token: 0x06000FB0 RID: 4016 RVA: 0x0004C0A5 File Offset: 0x0004A2A5
		internal static bool VariantsMatch(InternedString expected, InternedString actual)
		{
			return InputControlLayout.VariantsMatch(expected.ToLower(), actual.ToLower());
		}

		// Token: 0x06000FB1 RID: 4017 RVA: 0x0004C0BA File Offset: 0x0004A2BA
		internal static bool VariantsMatch(string expected, string actual)
		{
			return (actual != null && StringHelpers.CharacterSeparatedListsHaveAtLeastOneCommonElement(InputControlLayout.DefaultVariant, actual, ";"[0])) || expected == null || actual == null || StringHelpers.CharacterSeparatedListsHaveAtLeastOneCommonElement(expected, actual, ";"[0]);
		}

		// Token: 0x06000FB2 RID: 4018 RVA: 0x0004C0FC File Offset: 0x0004A2FC
		internal static void ParseHeaderFieldsFromJson(string json, out InternedString name, out InlinedArray<InternedString> baseLayouts, out InputDeviceMatcher deviceMatcher)
		{
			InputControlLayout.LayoutJsonNameAndDescriptorOnly layoutJsonNameAndDescriptorOnly = JsonUtility.FromJson<InputControlLayout.LayoutJsonNameAndDescriptorOnly>(json);
			name = new InternedString(layoutJsonNameAndDescriptorOnly.name);
			baseLayouts = default(InlinedArray<InternedString>);
			if (!string.IsNullOrEmpty(layoutJsonNameAndDescriptorOnly.extend))
			{
				baseLayouts.Append(new InternedString(layoutJsonNameAndDescriptorOnly.extend));
			}
			if (layoutJsonNameAndDescriptorOnly.extendMultiple != null)
			{
				foreach (string text in layoutJsonNameAndDescriptorOnly.extendMultiple)
				{
					baseLayouts.Append(new InternedString(text));
				}
			}
			deviceMatcher = layoutJsonNameAndDescriptorOnly.device.ToMatcher();
		}

		// Token: 0x1700048C RID: 1164
		// (get) Token: 0x06000FB3 RID: 4019 RVA: 0x0004C187 File Offset: 0x0004A387
		internal static ref InputControlLayout.Cache cache
		{
			get
			{
				return ref InputControlLayout.s_CacheInstance;
			}
		}

		// Token: 0x06000FB4 RID: 4020 RVA: 0x0004C190 File Offset: 0x0004A390
		internal static InputControlLayout.CacheRefInstance CacheRef()
		{
			InputControlLayout.s_CacheInstanceRef++;
			return new InputControlLayout.CacheRefInstance
			{
				valid = true
			};
		}

		// Token: 0x0400067F RID: 1663
		private static InternedString s_DefaultVariant = new InternedString("Default");

		// Token: 0x04000680 RID: 1664
		public const string VariantSeparator = ";";

		// Token: 0x04000681 RID: 1665
		private InternedString m_Name;

		// Token: 0x04000682 RID: 1666
		private Type m_Type;

		// Token: 0x04000683 RID: 1667
		private InternedString m_Variants;

		// Token: 0x04000684 RID: 1668
		private FourCC m_StateFormat;

		// Token: 0x04000685 RID: 1669
		internal int m_StateSizeInBytes;

		// Token: 0x04000686 RID: 1670
		internal bool? m_UpdateBeforeRender;

		// Token: 0x04000687 RID: 1671
		internal InlinedArray<InternedString> m_BaseLayouts;

		// Token: 0x04000688 RID: 1672
		private InlinedArray<InternedString> m_AppliedOverrides;

		// Token: 0x04000689 RID: 1673
		private InternedString[] m_CommonUsages;

		// Token: 0x0400068A RID: 1674
		internal InputControlLayout.ControlItem[] m_Controls;

		// Token: 0x0400068B RID: 1675
		internal string m_DisplayName;

		// Token: 0x0400068C RID: 1676
		private string m_Description;

		// Token: 0x0400068D RID: 1677
		private InputControlLayout.Flags m_Flags;

		// Token: 0x0400068E RID: 1678
		internal static InputControlLayout.Collection s_Layouts;

		// Token: 0x0400068F RID: 1679
		internal static InputControlLayout.Cache s_CacheInstance;

		// Token: 0x04000690 RID: 1680
		internal static int s_CacheInstanceRef;

		// Token: 0x0200022B RID: 555
		public struct ControlItem
		{
			// Token: 0x170005D8 RID: 1496
			// (get) Token: 0x06001598 RID: 5528 RVA: 0x000641AF File Offset: 0x000623AF
			// (set) Token: 0x06001599 RID: 5529 RVA: 0x000641B7 File Offset: 0x000623B7
			public InternedString name { readonly get; internal set; }

			// Token: 0x170005D9 RID: 1497
			// (get) Token: 0x0600159A RID: 5530 RVA: 0x000641C0 File Offset: 0x000623C0
			// (set) Token: 0x0600159B RID: 5531 RVA: 0x000641C8 File Offset: 0x000623C8
			public InternedString layout { readonly get; internal set; }

			// Token: 0x170005DA RID: 1498
			// (get) Token: 0x0600159C RID: 5532 RVA: 0x000641D1 File Offset: 0x000623D1
			// (set) Token: 0x0600159D RID: 5533 RVA: 0x000641D9 File Offset: 0x000623D9
			public InternedString variants { readonly get; internal set; }

			// Token: 0x170005DB RID: 1499
			// (get) Token: 0x0600159E RID: 5534 RVA: 0x000641E2 File Offset: 0x000623E2
			// (set) Token: 0x0600159F RID: 5535 RVA: 0x000641EA File Offset: 0x000623EA
			public string useStateFrom { readonly get; internal set; }

			// Token: 0x170005DC RID: 1500
			// (get) Token: 0x060015A0 RID: 5536 RVA: 0x000641F3 File Offset: 0x000623F3
			// (set) Token: 0x060015A1 RID: 5537 RVA: 0x000641FB File Offset: 0x000623FB
			public string displayName { readonly get; internal set; }

			// Token: 0x170005DD RID: 1501
			// (get) Token: 0x060015A2 RID: 5538 RVA: 0x00064204 File Offset: 0x00062404
			// (set) Token: 0x060015A3 RID: 5539 RVA: 0x0006420C File Offset: 0x0006240C
			public string shortDisplayName { readonly get; internal set; }

			// Token: 0x170005DE RID: 1502
			// (get) Token: 0x060015A4 RID: 5540 RVA: 0x00064215 File Offset: 0x00062415
			// (set) Token: 0x060015A5 RID: 5541 RVA: 0x0006421D File Offset: 0x0006241D
			public ReadOnlyArray<InternedString> usages { readonly get; internal set; }

			// Token: 0x170005DF RID: 1503
			// (get) Token: 0x060015A6 RID: 5542 RVA: 0x00064226 File Offset: 0x00062426
			// (set) Token: 0x060015A7 RID: 5543 RVA: 0x0006422E File Offset: 0x0006242E
			public ReadOnlyArray<InternedString> aliases { readonly get; internal set; }

			// Token: 0x170005E0 RID: 1504
			// (get) Token: 0x060015A8 RID: 5544 RVA: 0x00064237 File Offset: 0x00062437
			// (set) Token: 0x060015A9 RID: 5545 RVA: 0x0006423F File Offset: 0x0006243F
			public ReadOnlyArray<NamedValue> parameters { readonly get; internal set; }

			// Token: 0x170005E1 RID: 1505
			// (get) Token: 0x060015AA RID: 5546 RVA: 0x00064248 File Offset: 0x00062448
			// (set) Token: 0x060015AB RID: 5547 RVA: 0x00064250 File Offset: 0x00062450
			public ReadOnlyArray<NameAndParameters> processors { readonly get; internal set; }

			// Token: 0x170005E2 RID: 1506
			// (get) Token: 0x060015AC RID: 5548 RVA: 0x00064259 File Offset: 0x00062459
			// (set) Token: 0x060015AD RID: 5549 RVA: 0x00064261 File Offset: 0x00062461
			public uint offset { readonly get; internal set; }

			// Token: 0x170005E3 RID: 1507
			// (get) Token: 0x060015AE RID: 5550 RVA: 0x0006426A File Offset: 0x0006246A
			// (set) Token: 0x060015AF RID: 5551 RVA: 0x00064272 File Offset: 0x00062472
			public uint bit { readonly get; internal set; }

			// Token: 0x170005E4 RID: 1508
			// (get) Token: 0x060015B0 RID: 5552 RVA: 0x0006427B File Offset: 0x0006247B
			// (set) Token: 0x060015B1 RID: 5553 RVA: 0x00064283 File Offset: 0x00062483
			public uint sizeInBits { readonly get; internal set; }

			// Token: 0x170005E5 RID: 1509
			// (get) Token: 0x060015B2 RID: 5554 RVA: 0x0006428C File Offset: 0x0006248C
			// (set) Token: 0x060015B3 RID: 5555 RVA: 0x00064294 File Offset: 0x00062494
			public FourCC format { readonly get; internal set; }

			// Token: 0x170005E6 RID: 1510
			// (get) Token: 0x060015B4 RID: 5556 RVA: 0x0006429D File Offset: 0x0006249D
			// (set) Token: 0x060015B5 RID: 5557 RVA: 0x000642A5 File Offset: 0x000624A5
			private InputControlLayout.ControlItem.Flags flags { readonly get; set; }

			// Token: 0x170005E7 RID: 1511
			// (get) Token: 0x060015B6 RID: 5558 RVA: 0x000642AE File Offset: 0x000624AE
			// (set) Token: 0x060015B7 RID: 5559 RVA: 0x000642B6 File Offset: 0x000624B6
			public int arraySize { readonly get; internal set; }

			// Token: 0x170005E8 RID: 1512
			// (get) Token: 0x060015B8 RID: 5560 RVA: 0x000642BF File Offset: 0x000624BF
			// (set) Token: 0x060015B9 RID: 5561 RVA: 0x000642C7 File Offset: 0x000624C7
			public PrimitiveValue defaultState { readonly get; internal set; }

			// Token: 0x170005E9 RID: 1513
			// (get) Token: 0x060015BA RID: 5562 RVA: 0x000642D0 File Offset: 0x000624D0
			// (set) Token: 0x060015BB RID: 5563 RVA: 0x000642D8 File Offset: 0x000624D8
			public PrimitiveValue minValue { readonly get; internal set; }

			// Token: 0x170005EA RID: 1514
			// (get) Token: 0x060015BC RID: 5564 RVA: 0x000642E1 File Offset: 0x000624E1
			// (set) Token: 0x060015BD RID: 5565 RVA: 0x000642E9 File Offset: 0x000624E9
			public PrimitiveValue maxValue { readonly get; internal set; }

			// Token: 0x170005EB RID: 1515
			// (get) Token: 0x060015BE RID: 5566 RVA: 0x000642F2 File Offset: 0x000624F2
			// (set) Token: 0x060015BF RID: 5567 RVA: 0x000642FF File Offset: 0x000624FF
			public bool isModifyingExistingControl
			{
				get
				{
					return (this.flags & InputControlLayout.ControlItem.Flags.isModifyingExistingControl) == InputControlLayout.ControlItem.Flags.isModifyingExistingControl;
				}
				internal set
				{
					if (value)
					{
						this.flags |= InputControlLayout.ControlItem.Flags.isModifyingExistingControl;
						return;
					}
					this.flags &= ~InputControlLayout.ControlItem.Flags.isModifyingExistingControl;
				}
			}

			// Token: 0x170005EC RID: 1516
			// (get) Token: 0x060015C0 RID: 5568 RVA: 0x00064322 File Offset: 0x00062522
			// (set) Token: 0x060015C1 RID: 5569 RVA: 0x0006432F File Offset: 0x0006252F
			public bool isNoisy
			{
				get
				{
					return (this.flags & InputControlLayout.ControlItem.Flags.IsNoisy) == InputControlLayout.ControlItem.Flags.IsNoisy;
				}
				internal set
				{
					if (value)
					{
						this.flags |= InputControlLayout.ControlItem.Flags.IsNoisy;
						return;
					}
					this.flags &= ~InputControlLayout.ControlItem.Flags.IsNoisy;
				}
			}

			// Token: 0x170005ED RID: 1517
			// (get) Token: 0x060015C2 RID: 5570 RVA: 0x00064352 File Offset: 0x00062552
			// (set) Token: 0x060015C3 RID: 5571 RVA: 0x0006435F File Offset: 0x0006255F
			public bool isSynthetic
			{
				get
				{
					return (this.flags & InputControlLayout.ControlItem.Flags.IsSynthetic) == InputControlLayout.ControlItem.Flags.IsSynthetic;
				}
				internal set
				{
					if (value)
					{
						this.flags |= InputControlLayout.ControlItem.Flags.IsSynthetic;
						return;
					}
					this.flags &= ~InputControlLayout.ControlItem.Flags.IsSynthetic;
				}
			}

			// Token: 0x170005EE RID: 1518
			// (get) Token: 0x060015C4 RID: 5572 RVA: 0x00064382 File Offset: 0x00062582
			// (set) Token: 0x060015C5 RID: 5573 RVA: 0x00064391 File Offset: 0x00062591
			public bool dontReset
			{
				get
				{
					return (this.flags & InputControlLayout.ControlItem.Flags.DontReset) == InputControlLayout.ControlItem.Flags.DontReset;
				}
				internal set
				{
					if (value)
					{
						this.flags |= InputControlLayout.ControlItem.Flags.DontReset;
						return;
					}
					this.flags &= ~InputControlLayout.ControlItem.Flags.DontReset;
				}
			}

			// Token: 0x170005EF RID: 1519
			// (get) Token: 0x060015C6 RID: 5574 RVA: 0x000643B5 File Offset: 0x000625B5
			// (set) Token: 0x060015C7 RID: 5575 RVA: 0x000643C2 File Offset: 0x000625C2
			public bool isFirstDefinedInThisLayout
			{
				get
				{
					return (this.flags & InputControlLayout.ControlItem.Flags.IsFirstDefinedInThisLayout) > (InputControlLayout.ControlItem.Flags)0;
				}
				internal set
				{
					if (value)
					{
						this.flags |= InputControlLayout.ControlItem.Flags.IsFirstDefinedInThisLayout;
						return;
					}
					this.flags &= ~InputControlLayout.ControlItem.Flags.IsFirstDefinedInThisLayout;
				}
			}

			// Token: 0x170005F0 RID: 1520
			// (get) Token: 0x060015C8 RID: 5576 RVA: 0x000643E5 File Offset: 0x000625E5
			public bool isArray
			{
				get
				{
					return this.arraySize != 0;
				}
			}

			// Token: 0x060015C9 RID: 5577 RVA: 0x000643F0 File Offset: 0x000625F0
			public InputControlLayout.ControlItem Merge(InputControlLayout.ControlItem other)
			{
				InputControlLayout.ControlItem controlItem = default(InputControlLayout.ControlItem);
				controlItem.name = this.name;
				controlItem.isModifyingExistingControl = this.isModifyingExistingControl;
				controlItem.displayName = (string.IsNullOrEmpty(this.displayName) ? other.displayName : this.displayName);
				controlItem.shortDisplayName = (string.IsNullOrEmpty(this.shortDisplayName) ? other.shortDisplayName : this.shortDisplayName);
				controlItem.layout = (this.layout.IsEmpty() ? other.layout : this.layout);
				controlItem.variants = (this.variants.IsEmpty() ? other.variants : this.variants);
				controlItem.useStateFrom = this.useStateFrom ?? other.useStateFrom;
				controlItem.arraySize = ((!this.isArray) ? other.arraySize : this.arraySize);
				controlItem.isNoisy = this.isNoisy || other.isNoisy;
				controlItem.dontReset = this.dontReset || other.dontReset;
				controlItem.isSynthetic = this.isSynthetic || other.isSynthetic;
				controlItem.isFirstDefinedInThisLayout = false;
				if (this.offset != 4294967295U)
				{
					controlItem.offset = this.offset;
				}
				else
				{
					controlItem.offset = other.offset;
				}
				if (this.bit != 4294967295U)
				{
					controlItem.bit = this.bit;
				}
				else
				{
					controlItem.bit = other.bit;
				}
				if (this.format != 0)
				{
					controlItem.format = this.format;
				}
				else
				{
					controlItem.format = other.format;
				}
				if (this.sizeInBits != 0U)
				{
					controlItem.sizeInBits = this.sizeInBits;
				}
				else
				{
					controlItem.sizeInBits = other.sizeInBits;
				}
				if (this.aliases.Count > 0)
				{
					controlItem.aliases = this.aliases;
				}
				else
				{
					controlItem.aliases = other.aliases;
				}
				if (this.usages.Count > 0)
				{
					controlItem.usages = this.usages;
				}
				else
				{
					controlItem.usages = other.usages;
				}
				if (this.parameters.Count == 0)
				{
					controlItem.parameters = other.parameters;
				}
				else
				{
					controlItem.parameters = this.parameters;
				}
				if (this.processors.Count == 0)
				{
					controlItem.processors = other.processors;
				}
				else
				{
					controlItem.processors = this.processors;
				}
				if (!string.IsNullOrEmpty(this.displayName))
				{
					controlItem.displayName = this.displayName;
				}
				else
				{
					controlItem.displayName = other.displayName;
				}
				if (!this.defaultState.isEmpty)
				{
					controlItem.defaultState = this.defaultState;
				}
				else
				{
					controlItem.defaultState = other.defaultState;
				}
				if (!this.minValue.isEmpty)
				{
					controlItem.minValue = this.minValue;
				}
				else
				{
					controlItem.minValue = other.minValue;
				}
				if (!this.maxValue.isEmpty)
				{
					controlItem.maxValue = this.maxValue;
				}
				else
				{
					controlItem.maxValue = other.maxValue;
				}
				return controlItem;
			}

			// Token: 0x02000284 RID: 644
			[Flags]
			private enum Flags
			{
				// Token: 0x04000D2B RID: 3371
				isModifyingExistingControl = 1,
				// Token: 0x04000D2C RID: 3372
				IsNoisy = 2,
				// Token: 0x04000D2D RID: 3373
				IsSynthetic = 4,
				// Token: 0x04000D2E RID: 3374
				IsFirstDefinedInThisLayout = 8,
				// Token: 0x04000D2F RID: 3375
				DontReset = 16
			}
		}

		// Token: 0x0200022C RID: 556
		public class Builder
		{
			// Token: 0x170005F1 RID: 1521
			// (get) Token: 0x060015CA RID: 5578 RVA: 0x00064747 File Offset: 0x00062947
			// (set) Token: 0x060015CB RID: 5579 RVA: 0x0006474F File Offset: 0x0006294F
			public string name { get; set; }

			// Token: 0x170005F2 RID: 1522
			// (get) Token: 0x060015CC RID: 5580 RVA: 0x00064758 File Offset: 0x00062958
			// (set) Token: 0x060015CD RID: 5581 RVA: 0x00064760 File Offset: 0x00062960
			public string displayName { get; set; }

			// Token: 0x170005F3 RID: 1523
			// (get) Token: 0x060015CE RID: 5582 RVA: 0x00064769 File Offset: 0x00062969
			// (set) Token: 0x060015CF RID: 5583 RVA: 0x00064771 File Offset: 0x00062971
			public Type type { get; set; }

			// Token: 0x170005F4 RID: 1524
			// (get) Token: 0x060015D0 RID: 5584 RVA: 0x0006477A File Offset: 0x0006297A
			// (set) Token: 0x060015D1 RID: 5585 RVA: 0x00064782 File Offset: 0x00062982
			public FourCC stateFormat { get; set; }

			// Token: 0x170005F5 RID: 1525
			// (get) Token: 0x060015D2 RID: 5586 RVA: 0x0006478B File Offset: 0x0006298B
			// (set) Token: 0x060015D3 RID: 5587 RVA: 0x00064793 File Offset: 0x00062993
			public int stateSizeInBytes { get; set; }

			// Token: 0x170005F6 RID: 1526
			// (get) Token: 0x060015D4 RID: 5588 RVA: 0x0006479C File Offset: 0x0006299C
			// (set) Token: 0x060015D5 RID: 5589 RVA: 0x000647A4 File Offset: 0x000629A4
			public string extendsLayout
			{
				get
				{
					return this.m_ExtendsLayout;
				}
				set
				{
					if (!string.IsNullOrEmpty(value))
					{
						this.m_ExtendsLayout = value;
						return;
					}
					this.m_ExtendsLayout = null;
				}
			}

			// Token: 0x170005F7 RID: 1527
			// (get) Token: 0x060015D6 RID: 5590 RVA: 0x000647BD File Offset: 0x000629BD
			// (set) Token: 0x060015D7 RID: 5591 RVA: 0x000647C5 File Offset: 0x000629C5
			public bool? updateBeforeRender { get; set; }

			// Token: 0x170005F8 RID: 1528
			// (get) Token: 0x060015D8 RID: 5592 RVA: 0x000647CE File Offset: 0x000629CE
			public ReadOnlyArray<InputControlLayout.ControlItem> controls
			{
				get
				{
					return new ReadOnlyArray<InputControlLayout.ControlItem>(this.m_Controls, 0, this.m_ControlCount);
				}
			}

			// Token: 0x060015D9 RID: 5593 RVA: 0x000647E4 File Offset: 0x000629E4
			public InputControlLayout.Builder.ControlBuilder AddControl(string name)
			{
				if (string.IsNullOrEmpty(name))
				{
					throw new ArgumentException(name);
				}
				int num = ArrayHelpers.AppendWithCapacity<InputControlLayout.ControlItem>(ref this.m_Controls, ref this.m_ControlCount, new InputControlLayout.ControlItem
				{
					name = new InternedString(name),
					isModifyingExistingControl = (name.IndexOf('/') != -1),
					offset = uint.MaxValue,
					bit = uint.MaxValue
				}, 10);
				return new InputControlLayout.Builder.ControlBuilder
				{
					builder = this,
					index = num
				};
			}

			// Token: 0x060015DA RID: 5594 RVA: 0x00064868 File Offset: 0x00062A68
			public InputControlLayout.Builder WithName(string name)
			{
				this.name = name;
				return this;
			}

			// Token: 0x060015DB RID: 5595 RVA: 0x00064872 File Offset: 0x00062A72
			public InputControlLayout.Builder WithDisplayName(string displayName)
			{
				this.displayName = displayName;
				return this;
			}

			// Token: 0x060015DC RID: 5596 RVA: 0x0006487C File Offset: 0x00062A7C
			public InputControlLayout.Builder WithType<T>() where T : InputControl
			{
				this.type = typeof(T);
				return this;
			}

			// Token: 0x060015DD RID: 5597 RVA: 0x0006488F File Offset: 0x00062A8F
			public InputControlLayout.Builder WithFormat(FourCC format)
			{
				this.stateFormat = format;
				return this;
			}

			// Token: 0x060015DE RID: 5598 RVA: 0x00064899 File Offset: 0x00062A99
			public InputControlLayout.Builder WithFormat(string format)
			{
				return this.WithFormat(new FourCC(format));
			}

			// Token: 0x060015DF RID: 5599 RVA: 0x000648A7 File Offset: 0x00062AA7
			public InputControlLayout.Builder WithSizeInBytes(int sizeInBytes)
			{
				this.stateSizeInBytes = sizeInBytes;
				return this;
			}

			// Token: 0x060015E0 RID: 5600 RVA: 0x000648B1 File Offset: 0x00062AB1
			public InputControlLayout.Builder Extend(string baseLayoutName)
			{
				this.extendsLayout = baseLayoutName;
				return this;
			}

			// Token: 0x060015E1 RID: 5601 RVA: 0x000648BC File Offset: 0x00062ABC
			public InputControlLayout Build()
			{
				InputControlLayout.ControlItem[] array = null;
				if (this.m_ControlCount > 0)
				{
					array = new InputControlLayout.ControlItem[this.m_ControlCount];
					Array.Copy(this.m_Controls, array, this.m_ControlCount);
				}
				return new InputControlLayout(new InternedString(this.name), (this.type == null && string.IsNullOrEmpty(this.extendsLayout)) ? typeof(InputDevice) : this.type)
				{
					m_DisplayName = this.displayName,
					m_StateFormat = this.stateFormat,
					m_StateSizeInBytes = this.stateSizeInBytes,
					m_BaseLayouts = ((!string.IsNullOrEmpty(this.extendsLayout)) ? new InlinedArray<InternedString>(new InternedString(this.extendsLayout)) : default(InlinedArray<InternedString>)),
					m_Controls = array,
					m_UpdateBeforeRender = this.updateBeforeRender
				};
			}

			// Token: 0x04000BFB RID: 3067
			private string m_ExtendsLayout;

			// Token: 0x04000BFD RID: 3069
			private int m_ControlCount;

			// Token: 0x04000BFE RID: 3070
			private InputControlLayout.ControlItem[] m_Controls;

			// Token: 0x02000285 RID: 645
			public struct ControlBuilder
			{
				// Token: 0x060016B5 RID: 5813 RVA: 0x00067BF4 File Offset: 0x00065DF4
				public InputControlLayout.Builder.ControlBuilder WithDisplayName(string displayName)
				{
					this.builder.m_Controls[this.index].displayName = displayName;
					return this;
				}

				// Token: 0x060016B6 RID: 5814 RVA: 0x00067C18 File Offset: 0x00065E18
				public InputControlLayout.Builder.ControlBuilder WithLayout(string layout)
				{
					if (string.IsNullOrEmpty(layout))
					{
						throw new ArgumentException("Layout name cannot be null or empty", "layout");
					}
					this.builder.m_Controls[this.index].layout = new InternedString(layout);
					return this;
				}

				// Token: 0x060016B7 RID: 5815 RVA: 0x00067C64 File Offset: 0x00065E64
				public InputControlLayout.Builder.ControlBuilder WithFormat(FourCC format)
				{
					this.builder.m_Controls[this.index].format = format;
					return this;
				}

				// Token: 0x060016B8 RID: 5816 RVA: 0x00067C88 File Offset: 0x00065E88
				public InputControlLayout.Builder.ControlBuilder WithFormat(string format)
				{
					return this.WithFormat(new FourCC(format));
				}

				// Token: 0x060016B9 RID: 5817 RVA: 0x00067C96 File Offset: 0x00065E96
				public InputControlLayout.Builder.ControlBuilder WithByteOffset(uint offset)
				{
					this.builder.m_Controls[this.index].offset = offset;
					return this;
				}

				// Token: 0x060016BA RID: 5818 RVA: 0x00067CBA File Offset: 0x00065EBA
				public InputControlLayout.Builder.ControlBuilder WithBitOffset(uint bit)
				{
					this.builder.m_Controls[this.index].bit = bit;
					return this;
				}

				// Token: 0x060016BB RID: 5819 RVA: 0x00067CDE File Offset: 0x00065EDE
				public InputControlLayout.Builder.ControlBuilder IsSynthetic(bool value)
				{
					this.builder.m_Controls[this.index].isSynthetic = value;
					return this;
				}

				// Token: 0x060016BC RID: 5820 RVA: 0x00067D02 File Offset: 0x00065F02
				public InputControlLayout.Builder.ControlBuilder IsNoisy(bool value)
				{
					this.builder.m_Controls[this.index].isNoisy = value;
					return this;
				}

				// Token: 0x060016BD RID: 5821 RVA: 0x00067D26 File Offset: 0x00065F26
				public InputControlLayout.Builder.ControlBuilder DontReset(bool value)
				{
					this.builder.m_Controls[this.index].dontReset = value;
					return this;
				}

				// Token: 0x060016BE RID: 5822 RVA: 0x00067D4A File Offset: 0x00065F4A
				public InputControlLayout.Builder.ControlBuilder WithSizeInBits(uint sizeInBits)
				{
					this.builder.m_Controls[this.index].sizeInBits = sizeInBits;
					return this;
				}

				// Token: 0x060016BF RID: 5823 RVA: 0x00067D70 File Offset: 0x00065F70
				public InputControlLayout.Builder.ControlBuilder WithRange(float minValue, float maxValue)
				{
					this.builder.m_Controls[this.index].minValue = minValue;
					this.builder.m_Controls[this.index].maxValue = maxValue;
					return this;
				}

				// Token: 0x060016C0 RID: 5824 RVA: 0x00067DC8 File Offset: 0x00065FC8
				public InputControlLayout.Builder.ControlBuilder WithUsages(params InternedString[] usages)
				{
					if (usages == null || usages.Length == 0)
					{
						return this;
					}
					for (int i = 0; i < usages.Length; i++)
					{
						if (usages[i].IsEmpty())
						{
							throw new ArgumentException(string.Format("Empty usage entry at index {0} for control '{1}' in layout '{2}'", i, this.builder.m_Controls[this.index].name, this.builder.name), "usages");
						}
					}
					this.builder.m_Controls[this.index].usages = new ReadOnlyArray<InternedString>(usages);
					return this;
				}

				// Token: 0x060016C1 RID: 5825 RVA: 0x00067E6C File Offset: 0x0006606C
				public InputControlLayout.Builder.ControlBuilder WithUsages(IEnumerable<string> usages)
				{
					InternedString[] array = usages.Select<string, InternedString>((string x) => new InternedString(x)).ToArray<InternedString>();
					return this.WithUsages(array);
				}

				// Token: 0x060016C2 RID: 5826 RVA: 0x00067EAB File Offset: 0x000660AB
				public InputControlLayout.Builder.ControlBuilder WithUsages(params string[] usages)
				{
					return this.WithUsages(usages);
				}

				// Token: 0x060016C3 RID: 5827 RVA: 0x00067EB4 File Offset: 0x000660B4
				public InputControlLayout.Builder.ControlBuilder WithParameters(string parameters)
				{
					if (string.IsNullOrEmpty(parameters))
					{
						return this;
					}
					NamedValue[] array = NamedValue.ParseMultiple(parameters);
					this.builder.m_Controls[this.index].parameters = new ReadOnlyArray<NamedValue>(array);
					return this;
				}

				// Token: 0x060016C4 RID: 5828 RVA: 0x00067F00 File Offset: 0x00066100
				public InputControlLayout.Builder.ControlBuilder WithProcessors(string processors)
				{
					if (string.IsNullOrEmpty(processors))
					{
						return this;
					}
					NameAndParameters[] array = NameAndParameters.ParseMultiple(processors).ToArray<NameAndParameters>();
					this.builder.m_Controls[this.index].processors = new ReadOnlyArray<NameAndParameters>(array);
					return this;
				}

				// Token: 0x060016C5 RID: 5829 RVA: 0x00067F4F File Offset: 0x0006614F
				public InputControlLayout.Builder.ControlBuilder WithDefaultState(PrimitiveValue value)
				{
					this.builder.m_Controls[this.index].defaultState = value;
					return this;
				}

				// Token: 0x060016C6 RID: 5830 RVA: 0x00067F73 File Offset: 0x00066173
				public InputControlLayout.Builder.ControlBuilder UsingStateFrom(string path)
				{
					if (string.IsNullOrEmpty(path))
					{
						return this;
					}
					this.builder.m_Controls[this.index].useStateFrom = path;
					return this;
				}

				// Token: 0x060016C7 RID: 5831 RVA: 0x00067FA6 File Offset: 0x000661A6
				public InputControlLayout.Builder.ControlBuilder AsArrayOfControlsWithSize(int arraySize)
				{
					this.builder.m_Controls[this.index].arraySize = arraySize;
					return this;
				}

				// Token: 0x04000D30 RID: 3376
				internal InputControlLayout.Builder builder;

				// Token: 0x04000D31 RID: 3377
				internal int index;
			}
		}

		// Token: 0x0200022D RID: 557
		[Flags]
		private enum Flags
		{
			// Token: 0x04000C00 RID: 3072
			IsGenericTypeOfDevice = 1,
			// Token: 0x04000C01 RID: 3073
			HideInUI = 2,
			// Token: 0x04000C02 RID: 3074
			IsOverride = 4,
			// Token: 0x04000C03 RID: 3075
			CanRunInBackground = 8,
			// Token: 0x04000C04 RID: 3076
			CanRunInBackgroundIsSet = 16,
			// Token: 0x04000C05 RID: 3077
			IsNoisy = 32
		}

		// Token: 0x0200022E RID: 558
		[Serializable]
		internal struct LayoutJsonNameAndDescriptorOnly
		{
			// Token: 0x04000C06 RID: 3078
			public string name;

			// Token: 0x04000C07 RID: 3079
			public string extend;

			// Token: 0x04000C08 RID: 3080
			public string[] extendMultiple;

			// Token: 0x04000C09 RID: 3081
			public InputDeviceMatcher.MatcherJson device;
		}

		// Token: 0x0200022F RID: 559
		[Serializable]
		private struct LayoutJson
		{
			// Token: 0x060015E3 RID: 5603 RVA: 0x000649A4 File Offset: 0x00062BA4
			public InputControlLayout ToLayout()
			{
				Type type = null;
				if (!string.IsNullOrEmpty(this.type))
				{
					type = Type.GetType(this.type, false);
					if (type == null)
					{
						Debug.Log(string.Concat(new string[] { "Cannot find type '", this.type, "' used by layout '", this.name, "'; falling back to using InputDevice" }));
						type = typeof(InputDevice);
					}
					else if (!typeof(InputControl).IsAssignableFrom(type))
					{
						throw new InvalidOperationException(string.Concat(new string[] { "'", this.type, "' used by layout '", this.name, "' is not an InputControl" }));
					}
				}
				else if (string.IsNullOrEmpty(this.extend))
				{
					type = typeof(InputDevice);
				}
				InputControlLayout inputControlLayout = new InputControlLayout(this.name, type);
				inputControlLayout.m_DisplayName = this.displayName;
				inputControlLayout.m_Description = this.description;
				inputControlLayout.isGenericTypeOfDevice = this.isGenericTypeOfDevice;
				inputControlLayout.hideInUI = this.hideInUI;
				inputControlLayout.m_Variants = new InternedString(this.variant);
				inputControlLayout.m_CommonUsages = ArrayHelpers.Select<string, InternedString>(this.commonUsages, (string x) => new InternedString(x));
				InputControlLayout inputControlLayout2 = inputControlLayout;
				if (!string.IsNullOrEmpty(this.format))
				{
					inputControlLayout2.m_StateFormat = new FourCC(this.format);
				}
				if (!string.IsNullOrEmpty(this.extend))
				{
					inputControlLayout2.m_BaseLayouts.Append(new InternedString(this.extend));
				}
				if (this.extendMultiple != null)
				{
					foreach (string text in this.extendMultiple)
					{
						inputControlLayout2.m_BaseLayouts.Append(new InternedString(text));
					}
				}
				if (!string.IsNullOrEmpty(this.beforeRender))
				{
					string text2 = this.beforeRender.ToLowerInvariant();
					if (text2 == "ignore")
					{
						inputControlLayout2.m_UpdateBeforeRender = new bool?(false);
					}
					else
					{
						if (!(text2 == "update"))
						{
							throw new InvalidOperationException("Invalid beforeRender setting '" + this.beforeRender + "' (should be 'ignore' or 'update')");
						}
						inputControlLayout2.m_UpdateBeforeRender = new bool?(true);
					}
				}
				if (!string.IsNullOrEmpty(this.runInBackground))
				{
					string text3 = this.runInBackground.ToLowerInvariant();
					if (text3 == "enabled")
					{
						inputControlLayout2.canRunInBackground = new bool?(true);
					}
					else
					{
						if (!(text3 == "disabled"))
						{
							throw new InvalidOperationException("Invalid runInBackground setting '" + this.beforeRender + "' (should be 'enabled' or 'disabled')");
						}
						inputControlLayout2.canRunInBackground = new bool?(false);
					}
				}
				if (this.controls != null)
				{
					List<InputControlLayout.ControlItem> list = new List<InputControlLayout.ControlItem>();
					foreach (InputControlLayout.ControlItemJson controlItemJson in this.controls)
					{
						if (string.IsNullOrEmpty(controlItemJson.name))
						{
							throw new InvalidOperationException("Control with no name in layout '" + this.name);
						}
						InputControlLayout.ControlItem controlItem = controlItemJson.ToLayout();
						list.Add(controlItem);
					}
					inputControlLayout2.m_Controls = list.ToArray();
				}
				return inputControlLayout2;
			}

			// Token: 0x060015E4 RID: 5604 RVA: 0x00064CC0 File Offset: 0x00062EC0
			public static InputControlLayout.LayoutJson FromLayout(InputControlLayout layout)
			{
				InputControlLayout.LayoutJson layoutJson = default(InputControlLayout.LayoutJson);
				layoutJson.name = layout.m_Name;
				Type type = layout.type;
				layoutJson.type = ((type != null) ? type.AssemblyQualifiedName : null);
				layoutJson.variant = layout.m_Variants;
				layoutJson.displayName = layout.m_DisplayName;
				layoutJson.description = layout.m_Description;
				layoutJson.isGenericTypeOfDevice = layout.isGenericTypeOfDevice;
				layoutJson.hideInUI = layout.hideInUI;
				layoutJson.extend = ((layout.m_BaseLayouts.length == 1) ? layout.m_BaseLayouts[0].ToString() : null);
				string[] array;
				if (layout.m_BaseLayouts.length <= 1)
				{
					array = null;
				}
				else
				{
					array = layout.m_BaseLayouts.ToArray<string>((InternedString x) => x.ToString());
				}
				layoutJson.extendMultiple = array;
				layoutJson.format = layout.stateFormat.ToString();
				layoutJson.commonUsages = ArrayHelpers.Select<InternedString, string>(layout.m_CommonUsages, (InternedString x) => x.ToString());
				layoutJson.controls = InputControlLayout.ControlItemJson.FromControlItems(layout.m_Controls);
				layoutJson.beforeRender = ((layout.m_UpdateBeforeRender != null) ? (layout.m_UpdateBeforeRender.Value ? "Update" : "Ignore") : null);
				return layoutJson;
			}

			// Token: 0x04000C0A RID: 3082
			public string name;

			// Token: 0x04000C0B RID: 3083
			public string extend;

			// Token: 0x04000C0C RID: 3084
			public string[] extendMultiple;

			// Token: 0x04000C0D RID: 3085
			public string format;

			// Token: 0x04000C0E RID: 3086
			public string beforeRender;

			// Token: 0x04000C0F RID: 3087
			public string runInBackground;

			// Token: 0x04000C10 RID: 3088
			public string[] commonUsages;

			// Token: 0x04000C11 RID: 3089
			public string displayName;

			// Token: 0x04000C12 RID: 3090
			public string description;

			// Token: 0x04000C13 RID: 3091
			public string type;

			// Token: 0x04000C14 RID: 3092
			public string variant;

			// Token: 0x04000C15 RID: 3093
			public bool isGenericTypeOfDevice;

			// Token: 0x04000C16 RID: 3094
			public bool hideInUI;

			// Token: 0x04000C17 RID: 3095
			public InputControlLayout.ControlItemJson[] controls;
		}

		// Token: 0x02000230 RID: 560
		[Serializable]
		private class ControlItemJson
		{
			// Token: 0x060015E5 RID: 5605 RVA: 0x00064E49 File Offset: 0x00063049
			public ControlItemJson()
			{
				this.offset = uint.MaxValue;
				this.bit = uint.MaxValue;
			}

			// Token: 0x060015E6 RID: 5606 RVA: 0x00064E60 File Offset: 0x00063060
			public InputControlLayout.ControlItem ToLayout()
			{
				InputControlLayout.ControlItem controlItem = new InputControlLayout.ControlItem
				{
					name = new InternedString(this.name),
					layout = new InternedString(this.layout),
					variants = new InternedString(this.variants),
					displayName = this.displayName,
					shortDisplayName = this.shortDisplayName,
					offset = this.offset,
					useStateFrom = this.useStateFrom,
					bit = this.bit,
					sizeInBits = this.sizeInBits,
					isModifyingExistingControl = (this.name.IndexOf('/') != -1),
					isNoisy = this.noisy,
					dontReset = this.dontReset,
					isSynthetic = this.synthetic,
					isFirstDefinedInThisLayout = true,
					arraySize = this.arraySize
				};
				if (!string.IsNullOrEmpty(this.format))
				{
					controlItem.format = new FourCC(this.format);
				}
				if (!string.IsNullOrEmpty(this.usage) || this.usages != null)
				{
					List<string> list = new List<string>();
					if (!string.IsNullOrEmpty(this.usage))
					{
						list.Add(this.usage);
					}
					if (this.usages != null)
					{
						list.AddRange(this.usages);
					}
					controlItem.usages = new ReadOnlyArray<InternedString>(list.Select<string, InternedString>((string x) => new InternedString(x)).ToArray<InternedString>());
				}
				if (!string.IsNullOrEmpty(this.alias) || this.aliases != null)
				{
					List<string> list2 = new List<string>();
					if (!string.IsNullOrEmpty(this.alias))
					{
						list2.Add(this.alias);
					}
					if (this.aliases != null)
					{
						list2.AddRange(this.aliases);
					}
					controlItem.aliases = new ReadOnlyArray<InternedString>(list2.Select<string, InternedString>((string x) => new InternedString(x)).ToArray<InternedString>());
				}
				if (!string.IsNullOrEmpty(this.parameters))
				{
					controlItem.parameters = new ReadOnlyArray<NamedValue>(NamedValue.ParseMultiple(this.parameters));
				}
				if (!string.IsNullOrEmpty(this.processors))
				{
					controlItem.processors = new ReadOnlyArray<NameAndParameters>(NameAndParameters.ParseMultiple(this.processors).ToArray<NameAndParameters>());
				}
				if (this.defaultState != null)
				{
					controlItem.defaultState = PrimitiveValue.FromObject(this.defaultState);
				}
				if (this.minValue != null)
				{
					controlItem.minValue = PrimitiveValue.FromObject(this.minValue);
				}
				if (this.maxValue != null)
				{
					controlItem.maxValue = PrimitiveValue.FromObject(this.maxValue);
				}
				return controlItem;
			}

			// Token: 0x060015E7 RID: 5607 RVA: 0x00065108 File Offset: 0x00063308
			public static InputControlLayout.ControlItemJson[] FromControlItems(InputControlLayout.ControlItem[] items)
			{
				if (items == null)
				{
					return null;
				}
				int num = items.Length;
				InputControlLayout.ControlItemJson[] array = new InputControlLayout.ControlItemJson[num];
				for (int i = 0; i < num; i++)
				{
					InputControlLayout.ControlItem controlItem = items[i];
					InputControlLayout.ControlItemJson[] array2 = array;
					int num2 = i;
					InputControlLayout.ControlItemJson controlItemJson = new InputControlLayout.ControlItemJson();
					controlItemJson.name = controlItem.name;
					controlItemJson.layout = controlItem.layout;
					controlItemJson.variants = controlItem.variants;
					controlItemJson.displayName = controlItem.displayName;
					controlItemJson.shortDisplayName = controlItem.shortDisplayName;
					controlItemJson.bit = controlItem.bit;
					controlItemJson.offset = controlItem.offset;
					controlItemJson.sizeInBits = controlItem.sizeInBits;
					controlItemJson.format = controlItem.format.ToString();
					controlItemJson.parameters = string.Join(",", controlItem.parameters.Select<NamedValue, string>((NamedValue x) => x.ToString()).ToArray<string>());
					controlItemJson.processors = string.Join(",", controlItem.processors.Select<NameAndParameters, string>((NameAndParameters x) => x.ToString()).ToArray<string>());
					controlItemJson.usages = controlItem.usages.Select<InternedString, string>((InternedString x) => x.ToString()).ToArray<string>();
					controlItemJson.aliases = controlItem.aliases.Select<InternedString, string>((InternedString x) => x.ToString()).ToArray<string>();
					controlItemJson.noisy = controlItem.isNoisy;
					controlItemJson.dontReset = controlItem.dontReset;
					controlItemJson.synthetic = controlItem.isSynthetic;
					controlItemJson.arraySize = controlItem.arraySize;
					controlItemJson.defaultState = controlItem.defaultState.ToString();
					controlItemJson.minValue = controlItem.minValue.ToString();
					controlItemJson.maxValue = controlItem.maxValue.ToString();
					array2[num2] = controlItemJson;
				}
				return array;
			}

			// Token: 0x04000C18 RID: 3096
			public string name;

			// Token: 0x04000C19 RID: 3097
			public string layout;

			// Token: 0x04000C1A RID: 3098
			public string variants;

			// Token: 0x04000C1B RID: 3099
			public string usage;

			// Token: 0x04000C1C RID: 3100
			public string alias;

			// Token: 0x04000C1D RID: 3101
			public string useStateFrom;

			// Token: 0x04000C1E RID: 3102
			public uint offset;

			// Token: 0x04000C1F RID: 3103
			public uint bit;

			// Token: 0x04000C20 RID: 3104
			public uint sizeInBits;

			// Token: 0x04000C21 RID: 3105
			public string format;

			// Token: 0x04000C22 RID: 3106
			public int arraySize;

			// Token: 0x04000C23 RID: 3107
			public string[] usages;

			// Token: 0x04000C24 RID: 3108
			public string[] aliases;

			// Token: 0x04000C25 RID: 3109
			public string parameters;

			// Token: 0x04000C26 RID: 3110
			public string processors;

			// Token: 0x04000C27 RID: 3111
			public string displayName;

			// Token: 0x04000C28 RID: 3112
			public string shortDisplayName;

			// Token: 0x04000C29 RID: 3113
			public bool noisy;

			// Token: 0x04000C2A RID: 3114
			public bool dontReset;

			// Token: 0x04000C2B RID: 3115
			public bool synthetic;

			// Token: 0x04000C2C RID: 3116
			public string defaultState;

			// Token: 0x04000C2D RID: 3117
			public string minValue;

			// Token: 0x04000C2E RID: 3118
			public string maxValue;
		}

		// Token: 0x02000231 RID: 561
		internal struct Collection
		{
			// Token: 0x060015E8 RID: 5608 RVA: 0x00065364 File Offset: 0x00063564
			public void Allocate()
			{
				this.layoutTypes = new Dictionary<InternedString, Type>();
				this.layoutStrings = new Dictionary<InternedString, string>();
				this.layoutBuilders = new Dictionary<InternedString, Func<InputControlLayout>>();
				this.baseLayoutTable = new Dictionary<InternedString, InternedString>();
				this.layoutOverrides = new Dictionary<InternedString, InternedString[]>();
				this.layoutOverrideNames = new HashSet<InternedString>();
				this.layoutMatchers = new List<InputControlLayout.Collection.LayoutMatcher>();
				this.precompiledLayouts = new Dictionary<InternedString, InputControlLayout.Collection.PrecompiledLayout>();
			}

			// Token: 0x060015E9 RID: 5609 RVA: 0x000653CC File Offset: 0x000635CC
			public InternedString TryFindLayoutForType(Type layoutType)
			{
				foreach (KeyValuePair<InternedString, Type> keyValuePair in this.layoutTypes)
				{
					if (keyValuePair.Value == layoutType)
					{
						return keyValuePair.Key;
					}
				}
				return default(InternedString);
			}

			// Token: 0x060015EA RID: 5610 RVA: 0x0006543C File Offset: 0x0006363C
			public InternedString TryFindMatchingLayout(InputDeviceDescription deviceDescription)
			{
				float num = 0f;
				InternedString internedString = default(InternedString);
				int count = this.layoutMatchers.Count;
				for (int i = 0; i < count; i++)
				{
					InputDeviceMatcher deviceMatcher = this.layoutMatchers[i].deviceMatcher;
					float num2 = deviceMatcher.MatchPercentage(deviceDescription);
					if (num2 > 0f && !this.layoutBuilders.ContainsKey(this.layoutMatchers[i].layoutName))
					{
						num2 += 1f;
					}
					if (num2 > num)
					{
						num = num2;
						internedString = this.layoutMatchers[i].layoutName;
					}
				}
				return internedString;
			}

			// Token: 0x060015EB RID: 5611 RVA: 0x000654D8 File Offset: 0x000636D8
			public bool HasLayout(InternedString name)
			{
				return this.layoutTypes.ContainsKey(name) || this.layoutStrings.ContainsKey(name) || this.layoutBuilders.ContainsKey(name);
			}

			// Token: 0x060015EC RID: 5612 RVA: 0x00065504 File Offset: 0x00063704
			private InputControlLayout TryLoadLayoutInternal(InternedString name)
			{
				string text;
				if (this.layoutStrings.TryGetValue(name, out text))
				{
					return InputControlLayout.FromJson(text);
				}
				Type type;
				if (this.layoutTypes.TryGetValue(name, out type))
				{
					return InputControlLayout.FromType(name, type);
				}
				Func<InputControlLayout> func;
				if (!this.layoutBuilders.TryGetValue(name, out func))
				{
					return null;
				}
				InputControlLayout inputControlLayout = func();
				if (inputControlLayout == null)
				{
					throw new InvalidOperationException(string.Format("Layout builder '{0}' returned null when invoked", name));
				}
				return inputControlLayout;
			}

			// Token: 0x060015ED RID: 5613 RVA: 0x00065578 File Offset: 0x00063778
			public InputControlLayout TryLoadLayout(InternedString name, Dictionary<InternedString, InputControlLayout> table = null)
			{
				InputControlLayout inputControlLayout;
				if (table != null && table.TryGetValue(name, out inputControlLayout))
				{
					return inputControlLayout;
				}
				inputControlLayout = this.TryLoadLayoutInternal(name);
				if (inputControlLayout != null)
				{
					inputControlLayout.m_Name = name;
					if (this.layoutOverrideNames.Contains(name))
					{
						inputControlLayout.isOverride = true;
					}
					InternedString internedString = default(InternedString);
					if (!inputControlLayout.isOverride && this.baseLayoutTable.TryGetValue(name, out internedString))
					{
						InputControlLayout inputControlLayout2 = this.TryLoadLayout(internedString, table);
						if (inputControlLayout2 == null)
						{
							throw new InputControlLayout.LayoutNotFoundException(string.Format("Cannot find base layout '{0}' of layout '{1}'", internedString, name));
						}
						inputControlLayout.MergeLayout(inputControlLayout2);
						if (inputControlLayout.m_BaseLayouts.length == 0)
						{
							inputControlLayout.m_BaseLayouts.Append(internedString);
						}
					}
					InternedString[] array;
					if (this.layoutOverrides.TryGetValue(name, out array))
					{
						foreach (InternedString internedString2 in array)
						{
							InputControlLayout inputControlLayout3 = this.TryLoadLayout(internedString2, null);
							inputControlLayout3.MergeLayout(inputControlLayout);
							inputControlLayout3.m_BaseLayouts.Clear();
							inputControlLayout3.isOverride = false;
							inputControlLayout3.isGenericTypeOfDevice = inputControlLayout.isGenericTypeOfDevice;
							inputControlLayout3.m_Name = inputControlLayout.name;
							inputControlLayout3.m_BaseLayouts = inputControlLayout.m_BaseLayouts;
							inputControlLayout = inputControlLayout3;
							inputControlLayout.m_AppliedOverrides.Append(internedString2);
						}
					}
					if (table != null)
					{
						table[name] = inputControlLayout;
					}
				}
				return inputControlLayout;
			}

			// Token: 0x060015EE RID: 5614 RVA: 0x000656B8 File Offset: 0x000638B8
			public InternedString GetBaseLayoutName(InternedString layoutName)
			{
				InternedString internedString;
				if (this.baseLayoutTable.TryGetValue(layoutName, out internedString))
				{
					return internedString;
				}
				return default(InternedString);
			}

			// Token: 0x060015EF RID: 5615 RVA: 0x000656E0 File Offset: 0x000638E0
			public InternedString GetRootLayoutName(InternedString layoutName)
			{
				InternedString internedString;
				while (this.baseLayoutTable.TryGetValue(layoutName, out internedString))
				{
					layoutName = internedString;
				}
				return layoutName;
			}

			// Token: 0x060015F0 RID: 5616 RVA: 0x00065704 File Offset: 0x00063904
			public bool ComputeDistanceInInheritanceHierarchy(InternedString firstLayout, InternedString secondLayout, out int distance)
			{
				distance = 0;
				int num = 0;
				InternedString internedString = secondLayout;
				while (!internedString.IsEmpty() && internedString != firstLayout)
				{
					internedString = this.GetBaseLayoutName(internedString);
					num++;
				}
				if (internedString == firstLayout)
				{
					distance = num;
					return true;
				}
				int num2 = 0;
				internedString = firstLayout;
				while (!internedString.IsEmpty() && internedString != secondLayout)
				{
					internedString = this.GetBaseLayoutName(internedString);
					num2++;
				}
				if (internedString == secondLayout)
				{
					distance = num2;
					return true;
				}
				return false;
			}

			// Token: 0x060015F1 RID: 5617 RVA: 0x0006577C File Offset: 0x0006397C
			public InternedString FindLayoutThatIntroducesControl(InputControl control, InputControlLayout.Cache cache)
			{
				InputControl inputControl = control;
				while (inputControl.parent != control.device)
				{
					inputControl = inputControl.parent;
				}
				InternedString internedString = control.device.m_Layout;
				InternedString internedString2 = internedString;
				while (this.baseLayoutTable.TryGetValue(internedString2, out internedString2))
				{
					if (cache.FindOrLoadLayout(internedString2, true).FindControl(inputControl.m_Name) != null)
					{
						internedString = internedString2;
					}
				}
				return internedString;
			}

			// Token: 0x060015F2 RID: 5618 RVA: 0x000657E8 File Offset: 0x000639E8
			public Type GetControlTypeForLayout(InternedString layoutName)
			{
				while (this.layoutStrings.ContainsKey(layoutName))
				{
					InternedString internedString;
					if (!this.baseLayoutTable.TryGetValue(layoutName, out internedString))
					{
						return typeof(InputDevice);
					}
					layoutName = internedString;
				}
				Type type;
				this.layoutTypes.TryGetValue(layoutName, out type);
				return type;
			}

			// Token: 0x060015F3 RID: 5619 RVA: 0x00065838 File Offset: 0x00063A38
			public bool ValueTypeIsAssignableFrom(InternedString layoutName, Type valueType)
			{
				Type controlTypeForLayout = this.GetControlTypeForLayout(layoutName);
				if (controlTypeForLayout == null)
				{
					return false;
				}
				Type genericTypeArgumentFromHierarchy = TypeHelpers.GetGenericTypeArgumentFromHierarchy(controlTypeForLayout, typeof(InputControl<>), 0);
				return !(genericTypeArgumentFromHierarchy == null) && valueType.IsAssignableFrom(genericTypeArgumentFromHierarchy);
			}

			// Token: 0x060015F4 RID: 5620 RVA: 0x0006587C File Offset: 0x00063A7C
			public bool IsGeneratedLayout(InternedString layout)
			{
				return this.layoutBuilders.ContainsKey(layout);
			}

			// Token: 0x060015F5 RID: 5621 RVA: 0x0006588A File Offset: 0x00063A8A
			public IEnumerable<InternedString> GetBaseLayouts(InternedString layout, bool includeSelf = true)
			{
				if (includeSelf)
				{
					yield return layout;
				}
				while (this.baseLayoutTable.TryGetValue(layout, out layout))
				{
					yield return layout;
				}
				yield break;
			}

			// Token: 0x060015F6 RID: 5622 RVA: 0x000658B0 File Offset: 0x00063AB0
			public bool IsBasedOn(InternedString parentLayout, InternedString childLayout)
			{
				InternedString internedString = childLayout;
				while (this.baseLayoutTable.TryGetValue(internedString, out internedString))
				{
					if (internedString == parentLayout)
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x060015F7 RID: 5623 RVA: 0x000658E0 File Offset: 0x00063AE0
			public void AddMatcher(InternedString layout, InputDeviceMatcher matcher)
			{
				int count = this.layoutMatchers.Count;
				for (int i = 0; i < count; i++)
				{
					if (this.layoutMatchers[i].deviceMatcher == matcher)
					{
						return;
					}
				}
				this.layoutMatchers.Add(new InputControlLayout.Collection.LayoutMatcher
				{
					layoutName = layout,
					deviceMatcher = matcher
				});
			}

			// Token: 0x04000C2F RID: 3119
			public const float kBaseScoreForNonGeneratedLayouts = 1f;

			// Token: 0x04000C30 RID: 3120
			public Dictionary<InternedString, Type> layoutTypes;

			// Token: 0x04000C31 RID: 3121
			public Dictionary<InternedString, string> layoutStrings;

			// Token: 0x04000C32 RID: 3122
			public Dictionary<InternedString, Func<InputControlLayout>> layoutBuilders;

			// Token: 0x04000C33 RID: 3123
			public Dictionary<InternedString, InternedString> baseLayoutTable;

			// Token: 0x04000C34 RID: 3124
			public Dictionary<InternedString, InternedString[]> layoutOverrides;

			// Token: 0x04000C35 RID: 3125
			public HashSet<InternedString> layoutOverrideNames;

			// Token: 0x04000C36 RID: 3126
			public Dictionary<InternedString, InputControlLayout.Collection.PrecompiledLayout> precompiledLayouts;

			// Token: 0x04000C37 RID: 3127
			public List<InputControlLayout.Collection.LayoutMatcher> layoutMatchers;

			// Token: 0x02000288 RID: 648
			public struct LayoutMatcher
			{
				// Token: 0x04000D3D RID: 3389
				public InternedString layoutName;

				// Token: 0x04000D3E RID: 3390
				public InputDeviceMatcher deviceMatcher;
			}

			// Token: 0x02000289 RID: 649
			public struct PrecompiledLayout
			{
				// Token: 0x04000D3F RID: 3391
				public Func<InputDevice> factoryMethod;

				// Token: 0x04000D40 RID: 3392
				public string metadata;
			}
		}

		// Token: 0x02000232 RID: 562
		public class LayoutNotFoundException : Exception
		{
			// Token: 0x170005F9 RID: 1529
			// (get) Token: 0x060015F8 RID: 5624 RVA: 0x00065943 File Offset: 0x00063B43
			public string layout { get; }

			// Token: 0x060015F9 RID: 5625 RVA: 0x0006594B File Offset: 0x00063B4B
			public LayoutNotFoundException()
			{
			}

			// Token: 0x060015FA RID: 5626 RVA: 0x00065953 File Offset: 0x00063B53
			public LayoutNotFoundException(string name, string message)
				: base(message)
			{
				this.layout = name;
			}

			// Token: 0x060015FB RID: 5627 RVA: 0x00065963 File Offset: 0x00063B63
			public LayoutNotFoundException(string name)
				: base("Cannot find control layout '" + name + "'")
			{
				this.layout = name;
			}

			// Token: 0x060015FC RID: 5628 RVA: 0x00065982 File Offset: 0x00063B82
			public LayoutNotFoundException(string message, Exception innerException)
				: base(message, innerException)
			{
			}

			// Token: 0x060015FD RID: 5629 RVA: 0x0006598C File Offset: 0x00063B8C
			protected LayoutNotFoundException(SerializationInfo info, StreamingContext context)
				: base(info, context)
			{
			}
		}

		// Token: 0x02000233 RID: 563
		internal struct Cache
		{
			// Token: 0x060015FE RID: 5630 RVA: 0x00065996 File Offset: 0x00063B96
			public void Clear()
			{
				this.table = null;
			}

			// Token: 0x060015FF RID: 5631 RVA: 0x000659A0 File Offset: 0x00063BA0
			public InputControlLayout FindOrLoadLayout(string name, bool throwIfNotFound = true)
			{
				InternedString internedString = new InternedString(name);
				if (this.table == null)
				{
					this.table = new Dictionary<InternedString, InputControlLayout>();
				}
				InputControlLayout inputControlLayout = InputControlLayout.s_Layouts.TryLoadLayout(internedString, this.table);
				if (inputControlLayout != null)
				{
					return inputControlLayout;
				}
				if (throwIfNotFound)
				{
					throw new InputControlLayout.LayoutNotFoundException(name);
				}
				return null;
			}

			// Token: 0x04000C39 RID: 3129
			public Dictionary<InternedString, InputControlLayout> table;
		}

		// Token: 0x02000234 RID: 564
		internal struct CacheRefInstance : IDisposable
		{
			// Token: 0x06001600 RID: 5632 RVA: 0x000659EA File Offset: 0x00063BEA
			public void Dispose()
			{
				if (!this.valid)
				{
					return;
				}
				InputControlLayout.s_CacheInstanceRef--;
				if (InputControlLayout.s_CacheInstanceRef <= 0)
				{
					InputControlLayout.s_CacheInstance = default(InputControlLayout.Cache);
					InputControlLayout.s_CacheInstanceRef = 0;
				}
				this.valid = false;
			}

			// Token: 0x04000C3A RID: 3130
			public bool valid;
		}
	}
}
