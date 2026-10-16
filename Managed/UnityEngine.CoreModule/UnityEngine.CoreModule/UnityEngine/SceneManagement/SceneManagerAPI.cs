using System;

namespace UnityEngine.SceneManagement
{
	// Token: 0x0200037E RID: 894
	public class SceneManagerAPI
	{
		// Token: 0x17000682 RID: 1666
		// (get) Token: 0x06002918 RID: 10520 RVA: 0x0004F448 File Offset: 0x0004D648
		internal static SceneManagerAPI ActiveAPI
		{
			get
			{
				return SceneManagerAPI.overrideAPI ?? SceneManagerAPI.s_DefaultAPI;
			}
		}

		// Token: 0x17000683 RID: 1667
		// (get) Token: 0x06002919 RID: 10521 RVA: 0x0004F458 File Offset: 0x0004D658
		// (set) Token: 0x0600291A RID: 10522 RVA: 0x0004F45F File Offset: 0x0004D65F
		public static SceneManagerAPI overrideAPI { get; set; }

		// Token: 0x0600291B RID: 10523 RVA: 0x00030B93 File Offset: 0x0002ED93
		protected internal SceneManagerAPI()
		{
		}

		// Token: 0x0600291C RID: 10524 RVA: 0x0004F467 File Offset: 0x0004D667
		protected internal virtual int GetNumScenesInBuildSettings()
		{
			return SceneManagerAPIInternal.GetNumScenesInBuildSettings();
		}

		// Token: 0x0600291D RID: 10525 RVA: 0x0004F46E File Offset: 0x0004D66E
		protected internal virtual Scene GetSceneByBuildIndex(int buildIndex)
		{
			return SceneManagerAPIInternal.GetSceneByBuildIndex(buildIndex);
		}

		// Token: 0x0600291E RID: 10526 RVA: 0x0004F476 File Offset: 0x0004D676
		protected internal virtual AsyncOperation LoadSceneAsyncByNameOrIndex(string sceneName, int sceneBuildIndex, LoadSceneParameters parameters, bool mustCompleteNextFrame)
		{
			return SceneManagerAPIInternal.LoadSceneAsyncNameIndexInternal(sceneName, sceneBuildIndex, parameters, mustCompleteNextFrame);
		}

		// Token: 0x0600291F RID: 10527 RVA: 0x0004F482 File Offset: 0x0004D682
		protected internal virtual AsyncOperation UnloadSceneAsyncByNameOrIndex(string sceneName, int sceneBuildIndex, bool immediately, UnloadSceneOptions options, out bool outSuccess)
		{
			return SceneManagerAPIInternal.UnloadSceneNameIndexInternal(sceneName, sceneBuildIndex, immediately, options, out outSuccess);
		}

		// Token: 0x06002920 RID: 10528 RVA: 0x0003CDCD File Offset: 0x0003AFCD
		protected internal virtual AsyncOperation LoadFirstScene(bool mustLoadAsync)
		{
			return null;
		}

		// Token: 0x04000BA4 RID: 2980
		private static SceneManagerAPI s_DefaultAPI = new SceneManagerAPI();
	}
}
