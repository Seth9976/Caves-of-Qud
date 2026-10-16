using System;
using UnityEngine.Events;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Users;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem
{
	// Token: 0x0200005F RID: 95
	[AddComponentMenu("Input/Player Input Manager")]
	[HelpURL("https://docs.unity3d.com/Packages/com.unity.inputsystem@1.19/manual/PlayerInputManager.html")]
	public class PlayerInputManager : MonoBehaviour
	{
		// Token: 0x17000270 RID: 624
		// (get) Token: 0x060009AB RID: 2475 RVA: 0x00036770 File Offset: 0x00034970
		// (set) Token: 0x060009AC RID: 2476 RVA: 0x00036778 File Offset: 0x00034978
		public bool splitScreen
		{
			get
			{
				return this.m_SplitScreen;
			}
			set
			{
				if (this.m_SplitScreen == value)
				{
					return;
				}
				this.m_SplitScreen = value;
				if (!this.m_SplitScreen)
				{
					using (ReadOnlyArray<PlayerInput>.Enumerator enumerator = PlayerInput.all.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							PlayerInput playerInput = enumerator.Current;
							Camera camera = playerInput.camera;
							if (camera != null)
							{
								camera.rect = new Rect(0f, 0f, 1f, 1f);
							}
						}
						return;
					}
				}
				this.UpdateSplitScreen();
			}
		}

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x060009AD RID: 2477 RVA: 0x00036814 File Offset: 0x00034A14
		public bool maintainAspectRatioInSplitScreen
		{
			get
			{
				return this.m_MaintainAspectRatioInSplitScreen;
			}
		}

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x060009AE RID: 2478 RVA: 0x0003681C File Offset: 0x00034A1C
		public int fixedNumberOfSplitScreens
		{
			get
			{
				return this.m_FixedNumberOfSplitScreens;
			}
		}

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x060009AF RID: 2479 RVA: 0x00036824 File Offset: 0x00034A24
		public Rect splitScreenArea
		{
			get
			{
				return this.m_SplitScreenRect;
			}
		}

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x060009B0 RID: 2480 RVA: 0x0003682C File Offset: 0x00034A2C
		public int playerCount
		{
			get
			{
				return PlayerInput.s_AllActivePlayersCount;
			}
		}

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x060009B1 RID: 2481 RVA: 0x00036833 File Offset: 0x00034A33
		public int maxPlayerCount
		{
			get
			{
				return this.m_MaxPlayerCount;
			}
		}

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x060009B2 RID: 2482 RVA: 0x0003683B File Offset: 0x00034A3B
		public bool joiningEnabled
		{
			get
			{
				return this.m_AllowJoining;
			}
		}

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x060009B3 RID: 2483 RVA: 0x00036843 File Offset: 0x00034A43
		// (set) Token: 0x060009B4 RID: 2484 RVA: 0x0003684B File Offset: 0x00034A4B
		public PlayerJoinBehavior joinBehavior
		{
			get
			{
				return this.m_JoinBehavior;
			}
			set
			{
				if (this.m_JoinBehavior == value)
				{
					return;
				}
				bool allowJoining = this.m_AllowJoining;
				if (allowJoining)
				{
					this.DisableJoining();
				}
				this.m_JoinBehavior = value;
				if (allowJoining)
				{
					this.EnableJoining();
				}
			}
		}

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x060009B5 RID: 2485 RVA: 0x00036875 File Offset: 0x00034A75
		// (set) Token: 0x060009B6 RID: 2486 RVA: 0x0003687D File Offset: 0x00034A7D
		public InputActionProperty joinAction
		{
			get
			{
				return this.m_JoinAction;
			}
			set
			{
				if (this.m_JoinAction == value)
				{
					return;
				}
				bool flag = this.m_AllowJoining && this.m_JoinBehavior == PlayerJoinBehavior.JoinPlayersWhenJoinActionIsTriggered;
				if (flag)
				{
					this.DisableJoining();
				}
				this.m_JoinAction = value;
				if (flag)
				{
					this.EnableJoining();
				}
			}
		}

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x060009B7 RID: 2487 RVA: 0x000368BA File Offset: 0x00034ABA
		// (set) Token: 0x060009B8 RID: 2488 RVA: 0x000368C2 File Offset: 0x00034AC2
		public PlayerNotifications notificationBehavior
		{
			get
			{
				return this.m_NotificationBehavior;
			}
			set
			{
				this.m_NotificationBehavior = value;
			}
		}

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x060009B9 RID: 2489 RVA: 0x000368CB File Offset: 0x00034ACB
		public PlayerInputManager.PlayerJoinedEvent playerJoinedEvent
		{
			get
			{
				if (this.m_PlayerJoinedEvent == null)
				{
					this.m_PlayerJoinedEvent = new PlayerInputManager.PlayerJoinedEvent();
				}
				return this.m_PlayerJoinedEvent;
			}
		}

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x060009BA RID: 2490 RVA: 0x000368E6 File Offset: 0x00034AE6
		public PlayerInputManager.PlayerLeftEvent playerLeftEvent
		{
			get
			{
				if (this.m_PlayerLeftEvent == null)
				{
					this.m_PlayerLeftEvent = new PlayerInputManager.PlayerLeftEvent();
				}
				return this.m_PlayerLeftEvent;
			}
		}

		// Token: 0x1400001E RID: 30
		// (add) Token: 0x060009BB RID: 2491 RVA: 0x00036901 File Offset: 0x00034B01
		// (remove) Token: 0x060009BC RID: 2492 RVA: 0x0003691D File Offset: 0x00034B1D
		public event Action<PlayerInput> onPlayerJoined
		{
			add
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.m_PlayerJoinedCallbacks.AddCallback(value);
			}
			remove
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.m_PlayerJoinedCallbacks.RemoveCallback(value);
			}
		}

		// Token: 0x1400001F RID: 31
		// (add) Token: 0x060009BD RID: 2493 RVA: 0x00036939 File Offset: 0x00034B39
		// (remove) Token: 0x060009BE RID: 2494 RVA: 0x00036955 File Offset: 0x00034B55
		public event Action<PlayerInput> onPlayerLeft
		{
			add
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.m_PlayerLeftCallbacks.AddCallback(value);
			}
			remove
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.m_PlayerLeftCallbacks.RemoveCallback(value);
			}
		}

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x060009BF RID: 2495 RVA: 0x00036971 File Offset: 0x00034B71
		// (set) Token: 0x060009C0 RID: 2496 RVA: 0x00036979 File Offset: 0x00034B79
		public GameObject playerPrefab
		{
			get
			{
				return this.m_PlayerPrefab;
			}
			set
			{
				this.m_PlayerPrefab = value;
			}
		}

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x060009C1 RID: 2497 RVA: 0x00036982 File Offset: 0x00034B82
		// (set) Token: 0x060009C2 RID: 2498 RVA: 0x00036989 File Offset: 0x00034B89
		public static PlayerInputManager instance { get; private set; }

		// Token: 0x060009C3 RID: 2499 RVA: 0x00036994 File Offset: 0x00034B94
		public void EnableJoining()
		{
			PlayerJoinBehavior joinBehavior = this.m_JoinBehavior;
			if (joinBehavior != PlayerJoinBehavior.JoinPlayersWhenButtonIsPressed)
			{
				if (joinBehavior == PlayerJoinBehavior.JoinPlayersWhenJoinActionIsTriggered)
				{
					if (this.m_JoinAction.action != null)
					{
						if (!this.m_JoinActionDelegateHooked)
						{
							if (this.m_JoinActionDelegate == null)
							{
								this.m_JoinActionDelegate = new Action<InputAction.CallbackContext>(this.JoinPlayerFromActionIfNotAlreadyJoined);
							}
							this.m_JoinAction.action.performed += this.m_JoinActionDelegate;
							this.m_JoinActionDelegateHooked = true;
						}
						this.m_JoinAction.action.Enable();
					}
					else
					{
						Debug.LogError("No join action configured on PlayerInputManager but join behavior is set to JoinPlayersWhenJoinActionIsTriggered", this);
					}
				}
			}
			else
			{
				this.ValidateInputActionAsset();
				if (!this.m_UnpairedDeviceUsedDelegateHooked)
				{
					if (this.m_UnpairedDeviceUsedDelegate == null)
					{
						this.m_UnpairedDeviceUsedDelegate = new Action<InputControl, InputEventPtr>(this.OnUnpairedDeviceUsed);
					}
					InputUser.onUnpairedDeviceUsed += this.m_UnpairedDeviceUsedDelegate;
					this.m_UnpairedDeviceUsedDelegateHooked = true;
					InputUser.listenForUnpairedDeviceActivity++;
				}
			}
			this.m_AllowJoining = true;
		}

		// Token: 0x060009C4 RID: 2500 RVA: 0x00036A70 File Offset: 0x00034C70
		public void DisableJoining()
		{
			PlayerJoinBehavior joinBehavior = this.m_JoinBehavior;
			if (joinBehavior != PlayerJoinBehavior.JoinPlayersWhenButtonIsPressed)
			{
				if (joinBehavior == PlayerJoinBehavior.JoinPlayersWhenJoinActionIsTriggered)
				{
					if (this.m_JoinActionDelegateHooked)
					{
						if (this.m_JoinAction.action != null)
						{
							this.m_JoinAction.action.performed -= this.m_JoinActionDelegate;
						}
						this.m_JoinActionDelegateHooked = false;
					}
					InputAction action = this.m_JoinAction.action;
					if (action != null)
					{
						action.Disable();
					}
				}
			}
			else if (this.m_UnpairedDeviceUsedDelegateHooked)
			{
				InputUser.onUnpairedDeviceUsed -= this.m_UnpairedDeviceUsedDelegate;
				this.m_UnpairedDeviceUsedDelegateHooked = false;
				InputUser.listenForUnpairedDeviceActivity--;
			}
			this.m_AllowJoining = false;
		}

		// Token: 0x060009C5 RID: 2501 RVA: 0x00036B04 File Offset: 0x00034D04
		internal void JoinPlayerFromUI()
		{
			if (!this.CheckIfPlayerCanJoin(-1))
			{
				return;
			}
			throw new NotImplementedException();
		}

		// Token: 0x060009C6 RID: 2502 RVA: 0x00036B18 File Offset: 0x00034D18
		public void JoinPlayerFromAction(InputAction.CallbackContext context)
		{
			if (!this.CheckIfPlayerCanJoin(-1))
			{
				return;
			}
			InputDevice device = context.control.device;
			this.JoinPlayer(-1, -1, null, device);
		}

		// Token: 0x060009C7 RID: 2503 RVA: 0x00036B48 File Offset: 0x00034D48
		public void JoinPlayerFromActionIfNotAlreadyJoined(InputAction.CallbackContext context)
		{
			if (!this.CheckIfPlayerCanJoin(-1))
			{
				return;
			}
			InputDevice device = context.control.device;
			if (PlayerInput.FindFirstPairedToDevice(device) != null)
			{
				return;
			}
			this.JoinPlayer(-1, -1, null, device);
		}

		// Token: 0x060009C8 RID: 2504 RVA: 0x00036B88 File Offset: 0x00034D88
		public PlayerInput JoinPlayer(int playerIndex = -1, int splitScreenIndex = -1, string controlScheme = null, InputDevice pairWithDevice = null)
		{
			if (!this.CheckIfPlayerCanJoin(playerIndex))
			{
				return null;
			}
			PlayerInput.s_DestroyIfDeviceSetupUnsuccessful = true;
			return PlayerInput.Instantiate(this.m_PlayerPrefab, playerIndex, controlScheme, splitScreenIndex, pairWithDevice);
		}

		// Token: 0x060009C9 RID: 2505 RVA: 0x00036BB8 File Offset: 0x00034DB8
		public PlayerInput JoinPlayer(int playerIndex = -1, int splitScreenIndex = -1, string controlScheme = null, params InputDevice[] pairWithDevices)
		{
			if (!this.CheckIfPlayerCanJoin(playerIndex))
			{
				return null;
			}
			PlayerInput.s_DestroyIfDeviceSetupUnsuccessful = true;
			return PlayerInput.Instantiate(this.m_PlayerPrefab, playerIndex, controlScheme, splitScreenIndex, pairWithDevices);
		}

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x060009CA RID: 2506 RVA: 0x00036BE8 File Offset: 0x00034DE8
		internal static string[] messages
		{
			get
			{
				return new string[] { "OnPlayerJoined", "OnPlayerLeft" };
			}
		}

		// Token: 0x060009CB RID: 2507 RVA: 0x00036C00 File Offset: 0x00034E00
		private bool CheckIfPlayerCanJoin(int playerIndex = -1)
		{
			if (this.m_PlayerPrefab == null)
			{
				Debug.LogError("playerPrefab must be set in order to be able to join new players", this);
				return false;
			}
			if (this.m_MaxPlayerCount >= 0 && this.playerCount >= this.m_MaxPlayerCount)
			{
				Debug.LogWarning("Maximum number of supported players reached: " + this.maxPlayerCount.ToString(), this);
				return false;
			}
			if (playerIndex != -1)
			{
				for (int i = 0; i < PlayerInput.s_AllActivePlayersCount; i++)
				{
					if (PlayerInput.s_AllActivePlayers[i].playerIndex == playerIndex)
					{
						Debug.LogError(string.Format("Player index #{0} is already taken by player {1}", playerIndex, PlayerInput.s_AllActivePlayers[i]), PlayerInput.s_AllActivePlayers[i]);
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x060009CC RID: 2508 RVA: 0x00036CA8 File Offset: 0x00034EA8
		private void OnUnpairedDeviceUsed(InputControl control, InputEventPtr eventPtr)
		{
			if (!this.m_AllowJoining)
			{
				return;
			}
			if (this.m_JoinBehavior == PlayerJoinBehavior.JoinPlayersWhenButtonIsPressed)
			{
				if (!(control is ButtonControl))
				{
					return;
				}
				if (!this.IsDeviceUsableWithPlayerActions(control.device))
				{
					return;
				}
				this.JoinPlayer(-1, -1, null, control.device);
			}
		}

		// Token: 0x060009CD RID: 2509 RVA: 0x00036CE4 File Offset: 0x00034EE4
		private void OnEnable()
		{
			if (PlayerInputManager.instance == null)
			{
				PlayerInputManager.instance = this;
				if (this.joinAction.reference != null)
				{
					InputAction action = this.joinAction.action;
					Object @object;
					if (action == null)
					{
						@object = null;
					}
					else
					{
						InputActionMap actionMap = action.actionMap;
						@object = ((actionMap != null) ? actionMap.asset : null);
					}
					if (@object != null)
					{
						InputActionReference inputActionReference = InputActionReference.Create(Object.Instantiate<InputActionAsset>(this.joinAction.action.actionMap.asset).FindAction(this.joinAction.action.name, false));
						this.joinAction = new InputActionProperty(inputActionReference);
					}
				}
				for (int i = 0; i < PlayerInput.s_AllActivePlayersCount; i++)
				{
					this.NotifyPlayerJoined(PlayerInput.s_AllActivePlayers[i]);
				}
				if (this.m_AllowJoining)
				{
					this.EnableJoining();
				}
				return;
			}
			Debug.LogWarning("Multiple PlayerInputManagers in the game. There should only be one PlayerInputManager", this);
		}

		// Token: 0x060009CE RID: 2510 RVA: 0x00036DC9 File Offset: 0x00034FC9
		private void OnDisable()
		{
			if (PlayerInputManager.instance == this)
			{
				PlayerInputManager.instance = null;
			}
			if (this.m_AllowJoining)
			{
				this.DisableJoining();
			}
		}

		// Token: 0x060009CF RID: 2511 RVA: 0x00036DEC File Offset: 0x00034FEC
		private void UpdateSplitScreen()
		{
			if (!this.m_SplitScreen)
			{
				return;
			}
			int num = 0;
			foreach (PlayerInput playerInput in PlayerInput.all)
			{
				if (playerInput.playerIndex >= num)
				{
					num = playerInput.playerIndex + 1;
				}
			}
			if (this.m_FixedNumberOfSplitScreens > 0)
			{
				if (this.m_FixedNumberOfSplitScreens < num)
				{
					Debug.LogWarning(string.Format("Highest playerIndex of {0} exceeds fixed number of split-screens of {1}", num, this.m_FixedNumberOfSplitScreens), this);
				}
				num = this.m_FixedNumberOfSplitScreens;
			}
			int num2 = Mathf.CeilToInt(Mathf.Sqrt((float)num));
			int num3 = num2;
			if (!this.m_MaintainAspectRatioInSplitScreen && num2 * (num2 - 1) >= num)
			{
				num3--;
			}
			foreach (PlayerInput playerInput2 in PlayerInput.all)
			{
				int splitScreenIndex = playerInput2.splitScreenIndex;
				if (splitScreenIndex >= num2 * num3)
				{
					Debug.LogError(string.Format("Split-screen index of {0} on player is out of range (have {1} screens); resetting to playerIndex", splitScreenIndex, num2 * num3), playerInput2);
					playerInput2.m_SplitScreenIndex = playerInput2.playerIndex;
				}
				Camera camera = playerInput2.camera;
				if (camera == null)
				{
					Debug.LogError("Player has no camera associated with it. Cannot set up split-screen. Point PlayerInput.camera to camera for player.", playerInput2);
				}
				else
				{
					int num4 = splitScreenIndex % num2;
					int num5 = splitScreenIndex / num2;
					Rect rect = new Rect
					{
						width = this.m_SplitScreenRect.width / (float)num2,
						height = this.m_SplitScreenRect.height / (float)num3
					};
					rect.x = this.m_SplitScreenRect.x + (float)num4 * rect.width;
					rect.y = this.m_SplitScreenRect.y + this.m_SplitScreenRect.height - (float)(num5 + 1) * rect.height;
					camera.rect = rect;
				}
			}
		}

		// Token: 0x060009D0 RID: 2512 RVA: 0x0003700C File Offset: 0x0003520C
		private bool IsDeviceUsableWithPlayerActions(InputDevice device)
		{
			if (this.m_PlayerPrefab == null)
			{
				return true;
			}
			PlayerInput componentInChildren = this.m_PlayerPrefab.GetComponentInChildren<PlayerInput>();
			if (componentInChildren == null)
			{
				return true;
			}
			InputActionAsset actions = componentInChildren.actions;
			if (actions == null)
			{
				return true;
			}
			if (actions.controlSchemes.Count > 0)
			{
				using (InputControlList<InputDevice> unpairedInputDevices = InputUser.GetUnpairedInputDevices())
				{
					if (InputControlScheme.FindControlSchemeForDevices<InputControlList<InputDevice>, ReadOnlyArray<InputControlScheme>>(unpairedInputDevices, actions.controlSchemes, device, false) == null)
					{
						return false;
					}
				}
				return true;
			}
			using (ReadOnlyArray<InputActionMap>.Enumerator enumerator = actions.actionMaps.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.IsUsableWithDevice(device))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060009D1 RID: 2513 RVA: 0x000370FC File Offset: 0x000352FC
		private void ValidateInputActionAsset()
		{
		}

		// Token: 0x060009D2 RID: 2514 RVA: 0x00037100 File Offset: 0x00035300
		internal void NotifyPlayerJoined(PlayerInput player)
		{
			this.UpdateSplitScreen();
			switch (this.m_NotificationBehavior)
			{
			case PlayerNotifications.SendMessages:
				base.SendMessage("OnPlayerJoined", player, SendMessageOptions.DontRequireReceiver);
				return;
			case PlayerNotifications.BroadcastMessages:
				base.BroadcastMessage("OnPlayerJoined", player, SendMessageOptions.DontRequireReceiver);
				return;
			case PlayerNotifications.InvokeUnityEvents:
			{
				PlayerInputManager.PlayerJoinedEvent playerJoinedEvent = this.m_PlayerJoinedEvent;
				if (playerJoinedEvent == null)
				{
					return;
				}
				playerJoinedEvent.Invoke(player);
				return;
			}
			case PlayerNotifications.InvokeCSharpEvents:
				DelegateHelpers.InvokeCallbacksSafe<PlayerInput>(ref this.m_PlayerJoinedCallbacks, player, "onPlayerJoined", null);
				return;
			default:
				return;
			}
		}

		// Token: 0x060009D3 RID: 2515 RVA: 0x00037174 File Offset: 0x00035374
		internal void NotifyPlayerLeft(PlayerInput player)
		{
			this.UpdateSplitScreen();
			switch (this.m_NotificationBehavior)
			{
			case PlayerNotifications.SendMessages:
				base.SendMessage("OnPlayerLeft", player, SendMessageOptions.DontRequireReceiver);
				return;
			case PlayerNotifications.BroadcastMessages:
				base.BroadcastMessage("OnPlayerLeft", player, SendMessageOptions.DontRequireReceiver);
				return;
			case PlayerNotifications.InvokeUnityEvents:
			{
				PlayerInputManager.PlayerLeftEvent playerLeftEvent = this.m_PlayerLeftEvent;
				if (playerLeftEvent == null)
				{
					return;
				}
				playerLeftEvent.Invoke(player);
				return;
			}
			case PlayerNotifications.InvokeCSharpEvents:
				DelegateHelpers.InvokeCallbacksSafe<PlayerInput>(ref this.m_PlayerLeftCallbacks, player, "onPlayerLeft", null);
				return;
			default:
				return;
			}
		}

		// Token: 0x04000320 RID: 800
		public const string PlayerJoinedMessage = "OnPlayerJoined";

		// Token: 0x04000321 RID: 801
		public const string PlayerLeftMessage = "OnPlayerLeft";

		// Token: 0x04000323 RID: 803
		[SerializeField]
		internal PlayerNotifications m_NotificationBehavior;

		// Token: 0x04000324 RID: 804
		[Tooltip("Set a limit for the maximum number of players who are able to join.")]
		[SerializeField]
		internal int m_MaxPlayerCount = -1;

		// Token: 0x04000325 RID: 805
		[SerializeField]
		internal bool m_AllowJoining = true;

		// Token: 0x04000326 RID: 806
		[SerializeField]
		internal PlayerJoinBehavior m_JoinBehavior;

		// Token: 0x04000327 RID: 807
		[SerializeField]
		internal PlayerInputManager.PlayerJoinedEvent m_PlayerJoinedEvent;

		// Token: 0x04000328 RID: 808
		[SerializeField]
		internal PlayerInputManager.PlayerLeftEvent m_PlayerLeftEvent;

		// Token: 0x04000329 RID: 809
		[SerializeField]
		internal InputActionProperty m_JoinAction;

		// Token: 0x0400032A RID: 810
		[SerializeField]
		internal GameObject m_PlayerPrefab;

		// Token: 0x0400032B RID: 811
		[SerializeField]
		internal bool m_SplitScreen;

		// Token: 0x0400032C RID: 812
		[SerializeField]
		internal bool m_MaintainAspectRatioInSplitScreen;

		// Token: 0x0400032D RID: 813
		[Tooltip("Explicitly set a fixed number of screens or otherwise allow the screen to be divided automatically to best fit the number of players.")]
		[SerializeField]
		internal int m_FixedNumberOfSplitScreens = -1;

		// Token: 0x0400032E RID: 814
		[SerializeField]
		internal Rect m_SplitScreenRect = new Rect(0f, 0f, 1f, 1f);

		// Token: 0x0400032F RID: 815
		[NonSerialized]
		private bool m_JoinActionDelegateHooked;

		// Token: 0x04000330 RID: 816
		[NonSerialized]
		private bool m_UnpairedDeviceUsedDelegateHooked;

		// Token: 0x04000331 RID: 817
		[NonSerialized]
		private Action<InputAction.CallbackContext> m_JoinActionDelegate;

		// Token: 0x04000332 RID: 818
		[NonSerialized]
		private Action<InputControl, InputEventPtr> m_UnpairedDeviceUsedDelegate;

		// Token: 0x04000333 RID: 819
		[NonSerialized]
		private CallbackArray<Action<PlayerInput>> m_PlayerJoinedCallbacks;

		// Token: 0x04000334 RID: 820
		[NonSerialized]
		private CallbackArray<Action<PlayerInput>> m_PlayerLeftCallbacks;

		// Token: 0x020001C3 RID: 451
		[Serializable]
		public class PlayerJoinedEvent : UnityEvent<PlayerInput>
		{
		}

		// Token: 0x020001C4 RID: 452
		[Serializable]
		public class PlayerLeftEvent : UnityEvent<PlayerInput>
		{
		}
	}
}
