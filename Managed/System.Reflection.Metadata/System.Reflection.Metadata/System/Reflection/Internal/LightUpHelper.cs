using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace System.Reflection.Internal
{
	// Token: 0x02000183 RID: 387
	[NullableContext(1)]
	[Nullable(0)]
	internal static class LightUpHelper
	{
		// Token: 0x06000BCB RID: 3019 RVA: 0x00023848 File Offset: 0x00021A48
		internal static Type GetType(string typeName, params string[] assemblyNames)
		{
			foreach (string text in assemblyNames)
			{
				Type type = null;
				try
				{
					type = Type.GetType(typeName + "," + text, false);
				}
				catch (IOException)
				{
				}
				if (type != null)
				{
					return type;
				}
			}
			return null;
		}

		// Token: 0x06000BCC RID: 3020 RVA: 0x000238A0 File Offset: 0x00021AA0
		internal static MethodInfo GetMethod(Type type, string name, params Type[] parameterTypes)
		{
			MethodInfo methodInfo;
			try
			{
				methodInfo = RuntimeReflectionExtensions.GetRuntimeMethod(type, name, parameterTypes);
			}
			catch (AmbiguousMatchException)
			{
				methodInfo = null;
			}
			return methodInfo;
		}
	}
}
