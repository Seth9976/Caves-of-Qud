using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000DF RID: 223
	[StructLayout(LayoutKind.Explicit, Pack = 1, Size = 29)]
	public struct DeltaStateEvent : IInputEventTypeInfo
	{
		// Token: 0x170003DC RID: 988
		// (get) Token: 0x06000DAB RID: 3499 RVA: 0x000456D5 File Offset: 0x000438D5
		public uint deltaStateSizeInBytes
		{
			get
			{
				return this.baseEvent.sizeInBytes - 28U;
			}
		}

		// Token: 0x170003DD RID: 989
		// (get) Token: 0x06000DAC RID: 3500 RVA: 0x000456E8 File Offset: 0x000438E8
		public unsafe void* deltaState
		{
			get
			{
				fixed (byte* ptr = &this.stateData.FixedElementField)
				{
					return (void*)ptr;
				}
			}
		}

		// Token: 0x170003DE RID: 990
		// (get) Token: 0x06000DAD RID: 3501 RVA: 0x00045703 File Offset: 0x00043903
		public FourCC typeStatic
		{
			get
			{
				return 1145852993;
			}
		}

		// Token: 0x06000DAE RID: 3502 RVA: 0x00045710 File Offset: 0x00043910
		public unsafe InputEventPtr ToEventPtr()
		{
			fixed (DeltaStateEvent* ptr = &this)
			{
				return new InputEventPtr((InputEvent*)ptr);
			}
		}

		// Token: 0x06000DAF RID: 3503 RVA: 0x00045728 File Offset: 0x00043928
		public unsafe static DeltaStateEvent* From(InputEventPtr ptr)
		{
			if (!ptr.valid)
			{
				throw new ArgumentNullException("ptr");
			}
			if (!ptr.IsA<DeltaStateEvent>())
			{
				throw new InvalidCastException(string.Format("Cannot cast event with type '{0}' into DeltaStateEvent", ptr.type));
			}
			return DeltaStateEvent.FromUnchecked(ptr);
		}

		// Token: 0x06000DB0 RID: 3504 RVA: 0x00045774 File Offset: 0x00043974
		internal unsafe static DeltaStateEvent* FromUnchecked(InputEventPtr ptr)
		{
			return (DeltaStateEvent*)ptr.data;
		}

		// Token: 0x06000DB1 RID: 3505 RVA: 0x00045780 File Offset: 0x00043980
		public unsafe static NativeArray<byte> From(InputControl control, out InputEventPtr eventPtr, Allocator allocator = Allocator.Temp)
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			InputDevice device = control.device;
			if (!device.added)
			{
				throw new ArgumentException(string.Format("Device for control '{0}' has not been added to system", control), "control");
			}
			if (control.currentStatePtr == null)
			{
				throw new ArgumentNullException(string.Format("Control '{0}' does not have an associated state", control));
			}
			ref InputStateBlock ptr = ref device.m_StateBlock;
			ref InputStateBlock ptr2 = ref control.m_StateBlock;
			FourCC format = ptr.format;
			uint num;
			if (ptr2.bitOffset != 0U)
			{
				num = (ptr2.bitOffset + ptr2.sizeInBits + 7U) / 8U;
			}
			else
			{
				num = ptr2.alignedSizeInBytes;
			}
			uint byteOffset = ptr2.byteOffset;
			byte* ptr3 = (byte*)control.currentStatePtr + byteOffset;
			uint num2 = 28U + num;
			NativeArray<byte> nativeArray = new NativeArray<byte>((int)num2.AlignToMultipleOf(4U), allocator, NativeArrayOptions.ClearMemory);
			DeltaStateEvent* unsafePtr = (DeltaStateEvent*)nativeArray.GetUnsafePtr<byte>();
			unsafePtr->baseEvent = new InputEvent(1145852993, (int)num2, device.deviceId, InputRuntime.s_Instance.currentTime);
			unsafePtr->stateFormat = format;
			unsafePtr->stateOffset = ptr2.byteOffset - ptr.byteOffset;
			UnsafeUtility.MemCpy(unsafePtr->deltaState, (void*)ptr3, (long)((ulong)num));
			eventPtr = unsafePtr->ToEventPtr();
			return nativeArray;
		}

		// Token: 0x040005A4 RID: 1444
		public const int Type = 1145852993;

		// Token: 0x040005A5 RID: 1445
		[FieldOffset(0)]
		public InputEvent baseEvent;

		// Token: 0x040005A6 RID: 1446
		[FieldOffset(20)]
		public FourCC stateFormat;

		// Token: 0x040005A7 RID: 1447
		[FieldOffset(24)]
		public uint stateOffset;

		// Token: 0x040005A8 RID: 1448
		[FixedBuffer(typeof(byte), 1)]
		[FieldOffset(28)]
		internal DeltaStateEvent.<stateData>e__FixedBuffer stateData;

		// Token: 0x02000213 RID: 531
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 1)]
		public struct <stateData>e__FixedBuffer
		{
			// Token: 0x04000BA0 RID: 2976
			public byte FixedElementField;
		}
	}
}
