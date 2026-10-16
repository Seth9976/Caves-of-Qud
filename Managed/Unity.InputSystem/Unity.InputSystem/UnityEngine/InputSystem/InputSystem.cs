using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Profiling;
using UnityEngine.InputSystem.DualShock;
using UnityEngine.InputSystem.Haptics;
using UnityEngine.InputSystem.HID;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Switch;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.InputSystem.XInput;
using UnityEngine.InputSystem.XR;

namespace UnityEngine.InputSystem
{
	// Token: 0x0200002D RID: 45
	public static class InputSystem
	{
		// Token: 0x14000005 RID: 5
		// (add) Token: 0x0600032C RID: 812 RVA: 0x0000EBB0 File Offset: 0x0000CDB0
		// (remove) Token: 0x0600032D RID: 813 RVA: 0x0000EBF4 File Offset: 0x0000CDF4
		public static event Action<string, InputControlLayoutChange> onLayoutChange
		{
			add
			{
				InputManager inputManager = InputSystem.s_Manager;
				lock (inputManager)
				{
					InputSystem.s_Manager.onLayoutChange += value;
				}
			}
			remove
			{
				InputManager inputManager = InputSystem.s_Manager;
				lock (inputManager)
				{
					InputSystem.s_Manager.onLayoutChange -= value;
				}
			}
		}

		// Token: 0x0600032E RID: 814 RVA: 0x0000EC38 File Offset: 0x0000CE38
		public static void RegisterLayout(Type type, string name = null, InputDeviceMatcher? matches = null)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (string.IsNullOrEmpty(name))
			{
				name = type.Name;
			}
			InputSystem.s_Manager.RegisterControlLayout(name, type);
			if (matches != null)
			{
				InputSystem.s_Manager.RegisterControlLayoutMatcher(name, matches.Value);
			}
		}

		// Token: 0x0600032F RID: 815 RVA: 0x0000EC90 File Offset: 0x0000CE90
		public static void RegisterLayout<T>(string name = null, InputDeviceMatcher? matches = null) where T : InputControl
		{
			InputSystem.RegisterLayout(typeof(T), name, matches);
		}

		// Token: 0x06000330 RID: 816 RVA: 0x0000ECA3 File Offset: 0x0000CEA3
		public static void RegisterLayout(string json, string name = null, InputDeviceMatcher? matches = null)
		{
			InputSystem.s_Manager.RegisterControlLayout(json, name, false);
			if (matches != null)
			{
				InputSystem.s_Manager.RegisterControlLayoutMatcher(name, matches.Value);
			}
		}

		// Token: 0x06000331 RID: 817 RVA: 0x0000ECCD File Offset: 0x0000CECD
		public static void RegisterLayoutOverride(string json, string name = null)
		{
			InputSystem.s_Manager.RegisterControlLayout(json, name, true);
		}

		// Token: 0x06000332 RID: 818 RVA: 0x0000ECDC File Offset: 0x0000CEDC
		public static void RegisterLayoutMatcher(string layoutName, InputDeviceMatcher matcher)
		{
			InputSystem.s_Manager.RegisterControlLayoutMatcher(layoutName, matcher);
		}

		// Token: 0x06000333 RID: 819 RVA: 0x0000ECEA File Offset: 0x0000CEEA
		public static void RegisterLayoutMatcher<TDevice>(InputDeviceMatcher matcher) where TDevice : InputDevice
		{
			InputSystem.s_Manager.RegisterControlLayoutMatcher(typeof(TDevice), matcher);
		}

		// Token: 0x06000334 RID: 820 RVA: 0x0000ED04 File Offset: 0x0000CF04
		public static void RegisterLayoutBuilder(Func<InputControlLayout> buildMethod, string name, string baseLayout = null, InputDeviceMatcher? matches = null)
		{
			if (buildMethod == null)
			{
				throw new ArgumentNullException("buildMethod");
			}
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentNullException("name");
			}
			InputSystem.s_Manager.RegisterControlLayoutBuilder(buildMethod, name, baseLayout);
			if (matches != null)
			{
				InputSystem.s_Manager.RegisterControlLayoutMatcher(name, matches.Value);
			}
		}

		// Token: 0x06000335 RID: 821 RVA: 0x0000ED5A File Offset: 0x0000CF5A
		public static void RegisterPrecompiledLayout<TDevice>(string metadata) where TDevice : InputDevice, new()
		{
			InputSystem.s_Manager.RegisterPrecompiledLayout<TDevice>(metadata);
		}

		// Token: 0x06000336 RID: 822 RVA: 0x0000ED67 File Offset: 0x0000CF67
		public static void RemoveLayout(string name)
		{
			InputSystem.s_Manager.RemoveControlLayout(name);
		}

		// Token: 0x06000337 RID: 823 RVA: 0x0000ED74 File Offset: 0x0000CF74
		public static string TryFindMatchingLayout(InputDeviceDescription deviceDescription)
		{
			return InputSystem.s_Manager.TryFindMatchingControlLayout(ref deviceDescription, 0);
		}

		// Token: 0x06000338 RID: 824 RVA: 0x0000ED88 File Offset: 0x0000CF88
		public static IEnumerable<string> ListLayouts()
		{
			return InputSystem.s_Manager.ListControlLayouts(null);
		}

		// Token: 0x06000339 RID: 825 RVA: 0x0000ED95 File Offset: 0x0000CF95
		public static IEnumerable<string> ListLayoutsBasedOn(string baseLayout)
		{
			if (string.IsNullOrEmpty(baseLayout))
			{
				throw new ArgumentNullException("baseLayout");
			}
			return InputSystem.s_Manager.ListControlLayouts(baseLayout);
		}

		// Token: 0x0600033A RID: 826 RVA: 0x0000EDB5 File Offset: 0x0000CFB5
		public static InputControlLayout LoadLayout(string name)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentNullException("name");
			}
			return InputSystem.s_Manager.TryLoadControlLayout(new InternedString(name));
		}

		// Token: 0x0600033B RID: 827 RVA: 0x0000EDDA File Offset: 0x0000CFDA
		public static InputControlLayout LoadLayout<TControl>() where TControl : InputControl
		{
			return InputSystem.s_Manager.TryLoadControlLayout(typeof(TControl));
		}

		// Token: 0x0600033C RID: 828 RVA: 0x0000EDF0 File Offset: 0x0000CFF0
		public static string GetNameOfBaseLayout(string layoutName)
		{
			if (string.IsNullOrEmpty(layoutName))
			{
				throw new ArgumentNullException("layoutName");
			}
			InternedString internedString = new InternedString(layoutName);
			InternedString internedString2;
			if (InputControlLayout.s_Layouts.baseLayoutTable.TryGetValue(internedString, out internedString2))
			{
				return internedString2;
			}
			return null;
		}

		// Token: 0x0600033D RID: 829 RVA: 0x0000EE34 File Offset: 0x0000D034
		public static bool IsFirstLayoutBasedOnSecond(string firstLayoutName, string secondLayoutName)
		{
			if (string.IsNullOrEmpty(firstLayoutName))
			{
				throw new ArgumentNullException("firstLayoutName");
			}
			if (string.IsNullOrEmpty(secondLayoutName))
			{
				throw new ArgumentNullException("secondLayoutName");
			}
			InternedString internedString = new InternedString(firstLayoutName);
			InternedString internedString2 = new InternedString(secondLayoutName);
			return internedString == internedString2 || InputControlLayout.s_Layouts.IsBasedOn(internedString2, internedString);
		}

		// Token: 0x0600033E RID: 830 RVA: 0x0000EE90 File Offset: 0x0000D090
		public static void RegisterProcessor(Type type, string name = null)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (string.IsNullOrEmpty(name))
			{
				name = type.Name;
				if (name.EndsWith("Processor"))
				{
					name = name.Substring(0, name.Length - "Processor".Length);
				}
			}
			Dictionary<InternedString, InputControlLayout.Collection.PrecompiledLayout> precompiledLayouts = InputSystem.s_Manager.m_Layouts.precompiledLayouts;
			foreach (InternedString internedString in new List<InternedString>(precompiledLayouts.Keys))
			{
				if (StringHelpers.CharacterSeparatedListsHaveAtLeastOneCommonElement(precompiledLayouts[internedString].metadata, name, ';'))
				{
					InputSystem.s_Manager.m_Layouts.precompiledLayouts.Remove(internedString);
				}
			}
			InputSystem.s_Manager.processors.AddTypeRegistration(name, type);
		}

		// Token: 0x0600033F RID: 831 RVA: 0x0000EF7C File Offset: 0x0000D17C
		public static void RegisterProcessor<T>(string name = null)
		{
			InputSystem.RegisterProcessor(typeof(T), name);
		}

		// Token: 0x06000340 RID: 832 RVA: 0x0000EF90 File Offset: 0x0000D190
		public static Type TryGetProcessor(string name)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentNullException("name");
			}
			return InputSystem.s_Manager.processors.LookupTypeRegistration(name);
		}

		// Token: 0x06000341 RID: 833 RVA: 0x0000EFC4 File Offset: 0x0000D1C4
		public static IEnumerable<string> ListProcessors()
		{
			return InputSystem.s_Manager.processors.names;
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x06000342 RID: 834 RVA: 0x0000EFE3 File Offset: 0x0000D1E3
		public static ReadOnlyArray<InputDevice> devices
		{
			get
			{
				return InputSystem.s_Manager.devices;
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x06000343 RID: 835 RVA: 0x0000EFEF File Offset: 0x0000D1EF
		public static ReadOnlyArray<InputDevice> disconnectedDevices
		{
			get
			{
				return new ReadOnlyArray<InputDevice>(InputSystem.s_Manager.m_DisconnectedDevices, 0, InputSystem.s_Manager.m_DisconnectedDevicesCount);
			}
		}

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x06000344 RID: 836 RVA: 0x0000F00C File Offset: 0x0000D20C
		// (remove) Token: 0x06000345 RID: 837 RVA: 0x0000F060 File Offset: 0x0000D260
		public static event Action<InputDevice, InputDeviceChange> onDeviceChange
		{
			add
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				InputManager inputManager = InputSystem.s_Manager;
				lock (inputManager)
				{
					InputSystem.s_Manager.onDeviceChange += value;
				}
			}
			remove
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				InputManager inputManager = InputSystem.s_Manager;
				lock (inputManager)
				{
					InputSystem.s_Manager.onDeviceChange -= value;
				}
			}
		}

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x06000346 RID: 838 RVA: 0x0000F0B4 File Offset: 0x0000D2B4
		// (remove) Token: 0x06000347 RID: 839 RVA: 0x0000F108 File Offset: 0x0000D308
		public static event InputDeviceCommandDelegate onDeviceCommand
		{
			add
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				InputManager inputManager = InputSystem.s_Manager;
				lock (inputManager)
				{
					InputSystem.s_Manager.onDeviceCommand += value;
				}
			}
			remove
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				InputManager inputManager = InputSystem.s_Manager;
				lock (inputManager)
				{
					InputSystem.s_Manager.onDeviceCommand -= value;
				}
			}
		}

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x06000348 RID: 840 RVA: 0x0000F15C File Offset: 0x0000D35C
		// (remove) Token: 0x06000349 RID: 841 RVA: 0x0000F1A0 File Offset: 0x0000D3A0
		public static event InputDeviceFindControlLayoutDelegate onFindLayoutForDevice
		{
			add
			{
				InputManager inputManager = InputSystem.s_Manager;
				lock (inputManager)
				{
					InputSystem.s_Manager.onFindControlLayoutForDevice += value;
				}
			}
			remove
			{
				InputManager inputManager = InputSystem.s_Manager;
				lock (inputManager)
				{
					InputSystem.s_Manager.onFindControlLayoutForDevice -= value;
				}
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x0600034A RID: 842 RVA: 0x0000F1E4 File Offset: 0x0000D3E4
		// (set) Token: 0x0600034B RID: 843 RVA: 0x0000F1F0 File Offset: 0x0000D3F0
		public static float pollingFrequency
		{
			get
			{
				return InputSystem.s_Manager.pollingFrequency;
			}
			set
			{
				InputSystem.s_Manager.pollingFrequency = value;
			}
		}

		// Token: 0x0600034C RID: 844 RVA: 0x0000F1FD File Offset: 0x0000D3FD
		public static InputDevice AddDevice(string layout, string name = null, string variants = null)
		{
			if (string.IsNullOrEmpty(layout))
			{
				throw new ArgumentNullException("layout");
			}
			return InputSystem.s_Manager.AddDevice(layout, name, new InternedString(variants));
		}

		// Token: 0x0600034D RID: 845 RVA: 0x0000F224 File Offset: 0x0000D424
		public static TDevice AddDevice<TDevice>(string name = null) where TDevice : InputDevice
		{
			InputDevice inputDevice = InputSystem.s_Manager.AddDevice(typeof(TDevice), name);
			TDevice tdevice = inputDevice as TDevice;
			if (tdevice == null)
			{
				if (inputDevice != null)
				{
					InputSystem.RemoveDevice(inputDevice);
				}
				throw new InvalidOperationException("Layout registered for type '" + typeof(TDevice).Name + "' did not produce a device of that type; layout probably has been overridden");
			}
			return tdevice;
		}

		// Token: 0x0600034E RID: 846 RVA: 0x0000F287 File Offset: 0x0000D487
		public static InputDevice AddDevice(InputDeviceDescription description)
		{
			if (description.empty)
			{
				throw new ArgumentException("Description must not be empty", "description");
			}
			return InputSystem.s_Manager.AddDevice(description);
		}

		// Token: 0x0600034F RID: 847 RVA: 0x0000F2AD File Offset: 0x0000D4AD
		public static void AddDevice(InputDevice device)
		{
			if (device == null)
			{
				throw new ArgumentNullException("device");
			}
			InputSystem.s_Manager.AddDevice(device);
		}

		// Token: 0x06000350 RID: 848 RVA: 0x0000F2C8 File Offset: 0x0000D4C8
		public static void RemoveDevice(InputDevice device)
		{
			InputSystem.s_Manager.RemoveDevice(device, false);
		}

		// Token: 0x06000351 RID: 849 RVA: 0x0000F2D6 File Offset: 0x0000D4D6
		public static void FlushDisconnectedDevices()
		{
			InputSystem.s_Manager.FlushDisconnectedDevices();
		}

		// Token: 0x06000352 RID: 850 RVA: 0x0000F2E2 File Offset: 0x0000D4E2
		public static InputDevice GetDevice(string nameOrLayout)
		{
			return InputSystem.s_Manager.TryGetDevice(nameOrLayout);
		}

		// Token: 0x06000353 RID: 851 RVA: 0x0000F2EF File Offset: 0x0000D4EF
		public static TDevice GetDevice<TDevice>() where TDevice : InputDevice
		{
			return (TDevice)((object)InputSystem.GetDevice(typeof(TDevice)));
		}

		// Token: 0x06000354 RID: 852 RVA: 0x0000F308 File Offset: 0x0000D508
		public static InputDevice GetDevice(Type type)
		{
			InputDevice inputDevice = null;
			double num = -1.0;
			foreach (InputDevice inputDevice2 in InputSystem.devices)
			{
				if (type.IsInstanceOfType(inputDevice2) && (inputDevice == null || inputDevice2.m_LastUpdateTimeInternal > num))
				{
					inputDevice = inputDevice2;
					num = inputDevice.m_LastUpdateTimeInternal;
				}
			}
			return inputDevice;
		}

		// Token: 0x06000355 RID: 853 RVA: 0x0000F388 File Offset: 0x0000D588
		public static TDevice GetDevice<TDevice>(InternedString usage) where TDevice : InputDevice
		{
			TDevice tdevice = default(TDevice);
			double num = -1.0;
			foreach (InputDevice inputDevice in InputSystem.devices)
			{
				TDevice tdevice2 = inputDevice as TDevice;
				if (tdevice2 != null && tdevice2.usages.Contains(usage) && (tdevice == null || tdevice2.m_LastUpdateTimeInternal > num))
				{
					tdevice = tdevice2;
					num = tdevice.m_LastUpdateTimeInternal;
				}
			}
			return tdevice;
		}

		// Token: 0x06000356 RID: 854 RVA: 0x0000F438 File Offset: 0x0000D638
		public static TDevice GetDevice<TDevice>(string usage) where TDevice : InputDevice
		{
			return InputSystem.GetDevice<TDevice>(new InternedString(usage));
		}

		// Token: 0x06000357 RID: 855 RVA: 0x0000F445 File Offset: 0x0000D645
		public static InputDevice GetDeviceById(int deviceId)
		{
			return InputSystem.s_Manager.TryGetDeviceById(deviceId);
		}

		// Token: 0x06000358 RID: 856 RVA: 0x0000F452 File Offset: 0x0000D652
		public static List<InputDeviceDescription> GetUnsupportedDevices()
		{
			List<InputDeviceDescription> list = new List<InputDeviceDescription>();
			InputSystem.GetUnsupportedDevices(list);
			return list;
		}

		// Token: 0x06000359 RID: 857 RVA: 0x0000F460 File Offset: 0x0000D660
		public static int GetUnsupportedDevices(List<InputDeviceDescription> descriptions)
		{
			return InputSystem.s_Manager.GetUnsupportedDevices(descriptions);
		}

		// Token: 0x0600035A RID: 858 RVA: 0x0000F46D File Offset: 0x0000D66D
		public static void EnableDevice(InputDevice device)
		{
			InputSystem.s_Manager.EnableOrDisableDevice(device, true, InputManager.DeviceDisableScope.Everywhere);
		}

		// Token: 0x0600035B RID: 859 RVA: 0x0000F47C File Offset: 0x0000D67C
		public static void DisableDevice(InputDevice device, bool keepSendingEvents = false)
		{
			InputSystem.s_Manager.EnableOrDisableDevice(device, false, keepSendingEvents ? InputManager.DeviceDisableScope.InFrontendOnly : InputManager.DeviceDisableScope.Everywhere);
		}

		// Token: 0x0600035C RID: 860 RVA: 0x0000F491 File Offset: 0x0000D691
		public static bool TrySyncDevice(InputDevice device)
		{
			if (device == null)
			{
				throw new ArgumentNullException("device");
			}
			if (!device.added)
			{
				throw new InvalidOperationException(string.Format("Device '{0}' has not been added", device));
			}
			return device.RequestSync();
		}

		// Token: 0x0600035D RID: 861 RVA: 0x0000F4C0 File Offset: 0x0000D6C0
		public static void ResetDevice(InputDevice device, bool alsoResetDontResetControls = false)
		{
			InputSystem.s_Manager.ResetDevice(device, alsoResetDontResetControls, null);
		}

		// Token: 0x0600035E RID: 862 RVA: 0x0000F4E2 File Offset: 0x0000D6E2
		[Obsolete("Use 'ResetDevice' instead.", false)]
		public static bool TryResetDevice(InputDevice device)
		{
			if (device == null)
			{
				throw new ArgumentNullException("device");
			}
			return device.RequestReset();
		}

		// Token: 0x0600035F RID: 863 RVA: 0x0000F4F8 File Offset: 0x0000D6F8
		public static void PauseHaptics()
		{
			ReadOnlyArray<InputDevice> devices = InputSystem.devices;
			int count = devices.Count;
			for (int i = 0; i < count; i++)
			{
				IHaptics haptics = devices[i] as IHaptics;
				if (haptics != null)
				{
					haptics.PauseHaptics();
				}
			}
		}

		// Token: 0x06000360 RID: 864 RVA: 0x0000F538 File Offset: 0x0000D738
		public static void ResumeHaptics()
		{
			ReadOnlyArray<InputDevice> devices = InputSystem.devices;
			int count = devices.Count;
			for (int i = 0; i < count; i++)
			{
				IHaptics haptics = devices[i] as IHaptics;
				if (haptics != null)
				{
					haptics.ResumeHaptics();
				}
			}
		}

		// Token: 0x06000361 RID: 865 RVA: 0x0000F578 File Offset: 0x0000D778
		public static void ResetHaptics()
		{
			ReadOnlyArray<InputDevice> devices = InputSystem.devices;
			int count = devices.Count;
			for (int i = 0; i < count; i++)
			{
				IHaptics haptics = devices[i] as IHaptics;
				if (haptics != null)
				{
					haptics.ResetHaptics();
				}
			}
		}

		// Token: 0x06000362 RID: 866 RVA: 0x0000F5B6 File Offset: 0x0000D7B6
		public static void SetDeviceUsage(InputDevice device, string usage)
		{
			InputSystem.SetDeviceUsage(device, new InternedString(usage));
		}

		// Token: 0x06000363 RID: 867 RVA: 0x0000F5C4 File Offset: 0x0000D7C4
		public static void SetDeviceUsage(InputDevice device, InternedString usage)
		{
			InputSystem.s_Manager.SetDeviceUsage(device, usage);
		}

		// Token: 0x06000364 RID: 868 RVA: 0x0000F5D2 File Offset: 0x0000D7D2
		public static void AddDeviceUsage(InputDevice device, string usage)
		{
			InputSystem.s_Manager.AddDeviceUsage(device, new InternedString(usage));
		}

		// Token: 0x06000365 RID: 869 RVA: 0x0000F5E5 File Offset: 0x0000D7E5
		public static void AddDeviceUsage(InputDevice device, InternedString usage)
		{
			InputSystem.s_Manager.AddDeviceUsage(device, usage);
		}

		// Token: 0x06000366 RID: 870 RVA: 0x0000F5F3 File Offset: 0x0000D7F3
		public static void RemoveDeviceUsage(InputDevice device, string usage)
		{
			InputSystem.s_Manager.RemoveDeviceUsage(device, new InternedString(usage));
		}

		// Token: 0x06000367 RID: 871 RVA: 0x0000F606 File Offset: 0x0000D806
		public static void RemoveDeviceUsage(InputDevice device, InternedString usage)
		{
			InputSystem.s_Manager.RemoveDeviceUsage(device, usage);
		}

		// Token: 0x06000368 RID: 872 RVA: 0x0000F614 File Offset: 0x0000D814
		public static InputControl FindControl(string path)
		{
			if (string.IsNullOrEmpty(path))
			{
				throw new ArgumentNullException("path");
			}
			ReadOnlyArray<InputDevice> devices = InputSystem.s_Manager.devices;
			int count = devices.Count;
			for (int i = 0; i < count; i++)
			{
				InputControl inputControl = InputControlPath.TryFindControl(devices[i], path, 0);
				if (inputControl != null)
				{
					return inputControl;
				}
			}
			return null;
		}

		// Token: 0x06000369 RID: 873 RVA: 0x0000F669 File Offset: 0x0000D869
		public static InputControlList<InputControl> FindControls(string path)
		{
			return InputSystem.FindControls<InputControl>(path);
		}

		// Token: 0x0600036A RID: 874 RVA: 0x0000F674 File Offset: 0x0000D874
		public static InputControlList<TControl> FindControls<TControl>(string path) where TControl : InputControl
		{
			InputControlList<TControl> inputControlList = default(InputControlList<TControl>);
			InputSystem.FindControls<TControl>(path, ref inputControlList);
			return inputControlList;
		}

		// Token: 0x0600036B RID: 875 RVA: 0x0000F693 File Offset: 0x0000D893
		public static int FindControls<TControl>(string path, ref InputControlList<TControl> controls) where TControl : InputControl
		{
			return InputSystem.s_Manager.GetControls<TControl>(path, ref controls);
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x0600036C RID: 876 RVA: 0x0000F6A1 File Offset: 0x0000D8A1
		internal static bool isProcessingEvents
		{
			get
			{
				return InputSystem.s_Manager.isProcessingEvents;
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x0600036D RID: 877 RVA: 0x0000F6B0 File Offset: 0x0000D8B0
		// (set) Token: 0x0600036E RID: 878 RVA: 0x0000F6C6 File Offset: 0x0000D8C6
		public static InputEventListener onEvent
		{
			get
			{
				return default(InputEventListener);
			}
			set
			{
			}
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x0600036F RID: 879 RVA: 0x0000F6C8 File Offset: 0x0000D8C8
		public static IObservable<InputControl> onAnyButtonPress
		{
			get
			{
				return from e in InputSystem.onEvent
					select e.GetFirstButtonPressOrNull(-1f, true) into c
					where c != null
					select c;
			}
		}

		// Token: 0x06000370 RID: 880 RVA: 0x0000F727 File Offset: 0x0000D927
		public static void QueueEvent(InputEventPtr eventPtr)
		{
			if (!eventPtr.valid)
			{
				throw new ArgumentException("Received a null event pointer", "eventPtr");
			}
			InputSystem.s_Manager.QueueEvent(eventPtr);
		}

		// Token: 0x06000371 RID: 881 RVA: 0x0000F74D File Offset: 0x0000D94D
		public static void QueueEvent<TEvent>(ref TEvent inputEvent) where TEvent : struct, IInputEventTypeInfo
		{
			InputSystem.s_Manager.QueueEvent<TEvent>(ref inputEvent);
		}

		// Token: 0x06000372 RID: 882 RVA: 0x0000F75C File Offset: 0x0000D95C
		public unsafe static void QueueStateEvent<TState>(InputDevice device, TState state, double time = -1.0) where TState : struct, IInputStateTypeInfo
		{
			if (device == null)
			{
				throw new ArgumentNullException("device");
			}
			if (device.m_DeviceIndex == -1)
			{
				throw new InvalidOperationException(string.Format("Cannot queue state event for device '{0}' because device has not been added to system", device));
			}
			uint num = (uint)UnsafeUtility.SizeOf<TState>();
			if (num > 512U)
			{
				throw new ArgumentException(string.Format("Size of '{0}' exceeds maximum supported state size of {1}", typeof(TState).Name, 512), "state");
			}
			long num2 = (long)UnsafeUtility.SizeOf<StateEvent>() + (long)((ulong)num) - 1L;
			if (time < 0.0)
			{
				time = InputRuntime.s_Instance.currentTime;
			}
			else
			{
				time += InputRuntime.s_CurrentTimeOffsetToRealtimeSinceStartup;
			}
			InputSystem.StateEventBuffer stateEventBuffer;
			stateEventBuffer.stateEvent = new StateEvent
			{
				baseEvent = new InputEvent(1398030676, (int)num2, device.deviceId, time),
				stateFormat = state.format
			};
			UnsafeUtility.MemCpy((void*)(&stateEventBuffer.stateEvent.stateData.FixedElementField), UnsafeUtility.AddressOf<TState>(ref state), (long)((ulong)num));
			InputSystem.s_Manager.QueueEvent<StateEvent>(ref stateEventBuffer.stateEvent);
		}

		// Token: 0x06000373 RID: 883 RVA: 0x0000F874 File Offset: 0x0000DA74
		public unsafe static void QueueDeltaStateEvent<TDelta>(InputControl control, TDelta delta, double time = -1.0) where TDelta : struct
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			if (control.stateBlock.bitOffset != 0U)
			{
				throw new InvalidOperationException(string.Format("Cannot send delta state events against bitfield controls: {0}", control));
			}
			InputDevice device = control.device;
			if (device.m_DeviceIndex == -1)
			{
				throw new InvalidOperationException(string.Format("Cannot queue state event for control '{0}' on device '{1}' because device has not been added to system", control, device));
			}
			if (time < 0.0)
			{
				time = InputRuntime.s_Instance.currentTime;
			}
			else
			{
				time += InputRuntime.s_CurrentTimeOffsetToRealtimeSinceStartup;
			}
			uint num = (uint)UnsafeUtility.SizeOf<TDelta>();
			if (num > 512U)
			{
				throw new ArgumentException(string.Format("Size of state delta '{0}' exceeds maximum supported state size of {1}", typeof(TDelta).Name, 512), "delta");
			}
			if (num != control.stateBlock.alignedSizeInBytes)
			{
				throw new ArgumentException(string.Format("Size {0} of delta state of type {1} provided for control '{2}' does not match size {3} of control", new object[]
				{
					num,
					typeof(TDelta).Name,
					control,
					control.stateBlock.alignedSizeInBytes
				}), "delta");
			}
			long num2 = (long)UnsafeUtility.SizeOf<DeltaStateEvent>() + (long)((ulong)num) - 1L;
			InputSystem.DeltaStateEventBuffer deltaStateEventBuffer;
			deltaStateEventBuffer.stateEvent = new DeltaStateEvent
			{
				baseEvent = new InputEvent(1145852993, (int)num2, device.deviceId, time),
				stateFormat = device.stateBlock.format,
				stateOffset = control.m_StateBlock.byteOffset - device.m_StateBlock.byteOffset
			};
			UnsafeUtility.MemCpy((void*)(&deltaStateEventBuffer.stateEvent.stateData.FixedElementField), UnsafeUtility.AddressOf<TDelta>(ref delta), (long)((ulong)num));
			InputSystem.s_Manager.QueueEvent<DeltaStateEvent>(ref deltaStateEventBuffer.stateEvent);
		}

		// Token: 0x06000374 RID: 884 RVA: 0x0000FA38 File Offset: 0x0000DC38
		public static void QueueConfigChangeEvent(InputDevice device, double time = -1.0)
		{
			if (device == null)
			{
				throw new ArgumentNullException("device");
			}
			if (device.deviceId == 0)
			{
				throw new InvalidOperationException("Device has not been added");
			}
			if (time < 0.0)
			{
				time = InputRuntime.s_Instance.currentTime;
			}
			else
			{
				time += InputRuntime.s_CurrentTimeOffsetToRealtimeSinceStartup;
			}
			DeviceConfigurationEvent deviceConfigurationEvent = DeviceConfigurationEvent.Create(device.deviceId, time);
			InputSystem.s_Manager.QueueEvent<DeviceConfigurationEvent>(ref deviceConfigurationEvent);
		}

		// Token: 0x06000375 RID: 885 RVA: 0x0000FAA4 File Offset: 0x0000DCA4
		public static void QueueTextEvent(InputDevice device, char character, double time = -1.0)
		{
			if (device == null)
			{
				throw new ArgumentNullException("device");
			}
			if (device.deviceId == 0)
			{
				throw new InvalidOperationException("Device has not been added");
			}
			if (time < 0.0)
			{
				time = InputRuntime.s_Instance.currentTime;
			}
			else
			{
				time += InputRuntime.s_CurrentTimeOffsetToRealtimeSinceStartup;
			}
			TextEvent textEvent = TextEvent.Create(device.deviceId, character, time);
			InputSystem.s_Manager.QueueEvent<TextEvent>(ref textEvent);
		}

		// Token: 0x06000376 RID: 886 RVA: 0x0000FB0F File Offset: 0x0000DD0F
		public static void Update()
		{
			InputSystem.s_Manager.Update();
		}

		// Token: 0x06000377 RID: 887 RVA: 0x0000FB1C File Offset: 0x0000DD1C
		internal static void Update(InputUpdateType updateType)
		{
			if (updateType != InputUpdateType.None && (InputSystem.s_Manager.updateMask & updateType) == InputUpdateType.None)
			{
				throw new InvalidOperationException(string.Format("'{0}' updates are not enabled; InputSystem.settings.updateMode is set to '{1}'", updateType, InputSystem.settings.updateMode));
			}
			InputSystem.s_Manager.Update(updateType);
		}

		// Token: 0x14000009 RID: 9
		// (add) Token: 0x06000378 RID: 888 RVA: 0x0000FB6C File Offset: 0x0000DD6C
		// (remove) Token: 0x06000379 RID: 889 RVA: 0x0000FBB0 File Offset: 0x0000DDB0
		public static event Action onBeforeUpdate
		{
			add
			{
				InputManager inputManager = InputSystem.s_Manager;
				lock (inputManager)
				{
					InputSystem.s_Manager.onBeforeUpdate += value;
				}
			}
			remove
			{
				InputManager inputManager = InputSystem.s_Manager;
				lock (inputManager)
				{
					InputSystem.s_Manager.onBeforeUpdate -= value;
				}
			}
		}

		// Token: 0x1400000A RID: 10
		// (add) Token: 0x0600037A RID: 890 RVA: 0x0000FBF4 File Offset: 0x0000DDF4
		// (remove) Token: 0x0600037B RID: 891 RVA: 0x0000FC38 File Offset: 0x0000DE38
		public static event Action onAfterUpdate
		{
			add
			{
				InputManager inputManager = InputSystem.s_Manager;
				lock (inputManager)
				{
					InputSystem.s_Manager.onAfterUpdate += value;
				}
			}
			remove
			{
				InputManager inputManager = InputSystem.s_Manager;
				lock (inputManager)
				{
					InputSystem.s_Manager.onAfterUpdate -= value;
				}
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x0600037C RID: 892 RVA: 0x0000FC7C File Offset: 0x0000DE7C
		// (set) Token: 0x0600037D RID: 893 RVA: 0x0000FC88 File Offset: 0x0000DE88
		public static InputSettings settings
		{
			get
			{
				return InputSystem.s_Manager.settings;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (InputSystem.s_Manager.m_Settings == value)
				{
					return;
				}
				InputSystem.s_Manager.settings = value;
			}
		}

		// Token: 0x1400000B RID: 11
		// (add) Token: 0x0600037E RID: 894 RVA: 0x0000FCBC File Offset: 0x0000DEBC
		// (remove) Token: 0x0600037F RID: 895 RVA: 0x0000FCC9 File Offset: 0x0000DEC9
		public static event Action onSettingsChange
		{
			add
			{
				InputSystem.s_Manager.onSettingsChange += value;
			}
			remove
			{
				InputSystem.s_Manager.onSettingsChange -= value;
			}
		}

		// Token: 0x06000380 RID: 896 RVA: 0x0000FCD6 File Offset: 0x0000DED6
		private static void EnableActions()
		{
			if (InputSystem.actions == null)
			{
				return;
			}
			InputSystem.actions.Enable();
		}

		// Token: 0x06000381 RID: 897 RVA: 0x0000FCF0 File Offset: 0x0000DEF0
		private static void DisableActions(bool triggerSetupChanged = false)
		{
			InputActionAsset actions = InputSystem.actions;
			if (actions == null)
			{
				return;
			}
			actions.Disable();
			if (triggerSetupChanged)
			{
				actions.OnSetupChanged();
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x06000382 RID: 898 RVA: 0x0000FD1C File Offset: 0x0000DF1C
		// (set) Token: 0x06000383 RID: 899 RVA: 0x0000FD2E File Offset: 0x0000DF2E
		public static InputActionAsset actions
		{
			get
			{
				InputManager inputManager = InputSystem.s_Manager;
				if (inputManager == null)
				{
					return null;
				}
				return inputManager.actions;
			}
			set
			{
				if (Application.isPlaying)
				{
					throw new Exception("Attempted to set property InputSystem.actions during Play-mode which is not supported. Assigning this property is only allowed in Edit-mode.");
				}
				if (InputSystem.s_Manager.actions == value)
				{
					return;
				}
				value != null;
				InputSystem.s_Manager.actions = value;
			}
		}

		// Token: 0x1400000C RID: 12
		// (add) Token: 0x06000384 RID: 900 RVA: 0x0000FD63 File Offset: 0x0000DF63
		// (remove) Token: 0x06000385 RID: 901 RVA: 0x0000FD70 File Offset: 0x0000DF70
		public static event Action onActionsChange
		{
			add
			{
				InputSystem.s_Manager.onActionsChange += value;
			}
			remove
			{
				InputSystem.s_Manager.onActionsChange -= value;
			}
		}

		// Token: 0x1400000D RID: 13
		// (add) Token: 0x06000386 RID: 902 RVA: 0x0000FD7D File Offset: 0x0000DF7D
		// (remove) Token: 0x06000387 RID: 903 RVA: 0x0000FD9D File Offset: 0x0000DF9D
		public static event Action<object, InputActionChange> onActionChange
		{
			add
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				InputActionState.s_GlobalState.onActionChange.AddCallback(value);
			}
			remove
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				InputActionState.s_GlobalState.onActionChange.RemoveCallback(value);
			}
		}

		// Token: 0x06000388 RID: 904 RVA: 0x0000FDC0 File Offset: 0x0000DFC0
		public static void RegisterInteraction(Type type, string name = null)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (string.IsNullOrEmpty(name))
			{
				name = type.Name;
				if (name.EndsWith("Interaction"))
				{
					name = name.Substring(0, name.Length - "Interaction".Length);
				}
			}
			InputSystem.s_Manager.interactions.AddTypeRegistration(name, type);
		}

		// Token: 0x06000389 RID: 905 RVA: 0x0000FE2C File Offset: 0x0000E02C
		public static void RegisterInteraction<T>(string name = null)
		{
			InputSystem.RegisterInteraction(typeof(T), name);
		}

		// Token: 0x0600038A RID: 906 RVA: 0x0000FE40 File Offset: 0x0000E040
		public static Type TryGetInteraction(string name)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentNullException("name");
			}
			return InputSystem.s_Manager.interactions.LookupTypeRegistration(name);
		}

		// Token: 0x0600038B RID: 907 RVA: 0x0000FE74 File Offset: 0x0000E074
		public static IEnumerable<string> ListInteractions()
		{
			return InputSystem.s_Manager.interactions.names;
		}

		// Token: 0x0600038C RID: 908 RVA: 0x0000FE94 File Offset: 0x0000E094
		public static void RegisterBindingComposite(Type type, string name)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (string.IsNullOrEmpty(name))
			{
				name = type.Name;
				if (name.EndsWith("Composite"))
				{
					name = name.Substring(0, name.Length - "Composite".Length);
				}
			}
			InputSystem.s_Manager.composites.AddTypeRegistration(name, type);
		}

		// Token: 0x0600038D RID: 909 RVA: 0x0000FF00 File Offset: 0x0000E100
		public static void RegisterBindingComposite<T>(string name = null)
		{
			InputSystem.RegisterBindingComposite(typeof(T), name);
		}

		// Token: 0x0600038E RID: 910 RVA: 0x0000FF14 File Offset: 0x0000E114
		public static Type TryGetBindingComposite(string name)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentNullException("name");
			}
			return InputSystem.s_Manager.composites.LookupTypeRegistration(name);
		}

		// Token: 0x0600038F RID: 911 RVA: 0x0000FF47 File Offset: 0x0000E147
		public static void DisableAllEnabledActions()
		{
			InputActionState.DisableAllActions();
		}

		// Token: 0x06000390 RID: 912 RVA: 0x0000FF4E File Offset: 0x0000E14E
		public static List<InputAction> ListEnabledActions()
		{
			List<InputAction> list = new List<InputAction>();
			InputSystem.ListEnabledActions(list);
			return list;
		}

		// Token: 0x06000391 RID: 913 RVA: 0x0000FF5C File Offset: 0x0000E15C
		public static int ListEnabledActions(List<InputAction> actions)
		{
			if (actions == null)
			{
				throw new ArgumentNullException("actions");
			}
			return InputActionState.FindAllEnabledActions(actions);
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x06000392 RID: 914 RVA: 0x0000FF72 File Offset: 0x0000E172
		public static InputRemoting remoting
		{
			get
			{
				return InputSystem.s_Remote;
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x06000393 RID: 915 RVA: 0x0000FF79 File Offset: 0x0000E179
		public static Version version
		{
			get
			{
				return new Version("1.19.0");
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x06000394 RID: 916 RVA: 0x0000FF85 File Offset: 0x0000E185
		// (set) Token: 0x06000395 RID: 917 RVA: 0x0000FF96 File Offset: 0x0000E196
		public static bool runInBackground
		{
			get
			{
				return InputSystem.s_Manager.m_Runtime.runInBackground;
			}
			set
			{
				InputSystem.s_Manager.m_Runtime.runInBackground = value;
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x06000396 RID: 918 RVA: 0x0000FFA8 File Offset: 0x0000E1A8
		internal static float scrollWheelDeltaPerTick
		{
			get
			{
				return InputRuntime.s_Instance.scrollWheelDeltaPerTick;
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x06000397 RID: 919 RVA: 0x0000FFB4 File Offset: 0x0000E1B4
		public static InputMetrics metrics
		{
			get
			{
				return InputSystem.s_Manager.metrics;
			}
		}

		// Token: 0x06000398 RID: 920 RVA: 0x0000FFC0 File Offset: 0x0000E1C0
		static InputSystem()
		{
			InputSystem.InitializeInPlayer(null, null);
		}

		// Token: 0x06000399 RID: 921 RVA: 0x0000FFD8 File Offset: 0x0000E1D8
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
		private static void RunInitializeInPlayer()
		{
			if (InputSystem.s_Manager == null)
			{
				InputSystem.InitializeInPlayer(null, null);
			}
		}

		// Token: 0x0600039A RID: 922 RVA: 0x0000FFE8 File Offset: 0x0000E1E8
		internal static void EnsureInitialized()
		{
		}

		// Token: 0x0600039B RID: 923 RVA: 0x0000FFEC File Offset: 0x0000E1EC
		private static void InitializeInPlayer(IInputRuntime runtime = null, InputSettings settings = null)
		{
			if (settings == null)
			{
				settings = Resources.FindObjectsOfTypeAll<InputSettings>().FirstOrDefault<InputSettings>() ?? ScriptableObject.CreateInstance<InputSettings>();
			}
			InputSystem.s_Manager = new InputManager();
			InputSystem.s_Manager.Initialize(runtime ?? NativeInputRuntime.instance, settings);
			InputSystem.PerformDefaultPluginInitialization();
			InputSystem.EnableActions();
		}

		// Token: 0x0600039C RID: 924 RVA: 0x00010040 File Offset: 0x0000E240
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		private static void RunInitialUpdate()
		{
			InputSystem.Update(InputUpdateType.None);
		}

		// Token: 0x0600039D RID: 925 RVA: 0x00010048 File Offset: 0x0000E248
		private static void PerformDefaultPluginInitialization()
		{
			UISupport.Initialize();
			XInputSupport.Initialize();
			DualShockSupport.Initialize();
			HIDSupport.Initialize();
			SwitchSupportHID.Initialize();
			XRSupport.Initialize();
		}

		// Token: 0x04000105 RID: 261
		internal const string kAssemblyVersion = "1.19.0";

		// Token: 0x04000106 RID: 262
		internal const string kDocUrl = "https://docs.unity3d.com/Packages/com.unity.inputsystem@1.19";

		// Token: 0x04000107 RID: 263
		private static readonly ProfilerMarker k_InputResetMarker = new ProfilerMarker("InputSystem.Reset");

		// Token: 0x04000108 RID: 264
		internal static InputManager s_Manager;

		// Token: 0x04000109 RID: 265
		internal static InputRemoting s_Remote;

		// Token: 0x0200018A RID: 394
		private struct StateEventBuffer
		{
			// Token: 0x040008A6 RID: 2214
			public StateEvent stateEvent;

			// Token: 0x040008A7 RID: 2215
			public const int kMaxSize = 512;

			// Token: 0x040008A8 RID: 2216
			[FixedBuffer(typeof(byte), 511)]
			public InputSystem.StateEventBuffer.<data>e__FixedBuffer data;

			// Token: 0x02000275 RID: 629
			[CompilerGenerated]
			[UnsafeValueType]
			[StructLayout(LayoutKind.Sequential, Size = 511)]
			public struct <data>e__FixedBuffer
			{
				// Token: 0x04000D08 RID: 3336
				public byte FixedElementField;
			}
		}

		// Token: 0x0200018B RID: 395
		private struct DeltaStateEventBuffer
		{
			// Token: 0x040008A9 RID: 2217
			public DeltaStateEvent stateEvent;

			// Token: 0x040008AA RID: 2218
			public const int kMaxSize = 512;

			// Token: 0x040008AB RID: 2219
			[FixedBuffer(typeof(byte), 511)]
			public InputSystem.DeltaStateEventBuffer.<data>e__FixedBuffer data;

			// Token: 0x02000276 RID: 630
			[CompilerGenerated]
			[UnsafeValueType]
			[StructLayout(LayoutKind.Sequential, Size = 511)]
			public struct <data>e__FixedBuffer
			{
				// Token: 0x04000D09 RID: 3337
				public byte FixedElementField;
			}
		}
	}
}
