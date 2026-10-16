using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ConsoleLib.Console;
using Cysharp.Text;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;
using XRL;
using XRL.UI;

// Token: 0x0200006F RID: 111
public class ControlManager : MonoBehaviour
{
	// Token: 0x060003C6 RID: 966 RVA: 0x0001867C File Offset: 0x0001687C
	public void LateUpdate()
	{
		if (ControlManager.layersToEnableAtEndOfFrame != null)
		{
			List<string> list = ControlManager.layersToEnableAtEndOfFrame;
			ControlManager.layersToEnableAtEndOfFrame = null;
			list.ForEach(delegate(string l)
			{
				ControlManager.EnableLayer(l);
			});
		}
		ControlManager.CommandFramePressed.Clear();
		CommandBindingManager.LateUpdate();
		ControlManager.NeedsQueueUpdate = true;
	}

	// Token: 0x060003C7 RID: 967 RVA: 0x000186D4 File Offset: 0x000168D4
	public static bool GetButtonReleasedThisFrame(string Action)
	{
		return Application.isFocused && Application.isPlaying && GameManager.Instance.player.GetButtonReleasedThisFrame(Action, false);
	}

	// Token: 0x060003C8 RID: 968 RVA: 0x000186F7 File Offset: 0x000168F7
	public static bool GetButtonPerformedThisFrame(string Action)
	{
		return Application.isFocused && Application.isPlaying && GameManager.Instance.player.GetButtonPerformedThisFrame(Action, false);
	}

	// Token: 0x060003C9 RID: 969 RVA: 0x0001871A File Offset: 0x0001691A
	public static bool GetButtonPressedThisFrame(string Action)
	{
		return Application.isFocused && Application.isPlaying && GameManager.Instance.player.GetButtonPressedThisFrame(Action, false);
	}

	// Token: 0x060003CA RID: 970 RVA: 0x0001873D File Offset: 0x0001693D
	public static bool GetButtonDown(string Action, bool skipTutorialCheck = false)
	{
		return Application.isFocused && Application.isPlaying && GameManager.Instance.player.GetButtonDown(Action, false, skipTutorialCheck);
	}

	// Token: 0x060003CB RID: 971 RVA: 0x00018761 File Offset: 0x00016961
	public static bool GetButtonDownRepeating(string Action)
	{
		return Application.isFocused && Application.isPlaying && GameManager.Instance.player.GetButtonDownRepeating(Action, false);
	}

	// Token: 0x060003CC RID: 972 RVA: 0x00018784 File Offset: 0x00016984
	public static bool GetButton(string Action, bool forceEnable = false)
	{
		return Application.isFocused && Application.isPlaying && GameManager.Instance.player.GetButton(Action, forceEnable);
	}

	// Token: 0x17000064 RID: 100
	// (get) Token: 0x060003CD RID: 973 RVA: 0x000187A7 File Offset: 0x000169A7
	public static bool LastInputFromMouse
	{
		get
		{
			ControlManager controlManager = ControlManager.instance;
			return controlManager != null && controlManager.lastInputIsMouse;
		}
	}

	// Token: 0x060003CE RID: 974 RVA: 0x000187B9 File Offset: 0x000169B9
	public static void Skip(int min)
	{
		if (ControlManager.SkipFrames < min)
		{
			ControlManager.SkipFrames = min;
		}
	}

	// Token: 0x060003CF RID: 975 RVA: 0x000187CC File Offset: 0x000169CC
	public static void WaitForKeyup(string id)
	{
		List<string> waitForKeyupList = ControlManager._WaitForKeyupList;
		lock (waitForKeyupList)
		{
			if (!ControlManager._WaitForKeyupList.Contains(id))
			{
				ControlManager._WaitForKeyupList.Add(id);
			}
		}
	}

	// Token: 0x060003D0 RID: 976 RVA: 0x00018820 File Offset: 0x00016A20
	public static void AddCommandFramePressed(string command)
	{
		ControlManager.CommandFramePressed.Add(command);
	}

	// Token: 0x060003D1 RID: 977 RVA: 0x00018830 File Offset: 0x00016A30
	public void Update()
	{
		if (ControlManager.NeedsQueueUpdate)
		{
			ControlManager.UpdateTheCommandQueue();
		}
		ControlManager.ControllerChangedThisLateUpdate = false;
		ControlManager.CommandFramePressed.Clear();
		bool flag = false;
		if (ControlManager.SkipFrames > 0 && !ControlManager.mappingMode)
		{
			ControlManager.SkipFrames--;
		}
		if (Application.isPlaying)
		{
			ControlManager.InputDeviceType activeControllerType = ControlManager.activeControllerType;
			ControlManager.InputDeviceType? inputDeviceType = this.lastControllerType;
			if (!((activeControllerType == inputDeviceType.GetValueOrDefault()) & (inputDeviceType != null)) || ControlManager.updateFont)
			{
				if (ControlManager.activeControllerType == ControlManager.InputDeviceType.Keyboard)
				{
					this.controllerFontType = ControlManager.ControllerFontType.Keyboard;
				}
				else
				{
					string option = Options.GetOption("OptionControllerFont", "");
					uint num = <PrivateImplementationDetails>.ComputeStringHash(option);
					ControlManager.ControllerFontType controllerFontType;
					if (num <= 2641926360U)
					{
						if (num != 300721392U)
						{
							if (num != 1040194900U)
							{
								if (num == 2641926360U)
								{
									if (option == "XBox")
									{
										controllerFontType = ControlManager.ControllerFontType.XBox;
										goto IL_0184;
									}
								}
							}
							else if (option == "PS")
							{
								controllerFontType = ControlManager.ControllerFontType.PS5;
								goto IL_0184;
							}
						}
						else if (option == "XBox Filled")
						{
							controllerFontType = ControlManager.ControllerFontType.XBoxFilled;
							goto IL_0184;
						}
					}
					else if (num <= 3656636817U)
					{
						if (num != 3099176591U)
						{
							if (num == 3656636817U)
							{
								if (option == "Switch")
								{
									controllerFontType = ControlManager.ControllerFontType.Switch;
									goto IL_0184;
								}
							}
						}
						else if (option == "KBM")
						{
							controllerFontType = ControlManager.ControllerFontType.ForceKBM;
							goto IL_0184;
						}
					}
					else if (num != 3954370348U)
					{
						if (num == 3968586723U)
						{
							if (option == "Switch Filled")
							{
								controllerFontType = ControlManager.ControllerFontType.SwitchFilled;
								goto IL_0184;
							}
						}
					}
					else if (option == "PS Filled")
					{
						controllerFontType = ControlManager.ControllerFontType.PS5Filled;
						goto IL_0184;
					}
					controllerFontType = this.lastControllerFont;
					IL_0184:
					this.controllerFontType = controllerFontType;
				}
				this.lastControllerType = new ControlManager.InputDeviceType?(ControlManager.activeControllerType);
				ControlManager.updateFont = false;
				flag = true;
			}
		}
		if (Application.isPlaying)
		{
			if (Input.anyKeyDown)
			{
				this.lastInputIsMouse = false;
			}
			if (Options.MouseInput && (Input.mousePosition != this.lastMousePosition || Input.GetMouseButton(1) || Input.GetMouseButton(2) || Input.GetMouseButton(3)))
			{
				this.lastMousePosition = Input.mousePosition;
				this.lastInputIsMouse = true;
			}
		}
		ControlManager.ControllerFontType controllerFontType2 = this.controllerFontType;
		ControlManager.ControllerFontType? controllerFontType3 = this.lastControllerFontType;
		if (!((controllerFontType2 == controllerFontType3.GetValueOrDefault()) & (controllerFontType3 != null)))
		{
			this.lastControllerFontType = new ControlManager.ControllerFontType?(this.controllerFontType);
			TMP_Settings.fallbackFontAssets.RemoveAll((TMP_FontAsset f) => this.controllerFonts.Any<ControlManager.ControllerFontSelection>((ControlManager.ControllerFontSelection cf) => cf.fontAsset == f) || f == this.keyboardGlyphFont);
			TMP_FontAsset fontAsset = this.controllerFonts.Find((ControlManager.ControllerFontSelection cf) => cf.controllerType == this.controllerFontType).fontAsset;
			if (fontAsset != null)
			{
				TMP_Settings.fallbackFontAssets.Add(fontAsset);
			}
			TMP_Settings.fallbackFontAssets.Add(this.keyboardGlyphFont);
			TextMeshProUGUI[] array = global::UnityEngine.Object.FindObjectsByType<TextMeshProUGUI>(FindObjectsSortMode.None);
			for (int i = 0; i < array.Length; i++)
			{
				array[i].ForceMeshUpdate(false, true);
			}
			flag = true;
		}
		if (flag)
		{
			ControlManager.ControllerChangedThisLateUpdate = true;
			foreach (ControlManager.IControllerChangedEvent controllerChangedEvent in global::UnityEngine.Object.FindObjectsByType(typeof(MonoBehaviour), FindObjectsSortMode.None).OfType<ControlManager.IControllerChangedEvent>())
			{
				MonoBehaviour monoBehaviour = controllerChangedEvent as MonoBehaviour;
				if (monoBehaviour == null || monoBehaviour.isActiveAndEnabled)
				{
					controllerChangedEvent.ControllerChanged();
				}
			}
		}
	}

	// Token: 0x17000065 RID: 101
	// (get) Token: 0x060003D2 RID: 978 RVA: 0x00018B60 File Offset: 0x00016D60
	public static GameManager gameManager
	{
		get
		{
			return GameManager.Instance;
		}
	}

	// Token: 0x060003D3 RID: 979 RVA: 0x00018B67 File Offset: 0x00016D67
	private void Awake()
	{
		ControlManager.instance = this;
	}

	// Token: 0x17000066 RID: 102
	// (get) Token: 0x060003D4 RID: 980 RVA: 0x00018B6F File Offset: 0x00016D6F
	public global::PlayerInputManager player
	{
		get
		{
			GameManager gameManager = GameManager.Instance;
			if (gameManager == null)
			{
				return null;
			}
			return gameManager.player;
		}
	}

	// Token: 0x060003D5 RID: 981 RVA: 0x00018B81 File Offset: 0x00016D81
	private void SetupControllerGlyphs(InputDevice device)
	{
	}

	// Token: 0x14000001 RID: 1
	// (add) Token: 0x060003D6 RID: 982 RVA: 0x00018B84 File Offset: 0x00016D84
	// (remove) Token: 0x060003D7 RID: 983 RVA: 0x00018BB8 File Offset: 0x00016DB8
	public static event Action<InputDevice> onActiveDeviceChanged;

	// Token: 0x060003D8 RID: 984 RVA: 0x00018BEC File Offset: 0x00016DEC
	private void setLastUsedDevice(InputDevice device)
	{
		if (device is Mouse)
		{
			return;
		}
		if (ControlManager.lastUsedDevice != device)
		{
			ControlManager.lastUsedDevice = device;
			if (ControlManager.lastUsedDevice == Gamepad.current)
			{
				ControlManager._activeControllerType = ControlManager.InputDeviceType.Gamepad;
				InputDevice inputDevice = ControlManager.lastUsedDevice;
				if (inputDevice != null && inputDevice.description.manufacturer.Contains("Sony"))
				{
					this.lastControllerFont = ControlManager.ControllerFontType.PS5;
				}
				else
				{
					this.lastControllerFont = ControlManager.ControllerFontType.XBox;
				}
				ControlManager.OverrideInputControlNames(ControlManager.lastUsedDevice);
			}
			else
			{
				ControlManager._activeControllerType = ControlManager.InputDeviceType.Keyboard;
			}
			Action<InputDevice> action = ControlManager.onActiveDeviceChanged;
			if (action == null)
			{
				return;
			}
			action(device);
		}
	}

	// Token: 0x060003D9 RID: 985 RVA: 0x00018C79 File Offset: 0x00016E79
	private static void OverrideInputControlNames(InputDevice device)
	{
	}

	// Token: 0x060003DA RID: 986 RVA: 0x00018C7B File Offset: 0x00016E7B
	private void OnAnyButtonPress(InputControl control)
	{
		ControlManager.lastControlPressed = control;
	}

	// Token: 0x060003DB RID: 987 RVA: 0x00018C84 File Offset: 0x00016E84
	private void OnInputSystemEvent(InputEventPtr eventPtr, InputDevice device)
	{
		if (ControlManager.lastUsedDevice == device)
		{
			return;
		}
		if (eventPtr.type == 1398030676)
		{
			using (InputControlExtensions.InputEventControlEnumerator enumerator = eventPtr.EnumerateChangedControls(device, 0.0001f).GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					InputControl inputControl = enumerator.Current;
					goto IL_005C;
				}
			}
			return;
		}
		IL_005C:
		this.setLastUsedDevice(device);
	}

	// Token: 0x060003DC RID: 988 RVA: 0x00018D04 File Offset: 0x00016F04
	private void OnInputSystemDeviceChange(InputDevice device, InputDeviceChange change)
	{
		if (ControlManager.lastUsedDevice == device)
		{
			return;
		}
		if (device != null && !string.IsNullOrEmpty(device.name) && device.name.Contains("XInputControllerWindows"))
		{
			ControlManager.SuspectSteamInput = true;
		}
		this.setLastUsedDevice(device);
	}

	// Token: 0x060003DD RID: 989 RVA: 0x00018D40 File Offset: 0x00016F40
	public void Init()
	{
		ControlManager.ControllerFontType controllerFontType = PlatformManager.GetFontType();
		string option = Options.GetOption("OptionControllerFont", "");
		ControlManager.ControllerFontType controllerFontType2;
		if (!(option == "XBox"))
		{
			if (!(option == "PS"))
			{
				if (!(option == "XBox Filled"))
				{
					if (!(option == "PS Filled"))
					{
						if (!(option == "KBM"))
						{
							controllerFontType2 = ControlManager.ControllerFontType.Default;
						}
						else
						{
							controllerFontType2 = ControlManager.ControllerFontType.ForceKBM;
						}
					}
					else
					{
						controllerFontType2 = ControlManager.ControllerFontType.PS5Filled;
					}
				}
				else
				{
					controllerFontType2 = ControlManager.ControllerFontType.XBoxFilled;
				}
			}
			else
			{
				controllerFontType2 = ControlManager.ControllerFontType.PS5;
			}
		}
		else
		{
			controllerFontType2 = ControlManager.ControllerFontType.XBox;
		}
		ControlManager.ControllerFontType controllerFontType3 = controllerFontType2;
		if (controllerFontType3 != ControlManager.ControllerFontType.Default)
		{
			controllerFontType = controllerFontType3;
		}
		if (controllerFontType != ControlManager.ControllerFontType.Default)
		{
			this.controllerFontType = controllerFontType;
			this.Update();
		}
		InputSystem.onAnyButtonPress.Call<InputControl>(new Action<InputControl>(this.OnAnyButtonPress));
		InputSystem.onEvent += new Action<InputEventPtr, InputDevice>(this.OnInputSystemEvent);
		InputSystem.onDeviceChange += this.OnInputSystemDeviceChange;
		try
		{
			if (Options.GetOption("OptionDisableSteamInputWarning", "") == "No")
			{
				foreach (InputDevice inputDevice in InputSystem.devices)
				{
					MetricsManager.LogInfo("Detected device: " + inputDevice.name);
					if (!string.IsNullOrEmpty(inputDevice.name) && inputDevice.name.Contains("XInputControllerWindows"))
					{
						ControlManager.SuspectSteamInput = true;
					}
				}
			}
		}
		catch
		{
		}
	}

	// Token: 0x060003DE RID: 990 RVA: 0x00018EC8 File Offset: 0x000170C8
	public static void EnableOnlyLayers(List<string> layersToEnable)
	{
		ControlManager.EnabledLayers.Clear();
		ControlManager.EnabledLayers.AddRange(layersToEnable);
		ControlManager.ResetInput(false, true);
		foreach (KeyValuePair<string, CommandBindingLayer> keyValuePair in CommandBindingManager.CommandBindingLayers)
		{
			if (!layersToEnable.Contains(keyValuePair.Key))
			{
				ControlManager.DisableLayer(keyValuePair.Key);
			}
		}
		foreach (string text in layersToEnable)
		{
			ControlManager.EnableLayer(text);
		}
	}

	// Token: 0x060003DF RID: 991 RVA: 0x00018F84 File Offset: 0x00017184
	public static void OnUpdate()
	{
	}

	// Token: 0x060003E0 RID: 992 RVA: 0x00018F86 File Offset: 0x00017186
	public static void DisableLayer(CommandBindingLayer map)
	{
		if (GameManager.IsOnUIContext())
		{
			map.Disable();
		}
	}

	// Token: 0x060003E1 RID: 993 RVA: 0x00018F95 File Offset: 0x00017195
	public static void EnableLayer(CommandBindingLayer map)
	{
		if (GameManager.IsOnUIContext())
		{
			map.Enable();
		}
	}

	// Token: 0x060003E2 RID: 994 RVA: 0x00018FA4 File Offset: 0x000171A4
	public static void DisableAllLayers()
	{
		foreach (KeyValuePair<string, CommandBindingLayer> keyValuePair in CommandBindingManager.CommandBindingLayers)
		{
			ControlManager.DisableLayer(keyValuePair.Value);
		}
	}

	// Token: 0x060003E3 RID: 995 RVA: 0x00018FFC File Offset: 0x000171FC
	public static void EnableLayer(string layer)
	{
		if (ControlManager.layersToEnableAtEndOfFrame != null)
		{
			ControlManager.layersToEnableAtEndOfFrame = null;
		}
		CommandBindingLayer commandBindingLayer;
		if (CommandBindingManager.CommandBindingLayers.TryGetValue(layer, out commandBindingLayer))
		{
			ControlManager.EnableLayer(commandBindingLayer);
		}
	}

	// Token: 0x060003E4 RID: 996 RVA: 0x0001902C File Offset: 0x0001722C
	public static bool IsLayerEnabled(string layer)
	{
		CommandBindingLayer commandBindingLayer;
		return CommandBindingManager.CommandBindingLayers.TryGetValue(layer, out commandBindingLayer) && commandBindingLayer.enabled;
	}

	// Token: 0x060003E5 RID: 997 RVA: 0x00019050 File Offset: 0x00017250
	public static void DisableLayer(string layer)
	{
		if (ControlManager.layersToEnableAtEndOfFrame != null)
		{
			MetricsManager.LogError("Trying to disable a layer in the same frame as a resetinput this might be a problem.");
			ControlManager.layersToEnableAtEndOfFrame = null;
		}
		CommandBindingLayer commandBindingLayer;
		if (CommandBindingManager.CommandBindingLayers.TryGetValue(layer, out commandBindingLayer))
		{
			ControlManager.DisableLayer(commandBindingLayer);
		}
	}

	// Token: 0x060003E6 RID: 998 RVA: 0x0001908C File Offset: 0x0001728C
	public static string mapRewiredIDToLegacyID(string commandId)
	{
		string text;
		if (ControlManager._mapRewiredIDToLegacyID.TryGetValue(commandId, out text))
		{
			return text;
		}
		return commandId;
	}

	// Token: 0x060003E7 RID: 999 RVA: 0x000190AC File Offset: 0x000172AC
	public static List<global::UnityEngine.KeyCode> GetHotkeySpread(List<string> layersToInclude = null)
	{
		if (layersToInclude == null)
		{
			layersToInclude = ControlManager.EnabledLayers;
		}
		List<global::UnityEngine.KeyCode> list = new List<global::UnityEngine.KeyCode>();
		for (int i = 97; i <= 122; i++)
		{
			if (!ControlManager.isKeyMapped((global::UnityEngine.KeyCode)i, layersToInclude))
			{
				list.Add((global::UnityEngine.KeyCode)i);
			}
		}
		for (int j = 48; j <= 57; j++)
		{
			if (!ControlManager.isKeyMapped((global::UnityEngine.KeyCode)j, layersToInclude))
			{
				list.Add((global::UnityEngine.KeyCode)j);
			}
		}
		return list;
	}

	// Token: 0x060003E8 RID: 1000 RVA: 0x00019108 File Offset: 0x00017308
	public static char GetHotkeyCharFor(string Name, IReadOnlyList<char> Hotkeys)
	{
		if (Name.IsNullOrEmpty())
		{
			return '\0';
		}
		bool flag = false;
		int i = 0;
		int length = Name.Length;
		int count = Hotkeys.Count;
		while (i < length)
		{
			char c = (flag ? char.ToUpper(Name[i]) : char.ToLower(Name[i]));
			if (char.IsLetter(c))
			{
				bool flag2 = false;
				for (int j = 0; j < count; j++)
				{
					if (Hotkeys[j] == c)
					{
						flag2 = true;
						break;
					}
				}
				if (!flag2)
				{
					return c;
				}
				if (!flag && i == length - 1)
				{
					flag = true;
					i = 0;
				}
			}
			i++;
		}
		return '\0';
	}

	// Token: 0x17000067 RID: 103
	// (get) Token: 0x060003E9 RID: 1001 RVA: 0x0001919C File Offset: 0x0001739C
	public static Dictionary<int, global::UnityEngine.KeyCode> consoleKeycodeToUnityKeycode
	{
		get
		{
			if (ControlManager._consoleKeycodeToUnityKeycode == null)
			{
				ConsoleLib.Console.Keyboard.InitKeymap();
				ControlManager._consoleKeycodeToUnityKeycode = new Dictionary<int, global::UnityEngine.KeyCode>();
				foreach (KeyValuePair<global::UnityEngine.KeyCode, Keys> keyValuePair in ConsoleLib.Console.Keyboard.Keymap)
				{
					if (!ControlManager._consoleKeycodeToUnityKeycode.ContainsKey((int)keyValuePair.Value))
					{
						ControlManager._consoleKeycodeToUnityKeycode.Add((int)keyValuePair.Value, keyValuePair.Key);
					}
				}
			}
			return ControlManager._consoleKeycodeToUnityKeycode;
		}
	}

	// Token: 0x17000068 RID: 104
	// (get) Token: 0x060003EA RID: 1002 RVA: 0x00019230 File Offset: 0x00017430
	public static Dictionary<global::UnityEngine.KeyCode, Keys> UnityKeycodeToConsoleKeycodeMap
	{
		get
		{
			if (ControlManager._UnityKeycodeToConsoleKeycodeMap == null)
			{
				ConsoleLib.Console.Keyboard.InitKeymap();
				ControlManager._UnityKeycodeToConsoleKeycodeMap = new Dictionary<global::UnityEngine.KeyCode, Keys>();
				foreach (KeyValuePair<global::UnityEngine.KeyCode, Keys> keyValuePair in ConsoleLib.Console.Keyboard.Keymap)
				{
					if (!ControlManager._UnityKeycodeToConsoleKeycodeMap.ContainsKey(keyValuePair.Key))
					{
						ControlManager._UnityKeycodeToConsoleKeycodeMap.Add(keyValuePair.Key, keyValuePair.Value);
					}
				}
			}
			return ControlManager._UnityKeycodeToConsoleKeycodeMap;
		}
	}

	// Token: 0x060003EB RID: 1003 RVA: 0x000192C4 File Offset: 0x000174C4
	public static ControlManager.LegacyKeyCode mapCommandToPrimaryLegacyKeycode(string id)
	{
		string text = ControlManager.mapRewiredIDToLegacyID(id);
		foreach (KeyValuePair<string, Dictionary<string, int>> keyValuePair in CommandBindingManager.CurrentMap.PrimaryMapCommandToKeyLayer)
		{
			int num;
			global::UnityEngine.KeyCode keyCode;
			if (keyValuePair.Value.TryGetValue(text, out num) && ControlManager.consoleKeycodeToUnityKeycode.TryGetValue(num & 65535, out keyCode))
			{
				ControlManager.LegacyKeyCode legacyKeyCode = default(ControlManager.LegacyKeyCode);
				legacyKeyCode.rawCode = num;
				legacyKeyCode.code = keyCode;
				legacyKeyCode.bCtrl = (num & 131072) > 0;
				legacyKeyCode.bShift = (num & 65536) > 0;
				legacyKeyCode.bAlt = (num & 262144) > 0;
				return legacyKeyCode;
			}
		}
		return new ControlManager.LegacyKeyCode
		{
			code = global::UnityEngine.KeyCode.None
		};
	}

	// Token: 0x060003EC RID: 1004 RVA: 0x000193B0 File Offset: 0x000175B0
	public static ControlManager.LegacyKeyCode mapCommandToSecondaryLegacyKeycode(string id)
	{
		string text = ControlManager.mapRewiredIDToLegacyID(id);
		foreach (KeyValuePair<string, Dictionary<string, int>> keyValuePair in CommandBindingManager.CurrentMap.SecondaryMapCommandToKeyLayer)
		{
			int num;
			global::UnityEngine.KeyCode keyCode;
			if (keyValuePair.Value.TryGetValue(text, out num) && ControlManager.consoleKeycodeToUnityKeycode.TryGetValue(num & 65535, out keyCode))
			{
				ControlManager.LegacyKeyCode legacyKeyCode = default(ControlManager.LegacyKeyCode);
				legacyKeyCode.rawCode = num;
				legacyKeyCode.code = keyCode;
				legacyKeyCode.bCtrl = (num & 131072) > 0;
				legacyKeyCode.bShift = (num & 65536) > 0;
				legacyKeyCode.bAlt = (num & 262144) > 0;
				return legacyKeyCode;
			}
		}
		return new ControlManager.LegacyKeyCode
		{
			rawCode = 0,
			code = global::UnityEngine.KeyCode.None
		};
	}

	// Token: 0x060003ED RID: 1005 RVA: 0x000194A4 File Offset: 0x000176A4
	public static bool isKeyMapped(global::UnityEngine.KeyCode keycode, bool bAlt, bool bControl, bool bShift)
	{
		ConsoleLib.Console.Keyboard.InitKeymap();
		return ConsoleLib.Console.Keyboard.Keymap.ContainsKey(keycode) && ControlManager.isKeyMapped(ConsoleLib.Console.Keyboard.Keymap[keycode] + (bAlt ? 262144 : 0) + (bControl ? 131072 : 0) + (bShift ? 65536 : 0), null);
	}

	// Token: 0x060003EE RID: 1006 RVA: 0x000194FA File Offset: 0x000176FA
	public static bool isKeyMapped(char c, List<string> layersToInclude = null)
	{
		if (char.IsUpper(c))
		{
			return ControlManager.isKeyMapped(Keys.Shift + (int)char.ToUpperInvariant(c), layersToInclude);
		}
		return ControlManager.isKeyMapped((Keys)char.ToUpperInvariant(c), layersToInclude);
	}

	// Token: 0x060003EF RID: 1007 RVA: 0x00019523 File Offset: 0x00017723
	public static bool isCommandMapped(string id)
	{
		return CommandBindingManager.CommandBindings[id].IsMapped();
	}

	// Token: 0x060003F0 RID: 1008 RVA: 0x00019535 File Offset: 0x00017735
	public static bool isKeyMapped(global::UnityEngine.KeyCode key, List<string> layersToInclude = null)
	{
		ConsoleLib.Console.Keyboard.InitKeymap();
		if (layersToInclude == null)
		{
			layersToInclude = ControlManager.EnabledLayers;
		}
		return ControlManager.isKeyMapped(ConsoleLib.Console.Keyboard.Keymap[key], layersToInclude);
	}

	// Token: 0x060003F1 RID: 1009 RVA: 0x00019558 File Offset: 0x00017758
	public static bool isKeyMapped(Keys key, List<string> layersToInclude = null)
	{
		try
		{
			return CommandBindingManager.DoesAnyLayerConsumeKeycode(key, layersToInclude);
		}
		catch (Exception ex)
		{
			MetricsManager.LogException("isKeyMapped", ex, "game_exception");
		}
		return false;
	}

	// Token: 0x17000069 RID: 105
	// (get) Token: 0x060003F2 RID: 1010 RVA: 0x00019598 File Offset: 0x00017798
	private static Dictionary<global::UnityEngine.KeyCode, KeyControl> MacOS13UnityKeypadKeys
	{
		get
		{
			if (global::UnityEngine.InputSystem.Keyboard.current == null)
			{
				return ControlManager._MacOS13UnityKeypadKeysDummy;
			}
			global::UnityEngine.InputSystem.Keyboard current = global::UnityEngine.InputSystem.Keyboard.current;
			if (((current != null) ? current.numpad0Key : null) == null)
			{
				return ControlManager._MacOS13UnityKeypadKeysDummy;
			}
			if (ControlManager._MacOS13UnityKeypadKeys == null)
			{
				ControlManager._MacOS13UnityKeypadKeys = new Dictionary<global::UnityEngine.KeyCode, KeyControl>
				{
					{
						global::UnityEngine.KeyCode.Keypad0,
						global::UnityEngine.InputSystem.Keyboard.current.numpad0Key
					},
					{
						global::UnityEngine.KeyCode.Keypad1,
						global::UnityEngine.InputSystem.Keyboard.current.numpad1Key
					},
					{
						global::UnityEngine.KeyCode.Keypad2,
						global::UnityEngine.InputSystem.Keyboard.current.numpad2Key
					},
					{
						global::UnityEngine.KeyCode.Keypad3,
						global::UnityEngine.InputSystem.Keyboard.current.numpad3Key
					},
					{
						global::UnityEngine.KeyCode.Keypad4,
						global::UnityEngine.InputSystem.Keyboard.current.numpad4Key
					},
					{
						global::UnityEngine.KeyCode.Keypad5,
						global::UnityEngine.InputSystem.Keyboard.current.numpad5Key
					},
					{
						global::UnityEngine.KeyCode.Keypad6,
						global::UnityEngine.InputSystem.Keyboard.current.numpad6Key
					},
					{
						global::UnityEngine.KeyCode.Keypad7,
						global::UnityEngine.InputSystem.Keyboard.current.numpad7Key
					},
					{
						global::UnityEngine.KeyCode.Keypad8,
						global::UnityEngine.InputSystem.Keyboard.current.numpad8Key
					},
					{
						global::UnityEngine.KeyCode.Keypad9,
						global::UnityEngine.InputSystem.Keyboard.current.numpad9Key
					},
					{
						global::UnityEngine.KeyCode.Alpha0,
						global::UnityEngine.InputSystem.Keyboard.current.digit0Key
					},
					{
						global::UnityEngine.KeyCode.Alpha1,
						global::UnityEngine.InputSystem.Keyboard.current.digit1Key
					},
					{
						global::UnityEngine.KeyCode.Alpha2,
						global::UnityEngine.InputSystem.Keyboard.current.digit2Key
					},
					{
						global::UnityEngine.KeyCode.Alpha3,
						global::UnityEngine.InputSystem.Keyboard.current.digit3Key
					},
					{
						global::UnityEngine.KeyCode.Alpha4,
						global::UnityEngine.InputSystem.Keyboard.current.digit4Key
					},
					{
						global::UnityEngine.KeyCode.Alpha5,
						global::UnityEngine.InputSystem.Keyboard.current.digit5Key
					},
					{
						global::UnityEngine.KeyCode.Alpha6,
						global::UnityEngine.InputSystem.Keyboard.current.digit6Key
					},
					{
						global::UnityEngine.KeyCode.Alpha7,
						global::UnityEngine.InputSystem.Keyboard.current.digit7Key
					},
					{
						global::UnityEngine.KeyCode.Alpha8,
						global::UnityEngine.InputSystem.Keyboard.current.digit8Key
					},
					{
						global::UnityEngine.KeyCode.Alpha9,
						global::UnityEngine.InputSystem.Keyboard.current.digit9Key
					},
					{
						global::UnityEngine.KeyCode.KeypadPeriod,
						global::UnityEngine.InputSystem.Keyboard.current.numpadPeriodKey
					},
					{
						global::UnityEngine.KeyCode.KeypadDivide,
						global::UnityEngine.InputSystem.Keyboard.current.numpadDivideKey
					},
					{
						global::UnityEngine.KeyCode.KeypadMultiply,
						global::UnityEngine.InputSystem.Keyboard.current.numpadMinusKey
					},
					{
						global::UnityEngine.KeyCode.KeypadPlus,
						global::UnityEngine.InputSystem.Keyboard.current.numpadPlusKey
					},
					{
						global::UnityEngine.KeyCode.KeypadEnter,
						global::UnityEngine.InputSystem.Keyboard.current.numpadEnterKey
					},
					{
						global::UnityEngine.KeyCode.KeypadEquals,
						global::UnityEngine.InputSystem.Keyboard.current.numpadEqualsKey
					}
				};
			}
			return ControlManager._MacOS13UnityKeypadKeys;
		}
	}

	// Token: 0x060003F3 RID: 1011 RVA: 0x000197E8 File Offset: 0x000179E8
	public static bool isKey(global::UnityEngine.KeyCode key)
	{
		return !ControlManager.mappingMode && (!ControlManager.currentContext.isInInput() || !ControlManager.keysIgnoredWhileInAnInputField.Any<global::UnityEngine.KeyCode>(new Func<global::UnityEngine.KeyCode, bool>(Input.GetKey))) && Input.GetKey(key);
	}

	// Token: 0x060003F4 RID: 1012 RVA: 0x00019824 File Offset: 0x00017A24
	public static bool isHotkeyDown(global::UnityEngine.KeyCode key)
	{
		return !ControlManager.mappingMode && key != global::UnityEngine.KeyCode.None && (!ControlManager.currentContext.isInInput() || !ControlManager.keysIgnoredWhileInAnInputField.Any<global::UnityEngine.KeyCode>(new Func<global::UnityEngine.KeyCode, bool>(Input.GetKey))) && !ControlManager.isKeyMapped(key, null) && Input.GetKeyDown(key);
	}

	// Token: 0x060003F5 RID: 1013 RVA: 0x0001987B File Offset: 0x00017A7B
	public static bool isKeyDown(global::UnityEngine.KeyCode key)
	{
		return !ControlManager.mappingMode && (!ControlManager.currentContext.isInInput() || !ControlManager.keysIgnoredWhileInAnInputField.Any<global::UnityEngine.KeyCode>(new Func<global::UnityEngine.KeyCode, bool>(Input.GetKey))) && Input.GetKeyDown(key);
	}

	// Token: 0x060003F6 RID: 1014 RVA: 0x000198B8 File Offset: 0x00017AB8
	public static bool isCharDown(char c)
	{
		if (Input.GetKey(global::UnityEngine.KeyCode.LeftAlt) || Input.GetKey(global::UnityEngine.KeyCode.RightAlt) || Input.GetKey(global::UnityEngine.KeyCode.LeftControl) || Input.GetKey(global::UnityEngine.KeyCode.RightControl))
		{
			return false;
		}
		if (ControlManager.mappingMode)
		{
			return false;
		}
		if (ControlManager.currentContext.isInInput())
		{
			for (int i = 0; i < ControlManager.keysIgnoredWhileInAnInputField.Count; i++)
			{
				if (Input.GetKey(ControlManager.keysIgnoredWhileInAnInputField[i]))
				{
					return false;
				}
			}
		}
		ConsoleLib.Console.Keyboard.InitKeymap();
		if (!char.IsNumber(c) && Input.inputString != null && Input.inputString.Contains(c))
		{
			return true;
		}
		bool flag = false;
		if (char.IsLetter(c) && char.IsUpper(c))
		{
			flag = true;
		}
		List<global::UnityEngine.KeyCode> list;
		if (ConsoleLib.Console.Keyboard.reverselccharmap.TryGetValue(c, out list))
		{
			for (int j = 0; j < list.Count; j++)
			{
				if (ControlManager.isKeyDown(list[j]))
				{
					return flag || (!Input.GetKey(global::UnityEngine.KeyCode.LeftShift) && !Input.GetKey(global::UnityEngine.KeyCode.RightShift));
				}
			}
			return false;
		}
		if (ConsoleLib.Console.Keyboard.reverseuccharmap.TryGetValue(c, out list))
		{
			for (int k = 0; k < list.Count; k++)
			{
				if (ControlManager.isKeyDown(list[k]))
				{
					return !flag || Input.GetKey(global::UnityEngine.KeyCode.LeftShift) || Input.GetKey(global::UnityEngine.KeyCode.RightShift);
				}
			}
			return false;
		}
		return false;
	}

	// Token: 0x060003F7 RID: 1015 RVA: 0x00019A18 File Offset: 0x00017C18
	public static void ResetBindingCaches()
	{
		Dictionary<string, string> dictionary = ControlManager.gamepadCommandInputsWithGlyphs;
		if (dictionary != null)
		{
			dictionary.Clear();
		}
		Dictionary<string, string> dictionary2 = ControlManager.gamepadCommandInputsWithoutGlyphs;
		if (dictionary2 != null)
		{
			dictionary2.Clear();
		}
		Dictionary<string, string> dictionary3 = ControlManager.keyboardCommandInputsWithGlyphs;
		if (dictionary3 != null)
		{
			dictionary3.Clear();
		}
		Dictionary<string, string> dictionary4 = ControlManager.keyboardCommandInputsWithoutGlyphs;
		if (dictionary4 != null)
		{
			dictionary4.Clear();
		}
		if (CommandBindingManager.CommandBindings != null)
		{
			foreach (KeyValuePair<string, CommandBinding> keyValuePair in CommandBindingManager.CommandBindings)
			{
				Dictionary<string, string> dictionary5 = ControlManager.keyboardCommandInputsWithGlyphs;
				if (dictionary5 != null)
				{
					dictionary5.Add(keyValuePair.Key, ControlManager.getCommandInputDescription(keyValuePair.Key, true, false, ControlManager.InputDeviceType.Keyboard, false));
				}
				Dictionary<string, string> dictionary6 = ControlManager.keyboardCommandInputsWithoutGlyphs;
				if (dictionary6 != null)
				{
					dictionary6.Add(keyValuePair.Key, ControlManager.getCommandInputDescription(keyValuePair.Key, false, false, ControlManager.InputDeviceType.Keyboard, false));
				}
				Dictionary<string, string> dictionary7 = ControlManager.gamepadCommandInputsWithGlyphs;
				if (dictionary7 != null)
				{
					dictionary7.Add(keyValuePair.Key, ControlManager.getCommandInputDescription(keyValuePair.Key, true, false, ControlManager.InputDeviceType.Gamepad, false));
				}
				Dictionary<string, string> dictionary8 = ControlManager.gamepadCommandInputsWithoutGlyphs;
				if (dictionary8 != null)
				{
					dictionary8.Add(keyValuePair.Key, ControlManager.getCommandInputDescription(keyValuePair.Key, false, false, ControlManager.InputDeviceType.Gamepad, false));
				}
			}
		}
	}

	// Token: 0x060003F8 RID: 1016 RVA: 0x00019B50 File Offset: 0x00017D50
	public static string getCommandInputFormatted(string id, bool mapGlyphs = true, bool allBinds = false)
	{
		string commandInputDescription = ControlManager.getCommandInputDescription(id, mapGlyphs, false, ControlManager.InputDeviceType.Unknown, allBinds);
		if (string.IsNullOrEmpty(commandInputDescription))
		{
			return "";
		}
		return "{{hotkey|" + commandInputDescription + "}}";
	}

	// Token: 0x060003F9 RID: 1017 RVA: 0x00019B88 File Offset: 0x00017D88
	public static string getCommandInputDescription(string id, bool mapGlyphs = true, bool allowAlt = false, ControlManager.InputDeviceType deviceType = ControlManager.InputDeviceType.Unknown, bool allBinds = false)
	{
		if (deviceType == ControlManager.InputDeviceType.Unknown)
		{
			deviceType = ControlManager.activeControllerType;
			if (ControlManager.instance.controllerFontType == ControlManager.ControllerFontType.ForceKBM)
			{
				deviceType = ControlManager.InputDeviceType.Keyboard;
			}
		}
		if (mapGlyphs && !Options.ModernUI)
		{
			mapGlyphs = false;
		}
		if (id == "NavigationXYAxis")
		{
			if (deviceType != ControlManager.InputDeviceType.Gamepad)
			{
				return "\ue80a";
			}
			return "\ue90a";
		}
		else
		{
			if (!allBinds)
			{
				if (mapGlyphs)
				{
					if (deviceType == ControlManager.InputDeviceType.Gamepad)
					{
						if (ControlManager.gamepadCommandInputsWithGlyphs.ContainsKey(id))
						{
							return ControlManager.gamepadCommandInputsWithGlyphs[id];
						}
					}
					else if (ControlManager.keyboardCommandInputsWithGlyphs.ContainsKey(id))
					{
						return ControlManager.keyboardCommandInputsWithGlyphs[id];
					}
				}
				else if (deviceType == ControlManager.InputDeviceType.Gamepad)
				{
					if (ControlManager.gamepadCommandInputsWithoutGlyphs.ContainsKey(id))
					{
						return ControlManager.gamepadCommandInputsWithoutGlyphs[id];
					}
				}
				else if (ControlManager.keyboardCommandInputsWithoutGlyphs.ContainsKey(id))
				{
					return ControlManager.keyboardCommandInputsWithoutGlyphs[id];
				}
			}
			if (CommandBindingManager.CommandBindings == null)
			{
				return "";
			}
			if (!CommandBindingManager.CommandBindings.ContainsKey(id))
			{
				return "";
			}
			if (!GameManager.IsOnUIContext())
			{
				return "NEEDUICONTEXT";
			}
			if (CommandBindingManager.CommandBindings.ContainsKey(id) && CommandBindingManager.CommandBindings[id].IsMapped())
			{
				if (allBinds)
				{
					using (Utf16ValueStringBuilder utf16ValueStringBuilder = ZString.CreateStringBuilder())
					{
						foreach (string text in CommandBindingManager.GetCommandBindings(id, (deviceType == ControlManager.InputDeviceType.Gamepad) ? ControlManager.InputDeviceType.Gamepad : ControlManager.InputDeviceType.Keyboard, true))
						{
							string text2;
							if (mapGlyphs)
							{
								text2 = ControlManager.ConvertBindingTextToGlyphs(text ?? "", deviceType);
							}
							else
							{
								text2 = text ?? "";
							}
							if (!string.IsNullOrEmpty(text2))
							{
								if (utf16ValueStringBuilder.Length > 0)
								{
									utf16ValueStringBuilder.Append(" {{y|or}} ");
								}
								utf16ValueStringBuilder.Append(text2);
							}
						}
						if (utf16ValueStringBuilder.Length == 0)
						{
							return "{{K|<nothing bound to this command>}}";
						}
						return utf16ValueStringBuilder.ToString();
					}
				}
				if (mapGlyphs)
				{
					string text3 = CommandBindingManager.GetCommandBindings(id, (deviceType == ControlManager.InputDeviceType.Gamepad) ? ControlManager.InputDeviceType.Gamepad : ControlManager.InputDeviceType.Keyboard, true).FirstOrDefault<string>() ?? "";
					string text4 = ControlManager.ConvertBindingTextToGlyphs(text3, deviceType);
					if (text3 != text4)
					{
						return text4;
					}
				}
				return CommandBindingManager.GetCommandBindings(id, (deviceType == ControlManager.InputDeviceType.Gamepad) ? ControlManager.InputDeviceType.Gamepad : ControlManager.InputDeviceType.Keyboard, false).FirstOrDefault<string>() ?? "";
			}
			if (id == "NavigationXYAxis")
			{
				return "\ue90a";
			}
			if (id == "Page Up")
			{
				return "PgUp";
			}
			if (id == "Page Left")
			{
				return "Home";
			}
			if (id == "Page Down")
			{
				return "PgDown";
			}
			if (id == "Page Right")
			{
				return "End";
			}
			if (id == "U Positive")
			{
				return "Insert";
			}
			if (id == "U Negative")
			{
				return "Delete";
			}
			if (id == "V Positive")
			{
				return "KeyPad+";
			}
			if (id == "V Negative")
			{
				return "KeyPad-";
			}
			string text5 = ConsoleLib.Console.Keyboard.MetaToString(CommandBindingManager.GetKeyFromCommand(id));
			if (!(text5 == "None"))
			{
				return ControlManager.ConvertModifierGlyphs(text5);
			}
			if (id == "Cancel")
			{
				return "Esc";
			}
			if (id == "Accept")
			{
				return "Space";
			}
			return null;
		}
	}

	// Token: 0x060003FA RID: 1018 RVA: 0x00019EC4 File Offset: 0x000180C4
	private static void enqueueCommand(ControlManager.FrameCommand cmd)
	{
		if (cmd != null)
		{
			ControlManager.commandPool.Enqueue(cmd);
		}
	}

	// Token: 0x060003FB RID: 1019 RVA: 0x00019ED4 File Offset: 0x000180D4
	public static void ConsumeAllInput()
	{
		if (ControlManager.FrameDownCommand != null)
		{
			ControlManager.enqueueCommand(ControlManager.FrameDownCommand);
		}
		ControlManager.FrameDownCommand = null;
		Queue<ControlManager.FrameCommand> commandQueue = ControlManager.CommandQueue;
		if (commandQueue == null)
		{
			return;
		}
		commandQueue.Clear();
	}

	// Token: 0x060003FC RID: 1020 RVA: 0x00019EFC File Offset: 0x000180FC
	public static void ConsumeCurrentInput()
	{
		if (ControlManager.FrameDownCommand != null)
		{
			ControlManager.enqueueCommand(ControlManager.FrameDownCommand);
		}
		ControlManager.FrameDownCommand = null;
	}

	// Token: 0x060003FD RID: 1021 RVA: 0x00019F18 File Offset: 0x00018118
	public static void ConsumeAllInput(string cmd)
	{
		foreach (ControlManager.FrameCommand frameCommand in ControlManager.CommandQueue)
		{
			if (frameCommand.match(cmd) > 0)
			{
				frameCommand.ignore = true;
			}
		}
	}

	// Token: 0x060003FE RID: 1022 RVA: 0x00019F74 File Offset: 0x00018174
	public static void ResetInput(bool disableLayers = false, bool LeaveMovementEvents = true)
	{
		ControlManager.SkipFrames = 2;
		ConsoleLib.Console.Keyboard.ClearInput(LeaveMovementEvents, false);
		ControlManager.StickN = 0;
		ControlManager.StickS = 0;
		ControlManager.StickE = 0;
		ControlManager.StickW = 0;
		ControlManager.PauseStickNTillReleased = true;
		ControlManager.PauseStickSTillReleased = true;
		ControlManager.PauseStickETillReleased = true;
		ControlManager.PauseStickWTillReleased = true;
		if (GameManager.IsOnUIContext())
		{
			if (disableLayers && ControlManager.layersToEnableAtEndOfFrame == null && CommandBindingManager.CommandBindingLayers != null)
			{
				ControlManager.layersToEnableAtEndOfFrame = (from kv in CommandBindingManager.CommandBindingLayers
					where kv.Value.enabled
					select kv.Key).ToList<string>();
				ControlManager.DisableAllLayers();
			}
			Input.ResetInputAxes();
		}
	}

	// Token: 0x060003FF RID: 1023 RVA: 0x0001A038 File Offset: 0x00018238
	public static bool GetLegacyKeyDown(global::UnityEngine.KeyCode button, bool repeat = false)
	{
		if (ControlManager.mappingMode)
		{
			return false;
		}
		if (GameManager.IsOnUIContext() && !Application.isPlaying)
		{
			return false;
		}
		if (button == global::UnityEngine.KeyCode.None)
		{
			return false;
		}
		if (ControlManager.isKeyDown(button))
		{
			ControlManager.delayTimers.Set(button, ControlManager.delaytime);
			ControlManager.repeatTimers.Remove(button);
			return true;
		}
		if (!repeat)
		{
			return false;
		}
		if (ControlManager.isKey(button))
		{
			if (!ControlManager.delayTimers.ContainsKey(button))
			{
				ControlManager.delayTimers.Add(button, ControlManager.delaytime);
			}
			else if (ControlManager.delayTimers[button] <= 0f)
			{
				if (!ControlManager.repeatTimers.ContainsKey(button))
				{
					ControlManager.repeatTimers.Add(button, ControlManager.repeattime);
				}
				else
				{
					Dictionary<global::UnityEngine.KeyCode, float> dictionary = ControlManager.repeatTimers;
					dictionary[button] -= Time.deltaTime;
					if (ControlManager.repeatTimers[button] <= 0f)
					{
						ControlManager.repeatTimers[button] = ControlManager.repeattime;
						return true;
					}
				}
			}
			else
			{
				Dictionary<global::UnityEngine.KeyCode, float> dictionary = ControlManager.delayTimers;
				dictionary[button] -= Time.deltaTime;
			}
		}
		else
		{
			ControlManager.delayTimers.Remove(button);
			ControlManager.repeatTimers.Remove(button);
		}
		return false;
	}

	// Token: 0x06000400 RID: 1024 RVA: 0x0001A164 File Offset: 0x00018364
	public static bool GetLegacyKeyCodeDown(ControlManager.LegacyKeyCode button, bool repeat = false)
	{
		if (ControlManager.mappingMode)
		{
			return false;
		}
		if (GameManager.IsOnUIContext() && !Application.isPlaying)
		{
			return false;
		}
		if (button.code == global::UnityEngine.KeyCode.None)
		{
			return false;
		}
		if (ControlManager.isKeyDown(button.code) && (!button.bCtrl || ControlManager.isKey(global::UnityEngine.KeyCode.LeftControl) || ControlManager.isKey(global::UnityEngine.KeyCode.RightControl)) && (!button.bAlt || ControlManager.isKey(global::UnityEngine.KeyCode.LeftShift) || ControlManager.isKey(global::UnityEngine.KeyCode.RightShift)) && (!button.bShift || ControlManager.isKey(global::UnityEngine.KeyCode.LeftAlt) || ControlManager.isKey(global::UnityEngine.KeyCode.RightAlt)))
		{
			ControlManager.delayTimers2.Set(button.rawCode, ControlManager.delaytime);
			ControlManager.repeatTimers2.Remove(button.rawCode);
			return true;
		}
		if (!repeat)
		{
			return false;
		}
		if (ControlManager.isKey(button.code) && (!button.bCtrl || ControlManager.isKey(global::UnityEngine.KeyCode.LeftControl) || ControlManager.isKey(global::UnityEngine.KeyCode.RightControl)) && (!button.bAlt || ControlManager.isKey(global::UnityEngine.KeyCode.LeftShift) || ControlManager.isKey(global::UnityEngine.KeyCode.RightShift)) && (!button.bShift || ControlManager.isKey(global::UnityEngine.KeyCode.LeftAlt) || ControlManager.isKey(global::UnityEngine.KeyCode.RightAlt)))
		{
			if (!ControlManager.delayTimers2.ContainsKey(button.rawCode))
			{
				ControlManager.delayTimers2.Add(button.rawCode, ControlManager.delaytime);
			}
			else if (ControlManager.delayTimers2[button.rawCode] <= 0f)
			{
				if (!ControlManager.repeatTimers2.ContainsKey(button.rawCode))
				{
					ControlManager.repeatTimers2.Add(button.rawCode, ControlManager.repeattime);
				}
				else
				{
					Dictionary<int, float> dictionary = ControlManager.repeatTimers2;
					int num = button.rawCode;
					dictionary[num] -= Time.deltaTime;
					if (ControlManager.repeatTimers2[button.rawCode] <= 0f)
					{
						ControlManager.repeatTimers2[button.rawCode] = ControlManager.repeattime;
						return true;
					}
				}
			}
			else
			{
				Dictionary<int, float> dictionary = ControlManager.delayTimers2;
				int num = button.rawCode;
				dictionary[num] -= Time.deltaTime;
			}
		}
		else
		{
			ControlManager.delayTimers2.Remove(button.rawCode);
			ControlManager.repeatTimers2.Remove(button.rawCode);
		}
		return false;
	}

	// Token: 0x1700006A RID: 106
	// (get) Token: 0x06000401 RID: 1025 RVA: 0x0001A3B0 File Offset: 0x000185B0
	public static ControlManager.InputDeviceType activeControllerType
	{
		get
		{
			PlatformControlType controlType = PlatformManager.GetControlType();
			if (controlType == PlatformControlType.Deck || controlType == PlatformControlType.Controller)
			{
				return ControlManager.InputDeviceType.Gamepad;
			}
			return ControlManager._activeControllerType;
		}
	}

	// Token: 0x06000402 RID: 1026 RVA: 0x0001A3D4 File Offset: 0x000185D4
	public static bool isCommandPressed(string id)
	{
		if (ControlManager.NeedsQueueUpdate)
		{
			ControlManager.UpdateTheCommandQueue();
		}
		CommandBinding commandBinding;
		return !ControlManager.mappingMode && ControlManager.SkipFrames <= 0 && (SynchronizationContext.Current != The.UiContext || Application.isPlaying) && CommandBindingManager.CommandBindings.TryGetValue(id, out commandBinding) && commandBinding.IsPressed(false);
	}

	// Token: 0x06000403 RID: 1027 RVA: 0x0001A430 File Offset: 0x00018630
	public static bool isCommandReleasedThisFrame(string id)
	{
		if (ControlManager.NeedsQueueUpdate)
		{
			ControlManager.UpdateTheCommandQueue();
		}
		CommandBinding commandBinding;
		return !ControlManager.mappingMode && ControlManager.SkipFrames <= 0 && (SynchronizationContext.Current != The.UiContext || Application.isPlaying) && CommandBindingManager.CommandBindings.TryGetValue(id, out commandBinding) && commandBinding.WasReleasedThisFrame(false);
	}

	// Token: 0x06000404 RID: 1028 RVA: 0x0001A48C File Offset: 0x0001868C
	public static bool isCommandPerformedThisFrame(string id)
	{
		if (ControlManager.NeedsQueueUpdate)
		{
			ControlManager.UpdateTheCommandQueue();
		}
		CommandBinding commandBinding;
		return !ControlManager.mappingMode && ControlManager.SkipFrames <= 0 && (SynchronizationContext.Current != The.UiContext || Application.isPlaying) && CommandBindingManager.CommandBindings.TryGetValue(id, out commandBinding) && commandBinding.WasPerformedThisFrame(false);
	}

	// Token: 0x06000405 RID: 1029 RVA: 0x0001A4E8 File Offset: 0x000186E8
	public static bool isCommandPressedThisFrame(string id)
	{
		if (ControlManager.NeedsQueueUpdate)
		{
			ControlManager.UpdateTheCommandQueue();
		}
		CommandBinding commandBinding;
		return !ControlManager.mappingMode && ControlManager.SkipFrames <= 0 && (SynchronizationContext.Current != The.UiContext || Application.isPlaying) && CommandBindingManager.CommandBindings.TryGetValue(id, out commandBinding) && commandBinding.WasPressedThisFrame(false);
	}

	// Token: 0x06000406 RID: 1030 RVA: 0x0001A541 File Offset: 0x00018741
	public static bool isCommandDown(string id, bool repeat = true, bool ignoreSkipframes = false, bool skipTutorialCheck = false)
	{
		return ControlManager.isCommandDownValue(id, repeat, ignoreSkipframes, skipTutorialCheck) > 0;
	}

	// Token: 0x06000407 RID: 1031 RVA: 0x0001A550 File Offset: 0x00018750
	public static bool isConsoleCommandDown(string id)
	{
		if (id == "Accept")
		{
			return ConsoleLib.Console.Keyboard.Char == 32;
		}
		if (id == "Cancel")
		{
			return ConsoleLib.Console.Keyboard.Char == 3;
		}
		if (id == "Page Left")
		{
			return ConsoleLib.Console.Keyboard.Char == 103;
		}
		if (id == "Page Right")
		{
			return ConsoleLib.Console.Keyboard.Char == 105;
		}
		if (id == "Page Up")
		{
			return ConsoleLib.Console.Keyboard.Char == 33;
		}
		if (id == "Page Down")
		{
			return ConsoleLib.Console.Keyboard.Char == 34;
		}
		if (id == "V Positive")
		{
			return ConsoleLib.Console.Keyboard.Char == 107;
		}
		if (id == "V Negative")
		{
			return ConsoleLib.Console.Keyboard.Char == 109;
		}
		if (id == "CmdMoveN")
		{
			return ConsoleLib.Console.Keyboard.Char == 98;
		}
		if (id == "CmdMoveS")
		{
			return ConsoleLib.Console.Keyboard.Char == 102;
		}
		if (id == "CmdMoveE")
		{
			return ConsoleLib.Console.Keyboard.Char == 100;
		}
		if (id == "CmdMoveW")
		{
			return ConsoleLib.Console.Keyboard.Char == 102;
		}
		if (id == "CmdMoveNE")
		{
			return ConsoleLib.Console.Keyboard.Char == 105;
		}
		if (id == "CmdMoveSE")
		{
			return ConsoleLib.Console.Keyboard.Char == 99;
		}
		if (id == "CmdMoveSW")
		{
			return ConsoleLib.Console.Keyboard.Char == 97;
		}
		if (id == "CmdMoveNW")
		{
			return ConsoleLib.Console.Keyboard.Char == 103;
		}
		string text = "Command:" + id;
		return ConsoleLib.Console.Keyboard.Char == 252 && ConsoleLib.Console.Keyboard.CurrentMouseEvent.Event == text;
	}

	// Token: 0x06000408 RID: 1032 RVA: 0x0001A6F6 File Offset: 0x000188F6
	private static ControlManager.FrameCommand nextFrameCommand()
	{
		if (ControlManager.commandPool.Count > 0)
		{
			return ControlManager.commandPool.Dequeue() ?? new ControlManager.FrameCommand();
		}
		return new ControlManager.FrameCommand();
	}

	// Token: 0x06000409 RID: 1033 RVA: 0x0001A720 File Offset: 0x00018920
	public static void UpdateTheCommandQueue()
	{
		if (ControlManager._WaitForKeyupList.Count > 0)
		{
			List<string> list = ControlManager._WaitForKeyupList;
			lock (list)
			{
				for (int i = ControlManager._WaitForKeyupList.Count - 1; i >= 0; i--)
				{
					if (!CommandBindingManager.CommandBindings[ControlManager._WaitForKeyupList[i]].IsPressed(false))
					{
						ControlManager._WaitForKeyupList.RemoveAt(i);
					}
				}
			}
		}
		if (ControlManager.lastFrame == Time.frameCount)
		{
			Debug.LogError("updating the command queue twice in the same frame! " + Environment.StackTrace.ToString());
			return;
		}
		if (!ControlManager.NeedsQueueUpdate)
		{
			return;
		}
		ControlManager.lastFrame = Time.frameCount;
		ControlManager.NeedsQueueUpdate = false;
		if (ControlManager.FrameDownCommand != null)
		{
			ControlManager.commandPool.Enqueue(ControlManager.FrameDownCommand);
		}
		ControlManager.FrameDownCommand = null;
		ControlManager.currentFrameCommands.Clear();
		string text = ControlManager.ResolveAxisDirection("UI:Navigate");
		ControlManager.StickN = ControlManager.DoStick("UI:Navigate", "N", text);
		if (ControlManager.PauseStickNTillReleased && ControlManager.StickN == 0 && !ControlManager.commandDelayTimers.ContainsKey("__STICKNAV_N") && !ControlManager.commandRepeatTimers.ContainsKey("__STICKNAV_N") && !ControlManager.commandDelayTimers.ContainsKey("CmdMoveN") && !ControlManager.commandRepeatTimers.ContainsKey("CmdMoveN"))
		{
			ControlManager.PauseStickNTillReleased = false;
		}
		ControlManager.StickS = ControlManager.DoStick("UI:Navigate", "S", text);
		if (ControlManager.PauseStickSTillReleased && ControlManager.StickS == 0 && !ControlManager.commandDelayTimers.ContainsKey("__STICKNAV_S") && !ControlManager.commandRepeatTimers.ContainsKey("__STICKNAV_S") && !ControlManager.commandDelayTimers.ContainsKey("CmdMoveS") && !ControlManager.commandRepeatTimers.ContainsKey("CmdMoveS"))
		{
			ControlManager.PauseStickSTillReleased = false;
		}
		ControlManager.StickE = ControlManager.DoStick("UI:Navigate", "E", text);
		if (ControlManager.PauseStickETillReleased && ControlManager.StickE == 0 && !ControlManager.commandDelayTimers.ContainsKey("__STICKNAV_E") && !ControlManager.commandRepeatTimers.ContainsKey("__STICKNAV_E") && !ControlManager.commandDelayTimers.ContainsKey("CmdMoveE") && !ControlManager.commandRepeatTimers.ContainsKey("CmdMoveE"))
		{
			ControlManager.PauseStickETillReleased = false;
		}
		ControlManager.StickW = ControlManager.DoStick("UI:Navigate", "W", text);
		if (ControlManager.PauseStickWTillReleased && ControlManager.StickW == 0 && !ControlManager.commandDelayTimers.ContainsKey("__STICKNAV_W") && !ControlManager.commandRepeatTimers.ContainsKey("__STICKNAV_W") && !ControlManager.commandDelayTimers.ContainsKey("CmdMoveW") && !ControlManager.commandRepeatTimers.ContainsKey("CmdMoveW"))
		{
			ControlManager.PauseStickWTillReleased = false;
		}
		for (int j = 0; j < CommandBindingManager.CommandBindingLayersList.Count; j++)
		{
			CommandBindingLayer commandBindingLayer = CommandBindingManager.CommandBindingLayersList[j];
			int k = 0;
			while (k < commandBindingLayer.actions.Count)
			{
				CommandBinding commandBinding = commandBindingLayer.actions[k];
				if (ControlManager._WaitForKeyupList.Count > 0)
				{
					List<string> list = ControlManager._WaitForKeyupList;
					lock (list)
					{
						for (int l = 0; l < ControlManager._WaitForKeyupList.Count; l++)
						{
							if (ControlManager._WaitForKeyupList[l] == commandBinding.name)
							{
								goto IL_0618;
							}
						}
					}
					goto IL_031A;
				}
				goto IL_031A;
				IL_0618:
				k++;
				continue;
				IL_031A:
				if (commandBinding.WasPressedThisFrame(false))
				{
					if (ControlManager.currentFrameCommands.Count > 0)
					{
						for (int m = 0; m < ControlManager.currentFrameCommands.Count; m++)
						{
							if (ControlManager.currentFrameCommands[m].binding.SharesBindingsWith(commandBinding))
							{
								ControlManager.currentFrameCommands[m].aliases.Add(commandBinding.name);
								goto IL_0618;
							}
						}
					}
					ControlManager.FrameCommand frameCommand = ControlManager.nextFrameCommand();
					frameCommand.reset();
					frameCommand.binding = commandBinding;
					frameCommand.id = commandBinding.name;
					frameCommand.isRepeat = false;
					ControlManager.currentFrameCommands.Add(frameCommand);
					ControlManager.CommandQueue.Enqueue(frameCommand);
					ControlManager.commandRepeatTimers.Remove(commandBinding.name);
					ControlManager.commandDelayTimers[commandBinding.name] = 0f;
					goto IL_0618;
				}
				if (commandBinding.IsPressed(false) && commandBinding.command != null && commandBinding.name != null)
				{
					if (ControlManager.commandRepeatTimers.ContainsKey(commandBinding.name))
					{
						Dictionary<string, float> dictionary = ControlManager.commandRepeatTimers;
						string text2 = commandBinding.name;
						dictionary[text2] += Time.deltaTime;
						if (ControlManager.commandRepeatTimers[commandBinding.name] >= ControlManager.repeattime)
						{
							dictionary = ControlManager.commandRepeatTimers;
							text2 = commandBinding.name;
							dictionary[text2] -= ControlManager.repeattime;
							if (ControlManager.currentFrameCommands.Count > 0)
							{
								for (int n = 0; n < ControlManager.currentFrameCommands.Count; n++)
								{
									if (ControlManager.currentFrameCommands[n].binding.SharesBindingsWith(commandBinding))
									{
										ControlManager.currentFrameCommands[n].aliases.Add(commandBinding.name);
										goto IL_0618;
									}
								}
							}
							ControlManager.FrameCommand frameCommand2 = ControlManager.nextFrameCommand();
							frameCommand2.reset();
							frameCommand2.binding = commandBinding;
							frameCommand2.id = commandBinding.name;
							frameCommand2.isRepeat = true;
							ControlManager.currentFrameCommands.Add(frameCommand2);
							ControlManager.CommandQueue.Enqueue(frameCommand2);
							goto IL_0618;
						}
						goto IL_0618;
					}
					else
					{
						if (!ControlManager.commandDelayTimers.ContainsKey(commandBinding.name))
						{
							ControlManager.commandDelayTimers.Add(commandBinding.name, 0f);
						}
						Dictionary<string, float> dictionary = ControlManager.commandDelayTimers;
						string text2 = commandBinding.name;
						dictionary[text2] += Time.deltaTime;
						if (ControlManager.commandDelayTimers[commandBinding.name] >= ControlManager.delaytime)
						{
							ControlManager.commandRepeatTimers.Add(commandBinding.name, 0f);
							goto IL_0618;
						}
						goto IL_0618;
					}
				}
				else
				{
					if (ControlManager.commandDelayTimers.ContainsKey(commandBinding.name))
					{
						ControlManager.commandDelayTimers.Remove(commandBinding.name);
					}
					if (ControlManager.commandRepeatTimers.ContainsKey(commandBinding.name))
					{
						ControlManager.commandRepeatTimers.Remove(commandBinding.name);
						goto IL_0618;
					}
					goto IL_0618;
				}
			}
		}
		if (ControlManager.SkipFrames > 0)
		{
			return;
		}
		while (ControlManager.CommandQueue.Count > 0)
		{
			ControlManager.FrameCommand frameCommand3 = ControlManager.CommandQueue.Dequeue();
			if (frameCommand3.ignore)
			{
				ControlManager.commandPool.Enqueue(frameCommand3);
			}
			else if (!CommandBindingManager.CommandsByID.ContainsKey(frameCommand3.id) || CommandBindingManager.CommandBindingLayers[CommandBindingManager.CommandsByID[frameCommand3.id].Layer].enabled)
			{
				if (((frameCommand3 != null) ? frameCommand3.id : null) != "IndicateDirection")
				{
					if (ControlManager.FrameDownCommand != null)
					{
						ControlManager.commandPool.Enqueue(ControlManager.FrameDownCommand);
					}
					ControlManager.FrameDownCommand = frameCommand3;
					ConsoleLib.Console.Keyboard.KeyEvent.Set();
				}
				return;
			}
		}
		if (ControlManager.FrameDownCommand == null)
		{
			ConsoleLib.Console.Keyboard.ResetIfQueuesEmpty();
		}
	}

	// Token: 0x0600040A RID: 1034 RVA: 0x0001AE5C File Offset: 0x0001905C
	private static int DoStick(string axis, string direction, string resolvedDirection)
	{
		string text = null;
		if (direction == "N")
		{
			text = "__STICKNAV_N";
		}
		if (direction == "S")
		{
			text = "__STICKNAV_S";
		}
		if (direction == "E")
		{
			text = "__STICKNAV_E";
		}
		if (direction == "W")
		{
			text = "__STICKNAV_W";
		}
		if (resolvedDirection == direction)
		{
			if (!ControlManager.commandDelayTimers.ContainsKey(text))
			{
				ControlManager.commandDelayTimers.Add(text, 0f);
				return 1;
			}
			if (!ControlManager.commandRepeatTimers.ContainsKey(text))
			{
				Dictionary<string, float> dictionary = ControlManager.commandDelayTimers;
				string text2 = text;
				dictionary[text2] += Time.deltaTime;
				if (ControlManager.commandDelayTimers[text] >= ControlManager.delaytime)
				{
					ControlManager.commandRepeatTimers.Add(text, 0f);
					return 2;
				}
			}
			else if (ControlManager.commandRepeatTimers.ContainsKey(text))
			{
				Dictionary<string, float> dictionary = ControlManager.commandRepeatTimers;
				string text2 = text;
				dictionary[text2] += Time.deltaTime;
				if (ControlManager.commandRepeatTimers[text] >= ControlManager.repeattime)
				{
					dictionary = ControlManager.commandRepeatTimers;
					text2 = text;
					dictionary[text2] -= ControlManager.repeattime;
					return 2;
				}
			}
		}
		else
		{
			if (ControlManager.commandDelayTimers.ContainsKey(text))
			{
				ControlManager.commandDelayTimers.Remove(text);
			}
			if (ControlManager.commandRepeatTimers.ContainsKey(text))
			{
				ControlManager.commandRepeatTimers.Remove(text);
			}
		}
		return 0;
	}

	// Token: 0x0600040B RID: 1035 RVA: 0x0001AFC0 File Offset: 0x000191C0
	public static int isCommandDownValue(string id, bool repeat = true, bool ignoreSkipframes = false, bool skipTutorialCheck = false)
	{
		if (ControlManager.mappingMode)
		{
			return 0;
		}
		if (ControlManager.NeedsQueueUpdate)
		{
			ControlManager.UpdateTheCommandQueue();
		}
		if (!ignoreSkipframes && ControlManager.SkipFrames > 0)
		{
			return 0;
		}
		if (ControlManager._WaitForKeyupList.Count > 0)
		{
			List<string> waitForKeyupList = ControlManager._WaitForKeyupList;
			lock (waitForKeyupList)
			{
				for (int i = 0; i < ControlManager._WaitForKeyupList.Count; i++)
				{
					if (ControlManager._WaitForKeyupList[i] == id)
					{
						return 0;
					}
				}
			}
		}
		if (SynchronizationContext.Current == The.UiContext && !Application.isPlaying)
		{
			return 0;
		}
		if (ControlManager.currentContext.isInInput() && ControlManager.keysIgnoredWhileInAnInputField.Any<global::UnityEngine.KeyCode>(new Func<global::UnityEngine.KeyCode, bool>(Input.GetKey)))
		{
			return 0;
		}
		if (id == "Navigate Up")
		{
			if (ControlManager.PauseStickNTillReleased)
			{
				return 0;
			}
			if (ControlManager.StickN > 0)
			{
				return ControlManager.StickN;
			}
		}
		if (id == "Navigate Down")
		{
			if (ControlManager.PauseStickSTillReleased)
			{
				return 0;
			}
			if (ControlManager.StickS > 0)
			{
				return ControlManager.StickS;
			}
		}
		if (id == "Navigate Left")
		{
			if (ControlManager.PauseStickWTillReleased)
			{
				return 0;
			}
			if (ControlManager.StickW > 0)
			{
				return ControlManager.StickW;
			}
		}
		if (id == "Navigate Right")
		{
			if (ControlManager.PauseStickETillReleased)
			{
				return 0;
			}
			if (ControlManager.StickE > 0)
			{
				return ControlManager.StickE;
			}
		}
		if (ControlManager.FrameDownCommand == null)
		{
			return 0;
		}
		int num = ControlManager.FrameDownCommand.match(id);
		if (num != 0 && !skipTutorialCheck && !TutorialManager.AllowCommand(id))
		{
			return 0;
		}
		return num;
	}

	// Token: 0x0600040C RID: 1036 RVA: 0x0001B154 File Offset: 0x00019354
	public static string ResolveAxisDirection(string Axis)
	{
		if (!CommandBindingManager.CommandBindings.ContainsKey(Axis))
		{
			return null;
		}
		Vector2 vector = CommandBindingManager.CommandBindings[Axis].ReadValue<Vector2>();
		float num = vector.x;
		float num2 = vector.y;
		string text;
		if (!ControlManager.upActions.TryGetValue(Axis, out text))
		{
			ControlManager.upActions.Add(Axis, Axis + "/up");
			text = ControlManager.upActions[Axis];
		}
		string text2;
		if (!ControlManager.downActions.TryGetValue(Axis, out text2))
		{
			ControlManager.downActions.Add(Axis, Axis + "/down");
			text2 = ControlManager.downActions[Axis];
		}
		string text3;
		if (!ControlManager.leftActions.TryGetValue(Axis, out text3))
		{
			ControlManager.leftActions.Add(Axis, Axis + "/left");
			text3 = ControlManager.leftActions[Axis];
		}
		string text4;
		if (!ControlManager.rightActions.TryGetValue(Axis, out text4))
		{
			ControlManager.rightActions.Add(Axis, Axis + "/right");
			text4 = ControlManager.rightActions[Axis];
		}
		if (ControlManager.isCommandPressed(text) || (Axis == "UI:Navigate" && ControlManager.isCommandPressed("CmdMoveN")))
		{
			num2 = 1f;
		}
		if (ControlManager.isCommandPressed(text2) || (Axis == "UI:Navigate" && ControlManager.isCommandPressed("CmdMoveS")))
		{
			num2 = -1f;
		}
		if (ControlManager.isCommandPressed(text3) || (Axis == "UI:Navigate" && ControlManager.isCommandPressed("CmdMoveW")))
		{
			num = -1f;
		}
		if (ControlManager.isCommandPressed(text4) || (Axis == "UI:Navigate" && ControlManager.isCommandPressed("CmdMoveE")))
		{
			num = 1f;
		}
		float num3 = 0.4f;
		if (num > num3 && num2 > num3)
		{
			return "NE";
		}
		if (num > num3 && num2 < -num3)
		{
			return "SE";
		}
		if (num < -num3 && num2 > num3)
		{
			return "NW";
		}
		if (num < -num3 && num2 < -num3)
		{
			return "SW";
		}
		if (num < -num3)
		{
			return "W";
		}
		if (num > num3)
		{
			return "E";
		}
		if (num2 < -num3)
		{
			return "S";
		}
		if (num2 > num3)
		{
			return "N";
		}
		return null;
	}

	// Token: 0x0600040D RID: 1037 RVA: 0x0001B36C File Offset: 0x0001956C
	public static bool GetLegacyCommandDown(string id, bool repeat = true)
	{
		ControlManager.LegacyKeyCode legacyKeyCode = ControlManager.mapCommandToPrimaryLegacyKeycode(id);
		ControlManager.LegacyKeyCode legacyKeyCode2 = ControlManager.mapCommandToSecondaryLegacyKeycode(id);
		if (ControlManager.currentContext.isInInput())
		{
			if (legacyKeyCode.code == global::UnityEngine.KeyCode.Backspace)
			{
				legacyKeyCode.code = global::UnityEngine.KeyCode.None;
			}
			if (legacyKeyCode2.code == global::UnityEngine.KeyCode.Backspace)
			{
				legacyKeyCode2.code = global::UnityEngine.KeyCode.None;
			}
		}
		return ControlManager.GetLegacyKeyCodeDown(legacyKeyCode, repeat) || ControlManager.GetLegacyKeyCodeDown(legacyKeyCode2, repeat);
	}

	// Token: 0x0600040E RID: 1038 RVA: 0x0001B3CC File Offset: 0x000195CC
	public static async Task<T> SuspendControlsWhile<T>(Func<Task<T>> action)
	{
		List<string> activeLayers = (from kv in CommandBindingManager.CommandBindingLayers
			where kv.Value.enabled
			select kv.Key).ToList<string>();
		ControlManager.mappingMode = true;
		ControlManager.DisableAllLayers();
		T t;
		try
		{
			t = await action();
		}
		finally
		{
			ControlManager.DisableAllLayers();
			activeLayers.ForEach(new Action<string>(ControlManager.EnableLayer));
			ControlManager.ResetInput(true, true);
			ControlManager.mappingMode = false;
		}
		return t;
	}

	// Token: 0x0600040F RID: 1039 RVA: 0x0001B410 File Offset: 0x00019610
	public static string _ConvertBindingTextToGlyphs(string input, ControlManager.InputDeviceType type)
	{
		if (string.IsNullOrEmpty(input))
		{
			return input;
		}
		if (type == ControlManager.InputDeviceType.Keyboard || type == ControlManager.InputDeviceType.Mouse)
		{
			return Sidebar.ToCP437(ControlManager.ConvertModifierGlyphs(input));
		}
		string overrideNameFromBinding;
		if (ControlManager.controllerGlyphs.TryGetValue(input, out overrideNameFromBinding))
		{
			return overrideNameFromBinding;
		}
		overrideNameFromBinding = CommandBindingManager.GetOverrideNameFromBinding(input);
		if (!string.IsNullOrEmpty(overrideNameFromBinding))
		{
			return overrideNameFromBinding;
		}
		return Sidebar.ToCP437(input);
	}

	// Token: 0x06000410 RID: 1040 RVA: 0x0001B464 File Offset: 0x00019664
	public static string ConvertBindingTextToGlyphs(string input, ControlManager.InputDeviceType type)
	{
		if (string.IsNullOrEmpty(input))
		{
			return input;
		}
		if (input.Contains("+") && !input.StartsWith("+") && !input.EndsWith("+") && !input.Contains("++"))
		{
			string text = "";
			string[] array = input.Split("+", StringSplitOptions.None);
			for (int i = 0; i < array.Length; i++)
			{
				if (i > 0)
				{
					text += "+";
				}
				text += ControlManager._ConvertBindingTextToGlyphs(array[i], type);
			}
			return text;
		}
		return ControlManager._ConvertBindingTextToGlyphs(input, type);
	}

	// Token: 0x06000411 RID: 1041 RVA: 0x0001B4FC File Offset: 0x000196FC
	public static string ConvertModifierGlyphs(string input)
	{
		if (input == null)
		{
			return null;
		}
		return input.Replace("Control", "\ue816").Replace("Ctrl", "\ue816").Replace("Alt", "\ue818")
			.Replace("Shift", "\ue802")
			.Replace("LMB", "\ue809")
			.Replace("RMB", "\ue814");
	}

	// Token: 0x040002E0 RID: 736
	public TMP_FontAsset keyboardGlyphFont;

	// Token: 0x040002E1 RID: 737
	public List<ControlManager.ControllerFontSelection> controllerFonts = new List<ControlManager.ControllerFontSelection>();

	// Token: 0x040002E2 RID: 738
	public ControlManager.ControllerFontType controllerFontType = ControlManager.ControllerFontType.Default;

	// Token: 0x040002E3 RID: 739
	private ControlManager.ControllerFontType? lastControllerFontType;

	// Token: 0x040002E4 RID: 740
	private ControlManager.InputDeviceType? lastControllerType;

	// Token: 0x040002E5 RID: 741
	private Vector3 lastMousePosition;

	// Token: 0x040002E6 RID: 742
	private double lastTimeChangedController;

	// Token: 0x040002E7 RID: 743
	protected bool lastInputIsMouse;

	// Token: 0x040002E8 RID: 744
	public static int SkipFrames = 0;

	// Token: 0x040002E9 RID: 745
	private static List<string> _WaitForKeyupList = new List<string>();

	// Token: 0x040002EA RID: 746
	public static HashSet<string> CommandFramePressed = new HashSet<string>();

	// Token: 0x040002EB RID: 747
	public static bool ControllerChangedThisLateUpdate = false;

	// Token: 0x040002EC RID: 748
	public static bool updateFont = false;

	// Token: 0x040002ED RID: 749
	public static ControlManager.ControlInputContext currentContext = new ControlManager.ControlInputContext();

	// Token: 0x040002EE RID: 750
	public static ControlManager instance;

	// Token: 0x040002EF RID: 751
	public ControlManager.ControllerFontType lastControllerFont;

	// Token: 0x040002F1 RID: 753
	public static InputDevice lastUsedDevice;

	// Token: 0x040002F2 RID: 754
	public static InputControl lastControlPressed;

	// Token: 0x040002F3 RID: 755
	public static bool SuspectSteamInput = false;

	// Token: 0x040002F4 RID: 756
	public static List<string> EnabledLayers = new List<string>();

	// Token: 0x040002F5 RID: 757
	public static Dictionary<string, string> _mapRewiredIDToLegacyID = new Dictionary<string, string>
	{
		{ "Accept", "Accept" },
		{ "Cancel", "Cancel" },
		{ "Interact", "CmdUse" },
		{ "Fire", "CmdFire" },
		{ "Reload", "CmdReload" },
		{ "Abilities", "CmdAbilities" },
		{ "Character Sheet", "CmdCharacter" },
		{ "Walk", "CmdWalk" },
		{ "Look", "CmdLook" },
		{ "ZoomIn", "CmdZoomIn" },
		{ "ZoomOut", "CmdZoomOut" },
		{ "Get", "CmdGet" },
		{ "GetNearby", "CmdGetFrom" },
		{ "InteractNearby", "CmdGetFrom" },
		{ "Autoexplore", "CmdAutoExplore" },
		{ "Wait", "CmdWait" },
		{ "Wait Menu", "CmdWaitMenu" },
		{ "Rest", "CmdWaitUntilHealed" },
		{ "Throw", "CmdThrow" },
		{ "Wish", "CmdWish" },
		{ "Quests", "CmdQuests" },
		{ "Save", "CmdSave" },
		{ "Load", "CmdLoad" },
		{ "AutoAttackNearest", "CmdAttackNearest" },
		{ "Move Up", "CmdMoveU" },
		{ "Move Down", "CmdMoveD" },
		{ "Move Northwest", "CmdMoveNW" },
		{ "Move North", "CmdMoveN" },
		{ "Move Northeast", "CmdMoveNE" },
		{ "Move East", "CmdMoveE" },
		{ "Move Southeast", "CmdMoveSE" },
		{ "Move South", "CmdMoveS" },
		{ "Move Southwest", "CmdMoveSW" },
		{ "Move West", "CmdMoveW" },
		{ "Attack Up", "CmdAttackN" },
		{ "Attack Down", "CmdAttackD" },
		{ "Attack Northwest", "CmdAttackNW" },
		{ "Attack North", "CmdAttackN" },
		{ "Attack Northeast", "CmdAttackNE" },
		{ "Attack East", "CmdAttackE" },
		{ "Attack Southeast", "CmdAttackSE" },
		{ "Attack South", "CmdAttackS" },
		{ "Attack Southwest", "CmdAttackSW" },
		{ "Attack West", "CmdAttackW" },
		{ "Toggle Message Log", "CmdShowSidebar" }
	};

	// Token: 0x040002F6 RID: 758
	public static Dictionary<int, global::UnityEngine.KeyCode> _consoleKeycodeToUnityKeycode = null;

	// Token: 0x040002F7 RID: 759
	public static Dictionary<global::UnityEngine.KeyCode, Keys> _UnityKeycodeToConsoleKeycodeMap = null;

	// Token: 0x040002F8 RID: 760
	private static Dictionary<global::UnityEngine.KeyCode, KeyControl> _MacOS13UnityKeypadKeysDummy = new Dictionary<global::UnityEngine.KeyCode, KeyControl>();

	// Token: 0x040002F9 RID: 761
	private static Dictionary<global::UnityEngine.KeyCode, KeyControl> _MacOS13UnityKeypadKeys = null;

	// Token: 0x040002FA RID: 762
	public static string frameString = null;

	// Token: 0x040002FB RID: 763
	public static List<global::UnityEngine.KeyCode> submitKeys = new List<global::UnityEngine.KeyCode>
	{
		global::UnityEngine.KeyCode.KeypadEnter,
		global::UnityEngine.KeyCode.Return,
		global::UnityEngine.KeyCode.Space
	};

	// Token: 0x040002FC RID: 764
	public static List<global::UnityEngine.KeyCode> keysIgnoredWhileInAnInputField = new List<global::UnityEngine.KeyCode>
	{
		global::UnityEngine.KeyCode.Backspace,
		global::UnityEngine.KeyCode.Keypad0,
		global::UnityEngine.KeyCode.Keypad1,
		global::UnityEngine.KeyCode.Keypad2,
		global::UnityEngine.KeyCode.Keypad3,
		global::UnityEngine.KeyCode.Keypad4,
		global::UnityEngine.KeyCode.Keypad5,
		global::UnityEngine.KeyCode.Keypad6,
		global::UnityEngine.KeyCode.Keypad7,
		global::UnityEngine.KeyCode.Keypad8,
		global::UnityEngine.KeyCode.Keypad9,
		global::UnityEngine.KeyCode.Alpha0,
		global::UnityEngine.KeyCode.Alpha1,
		global::UnityEngine.KeyCode.Alpha2,
		global::UnityEngine.KeyCode.Alpha3,
		global::UnityEngine.KeyCode.Alpha4,
		global::UnityEngine.KeyCode.Alpha5,
		global::UnityEngine.KeyCode.Alpha6,
		global::UnityEngine.KeyCode.Alpha7,
		global::UnityEngine.KeyCode.Alpha8,
		global::UnityEngine.KeyCode.Alpha9,
		global::UnityEngine.KeyCode.A,
		global::UnityEngine.KeyCode.B,
		global::UnityEngine.KeyCode.C,
		global::UnityEngine.KeyCode.D,
		global::UnityEngine.KeyCode.E,
		global::UnityEngine.KeyCode.F,
		global::UnityEngine.KeyCode.G,
		global::UnityEngine.KeyCode.H,
		global::UnityEngine.KeyCode.I,
		global::UnityEngine.KeyCode.J,
		global::UnityEngine.KeyCode.K,
		global::UnityEngine.KeyCode.L,
		global::UnityEngine.KeyCode.M,
		global::UnityEngine.KeyCode.N,
		global::UnityEngine.KeyCode.O,
		global::UnityEngine.KeyCode.P,
		global::UnityEngine.KeyCode.Q,
		global::UnityEngine.KeyCode.R,
		global::UnityEngine.KeyCode.S,
		global::UnityEngine.KeyCode.T,
		global::UnityEngine.KeyCode.U,
		global::UnityEngine.KeyCode.V,
		global::UnityEngine.KeyCode.W,
		global::UnityEngine.KeyCode.X,
		global::UnityEngine.KeyCode.Y,
		global::UnityEngine.KeyCode.Z,
		global::UnityEngine.KeyCode.Space,
		global::UnityEngine.KeyCode.Comma,
		global::UnityEngine.KeyCode.Colon,
		global::UnityEngine.KeyCode.Semicolon,
		global::UnityEngine.KeyCode.LeftArrow,
		global::UnityEngine.KeyCode.RightArrow,
		global::UnityEngine.KeyCode.Slash,
		global::UnityEngine.KeyCode.Quote,
		global::UnityEngine.KeyCode.DoubleQuote,
		global::UnityEngine.KeyCode.LeftBracket,
		global::UnityEngine.KeyCode.RightBracket
	};

	// Token: 0x040002FD RID: 765
	public static Dictionary<string, string> gamepadCommandInputsWithGlyphs = new Dictionary<string, string>();

	// Token: 0x040002FE RID: 766
	public static Dictionary<string, string> gamepadCommandInputsWithoutGlyphs = new Dictionary<string, string>();

	// Token: 0x040002FF RID: 767
	public static Dictionary<string, string> keyboardCommandInputsWithGlyphs = new Dictionary<string, string>();

	// Token: 0x04000300 RID: 768
	public static Dictionary<string, string> keyboardCommandInputsWithoutGlyphs = new Dictionary<string, string>();

	// Token: 0x04000301 RID: 769
	public static float delaytime = 0.5f;

	// Token: 0x04000302 RID: 770
	public static float repeattime = 0.1f;

	// Token: 0x04000303 RID: 771
	public static Dictionary<global::UnityEngine.KeyCode, float> delayTimers = new Dictionary<global::UnityEngine.KeyCode, float>();

	// Token: 0x04000304 RID: 772
	public static Dictionary<global::UnityEngine.KeyCode, float> repeatTimers = new Dictionary<global::UnityEngine.KeyCode, float>();

	// Token: 0x04000305 RID: 773
	public static Dictionary<int, float> delayTimers2 = new Dictionary<int, float>();

	// Token: 0x04000306 RID: 774
	public static Dictionary<int, float> repeatTimers2 = new Dictionary<int, float>();

	// Token: 0x04000307 RID: 775
	public static global::UnityEngine.KeyCode lastMacOS13KeyPress = global::UnityEngine.KeyCode.None;

	// Token: 0x04000308 RID: 776
	public static float MacOS13repeatTime = 0f;

	// Token: 0x04000309 RID: 777
	public static bool MacOS13repeating = false;

	// Token: 0x0400030A RID: 778
	private static List<string> scratch = new List<string>(64);

	// Token: 0x0400030B RID: 779
	private static List<string> layersToEnableAtEndOfFrame = null;

	// Token: 0x0400030C RID: 780
	public static ControlManager.InputDeviceType _activeControllerType;

	// Token: 0x0400030D RID: 781
	public static bool mappingMode = false;

	// Token: 0x0400030E RID: 782
	public static Dictionary<string, float> commandDelayTimers = new Dictionary<string, float>();

	// Token: 0x0400030F RID: 783
	public static Dictionary<string, float> commandRepeatTimers = new Dictionary<string, float>();

	// Token: 0x04000310 RID: 784
	private static Queue<ControlManager.FrameCommand> commandPool = new Queue<ControlManager.FrameCommand>(16);

	// Token: 0x04000311 RID: 785
	private static ControlManager.FrameCommand FrameDownCommand;

	// Token: 0x04000312 RID: 786
	private static Queue<ControlManager.FrameCommand> CommandQueue = new Queue<ControlManager.FrameCommand>();

	// Token: 0x04000313 RID: 787
	private static bool NeedsQueueUpdate = false;

	// Token: 0x04000314 RID: 788
	private static int lastFrame = 0;

	// Token: 0x04000315 RID: 789
	public static List<ControlManager.FrameCommand> currentFrameCommands = new List<ControlManager.FrameCommand>(8);

	// Token: 0x04000316 RID: 790
	private static bool PauseStickNTillReleased = false;

	// Token: 0x04000317 RID: 791
	private static int StickN = 0;

	// Token: 0x04000318 RID: 792
	private static bool PauseStickSTillReleased = false;

	// Token: 0x04000319 RID: 793
	private static int StickS = 0;

	// Token: 0x0400031A RID: 794
	private static bool PauseStickETillReleased = false;

	// Token: 0x0400031B RID: 795
	private static int StickE = 0;

	// Token: 0x0400031C RID: 796
	private static bool PauseStickWTillReleased = false;

	// Token: 0x0400031D RID: 797
	private static int StickW = 0;

	// Token: 0x0400031E RID: 798
	private static Dictionary<string, string> upActions = new Dictionary<string, string>();

	// Token: 0x0400031F RID: 799
	private static Dictionary<string, string> downActions = new Dictionary<string, string>();

	// Token: 0x04000320 RID: 800
	private static Dictionary<string, string> leftActions = new Dictionary<string, string>();

	// Token: 0x04000321 RID: 801
	private static Dictionary<string, string> rightActions = new Dictionary<string, string>();

	// Token: 0x04000322 RID: 802
	private static Dictionary<string, string> controllerGlyphs = new Dictionary<string, string>
	{
		{ "LS", "\ue904" },
		{ "Left Stick", "\ue904" },
		{ "RS", "\ue90f" },
		{ "Right Stick", "\ue90f" },
		{ "Cross", "\ue900" },
		{ "A", "\ue900" },
		{ "Button 0", "\ue900" },
		{ "Circle", "\ue901" },
		{ "B", "\ue901" },
		{ "Button 1", "\ue901" },
		{ "Square", "\ue902" },
		{ "X", "\ue902" },
		{ "Button 2", "\ue902" },
		{ "Triangle", "\ue903" },
		{ "Y", "\ue903" },
		{ "Button 3", "\ue903" },
		{ "Left Shoulder", "\ue915" },
		{ "L1", "\ue915" },
		{ "LB", "\ue915" },
		{ "Right Shoulder", "\ue917" },
		{ "R1", "\ue917" },
		{ "RB", "\ue917" },
		{ "Left Trigger", "\ue916" },
		{ "L2", "\ue916" },
		{ "LT", "\ue916" },
		{ "Right Trigger", "\ue918" },
		{ "R2", "\ue918" },
		{ "RT", "\ue918" },
		{ "D-Pad Up", "\ue90b" },
		{ "Up", "\ue90b" },
		{ "D-Pad Down", "\ue90d" },
		{ "Down", "\ue90d" },
		{ "D-Pad Left", "\ue90e" },
		{ "Left", "\ue90e" },
		{ "D-Pad Right", "\ue90c" },
		{ "Right", "\ue90c" },
		{ "D-Pad/Up", "\ue90b" },
		{ "D-Pad/Down", "\ue90d" },
		{ "D-Pad/Left", "\ue90e" },
		{ "D-Pad/Right", "\ue90c" },
		{ "Start", "\ue91a" },
		{ "Options", "\ue91a" },
		{ "Share", "\ue919" },
		{ "Select", "\ue919" },
		{ "Back", "\ue919" },
		{ "Create", "\ue919" },
		{ "Left Stick X", "\ue908\ue906" },
		{ "Left Stick Y", "\ue905\ue907" },
		{ "Right Stick X", "\ue913\ue911" },
		{ "Right Stick Y", "\ue910\ue912" },
		{ "Left Stick Press", "\ue909" },
		{ "Right Stick Press", "\ue914" },
		{ "L3", "\ue909" },
		{ "R3", "\ue914" }
	};

	// Token: 0x02001587 RID: 5511
	public class ControlInputContext
	{
		// Token: 0x0600D570 RID: 54640 RVA: 0x004F45AD File Offset: 0x004F27AD
		private static bool isFalse()
		{
			return false;
		}

		// Token: 0x040056A4 RID: 22180
		public Func<bool> isInInput = new Func<bool>(ControlManager.ControlInputContext.isFalse);
	}

	// Token: 0x02001588 RID: 5512
	[Serializable]
	public enum ControllerFontType
	{
		// Token: 0x040056A6 RID: 22182
		XBox,
		// Token: 0x040056A7 RID: 22183
		PS5,
		// Token: 0x040056A8 RID: 22184
		Switch,
		// Token: 0x040056A9 RID: 22185
		Keyboard,
		// Token: 0x040056AA RID: 22186
		Default,
		// Token: 0x040056AB RID: 22187
		XBoxFilled,
		// Token: 0x040056AC RID: 22188
		PS5Filled,
		// Token: 0x040056AD RID: 22189
		ForceKBM,
		// Token: 0x040056AE RID: 22190
		SwitchFilled
	}

	// Token: 0x02001589 RID: 5513
	[Serializable]
	public struct ControllerFontSelection
	{
		// Token: 0x040056AF RID: 22191
		public ControlManager.ControllerFontType controllerType;

		// Token: 0x040056B0 RID: 22192
		public TMP_FontAsset fontAsset;
	}

	// Token: 0x0200158A RID: 5514
	public interface IControllerChangedEvent
	{
		// Token: 0x0600D572 RID: 54642
		void ControllerChanged();
	}

	// Token: 0x0200158B RID: 5515
	public static class Keybinds
	{
		// Token: 0x040056B1 RID: 22193
		public static readonly string ACCEPT = "accept";
	}

	// Token: 0x0200158C RID: 5516
	public struct LegacyKeyCode
	{
		// Token: 0x0600D574 RID: 54644 RVA: 0x004F45D8 File Offset: 0x004F27D8
		public LegacyKeyCode(global::UnityEngine.KeyCode code, bool bAlt, bool bCtrl, bool bShift)
		{
			this.code = code;
			this.bAlt = bAlt;
			this.bCtrl = bCtrl;
			this.bShift = bShift;
			this.rawCode = (int)(ControlManager.UnityKeycodeToConsoleKeycodeMap[code] + (bAlt ? 262144 : 0) + (bCtrl ? 131072 : 0) + (bShift ? 65536 : 0));
		}

		// Token: 0x040056B2 RID: 22194
		public bool bCtrl;

		// Token: 0x040056B3 RID: 22195
		public bool bShift;

		// Token: 0x040056B4 RID: 22196
		public bool bAlt;

		// Token: 0x040056B5 RID: 22197
		public global::UnityEngine.KeyCode code;

		// Token: 0x040056B6 RID: 22198
		public int rawCode;
	}

	// Token: 0x0200158D RID: 5517
	public enum InputDeviceType
	{
		// Token: 0x040056B8 RID: 22200
		Mouse,
		// Token: 0x040056B9 RID: 22201
		Keyboard,
		// Token: 0x040056BA RID: 22202
		Gamepad,
		// Token: 0x040056BB RID: 22203
		Unknown
	}

	// Token: 0x0200158E RID: 5518
	public class FrameCommand
	{
		// Token: 0x0600D575 RID: 54645 RVA: 0x004F4638 File Offset: 0x004F2838
		public override string ToString()
		{
			string res = this.id;
			this.aliases.ForEach(delegate(string a)
			{
				res = res + " " + a;
			});
			return res;
		}

		// Token: 0x0600D576 RID: 54646 RVA: 0x004F4674 File Offset: 0x004F2874
		public int match(string test)
		{
			bool flag = false;
			for (int i = 0; i < this.aliases.Count; i++)
			{
				if (this.aliases[i] == test)
				{
					flag = true;
					break;
				}
			}
			if (!(this.id == test) && !flag)
			{
				return 0;
			}
			if (this.isRepeat)
			{
				return 2;
			}
			return 1;
		}

		// Token: 0x0600D577 RID: 54647 RVA: 0x004F46CE File Offset: 0x004F28CE
		public void reset()
		{
			this.ignore = false;
			this.binding = null;
			this.id = null;
			if (this.aliases.Count > 0)
			{
				this.aliases.Clear();
			}
			this.isRepeat = false;
		}

		// Token: 0x040056BC RID: 22204
		public CommandBinding binding;

		// Token: 0x040056BD RID: 22205
		public string id;

		// Token: 0x040056BE RID: 22206
		public List<string> aliases = new List<string>(8);

		// Token: 0x040056BF RID: 22207
		public bool isRepeat;

		// Token: 0x040056C0 RID: 22208
		public bool ignore;
	}
}
