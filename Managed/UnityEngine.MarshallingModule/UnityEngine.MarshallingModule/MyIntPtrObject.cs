using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x0200000F RID: 15
	[ExcludeFromDocs]
	internal class MyIntPtrObject : IDisposable
	{
		// Token: 0x06000064 RID: 100 RVA: 0x000028EC File Offset: 0x00000AEC
		internal MyIntPtrObject(IntPtr ptr)
		{
			this.m_Ptr = ptr;
		}

		// Token: 0x06000065 RID: 101 RVA: 0x000028FD File Offset: 0x00000AFD
		public MyIntPtrObject()
		{
			this.m_Ptr = MyIntPtrObject.Internal_Create();
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00002914 File Offset: 0x00000B14
		public void Dispose()
		{
			bool flag = this.m_Ptr != IntPtr.Zero;
			if (flag)
			{
				MyIntPtrObject.Internal_Destroy(this.m_Ptr);
				this.m_Ptr = IntPtr.Zero;
			}
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00002950 File Offset: 0x00000B50
		public static MyIntPtrObject Create()
		{
			IntPtr intPtr = MyIntPtrObject.Create_Injected();
			return (intPtr == 0) ? null : MyIntPtrObject.BindingsMarshaller.ConvertToManaged(intPtr);
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00002970 File Offset: 0x00000B70
		public int MemberFunction(int a)
		{
			IntPtr intPtr = MyIntPtrObject.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return MyIntPtrObject.MemberFunction_Injected(intPtr, a);
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000069 RID: 105 RVA: 0x00002994 File Offset: 0x00000B94
		// (set) Token: 0x0600006A RID: 106 RVA: 0x000029B8 File Offset: 0x00000BB8
		public int MemberProperty
		{
			get
			{
				IntPtr intPtr = MyIntPtrObject.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return MyIntPtrObject.get_MemberProperty_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = MyIntPtrObject.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				MyIntPtrObject.set_MemberProperty_Injected(intPtr, value);
			}
		}

		// Token: 0x0600006B RID: 107
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr Internal_Create();

		// Token: 0x0600006C RID: 108
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_Destroy(IntPtr ptr);

		// Token: 0x0600006D RID: 109
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr Create_Injected();

		// Token: 0x0600006E RID: 110
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int MemberFunction_Injected(IntPtr _unity_self, int a);

		// Token: 0x0600006F RID: 111
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_MemberProperty_Injected(IntPtr _unity_self);

		// Token: 0x06000070 RID: 112
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_MemberProperty_Injected(IntPtr _unity_self, int value);

		// Token: 0x0400000B RID: 11
		public IntPtr m_Ptr;

		// Token: 0x02000010 RID: 16
		internal static class BindingsMarshaller
		{
			// Token: 0x06000071 RID: 113 RVA: 0x000029DB File Offset: 0x00000BDB
			public static IntPtr ConvertToNative(MyIntPtrObject obj)
			{
				return obj.m_Ptr;
			}

			// Token: 0x06000072 RID: 114 RVA: 0x000029E3 File Offset: 0x00000BE3
			public static MyIntPtrObject ConvertToManaged(IntPtr ptr)
			{
				return new MyIntPtrObject(ptr);
			}
		}
	}
}
