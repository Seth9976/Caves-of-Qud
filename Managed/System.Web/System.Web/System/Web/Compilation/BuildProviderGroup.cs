using System;
using System.Collections.Generic;

namespace System.Web.Compilation
{
	// Token: 0x02000640 RID: 1600
	internal class BuildProviderGroup : List<BuildProvider>
	{
		// Token: 0x17001592 RID: 5522
		// (get) Token: 0x060044C8 RID: 17608 RVA: 0x000BA3DC File Offset: 0x000B85DC
		// (set) Token: 0x060044C9 RID: 17609 RVA: 0x000BA3E4 File Offset: 0x000B85E4
		public string NamePrefix { get; private set; }

		// Token: 0x17001593 RID: 5523
		// (get) Token: 0x060044CA RID: 17610 RVA: 0x000BA3ED File Offset: 0x000B85ED
		// (set) Token: 0x060044CB RID: 17611 RVA: 0x000BA3F5 File Offset: 0x000B85F5
		public bool Standalone { get; set; }

		// Token: 0x17001594 RID: 5524
		// (get) Token: 0x060044CC RID: 17612 RVA: 0x000BA3FE File Offset: 0x000B85FE
		// (set) Token: 0x060044CD RID: 17613 RVA: 0x000BA406 File Offset: 0x000B8606
		public bool Application { get; private set; }

		// Token: 0x17001595 RID: 5525
		// (get) Token: 0x060044CE RID: 17614 RVA: 0x000BA40F File Offset: 0x000B860F
		// (set) Token: 0x060044CF RID: 17615 RVA: 0x000BA417 File Offset: 0x000B8617
		public bool Master { get; set; }

		// Token: 0x17001596 RID: 5526
		// (get) Token: 0x060044D0 RID: 17616 RVA: 0x000BA420 File Offset: 0x000B8620
		// (set) Token: 0x060044D1 RID: 17617 RVA: 0x000BA428 File Offset: 0x000B8628
		public CompilerType CompilerType { get; private set; }

		// Token: 0x060044D3 RID: 17619 RVA: 0x000BA43C File Offset: 0x000B863C
		public void AddProvider(BuildProvider bp)
		{
			if (base.Count == 0)
			{
				if (bp is ApplicationFileBuildProvider)
				{
					this.NamePrefix = "App_global.asax";
					this.Application = true;
				}
				else if (bp is ThemeDirectoryBuildProvider)
				{
					this.NamePrefix = "App_Theme";
					this.Master = true;
				}
				else
				{
					this.NamePrefix = "App_Web";
				}
				CompilerType defaultCompilerTypeForLanguage = BuildManager.GetDefaultCompilerTypeForLanguage(bp.LanguageName, null);
				if (defaultCompilerTypeForLanguage != null)
				{
					this.CompilerType = defaultCompilerTypeForLanguage;
				}
			}
			base.Add(bp);
		}
	}
}
