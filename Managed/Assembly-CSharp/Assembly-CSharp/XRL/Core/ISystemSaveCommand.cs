using System;
using XRL.World;

namespace XRL.Core
{
	// Token: 0x0200063C RID: 1596
	public interface ISystemSaveCommand : IActionCommand, IComposite
	{
		// Token: 0x170006B4 RID: 1716
		// (get) Token: 0x060042A0 RID: 17056 RVA: 0x00161417 File Offset: 0x0015F617
		bool IComposite.WantFieldReflection
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170006B5 RID: 1717
		// (get) Token: 0x060042A1 RID: 17057 RVA: 0x0016141A File Offset: 0x0015F61A
		int IActionCommand.Priority
		{
			get
			{
				return 15000;
			}
		}

		// Token: 0x170006B6 RID: 1718
		// (get) Token: 0x060042A2 RID: 17058
		string Type { get; }
	}
}
