using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using AOT;
using Unity.Collections.LowLevel.Unsafe;

namespace UnityEngine.Bindings
{
	// Token: 0x02000366 RID: 870
	[VisibleToOtherModules]
	internal readonly struct ArrayHandleOnStack<[IsUnmanaged] TT> where TT : struct, ValueType
	{
		// Token: 0x060028BE RID: 10430 RVA: 0x0004E9D9 File Offset: 0x0004CBD9
		public unsafe ArrayHandleOnStack(void* arrayRefPtr)
		{
			this._arrayRefPtr = arrayRefPtr;
			this._allocArrayCallbackPtr = ArrayHandleOnStack<TT>.s_createArrayFcnPtr;
		}

		// Token: 0x060028BF RID: 10431 RVA: 0x0004E9EE File Offset: 0x0004CBEE
		public unsafe ArrayHandleOnStack(void* arrayRefPtr, IntPtr allocArrayCallbackPtr)
		{
			this._arrayRefPtr = arrayRefPtr;
			this._allocArrayCallbackPtr = allocArrayCallbackPtr;
		}

		// Token: 0x060028C0 RID: 10432 RVA: 0x0004EA00 File Offset: 0x0004CC00
		[MonoPInvokeCallback(typeof(ArrayHandleOnStack.CreateArrayDelegate))]
		public unsafe static void* AllocArrayManagedCallback(void* targetRef, int size)
		{
			TT[] array = new TT[size];
			*UnsafeUtility.ClassAsRef<TT[]>(targetRef) = array;
			bool flag = size < 1;
			void* ptr;
			if (flag)
			{
				ptr = null;
			}
			else
			{
				TT[] array2;
				void* ptr2;
				if ((array2 = array) == null || array2.Length == 0)
				{
					ptr2 = null;
				}
				else
				{
					ptr2 = (void*)(&array2[0]);
				}
				ptr = ptr2;
			}
			return ptr;
		}

		// Token: 0x04000B7D RID: 2941
		private unsafe readonly void* _arrayRefPtr;

		// Token: 0x04000B7E RID: 2942
		private readonly IntPtr _allocArrayCallbackPtr;

		// Token: 0x04000B7F RID: 2943
		private static ArrayHandleOnStack.CreateArrayDelegate s_createArrayDelegate = new ArrayHandleOnStack.CreateArrayDelegate(ArrayHandleOnStack<TT>.AllocArrayManagedCallback);

		// Token: 0x04000B80 RID: 2944
		private static IntPtr s_createArrayFcnPtr = Marshal.GetFunctionPointerForDelegate<ArrayHandleOnStack.CreateArrayDelegate>(ArrayHandleOnStack<TT>.s_createArrayDelegate);
	}
}
