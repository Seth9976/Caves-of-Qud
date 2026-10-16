using System;

namespace NUnit.Framework.Constraints
{
	// Token: 0x02000127 RID: 295
	public abstract class ConstraintOperator
	{
		// Token: 0x1700021F RID: 543
		// (get) Token: 0x0600092F RID: 2351 RVA: 0x0001651A File Offset: 0x0001471A
		// (set) Token: 0x06000930 RID: 2352 RVA: 0x00016522 File Offset: 0x00014722
		public object LeftContext
		{
			get
			{
				return this.leftContext;
			}
			set
			{
				this.leftContext = value;
			}
		}

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x06000931 RID: 2353 RVA: 0x0001652B File Offset: 0x0001472B
		// (set) Token: 0x06000932 RID: 2354 RVA: 0x00016533 File Offset: 0x00014733
		public object RightContext
		{
			get
			{
				return this.rightContext;
			}
			set
			{
				this.rightContext = value;
			}
		}

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x06000933 RID: 2355 RVA: 0x0001653C File Offset: 0x0001473C
		public virtual int LeftPrecedence
		{
			get
			{
				return this.left_precedence;
			}
		}

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x06000934 RID: 2356 RVA: 0x00016544 File Offset: 0x00014744
		public virtual int RightPrecedence
		{
			get
			{
				return this.right_precedence;
			}
		}

		// Token: 0x06000935 RID: 2357
		public abstract void Reduce(ConstraintBuilder.ConstraintStack stack);

		// Token: 0x040001DB RID: 475
		private object leftContext;

		// Token: 0x040001DC RID: 476
		private object rightContext;

		// Token: 0x040001DD RID: 477
		protected int left_precedence;

		// Token: 0x040001DE RID: 478
		protected int right_precedence;
	}
}
