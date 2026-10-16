using System;
using System.Reflection;
using System.Reflection.Emit;
using System.Security.Permissions;

namespace System.Web.Util
{
	// Token: 0x02000117 RID: 279
	[SecurityPermission(SecurityAction.LinkDemand, Unrestricted = true)]
	internal static class FastDelegateCreator<TDelegate> where TDelegate : class
	{
		// Token: 0x06000DEE RID: 3566 RVA: 0x000256C7 File Offset: 0x000238C7
		internal static TDelegate BindTo(object obj, IntPtr method)
		{
			return FastDelegateCreator<TDelegate>._factory(obj, method);
		}

		// Token: 0x06000DEF RID: 3567 RVA: 0x000256D8 File Offset: 0x000238D8
		internal static TDelegate BindTo(object obj, MethodInfo method)
		{
			return FastDelegateCreator<TDelegate>.BindTo(obj, method.MethodHandle.GetFunctionPointer());
		}

		// Token: 0x06000DF0 RID: 3568 RVA: 0x000256FC File Offset: 0x000238FC
		[ReflectionPermission(SecurityAction.Assert, MemberAccess = true)]
		private static Func<object, IntPtr, TDelegate> GetFactory()
		{
			ConstructorInfo constructor = typeof(TDelegate).GetConstructor(new Type[]
			{
				typeof(object),
				typeof(IntPtr)
			});
			DynamicMethod dynamicMethod = new DynamicMethod("FastCreateDelegate_" + typeof(TDelegate).Name, typeof(TDelegate), new Type[]
			{
				typeof(object),
				typeof(IntPtr)
			}, typeof(FastDelegateCreator<TDelegate>), true);
			ILGenerator ilgenerator = dynamicMethod.GetILGenerator();
			ilgenerator.Emit(OpCodes.Ldarg_0);
			ilgenerator.Emit(OpCodes.Ldarg_1);
			ilgenerator.Emit(OpCodes.Newobj, constructor);
			ilgenerator.Emit(OpCodes.Ret);
			return (Func<object, IntPtr, TDelegate>)dynamicMethod.CreateDelegate(typeof(Func<object, IntPtr, TDelegate>));
		}

		// Token: 0x040011B1 RID: 4529
		private static readonly Func<object, IntPtr, TDelegate> _factory = FastDelegateCreator<TDelegate>.GetFactory();
	}
}
