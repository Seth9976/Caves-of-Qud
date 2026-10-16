using System;
using XRL.World;

namespace XRL.Core
{
	// Token: 0x0200063B RID: 1595
	public interface IActionCommand : IComposite
	{
		// Token: 0x170006B3 RID: 1715
		// (get) Token: 0x0600429E RID: 17054 RVA: 0x00161410 File Offset: 0x0015F610
		int Priority
		{
			get
			{
				return 45000;
			}
		}

		// Token: 0x0600429F RID: 17055
		void Execute(XRLGame Game, ActionManager Manager);

		// Token: 0x040020F2 RID: 8434
		public const int PRIORITY_VERY_LOW = 15000;

		// Token: 0x040020F3 RID: 8435
		public const int PRIORITY_LOW = 30000;

		// Token: 0x040020F4 RID: 8436
		public const int PRIORITY_MEDIUM = 45000;

		// Token: 0x040020F5 RID: 8437
		public const int PRIORITY_HIGH = 60000;

		// Token: 0x040020F6 RID: 8438
		public const int PRIORITY_VERY_HIGH = 75000;

		// Token: 0x040020F7 RID: 8439
		public const int PRIORITY_ADJUST_VERY_SMALL = 1;

		// Token: 0x040020F8 RID: 8440
		public const int PRIORITY_ADJUST_SMALL = 10;

		// Token: 0x040020F9 RID: 8441
		public const int PRIORITY_ADJUST_MEDIUM = 100;

		// Token: 0x040020FA RID: 8442
		public const int PRIORITY_ADJUST_LARGE = 1000;

		// Token: 0x040020FB RID: 8443
		public const int PRIORITY_ADJUST_VERY_LARGE = 10000;
	}
}
