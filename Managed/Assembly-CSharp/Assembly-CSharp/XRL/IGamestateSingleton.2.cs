using System;

namespace XRL
{
	// Token: 0x020004D5 RID: 1237
	[Obsolete("Use IGameStateSingleton")]
	public interface IGamestateSingleton
	{
		// Token: 0x06003508 RID: 13576 RVA: 0x0010026F File Offset: 0x000FE46F
		[Obsolete("Use Initialize")]
		void init()
		{
		}

		// Token: 0x06003509 RID: 13577 RVA: 0x00100271 File Offset: 0x000FE471
		[Obsolete("Use HandleEvent with ID == QudGameBootModule.BOOTEVENT_AFTERINITIALIZEWORLDS)")]
		void worldBuild()
		{
		}
	}
}
