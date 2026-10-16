using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x020008F7 RID: 2295
	internal struct MonoMethodInfo
	{
		// Token: 0x06004D47 RID: 19783
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_method_info(IntPtr handle, out MonoMethodInfo info);

		// Token: 0x06004D48 RID: 19784
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_method_attributes(IntPtr handle);

		// Token: 0x06004D49 RID: 19785 RVA: 0x000F4038 File Offset: 0x000F2238
		internal static MonoMethodInfo GetMethodInfo(IntPtr handle)
		{
			MonoMethodInfo monoMethodInfo;
			MonoMethodInfo.get_method_info(handle, out monoMethodInfo);
			return monoMethodInfo;
		}

		// Token: 0x06004D4A RID: 19786 RVA: 0x000F404E File Offset: 0x000F224E
		internal static Type GetDeclaringType(IntPtr handle)
		{
			return MonoMethodInfo.GetMethodInfo(handle).parent;
		}

		// Token: 0x06004D4B RID: 19787 RVA: 0x000F405B File Offset: 0x000F225B
		internal static Type GetReturnType(IntPtr handle)
		{
			return MonoMethodInfo.GetMethodInfo(handle).ret;
		}

		// Token: 0x06004D4C RID: 19788 RVA: 0x000F4068 File Offset: 0x000F2268
		internal static MethodAttributes GetAttributes(IntPtr handle)
		{
			return (MethodAttributes)MonoMethodInfo.get_method_attributes(handle);
		}

		// Token: 0x06004D4D RID: 19789 RVA: 0x000F4070 File Offset: 0x000F2270
		internal static CallingConventions GetCallingConvention(IntPtr handle)
		{
			return MonoMethodInfo.GetMethodInfo(handle).callconv;
		}

		// Token: 0x06004D4E RID: 19790 RVA: 0x000F407D File Offset: 0x000F227D
		internal static MethodImplAttributes GetMethodImplementationFlags(IntPtr handle)
		{
			return MonoMethodInfo.GetMethodInfo(handle).iattrs;
		}

		// Token: 0x06004D4F RID: 19791
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern ParameterInfo[] get_parameter_info(IntPtr handle, MemberInfo member);

		// Token: 0x06004D50 RID: 19792 RVA: 0x000F408A File Offset: 0x000F228A
		internal static ParameterInfo[] GetParametersInfo(IntPtr handle, MemberInfo member)
		{
			return MonoMethodInfo.get_parameter_info(handle, member);
		}

		// Token: 0x06004D51 RID: 19793
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern MarshalAsAttribute get_retval_marshal(IntPtr handle);

		// Token: 0x06004D52 RID: 19794 RVA: 0x000F4093 File Offset: 0x000F2293
		internal static ParameterInfo GetReturnParameterInfo(RuntimeMethodInfo method)
		{
			return RuntimeParameterInfo.New(MonoMethodInfo.GetReturnType(method.mhandle), method, MonoMethodInfo.get_retval_marshal(method.mhandle));
		}

		// Token: 0x04003055 RID: 12373
		private Type parent;

		// Token: 0x04003056 RID: 12374
		private Type ret;

		// Token: 0x04003057 RID: 12375
		internal MethodAttributes attrs;

		// Token: 0x04003058 RID: 12376
		internal MethodImplAttributes iattrs;

		// Token: 0x04003059 RID: 12377
		private CallingConventions callconv;
	}
}
