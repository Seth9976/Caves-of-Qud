using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000ED RID: 237
	[StructLayout(LayoutKind.Explicit, Pack = 1, Size = 25)]
	public struct StateEvent : IInputEventTypeInfo
	{
		// Token: 0x17000411 RID: 1041
		// (get) Token: 0x06000E40 RID: 3648 RVA: 0x00047956 File Offset: 0x00045B56
		public uint stateSizeInBytes
		{
			get
			{
				return this.baseEvent.sizeInBytes - 24U;
			}
		}

		// Token: 0x17000412 RID: 1042
		// (get) Token: 0x06000E41 RID: 3649 RVA: 0x00047968 File Offset: 0x00045B68
		public unsafe void* state
		{
			get
			{
				fixed (byte* ptr = &this.stateData.FixedElementField)
				{
					return (void*)ptr;
				}
			}
		}

		// Token: 0x06000E42 RID: 3650 RVA: 0x00047984 File Offset: 0x00045B84
		public unsafe InputEventPtr ToEventPtr()
		{
			fixed (StateEvent* ptr = &this)
			{
				return new InputEventPtr((InputEvent*)ptr);
			}
		}

		// Token: 0x17000413 RID: 1043
		// (get) Token: 0x06000E43 RID: 3651 RVA: 0x0004799A File Offset: 0x00045B9A
		public FourCC typeStatic
		{
			get
			{
				return 1398030676;
			}
		}

		// Token: 0x06000E44 RID: 3652 RVA: 0x000479A8 File Offset: 0x00045BA8
		public TState GetState<TState>() where TState : struct, IInputStateTypeInfo
		{
			TState tstate = default(TState);
			if (this.stateFormat != tstate.format)
			{
				throw new InvalidOperationException(string.Format("Expected state format '{0}' but got '{1}' instead", tstate.format, this.stateFormat));
			}
			UnsafeUtility.MemCpy(UnsafeUtility.AddressOf<TState>(ref tstate), this.state, Math.Min((long)((ulong)this.stateSizeInBytes), (long)UnsafeUtility.SizeOf<TState>()));
			return tstate;
		}

		// Token: 0x06000E45 RID: 3653 RVA: 0x00047A29 File Offset: 0x00045C29
		public unsafe static TState GetState<TState>(InputEventPtr ptr) where TState : struct, IInputStateTypeInfo
		{
			return StateEvent.From(ptr)->GetState<TState>();
		}

		// Token: 0x06000E46 RID: 3654 RVA: 0x00047A36 File Offset: 0x00045C36
		public static int GetEventSizeWithPayload<TState>() where TState : struct
		{
			return UnsafeUtility.SizeOf<TState>() + 20 + 4;
		}

		// Token: 0x06000E47 RID: 3655 RVA: 0x00047A44 File Offset: 0x00045C44
		public unsafe static StateEvent* From(InputEventPtr ptr)
		{
			if (!ptr.valid)
			{
				throw new ArgumentNullException("ptr");
			}
			if (!ptr.IsA<StateEvent>())
			{
				throw new InvalidCastException(string.Format("Cannot cast event with type '{0}' into StateEvent", ptr.type));
			}
			return StateEvent.FromUnchecked(ptr);
		}

		// Token: 0x06000E48 RID: 3656 RVA: 0x00047A90 File Offset: 0x00045C90
		internal unsafe static StateEvent* FromUnchecked(InputEventPtr ptr)
		{
			return (StateEvent*)ptr.data;
		}

		// Token: 0x06000E49 RID: 3657 RVA: 0x00047A99 File Offset: 0x00045C99
		public static NativeArray<byte> From(InputDevice device, out InputEventPtr eventPtr, Allocator allocator = Allocator.Temp)
		{
			return StateEvent.From(device, out eventPtr, allocator, false);
		}

		// Token: 0x06000E4A RID: 3658 RVA: 0x00047AA4 File Offset: 0x00045CA4
		public static NativeArray<byte> FromDefaultStateFor(InputDevice device, out InputEventPtr eventPtr, Allocator allocator = Allocator.Temp)
		{
			return StateEvent.From(device, out eventPtr, allocator, true);
		}

		// Token: 0x06000E4B RID: 3659 RVA: 0x00047AB0 File Offset: 0x00045CB0
		private unsafe static NativeArray<byte> From(InputDevice device, out InputEventPtr eventPtr, Allocator allocator, bool useDefaultState)
		{
			if (device == null)
			{
				throw new ArgumentNullException("device");
			}
			if (!device.added)
			{
				throw new ArgumentException(string.Format("Device '{0}' has not been added to system", device), "device");
			}
			FourCC format = device.m_StateBlock.format;
			uint alignedSizeInBytes = device.m_StateBlock.alignedSizeInBytes;
			uint byteOffset = device.m_StateBlock.byteOffset;
			byte* ptr = (byte*)((useDefaultState ? device.defaultStatePtr : device.currentStatePtr) + byteOffset);
			uint num = 24U + alignedSizeInBytes;
			NativeArray<byte> nativeArray = new NativeArray<byte>((int)num.AlignToMultipleOf(4U), allocator, NativeArrayOptions.ClearMemory);
			StateEvent* unsafePtr = (StateEvent*)nativeArray.GetUnsafePtr<byte>();
			unsafePtr->baseEvent = new InputEvent(1398030676, (int)num, device.deviceId, InputRuntime.s_Instance.currentTime);
			unsafePtr->stateFormat = format;
			UnsafeUtility.MemCpy(unsafePtr->state, (void*)ptr, (long)((ulong)alignedSizeInBytes));
			eventPtr = unsafePtr->ToEventPtr();
			return nativeArray;
		}

		// Token: 0x040005E4 RID: 1508
		public const int Type = 1398030676;

		// Token: 0x040005E5 RID: 1509
		internal const int kStateDataSizeToSubtract = 1;

		// Token: 0x040005E6 RID: 1510
		[FieldOffset(0)]
		public InputEvent baseEvent;

		// Token: 0x040005E7 RID: 1511
		[FieldOffset(20)]
		public FourCC stateFormat;

		// Token: 0x040005E8 RID: 1512
		[FixedBuffer(typeof(byte), 1)]
		[FieldOffset(24)]
		internal StateEvent.<stateData>e__FixedBuffer stateData;

		// Token: 0x0200021D RID: 541
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 1)]
		public struct <stateData>e__FixedBuffer
		{
			// Token: 0x04000BC4 RID: 3012
			public byte FixedElementField;
		}
	}
}
