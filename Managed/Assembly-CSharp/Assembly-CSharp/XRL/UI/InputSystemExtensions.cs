using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using ConsoleLib.Console;
using UnityEngine.InputSystem;

namespace XRL.UI
{
	// Token: 0x02001472 RID: 5234
	public static class InputSystemExtensions
	{
		// Token: 0x0600CE09 RID: 52745 RVA: 0x004ACA98 File Offset: 0x004AAC98
		public static List<string> SerializedFormat(this InputAction action)
		{
			List<string> list = new List<string>();
			string text;
			if (!(action.name == InputSystemExtensions.GAMEPADALT))
			{
				IEnumerable<string> serializedBindingsForCommand = CommandBindingManager.GetSerializedBindingsForCommand(InputSystemExtensions.GAMEPADALT);
				text = ((serializedBindingsForCommand != null) ? serializedBindingsForCommand.First<string>() : null) ?? "<Gamepad>/leftTrigger";
			}
			else
			{
				text = null;
			}
			string text2 = text;
			foreach (InputBinding inputBinding in action.bindings)
			{
				if (inputBinding.isComposite)
				{
					list.Add(InputSystemExtensions.COMPOSITE);
					list.Add(inputBinding.GetNameOfComposite());
				}
				else if (inputBinding.path == text2)
				{
					list.Add(InputSystemExtensions.GAMEPADALT);
				}
				else
				{
					list.Add(inputBinding.path);
				}
			}
			if (list.Count == 0)
			{
				return null;
			}
			return list;
		}

		// Token: 0x0600CE0A RID: 52746 RVA: 0x004ACB7C File Offset: 0x004AAD7C
		public static Keys PathToKeys(string path)
		{
			Keys keys;
			if (InputSystemExtensions.PathToKeysMap.TryGetValue(path, out keys))
			{
				return keys;
			}
			if (!string.IsNullOrEmpty(path))
			{
				path.StartsWith("<Gampead>");
			}
			return Keys.None;
		}

		// Token: 0x0600CE0B RID: 52747 RVA: 0x004ACBAF File Offset: 0x004AADAF
		public static IEnumerable<int> GetConsoleKeycodes(this InputAction action)
		{
			int num = 0;
			int num2;
			for (int x = 0; x < action.bindings.Count; x = num2 + 1)
			{
				InputBinding bind = action.bindings[x];
				if (bind.isComposite)
				{
					if (num != 0)
					{
						yield return num;
						num = 0;
					}
				}
				else if (bind.isPartOfComposite)
				{
					if (bind.path != null)
					{
						num = (int)(num + InputSystemExtensions.PathToKeys(bind.path));
					}
				}
				else
				{
					if (num != 0)
					{
						yield return num;
						num = 0;
					}
					if (bind.path != null)
					{
						num = (int)(num + InputSystemExtensions.PathToKeys(bind.path));
					}
				}
				bind = default(InputBinding);
				num2 = x;
			}
			if (num != 0)
			{
				yield return num;
			}
			yield break;
		}

		// Token: 0x0600CE0C RID: 52748 RVA: 0x004ACBC0 File Offset: 0x004AADC0
		public static void LoadSerializedBindings(this InputAction action, List<string> bindings)
		{
			if (bindings == null)
			{
				return;
			}
			int i = 0;
			while (i < bindings.Count)
			{
				if (bindings[i] == InputSystemExtensions.COMPOSITE)
				{
					InputActionSetupExtensions.CompositeSyntax compositeSyntax = action.AddCompositeBinding(bindings[i + 1], null, null);
					if (bindings[i + 1] == "OneModifier")
					{
						compositeSyntax.With("Binding", bindings[i + 2], null, null);
						compositeSyntax.With("Modifier", InputSystemExtensions.<LoadSerializedBindings>g__resolveAlt|7_0(bindings[i + 3]), null, null);
						i += 4;
					}
					else
					{
						if (!(bindings[i + 1] == "TwoModifiers"))
						{
							MetricsManager.LogError("Unknown composite type " + bindings[i + 1] + " - aborting load for this action");
							return;
						}
						compositeSyntax.With("Binding", bindings[i + 2], null, null);
						compositeSyntax.With("Modifier1", InputSystemExtensions.<LoadSerializedBindings>g__resolveAlt|7_0(bindings[i + 3]), null, null);
						compositeSyntax.With("Modifier2", InputSystemExtensions.<LoadSerializedBindings>g__resolveAlt|7_0(bindings[i + 4]), null, null);
						i += 5;
					}
				}
				else
				{
					action.AddBinding(bindings[i++], null, null, null);
				}
			}
		}

		// Token: 0x0600CE0D RID: 52749 RVA: 0x004ACCFF File Offset: 0x004AAEFF
		public static void SetDisplayName(this InputControl control, string displayName)
		{
			string displayName2 = control.displayName;
			if (InputSystemExtensions._displayname == null)
			{
				InputSystemExtensions._displayname = typeof(InputControl).GetField("m_DisplayName", BindingFlags.Instance | BindingFlags.NonPublic);
			}
			InputSystemExtensions._displayname.SetValue(control, displayName);
		}

		// Token: 0x0600CE0E RID: 52750 RVA: 0x004ACD38 File Offset: 0x004AAF38
		public static void SetDisplayName(this InputControlList<InputControl> controls, string displayName)
		{
			foreach (InputControl inputControl in controls)
			{
				inputControl.SetDisplayName(displayName);
			}
		}

		// Token: 0x0600CE10 RID: 52752 RVA: 0x004AD3C9 File Offset: 0x004AB5C9
		[CompilerGenerated]
		internal static string <LoadSerializedBindings>g__resolveAlt|7_0(string binding)
		{
			if (binding == InputSystemExtensions.GAMEPADALT || binding.StartsWith("<Gamepad>"))
			{
				return CommandBindingManager.CurrentMap.ResolveGamepadAltBind();
			}
			return binding;
		}

		// Token: 0x04004FBB RID: 20411
		public static readonly string COMPOSITE = "Composite";

		// Token: 0x04004FBC RID: 20412
		public static readonly string GAMEPADALT = "GamepadAlt";

		// Token: 0x04004FBD RID: 20413
		private static Dictionary<string, Keys> PathToKeysMap = new Dictionary<string, Keys>(InputSystemExtensions.KeyboardKeyComparer.IgnoringKeyboard)
		{
			{
				"space",
				Keys.Space
			},
			{
				"enter",
				Keys.Enter
			},
			{
				"tab",
				Keys.Tab
			},
			{
				"backquote",
				Keys.Oemtilde
			},
			{
				"quote",
				Keys.Oem7
			},
			{
				"semicolon",
				Keys.Oem1
			},
			{
				"comma",
				Keys.Oemcomma
			},
			{
				"period",
				Keys.OemPeriod
			},
			{
				"slash",
				Keys.OemQuestion
			},
			{
				"backslash",
				Keys.Oem102
			},
			{
				"leftbracket",
				Keys.Oem4
			},
			{
				"rightbracket",
				Keys.Oem6
			},
			{
				"minus",
				Keys.OemMinus
			},
			{
				"equals",
				Keys.Oemplus
			},
			{
				"a",
				Keys.A
			},
			{
				"b",
				Keys.B
			},
			{
				"c",
				Keys.C
			},
			{
				"d",
				Keys.D
			},
			{
				"e",
				Keys.E
			},
			{
				"f",
				Keys.F
			},
			{
				"g",
				Keys.G
			},
			{
				"h",
				Keys.H
			},
			{
				"i",
				Keys.I
			},
			{
				"j",
				Keys.J
			},
			{
				"k",
				Keys.K
			},
			{
				"l",
				Keys.L
			},
			{
				"m",
				Keys.M
			},
			{
				"n",
				Keys.N
			},
			{
				"o",
				Keys.O
			},
			{
				"p",
				Keys.P
			},
			{
				"q",
				Keys.Q
			},
			{
				"r",
				Keys.R
			},
			{
				"s",
				Keys.S
			},
			{
				"t",
				Keys.T
			},
			{
				"u",
				Keys.U
			},
			{
				"v",
				Keys.V
			},
			{
				"w",
				Keys.W
			},
			{
				"x",
				Keys.X
			},
			{
				"y",
				Keys.Y
			},
			{
				"z",
				Keys.Z
			},
			{
				"1",
				Keys.D1
			},
			{
				"2",
				Keys.D2
			},
			{
				"3",
				Keys.D3
			},
			{
				"4",
				Keys.D4
			},
			{
				"5",
				Keys.D5
			},
			{
				"6",
				Keys.D6
			},
			{
				"7",
				Keys.D7
			},
			{
				"8",
				Keys.D8
			},
			{
				"9",
				Keys.D9
			},
			{
				"0",
				Keys.D0
			},
			{
				"shift",
				Keys.Shift
			},
			{
				"leftshift",
				Keys.Shift
			},
			{
				"rightshift",
				Keys.Shift
			},
			{
				"alt",
				Keys.Alt
			},
			{
				"leftalt",
				Keys.Alt
			},
			{
				"rightalt",
				Keys.Alt
			},
			{
				"ctrl",
				Keys.Control
			},
			{
				"leftctrl",
				Keys.Control
			},
			{
				"rightctrl",
				Keys.Control
			},
			{
				"leftmeta",
				Keys.LWin
			},
			{
				"rightmeta",
				Keys.RWin
			},
			{
				"contextmenu",
				Keys.Menu
			},
			{
				"escape",
				Keys.Escape
			},
			{
				"leftarrow",
				Keys.Left
			},
			{
				"rightarrow",
				Keys.Right
			},
			{
				"uparrow",
				Keys.Up
			},
			{
				"downarrow",
				Keys.Down
			},
			{
				"backspace",
				Keys.Back
			},
			{
				"pagedown",
				Keys.Prior
			},
			{
				"pageup",
				Keys.Next
			},
			{
				"home",
				Keys.Home
			},
			{
				"end",
				Keys.End
			},
			{
				"insert",
				Keys.Insert
			},
			{
				"delete",
				Keys.Delete
			},
			{
				"capslock",
				Keys.CapsLock
			},
			{
				"numlock",
				Keys.NumLock
			},
			{
				"printscreen",
				Keys.PrintScreen
			},
			{
				"scrolllock",
				Keys.Scroll
			},
			{
				"pause",
				Keys.Pause
			},
			{
				"numpadenter",
				Keys.Enter
			},
			{
				"numpaddivide",
				Keys.Divide
			},
			{
				"numpadmultiply",
				Keys.Multiply
			},
			{
				"numpadplus",
				Keys.Oemplus
			},
			{
				"numpadminus",
				Keys.OemMinus
			},
			{
				"numpadperiod",
				Keys.OemPeriod
			},
			{
				"numpadequals",
				Keys.Oemplus
			},
			{
				"numpad0",
				Keys.NumPad0
			},
			{
				"numpad1",
				Keys.NumPad1
			},
			{
				"numpad2",
				Keys.NumPad2
			},
			{
				"numpad3",
				Keys.NumPad3
			},
			{
				"numpad4",
				Keys.NumPad4
			},
			{
				"numpad5",
				Keys.NumPad5
			},
			{
				"numpad6",
				Keys.NumPad6
			},
			{
				"numpad7",
				Keys.NumPad7
			},
			{
				"numpad8",
				Keys.NumPad8
			},
			{
				"numpad9",
				Keys.NumPad9
			},
			{
				"f1",
				Keys.F1
			},
			{
				"f2",
				Keys.F2
			},
			{
				"f3",
				Keys.F3
			},
			{
				"f4",
				Keys.F4
			},
			{
				"f5",
				Keys.F5
			},
			{
				"f6",
				Keys.F6
			},
			{
				"f7",
				Keys.F7
			},
			{
				"f8",
				Keys.F8
			},
			{
				"f9",
				Keys.F9
			},
			{
				"f10",
				Keys.F10
			},
			{
				"f11",
				Keys.F11
			},
			{
				"f12",
				Keys.F12
			},
			{
				"oem1",
				Keys.Oem1
			},
			{
				"oem2",
				Keys.OemQuestion
			},
			{
				"oem3",
				Keys.Oemtilde
			},
			{
				"oem4",
				Keys.Oem4
			},
			{
				"oem5",
				Keys.OemPipe
			}
		};

		// Token: 0x04004FBE RID: 20414
		private static FieldInfo _displayname;

		// Token: 0x02001D87 RID: 7559
		public class KeyboardKeyComparer : IComparer<string>, IEqualityComparer<string>
		{
			// Token: 0x17000FEB RID: 4075
			// (get) Token: 0x0600F495 RID: 62613 RVA: 0x0055361F File Offset: 0x0055181F
			public static InputSystemExtensions.KeyboardKeyComparer IgnoringKeyboard
			{
				get
				{
					InputSystemExtensions.KeyboardKeyComparer keyboardKeyComparer;
					if ((keyboardKeyComparer = InputSystemExtensions.KeyboardKeyComparer.instance) == null)
					{
						keyboardKeyComparer = (InputSystemExtensions.KeyboardKeyComparer.instance = new InputSystemExtensions.KeyboardKeyComparer());
					}
					return keyboardKeyComparer;
				}
			}

			// Token: 0x0600F496 RID: 62614 RVA: 0x00553638 File Offset: 0x00551838
			public int Compare(string f1, string f2)
			{
				if (f1 != null && f1.StartsWith(InputSystemExtensions.KeyboardKeyComparer.keyboardPrefix, StringComparison.InvariantCultureIgnoreCase))
				{
					f1 = f1.Substring(InputSystemExtensions.KeyboardKeyComparer.keyboardPrefix.Length);
				}
				if (f2 != null && f2.StartsWith(InputSystemExtensions.KeyboardKeyComparer.keyboardPrefix, StringComparison.InvariantCultureIgnoreCase))
				{
					f2 = f2.Substring(InputSystemExtensions.KeyboardKeyComparer.keyboardPrefix.Length);
				}
				return StringComparer.InvariantCultureIgnoreCase.Compare(f1, f2);
			}

			// Token: 0x0600F497 RID: 62615 RVA: 0x00553698 File Offset: 0x00551898
			public bool Equals(string f1, string f2)
			{
				if (f1 != null && f1.StartsWith(InputSystemExtensions.KeyboardKeyComparer.keyboardPrefix, StringComparison.InvariantCultureIgnoreCase))
				{
					f1 = f1.Substring(InputSystemExtensions.KeyboardKeyComparer.keyboardPrefix.Length);
				}
				if (f2 != null && f2.StartsWith(InputSystemExtensions.KeyboardKeyComparer.keyboardPrefix, StringComparison.InvariantCultureIgnoreCase))
				{
					f2 = f2.Substring(InputSystemExtensions.KeyboardKeyComparer.keyboardPrefix.Length);
				}
				return StringComparer.InvariantCultureIgnoreCase.Equals(f1, f2);
			}

			// Token: 0x0600F498 RID: 62616 RVA: 0x005536F7 File Offset: 0x005518F7
			public int GetHashCode(string f1)
			{
				if (f1 != null && f1.StartsWith(InputSystemExtensions.KeyboardKeyComparer.keyboardPrefix, StringComparison.InvariantCultureIgnoreCase))
				{
					f1 = f1.Substring(InputSystemExtensions.KeyboardKeyComparer.keyboardPrefix.Length);
				}
				return StringComparer.InvariantCultureIgnoreCase.GetHashCode(f1);
			}

			// Token: 0x04007324 RID: 29476
			internal static readonly string keyboardPrefix = "<keyboard>/";

			// Token: 0x04007325 RID: 29477
			private static InputSystemExtensions.KeyboardKeyComparer instance;
		}
	}
}
