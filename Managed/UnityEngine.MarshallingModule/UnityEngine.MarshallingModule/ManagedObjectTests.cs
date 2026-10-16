using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x0200001D RID: 29
	[NativeHeader("Modules/Marshalling/MarshallingTests.h")]
	[ExcludeFromDocs]
	internal class ManagedObjectTests
	{
		// Token: 0x060000C6 RID: 198
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern MyManagedObject ParameterManagedObject(MyManagedObject param);

		// Token: 0x060000C7 RID: 199 RVA: 0x00002EB4 File Offset: 0x000010B4
		[NativeThrows]
		public static StructManagedObject ParameterStructManagedObject(StructManagedObject param)
		{
			StructManagedObject structManagedObject;
			ManagedObjectTests.ParameterStructManagedObject_Injected(ref param, out structManagedObject);
			return structManagedObject;
		}

		// Token: 0x060000C8 RID: 200
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern MyManagedObject[] ReturnNullManagedObjectArray();

		// Token: 0x060000C9 RID: 201
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern MyManagedObject[] ParameterManagedObjectVector(MyManagedObject[] param);

		// Token: 0x060000CA RID: 202 RVA: 0x00002ECC File Offset: 0x000010CC
		[NativeThrows]
		public static StructManagedObjectVector ParameterStructManagedObjectVector(StructManagedObjectVector param)
		{
			StructManagedObjectVector structManagedObjectVector;
			ManagedObjectTests.ParameterStructManagedObjectVector_Injected(ref param, out structManagedObjectVector);
			return structManagedObjectVector;
		}

		// Token: 0x060000CC RID: 204
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ParameterStructManagedObject_Injected([In] ref StructManagedObject param, out StructManagedObject ret);

		// Token: 0x060000CD RID: 205
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ParameterStructManagedObjectVector_Injected([In] ref StructManagedObjectVector param, out StructManagedObjectVector ret);
	}
}
