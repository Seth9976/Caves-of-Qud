using System;
using UnityEngine;
using XRL.UI;

// Token: 0x0200006C RID: 108
public class PlayerInputManager : MonoBehaviour
{
	// Token: 0x17000063 RID: 99
	// (get) Token: 0x060003B0 RID: 944 RVA: 0x00017DB5 File Offset: 0x00015FB5
	public static PlayerInputManager instance
	{
		get
		{
			GameManager instance = GameManager.Instance;
			if (instance == null)
			{
				return null;
			}
			return instance.player;
		}
	}

	// Token: 0x060003B1 RID: 945 RVA: 0x00017DC7 File Offset: 0x00015FC7
	public bool GetButton(string id, bool forceEnable = false)
	{
		id = ControlManager.mapRewiredIDToLegacyID(id);
		return PlayerInputManager.ready && CommandBindingManager.CommandBindings[id].IsPressed(forceEnable);
	}

	// Token: 0x060003B2 RID: 946 RVA: 0x00017DEB File Offset: 0x00015FEB
	public bool GetButtonPerformedThisFrame(string id, bool ignoreSkipframes = false)
	{
		return PlayerInputManager.ready && ControlManager.isCommandPerformedThisFrame(id);
	}

	// Token: 0x060003B3 RID: 947 RVA: 0x00017DFC File Offset: 0x00015FFC
	public bool GetButtonReleasedThisFrame(string id, bool ignoreSkipframes = false)
	{
		return PlayerInputManager.ready && ControlManager.isCommandReleasedThisFrame(id);
	}

	// Token: 0x060003B4 RID: 948 RVA: 0x00017E0D File Offset: 0x0001600D
	public bool GetButtonPressedThisFrame(string id, bool ignoreSkipframes = false)
	{
		return PlayerInputManager.ready && ControlManager.isCommandPressedThisFrame(id);
	}

	// Token: 0x060003B5 RID: 949 RVA: 0x00017E1E File Offset: 0x0001601E
	public bool GetButtonDown(string id, bool ignoreSkipframes = false, bool skipTutorialCheck = false)
	{
		return PlayerInputManager.ready && ControlManager.isCommandDown(id, false, ignoreSkipframes, skipTutorialCheck);
	}

	// Token: 0x060003B6 RID: 950 RVA: 0x00017E32 File Offset: 0x00016032
	public bool GetButtonDownRepeating(string id, bool ignoreSkipframes = false)
	{
		return PlayerInputManager.ready && ControlManager.isCommandDown(id, true, ignoreSkipframes, false);
	}

	// Token: 0x060003B7 RID: 951 RVA: 0x00017E46 File Offset: 0x00016046
	public bool GetNegativeButtonDownRepeating(string id, bool ignoreSkipframes = false)
	{
		return PlayerInputManager.ready && ControlManager.isCommandDown(id, true, ignoreSkipframes, false);
	}

	// Token: 0x060003B8 RID: 952 RVA: 0x00017E5A File Offset: 0x0001605A
	public float GetAxis(string id)
	{
		id = ControlManager.mapRewiredIDToLegacyID(id);
		if (!PlayerInputManager.ready)
		{
			return 0f;
		}
		return CommandBindingManager.CommandBindings[id].ReadValue<float>();
	}

	// Token: 0x060003B9 RID: 953 RVA: 0x00017E81 File Offset: 0x00016081
	public Vector2 GetAxis2(string id)
	{
		id = ControlManager.mapRewiredIDToLegacyID(id);
		if (!PlayerInputManager.ready)
		{
			return Vector2.zero;
		}
		return CommandBindingManager.CommandBindings[id].ReadValue<Vector2>();
	}

	// Token: 0x040002D1 RID: 721
	public static bool ready;
}
