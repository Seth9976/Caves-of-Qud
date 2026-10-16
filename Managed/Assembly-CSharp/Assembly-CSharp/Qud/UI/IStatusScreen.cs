using System;
using ConsoleLib.Console;
using XRL.UI.Framework;
using XRL.World;

namespace Qud.UI
{
	// Token: 0x02000479 RID: 1145
	public interface IStatusScreen
	{
		// Token: 0x06002BB0 RID: 11184
		string GetNavigationCategory();

		// Token: 0x06002BB1 RID: 11185
		IRenderable GetTabIcon();

		// Token: 0x06002BB2 RID: 11186
		string GetTabString();

		// Token: 0x06002BB3 RID: 11187
		bool WantsCategoryBar();

		// Token: 0x06002BB4 RID: 11188
		NavigationContext ShowScreen(GameObject GO, StatusScreensScreen parent);

		// Token: 0x06002BB5 RID: 11189
		void HideScreen();

		// Token: 0x06002BB6 RID: 11190
		bool Exit();

		// Token: 0x06002BB7 RID: 11191
		void CheckLayoutFrame();

		// Token: 0x06002BB8 RID: 11192
		void ResetLayoutFrame(int Frames);

		// Token: 0x06002BB9 RID: 11193
		void PrepareLayoutFrame();

		// Token: 0x06002BBA RID: 11194
		void FilterUpdated(string filterText);

		// Token: 0x06002BBB RID: 11195
		void HandleMenuOption(FrameworkDataElement data);

		// Token: 0x06002BBC RID: 11196
		void Prewarm();
	}
}
