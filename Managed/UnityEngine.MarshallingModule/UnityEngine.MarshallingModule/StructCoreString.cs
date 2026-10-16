using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000003 RID: 3
	[ExcludeFromDocs]
	[NativeHeader("Modules/Marshalling/MarshallingTests.h")]
	[NativeClass("StructCoreString", "struct StructCoreString;")]
	[RequiredByNativeCode(GenerateProxy = true, Name = "StructCoreStringManaged", Optional = true)]
	internal struct StructCoreString
	{
		// Token: 0x06000013 RID: 19 RVA: 0x0000219C File Offset: 0x0000039C
		public string GetField()
		{
			string stringAndDispose;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				StructCoreString.GetField_Injected(ref this, out managedSpanWrapper);
			}
			finally
			{
				ManagedSpanWrapper managedSpanWrapper;
				stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
			}
			return stringAndDispose;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000021CC File Offset: 0x000003CC
		public unsafe void SetField(string value)
		{
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(value, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = value.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				StructCoreString.SetField_Injected(ref this, ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x06000015 RID: 21
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetField_Injected(ref StructCoreString _unity_self, out ManagedSpanWrapper ret);

		// Token: 0x06000016 RID: 22
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetField_Injected(ref StructCoreString _unity_self, ref ManagedSpanWrapper value);

		// Token: 0x04000001 RID: 1
		public string field;
	}
}
