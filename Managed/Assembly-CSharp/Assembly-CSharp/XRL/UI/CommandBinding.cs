using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine.InputSystem;

namespace XRL.UI
{
	// Token: 0x0200146E RID: 5230
	public class CommandBinding : IDisposable
	{
		// Token: 0x0600CDA0 RID: 52640 RVA: 0x004A7EF2 File Offset: 0x004A60F2
		public bool SharesBindingsWith(CommandBinding with)
		{
			return this.command == null || with.command == null || this.command.SharesBindsWith(with.command);
		}

		// Token: 0x17000BE3 RID: 3043
		// (get) Token: 0x0600CDA1 RID: 52641 RVA: 0x004A7F17 File Offset: 0x004A6117
		public string name
		{
			get
			{
				InputAction inputAction = this._inputAction;
				return ((inputAction != null) ? inputAction.name : null) ?? "(null)";
			}
		}

		// Token: 0x0600CDA2 RID: 52642 RVA: 0x004A7F34 File Offset: 0x004A6134
		public void Enable()
		{
			this.enabled = true;
			if (!this._inputAction.enabled)
			{
				this._inputAction.Enable();
			}
		}

		// Token: 0x0600CDA3 RID: 52643 RVA: 0x004A7F55 File Offset: 0x004A6155
		public void Disable()
		{
			this.enabled = false;
		}

		// Token: 0x0600CDA4 RID: 52644 RVA: 0x004A7F60 File Offset: 0x004A6160
		public bool IsMapped()
		{
			InputAction inputAction = this._inputAction;
			return inputAction != null && inputAction.bindings.Count > 0;
		}

		// Token: 0x0600CDA5 RID: 52645 RVA: 0x004A7F89 File Offset: 0x004A6189
		public IEnumerable<int> GetConsoleKeycodes()
		{
			InputAction inputAction = this._inputAction;
			foreach (int num in ((inputAction != null) ? inputAction.GetConsoleKeycodes() : null))
			{
				yield return num;
			}
			IEnumerator<int> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x0600CDA6 RID: 52646 RVA: 0x004A7F99 File Offset: 0x004A6199
		public T ReadValue<T>() where T : struct
		{
			return this._inputAction.ReadValue<T>();
		}

		// Token: 0x0600CDA7 RID: 52647 RVA: 0x004A7FA8 File Offset: 0x004A61A8
		public void InitSubactions()
		{
			if (this._inputAction != null && this._inputAction.bindings.Count > 0)
			{
				int num = 0;
				string text = null;
				int num2 = 0;
				for (int i = 0; i < this._inputAction.bindings.Count; i++)
				{
					InputBinding inputBinding = this._inputAction.bindings[i];
					if (inputBinding.isComposite)
					{
						if (inputBinding.path == "OneModifier")
						{
							num2 = 2;
						}
						else if (inputBinding.path == "TwoModifiers")
						{
							num2 = 3;
						}
						else
						{
							MetricsManager.LogError("Unknown composiite " + inputBinding.path);
						}
					}
					else if (inputBinding.isPartOfComposite)
					{
						CommandBindingManager.GetGamepadAltBindings();
						if (CommandBindingManager.keyboardModifierFlags.ContainsKey(inputBinding.path))
						{
							num = (int)(num + CommandBindingManager.keyboardModifierFlags[inputBinding.path]);
						}
						else if (CommandBindingManager.GetGamepadAltBindings() == inputBinding.path)
						{
							num += 8;
						}
						else
						{
							text = inputBinding.path;
						}
						num2--;
					}
					else
					{
						text = inputBinding.path;
					}
					if (!inputBinding.isComposite && num2 == 0)
					{
						InputAction inputAction = new InputAction(string.Concat(new string[]
						{
							this._inputAction.name,
							"_subaction_",
							num.ToString(),
							"|",
							text
						}), this._inputAction.type, null, null, null, null);
						inputAction.AddBinding(text, null, null, null);
						if (inputBinding.path.StartsWith("<Keyboard>"))
						{
							if (this.keyboardSubActions[num] == null)
							{
								this.keyboardSubActions[num] = new List<InputAction>();
							}
							this.keyboardSubActions[num].Add(inputAction);
						}
						else if (inputBinding.path.StartsWith("<Gamepad>"))
						{
							if (this.gamepadSubActions[num] == null)
							{
								this.gamepadSubActions[num] = new List<InputAction>();
							}
							this.gamepadSubActions[num].Add(inputAction);
						}
						else if (inputBinding.path.StartsWith("<Mouse>"))
						{
							if (this.mouseSubActions[num] == null)
							{
								this.mouseSubActions[num] = new List<InputAction>();
							}
							this.mouseSubActions[num].Add(inputAction);
						}
						else
						{
							MetricsManager.LogError("Unknown binding path while generating subactions " + inputBinding.path);
						}
						inputAction.Enable();
						num = 0;
					}
				}
			}
		}

		// Token: 0x0600CDA8 RID: 52648 RVA: 0x004A8218 File Offset: 0x004A6418
		public bool Evaluate(CommandBinding.EVALUATE_MODE mode)
		{
			if (this.UseSubactions)
			{
				int frameKeyboardModifier = CommandBindingManager.GetFrameKeyboardModifier();
				List<InputAction> list;
				if (this.keyboardSubActions != null && (list = this.keyboardSubActions[frameKeyboardModifier]) != null)
				{
					for (int i = 0; i < list.Count; i++)
					{
						if (mode == CommandBinding.EVALUATE_MODE.IS_PRESSED && list[i].IsPressed())
						{
							return true;
						}
						if (mode == CommandBinding.EVALUATE_MODE.WAS_PERFORMED_THIS_FRAME && list[i].WasPerformedThisFrame())
						{
							return true;
						}
						if (mode == CommandBinding.EVALUATE_MODE.WAS_PRESSED_THIS_FRAME && list[i].WasPressedThisFrame())
						{
							return true;
						}
						if (mode == CommandBinding.EVALUATE_MODE.WAS_RELEASED_THIS_FRAME && list[i].WasReleasedThisFrame())
						{
							return true;
						}
					}
				}
				if (this.mouseSubActions != null && (list = this.mouseSubActions[frameKeyboardModifier]) != null)
				{
					for (int j = 0; j < list.Count; j++)
					{
						if (mode == CommandBinding.EVALUATE_MODE.IS_PRESSED && list[j].IsPressed())
						{
							return true;
						}
						if (mode == CommandBinding.EVALUATE_MODE.WAS_PERFORMED_THIS_FRAME && list[j].WasPerformedThisFrame())
						{
							return true;
						}
						if (mode == CommandBinding.EVALUATE_MODE.WAS_PRESSED_THIS_FRAME && list[j].WasPressedThisFrame())
						{
							return true;
						}
						if (mode == CommandBinding.EVALUATE_MODE.WAS_RELEASED_THIS_FRAME && list[j].WasReleasedThisFrame())
						{
							return true;
						}
					}
				}
				int frameGamepadModifier = CommandBindingManager.GetFrameGamepadModifier();
				if (this.gamepadSubActions != null && (list = this.gamepadSubActions[frameGamepadModifier]) != null)
				{
					for (int k = 0; k < list.Count; k++)
					{
						if (mode == CommandBinding.EVALUATE_MODE.IS_PRESSED && list[k].IsPressed())
						{
							return true;
						}
						if (mode == CommandBinding.EVALUATE_MODE.WAS_PERFORMED_THIS_FRAME && list[k].WasPerformedThisFrame())
						{
							return true;
						}
						if (mode == CommandBinding.EVALUATE_MODE.WAS_PRESSED_THIS_FRAME && list[k].WasPressedThisFrame())
						{
							return true;
						}
						if (mode == CommandBinding.EVALUATE_MODE.WAS_RELEASED_THIS_FRAME && list[k].WasReleasedThisFrame())
						{
							return true;
						}
					}
				}
				return false;
			}
			if (mode == CommandBinding.EVALUATE_MODE.IS_PRESSED)
			{
				return this._inputAction.IsPressed();
			}
			if (mode == CommandBinding.EVALUATE_MODE.WAS_PERFORMED_THIS_FRAME)
			{
				return this._inputAction.WasPerformedThisFrame();
			}
			if (mode == CommandBinding.EVALUATE_MODE.WAS_PRESSED_THIS_FRAME)
			{
				return this._inputAction.WasPressedThisFrame();
			}
			return mode == CommandBinding.EVALUATE_MODE.WAS_RELEASED_THIS_FRAME && this._inputAction.WasReleasedThisFrame();
		}

		// Token: 0x0600CDA9 RID: 52649 RVA: 0x004A83EA File Offset: 0x004A65EA
		private bool _IsPressed(InputAction a)
		{
			return a.IsPressed();
		}

		// Token: 0x0600CDAA RID: 52650 RVA: 0x004A83F2 File Offset: 0x004A65F2
		public bool IsPressed(bool forceEnable = false)
		{
			return (forceEnable || this.enabled) && this.Evaluate(CommandBinding.EVALUATE_MODE.IS_PRESSED);
		}

		// Token: 0x0600CDAB RID: 52651 RVA: 0x004A8408 File Offset: 0x004A6608
		private bool _WasPerformedThisFrame(InputAction a)
		{
			return a.WasPerformedThisFrame();
		}

		// Token: 0x0600CDAC RID: 52652 RVA: 0x004A8410 File Offset: 0x004A6610
		public bool WasPerformedThisFrame(bool ignoreEnabled = false)
		{
			return (this.enabled || ignoreEnabled) && this.Evaluate(CommandBinding.EVALUATE_MODE.WAS_PERFORMED_THIS_FRAME);
		}

		// Token: 0x0600CDAD RID: 52653 RVA: 0x004A8426 File Offset: 0x004A6626
		public bool WasReleasedThisFrame(bool ignoreEnabled = false)
		{
			return (this.enabled || ignoreEnabled) && this.Evaluate(CommandBinding.EVALUATE_MODE.WAS_RELEASED_THIS_FRAME);
		}

		// Token: 0x0600CDAE RID: 52654 RVA: 0x004A843C File Offset: 0x004A663C
		private bool _WasPressedThisFrame(InputAction a)
		{
			return a.WasPressedThisFrame();
		}

		// Token: 0x0600CDAF RID: 52655 RVA: 0x004A8444 File Offset: 0x004A6644
		public bool WasPressedThisFrame(bool ignoreEnabled = false)
		{
			return (this.enabled || ignoreEnabled) && this.Evaluate(CommandBinding.EVALUATE_MODE.WAS_PRESSED_THIS_FRAME);
		}

		// Token: 0x0600CDB0 RID: 52656 RVA: 0x004A845A File Offset: 0x004A665A
		public List<string> SerializedFormat()
		{
			if (this.isDefault)
			{
				return null;
			}
			return this._inputAction.SerializedFormat();
		}

		// Token: 0x0600CDB1 RID: 52657 RVA: 0x004A8474 File Offset: 0x004A6674
		public void Dispose()
		{
			if (this.keyboardSubActions != null)
			{
				foreach (List<InputAction> list in this.keyboardSubActions)
				{
					if (list != null)
					{
						foreach (InputAction inputAction in list)
						{
							if (inputAction != null)
							{
								inputAction.Dispose();
							}
						}
					}
				}
			}
			if (this.mouseSubActions != null)
			{
				foreach (List<InputAction> list2 in this.mouseSubActions)
				{
					if (list2 != null)
					{
						foreach (InputAction inputAction2 in list2)
						{
							if (inputAction2 != null)
							{
								inputAction2.Dispose();
							}
						}
					}
				}
			}
			if (this.gamepadSubActions != null)
			{
				foreach (List<InputAction> list3 in this.gamepadSubActions)
				{
					if (list3 != null)
					{
						foreach (InputAction inputAction3 in list3)
						{
							if (inputAction3 != null)
							{
								inputAction3.Dispose();
							}
						}
					}
				}
			}
			for (int j = 0; j < 16; j++)
			{
				List<InputAction> list4 = this.mouseSubActions[j];
				if (list4 != null)
				{
					list4.Clear();
				}
				List<InputAction> list5 = this.keyboardSubActions[j];
				if (list5 != null)
				{
					list5.Clear();
				}
				List<InputAction> list6 = this.gamepadSubActions[j];
				if (list6 != null)
				{
					list6.Clear();
				}
			}
			InputAction inputAction4 = this._inputAction;
			if (inputAction4 != null)
			{
				inputAction4.Dispose();
			}
			this._inputAction = null;
		}

		// Token: 0x0600CDB2 RID: 52658 RVA: 0x004A861C File Offset: 0x004A681C
		public static CommandBinding FromSerializedFormat(InputAction action, GameCommand cmd, List<string> bindings, bool AllowLegacyUpgrade, string targetSet)
		{
			CommandBinding commandBinding = new CommandBinding();
			commandBinding.command = cmd;
			commandBinding._inputAction = action;
			if (bindings != null)
			{
				commandBinding.isDefault = false;
				int i = 0;
				while (i < bindings.Count)
				{
					if (bindings[i] == InputSystemExtensions.COMPOSITE)
					{
						InputActionSetupExtensions.CompositeSyntax compositeSyntax = action.AddCompositeBinding(bindings[i + 1], null, null);
						if (bindings[i + 1] == "OneModifier")
						{
							compositeSyntax.With("Binding", bindings[i + 2], null, null);
							compositeSyntax.With("Modifier", CommandBinding.<FromSerializedFormat>g__resolveAlt|28_0(bindings[i + 3]), null, null);
							i += 4;
						}
						else if (bindings[i + 1] == "TwoModifiers")
						{
							compositeSyntax.With("Binding", bindings[i + 2], null, null);
							compositeSyntax.With("Modifier1", CommandBinding.<FromSerializedFormat>g__resolveAlt|28_0(bindings[i + 3]), null, null);
							compositeSyntax.With("Modifier2", CommandBinding.<FromSerializedFormat>g__resolveAlt|28_0(bindings[i + 4]), null, null);
							i += 5;
						}
						else
						{
							MetricsManager.LogError("Unknown composite type " + bindings[i + 1] + " - aborting load for this action");
							i++;
						}
					}
					else
					{
						action.AddBinding(bindings[i++], null, null, null);
					}
				}
			}
			else
			{
				KeyMap keyMap = (AllowLegacyUpgrade ? CommandBindingManager.GetLegacyKeymap() : null);
				if (cmd.keyboardBindings.Count > 0 || AllowLegacyUpgrade)
				{
					bool flag = false;
					try
					{
						try
						{
							if (keyMap != null && !cmd.SkipUpgrade)
							{
								List<int> list = new List<int>();
								try
								{
									Func<KeyValuePair<string, int>, bool> <>9__3;
									IEnumerable<KeyValuePair<string, int>> enumerable = keyMap.PrimaryMapCommandToKeyLayer.Where<KeyValuePair<string, Dictionary<string, int>>>(delegate(KeyValuePair<string, Dictionary<string, int>> l)
									{
										IEnumerable<KeyValuePair<string, int>> value = l.Value;
										Func<KeyValuePair<string, int>, bool> func;
										if ((func = <>9__3) == null)
										{
											func = (<>9__3 = (KeyValuePair<string, int> legacyCmd) => legacyCmd.Key.ToLower() == cmd.UpgradeFrom.ToLower());
										}
										return value.Any<KeyValuePair<string, int>>(func);
									}).FirstOrDefault<KeyValuePair<string, Dictionary<string, int>>>().Value.Where<KeyValuePair<string, int>>((KeyValuePair<string, int> v) => v.Key.ToLower() == cmd.ID.ToLower());
									list.Add((enumerable != null) ? enumerable.FirstOrDefault<KeyValuePair<string, int>>().Value : 0);
								}
								catch
								{
								}
								try
								{
									Func<KeyValuePair<string, int>, bool> <>9__6;
									IEnumerable<KeyValuePair<string, int>> enumerable2 = keyMap.SecondaryMapCommandToKeyLayer.Where<KeyValuePair<string, Dictionary<string, int>>>(delegate(KeyValuePair<string, Dictionary<string, int>> l)
									{
										IEnumerable<KeyValuePair<string, int>> value2 = l.Value;
										Func<KeyValuePair<string, int>, bool> func2;
										if ((func2 = <>9__6) == null)
										{
											func2 = (<>9__6 = (KeyValuePair<string, int> legacyCmd) => legacyCmd.Key.ToLower() == cmd.UpgradeFrom.ToLower());
										}
										return value2.Any<KeyValuePair<string, int>>(func2);
									}).FirstOrDefault<KeyValuePair<string, Dictionary<string, int>>>().Value.Where<KeyValuePair<string, int>>((KeyValuePair<string, int> v) => v.Key.ToLower() == cmd.ID.ToLower());
									list.Add(enumerable2.FirstOrDefault<KeyValuePair<string, int>>().Value);
								}
								catch
								{
								}
								using (List<int>.Enumerator enumerator = list.GetEnumerator())
								{
									while (enumerator.MoveNext())
									{
										if (CommandBindingManager.AddKeysValueToActionAsBinding(enumerator.Current, action, true))
										{
											flag = true;
											commandBinding.isDefault = false;
										}
									}
								}
							}
						}
						catch (Exception)
						{
						}
					}
					catch (Exception ex)
					{
						MetricsManager.LogException("KeyMapping upgrade old keymap", ex, "game_exception");
					}
					if (!flag)
					{
						foreach (GameCommand.KeyboardBinding keyboardBinding in cmd.keyboardBindings)
						{
							if (string.IsNullOrEmpty(keyboardBinding.Set) || keyboardBinding.Set == targetSet || (keyboardBinding.Set == "default" && string.IsNullOrEmpty(targetSet)))
							{
								string modifier = keyboardBinding.Modifier;
								List<string> list2 = ((modifier != null) ? modifier.CachedCommaExpansion() : null);
								if (list2 == null || list2.Count == 0)
								{
									action.AddBinding("<Keyboard>/" + keyboardBinding.Key, null, null, null);
								}
								else if (list2.Count == 1)
								{
									action.AddCompositeBinding("OneModifier", null, null).With("Binding", "<Keyboard>/" + keyboardBinding.Key, null, null).With("Modifier", "<Keyboard>/" + list2[0], null, null);
								}
								else
								{
									if (list2.Count != 2)
									{
										throw new Exception("Invalid or too many modifiers on " + cmd.ID + "!");
									}
									action.AddCompositeBinding("TwoModifiers", null, null).With("Binding", "<Keyboard>/" + keyboardBinding.Key, null, null).With("Modifier1", "<Keyboard>/" + list2[0], null, null)
										.With("Modifier2", "<Keyboard>/" + list2[1], null, null);
								}
							}
						}
					}
				}
				foreach (GameCommand.MouseBinding mouseBinding in cmd.mouseBindings)
				{
					if (string.IsNullOrEmpty(mouseBinding.Set) || mouseBinding.Set == targetSet || (mouseBinding.Set == "default" && string.IsNullOrEmpty(targetSet)))
					{
						string modifier2 = mouseBinding.Modifier;
						List<string> list3 = ((modifier2 != null) ? modifier2.CachedCommaExpansion() : null);
						if (list3 == null || list3.Count == 0)
						{
							action.AddBinding("<Mouse>/" + mouseBinding.Button, null, null, null);
						}
						else if (list3.Count == 1)
						{
							action.AddCompositeBinding("OneModifier", null, null).With("Binding", "<Mouse>/" + mouseBinding.Button, null, null).With("Modifier", "<Keyboard>/" + list3[0], null, null);
						}
						else
						{
							if (list3.Count != 2)
							{
								throw new Exception("Invalid or too many modifiers on " + cmd.ID + "!");
							}
							action.AddCompositeBinding("TwoModifiers", null, null).With("Binding", "<Mouse>/" + mouseBinding.Button, null, null).With("Modifier1", "<Keyboard>/" + list3[0], null, null)
								.With("Modifier2", "<Keyboard>/" + list3[1], null, null);
						}
					}
				}
				if (cmd.gamepadBindings.Count > 0)
				{
					foreach (GameCommand.GamepadBinding gamepadBinding in cmd.gamepadBindings)
					{
						if (!gamepadBinding.Alt)
						{
							action.AddBinding("<Gamepad>/" + gamepadBinding.Button, null, null, null);
						}
						else
						{
							action.AddCompositeBinding("OneModifier", null, null).With("Binding", "<Gamepad>/" + gamepadBinding.Button, null, null).With("Modifier", CommandBindingManager.CurrentMap.ResolveGamepadAltBind(), null, null);
						}
					}
				}
			}
			if (action.name == "GamepadAlt")
			{
				commandBinding.UseSubactions = false;
				CommandBindingManager.gamepadAltAction = commandBinding._inputAction;
			}
			else
			{
				commandBinding.InitSubactions();
			}
			return commandBinding;
		}

		// Token: 0x0600CDB3 RID: 52659 RVA: 0x004A8E18 File Offset: 0x004A7018
		public static CommandBinding FromInputAction(InputAction action)
		{
			CommandBinding commandBinding = new CommandBinding();
			commandBinding._inputAction = action;
			if (action.name == "GamepadAlt")
			{
				commandBinding.UseSubactions = false;
				CommandBindingManager.gamepadAltAction = commandBinding._inputAction;
			}
			else
			{
				commandBinding.InitSubactions();
			}
			return commandBinding;
		}

		// Token: 0x0600CDB5 RID: 52661 RVA: 0x004A8E9C File Offset: 0x004A709C
		[CompilerGenerated]
		internal static string <FromSerializedFormat>g__resolveAlt|28_0(string binding)
		{
			if (binding == InputSystemExtensions.GAMEPADALT || binding.StartsWith("<Gamepad>"))
			{
				return CommandBindingManager.CurrentMap.ResolveGamepadAltBind();
			}
			return binding;
		}

		// Token: 0x04004F76 RID: 20342
		public bool enabled;

		// Token: 0x04004F77 RID: 20343
		public bool isDefault = true;

		// Token: 0x04004F78 RID: 20344
		public GameCommand command;

		// Token: 0x04004F79 RID: 20345
		public InputAction _inputAction;

		// Token: 0x04004F7A RID: 20346
		public List<InputAction>[] keyboardSubActions = new List<InputAction>[16];

		// Token: 0x04004F7B RID: 20347
		public List<InputAction>[] mouseSubActions = new List<InputAction>[16];

		// Token: 0x04004F7C RID: 20348
		public List<InputAction>[] gamepadSubActions = new List<InputAction>[16];

		// Token: 0x04004F7D RID: 20349
		public bool UseSubactions = true;

		// Token: 0x02001D67 RID: 7527
		public enum EVALUATE_MODE
		{
			// Token: 0x040072A4 RID: 29348
			IS_PRESSED,
			// Token: 0x040072A5 RID: 29349
			WAS_PERFORMED_THIS_FRAME,
			// Token: 0x040072A6 RID: 29350
			WAS_PRESSED_THIS_FRAME,
			// Token: 0x040072A7 RID: 29351
			WAS_RELEASED_THIS_FRAME
		}
	}
}
