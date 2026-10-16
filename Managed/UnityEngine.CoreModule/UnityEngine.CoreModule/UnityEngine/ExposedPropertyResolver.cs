using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x0200013D RID: 317
	[NativeHeader("Runtime/Director/Core/ExposedPropertyTable.bindings.h")]
	[NativeHeader("Runtime/Utilities/PropertyName.h")]
	public struct ExposedPropertyResolver
	{
		// Token: 0x0600095D RID: 2397 RVA: 0x0000FDCC File Offset: 0x0000DFCC
		internal static Object ResolveReferenceInternal(IntPtr ptr, PropertyName name, out bool isValid)
		{
			bool flag = ptr == IntPtr.Zero;
			if (flag)
			{
				throw new ArgumentNullException("Argument \"ptr\" can't be null.");
			}
			return ExposedPropertyResolver.ResolveReferenceBindingsInternal(ptr, name, out isValid);
		}

		// Token: 0x0600095E RID: 2398 RVA: 0x0000FE00 File Offset: 0x0000E000
		[FreeFunction("ExposedPropertyTableBindings::ResolveReferenceInternal")]
		private static Object ResolveReferenceBindingsInternal(IntPtr ptr, PropertyName name, out bool isValid)
		{
			return Unmarshal.UnmarshalUnityObject<Object>(ExposedPropertyResolver.ResolveReferenceBindingsInternal_Injected(ptr, ref name, out isValid));
		}

		// Token: 0x0600095F RID: 2399
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr ResolveReferenceBindingsInternal_Injected(IntPtr ptr, [In] ref PropertyName name, out bool isValid);

		// Token: 0x0400042B RID: 1067
		internal IntPtr table;
	}
}
