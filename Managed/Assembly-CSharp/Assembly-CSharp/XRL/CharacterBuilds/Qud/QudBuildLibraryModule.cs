using System;

namespace XRL.CharacterBuilds.Qud
{
	// Token: 0x02000596 RID: 1430
	public class QudBuildLibraryModule : QudEmbarkBuilderModule<QudBuildLibraryModuleData>
	{
		// Token: 0x06003D4F RID: 15695 RVA: 0x0012C7F7 File Offset: 0x0012A9F7
		public override bool shouldBeEditable()
		{
			return this.builder.IsEditableGameMode();
		}

		// Token: 0x06003D50 RID: 15696 RVA: 0x0012C804 File Offset: 0x0012AA04
		public override bool IncludeInBuildCodes()
		{
			return false;
		}

		// Token: 0x06003D51 RID: 15697 RVA: 0x0012C807 File Offset: 0x0012AA07
		public override bool shouldBeEnabled()
		{
			EmbarkBuilder builder = this.builder;
			string text;
			if (builder == null)
			{
				text = null;
			}
			else
			{
				QudChartypeModule module = builder.GetModule<QudChartypeModule>();
				if (module == null)
				{
					text = null;
				}
				else
				{
					QudChartypeModuleData data = module.data;
					text = ((data != null) ? data.type : null);
				}
			}
			return text == "Library";
		}

		// Token: 0x06003D52 RID: 15698 RVA: 0x0012C83D File Offset: 0x0012AA3D
		public override void InitFromSeed(string seed)
		{
		}
	}
}
