using System;

namespace UnityEngine.Timeline
{
	// Token: 0x0200002D RID: 45
	[AssetFileNameExtension("signal", new string[] { })]
	public class SignalAsset : ScriptableObject
	{
		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000248 RID: 584 RVA: 0x000087F0 File Offset: 0x000069F0
		// (remove) Token: 0x06000249 RID: 585 RVA: 0x00008824 File Offset: 0x00006A24
		internal static event Action<SignalAsset> OnEnableCallback;

		// Token: 0x0600024A RID: 586 RVA: 0x00008857 File Offset: 0x00006A57
		private void OnEnable()
		{
			if (SignalAsset.OnEnableCallback != null)
			{
				SignalAsset.OnEnableCallback(this);
			}
		}
	}
}
