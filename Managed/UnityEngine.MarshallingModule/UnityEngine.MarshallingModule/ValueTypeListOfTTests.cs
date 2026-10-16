using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000043 RID: 67
	[NativeType("Modules/Marshalling/MarshallingTests.h")]
	internal class ValueTypeListOfTTests
	{
		// Token: 0x060001AE RID: 430 RVA: 0x00003DB4 File Offset: 0x00001FB4
		[NativeThrows]
		public unsafe static void ParameterListOfIntRead(List<int> param)
		{
			try
			{
				BlittableListWrapper blittableListWrapper;
				if (param != null)
				{
					fixed (int[] array = NoAllocHelpers.ExtractArrayFromList<int>(param))
					{
						BlittableArrayWrapper blittableArrayWrapper;
						if (array.Length != 0)
						{
							blittableArrayWrapper = new BlittableArrayWrapper((void*)(&array[0]), array.Length);
						}
						blittableListWrapper = new BlittableListWrapper(blittableArrayWrapper, param.Count);
					}
				}
				ValueTypeListOfTTests.ParameterListOfIntRead_Injected(ref blittableListWrapper);
			}
			finally
			{
				BlittableListWrapper blittableListWrapper;
				blittableListWrapper.Unmarshal<int>(param);
			}
		}

		// Token: 0x060001AF RID: 431 RVA: 0x00003E18 File Offset: 0x00002018
		[NativeThrows]
		public unsafe static void ParameterListOfIntReadChangeVaules(List<int> param)
		{
			try
			{
				BlittableListWrapper blittableListWrapper;
				if (param != null)
				{
					fixed (int[] array = NoAllocHelpers.ExtractArrayFromList<int>(param))
					{
						BlittableArrayWrapper blittableArrayWrapper;
						if (array.Length != 0)
						{
							blittableArrayWrapper = new BlittableArrayWrapper((void*)(&array[0]), array.Length);
						}
						blittableListWrapper = new BlittableListWrapper(blittableArrayWrapper, param.Count);
					}
				}
				ValueTypeListOfTTests.ParameterListOfIntReadChangeVaules_Injected(ref blittableListWrapper);
			}
			finally
			{
				BlittableListWrapper blittableListWrapper;
				blittableListWrapper.Unmarshal<int>(param);
			}
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x00003E7C File Offset: 0x0000207C
		[NativeThrows]
		public unsafe static void ParameterListOfIntAddNoGrow(List<int> param)
		{
			try
			{
				BlittableListWrapper blittableListWrapper;
				if (param != null)
				{
					fixed (int[] array = NoAllocHelpers.ExtractArrayFromList<int>(param))
					{
						BlittableArrayWrapper blittableArrayWrapper;
						if (array.Length != 0)
						{
							blittableArrayWrapper = new BlittableArrayWrapper((void*)(&array[0]), array.Length);
						}
						blittableListWrapper = new BlittableListWrapper(blittableArrayWrapper, param.Count);
					}
				}
				ValueTypeListOfTTests.ParameterListOfIntAddNoGrow_Injected(ref blittableListWrapper);
			}
			finally
			{
				BlittableListWrapper blittableListWrapper;
				blittableListWrapper.Unmarshal<int>(param);
			}
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x00003EE0 File Offset: 0x000020E0
		[NativeThrows]
		public unsafe static void ParameterListOfIntAddAndGrow(List<int> param)
		{
			try
			{
				BlittableListWrapper blittableListWrapper;
				if (param != null)
				{
					fixed (int[] array = NoAllocHelpers.ExtractArrayFromList<int>(param))
					{
						BlittableArrayWrapper blittableArrayWrapper;
						if (array.Length != 0)
						{
							blittableArrayWrapper = new BlittableArrayWrapper((void*)(&array[0]), array.Length);
						}
						blittableListWrapper = new BlittableListWrapper(blittableArrayWrapper, param.Count);
					}
				}
				ValueTypeListOfTTests.ParameterListOfIntAddAndGrow_Injected(ref blittableListWrapper);
			}
			finally
			{
				BlittableListWrapper blittableListWrapper;
				blittableListWrapper.Unmarshal<int>(param);
			}
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x00003F44 File Offset: 0x00002144
		[NativeThrows]
		public unsafe static void ParameterListOfIntPassNullThrow([NotNull] List<int> param)
		{
			if (param == null)
			{
				ThrowHelper.ThrowArgumentNullException(param, "param");
			}
			try
			{
				fixed (int[] array = NoAllocHelpers.ExtractArrayFromList<int>(param))
				{
					BlittableArrayWrapper blittableArrayWrapper;
					if (array.Length != 0)
					{
						blittableArrayWrapper = new BlittableArrayWrapper((void*)(&array[0]), array.Length);
					}
					BlittableListWrapper blittableListWrapper = new BlittableListWrapper(blittableArrayWrapper, param.Count);
					ValueTypeListOfTTests.ParameterListOfIntPassNullThrow_Injected(ref blittableListWrapper);
				}
			}
			finally
			{
				BlittableListWrapper blittableListWrapper;
				blittableListWrapper.Unmarshal<int>(param);
			}
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x00003FB0 File Offset: 0x000021B0
		[NativeThrows]
		public unsafe static void ParameterListOfIntPassNullNoThrow(List<int> param)
		{
			try
			{
				BlittableListWrapper blittableListWrapper;
				if (param != null)
				{
					fixed (int[] array = NoAllocHelpers.ExtractArrayFromList<int>(param))
					{
						BlittableArrayWrapper blittableArrayWrapper;
						if (array.Length != 0)
						{
							blittableArrayWrapper = new BlittableArrayWrapper((void*)(&array[0]), array.Length);
						}
						blittableListWrapper = new BlittableListWrapper(blittableArrayWrapper, param.Count);
					}
				}
				ValueTypeListOfTTests.ParameterListOfIntPassNullNoThrow_Injected(ref blittableListWrapper);
			}
			finally
			{
				BlittableListWrapper blittableListWrapper;
				blittableListWrapper.Unmarshal<int>(param);
			}
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x00004014 File Offset: 0x00002214
		[NativeThrows]
		public unsafe static void ParameterListOfIntNativeAllocateSmaller(List<int> param)
		{
			try
			{
				BlittableListWrapper blittableListWrapper;
				if (param != null)
				{
					fixed (int[] array = NoAllocHelpers.ExtractArrayFromList<int>(param))
					{
						BlittableArrayWrapper blittableArrayWrapper;
						if (array.Length != 0)
						{
							blittableArrayWrapper = new BlittableArrayWrapper((void*)(&array[0]), array.Length);
						}
						blittableListWrapper = new BlittableListWrapper(blittableArrayWrapper, param.Count);
					}
				}
				ValueTypeListOfTTests.ParameterListOfIntNativeAllocateSmaller_Injected(ref blittableListWrapper);
			}
			finally
			{
				BlittableListWrapper blittableListWrapper;
				blittableListWrapper.Unmarshal<int>(param);
			}
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x00004078 File Offset: 0x00002278
		[NativeThrows]
		public unsafe static void ParameterListOfIntNativeAttachOtherMemoryBlock(List<int> param)
		{
			try
			{
				BlittableListWrapper blittableListWrapper;
				if (param != null)
				{
					fixed (int[] array = NoAllocHelpers.ExtractArrayFromList<int>(param))
					{
						BlittableArrayWrapper blittableArrayWrapper;
						if (array.Length != 0)
						{
							blittableArrayWrapper = new BlittableArrayWrapper((void*)(&array[0]), array.Length);
						}
						blittableListWrapper = new BlittableListWrapper(blittableArrayWrapper, param.Count);
					}
				}
				ValueTypeListOfTTests.ParameterListOfIntNativeAttachOtherMemoryBlock_Injected(ref blittableListWrapper);
			}
			finally
			{
				BlittableListWrapper blittableListWrapper;
				blittableListWrapper.Unmarshal<int>(param);
			}
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x000040DC File Offset: 0x000022DC
		[NativeThrows]
		public unsafe static void ParameterListOfIntNativeCallsClear(List<int> param)
		{
			try
			{
				BlittableListWrapper blittableListWrapper;
				if (param != null)
				{
					fixed (int[] array = NoAllocHelpers.ExtractArrayFromList<int>(param))
					{
						BlittableArrayWrapper blittableArrayWrapper;
						if (array.Length != 0)
						{
							blittableArrayWrapper = new BlittableArrayWrapper((void*)(&array[0]), array.Length);
						}
						blittableListWrapper = new BlittableListWrapper(blittableArrayWrapper, param.Count);
					}
				}
				ValueTypeListOfTTests.ParameterListOfIntNativeCallsClear_Injected(ref blittableListWrapper);
			}
			finally
			{
				BlittableListWrapper blittableListWrapper;
				blittableListWrapper.Unmarshal<int>(param);
			}
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x00004140 File Offset: 0x00002340
		[NativeThrows]
		public unsafe static void ParameterListOfBoolReadWrite(List<bool> param)
		{
			try
			{
				BlittableListWrapper blittableListWrapper;
				if (param != null)
				{
					fixed (bool[] array = NoAllocHelpers.ExtractArrayFromList<bool>(param))
					{
						BlittableArrayWrapper blittableArrayWrapper;
						if (array.Length != 0)
						{
							blittableArrayWrapper = new BlittableArrayWrapper((void*)(&array[0]), array.Length);
						}
						blittableListWrapper = new BlittableListWrapper(blittableArrayWrapper, param.Count);
					}
				}
				ValueTypeListOfTTests.ParameterListOfBoolReadWrite_Injected(ref blittableListWrapper);
			}
			finally
			{
				BlittableListWrapper blittableListWrapper;
				blittableListWrapper.Unmarshal<bool>(param);
			}
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x000041A4 File Offset: 0x000023A4
		[NativeThrows]
		public unsafe static void ParameterListOfCharReadWrite(List<char> param)
		{
			try
			{
				BlittableListWrapper blittableListWrapper;
				if (param != null)
				{
					fixed (char[] array = NoAllocHelpers.ExtractArrayFromList<char>(param))
					{
						BlittableArrayWrapper blittableArrayWrapper;
						if (array.Length != 0)
						{
							blittableArrayWrapper = new BlittableArrayWrapper((void*)(&array[0]), array.Length);
						}
						blittableListWrapper = new BlittableListWrapper(blittableArrayWrapper, param.Count);
					}
				}
				ValueTypeListOfTTests.ParameterListOfCharReadWrite_Injected(ref blittableListWrapper);
			}
			finally
			{
				BlittableListWrapper blittableListWrapper;
				blittableListWrapper.Unmarshal<char>(param);
			}
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x00004208 File Offset: 0x00002408
		[NativeThrows]
		public unsafe static void ParameterListOfEnumReadWrite(List<SomeEnum> param)
		{
			try
			{
				BlittableListWrapper blittableListWrapper;
				if (param != null)
				{
					fixed (SomeEnum[] array = NoAllocHelpers.ExtractArrayFromList<SomeEnum>(param))
					{
						BlittableArrayWrapper blittableArrayWrapper;
						if (array.Length != 0)
						{
							blittableArrayWrapper = new BlittableArrayWrapper((void*)(&array[0]), array.Length);
						}
						blittableListWrapper = new BlittableListWrapper(blittableArrayWrapper, param.Count);
					}
				}
				ValueTypeListOfTTests.ParameterListOfEnumReadWrite_Injected(ref blittableListWrapper);
			}
			finally
			{
				BlittableListWrapper blittableListWrapper;
				blittableListWrapper.Unmarshal<SomeEnum>(param);
			}
		}

		// Token: 0x060001BA RID: 442 RVA: 0x0000426C File Offset: 0x0000246C
		[NativeThrows]
		public unsafe static void ParameterListOfCornerCaseStructReadWrite(List<BlittableCornerCases> param)
		{
			try
			{
				BlittableListWrapper blittableListWrapper;
				if (param != null)
				{
					fixed (BlittableCornerCases[] array = NoAllocHelpers.ExtractArrayFromList<BlittableCornerCases>(param))
					{
						BlittableArrayWrapper blittableArrayWrapper;
						if (array.Length != 0)
						{
							blittableArrayWrapper = new BlittableArrayWrapper((void*)(&array[0]), array.Length);
						}
						blittableListWrapper = new BlittableListWrapper(blittableArrayWrapper, param.Count);
					}
				}
				ValueTypeListOfTTests.ParameterListOfCornerCaseStructReadWrite_Injected(ref blittableListWrapper);
			}
			finally
			{
				BlittableListWrapper blittableListWrapper;
				blittableListWrapper.Unmarshal<BlittableCornerCases>(param);
			}
		}

		// Token: 0x060001BC RID: 444
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ParameterListOfIntRead_Injected(ref BlittableListWrapper param);

		// Token: 0x060001BD RID: 445
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ParameterListOfIntReadChangeVaules_Injected(ref BlittableListWrapper param);

		// Token: 0x060001BE RID: 446
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ParameterListOfIntAddNoGrow_Injected(ref BlittableListWrapper param);

		// Token: 0x060001BF RID: 447
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ParameterListOfIntAddAndGrow_Injected(ref BlittableListWrapper param);

		// Token: 0x060001C0 RID: 448
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ParameterListOfIntPassNullThrow_Injected(ref BlittableListWrapper param);

		// Token: 0x060001C1 RID: 449
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ParameterListOfIntPassNullNoThrow_Injected(ref BlittableListWrapper param);

		// Token: 0x060001C2 RID: 450
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ParameterListOfIntNativeAllocateSmaller_Injected(ref BlittableListWrapper param);

		// Token: 0x060001C3 RID: 451
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ParameterListOfIntNativeAttachOtherMemoryBlock_Injected(ref BlittableListWrapper param);

		// Token: 0x060001C4 RID: 452
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ParameterListOfIntNativeCallsClear_Injected(ref BlittableListWrapper param);

		// Token: 0x060001C5 RID: 453
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ParameterListOfBoolReadWrite_Injected(ref BlittableListWrapper param);

		// Token: 0x060001C6 RID: 454
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ParameterListOfCharReadWrite_Injected(ref BlittableListWrapper param);

		// Token: 0x060001C7 RID: 455
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ParameterListOfEnumReadWrite_Injected(ref BlittableListWrapper param);

		// Token: 0x060001C8 RID: 456
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ParameterListOfCornerCaseStructReadWrite_Injected(ref BlittableListWrapper param);
	}
}
