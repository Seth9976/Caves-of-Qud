using System;
using System.Collections.Generic;
using System.Reflection;
using AiUnity.Common.InternalLog;
using AiUnity.Common.Patterns;
using AiUnity.NLog.Core.Common;

namespace AiUnity.NLog.Core.Internal
{
	// Token: 0x0200026C RID: 620
	internal static class ReflectionHelpers
	{
		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x06001662 RID: 5730 RVA: 0x000789DC File Offset: 0x00076BDC
		private static IInternalLogger Logger
		{
			get
			{
				return Singleton<NLogInternalLogger>.Instance;
			}
		}

		// Token: 0x06001663 RID: 5731 RVA: 0x000789E4 File Offset: 0x00076BE4
		public static Type[] SafeGetTypes(this Assembly assembly)
		{
			Type[] array;
			try
			{
				array = assembly.GetTypes();
			}
			catch (ReflectionTypeLoadException ex)
			{
				foreach (Exception ex2 in ex.LoaderExceptions)
				{
					ReflectionHelpers.Logger.Warn("Type load exception: {0}", new object[] { ex2 });
				}
				List<Type> list = new List<Type>();
				foreach (Type type in ex.Types)
				{
					if (type != null)
					{
						list.Add(type);
					}
				}
				array = list.ToArray();
			}
			return array;
		}
	}
}
