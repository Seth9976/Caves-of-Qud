using System;
using UnityEngine;

namespace LaundryBear.PlatformServices
{
	// Token: 0x0200001F RID: 31
	public class PlatformGameObjectSwitcher : MonoBehaviour
	{
		// Token: 0x0600005F RID: 95 RVA: 0x00002524 File Offset: 0x00000724
		private void OnEnable()
		{
			if ((Utilities.GetCurrentPlatform() & this.m_enabledForPlatforms) > Platform.None)
			{
				base.gameObject.SetActive(true);
				return;
			}
			base.gameObject.SetActive(false);
		}

		// Token: 0x04000038 RID: 56
		[SerializeField]
		private Platform m_enabledForPlatforms;
	}
}
