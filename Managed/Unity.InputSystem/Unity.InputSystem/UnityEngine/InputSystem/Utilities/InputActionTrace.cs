using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.InputSystem.LowLevel;

namespace UnityEngine.InputSystem.Utilities
{
	// Token: 0x0200012B RID: 299
	public sealed class InputActionTrace : IEnumerable<InputActionTrace.ActionEventPtr>, IEnumerable, IDisposable
	{
		// Token: 0x170004DA RID: 1242
		// (get) Token: 0x060010C6 RID: 4294 RVA: 0x00050896 File Offset: 0x0004EA96
		public InputEventBuffer buffer
		{
			get
			{
				return this.m_EventBuffer;
			}
		}

		// Token: 0x170004DB RID: 1243
		// (get) Token: 0x060010C7 RID: 4295 RVA: 0x0005089E File Offset: 0x0004EA9E
		public int count
		{
			get
			{
				return this.m_EventBuffer.eventCount;
			}
		}

		// Token: 0x060010C8 RID: 4296 RVA: 0x000508AB File Offset: 0x0004EAAB
		public InputActionTrace()
		{
		}

		// Token: 0x060010C9 RID: 4297 RVA: 0x000508B3 File Offset: 0x0004EAB3
		public InputActionTrace(InputAction action)
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			this.SubscribeTo(action);
		}

		// Token: 0x060010CA RID: 4298 RVA: 0x000508D0 File Offset: 0x0004EAD0
		public InputActionTrace(InputActionMap actionMap)
		{
			if (actionMap == null)
			{
				throw new ArgumentNullException("actionMap");
			}
			this.SubscribeTo(actionMap);
		}

		// Token: 0x060010CB RID: 4299 RVA: 0x000508F0 File Offset: 0x0004EAF0
		public void SubscribeToAll()
		{
			if (this.m_SubscribedToAll)
			{
				return;
			}
			this.HookOnActionChange();
			this.m_SubscribedToAll = true;
			while (this.m_SubscribedActions.length > 0)
			{
				this.UnsubscribeFrom(this.m_SubscribedActions[this.m_SubscribedActions.length - 1]);
			}
			while (this.m_SubscribedActionMaps.length > 0)
			{
				this.UnsubscribeFrom(this.m_SubscribedActionMaps[this.m_SubscribedActionMaps.length - 1]);
			}
		}

		// Token: 0x060010CC RID: 4300 RVA: 0x00050970 File Offset: 0x0004EB70
		public void UnsubscribeFromAll()
		{
			if (this.count == 0)
			{
				this.UnhookOnActionChange();
			}
			this.m_SubscribedToAll = false;
			while (this.m_SubscribedActions.length > 0)
			{
				this.UnsubscribeFrom(this.m_SubscribedActions[this.m_SubscribedActions.length - 1]);
			}
			while (this.m_SubscribedActionMaps.length > 0)
			{
				this.UnsubscribeFrom(this.m_SubscribedActionMaps[this.m_SubscribedActionMaps.length - 1]);
			}
		}

		// Token: 0x060010CD RID: 4301 RVA: 0x000509F0 File Offset: 0x0004EBF0
		public void SubscribeTo(InputAction action)
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			if (this.m_CallbackDelegate == null)
			{
				this.m_CallbackDelegate = new Action<InputAction.CallbackContext>(this.RecordAction);
			}
			action.performed += this.m_CallbackDelegate;
			action.started += this.m_CallbackDelegate;
			action.canceled += this.m_CallbackDelegate;
			this.m_SubscribedActions.AppendWithCapacity(action, 10);
		}

		// Token: 0x060010CE RID: 4302 RVA: 0x00050A58 File Offset: 0x0004EC58
		public void SubscribeTo(InputActionMap actionMap)
		{
			if (actionMap == null)
			{
				throw new ArgumentNullException("actionMap");
			}
			if (this.m_CallbackDelegate == null)
			{
				this.m_CallbackDelegate = new Action<InputAction.CallbackContext>(this.RecordAction);
			}
			actionMap.actionTriggered += this.m_CallbackDelegate;
			this.m_SubscribedActionMaps.AppendWithCapacity(actionMap, 10);
		}

		// Token: 0x060010CF RID: 4303 RVA: 0x00050AA8 File Offset: 0x0004ECA8
		public void UnsubscribeFrom(InputAction action)
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			if (this.m_CallbackDelegate == null)
			{
				return;
			}
			action.performed -= this.m_CallbackDelegate;
			action.started -= this.m_CallbackDelegate;
			action.canceled -= this.m_CallbackDelegate;
			int num = this.m_SubscribedActions.IndexOfReference(action);
			if (num != -1)
			{
				this.m_SubscribedActions.RemoveAtWithCapacity(num);
			}
		}

		// Token: 0x060010D0 RID: 4304 RVA: 0x00050B10 File Offset: 0x0004ED10
		public void UnsubscribeFrom(InputActionMap actionMap)
		{
			if (actionMap == null)
			{
				throw new ArgumentNullException("actionMap");
			}
			if (this.m_CallbackDelegate == null)
			{
				return;
			}
			actionMap.actionTriggered -= this.m_CallbackDelegate;
			int num = this.m_SubscribedActionMaps.IndexOfReference(actionMap);
			if (num != -1)
			{
				this.m_SubscribedActionMaps.RemoveAtWithCapacity(num);
			}
		}

		// Token: 0x060010D1 RID: 4305 RVA: 0x00050B60 File Offset: 0x0004ED60
		public unsafe void RecordAction(InputAction.CallbackContext context)
		{
			int num = this.m_ActionMapStates.IndexOfReference(context.m_State);
			if (num == -1)
			{
				num = this.m_ActionMapStates.AppendWithCapacity(context.m_State, 10);
			}
			this.HookOnActionChange();
			int valueSizeInBytes = context.valueSizeInBytes;
			ActionEvent* ptr = (ActionEvent*)this.m_EventBuffer.AllocateEvent(ActionEvent.GetEventSizeWithValueSize(valueSizeInBytes), 2048, Allocator.Persistent);
			ref InputActionState.TriggerState ptr2 = ref context.m_State.actionStates[context.m_ActionIndex];
			ptr->baseEvent.type = ActionEvent.Type;
			ptr->baseEvent.time = ptr2.time;
			ptr->stateIndex = num;
			ptr->controlIndex = ptr2.controlIndex;
			ptr->bindingIndex = ptr2.bindingIndex;
			ptr->interactionIndex = ptr2.interactionIndex;
			ptr->startTime = ptr2.startTime;
			ptr->phase = ptr2.phase;
			byte* valueData = ptr->valueData;
			context.ReadValue((void*)valueData, valueSizeInBytes);
		}

		// Token: 0x060010D2 RID: 4306 RVA: 0x00050C4E File Offset: 0x0004EE4E
		public void Clear()
		{
			this.m_EventBuffer.Reset();
			this.m_ActionMapStates.ClearWithCapacity();
		}

		// Token: 0x060010D3 RID: 4307 RVA: 0x00050C68 File Offset: 0x0004EE68
		~InputActionTrace()
		{
			this.DisposeInternal();
		}

		// Token: 0x060010D4 RID: 4308 RVA: 0x00050C94 File Offset: 0x0004EE94
		public override string ToString()
		{
			if (this.count == 0)
			{
				return "[]";
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append('[');
			bool flag = true;
			foreach (InputActionTrace.ActionEventPtr actionEventPtr in this)
			{
				if (!flag)
				{
					stringBuilder.Append(",\n");
				}
				stringBuilder.Append(actionEventPtr.ToString());
				flag = false;
			}
			stringBuilder.Append(']');
			return stringBuilder.ToString();
		}

		// Token: 0x060010D5 RID: 4309 RVA: 0x00050D28 File Offset: 0x0004EF28
		public void Dispose()
		{
			this.UnsubscribeFromAll();
			this.DisposeInternal();
		}

		// Token: 0x060010D6 RID: 4310 RVA: 0x00050D38 File Offset: 0x0004EF38
		private void DisposeInternal()
		{
			for (int i = 0; i < this.m_ActionMapStateClones.length; i++)
			{
				this.m_ActionMapStateClones[i].Dispose();
			}
			this.m_EventBuffer.Dispose();
			this.m_ActionMapStates.Clear();
			this.m_ActionMapStateClones.Clear();
			if (this.m_ActionChangeDelegate != null)
			{
				InputSystem.onActionChange -= this.m_ActionChangeDelegate;
				this.m_ActionChangeDelegate = null;
			}
		}

		// Token: 0x060010D7 RID: 4311 RVA: 0x00050DA7 File Offset: 0x0004EFA7
		public IEnumerator<InputActionTrace.ActionEventPtr> GetEnumerator()
		{
			return new InputActionTrace.Enumerator(this);
		}

		// Token: 0x060010D8 RID: 4312 RVA: 0x00050DB4 File Offset: 0x0004EFB4
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x060010D9 RID: 4313 RVA: 0x00050DBC File Offset: 0x0004EFBC
		private void HookOnActionChange()
		{
			if (this.m_OnActionChangeHooked)
			{
				return;
			}
			if (this.m_ActionChangeDelegate == null)
			{
				this.m_ActionChangeDelegate = new Action<object, InputActionChange>(this.OnActionChange);
			}
			InputSystem.onActionChange += this.m_ActionChangeDelegate;
			this.m_OnActionChangeHooked = true;
		}

		// Token: 0x060010DA RID: 4314 RVA: 0x00050DF3 File Offset: 0x0004EFF3
		private void UnhookOnActionChange()
		{
			if (!this.m_OnActionChangeHooked)
			{
				return;
			}
			InputSystem.onActionChange -= this.m_ActionChangeDelegate;
			this.m_OnActionChangeHooked = false;
		}

		// Token: 0x060010DB RID: 4315 RVA: 0x00050E10 File Offset: 0x0004F010
		private void OnActionChange(object actionOrMapOrAsset, InputActionChange change)
		{
			if (this.m_SubscribedToAll && change - InputActionChange.ActionStarted <= 2)
			{
				InputAction inputAction = (InputAction)actionOrMapOrAsset;
				int actionIndexInState = inputAction.m_ActionIndexInState;
				InputActionState state = inputAction.m_ActionMap.m_State;
				InputAction.CallbackContext callbackContext = new InputAction.CallbackContext
				{
					m_State = state,
					m_ActionIndex = actionIndexInState
				};
				this.RecordAction(callbackContext);
				return;
			}
			if (change != InputActionChange.BoundControlsAboutToChange)
			{
				return;
			}
			InputAction inputAction2 = actionOrMapOrAsset as InputAction;
			if (inputAction2 != null)
			{
				this.CloneActionStateBeforeBindingsChange(inputAction2.m_ActionMap);
				return;
			}
			InputActionMap inputActionMap = actionOrMapOrAsset as InputActionMap;
			if (inputActionMap != null)
			{
				this.CloneActionStateBeforeBindingsChange(inputActionMap);
				return;
			}
			InputActionAsset inputActionAsset = actionOrMapOrAsset as InputActionAsset;
			if (inputActionAsset != null)
			{
				foreach (InputActionMap inputActionMap2 in inputActionAsset.actionMaps)
				{
					this.CloneActionStateBeforeBindingsChange(inputActionMap2);
				}
			}
		}

		// Token: 0x060010DC RID: 4316 RVA: 0x00050EF4 File Offset: 0x0004F0F4
		private void CloneActionStateBeforeBindingsChange(InputActionMap actionMap)
		{
			InputActionState state = actionMap.m_State;
			if (state == null)
			{
				return;
			}
			int num = this.m_ActionMapStates.IndexOfReference(state);
			if (num == -1)
			{
				return;
			}
			InputActionState inputActionState = state.Clone();
			this.m_ActionMapStateClones.Append(inputActionState);
			this.m_ActionMapStates[num] = inputActionState;
		}

		// Token: 0x04000708 RID: 1800
		private bool m_SubscribedToAll;

		// Token: 0x04000709 RID: 1801
		private bool m_OnActionChangeHooked;

		// Token: 0x0400070A RID: 1802
		private InlinedArray<InputAction> m_SubscribedActions;

		// Token: 0x0400070B RID: 1803
		private InlinedArray<InputActionMap> m_SubscribedActionMaps;

		// Token: 0x0400070C RID: 1804
		private InputEventBuffer m_EventBuffer;

		// Token: 0x0400070D RID: 1805
		private InlinedArray<InputActionState> m_ActionMapStates;

		// Token: 0x0400070E RID: 1806
		private InlinedArray<InputActionState> m_ActionMapStateClones;

		// Token: 0x0400070F RID: 1807
		private Action<InputAction.CallbackContext> m_CallbackDelegate;

		// Token: 0x04000710 RID: 1808
		private Action<object, InputActionChange> m_ActionChangeDelegate;

		// Token: 0x02000240 RID: 576
		public struct ActionEventPtr
		{
			// Token: 0x170005FD RID: 1533
			// (get) Token: 0x06001619 RID: 5657 RVA: 0x00066000 File Offset: 0x00064200
			public unsafe InputAction action
			{
				get
				{
					return this.m_State.GetActionOrNull(this.m_Ptr->bindingIndex);
				}
			}

			// Token: 0x170005FE RID: 1534
			// (get) Token: 0x0600161A RID: 5658 RVA: 0x00066018 File Offset: 0x00064218
			public unsafe InputActionPhase phase
			{
				get
				{
					return this.m_Ptr->phase;
				}
			}

			// Token: 0x170005FF RID: 1535
			// (get) Token: 0x0600161B RID: 5659 RVA: 0x00066025 File Offset: 0x00064225
			public unsafe InputControl control
			{
				get
				{
					return this.m_State.controls[this.m_Ptr->controlIndex];
				}
			}

			// Token: 0x17000600 RID: 1536
			// (get) Token: 0x0600161C RID: 5660 RVA: 0x00066040 File Offset: 0x00064240
			public unsafe IInputInteraction interaction
			{
				get
				{
					int interactionIndex = this.m_Ptr->interactionIndex;
					if (interactionIndex == -1)
					{
						return null;
					}
					return this.m_State.interactions[interactionIndex];
				}
			}

			// Token: 0x17000601 RID: 1537
			// (get) Token: 0x0600161D RID: 5661 RVA: 0x0006606C File Offset: 0x0006426C
			public unsafe double time
			{
				get
				{
					return this.m_Ptr->baseEvent.time;
				}
			}

			// Token: 0x17000602 RID: 1538
			// (get) Token: 0x0600161E RID: 5662 RVA: 0x0006607E File Offset: 0x0006427E
			public unsafe double startTime
			{
				get
				{
					return this.m_Ptr->startTime;
				}
			}

			// Token: 0x17000603 RID: 1539
			// (get) Token: 0x0600161F RID: 5663 RVA: 0x0006608B File Offset: 0x0006428B
			public double duration
			{
				get
				{
					return this.time - this.startTime;
				}
			}

			// Token: 0x17000604 RID: 1540
			// (get) Token: 0x06001620 RID: 5664 RVA: 0x0006609A File Offset: 0x0006429A
			public unsafe int valueSizeInBytes
			{
				get
				{
					return this.m_Ptr->valueSizeInBytes;
				}
			}

			// Token: 0x06001621 RID: 5665 RVA: 0x000660A8 File Offset: 0x000642A8
			public unsafe object ReadValueAsObject()
			{
				if (this.m_Ptr == null)
				{
					throw new InvalidOperationException("ActionEventPtr is invalid");
				}
				byte* valueData = this.m_Ptr->valueData;
				int bindingIndex = this.m_Ptr->bindingIndex;
				if (!this.m_State.bindingStates[bindingIndex].isPartOfComposite)
				{
					int valueSizeInBytes = this.m_Ptr->valueSizeInBytes;
					return this.control.ReadValueFromBufferAsObject((void*)valueData, valueSizeInBytes);
				}
				int compositeOrCompositeBindingIndex = this.m_State.bindingStates[bindingIndex].compositeOrCompositeBindingIndex;
				int compositeOrCompositeBindingIndex2 = this.m_State.bindingStates[compositeOrCompositeBindingIndex].compositeOrCompositeBindingIndex;
				InputBindingComposite inputBindingComposite = this.m_State.composites[compositeOrCompositeBindingIndex2];
				Type valueType = inputBindingComposite.valueType;
				if (valueType == null)
				{
					throw new InvalidOperationException(string.Format("Cannot read value from Composite '{0}' which does not have a valueType set", inputBindingComposite));
				}
				return Marshal.PtrToStructure(new IntPtr((void*)valueData), valueType);
			}

			// Token: 0x06001622 RID: 5666 RVA: 0x00066194 File Offset: 0x00064394
			public unsafe void ReadValue(void* buffer, int bufferSize)
			{
				int valueSizeInBytes = this.m_Ptr->valueSizeInBytes;
				if (bufferSize < valueSizeInBytes)
				{
					throw new ArgumentException(string.Format("Expected buffer of at least {0} bytes but got buffer of just {1} bytes instead", valueSizeInBytes, bufferSize), "bufferSize");
				}
				UnsafeUtility.MemCpy(buffer, (void*)this.m_Ptr->valueData, (long)valueSizeInBytes);
			}

			// Token: 0x06001623 RID: 5667 RVA: 0x000661E8 File Offset: 0x000643E8
			public unsafe TValue ReadValue<TValue>() where TValue : struct
			{
				int valueSizeInBytes = this.m_Ptr->valueSizeInBytes;
				if (UnsafeUtility.SizeOf<TValue>() != valueSizeInBytes)
				{
					throw new InvalidOperationException(string.Format("Cannot read a value of type '{0}' with size {1} from event on action '{2}' with value size {3}", new object[]
					{
						typeof(TValue).Name,
						UnsafeUtility.SizeOf<TValue>(),
						this.action,
						valueSizeInBytes
					}));
				}
				TValue tvalue = new TValue();
				UnsafeUtility.MemCpy(UnsafeUtility.AddressOf<TValue>(ref tvalue), (void*)this.m_Ptr->valueData, (long)valueSizeInBytes);
				return tvalue;
			}

			// Token: 0x06001624 RID: 5668 RVA: 0x00066270 File Offset: 0x00064470
			public override string ToString()
			{
				if (this.m_Ptr == null)
				{
					return "<null>";
				}
				string text = ((this.action.actionMap != null) ? (this.action.actionMap.name + "/" + this.action.name) : this.action.name);
				return string.Format("{{ action={0} phase={1} time={2} control={3} value={4} interaction={5} duration={6} }}", new object[]
				{
					text,
					this.phase,
					this.time,
					this.control,
					this.ReadValueAsObject(),
					this.interaction,
					this.duration
				});
			}

			// Token: 0x04000C6D RID: 3181
			internal InputActionState m_State;

			// Token: 0x04000C6E RID: 3182
			internal unsafe ActionEvent* m_Ptr;
		}

		// Token: 0x02000241 RID: 577
		private struct Enumerator : IEnumerator<InputActionTrace.ActionEventPtr>, IEnumerator, IDisposable
		{
			// Token: 0x06001625 RID: 5669 RVA: 0x00066328 File Offset: 0x00064528
			public unsafe Enumerator(InputActionTrace trace)
			{
				this.m_Trace = trace;
				this.m_Buffer = (ActionEvent*)trace.m_EventBuffer.bufferPtr.data;
				this.m_EventCount = trace.m_EventBuffer.eventCount;
				this.m_CurrentEvent = null;
				this.m_CurrentIndex = 0;
			}

			// Token: 0x06001626 RID: 5670 RVA: 0x00066378 File Offset: 0x00064578
			public unsafe bool MoveNext()
			{
				if (this.m_CurrentIndex == this.m_EventCount)
				{
					return false;
				}
				if (this.m_CurrentEvent == null)
				{
					this.m_CurrentEvent = this.m_Buffer;
					return this.m_CurrentEvent != null;
				}
				this.m_CurrentIndex++;
				if (this.m_CurrentIndex == this.m_EventCount)
				{
					return false;
				}
				this.m_CurrentEvent = (ActionEvent*)InputEvent.GetNextInMemory((InputEvent*)this.m_CurrentEvent);
				return true;
			}

			// Token: 0x06001627 RID: 5671 RVA: 0x000663E9 File Offset: 0x000645E9
			public void Reset()
			{
				this.m_CurrentEvent = null;
				this.m_CurrentIndex = 0;
			}

			// Token: 0x06001628 RID: 5672 RVA: 0x000663FA File Offset: 0x000645FA
			public void Dispose()
			{
			}

			// Token: 0x17000605 RID: 1541
			// (get) Token: 0x06001629 RID: 5673 RVA: 0x000663FC File Offset: 0x000645FC
			public unsafe InputActionTrace.ActionEventPtr Current
			{
				get
				{
					InputActionState inputActionState = this.m_Trace.m_ActionMapStates[this.m_CurrentEvent->stateIndex];
					return new InputActionTrace.ActionEventPtr
					{
						m_State = inputActionState,
						m_Ptr = this.m_CurrentEvent
					};
				}
			}

			// Token: 0x17000606 RID: 1542
			// (get) Token: 0x0600162A RID: 5674 RVA: 0x00066443 File Offset: 0x00064643
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x04000C6F RID: 3183
			private readonly InputActionTrace m_Trace;

			// Token: 0x04000C70 RID: 3184
			private unsafe readonly ActionEvent* m_Buffer;

			// Token: 0x04000C71 RID: 3185
			private readonly int m_EventCount;

			// Token: 0x04000C72 RID: 3186
			private unsafe ActionEvent* m_CurrentEvent;

			// Token: 0x04000C73 RID: 3187
			private int m_CurrentIndex;
		}
	}
}
