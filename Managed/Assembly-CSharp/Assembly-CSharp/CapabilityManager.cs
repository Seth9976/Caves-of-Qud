using System;
using System.Collections.Generic;
using LaundryBear.PlatformServices;
using UnityEngine;
using XRL.UI;

// Token: 0x0200003F RID: 63
public class CapabilityManager : MonoBehaviour
{
	// Token: 0x1700003E RID: 62
	// (get) Token: 0x0600021D RID: 541 RVA: 0x0000AE4C File Offset: 0x0000904C
	public static GameManager gameManager
	{
		get
		{
			return GameManager.Instance;
		}
	}

	// Token: 0x1700003F RID: 63
	// (get) Token: 0x0600021E RID: 542 RVA: 0x0000AE53 File Offset: 0x00009053
	public static bool AllowClipboardFunctionality
	{
		get
		{
			return true;
		}
	}

	// Token: 0x17000040 RID: 64
	// (get) Token: 0x0600021F RID: 543 RVA: 0x0000AE56 File Offset: 0x00009056
	public static bool AllowKeyboardHotkeys
	{
		get
		{
			return true;
		}
	}

	// Token: 0x06000220 RID: 544 RVA: 0x0000AE59 File Offset: 0x00009059
	public static void SuggestOnscreenKeyboard()
	{
		if (Options.GetOption("OptionFloatingKeyboard", "") != "Yes")
		{
			return;
		}
		if (PlatformManager.GetControlType() == PlatformControlType.Deck)
		{
			PlatformManager.TryShowVirtualKeyboard(VirtualKeyboardType.SingleLine, 0, 0, 80, 1);
			return;
		}
	}

	// Token: 0x06000221 RID: 545 RVA: 0x0000AE8C File Offset: 0x0000908C
	public static CapabilityManager.CapabilityType CapabilityEnumFromString(string capability)
	{
		if (CapabilityManager.StringToCapability == null)
		{
			CapabilityManager.StringToCapability = new Dictionary<string, CapabilityManager.CapabilityType>();
			foreach (object obj in Enum.GetValues(typeof(CapabilityManager.CapabilityType)))
			{
				CapabilityManager.CapabilityType capabilityType = (CapabilityManager.CapabilityType)obj;
				CapabilityManager.StringToCapability.Add(capabilityType.ToString(), capabilityType);
			}
		}
		CapabilityManager.CapabilityType capabilityType2;
		if (CapabilityManager.StringToCapability.TryGetValue(capability, out capabilityType2))
		{
			return capabilityType2;
		}
		return CapabilityManager.CapabilityType.Unknown;
	}

	// Token: 0x06000222 RID: 546 RVA: 0x0000AF24 File Offset: 0x00009124
	public static bool HasCapability(string capability)
	{
		return CapabilityManager.HasCapability(new CapabilityManager.CapabilityType?(CapabilityManager.CapabilityEnumFromString(capability)));
	}

	// Token: 0x06000223 RID: 547 RVA: 0x0000AF38 File Offset: 0x00009138
	public static bool HasCapability(CapabilityManager.CapabilityType? capability)
	{
		if (capability != null)
		{
			switch (capability.GetValueOrDefault())
			{
			case CapabilityManager.CapabilityType.OnscreenKeyboard:
				return PlatformFeatureGameObjectSwitcher.CurrentPlatformSupportsFeature(PlatformFeature.SupportsOnScreenKeyboard);
			case CapabilityManager.CapabilityType.CanQuit:
				return PlatformFeatureGameObjectSwitcher.CurrentPlatformSupportsFeature(PlatformFeature.CanQuit);
			case CapabilityManager.CapabilityType.CanModifyWindowSettings:
				return PlatformFeatureGameObjectSwitcher.CurrentPlatformSupportsFeature(PlatformFeature.CanModifyWindowSettings);
			case CapabilityManager.CapabilityType.SupportsSystemUsers:
				return PlatformFeatureGameObjectSwitcher.CurrentPlatformSupportsFeature(PlatformFeature.SupportsSystemUsers);
			case CapabilityManager.CapabilityType.SupportsSystemAchievements:
				return PlatformFeatureGameObjectSwitcher.CurrentPlatformSupportsFeature(PlatformFeature.SupportsSystemAchievements);
			case CapabilityManager.CapabilityType.IsDesktop:
			case CapabilityManager.CapabilityType.Mods:
			case CapabilityManager.CapabilityType.Analytics:
			case CapabilityManager.CapabilityType.SteamInput:
			case CapabilityManager.CapabilityType.UnlimitedStorage:
				return CapabilityManager.GetStandaloneOnlyCapability(capability);
			case CapabilityManager.CapabilityType.IsSwitch:
			case CapabilityManager.CapabilityType.LimitedWriteFrequency:
			case CapabilityManager.CapabilityType.RequiresStorageExpansion:
				return CapabilityManager.GetSwitchOnlyCapability(capability);
			case CapabilityManager.CapabilityType.Mouse:
				return PlatformFeatureGameObjectSwitcher.CurrentPlatformSupportsFeature(PlatformFeature.SupportsMouse);
			case CapabilityManager.CapabilityType.HardwareKeyboard:
				return PlatformFeatureGameObjectSwitcher.CurrentPlatformSupportsFeature(PlatformFeature.SupportsHardwareKeyboard);
			}
			return false;
		}
		return true;
	}

	// Token: 0x06000224 RID: 548 RVA: 0x0000AFE0 File Offset: 0x000091E0
	private static bool GetStandaloneOnlyCapability(CapabilityManager.CapabilityType? capability)
	{
		CapabilityManager.CapabilityType? capabilityType = capability;
		CapabilityManager.CapabilityType capabilityType2 = CapabilityManager.CapabilityType.OnscreenKeyboard;
		return !((capabilityType.GetValueOrDefault() == capabilityType2) & (capabilityType != null)) || PlatformManager.GetControlType() == PlatformControlType.Deck;
	}

	// Token: 0x06000225 RID: 549 RVA: 0x0000B00F File Offset: 0x0000920F
	private static bool GetSwitchOnlyCapability(CapabilityManager.CapabilityType? capability)
	{
		return false;
	}

	// Token: 0x06000226 RID: 550 RVA: 0x0000B012 File Offset: 0x00009212
	public static CapabilityManager.PlatformClassification CurrentPlatformClassification()
	{
		return CapabilityManager.PlatformClassification.PC;
	}

	// Token: 0x06000227 RID: 551 RVA: 0x0000B015 File Offset: 0x00009215
	private void Awake()
	{
	}

	// Token: 0x06000228 RID: 552 RVA: 0x0000B017 File Offset: 0x00009217
	public string GetDefaultOptionOverrideForCapabilities(string Option, string Default)
	{
		return PlatformManager.GetOptionDefault(Option) ?? Default;
	}

	// Token: 0x06000229 RID: 553 RVA: 0x0000B024 File Offset: 0x00009224
	public void Init()
	{
		CapabilityManager.instance = this;
	}

	// Token: 0x04000122 RID: 290
	public static CapabilityManager instance;

	// Token: 0x04000123 RID: 291
	private static Dictionary<string, CapabilityManager.CapabilityType> StringToCapability;

	// Token: 0x02001565 RID: 5477
	public enum CapabilityType
	{
		// Token: 0x04005627 RID: 22055
		Unknown,
		// Token: 0x04005628 RID: 22056
		OnscreenKeyboard,
		// Token: 0x04005629 RID: 22057
		CanQuit,
		// Token: 0x0400562A RID: 22058
		CanModifyWindowSettings,
		// Token: 0x0400562B RID: 22059
		SupportsSystemUsers,
		// Token: 0x0400562C RID: 22060
		SupportsSystemAchievements,
		// Token: 0x0400562D RID: 22061
		IsDesktop,
		// Token: 0x0400562E RID: 22062
		IsSwitch,
		// Token: 0x0400562F RID: 22063
		Mouse,
		// Token: 0x04005630 RID: 22064
		HardwareKeyboard,
		// Token: 0x04005631 RID: 22065
		Mods,
		// Token: 0x04005632 RID: 22066
		Analytics,
		// Token: 0x04005633 RID: 22067
		SteamInput,
		// Token: 0x04005634 RID: 22068
		UnlimitedStorage,
		// Token: 0x04005635 RID: 22069
		LimitedWriteFrequency,
		// Token: 0x04005636 RID: 22070
		RequiresStorageExpansion
	}

	// Token: 0x02001566 RID: 5478
	public enum PlatformClassification
	{
		// Token: 0x04005638 RID: 22072
		PC,
		// Token: 0x04005639 RID: 22073
		Console
	}
}
