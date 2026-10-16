using System;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000FC RID: 252
	internal struct InputStateBuffers
	{
		// Token: 0x06000EE0 RID: 3808 RVA: 0x00049628 File Offset: 0x00047828
		public InputStateBuffers.DoubleBuffers GetDoubleBuffersFor(InputUpdateType updateType)
		{
			if (updateType - InputUpdateType.Dynamic <= 1 || updateType == InputUpdateType.BeforeRender || updateType == InputUpdateType.Manual)
			{
				return this.m_PlayerStateBuffers;
			}
			throw new ArgumentException("Unrecognized InputUpdateType: " + updateType.ToString(), "updateType");
		}

		// Token: 0x06000EE1 RID: 3809 RVA: 0x00049661 File Offset: 0x00047861
		public unsafe static void* GetFrontBufferForDevice(int deviceIndex)
		{
			return InputStateBuffers.s_CurrentBuffers.GetFrontBuffer(deviceIndex);
		}

		// Token: 0x06000EE2 RID: 3810 RVA: 0x0004966E File Offset: 0x0004786E
		public unsafe static void* GetBackBufferForDevice(int deviceIndex)
		{
			return InputStateBuffers.s_CurrentBuffers.GetBackBuffer(deviceIndex);
		}

		// Token: 0x06000EE3 RID: 3811 RVA: 0x0004967B File Offset: 0x0004787B
		public static void SwitchTo(InputStateBuffers buffers, InputUpdateType update)
		{
			InputStateBuffers.s_CurrentBuffers = buffers.GetDoubleBuffersFor(update);
		}

		// Token: 0x06000EE4 RID: 3812 RVA: 0x0004968C File Offset: 0x0004788C
		public unsafe void AllocateAll(InputDevice[] devices, int deviceCount)
		{
			this.sizePerBuffer = InputStateBuffers.ComputeSizeOfSingleStateBuffer(devices, deviceCount);
			if (this.sizePerBuffer == 0U)
			{
				return;
			}
			this.sizePerBuffer = this.sizePerBuffer.AlignToMultipleOf(4U);
			uint num = (uint)(deviceCount * sizeof(void*) * 2);
			this.totalSize = 0U;
			this.totalSize += this.sizePerBuffer * 2U;
			this.totalSize += num;
			this.totalSize += this.sizePerBuffer * 3U;
			this.m_AllBuffers = UnsafeUtility.Malloc((long)((ulong)this.totalSize), 4, Allocator.Persistent);
			UnsafeUtility.MemClear(this.m_AllBuffers, (long)((ulong)this.totalSize));
			byte* allBuffers = (byte*)this.m_AllBuffers;
			this.m_PlayerStateBuffers = InputStateBuffers.SetUpDeviceToBufferMappings(deviceCount, ref allBuffers, this.sizePerBuffer, num);
			this.defaultStateBuffer = (void*)allBuffers;
			this.noiseMaskBuffer = (void*)(allBuffers + this.sizePerBuffer);
			this.resetMaskBuffer = (void*)(allBuffers + this.sizePerBuffer * 2U);
		}

		// Token: 0x06000EE5 RID: 3813 RVA: 0x00049774 File Offset: 0x00047974
		private unsafe static InputStateBuffers.DoubleBuffers SetUpDeviceToBufferMappings(int deviceCount, ref byte* bufferPtr, uint sizePerBuffer, uint mappingTableSizePerBuffer)
		{
			byte* ptr = bufferPtr;
			byte* ptr2 = bufferPtr + sizePerBuffer;
			void** ptr3 = bufferPtr / (IntPtr)sizeof(void*) + sizePerBuffer * 2U;
			bufferPtr += (IntPtr)((UIntPtr)(sizePerBuffer * 2U + mappingTableSizePerBuffer));
			InputStateBuffers.DoubleBuffers doubleBuffers = new InputStateBuffers.DoubleBuffers
			{
				deviceToBufferMapping = ptr3,
				deviceCount = deviceCount
			};
			for (int i = 0; i < deviceCount; i++)
			{
				int num = i;
				doubleBuffers.SetFrontBuffer(num, (void*)ptr);
				doubleBuffers.SetBackBuffer(num, (void*)ptr2);
			}
			return doubleBuffers;
		}

		// Token: 0x06000EE6 RID: 3814 RVA: 0x000497E4 File Offset: 0x000479E4
		public void FreeAll()
		{
			if (this.m_AllBuffers != null)
			{
				UnsafeUtility.Free(this.m_AllBuffers, Allocator.Persistent);
				this.m_AllBuffers = null;
			}
			this.m_PlayerStateBuffers = default(InputStateBuffers.DoubleBuffers);
			InputStateBuffers.s_CurrentBuffers = default(InputStateBuffers.DoubleBuffers);
			if (InputStateBuffers.s_DefaultStateBuffer == this.defaultStateBuffer)
			{
				InputStateBuffers.s_DefaultStateBuffer = null;
			}
			this.defaultStateBuffer = null;
			if (InputStateBuffers.s_NoiseMaskBuffer == this.noiseMaskBuffer)
			{
				InputStateBuffers.s_NoiseMaskBuffer = null;
			}
			if (InputStateBuffers.s_ResetMaskBuffer == this.resetMaskBuffer)
			{
				InputStateBuffers.s_ResetMaskBuffer = null;
			}
			this.noiseMaskBuffer = null;
			this.resetMaskBuffer = null;
			this.totalSize = 0U;
			this.sizePerBuffer = 0U;
		}

		// Token: 0x06000EE7 RID: 3815 RVA: 0x00049888 File Offset: 0x00047A88
		public void MigrateAll(InputDevice[] devices, int deviceCount, InputStateBuffers oldBuffers)
		{
			if (oldBuffers.totalSize > 0U)
			{
				InputStateBuffers.MigrateDoubleBuffer(this.m_PlayerStateBuffers, devices, deviceCount, oldBuffers.m_PlayerStateBuffers);
				InputStateBuffers.MigrateSingleBuffer(this.defaultStateBuffer, devices, deviceCount, oldBuffers.defaultStateBuffer);
				InputStateBuffers.MigrateSingleBuffer(this.noiseMaskBuffer, devices, deviceCount, oldBuffers.noiseMaskBuffer);
				InputStateBuffers.MigrateSingleBuffer(this.resetMaskBuffer, devices, deviceCount, oldBuffers.resetMaskBuffer);
			}
			uint num = 0U;
			for (int i = 0; i < deviceCount; i++)
			{
				InputDevice inputDevice = devices[i];
				uint byteOffset = inputDevice.m_StateBlock.byteOffset;
				if (byteOffset == 4294967295U)
				{
					inputDevice.m_StateBlock.byteOffset = 0U;
					if (num != 0U)
					{
						inputDevice.BakeOffsetIntoStateBlockRecursive(num);
					}
				}
				else
				{
					uint num2 = num - byteOffset;
					if (num2 != 0U)
					{
						inputDevice.BakeOffsetIntoStateBlockRecursive(num2);
					}
				}
				num = InputStateBuffers.NextDeviceOffset(num, inputDevice);
			}
		}

		// Token: 0x06000EE8 RID: 3816 RVA: 0x00049940 File Offset: 0x00047B40
		private unsafe static void MigrateDoubleBuffer(InputStateBuffers.DoubleBuffers newBuffer, InputDevice[] devices, int deviceCount, InputStateBuffers.DoubleBuffers oldBuffer)
		{
			if (!newBuffer.valid)
			{
				return;
			}
			if (!oldBuffer.valid)
			{
				return;
			}
			uint num = 0U;
			for (int i = 0; i < deviceCount; i++)
			{
				InputDevice inputDevice = devices[i];
				if (inputDevice.m_StateBlock.byteOffset == 4294967295U)
				{
					break;
				}
				int deviceIndex = inputDevice.m_DeviceIndex;
				int num2 = i;
				uint alignedSizeInBytes = inputDevice.m_StateBlock.alignedSizeInBytes;
				byte* ptr = (byte*)oldBuffer.GetFrontBuffer(deviceIndex) + inputDevice.m_StateBlock.byteOffset;
				byte* ptr2 = (byte*)oldBuffer.GetBackBuffer(deviceIndex) + inputDevice.m_StateBlock.byteOffset;
				byte* ptr3 = (byte*)newBuffer.GetFrontBuffer(num2) + num;
				void* ptr4 = (void*)((byte*)newBuffer.GetBackBuffer(num2) + num);
				UnsafeUtility.MemCpy((void*)ptr3, (void*)ptr, (long)((ulong)alignedSizeInBytes));
				UnsafeUtility.MemCpy(ptr4, (void*)ptr2, (long)((ulong)alignedSizeInBytes));
				num = InputStateBuffers.NextDeviceOffset(num, inputDevice);
			}
		}

		// Token: 0x06000EE9 RID: 3817 RVA: 0x00049A04 File Offset: 0x00047C04
		private unsafe static void MigrateSingleBuffer(void* newBuffer, InputDevice[] devices, int deviceCount, void* oldBuffer)
		{
			uint num = 0U;
			for (int i = 0; i < deviceCount; i++)
			{
				InputDevice inputDevice = devices[i];
				if (inputDevice.m_StateBlock.byteOffset == 4294967295U)
				{
					break;
				}
				uint alignedSizeInBytes = inputDevice.m_StateBlock.alignedSizeInBytes;
				byte* ptr = (byte*)oldBuffer + inputDevice.m_StateBlock.byteOffset;
				UnsafeUtility.MemCpy((void*)((byte*)newBuffer + num), (void*)ptr, (long)((ulong)alignedSizeInBytes));
				num = InputStateBuffers.NextDeviceOffset(num, inputDevice);
			}
		}

		// Token: 0x06000EEA RID: 3818 RVA: 0x00049A64 File Offset: 0x00047C64
		private static uint ComputeSizeOfSingleStateBuffer(InputDevice[] devices, int deviceCount)
		{
			uint num = 0U;
			for (int i = 0; i < deviceCount; i++)
			{
				num = InputStateBuffers.NextDeviceOffset(num, devices[i]);
			}
			return num;
		}

		// Token: 0x06000EEB RID: 3819 RVA: 0x00049A8C File Offset: 0x00047C8C
		private static uint NextDeviceOffset(uint currentOffset, InputDevice device)
		{
			uint alignedSizeInBytes = device.m_StateBlock.alignedSizeInBytes;
			if (alignedSizeInBytes == 0U)
			{
				throw new ArgumentException(string.Format("Device '{0}' has a zero-size state buffer", device), "device");
			}
			return currentOffset + alignedSizeInBytes.AlignToMultipleOf(4U);
		}

		// Token: 0x04000639 RID: 1593
		public uint sizePerBuffer;

		// Token: 0x0400063A RID: 1594
		public uint totalSize;

		// Token: 0x0400063B RID: 1595
		public unsafe void* defaultStateBuffer;

		// Token: 0x0400063C RID: 1596
		public unsafe void* noiseMaskBuffer;

		// Token: 0x0400063D RID: 1597
		public unsafe void* resetMaskBuffer;

		// Token: 0x0400063E RID: 1598
		private unsafe void* m_AllBuffers;

		// Token: 0x0400063F RID: 1599
		internal InputStateBuffers.DoubleBuffers m_PlayerStateBuffers;

		// Token: 0x04000640 RID: 1600
		internal unsafe static void* s_DefaultStateBuffer;

		// Token: 0x04000641 RID: 1601
		internal unsafe static void* s_NoiseMaskBuffer;

		// Token: 0x04000642 RID: 1602
		internal unsafe static void* s_ResetMaskBuffer;

		// Token: 0x04000643 RID: 1603
		internal static InputStateBuffers.DoubleBuffers s_CurrentBuffers;

		// Token: 0x02000224 RID: 548
		[Serializable]
		internal struct DoubleBuffers
		{
			// Token: 0x170005BE RID: 1470
			// (get) Token: 0x06001553 RID: 5459 RVA: 0x00063655 File Offset: 0x00061855
			public bool valid
			{
				get
				{
					return this.deviceToBufferMapping != null;
				}
			}

			// Token: 0x06001554 RID: 5460 RVA: 0x00063664 File Offset: 0x00061864
			public unsafe void SetFrontBuffer(int deviceIndex, void* ptr)
			{
				if (deviceIndex < this.deviceCount)
				{
					*(IntPtr*)(this.deviceToBufferMapping + (IntPtr)(deviceIndex * 2) * (IntPtr)sizeof(void*) / (IntPtr)sizeof(void*)) = ptr;
				}
			}

			// Token: 0x06001555 RID: 5461 RVA: 0x00063683 File Offset: 0x00061883
			public unsafe void SetBackBuffer(int deviceIndex, void* ptr)
			{
				if (deviceIndex < this.deviceCount)
				{
					*(IntPtr*)(this.deviceToBufferMapping + (IntPtr)(deviceIndex * 2 + 1) * (IntPtr)sizeof(void*) / (IntPtr)sizeof(void*)) = ptr;
				}
			}

			// Token: 0x06001556 RID: 5462 RVA: 0x000636A4 File Offset: 0x000618A4
			public unsafe void* GetFrontBuffer(int deviceIndex)
			{
				if (deviceIndex < this.deviceCount)
				{
					return *(IntPtr*)(this.deviceToBufferMapping + (IntPtr)(deviceIndex * 2) * (IntPtr)sizeof(void*) / (IntPtr)sizeof(void*));
				}
				return null;
			}

			// Token: 0x06001557 RID: 5463 RVA: 0x000636C5 File Offset: 0x000618C5
			public unsafe void* GetBackBuffer(int deviceIndex)
			{
				if (deviceIndex < this.deviceCount)
				{
					return *(IntPtr*)(this.deviceToBufferMapping + (IntPtr)(deviceIndex * 2 + 1) * (IntPtr)sizeof(void*) / (IntPtr)sizeof(void*));
				}
				return null;
			}

			// Token: 0x06001558 RID: 5464 RVA: 0x000636E8 File Offset: 0x000618E8
			public unsafe void SwapBuffers(int deviceIndex)
			{
				if (!this.valid)
				{
					return;
				}
				void* frontBuffer = this.GetFrontBuffer(deviceIndex);
				void* backBuffer = this.GetBackBuffer(deviceIndex);
				this.SetFrontBuffer(deviceIndex, backBuffer);
				this.SetBackBuffer(deviceIndex, frontBuffer);
			}

			// Token: 0x04000BCE RID: 3022
			public unsafe void** deviceToBufferMapping;

			// Token: 0x04000BCF RID: 3023
			public int deviceCount;
		}
	}
}
