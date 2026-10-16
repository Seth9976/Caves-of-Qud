using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x0200023E RID: 574
	[NativeHeader("Runtime/Utilities/PropertyName.h")]
	internal class PropertyNameUtils
	{
		// Token: 0x06001E72 RID: 7794 RVA: 0x0003AA88 File Offset: 0x00038C88
		[FreeFunction("PropertyNameFromStringICall", IsThreadSafe = true)]
		public unsafe static PropertyName PropertyNameFromString(string name)
		{
			PropertyName propertyName2;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(name, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = name.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				PropertyName propertyName;
				PropertyNameUtils.PropertyNameFromString_Injected(ref managedSpanWrapper, out propertyName);
			}
			finally
			{
				char* ptr = null;
				PropertyName propertyName;
				propertyName2 = propertyName;
			}
			return propertyName2;
		}

		// Token: 0x06001E74 RID: 7796
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void PropertyNameFromString_Injected(ref ManagedSpanWrapper name, out PropertyName ret);
	}
}
