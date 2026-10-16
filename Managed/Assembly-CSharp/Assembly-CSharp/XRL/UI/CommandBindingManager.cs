using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ConsoleLib.Console;
using Newtonsoft.Json;
using Platform.IO;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using XRL.Collections;

namespace XRL.UI
{
	// Token: 0x02001470 RID: 5232
	[HasModSensitiveStaticCache]
	public static class CommandBindingManager
	{
		// Token: 0x0600CDBB RID: 52667 RVA: 0x004A9078 File Offset: 0x004A7278
		public static ControlManager.InputDeviceType GetInputDeviceTypeFromBindingPath(string path)
		{
			if (path != null && path.Contains("Gamepad"))
			{
				return ControlManager.InputDeviceType.Gamepad;
			}
			foreach (KeyValuePair<string, ControlManager.InputDeviceType> keyValuePair in CommandBindingManager.ControlPrefixType)
			{
				if (path.StartsWith(keyValuePair.Key, StringComparison.InvariantCultureIgnoreCase))
				{
					return keyValuePair.Value;
				}
			}
			return ControlManager.InputDeviceType.Unknown;
		}

		// Token: 0x0600CDBC RID: 52668 RVA: 0x004A90F4 File Offset: 0x004A72F4
		public static IEnumerable<string> GetCommandBindings(string cmd)
		{
			CommandBinding action;
			if (CommandBindingManager.CommandBindings.TryGetValue(cmd, out action))
			{
				int num;
				for (int x = 0; x < action._inputAction.bindings.Count; x = num + 1)
				{
					if (!action._inputAction.bindings[x].isPartOfComposite)
					{
						yield return action._inputAction.GetBindingDisplayString(x, (InputBinding.DisplayStringOptions)0);
					}
					num = x;
				}
			}
			yield break;
		}

		// Token: 0x0600CDBD RID: 52669 RVA: 0x004A9104 File Offset: 0x004A7304
		public static void GetCommandBindings(string cmd, ControlManager.InputDeviceType controllerType, out string bind1)
		{
			IEnumerator<string> enumerator = CommandBindingManager.GetCommandBindings(cmd, controllerType, true).GetEnumerator();
			bind1 = null;
			if (enumerator.MoveNext())
			{
				bind1 = enumerator.Current;
			}
			enumerator.Dispose();
		}

		// Token: 0x0600CDBE RID: 52670 RVA: 0x004A9138 File Offset: 0x004A7338
		public static void GetCommandBindings(string cmd, ControlManager.InputDeviceType controllerType, out string bind1, out string bind2)
		{
			IEnumerator<string> enumerator = CommandBindingManager.GetCommandBindings(cmd, controllerType, true).GetEnumerator();
			bind1 = null;
			bind2 = null;
			if (enumerator.MoveNext())
			{
				bind1 = enumerator.Current;
			}
			if (enumerator.MoveNext())
			{
				bind2 = enumerator.Current;
			}
			enumerator.Dispose();
		}

		// Token: 0x0600CDBF RID: 52671 RVA: 0x004A9180 File Offset: 0x004A7380
		public static void GetCommandBindings(string cmd, ControlManager.InputDeviceType controllerType, out string bind1, out string bind2, out string bind3, out string bind4)
		{
			IEnumerator<string> enumerator = CommandBindingManager.GetCommandBindings(cmd, controllerType, false).Select<string, string>(new Func<string, string>(Sidebar.ToCP437)).GetEnumerator();
			bind1 = null;
			bind2 = null;
			bind3 = null;
			bind4 = null;
			if (enumerator.MoveNext())
			{
				bind1 = enumerator.Current;
			}
			if (enumerator.MoveNext())
			{
				bind2 = enumerator.Current;
			}
			if (enumerator.MoveNext())
			{
				bind3 = enumerator.Current;
			}
			if (enumerator.MoveNext())
			{
				bind4 = enumerator.Current;
			}
			enumerator.Dispose();
		}

		// Token: 0x0600CDC0 RID: 52672 RVA: 0x004A9202 File Offset: 0x004A7402
		public static IEnumerable<string> GetCommandBindings(string cmd, ControlManager.InputDeviceType byType, bool forGlyph = true)
		{
			CommandBinding action;
			if (CommandBindingManager.CommandBindings.TryGetValue(cmd, out action))
			{
				int num;
				for (int x = 0; x < action._inputAction.bindings.Count; x = num + 1)
				{
					if (!action._inputAction.bindings[x].isPartOfComposite)
					{
						if (action._inputAction.bindings[x].isComposite)
						{
							if (CommandBindingManager.GetInputDeviceTypeFromBindingPath(action._inputAction.bindings[x + 1].path) != byType)
							{
								goto IL_01B7;
							}
						}
						else if (CommandBindingManager.GetInputDeviceTypeFromBindingPath(action._inputAction.bindings[x].path) != byType)
						{
							goto IL_01B7;
						}
						if (CapabilityManager.CurrentPlatformClassification() == CapabilityManager.PlatformClassification.PC)
						{
							yield return action._inputAction.GetBindingDisplayString(x, (InputBinding.DisplayStringOptions)0);
						}
						else
						{
							string text;
							string text2;
							string bindingDisplayString = action._inputAction.GetBindingDisplayString(x, out text, out text2, (InputBinding.DisplayStringOptions)0);
							if (string.IsNullOrEmpty(text))
							{
								yield return CommandBindingManager.GetConsoleOverrideDisplayName(bindingDisplayString);
							}
							else
							{
								text.Equals("Gamepad", StringComparison.InvariantCultureIgnoreCase);
								yield return CommandBindingManager.GetOverrideNameFromBinding(bindingDisplayString, text, text2);
							}
						}
					}
					IL_01B7:
					num = x;
				}
			}
			yield break;
		}

		// Token: 0x0600CDC1 RID: 52673 RVA: 0x004A9219 File Offset: 0x004A7419
		public static string GetConsoleOverrideDisplayName(string defaultName)
		{
			return defaultName;
		}

		// Token: 0x0600CDC2 RID: 52674 RVA: 0x004A921C File Offset: 0x004A741C
		private static string GetSwitchOverrideDisplayName(string defaultName)
		{
			for (int i = 0; i < CommandBindingManager.SwitchDisplayNameOverrides.Length; i++)
			{
				ValueTuple<string, string> valueTuple = CommandBindingManager.SwitchDisplayNameOverrides[i];
				defaultName = Regex.Replace(defaultName, valueTuple.Item1, valueTuple.Item2);
			}
			return defaultName;
		}

		// Token: 0x0600CDC3 RID: 52675 RVA: 0x004A925C File Offset: 0x004A745C
		public static string GetOverrideNameFromBinding(string defaultName, string deviceLayoutName, string controlPath)
		{
			if (deviceLayoutName == "NPad")
			{
				return CommandBindingManager.GetSwitchOverrideNameFromBinding(defaultName, controlPath);
			}
			return defaultName;
		}

		// Token: 0x0600CDC4 RID: 52676 RVA: 0x004A9274 File Offset: 0x004A7474
		public static string GetOverrideNameFromBinding(string bindingPath)
		{
			Match match = CommandBindingManager._deviceFromBinding.Match(bindingPath);
			string value = match.Groups["device"].Value;
			string value2 = match.Groups["control"].Value;
			return CommandBindingManager.GetOverrideNameFromBinding("", value, value2);
		}

		// Token: 0x0600CDC5 RID: 52677 RVA: 0x004A92C4 File Offset: 0x004A74C4
		private static string GetSwitchOverrideNameFromBinding(string defaultName, string controlPath)
		{
			uint num = <PrivateImplementationDetails>.ComputeStringHash(controlPath);
			if (num <= 2072635087U)
			{
				if (num <= 938826173U)
				{
					if (num <= 527024624U)
					{
						if (num != 91826501U)
						{
							if (num != 297952813U)
							{
								if (num == 527024624U)
								{
									if (controlPath == "leftStick")
									{
										return "Left Stick";
									}
								}
							}
							else if (controlPath == "select")
							{
								return "-";
							}
						}
						else if (controlPath == "rightStick/up")
						{
							return "Right Stick Up";
						}
					}
					else if (num != 869412772U)
					{
						if (num != 881114153U)
						{
							if (num == 938826173U)
							{
								if (controlPath == "leftStick/right")
								{
									return "Left Stick Right";
								}
							}
						}
						else if (controlPath == "dpad/right")
						{
							return "Right";
						}
					}
					else if (controlPath == "rightStick/down")
					{
						return "Right Stick Down";
					}
				}
				else if (num <= 1697318111U)
				{
					if (num != 996955572U)
					{
						if (num != 1411432780U)
						{
							if (num == 1697318111U)
							{
								if (controlPath == "start")
								{
									return "+";
								}
							}
						}
						else if (controlPath == "leftTrigger")
						{
							return "ZL";
						}
					}
					else if (controlPath == "dpad/up")
					{
						return "Up";
					}
				}
				else if (num <= 1760883434U)
				{
					if (num != 1757831241U)
					{
						if (num == 1760883434U)
						{
							if (controlPath == "rightStick/right")
							{
								return "Right Stick Right";
							}
						}
					}
					else if (controlPath == "rightStick/left")
					{
						return "Right Stick Left";
					}
				}
				else if (num != 1904858897U)
				{
					if (num == 2072635087U)
					{
						if (controlPath == "leftSL")
						{
							return "SL";
						}
					}
				}
				else if (controlPath == "leftSR")
				{
					return "SR";
				}
			}
			else if (num <= 2803379527U)
			{
				if (num <= 2254632560U)
				{
					if (num != 2113007854U)
					{
						if (num != 2221077322U)
						{
							if (num == 2254632560U)
							{
								if (controlPath == "rightSR")
								{
									return "SR";
								}
							}
						}
						else if (controlPath == "rightSL")
						{
							return "SL";
						}
					}
					else if (controlPath == "rightStickPress")
					{
						return "Press Right Stick";
					}
				}
				else if (num != 2427478531U)
				{
					if (num != 2482153640U)
					{
						if (num == 2803379527U)
						{
							if (controlPath == "rightStick")
							{
								return "Right Stick";
							}
						}
					}
					else if (controlPath == "leftStick/left")
					{
						return "Left Stick Left";
					}
				}
				else if (controlPath == "rightTrigger")
				{
					return "ZR";
				}
			}
			else if (num <= 3624025213U)
			{
				if (num != 2991474140U)
				{
					if (num != 3616964664U)
					{
						if (num == 3624025213U)
						{
							if (controlPath == "leftStick/down")
							{
								return "Left Stick Down";
							}
						}
					}
					else if (controlPath == "leftShoulder")
					{
						return "L";
					}
				}
				else if (controlPath == "dpad/left")
				{
					return "Left";
				}
			}
			else if (num <= 3924649288U)
			{
				if (num != 3792611837U)
				{
					if (num == 3924649288U)
					{
						if (controlPath == "leftStick/up")
						{
							return "Left Stick Up";
						}
					}
				}
				else if (controlPath == "rightShoulder")
				{
					return "R";
				}
			}
			else if (num != 4104197953U)
			{
				if (num == 4285979527U)
				{
					if (controlPath == "leftStickPress")
					{
						return "Press Left Stick";
					}
				}
			}
			else if (controlPath == "dpad/down")
			{
				return "Down";
			}
			return defaultName;
		}

		// Token: 0x0600CDC6 RID: 52678 RVA: 0x004A972C File Offset: 0x004A792C
		public static IEnumerable<GameCommand> GetBindingCommands(InputBinding binding, string layer = null)
		{
			foreach (KeyValuePair<string, CommandBinding> keyValuePair in CommandBindingManager.CommandBindings)
			{
				if (keyValuePair.Value._inputAction.GetBindingIndex(binding) >= 0)
				{
					yield return CommandBindingManager.CommandsByID[keyValuePair.Key];
				}
			}
			Dictionary<string, CommandBinding>.Enumerator enumerator = default(Dictionary<string, CommandBinding>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x0600CDC7 RID: 52679 RVA: 0x004A973C File Offset: 0x004A793C
		public static string GetCommandMappedTo(int key, string[] layersToInclude = null)
		{
			return CommandBindingManager.CurrentMap.GetCommandMappedTo(key, layersToInclude);
		}

		// Token: 0x0600CDC8 RID: 52680 RVA: 0x004A974C File Offset: 0x004A794C
		public static string GetCommandFromKey(int c)
		{
			foreach (KeyValuePair<string, Dictionary<int, string>> keyValuePair in CommandBindingManager.CurrentMap.PrimaryMapKeyToCommandLayer)
			{
				if (keyValuePair.Value.ContainsKey(c))
				{
					return keyValuePair.Value[c];
				}
			}
			foreach (KeyValuePair<string, Dictionary<int, string>> keyValuePair2 in CommandBindingManager.CurrentMap.SecondaryMapKeyToCommandLayer)
			{
				if (keyValuePair2.Value.ContainsKey(c))
				{
					return keyValuePair2.Value[c];
				}
			}
			return "CmdUnknown";
		}

		// Token: 0x0600CDC9 RID: 52681 RVA: 0x004A9820 File Offset: 0x004A7A20
		public static string GetCommandFromKey(Keys k)
		{
			return CommandBindingManager.GetCommandFromKey((int)k);
		}

		// Token: 0x0600CDCA RID: 52682 RVA: 0x004A9828 File Offset: 0x004A7A28
		public static string MapKeyToCommand(int Meta, string[] exclusions = null)
		{
			if (exclusions == null)
			{
				exclusions = CommandBindingManager.default_exclusions;
			}
			foreach (KeyValuePair<string, Dictionary<int, string>> keyValuePair in CommandBindingManager.CurrentMap.PrimaryMapKeyToCommandLayer)
			{
				if (!exclusions.Contains(keyValuePair.Key) && keyValuePair.Value.ContainsKey(Meta))
				{
					return keyValuePair.Value[Meta];
				}
			}
			foreach (KeyValuePair<string, Dictionary<int, string>> keyValuePair2 in CommandBindingManager.CurrentMap.SecondaryMapKeyToCommandLayer)
			{
				if (!exclusions.Contains(keyValuePair2.Key) && keyValuePair2.Value.ContainsKey(Meta))
				{
					return keyValuePair2.Value[Meta];
				}
			}
			return "CmdUnknown";
		}

		// Token: 0x0600CDCB RID: 52683 RVA: 0x004A9928 File Offset: 0x004A7B28
		public static string GetNextCommand(string[] exclusions = null)
		{
			return CommandBindingManager.MapKeyToCommand(ConsoleLib.Console.Keyboard.getmeta(false), exclusions);
		}

		// Token: 0x0600CDCC RID: 52684 RVA: 0x004A9938 File Offset: 0x004A7B38
		public static ValueTuple<int, int> GetAllKeysFromCommand(string Cmd)
		{
			int num = -1;
			int num2 = -1;
			KeyMap currentMap = CommandBindingManager.CurrentMap;
			if (((currentMap != null) ? currentMap.PrimaryMapCommandToKeyLayer : null) != null)
			{
				foreach (KeyValuePair<string, Dictionary<string, int>> keyValuePair in CommandBindingManager.CurrentMap.PrimaryMapCommandToKeyLayer)
				{
					if (keyValuePair.Value.TryGetValue(Cmd, out num))
					{
						break;
					}
				}
			}
			KeyMap currentMap2 = CommandBindingManager.CurrentMap;
			if (((currentMap2 != null) ? currentMap2.SecondaryMapCommandToKeyLayer : null) != null)
			{
				foreach (KeyValuePair<string, Dictionary<string, int>> keyValuePair2 in CommandBindingManager.CurrentMap.SecondaryMapCommandToKeyLayer)
				{
					if (keyValuePair2.Value.TryGetValue(Cmd, out num2))
					{
						break;
					}
				}
			}
			return new ValueTuple<int, int>(num, num2);
		}

		// Token: 0x0600CDCD RID: 52685 RVA: 0x004A9A20 File Offset: 0x004A7C20
		public static int GetKeyFromCommand(string Cmd)
		{
			KeyMap currentMap = CommandBindingManager.CurrentMap;
			if (((currentMap != null) ? currentMap.PrimaryMapCommandToKeyLayer : null) != null)
			{
				foreach (KeyValuePair<string, Dictionary<string, int>> keyValuePair in CommandBindingManager.CurrentMap.PrimaryMapCommandToKeyLayer)
				{
					int num;
					if (keyValuePair.Value.TryGetValue(Cmd, out num))
					{
						return num;
					}
				}
			}
			KeyMap currentMap2 = CommandBindingManager.CurrentMap;
			if (((currentMap2 != null) ? currentMap2.SecondaryMapCommandToKeyLayer : null) != null)
			{
				foreach (KeyValuePair<string, Dictionary<string, int>> keyValuePair2 in CommandBindingManager.CurrentMap.SecondaryMapCommandToKeyLayer)
				{
					int num2;
					if (keyValuePair2.Value.TryGetValue(Cmd, out num2))
					{
						return num2;
					}
				}
			}
			return 0;
		}

		// Token: 0x0600CDCE RID: 52686 RVA: 0x004A9B04 File Offset: 0x004A7D04
		public static string GetLegacyKeymapPath()
		{
			return DataManager.SyncedPath(Environment.UserName + ".Keymap.json");
		}

		// Token: 0x0600CDCF RID: 52687 RVA: 0x004A9B1A File Offset: 0x004A7D1A
		public static string GetCurrentKeymapPath()
		{
			return DataManager.SyncedPath(Environment.UserName + ".Keymap2.json");
		}

		// Token: 0x0600CDD0 RID: 52688 RVA: 0x004A9B30 File Offset: 0x004A7D30
		public static async Task SaveCurrentKeymapAsync()
		{
			await The.UiContext;
			CommandBindingManager.SaveCurrentKeymap();
		}

		// Token: 0x0600CDD1 RID: 52689 RVA: 0x004A9B6B File Offset: 0x004A7D6B
		public static void SaveCurrentKeymap()
		{
			CommandBindingManager.SaveKeymap(CommandBindingManager.CurrentMap, CommandBindingManager.GetCurrentKeymapPath());
		}

		// Token: 0x0600CDD2 RID: 52690 RVA: 0x004A9B7C File Offset: 0x004A7D7C
		public static void SaveKeymap(KeyMap Map, string FileName)
		{
			foreach (KeyValuePair<string, CommandBinding> keyValuePair in CommandBindingManager.CommandBindings)
			{
				List<string> list = keyValuePair.Value.SerializedFormat();
				if (list != null)
				{
					Map.CommandToSerializedInputBindings[keyValuePair.Key] = list;
				}
			}
			Map.GamepadAltBind = (Map.CommandToSerializedInputBindings.ContainsKey("GamepadAlt") ? Map.CommandToSerializedInputBindings["GamepadAlt"].First<string>() : "<Gamepad>/leftTrigger");
			Blob.WriteAllJson(FileName, Map, null).LogIfErrored();
		}

		// Token: 0x0600CDD3 RID: 52691 RVA: 0x004A9C30 File Offset: 0x004A7E30
		public static async Task LoadCurrentKeymapAsync(bool restoreLayers = false)
		{
			await The.UiContext;
			CommandBindingManager.LoadCurrentKeymap(restoreLayers);
		}

		// Token: 0x0600CDD4 RID: 52692 RVA: 0x004A9C73 File Offset: 0x004A7E73
		public static void LoadCurrentKeymap(bool restoreLayers = false)
		{
			CommandBindingManager.LoadCurrentKeymap(CommandBindingManager.GetCurrentKeymapPath(), false, restoreLayers, null);
		}

		// Token: 0x0600CDD5 RID: 52693 RVA: 0x004A9C82 File Offset: 0x004A7E82
		public static void LoadCurrentKeymap(string FileName, bool AllowLegacyUpgrade = true, bool restoreLayers = false, string targetSet = null)
		{
			CommandBindingManager.CurrentMap = CommandBindingManager.LoadKeymap(FileName) ?? new KeyMap();
			CommandBindingManager.InitializeInputManager(AllowLegacyUpgrade, restoreLayers, targetSet);
		}

		// Token: 0x0600CDD6 RID: 52694 RVA: 0x004A9CA0 File Offset: 0x004A7EA0
		public static async Task RestoreDefaults()
		{
			int num = 0;
			if (CapabilityManager.CurrentPlatformClassification() == CapabilityManager.PlatformClassification.PC)
			{
				num = await Popup.PickOptionAsync("Selected Bind Set", null, "", new string[] { "Normal", "HJKL" }, null, null, null, null, 0, 60, 0, -1, false, false, false, true);
			}
			if (num >= 0)
			{
				string text = "";
				if (num == 1)
				{
					text = "hjkl";
				}
				List<string> list = new List<string>();
				foreach (KeyValuePair<string, CommandBindingLayer> keyValuePair in CommandBindingManager.CommandBindingLayers)
				{
					if (keyValuePair.Value.enabled)
					{
						list.Add(keyValuePair.Key);
					}
				}
				CommandBindingManager.LoadCurrentKeymap(DataManager.FilePath("DefaultKeymap.json"), false, false, text);
				foreach (KeyValuePair<string, CommandBindingLayer> keyValuePair2 in CommandBindingManager.CommandBindingLayers)
				{
					if (list.Contains(keyValuePair2.Key))
					{
						keyValuePair2.Value.Enable();
					}
					else
					{
						keyValuePair2.Value.Disable();
					}
				}
			}
		}

		// Token: 0x0600CDD7 RID: 52695 RVA: 0x004A9CDC File Offset: 0x004A7EDC
		public static Predicate<GameCommand> ConflictChecker(GameCommand with)
		{
			if (with.IgnoreConflicts)
			{
				return (GameCommand command) => false;
			}
			if (Options.GetOption("OptionDisableBindConflictChecking", "") == "Yes")
			{
				return (GameCommand command) => false;
			}
			HashSet<string> Conflicts = new HashSet<string>();
			foreach (NavCategory navCategory in CommandBindingManager.NavCategories.Values)
			{
				if (navCategory.Layers.Contains(with.Layer))
				{
					foreach (string text in navCategory.Layers)
					{
						Conflicts.Add(text);
					}
				}
			}
			return delegate(GameCommand command)
			{
				if (command != with && command.Bindable && !command.IgnoreConflicts && Conflicts.Contains(command.Layer))
				{
					List<string> canShareBindsWith = command.CanShareBindsWith;
					if (canShareBindsWith == null || !canShareBindsWith.Contains(with.ID))
					{
						List<string> canShareBindsWith2 = with.CanShareBindsWith;
						return canShareBindsWith2 == null || !canShareBindsWith2.Contains(command.ID);
					}
				}
				return false;
			};
		}

		// Token: 0x0600CDD8 RID: 52696 RVA: 0x004A9E1C File Offset: 0x004A801C
		public static Predicate<GameCommand> DynamicLayerConflictChecker()
		{
			HashSet<string> Conflicts = new HashSet<string>();
			foreach (NavCategory navCategory in CommandBindingManager.NavCategories.Values)
			{
				if (navCategory.Layers.Contains("Dynamic"))
				{
					foreach (string text in navCategory.Layers)
					{
						Conflicts.Add(text);
					}
				}
			}
			return (GameCommand command) => command.Bindable && !command.IgnoreConflicts && Conflicts.Contains(command.Layer);
		}

		// Token: 0x0600CDD9 RID: 52697 RVA: 0x004A9EE8 File Offset: 0x004A80E8
		public static async Task RestoreDefaultsAsync()
		{
			await The.UiContext;
			await CommandBindingManager.RestoreDefaults();
		}

		// Token: 0x0600CDDA RID: 52698 RVA: 0x004A9F24 File Offset: 0x004A8124
		public static KeyMap LoadKeymap(string FileName)
		{
			if (!Platform.IO.File.Exists(FileName))
			{
				return null;
			}
			KeyMap keyMap = JsonConvert.DeserializeObject<KeyMap>(Platform.IO.File.ReadAllText(FileName));
			if (keyMap.CommandToSerializedInputBindings == null)
			{
				keyMap.CommandToSerializedInputBindings = new Dictionary<string, List<string>>();
			}
			return keyMap;
		}

		// Token: 0x0600CDDB RID: 52699 RVA: 0x004A9F5C File Offset: 0x004A815C
		[Obsolete("Should be using new keymap2.json")]
		public static void SaveLegacyKeymap(KeyMap Map, string FileName)
		{
			Stream stream = Platform.IO.File.OpenWrite(FileName);
			((IFormatter)new BinaryFormatter()).Serialize(stream, Map);
			stream.Close();
		}

		// Token: 0x0600CDDC RID: 52700 RVA: 0x004A9F84 File Offset: 0x004A8184
		[Obsolete("Should be using new keymap2.json")]
		public static KeyMap LoadLegacyKeymap(string FileName)
		{
			Stream stream = Platform.IO.File.OpenRead(FileName);
			KeyMap keyMap = ((IFormatter)new BinaryFormatter()).Deserialize(stream) as KeyMap;
			keyMap.PrimaryMapCommandToKeyLayer = new Dictionary<string, Dictionary<string, int>>();
			keyMap.PrimaryMapKeyToCommandLayer = new Dictionary<string, Dictionary<int, string>>();
			keyMap.SecondaryMapKeyToCommandLayer = new Dictionary<string, Dictionary<int, string>>();
			keyMap.SecondaryMapCommandToKeyLayer = new Dictionary<string, Dictionary<string, int>>();
			keyMap.PrimaryMapCommandToKeyLayer.Add("*default", keyMap.PrimaryMapCommandToKey);
			keyMap.PrimaryMapKeyToCommandLayer.Add("*default", keyMap.PrimaryMapKeyToCommand);
			keyMap.SecondaryMapKeyToCommandLayer.Add("*default", keyMap.SecondaryMapKeyToCommand);
			keyMap.SecondaryMapCommandToKeyLayer.Add("*default", keyMap.SecondaryMapCommandToKey);
			keyMap.PrimaryMapCommandToKey = null;
			keyMap.PrimaryMapKeyToCommand = null;
			keyMap.SecondaryMapCommandToKey = null;
			keyMap.SecondaryMapKeyToCommand = null;
			stream.Close();
			keyMap.upgradeLayers();
			CommandBindingManager.CleanKeymapCommands(keyMap);
			return keyMap;
		}

		// Token: 0x0600CDDD RID: 52701 RVA: 0x004AA05C File Offset: 0x004A825C
		public static void CleanKeymapCommands(KeyMap Map)
		{
		}

		// Token: 0x0600CDDE RID: 52702 RVA: 0x004AA05E File Offset: 0x004A825E
		public static void HandleNodes(XmlDataHelper xml)
		{
			xml.HandleNodes(CommandBindingManager._Nodes, null);
		}

		// Token: 0x0600CDDF RID: 52703 RVA: 0x004AA06C File Offset: 0x004A826C
		public static void HandleNavCategoryNode(XmlDataHelper xml)
		{
			string text = xml.ParseAttribute<string>("ID", null, true, null);
			if (text == null)
			{
				xml.DoneWithElement();
				return;
			}
			if (!CommandBindingManager.NavCategories.TryGetValue(text, out CommandBindingManager.CurrentLoadingNavCategory))
			{
				CommandBindingManager.CurrentLoadingNavCategory = new NavCategory
				{
					ID = text
				};
				CommandBindingManager.NavCategories.Add(text, CommandBindingManager.CurrentLoadingNavCategory);
			}
			CommandBindingManager.CurrentLoadingNavCategory.Layers = new List<string>();
			xml.HandleNodes(CommandBindingManager._NavCategorySubnodes, null);
			CommandBindingManager.CurrentLoadingNavCategory = null;
		}

		// Token: 0x0600CDE0 RID: 52704 RVA: 0x004AA0E8 File Offset: 0x004A82E8
		public static void HandleLayerNode(XmlDataHelper xml)
		{
			if (CommandBindingManager.CurrentLoadingNavCategory == null)
			{
				xml.ParseWarning("Layer node detected outside nav category context");
				xml.DoneWithElement();
				return;
			}
			string text = xml.ParseAttribute<string>("ID", null, true, null);
			if (text != null)
			{
				CommandBindingManager.CurrentLoadingNavCategory.Layers.Add(text);
			}
			xml.DoneWithElement();
		}

		// Token: 0x0600CDE1 RID: 52705 RVA: 0x004AA138 File Offset: 0x004A8338
		public static void HandleKeyboardBind(XmlDataHelper xml)
		{
			GameCommand.KeyboardBinding binding = new GameCommand.KeyboardBinding();
			binding.Key = xml.ParseAttribute<string>("Key", null, true, null);
			binding.Modifier = xml.ParseAttribute<string>("Modifier", null, false, null);
			binding.Set = xml.ParseAttribute<string>("Set", null, false, null);
			GameCommand.KeyboardBinding keyboardBinding = CommandBindingManager.CurrentParsingCommand.keyboardBindings.Find((GameCommand.KeyboardBinding bind) => bind.Key == binding.Key && bind.Modifier == binding.Modifier && bind.Set == binding.Set);
			string text;
			if (xml.TryParseAttribute<string>("Load", out text, null) && text == "Remove")
			{
				if (keyboardBinding != null)
				{
					CommandBindingManager.CurrentParsingCommand.keyboardBindings.Remove(keyboardBinding);
					return;
				}
			}
			else if (keyboardBinding == null)
			{
				CommandBindingManager.CurrentParsingCommand.keyboardBindings.Add(binding);
			}
		}

		// Token: 0x0600CDE2 RID: 52706 RVA: 0x004AA208 File Offset: 0x004A8408
		public static void HandleGamepadBind(XmlDataHelper xml)
		{
			GameCommand.GamepadBinding binding = new GameCommand.GamepadBinding();
			binding.Button = xml.ParseAttribute<string>("Button", null, true, null);
			binding.Alt = xml.ParseAttribute<bool>("Alt", false, false, null);
			GameCommand.GamepadBinding gamepadBinding = CommandBindingManager.CurrentParsingCommand.gamepadBindings.Find((GameCommand.GamepadBinding bind) => bind.Button == binding.Button && bind.Alt == binding.Alt);
			string text;
			if (xml.TryParseAttribute<string>("Load", out text, null) && text == "Remove")
			{
				if (gamepadBinding != null)
				{
					CommandBindingManager.CurrentParsingCommand.gamepadBindings.Remove(gamepadBinding);
					return;
				}
			}
			else if (gamepadBinding == null)
			{
				CommandBindingManager.CurrentParsingCommand.gamepadBindings.Add(binding);
			}
		}

		// Token: 0x0600CDE3 RID: 52707 RVA: 0x004AA2C0 File Offset: 0x004A84C0
		public static void HandleMouseBind(XmlDataHelper xml)
		{
			GameCommand.MouseBinding binding = new GameCommand.MouseBinding();
			binding.Button = xml.ParseAttribute<string>("Button", null, true, null);
			binding.Modifier = xml.ParseAttribute<string>("Modifier", null, false, null);
			GameCommand.MouseBinding mouseBinding = CommandBindingManager.CurrentParsingCommand.mouseBindings.Find((GameCommand.MouseBinding bind) => bind.Button == binding.Button && bind.Modifier == binding.Modifier);
			string text;
			if (xml.TryParseAttribute<string>("Load", out text, null) && text == "Remove")
			{
				if (mouseBinding != null)
				{
					CommandBindingManager.CurrentParsingCommand.mouseBindings.Remove(mouseBinding);
					return;
				}
			}
			else if (mouseBinding == null)
			{
				CommandBindingManager.CurrentParsingCommand.mouseBindings.Add(binding);
			}
		}

		// Token: 0x0600CDE4 RID: 52708 RVA: 0x004AA378 File Offset: 0x004A8578
		private static InputActionType ParseCommandType(string s)
		{
			InputActionType inputActionType;
			if (!(s == "Button"))
			{
				if (!(s == "PassThrough"))
				{
					if (!(s == "Value"))
					{
						throw new Exception("Unknown command type " + s);
					}
					inputActionType = InputActionType.Value;
				}
				else
				{
					inputActionType = InputActionType.PassThrough;
				}
			}
			else
			{
				inputActionType = InputActionType.Button;
			}
			return inputActionType;
		}

		// Token: 0x0600CDE5 RID: 52709 RVA: 0x004AA3CC File Offset: 0x004A85CC
		public static void HandleCommandNode(XmlDataHelper xml)
		{
			string text = xml.ParseAttribute<string>("ID", null, true, null);
			if (!CommandBindingManager.CommandsByID.TryGetValue(text, out CommandBindingManager.CurrentParsingCommand))
			{
				CommandBindingManager.CurrentParsingCommand = new GameCommand
				{
					ID = text
				};
				CommandBindingManager.CommandsByID.Add(text, CommandBindingManager.CurrentParsingCommand);
			}
			CommandBindingManager.CurrentParsingCommand.RequiresCapability = xml.ParseAttribute<CapabilityManager.CapabilityType?>("RequiresCapability", CommandBindingManager.CurrentParsingCommand.RequiresCapability, false, null);
			string text2;
			if (xml.TryParseAttribute<string>("Category", out text2, null) && CommandBindingManager.CurrentParsingCommand.Category != text2)
			{
				List<GameCommand> list;
				if (CommandBindingManager.CurrentParsingCommand.Category != null && CommandBindingManager.CommandsByCategory.TryGetValue(CommandBindingManager.CurrentParsingCommand.Category, out list))
				{
					list.Remove(CommandBindingManager.CurrentParsingCommand);
				}
				CommandBindingManager.CurrentParsingCommand.Category = text2;
				if (!CommandBindingManager.CategoriesInOrder.Contains(text2))
				{
					CommandBindingManager.CategoriesInOrder.Add(text2);
				}
				if (!CommandBindingManager.CommandsByCategory.ContainsKey(text2))
				{
					CommandBindingManager.CommandsByCategory.Add(text2, new List<GameCommand>());
				}
				CommandBindingManager.CommandsByCategory[text2].Add(CommandBindingManager.CurrentParsingCommand);
			}
			CommandBindingManager.CurrentParsingCommand.DisplayText = xml.ParseAttribute<string>("DisplayText", CommandBindingManager.CurrentParsingCommand.DisplayText, false, null);
			CommandBindingManager.CurrentParsingCommand.ConsoleDisplayText = xml.ParseAttribute<string>("ConsoleDisplayText", CommandBindingManager.CurrentParsingCommand.DisplayText, false, null);
			CommandBindingManager.CurrentParsingCommand.Layer = xml.ParseAttribute<string>("Layer", CommandBindingManager.CurrentParsingCommand.Layer ?? "*default", false, null);
			CommandBindingManager.CurrentParsingCommand.Auto = xml.ParseAttribute<string>("Auto", CommandBindingManager.CurrentParsingCommand.Auto, false, null);
			CommandBindingManager.CurrentParsingCommand.Instrument = xml.ParseAttribute<string>("Instrument", CommandBindingManager.CurrentParsingCommand.Instrument, false, null);
			CommandBindingManager.CurrentParsingCommand.InputModuleBind = xml.ParseAttribute<string>("InputModuleBind", CommandBindingManager.CurrentParsingCommand.InputModuleBind, false, null);
			CommandBindingManager.CurrentParsingCommand.SkipUpgrade = xml.ParseAttribute<bool>("SkipUpgrade", CommandBindingManager.CurrentParsingCommand.SkipUpgrade, false, null);
			CommandBindingManager.CurrentParsingCommand.Bindable = xml.ParseAttribute<bool>("Bindable", CommandBindingManager.CurrentParsingCommand.Bindable, false, null);
			CommandBindingManager.CurrentParsingCommand.Display = xml.ParseAttribute<string>("Display", CommandBindingManager.CurrentParsingCommand.Display, false, null);
			CommandBindingManager.CurrentParsingCommand.Required = xml.ParseAttribute<bool>("Required", CommandBindingManager.CurrentParsingCommand.Required, false, null);
			CommandBindingManager.CurrentParsingCommand.CanShareBindsWith = xml.ParseAttribute<List<string>>("CanShareBindsWith", CommandBindingManager.CurrentParsingCommand.CanShareBindsWith, false, null);
			CommandBindingManager.CurrentParsingCommand.Type = xml.ParseAttribute<InputActionType>("Type", CommandBindingManager.CurrentParsingCommand.Type, false, new XmlDataHelper.AttributeParser<InputActionType>.ParseDelegate(CommandBindingManager.ParseCommandType));
			CommandBindingManager.CurrentParsingCommand.UpgradeFrom = xml.ParseAttribute<string>("UpgradeFrom", CommandBindingManager.CurrentParsingCommand.ID, false, null);
			if (CapabilityManager.CurrentPlatformClassification() == CapabilityManager.PlatformClassification.Console)
			{
				CommandBindingManager.CurrentParsingCommand.DisplayText = CommandBindingManager.CurrentParsingCommand.ConsoleDisplayText;
			}
			xml.HandleNodes(CommandBindingManager._CommandSubnodes, null);
			CommandBindingManager.CurrentParsingCommand = null;
		}

		// Token: 0x0600CDE6 RID: 52710 RVA: 0x004AA6D8 File Offset: 0x004A88D8
		public static bool DoesAnyLayerConsumeKeycode(Keys code, List<string> layersToInclude)
		{
			if (layersToInclude == null)
			{
				return CommandBindingManager.DoesAnyActiveLayerConsumeKeycode(code);
			}
			for (int i = 0; i < layersToInclude.Count; i++)
			{
				if (layersToInclude[i] != null && CommandBindingManager.ConsumedKeyCodesByLayer.ContainsKey(layersToInclude[i]) && CommandBindingManager.ConsumedKeyCodesByLayer[layersToInclude[i]].Contains(code))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600CDE7 RID: 52711 RVA: 0x004AA738 File Offset: 0x004A8938
		public static bool DoesAnyActiveLayerConsumeKeycode(Keys code)
		{
			return CommandBindingManager.ConsumedKeyCodesByLayer.Any<KeyValuePair<string, HashSet<Keys>>>((KeyValuePair<string, HashSet<Keys>> l) => CommandBindingManager.CommandBindingLayers[l.Key].enabled && CommandBindingManager.CommandBindingLayers.ContainsKey(l.Key) && l.Value.Contains(code));
		}

		// Token: 0x0600CDE8 RID: 52712 RVA: 0x004AA768 File Offset: 0x004A8968
		public static bool AddKeysValueToActionAsBinding(int keycode, InputAction action, bool doExpansions = true)
		{
			if (keycode == 0)
			{
				return false;
			}
			bool flag = (keycode & 262144) > 0;
			bool flag2 = (keycode & 131072) > 0;
			bool flag3 = (keycode & 65536) > 0;
			int num = (flag ? 262144 : 0) + (flag2 ? 131072 : 0) + (flag3 ? 65536 : 0);
			List<string> list = new List<string>();
			if (flag)
			{
				list.Add("<Keyboard>/alt");
			}
			if (flag2)
			{
				list.Add("<Keyboard>/ctrl");
			}
			if (flag3)
			{
				list.Add("<Keyboard>/shift");
			}
			int num2 = keycode & 65535;
			if (doExpansions)
			{
				List<int> list2 = new List<int>();
				if (num2 == 38)
				{
					list2.Add(104);
				}
				if (num2 == 104)
				{
					list2.Add(38);
				}
				if (num2 == 40)
				{
					list2.Add(98);
				}
				if (num2 == 98)
				{
					list2.Add(40);
				}
				if (num2 == 39)
				{
					list2.Add(102);
				}
				if (num2 == 102)
				{
					list2.Add(39);
				}
				if (num2 == 37)
				{
					list2.Add(100);
				}
				if (num2 == 100)
				{
					list2.Add(37);
				}
				if (num2 == 101 && !flag3)
				{
					list2.Add(65568);
				}
				if (num2 == 103 && !flag3)
				{
					list2.Add(65573);
				}
				if (num2 == 105 && !flag3)
				{
					list2.Add(65574);
				}
				if (num2 == 97 && !flag3)
				{
					list2.Add(65576);
				}
				if (num2 == 99 && !flag3)
				{
					list2.Add(65575);
				}
				if (num2 == 33 && !flag2)
				{
					list2.Add(131177);
				}
				if (num2 == 34 && !flag2)
				{
					list2.Add(131171);
				}
				foreach (int num3 in list2)
				{
					CommandBindingManager.AddKeysValueToActionAsBinding(num3 + num, action, false);
				}
			}
			string text;
			if (CommandBindingManager.KeysToBindings.TryGetValue((Keys)num2, out text))
			{
				if (list.Count == 0)
				{
					action.AddBinding(text, null, null, null);
				}
				else if (list.Count == 1)
				{
					action.AddCompositeBinding("OneModifier", null, null).With("Binding", text, null, null).With("Modifier", list[0], null, null);
				}
				else if (list.Count == 2)
				{
					action.AddCompositeBinding("TwoModifiers", null, null).With("Binding", text, null, null).With("Modifier1", list[0], null, null)
						.With("Modifier2", list[1], null, null);
				}
				else
				{
					MetricsManager.LogWarning(string.Format("Couldn't upgrade keybind for {0} because it had too many modifiers.", action.id));
				}
				return true;
			}
			return false;
		}

		// Token: 0x0600CDE9 RID: 52713 RVA: 0x004AAA38 File Offset: 0x004A8C38
		public static KeyMap GetLegacyKeymap()
		{
			if (CommandBindingManager._legacyKeymap != null)
			{
				return CommandBindingManager._legacyKeymap;
			}
			if (!Platform.IO.File.Exists(CommandBindingManager.GetLegacyKeymapPath()))
			{
				return CommandBindingManager._legacyKeymap;
			}
			CommandBindingManager._legacyKeymap = CommandBindingManager.LoadKeymap(CommandBindingManager.GetLegacyKeymapPath());
			return CommandBindingManager._legacyKeymap;
		}

		// Token: 0x0600CDEA RID: 52714 RVA: 0x004AAA70 File Offset: 0x004A8C70
		public static void InitializeInputManager(bool AllowLegacyUpgrade = false, bool restoreLayers = false, string targetSet = null)
		{
			Debug.Log("Initializing input manager...");
			string option = Options.GetOption("KeybindSet", "");
			if (targetSet == null)
			{
				targetSet = option;
			}
			else if (option != targetSet)
			{
				Options.SetOption("KeybindSet", targetSet);
			}
			CommandBindingManager.BindingRefreshIndex++;
			List<string> list = null;
			if (restoreLayers && CommandBindingManager.CommandBindingLayers != null)
			{
				list = (from kv in CommandBindingManager.CommandBindingLayers
					where kv.Value.enabled
					select kv.Key).ToList<string>();
			}
			if (CommandBindingManager.CommandBindings != null)
			{
				foreach (CommandBinding commandBinding in CommandBindingManager.CommandBindings.Values)
				{
					commandBinding.Disable();
					commandBinding.Dispose();
				}
				CommandBindingManager.CommandBindings.Clear();
				CommandBindingManager.currentActionMap.Disable();
				CommandBindingManager.currentActionMap.Dispose();
				CommandBindingManager.CommandBindingLayers.Clear();
				CommandBindingManager.CommandBindingLayersList.Clear();
			}
			CommandBindingManager.GetLegacyKeymapPath();
			global::PlayerInputManager.ready = false;
			CommandBindingManager.currentActionMap = new InputActionMap("global");
			CommandBindingManager.currentActionMap.Disable();
			CommandBindingManager.AutoRepeatInputActions = new List<string>();
			CommandBindingManager.AutoDownAdventureInputActions = new List<CommandBinding>();
			CommandBindingManager.AutoDownInputActions = new List<CommandBinding>();
			CommandBindingManager.AutoDownPassInputActions = new List<CommandBinding>();
			CommandBindingManager.AutoDownUIInputActions = new List<CommandBinding>();
			CommandBindingManager.CommandBindings = new Dictionary<string, CommandBinding>();
			CommandBindingManager.CommandBindingLayers = new Dictionary<string, CommandBindingLayer>();
			HashSet<string> hashSet = new HashSet<string>();
			foreach (KeyValuePair<string, GameCommand> keyValuePair in CommandBindingManager.CommandsByID)
			{
				string layer = keyValuePair.Value.Layer;
				if (!CommandBindingManager.CommandBindingLayers.ContainsKey(layer))
				{
					CommandBindingManager.CommandBindingLayers.Add(layer, new CommandBindingLayer(layer));
				}
				CommandBindingManager.CommandBindingLayers[layer].Disable();
				InputAction newAction = CommandBindingManager.CommandBindingLayers[layer].AddAction(keyValuePair.Value.ID, keyValuePair.Value.Type);
				if (keyValuePair.Value.Instrument == "Yes")
				{
					newAction.started += delegate(InputAction.CallbackContext c)
					{
						Debug.Log("started: " + newAction.ToString());
					};
					newAction.canceled += delegate(InputAction.CallbackContext c)
					{
						Debug.Log("cancelled: " + newAction.ToString());
					};
					newAction.performed += delegate(InputAction.CallbackContext c)
					{
						Debug.Log("performed: " + newAction.ToString());
					};
				}
				hashSet.Add(keyValuePair.Value.ID);
				List<string> list2 = null;
				KeyMap currentMap = CommandBindingManager.CurrentMap;
				if (currentMap != null)
				{
					Dictionary<string, List<string>> commandToSerializedInputBindings = currentMap.CommandToSerializedInputBindings;
					if (commandToSerializedInputBindings != null)
					{
						commandToSerializedInputBindings.TryGetValue(keyValuePair.Value.ID, out list2);
					}
				}
				CommandBinding commandBinding2 = CommandBinding.FromSerializedFormat(newAction, keyValuePair.Value, list2, AllowLegacyUpgrade, targetSet);
				CommandBindingManager.CommandBindings.Add(keyValuePair.Value.ID, commandBinding2);
				if (keyValuePair.Value.Auto == "Repeat")
				{
					CommandBindingManager.AutoRepeatInputActions.Add(keyValuePair.Value.ID);
				}
				else if (keyValuePair.Value.Auto == "Down")
				{
					if (keyValuePair.Value.Layer.Contains("Adventure"))
					{
						CommandBindingManager.AutoDownAdventureInputActions.Add(commandBinding2);
					}
					else if (keyValuePair.Value.Layer == "UI")
					{
						CommandBindingManager.AutoDownUIInputActions.Add(commandBinding2);
					}
					else
					{
						CommandBindingManager.AutoDownInputActions.Add(commandBinding2);
					}
				}
				else if (keyValuePair.Value.Auto == "DownPass")
				{
					CommandBindingManager.AutoDownPassInputActions.Add(commandBinding2);
				}
				CommandBindingManager.CommandBindingLayers[layer].AddBinding(commandBinding2);
			}
			if (AllowLegacyUpgrade)
			{
				KeyMap currentMap2 = CommandBindingManager.CurrentMap;
				if (((currentMap2 != null) ? currentMap2.CommandToSerializedInputBindings : null) == null)
				{
					Dictionary<string, int> dictionary = new Dictionary<string, int>();
					foreach (KeyValuePair<string, List<int>> keyValuePair2 in HotkeyFavorites.LoadForUpgrade())
					{
						using (List<int>.Enumerator enumerator4 = keyValuePair2.Value.GetEnumerator())
						{
							while (enumerator4.MoveNext())
							{
								int cmdkv = enumerator4.Current;
								if (!dictionary.Any<KeyValuePair<string, int>>((KeyValuePair<string, int> kv) => kv.Value == cmdkv))
								{
									dictionary.Add(keyValuePair2.Key, cmdkv);
									break;
								}
							}
						}
					}
					foreach (KeyValuePair<string, int> keyValuePair3 in dictionary)
					{
						string text = "Dynamic";
						if (!CommandBindingManager.CommandBindingLayers.ContainsKey(text))
						{
							CommandBindingManager.CommandBindingLayers.Add(text, new CommandBindingLayer(text));
						}
						CommandBindingManager.CommandBindingLayers[text].Disable();
						InputAction inputAction = CommandBindingManager.CommandBindingLayers[text].AddAction(keyValuePair3.Key, InputActionType.Button);
						CommandBindingManager.AddKeysValueToActionAsBinding(keyValuePair3.Value, inputAction, true);
						CommandBinding commandBinding3 = CommandBinding.FromInputAction(inputAction);
						CommandBindingManager.CommandBindings.Add(keyValuePair3.Key, commandBinding3);
						CommandBindingManager.CommandBindingLayers[text].AddBinding(commandBinding3);
					}
				}
			}
			if (CommandBindingManager.CurrentMap != null && CommandBindingManager.CurrentMap.CommandToSerializedInputBindings != null)
			{
				KeyMap currentMap3 = CommandBindingManager.CurrentMap;
				foreach (KeyValuePair<string, List<string>> keyValuePair4 in ((currentMap3 != null) ? currentMap3.CommandToSerializedInputBindings : null))
				{
					string key = keyValuePair4.Key;
					if (!hashSet.Contains(keyValuePair4.Key))
					{
						hashSet.Add(keyValuePair4.Key);
						string text2 = "Dynamic";
						if (!CommandBindingManager.CommandBindingLayers.ContainsKey(text2))
						{
							CommandBindingManager.CommandBindingLayers.Add(text2, new CommandBindingLayer(text2));
						}
						CommandBindingManager.CommandBindingLayers[text2].Disable();
						InputAction inputAction2 = CommandBindingManager.CommandBindingLayers[text2].AddAction(keyValuePair4.Key, InputActionType.Button);
						inputAction2.LoadSerializedBindings(keyValuePair4.Value);
						CommandBinding commandBinding4 = CommandBinding.FromInputAction(inputAction2);
						CommandBindingManager.CommandBindings.Add(keyValuePair4.Key, commandBinding4);
						CommandBindingManager.CommandBindingLayers[text2].AddBinding(commandBinding4);
					}
				}
			}
			CommandBindingManager.currentActionMap.Enable();
			foreach (KeyValuePair<string, CommandBinding> keyValuePair5 in CommandBindingManager.CommandBindings)
			{
				if (!keyValuePair5.Value.enabled)
				{
					keyValuePair5.Value.Enable();
				}
			}
			foreach (KeyValuePair<string, CommandBindingLayer> keyValuePair6 in CommandBindingManager.CommandBindingLayers)
			{
				if (!keyValuePair6.Value.enabled)
				{
					keyValuePair6.Value.Enable();
				}
			}
			if (global::UnityEngine.InputSystem.Keyboard.current != null)
			{
				global::UnityEngine.InputSystem.Keyboard.current.upArrowKey.SetDisplayName("\u0018");
				global::UnityEngine.InputSystem.Keyboard.current.downArrowKey.SetDisplayName("\u0019");
				global::UnityEngine.InputSystem.Keyboard.current.rightArrowKey.SetDisplayName("\u001a");
				global::UnityEngine.InputSystem.Keyboard.current.leftArrowKey.SetDisplayName("\u001b");
				global::UnityEngine.InputSystem.Keyboard.current.pageUpKey.SetDisplayName("PgUp");
				global::UnityEngine.InputSystem.Keyboard.current.pageDownKey.SetDisplayName("PgDown");
				global::UnityEngine.InputSystem.Keyboard.current.homeKey.SetDisplayName("Home");
				global::UnityEngine.InputSystem.Keyboard.current.endKey.SetDisplayName("End");
				global::UnityEngine.InputSystem.Keyboard.current.insertKey.SetDisplayName("Insert");
				global::UnityEngine.InputSystem.Keyboard.current.deleteKey.SetDisplayName("Delete");
				global::UnityEngine.InputSystem.Keyboard.current.numpadEnterKey.SetDisplayName("NumEnter");
			}
			CommandBindingManager.ConsumedKeyCodesByLayer = new Dictionary<string, HashSet<Keys>>();
			foreach (KeyValuePair<string, CommandBindingLayer> keyValuePair7 in CommandBindingManager.CommandBindingLayers)
			{
				CommandBindingManager.ConsumedKeyCodesByLayer.Add(keyValuePair7.Key, new HashSet<Keys>());
				HashSet<Keys> keyLayer = CommandBindingManager.ConsumedKeyCodesByLayer[keyValuePair7.Key];
				Action<int> <>9__6;
				foreach (CommandBinding commandBinding5 in keyValuePair7.Value.actions)
				{
					List<int> list3 = commandBinding5.GetConsoleKeycodes().ToList<int>();
					Action<int> action;
					if ((action = <>9__6) == null)
					{
						action = (<>9__6 = delegate(int b)
						{
							if ((b & 65535) == 187)
							{
								keyLayer.Add(Keys.Add + (b & -65536));
							}
							if ((b & 65535) == 189)
							{
								keyLayer.Add(Keys.Subtract + (b & -65536));
							}
							if ((b & 65535) == 190)
							{
								keyLayer.Add(Keys.Decimal + (b & -65536));
							}
							keyLayer.Add((Keys)b);
						});
					}
					list3.ForEach(action);
				}
			}
			ControlManager.DisableAllLayers();
			foreach (KeyValuePair<string, CommandBindingLayer> keyValuePair8 in CommandBindingManager.CommandBindingLayers)
			{
				ControlManager.EnableLayer(keyValuePair8.Key);
			}
			foreach (KeyValuePair<string, GameCommand> keyValuePair9 in CommandBindingManager.CommandsByID)
			{
				if (!string.IsNullOrEmpty(keyValuePair9.Value.InputModuleBind))
				{
					try
					{
						CommandBinding commandBinding6 = CommandBindingManager.CommandBindings[keyValuePair9.Key];
						InputSystemUIInputModule component = GameObject.Find("EventSystem").GetComponent<InputSystemUIInputModule>();
						if (keyValuePair9.Value.InputModuleBind == "submit")
						{
							component.submit = CommandBindingManager.InputActionReferenceCreateNaughty(commandBinding6._inputAction);
						}
						if (keyValuePair9.Value.InputModuleBind == "cancel")
						{
							component.cancel = CommandBindingManager.InputActionReferenceCreateNaughty(commandBinding6._inputAction);
						}
						if (keyValuePair9.Value.InputModuleBind == "move")
						{
							component.move = CommandBindingManager.InputActionReferenceCreateNaughty(commandBinding6._inputAction);
						}
						if (keyValuePair9.Value.InputModuleBind == "leftClick")
						{
							component.leftClick = CommandBindingManager.InputActionReferenceCreateNaughty(commandBinding6._inputAction);
						}
						if (keyValuePair9.Value.InputModuleBind == "rightClick")
						{
							component.rightClick = CommandBindingManager.InputActionReferenceCreateNaughty(commandBinding6._inputAction);
						}
						if (keyValuePair9.Value.InputModuleBind == "scrollWheel")
						{
							component.scrollWheel = CommandBindingManager.InputActionReferenceCreateNaughty(commandBinding6._inputAction);
						}
					}
					catch (Exception ex)
					{
						string text3 = "Exception setting up nav on command ";
						KeyValuePair<string, GameCommand> keyValuePair10 = keyValuePair9;
						MetricsManager.LogException(text3 + keyValuePair10.ToString(), ex, "game_exception");
					}
				}
			}
			CommandBindingManager.CommandBindingLayersList = CommandBindingManager.CommandBindingLayers.Values.ToList<CommandBindingLayer>();
			ControlManager.ResetBindingCaches();
			global::PlayerInputManager.ready = true;
			if (list != null)
			{
				foreach (KeyValuePair<string, CommandBindingLayer> keyValuePair11 in CommandBindingManager.CommandBindingLayers)
				{
					if (list.Contains(keyValuePair11.Key))
					{
						keyValuePair11.Value.Enable();
					}
					else
					{
						keyValuePair11.Value.Disable();
					}
				}
			}
			if (CommandBindingManager.ctrlAction == null)
			{
				CommandBindingManager.ctrlAction = new InputAction("_internal_CtrlModifierAction", InputActionType.Value, null, null, null, null);
				CommandBindingManager.ctrlAction.AddBinding("<Keyboard>/ctrl", null, null, null);
				CommandBindingManager.ctrlAction.Enable();
			}
			if (CommandBindingManager.altAction == null)
			{
				CommandBindingManager.altAction = new InputAction("_internal_AltModifierAction", InputActionType.Value, null, null, null, null);
				CommandBindingManager.altAction.AddBinding("<Keyboard>/alt", null, null, null);
				CommandBindingManager.altAction.Enable();
			}
			if (CommandBindingManager.shiftAction == null)
			{
				CommandBindingManager.shiftAction = new InputAction("_internal_ShiftModifierAction", InputActionType.Value, null, null, null, null);
				CommandBindingManager.shiftAction.AddBinding("<Keyboard>/shift", null, null, null);
				CommandBindingManager.shiftAction.Enable();
			}
		}

		// Token: 0x0600CDEB RID: 52715 RVA: 0x004AB7B0 File Offset: 0x004A99B0
		public static InputActionReference InputActionReferenceCreateNaughty(InputAction from)
		{
			if (CommandBindingManager.dummyAsset == null)
			{
				CommandBindingManager.dummyAsset = new InputActionAsset();
			}
			InputActionReference inputActionReference = ScriptableObject.CreateInstance<InputActionReference>();
			CommandBindingManager.SetPrivateFieldValue<InputAction>(inputActionReference, "m_Action", from);
			CommandBindingManager.SetPrivateFieldValue<InputActionAsset>(inputActionReference, "m_Asset", CommandBindingManager.dummyAsset);
			CommandBindingManager.SetPrivateFieldValue<string>(inputActionReference, "m_ActionId", from.id.ToString());
			return inputActionReference;
		}

		// Token: 0x0600CDEC RID: 52716 RVA: 0x004AB814 File Offset: 0x004A9A14
		public static void SetPrivatePropertyValue<T>(object obj, string propName, T val)
		{
			Type type = obj.GetType();
			if (type.GetProperty(propName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic) == null)
			{
				throw new ArgumentOutOfRangeException("propName", string.Format("Property {0} was not found in Type {1}", propName, obj.GetType().FullName));
			}
			type.InvokeMember(propName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.SetProperty, null, obj, new object[] { val });
		}

		// Token: 0x0600CDED RID: 52717 RVA: 0x004AB878 File Offset: 0x004A9A78
		public static void SetPrivateFieldValue<T>(object obj, string propName, T val)
		{
			FieldInfo field = obj.GetType().GetField(propName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			if (field == null)
			{
				throw new ArgumentOutOfRangeException("propName", string.Format("Property {0} was not found in Type {1}", propName, obj.GetType().FullName));
			}
			field.SetValue(obj, val);
		}

		// Token: 0x0600CDEE RID: 52718 RVA: 0x004AB8CC File Offset: 0x004A9ACC
		public static string GetBindingDisplayString(List<string> binding)
		{
			string text2;
			using (InputAction inputAction = new InputAction())
			{
				inputAction.LoadSerializedBindings(binding);
				string text = inputAction.GetBindingDisplayString(0, (InputBinding.DisplayStringOptions)0);
				if (CapabilityManager.CurrentPlatformClassification() == CapabilityManager.PlatformClassification.Console)
				{
					text = CommandBindingManager.GetConsoleOverrideDisplayName(text);
				}
				text2 = text;
			}
			return text2;
		}

		// Token: 0x0600CDEF RID: 52719 RVA: 0x004AB920 File Offset: 0x004A9B20
		[ModSensitiveCacheInit]
		public static void LoadCommands()
		{
			CommandBindingManager.CommandsByCategory = new Dictionary<string, List<GameCommand>>();
			CommandBindingManager.CommandsByID = new Dictionary<string, GameCommand>();
			CommandBindingManager.CategoriesInOrder = new List<string>();
			foreach (XmlDataHelper xmlDataHelper in DataManager.YieldXMLStreamsWithRoot("commands", true, true))
			{
				CommandBindingManager.HandleNodes(xmlDataHelper);
			}
			CommandBindingManager.CurrentMap = null;
			using (ScopeDisposedList<GameCommand> fromPool = ScopeDisposedList<GameCommand>.GetFromPool())
			{
				foreach (KeyValuePair<string, GameCommand> keyValuePair in CommandBindingManager.CommandsByID)
				{
					if (!CapabilityManager.HasCapability(keyValuePair.Value.RequiresCapability))
					{
						fromPool.Add(keyValuePair.Value);
					}
				}
				foreach (GameCommand gameCommand in fromPool)
				{
					if (CommandBindingManager.CommandsByID.ContainsKey(gameCommand.ID))
					{
						CommandBindingManager.CommandsByID.Remove(gameCommand.ID);
					}
					List<GameCommand> list;
					if (CommandBindingManager.CommandsByCategory.TryGetValue(gameCommand.Category, out list))
					{
						list.Remove(gameCommand);
						if (list.Count == 0)
						{
							CommandBindingManager.CommandsByCategory.Remove(gameCommand.Category);
							CommandBindingManager.CategoriesInOrder.Remove(gameCommand.Category);
						}
					}
				}
				bool flag = false;
				try
				{
					if (Platform.IO.File.Exists(CommandBindingManager.GetCurrentKeymapPath()))
					{
						try
						{
							CommandBindingManager.CurrentMap = CommandBindingManager.LoadKeymap(CommandBindingManager.GetCurrentKeymapPath());
							goto IL_0184;
						}
						catch (Exception ex)
						{
							flag = true;
							MetricsManager.LogException("Load keymap2.json", ex, "game_exception");
							goto IL_0184;
						}
					}
					flag = true;
					IL_0184:;
				}
				catch (Exception ex2)
				{
					MetricsManager.LogException("Loading keymapping", ex2, "game_exception");
				}
				if (CommandBindingManager.CurrentMap == null)
				{
					CommandBindingManager.CurrentMap = new KeyMap();
				}
				CommandBindingManager.InitializeInputManager(flag, false, null);
			}
		}

		// Token: 0x0600CDF0 RID: 52720 RVA: 0x004ABB84 File Offset: 0x004A9D84
		public static string GetGamepadAltBindings()
		{
			if (CommandBindingManager.CommandBindings.ContainsKey("GamepadAlt"))
			{
				IEnumerable<string> serializedBindingsForCommand = CommandBindingManager.GetSerializedBindingsForCommand("GamepadAlt");
				return ((serializedBindingsForCommand != null) ? serializedBindingsForCommand.FirstOrDefault<string>() : null) ?? "<Gamepad>/leftTrigger";
			}
			return "<Gamepad>/leftTrigger";
		}

		// Token: 0x17000BE4 RID: 3044
		// (get) Token: 0x0600CDF1 RID: 52721 RVA: 0x004ABBBC File Offset: 0x004A9DBC
		private static int FrameKeyboardModifier
		{
			get
			{
				if (CommandBindingManager._FrameKeyboardModifier != -1)
				{
					return CommandBindingManager._FrameKeyboardModifier;
				}
				CommandBindingManager._FrameKeyboardModifier = 0;
				InputAction inputAction = CommandBindingManager.ctrlAction;
				if (inputAction != null && inputAction.IsPressed())
				{
					CommandBindingManager._FrameKeyboardModifier |= 1;
				}
				InputAction inputAction2 = CommandBindingManager.shiftAction;
				if (inputAction2 != null && inputAction2.IsPressed())
				{
					CommandBindingManager._FrameKeyboardModifier |= 4;
				}
				InputAction inputAction3 = CommandBindingManager.altAction;
				if (inputAction3 != null && inputAction3.IsPressed())
				{
					CommandBindingManager._FrameKeyboardModifier |= 2;
				}
				return CommandBindingManager._FrameKeyboardModifier;
			}
		}

		// Token: 0x0600CDF2 RID: 52722 RVA: 0x004ABC3F File Offset: 0x004A9E3F
		public static int GetFrameKeyboardModifier()
		{
			return CommandBindingManager.FrameKeyboardModifier;
		}

		// Token: 0x17000BE5 RID: 3045
		// (get) Token: 0x0600CDF3 RID: 52723 RVA: 0x004ABC46 File Offset: 0x004A9E46
		private static int FrameGamepadModifier
		{
			get
			{
				if (CommandBindingManager._FrameGamepadModifier != -1)
				{
					return CommandBindingManager._FrameGamepadModifier;
				}
				CommandBindingManager._FrameGamepadModifier = 0;
				InputAction inputAction = CommandBindingManager.gamepadAltAction;
				if (inputAction != null && inputAction.IsPressed())
				{
					CommandBindingManager._FrameGamepadModifier |= 8;
				}
				return CommandBindingManager._FrameGamepadModifier;
			}
		}

		// Token: 0x0600CDF4 RID: 52724 RVA: 0x004ABC80 File Offset: 0x004A9E80
		public static void LateUpdate()
		{
			CommandBindingManager._FrameGamepadModifier = -1;
			CommandBindingManager._FrameKeyboardModifier = -1;
		}

		// Token: 0x0600CDF5 RID: 52725 RVA: 0x004ABC8E File Offset: 0x004A9E8E
		public static int GetFrameGamepadModifier()
		{
			return CommandBindingManager.FrameGamepadModifier;
		}

		// Token: 0x0600CDF6 RID: 52726 RVA: 0x004ABC98 File Offset: 0x004A9E98
		public static async Task<List<string>> GetRebindAsync(ControlManager.InputDeviceType deviceType, bool AllowGamepadAlt = true, InputActionType type = InputActionType.Button)
		{
			List<string> list;
			try
			{
				await The.UiContext;
				using (InputActionRebindingExtensions.RebindingOperation rebind = new InputActionRebindingExtensions.RebindingOperation())
				{
					TaskCompletionSource<List<string>> taskComplete = new TaskCompletionSource<List<string>>();
					rebind.WithTimeout(10f);
					rebind.WithExpectedControlType((type == InputActionType.Button) ? "Button" : "Stick");
					foreach (KeyValuePair<string, ControlManager.InputDeviceType> keyValuePair in CommandBindingManager.ControlPrefixType)
					{
						if (keyValuePair.Value == deviceType)
						{
							rebind.WithControlsHavingToMatchPath(keyValuePair.Key);
						}
					}
					string[] array = CommandBindingManager.KeyboardModifiers;
					for (int i = 0; i < array.Length; i++)
					{
						rebind.WithControlsExcluding(array[i]);
					}
					array = CommandBindingManager.IgnoredControls;
					for (int i = 0; i < array.Length; i++)
					{
						rebind.WithControlsExcluding(array[i]);
					}
					string gamepadbind = null;
					if (AllowGamepadAlt)
					{
						foreach (InputBinding inputBinding in CommandBindingManager.CommandBindings["GamepadAlt"]._inputAction.bindings)
						{
							gamepadbind = inputBinding.path;
							rebind.WithControlsExcluding(inputBinding.path);
						}
					}
					foreach (InputControl inputControl in CommandBindingManager.CommandBindings["Cancel"]._inputAction.controls)
					{
						if (inputControl.device is global::UnityEngine.InputSystem.Keyboard)
						{
							rebind.WithCancelingThrough(inputControl);
						}
					}
					rebind.OnCancel(delegate(InputActionRebindingExtensions.RebindingOperation _)
					{
						taskComplete.TrySetResult(null);
						ControlManager.WaitForKeyup("Cancel");
					});
					rebind.OnApplyBinding(delegate(InputActionRebindingExtensions.RebindingOperation _, string bind)
					{
						global::UnityEngine.InputSystem.Keyboard current = global::UnityEngine.InputSystem.Keyboard.current;
						bool flag = current != null && current.ctrlKey.isPressed;
						global::UnityEngine.InputSystem.Keyboard current2 = global::UnityEngine.InputSystem.Keyboard.current;
						bool flag2 = current2 != null && current2.altKey.isPressed;
						global::UnityEngine.InputSystem.Keyboard current3 = global::UnityEngine.InputSystem.Keyboard.current;
						bool flag3 = current3 != null && current3.shiftKey.isPressed;
						bool flag4 = gamepadbind != null && InputSystem.FindControl(gamepadbind).IsPressed(0f);
						MetricsManager.LogInfo(string.Format("Got bind {0} c:{1} a:{2} s:{3} g:{4}", new object[] { bind, flag, flag2, flag3, flag4 }));
						int num = (flag ? 1 : 0) + (flag2 ? 1 : 0) + (flag3 ? 1 : 0) + (flag4 ? 1 : 0);
						using (InputAction inputAction = new InputAction())
						{
							if (num == 0)
							{
								inputAction.AddBinding(bind, null, null, null);
							}
							else if (num == 1)
							{
								InputActionSetupExtensions.CompositeSyntax compositeSyntax = inputAction.AddCompositeBinding("OneModifier", null, null);
								compositeSyntax.With("Binding", bind, null, null);
								if (flag4)
								{
									compositeSyntax.With("Modifier", gamepadbind, null, null);
								}
								if (flag)
								{
									compositeSyntax.With("Modifier", "<Keyboard>/ctrl", null, null);
								}
								if (flag3)
								{
									compositeSyntax.With("Modifier", "<Keyboard>/shift", null, null);
								}
								if (flag2)
								{
									compositeSyntax.With("Modifier", "<Keyboard>/alt", null, null);
								}
							}
							else
							{
								InputActionSetupExtensions.CompositeSyntax compositeSyntax2 = inputAction.AddCompositeBinding("TwoModifiers", null, null);
								compositeSyntax2.With("Binding", bind, null, null);
								int num2 = 1;
								if (flag)
								{
									compositeSyntax2.With(string.Format("Modifier{0}", num2++), "<Keyboard>/ctrl", null, null);
								}
								if (flag3)
								{
									compositeSyntax2.With(string.Format("Modifier{0}", num2++), "<Keyboard>/shift", null, null);
								}
								if (flag2 && num2 != 3)
								{
									compositeSyntax2.With(string.Format("Modifier{0}", num2++), "<Keyboard>/alt", null, null);
								}
							}
							taskComplete.SetResult(inputAction.SerializedFormat());
						}
					});
					rebind.Start();
					list = await taskComplete.Task;
				}
			}
			finally
			{
			}
			return list;
		}

		// Token: 0x0600CDF7 RID: 52727 RVA: 0x004ABCEC File Offset: 0x004A9EEC
		public static IEnumerable<string> GetSerializedBindingsForCommand(string command)
		{
			KeyMap currentMap = CommandBindingManager.CurrentMap;
			List<string> list;
			if (((currentMap != null) ? currentMap.CommandToSerializedInputBindings : null) != null && CommandBindingManager.CurrentMap.CommandToSerializedInputBindings.TryGetValue(command, out list) && list != null)
			{
				return list;
			}
			CommandBinding commandBinding;
			if (CommandBindingManager.CommandBindings.TryGetValue(command, out commandBinding))
			{
				return commandBinding._inputAction.SerializedFormat();
			}
			return new List<string>();
		}

		// Token: 0x0600CDF8 RID: 52728 RVA: 0x004ABD48 File Offset: 0x004A9F48
		public static void ReplaceCommandBindingIndex(string command, int index, List<string> binding, ControlManager.InputDeviceType deviceType)
		{
			List<string> list = CommandBindingManager.ReplaceSerializedBindingFormatIndex(CommandBindingManager.GetSerializedBindingsForCommand(command), index, binding, deviceType).ToList<string>();
			KeyMap currentMap = CommandBindingManager.CurrentMap;
			if (currentMap.CommandToSerializedInputBindings == null)
			{
				currentMap.CommandToSerializedInputBindings = new Dictionary<string, List<string>>();
			}
			CommandBindingManager.CurrentMap.CommandToSerializedInputBindings[command] = list;
		}

		// Token: 0x0600CDF9 RID: 52729 RVA: 0x004ABD94 File Offset: 0x004A9F94
		public static void RemoveCommandBinding(string command, List<string> binding)
		{
			List<string> list = new List<string>();
			foreach (IEnumerable<string> enumerable in CommandBindingManager.SplitSerializedBindings(CommandBindingManager.GetSerializedBindingsForCommand(command)))
			{
				if (!enumerable.SequenceEqual<string>(binding))
				{
					foreach (string text in enumerable)
					{
						list.Add(text);
					}
				}
			}
			KeyMap currentMap = CommandBindingManager.CurrentMap;
			if (currentMap.CommandToSerializedInputBindings == null)
			{
				currentMap.CommandToSerializedInputBindings = new Dictionary<string, List<string>>();
			}
			CommandBindingManager.CurrentMap.CommandToSerializedInputBindings[command] = list;
		}

		// Token: 0x0600CDFA RID: 52730 RVA: 0x004ABE54 File Offset: 0x004AA054
		public static async Task RemoveCommandBindingAsync(string command, int index)
		{
			await The.UiContext;
			CommandBindingManager.RemoveCommandBinding(command, index);
			CommandBindingManager.InitializeInputManager(false, true, null);
			CommandBindingManager.SaveCurrentKeymap();
		}

		// Token: 0x0600CDFB RID: 52731 RVA: 0x004ABEA0 File Offset: 0x004AA0A0
		public static void RemoveCommandBinding(string command, int index)
		{
			List<string> list = new List<string>();
			int num = 0;
			foreach (IEnumerable<string> enumerable in CommandBindingManager.SplitSerializedBindings(CommandBindingManager.GetSerializedBindingsForCommand(command)))
			{
				if (num++ != index)
				{
					foreach (string text in enumerable)
					{
						list.Add(text);
					}
				}
			}
			KeyMap currentMap = CommandBindingManager.CurrentMap;
			if (currentMap.CommandToSerializedInputBindings == null)
			{
				currentMap.CommandToSerializedInputBindings = new Dictionary<string, List<string>>();
			}
			CommandBindingManager.CurrentMap.CommandToSerializedInputBindings[command] = list;
		}

		// Token: 0x0600CDFC RID: 52732 RVA: 0x004ABF68 File Offset: 0x004AA168
		private static IEnumerable<IEnumerable<string>> SplitSerializedBindings(IEnumerable<string> original)
		{
			if (original == null)
			{
				yield break;
			}
			int numRemaining = 0;
			int index = 0;
			int num = 0;
			foreach (string text in original)
			{
				int num2;
				if (text == InputSystemExtensions.COMPOSITE)
				{
					numRemaining += 2;
				}
				else if (text == "OneModifier")
				{
					num2 = numRemaining;
					numRemaining = num2 + 1;
				}
				else if (text == "TwoModifiers")
				{
					numRemaining += 2;
				}
				num2 = index;
				index = num2 + 1;
				num2 = numRemaining;
				numRemaining = num2 - 1;
				if (num2 <= 0)
				{
					numRemaining = 0;
					yield return original.Skip<string>(num).Take<string>(index - num);
					num = index;
				}
			}
			IEnumerator<string> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x0600CDFD RID: 52733 RVA: 0x004ABF78 File Offset: 0x004AA178
		private static ControlManager.InputDeviceType GetInputDeviceTypeFromBinding(IEnumerable<string> binding)
		{
			foreach (string text in binding)
			{
				ControlManager.InputDeviceType inputDeviceTypeFromBindingPath = CommandBindingManager.GetInputDeviceTypeFromBindingPath(text);
				if (inputDeviceTypeFromBindingPath != ControlManager.InputDeviceType.Unknown)
				{
					return inputDeviceTypeFromBindingPath;
				}
			}
			return ControlManager.InputDeviceType.Unknown;
		}

		// Token: 0x0600CDFE RID: 52734 RVA: 0x004ABFCC File Offset: 0x004AA1CC
		private static IEnumerable<string> ReplaceSerializedBindingFormatIndex(IEnumerable<string> original, int replaceIndex, IEnumerable<string> replacement, ControlManager.InputDeviceType deviceType)
		{
			bool replaceYielded = false;
			int index = 0;
			foreach (IEnumerable<string> enumerable in CommandBindingManager.SplitSerializedBindings(original))
			{
				IEnumerator<string> enumerator2;
				if (CommandBindingManager.GetInputDeviceTypeFromBinding(enumerable) == deviceType)
				{
					int num = index;
					index = num + 1;
					if (num == replaceIndex)
					{
						foreach (string text in replacement)
						{
							yield return text;
						}
						enumerator2 = null;
						replaceYielded = true;
						continue;
					}
				}
				foreach (string text2 in enumerable)
				{
					yield return text2;
				}
				enumerator2 = null;
			}
			IEnumerator<IEnumerable<string>> enumerator = null;
			if (!replaceYielded)
			{
				foreach (string text3 in replacement)
				{
					yield return text3;
				}
				IEnumerator<string> enumerator2 = null;
			}
			yield break;
			yield break;
		}

		// Token: 0x0600CDFF RID: 52735 RVA: 0x004ABFF1 File Offset: 0x004AA1F1
		public static IEnumerable<GameCommand> GetCommandsWithBinding(IEnumerable<string> binding, Predicate<GameCommand> filter = null)
		{
			foreach (GameCommand gameCommand in CommandBindingManager.CommandsByID.Values)
			{
				if ((filter == null || filter(gameCommand)) && CommandBindingManager.CommandUsesBinding(gameCommand, binding))
				{
					yield return gameCommand;
				}
			}
			Dictionary<string, GameCommand>.ValueCollection.Enumerator enumerator = default(Dictionary<string, GameCommand>.ValueCollection.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x0600CE00 RID: 52736 RVA: 0x004AC008 File Offset: 0x004AA208
		public static bool TryGetCommandFromBinding(IEnumerable<string> binding, Predicate<GameCommand> filter, out GameCommand result)
		{
			result = null;
			using (IEnumerator<GameCommand> enumerator = CommandBindingManager.GetCommandsWithBinding(binding, filter).GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					GameCommand gameCommand = enumerator.Current;
					result = gameCommand;
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600CE01 RID: 52737 RVA: 0x004AC05C File Offset: 0x004AA25C
		public static bool CommandUsesBinding(GameCommand command, IEnumerable<string> binding)
		{
			return CommandBindingManager.CommandUsesBinding(command.ID, binding);
		}

		// Token: 0x0600CE02 RID: 52738 RVA: 0x004AC06C File Offset: 0x004AA26C
		public static bool CommandUsesBinding(string commandId, IEnumerable<string> binding)
		{
			return CommandBindingManager.SplitSerializedBindings(CommandBindingManager.GetSerializedBindingsForCommand(commandId)).Any<IEnumerable<string>>((IEnumerable<string> s) => s.SequenceEqual<string>(binding));
		}

		// Token: 0x04004F82 RID: 20354
		[ModSensitiveStaticCache(false)]
		public static Dictionary<string, List<GameCommand>> CommandsByCategory;

		// Token: 0x04004F83 RID: 20355
		[ModSensitiveStaticCache(false)]
		public static Dictionary<string, GameCommand> CommandsByID;

		// Token: 0x04004F84 RID: 20356
		[ModSensitiveStaticCache(false)]
		public static List<string> CategoriesInOrder;

		// Token: 0x04004F85 RID: 20357
		[ModSensitiveStaticCache(true)]
		public static Dictionary<string, NavCategory> NavCategories;

		// Token: 0x04004F86 RID: 20358
		public static KeyMap CurrentMap;

		// Token: 0x04004F87 RID: 20359
		private static Dictionary<string, ControlManager.InputDeviceType> ControlPrefixType = new Dictionary<string, ControlManager.InputDeviceType>(StringComparer.InvariantCultureIgnoreCase)
		{
			{
				"<Keyboard>",
				ControlManager.InputDeviceType.Keyboard
			},
			{
				"<Mouse>",
				ControlManager.InputDeviceType.Keyboard
			},
			{
				"<Pointer>",
				ControlManager.InputDeviceType.Keyboard
			},
			{
				"<Gamepad>",
				ControlManager.InputDeviceType.Gamepad
			}
		};

		// Token: 0x04004F88 RID: 20360
		[TupleElementNames(new string[] { "defaultName", "nameOverride" })]
		private static ValueTuple<string, string>[] SwitchDisplayNameOverrides = new ValueTuple<string, string>[]
		{
			new ValueTuple<string, string>("D-Pad\\/? ?", ""),
			new ValueTuple<string, string>("Left Stick$", "Left Stick Press"),
			new ValueTuple<string, string>("Right Stick$", "Right Stick Press"),
			new ValueTuple<string, string>("Left Stick/", "Left Stick "),
			new ValueTuple<string, string>("Right Stick/", "Right Stick "),
			new ValueTuple<string, string>("LS", "Left Stick"),
			new ValueTuple<string, string>("RS", "Right Stick"),
			new ValueTuple<string, string>("LT", "ZL"),
			new ValueTuple<string, string>("RT", "ZR")
		};

		// Token: 0x04004F89 RID: 20361
		private static Regex _deviceFromBinding = new Regex("^<?(?<device>[^\\/>]+)>?\\/(?<control>.*)", RegexOptions.Compiled);

		// Token: 0x04004F8A RID: 20362
		private static string[] default_exclusions = new string[] { "Chargen" };

		// Token: 0x04004F8B RID: 20363
		public static readonly string[] CoreLoopExcludes = new string[] { "Chargen", "Actions" };

		// Token: 0x04004F8C RID: 20364
		private static Dictionary<string, Action<XmlDataHelper>> _Nodes = new Dictionary<string, Action<XmlDataHelper>>
		{
			{
				"commands",
				new Action<XmlDataHelper>(CommandBindingManager.HandleNodes)
			},
			{
				"command",
				new Action<XmlDataHelper>(CommandBindingManager.HandleCommandNode)
			},
			{
				"navcategory",
				new Action<XmlDataHelper>(CommandBindingManager.HandleNavCategoryNode)
			}
		};

		// Token: 0x04004F8D RID: 20365
		private static Dictionary<string, Action<XmlDataHelper>> _CommandSubnodes = new Dictionary<string, Action<XmlDataHelper>>
		{
			{
				"keyboardBind",
				new Action<XmlDataHelper>(CommandBindingManager.HandleKeyboardBind)
			},
			{
				"gamepadBind",
				new Action<XmlDataHelper>(CommandBindingManager.HandleGamepadBind)
			},
			{
				"mouseBind",
				new Action<XmlDataHelper>(CommandBindingManager.HandleMouseBind)
			}
		};

		// Token: 0x04004F8E RID: 20366
		private static Dictionary<string, Action<XmlDataHelper>> _NavCategorySubnodes = new Dictionary<string, Action<XmlDataHelper>> { 
		{
			"layer",
			new Action<XmlDataHelper>(CommandBindingManager.HandleLayerNode)
		} };

		// Token: 0x04004F8F RID: 20367
		private static NavCategory CurrentLoadingNavCategory;

		// Token: 0x04004F90 RID: 20368
		private static GameCommand CurrentParsingCommand;

		// Token: 0x04004F91 RID: 20369
		public static InputActionMap currentActionMap;

		// Token: 0x04004F92 RID: 20370
		public static Dictionary<string, HashSet<Keys>> ConsumedKeyCodesByLayer;

		// Token: 0x04004F93 RID: 20371
		public static Dictionary<string, CommandBinding> CommandBindings;

		// Token: 0x04004F94 RID: 20372
		public static Dictionary<string, CommandBindingLayer> CommandBindingLayers;

		// Token: 0x04004F95 RID: 20373
		public static List<CommandBindingLayer> CommandBindingLayersList;

		// Token: 0x04004F96 RID: 20374
		public static List<CommandBinding> AutoDownAdventureInputActions;

		// Token: 0x04004F97 RID: 20375
		public static List<CommandBinding> AutoDownInputActions;

		// Token: 0x04004F98 RID: 20376
		public static List<CommandBinding> AutoDownPassInputActions;

		// Token: 0x04004F99 RID: 20377
		public static List<CommandBinding> AutoDownUIInputActions;

		// Token: 0x04004F9A RID: 20378
		public static List<string> AutoRepeatInputActions;

		// Token: 0x04004F9B RID: 20379
		private static KeyMap _legacyKeymap;

		// Token: 0x04004F9C RID: 20380
		public static int BindingRefreshIndex = 0;

		// Token: 0x04004F9D RID: 20381
		private static InputActionAsset dummyAsset = null;

		// Token: 0x04004F9E RID: 20382
		public static Dictionary<string, CommandBindingManager.ModifierFlags> keyboardModifierFlags = new Dictionary<string, CommandBindingManager.ModifierFlags>
		{
			{
				"<Keyboard>/ctrl",
				CommandBindingManager.ModifierFlags.ctrl
			},
			{
				"<Keyboard>/shift",
				CommandBindingManager.ModifierFlags.shift
			},
			{
				"<Keyboard>/alt",
				CommandBindingManager.ModifierFlags.alt
			},
			{
				"<Keyboard>/leftCtrl",
				CommandBindingManager.ModifierFlags.ctrl
			},
			{
				"<Keyboard>/rightCtrl",
				CommandBindingManager.ModifierFlags.ctrl
			},
			{
				"<Keyboard>/leftShift",
				CommandBindingManager.ModifierFlags.shift
			},
			{
				"<Keyboard>/rightShift",
				CommandBindingManager.ModifierFlags.shift
			},
			{
				"<Keyboard>/leftAlt",
				CommandBindingManager.ModifierFlags.alt
			},
			{
				"<Keyboard>/rightAlt",
				CommandBindingManager.ModifierFlags.alt
			}
		};

		// Token: 0x04004F9F RID: 20383
		private static string[] KeyboardModifiers = new string[] { "<Keyboard>/ctrl", "<Keyboard>/shift", "<Keyboard>/alt", "<Keyboard>/leftCtrl", "<Keyboard>/rightCtrl", "<Keyboard>/leftShift", "<Keyboard>/rightShift", "<Keyboard>/leftAlt", "<Keyboard>/rightAlt", "<Keyboard>/anyKey" };

		// Token: 0x04004FA0 RID: 20384
		private static string[] IgnoredControls = new string[] { "<Gamepad>/leftStick/x", "<Gamepad>/leftStick/y", "<Gamepad>/rightStick/x", "<Gamepad>/rightStick/y", "<Gamepad>/dpad/x", "<Gamepad>/dpad/y", "<Gamepad>/leftTriggerButton", "<Gamepad>/rightTriggerButton" };

		// Token: 0x04004FA1 RID: 20385
		public static Dictionary<Keys, string> KeysToBindings = new Dictionary<Keys, string>
		{
			{
				Keys.None,
				null
			},
			{
				Keys.Back,
				"<Keyboard>/backspace"
			},
			{
				Keys.Tab,
				"<Keyboard>/tab"
			},
			{
				Keys.Enter,
				"<Keyboard>/enter"
			},
			{
				Keys.ShiftKey,
				"<Keyboard>/shift"
			},
			{
				Keys.ControlKey,
				"<Keyboard>/ctrl"
			},
			{
				Keys.Menu,
				"<Keyboard>/alt"
			},
			{
				Keys.Pause,
				"<Keyboard>/pause"
			},
			{
				Keys.CapsLock,
				"<Keyboard>/capsLock"
			},
			{
				Keys.Escape,
				"<Keyboard>/escape"
			},
			{
				Keys.Space,
				"<Keyboard>/space"
			},
			{
				Keys.Prior,
				"<Keyboard>/pageup"
			},
			{
				Keys.Next,
				"<Keyboard>/pagedown"
			},
			{
				Keys.End,
				"<Keyboard>/end"
			},
			{
				Keys.Home,
				"<Keyboard>/home"
			},
			{
				Keys.Left,
				"<Keyboard>/leftArrow"
			},
			{
				Keys.Up,
				"<Keyboard>/upArrow"
			},
			{
				Keys.Right,
				"<Keyboard>/rightArrow"
			},
			{
				Keys.Down,
				"<Keyboard>/downArrow"
			},
			{
				Keys.PrintScreen,
				"<Keyboard>/printScreen"
			},
			{
				Keys.Insert,
				"<Keyboard>/insert"
			},
			{
				Keys.Delete,
				"<Keyboard>/delete"
			},
			{
				Keys.D0,
				"<Keyboard>/0"
			},
			{
				Keys.D1,
				"<Keyboard>/1"
			},
			{
				Keys.D2,
				"<Keyboard>/2"
			},
			{
				Keys.D3,
				"<Keyboard>/3"
			},
			{
				Keys.D4,
				"<Keyboard>/4"
			},
			{
				Keys.D5,
				"<Keyboard>/5"
			},
			{
				Keys.D6,
				"<Keyboard>/6"
			},
			{
				Keys.D7,
				"<Keyboard>/7"
			},
			{
				Keys.D8,
				"<Keyboard>/8"
			},
			{
				Keys.D9,
				"<Keyboard>/9"
			},
			{
				Keys.A,
				"<Keyboard>/a"
			},
			{
				Keys.B,
				"<Keyboard>/b"
			},
			{
				Keys.C,
				"<Keyboard>/c"
			},
			{
				Keys.D,
				"<Keyboard>/d"
			},
			{
				Keys.E,
				"<Keyboard>/e"
			},
			{
				Keys.F,
				"<Keyboard>/f"
			},
			{
				Keys.G,
				"<Keyboard>/g"
			},
			{
				Keys.H,
				"<Keyboard>/h"
			},
			{
				Keys.I,
				"<Keyboard>/i"
			},
			{
				Keys.J,
				"<Keyboard>/j"
			},
			{
				Keys.K,
				"<Keyboard>/k"
			},
			{
				Keys.L,
				"<Keyboard>/l"
			},
			{
				Keys.M,
				"<Keyboard>/m"
			},
			{
				Keys.N,
				"<Keyboard>/n"
			},
			{
				Keys.O,
				"<Keyboard>/o"
			},
			{
				Keys.P,
				"<Keyboard>/p"
			},
			{
				Keys.Q,
				"<Keyboard>/q"
			},
			{
				Keys.R,
				"<Keyboard>/r"
			},
			{
				Keys.S,
				"<Keyboard>/s"
			},
			{
				Keys.T,
				"<Keyboard>/t"
			},
			{
				Keys.U,
				"<Keyboard>/u"
			},
			{
				Keys.V,
				"<Keyboard>/v"
			},
			{
				Keys.W,
				"<Keyboard>/w"
			},
			{
				Keys.X,
				"<Keyboard>/x"
			},
			{
				Keys.Y,
				"<Keyboard>/y"
			},
			{
				Keys.Z,
				"<Keyboard>/z"
			},
			{
				Keys.LWin,
				"<Keyboard>/leftMeta"
			},
			{
				Keys.RWin,
				"<Keyboard>/rightMeta"
			},
			{
				Keys.Apps,
				"<Keyboard>/contextMenu"
			},
			{
				Keys.NumPad0,
				"<Keyboard>/numpad0"
			},
			{
				Keys.NumPad1,
				"<Keyboard>/numpad1"
			},
			{
				Keys.NumPad2,
				"<Keyboard>/numpad2"
			},
			{
				Keys.NumPad3,
				"<Keyboard>/numpad3"
			},
			{
				Keys.NumPad4,
				"<Keyboard>/numpad4"
			},
			{
				Keys.NumPad5,
				"<Keyboard>/numpad5"
			},
			{
				Keys.NumPad6,
				"<Keyboard>/numpad6"
			},
			{
				Keys.NumPad7,
				"<Keyboard>/numpad7"
			},
			{
				Keys.NumPad8,
				"<Keyboard>/numpad8"
			},
			{
				Keys.NumPad9,
				"<Keyboard>/numpad9"
			},
			{
				Keys.Multiply,
				"<Keyboard>/numpadMultiply"
			},
			{
				Keys.Divide,
				"<Keyboard>/numpadDivide"
			},
			{
				Keys.Add,
				"<Keyboard>/numpadPlus"
			},
			{
				Keys.Subtract,
				"<Keyboard>/numpadMinus"
			},
			{
				Keys.Decimal,
				"<Keyboard>/numpadPeriod"
			},
			{
				Keys.F1,
				"<Keyboard>/F1"
			},
			{
				Keys.F2,
				"<Keyboard>/F2"
			},
			{
				Keys.F3,
				"<Keyboard>/F3"
			},
			{
				Keys.F4,
				"<Keyboard>/F4"
			},
			{
				Keys.F5,
				"<Keyboard>/F5"
			},
			{
				Keys.F6,
				"<Keyboard>/F6"
			},
			{
				Keys.F7,
				"<Keyboard>/F7"
			},
			{
				Keys.F8,
				"<Keyboard>/F8"
			},
			{
				Keys.F9,
				"<Keyboard>/F9"
			},
			{
				Keys.F10,
				"<Keyboard>/F10"
			},
			{
				Keys.F11,
				"<Keyboard>/F11"
			},
			{
				Keys.F12,
				"<Keyboard>/F12"
			},
			{
				Keys.NumLock,
				"<Keyboard>/numLock"
			},
			{
				Keys.Scroll,
				"<Keyboard>/scrollLock"
			},
			{
				Keys.LShiftKey,
				"<Keyboard>/leftShift"
			},
			{
				Keys.RShiftKey,
				"<Keyboard>/rightShift"
			},
			{
				Keys.LControlKey,
				"<Keyboard>/leftCtrl"
			},
			{
				Keys.RControlKey,
				"<Keyboard>/rightCtrl"
			},
			{
				Keys.LMenu,
				"<Keyboard>/leftMeta"
			},
			{
				Keys.RMenu,
				"<Keyboard>/rightMeta"
			},
			{
				Keys.Oem1,
				"<Keyboard>/semicolon"
			},
			{
				Keys.Oemplus,
				"<Keyboard>/equals"
			},
			{
				Keys.Oemcomma,
				"<Keyboard>/comma"
			},
			{
				Keys.OemPeriod,
				"<Keyboard>/period"
			},
			{
				Keys.OemQuestion,
				"<Keyboard>/slash"
			},
			{
				Keys.Oemtilde,
				"<Keyboard>/backquote"
			},
			{
				Keys.Oem4,
				"<Keyboard>/leftBracket"
			},
			{
				Keys.Oem6,
				"<Keyboard>/rightBracket"
			},
			{
				Keys.OemPipe,
				"<Keyboard>/backslash"
			},
			{
				Keys.Oem7,
				"<Keyboard>/quote"
			},
			{
				Keys.Oem102,
				"<Keyboard>/backslash"
			}
		};

		// Token: 0x04004FA2 RID: 20386
		private static InputAction ctrlAction;

		// Token: 0x04004FA3 RID: 20387
		private static InputAction shiftAction;

		// Token: 0x04004FA4 RID: 20388
		private static InputAction altAction;

		// Token: 0x04004FA5 RID: 20389
		private static int _FrameKeyboardModifier = -1;

		// Token: 0x04004FA6 RID: 20390
		private static int _FrameGamepadModifier = -1;

		// Token: 0x04004FA7 RID: 20391
		public static InputAction gamepadAltAction;

		// Token: 0x02001D6A RID: 7530
		[Serializable]
		public class SerializedBindings
		{
			// Token: 0x040072B0 RID: 29360
			public int Version = 1;

			// Token: 0x040072B1 RID: 29361
			public Dictionary<string, List<string>> Commands = new Dictionary<string, List<string>>();
		}

		// Token: 0x02001D6B RID: 7531
		[Flags]
		public enum ModifierFlags
		{
			// Token: 0x040072B3 RID: 29363
			none = 0,
			// Token: 0x040072B4 RID: 29364
			ctrl = 1,
			// Token: 0x040072B5 RID: 29365
			alt = 2,
			// Token: 0x040072B6 RID: 29366
			shift = 4,
			// Token: 0x040072B7 RID: 29367
			gamepadAlt = 8
		}
	}
}
