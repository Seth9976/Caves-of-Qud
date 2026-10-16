using System;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Profiling;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.Users
{
	// Token: 0x02000080 RID: 128
	public struct InputUser : IEquatable<InputUser>
	{
		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x06000AA0 RID: 2720 RVA: 0x000395AC File Offset: 0x000377AC
		public bool valid
		{
			get
			{
				if (this.m_Id == 0U)
				{
					return false;
				}
				for (int i = 0; i < InputUser.s_GlobalState.allUserCount; i++)
				{
					if (InputUser.s_GlobalState.allUsers[i].m_Id == this.m_Id)
					{
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x06000AA1 RID: 2721 RVA: 0x000395F8 File Offset: 0x000377F8
		public int index
		{
			get
			{
				if (this.m_Id == 0U)
				{
					throw new InvalidOperationException("Invalid user");
				}
				int num = InputUser.TryFindUserIndex(this.m_Id);
				if (num == -1)
				{
					throw new InvalidOperationException(string.Format("User with ID {0} is no longer valid", this.m_Id));
				}
				return num;
			}
		}

		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x06000AA2 RID: 2722 RVA: 0x00039637 File Offset: 0x00037837
		public uint id
		{
			get
			{
				return this.m_Id;
			}
		}

		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x06000AA3 RID: 2723 RVA: 0x0003963F File Offset: 0x0003783F
		public InputUserAccountHandle? platformUserAccountHandle
		{
			get
			{
				return InputUser.s_GlobalState.allUserData[this.index].platformUserAccountHandle;
			}
		}

		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x06000AA4 RID: 2724 RVA: 0x0003965B File Offset: 0x0003785B
		public string platformUserAccountName
		{
			get
			{
				return InputUser.s_GlobalState.allUserData[this.index].platformUserAccountName;
			}
		}

		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x06000AA5 RID: 2725 RVA: 0x00039677 File Offset: 0x00037877
		public string platformUserAccountId
		{
			get
			{
				return InputUser.s_GlobalState.allUserData[this.index].platformUserAccountId;
			}
		}

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x06000AA6 RID: 2726 RVA: 0x00039694 File Offset: 0x00037894
		public ReadOnlyArray<InputDevice> pairedDevices
		{
			get
			{
				int index = this.index;
				return new ReadOnlyArray<InputDevice>(InputUser.s_GlobalState.allPairedDevices, InputUser.s_GlobalState.allUserData[index].deviceStartIndex, InputUser.s_GlobalState.allUserData[index].deviceCount);
			}
		}

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x06000AA7 RID: 2727 RVA: 0x000396E4 File Offset: 0x000378E4
		public ReadOnlyArray<InputDevice> lostDevices
		{
			get
			{
				int index = this.index;
				return new ReadOnlyArray<InputDevice>(InputUser.s_GlobalState.allLostDevices, InputUser.s_GlobalState.allUserData[index].lostDeviceStartIndex, InputUser.s_GlobalState.allUserData[index].lostDeviceCount);
			}
		}

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x06000AA8 RID: 2728 RVA: 0x00039731 File Offset: 0x00037931
		public IInputActionCollection actions
		{
			get
			{
				return InputUser.s_GlobalState.allUserData[this.index].actions;
			}
		}

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x06000AA9 RID: 2729 RVA: 0x0003974D File Offset: 0x0003794D
		public InputControlScheme? controlScheme
		{
			get
			{
				return InputUser.s_GlobalState.allUserData[this.index].controlScheme;
			}
		}

		// Token: 0x170002CD RID: 717
		// (get) Token: 0x06000AAA RID: 2730 RVA: 0x00039769 File Offset: 0x00037969
		public InputControlScheme.MatchResult controlSchemeMatch
		{
			get
			{
				return InputUser.s_GlobalState.allUserData[this.index].controlSchemeMatch;
			}
		}

		// Token: 0x170002CE RID: 718
		// (get) Token: 0x06000AAB RID: 2731 RVA: 0x00039785 File Offset: 0x00037985
		public bool hasMissingRequiredDevices
		{
			get
			{
				return InputUser.s_GlobalState.allUserData[this.index].controlSchemeMatch.hasMissingRequiredDevices;
			}
		}

		// Token: 0x170002CF RID: 719
		// (get) Token: 0x06000AAC RID: 2732 RVA: 0x000397A6 File Offset: 0x000379A6
		public static ReadOnlyArray<InputUser> all
		{
			get
			{
				return new ReadOnlyArray<InputUser>(InputUser.s_GlobalState.allUsers, 0, InputUser.s_GlobalState.allUserCount);
			}
		}

		// Token: 0x14000020 RID: 32
		// (add) Token: 0x06000AAD RID: 2733 RVA: 0x000397C2 File Offset: 0x000379C2
		// (remove) Token: 0x06000AAE RID: 2734 RVA: 0x000397E2 File Offset: 0x000379E2
		public static event Action<InputUser, InputUserChange, InputDevice> onChange
		{
			add
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				InputUser.s_GlobalState.onChange.AddCallback(value);
			}
			remove
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				InputUser.s_GlobalState.onChange.RemoveCallback(value);
			}
		}

		// Token: 0x14000021 RID: 33
		// (add) Token: 0x06000AAF RID: 2735 RVA: 0x00039802 File Offset: 0x00037A02
		// (remove) Token: 0x06000AB0 RID: 2736 RVA: 0x00039834 File Offset: 0x00037A34
		public static event Action<InputControl, InputEventPtr> onUnpairedDeviceUsed
		{
			add
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				InputUser.s_GlobalState.onUnpairedDeviceUsed.AddCallback(value);
				if (InputUser.s_GlobalState.listenForUnpairedDeviceActivity > 0)
				{
					InputUser.HookIntoEvents();
				}
			}
			remove
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				InputUser.s_GlobalState.onUnpairedDeviceUsed.RemoveCallback(value);
				if (InputUser.s_GlobalState.onUnpairedDeviceUsed.length == 0)
				{
					InputUser.UnhookFromDeviceStateChange();
				}
			}
		}

		// Token: 0x14000022 RID: 34
		// (add) Token: 0x06000AB1 RID: 2737 RVA: 0x0003986A File Offset: 0x00037A6A
		// (remove) Token: 0x06000AB2 RID: 2738 RVA: 0x0003988A File Offset: 0x00037A8A
		public static event Func<InputDevice, InputEventPtr, bool> onPrefilterUnpairedDeviceActivity
		{
			add
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				InputUser.s_GlobalState.onPreFilterUnpairedDeviceUsed.AddCallback(value);
			}
			remove
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				InputUser.s_GlobalState.onPreFilterUnpairedDeviceUsed.RemoveCallback(value);
			}
		}

		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x06000AB3 RID: 2739 RVA: 0x000398AA File Offset: 0x00037AAA
		// (set) Token: 0x06000AB4 RID: 2740 RVA: 0x000398B8 File Offset: 0x00037AB8
		public static int listenForUnpairedDeviceActivity
		{
			get
			{
				return InputUser.s_GlobalState.listenForUnpairedDeviceActivity;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("value", "Cannot be negative");
				}
				if (value > 0 && InputUser.s_GlobalState.onUnpairedDeviceUsed.length > 0)
				{
					InputUser.HookIntoEvents();
				}
				else if (value == 0)
				{
					InputUser.UnhookFromDeviceStateChange();
				}
				InputUser.s_GlobalState.listenForUnpairedDeviceActivity = value;
			}
		}

		// Token: 0x06000AB5 RID: 2741 RVA: 0x0003990C File Offset: 0x00037B0C
		public override string ToString()
		{
			if (!this.valid)
			{
				return string.Format("<Invalid> (id: {0})", this.m_Id);
			}
			string text = string.Join<InputDevice>(",", this.pairedDevices);
			return string.Format("User #{0} (id: {1}, devices: {2}, actions: {3})", new object[] { this.index, this.m_Id, text, this.actions });
		}

		// Token: 0x06000AB6 RID: 2742 RVA: 0x00039988 File Offset: 0x00037B88
		public void AssociateActionsWithUser(IInputActionCollection actions)
		{
			int index = this.index;
			if (InputUser.s_GlobalState.allUserData[index].actions == actions)
			{
				return;
			}
			IInputActionCollection actions2 = InputUser.s_GlobalState.allUserData[index].actions;
			if (actions2 != null)
			{
				actions2.devices = null;
				actions2.bindingMask = null;
			}
			InputUser.s_GlobalState.allUserData[index].actions = actions;
			if (actions != null)
			{
				InputUser.HookIntoActionChange();
				actions.devices = new ReadOnlyArray<InputDevice>?(this.pairedDevices);
				if (InputUser.s_GlobalState.allUserData[index].controlScheme != null)
				{
					this.ActivateControlSchemeInternal(index, InputUser.s_GlobalState.allUserData[index].controlScheme.Value);
				}
			}
		}

		// Token: 0x06000AB7 RID: 2743 RVA: 0x00039A58 File Offset: 0x00037C58
		public InputUser.ControlSchemeChangeSyntax ActivateControlScheme(string schemeName)
		{
			if (!string.IsNullOrEmpty(schemeName))
			{
				InputControlScheme inputControlScheme;
				this.FindControlScheme(schemeName, out inputControlScheme);
				return this.ActivateControlScheme(inputControlScheme);
			}
			return this.ActivateControlScheme(default(InputControlScheme));
		}

		// Token: 0x06000AB8 RID: 2744 RVA: 0x00039A90 File Offset: 0x00037C90
		private bool TryFindControlScheme(string schemeName, out InputControlScheme scheme)
		{
			if (string.IsNullOrEmpty(schemeName))
			{
				scheme = default(InputControlScheme);
				return false;
			}
			if (InputUser.s_GlobalState.allUserData[this.index].actions == null)
			{
				throw new InvalidOperationException(string.Format("Cannot set control scheme '{0}' by name on user #{1} as not actions have been associated with the user yet (AssociateActionsWithUser)", schemeName, this.index));
			}
			ReadOnlyArray<InputControlScheme> controlSchemes = InputUser.s_GlobalState.allUserData[this.index].actions.controlSchemes;
			for (int i = 0; i < controlSchemes.Count; i++)
			{
				if (string.Compare(controlSchemes[i].name, schemeName, StringComparison.InvariantCultureIgnoreCase) == 0)
				{
					scheme = controlSchemes[i];
					return true;
				}
			}
			scheme = default(InputControlScheme);
			return false;
		}

		// Token: 0x06000AB9 RID: 2745 RVA: 0x00039B49 File Offset: 0x00037D49
		internal void FindControlScheme(string schemeName, out InputControlScheme scheme)
		{
			if (this.TryFindControlScheme(schemeName, out scheme))
			{
				return;
			}
			throw new ArgumentException(string.Format("Cannot find control scheme '{0}' in actions '{1}'", schemeName, InputUser.s_GlobalState.allUserData[this.index].actions));
		}

		// Token: 0x06000ABA RID: 2746 RVA: 0x00039B80 File Offset: 0x00037D80
		public InputUser.ControlSchemeChangeSyntax ActivateControlScheme(InputControlScheme scheme)
		{
			int index = this.index;
			InputControlScheme? controlScheme = InputUser.s_GlobalState.allUserData[index].controlScheme;
			if (controlScheme == null || (controlScheme != null && controlScheme.GetValueOrDefault() != scheme) || (scheme == default(InputControlScheme) && InputUser.s_GlobalState.allUserData[index].controlScheme != null))
			{
				this.ActivateControlSchemeInternal(index, scheme);
				InputUser.Notify(index, InputUserChange.ControlSchemeChanged, null);
			}
			return new InputUser.ControlSchemeChangeSyntax
			{
				m_UserIndex = index
			};
		}

		// Token: 0x06000ABB RID: 2747 RVA: 0x00039C24 File Offset: 0x00037E24
		private void ActivateControlSchemeInternal(int userIndex, InputControlScheme scheme)
		{
			bool flag = scheme == default(InputControlScheme);
			if (flag)
			{
				InputUser.s_GlobalState.allUserData[userIndex].controlScheme = null;
			}
			else
			{
				InputUser.s_GlobalState.allUserData[userIndex].controlScheme = new InputControlScheme?(scheme);
			}
			if (InputUser.s_GlobalState.allUserData[userIndex].actions != null)
			{
				if (flag)
				{
					InputUser.s_GlobalState.allUserData[userIndex].actions.bindingMask = null;
					InputUser.s_GlobalState.allUserData[userIndex].controlSchemeMatch.Dispose();
					InputUser.s_GlobalState.allUserData[userIndex].controlSchemeMatch = default(InputControlScheme.MatchResult);
					return;
				}
				InputUser.s_GlobalState.allUserData[userIndex].actions.bindingMask = new InputBinding?(new InputBinding
				{
					groups = scheme.bindingGroup
				});
				InputUser.UpdateControlSchemeMatch(userIndex, false);
				if (InputUser.s_GlobalState.allUserData[userIndex].controlSchemeMatch.isSuccessfulMatch)
				{
					InputUser.RemoveLostDevicesForUser(userIndex);
				}
			}
		}

		// Token: 0x06000ABC RID: 2748 RVA: 0x00039D54 File Offset: 0x00037F54
		public void UnpairDevice(InputDevice device)
		{
			if (device == null)
			{
				throw new ArgumentNullException("device");
			}
			int index = this.index;
			if (!this.pairedDevices.ContainsReference(device))
			{
				return;
			}
			InputUser.RemoveDeviceFromUser(index, device, false);
		}

		// Token: 0x06000ABD RID: 2749 RVA: 0x00039D90 File Offset: 0x00037F90
		public void UnpairDevices()
		{
			int index = this.index;
			InputUser.RemoveLostDevicesForUser(index);
			using (InputActionRebindingExtensions.DeferBindingResolution())
			{
				while (InputUser.s_GlobalState.allUserData[index].deviceCount > 0)
				{
					this.UnpairDevice(InputUser.s_GlobalState.allPairedDevices[InputUser.s_GlobalState.allUserData[index].deviceStartIndex + InputUser.s_GlobalState.allUserData[index].deviceCount - 1]);
				}
			}
			if (InputUser.s_GlobalState.allUserData[index].controlScheme != null)
			{
				InputUser.UpdateControlSchemeMatch(index, false);
			}
		}

		// Token: 0x06000ABE RID: 2750 RVA: 0x00039E48 File Offset: 0x00038048
		private static void RemoveLostDevicesForUser(int userIndex)
		{
			int lostDeviceCount = InputUser.s_GlobalState.allUserData[userIndex].lostDeviceCount;
			if (lostDeviceCount > 0)
			{
				int lostDeviceStartIndex = InputUser.s_GlobalState.allUserData[userIndex].lostDeviceStartIndex;
				ArrayHelpers.EraseSliceWithCapacity<InputDevice>(ref InputUser.s_GlobalState.allLostDevices, ref InputUser.s_GlobalState.allLostDeviceCount, lostDeviceStartIndex, lostDeviceCount);
				InputUser.s_GlobalState.allUserData[userIndex].lostDeviceCount = 0;
				InputUser.s_GlobalState.allUserData[userIndex].lostDeviceStartIndex = 0;
				for (int i = 0; i < InputUser.s_GlobalState.allUserCount; i++)
				{
					if (InputUser.s_GlobalState.allUserData[i].lostDeviceStartIndex > lostDeviceStartIndex)
					{
						InputUser.UserData[] allUserData = InputUser.s_GlobalState.allUserData;
						int num = i;
						allUserData[num].lostDeviceStartIndex = allUserData[num].lostDeviceStartIndex - lostDeviceCount;
					}
				}
			}
		}

		// Token: 0x06000ABF RID: 2751 RVA: 0x00039F16 File Offset: 0x00038116
		public void UnpairDevicesAndRemoveUser()
		{
			this.UnpairDevices();
			InputUser.RemoveUser(this.index);
			this.m_Id = 0U;
		}

		// Token: 0x06000AC0 RID: 2752 RVA: 0x00039F30 File Offset: 0x00038130
		public static InputControlList<InputDevice> GetUnpairedInputDevices()
		{
			InputControlList<InputDevice> inputControlList = new InputControlList<InputDevice>(Allocator.Temp, 0);
			InputUser.GetUnpairedInputDevices(ref inputControlList);
			return inputControlList;
		}

		// Token: 0x06000AC1 RID: 2753 RVA: 0x00039F50 File Offset: 0x00038150
		public static int GetUnpairedInputDevices(ref InputControlList<InputDevice> list)
		{
			int count = list.Count;
			foreach (InputDevice inputDevice in InputSystem.devices)
			{
				if (!InputUser.s_GlobalState.allPairedDevices.ContainsReference(InputUser.s_GlobalState.allPairedDeviceCount, inputDevice))
				{
					list.Add(inputDevice);
				}
			}
			return list.Count - count;
		}

		// Token: 0x06000AC2 RID: 2754 RVA: 0x00039FD0 File Offset: 0x000381D0
		public static InputUser? FindUserPairedToDevice(InputDevice device)
		{
			if (device == null)
			{
				throw new ArgumentNullException("device");
			}
			int num = InputUser.TryFindUserIndex(device);
			if (num == -1)
			{
				return null;
			}
			return new InputUser?(InputUser.s_GlobalState.allUsers[num]);
		}

		// Token: 0x06000AC3 RID: 2755 RVA: 0x0003A018 File Offset: 0x00038218
		public static InputUser? FindUserByAccount(InputUserAccountHandle platformUserAccountHandle)
		{
			if (platformUserAccountHandle == default(InputUserAccountHandle))
			{
				throw new ArgumentException("Empty platform user account handle", "platformUserAccountHandle");
			}
			int num = InputUser.TryFindUserIndex(platformUserAccountHandle);
			if (num == -1)
			{
				return null;
			}
			return new InputUser?(InputUser.s_GlobalState.allUsers[num]);
		}

		// Token: 0x06000AC4 RID: 2756 RVA: 0x0003A070 File Offset: 0x00038270
		public static InputUser CreateUserWithoutPairedDevices()
		{
			int num = InputUser.AddUser();
			return InputUser.s_GlobalState.allUsers[num];
		}

		// Token: 0x06000AC5 RID: 2757 RVA: 0x0003A094 File Offset: 0x00038294
		public static InputUser PerformPairingWithDevice(InputDevice device, InputUser user = default(InputUser), InputUserPairingOptions options = InputUserPairingOptions.None)
		{
			if (device == null)
			{
				throw new ArgumentNullException("device");
			}
			if (user != default(InputUser) && !user.valid)
			{
				throw new ArgumentException("Invalid user", "user");
			}
			int num;
			if (user == default(InputUser))
			{
				num = InputUser.AddUser();
			}
			else
			{
				num = user.index;
				if ((options & InputUserPairingOptions.UnpairCurrentDevicesFromUser) != InputUserPairingOptions.None)
				{
					user.UnpairDevices();
				}
				if (user.pairedDevices.ContainsReference(device))
				{
					if ((options & InputUserPairingOptions.ForcePlatformUserAccountSelection) != InputUserPairingOptions.None)
					{
						InputUser.InitiateUserAccountSelection(num, device, options);
					}
					return user;
				}
			}
			if (!InputUser.InitiateUserAccountSelection(num, device, options))
			{
				InputUser.AddDeviceToUser(num, device, false, false);
			}
			return InputUser.s_GlobalState.allUsers[num];
		}

		// Token: 0x06000AC6 RID: 2758 RVA: 0x0003A148 File Offset: 0x00038348
		private static bool InitiateUserAccountSelection(int userIndex, InputDevice device, InputUserPairingOptions options)
		{
			long num = (((options & InputUserPairingOptions.ForcePlatformUserAccountSelection) == InputUserPairingOptions.None) ? InputUser.UpdatePlatformUserAccount(userIndex, device) : 0L);
			if (((options & InputUserPairingOptions.ForcePlatformUserAccountSelection) != InputUserPairingOptions.None || (num != -1L && (num & 2L) == 0L && (options & InputUserPairingOptions.ForceNoPlatformUserAccountSelection) == InputUserPairingOptions.None)) && InputUser.InitiateUserAccountSelectionAtPlatformLevel(device))
			{
				InputUser.UserData[] allUserData = InputUser.s_GlobalState.allUserData;
				allUserData[userIndex].flags = allUserData[userIndex].flags | InputUser.UserFlags.UserAccountSelectionInProgress;
				InputUser.s_GlobalState.ongoingAccountSelections.Append(new InputUser.OngoingAccountSelection
				{
					device = device,
					userId = InputUser.s_GlobalState.allUsers[userIndex].id
				});
				InputUser.HookIntoDeviceChange();
				InputUser.Notify(userIndex, InputUserChange.AccountSelectionInProgress, device);
				return true;
			}
			return false;
		}

		// Token: 0x06000AC7 RID: 2759 RVA: 0x0003A1EA File Offset: 0x000383EA
		public bool Equals(InputUser other)
		{
			return this.m_Id == other.m_Id;
		}

		// Token: 0x06000AC8 RID: 2760 RVA: 0x0003A1FA File Offset: 0x000383FA
		public override bool Equals(object obj)
		{
			return obj != null && obj is InputUser && this.Equals((InputUser)obj);
		}

		// Token: 0x06000AC9 RID: 2761 RVA: 0x0003A217 File Offset: 0x00038417
		public override int GetHashCode()
		{
			return (int)this.m_Id;
		}

		// Token: 0x06000ACA RID: 2762 RVA: 0x0003A21F File Offset: 0x0003841F
		public static bool operator ==(InputUser left, InputUser right)
		{
			return left.m_Id == right.m_Id;
		}

		// Token: 0x06000ACB RID: 2763 RVA: 0x0003A22F File Offset: 0x0003842F
		public static bool operator !=(InputUser left, InputUser right)
		{
			return left.m_Id != right.m_Id;
		}

		// Token: 0x06000ACC RID: 2764 RVA: 0x0003A244 File Offset: 0x00038444
		private static int AddUser()
		{
			uint num = InputUser.s_GlobalState.lastUserId + 1U;
			InputUser.s_GlobalState.lastUserId = num;
			uint num2 = num;
			int allUserCount = InputUser.s_GlobalState.allUserCount;
			ArrayHelpers.AppendWithCapacity<InputUser>(ref InputUser.s_GlobalState.allUsers, ref allUserCount, new InputUser
			{
				m_Id = num2
			}, 10);
			int num3 = ArrayHelpers.AppendWithCapacity<InputUser.UserData>(ref InputUser.s_GlobalState.allUserData, ref InputUser.s_GlobalState.allUserCount, default(InputUser.UserData), 10);
			InputUser.Notify(num3, InputUserChange.Added, null);
			return num3;
		}

		// Token: 0x06000ACD RID: 2765 RVA: 0x0003A2C4 File Offset: 0x000384C4
		private static void RemoveUser(int userIndex)
		{
			if (InputUser.s_GlobalState.allUserData[userIndex].controlScheme != null && InputUser.s_GlobalState.allUserData[userIndex].actions != null)
			{
				InputUser.s_GlobalState.allUserData[userIndex].actions.bindingMask = null;
			}
			InputUser.s_GlobalState.allUserData[userIndex].controlSchemeMatch.Dispose();
			InputUser.RemoveLostDevicesForUser(userIndex);
			for (int i = 0; i < InputUser.s_GlobalState.ongoingAccountSelections.length; i++)
			{
				if (InputUser.s_GlobalState.ongoingAccountSelections[i].userId == InputUser.s_GlobalState.allUsers[userIndex].id)
				{
					InputUser.s_GlobalState.ongoingAccountSelections.RemoveAtByMovingTailWithCapacity(i);
					i--;
				}
			}
			InputUser.Notify(userIndex, InputUserChange.Removed, null);
			int allUserCount = InputUser.s_GlobalState.allUserCount;
			InputUser.s_GlobalState.allUsers.EraseAtWithCapacity<InputUser>(ref allUserCount, userIndex);
			InputUser.s_GlobalState.allUserData.EraseAtWithCapacity<InputUser.UserData>(ref InputUser.s_GlobalState.allUserCount, userIndex);
			if (InputUser.s_GlobalState.allUserCount == 0)
			{
				InputUser.UnhookFromDeviceChange();
				InputUser.UnhookFromActionChange();
			}
		}

		// Token: 0x06000ACE RID: 2766 RVA: 0x0003A3F8 File Offset: 0x000385F8
		private static void Notify(int userIndex, InputUserChange change, InputDevice device)
		{
			if (InputUser.s_GlobalState.onChange.length == 0)
			{
				return;
			}
			InputUser.s_GlobalState.onChange.LockForChanges();
			for (int i = 0; i < InputUser.s_GlobalState.onChange.length; i++)
			{
				try
				{
					InputUser.s_GlobalState.onChange[i](InputUser.s_GlobalState.allUsers[userIndex], change, device);
				}
				catch (Exception ex)
				{
					Debug.LogError(ex.GetType().Name + " while executing 'InputUser.onChange' callbacks");
					Debug.LogException(ex);
				}
			}
			InputUser.s_GlobalState.onChange.UnlockForChanges();
		}

		// Token: 0x06000ACF RID: 2767 RVA: 0x0003A4AC File Offset: 0x000386AC
		private static int TryFindUserIndex(uint userId)
		{
			for (int i = 0; i < InputUser.s_GlobalState.allUserCount; i++)
			{
				if (InputUser.s_GlobalState.allUsers[i].m_Id == userId)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06000AD0 RID: 2768 RVA: 0x0003A4EC File Offset: 0x000386EC
		private static int TryFindUserIndex(InputUserAccountHandle platformHandle)
		{
			for (int i = 0; i < InputUser.s_GlobalState.allUserCount; i++)
			{
				InputUserAccountHandle? platformUserAccountHandle = InputUser.s_GlobalState.allUserData[i].platformUserAccountHandle;
				if (platformUserAccountHandle != null && (platformUserAccountHandle == null || platformUserAccountHandle.GetValueOrDefault() == platformHandle))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06000AD1 RID: 2769 RVA: 0x0003A550 File Offset: 0x00038750
		private static int TryFindUserIndex(InputDevice device)
		{
			int num = InputUser.s_GlobalState.allPairedDevices.IndexOfReference(device, InputUser.s_GlobalState.allPairedDeviceCount);
			if (num == -1)
			{
				return -1;
			}
			for (int i = 0; i < InputUser.s_GlobalState.allUserCount; i++)
			{
				int deviceStartIndex = InputUser.s_GlobalState.allUserData[i].deviceStartIndex;
				if (deviceStartIndex <= num && num < deviceStartIndex + InputUser.s_GlobalState.allUserData[i].deviceCount)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06000AD2 RID: 2770 RVA: 0x0003A5CC File Offset: 0x000387CC
		private static void AddDeviceToUser(int userIndex, InputDevice device, bool asLostDevice = false, bool dontUpdateControlScheme = false)
		{
			int num = (asLostDevice ? InputUser.s_GlobalState.allUserData[userIndex].lostDeviceCount : InputUser.s_GlobalState.allUserData[userIndex].deviceCount);
			int num2 = (asLostDevice ? InputUser.s_GlobalState.allUserData[userIndex].lostDeviceStartIndex : InputUser.s_GlobalState.allUserData[userIndex].deviceStartIndex);
			InputUser.s_GlobalState.pairingStateVersion = InputUser.s_GlobalState.pairingStateVersion + 1;
			if (num > 0)
			{
				ArrayHelpers.MoveSlice<InputDevice>(asLostDevice ? InputUser.s_GlobalState.allLostDevices : InputUser.s_GlobalState.allPairedDevices, num2, asLostDevice ? (InputUser.s_GlobalState.allLostDeviceCount - num) : (InputUser.s_GlobalState.allPairedDeviceCount - num), num);
				for (int i = 0; i < InputUser.s_GlobalState.allUserCount; i++)
				{
					if (i != userIndex && (asLostDevice ? InputUser.s_GlobalState.allUserData[i].lostDeviceStartIndex : InputUser.s_GlobalState.allUserData[i].deviceStartIndex) > num2)
					{
						if (asLostDevice)
						{
							InputUser.UserData[] allUserData = InputUser.s_GlobalState.allUserData;
							int num3 = i;
							allUserData[num3].lostDeviceStartIndex = allUserData[num3].lostDeviceStartIndex - num;
						}
						else
						{
							InputUser.UserData[] allUserData2 = InputUser.s_GlobalState.allUserData;
							int num4 = i;
							allUserData2[num4].deviceStartIndex = allUserData2[num4].deviceStartIndex - num;
						}
					}
				}
			}
			if (asLostDevice)
			{
				InputUser.s_GlobalState.allUserData[userIndex].lostDeviceStartIndex = InputUser.s_GlobalState.allLostDeviceCount - num;
				ArrayHelpers.AppendWithCapacity<InputDevice>(ref InputUser.s_GlobalState.allLostDevices, ref InputUser.s_GlobalState.allLostDeviceCount, device, 10);
				InputUser.UserData[] allUserData3 = InputUser.s_GlobalState.allUserData;
				allUserData3[userIndex].lostDeviceCount = allUserData3[userIndex].lostDeviceCount + 1;
			}
			else
			{
				InputUser.s_GlobalState.allUserData[userIndex].deviceStartIndex = InputUser.s_GlobalState.allPairedDeviceCount - num;
				ArrayHelpers.AppendWithCapacity<InputDevice>(ref InputUser.s_GlobalState.allPairedDevices, ref InputUser.s_GlobalState.allPairedDeviceCount, device, 10);
				InputUser.UserData[] allUserData4 = InputUser.s_GlobalState.allUserData;
				allUserData4[userIndex].deviceCount = allUserData4[userIndex].deviceCount + 1;
				IInputActionCollection actions = InputUser.s_GlobalState.allUserData[userIndex].actions;
				if (actions != null)
				{
					actions.devices = new ReadOnlyArray<InputDevice>?(InputUser.s_GlobalState.allUsers[userIndex].pairedDevices);
					if (!dontUpdateControlScheme && InputUser.s_GlobalState.allUserData[userIndex].controlScheme != null)
					{
						InputUser.UpdateControlSchemeMatch(userIndex, false);
					}
				}
			}
			InputUser.HookIntoDeviceChange();
			InputUser.Notify(userIndex, asLostDevice ? InputUserChange.DeviceLost : InputUserChange.DevicePaired, device);
		}

		// Token: 0x06000AD3 RID: 2771 RVA: 0x0003A83C File Offset: 0x00038A3C
		private static void RemoveDeviceFromUser(int userIndex, InputDevice device, bool asLostDevice = false)
		{
			int num = (asLostDevice ? InputUser.s_GlobalState.allLostDevices.IndexOfReference(device, InputUser.s_GlobalState.allLostDeviceCount) : InputUser.s_GlobalState.allPairedDevices.IndexOfReference(device, InputUser.s_GlobalState.allUserData[userIndex].deviceStartIndex, InputUser.s_GlobalState.allUserData[userIndex].deviceCount));
			if (num == -1)
			{
				return;
			}
			if (asLostDevice)
			{
				InputUser.s_GlobalState.allLostDevices.EraseAtWithCapacity<InputDevice>(ref InputUser.s_GlobalState.allLostDeviceCount, num);
				InputUser.UserData[] allUserData = InputUser.s_GlobalState.allUserData;
				allUserData[userIndex].lostDeviceCount = allUserData[userIndex].lostDeviceCount - 1;
			}
			else
			{
				InputUser.s_GlobalState.pairingStateVersion = InputUser.s_GlobalState.pairingStateVersion + 1;
				InputUser.s_GlobalState.allPairedDevices.EraseAtWithCapacity<InputDevice>(ref InputUser.s_GlobalState.allPairedDeviceCount, num);
				InputUser.UserData[] allUserData2 = InputUser.s_GlobalState.allUserData;
				allUserData2[userIndex].deviceCount = allUserData2[userIndex].deviceCount - 1;
			}
			for (int i = 0; i < InputUser.s_GlobalState.allUserCount; i++)
			{
				if ((asLostDevice ? InputUser.s_GlobalState.allUserData[i].lostDeviceStartIndex : InputUser.s_GlobalState.allUserData[i].deviceStartIndex) > num)
				{
					if (asLostDevice)
					{
						InputUser.UserData[] allUserData3 = InputUser.s_GlobalState.allUserData;
						int num2 = i;
						allUserData3[num2].lostDeviceStartIndex = allUserData3[num2].lostDeviceStartIndex - 1;
					}
					else
					{
						InputUser.UserData[] allUserData4 = InputUser.s_GlobalState.allUserData;
						int num3 = i;
						allUserData4[num3].deviceStartIndex = allUserData4[num3].deviceStartIndex - 1;
					}
				}
			}
			if (!asLostDevice)
			{
				for (int j = 0; j < InputUser.s_GlobalState.ongoingAccountSelections.length; j++)
				{
					if (InputUser.s_GlobalState.ongoingAccountSelections[j].userId == InputUser.s_GlobalState.allUsers[userIndex].id && InputUser.s_GlobalState.ongoingAccountSelections[j].device == device)
					{
						InputUser.s_GlobalState.ongoingAccountSelections.RemoveAtByMovingTailWithCapacity(j);
						j--;
					}
				}
				IInputActionCollection actions = InputUser.s_GlobalState.allUserData[userIndex].actions;
				if (actions != null)
				{
					actions.devices = new ReadOnlyArray<InputDevice>?(InputUser.s_GlobalState.allUsers[userIndex].pairedDevices);
					if (InputUser.s_GlobalState.allUsers[userIndex].controlScheme != null)
					{
						InputUser.UpdateControlSchemeMatch(userIndex, false);
					}
				}
				InputUser.Notify(userIndex, InputUserChange.DeviceUnpaired, device);
			}
		}

		// Token: 0x06000AD4 RID: 2772 RVA: 0x0003AA84 File Offset: 0x00038C84
		private static void UpdateControlSchemeMatch(int userIndex, bool autoPairMissing = false)
		{
			if (InputUser.s_GlobalState.allUserData[userIndex].controlScheme == null)
			{
				return;
			}
			InputUser.s_GlobalState.allUserData[userIndex].controlSchemeMatch.Dispose();
			InputControlScheme.MatchResult matchResult = default(InputControlScheme.MatchResult);
			try
			{
				InputControlScheme value = InputUser.s_GlobalState.allUserData[userIndex].controlScheme.Value;
				if (value.deviceRequirements.Count > 0)
				{
					InputControlList<InputDevice> inputControlList = new InputControlList<InputDevice>(Allocator.Temp, 0);
					try
					{
						inputControlList.AddSlice<ReadOnlyArray<InputDevice>>(InputUser.s_GlobalState.allUsers[userIndex].pairedDevices, -1, -1, 0);
						if (autoPairMissing)
						{
							int count = inputControlList.Count;
							int unpairedInputDevices = InputUser.GetUnpairedInputDevices(ref inputControlList);
							if (InputUser.s_GlobalState.allUserData[userIndex].platformUserAccountHandle != null)
							{
								inputControlList.Sort<InputUser.CompareDevicesByUserAccount>(count, unpairedInputDevices, new InputUser.CompareDevicesByUserAccount
								{
									platformUserAccountHandle = InputUser.s_GlobalState.allUserData[userIndex].platformUserAccountHandle.Value
								});
							}
						}
						matchResult = value.PickDevicesFrom<InputControlList<InputDevice>>(inputControlList, null);
						if (matchResult.isSuccessfulMatch && autoPairMissing)
						{
							InputUser.s_GlobalState.allUserData[userIndex].controlSchemeMatch = matchResult;
							foreach (InputDevice inputDevice in matchResult.devices)
							{
								if (!InputUser.s_GlobalState.allUsers[userIndex].pairedDevices.ContainsReference(inputDevice))
								{
									InputUser.AddDeviceToUser(userIndex, inputDevice, false, true);
								}
							}
						}
					}
					finally
					{
						inputControlList.Dispose();
					}
				}
				InputUser.s_GlobalState.allUserData[userIndex].controlSchemeMatch = matchResult;
			}
			catch (Exception)
			{
				matchResult.Dispose();
				throw;
			}
		}

		// Token: 0x06000AD5 RID: 2773 RVA: 0x0003AC90 File Offset: 0x00038E90
		private static long UpdatePlatformUserAccount(int userIndex, InputDevice device)
		{
			InputUserAccountHandle? inputUserAccountHandle;
			string text;
			string text2;
			long num = InputUser.QueryPairedPlatformUserAccount(device, out inputUserAccountHandle, out text, out text2);
			if (num == -1L)
			{
				if ((InputUser.s_GlobalState.allUserData[userIndex].flags & InputUser.UserFlags.UserAccountSelectionInProgress) != (InputUser.UserFlags)0)
				{
					InputUser.Notify(userIndex, InputUserChange.AccountSelectionCanceled, null);
				}
				InputUser.s_GlobalState.allUserData[userIndex].platformUserAccountHandle = null;
				InputUser.s_GlobalState.allUserData[userIndex].platformUserAccountName = null;
				InputUser.s_GlobalState.allUserData[userIndex].platformUserAccountId = null;
				return num;
			}
			if ((InputUser.s_GlobalState.allUserData[userIndex].flags & InputUser.UserFlags.UserAccountSelectionInProgress) != (InputUser.UserFlags)0)
			{
				if ((num & 4L) == 0L)
				{
					if ((num & 16L) != 0L)
					{
						InputUser.Notify(userIndex, InputUserChange.AccountSelectionCanceled, device);
					}
					else
					{
						InputUser.UserData[] allUserData = InputUser.s_GlobalState.allUserData;
						allUserData[userIndex].flags = allUserData[userIndex].flags & ~InputUser.UserFlags.UserAccountSelectionInProgress;
						InputUser.s_GlobalState.allUserData[userIndex].platformUserAccountHandle = inputUserAccountHandle;
						InputUser.s_GlobalState.allUserData[userIndex].platformUserAccountName = text;
						InputUser.s_GlobalState.allUserData[userIndex].platformUserAccountId = text2;
						InputUser.Notify(userIndex, InputUserChange.AccountSelectionComplete, device);
					}
				}
			}
			else if (InputUser.s_GlobalState.allUserData[userIndex].platformUserAccountHandle != inputUserAccountHandle || InputUser.s_GlobalState.allUserData[userIndex].platformUserAccountId != text2)
			{
				InputUser.s_GlobalState.allUserData[userIndex].platformUserAccountHandle = inputUserAccountHandle;
				InputUser.s_GlobalState.allUserData[userIndex].platformUserAccountName = text;
				InputUser.s_GlobalState.allUserData[userIndex].platformUserAccountId = text2;
				InputUser.Notify(userIndex, InputUserChange.AccountChanged, device);
			}
			else if (InputUser.s_GlobalState.allUserData[userIndex].platformUserAccountName != text)
			{
				InputUser.Notify(userIndex, InputUserChange.AccountNameChanged, device);
			}
			return num;
		}

		// Token: 0x06000AD6 RID: 2774 RVA: 0x0003AE9C File Offset: 0x0003909C
		private static long QueryPairedPlatformUserAccount(InputDevice device, out InputUserAccountHandle? platformAccountHandle, out string platformAccountName, out string platformAccountId)
		{
			QueryPairedUserAccountCommand queryPairedUserAccountCommand = QueryPairedUserAccountCommand.Create();
			long num = device.ExecuteCommand<QueryPairedUserAccountCommand>(ref queryPairedUserAccountCommand);
			if (num == -1L)
			{
				platformAccountHandle = null;
				platformAccountName = null;
				platformAccountId = null;
				return -1L;
			}
			if ((num & 2L) != 0L)
			{
				platformAccountHandle = new InputUserAccountHandle?(new InputUserAccountHandle(device.description.interfaceName ?? "<Unknown>", queryPairedUserAccountCommand.handle));
				platformAccountName = queryPairedUserAccountCommand.name;
				platformAccountId = queryPairedUserAccountCommand.id;
			}
			else
			{
				platformAccountHandle = null;
				platformAccountName = null;
				platformAccountId = null;
			}
			return num;
		}

		// Token: 0x06000AD7 RID: 2775 RVA: 0x0003AF24 File Offset: 0x00039124
		private static bool InitiateUserAccountSelectionAtPlatformLevel(InputDevice device)
		{
			InitiateUserAccountPairingCommand initiateUserAccountPairingCommand = InitiateUserAccountPairingCommand.Create();
			long num = device.ExecuteCommand<InitiateUserAccountPairingCommand>(ref initiateUserAccountPairingCommand);
			if (num == -2L)
			{
				throw new InvalidOperationException("User pairing already in progress");
			}
			return num == 1L;
		}

		// Token: 0x06000AD8 RID: 2776 RVA: 0x0003AF54 File Offset: 0x00039154
		private static void OnActionChange(object obj, InputActionChange change)
		{
			if (change == InputActionChange.BoundControlsChanged)
			{
				for (int i = 0; i < InputUser.s_GlobalState.allUserCount; i++)
				{
					if (InputUser.s_GlobalState.allUsers[i].actions == obj)
					{
						InputUser.Notify(i, InputUserChange.ControlsChanged, null);
					}
				}
			}
		}

		// Token: 0x06000AD9 RID: 2777 RVA: 0x0003AF9C File Offset: 0x0003919C
		private static void OnDeviceChange(InputDevice device, InputDeviceChange change)
		{
			if (change == InputDeviceChange.Added)
			{
				for (int num = InputUser.FindLostDevice(device, 0); num != -1; num = InputUser.FindLostDevice(device, num))
				{
					int num2 = -1;
					for (int i = 0; i < InputUser.s_GlobalState.allUserCount; i++)
					{
						int lostDeviceStartIndex = InputUser.s_GlobalState.allUserData[i].lostDeviceStartIndex;
						if (lostDeviceStartIndex <= num && num < lostDeviceStartIndex + InputUser.s_GlobalState.allUserData[i].lostDeviceCount)
						{
							num2 = i;
							break;
						}
					}
					InputUser.RemoveDeviceFromUser(num2, InputUser.s_GlobalState.allLostDevices[num], true);
					InputUser.Notify(num2, InputUserChange.DeviceRegained, device);
					InputUser.AddDeviceToUser(num2, device, false, false);
				}
				return;
			}
			if (change == InputDeviceChange.Removed)
			{
				for (int num3 = InputUser.s_GlobalState.allPairedDevices.IndexOfReference(device, InputUser.s_GlobalState.allPairedDeviceCount); num3 != -1; num3 = InputUser.s_GlobalState.allPairedDevices.IndexOfReference(device, InputUser.s_GlobalState.allPairedDeviceCount))
				{
					int num4 = -1;
					for (int j = 0; j < InputUser.s_GlobalState.allUserCount; j++)
					{
						int deviceStartIndex = InputUser.s_GlobalState.allUserData[j].deviceStartIndex;
						if (deviceStartIndex <= num3 && num3 < deviceStartIndex + InputUser.s_GlobalState.allUserData[j].deviceCount)
						{
							num4 = j;
							break;
						}
					}
					InputUser.AddDeviceToUser(num4, device, true, false);
					InputUser.RemoveDeviceFromUser(num4, device, false);
				}
				return;
			}
			if (change != InputDeviceChange.ConfigurationChanged)
			{
				return;
			}
			bool flag = false;
			for (int k = 0; k < InputUser.s_GlobalState.ongoingAccountSelections.length; k++)
			{
				if (InputUser.s_GlobalState.ongoingAccountSelections[k].device == device)
				{
					InputUser inputUser = default(InputUser);
					inputUser.m_Id = InputUser.s_GlobalState.ongoingAccountSelections[k].userId;
					int index = inputUser.index;
					if ((InputUser.UpdatePlatformUserAccount(index, device) & 4L) == 0L)
					{
						flag = true;
						InputUser.s_GlobalState.ongoingAccountSelections.RemoveAtByMovingTailWithCapacity(k);
						k--;
						if (!InputUser.s_GlobalState.allUsers[index].pairedDevices.ContainsReference(device))
						{
							InputUser.AddDeviceToUser(index, device, false, false);
						}
					}
				}
			}
			if (!flag)
			{
				int num7;
				for (int num5 = InputUser.s_GlobalState.allPairedDevices.IndexOfReference(device, InputUser.s_GlobalState.allPairedDeviceCount); num5 != -1; num5 = InputUser.s_GlobalState.allPairedDevices.IndexOfReference(device, num7, InputUser.s_GlobalState.allPairedDeviceCount - num7))
				{
					int num6 = -1;
					for (int l = 0; l < InputUser.s_GlobalState.allUserCount; l++)
					{
						int deviceStartIndex2 = InputUser.s_GlobalState.allUserData[l].deviceStartIndex;
						if (deviceStartIndex2 <= num5 && num5 < deviceStartIndex2 + InputUser.s_GlobalState.allUserData[l].deviceCount)
						{
							num6 = l;
							break;
						}
					}
					InputUser.UpdatePlatformUserAccount(num6, device);
					num7 = num5 + Math.Max(1, InputUser.s_GlobalState.allUserData[num6].deviceCount);
				}
			}
		}

		// Token: 0x06000ADA RID: 2778 RVA: 0x0003B294 File Offset: 0x00039494
		private static int FindLostDevice(InputDevice device, int startIndex = 0)
		{
			int deviceId = device.deviceId;
			for (int i = startIndex; i < InputUser.s_GlobalState.allLostDeviceCount; i++)
			{
				InputDevice inputDevice = InputUser.s_GlobalState.allLostDevices[i];
				if (device == inputDevice || inputDevice.deviceId == deviceId)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06000ADB RID: 2779 RVA: 0x0003B2DC File Offset: 0x000394DC
		private static void OnEvent(InputEventPtr eventPtr, InputDevice device)
		{
			if (InputUser.s_GlobalState.listenForUnpairedDeviceActivity == 0)
			{
				return;
			}
			FourCC type = eventPtr.type;
			if (type != 1398030676 && type != 1145852993)
			{
				return;
			}
			if (!device.enabled)
			{
				return;
			}
			if (InputUser.s_GlobalState.allPairedDevices.ContainsReference(InputUser.s_GlobalState.allPairedDeviceCount, device))
			{
				return;
			}
			if (!DelegateHelpers.InvokeCallbacksSafe_AnyCallbackReturnsTrue<InputDevice, InputEventPtr>(ref InputUser.s_GlobalState.onPreFilterUnpairedDeviceUsed, device, eventPtr, "InputUser.onPreFilterUnpairedDeviceActivity", null))
			{
				return;
			}
			foreach (InputControl inputControl in eventPtr.EnumerateChangedControls(device, 0.0001f))
			{
				bool flag = false;
				InputUser.s_GlobalState.onUnpairedDeviceUsed.LockForChanges();
				for (int i = 0; i < InputUser.s_GlobalState.onUnpairedDeviceUsed.length; i++)
				{
					int pairingStateVersion = InputUser.s_GlobalState.pairingStateVersion;
					try
					{
						InputUser.s_GlobalState.onUnpairedDeviceUsed[i](inputControl, eventPtr);
					}
					catch (Exception ex)
					{
						Debug.LogError(ex.GetType().Name + " while executing 'InputUser.onUnpairedDeviceUsed' callbacks");
						Debug.LogException(ex);
					}
					if (pairingStateVersion != InputUser.s_GlobalState.pairingStateVersion && InputUser.FindUserPairedToDevice(device) != null)
					{
						flag = true;
						break;
					}
				}
				InputUser.s_GlobalState.onUnpairedDeviceUsed.UnlockForChanges();
				if (flag)
				{
					break;
				}
			}
		}

		// Token: 0x06000ADC RID: 2780 RVA: 0x0003B470 File Offset: 0x00039670
		internal static ISavedState SaveAndResetState()
		{
			ISavedState savedState = new SavedStructState<InputUser.GlobalState>(ref InputUser.s_GlobalState, delegate(ref InputUser.GlobalState state)
			{
				InputUser.s_GlobalState = state;
			}, delegate
			{
				InputUser.DisposeAndResetGlobalState();
			});
			InputUser.s_GlobalState = default(InputUser.GlobalState);
			return savedState;
		}

		// Token: 0x06000ADD RID: 2781 RVA: 0x0003B4D0 File Offset: 0x000396D0
		private static void HookIntoActionChange()
		{
			if (InputUser.s_GlobalState.onActionChangeHooked)
			{
				return;
			}
			if (InputUser.s_GlobalState.actionChangeDelegate == null)
			{
				InputUser.s_GlobalState.actionChangeDelegate = new Action<object, InputActionChange>(InputUser.OnActionChange);
			}
			InputSystem.onActionChange += InputUser.OnActionChange;
			InputUser.s_GlobalState.onActionChangeHooked = true;
		}

		// Token: 0x06000ADE RID: 2782 RVA: 0x0003B528 File Offset: 0x00039728
		private static void UnhookFromActionChange()
		{
			if (!InputUser.s_GlobalState.onActionChangeHooked)
			{
				return;
			}
			InputSystem.onActionChange -= InputUser.OnActionChange;
			InputUser.s_GlobalState.onActionChangeHooked = false;
		}

		// Token: 0x06000ADF RID: 2783 RVA: 0x0003B554 File Offset: 0x00039754
		private static void HookIntoDeviceChange()
		{
			if (InputUser.s_GlobalState.onDeviceChangeHooked)
			{
				return;
			}
			if (InputUser.s_GlobalState.onDeviceChangeDelegate == null)
			{
				InputUser.s_GlobalState.onDeviceChangeDelegate = new Action<InputDevice, InputDeviceChange>(InputUser.OnDeviceChange);
			}
			InputSystem.onDeviceChange += InputUser.s_GlobalState.onDeviceChangeDelegate;
			InputUser.s_GlobalState.onDeviceChangeHooked = true;
		}

		// Token: 0x06000AE0 RID: 2784 RVA: 0x0003B5AA File Offset: 0x000397AA
		private static void UnhookFromDeviceChange()
		{
			if (!InputUser.s_GlobalState.onDeviceChangeHooked)
			{
				return;
			}
			InputSystem.onDeviceChange -= InputUser.s_GlobalState.onDeviceChangeDelegate;
			InputUser.s_GlobalState.onDeviceChangeHooked = false;
		}

		// Token: 0x06000AE1 RID: 2785 RVA: 0x0003B5D4 File Offset: 0x000397D4
		private static void HookIntoEvents()
		{
			if (InputUser.s_GlobalState.onEventHooked)
			{
				return;
			}
			if (InputUser.s_GlobalState.onEventDelegate == null)
			{
				InputUser.s_GlobalState.onEventDelegate = new Action<InputEventPtr, InputDevice>(InputUser.OnEvent);
			}
			InputSystem.onEvent += InputUser.s_GlobalState.onEventDelegate;
			InputUser.s_GlobalState.onEventHooked = true;
		}

		// Token: 0x06000AE2 RID: 2786 RVA: 0x0003B634 File Offset: 0x00039834
		private static void UnhookFromDeviceStateChange()
		{
			if (!InputUser.s_GlobalState.onEventHooked)
			{
				return;
			}
			InputSystem.onEvent -= InputUser.s_GlobalState.onEventDelegate;
			InputUser.s_GlobalState.onEventHooked = false;
		}

		// Token: 0x06000AE3 RID: 2787 RVA: 0x0003B668 File Offset: 0x00039868
		private static void DisposeAndResetGlobalState()
		{
			for (int i = 0; i < InputUser.s_GlobalState.allUserCount; i++)
			{
				InputUser.s_GlobalState.allUserData[i].controlSchemeMatch.Dispose();
			}
			uint lastUserId = InputUser.s_GlobalState.lastUserId;
			InputUser.s_GlobalState = default(InputUser.GlobalState);
			InputUser.s_GlobalState.lastUserId = lastUserId;
		}

		// Token: 0x06000AE4 RID: 2788 RVA: 0x0003B6C5 File Offset: 0x000398C5
		internal static void ResetGlobals()
		{
			InputUser.UnhookFromActionChange();
			InputUser.UnhookFromDeviceChange();
			InputUser.UnhookFromDeviceStateChange();
			InputUser.DisposeAndResetGlobalState();
		}

		// Token: 0x040003C2 RID: 962
		public const uint InvalidId = 0U;

		// Token: 0x040003C3 RID: 963
		private static readonly ProfilerMarker k_InputUserOnChangeMarker = new ProfilerMarker("InputUser.onChange");

		// Token: 0x040003C4 RID: 964
		private static readonly ProfilerMarker k_InputCheckForUnpairMarker = new ProfilerMarker("InputCheckForUnpairedDeviceActivity");

		// Token: 0x040003C5 RID: 965
		private uint m_Id;

		// Token: 0x040003C6 RID: 966
		private static InputUser.GlobalState s_GlobalState;

		// Token: 0x020001CF RID: 463
		public struct ControlSchemeChangeSyntax
		{
			// Token: 0x060014A3 RID: 5283 RVA: 0x0005FE78 File Offset: 0x0005E078
			public InputUser.ControlSchemeChangeSyntax AndPairRemainingDevices()
			{
				InputUser.UpdateControlSchemeMatch(this.m_UserIndex, true);
				return this;
			}

			// Token: 0x0400099A RID: 2458
			internal int m_UserIndex;
		}

		// Token: 0x020001D0 RID: 464
		[Flags]
		internal enum UserFlags
		{
			// Token: 0x0400099C RID: 2460
			BindToAllDevices = 1,
			// Token: 0x0400099D RID: 2461
			UserAccountSelectionInProgress = 2
		}

		// Token: 0x020001D1 RID: 465
		private struct UserData
		{
			// Token: 0x0400099E RID: 2462
			public InputUserAccountHandle? platformUserAccountHandle;

			// Token: 0x0400099F RID: 2463
			public string platformUserAccountName;

			// Token: 0x040009A0 RID: 2464
			public string platformUserAccountId;

			// Token: 0x040009A1 RID: 2465
			public int deviceCount;

			// Token: 0x040009A2 RID: 2466
			public int deviceStartIndex;

			// Token: 0x040009A3 RID: 2467
			public IInputActionCollection actions;

			// Token: 0x040009A4 RID: 2468
			public InputControlScheme? controlScheme;

			// Token: 0x040009A5 RID: 2469
			public InputControlScheme.MatchResult controlSchemeMatch;

			// Token: 0x040009A6 RID: 2470
			public int lostDeviceCount;

			// Token: 0x040009A7 RID: 2471
			public int lostDeviceStartIndex;

			// Token: 0x040009A8 RID: 2472
			public InputUser.UserFlags flags;
		}

		// Token: 0x020001D2 RID: 466
		private struct CompareDevicesByUserAccount : IComparer<InputDevice>
		{
			// Token: 0x060014A4 RID: 5284 RVA: 0x0005FE8C File Offset: 0x0005E08C
			public int Compare(InputDevice x, InputDevice y)
			{
				InputUserAccountHandle? userAccountHandleForDevice = InputUser.CompareDevicesByUserAccount.GetUserAccountHandleForDevice(x);
				InputUserAccountHandle? userAccountHandleForDevice2 = InputUser.CompareDevicesByUserAccount.GetUserAccountHandleForDevice(x);
				InputUserAccountHandle? inputUserAccountHandle = userAccountHandleForDevice;
				InputUserAccountHandle inputUserAccountHandle2 = this.platformUserAccountHandle;
				if (inputUserAccountHandle != null && (inputUserAccountHandle == null || inputUserAccountHandle.GetValueOrDefault() == inputUserAccountHandle2))
				{
					inputUserAccountHandle = userAccountHandleForDevice2;
					inputUserAccountHandle2 = this.platformUserAccountHandle;
					if (inputUserAccountHandle != null && (inputUserAccountHandle == null || inputUserAccountHandle.GetValueOrDefault() == inputUserAccountHandle2))
					{
						return 0;
					}
				}
				inputUserAccountHandle = userAccountHandleForDevice;
				inputUserAccountHandle2 = this.platformUserAccountHandle;
				if (inputUserAccountHandle != null && (inputUserAccountHandle == null || inputUserAccountHandle.GetValueOrDefault() == inputUserAccountHandle2))
				{
					return -1;
				}
				inputUserAccountHandle = userAccountHandleForDevice2;
				inputUserAccountHandle2 = this.platformUserAccountHandle;
				if (inputUserAccountHandle != null && (inputUserAccountHandle == null || inputUserAccountHandle.GetValueOrDefault() == inputUserAccountHandle2))
				{
					return 1;
				}
				return 0;
			}

			// Token: 0x060014A5 RID: 5285 RVA: 0x0005FF70 File Offset: 0x0005E170
			private static InputUserAccountHandle? GetUserAccountHandleForDevice(InputDevice device)
			{
				return null;
			}

			// Token: 0x040009A9 RID: 2473
			public InputUserAccountHandle platformUserAccountHandle;
		}

		// Token: 0x020001D3 RID: 467
		private struct OngoingAccountSelection
		{
			// Token: 0x040009AA RID: 2474
			public InputDevice device;

			// Token: 0x040009AB RID: 2475
			public uint userId;
		}

		// Token: 0x020001D4 RID: 468
		private struct GlobalState
		{
			// Token: 0x040009AC RID: 2476
			internal int pairingStateVersion;

			// Token: 0x040009AD RID: 2477
			internal uint lastUserId;

			// Token: 0x040009AE RID: 2478
			internal int allUserCount;

			// Token: 0x040009AF RID: 2479
			internal int allPairedDeviceCount;

			// Token: 0x040009B0 RID: 2480
			internal int allLostDeviceCount;

			// Token: 0x040009B1 RID: 2481
			internal InputUser[] allUsers;

			// Token: 0x040009B2 RID: 2482
			internal InputUser.UserData[] allUserData;

			// Token: 0x040009B3 RID: 2483
			internal InputDevice[] allPairedDevices;

			// Token: 0x040009B4 RID: 2484
			internal InputDevice[] allLostDevices;

			// Token: 0x040009B5 RID: 2485
			internal InlinedArray<InputUser.OngoingAccountSelection> ongoingAccountSelections;

			// Token: 0x040009B6 RID: 2486
			internal CallbackArray<Action<InputUser, InputUserChange, InputDevice>> onChange;

			// Token: 0x040009B7 RID: 2487
			internal CallbackArray<Action<InputControl, InputEventPtr>> onUnpairedDeviceUsed;

			// Token: 0x040009B8 RID: 2488
			internal CallbackArray<Func<InputDevice, InputEventPtr, bool>> onPreFilterUnpairedDeviceUsed;

			// Token: 0x040009B9 RID: 2489
			internal Action<object, InputActionChange> actionChangeDelegate;

			// Token: 0x040009BA RID: 2490
			internal Action<InputDevice, InputDeviceChange> onDeviceChangeDelegate;

			// Token: 0x040009BB RID: 2491
			internal Action<InputEventPtr, InputDevice> onEventDelegate;

			// Token: 0x040009BC RID: 2492
			internal bool onActionChangeHooked;

			// Token: 0x040009BD RID: 2493
			internal bool onDeviceChangeHooked;

			// Token: 0x040009BE RID: 2494
			internal bool onEventHooked;

			// Token: 0x040009BF RID: 2495
			internal int listenForUnpairedDeviceActivity;
		}
	}
}
