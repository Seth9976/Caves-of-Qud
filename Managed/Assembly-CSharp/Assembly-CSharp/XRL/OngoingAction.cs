using System;

namespace XRL
{
	// Token: 0x020004E8 RID: 1256
	public abstract class OngoingAction : IOngoingAction
	{
		// Token: 0x0600363E RID: 13886 RVA: 0x00104C3A File Offset: 0x00102E3A
		public virtual string GetDescription()
		{
			return "acting";
		}

		// Token: 0x0600363F RID: 13887 RVA: 0x00104C41 File Offset: 0x00102E41
		public virtual bool IsMovement()
		{
			return false;
		}

		// Token: 0x06003640 RID: 13888 RVA: 0x00104C44 File Offset: 0x00102E44
		public virtual bool IsCombat()
		{
			return false;
		}

		// Token: 0x06003641 RID: 13889 RVA: 0x00104C47 File Offset: 0x00102E47
		public virtual bool IsExploration()
		{
			return false;
		}

		// Token: 0x06003642 RID: 13890 RVA: 0x00104C4A File Offset: 0x00102E4A
		public virtual bool IsGathering()
		{
			return false;
		}

		// Token: 0x06003643 RID: 13891 RVA: 0x00104C4D File Offset: 0x00102E4D
		public virtual bool IsResting()
		{
			return false;
		}

		// Token: 0x06003644 RID: 13892 RVA: 0x00104C50 File Offset: 0x00102E50
		public virtual bool IsRateLimited()
		{
			return false;
		}

		// Token: 0x06003645 RID: 13893 RVA: 0x00104C53 File Offset: 0x00102E53
		public virtual bool Continue()
		{
			return true;
		}

		// Token: 0x06003646 RID: 13894 RVA: 0x00104C56 File Offset: 0x00102E56
		public virtual string GetInterruptBecause()
		{
			return null;
		}

		// Token: 0x06003647 RID: 13895 RVA: 0x00104C59 File Offset: 0x00102E59
		public virtual bool ShouldHostilesInterrupt()
		{
			return true;
		}

		// Token: 0x06003648 RID: 13896 RVA: 0x00104C5C File Offset: 0x00102E5C
		public virtual void Interrupt()
		{
		}

		// Token: 0x06003649 RID: 13897 RVA: 0x00104C5E File Offset: 0x00102E5E
		public virtual void Resume()
		{
		}

		// Token: 0x0600364A RID: 13898 RVA: 0x00104C60 File Offset: 0x00102E60
		public virtual bool CanComplete()
		{
			return true;
		}

		// Token: 0x0600364B RID: 13899 RVA: 0x00104C63 File Offset: 0x00102E63
		public virtual void Complete()
		{
		}

		// Token: 0x0600364C RID: 13900 RVA: 0x00104C65 File Offset: 0x00102E65
		public virtual void End()
		{
		}
	}
}
