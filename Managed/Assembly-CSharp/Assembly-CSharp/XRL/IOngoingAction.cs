using System;

namespace XRL
{
	// Token: 0x020004E3 RID: 1251
	internal interface IOngoingAction
	{
		// Token: 0x06003601 RID: 13825
		string GetDescription();

		// Token: 0x06003602 RID: 13826
		bool IsMovement();

		// Token: 0x06003603 RID: 13827
		bool IsCombat();

		// Token: 0x06003604 RID: 13828
		bool IsExploration();

		// Token: 0x06003605 RID: 13829
		bool IsGathering();

		// Token: 0x06003606 RID: 13830
		bool IsResting();

		// Token: 0x06003607 RID: 13831
		bool IsRateLimited();

		// Token: 0x06003608 RID: 13832
		bool Continue();

		// Token: 0x06003609 RID: 13833
		string GetInterruptBecause();

		// Token: 0x0600360A RID: 13834
		bool ShouldHostilesInterrupt();

		// Token: 0x0600360B RID: 13835
		void Interrupt();

		// Token: 0x0600360C RID: 13836
		void Resume();

		// Token: 0x0600360D RID: 13837
		bool CanComplete();

		// Token: 0x0600360E RID: 13838
		void Complete();

		// Token: 0x0600360F RID: 13839
		void End();
	}
}
