using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem
{
	// Token: 0x0200001F RID: 31
	public static class InputActionRebindingExtensions
	{
		// Token: 0x060001D0 RID: 464 RVA: 0x00005D8C File Offset: 0x00003F8C
		public static PrimitiveValue? GetParameterValue(this InputAction action, string name, InputBinding bindingMask = default(InputBinding))
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentNullException("name");
			}
			return action.GetParameterValue(new InputActionRebindingExtensions.ParameterOverride(name, bindingMask, default(PrimitiveValue)));
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x00005DD0 File Offset: 0x00003FD0
		private static PrimitiveValue? GetParameterValue(this InputAction action, InputActionRebindingExtensions.ParameterOverride parameterOverride)
		{
			parameterOverride.bindingMask.action = action.name;
			InputActionMap orCreateActionMap = action.GetOrCreateActionMap();
			orCreateActionMap.ResolveBindingsIfNecessary();
			using (InputActionRebindingExtensions.ParameterEnumerator enumerator = new InputActionRebindingExtensions.ParameterEnumerable(orCreateActionMap.m_State, parameterOverride, orCreateActionMap.m_MapIndexInState).GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					InputActionRebindingExtensions.Parameter parameter = enumerator.Current;
					return new PrimitiveValue?(PrimitiveValue.FromObject(parameter.field.GetValue(parameter.instance)));
				}
			}
			return null;
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x00005E78 File Offset: 0x00004078
		public static PrimitiveValue? GetParameterValue(this InputAction action, string name, int bindingIndex)
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentNullException("name");
			}
			if (bindingIndex < 0)
			{
				throw new ArgumentOutOfRangeException("bindingIndex");
			}
			int num = action.BindingIndexOnActionToBindingIndexOnMap(bindingIndex);
			InputBinding inputBinding = new InputBinding
			{
				id = action.GetOrCreateActionMap().bindings[num].id
			};
			return action.GetParameterValue(name, inputBinding);
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x00005EF4 File Offset: 0x000040F4
		public unsafe static TValue? GetParameterValue<TObject, TValue>(this InputAction action, Expression<Func<TObject, TValue>> expr, InputBinding bindingMask = default(InputBinding)) where TValue : struct
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			if (expr == null)
			{
				throw new ArgumentNullException("expr");
			}
			InputActionRebindingExtensions.ParameterOverride parameterOverride = InputActionRebindingExtensions.ExtractParameterOverride<TObject, TValue>(expr, bindingMask, default(PrimitiveValue));
			PrimitiveValue? parameterValue = action.GetParameterValue(parameterOverride);
			if (parameterValue == null)
			{
				return null;
			}
			if (Type.GetTypeCode(typeof(TValue)) == parameterValue.Value.type)
			{
				PrimitiveValue value = parameterValue.Value;
				TValue tvalue = default(TValue);
				UnsafeUtility.MemCpy(UnsafeUtility.AddressOf<TValue>(ref tvalue), (void*)value.valuePtr, (long)UnsafeUtility.SizeOf<TValue>());
				return new TValue?(tvalue);
			}
			return new TValue?((TValue)((object)Convert.ChangeType(parameterValue.Value.ToObject(), typeof(TValue))));
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x00005FC4 File Offset: 0x000041C4
		public static void ApplyParameterOverride<TObject, TValue>(this InputAction action, Expression<Func<TObject, TValue>> expr, TValue value, InputBinding bindingMask = default(InputBinding)) where TValue : struct
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			if (expr == null)
			{
				throw new ArgumentNullException("expr");
			}
			InputActionMap orCreateActionMap = action.GetOrCreateActionMap();
			orCreateActionMap.ResolveBindingsIfNecessary();
			bindingMask.action = action.name;
			InputActionRebindingExtensions.ParameterOverride parameterOverride = InputActionRebindingExtensions.ExtractParameterOverride<TObject, TValue>(expr, bindingMask, PrimitiveValue.From<TValue>(value));
			InputActionRebindingExtensions.ApplyParameterOverride(orCreateActionMap.m_State, orCreateActionMap.m_MapIndexInState, ref orCreateActionMap.m_ParameterOverrides, ref orCreateActionMap.m_ParameterOverridesCount, parameterOverride);
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x00006034 File Offset: 0x00004234
		public static void ApplyParameterOverride<TObject, TValue>(this InputActionMap actionMap, Expression<Func<TObject, TValue>> expr, TValue value, InputBinding bindingMask = default(InputBinding)) where TValue : struct
		{
			if (actionMap == null)
			{
				throw new ArgumentNullException("actionMap");
			}
			if (expr == null)
			{
				throw new ArgumentNullException("expr");
			}
			actionMap.ResolveBindingsIfNecessary();
			InputActionRebindingExtensions.ParameterOverride parameterOverride = InputActionRebindingExtensions.ExtractParameterOverride<TObject, TValue>(expr, bindingMask, PrimitiveValue.From<TValue>(value));
			InputActionRebindingExtensions.ApplyParameterOverride(actionMap.m_State, actionMap.m_MapIndexInState, ref actionMap.m_ParameterOverrides, ref actionMap.m_ParameterOverridesCount, parameterOverride);
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x00006090 File Offset: 0x00004290
		public static void ApplyParameterOverride<TObject, TValue>(this InputActionAsset asset, Expression<Func<TObject, TValue>> expr, TValue value, InputBinding bindingMask = default(InputBinding)) where TValue : struct
		{
			if (asset == null)
			{
				throw new ArgumentNullException("asset");
			}
			if (expr == null)
			{
				throw new ArgumentNullException("expr");
			}
			asset.ResolveBindingsIfNecessary();
			InputActionRebindingExtensions.ParameterOverride parameterOverride = InputActionRebindingExtensions.ExtractParameterOverride<TObject, TValue>(expr, bindingMask, PrimitiveValue.From<TValue>(value));
			InputActionRebindingExtensions.ApplyParameterOverride(asset.m_SharedStateForAllMaps, -1, ref asset.m_ParameterOverrides, ref asset.m_ParameterOverridesCount, parameterOverride);
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x000060EC File Offset: 0x000042EC
		private static InputActionRebindingExtensions.ParameterOverride ExtractParameterOverride<TObject, TValue>(Expression<Func<TObject, TValue>> expr, InputBinding bindingMask = default(InputBinding), PrimitiveValue value = default(PrimitiveValue))
		{
			if (expr == null)
			{
				throw new ArgumentException("Expression must be a LambdaExpression but was a " + expr.GetType().Name + " instead", "expr");
			}
			MemberExpression memberExpression = expr.Body as MemberExpression;
			if (memberExpression == null)
			{
				UnaryExpression unaryExpression = expr.Body as UnaryExpression;
				if (unaryExpression != null && unaryExpression.NodeType == ExpressionType.Convert)
				{
					MemberExpression memberExpression2 = unaryExpression.Operand as MemberExpression;
					if (memberExpression2 != null)
					{
						memberExpression = memberExpression2;
						goto IL_008D;
					}
				}
				throw new ArgumentException("Body in LambdaExpression must be a MemberExpression (x.name) but was a " + expr.GetType().Name + " instead", "expr");
			}
			IL_008D:
			string text;
			if (typeof(InputProcessor).IsAssignableFrom(typeof(TObject)))
			{
				text = InputProcessor.s_Processors.FindNameForType(typeof(TObject));
			}
			else if (typeof(IInputInteraction).IsAssignableFrom(typeof(TObject)))
			{
				text = InputInteraction.s_Interactions.FindNameForType(typeof(TObject));
			}
			else
			{
				if (!typeof(InputBindingComposite).IsAssignableFrom(typeof(TObject)))
				{
					throw new ArgumentException("Given type must be an InputProcessor, IInputInteraction, or InputBindingComposite (was " + typeof(TObject).Name + ")", "TObject");
				}
				text = InputBindingComposite.s_Composites.FindNameForType(typeof(TObject));
			}
			return new InputActionRebindingExtensions.ParameterOverride(text, memberExpression.Member.Name, bindingMask, value);
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x0000626C File Offset: 0x0000446C
		public static void ApplyParameterOverride(this InputActionMap actionMap, string name, PrimitiveValue value, InputBinding bindingMask = default(InputBinding))
		{
			if (actionMap == null)
			{
				throw new ArgumentNullException("actionMap");
			}
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentNullException("name");
			}
			actionMap.ResolveBindingsIfNecessary();
			InputActionRebindingExtensions.ApplyParameterOverride(actionMap.m_State, actionMap.m_MapIndexInState, ref actionMap.m_ParameterOverrides, ref actionMap.m_ParameterOverridesCount, new InputActionRebindingExtensions.ParameterOverride(name, bindingMask, value));
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x000062C8 File Offset: 0x000044C8
		public static void ApplyParameterOverride(this InputActionAsset asset, string name, PrimitiveValue value, InputBinding bindingMask = default(InputBinding))
		{
			if (asset == null)
			{
				throw new ArgumentNullException("asset");
			}
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentNullException("name");
			}
			asset.ResolveBindingsIfNecessary();
			InputActionRebindingExtensions.ApplyParameterOverride(asset.m_SharedStateForAllMaps, -1, ref asset.m_ParameterOverrides, ref asset.m_ParameterOverridesCount, new InputActionRebindingExtensions.ParameterOverride(name, bindingMask, value));
		}

		// Token: 0x060001DA RID: 474 RVA: 0x00006324 File Offset: 0x00004524
		public static void ApplyParameterOverride(this InputAction action, string name, PrimitiveValue value, InputBinding bindingMask = default(InputBinding))
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			InputActionMap orCreateActionMap = action.GetOrCreateActionMap();
			orCreateActionMap.ResolveBindingsIfNecessary();
			bindingMask.action = action.name;
			InputActionRebindingExtensions.ApplyParameterOverride(orCreateActionMap.m_State, orCreateActionMap.m_MapIndexInState, ref orCreateActionMap.m_ParameterOverrides, ref orCreateActionMap.m_ParameterOverridesCount, new InputActionRebindingExtensions.ParameterOverride(name, bindingMask, value));
		}

		// Token: 0x060001DB RID: 475 RVA: 0x00006390 File Offset: 0x00004590
		public static void ApplyParameterOverride(this InputAction action, string name, PrimitiveValue value, int bindingIndex)
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentNullException("name");
			}
			if (bindingIndex < 0)
			{
				throw new ArgumentOutOfRangeException("bindingIndex");
			}
			int num = action.BindingIndexOnActionToBindingIndexOnMap(bindingIndex);
			InputBinding inputBinding = new InputBinding
			{
				id = action.GetOrCreateActionMap().bindings[num].id
			};
			action.ApplyParameterOverride(name, value, inputBinding);
		}

		// Token: 0x060001DC RID: 476 RVA: 0x0000640C File Offset: 0x0000460C
		private static void ApplyParameterOverride(InputActionState state, int mapIndex, ref InputActionRebindingExtensions.ParameterOverride[] parameterOverrides, ref int parameterOverridesCount, InputActionRebindingExtensions.ParameterOverride parameterOverride)
		{
			bool flag = false;
			if (parameterOverrides != null)
			{
				for (int i = 0; i < parameterOverridesCount; i++)
				{
					ref InputActionRebindingExtensions.ParameterOverride ptr = ref parameterOverrides[i];
					if (string.Equals(ptr.objectRegistrationName, parameterOverride.objectRegistrationName, StringComparison.OrdinalIgnoreCase) && string.Equals(ptr.parameter, parameterOverride.parameter, StringComparison.OrdinalIgnoreCase) && ptr.bindingMask == parameterOverride.bindingMask)
					{
						flag = true;
						ptr = parameterOverride;
						break;
					}
				}
			}
			if (!flag)
			{
				ArrayHelpers.AppendWithCapacity<InputActionRebindingExtensions.ParameterOverride>(ref parameterOverrides, ref parameterOverridesCount, parameterOverride, 10);
			}
			foreach (InputActionRebindingExtensions.Parameter parameter in new InputActionRebindingExtensions.ParameterEnumerable(state, parameterOverride, mapIndex))
			{
				InputActionMap actionMap = state.GetActionMap(parameter.bindingIndex);
				ref InputBinding binding = ref state.GetBinding(parameter.bindingIndex);
				InputActionRebindingExtensions.ParameterOverride? parameterOverride2 = InputActionRebindingExtensions.ParameterOverride.Find(actionMap, ref binding, parameterOverride.parameter, parameterOverride.objectRegistrationName);
				if (parameterOverride2 != null)
				{
					TypeCode typeCode = Type.GetTypeCode(parameter.field.FieldType);
					parameter.field.SetValue(parameter.instance, parameterOverride2.Value.value.ConvertTo(typeCode).ToObject());
				}
			}
		}

		// Token: 0x060001DD RID: 477 RVA: 0x00006560 File Offset: 0x00004760
		public static int GetBindingIndex(this InputAction action, InputBinding bindingMask)
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			ReadOnlyArray<InputBinding> bindings = action.bindings;
			for (int i = 0; i < bindings.Count; i++)
			{
				if (bindingMask.Matches(bindings[i]))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x060001DE RID: 478 RVA: 0x000065A8 File Offset: 0x000047A8
		public static int GetBindingIndex(this InputActionMap actionMap, InputBinding bindingMask)
		{
			if (actionMap == null)
			{
				throw new ArgumentNullException("actionMap");
			}
			ReadOnlyArray<InputBinding> bindings = actionMap.bindings;
			for (int i = 0; i < bindings.Count; i++)
			{
				if (bindingMask.Matches(bindings[i]))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x060001DF RID: 479 RVA: 0x000065F0 File Offset: 0x000047F0
		public static int GetBindingIndex(this InputAction action, string group = null, string path = null)
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			return action.GetBindingIndex(new InputBinding(path, null, group, null, null, null));
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x00006620 File Offset: 0x00004820
		public static InputBinding? GetBindingForControl(this InputAction action, InputControl control)
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			int bindingIndexForControl = action.GetBindingIndexForControl(control);
			if (bindingIndexForControl == -1)
			{
				return null;
			}
			return new InputBinding?(action.bindings[bindingIndexForControl]);
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x00006674 File Offset: 0x00004874
		public unsafe static int GetBindingIndexForControl(this InputAction action, InputControl control)
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			InputActionMap orCreateActionMap = action.GetOrCreateActionMap();
			orCreateActionMap.ResolveBindingsIfNecessary();
			InputActionState state = orCreateActionMap.m_State;
			InputControl[] controls = state.controls;
			int totalControlCount = state.totalControlCount;
			InputActionState.BindingState* bindingStates = state.bindingStates;
			int* controlIndexToBindingIndex = state.controlIndexToBindingIndex;
			int actionIndexInState = action.m_ActionIndexInState;
			for (int i = 0; i < totalControlCount; i++)
			{
				if (controls[i] == control)
				{
					int num = controlIndexToBindingIndex[i];
					if (bindingStates[num].actionIndex == actionIndexInState)
					{
						int bindingIndexInMap = state.GetBindingIndexInMap(num);
						return action.BindingIndexOnMapToBindingIndexOnAction(bindingIndexInMap);
					}
				}
			}
			return -1;
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x00006720 File Offset: 0x00004920
		public static string GetBindingDisplayString(this InputAction action, InputBinding.DisplayStringOptions options = (InputBinding.DisplayStringOptions)0, string group = null)
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			InputBinding inputBinding;
			if (!string.IsNullOrEmpty(group))
			{
				inputBinding = InputBinding.MaskByGroup(group);
			}
			else
			{
				InputBinding? inputBinding2 = action.FindEffectiveBindingMask();
				if (inputBinding2 != null)
				{
					inputBinding = inputBinding2.Value;
				}
				else
				{
					inputBinding = default(InputBinding);
				}
			}
			return action.GetBindingDisplayString(inputBinding, options);
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x00006778 File Offset: 0x00004978
		public static string GetBindingDisplayString(this InputAction action, InputBinding bindingMask, InputBinding.DisplayStringOptions options = (InputBinding.DisplayStringOptions)0)
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			string text = string.Empty;
			ReadOnlyArray<InputBinding> bindings = action.bindings;
			for (int i = 0; i < bindings.Count; i++)
			{
				if (!bindings[i].isPartOfComposite && bindingMask.Matches(bindings[i]))
				{
					string bindingDisplayString = action.GetBindingDisplayString(i, options);
					if (text != "")
					{
						text = text + " | " + bindingDisplayString;
					}
					else
					{
						text = bindingDisplayString;
					}
				}
			}
			return text;
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x00006800 File Offset: 0x00004A00
		public static string GetBindingDisplayString(this InputAction action, int bindingIndex, InputBinding.DisplayStringOptions options = (InputBinding.DisplayStringOptions)0)
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			string text;
			string text2;
			return action.GetBindingDisplayString(bindingIndex, out text, out text2, options);
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x00006828 File Offset: 0x00004A28
		public unsafe static string GetBindingDisplayString(this InputAction action, int bindingIndex, out string deviceLayoutName, out string controlPath, InputBinding.DisplayStringOptions options = (InputBinding.DisplayStringOptions)0)
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			deviceLayoutName = null;
			controlPath = null;
			ReadOnlyArray<InputBinding> bindings = action.bindings;
			int count = bindings.Count;
			if (bindingIndex < 0 || bindingIndex >= count)
			{
				throw new ArgumentOutOfRangeException(string.Format("Binding index {0} is out of range on action '{1}' with {2} bindings", bindingIndex, action, bindings.Count), "bindingIndex");
			}
			if (!bindings[bindingIndex].isComposite)
			{
				InputControl inputControl = null;
				InputActionMap orCreateActionMap = action.GetOrCreateActionMap();
				orCreateActionMap.ResolveBindingsIfNecessary();
				InputActionState state = orCreateActionMap.m_State;
				int num = action.BindingIndexOnActionToBindingIndexOnMap(bindingIndex);
				int bindingIndexInState = state.GetBindingIndexInState(orCreateActionMap.m_MapIndexInState, num);
				InputActionState.BindingState* ptr = state.bindingStates + bindingIndexInState;
				if (ptr->controlCount > 0)
				{
					inputControl = state.controls[ptr->controlStartIndex];
				}
				InputBinding inputBinding = bindings[bindingIndex];
				if (string.IsNullOrEmpty(inputBinding.effectiveInteractions))
				{
					inputBinding.overrideInteractions = action.interactions;
				}
				else if (!string.IsNullOrEmpty(action.interactions))
				{
					inputBinding.overrideInteractions = inputBinding.effectiveInteractions + ";action.interactions";
				}
				return inputBinding.ToDisplayString(out deviceLayoutName, out controlPath, options, inputControl);
			}
			string name = NameAndParameters.Parse(bindings[bindingIndex].effectivePath).name;
			int firstPartIndex = bindingIndex + 1;
			int num2 = firstPartIndex;
			while (num2 < count && bindings[num2].isPartOfComposite)
			{
				num2++;
			}
			int partCount = num2 - firstPartIndex;
			string[] partStrings = new string[partCount];
			for (int i = 0; i < partCount; i++)
			{
				string text = action.GetBindingDisplayString(firstPartIndex + i, options);
				if (string.IsNullOrEmpty(text))
				{
					text = " ";
				}
				partStrings[i] = text;
			}
			string displayFormatString = InputBindingComposite.GetDisplayFormatString(name);
			if (string.IsNullOrEmpty(displayFormatString))
			{
				return StringHelpers.Join<string>("/", partStrings);
			}
			return StringHelpers.ExpandTemplateString(displayFormatString, delegate(string fragment)
			{
				string text2 = string.Empty;
				for (int j = 0; j < partCount; j++)
				{
					if (string.Equals(bindings[firstPartIndex + j].name, fragment, StringComparison.InvariantCultureIgnoreCase))
					{
						if (!string.IsNullOrEmpty(text2))
						{
							text2 = text2 + "|" + partStrings[j];
						}
						else
						{
							text2 = partStrings[j];
						}
					}
				}
				if (string.IsNullOrEmpty(text2))
				{
					text2 = " ";
				}
				return text2;
			});
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x00006A68 File Offset: 0x00004C68
		public static void ApplyBindingOverride(this InputAction action, string newPath, string group = null, string path = null)
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			action.ApplyBindingOverride(new InputBinding
			{
				overridePath = newPath,
				groups = group,
				path = path
			});
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x00006AAC File Offset: 0x00004CAC
		public static void ApplyBindingOverride(this InputAction action, InputBinding bindingOverride)
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			bool enabled = action.enabled;
			if (enabled)
			{
				action.Disable();
			}
			bindingOverride.action = action.name;
			action.GetOrCreateActionMap().ApplyBindingOverride(bindingOverride);
			if (enabled)
			{
				action.Enable();
				action.RequestInitialStateCheckOnEnabledAction();
			}
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x00006B00 File Offset: 0x00004D00
		public static void ApplyBindingOverride(this InputAction action, int bindingIndex, InputBinding bindingOverride)
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			int num = action.BindingIndexOnActionToBindingIndexOnMap(bindingIndex);
			bindingOverride.action = action.name;
			action.GetOrCreateActionMap().ApplyBindingOverride(num, bindingOverride);
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x00006B40 File Offset: 0x00004D40
		public static void ApplyBindingOverride(this InputAction action, int bindingIndex, string path)
		{
			if (path == null)
			{
				throw new ArgumentException("Binding path cannot be null", "path");
			}
			action.ApplyBindingOverride(bindingIndex, new InputBinding
			{
				overridePath = path
			});
		}

		// Token: 0x060001EA RID: 490 RVA: 0x00006B78 File Offset: 0x00004D78
		public static int ApplyBindingOverride(this InputActionMap actionMap, InputBinding bindingOverride)
		{
			if (actionMap == null)
			{
				throw new ArgumentNullException("actionMap");
			}
			InputBinding[] bindings = actionMap.m_Bindings;
			if (bindings == null)
			{
				return 0;
			}
			int num = bindings.Length;
			int num2 = 0;
			for (int i = 0; i < num; i++)
			{
				if (bindingOverride.Matches(ref bindings[i], (InputBinding.MatchOptions)0))
				{
					bindings[i].overridePath = bindingOverride.overridePath;
					bindings[i].overrideInteractions = bindingOverride.overrideInteractions;
					bindings[i].overrideProcessors = bindingOverride.overrideProcessors;
					num2++;
				}
			}
			if (num2 > 0)
			{
				actionMap.OnBindingModified();
			}
			return num2;
		}

		// Token: 0x060001EB RID: 491 RVA: 0x00006C0C File Offset: 0x00004E0C
		public static void ApplyBindingOverride(this InputActionMap actionMap, int bindingIndex, InputBinding bindingOverride)
		{
			if (actionMap == null)
			{
				throw new ArgumentNullException("actionMap");
			}
			InputBinding[] bindings = actionMap.m_Bindings;
			int num = ((bindings != null) ? bindings.Length : 0);
			if (bindingIndex < 0 || bindingIndex >= num)
			{
				throw new ArgumentOutOfRangeException("bindingIndex", string.Format("Cannot apply override to binding at index {0} in map '{1}' with only {2} bindings", bindingIndex, actionMap, num));
			}
			actionMap.m_Bindings[bindingIndex].overridePath = bindingOverride.overridePath;
			actionMap.m_Bindings[bindingIndex].overrideInteractions = bindingOverride.overrideInteractions;
			actionMap.m_Bindings[bindingIndex].overrideProcessors = bindingOverride.overrideProcessors;
			actionMap.OnBindingModified();
		}

		// Token: 0x060001EC RID: 492 RVA: 0x00006CB0 File Offset: 0x00004EB0
		public static void RemoveBindingOverride(this InputAction action, int bindingIndex)
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			action.ApplyBindingOverride(bindingIndex, default(InputBinding));
		}

		// Token: 0x060001ED RID: 493 RVA: 0x00006CDB File Offset: 0x00004EDB
		public static void RemoveBindingOverride(this InputAction action, InputBinding bindingMask)
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			bindingMask.overridePath = null;
			bindingMask.overrideInteractions = null;
			bindingMask.overrideProcessors = null;
			action.ApplyBindingOverride(bindingMask);
		}

		// Token: 0x060001EE RID: 494 RVA: 0x00006D0A File Offset: 0x00004F0A
		private static void RemoveBindingOverride(this InputActionMap actionMap, InputBinding bindingMask)
		{
			if (actionMap == null)
			{
				throw new ArgumentNullException("actionMap");
			}
			bindingMask.overridePath = null;
			bindingMask.overrideInteractions = null;
			bindingMask.overrideProcessors = null;
			actionMap.ApplyBindingOverride(bindingMask);
		}

		// Token: 0x060001EF RID: 495 RVA: 0x00006D3C File Offset: 0x00004F3C
		public static void RemoveAllBindingOverrides(this IInputActionCollection2 actions)
		{
			if (actions == null)
			{
				throw new ArgumentNullException("actions");
			}
			using (InputActionRebindingExtensions.DeferBindingResolution())
			{
				foreach (InputAction inputAction in actions)
				{
					InputActionMap orCreateActionMap = inputAction.GetOrCreateActionMap();
					InputBinding[] bindings = orCreateActionMap.m_Bindings;
					int num = bindings.LengthSafe<InputBinding>();
					for (int i = 0; i < num; i++)
					{
						ref InputBinding ptr = ref bindings[i];
						if (ptr.TriggersAction(inputAction))
						{
							ptr.RemoveOverrides();
						}
					}
					orCreateActionMap.OnBindingModified();
				}
			}
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x00006DF4 File Offset: 0x00004FF4
		public static void RemoveAllBindingOverrides(this InputAction action)
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			string name = action.name;
			InputActionMap orCreateActionMap = action.GetOrCreateActionMap();
			InputBinding[] bindings = orCreateActionMap.m_Bindings;
			if (bindings == null)
			{
				return;
			}
			int num = bindings.Length;
			for (int i = 0; i < num; i++)
			{
				if (string.Compare(bindings[i].action, name, StringComparison.InvariantCultureIgnoreCase) == 0)
				{
					bindings[i].overridePath = null;
					bindings[i].overrideInteractions = null;
					bindings[i].overrideProcessors = null;
				}
			}
			orCreateActionMap.OnBindingModified();
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x00006E84 File Offset: 0x00005084
		public static void ApplyBindingOverrides(this InputActionMap actionMap, IEnumerable<InputBinding> overrides)
		{
			if (actionMap == null)
			{
				throw new ArgumentNullException("actionMap");
			}
			if (overrides == null)
			{
				throw new ArgumentNullException("overrides");
			}
			foreach (InputBinding inputBinding in overrides)
			{
				actionMap.ApplyBindingOverride(inputBinding);
			}
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x00006EEC File Offset: 0x000050EC
		public static void RemoveBindingOverrides(this InputActionMap actionMap, IEnumerable<InputBinding> overrides)
		{
			if (actionMap == null)
			{
				throw new ArgumentNullException("actionMap");
			}
			if (overrides == null)
			{
				throw new ArgumentNullException("overrides");
			}
			foreach (InputBinding inputBinding in overrides)
			{
				actionMap.RemoveBindingOverride(inputBinding);
			}
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x00006F50 File Offset: 0x00005150
		public static int ApplyBindingOverridesOnMatchingControls(this InputAction action, InputControl control)
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			ReadOnlyArray<InputBinding> bindings = action.bindings;
			int count = bindings.Count;
			int num = 0;
			for (int i = 0; i < count; i++)
			{
				InputControl inputControl = InputControlPath.TryFindControl(control, bindings[i].path, 0);
				if (inputControl != null)
				{
					action.ApplyBindingOverride(i, inputControl.path);
					num++;
				}
			}
			return num;
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x00006FC8 File Offset: 0x000051C8
		public static int ApplyBindingOverridesOnMatchingControls(this InputActionMap actionMap, InputControl control)
		{
			if (actionMap == null)
			{
				throw new ArgumentNullException("actionMap");
			}
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			ReadOnlyArray<InputAction> actions = actionMap.actions;
			int count = actions.Count;
			int num = 0;
			for (int i = 0; i < count; i++)
			{
				num = actions[i].ApplyBindingOverridesOnMatchingControls(control);
			}
			return num;
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x00007020 File Offset: 0x00005220
		public static string SaveBindingOverridesAsJson(this IInputActionCollection2 actions)
		{
			if (actions == null)
			{
				throw new ArgumentNullException("actions");
			}
			List<InputActionMap.BindingOverrideJson> list = new List<InputActionMap.BindingOverrideJson>();
			foreach (InputBinding inputBinding in actions.bindings)
			{
				actions.AddBindingOverrideJsonTo(inputBinding, list, null);
			}
			if (list.Count == 0)
			{
				return string.Empty;
			}
			return JsonUtility.ToJson(new InputActionMap.BindingOverrideListJson
			{
				bindings = list
			});
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x000070AC File Offset: 0x000052AC
		public static string SaveBindingOverridesAsJson(this InputAction action)
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			bool isSingletonAction = action.isSingletonAction;
			InputActionMap orCreateActionMap = action.GetOrCreateActionMap();
			List<InputActionMap.BindingOverrideJson> list = new List<InputActionMap.BindingOverrideJson>();
			foreach (InputBinding inputBinding in action.bindings)
			{
				if (isSingletonAction || inputBinding.TriggersAction(action))
				{
					orCreateActionMap.AddBindingOverrideJsonTo(inputBinding, list, isSingletonAction ? action : null);
				}
			}
			if (list.Count == 0)
			{
				return string.Empty;
			}
			return JsonUtility.ToJson(new InputActionMap.BindingOverrideListJson
			{
				bindings = list
			});
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x00007168 File Offset: 0x00005368
		private static void AddBindingOverrideJsonTo(this IInputActionCollection2 actions, InputBinding binding, List<InputActionMap.BindingOverrideJson> list, InputAction action = null)
		{
			if (!binding.hasOverrides)
			{
				return;
			}
			if (action == null)
			{
				action = actions.FindAction(binding.action, false);
			}
			string text = ((action != null && !action.isSingletonAction) ? (action.actionMap.name + "/" + action.name) : "");
			InputActionMap.BindingOverrideJson bindingOverrideJson = InputActionMap.BindingOverrideJson.FromBinding(binding, text);
			list.Add(bindingOverrideJson);
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x000071D0 File Offset: 0x000053D0
		public static void LoadBindingOverridesFromJson(this IInputActionCollection2 actions, string json, bool removeExisting = true)
		{
			if (actions == null)
			{
				throw new ArgumentNullException("actions");
			}
			using (InputActionRebindingExtensions.DeferBindingResolution())
			{
				if (removeExisting)
				{
					actions.RemoveAllBindingOverrides();
				}
				actions.LoadBindingOverridesFromJsonInternal(json);
			}
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x00007220 File Offset: 0x00005420
		public static void LoadBindingOverridesFromJson(this InputAction action, string json, bool removeExisting = true)
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			using (InputActionRebindingExtensions.DeferBindingResolution())
			{
				if (removeExisting)
				{
					action.RemoveAllBindingOverrides();
				}
				action.GetOrCreateActionMap().LoadBindingOverridesFromJsonInternal(json);
			}
		}

		// Token: 0x060001FA RID: 506 RVA: 0x00007274 File Offset: 0x00005474
		private static void LoadBindingOverridesFromJsonInternal(this IInputActionCollection2 actions, string json)
		{
			if (string.IsNullOrEmpty(json))
			{
				return;
			}
			foreach (InputActionMap.BindingOverrideJson bindingOverrideJson in JsonUtility.FromJson<InputActionMap.BindingOverrideListJson>(json).bindings)
			{
				if (!string.IsNullOrEmpty(bindingOverrideJson.id))
				{
					InputAction inputAction;
					int num = actions.FindBinding(new InputBinding
					{
						m_Id = bindingOverrideJson.id
					}, out inputAction);
					if (num != -1)
					{
						inputAction.ApplyBindingOverride(num, InputActionMap.BindingOverrideJson.ToBinding(bindingOverrideJson));
						continue;
					}
				}
				Debug.LogWarning("Could not override binding as no existing binding was found with the id: " + bindingOverrideJson.id);
			}
		}

		// Token: 0x060001FB RID: 507 RVA: 0x00007324 File Offset: 0x00005524
		public static InputActionRebindingExtensions.RebindingOperation PerformInteractiveRebinding(this InputAction action, int bindingIndex = -1)
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			InputActionRebindingExtensions.RebindingOperation rebindingOperation = new InputActionRebindingExtensions.RebindingOperation().WithAction(action).OnMatchWaitForAnother(0.05f).WithControlsExcluding("<Pointer>/delta")
				.WithControlsExcluding("<Pointer>/position")
				.WithControlsExcluding("<Touchscreen>/touch*/position")
				.WithControlsExcluding("<Touchscreen>/touch*/delta")
				.WithControlsExcluding("<Mouse>/clickCount")
				.WithMatchingEventsBeingSuppressed(true);
			if (rebindingOperation.expectedControlType != "Button")
			{
				rebindingOperation.WithCancelingThrough("<Keyboard>/escape");
			}
			if (bindingIndex >= 0)
			{
				ReadOnlyArray<InputBinding> bindings = action.bindings;
				if (bindingIndex >= bindings.Count)
				{
					throw new ArgumentOutOfRangeException(string.Format("Binding index {0} is out of range for action '{1}' with {2} bindings", bindingIndex, action, bindings.Count), "bindings");
				}
				if (bindings[bindingIndex].isComposite)
				{
					throw new InvalidOperationException(string.Format("Cannot perform rebinding on composite binding '{0}' of '{1}'", bindings[bindingIndex], action));
				}
				rebindingOperation.WithTargetBinding(bindingIndex);
			}
			return rebindingOperation;
		}

		// Token: 0x060001FC RID: 508 RVA: 0x00007421 File Offset: 0x00005621
		internal static InputActionRebindingExtensions.DeferBindingResolutionWrapper DeferBindingResolution()
		{
			if (InputActionRebindingExtensions.s_DeferBindingResolutionWrapper == null)
			{
				InputActionRebindingExtensions.s_DeferBindingResolutionWrapper = new InputActionRebindingExtensions.DeferBindingResolutionWrapper();
			}
			InputActionRebindingExtensions.s_DeferBindingResolutionWrapper.Acquire();
			return InputActionRebindingExtensions.s_DeferBindingResolutionWrapper;
		}

		// Token: 0x040000BD RID: 189
		private static InputActionRebindingExtensions.DeferBindingResolutionWrapper s_DeferBindingResolutionWrapper;

		// Token: 0x0200016C RID: 364
		internal struct Parameter
		{
			// Token: 0x040007DD RID: 2013
			public object instance;

			// Token: 0x040007DE RID: 2014
			public FieldInfo field;

			// Token: 0x040007DF RID: 2015
			public int bindingIndex;
		}

		// Token: 0x0200016D RID: 365
		private struct ParameterEnumerable : IEnumerable<InputActionRebindingExtensions.Parameter>, IEnumerable
		{
			// Token: 0x060012BC RID: 4796 RVA: 0x00058EFB File Offset: 0x000570FB
			public ParameterEnumerable(InputActionState state, InputActionRebindingExtensions.ParameterOverride parameter, int mapIndex = -1)
			{
				this.m_State = state;
				this.m_Parameter = parameter;
				this.m_MapIndex = mapIndex;
			}

			// Token: 0x060012BD RID: 4797 RVA: 0x00058F12 File Offset: 0x00057112
			public InputActionRebindingExtensions.ParameterEnumerator GetEnumerator()
			{
				return new InputActionRebindingExtensions.ParameterEnumerator(this.m_State, this.m_Parameter, this.m_MapIndex);
			}

			// Token: 0x060012BE RID: 4798 RVA: 0x00058F2B File Offset: 0x0005712B
			IEnumerator<InputActionRebindingExtensions.Parameter> IEnumerable<InputActionRebindingExtensions.Parameter>.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x060012BF RID: 4799 RVA: 0x00058F38 File Offset: 0x00057138
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x040007E0 RID: 2016
			private InputActionState m_State;

			// Token: 0x040007E1 RID: 2017
			private InputActionRebindingExtensions.ParameterOverride m_Parameter;

			// Token: 0x040007E2 RID: 2018
			private int m_MapIndex;
		}

		// Token: 0x0200016E RID: 366
		private struct ParameterEnumerator : IEnumerator<InputActionRebindingExtensions.Parameter>, IEnumerator, IDisposable
		{
			// Token: 0x060012C0 RID: 4800 RVA: 0x00058F48 File Offset: 0x00057148
			public ParameterEnumerator(InputActionState state, InputActionRebindingExtensions.ParameterOverride parameter, int mapIndex = -1)
			{
				this = default(InputActionRebindingExtensions.ParameterEnumerator);
				this.m_State = state;
				this.m_ParameterName = parameter.parameter;
				this.m_MapIndex = mapIndex;
				this.m_ObjectType = parameter.objectType;
				this.m_MayBeComposite = this.m_ObjectType == null || typeof(InputBindingComposite).IsAssignableFrom(this.m_ObjectType);
				this.m_MayBeProcessor = this.m_ObjectType == null || typeof(InputProcessor).IsAssignableFrom(this.m_ObjectType);
				this.m_MayBeInteraction = this.m_ObjectType == null || typeof(IInputInteraction).IsAssignableFrom(this.m_ObjectType);
				this.m_BindingMask = parameter.bindingMask;
				this.Reset();
			}

			// Token: 0x060012C1 RID: 4801 RVA: 0x0005901C File Offset: 0x0005721C
			private bool MoveToNextBinding()
			{
				ref InputActionState.BindingState bindingState;
				for (;;)
				{
					this.m_BindingCurrentIndex++;
					if (this.m_BindingCurrentIndex >= this.m_BindingEndIndex)
					{
						break;
					}
					ref InputBinding binding = ref this.m_State.GetBinding(this.m_BindingCurrentIndex);
					bindingState = this.m_State.GetBindingState(this.m_BindingCurrentIndex);
					if ((bindingState.processorCount != 0 || bindingState.interactionCount != 0 || binding.isComposite) && (!this.m_MayBeComposite || this.m_MayBeProcessor || this.m_MayBeInteraction || binding.isComposite) && (!this.m_MayBeProcessor || this.m_MayBeComposite || this.m_MayBeInteraction || bindingState.processorCount != 0) && (!this.m_MayBeInteraction || this.m_MayBeComposite || this.m_MayBeProcessor || bindingState.interactionCount != 0) && this.m_BindingMask.Matches(ref binding, (InputBinding.MatchOptions)0))
					{
						goto Block_12;
					}
				}
				return false;
				Block_12:
				if (this.m_MayBeComposite)
				{
					ref InputBinding binding;
					this.m_CurrentBindingIsComposite = binding.isComposite;
				}
				this.m_ProcessorCurrentIndex = bindingState.processorStartIndex - 1;
				this.m_ProcessorEndIndex = bindingState.processorStartIndex + bindingState.processorCount;
				this.m_InteractionCurrentIndex = bindingState.interactionStartIndex - 1;
				this.m_InteractionEndIndex = bindingState.interactionStartIndex + bindingState.interactionCount;
				return true;
			}

			// Token: 0x060012C2 RID: 4802 RVA: 0x00059154 File Offset: 0x00057354
			private bool MoveToNextInteraction()
			{
				while (this.m_InteractionCurrentIndex < this.m_InteractionEndIndex)
				{
					this.m_InteractionCurrentIndex++;
					if (this.m_InteractionCurrentIndex == this.m_InteractionEndIndex)
					{
						break;
					}
					IInputInteraction inputInteraction = this.m_State.interactions[this.m_InteractionCurrentIndex];
					if (this.FindParameter(inputInteraction))
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x060012C3 RID: 4803 RVA: 0x000591AC File Offset: 0x000573AC
			private bool MoveToNextProcessor()
			{
				while (this.m_ProcessorCurrentIndex < this.m_ProcessorEndIndex)
				{
					this.m_ProcessorCurrentIndex++;
					if (this.m_ProcessorCurrentIndex == this.m_ProcessorEndIndex)
					{
						break;
					}
					InputProcessor inputProcessor = this.m_State.processors[this.m_ProcessorCurrentIndex];
					if (this.FindParameter(inputProcessor))
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x060012C4 RID: 4804 RVA: 0x00059204 File Offset: 0x00057404
			private bool FindParameter(object instance)
			{
				if (this.m_ObjectType != null && !this.m_ObjectType.IsInstanceOfType(instance))
				{
					return false;
				}
				FieldInfo field = instance.GetType().GetField(this.m_ParameterName, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public);
				if (field == null)
				{
					return false;
				}
				this.m_CurrentParameter = field;
				this.m_CurrentObject = instance;
				return true;
			}

			// Token: 0x060012C5 RID: 4805 RVA: 0x00059260 File Offset: 0x00057460
			public bool MoveNext()
			{
				while (!this.m_MayBeInteraction || !this.MoveToNextInteraction())
				{
					if (this.m_MayBeProcessor && this.MoveToNextProcessor())
					{
						return true;
					}
					if (!this.MoveToNextBinding())
					{
						return false;
					}
					if (this.m_MayBeComposite && this.m_CurrentBindingIsComposite)
					{
						int compositeOrCompositeBindingIndex = this.m_State.GetBindingState(this.m_BindingCurrentIndex).compositeOrCompositeBindingIndex;
						InputBindingComposite inputBindingComposite = this.m_State.composites[compositeOrCompositeBindingIndex];
						if (this.FindParameter(inputBindingComposite))
						{
							return true;
						}
					}
				}
				return true;
			}

			// Token: 0x060012C6 RID: 4806 RVA: 0x000592DC File Offset: 0x000574DC
			public unsafe void Reset()
			{
				this.m_CurrentObject = null;
				this.m_CurrentParameter = null;
				this.m_InteractionCurrentIndex = 0;
				this.m_InteractionEndIndex = 0;
				this.m_ProcessorCurrentIndex = 0;
				this.m_ProcessorEndIndex = 0;
				this.m_CurrentBindingIsComposite = false;
				if (this.m_MapIndex < 0)
				{
					this.m_BindingCurrentIndex = -1;
					this.m_BindingEndIndex = this.m_State.totalBindingCount;
					return;
				}
				this.m_BindingCurrentIndex = this.m_State.mapIndices[this.m_MapIndex].bindingStartIndex - 1;
				this.m_BindingEndIndex = this.m_State.mapIndices[this.m_MapIndex].bindingStartIndex + this.m_State.mapIndices[this.m_MapIndex].bindingCount;
			}

			// Token: 0x1700050E RID: 1294
			// (get) Token: 0x060012C7 RID: 4807 RVA: 0x000593A8 File Offset: 0x000575A8
			public InputActionRebindingExtensions.Parameter Current
			{
				get
				{
					return new InputActionRebindingExtensions.Parameter
					{
						instance = this.m_CurrentObject,
						field = this.m_CurrentParameter,
						bindingIndex = this.m_BindingCurrentIndex
					};
				}
			}

			// Token: 0x1700050F RID: 1295
			// (get) Token: 0x060012C8 RID: 4808 RVA: 0x000593E5 File Offset: 0x000575E5
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x060012C9 RID: 4809 RVA: 0x000593F2 File Offset: 0x000575F2
			public void Dispose()
			{
			}

			// Token: 0x040007E3 RID: 2019
			private InputActionState m_State;

			// Token: 0x040007E4 RID: 2020
			private int m_MapIndex;

			// Token: 0x040007E5 RID: 2021
			private int m_BindingCurrentIndex;

			// Token: 0x040007E6 RID: 2022
			private int m_BindingEndIndex;

			// Token: 0x040007E7 RID: 2023
			private int m_InteractionCurrentIndex;

			// Token: 0x040007E8 RID: 2024
			private int m_InteractionEndIndex;

			// Token: 0x040007E9 RID: 2025
			private int m_ProcessorCurrentIndex;

			// Token: 0x040007EA RID: 2026
			private int m_ProcessorEndIndex;

			// Token: 0x040007EB RID: 2027
			private InputBinding m_BindingMask;

			// Token: 0x040007EC RID: 2028
			private Type m_ObjectType;

			// Token: 0x040007ED RID: 2029
			private string m_ParameterName;

			// Token: 0x040007EE RID: 2030
			private bool m_MayBeInteraction;

			// Token: 0x040007EF RID: 2031
			private bool m_MayBeProcessor;

			// Token: 0x040007F0 RID: 2032
			private bool m_MayBeComposite;

			// Token: 0x040007F1 RID: 2033
			private bool m_CurrentBindingIsComposite;

			// Token: 0x040007F2 RID: 2034
			private object m_CurrentObject;

			// Token: 0x040007F3 RID: 2035
			private FieldInfo m_CurrentParameter;
		}

		// Token: 0x0200016F RID: 367
		internal struct ParameterOverride
		{
			// Token: 0x17000510 RID: 1296
			// (get) Token: 0x060012CA RID: 4810 RVA: 0x000593F4 File Offset: 0x000575F4
			public Type objectType
			{
				get
				{
					Type type;
					if ((type = InputProcessor.s_Processors.LookupTypeRegistration(this.objectRegistrationName)) == null)
					{
						type = InputInteraction.s_Interactions.LookupTypeRegistration(this.objectRegistrationName) ?? InputBindingComposite.s_Composites.LookupTypeRegistration(this.objectRegistrationName);
					}
					return type;
				}
			}

			// Token: 0x060012CB RID: 4811 RVA: 0x00059430 File Offset: 0x00057630
			public ParameterOverride(string parameterName, InputBinding bindingMask, PrimitiveValue value = default(PrimitiveValue))
			{
				int num = parameterName.IndexOf(':');
				if (num < 0)
				{
					this.objectRegistrationName = null;
					this.parameter = parameterName;
				}
				else
				{
					this.objectRegistrationName = parameterName.Substring(0, num);
					this.parameter = parameterName.Substring(num + 1);
				}
				this.bindingMask = bindingMask;
				this.value = value;
			}

			// Token: 0x060012CC RID: 4812 RVA: 0x00059485 File Offset: 0x00057685
			public ParameterOverride(string objectRegistrationName, string parameterName, InputBinding bindingMask, PrimitiveValue value = default(PrimitiveValue))
			{
				this.objectRegistrationName = objectRegistrationName;
				this.parameter = parameterName;
				this.bindingMask = bindingMask;
				this.value = value;
			}

			// Token: 0x060012CD RID: 4813 RVA: 0x000594A4 File Offset: 0x000576A4
			public static InputActionRebindingExtensions.ParameterOverride? Find(InputActionMap actionMap, ref InputBinding binding, string parameterName, string objectRegistrationName)
			{
				InputActionRebindingExtensions.ParameterOverride? parameterOverride = InputActionRebindingExtensions.ParameterOverride.Find(actionMap.m_ParameterOverrides, actionMap.m_ParameterOverridesCount, ref binding, parameterName, objectRegistrationName);
				InputActionAsset asset = actionMap.asset;
				InputActionRebindingExtensions.ParameterOverride? parameterOverride2 = ((asset != null) ? InputActionRebindingExtensions.ParameterOverride.Find(asset.m_ParameterOverrides, asset.m_ParameterOverridesCount, ref binding, parameterName, objectRegistrationName) : null);
				return InputActionRebindingExtensions.ParameterOverride.PickMoreSpecificOne(parameterOverride, parameterOverride2);
			}

			// Token: 0x060012CE RID: 4814 RVA: 0x000594FC File Offset: 0x000576FC
			private static InputActionRebindingExtensions.ParameterOverride? Find(InputActionRebindingExtensions.ParameterOverride[] overrides, int overrideCount, ref InputBinding binding, string parameterName, string objectRegistrationName)
			{
				InputActionRebindingExtensions.ParameterOverride? parameterOverride = null;
				for (int i = 0; i < overrideCount; i++)
				{
					ref InputActionRebindingExtensions.ParameterOverride ptr = ref overrides[i];
					if (string.Equals(parameterName, ptr.parameter, StringComparison.OrdinalIgnoreCase) && ptr.bindingMask.Matches(binding) && (ptr.objectRegistrationName == null || string.Equals(ptr.objectRegistrationName, objectRegistrationName, StringComparison.OrdinalIgnoreCase)))
					{
						if (parameterOverride == null)
						{
							parameterOverride = new InputActionRebindingExtensions.ParameterOverride?(ptr);
						}
						else
						{
							parameterOverride = InputActionRebindingExtensions.ParameterOverride.PickMoreSpecificOne(parameterOverride, new InputActionRebindingExtensions.ParameterOverride?(ptr));
						}
					}
				}
				return parameterOverride;
			}

			// Token: 0x060012CF RID: 4815 RVA: 0x0005958C File Offset: 0x0005778C
			private static InputActionRebindingExtensions.ParameterOverride? PickMoreSpecificOne(InputActionRebindingExtensions.ParameterOverride? first, InputActionRebindingExtensions.ParameterOverride? second)
			{
				if (first == null)
				{
					return second;
				}
				if (second == null)
				{
					return first;
				}
				if (first.Value.objectRegistrationName != null && second.Value.objectRegistrationName == null)
				{
					return first;
				}
				if (second.Value.objectRegistrationName != null && first.Value.objectRegistrationName == null)
				{
					return second;
				}
				InputActionRebindingExtensions.ParameterOverride parameterOverride = first.Value;
				if (parameterOverride.bindingMask.effectivePath != null)
				{
					parameterOverride = second.Value;
					if (parameterOverride.bindingMask.effectivePath == null)
					{
						return first;
					}
				}
				parameterOverride = second.Value;
				if (parameterOverride.bindingMask.effectivePath != null)
				{
					parameterOverride = first.Value;
					if (parameterOverride.bindingMask.effectivePath == null)
					{
						return second;
					}
				}
				parameterOverride = first.Value;
				if (parameterOverride.bindingMask.action != null)
				{
					parameterOverride = second.Value;
					if (parameterOverride.bindingMask.action == null)
					{
						return first;
					}
				}
				parameterOverride = second.Value;
				if (parameterOverride.bindingMask.action != null)
				{
					parameterOverride = first.Value;
					if (parameterOverride.bindingMask.action == null)
					{
						return second;
					}
				}
				return first;
			}

			// Token: 0x040007F4 RID: 2036
			public string objectRegistrationName;

			// Token: 0x040007F5 RID: 2037
			public string parameter;

			// Token: 0x040007F6 RID: 2038
			public InputBinding bindingMask;

			// Token: 0x040007F7 RID: 2039
			public PrimitiveValue value;
		}

		// Token: 0x02000170 RID: 368
		public sealed class RebindingOperation : IDisposable
		{
			// Token: 0x17000511 RID: 1297
			// (get) Token: 0x060012D0 RID: 4816 RVA: 0x000596A4 File Offset: 0x000578A4
			public InputAction action
			{
				get
				{
					return this.m_ActionToRebind;
				}
			}

			// Token: 0x17000512 RID: 1298
			// (get) Token: 0x060012D1 RID: 4817 RVA: 0x000596AC File Offset: 0x000578AC
			public InputBinding? bindingMask
			{
				get
				{
					return this.m_BindingMask;
				}
			}

			// Token: 0x17000513 RID: 1299
			// (get) Token: 0x060012D2 RID: 4818 RVA: 0x000596B4 File Offset: 0x000578B4
			public InputControlList<InputControl> candidates
			{
				get
				{
					return this.m_Candidates;
				}
			}

			// Token: 0x17000514 RID: 1300
			// (get) Token: 0x060012D3 RID: 4819 RVA: 0x000596BC File Offset: 0x000578BC
			public ReadOnlyArray<float> scores
			{
				get
				{
					return new ReadOnlyArray<float>(this.m_Scores, 0, this.m_Candidates.Count);
				}
			}

			// Token: 0x17000515 RID: 1301
			// (get) Token: 0x060012D4 RID: 4820 RVA: 0x000596D5 File Offset: 0x000578D5
			public ReadOnlyArray<float> magnitudes
			{
				get
				{
					return new ReadOnlyArray<float>(this.m_Magnitudes, 0, this.m_Candidates.Count);
				}
			}

			// Token: 0x17000516 RID: 1302
			// (get) Token: 0x060012D5 RID: 4821 RVA: 0x000596EE File Offset: 0x000578EE
			public InputControl selectedControl
			{
				get
				{
					if (this.m_Candidates.Count == 0)
					{
						return null;
					}
					return this.m_Candidates[0];
				}
			}

			// Token: 0x17000517 RID: 1303
			// (get) Token: 0x060012D6 RID: 4822 RVA: 0x0005970B File Offset: 0x0005790B
			public bool started
			{
				get
				{
					return (this.m_Flags & InputActionRebindingExtensions.RebindingOperation.Flags.Started) > (InputActionRebindingExtensions.RebindingOperation.Flags)0;
				}
			}

			// Token: 0x17000518 RID: 1304
			// (get) Token: 0x060012D7 RID: 4823 RVA: 0x00059718 File Offset: 0x00057918
			public bool completed
			{
				get
				{
					return (this.m_Flags & InputActionRebindingExtensions.RebindingOperation.Flags.Completed) > (InputActionRebindingExtensions.RebindingOperation.Flags)0;
				}
			}

			// Token: 0x17000519 RID: 1305
			// (get) Token: 0x060012D8 RID: 4824 RVA: 0x00059725 File Offset: 0x00057925
			public bool canceled
			{
				get
				{
					return (this.m_Flags & InputActionRebindingExtensions.RebindingOperation.Flags.Canceled) > (InputActionRebindingExtensions.RebindingOperation.Flags)0;
				}
			}

			// Token: 0x1700051A RID: 1306
			// (get) Token: 0x060012D9 RID: 4825 RVA: 0x00059732 File Offset: 0x00057932
			public double startTime
			{
				get
				{
					return this.m_StartTime;
				}
			}

			// Token: 0x1700051B RID: 1307
			// (get) Token: 0x060012DA RID: 4826 RVA: 0x0005973A File Offset: 0x0005793A
			public float timeout
			{
				get
				{
					return this.m_Timeout;
				}
			}

			// Token: 0x1700051C RID: 1308
			// (get) Token: 0x060012DB RID: 4827 RVA: 0x00059742 File Offset: 0x00057942
			public string expectedControlType
			{
				get
				{
					return this.m_ExpectedLayout;
				}
			}

			// Token: 0x060012DC RID: 4828 RVA: 0x00059750 File Offset: 0x00057950
			public InputActionRebindingExtensions.RebindingOperation WithAction(InputAction action)
			{
				this.ThrowIfRebindInProgress();
				if (action == null)
				{
					throw new ArgumentNullException("action");
				}
				if (action.enabled)
				{
					throw new InvalidOperationException(string.Format("Cannot rebind action '{0}' while it is enabled", action));
				}
				this.m_ActionToRebind = action;
				if (!string.IsNullOrEmpty(action.expectedControlType))
				{
					this.WithExpectedControlType(action.expectedControlType);
				}
				else if (action.type == InputActionType.Button)
				{
					this.WithExpectedControlType("Button");
				}
				return this;
			}

			// Token: 0x060012DD RID: 4829 RVA: 0x000597C3 File Offset: 0x000579C3
			public InputActionRebindingExtensions.RebindingOperation WithMatchingEventsBeingSuppressed(bool value = true)
			{
				this.ThrowIfRebindInProgress();
				if (value)
				{
					this.m_Flags |= InputActionRebindingExtensions.RebindingOperation.Flags.SuppressMatchingEvents;
				}
				else
				{
					this.m_Flags &= ~InputActionRebindingExtensions.RebindingOperation.Flags.SuppressMatchingEvents;
				}
				return this;
			}

			// Token: 0x060012DE RID: 4830 RVA: 0x000597F5 File Offset: 0x000579F5
			public InputActionRebindingExtensions.RebindingOperation WithCancelingThrough(string binding)
			{
				this.ThrowIfRebindInProgress();
				this.m_CancelBinding = binding;
				return this;
			}

			// Token: 0x060012DF RID: 4831 RVA: 0x00059805 File Offset: 0x00057A05
			public InputActionRebindingExtensions.RebindingOperation WithCancelingThrough(InputControl control)
			{
				this.ThrowIfRebindInProgress();
				if (control == null)
				{
					throw new ArgumentNullException("control");
				}
				return this.WithCancelingThrough(control.path);
			}

			// Token: 0x060012E0 RID: 4832 RVA: 0x00059827 File Offset: 0x00057A27
			public InputActionRebindingExtensions.RebindingOperation WithExpectedControlType(string layoutName)
			{
				this.ThrowIfRebindInProgress();
				this.m_ExpectedLayout = new InternedString(layoutName);
				return this;
			}

			// Token: 0x060012E1 RID: 4833 RVA: 0x0005983C File Offset: 0x00057A3C
			public InputActionRebindingExtensions.RebindingOperation WithExpectedControlType(Type type)
			{
				this.ThrowIfRebindInProgress();
				if (type != null && !typeof(InputControl).IsAssignableFrom(type))
				{
					throw new ArgumentException("Type '" + type.Name + "' is not an InputControl", "type");
				}
				this.m_ControlType = type;
				return this;
			}

			// Token: 0x060012E2 RID: 4834 RVA: 0x00059892 File Offset: 0x00057A92
			public InputActionRebindingExtensions.RebindingOperation WithExpectedControlType<TControl>() where TControl : InputControl
			{
				this.ThrowIfRebindInProgress();
				return this.WithExpectedControlType(typeof(TControl));
			}

			// Token: 0x060012E3 RID: 4835 RVA: 0x000598AC File Offset: 0x00057AAC
			public InputActionRebindingExtensions.RebindingOperation WithTargetBinding(int bindingIndex)
			{
				if (bindingIndex < 0)
				{
					throw new ArgumentOutOfRangeException("bindingIndex");
				}
				this.m_TargetBindingIndex = bindingIndex;
				if (this.m_ActionToRebind != null && bindingIndex < this.m_ActionToRebind.bindings.Count)
				{
					InputBinding inputBinding = this.m_ActionToRebind.bindings[bindingIndex];
					if (inputBinding.isPartOfComposite)
					{
						string nameOfComposite = this.m_ActionToRebind.ChangeBinding(bindingIndex).PreviousCompositeBinding(null).binding.GetNameOfComposite();
						string name = inputBinding.name;
						string expectedControlLayoutName = InputBindingComposite.GetExpectedControlLayoutName(nameOfComposite, name);
						if (!string.IsNullOrEmpty(expectedControlLayoutName))
						{
							this.WithExpectedControlType(expectedControlLayoutName);
						}
					}
					InputActionMap actionMap = this.action.actionMap;
					InputActionAsset inputActionAsset = ((actionMap != null) ? actionMap.asset : null);
					if (inputActionAsset != null && !string.IsNullOrEmpty(inputBinding.groups))
					{
						string[] array = inputBinding.groups.Split(';', StringSplitOptions.None);
						for (int i = 0; i < array.Length; i++)
						{
							string group = array[i];
							int num = inputActionAsset.controlSchemes.IndexOf((InputControlScheme x) => group.Equals(x.bindingGroup, StringComparison.InvariantCultureIgnoreCase));
							if (num != -1)
							{
								foreach (InputControlScheme.DeviceRequirement deviceRequirement in inputActionAsset.controlSchemes[num].deviceRequirements)
								{
									this.WithControlsHavingToMatchPath(deviceRequirement.controlPath);
								}
							}
						}
					}
				}
				return this;
			}

			// Token: 0x060012E4 RID: 4836 RVA: 0x00059A58 File Offset: 0x00057C58
			public InputActionRebindingExtensions.RebindingOperation WithBindingMask(InputBinding? bindingMask)
			{
				this.m_BindingMask = bindingMask;
				return this;
			}

			// Token: 0x060012E5 RID: 4837 RVA: 0x00059A64 File Offset: 0x00057C64
			public InputActionRebindingExtensions.RebindingOperation WithBindingGroup(string group)
			{
				return this.WithBindingMask(new InputBinding?(new InputBinding
				{
					groups = group
				}));
			}

			// Token: 0x060012E6 RID: 4838 RVA: 0x00059A8D File Offset: 0x00057C8D
			public InputActionRebindingExtensions.RebindingOperation WithoutGeneralizingPathOfSelectedControl()
			{
				this.m_Flags |= InputActionRebindingExtensions.RebindingOperation.Flags.DontGeneralizePathOfSelectedControl;
				return this;
			}

			// Token: 0x060012E7 RID: 4839 RVA: 0x00059AA2 File Offset: 0x00057CA2
			public InputActionRebindingExtensions.RebindingOperation WithRebindAddingNewBinding(string group = null)
			{
				this.m_Flags |= InputActionRebindingExtensions.RebindingOperation.Flags.AddNewBinding;
				this.m_BindingGroupForNewBinding = group;
				return this;
			}

			// Token: 0x060012E8 RID: 4840 RVA: 0x00059ABE File Offset: 0x00057CBE
			public InputActionRebindingExtensions.RebindingOperation WithMagnitudeHavingToBeGreaterThan(float magnitude)
			{
				this.ThrowIfRebindInProgress();
				if (magnitude < 0f)
				{
					throw new ArgumentException(string.Format("Magnitude has to be positive but was {0}", magnitude), "magnitude");
				}
				this.m_MagnitudeThreshold = magnitude;
				return this;
			}

			// Token: 0x060012E9 RID: 4841 RVA: 0x00059AF1 File Offset: 0x00057CF1
			public InputActionRebindingExtensions.RebindingOperation WithoutIgnoringNoisyControls()
			{
				this.ThrowIfRebindInProgress();
				this.m_Flags |= InputActionRebindingExtensions.RebindingOperation.Flags.DontIgnoreNoisyControls;
				return this;
			}

			// Token: 0x060012EA RID: 4842 RVA: 0x00059B0C File Offset: 0x00057D0C
			public InputActionRebindingExtensions.RebindingOperation WithControlsHavingToMatchPath(string path)
			{
				this.ThrowIfRebindInProgress();
				if (string.IsNullOrEmpty(path))
				{
					throw new ArgumentNullException("path");
				}
				for (int i = 0; i < this.m_IncludePathCount; i++)
				{
					if (string.Compare(this.m_IncludePaths[i], path, StringComparison.InvariantCultureIgnoreCase) == 0)
					{
						return this;
					}
				}
				ArrayHelpers.AppendWithCapacity<string>(ref this.m_IncludePaths, ref this.m_IncludePathCount, path, 10);
				return this;
			}

			// Token: 0x060012EB RID: 4843 RVA: 0x00059B6C File Offset: 0x00057D6C
			public InputActionRebindingExtensions.RebindingOperation WithControlsExcluding(string path)
			{
				this.ThrowIfRebindInProgress();
				if (string.IsNullOrEmpty(path))
				{
					throw new ArgumentNullException("path");
				}
				for (int i = 0; i < this.m_ExcludePathCount; i++)
				{
					if (string.Compare(this.m_ExcludePaths[i], path, StringComparison.InvariantCultureIgnoreCase) == 0)
					{
						return this;
					}
				}
				ArrayHelpers.AppendWithCapacity<string>(ref this.m_ExcludePaths, ref this.m_ExcludePathCount, path, 10);
				return this;
			}

			// Token: 0x060012EC RID: 4844 RVA: 0x00059BCC File Offset: 0x00057DCC
			public InputActionRebindingExtensions.RebindingOperation WithTimeout(float timeInSeconds)
			{
				this.ThrowIfRebindInProgress();
				this.m_Timeout = timeInSeconds;
				return this;
			}

			// Token: 0x060012ED RID: 4845 RVA: 0x00059BDC File Offset: 0x00057DDC
			public InputActionRebindingExtensions.RebindingOperation OnComplete(Action<InputActionRebindingExtensions.RebindingOperation> callback)
			{
				this.m_OnComplete = callback;
				return this;
			}

			// Token: 0x060012EE RID: 4846 RVA: 0x00059BE6 File Offset: 0x00057DE6
			public InputActionRebindingExtensions.RebindingOperation OnCancel(Action<InputActionRebindingExtensions.RebindingOperation> callback)
			{
				this.m_OnCancel = callback;
				return this;
			}

			// Token: 0x060012EF RID: 4847 RVA: 0x00059BF0 File Offset: 0x00057DF0
			public InputActionRebindingExtensions.RebindingOperation OnPotentialMatch(Action<InputActionRebindingExtensions.RebindingOperation> callback)
			{
				this.m_OnPotentialMatch = callback;
				return this;
			}

			// Token: 0x060012F0 RID: 4848 RVA: 0x00059BFA File Offset: 0x00057DFA
			public InputActionRebindingExtensions.RebindingOperation OnGeneratePath(Func<InputControl, string> callback)
			{
				this.m_OnGeneratePath = callback;
				return this;
			}

			// Token: 0x060012F1 RID: 4849 RVA: 0x00059C04 File Offset: 0x00057E04
			public InputActionRebindingExtensions.RebindingOperation OnComputeScore(Func<InputControl, InputEventPtr, float> callback)
			{
				this.m_OnComputeScore = callback;
				return this;
			}

			// Token: 0x060012F2 RID: 4850 RVA: 0x00059C0E File Offset: 0x00057E0E
			public InputActionRebindingExtensions.RebindingOperation OnApplyBinding(Action<InputActionRebindingExtensions.RebindingOperation, string> callback)
			{
				this.m_OnApplyBinding = callback;
				return this;
			}

			// Token: 0x060012F3 RID: 4851 RVA: 0x00059C18 File Offset: 0x00057E18
			public InputActionRebindingExtensions.RebindingOperation OnMatchWaitForAnother(float seconds)
			{
				this.m_WaitSecondsAfterMatch = seconds;
				return this;
			}

			// Token: 0x060012F4 RID: 4852 RVA: 0x00059C22 File Offset: 0x00057E22
			public InputActionRebindingExtensions.RebindingOperation WithActionEventNotificationsBeingSuppressed(bool value = true)
			{
				this.ThrowIfRebindInProgress();
				this.m_TargetInputEventHandledPolicy = (value ? InputEventHandledPolicy.SuppressActionEventNotifications : InputEventHandledPolicy.SuppressStateUpdates);
				return this;
			}

			// Token: 0x060012F5 RID: 4853 RVA: 0x00059C38 File Offset: 0x00057E38
			public InputActionRebindingExtensions.RebindingOperation Start()
			{
				if (this.started)
				{
					return this;
				}
				if (this.m_ActionToRebind != null && this.m_ActionToRebind.bindings.Count == 0 && (this.m_Flags & InputActionRebindingExtensions.RebindingOperation.Flags.AddNewBinding) == (InputActionRebindingExtensions.RebindingOperation.Flags)0)
				{
					throw new InvalidOperationException(string.Format("Action '{0}' must have at least one existing binding or must be used with WithRebindingAddNewBinding()", this.action));
				}
				if (this.m_ActionToRebind == null && this.m_OnApplyBinding == null)
				{
					throw new InvalidOperationException("Must either have an action (call WithAction()) to apply binding to or have a custom callback to apply the binding (call OnApplyBinding())");
				}
				this.m_StartTime = InputState.currentTime;
				this.m_SavedInputEventHandledPolicy = InputSystem.s_Manager.inputEventHandledPolicy;
				InputSystem.s_Manager.inputEventHandledPolicy = this.m_TargetInputEventHandledPolicy;
				if (this.m_WaitSecondsAfterMatch > 0f || this.m_Timeout > 0f)
				{
					this.HookOnAfterUpdate();
					this.m_LastMatchTime = -1.0;
				}
				this.HookOnEvent();
				this.m_Flags |= InputActionRebindingExtensions.RebindingOperation.Flags.Started;
				this.m_Flags &= ~InputActionRebindingExtensions.RebindingOperation.Flags.Canceled;
				this.m_Flags &= ~InputActionRebindingExtensions.RebindingOperation.Flags.Completed;
				return this;
			}

			// Token: 0x060012F6 RID: 4854 RVA: 0x00059D38 File Offset: 0x00057F38
			public void Cancel()
			{
				if (!this.started)
				{
					return;
				}
				this.OnCancel();
			}

			// Token: 0x060012F7 RID: 4855 RVA: 0x00059D49 File Offset: 0x00057F49
			public void Complete()
			{
				if (!this.started)
				{
					return;
				}
				this.OnComplete();
			}

			// Token: 0x060012F8 RID: 4856 RVA: 0x00059D5C File Offset: 0x00057F5C
			public void AddCandidate(InputControl control, float score, float magnitude = -1f)
			{
				if (control == null)
				{
					throw new ArgumentNullException("control");
				}
				int num = this.m_Candidates.IndexOf(control);
				if (num != -1)
				{
					this.m_Scores[num] = score;
				}
				else
				{
					int count = this.m_Candidates.Count;
					int count2 = this.m_Candidates.Count;
					this.m_Candidates.Add(control);
					ArrayHelpers.AppendWithCapacity<float>(ref this.m_Scores, ref count, score, 10);
					ArrayHelpers.AppendWithCapacity<float>(ref this.m_Magnitudes, ref count2, magnitude, 10);
				}
				this.SortCandidatesByScore();
			}

			// Token: 0x060012F9 RID: 4857 RVA: 0x00059DE0 File Offset: 0x00057FE0
			public void RemoveCandidate(InputControl control)
			{
				if (control == null)
				{
					throw new ArgumentNullException("control");
				}
				int num = this.m_Candidates.IndexOf(control);
				if (num == -1)
				{
					return;
				}
				int count = this.m_Candidates.Count;
				this.m_Candidates.RemoveAt(num);
				this.m_Scores.EraseAtWithCapacity<float>(ref count, num);
			}

			// Token: 0x060012FA RID: 4858 RVA: 0x00059E33 File Offset: 0x00058033
			public void Dispose()
			{
				this.UnhookOnEvent();
				this.UnhookOnAfterUpdate();
				this.m_Candidates.Dispose();
				this.m_LayoutCache.Clear();
			}

			// Token: 0x060012FB RID: 4859 RVA: 0x00059E58 File Offset: 0x00058058
			~RebindingOperation()
			{
				this.Dispose();
			}

			// Token: 0x060012FC RID: 4860 RVA: 0x00059E84 File Offset: 0x00058084
			public InputActionRebindingExtensions.RebindingOperation Reset()
			{
				this.Cancel();
				this.m_ActionToRebind = null;
				this.m_BindingMask = null;
				this.m_ControlType = null;
				this.m_ExpectedLayout = default(InternedString);
				this.m_IncludePathCount = 0;
				this.m_ExcludePathCount = 0;
				this.m_TargetBindingIndex = -1;
				this.m_BindingGroupForNewBinding = null;
				this.m_CancelBinding = null;
				this.m_MagnitudeThreshold = 0.2f;
				this.m_Timeout = 0f;
				this.m_WaitSecondsAfterMatch = 0f;
				this.m_Flags = (InputActionRebindingExtensions.RebindingOperation.Flags)0;
				Dictionary<InputControl, float> startingActuations = this.m_StartingActuations;
				if (startingActuations != null)
				{
					startingActuations.Clear();
				}
				return this;
			}

			// Token: 0x060012FD RID: 4861 RVA: 0x00059F1C File Offset: 0x0005811C
			private void HookOnEvent()
			{
				if ((this.m_Flags & InputActionRebindingExtensions.RebindingOperation.Flags.OnEventHooked) != (InputActionRebindingExtensions.RebindingOperation.Flags)0)
				{
					return;
				}
				if (this.m_OnEventDelegate == null)
				{
					this.m_OnEventDelegate = new Action<InputEventPtr, InputDevice>(this.OnEvent);
				}
				InputSystem.onEvent += this.m_OnEventDelegate;
				this.m_Flags |= InputActionRebindingExtensions.RebindingOperation.Flags.OnEventHooked;
			}

			// Token: 0x060012FE RID: 4862 RVA: 0x00059F71 File Offset: 0x00058171
			private void UnhookOnEvent()
			{
				if ((this.m_Flags & InputActionRebindingExtensions.RebindingOperation.Flags.OnEventHooked) == (InputActionRebindingExtensions.RebindingOperation.Flags)0)
				{
					return;
				}
				InputSystem.onEvent -= this.m_OnEventDelegate;
				this.m_Flags &= ~InputActionRebindingExtensions.RebindingOperation.Flags.OnEventHooked;
			}

			// Token: 0x060012FF RID: 4863 RVA: 0x00059FA4 File Offset: 0x000581A4
			private unsafe void OnEvent(InputEventPtr eventPtr, InputDevice device)
			{
				FourCC type = eventPtr.type;
				if (type != 1398030676 && type != 1145852993)
				{
					return;
				}
				bool flag = false;
				bool flag2 = false;
				InputControlExtensions.Enumerate enumerate = InputControlExtensions.Enumerate.IncludeSyntheticControls | InputControlExtensions.Enumerate.IncludeNonLeafControls;
				if ((this.m_Flags & InputActionRebindingExtensions.RebindingOperation.Flags.DontIgnoreNoisyControls) != (InputActionRebindingExtensions.RebindingOperation.Flags)0)
				{
					enumerate |= InputControlExtensions.Enumerate.IncludeNoisyControls;
				}
				foreach (InputControl inputControl in eventPtr.EnumerateControls(enumerate, device, 0f))
				{
					void* statePtrFromStateEventUnchecked = inputControl.GetStatePtrFromStateEventUnchecked(eventPtr, type);
					if (!string.IsNullOrEmpty(this.m_CancelBinding) && InputControlPath.Matches(this.m_CancelBinding, inputControl) && inputControl.HasValueChangeInState(statePtrFromStateEventUnchecked))
					{
						eventPtr.handled = true;
						this.OnCancel();
						break;
					}
					if ((this.m_ExcludePathCount <= 0 || !InputActionRebindingExtensions.RebindingOperation.HavePathMatch(inputControl, this.m_ExcludePaths, this.m_ExcludePathCount)) && (this.m_IncludePathCount <= 0 || InputActionRebindingExtensions.RebindingOperation.HavePathMatch(inputControl, this.m_IncludePaths, this.m_IncludePathCount)) && (!(this.m_ControlType != null) || this.m_ControlType.IsInstanceOfType(inputControl)) && (this.m_ExpectedLayout.IsEmpty() || !(this.m_ExpectedLayout != inputControl.m_Layout) || InputControlLayout.s_Layouts.IsBasedOn(this.m_ExpectedLayout, inputControl.m_Layout)))
					{
						if (inputControl.CheckStateIsAtDefault(statePtrFromStateEventUnchecked, null))
						{
							if (!this.m_StartingActuations.ContainsKey(inputControl))
							{
								this.m_StartingActuations.Add(inputControl, 0f);
							}
							this.m_StartingActuations[inputControl] = 0f;
						}
						else
						{
							flag2 = true;
							float num = inputControl.EvaluateMagnitude(statePtrFromStateEventUnchecked);
							if (num >= 0f)
							{
								float magnitude;
								if (!this.m_StartingActuations.TryGetValue(inputControl, out magnitude))
								{
									magnitude = inputControl.magnitude;
									this.m_StartingActuations.Add(inputControl, magnitude);
								}
								if (Mathf.Abs(magnitude - num) < this.m_MagnitudeThreshold)
								{
									continue;
								}
							}
							float num2;
							if (this.m_OnComputeScore != null)
							{
								num2 = this.m_OnComputeScore(inputControl, eventPtr);
							}
							else
							{
								num2 = num;
								if (!inputControl.synthetic)
								{
									num2 += 1f;
								}
							}
							int num3 = this.m_Candidates.IndexOf(inputControl);
							if (num3 != -1)
							{
								if (this.m_Scores[num3] < num2)
								{
									flag = true;
									this.m_Scores[num3] = num2;
									if (this.m_WaitSecondsAfterMatch > 0f)
									{
										this.m_LastMatchTime = InputState.currentTime;
									}
								}
							}
							else
							{
								int count = this.m_Candidates.Count;
								int count2 = this.m_Candidates.Count;
								this.m_Candidates.Add(inputControl);
								ArrayHelpers.AppendWithCapacity<float>(ref this.m_Scores, ref count, num2, 10);
								ArrayHelpers.AppendWithCapacity<float>(ref this.m_Magnitudes, ref count2, num, 10);
								flag = true;
								if (this.m_WaitSecondsAfterMatch > 0f)
								{
									this.m_LastMatchTime = InputState.currentTime;
								}
							}
						}
					}
				}
				if (flag2 && (this.m_Flags & InputActionRebindingExtensions.RebindingOperation.Flags.SuppressMatchingEvents) != (InputActionRebindingExtensions.RebindingOperation.Flags)0)
				{
					eventPtr.handled = true;
				}
				if (flag && !this.canceled)
				{
					if (this.m_OnPotentialMatch != null)
					{
						this.SortCandidatesByScore();
						this.m_OnPotentialMatch(this);
						return;
					}
					if (this.m_WaitSecondsAfterMatch <= 0f)
					{
						this.OnComplete();
						return;
					}
					this.SortCandidatesByScore();
				}
			}

			// Token: 0x06001300 RID: 4864 RVA: 0x0005A30C File Offset: 0x0005850C
			private void SortCandidatesByScore()
			{
				int count = this.m_Candidates.Count;
				if (count <= 1)
				{
					return;
				}
				for (int i = 1; i < count; i++)
				{
					int num = i;
					while (num > 0 && this.m_Scores[num - 1] < this.m_Scores[num])
					{
						int num2 = num - 1;
						this.m_Scores.SwapElements<float>(num, num2);
						this.m_Candidates.SwapElements(num, num2);
						this.m_Magnitudes.SwapElements<float>(num, num2);
						num--;
					}
				}
			}

			// Token: 0x06001301 RID: 4865 RVA: 0x0005A384 File Offset: 0x00058584
			private static bool HavePathMatch(InputControl control, string[] paths, int pathCount)
			{
				for (int i = 0; i < pathCount; i++)
				{
					if (InputControlPath.MatchesPrefix(paths[i], control))
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x06001302 RID: 4866 RVA: 0x0005A3AC File Offset: 0x000585AC
			private void HookOnAfterUpdate()
			{
				if ((this.m_Flags & InputActionRebindingExtensions.RebindingOperation.Flags.OnAfterUpdateHooked) != (InputActionRebindingExtensions.RebindingOperation.Flags)0)
				{
					return;
				}
				if (this.m_OnAfterUpdateDelegate == null)
				{
					this.m_OnAfterUpdateDelegate = new Action(this.OnAfterUpdate);
				}
				InputSystem.onAfterUpdate += this.m_OnAfterUpdateDelegate;
				this.m_Flags |= InputActionRebindingExtensions.RebindingOperation.Flags.OnAfterUpdateHooked;
			}

			// Token: 0x06001303 RID: 4867 RVA: 0x0005A3F9 File Offset: 0x000585F9
			private void UnhookOnAfterUpdate()
			{
				if ((this.m_Flags & InputActionRebindingExtensions.RebindingOperation.Flags.OnAfterUpdateHooked) == (InputActionRebindingExtensions.RebindingOperation.Flags)0)
				{
					return;
				}
				InputSystem.onAfterUpdate -= this.m_OnAfterUpdateDelegate;
				this.m_Flags &= ~InputActionRebindingExtensions.RebindingOperation.Flags.OnAfterUpdateHooked;
			}

			// Token: 0x06001304 RID: 4868 RVA: 0x0005A424 File Offset: 0x00058624
			private void OnAfterUpdate()
			{
				if (this.m_LastMatchTime < 0.0 && this.m_Timeout > 0f && InputState.currentTime - this.m_StartTime > (double)this.m_Timeout)
				{
					this.Cancel();
					return;
				}
				if (this.m_WaitSecondsAfterMatch <= 0f)
				{
					return;
				}
				if (this.m_LastMatchTime < 0.0)
				{
					return;
				}
				if (InputState.currentTime >= this.m_LastMatchTime + (double)this.m_WaitSecondsAfterMatch)
				{
					this.Complete();
				}
			}

			// Token: 0x06001305 RID: 4869 RVA: 0x0005A4A8 File Offset: 0x000586A8
			private void OnComplete()
			{
				this.SortCandidatesByScore();
				if (this.m_Candidates.Count > 0)
				{
					InputControl inputControl = this.m_Candidates[0];
					string text = inputControl.path;
					if (this.m_OnGeneratePath != null)
					{
						string text2 = this.m_OnGeneratePath(inputControl);
						if (!string.IsNullOrEmpty(text2))
						{
							text = text2;
						}
						else if ((this.m_Flags & InputActionRebindingExtensions.RebindingOperation.Flags.DontGeneralizePathOfSelectedControl) == (InputActionRebindingExtensions.RebindingOperation.Flags)0)
						{
							text = this.GeneratePathForControl(inputControl);
						}
					}
					else if ((this.m_Flags & InputActionRebindingExtensions.RebindingOperation.Flags.DontGeneralizePathOfSelectedControl) == (InputActionRebindingExtensions.RebindingOperation.Flags)0)
					{
						text = this.GeneratePathForControl(inputControl);
					}
					if (this.m_OnApplyBinding != null)
					{
						this.m_OnApplyBinding(this, text);
					}
					else if ((this.m_Flags & InputActionRebindingExtensions.RebindingOperation.Flags.AddNewBinding) != (InputActionRebindingExtensions.RebindingOperation.Flags)0)
					{
						this.m_ActionToRebind.AddBinding(text, null, null, this.m_BindingGroupForNewBinding);
					}
					else if (this.m_TargetBindingIndex >= 0)
					{
						if (this.m_TargetBindingIndex >= this.m_ActionToRebind.bindings.Count)
						{
							throw new InvalidOperationException(string.Format("Target binding index {0} out of range for action '{1}' with {2} bindings", this.m_TargetBindingIndex, this.m_ActionToRebind, this.m_ActionToRebind.bindings.Count));
						}
						this.m_ActionToRebind.ApplyBindingOverride(this.m_TargetBindingIndex, text);
					}
					else if (this.m_BindingMask != null)
					{
						InputBinding value = this.m_BindingMask.Value;
						value.overridePath = text;
						this.m_ActionToRebind.ApplyBindingOverride(value);
					}
					else
					{
						this.m_ActionToRebind.ApplyBindingOverride(text, null, null);
					}
				}
				this.m_Flags |= InputActionRebindingExtensions.RebindingOperation.Flags.Completed;
				Action<InputActionRebindingExtensions.RebindingOperation> onComplete = this.m_OnComplete;
				if (onComplete != null)
				{
					onComplete(this);
				}
				this.ResetAfterMatchCompleted();
			}

			// Token: 0x06001306 RID: 4870 RVA: 0x0005A647 File Offset: 0x00058847
			private void OnCancel()
			{
				this.m_Flags |= InputActionRebindingExtensions.RebindingOperation.Flags.Canceled;
				Action<InputActionRebindingExtensions.RebindingOperation> onCancel = this.m_OnCancel;
				if (onCancel != null)
				{
					onCancel(this);
				}
				this.ResetAfterMatchCompleted();
			}

			// Token: 0x06001307 RID: 4871 RVA: 0x0005A670 File Offset: 0x00058870
			private void ResetAfterMatchCompleted()
			{
				this.m_Flags &= ~InputActionRebindingExtensions.RebindingOperation.Flags.Started;
				this.m_Candidates.Clear();
				this.m_Candidates.Capacity = 0;
				this.m_StartTime = -1.0;
				this.m_StartingActuations.Clear();
				this.UnhookOnEvent();
				this.UnhookOnAfterUpdate();
				InputSystem.s_Manager.inputEventHandledPolicy = this.m_SavedInputEventHandledPolicy;
			}

			// Token: 0x06001308 RID: 4872 RVA: 0x0005A6D9 File Offset: 0x000588D9
			private void ThrowIfRebindInProgress()
			{
				if (this.started)
				{
					throw new InvalidOperationException("Cannot reconfigure rebinding while operation is in progress");
				}
			}

			// Token: 0x06001309 RID: 4873 RVA: 0x0005A6F0 File Offset: 0x000588F0
			private string GeneratePathForControl(InputControl control)
			{
				InputDevice device = control.device;
				InternedString internedString = InputControlLayout.s_Layouts.FindLayoutThatIntroducesControl(control, this.m_LayoutCache);
				if (this.m_PathBuilder == null)
				{
					this.m_PathBuilder = new StringBuilder();
				}
				else
				{
					this.m_PathBuilder.Length = 0;
				}
				control.BuildPath(internedString, this.m_PathBuilder);
				return this.m_PathBuilder.ToString();
			}

			// Token: 0x040007F8 RID: 2040
			public const float kDefaultMagnitudeThreshold = 0.2f;

			// Token: 0x040007F9 RID: 2041
			private InputAction m_ActionToRebind;

			// Token: 0x040007FA RID: 2042
			private InputBinding? m_BindingMask;

			// Token: 0x040007FB RID: 2043
			private Type m_ControlType;

			// Token: 0x040007FC RID: 2044
			private InternedString m_ExpectedLayout;

			// Token: 0x040007FD RID: 2045
			private int m_IncludePathCount;

			// Token: 0x040007FE RID: 2046
			private string[] m_IncludePaths;

			// Token: 0x040007FF RID: 2047
			private int m_ExcludePathCount;

			// Token: 0x04000800 RID: 2048
			private string[] m_ExcludePaths;

			// Token: 0x04000801 RID: 2049
			private int m_TargetBindingIndex = -1;

			// Token: 0x04000802 RID: 2050
			private string m_BindingGroupForNewBinding;

			// Token: 0x04000803 RID: 2051
			private string m_CancelBinding;

			// Token: 0x04000804 RID: 2052
			private float m_MagnitudeThreshold = 0.2f;

			// Token: 0x04000805 RID: 2053
			private float[] m_Scores;

			// Token: 0x04000806 RID: 2054
			private float[] m_Magnitudes;

			// Token: 0x04000807 RID: 2055
			private double m_LastMatchTime;

			// Token: 0x04000808 RID: 2056
			private double m_StartTime;

			// Token: 0x04000809 RID: 2057
			private float m_Timeout;

			// Token: 0x0400080A RID: 2058
			private float m_WaitSecondsAfterMatch;

			// Token: 0x0400080B RID: 2059
			private InputEventHandledPolicy m_SavedInputEventHandledPolicy;

			// Token: 0x0400080C RID: 2060
			private InputEventHandledPolicy m_TargetInputEventHandledPolicy;

			// Token: 0x0400080D RID: 2061
			private InputControlList<InputControl> m_Candidates;

			// Token: 0x0400080E RID: 2062
			private Action<InputActionRebindingExtensions.RebindingOperation> m_OnComplete;

			// Token: 0x0400080F RID: 2063
			private Action<InputActionRebindingExtensions.RebindingOperation> m_OnCancel;

			// Token: 0x04000810 RID: 2064
			private Action<InputActionRebindingExtensions.RebindingOperation> m_OnPotentialMatch;

			// Token: 0x04000811 RID: 2065
			private Func<InputControl, string> m_OnGeneratePath;

			// Token: 0x04000812 RID: 2066
			private Func<InputControl, InputEventPtr, float> m_OnComputeScore;

			// Token: 0x04000813 RID: 2067
			private Action<InputActionRebindingExtensions.RebindingOperation, string> m_OnApplyBinding;

			// Token: 0x04000814 RID: 2068
			private Action<InputEventPtr, InputDevice> m_OnEventDelegate;

			// Token: 0x04000815 RID: 2069
			private Action m_OnAfterUpdateDelegate;

			// Token: 0x04000816 RID: 2070
			private InputControlLayout.Cache m_LayoutCache;

			// Token: 0x04000817 RID: 2071
			private StringBuilder m_PathBuilder;

			// Token: 0x04000818 RID: 2072
			private InputActionRebindingExtensions.RebindingOperation.Flags m_Flags;

			// Token: 0x04000819 RID: 2073
			private Dictionary<InputControl, float> m_StartingActuations = new Dictionary<InputControl, float>();

			// Token: 0x0200026B RID: 619
			[Flags]
			private enum Flags
			{
				// Token: 0x04000CDC RID: 3292
				Started = 1,
				// Token: 0x04000CDD RID: 3293
				Completed = 2,
				// Token: 0x04000CDE RID: 3294
				Canceled = 4,
				// Token: 0x04000CDF RID: 3295
				OnEventHooked = 8,
				// Token: 0x04000CE0 RID: 3296
				OnAfterUpdateHooked = 16,
				// Token: 0x04000CE1 RID: 3297
				DontIgnoreNoisyControls = 64,
				// Token: 0x04000CE2 RID: 3298
				DontGeneralizePathOfSelectedControl = 128,
				// Token: 0x04000CE3 RID: 3299
				AddNewBinding = 256,
				// Token: 0x04000CE4 RID: 3300
				SuppressMatchingEvents = 512
			}
		}

		// Token: 0x02000171 RID: 369
		internal class DeferBindingResolutionWrapper : IDisposable
		{
			// Token: 0x0600130B RID: 4875 RVA: 0x0005A77A File Offset: 0x0005897A
			public void Acquire()
			{
				InputActionMap.s_DeferBindingResolution++;
			}

			// Token: 0x0600130C RID: 4876 RVA: 0x0005A788 File Offset: 0x00058988
			public void Dispose()
			{
				if (InputActionMap.s_DeferBindingResolution > 0)
				{
					InputActionMap.s_DeferBindingResolution--;
				}
				if (InputActionMap.s_DeferBindingResolution == 0)
				{
					InputActionState.DeferredResolutionOfBindings();
				}
			}
		}
	}
}
