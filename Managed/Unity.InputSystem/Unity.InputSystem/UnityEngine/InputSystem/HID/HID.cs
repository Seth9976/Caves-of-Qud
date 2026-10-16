using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Profiling;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.Pool;

namespace UnityEngine.InputSystem.HID
{
	// Token: 0x0200009A RID: 154
	public class HID : InputDevice
	{
		// Token: 0x17000330 RID: 816
		// (get) Token: 0x06000C2D RID: 3117 RVA: 0x00041300 File Offset: 0x0003F500
		public static FourCC QueryHIDReportDescriptorDeviceCommandType
		{
			get
			{
				return new FourCC('H', 'I', 'D', 'D');
			}
		}

		// Token: 0x17000331 RID: 817
		// (get) Token: 0x06000C2E RID: 3118 RVA: 0x0004130F File Offset: 0x0003F50F
		public static FourCC QueryHIDReportDescriptorSizeDeviceCommandType
		{
			get
			{
				return new FourCC('H', 'I', 'D', 'S');
			}
		}

		// Token: 0x17000332 RID: 818
		// (get) Token: 0x06000C2F RID: 3119 RVA: 0x0004131E File Offset: 0x0003F51E
		public static FourCC QueryHIDParsedReportDescriptorDeviceCommandType
		{
			get
			{
				return new FourCC('H', 'I', 'D', 'P');
			}
		}

		// Token: 0x17000333 RID: 819
		// (get) Token: 0x06000C30 RID: 3120 RVA: 0x00041330 File Offset: 0x0003F530
		public HID.HIDDeviceDescriptor hidDescriptor
		{
			get
			{
				if (!this.m_HaveParsedHIDDescriptor)
				{
					if (!string.IsNullOrEmpty(base.description.capabilities))
					{
						this.m_HIDDescriptor = JsonUtility.FromJson<HID.HIDDeviceDescriptor>(base.description.capabilities);
					}
					this.m_HaveParsedHIDDescriptor = true;
				}
				return this.m_HIDDescriptor;
			}
		}

		// Token: 0x06000C31 RID: 3121 RVA: 0x00041380 File Offset: 0x0003F580
		internal static string OnFindLayoutForDevice(ref InputDeviceDescription description, string matchedLayout, InputDeviceExecuteCommandDelegate executeDeviceCommand)
		{
			if (!string.IsNullOrEmpty(matchedLayout))
			{
				return null;
			}
			if (description.interfaceName != "HID")
			{
				return null;
			}
			HID.HIDDeviceDescriptor hiddeviceDescriptor = HID.ReadHIDDeviceDescriptor(ref description, executeDeviceCommand);
			if (!HIDSupport.supportedHIDUsages.Contains(new HIDSupport.HIDPageUsage(hiddeviceDescriptor.usagePage, hiddeviceDescriptor.usage)))
			{
				return null;
			}
			bool flag = false;
			if (hiddeviceDescriptor.elements != null)
			{
				foreach (HID.HIDElementDescriptor hidelementDescriptor in hiddeviceDescriptor.elements)
				{
					if (hidelementDescriptor.IsUsableElement())
					{
						flag = true;
						break;
					}
				}
			}
			if (!flag)
			{
				return null;
			}
			Type type = typeof(HID);
			string text = "HID";
			if (hiddeviceDescriptor.usagePage == HID.UsagePage.GenericDesktop && (hiddeviceDescriptor.usage == 4 || hiddeviceDescriptor.usage == 5))
			{
				text = "Joystick";
				type = typeof(Joystick);
			}
			string text2 = "";
			if (text != "Joystick")
			{
				text2 = ((hiddeviceDescriptor.usagePage == HID.UsagePage.GenericDesktop) ? string.Format(" {0}", (HID.GenericDesktop)hiddeviceDescriptor.usage) : string.Format(" {0}-{1}", hiddeviceDescriptor.usagePage, hiddeviceDescriptor.usage));
			}
			InputDeviceMatcher inputDeviceMatcher = InputDeviceMatcher.FromDeviceDescription(description);
			string text3;
			if (!string.IsNullOrEmpty(description.product) && !string.IsNullOrEmpty(description.manufacturer))
			{
				text3 = string.Concat(new string[] { "HID::", description.manufacturer, " ", description.product, text2 });
			}
			else if (!string.IsNullOrEmpty(description.product))
			{
				text3 = "HID::" + description.product + text2;
			}
			else
			{
				if (hiddeviceDescriptor.vendorId == 0)
				{
					return null;
				}
				text3 = string.Format("{0}::{1:X}-{2:X}{3}", new object[] { "HID", hiddeviceDescriptor.vendorId, hiddeviceDescriptor.productId, text2 });
				inputDeviceMatcher = inputDeviceMatcher.WithCapability<int>("productId", hiddeviceDescriptor.productId).WithCapability<int>("vendorId", hiddeviceDescriptor.vendorId);
			}
			inputDeviceMatcher = inputDeviceMatcher.WithCapability<int>("usage", hiddeviceDescriptor.usage).WithCapability<HID.UsagePage>("usagePage", hiddeviceDescriptor.usagePage);
			HID.HIDLayoutBuilder layout = new HID.HIDLayoutBuilder
			{
				displayName = description.product,
				hidDescriptor = hiddeviceDescriptor,
				parentLayout = text,
				deviceType = (type ?? typeof(HID))
			};
			InputSystem.RegisterLayoutBuilder(() => layout.Build(), text3, text, new InputDeviceMatcher?(inputDeviceMatcher));
			return text3;
		}

		// Token: 0x06000C32 RID: 3122 RVA: 0x00041628 File Offset: 0x0003F828
		internal unsafe static HID.HIDDeviceDescriptor ReadHIDDeviceDescriptor(ref InputDeviceDescription deviceDescription, InputDeviceExecuteCommandDelegate executeCommandDelegate)
		{
			if (deviceDescription.interfaceName != "HID")
			{
				throw new ArgumentException(string.Format("Device '{0}' is not a HID", deviceDescription));
			}
			bool flag = true;
			HID.HIDDeviceDescriptor hiddeviceDescriptor = default(HID.HIDDeviceDescriptor);
			if (!string.IsNullOrEmpty(deviceDescription.capabilities))
			{
				try
				{
					hiddeviceDescriptor = HID.HIDDeviceDescriptor.FromJson(deviceDescription.capabilities);
					if (hiddeviceDescriptor.elements != null && hiddeviceDescriptor.elements.Length != 0)
					{
						flag = false;
					}
				}
				catch (Exception ex)
				{
					Debug.LogError(string.Format("Could not parse HID descriptor of device '{0}'", deviceDescription));
					Debug.LogException(ex);
				}
			}
			if (flag)
			{
				InputDeviceCommand inputDeviceCommand = new InputDeviceCommand(HID.QueryHIDReportDescriptorSizeDeviceCommandType, 8);
				long num = executeCommandDelegate(ref inputDeviceCommand);
				if (num > 0L)
				{
					using (NativeArray<byte> nativeArray = InputDeviceCommand.AllocateNative(HID.QueryHIDReportDescriptorDeviceCommandType, (int)num))
					{
						InputDeviceCommand* unsafePtr = (InputDeviceCommand*)nativeArray.GetUnsafePtr<byte>();
						if (executeCommandDelegate(ref *unsafePtr) != num)
						{
							HID.HIDDeviceDescriptor hiddeviceDescriptor2 = default(HID.HIDDeviceDescriptor);
							return hiddeviceDescriptor2;
						}
						if (!HIDParser.ParseReportDescriptor((byte*)unsafePtr->payloadPtr, (int)num, ref hiddeviceDescriptor))
						{
							return default(HID.HIDDeviceDescriptor);
						}
					}
					deviceDescription.capabilities = hiddeviceDescriptor.ToJson();
					return hiddeviceDescriptor;
				}
				using (NativeArray<byte> nativeArray2 = InputDeviceCommand.AllocateNative(HID.QueryHIDParsedReportDescriptorDeviceCommandType, 2097152))
				{
					InputDeviceCommand* unsafePtr2 = (InputDeviceCommand*)nativeArray2.GetUnsafePtr<byte>();
					long num2 = executeCommandDelegate(ref *unsafePtr2);
					if (num2 < 0L)
					{
						return default(HID.HIDDeviceDescriptor);
					}
					byte[] array = new byte[num2];
					try
					{
						byte[] array2;
						byte* ptr;
						if ((array2 = array) == null || array2.Length == 0)
						{
							ptr = null;
						}
						else
						{
							ptr = &array2[0];
						}
						UnsafeUtility.MemCpy((void*)ptr, unsafePtr2->payloadPtr, num2);
					}
					finally
					{
						byte[] array2 = null;
					}
					string @string = Encoding.UTF8.GetString(array, 0, (int)num2);
					try
					{
						hiddeviceDescriptor = HID.HIDDeviceDescriptor.FromJson(@string);
					}
					catch (Exception ex2)
					{
						Debug.LogError(string.Format("Could not parse HID descriptor of device '{0}'", deviceDescription));
						Debug.LogException(ex2);
						return default(HID.HIDDeviceDescriptor);
					}
					deviceDescription.capabilities = @string;
				}
				return hiddeviceDescriptor;
			}
			return hiddeviceDescriptor;
		}

		// Token: 0x06000C33 RID: 3123 RVA: 0x00041874 File Offset: 0x0003FA74
		public static string UsagePageToString(HID.UsagePage usagePage)
		{
			if (usagePage < HID.UsagePage.VendorDefined)
			{
				return usagePage.ToString();
			}
			return "Vendor-Defined";
		}

		// Token: 0x06000C34 RID: 3124 RVA: 0x00041894 File Offset: 0x0003FA94
		public static string UsageToString(HID.UsagePage usagePage, int usage)
		{
			if (usagePage == HID.UsagePage.GenericDesktop)
			{
				HID.GenericDesktop genericDesktop = (HID.GenericDesktop)usage;
				return genericDesktop.ToString();
			}
			if (usagePage != HID.UsagePage.Simulation)
			{
				return null;
			}
			HID.Simulation simulation = (HID.Simulation)usage;
			return simulation.ToString();
		}

		// Token: 0x0400047D RID: 1149
		internal const string kHIDInterface = "HID";

		// Token: 0x0400047E RID: 1150
		internal const string kHIDNamespace = "HID";

		// Token: 0x0400047F RID: 1151
		private bool m_HaveParsedHIDDescriptor;

		// Token: 0x04000480 RID: 1152
		private HID.HIDDeviceDescriptor m_HIDDescriptor;

		// Token: 0x04000481 RID: 1153
		private static readonly ProfilerMarker k_HIDParseDescriptorFallback = new ProfilerMarker("HIDParseDescriptorFallback");

		// Token: 0x020001E6 RID: 486
		[Serializable]
		private class HIDLayoutBuilder
		{
			// Token: 0x060014CC RID: 5324 RVA: 0x00060A08 File Offset: 0x0005EC08
			public InputControlLayout Build()
			{
				InputControlLayout.Builder builder = new InputControlLayout.Builder
				{
					displayName = this.displayName,
					type = this.deviceType,
					extendsLayout = this.parentLayout,
					stateFormat = new FourCC('H', 'I', 'D', ' ')
				};
				HID.HIDElementDescriptor hidelementDescriptor = Array.Find<HID.HIDElementDescriptor>(this.hidDescriptor.elements, (HID.HIDElementDescriptor element) => element.usagePage == HID.UsagePage.GenericDesktop && element.usage == 48);
				HID.HIDElementDescriptor hidelementDescriptor2 = Array.Find<HID.HIDElementDescriptor>(this.hidDescriptor.elements, (HID.HIDElementDescriptor element) => element.usagePage == HID.UsagePage.GenericDesktop && element.usage == 49);
				bool flag = hidelementDescriptor.usage == 48 && hidelementDescriptor2.usage == 49;
				if (flag)
				{
					int num;
					int num2;
					int num3;
					if (hidelementDescriptor.reportOffsetInBits <= hidelementDescriptor2.reportOffsetInBits)
					{
						num = hidelementDescriptor.reportOffsetInBits % 8;
						num2 = hidelementDescriptor.reportOffsetInBits / 8;
						num3 = hidelementDescriptor2.reportOffsetInBits + hidelementDescriptor2.reportSizeInBits - hidelementDescriptor.reportOffsetInBits;
					}
					else
					{
						num = hidelementDescriptor2.reportOffsetInBits % 8;
						num2 = hidelementDescriptor2.reportOffsetInBits / 8;
						num3 = hidelementDescriptor.reportOffsetInBits + hidelementDescriptor.reportSizeInBits - hidelementDescriptor2.reportSizeInBits;
					}
					builder.AddControl("stick").WithDisplayName("Stick").WithLayout("Stick")
						.WithBitOffset((uint)num)
						.WithByteOffset((uint)num2)
						.WithSizeInBits((uint)num3)
						.WithUsages(new InternedString[] { CommonUsages.Primary2DMotion });
					string text = hidelementDescriptor.DetermineParameters();
					string text2 = hidelementDescriptor2.DetermineParameters();
					builder.AddControl("stick/x").WithFormat(hidelementDescriptor.DetermineFormat()).WithByteOffset((uint)(hidelementDescriptor.reportOffsetInBits / 8 - num2))
						.WithBitOffset((uint)(hidelementDescriptor.reportOffsetInBits % 8))
						.WithSizeInBits((uint)hidelementDescriptor.reportSizeInBits)
						.WithParameters(text)
						.WithDefaultState(hidelementDescriptor.DetermineDefaultState())
						.WithProcessors(hidelementDescriptor.DetermineProcessors());
					builder.AddControl("stick/y").WithFormat(hidelementDescriptor2.DetermineFormat()).WithByteOffset((uint)(hidelementDescriptor2.reportOffsetInBits / 8 - num2))
						.WithBitOffset((uint)(hidelementDescriptor2.reportOffsetInBits % 8))
						.WithSizeInBits((uint)hidelementDescriptor2.reportSizeInBits)
						.WithParameters(text2)
						.WithDefaultState(hidelementDescriptor2.DetermineDefaultState())
						.WithProcessors(hidelementDescriptor2.DetermineProcessors());
					builder.AddControl("stick/up").WithParameters(StringHelpers.Join<string>(",", new string[] { text2, "clamp=2,clampMin=-1,clampMax=0,invert=true" }));
					builder.AddControl("stick/down").WithParameters(StringHelpers.Join<string>(",", new string[] { text2, "clamp=2,clampMin=0,clampMax=1,invert=false" }));
					builder.AddControl("stick/left").WithParameters(StringHelpers.Join<string>(",", new string[] { text, "clamp=2,clampMin=-1,clampMax=0,invert" }));
					builder.AddControl("stick/right").WithParameters(StringHelpers.Join<string>(",", new string[] { text, "clamp=2,clampMin=0,clampMax=1" }));
				}
				HID.HIDElementDescriptor[] elements = this.hidDescriptor.elements;
				int num4 = elements.Length;
				for (int i = 0; i < num4; i++)
				{
					ref HID.HIDElementDescriptor ptr = ref elements[i];
					if (ptr.reportType == HID.HIDReportType.Input && (!flag || (!ptr.Is(HID.UsagePage.GenericDesktop, 48) && !ptr.Is(HID.UsagePage.GenericDesktop, 49))))
					{
						string text3 = ptr.DetermineLayout();
						if (text3 != null)
						{
							string text4 = ptr.DetermineName();
							text4 = StringHelpers.MakeUniqueName<InputControlLayout.ControlItem>(text4, builder.controls, (InputControlLayout.ControlItem x) => x.name);
							InputControlLayout.Builder.ControlBuilder controlBuilder = builder.AddControl(text4).WithDisplayName(ptr.DetermineDisplayName()).WithLayout(text3)
								.WithByteOffset((uint)(ptr.reportOffsetInBits / 8))
								.WithBitOffset((uint)(ptr.reportOffsetInBits % 8))
								.WithSizeInBits((uint)ptr.reportSizeInBits)
								.WithFormat(ptr.DetermineFormat())
								.WithDefaultState(ptr.DetermineDefaultState())
								.WithProcessors(ptr.DetermineProcessors());
							string text5 = ptr.DetermineParameters();
							if (!string.IsNullOrEmpty(text5))
							{
								controlBuilder.WithParameters(text5);
							}
							InternedString[] array = ptr.DetermineUsages();
							if (array != null)
							{
								controlBuilder.WithUsages(array);
							}
							ptr.AddChildControls(ref ptr, text4, ref builder);
						}
					}
				}
				return builder.Build();
			}

			// Token: 0x04000A27 RID: 2599
			public string displayName;

			// Token: 0x04000A28 RID: 2600
			public HID.HIDDeviceDescriptor hidDescriptor;

			// Token: 0x04000A29 RID: 2601
			public string parentLayout;

			// Token: 0x04000A2A RID: 2602
			public Type deviceType;
		}

		// Token: 0x020001E7 RID: 487
		public enum HIDReportType
		{
			// Token: 0x04000A2C RID: 2604
			Unknown,
			// Token: 0x04000A2D RID: 2605
			Input,
			// Token: 0x04000A2E RID: 2606
			Output,
			// Token: 0x04000A2F RID: 2607
			Feature
		}

		// Token: 0x020001E8 RID: 488
		public enum HIDCollectionType
		{
			// Token: 0x04000A31 RID: 2609
			Physical,
			// Token: 0x04000A32 RID: 2610
			Application,
			// Token: 0x04000A33 RID: 2611
			Logical,
			// Token: 0x04000A34 RID: 2612
			Report,
			// Token: 0x04000A35 RID: 2613
			NamedArray,
			// Token: 0x04000A36 RID: 2614
			UsageSwitch,
			// Token: 0x04000A37 RID: 2615
			UsageModifier
		}

		// Token: 0x020001E9 RID: 489
		[Flags]
		public enum HIDElementFlags
		{
			// Token: 0x04000A39 RID: 2617
			Constant = 1,
			// Token: 0x04000A3A RID: 2618
			Variable = 2,
			// Token: 0x04000A3B RID: 2619
			Relative = 4,
			// Token: 0x04000A3C RID: 2620
			Wrap = 8,
			// Token: 0x04000A3D RID: 2621
			NonLinear = 16,
			// Token: 0x04000A3E RID: 2622
			NoPreferred = 32,
			// Token: 0x04000A3F RID: 2623
			NullState = 64,
			// Token: 0x04000A40 RID: 2624
			Volatile = 128,
			// Token: 0x04000A41 RID: 2625
			BufferedBytes = 256
		}

		// Token: 0x020001EA RID: 490
		[Serializable]
		public struct HIDElementDescriptor
		{
			// Token: 0x170005A0 RID: 1440
			// (get) Token: 0x060014CE RID: 5326 RVA: 0x00060EEE File Offset: 0x0005F0EE
			public bool hasNullState
			{
				get
				{
					return (this.flags & HID.HIDElementFlags.NullState) == HID.HIDElementFlags.NullState;
				}
			}

			// Token: 0x170005A1 RID: 1441
			// (get) Token: 0x060014CF RID: 5327 RVA: 0x00060EFD File Offset: 0x0005F0FD
			public bool hasPreferredState
			{
				get
				{
					return (this.flags & HID.HIDElementFlags.NoPreferred) != HID.HIDElementFlags.NoPreferred;
				}
			}

			// Token: 0x170005A2 RID: 1442
			// (get) Token: 0x060014D0 RID: 5328 RVA: 0x00060F0F File Offset: 0x0005F10F
			public bool isArray
			{
				get
				{
					return (this.flags & HID.HIDElementFlags.Variable) != HID.HIDElementFlags.Variable;
				}
			}

			// Token: 0x170005A3 RID: 1443
			// (get) Token: 0x060014D1 RID: 5329 RVA: 0x00060F1F File Offset: 0x0005F11F
			public bool isNonLinear
			{
				get
				{
					return (this.flags & HID.HIDElementFlags.NonLinear) == HID.HIDElementFlags.NonLinear;
				}
			}

			// Token: 0x170005A4 RID: 1444
			// (get) Token: 0x060014D2 RID: 5330 RVA: 0x00060F2E File Offset: 0x0005F12E
			public bool isRelative
			{
				get
				{
					return (this.flags & HID.HIDElementFlags.Relative) == HID.HIDElementFlags.Relative;
				}
			}

			// Token: 0x170005A5 RID: 1445
			// (get) Token: 0x060014D3 RID: 5331 RVA: 0x00060F3B File Offset: 0x0005F13B
			public bool isConstant
			{
				get
				{
					return (this.flags & HID.HIDElementFlags.Constant) == HID.HIDElementFlags.Constant;
				}
			}

			// Token: 0x170005A6 RID: 1446
			// (get) Token: 0x060014D4 RID: 5332 RVA: 0x00060F48 File Offset: 0x0005F148
			public bool isWrapping
			{
				get
				{
					return (this.flags & HID.HIDElementFlags.Wrap) == HID.HIDElementFlags.Wrap;
				}
			}

			// Token: 0x170005A7 RID: 1447
			// (get) Token: 0x060014D5 RID: 5333 RVA: 0x00060F55 File Offset: 0x0005F155
			internal bool isSigned
			{
				get
				{
					return this.logicalMin < 0;
				}
			}

			// Token: 0x170005A8 RID: 1448
			// (get) Token: 0x060014D6 RID: 5334 RVA: 0x00060F60 File Offset: 0x0005F160
			internal float minFloatValue
			{
				get
				{
					if (this.isSigned)
					{
						int num = (int)(-(int)((int)1L << this.reportSizeInBits - 1));
						int num2 = (int)((1L << this.reportSizeInBits - 1) - 1L);
						return NumberHelpers.IntToNormalizedFloat(this.logicalMin, num, num2) * 2f - 1f;
					}
					uint num3 = (uint)((1L << this.reportSizeInBits) - 1L);
					return NumberHelpers.UIntToNormalizedFloat((uint)this.logicalMin, 0U, num3);
				}
			}

			// Token: 0x170005A9 RID: 1449
			// (get) Token: 0x060014D7 RID: 5335 RVA: 0x00060FD4 File Offset: 0x0005F1D4
			internal float maxFloatValue
			{
				get
				{
					if (this.isSigned)
					{
						int num = (int)(-(int)((int)1L << this.reportSizeInBits - 1));
						int num2 = (int)((1L << this.reportSizeInBits - 1) - 1L);
						return NumberHelpers.IntToNormalizedFloat(this.logicalMax, num, num2) * 2f - 1f;
					}
					uint num3 = (uint)((1L << this.reportSizeInBits) - 1L);
					return NumberHelpers.UIntToNormalizedFloat((uint)this.logicalMax, 0U, num3);
				}
			}

			// Token: 0x060014D8 RID: 5336 RVA: 0x00061045 File Offset: 0x0005F245
			public bool Is(HID.UsagePage usagePage, int usage)
			{
				return usagePage == this.usagePage && usage == this.usage;
			}

			// Token: 0x060014D9 RID: 5337 RVA: 0x0006105C File Offset: 0x0005F25C
			internal string DetermineName()
			{
				HID.UsagePage usagePage = this.usagePage;
				if (usagePage != HID.UsagePage.GenericDesktop)
				{
					if (usagePage != HID.UsagePage.Button)
					{
						return string.Format("UsagePage({0:X}) Usage({1:X})", this.usagePage, this.usage);
					}
					if (this.usage == 1)
					{
						return "trigger";
					}
					return string.Format("button{0}", this.usage);
				}
				else
				{
					if (this.usage == 57)
					{
						return "hat";
					}
					HID.GenericDesktop genericDesktop = (HID.GenericDesktop)this.usage;
					string text = genericDesktop.ToString();
					return char.ToLowerInvariant(text[0]).ToString() + text.Substring(1);
				}
			}

			// Token: 0x060014DA RID: 5338 RVA: 0x00061108 File Offset: 0x0005F308
			internal string DetermineDisplayName()
			{
				HID.UsagePage usagePage = this.usagePage;
				if (usagePage == HID.UsagePage.GenericDesktop)
				{
					HID.GenericDesktop genericDesktop = (HID.GenericDesktop)this.usage;
					return genericDesktop.ToString();
				}
				if (usagePage != HID.UsagePage.Button)
				{
					return null;
				}
				if (this.usage == 1)
				{
					return "Trigger";
				}
				return string.Format("Button {0}", this.usage);
			}

			// Token: 0x060014DB RID: 5339 RVA: 0x00061160 File Offset: 0x0005F360
			internal bool IsUsableElement()
			{
				int num = this.usage;
				if (num - 48 <= 1)
				{
					return this.usagePage == HID.UsagePage.GenericDesktop;
				}
				return this.DetermineLayout() != null;
			}

			// Token: 0x060014DC RID: 5340 RVA: 0x00061190 File Offset: 0x0005F390
			internal string DetermineLayout()
			{
				if (this.reportType != HID.HIDReportType.Input)
				{
					return null;
				}
				HID.UsagePage usagePage = this.usagePage;
				if (usagePage == HID.UsagePage.GenericDesktop)
				{
					int num = this.usage;
					switch (num)
					{
					case 48:
					case 49:
					case 50:
					case 51:
					case 52:
					case 53:
					case 54:
					case 55:
					case 56:
					case 64:
					case 65:
					case 66:
					case 67:
					case 68:
					case 69:
						return "Axis";
					case 57:
						if (this.logicalMax - this.logicalMin + 1 == 8)
						{
							return "Dpad";
						}
						goto IL_00BC;
					case 58:
					case 59:
					case 60:
					case 63:
						goto IL_00BC;
					case 61:
					case 62:
						break;
					default:
						if (num - 144 > 3)
						{
							goto IL_00BC;
						}
						break;
					}
					return "Button";
				}
				if (usagePage == HID.UsagePage.Button)
				{
					return "Button";
				}
				IL_00BC:
				return null;
			}

			// Token: 0x060014DD RID: 5341 RVA: 0x0006125C File Offset: 0x0005F45C
			internal FourCC DetermineFormat()
			{
				int num = this.reportSizeInBits;
				if (num != 8)
				{
					if (num != 16)
					{
						if (num != 32)
						{
							return InputStateBlock.FormatBit;
						}
						if (!this.isSigned)
						{
							return InputStateBlock.FormatUInt;
						}
						return InputStateBlock.FormatInt;
					}
					else
					{
						if (!this.isSigned)
						{
							return InputStateBlock.FormatUShort;
						}
						return InputStateBlock.FormatShort;
					}
				}
				else
				{
					if (!this.isSigned)
					{
						return InputStateBlock.FormatByte;
					}
					return InputStateBlock.FormatSByte;
				}
			}

			// Token: 0x060014DE RID: 5342 RVA: 0x000612C4 File Offset: 0x0005F4C4
			internal InternedString[] DetermineUsages()
			{
				if (this.usagePage == HID.UsagePage.Button && this.usage == 1)
				{
					return new InternedString[]
					{
						CommonUsages.PrimaryTrigger,
						CommonUsages.PrimaryAction
					};
				}
				if (this.usagePage == HID.UsagePage.Button && this.usage == 2)
				{
					return new InternedString[]
					{
						CommonUsages.SecondaryTrigger,
						CommonUsages.SecondaryAction
					};
				}
				if (this.usagePage == HID.UsagePage.GenericDesktop && this.usage == 53)
				{
					return new InternedString[] { CommonUsages.Twist };
				}
				return null;
			}

			// Token: 0x060014DF RID: 5343 RVA: 0x0006135C File Offset: 0x0005F55C
			internal string DetermineParameters()
			{
				if (this.usagePage == HID.UsagePage.GenericDesktop)
				{
					switch (this.usage)
					{
					case 48:
					case 50:
					case 51:
					case 53:
					case 54:
					case 55:
					case 56:
					case 64:
					case 66:
					case 67:
					case 69:
						return this.DetermineAxisNormalizationParameters();
					case 49:
					case 52:
					case 65:
					case 68:
						return StringHelpers.Join<string>(",", new string[]
						{
							"invert",
							this.DetermineAxisNormalizationParameters()
						});
					}
				}
				return null;
			}

			// Token: 0x060014E0 RID: 5344 RVA: 0x0006140C File Offset: 0x0005F60C
			private string DetermineAxisNormalizationParameters()
			{
				if (this.logicalMin == 0 && this.logicalMax == 0)
				{
					return "normalize,normalizeMin=0,normalizeMax=1,normalizeZero=0.5";
				}
				float minFloatValue = this.minFloatValue;
				float maxFloatValue = this.maxFloatValue;
				if (Mathf.Approximately(0f, minFloatValue) && Mathf.Approximately(0f, maxFloatValue))
				{
					return null;
				}
				float num = minFloatValue + (maxFloatValue - minFloatValue) / 2f;
				return string.Format(CultureInfo.InvariantCulture, "normalize,normalizeMin={0},normalizeMax={1},normalizeZero={2}", minFloatValue, maxFloatValue, num);
			}

			// Token: 0x060014E1 RID: 5345 RVA: 0x00061488 File Offset: 0x0005F688
			internal string DetermineProcessors()
			{
				if (this.usagePage == HID.UsagePage.GenericDesktop)
				{
					int num = this.usage;
					if (num - 48 <= 8 || num - 64 <= 5)
					{
						return "axisDeadzone";
					}
				}
				return null;
			}

			// Token: 0x060014E2 RID: 5346 RVA: 0x000614BC File Offset: 0x0005F6BC
			internal PrimitiveValue DetermineDefaultState()
			{
				if (this.usagePage == HID.UsagePage.GenericDesktop)
				{
					switch (this.usage)
					{
					case 48:
					case 49:
					case 50:
					case 51:
					case 52:
					case 53:
					case 54:
					case 55:
					case 56:
					case 64:
					case 65:
					case 66:
					case 67:
					case 68:
					case 69:
						if (!this.isSigned)
						{
							int num = this.logicalMin + (this.logicalMax - this.logicalMin) / 2;
							if (num != 0)
							{
								return new PrimitiveValue(num);
							}
						}
						break;
					case 57:
						if (this.hasNullState)
						{
							if (this.logicalMin >= 1)
							{
								return new PrimitiveValue(this.logicalMin - 1);
							}
							ulong num2 = (1UL << this.reportSizeInBits) - 1UL;
							if ((long)this.logicalMax < (long)num2)
							{
								return new PrimitiveValue(this.logicalMax + 1);
							}
						}
						break;
					}
				}
				return default(PrimitiveValue);
			}

			// Token: 0x060014E3 RID: 5347 RVA: 0x000615B8 File Offset: 0x0005F7B8
			internal void AddChildControls(ref HID.HIDElementDescriptor element, string controlName, ref InputControlLayout.Builder builder)
			{
				if (this.usagePage == HID.UsagePage.GenericDesktop && this.usage == 57)
				{
					PrimitiveValue primitiveValue = this.DetermineDefaultState();
					if (primitiveValue.isEmpty)
					{
						return;
					}
					builder.AddControl(controlName + "/up").WithFormat(InputStateBlock.FormatBit).WithLayout("DiscreteButton")
						.WithParameters(string.Format(CultureInfo.InvariantCulture, "minValue={0},maxValue={1},nullValue={2},wrapAtValue={3}", new object[]
						{
							this.logicalMax,
							this.logicalMin + 1,
							primitiveValue.ToString(),
							this.logicalMax
						}))
						.WithBitOffset((uint)(element.reportOffsetInBits % 8))
						.WithSizeInBits((uint)this.reportSizeInBits);
					builder.AddControl(controlName + "/right").WithFormat(InputStateBlock.FormatBit).WithLayout("DiscreteButton")
						.WithParameters(string.Format(CultureInfo.InvariantCulture, "minValue={0},maxValue={1}", this.logicalMin + 1, this.logicalMin + 3))
						.WithBitOffset((uint)(element.reportOffsetInBits % 8))
						.WithSizeInBits((uint)this.reportSizeInBits);
					builder.AddControl(controlName + "/down").WithFormat(InputStateBlock.FormatBit).WithLayout("DiscreteButton")
						.WithParameters(string.Format(CultureInfo.InvariantCulture, "minValue={0},maxValue={1}", this.logicalMin + 3, this.logicalMin + 5))
						.WithBitOffset((uint)(element.reportOffsetInBits % 8))
						.WithSizeInBits((uint)this.reportSizeInBits);
					builder.AddControl(controlName + "/left").WithFormat(InputStateBlock.FormatBit).WithLayout("DiscreteButton")
						.WithParameters(string.Format(CultureInfo.InvariantCulture, "minValue={0},maxValue={1}", this.logicalMin + 5, this.logicalMin + 7))
						.WithBitOffset((uint)(element.reportOffsetInBits % 8))
						.WithSizeInBits((uint)this.reportSizeInBits);
				}
			}

			// Token: 0x04000A42 RID: 2626
			public int usage;

			// Token: 0x04000A43 RID: 2627
			public HID.UsagePage usagePage;

			// Token: 0x04000A44 RID: 2628
			public int unit;

			// Token: 0x04000A45 RID: 2629
			public int unitExponent;

			// Token: 0x04000A46 RID: 2630
			public int logicalMin;

			// Token: 0x04000A47 RID: 2631
			public int logicalMax;

			// Token: 0x04000A48 RID: 2632
			public int physicalMin;

			// Token: 0x04000A49 RID: 2633
			public int physicalMax;

			// Token: 0x04000A4A RID: 2634
			public HID.HIDReportType reportType;

			// Token: 0x04000A4B RID: 2635
			public int collectionIndex;

			// Token: 0x04000A4C RID: 2636
			public int reportId;

			// Token: 0x04000A4D RID: 2637
			public int reportSizeInBits;

			// Token: 0x04000A4E RID: 2638
			public int reportOffsetInBits;

			// Token: 0x04000A4F RID: 2639
			public HID.HIDElementFlags flags;

			// Token: 0x04000A50 RID: 2640
			public int? usageMin;

			// Token: 0x04000A51 RID: 2641
			public int? usageMax;
		}

		// Token: 0x020001EB RID: 491
		[Serializable]
		public struct HIDCollectionDescriptor
		{
			// Token: 0x04000A52 RID: 2642
			public HID.HIDCollectionType type;

			// Token: 0x04000A53 RID: 2643
			public int usage;

			// Token: 0x04000A54 RID: 2644
			public HID.UsagePage usagePage;

			// Token: 0x04000A55 RID: 2645
			public int parent;

			// Token: 0x04000A56 RID: 2646
			public int childCount;

			// Token: 0x04000A57 RID: 2647
			public int firstChild;
		}

		// Token: 0x020001EC RID: 492
		[Serializable]
		public struct HIDDeviceDescriptor
		{
			// Token: 0x060014E4 RID: 5348 RVA: 0x00061807 File Offset: 0x0005FA07
			public string ToJson()
			{
				return JsonUtility.ToJson(this, true);
			}

			// Token: 0x060014E5 RID: 5349 RVA: 0x0006181C File Offset: 0x0005FA1C
			public static HID.HIDDeviceDescriptor FromJson(string json)
			{
				HID.HIDDeviceDescriptor hiddeviceDescriptor2;
				try
				{
					HID.HIDDeviceDescriptor hiddeviceDescriptor = default(HID.HIDDeviceDescriptor);
					ReadOnlySpan<char> readOnlySpan = json.AsSpan();
					PredictiveParser predictiveParser = default(PredictiveParser);
					predictiveParser.ExpectSingleChar(readOnlySpan, '{');
					ReadOnlySpan<char> readOnlySpan2;
					predictiveParser.AcceptString(readOnlySpan, out readOnlySpan2);
					predictiveParser.ExpectSingleChar(readOnlySpan, ':');
					hiddeviceDescriptor.vendorId = predictiveParser.ExpectInt(readOnlySpan);
					predictiveParser.AcceptSingleChar(readOnlySpan, ',');
					predictiveParser.AcceptString(readOnlySpan, out readOnlySpan2);
					predictiveParser.ExpectSingleChar(readOnlySpan, ':');
					hiddeviceDescriptor.productId = predictiveParser.ExpectInt(readOnlySpan);
					predictiveParser.AcceptSingleChar(readOnlySpan, ',');
					predictiveParser.AcceptString(readOnlySpan, out readOnlySpan2);
					predictiveParser.ExpectSingleChar(readOnlySpan, ':');
					hiddeviceDescriptor.usage = predictiveParser.ExpectInt(readOnlySpan);
					predictiveParser.AcceptSingleChar(readOnlySpan, ',');
					predictiveParser.AcceptString(readOnlySpan, out readOnlySpan2);
					predictiveParser.ExpectSingleChar(readOnlySpan, ':');
					hiddeviceDescriptor.usagePage = (HID.UsagePage)predictiveParser.ExpectInt(readOnlySpan);
					predictiveParser.AcceptSingleChar(readOnlySpan, ',');
					predictiveParser.AcceptString(readOnlySpan, out readOnlySpan2);
					predictiveParser.ExpectSingleChar(readOnlySpan, ':');
					hiddeviceDescriptor.inputReportSize = predictiveParser.ExpectInt(readOnlySpan);
					predictiveParser.AcceptSingleChar(readOnlySpan, ',');
					predictiveParser.AcceptString(readOnlySpan, out readOnlySpan2);
					predictiveParser.ExpectSingleChar(readOnlySpan, ':');
					hiddeviceDescriptor.outputReportSize = predictiveParser.ExpectInt(readOnlySpan);
					predictiveParser.AcceptSingleChar(readOnlySpan, ',');
					predictiveParser.AcceptString(readOnlySpan, out readOnlySpan2);
					predictiveParser.ExpectSingleChar(readOnlySpan, ':');
					hiddeviceDescriptor.featureReportSize = predictiveParser.ExpectInt(readOnlySpan);
					predictiveParser.AcceptSingleChar(readOnlySpan, ',');
					ReadOnlySpan<char> readOnlySpan3;
					predictiveParser.AcceptString(readOnlySpan, out readOnlySpan3);
					if (readOnlySpan3.ToString() != "elements")
					{
						hiddeviceDescriptor2 = hiddeviceDescriptor;
					}
					else
					{
						predictiveParser.ExpectSingleChar(readOnlySpan, ':');
						predictiveParser.ExpectSingleChar(readOnlySpan, '[');
						List<HID.HIDElementDescriptor> list;
						using (CollectionPool<List<HID.HIDElementDescriptor>, HID.HIDElementDescriptor>.Get(out list))
						{
							while (!predictiveParser.AcceptSingleChar(readOnlySpan, ']'))
							{
								predictiveParser.AcceptSingleChar(readOnlySpan, ',');
								predictiveParser.ExpectSingleChar(readOnlySpan, '{');
								HID.HIDElementDescriptor hidelementDescriptor = default(HID.HIDElementDescriptor);
								predictiveParser.AcceptSingleChar(readOnlySpan, '}');
								predictiveParser.AcceptSingleChar(readOnlySpan, ',');
								predictiveParser.ExpectString(readOnlySpan);
								predictiveParser.ExpectSingleChar(readOnlySpan, ':');
								hidelementDescriptor.usage = predictiveParser.ExpectInt(readOnlySpan);
								predictiveParser.AcceptSingleChar(readOnlySpan, ',');
								predictiveParser.ExpectString(readOnlySpan);
								predictiveParser.ExpectSingleChar(readOnlySpan, ':');
								hidelementDescriptor.usagePage = (HID.UsagePage)predictiveParser.ExpectInt(readOnlySpan);
								predictiveParser.AcceptSingleChar(readOnlySpan, ',');
								predictiveParser.ExpectString(readOnlySpan);
								predictiveParser.ExpectSingleChar(readOnlySpan, ':');
								hidelementDescriptor.unit = predictiveParser.ExpectInt(readOnlySpan);
								predictiveParser.AcceptSingleChar(readOnlySpan, ',');
								predictiveParser.ExpectString(readOnlySpan);
								predictiveParser.ExpectSingleChar(readOnlySpan, ':');
								hidelementDescriptor.unitExponent = predictiveParser.ExpectInt(readOnlySpan);
								predictiveParser.AcceptSingleChar(readOnlySpan, ',');
								predictiveParser.ExpectString(readOnlySpan);
								predictiveParser.ExpectSingleChar(readOnlySpan, ':');
								hidelementDescriptor.logicalMin = predictiveParser.ExpectInt(readOnlySpan);
								predictiveParser.AcceptSingleChar(readOnlySpan, ',');
								predictiveParser.ExpectString(readOnlySpan);
								predictiveParser.ExpectSingleChar(readOnlySpan, ':');
								hidelementDescriptor.logicalMax = predictiveParser.ExpectInt(readOnlySpan);
								predictiveParser.AcceptSingleChar(readOnlySpan, ',');
								predictiveParser.ExpectString(readOnlySpan);
								predictiveParser.ExpectSingleChar(readOnlySpan, ':');
								hidelementDescriptor.physicalMin = predictiveParser.ExpectInt(readOnlySpan);
								predictiveParser.AcceptSingleChar(readOnlySpan, ',');
								predictiveParser.ExpectString(readOnlySpan);
								predictiveParser.ExpectSingleChar(readOnlySpan, ':');
								hidelementDescriptor.physicalMax = predictiveParser.ExpectInt(readOnlySpan);
								predictiveParser.AcceptSingleChar(readOnlySpan, ',');
								predictiveParser.ExpectString(readOnlySpan);
								predictiveParser.ExpectSingleChar(readOnlySpan, ':');
								hidelementDescriptor.collectionIndex = predictiveParser.ExpectInt(readOnlySpan);
								predictiveParser.AcceptSingleChar(readOnlySpan, ',');
								predictiveParser.ExpectString(readOnlySpan);
								predictiveParser.ExpectSingleChar(readOnlySpan, ':');
								hidelementDescriptor.reportType = (HID.HIDReportType)predictiveParser.ExpectInt(readOnlySpan);
								predictiveParser.AcceptSingleChar(readOnlySpan, ',');
								predictiveParser.ExpectString(readOnlySpan);
								predictiveParser.ExpectSingleChar(readOnlySpan, ':');
								hidelementDescriptor.reportId = predictiveParser.ExpectInt(readOnlySpan);
								predictiveParser.AcceptSingleChar(readOnlySpan, ',');
								predictiveParser.ExpectString(readOnlySpan);
								predictiveParser.ExpectSingleChar(readOnlySpan, ':');
								predictiveParser.AcceptInt(readOnlySpan);
								predictiveParser.AcceptSingleChar(readOnlySpan, ',');
								predictiveParser.ExpectString(readOnlySpan);
								predictiveParser.ExpectSingleChar(readOnlySpan, ':');
								hidelementDescriptor.reportSizeInBits = predictiveParser.ExpectInt(readOnlySpan);
								predictiveParser.AcceptSingleChar(readOnlySpan, ',');
								predictiveParser.ExpectString(readOnlySpan);
								predictiveParser.ExpectSingleChar(readOnlySpan, ':');
								hidelementDescriptor.reportOffsetInBits = predictiveParser.ExpectInt(readOnlySpan);
								predictiveParser.AcceptSingleChar(readOnlySpan, ',');
								predictiveParser.ExpectString(readOnlySpan);
								predictiveParser.ExpectSingleChar(readOnlySpan, ':');
								hidelementDescriptor.flags = (HID.HIDElementFlags)predictiveParser.ExpectInt(readOnlySpan);
								predictiveParser.ExpectSingleChar(readOnlySpan, '}');
								list.Add(hidelementDescriptor);
							}
							hiddeviceDescriptor.elements = list.ToArray();
							hiddeviceDescriptor2 = hiddeviceDescriptor;
						}
					}
				}
				catch (Exception)
				{
					hiddeviceDescriptor2 = JsonUtility.FromJson<HID.HIDDeviceDescriptor>(json);
				}
				return hiddeviceDescriptor2;
			}

			// Token: 0x04000A58 RID: 2648
			public int vendorId;

			// Token: 0x04000A59 RID: 2649
			public int productId;

			// Token: 0x04000A5A RID: 2650
			public int usage;

			// Token: 0x04000A5B RID: 2651
			public HID.UsagePage usagePage;

			// Token: 0x04000A5C RID: 2652
			public int inputReportSize;

			// Token: 0x04000A5D RID: 2653
			public int outputReportSize;

			// Token: 0x04000A5E RID: 2654
			public int featureReportSize;

			// Token: 0x04000A5F RID: 2655
			public HID.HIDElementDescriptor[] elements;

			// Token: 0x04000A60 RID: 2656
			public HID.HIDCollectionDescriptor[] collections;
		}

		// Token: 0x020001ED RID: 493
		public struct HIDDeviceDescriptorBuilder
		{
			// Token: 0x060014E6 RID: 5350 RVA: 0x00061D2C File Offset: 0x0005FF2C
			public HIDDeviceDescriptorBuilder(HID.UsagePage usagePage, int usage)
			{
				this = default(HID.HIDDeviceDescriptorBuilder);
				this.usagePage = usagePage;
				this.usage = usage;
			}

			// Token: 0x060014E7 RID: 5351 RVA: 0x00061D43 File Offset: 0x0005FF43
			public HIDDeviceDescriptorBuilder(HID.GenericDesktop usage)
			{
				this = new HID.HIDDeviceDescriptorBuilder(HID.UsagePage.GenericDesktop, (int)usage);
			}

			// Token: 0x060014E8 RID: 5352 RVA: 0x00061D4D File Offset: 0x0005FF4D
			public HID.HIDDeviceDescriptorBuilder StartReport(HID.HIDReportType reportType, int reportId = 1)
			{
				this.m_CurrentReportId = reportId;
				this.m_CurrentReportType = reportType;
				this.m_CurrentReportOffsetInBits = 8;
				return this;
			}

			// Token: 0x060014E9 RID: 5353 RVA: 0x00061D6C File Offset: 0x0005FF6C
			public HID.HIDDeviceDescriptorBuilder AddElement(HID.UsagePage usagePage, int usage, int sizeInBits)
			{
				if (this.m_Elements == null)
				{
					this.m_Elements = new List<HID.HIDElementDescriptor>();
				}
				else
				{
					foreach (HID.HIDElementDescriptor hidelementDescriptor in this.m_Elements)
					{
						if (hidelementDescriptor.reportId == this.m_CurrentReportId && hidelementDescriptor.reportType == this.m_CurrentReportType && hidelementDescriptor.usagePage == usagePage && hidelementDescriptor.usage == usage)
						{
							throw new InvalidOperationException(string.Format("Cannot add two elements with the same usage page '{0}' and usage '0x{1:X} the to same device", usagePage, usage));
						}
					}
				}
				this.m_Elements.Add(new HID.HIDElementDescriptor
				{
					usage = usage,
					usagePage = usagePage,
					reportOffsetInBits = this.m_CurrentReportOffsetInBits,
					reportSizeInBits = sizeInBits,
					reportType = this.m_CurrentReportType,
					reportId = this.m_CurrentReportId
				});
				this.m_CurrentReportOffsetInBits += sizeInBits;
				return this;
			}

			// Token: 0x060014EA RID: 5354 RVA: 0x00061E80 File Offset: 0x00060080
			public HID.HIDDeviceDescriptorBuilder AddElement(HID.GenericDesktop usage, int sizeInBits)
			{
				return this.AddElement(HID.UsagePage.GenericDesktop, (int)usage, sizeInBits);
			}

			// Token: 0x060014EB RID: 5355 RVA: 0x00061E8C File Offset: 0x0006008C
			public HID.HIDDeviceDescriptorBuilder WithPhysicalMinMax(int min, int max)
			{
				int num = this.m_Elements.Count - 1;
				if (num < 0)
				{
					throw new InvalidOperationException("No element has been added to the descriptor yet");
				}
				HID.HIDElementDescriptor hidelementDescriptor = this.m_Elements[num];
				hidelementDescriptor.physicalMin = min;
				hidelementDescriptor.physicalMax = max;
				this.m_Elements[num] = hidelementDescriptor;
				return this;
			}

			// Token: 0x060014EC RID: 5356 RVA: 0x00061EE8 File Offset: 0x000600E8
			public HID.HIDDeviceDescriptorBuilder WithLogicalMinMax(int min, int max)
			{
				int num = this.m_Elements.Count - 1;
				if (num < 0)
				{
					throw new InvalidOperationException("No element has been added to the descriptor yet");
				}
				HID.HIDElementDescriptor hidelementDescriptor = this.m_Elements[num];
				hidelementDescriptor.logicalMin = min;
				hidelementDescriptor.logicalMax = max;
				this.m_Elements[num] = hidelementDescriptor;
				return this;
			}

			// Token: 0x060014ED RID: 5357 RVA: 0x00061F44 File Offset: 0x00060144
			public HID.HIDDeviceDescriptor Finish()
			{
				HID.HIDDeviceDescriptor hiddeviceDescriptor = default(HID.HIDDeviceDescriptor);
				hiddeviceDescriptor.usage = this.usage;
				hiddeviceDescriptor.usagePage = this.usagePage;
				List<HID.HIDElementDescriptor> elements = this.m_Elements;
				hiddeviceDescriptor.elements = ((elements != null) ? elements.ToArray() : null);
				List<HID.HIDCollectionDescriptor> collections = this.m_Collections;
				hiddeviceDescriptor.collections = ((collections != null) ? collections.ToArray() : null);
				return hiddeviceDescriptor;
			}

			// Token: 0x04000A61 RID: 2657
			public HID.UsagePage usagePage;

			// Token: 0x04000A62 RID: 2658
			public int usage;

			// Token: 0x04000A63 RID: 2659
			private int m_CurrentReportId;

			// Token: 0x04000A64 RID: 2660
			private HID.HIDReportType m_CurrentReportType;

			// Token: 0x04000A65 RID: 2661
			private int m_CurrentReportOffsetInBits;

			// Token: 0x04000A66 RID: 2662
			private List<HID.HIDElementDescriptor> m_Elements;

			// Token: 0x04000A67 RID: 2663
			private List<HID.HIDCollectionDescriptor> m_Collections;

			// Token: 0x04000A68 RID: 2664
			private int m_InputReportSize;

			// Token: 0x04000A69 RID: 2665
			private int m_OutputReportSize;

			// Token: 0x04000A6A RID: 2666
			private int m_FeatureReportSize;
		}

		// Token: 0x020001EE RID: 494
		public enum UsagePage
		{
			// Token: 0x04000A6C RID: 2668
			Undefined,
			// Token: 0x04000A6D RID: 2669
			GenericDesktop,
			// Token: 0x04000A6E RID: 2670
			Simulation,
			// Token: 0x04000A6F RID: 2671
			VRControls,
			// Token: 0x04000A70 RID: 2672
			SportControls,
			// Token: 0x04000A71 RID: 2673
			GameControls,
			// Token: 0x04000A72 RID: 2674
			GenericDeviceControls,
			// Token: 0x04000A73 RID: 2675
			Keyboard,
			// Token: 0x04000A74 RID: 2676
			LEDs,
			// Token: 0x04000A75 RID: 2677
			Button,
			// Token: 0x04000A76 RID: 2678
			Ordinal,
			// Token: 0x04000A77 RID: 2679
			Telephony,
			// Token: 0x04000A78 RID: 2680
			Consumer,
			// Token: 0x04000A79 RID: 2681
			Digitizer,
			// Token: 0x04000A7A RID: 2682
			PID = 15,
			// Token: 0x04000A7B RID: 2683
			Unicode,
			// Token: 0x04000A7C RID: 2684
			AlphanumericDisplay = 20,
			// Token: 0x04000A7D RID: 2685
			MedicalInstruments = 64,
			// Token: 0x04000A7E RID: 2686
			Monitor = 128,
			// Token: 0x04000A7F RID: 2687
			Power = 132,
			// Token: 0x04000A80 RID: 2688
			BarCodeScanner = 140,
			// Token: 0x04000A81 RID: 2689
			MagneticStripeReader = 142,
			// Token: 0x04000A82 RID: 2690
			Camera = 144,
			// Token: 0x04000A83 RID: 2691
			Arcade,
			// Token: 0x04000A84 RID: 2692
			VendorDefined = 65280
		}

		// Token: 0x020001EF RID: 495
		public enum GenericDesktop
		{
			// Token: 0x04000A86 RID: 2694
			Undefined,
			// Token: 0x04000A87 RID: 2695
			Pointer,
			// Token: 0x04000A88 RID: 2696
			Mouse,
			// Token: 0x04000A89 RID: 2697
			Joystick = 4,
			// Token: 0x04000A8A RID: 2698
			Gamepad,
			// Token: 0x04000A8B RID: 2699
			Keyboard,
			// Token: 0x04000A8C RID: 2700
			Keypad,
			// Token: 0x04000A8D RID: 2701
			MultiAxisController,
			// Token: 0x04000A8E RID: 2702
			TabletPCControls,
			// Token: 0x04000A8F RID: 2703
			AssistiveControl,
			// Token: 0x04000A90 RID: 2704
			X = 48,
			// Token: 0x04000A91 RID: 2705
			Y,
			// Token: 0x04000A92 RID: 2706
			Z,
			// Token: 0x04000A93 RID: 2707
			Rx,
			// Token: 0x04000A94 RID: 2708
			Ry,
			// Token: 0x04000A95 RID: 2709
			Rz,
			// Token: 0x04000A96 RID: 2710
			Slider,
			// Token: 0x04000A97 RID: 2711
			Dial,
			// Token: 0x04000A98 RID: 2712
			Wheel,
			// Token: 0x04000A99 RID: 2713
			HatSwitch,
			// Token: 0x04000A9A RID: 2714
			CountedBuffer,
			// Token: 0x04000A9B RID: 2715
			ByteCount,
			// Token: 0x04000A9C RID: 2716
			MotionWakeup,
			// Token: 0x04000A9D RID: 2717
			Start,
			// Token: 0x04000A9E RID: 2718
			Select,
			// Token: 0x04000A9F RID: 2719
			Vx = 64,
			// Token: 0x04000AA0 RID: 2720
			Vy,
			// Token: 0x04000AA1 RID: 2721
			Vz,
			// Token: 0x04000AA2 RID: 2722
			Vbrx,
			// Token: 0x04000AA3 RID: 2723
			Vbry,
			// Token: 0x04000AA4 RID: 2724
			Vbrz,
			// Token: 0x04000AA5 RID: 2725
			Vno,
			// Token: 0x04000AA6 RID: 2726
			FeatureNotification,
			// Token: 0x04000AA7 RID: 2727
			ResolutionMultiplier,
			// Token: 0x04000AA8 RID: 2728
			SystemControl = 128,
			// Token: 0x04000AA9 RID: 2729
			SystemPowerDown,
			// Token: 0x04000AAA RID: 2730
			SystemSleep,
			// Token: 0x04000AAB RID: 2731
			SystemWakeUp,
			// Token: 0x04000AAC RID: 2732
			SystemContextMenu,
			// Token: 0x04000AAD RID: 2733
			SystemMainMenu,
			// Token: 0x04000AAE RID: 2734
			SystemAppMenu,
			// Token: 0x04000AAF RID: 2735
			SystemMenuHelp,
			// Token: 0x04000AB0 RID: 2736
			SystemMenuExit,
			// Token: 0x04000AB1 RID: 2737
			SystemMenuSelect,
			// Token: 0x04000AB2 RID: 2738
			SystemMenuRight,
			// Token: 0x04000AB3 RID: 2739
			SystemMenuLeft,
			// Token: 0x04000AB4 RID: 2740
			SystemMenuUp,
			// Token: 0x04000AB5 RID: 2741
			SystemMenuDown,
			// Token: 0x04000AB6 RID: 2742
			SystemColdRestart,
			// Token: 0x04000AB7 RID: 2743
			SystemWarmRestart,
			// Token: 0x04000AB8 RID: 2744
			DpadUp,
			// Token: 0x04000AB9 RID: 2745
			DpadDown,
			// Token: 0x04000ABA RID: 2746
			DpadRight,
			// Token: 0x04000ABB RID: 2747
			DpadLeft,
			// Token: 0x04000ABC RID: 2748
			SystemDock = 160,
			// Token: 0x04000ABD RID: 2749
			SystemUndock,
			// Token: 0x04000ABE RID: 2750
			SystemSetup,
			// Token: 0x04000ABF RID: 2751
			SystemBreak,
			// Token: 0x04000AC0 RID: 2752
			SystemDebuggerBreak,
			// Token: 0x04000AC1 RID: 2753
			ApplicationBreak,
			// Token: 0x04000AC2 RID: 2754
			ApplicationDebuggerBreak,
			// Token: 0x04000AC3 RID: 2755
			SystemSpeakerMute,
			// Token: 0x04000AC4 RID: 2756
			SystemHibernate,
			// Token: 0x04000AC5 RID: 2757
			SystemDisplayInvert = 176,
			// Token: 0x04000AC6 RID: 2758
			SystemDisplayInternal,
			// Token: 0x04000AC7 RID: 2759
			SystemDisplayExternal,
			// Token: 0x04000AC8 RID: 2760
			SystemDisplayBoth,
			// Token: 0x04000AC9 RID: 2761
			SystemDisplayDual,
			// Token: 0x04000ACA RID: 2762
			SystemDisplayToggleIntExt,
			// Token: 0x04000ACB RID: 2763
			SystemDisplaySwapPrimarySecondary,
			// Token: 0x04000ACC RID: 2764
			SystemDisplayLCDAutoScale
		}

		// Token: 0x020001F0 RID: 496
		public enum Simulation
		{
			// Token: 0x04000ACE RID: 2766
			Undefined,
			// Token: 0x04000ACF RID: 2767
			FlightSimulationDevice,
			// Token: 0x04000AD0 RID: 2768
			AutomobileSimulationDevice,
			// Token: 0x04000AD1 RID: 2769
			TankSimulationDevice,
			// Token: 0x04000AD2 RID: 2770
			SpaceshipSimulationDevice,
			// Token: 0x04000AD3 RID: 2771
			SubmarineSimulationDevice,
			// Token: 0x04000AD4 RID: 2772
			SailingSimulationDevice,
			// Token: 0x04000AD5 RID: 2773
			MotorcycleSimulationDevice,
			// Token: 0x04000AD6 RID: 2774
			SportsSimulationDevice,
			// Token: 0x04000AD7 RID: 2775
			AirplaneSimulationDevice,
			// Token: 0x04000AD8 RID: 2776
			HelicopterSimulationDevice,
			// Token: 0x04000AD9 RID: 2777
			MagicCarpetSimulationDevice,
			// Token: 0x04000ADA RID: 2778
			BicylcleSimulationDevice,
			// Token: 0x04000ADB RID: 2779
			FlightControlStick = 32,
			// Token: 0x04000ADC RID: 2780
			FlightStick,
			// Token: 0x04000ADD RID: 2781
			CyclicControl,
			// Token: 0x04000ADE RID: 2782
			CyclicTrim,
			// Token: 0x04000ADF RID: 2783
			FlightYoke,
			// Token: 0x04000AE0 RID: 2784
			TrackControl,
			// Token: 0x04000AE1 RID: 2785
			Aileron = 176,
			// Token: 0x04000AE2 RID: 2786
			AileronTrim,
			// Token: 0x04000AE3 RID: 2787
			AntiTorqueControl,
			// Token: 0x04000AE4 RID: 2788
			AutopilotEnable,
			// Token: 0x04000AE5 RID: 2789
			ChaffRelease,
			// Token: 0x04000AE6 RID: 2790
			CollectiveControl,
			// Token: 0x04000AE7 RID: 2791
			DiveBreak,
			// Token: 0x04000AE8 RID: 2792
			ElectronicCountermeasures,
			// Token: 0x04000AE9 RID: 2793
			Elevator,
			// Token: 0x04000AEA RID: 2794
			ElevatorTrim,
			// Token: 0x04000AEB RID: 2795
			Rudder,
			// Token: 0x04000AEC RID: 2796
			Throttle,
			// Token: 0x04000AED RID: 2797
			FlightCommunications,
			// Token: 0x04000AEE RID: 2798
			FlareRelease,
			// Token: 0x04000AEF RID: 2799
			LandingGear,
			// Token: 0x04000AF0 RID: 2800
			ToeBreak,
			// Token: 0x04000AF1 RID: 2801
			Trigger,
			// Token: 0x04000AF2 RID: 2802
			WeaponsArm,
			// Token: 0x04000AF3 RID: 2803
			WeaponsSelect,
			// Token: 0x04000AF4 RID: 2804
			WingFlaps,
			// Token: 0x04000AF5 RID: 2805
			Accelerator,
			// Token: 0x04000AF6 RID: 2806
			Brake,
			// Token: 0x04000AF7 RID: 2807
			Clutch,
			// Token: 0x04000AF8 RID: 2808
			Shifter,
			// Token: 0x04000AF9 RID: 2809
			Steering,
			// Token: 0x04000AFA RID: 2810
			TurretDirection,
			// Token: 0x04000AFB RID: 2811
			BarrelElevation,
			// Token: 0x04000AFC RID: 2812
			DivePlane,
			// Token: 0x04000AFD RID: 2813
			Ballast,
			// Token: 0x04000AFE RID: 2814
			BicycleCrank,
			// Token: 0x04000AFF RID: 2815
			HandleBars,
			// Token: 0x04000B00 RID: 2816
			FrontBrake,
			// Token: 0x04000B01 RID: 2817
			RearBrake
		}

		// Token: 0x020001F1 RID: 497
		public enum Button
		{
			// Token: 0x04000B03 RID: 2819
			Undefined,
			// Token: 0x04000B04 RID: 2820
			Primary,
			// Token: 0x04000B05 RID: 2821
			Secondary,
			// Token: 0x04000B06 RID: 2822
			Tertiary
		}
	}
}
