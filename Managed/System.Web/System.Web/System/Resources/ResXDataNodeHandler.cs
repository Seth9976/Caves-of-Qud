using System;
using System.ComponentModel.Design;
using System.Reflection;

namespace System.Resources
{
	// Token: 0x02000023 RID: 35
	internal abstract class ResXDataNodeHandler
	{
		// Token: 0x060000A3 RID: 163
		public abstract object GetValue(ITypeResolutionService typeResolver);

		// Token: 0x060000A4 RID: 164
		public abstract object GetValue(AssemblyName[] assemblyNames);

		// Token: 0x060000A5 RID: 165
		public abstract string GetValueTypeName(ITypeResolutionService typeResolver);

		// Token: 0x060000A6 RID: 166
		public abstract string GetValueTypeName(AssemblyName[] assemblyNames);

		// Token: 0x060000A7 RID: 167 RVA: 0x00003F97 File Offset: 0x00002197
		public virtual object GetValueForResX()
		{
			return this.GetValue(null);
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00003FA0 File Offset: 0x000021A0
		protected Type ResolveType(string typeString)
		{
			return Type.GetType(typeString);
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00003FA8 File Offset: 0x000021A8
		protected Type ResolveType(string typeString, AssemblyName[] assemblyNames)
		{
			Type type = null;
			if (assemblyNames != null)
			{
				for (int i = 0; i < assemblyNames.Length; i++)
				{
					type = Assembly.Load(assemblyNames[i]).GetType(typeString, false);
					if (type != null)
					{
						return type;
					}
				}
			}
			if (type == null)
			{
				type = this.ResolveType(typeString);
			}
			return type;
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00003FF8 File Offset: 0x000021F8
		protected Type ResolveType(string typeString, ITypeResolutionService typeResolver)
		{
			Type type = null;
			if (typeResolver != null)
			{
				type = typeResolver.GetType(typeString);
			}
			if (type == null)
			{
				type = this.ResolveType(typeString);
			}
			return type;
		}
	}
}
