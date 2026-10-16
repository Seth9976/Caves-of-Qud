using System;
using UnityEngine;

namespace LaundryBear.PlatformServices
{
	// Token: 0x0200001E RID: 30
	public class PlatformFeatureGameObjectSwitcher : MonoBehaviour
	{
		// Token: 0x0600005A RID: 90 RVA: 0x000023F4 File Offset: 0x000005F4
		private void Awake()
		{
			ServiceLocator.TryGetService<IPlatform>(out this.m_platform);
			ServiceLocator.TryGetService<IKeyboard>(out this.m_keyboard);
			IUserService userService;
			this.m_supportsUsers = ServiceLocator.TryGetService<IUserService>(out userService);
			IAchievementService achievementService;
			this.m_supportsAchievements = ServiceLocator.TryGetService<IAchievementService>(out achievementService);
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00002434 File Offset: 0x00000634
		private void OnEnable()
		{
			switch (this.m_enabledForFeatures)
			{
			case PlatformFeature.CanQuit:
				this.SetEnabled(this.m_platform.SupportsQuit);
				return;
			case PlatformFeature.CanModifyWindowSettings:
				this.SetEnabled(this.m_platform.AllowsUserWindowModification);
				return;
			case PlatformFeature.SupportsSystemUsers:
				this.SetEnabled(this.m_supportsUsers);
				return;
			case PlatformFeature.SupportsSystemAchievements:
				this.SetEnabled(this.m_supportsAchievements);
				break;
			case PlatformFeature.SupportsOnScreenKeyboard:
				break;
			default:
				return;
			}
		}

		// Token: 0x0600005C RID: 92 RVA: 0x000024A0 File Offset: 0x000006A0
		private void SetEnabled(bool value)
		{
			base.gameObject.SetActive(value);
		}

		// Token: 0x0600005D RID: 93 RVA: 0x000024B0 File Offset: 0x000006B0
		public static bool CurrentPlatformSupportsFeature(PlatformFeature feature)
		{
			switch (feature)
			{
			case PlatformFeature.CanQuit:
			{
				IPlatform platform;
				ServiceLocator.TryGetService<IPlatform>(out platform);
				return platform.SupportsQuit;
			}
			case PlatformFeature.CanModifyWindowSettings:
			{
				IPlatform platform2;
				ServiceLocator.TryGetService<IPlatform>(out platform2);
				return platform2.AllowsUserWindowModification;
			}
			case PlatformFeature.SupportsSystemUsers:
			{
				IUserService userService;
				return ServiceLocator.TryGetService<IUserService>(out userService);
			}
			case PlatformFeature.SupportsSystemAchievements:
			{
				IAchievementService achievementService;
				return ServiceLocator.TryGetService<IAchievementService>(out achievementService);
			}
			case PlatformFeature.SupportsOnScreenKeyboard:
			{
				IKeyboard keyboard;
				return ServiceLocator.TryGetService<IKeyboard>(out keyboard);
			}
			case PlatformFeature.SupportsMouse:
				return true;
			case PlatformFeature.SupportsHardwareKeyboard:
				return true;
			default:
				return false;
			}
		}

		// Token: 0x04000033 RID: 51
		[SerializeField]
		private PlatformFeature m_enabledForFeatures;

		// Token: 0x04000034 RID: 52
		private IPlatform m_platform;

		// Token: 0x04000035 RID: 53
		private IKeyboard m_keyboard;

		// Token: 0x04000036 RID: 54
		private bool m_supportsUsers;

		// Token: 0x04000037 RID: 55
		private bool m_supportsAchievements;
	}
}
