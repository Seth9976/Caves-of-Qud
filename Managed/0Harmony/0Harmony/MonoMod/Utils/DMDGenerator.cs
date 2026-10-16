using System;
using System.Reflection;
using System.Reflection.Emit;

namespace MonoMod.Utils
{
	// Token: 0x0200042E RID: 1070
	public abstract class DMDGenerator<TSelf> : _IDMDGenerator where TSelf : DMDGenerator<TSelf>, new()
	{
		// Token: 0x0600167F RID: 5759
		protected abstract MethodInfo _Generate(DynamicMethodDefinition dmd, object context);

		// Token: 0x06001680 RID: 5760 RVA: 0x00048B43 File Offset: 0x00046D43
		MethodInfo _IDMDGenerator.Generate(DynamicMethodDefinition dmd, object context)
		{
			return DMDGenerator<TSelf>._Postbuild(this._Generate(dmd, context));
		}

		// Token: 0x06001681 RID: 5761 RVA: 0x00048B52 File Offset: 0x00046D52
		public static MethodInfo Generate(DynamicMethodDefinition dmd, object context = null)
		{
			TSelf tself;
			if ((tself = DMDGenerator<TSelf>._Instance) == null)
			{
				tself = (DMDGenerator<TSelf>._Instance = new TSelf());
			}
			return DMDGenerator<TSelf>._Postbuild(tself._Generate(dmd, context));
		}

		// Token: 0x06001682 RID: 5762 RVA: 0x00048B80 File Offset: 0x00046D80
		internal static MethodInfo _Postbuild(MethodInfo mi)
		{
			if (mi == null)
			{
				return null;
			}
			if (ReflectionHelper.IsMono && !(mi is DynamicMethod) && mi.DeclaringType != null)
			{
				Module module = ((mi != null) ? mi.Module : null);
				if (module == null)
				{
					return mi;
				}
				Assembly assembly = module.Assembly;
				if (((assembly != null) ? assembly.GetType() : null) == null)
				{
					return mi;
				}
				assembly.SetMonoCorlibInternal(true);
			}
			return mi;
		}

		// Token: 0x04000FB1 RID: 4017
		private static TSelf _Instance;
	}
}
