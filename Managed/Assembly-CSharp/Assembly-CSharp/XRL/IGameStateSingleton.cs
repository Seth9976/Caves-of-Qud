using System;
using XRL.World;

namespace XRL
{
	// Token: 0x020004D4 RID: 1236
	public interface IGameStateSingleton : IComposite
	{
		// Token: 0x06003506 RID: 13574 RVA: 0x0010026B File Offset: 0x000FE46B
		void Initialize()
		{
		}

		// Token: 0x06003507 RID: 13575 RVA: 0x0010026D File Offset: 0x000FE46D
		void HandleEvent(EmbarkEvent E)
		{
		}
	}
}
