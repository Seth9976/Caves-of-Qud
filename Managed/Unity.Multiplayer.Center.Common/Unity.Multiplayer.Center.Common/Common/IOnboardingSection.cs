using System;
using UnityEngine.UIElements;

namespace Unity.Multiplayer.Center.Common
{
	// Token: 0x0200000A RID: 10
	public interface IOnboardingSection
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000014 RID: 20
		VisualElement Root { get; }

		// Token: 0x06000015 RID: 21
		void Load();

		// Token: 0x06000016 RID: 22
		void Unload();
	}
}
