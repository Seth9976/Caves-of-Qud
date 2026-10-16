using System;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem
{
	// Token: 0x02000023 RID: 35
	public static class InputActionSetupExtensions
	{
		// Token: 0x06000219 RID: 537 RVA: 0x00007928 File Offset: 0x00005B28
		public static InputActionMap AddActionMap(this InputActionAsset asset, string name)
		{
			if (asset == null)
			{
				throw new ArgumentNullException("asset");
			}
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentNullException("name");
			}
			if (asset.FindActionMap(name, false) != null)
			{
				throw new InvalidOperationException("An action map called '" + name + "' already exists in the asset");
			}
			InputActionMap inputActionMap = new InputActionMap(name);
			inputActionMap.GenerateId();
			asset.AddActionMap(inputActionMap);
			return inputActionMap;
		}

		// Token: 0x0600021A RID: 538 RVA: 0x00007994 File Offset: 0x00005B94
		public static void AddActionMap(this InputActionAsset asset, InputActionMap map)
		{
			if (asset == null)
			{
				throw new ArgumentNullException("asset");
			}
			if (map == null)
			{
				throw new ArgumentNullException("map");
			}
			if (string.IsNullOrEmpty(map.name))
			{
				throw new InvalidOperationException("Maps added to an input action asset must be named");
			}
			if (map.asset != null)
			{
				throw new InvalidOperationException(string.Format("Cannot add map '{0}' to asset '{1}' as it has already been added to asset '{2}'", map, asset, map.asset));
			}
			if (asset.FindActionMap(map.name, false) != null)
			{
				throw new InvalidOperationException("An action map called '" + map.name + "' already exists in the asset");
			}
			map.OnWantToChangeSetup();
			asset.OnWantToChangeSetup();
			ArrayHelpers.Append<InputActionMap>(ref asset.m_ActionMaps, map);
			map.m_Asset = asset;
			asset.OnSetupChanged();
		}

		// Token: 0x0600021B RID: 539 RVA: 0x00007A54 File Offset: 0x00005C54
		public static void RemoveActionMap(this InputActionAsset asset, InputActionMap map)
		{
			if (asset == null)
			{
				throw new ArgumentNullException("asset");
			}
			if (map == null)
			{
				throw new ArgumentNullException("map");
			}
			map.OnWantToChangeSetup();
			asset.OnWantToChangeSetup();
			if (map.m_Asset != asset)
			{
				return;
			}
			ArrayHelpers.Erase<InputActionMap>(ref asset.m_ActionMaps, map);
			map.m_Asset = null;
			asset.OnSetupChanged();
		}

		// Token: 0x0600021C RID: 540 RVA: 0x00007AB8 File Offset: 0x00005CB8
		public static void RemoveActionMap(this InputActionAsset asset, string nameOrId)
		{
			if (asset == null)
			{
				throw new ArgumentNullException("asset");
			}
			if (nameOrId == null)
			{
				throw new ArgumentNullException("nameOrId");
			}
			InputActionMap inputActionMap = asset.FindActionMap(nameOrId, false);
			if (inputActionMap != null)
			{
				asset.RemoveActionMap(inputActionMap);
			}
		}

		// Token: 0x0600021D RID: 541 RVA: 0x00007AFC File Offset: 0x00005CFC
		public static InputAction AddAction(this InputActionMap map, string name, InputActionType type = InputActionType.Value, string binding = null, string interactions = null, string processors = null, string groups = null, string expectedControlLayout = null)
		{
			if (map == null)
			{
				throw new ArgumentNullException("map");
			}
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentException("Action must have name", "name");
			}
			map.OnWantToChangeSetup();
			if (map.FindAction(name, false) != null)
			{
				throw new InvalidOperationException(string.Concat(new string[] { "Cannot add action with duplicate name '", name, "' to set '", map.name, "'" }));
			}
			InputAction inputAction = new InputAction(name, type, null, null, null, null)
			{
				expectedControlType = expectedControlLayout
			};
			inputAction.GenerateId();
			ArrayHelpers.Append<InputAction>(ref map.m_Actions, inputAction);
			inputAction.m_ActionMap = map;
			if (!string.IsNullOrEmpty(binding))
			{
				inputAction.AddBinding(binding, interactions, processors, groups);
			}
			else
			{
				if (!string.IsNullOrEmpty(groups))
				{
					throw new ArgumentException(string.Format("No binding path was specified for action '{0}' but groups was specified ('{1}'); cannot apply groups without binding", inputAction, groups), "groups");
				}
				inputAction.m_Interactions = interactions;
				inputAction.m_Processors = processors;
				map.OnSetupChanged();
			}
			return inputAction;
		}

		// Token: 0x0600021E RID: 542 RVA: 0x00007BF4 File Offset: 0x00005DF4
		public static void RemoveAction(this InputAction action)
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			InputActionMap actionMap = action.actionMap;
			if (actionMap == null)
			{
				throw new ArgumentException(string.Format("Action '{0}' does not belong to an action map; nowhere to remove from", action), "action");
			}
			actionMap.OnWantToChangeSetup();
			InputBinding[] array = action.bindings.ToArray();
			int num = actionMap.m_Actions.IndexOfReference(action, -1);
			ArrayHelpers.EraseAt<InputAction>(ref actionMap.m_Actions, num);
			action.m_ActionMap = null;
			action.m_SingletonActionBindings = array;
			int num2 = ((actionMap.m_Bindings != null) ? (actionMap.m_Bindings.Length - array.Length) : 0);
			if (num2 == 0)
			{
				actionMap.m_Bindings = null;
			}
			else
			{
				InputBinding[] array2 = new InputBinding[num2];
				InputBinding[] bindings = actionMap.m_Bindings;
				int num3 = 0;
				for (int i = 0; i < bindings.Length; i++)
				{
					InputBinding binding = bindings[i];
					if (array.IndexOf<InputBinding>((InputBinding b) => b == binding) == -1)
					{
						array2[num3++] = binding;
					}
				}
				actionMap.m_Bindings = array2;
			}
			actionMap.OnSetupChanged();
		}

		// Token: 0x0600021F RID: 543 RVA: 0x00007D07 File Offset: 0x00005F07
		public static void RemoveAction(this InputActionAsset asset, string nameOrId)
		{
			if (asset == null)
			{
				throw new ArgumentNullException("asset");
			}
			if (nameOrId == null)
			{
				throw new ArgumentNullException("nameOrId");
			}
			InputAction inputAction = asset.FindAction(nameOrId, false);
			if (inputAction == null)
			{
				return;
			}
			inputAction.RemoveAction();
		}

		// Token: 0x06000220 RID: 544 RVA: 0x00007D40 File Offset: 0x00005F40
		public static InputActionSetupExtensions.BindingSyntax AddBinding(this InputAction action, string path, string interactions = null, string processors = null, string groups = null)
		{
			return action.AddBinding(new InputBinding
			{
				path = path,
				interactions = interactions,
				processors = processors,
				groups = groups
			});
		}

		// Token: 0x06000221 RID: 545 RVA: 0x00007D7D File Offset: 0x00005F7D
		public static InputActionSetupExtensions.BindingSyntax AddBinding(this InputAction action, InputControl control)
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			return action.AddBinding(control.path, null, null, null);
		}

		// Token: 0x06000222 RID: 546 RVA: 0x00007D9C File Offset: 0x00005F9C
		public static InputActionSetupExtensions.BindingSyntax AddBinding(this InputAction action, InputBinding binding = default(InputBinding))
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			binding.action = action.name;
			InputActionMap orCreateActionMap = action.GetOrCreateActionMap();
			int num = InputActionSetupExtensions.AddBindingInternal(orCreateActionMap, binding, -1);
			return new InputActionSetupExtensions.BindingSyntax(orCreateActionMap, num, null);
		}

		// Token: 0x06000223 RID: 547 RVA: 0x00007DDC File Offset: 0x00005FDC
		public static InputActionSetupExtensions.BindingSyntax AddBinding(this InputActionMap actionMap, string path, string interactions = null, string groups = null, string action = null, string processors = null)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path", "Binding path cannot be null");
			}
			return actionMap.AddBinding(new InputBinding
			{
				path = path,
				interactions = interactions,
				groups = groups,
				action = action,
				processors = processors
			});
		}

		// Token: 0x06000224 RID: 548 RVA: 0x00007E38 File Offset: 0x00006038
		public static InputActionSetupExtensions.BindingSyntax AddBinding(this InputActionMap actionMap, string path, InputAction action, string interactions = null, string groups = null)
		{
			if (action != null && action.actionMap != actionMap)
			{
				throw new ArgumentException(string.Format("Action '{0}' is not part of action map '{1}'", action, actionMap), "action");
			}
			if (action == null)
			{
				return actionMap.AddBinding(path, interactions, groups, null, null);
			}
			return actionMap.AddBinding(path, action.id, interactions, groups);
		}

		// Token: 0x06000225 RID: 549 RVA: 0x00007E8C File Offset: 0x0000608C
		public static InputActionSetupExtensions.BindingSyntax AddBinding(this InputActionMap actionMap, string path, Guid action, string interactions = null, string groups = null)
		{
			if (action == Guid.Empty)
			{
				return actionMap.AddBinding(path, interactions, groups, null, null);
			}
			return actionMap.AddBinding(path, interactions, groups, action.ToString(), null);
		}

		// Token: 0x06000226 RID: 550 RVA: 0x00007EC0 File Offset: 0x000060C0
		public static InputActionSetupExtensions.BindingSyntax AddBinding(this InputActionMap actionMap, InputBinding binding)
		{
			if (actionMap == null)
			{
				throw new ArgumentNullException("actionMap");
			}
			if (binding.path == null)
			{
				throw new ArgumentException("Binding path cannot be null", "binding");
			}
			int num = InputActionSetupExtensions.AddBindingInternal(actionMap, binding, -1);
			return new InputActionSetupExtensions.BindingSyntax(actionMap, num, null);
		}

		// Token: 0x06000227 RID: 551 RVA: 0x00007F08 File Offset: 0x00006108
		public static InputActionSetupExtensions.CompositeSyntax AddCompositeBinding(this InputAction action, string composite, string interactions = null, string processors = null)
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			if (string.IsNullOrEmpty(composite))
			{
				throw new ArgumentException("Composite name cannot be null or empty", "composite");
			}
			InputActionMap orCreateActionMap = action.GetOrCreateActionMap();
			InputBinding inputBinding = new InputBinding
			{
				name = NameAndParameters.ParseName(composite),
				path = composite,
				interactions = interactions,
				processors = processors,
				isComposite = true,
				action = action.name
			};
			int num = InputActionSetupExtensions.AddBindingInternal(orCreateActionMap, inputBinding, -1);
			return new InputActionSetupExtensions.CompositeSyntax(orCreateActionMap, action, num);
		}

		// Token: 0x06000228 RID: 552 RVA: 0x00007F98 File Offset: 0x00006198
		private static int AddBindingInternal(InputActionMap map, InputBinding binding, int bindingIndex = -1)
		{
			if (string.IsNullOrEmpty(binding.m_Id))
			{
				binding.GenerateId();
			}
			if (bindingIndex < 0)
			{
				bindingIndex = ArrayHelpers.Append<InputBinding>(ref map.m_Bindings, binding);
			}
			else
			{
				ArrayHelpers.InsertAt<InputBinding>(ref map.m_Bindings, bindingIndex, binding);
			}
			if (map.asset != null)
			{
				map.asset.MarkAsDirty();
			}
			if (map.m_SingletonAction != null)
			{
				map.m_SingletonAction.m_SingletonActionBindings = map.m_Bindings;
			}
			map.OnBindingModified();
			return bindingIndex;
		}

		// Token: 0x06000229 RID: 553 RVA: 0x00008014 File Offset: 0x00006214
		public static InputActionSetupExtensions.BindingSyntax ChangeBinding(this InputAction action, int index)
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			int num = action.BindingIndexOnActionToBindingIndexOnMap(index);
			return new InputActionSetupExtensions.BindingSyntax(action.GetOrCreateActionMap(), num, action);
		}

		// Token: 0x0600022A RID: 554 RVA: 0x00008044 File Offset: 0x00006244
		public static InputActionSetupExtensions.BindingSyntax ChangeBinding(this InputAction action, string name)
		{
			return action.ChangeBinding(new InputBinding
			{
				name = name
			});
		}

		// Token: 0x0600022B RID: 555 RVA: 0x00008068 File Offset: 0x00006268
		public static InputActionSetupExtensions.BindingSyntax ChangeBinding(this InputActionMap actionMap, int index)
		{
			if (actionMap == null)
			{
				throw new ArgumentNullException("actionMap");
			}
			if (index < 0 || index >= actionMap.m_Bindings.LengthSafe<InputBinding>())
			{
				throw new ArgumentOutOfRangeException("index");
			}
			return new InputActionSetupExtensions.BindingSyntax(actionMap, index, null);
		}

		// Token: 0x0600022C RID: 556 RVA: 0x000080A0 File Offset: 0x000062A0
		public static InputActionSetupExtensions.BindingSyntax ChangeBindingWithId(this InputAction action, string id)
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			return action.ChangeBinding(new InputBinding
			{
				m_Id = id
			});
		}

		// Token: 0x0600022D RID: 557 RVA: 0x000080D4 File Offset: 0x000062D4
		public static InputActionSetupExtensions.BindingSyntax ChangeBindingWithId(this InputAction action, Guid id)
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			return action.ChangeBinding(new InputBinding
			{
				id = id
			});
		}

		// Token: 0x0600022E RID: 558 RVA: 0x00008108 File Offset: 0x00006308
		public static InputActionSetupExtensions.BindingSyntax ChangeBindingWithGroup(this InputAction action, string group)
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			return action.ChangeBinding(new InputBinding
			{
				groups = group
			});
		}

		// Token: 0x0600022F RID: 559 RVA: 0x0000813C File Offset: 0x0000633C
		public static InputActionSetupExtensions.BindingSyntax ChangeBindingWithPath(this InputAction action, string path)
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			return action.ChangeBinding(new InputBinding
			{
				path = path
			});
		}

		// Token: 0x06000230 RID: 560 RVA: 0x00008170 File Offset: 0x00006370
		public static InputActionSetupExtensions.BindingSyntax ChangeBinding(this InputAction action, InputBinding match)
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			InputActionMap orCreateActionMap = action.GetOrCreateActionMap();
			Guid idDontGenerate = action.idDontGenerate;
			match.action = action.id.ToString();
			int num = orCreateActionMap.FindBindingRelativeToMap(match);
			if (num == -1)
			{
				match.action = action.name;
				num = orCreateActionMap.FindBindingRelativeToMap(match);
			}
			if (num == -1)
			{
				return default(InputActionSetupExtensions.BindingSyntax);
			}
			return new InputActionSetupExtensions.BindingSyntax(orCreateActionMap, num, action);
		}

		// Token: 0x06000231 RID: 561 RVA: 0x000081F0 File Offset: 0x000063F0
		public static InputActionSetupExtensions.BindingSyntax ChangeCompositeBinding(this InputAction action, string compositeName)
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			if (string.IsNullOrEmpty(compositeName))
			{
				throw new ArgumentNullException("compositeName");
			}
			InputActionMap orCreateActionMap = action.GetOrCreateActionMap();
			InputBinding[] bindings = orCreateActionMap.m_Bindings;
			int num = bindings.LengthSafe<InputBinding>();
			for (int i = 0; i < num; i++)
			{
				ref InputBinding ptr = ref bindings[i];
				if (ptr.isComposite && ptr.TriggersAction(action) && (compositeName.Equals(ptr.name, StringComparison.InvariantCultureIgnoreCase) || compositeName.Equals(NameAndParameters.ParseName(ptr.path), StringComparison.InvariantCultureIgnoreCase)))
				{
					return new InputActionSetupExtensions.BindingSyntax(orCreateActionMap, i, action);
				}
			}
			return default(InputActionSetupExtensions.BindingSyntax);
		}

		// Token: 0x06000232 RID: 562 RVA: 0x00008294 File Offset: 0x00006494
		public static void Rename(this InputAction action, string newName)
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			if (string.IsNullOrEmpty(newName))
			{
				throw new ArgumentNullException("newName");
			}
			if (action.name == newName)
			{
				return;
			}
			InputActionMap actionMap = action.actionMap;
			if (((actionMap != null) ? actionMap.FindAction(newName, false) : null) != null)
			{
				throw new InvalidOperationException(string.Format("Cannot rename '{0}' to '{1}' in map '{2}' as the map already contains an action with that name", action, newName, actionMap));
			}
			string name = action.m_Name;
			action.m_Name = newName;
			if (actionMap != null)
			{
				actionMap.ClearActionLookupTable();
			}
			if (((actionMap != null) ? actionMap.asset : null) != null && actionMap != null)
			{
				actionMap.asset.MarkAsDirty();
			}
			InputBinding[] bindings = action.GetOrCreateActionMap().m_Bindings;
			int num = bindings.LengthSafe<InputBinding>();
			for (int i = 0; i < num; i++)
			{
				if (string.Compare(bindings[i].action, name, StringComparison.InvariantCultureIgnoreCase) == 0)
				{
					bindings[i].action = newName;
				}
			}
		}

		// Token: 0x06000233 RID: 563 RVA: 0x0000837C File Offset: 0x0000657C
		public static void AddControlScheme(this InputActionAsset asset, InputControlScheme controlScheme)
		{
			if (asset == null)
			{
				throw new ArgumentNullException("asset");
			}
			if (string.IsNullOrEmpty(controlScheme.name))
			{
				throw new ArgumentException("Cannot add control scheme without name to asset " + asset.name, "controlScheme");
			}
			if (asset.FindControlScheme(controlScheme.name) != null)
			{
				throw new InvalidOperationException(string.Concat(new string[] { "Asset '", asset.name, "' already contains a control scheme called '", controlScheme.name, "'" }));
			}
			ArrayHelpers.Append<InputControlScheme>(ref asset.m_ControlSchemes, controlScheme);
			asset.MarkAsDirty();
		}

		// Token: 0x06000234 RID: 564 RVA: 0x0000842C File Offset: 0x0000662C
		public static InputActionSetupExtensions.ControlSchemeSyntax AddControlScheme(this InputActionAsset asset, string name)
		{
			if (asset == null)
			{
				throw new ArgumentNullException("asset");
			}
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentNullException("name");
			}
			int count = asset.controlSchemes.Count;
			asset.AddControlScheme(new InputControlScheme(name, null, null));
			return new InputActionSetupExtensions.ControlSchemeSyntax(asset, count);
		}

		// Token: 0x06000235 RID: 565 RVA: 0x00008484 File Offset: 0x00006684
		public static void RemoveControlScheme(this InputActionAsset asset, string name)
		{
			if (asset == null)
			{
				throw new ArgumentNullException("asset");
			}
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentNullException("name");
			}
			int num = asset.FindControlSchemeIndex(name);
			if (num != -1)
			{
				ArrayHelpers.EraseAt<InputControlScheme>(ref asset.m_ControlSchemes, num);
			}
			asset.MarkAsDirty();
		}

		// Token: 0x06000236 RID: 566 RVA: 0x000084D8 File Offset: 0x000066D8
		public static InputControlScheme WithBindingGroup(this InputControlScheme scheme, string bindingGroup)
		{
			return new InputActionSetupExtensions.ControlSchemeSyntax(scheme).WithBindingGroup(bindingGroup).Done();
		}

		// Token: 0x06000237 RID: 567 RVA: 0x000084FC File Offset: 0x000066FC
		public static InputControlScheme WithDevice(this InputControlScheme scheme, string controlPath, bool required)
		{
			if (required)
			{
				return new InputActionSetupExtensions.ControlSchemeSyntax(scheme).WithRequiredDevice(controlPath).Done();
			}
			return new InputActionSetupExtensions.ControlSchemeSyntax(scheme).WithOptionalDevice(controlPath).Done();
		}

		// Token: 0x06000238 RID: 568 RVA: 0x0000853C File Offset: 0x0000673C
		public static InputControlScheme WithRequiredDevice(this InputControlScheme scheme, string controlPath)
		{
			return new InputActionSetupExtensions.ControlSchemeSyntax(scheme).WithRequiredDevice(controlPath).Done();
		}

		// Token: 0x06000239 RID: 569 RVA: 0x00008560 File Offset: 0x00006760
		public static InputControlScheme WithOptionalDevice(this InputControlScheme scheme, string controlPath)
		{
			return new InputActionSetupExtensions.ControlSchemeSyntax(scheme).WithOptionalDevice(controlPath).Done();
		}

		// Token: 0x0600023A RID: 570 RVA: 0x00008584 File Offset: 0x00006784
		public static InputControlScheme OrWithRequiredDevice(this InputControlScheme scheme, string controlPath)
		{
			return new InputActionSetupExtensions.ControlSchemeSyntax(scheme).OrWithRequiredDevice(controlPath).Done();
		}

		// Token: 0x0600023B RID: 571 RVA: 0x000085A8 File Offset: 0x000067A8
		public static InputControlScheme OrWithOptionalDevice(this InputControlScheme scheme, string controlPath)
		{
			return new InputActionSetupExtensions.ControlSchemeSyntax(scheme).OrWithOptionalDevice(controlPath).Done();
		}

		// Token: 0x02000173 RID: 371
		public struct BindingSyntax
		{
			// Token: 0x1700051D RID: 1309
			// (get) Token: 0x06001310 RID: 4880 RVA: 0x0005A83A File Offset: 0x00058A3A
			public bool valid
			{
				get
				{
					return this.m_ActionMap != null && this.m_BindingIndexInMap >= 0 && this.m_BindingIndexInMap < this.m_ActionMap.m_Bindings.LengthSafe<InputBinding>();
				}
			}

			// Token: 0x1700051E RID: 1310
			// (get) Token: 0x06001311 RID: 4881 RVA: 0x0005A867 File Offset: 0x00058A67
			public int bindingIndex
			{
				get
				{
					if (!this.valid)
					{
						return -1;
					}
					if (this.m_Action != null)
					{
						return this.m_Action.BindingIndexOnMapToBindingIndexOnAction(this.m_BindingIndexInMap);
					}
					return this.m_BindingIndexInMap;
				}
			}

			// Token: 0x1700051F RID: 1311
			// (get) Token: 0x06001312 RID: 4882 RVA: 0x0005A893 File Offset: 0x00058A93
			public InputBinding binding
			{
				get
				{
					if (!this.valid)
					{
						throw new InvalidOperationException("BindingSyntax accessor is not valid");
					}
					return this.m_ActionMap.m_Bindings[this.m_BindingIndexInMap];
				}
			}

			// Token: 0x06001313 RID: 4883 RVA: 0x0005A8BE File Offset: 0x00058ABE
			internal BindingSyntax(InputActionMap map, int bindingIndexInMap, InputAction action = null)
			{
				this.m_ActionMap = map;
				this.m_BindingIndexInMap = bindingIndexInMap;
				this.m_Action = action;
			}

			// Token: 0x06001314 RID: 4884 RVA: 0x0005A8D8 File Offset: 0x00058AD8
			public InputActionSetupExtensions.BindingSyntax WithName(string name)
			{
				if (!this.valid)
				{
					throw new InvalidOperationException("Accessor is not valid");
				}
				this.m_ActionMap.m_Bindings[this.m_BindingIndexInMap].name = name;
				this.m_ActionMap.OnBindingModified();
				return this;
			}

			// Token: 0x06001315 RID: 4885 RVA: 0x0005A928 File Offset: 0x00058B28
			public InputActionSetupExtensions.BindingSyntax WithPath(string path)
			{
				if (!this.valid)
				{
					throw new InvalidOperationException("Accessor is not valid");
				}
				this.m_ActionMap.m_Bindings[this.m_BindingIndexInMap].path = path;
				this.m_ActionMap.OnBindingModified();
				return this;
			}

			// Token: 0x06001316 RID: 4886 RVA: 0x0005A978 File Offset: 0x00058B78
			public InputActionSetupExtensions.BindingSyntax WithGroup(string group)
			{
				if (!this.valid)
				{
					throw new InvalidOperationException("Accessor is not valid");
				}
				if (string.IsNullOrEmpty(group))
				{
					throw new ArgumentException("Group name cannot be null or empty", "group");
				}
				if (group.IndexOf(';') != -1)
				{
					throw new ArgumentException(string.Format("Group name cannot contain separator character '{0}'", ';'), "group");
				}
				return this.WithGroups(group);
			}

			// Token: 0x06001317 RID: 4887 RVA: 0x0005A9E0 File Offset: 0x00058BE0
			public InputActionSetupExtensions.BindingSyntax WithGroups(string groups)
			{
				if (!this.valid)
				{
					throw new InvalidOperationException("Accessor is not valid");
				}
				if (string.IsNullOrEmpty(groups))
				{
					return this;
				}
				string groups2 = this.m_ActionMap.m_Bindings[this.m_BindingIndexInMap].groups;
				if (!string.IsNullOrEmpty(groups2))
				{
					groups = string.Join(";", new string[] { groups2, groups });
				}
				this.m_ActionMap.m_Bindings[this.m_BindingIndexInMap].groups = groups;
				this.m_ActionMap.OnBindingModified();
				return this;
			}

			// Token: 0x06001318 RID: 4888 RVA: 0x0005AA7C File Offset: 0x00058C7C
			public InputActionSetupExtensions.BindingSyntax WithInteraction(string interaction)
			{
				if (!this.valid)
				{
					throw new InvalidOperationException("Accessor is not valid");
				}
				if (string.IsNullOrEmpty(interaction))
				{
					throw new ArgumentException("Interaction cannot be null or empty", "interaction");
				}
				if (interaction.IndexOf(';') != -1)
				{
					throw new ArgumentException(string.Format("Interaction string cannot contain separator character '{0}'", ';'), "interaction");
				}
				return this.WithInteractions(interaction);
			}

			// Token: 0x06001319 RID: 4889 RVA: 0x0005AAE4 File Offset: 0x00058CE4
			public InputActionSetupExtensions.BindingSyntax WithInteractions(string interactions)
			{
				if (!this.valid)
				{
					throw new InvalidOperationException("Accessor is not valid");
				}
				if (string.IsNullOrEmpty(interactions))
				{
					return this;
				}
				string interactions2 = this.m_ActionMap.m_Bindings[this.m_BindingIndexInMap].interactions;
				if (!string.IsNullOrEmpty(interactions2))
				{
					interactions = string.Join(";", new string[] { interactions2, interactions });
				}
				this.m_ActionMap.m_Bindings[this.m_BindingIndexInMap].interactions = interactions;
				this.m_ActionMap.OnBindingModified();
				return this;
			}

			// Token: 0x0600131A RID: 4890 RVA: 0x0005AB80 File Offset: 0x00058D80
			public InputActionSetupExtensions.BindingSyntax WithInteraction<TInteraction>() where TInteraction : IInputInteraction
			{
				if (!this.valid)
				{
					throw new InvalidOperationException("Accessor is not valid");
				}
				InternedString internedString = InputInteraction.s_Interactions.FindNameForType(typeof(TInteraction));
				if (internedString.IsEmpty())
				{
					throw new NotSupportedException(string.Format("Type '{0}' has not been registered as a interaction", typeof(TInteraction)));
				}
				return this.WithInteraction(internedString);
			}

			// Token: 0x0600131B RID: 4891 RVA: 0x0005ABE4 File Offset: 0x00058DE4
			public InputActionSetupExtensions.BindingSyntax WithProcessor(string processor)
			{
				if (!this.valid)
				{
					throw new InvalidOperationException("Accessor is not valid");
				}
				if (string.IsNullOrEmpty(processor))
				{
					throw new ArgumentException("Processor cannot be null or empty", "processor");
				}
				if (processor.IndexOf(';') != -1)
				{
					throw new ArgumentException(string.Format("Processor string cannot contain separator character '{0}'", ';'), "processor");
				}
				return this.WithProcessors(processor);
			}

			// Token: 0x0600131C RID: 4892 RVA: 0x0005AC4C File Offset: 0x00058E4C
			public InputActionSetupExtensions.BindingSyntax WithProcessors(string processors)
			{
				if (!this.valid)
				{
					throw new InvalidOperationException("Accessor is not valid");
				}
				if (string.IsNullOrEmpty(processors))
				{
					return this;
				}
				string processors2 = this.m_ActionMap.m_Bindings[this.m_BindingIndexInMap].processors;
				if (!string.IsNullOrEmpty(processors2))
				{
					processors = string.Join(";", new string[] { processors2, processors });
				}
				this.m_ActionMap.m_Bindings[this.m_BindingIndexInMap].processors = processors;
				this.m_ActionMap.OnBindingModified();
				return this;
			}

			// Token: 0x0600131D RID: 4893 RVA: 0x0005ACE8 File Offset: 0x00058EE8
			public InputActionSetupExtensions.BindingSyntax WithProcessor<TProcessor>()
			{
				if (!this.valid)
				{
					throw new InvalidOperationException("Accessor is not valid");
				}
				InternedString internedString = InputProcessor.s_Processors.FindNameForType(typeof(TProcessor));
				if (internedString.IsEmpty())
				{
					throw new NotSupportedException(string.Format("Type '{0}' has not been registered as a processor", typeof(TProcessor)));
				}
				return this.WithProcessor(internedString);
			}

			// Token: 0x0600131E RID: 4894 RVA: 0x0005AD4C File Offset: 0x00058F4C
			public InputActionSetupExtensions.BindingSyntax Triggering(InputAction action)
			{
				if (!this.valid)
				{
					throw new InvalidOperationException("Accessor is not valid");
				}
				if (action == null)
				{
					throw new ArgumentNullException("action");
				}
				if (action.isSingletonAction)
				{
					throw new ArgumentException(string.Format("Cannot change the action a binding triggers on singleton action '{0}'", action), "action");
				}
				this.m_ActionMap.m_Bindings[this.m_BindingIndexInMap].action = action.name;
				this.m_ActionMap.OnBindingModified();
				return this;
			}

			// Token: 0x0600131F RID: 4895 RVA: 0x0005ADCC File Offset: 0x00058FCC
			public InputActionSetupExtensions.BindingSyntax To(InputBinding binding)
			{
				if (!this.valid)
				{
					throw new InvalidOperationException("Accessor is not valid");
				}
				this.m_ActionMap.m_Bindings[this.m_BindingIndexInMap] = binding;
				if (this.m_ActionMap.m_SingletonAction != null)
				{
					this.m_ActionMap.m_Bindings[this.m_BindingIndexInMap].action = this.m_ActionMap.m_SingletonAction.name;
				}
				this.m_ActionMap.OnBindingModified();
				return this;
			}

			// Token: 0x06001320 RID: 4896 RVA: 0x0005AE4C File Offset: 0x0005904C
			public InputActionSetupExtensions.BindingSyntax NextBinding()
			{
				return this.Iterate(true);
			}

			// Token: 0x06001321 RID: 4897 RVA: 0x0005AE55 File Offset: 0x00059055
			public InputActionSetupExtensions.BindingSyntax PreviousBinding()
			{
				return this.Iterate(false);
			}

			// Token: 0x06001322 RID: 4898 RVA: 0x0005AE5E File Offset: 0x0005905E
			public InputActionSetupExtensions.BindingSyntax NextPartBinding(string partName)
			{
				if (string.IsNullOrEmpty(partName))
				{
					throw new ArgumentNullException("partName");
				}
				return this.IteratePartBinding(true, partName);
			}

			// Token: 0x06001323 RID: 4899 RVA: 0x0005AE7B File Offset: 0x0005907B
			public InputActionSetupExtensions.BindingSyntax PreviousPartBinding(string partName)
			{
				if (string.IsNullOrEmpty(partName))
				{
					throw new ArgumentNullException("partName");
				}
				return this.IteratePartBinding(false, partName);
			}

			// Token: 0x06001324 RID: 4900 RVA: 0x0005AE98 File Offset: 0x00059098
			public InputActionSetupExtensions.BindingSyntax NextCompositeBinding(string compositeName = null)
			{
				return this.IterateCompositeBinding(true, compositeName);
			}

			// Token: 0x06001325 RID: 4901 RVA: 0x0005AEA2 File Offset: 0x000590A2
			public InputActionSetupExtensions.BindingSyntax PreviousCompositeBinding(string compositeName = null)
			{
				return this.IterateCompositeBinding(false, compositeName);
			}

			// Token: 0x06001326 RID: 4902 RVA: 0x0005AEAC File Offset: 0x000590AC
			private InputActionSetupExtensions.BindingSyntax Iterate(bool next)
			{
				if (this.m_ActionMap == null)
				{
					return default(InputActionSetupExtensions.BindingSyntax);
				}
				InputBinding[] bindings = this.m_ActionMap.m_Bindings;
				if (bindings == null)
				{
					return default(InputActionSetupExtensions.BindingSyntax);
				}
				int num = this.m_BindingIndexInMap;
				for (;;)
				{
					num += (next ? 1 : (-1));
					if (num < 0 || num >= bindings.Length)
					{
						break;
					}
					if (this.m_Action == null || bindings[num].TriggersAction(this.m_Action))
					{
						goto IL_006C;
					}
				}
				return default(InputActionSetupExtensions.BindingSyntax);
				IL_006C:
				return new InputActionSetupExtensions.BindingSyntax(this.m_ActionMap, num, this.m_Action);
			}

			// Token: 0x06001327 RID: 4903 RVA: 0x0005AF38 File Offset: 0x00059138
			private InputActionSetupExtensions.BindingSyntax IterateCompositeBinding(bool next, string compositeName)
			{
				InputActionSetupExtensions.BindingSyntax bindingSyntax = this.Iterate(next);
				while (bindingSyntax.valid)
				{
					if (bindingSyntax.binding.isComposite)
					{
						if (compositeName == null)
						{
							return bindingSyntax;
						}
						if (compositeName.Equals(bindingSyntax.binding.name, StringComparison.InvariantCultureIgnoreCase))
						{
							return bindingSyntax;
						}
						string text = NameAndParameters.ParseName(bindingSyntax.binding.path);
						if (compositeName.Equals(text, StringComparison.InvariantCultureIgnoreCase))
						{
							return bindingSyntax;
						}
					}
					bindingSyntax = bindingSyntax.Iterate(next);
				}
				return default(InputActionSetupExtensions.BindingSyntax);
			}

			// Token: 0x06001328 RID: 4904 RVA: 0x0005AFBC File Offset: 0x000591BC
			private InputActionSetupExtensions.BindingSyntax IteratePartBinding(bool next, string partName)
			{
				if (!this.valid)
				{
					return default(InputActionSetupExtensions.BindingSyntax);
				}
				if (this.binding.isComposite)
				{
					if (!next)
					{
						return default(InputActionSetupExtensions.BindingSyntax);
					}
				}
				else if (!this.binding.isPartOfComposite)
				{
					return default(InputActionSetupExtensions.BindingSyntax);
				}
				InputActionSetupExtensions.BindingSyntax bindingSyntax = this.Iterate(next);
				while (bindingSyntax.valid)
				{
					if (!bindingSyntax.binding.isPartOfComposite)
					{
						return default(InputActionSetupExtensions.BindingSyntax);
					}
					if (partName.Equals(bindingSyntax.binding.name, StringComparison.InvariantCultureIgnoreCase))
					{
						return bindingSyntax;
					}
					bindingSyntax = bindingSyntax.Iterate(next);
				}
				return default(InputActionSetupExtensions.BindingSyntax);
			}

			// Token: 0x06001329 RID: 4905 RVA: 0x0005B06C File Offset: 0x0005926C
			public void Erase()
			{
				if (!this.valid)
				{
					throw new InvalidOperationException("Instance not valid");
				}
				bool isComposite = this.m_ActionMap.m_Bindings[this.m_BindingIndexInMap].isComposite;
				ArrayHelpers.EraseAt<InputBinding>(ref this.m_ActionMap.m_Bindings, this.m_BindingIndexInMap);
				if (isComposite)
				{
					while (this.m_BindingIndexInMap < this.m_ActionMap.m_Bindings.LengthSafe<InputBinding>() && this.m_ActionMap.m_Bindings[this.m_BindingIndexInMap].isPartOfComposite)
					{
						ArrayHelpers.EraseAt<InputBinding>(ref this.m_ActionMap.m_Bindings, this.m_BindingIndexInMap);
					}
				}
				this.m_Action.m_BindingsCount = this.m_ActionMap.m_Bindings.LengthSafe<InputBinding>();
				this.m_ActionMap.OnBindingModified();
				if (this.m_ActionMap.m_SingletonAction != null)
				{
					this.m_ActionMap.m_SingletonAction.m_SingletonActionBindings = this.m_ActionMap.m_Bindings;
				}
			}

			// Token: 0x0600132A RID: 4906 RVA: 0x0005B15C File Offset: 0x0005935C
			public InputActionSetupExtensions.BindingSyntax InsertPartBinding(string partName, string path)
			{
				if (string.IsNullOrEmpty(partName))
				{
					throw new ArgumentNullException("partName");
				}
				if (!this.valid)
				{
					throw new InvalidOperationException("Binding accessor is not valid");
				}
				InputBinding binding = this.binding;
				if (!binding.isPartOfComposite && !binding.isComposite)
				{
					throw new InvalidOperationException("Binding accessor must point to composite or part binding");
				}
				InputActionMap actionMap = this.m_ActionMap;
				InputBinding inputBinding = default(InputBinding);
				inputBinding.path = path;
				inputBinding.isPartOfComposite = true;
				inputBinding.name = partName;
				InputAction action = this.m_Action;
				inputBinding.action = ((action != null) ? action.name : null);
				InputActionSetupExtensions.AddBindingInternal(actionMap, inputBinding, this.m_BindingIndexInMap + 1);
				return new InputActionSetupExtensions.BindingSyntax(this.m_ActionMap, this.m_BindingIndexInMap + 1, this.m_Action);
			}

			// Token: 0x0400081E RID: 2078
			private readonly InputActionMap m_ActionMap;

			// Token: 0x0400081F RID: 2079
			private readonly InputAction m_Action;

			// Token: 0x04000820 RID: 2080
			internal readonly int m_BindingIndexInMap;
		}

		// Token: 0x02000174 RID: 372
		public struct CompositeSyntax
		{
			// Token: 0x17000520 RID: 1312
			// (get) Token: 0x0600132B RID: 4907 RVA: 0x0005B21A File Offset: 0x0005941A
			public int bindingIndex
			{
				get
				{
					if (this.m_ActionMap == null)
					{
						return -1;
					}
					if (this.m_Action != null)
					{
						return this.m_Action.BindingIndexOnMapToBindingIndexOnAction(this.m_BindingIndexInMap);
					}
					return this.m_BindingIndexInMap;
				}
			}

			// Token: 0x0600132C RID: 4908 RVA: 0x0005B246 File Offset: 0x00059446
			internal CompositeSyntax(InputActionMap map, InputAction action, int compositeIndex)
			{
				this.m_Action = action;
				this.m_ActionMap = map;
				this.m_BindingIndexInMap = compositeIndex;
			}

			// Token: 0x0600132D RID: 4909 RVA: 0x0005B260 File Offset: 0x00059460
			public InputActionSetupExtensions.CompositeSyntax With(string name, string binding, string groups = null, string processors = null)
			{
				using (InputActionRebindingExtensions.DeferBindingResolution())
				{
					int num;
					if (this.m_Action != null)
					{
						num = this.m_Action.AddBinding(binding, null, processors, groups).m_BindingIndexInMap;
					}
					else
					{
						num = this.m_ActionMap.AddBinding(binding, null, groups, null, processors).m_BindingIndexInMap;
					}
					this.m_ActionMap.m_Bindings[num].name = name;
					this.m_ActionMap.m_Bindings[num].isPartOfComposite = true;
				}
				return this;
			}

			// Token: 0x04000821 RID: 2081
			private readonly InputAction m_Action;

			// Token: 0x04000822 RID: 2082
			private readonly InputActionMap m_ActionMap;

			// Token: 0x04000823 RID: 2083
			private int m_BindingIndexInMap;
		}

		// Token: 0x02000175 RID: 373
		public struct ControlSchemeSyntax
		{
			// Token: 0x0600132E RID: 4910 RVA: 0x0005B2FC File Offset: 0x000594FC
			internal ControlSchemeSyntax(InputActionAsset asset, int index)
			{
				this.m_Asset = asset;
				this.m_ControlSchemeIndex = index;
				this.m_ControlScheme = default(InputControlScheme);
			}

			// Token: 0x0600132F RID: 4911 RVA: 0x0005B318 File Offset: 0x00059518
			internal ControlSchemeSyntax(InputControlScheme controlScheme)
			{
				this.m_Asset = null;
				this.m_ControlSchemeIndex = -1;
				this.m_ControlScheme = controlScheme;
			}

			// Token: 0x06001330 RID: 4912 RVA: 0x0005B330 File Offset: 0x00059530
			public InputActionSetupExtensions.ControlSchemeSyntax WithBindingGroup(string bindingGroup)
			{
				if (string.IsNullOrEmpty(bindingGroup))
				{
					throw new ArgumentNullException("bindingGroup");
				}
				if (this.m_Asset == null)
				{
					this.m_ControlScheme.m_BindingGroup = bindingGroup;
				}
				else
				{
					this.m_Asset.m_ControlSchemes[this.m_ControlSchemeIndex].bindingGroup = bindingGroup;
				}
				return this;
			}

			// Token: 0x06001331 RID: 4913 RVA: 0x0005B38E File Offset: 0x0005958E
			public InputActionSetupExtensions.ControlSchemeSyntax WithRequiredDevice<TDevice>() where TDevice : InputDevice
			{
				return this.WithRequiredDevice(this.DeviceTypeToControlPath<TDevice>());
			}

			// Token: 0x06001332 RID: 4914 RVA: 0x0005B39C File Offset: 0x0005959C
			public InputActionSetupExtensions.ControlSchemeSyntax WithOptionalDevice<TDevice>() where TDevice : InputDevice
			{
				return this.WithOptionalDevice(this.DeviceTypeToControlPath<TDevice>());
			}

			// Token: 0x06001333 RID: 4915 RVA: 0x0005B3AA File Offset: 0x000595AA
			public InputActionSetupExtensions.ControlSchemeSyntax OrWithRequiredDevice<TDevice>() where TDevice : InputDevice
			{
				return this.OrWithRequiredDevice(this.DeviceTypeToControlPath<TDevice>());
			}

			// Token: 0x06001334 RID: 4916 RVA: 0x0005B3B8 File Offset: 0x000595B8
			public InputActionSetupExtensions.ControlSchemeSyntax OrWithOptionalDevice<TDevice>() where TDevice : InputDevice
			{
				return this.OrWithOptionalDevice(this.DeviceTypeToControlPath<TDevice>());
			}

			// Token: 0x06001335 RID: 4917 RVA: 0x0005B3C6 File Offset: 0x000595C6
			public InputActionSetupExtensions.ControlSchemeSyntax WithRequiredDevice(string controlPath)
			{
				this.AddDeviceEntry(controlPath, InputControlScheme.DeviceRequirement.Flags.None);
				return this;
			}

			// Token: 0x06001336 RID: 4918 RVA: 0x0005B3D6 File Offset: 0x000595D6
			public InputActionSetupExtensions.ControlSchemeSyntax WithOptionalDevice(string controlPath)
			{
				this.AddDeviceEntry(controlPath, InputControlScheme.DeviceRequirement.Flags.Optional);
				return this;
			}

			// Token: 0x06001337 RID: 4919 RVA: 0x0005B3E6 File Offset: 0x000595E6
			public InputActionSetupExtensions.ControlSchemeSyntax OrWithRequiredDevice(string controlPath)
			{
				this.AddDeviceEntry(controlPath, InputControlScheme.DeviceRequirement.Flags.Or);
				return this;
			}

			// Token: 0x06001338 RID: 4920 RVA: 0x0005B3F6 File Offset: 0x000595F6
			public InputActionSetupExtensions.ControlSchemeSyntax OrWithOptionalDevice(string controlPath)
			{
				this.AddDeviceEntry(controlPath, InputControlScheme.DeviceRequirement.Flags.Optional | InputControlScheme.DeviceRequirement.Flags.Or);
				return this;
			}

			// Token: 0x06001339 RID: 4921 RVA: 0x0005B408 File Offset: 0x00059608
			private string DeviceTypeToControlPath<TDevice>() where TDevice : InputDevice
			{
				string text = InputControlLayout.s_Layouts.TryFindLayoutForType(typeof(TDevice)).ToString();
				if (string.IsNullOrEmpty(text))
				{
					text = typeof(TDevice).Name;
				}
				return "<" + text + ">";
			}

			// Token: 0x0600133A RID: 4922 RVA: 0x0005B460 File Offset: 0x00059660
			public InputControlScheme Done()
			{
				if (this.m_Asset != null)
				{
					return this.m_Asset.m_ControlSchemes[this.m_ControlSchemeIndex];
				}
				return this.m_ControlScheme;
			}

			// Token: 0x0600133B RID: 4923 RVA: 0x0005B490 File Offset: 0x00059690
			private void AddDeviceEntry(string controlPath, InputControlScheme.DeviceRequirement.Flags flags)
			{
				if (string.IsNullOrEmpty(controlPath))
				{
					throw new ArgumentNullException("controlPath");
				}
				InputControlScheme inputControlScheme = ((this.m_Asset != null) ? this.m_Asset.m_ControlSchemes[this.m_ControlSchemeIndex] : this.m_ControlScheme);
				ArrayHelpers.Append<InputControlScheme.DeviceRequirement>(ref inputControlScheme.m_DeviceRequirements, new InputControlScheme.DeviceRequirement
				{
					m_ControlPath = controlPath,
					m_Flags = flags
				});
				if (this.m_Asset == null)
				{
					this.m_ControlScheme = inputControlScheme;
					return;
				}
				this.m_Asset.m_ControlSchemes[this.m_ControlSchemeIndex] = inputControlScheme;
			}

			// Token: 0x04000824 RID: 2084
			private readonly InputActionAsset m_Asset;

			// Token: 0x04000825 RID: 2085
			private readonly int m_ControlSchemeIndex;

			// Token: 0x04000826 RID: 2086
			private InputControlScheme m_ControlScheme;
		}
	}
}
