using System;
using System.Reflection;
using System.Runtime.Serialization;

namespace Mono.Remoting.Channels.Unix
{
	// Token: 0x02000083 RID: 131
	internal class SimpleBinder : SerializationBinder
	{
		// Token: 0x06000690 RID: 1680 RVA: 0x0000E730 File Offset: 0x0000C930
		public override Type BindToType(string assemblyName, string typeName)
		{
			Assembly assembly;
			if (assemblyName.IndexOf(',') != -1)
			{
				try
				{
					assembly = Assembly.Load(assemblyName);
					if (assembly == null)
					{
						return null;
					}
					Type type = assembly.GetType(typeName);
					if (type != null)
					{
						return type;
					}
				}
				catch
				{
				}
			}
			assembly = Assembly.LoadWithPartialName(assemblyName);
			if (assembly == null)
			{
				return null;
			}
			return assembly.GetType(typeName, true);
		}

		// Token: 0x040004C0 RID: 1216
		public static SimpleBinder Instance = new SimpleBinder();
	}
}
