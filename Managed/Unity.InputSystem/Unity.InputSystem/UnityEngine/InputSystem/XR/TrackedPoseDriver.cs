using System;
using UnityEngine.InputSystem.LowLevel;

namespace UnityEngine.InputSystem.XR
{
	// Token: 0x02000068 RID: 104
	[AddComponentMenu("XR/Tracked Pose Driver (Input System)")]
	[HelpURL("https://docs.unity3d.com/Packages/com.unity.inputsystem@1.19/manual/TrackedInputDevices.html#tracked-pose-driver")]
	[Serializable]
	public class TrackedPoseDriver : MonoBehaviour, ISerializationCallbackReceiver
	{
		// Token: 0x1700028E RID: 654
		// (get) Token: 0x06000A03 RID: 2563 RVA: 0x00037907 File Offset: 0x00035B07
		// (set) Token: 0x06000A04 RID: 2564 RVA: 0x0003790F File Offset: 0x00035B0F
		public TrackedPoseDriver.TrackingType trackingType
		{
			get
			{
				return this.m_TrackingType;
			}
			set
			{
				this.m_TrackingType = value;
			}
		}

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x06000A05 RID: 2565 RVA: 0x00037918 File Offset: 0x00035B18
		// (set) Token: 0x06000A06 RID: 2566 RVA: 0x00037920 File Offset: 0x00035B20
		public TrackedPoseDriver.UpdateType updateType
		{
			get
			{
				return this.m_UpdateType;
			}
			set
			{
				this.m_UpdateType = value;
			}
		}

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x06000A07 RID: 2567 RVA: 0x00037929 File Offset: 0x00035B29
		// (set) Token: 0x06000A08 RID: 2568 RVA: 0x00037931 File Offset: 0x00035B31
		public bool ignoreTrackingState
		{
			get
			{
				return this.m_IgnoreTrackingState;
			}
			set
			{
				this.m_IgnoreTrackingState = value;
			}
		}

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x06000A09 RID: 2569 RVA: 0x0003793A File Offset: 0x00035B3A
		// (set) Token: 0x06000A0A RID: 2570 RVA: 0x00037942 File Offset: 0x00035B42
		public InputActionProperty positionInput
		{
			get
			{
				return this.m_PositionInput;
			}
			set
			{
				if (Application.isPlaying)
				{
					this.UnbindPosition();
				}
				this.m_PositionInput = value;
				if (Application.isPlaying && base.isActiveAndEnabled)
				{
					this.BindPosition();
				}
			}
		}

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x06000A0B RID: 2571 RVA: 0x0003796D File Offset: 0x00035B6D
		// (set) Token: 0x06000A0C RID: 2572 RVA: 0x00037975 File Offset: 0x00035B75
		public InputActionProperty rotationInput
		{
			get
			{
				return this.m_RotationInput;
			}
			set
			{
				if (Application.isPlaying)
				{
					this.UnbindRotation();
				}
				this.m_RotationInput = value;
				if (Application.isPlaying && base.isActiveAndEnabled)
				{
					this.BindRotation();
				}
			}
		}

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x06000A0D RID: 2573 RVA: 0x000379A0 File Offset: 0x00035BA0
		// (set) Token: 0x06000A0E RID: 2574 RVA: 0x000379A8 File Offset: 0x00035BA8
		public InputActionProperty trackingStateInput
		{
			get
			{
				return this.m_TrackingStateInput;
			}
			set
			{
				if (Application.isPlaying)
				{
					this.UnbindTrackingState();
				}
				this.m_TrackingStateInput = value;
				if (Application.isPlaying && base.isActiveAndEnabled)
				{
					this.BindTrackingState();
				}
			}
		}

		// Token: 0x06000A0F RID: 2575 RVA: 0x000379D3 File Offset: 0x00035BD3
		private void BindActions()
		{
			this.BindPosition();
			this.BindRotation();
			this.BindTrackingState();
		}

		// Token: 0x06000A10 RID: 2576 RVA: 0x000379E7 File Offset: 0x00035BE7
		private void UnbindActions()
		{
			this.UnbindPosition();
			this.UnbindRotation();
			this.UnbindTrackingState();
		}

		// Token: 0x06000A11 RID: 2577 RVA: 0x000379FC File Offset: 0x00035BFC
		private void BindPosition()
		{
			if (this.m_PositionBound)
			{
				return;
			}
			InputAction action = this.m_PositionInput.action;
			if (action == null)
			{
				return;
			}
			action.performed += this.OnPositionPerformed;
			action.canceled += this.OnPositionCanceled;
			this.m_PositionBound = true;
			if (this.m_PositionInput.reference == null)
			{
				TrackedPoseDriver.RenameAndEnable(action, base.gameObject.name + " - TPD - Position");
			}
		}

		// Token: 0x06000A12 RID: 2578 RVA: 0x00037A7C File Offset: 0x00035C7C
		private void BindRotation()
		{
			if (this.m_RotationBound)
			{
				return;
			}
			InputAction action = this.m_RotationInput.action;
			if (action == null)
			{
				return;
			}
			action.performed += this.OnRotationPerformed;
			action.canceled += this.OnRotationCanceled;
			this.m_RotationBound = true;
			if (this.m_RotationInput.reference == null)
			{
				TrackedPoseDriver.RenameAndEnable(action, base.gameObject.name + " - TPD - Rotation");
			}
		}

		// Token: 0x06000A13 RID: 2579 RVA: 0x00037AFC File Offset: 0x00035CFC
		private void BindTrackingState()
		{
			if (this.m_TrackingStateBound)
			{
				return;
			}
			InputAction action = this.m_TrackingStateInput.action;
			if (action == null)
			{
				return;
			}
			action.performed += this.OnTrackingStatePerformed;
			action.canceled += this.OnTrackingStateCanceled;
			this.m_TrackingStateBound = true;
			if (this.m_TrackingStateInput.reference == null)
			{
				TrackedPoseDriver.RenameAndEnable(action, base.gameObject.name + " - TPD - Tracking State");
			}
		}

		// Token: 0x06000A14 RID: 2580 RVA: 0x00037B7B File Offset: 0x00035D7B
		private static void RenameAndEnable(InputAction action, string name)
		{
			action.Rename(name);
			action.Enable();
		}

		// Token: 0x06000A15 RID: 2581 RVA: 0x00037B8C File Offset: 0x00035D8C
		private void UnbindPosition()
		{
			if (!this.m_PositionBound)
			{
				return;
			}
			InputAction action = this.m_PositionInput.action;
			if (action == null)
			{
				return;
			}
			if (this.m_PositionInput.reference == null)
			{
				action.Disable();
			}
			action.performed -= this.OnPositionPerformed;
			action.canceled -= this.OnPositionCanceled;
			this.m_PositionBound = false;
		}

		// Token: 0x06000A16 RID: 2582 RVA: 0x00037BF8 File Offset: 0x00035DF8
		private void UnbindRotation()
		{
			if (!this.m_RotationBound)
			{
				return;
			}
			InputAction action = this.m_RotationInput.action;
			if (action == null)
			{
				return;
			}
			if (this.m_RotationInput.reference == null)
			{
				action.Disable();
			}
			action.performed -= this.OnRotationPerformed;
			action.canceled -= this.OnRotationCanceled;
			this.m_RotationBound = false;
		}

		// Token: 0x06000A17 RID: 2583 RVA: 0x00037C64 File Offset: 0x00035E64
		private void UnbindTrackingState()
		{
			if (!this.m_TrackingStateBound)
			{
				return;
			}
			InputAction action = this.m_TrackingStateInput.action;
			if (action == null)
			{
				return;
			}
			if (this.m_TrackingStateInput.reference == null)
			{
				action.Disable();
			}
			action.performed -= this.OnTrackingStatePerformed;
			action.canceled -= this.OnTrackingStateCanceled;
			this.m_TrackingStateBound = false;
		}

		// Token: 0x06000A18 RID: 2584 RVA: 0x00037CCE File Offset: 0x00035ECE
		private void OnPositionPerformed(InputAction.CallbackContext context)
		{
			this.m_CurrentPosition = context.ReadValue<Vector3>();
		}

		// Token: 0x06000A19 RID: 2585 RVA: 0x00037CDD File Offset: 0x00035EDD
		private void OnPositionCanceled(InputAction.CallbackContext context)
		{
			this.m_CurrentPosition = Vector3.zero;
		}

		// Token: 0x06000A1A RID: 2586 RVA: 0x00037CEA File Offset: 0x00035EEA
		private void OnRotationPerformed(InputAction.CallbackContext context)
		{
			this.m_CurrentRotation = context.ReadValue<Quaternion>();
		}

		// Token: 0x06000A1B RID: 2587 RVA: 0x00037CF9 File Offset: 0x00035EF9
		private void OnRotationCanceled(InputAction.CallbackContext context)
		{
			this.m_CurrentRotation = Quaternion.identity;
		}

		// Token: 0x06000A1C RID: 2588 RVA: 0x00037D06 File Offset: 0x00035F06
		private void OnTrackingStatePerformed(InputAction.CallbackContext context)
		{
			this.m_CurrentTrackingState = (TrackedPoseDriver.TrackingStates)context.ReadValue<int>();
		}

		// Token: 0x06000A1D RID: 2589 RVA: 0x00037D15 File Offset: 0x00035F15
		private void OnTrackingStateCanceled(InputAction.CallbackContext context)
		{
			this.m_CurrentTrackingState = TrackedPoseDriver.TrackingStates.None;
		}

		// Token: 0x06000A1E RID: 2590 RVA: 0x00037D20 File Offset: 0x00035F20
		protected void Reset()
		{
			this.m_PositionInput = new InputActionProperty(new InputAction("Position", InputActionType.Value, null, null, null, "Vector3"));
			this.m_RotationInput = new InputActionProperty(new InputAction("Rotation", InputActionType.Value, null, null, null, "Quaternion"));
			this.m_TrackingStateInput = new InputActionProperty(new InputAction("Tracking State", InputActionType.Value, null, null, null, "Integer"));
		}

		// Token: 0x06000A1F RID: 2591 RVA: 0x00037D87 File Offset: 0x00035F87
		protected virtual void Awake()
		{
		}

		// Token: 0x06000A20 RID: 2592 RVA: 0x00037D89 File Offset: 0x00035F89
		protected void OnEnable()
		{
			InputSystem.onAfterUpdate += this.UpdateCallback;
			InputSystem.onDeviceChange += this.OnDeviceChanged;
			this.BindActions();
			this.m_IsFirstUpdate = true;
		}

		// Token: 0x06000A21 RID: 2593 RVA: 0x00037DBA File Offset: 0x00035FBA
		protected void OnDisable()
		{
			this.UnbindActions();
			InputSystem.onAfterUpdate -= this.UpdateCallback;
			InputSystem.onDeviceChange -= this.OnDeviceChanged;
		}

		// Token: 0x06000A22 RID: 2594 RVA: 0x00037DE4 File Offset: 0x00035FE4
		protected virtual void OnDestroy()
		{
		}

		// Token: 0x06000A23 RID: 2595 RVA: 0x00037DE8 File Offset: 0x00035FE8
		protected void UpdateCallback()
		{
			if (this.m_IsFirstUpdate)
			{
				if (TrackedPoseDriver.HasResolvedControl(this.m_PositionInput.action))
				{
					this.m_CurrentPosition = this.m_PositionInput.action.ReadValue<Vector3>();
				}
				else
				{
					this.m_CurrentPosition = base.transform.localPosition;
				}
				if (TrackedPoseDriver.HasResolvedControl(this.m_RotationInput.action))
				{
					this.m_CurrentRotation = this.m_RotationInput.action.ReadValue<Quaternion>();
				}
				else
				{
					this.m_CurrentRotation = base.transform.localRotation;
				}
				this.ReadTrackingState();
				this.m_IsFirstUpdate = false;
			}
			if (InputState.currentUpdateType == InputUpdateType.BeforeRender)
			{
				this.OnBeforeRender();
				return;
			}
			this.OnUpdate();
		}

		// Token: 0x06000A24 RID: 2596 RVA: 0x00037E98 File Offset: 0x00036098
		private void OnDeviceChanged(InputDevice inputDevice, InputDeviceChange inputDeviceChange)
		{
			if (this.m_IsFirstUpdate)
			{
				return;
			}
			this.ReadTrackingStateWithoutTrackingAction();
		}

		// Token: 0x06000A25 RID: 2597 RVA: 0x00037EAC File Offset: 0x000360AC
		private void ReadTrackingStateWithoutTrackingAction()
		{
			InputAction action = this.m_TrackingStateInput.action;
			if (action != null && action.m_BindingsCount != 0)
			{
				return;
			}
			bool flag = TrackedPoseDriver.HasResolvedControl(this.m_PositionInput.action);
			bool flag2 = TrackedPoseDriver.HasResolvedControl(this.m_RotationInput.action);
			if (flag && flag2)
			{
				this.m_CurrentTrackingState = TrackedPoseDriver.TrackingStates.Position | TrackedPoseDriver.TrackingStates.Rotation;
				return;
			}
			if (flag)
			{
				this.m_CurrentTrackingState = TrackedPoseDriver.TrackingStates.Position;
				return;
			}
			if (flag2)
			{
				this.m_CurrentTrackingState = TrackedPoseDriver.TrackingStates.Rotation;
				return;
			}
			this.m_CurrentTrackingState = TrackedPoseDriver.TrackingStates.None;
		}

		// Token: 0x06000A26 RID: 2598 RVA: 0x00037F20 File Offset: 0x00036120
		private void ReadTrackingState()
		{
			InputAction action = this.m_TrackingStateInput.action;
			if (action != null && !action.enabled)
			{
				this.m_CurrentTrackingState = TrackedPoseDriver.TrackingStates.None;
				return;
			}
			if (TrackedPoseDriver.HasResolvedControl(action))
			{
				this.m_CurrentTrackingState = (TrackedPoseDriver.TrackingStates)action.ReadValue<int>();
				return;
			}
			this.ReadTrackingStateWithoutTrackingAction();
		}

		// Token: 0x06000A27 RID: 2599 RVA: 0x00037F67 File Offset: 0x00036167
		protected virtual void OnUpdate()
		{
			if (this.m_UpdateType == TrackedPoseDriver.UpdateType.Update || this.m_UpdateType == TrackedPoseDriver.UpdateType.UpdateAndBeforeRender)
			{
				this.PerformUpdate();
			}
		}

		// Token: 0x06000A28 RID: 2600 RVA: 0x00037F80 File Offset: 0x00036180
		protected virtual void OnBeforeRender()
		{
			if (this.m_UpdateType == TrackedPoseDriver.UpdateType.BeforeRender || this.m_UpdateType == TrackedPoseDriver.UpdateType.UpdateAndBeforeRender)
			{
				this.PerformUpdate();
			}
		}

		// Token: 0x06000A29 RID: 2601 RVA: 0x00037F99 File Offset: 0x00036199
		protected virtual void PerformUpdate()
		{
			this.SetLocalTransform(this.m_CurrentPosition, this.m_CurrentRotation);
		}

		// Token: 0x06000A2A RID: 2602 RVA: 0x00037FB0 File Offset: 0x000361B0
		protected virtual void SetLocalTransform(Vector3 newPosition, Quaternion newRotation)
		{
			bool flag = this.m_IgnoreTrackingState || (this.m_CurrentTrackingState & TrackedPoseDriver.TrackingStates.Position) > TrackedPoseDriver.TrackingStates.None;
			bool flag2 = this.m_IgnoreTrackingState || (this.m_CurrentTrackingState & TrackedPoseDriver.TrackingStates.Rotation) > TrackedPoseDriver.TrackingStates.None;
			switch (this.m_TrackingType)
			{
			case TrackedPoseDriver.TrackingType.RotationAndPosition:
				if (flag2 && flag)
				{
					base.transform.SetLocalPositionAndRotation(newPosition, newRotation);
					return;
				}
				if (flag2)
				{
					base.transform.localRotation = newRotation;
					return;
				}
				if (flag)
				{
					base.transform.localPosition = newPosition;
					return;
				}
				break;
			case TrackedPoseDriver.TrackingType.RotationOnly:
				if (flag2)
				{
					base.transform.localRotation = newRotation;
				}
				break;
			case TrackedPoseDriver.TrackingType.PositionOnly:
				if (flag)
				{
					base.transform.localPosition = newPosition;
					return;
				}
				break;
			default:
				return;
			}
		}

		// Token: 0x06000A2B RID: 2603 RVA: 0x00038058 File Offset: 0x00036258
		private unsafe static bool HasResolvedControl(InputAction action)
		{
			if (action == null)
			{
				return false;
			}
			InputActionMap orCreateActionMap = action.GetOrCreateActionMap();
			orCreateActionMap.ResolveBindingsIfNecessary();
			InputActionState state = orCreateActionMap.m_State;
			if (state == null)
			{
				return false;
			}
			int actionIndexInState = action.m_ActionIndexInState;
			int totalBindingCount = state.totalBindingCount;
			for (int i = 0; i < totalBindingCount; i++)
			{
				ref InputActionState.BindingState ptr = ref state.bindingStates[i];
				if (ptr.actionIndex == actionIndexInState && !ptr.isComposite && ptr.controlCount > 0)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x17000294 RID: 660
		// (get) Token: 0x06000A2C RID: 2604 RVA: 0x000380CE File Offset: 0x000362CE
		// (set) Token: 0x06000A2D RID: 2605 RVA: 0x000380DB File Offset: 0x000362DB
		public InputAction positionAction
		{
			get
			{
				return this.m_PositionInput.action;
			}
			set
			{
				this.positionInput = new InputActionProperty(value);
			}
		}

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x06000A2E RID: 2606 RVA: 0x000380E9 File Offset: 0x000362E9
		// (set) Token: 0x06000A2F RID: 2607 RVA: 0x000380F6 File Offset: 0x000362F6
		public InputAction rotationAction
		{
			get
			{
				return this.m_RotationInput.action;
			}
			set
			{
				this.rotationInput = new InputActionProperty(value);
			}
		}

		// Token: 0x06000A30 RID: 2608 RVA: 0x00038104 File Offset: 0x00036304
		void ISerializationCallbackReceiver.OnBeforeSerialize()
		{
		}

		// Token: 0x06000A31 RID: 2609 RVA: 0x00038108 File Offset: 0x00036308
		void ISerializationCallbackReceiver.OnAfterDeserialize()
		{
			if (this.m_PositionInput.serializedReference == null && this.m_PositionInput.serializedAction == null && this.m_PositionAction != null)
			{
				this.m_PositionInput = new InputActionProperty(this.m_PositionAction);
			}
			if (this.m_RotationInput.serializedReference == null && this.m_RotationInput.serializedAction == null && this.m_RotationAction != null)
			{
				this.m_RotationInput = new InputActionProperty(this.m_RotationAction);
			}
		}

		// Token: 0x04000354 RID: 852
		[SerializeField]
		[Tooltip("Which Transform properties to update.")]
		private TrackedPoseDriver.TrackingType m_TrackingType;

		// Token: 0x04000355 RID: 853
		[SerializeField]
		[Tooltip("Updates the Transform properties after these phases of Input System event processing.")]
		private TrackedPoseDriver.UpdateType m_UpdateType;

		// Token: 0x04000356 RID: 854
		[SerializeField]
		[Tooltip("Ignore Tracking State and always treat the input pose as valid.")]
		private bool m_IgnoreTrackingState;

		// Token: 0x04000357 RID: 855
		[SerializeField]
		[Tooltip("The input action to read the position value of a tracked device. Must be a Vector 3 control type.")]
		private InputActionProperty m_PositionInput;

		// Token: 0x04000358 RID: 856
		[SerializeField]
		[Tooltip("The input action to read the rotation value of a tracked device. Must be a Quaternion control type.")]
		private InputActionProperty m_RotationInput;

		// Token: 0x04000359 RID: 857
		[SerializeField]
		[Tooltip("The input action to read the tracking state value of a tracked device. Identifies if position and rotation have valid data. Must be an Integer control type.")]
		private InputActionProperty m_TrackingStateInput;

		// Token: 0x0400035A RID: 858
		private Vector3 m_CurrentPosition = Vector3.zero;

		// Token: 0x0400035B RID: 859
		private Quaternion m_CurrentRotation = Quaternion.identity;

		// Token: 0x0400035C RID: 860
		private TrackedPoseDriver.TrackingStates m_CurrentTrackingState = TrackedPoseDriver.TrackingStates.Position | TrackedPoseDriver.TrackingStates.Rotation;

		// Token: 0x0400035D RID: 861
		private bool m_RotationBound;

		// Token: 0x0400035E RID: 862
		private bool m_PositionBound;

		// Token: 0x0400035F RID: 863
		private bool m_TrackingStateBound;

		// Token: 0x04000360 RID: 864
		private bool m_IsFirstUpdate = true;

		// Token: 0x04000361 RID: 865
		[Obsolete]
		[SerializeField]
		[HideInInspector]
		private InputAction m_PositionAction;

		// Token: 0x04000362 RID: 866
		[Obsolete]
		[SerializeField]
		[HideInInspector]
		private InputAction m_RotationAction;

		// Token: 0x020001C5 RID: 453
		public enum TrackingType
		{
			// Token: 0x04000967 RID: 2407
			RotationAndPosition,
			// Token: 0x04000968 RID: 2408
			RotationOnly,
			// Token: 0x04000969 RID: 2409
			PositionOnly
		}

		// Token: 0x020001C6 RID: 454
		[Flags]
		private enum TrackingStates
		{
			// Token: 0x0400096B RID: 2411
			None = 0,
			// Token: 0x0400096C RID: 2412
			Position = 1,
			// Token: 0x0400096D RID: 2413
			Rotation = 2
		}

		// Token: 0x020001C7 RID: 455
		public enum UpdateType
		{
			// Token: 0x0400096F RID: 2415
			UpdateAndBeforeRender,
			// Token: 0x04000970 RID: 2416
			Update,
			// Token: 0x04000971 RID: 2417
			BeforeRender
		}
	}
}
