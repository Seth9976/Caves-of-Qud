using System;
using System.Reflection;

namespace AiUnity.NLog.Core.Internal
{
	// Token: 0x02000263 RID: 611
	internal class FactoryHelper
	{
		// Token: 0x0600162E RID: 5678 RVA: 0x00078019 File Offset: 0x00076219
		private FactoryHelper()
		{
		}

		// Token: 0x0600162F RID: 5679 RVA: 0x00078024 File Offset: 0x00076224
		internal static object CreateInstance(Type t)
		{
			ConstructorInfo constructor = t.GetConstructor(FactoryHelper.emptyTypes);
			if (constructor != null)
			{
				return constructor.Invoke(FactoryHelper.emptyParams);
			}
			throw new NLogConfigurationException("Cannot access the constructor of type: " + t.FullName + ". Is the required permission granted?");
		}

		// Token: 0x04000DD3 RID: 3539
		private static Type[] emptyTypes = new Type[0];

		// Token: 0x04000DD4 RID: 3540
		private static object[] emptyParams = new object[0];
	}
}
