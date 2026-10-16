using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using ConsoleLib.Console;
using UnityEngine;

namespace XRL.UI
{
	// Token: 0x02001473 RID: 5235
	[Serializable]
	public class KeyMap
	{
		// Token: 0x0600CE11 RID: 52753 RVA: 0x004AD3F1 File Offset: 0x004AB5F1
		public string ResolveGamepadAltBind()
		{
			string text;
			if ((text = this.GamepadAltBind) == null)
			{
				IEnumerable<string> serializedBindingsForCommand = CommandBindingManager.GetSerializedBindingsForCommand("GamepadAlt");
				text = ((serializedBindingsForCommand != null) ? serializedBindingsForCommand.FirstOrDefault<string>() : null) ?? "<Gamepad>/leftTrigger";
			}
			return text;
		}

		// Token: 0x0600CE12 RID: 52754 RVA: 0x004AD41C File Offset: 0x004AB61C
		public bool IsKeyMapped(global::UnityEngine.KeyCode key, string[] layersToInclude = null)
		{
			return this.IsKeyMapped((int)Keyboard.Keymap[key], layersToInclude);
		}

		// Token: 0x0600CE13 RID: 52755 RVA: 0x004AD430 File Offset: 0x004AB630
		public bool IsKeyMapped(Keys key, string[] layersToInclude = null)
		{
			return this.IsKeyMapped((int)key, layersToInclude);
		}

		// Token: 0x0600CE14 RID: 52756 RVA: 0x004AD43C File Offset: 0x004AB63C
		public bool IsKeyMapped(int key, string[] layersToInclude = null)
		{
			return this.PrimaryMapKeyToCommandLayer.Any<KeyValuePair<string, Dictionary<int, string>>>((KeyValuePair<string, Dictionary<int, string>> layer) => (layersToInclude == null || layersToInclude.Contains(layer.Key)) && layer.Value.ContainsKey(key)) || this.SecondaryMapKeyToCommandLayer.Any<KeyValuePair<string, Dictionary<int, string>>>((KeyValuePair<string, Dictionary<int, string>> layer) => (layersToInclude == null || layersToInclude.Contains(layer.Key)) && layer.Value.ContainsKey(key));
		}

		// Token: 0x0600CE15 RID: 52757 RVA: 0x004AD490 File Offset: 0x004AB690
		public bool IsKeyMapped(int key, IEnumerable<string> layersToInclude = null)
		{
			return this.PrimaryMapKeyToCommandLayer.Any<KeyValuePair<string, Dictionary<int, string>>>((KeyValuePair<string, Dictionary<int, string>> layer) => (layersToInclude == null || layersToInclude.Contains(layer.Key)) && layer.Value.ContainsKey(key)) || this.SecondaryMapKeyToCommandLayer.Any<KeyValuePair<string, Dictionary<int, string>>>((KeyValuePair<string, Dictionary<int, string>> layer) => (layersToInclude == null || layersToInclude.Contains(layer.Key)) && layer.Value.ContainsKey(key));
		}

		// Token: 0x0600CE16 RID: 52758 RVA: 0x004AD4E4 File Offset: 0x004AB6E4
		public string GetCommandMappedTo(int key, string[] layersToInclude = null)
		{
			Dictionary<int, string> value = this.PrimaryMapKeyToCommandLayer.Where<KeyValuePair<string, Dictionary<int, string>>>((KeyValuePair<string, Dictionary<int, string>> layer) => (layersToInclude == null || layersToInclude.Contains(layer.Key)) && layer.Value.ContainsKey(key)).FirstOrDefault<KeyValuePair<string, Dictionary<int, string>>>().Value;
			string text = ((value != null) ? value[key] : null);
			Dictionary<int, string> value2 = this.SecondaryMapKeyToCommandLayer.Where<KeyValuePair<string, Dictionary<int, string>>>((KeyValuePair<string, Dictionary<int, string>> layer) => (layersToInclude == null || layersToInclude.Contains(layer.Key)) && layer.Value.ContainsKey(key)).FirstOrDefault<KeyValuePair<string, Dictionary<int, string>>>().Value;
			string text2 = ((value2 != null) ? value2[key] : null);
			if (text != null)
			{
				return text;
			}
			return text2;
		}

		// Token: 0x0600CE17 RID: 52759 RVA: 0x004AD57C File Offset: 0x004AB77C
		public void upgradeLayers()
		{
			try
			{
				using (Dictionary<string, GameCommand>.Enumerator enumerator = CommandBindingManager.CommandsByID.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						KeyValuePair<string, GameCommand> cmd = enumerator.Current;
						string cmdID = cmd.Key;
						Func<KeyValuePair<string, Dictionary<string, int>>, bool> <>9__0;
						for (;;)
						{
							IEnumerable<KeyValuePair<string, Dictionary<string, int>>> primaryMapCommandToKeyLayer = this.PrimaryMapCommandToKeyLayer;
							Func<KeyValuePair<string, Dictionary<string, int>>, bool> func;
							if ((func = <>9__0) == null)
							{
								func = (<>9__0 = (KeyValuePair<string, Dictionary<string, int>> layer) => layer.Key != cmd.Value.Layer && layer.Value.ContainsKey(cmdID));
							}
							using (IEnumerator<KeyValuePair<string, Dictionary<string, int>>> enumerator2 = primaryMapCommandToKeyLayer.Where<KeyValuePair<string, Dictionary<string, int>>>(func).GetEnumerator())
							{
								if (enumerator2.MoveNext())
								{
									KeyValuePair<string, Dictionary<string, int>> keyValuePair = enumerator2.Current;
									Dictionary<string, int> value = keyValuePair.Value;
									Dictionary<int, string> dictionary = this.PrimaryMapKeyToCommandLayer[keyValuePair.Key];
									string layer3 = cmd.Value.Layer;
									int num = value[cmd.Key];
									value.Remove(cmd.Key);
									dictionary.Remove(num);
									if (!this.PrimaryMapKeyToCommandLayer.ContainsKey(layer3))
									{
										this.PrimaryMapKeyToCommandLayer.Add(layer3, new Dictionary<int, string>());
									}
									if (this.PrimaryMapKeyToCommandLayer[layer3].ContainsKey(num))
									{
										MetricsManager.LogWarning("duplicate upgrade mapping for " + num.ToString());
									}
									else
									{
										this.PrimaryMapKeyToCommandLayer[layer3].Add(num, cmd.Key);
									}
									if (!this.PrimaryMapCommandToKeyLayer.ContainsKey(layer3))
									{
										this.PrimaryMapCommandToKeyLayer.Add(layer3, new Dictionary<string, int>());
									}
									if (this.PrimaryMapCommandToKeyLayer[layer3].ContainsKey(cmd.Key))
									{
										MetricsManager.LogWarning("duplicate upgrade mapping for command " + cmd.Key);
									}
									else
									{
										this.PrimaryMapCommandToKeyLayer[layer3].Add(cmd.Key, num);
									}
									continue;
								}
							}
							break;
						}
						Func<KeyValuePair<string, Dictionary<string, int>>, bool> <>9__1;
						for (;;)
						{
							IEnumerable<KeyValuePair<string, Dictionary<string, int>>> secondaryMapCommandToKeyLayer = this.SecondaryMapCommandToKeyLayer;
							Func<KeyValuePair<string, Dictionary<string, int>>, bool> func2;
							if ((func2 = <>9__1) == null)
							{
								func2 = (<>9__1 = (KeyValuePair<string, Dictionary<string, int>> layer) => layer.Key != cmd.Value.Layer && layer.Value.ContainsKey(cmdID));
							}
							using (IEnumerator<KeyValuePair<string, Dictionary<string, int>>> enumerator2 = secondaryMapCommandToKeyLayer.Where<KeyValuePair<string, Dictionary<string, int>>>(func2).GetEnumerator())
							{
								if (enumerator2.MoveNext())
								{
									KeyValuePair<string, Dictionary<string, int>> keyValuePair2 = enumerator2.Current;
									Dictionary<string, int> value2 = keyValuePair2.Value;
									Dictionary<int, string> dictionary2 = this.SecondaryMapKeyToCommandLayer[keyValuePair2.Key];
									string layer2 = cmd.Value.Layer;
									int num2 = value2[cmd.Key];
									value2.Remove(cmd.Key);
									dictionary2.Remove(num2);
									if (!this.SecondaryMapKeyToCommandLayer.ContainsKey(layer2))
									{
										this.SecondaryMapKeyToCommandLayer.Add(layer2, new Dictionary<int, string>());
									}
									if (this.SecondaryMapKeyToCommandLayer[layer2].ContainsKey(num2))
									{
										MetricsManager.LogWarning("Duplicate upgrade mapping for secondary key to command " + num2.ToString());
									}
									else
									{
										this.SecondaryMapKeyToCommandLayer[layer2].Add(num2, cmd.Key);
									}
									if (!this.SecondaryMapCommandToKeyLayer.ContainsKey(layer2))
									{
										this.SecondaryMapCommandToKeyLayer.Add(layer2, new Dictionary<string, int>());
									}
									if (this.SecondaryMapCommandToKeyLayer[layer2].ContainsKey(cmd.Key))
									{
										MetricsManager.LogWarning("Duplicate upgrade mapping for command key to layer " + cmd.Key);
									}
									else
									{
										this.SecondaryMapCommandToKeyLayer[layer2].Add(cmd.Key, num2);
									}
									continue;
								}
							}
							break;
						}
					}
				}
			}
			catch (Exception ex)
			{
				MetricsManager.LogException("KeyMap::upgradeLayers", ex, "game_exception");
			}
		}

		// Token: 0x0600CE18 RID: 52760 RVA: 0x004AD990 File Offset: 0x004ABB90
		public KeyMap()
		{
			this.PrimaryMapKeyToCommandLayer = new Dictionary<string, Dictionary<int, string>>();
			this.SecondaryMapKeyToCommandLayer = new Dictionary<string, Dictionary<int, string>>();
			this.PrimaryMapCommandToKeyLayer = new Dictionary<string, Dictionary<string, int>>();
			this.SecondaryMapCommandToKeyLayer = new Dictionary<string, Dictionary<string, int>>();
			this.PrimaryMapCommandToKeyLayer.Add("*default", new Dictionary<string, int>());
			this.SecondaryMapCommandToKeyLayer.Add("*default", new Dictionary<string, int>());
			this.PrimaryMapKeyToCommandLayer.Add("*default", new Dictionary<int, string>());
			this.SecondaryMapKeyToCommandLayer.Add("*default", new Dictionary<int, string>());
		}

		// Token: 0x0600CE19 RID: 52761 RVA: 0x004ADA5C File Offset: 0x004ABC5C
		public Dictionary<int, string> getPrimaryKeyToCommand(string commandId)
		{
			string layer = CommandBindingManager.CommandsByID[commandId].Layer;
			if (!this.PrimaryMapKeyToCommandLayer.ContainsKey(layer))
			{
				this.PrimaryMapKeyToCommandLayer.Add(layer, new Dictionary<int, string>());
			}
			return this.PrimaryMapKeyToCommandLayer[layer];
		}

		// Token: 0x0600CE1A RID: 52762 RVA: 0x004ADAA8 File Offset: 0x004ABCA8
		public Dictionary<string, int> getPrimaryCommandToKey(string commandId)
		{
			string layer = CommandBindingManager.CommandsByID[commandId].Layer;
			if (!this.PrimaryMapCommandToKeyLayer.ContainsKey(layer))
			{
				this.PrimaryMapCommandToKeyLayer.Add(layer, new Dictionary<string, int>());
			}
			return this.PrimaryMapCommandToKeyLayer[layer];
		}

		// Token: 0x0600CE1B RID: 52763 RVA: 0x004ADAF4 File Offset: 0x004ABCF4
		public Dictionary<int, string> getSecondaryKeyToCommand(string commandId)
		{
			string layer = CommandBindingManager.CommandsByID[commandId].Layer;
			if (!this.SecondaryMapKeyToCommandLayer.ContainsKey(layer))
			{
				this.SecondaryMapKeyToCommandLayer.Add(layer, new Dictionary<int, string>());
			}
			return this.SecondaryMapKeyToCommandLayer[layer];
		}

		// Token: 0x0600CE1C RID: 52764 RVA: 0x004ADB40 File Offset: 0x004ABD40
		public Dictionary<string, int> getSecondaryCommandToKey(string commandId)
		{
			string layer = CommandBindingManager.CommandsByID[commandId].Layer;
			if (!this.SecondaryMapCommandToKeyLayer.ContainsKey(layer))
			{
				this.SecondaryMapCommandToKeyLayer.Add(layer, new Dictionary<string, int>());
			}
			return this.SecondaryMapCommandToKeyLayer[layer];
		}

		// Token: 0x0600CE1D RID: 52765 RVA: 0x004ADB8C File Offset: 0x004ABD8C
		public void ApplyDefault(string Cmd, int Key, int Key2)
		{
			if (!this.getPrimaryCommandToKey(Cmd).ContainsKey(Cmd) && !this.getSecondaryKeyToCommand(Cmd).ContainsKey(Key) && !this.getPrimaryKeyToCommand(Cmd).ContainsKey(Key))
			{
				this.getPrimaryCommandToKey(Cmd).Add(Cmd, Key);
				this.getPrimaryKeyToCommand(Cmd).Add(Key, Cmd);
				if (Key2 != 0)
				{
					this.getSecondaryCommandToKey(Cmd).Add(Cmd, Key2);
					this.getSecondaryKeyToCommand(Cmd).Add(Key2, Cmd);
				}
			}
		}

		// Token: 0x0600CE1E RID: 52766 RVA: 0x004ADC01 File Offset: 0x004ABE01
		public void ApplyDefault(string Cmd, Keys Key, Keys Key2 = Keys.None)
		{
			this.ApplyDefault(Cmd, (int)Key, (int)Key2);
		}

		// Token: 0x0600CE1F RID: 52767 RVA: 0x004ADC0C File Offset: 0x004ABE0C
		public void ApplyDefaults()
		{
			this.ApplyDefault("CmdSystemMenu", Keys.Escape, Keys.None);
			this.ApplyDefault("CmdAttackDirection", Keys.Oem102, Keys.None);
			this.ApplyDefault("CmdAttackCell", (Keys)65762, Keys.None);
			this.ApplyDefault("CmdAttackNearest", (Keys)65601, Keys.None);
			this.ApplyDefault("CmdAutoAttack", (Keys)131137, Keys.None);
			this.ApplyDefault("Cancel", Keys.Escape, Keys.None);
			this.ApplyDefault("Accept", Keys.Space, Keys.None);
			this.ApplyDefault("CmdGetFrom", (Keys)131143, Keys.None);
			this.ApplyDefault("CmdJournal", Keys.J, Keys.None);
			this.ApplyDefault("CmdMoveTo", Keys.LButton | Keys.MButton | Keys.Back | Keys.Control, Keys.None);
			this.ApplyDefault("CmdMoveToEdge", Keys.LButton | Keys.MButton | Keys.Back | Keys.Shift, Keys.None);
			this.ApplyDefault("CmdMoveToPointOfInterest", Keys.Back, Keys.None);
			this.ApplyDefault("CmdReload", Keys.R, Keys.None);
			this.ApplyDefault("CmdChargenRandom", Keys.R, Keys.None);
			this.ApplyDefault("CmdChargenReset", Keys.Delete, Keys.None);
			this.ApplyDefault("CmdChargenMutationVariant", Keys.V, Keys.None);
			this.ApplyDefault("CmdChargenItemOptions", Keys.O, Keys.None);
			this.ApplyDefault("CmdCompanions", (Keys)131139, Keys.None);
			this.ApplyDefault("CmdMissileWeaponMenu", Keys.Multiply, Keys.None);
			this.ApplyDefault("Page Up", Keys.Prior, Keys.RButton | Keys.MButton | Keys.Space | Keys.Control);
			this.ApplyDefault("Page Down", Keys.Next, Keys.Back | Keys.Space | Keys.Control);
		}

		// Token: 0x04004FBF RID: 20415
		public Dictionary<int, string> PrimaryMapKeyToCommand;

		// Token: 0x04004FC0 RID: 20416
		public Dictionary<string, int> PrimaryMapCommandToKey;

		// Token: 0x04004FC1 RID: 20417
		public Dictionary<int, string> SecondaryMapKeyToCommand;

		// Token: 0x04004FC2 RID: 20418
		public Dictionary<string, int> SecondaryMapCommandToKey;

		// Token: 0x04004FC3 RID: 20419
		[OptionalField]
		public Dictionary<string, Dictionary<int, string>> PrimaryMapKeyToCommandLayer = new Dictionary<string, Dictionary<int, string>>();

		// Token: 0x04004FC4 RID: 20420
		[OptionalField]
		public Dictionary<string, Dictionary<int, string>> SecondaryMapKeyToCommandLayer = new Dictionary<string, Dictionary<int, string>>();

		// Token: 0x04004FC5 RID: 20421
		[OptionalField]
		public Dictionary<string, Dictionary<string, int>> PrimaryMapCommandToKeyLayer = new Dictionary<string, Dictionary<string, int>>();

		// Token: 0x04004FC6 RID: 20422
		[OptionalField]
		public Dictionary<string, Dictionary<string, int>> SecondaryMapCommandToKeyLayer = new Dictionary<string, Dictionary<string, int>>();

		// Token: 0x04004FC7 RID: 20423
		[OptionalField]
		public Dictionary<string, List<string>> CommandToSerializedInputBindings = new Dictionary<string, List<string>>();

		// Token: 0x04004FC8 RID: 20424
		[OptionalField]
		public string GamepadAltBind;
	}
}
