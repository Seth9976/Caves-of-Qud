using System;

namespace AiUnity.Common.Patterns
{
	// Token: 0x020002C3 RID: 707
	public abstract class Singleton<T> where T : new()
	{
		// Token: 0x1700035B RID: 859
		// (get) Token: 0x06001892 RID: 6290 RVA: 0x0007F6E8 File Offset: 0x0007D8E8
		public static T Instance
		{
			get
			{
				if (Singleton<T>.instance == null)
				{
					try
					{
						Singleton<T>.instance = new T();
					}
					catch (Exception ex)
					{
						throw new Exception("Failed to create Singleton instance.", ex);
					}
				}
				return Singleton<T>.instance;
			}
		}

		// Token: 0x1700035C RID: 860
		// (get) Token: 0x06001893 RID: 6291 RVA: 0x0007F730 File Offset: 0x0007D930
		public static bool InstanceExists
		{
			get
			{
				return Singleton<T>.instance != null;
			}
		}

		// Token: 0x04000E8E RID: 3726
		private static T instance;
	}
}
