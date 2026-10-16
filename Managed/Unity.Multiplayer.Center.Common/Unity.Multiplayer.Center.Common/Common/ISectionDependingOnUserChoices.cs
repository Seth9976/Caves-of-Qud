using System;

namespace Unity.Multiplayer.Center.Common
{
	// Token: 0x0200000B RID: 11
	public interface ISectionDependingOnUserChoices : IOnboardingSection
	{
		// Token: 0x06000017 RID: 23 RVA: 0x0000217A File Offset: 0x0000037A
		void HandleAnswerData(AnswerData answerData)
		{
		}

		// Token: 0x06000018 RID: 24 RVA: 0x0000217C File Offset: 0x0000037C
		void HandleUserSelectionData(SelectedSolutionsData selectedSolutionsData)
		{
		}

		// Token: 0x06000019 RID: 25 RVA: 0x0000217E File Offset: 0x0000037E
		void HandlePreset(Preset preset)
		{
		}
	}
}
