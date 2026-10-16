using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000012 RID: 18
	[ExcludeFromDocs]
	[NativeHeader("Modules/Marshalling/MarshallingTests.h")]
	[StructLayout(LayoutKind.Sequential)]
	internal class MarshallingTestObject : Object
	{
		// Token: 0x0600007E RID: 126 RVA: 0x00002A59 File Offset: 0x00000C59
		public MarshallingTestObject()
		{
			MarshallingTestObject.Internal_CreateMarshallingTestObject(this);
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00002A6C File Offset: 0x00000C6C
		public int MemberFunction(int a)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<MarshallingTestObject>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return MarshallingTestObject.MemberFunction_Injected(intPtr, a);
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000080 RID: 128 RVA: 0x00002A90 File Offset: 0x00000C90
		// (set) Token: 0x06000081 RID: 129 RVA: 0x00002AB4 File Offset: 0x00000CB4
		public int MemberProperty
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<MarshallingTestObject>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return MarshallingTestObject.get_MemberProperty_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<MarshallingTestObject>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				MarshallingTestObject.set_MemberProperty_Injected(intPtr, value);
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000082 RID: 130 RVA: 0x00002AD8 File Offset: 0x00000CD8
		// (set) Token: 0x06000083 RID: 131 RVA: 0x00002AFC File Offset: 0x00000CFC
		[NativeProperty("m_fieldBoundProp", false, TargetType.Field)]
		public int FieldBoundMemberProperty
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<MarshallingTestObject>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return MarshallingTestObject.get_FieldBoundMemberProperty_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<MarshallingTestObject>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				MarshallingTestObject.set_FieldBoundMemberProperty_Injected(intPtr, value);
			}
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00002B20 File Offset: 0x00000D20
		public static MarshallingTestObject Create()
		{
			return Unmarshal.UnmarshalUnityObject<MarshallingTestObject>(MarshallingTestObject.Create_Injected());
		}

		// Token: 0x06000085 RID: 133
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_CreateMarshallingTestObject([Writable] MarshallingTestObject notSelf);

		// Token: 0x06000086 RID: 134
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int MemberFunction_Injected(IntPtr _unity_self, int a);

		// Token: 0x06000087 RID: 135
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_MemberProperty_Injected(IntPtr _unity_self);

		// Token: 0x06000088 RID: 136
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_MemberProperty_Injected(IntPtr _unity_self, int value);

		// Token: 0x06000089 RID: 137
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_FieldBoundMemberProperty_Injected(IntPtr _unity_self);

		// Token: 0x0600008A RID: 138
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_FieldBoundMemberProperty_Injected(IntPtr _unity_self, int value);

		// Token: 0x0600008B RID: 139
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr Create_Injected();

		// Token: 0x0400000C RID: 12
		[RequiredByNativeCode(Optional = true)]
		[RequiredMember]
		private int TestField;
	}
}
