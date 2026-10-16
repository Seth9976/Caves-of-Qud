using System;

namespace MonoMod.Utils
{
	// Token: 0x02000448 RID: 1096
	internal static class GCListener
	{
		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06001753 RID: 5971 RVA: 0x00050868 File Offset: 0x0004EA68
		// (remove) Token: 0x06001754 RID: 5972 RVA: 0x0005089C File Offset: 0x0004EA9C
		public static event Action OnCollect;

		// Token: 0x06001755 RID: 5973 RVA: 0x000508CF File Offset: 0x0004EACF
		static GCListener()
		{
			new GCListener.CollectionDummy();
		}

		// Token: 0x04001017 RID: 4119
		private static bool Unloading;

		// Token: 0x02000449 RID: 1097
		private sealed class CollectionDummy
		{
			// Token: 0x06001756 RID: 5974 RVA: 0x000508D8 File Offset: 0x0004EAD8
			protected override void Finalize()
			{
				try
				{
					GCListener.Unloading |= AppDomain.CurrentDomain.IsFinalizingForUnload() || Environment.HasShutdownStarted;
					if (!GCListener.Unloading)
					{
						GC.ReRegisterForFinalize(this);
					}
					Action onCollect = GCListener.OnCollect;
					if (onCollect != null)
					{
						onCollect();
					}
				}
				finally
				{
					base.Finalize();
				}
			}
		}
	}
}
