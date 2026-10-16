using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Profiling;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000EC RID: 236
	[Serializable]
	public sealed class InputEventTrace : IDisposable, IEnumerable<InputEventPtr>, IEnumerable
	{
		// Token: 0x17000403 RID: 1027
		// (get) Token: 0x06000E14 RID: 3604 RVA: 0x00046A79 File Offset: 0x00044C79
		public static FourCC FrameMarkerEvent
		{
			get
			{
				return new FourCC('F', 'R', 'M', 'E');
			}
		}

		// Token: 0x17000404 RID: 1028
		// (get) Token: 0x06000E15 RID: 3605 RVA: 0x00046A88 File Offset: 0x00044C88
		// (set) Token: 0x06000E16 RID: 3606 RVA: 0x00046A90 File Offset: 0x00044C90
		public int deviceId
		{
			get
			{
				return this.m_DeviceId;
			}
			set
			{
				this.m_DeviceId = value;
			}
		}

		// Token: 0x17000405 RID: 1029
		// (get) Token: 0x06000E17 RID: 3607 RVA: 0x00046A99 File Offset: 0x00044C99
		public bool enabled
		{
			get
			{
				return this.m_Enabled;
			}
		}

		// Token: 0x17000406 RID: 1030
		// (get) Token: 0x06000E18 RID: 3608 RVA: 0x00046AA1 File Offset: 0x00044CA1
		// (set) Token: 0x06000E19 RID: 3609 RVA: 0x00046AAC File Offset: 0x00044CAC
		public bool recordFrameMarkers
		{
			get
			{
				return this.m_RecordFrameMarkers;
			}
			set
			{
				if (this.m_RecordFrameMarkers == value)
				{
					return;
				}
				this.m_RecordFrameMarkers = value;
				if (this.m_Enabled)
				{
					if (value)
					{
						InputSystem.onBeforeUpdate += this.OnBeforeUpdate;
						return;
					}
					InputSystem.onBeforeUpdate -= this.OnBeforeUpdate;
				}
			}
		}

		// Token: 0x17000407 RID: 1031
		// (get) Token: 0x06000E1A RID: 3610 RVA: 0x00046AF8 File Offset: 0x00044CF8
		public long eventCount
		{
			get
			{
				return this.m_EventCount;
			}
		}

		// Token: 0x17000408 RID: 1032
		// (get) Token: 0x06000E1B RID: 3611 RVA: 0x00046B00 File Offset: 0x00044D00
		public long totalEventSizeInBytes
		{
			get
			{
				return this.m_EventSizeInBytes;
			}
		}

		// Token: 0x17000409 RID: 1033
		// (get) Token: 0x06000E1C RID: 3612 RVA: 0x00046B08 File Offset: 0x00044D08
		public long allocatedSizeInBytes
		{
			get
			{
				if (this.m_EventBuffer == null)
				{
					return 0L;
				}
				return this.m_EventBufferSize;
			}
		}

		// Token: 0x1700040A RID: 1034
		// (get) Token: 0x06000E1D RID: 3613 RVA: 0x00046B1D File Offset: 0x00044D1D
		public long maxSizeInBytes
		{
			get
			{
				return this.m_MaxEventBufferSize;
			}
		}

		// Token: 0x1700040B RID: 1035
		// (get) Token: 0x06000E1E RID: 3614 RVA: 0x00046B25 File Offset: 0x00044D25
		public ReadOnlyArray<InputEventTrace.DeviceInfo> deviceInfos
		{
			get
			{
				return this.m_DeviceInfos;
			}
		}

		// Token: 0x1700040C RID: 1036
		// (get) Token: 0x06000E1F RID: 3615 RVA: 0x00046B32 File Offset: 0x00044D32
		// (set) Token: 0x06000E20 RID: 3616 RVA: 0x00046B3A File Offset: 0x00044D3A
		public Func<InputEventPtr, InputDevice, bool> onFilterEvent
		{
			get
			{
				return this.m_OnFilterEvent;
			}
			set
			{
				this.m_OnFilterEvent = value;
			}
		}

		// Token: 0x14000026 RID: 38
		// (add) Token: 0x06000E21 RID: 3617 RVA: 0x00046B43 File Offset: 0x00044D43
		// (remove) Token: 0x06000E22 RID: 3618 RVA: 0x00046B51 File Offset: 0x00044D51
		public event Action<InputEventPtr> onEvent
		{
			add
			{
				this.m_EventListeners.AddCallback(value);
			}
			remove
			{
				this.m_EventListeners.RemoveCallback(value);
			}
		}

		// Token: 0x06000E23 RID: 3619 RVA: 0x00046B5F File Offset: 0x00044D5F
		public InputEventTrace(InputDevice device, long bufferSizeInBytes = 1048576L, bool growBuffer = false, long maxBufferSizeInBytes = -1L, long growIncrementSizeInBytes = -1L)
			: this(bufferSizeInBytes, growBuffer, maxBufferSizeInBytes, growIncrementSizeInBytes)
		{
			if (device == null)
			{
				throw new ArgumentNullException("device");
			}
			this.m_DeviceId = device.deviceId;
		}

		// Token: 0x06000E24 RID: 3620 RVA: 0x00046B88 File Offset: 0x00044D88
		public InputEventTrace(long bufferSizeInBytes = 1048576L, bool growBuffer = false, long maxBufferSizeInBytes = -1L, long growIncrementSizeInBytes = -1L)
		{
			this.m_EventBufferSize = (long)((ulong)((uint)bufferSizeInBytes));
			if (!growBuffer)
			{
				this.m_MaxEventBufferSize = this.m_EventBufferSize;
				return;
			}
			if (maxBufferSizeInBytes < 0L)
			{
				this.m_MaxEventBufferSize = 268435456L;
			}
			else
			{
				this.m_MaxEventBufferSize = maxBufferSizeInBytes;
			}
			if (growIncrementSizeInBytes < 0L)
			{
				this.m_GrowIncrementSize = 1048576L;
				return;
			}
			this.m_GrowIncrementSize = growIncrementSizeInBytes;
		}

		// Token: 0x06000E25 RID: 3621 RVA: 0x00046BEC File Offset: 0x00044DEC
		public void WriteTo(string filePath)
		{
			if (string.IsNullOrEmpty(filePath))
			{
				throw new ArgumentNullException("filePath");
			}
			using (FileStream fileStream = File.OpenWrite(filePath))
			{
				this.WriteTo(fileStream);
			}
		}

		// Token: 0x06000E26 RID: 3622 RVA: 0x00046C38 File Offset: 0x00044E38
		public unsafe void WriteTo(Stream stream)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			if (!stream.CanSeek)
			{
				throw new ArgumentException("Stream does not support seeking", "stream");
			}
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			InputEventTrace.FileFlags fileFlags = (InputEventTrace.FileFlags)0;
			if (InputSystem.settings.updateMode == InputSettings.UpdateMode.ProcessEventsInFixedUpdate)
			{
				fileFlags |= InputEventTrace.FileFlags.FixedUpdate;
			}
			binaryWriter.Write(InputEventTrace.kFileFormat);
			binaryWriter.Write(InputEventTrace.kFileVersion);
			binaryWriter.Write((int)fileFlags);
			binaryWriter.Write((int)Application.platform);
			binaryWriter.Write((ulong)this.m_EventCount);
			binaryWriter.Write((ulong)this.m_EventSizeInBytes);
			foreach (InputEventPtr inputEventPtr in this)
			{
				uint sizeInBytes = inputEventPtr.sizeInBytes;
				byte[] array = new byte[sizeInBytes];
				try
				{
					byte[] array2;
					byte* ptr;
					if ((array2 = array) == null || array2.Length == 0)
					{
						ptr = null;
					}
					else
					{
						ptr = &array2[0];
					}
					UnsafeUtility.MemCpy((void*)ptr, (void*)inputEventPtr.data, (long)((ulong)sizeInBytes));
					binaryWriter.Write(array);
				}
				finally
				{
					byte[] array2 = null;
				}
			}
			binaryWriter.Flush();
			long position = stream.Position;
			int num = this.m_DeviceInfos.LengthSafe<InputEventTrace.DeviceInfo>();
			binaryWriter.Write(num);
			for (int i = 0; i < num; i++)
			{
				ref InputEventTrace.DeviceInfo ptr2 = ref this.m_DeviceInfos[i];
				binaryWriter.Write(ptr2.deviceId);
				binaryWriter.Write(ptr2.layout);
				binaryWriter.Write(ptr2.stateFormat);
				binaryWriter.Write(ptr2.stateSizeInBytes);
				binaryWriter.Write(ptr2.m_FullLayoutJson ?? string.Empty);
			}
			binaryWriter.Flush();
			long num2 = stream.Position - position;
			binaryWriter.Write(num2);
		}

		// Token: 0x06000E27 RID: 3623 RVA: 0x00046E08 File Offset: 0x00045008
		public void ReadFrom(string filePath)
		{
			if (string.IsNullOrEmpty(filePath))
			{
				throw new ArgumentNullException("filePath");
			}
			using (FileStream fileStream = File.OpenRead(filePath))
			{
				this.ReadFrom(fileStream);
			}
		}

		// Token: 0x06000E28 RID: 3624 RVA: 0x00046E54 File Offset: 0x00045054
		public unsafe void ReadFrom(Stream stream)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			if (!stream.CanRead)
			{
				throw new ArgumentException("Stream does not support reading", "stream");
			}
			BinaryReader binaryReader = new BinaryReader(stream);
			if (binaryReader.ReadInt32() != InputEventTrace.kFileFormat)
			{
				throw new IOException(string.Format("Stream does not appear to be an InputEventTrace (no '{0}' code)", InputEventTrace.kFileFormat));
			}
			if (binaryReader.ReadInt32() > InputEventTrace.kFileVersion)
			{
				throw new IOException(string.Format("Stream is an InputEventTrace but a newer version (expected version {0} or below)", InputEventTrace.kFileVersion));
			}
			binaryReader.ReadInt32();
			binaryReader.ReadInt32();
			ulong num = binaryReader.ReadUInt64();
			ulong num2 = binaryReader.ReadUInt64();
			byte* eventBuffer = this.m_EventBuffer;
			if (num > 0UL && num2 > 0UL)
			{
				byte* ptr;
				if (this.m_EventBuffer != null && this.m_EventBufferSize >= (long)num2)
				{
					ptr = this.m_EventBuffer;
				}
				else
				{
					ptr = (byte*)UnsafeUtility.Malloc((long)num2, 4, Allocator.Persistent);
					this.m_EventBufferSize = (long)num2;
				}
				try
				{
					byte* ptr2 = ptr;
					byte* ptr3 = ptr2 + num2;
					long num3 = 0L;
					for (ulong num4 = 0UL; num4 < num; num4 += 1UL)
					{
						int num5 = binaryReader.ReadInt32();
						uint num6 = (uint)binaryReader.ReadUInt16();
						uint num7 = (uint)binaryReader.ReadUInt16();
						if ((ulong)num6 > (ulong)((long)(ptr3 - ptr2)))
						{
							break;
						}
						*(int*)ptr2 = num5;
						ptr2 += 4;
						*(short*)ptr2 = (short)((ushort)num6);
						ptr2 += 2;
						*(short*)ptr2 = (short)((ushort)num7);
						ptr2 += 2;
						int num8 = (int)(num6 - 4U - 2U - 2U);
						byte[] array = binaryReader.ReadBytes(num8);
						try
						{
							byte[] array2;
							byte* ptr4;
							if ((array2 = array) == null || array2.Length == 0)
							{
								ptr4 = null;
							}
							else
							{
								ptr4 = &array2[0];
							}
							UnsafeUtility.MemCpy((void*)ptr2, (void*)ptr4, (long)num8);
						}
						finally
						{
							byte[] array2 = null;
						}
						ptr2 += num8.AlignToMultipleOf(4);
						num3 += (long)((ulong)num6.AlignToMultipleOf(4U));
						if (ptr2 >= ptr3)
						{
							break;
						}
					}
					int num9 = binaryReader.ReadInt32();
					InputEventTrace.DeviceInfo[] array3 = new InputEventTrace.DeviceInfo[num9];
					for (int i = 0; i < num9; i++)
					{
						array3[i] = new InputEventTrace.DeviceInfo
						{
							deviceId = binaryReader.ReadInt32(),
							layout = binaryReader.ReadString(),
							stateFormat = binaryReader.ReadInt32(),
							stateSizeInBytes = binaryReader.ReadInt32(),
							m_FullLayoutJson = binaryReader.ReadString()
						};
					}
					this.m_EventBuffer = ptr;
					this.m_EventBufferHead = this.m_EventBuffer;
					this.m_EventBufferTail = ptr3;
					this.m_EventCount = (long)num;
					this.m_EventSizeInBytes = num3;
					this.m_DeviceInfos = array3;
					goto IL_0296;
				}
				catch
				{
					if (ptr != eventBuffer)
					{
						UnsafeUtility.Free((void*)ptr, Allocator.Persistent);
					}
					throw;
				}
			}
			this.m_EventBuffer = null;
			this.m_EventBufferHead = null;
			this.m_EventBufferTail = null;
			IL_0296:
			if (this.m_EventBuffer != eventBuffer && eventBuffer != null)
			{
				UnsafeUtility.Free((void*)eventBuffer, Allocator.Persistent);
			}
			this.m_ChangeCounter++;
		}

		// Token: 0x06000E29 RID: 3625 RVA: 0x00047150 File Offset: 0x00045350
		public static InputEventTrace LoadFrom(string filePath)
		{
			if (string.IsNullOrEmpty(filePath))
			{
				throw new ArgumentNullException("filePath");
			}
			InputEventTrace inputEventTrace;
			using (FileStream fileStream = File.OpenRead(filePath))
			{
				inputEventTrace = InputEventTrace.LoadFrom(fileStream);
			}
			return inputEventTrace;
		}

		// Token: 0x06000E2A RID: 3626 RVA: 0x0004719C File Offset: 0x0004539C
		public static InputEventTrace LoadFrom(Stream stream)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			if (!stream.CanRead)
			{
				throw new ArgumentException("Stream must be readable", "stream");
			}
			InputEventTrace inputEventTrace = new InputEventTrace(1048576L, false, -1L, -1L);
			inputEventTrace.ReadFrom(stream);
			return inputEventTrace;
		}

		// Token: 0x06000E2B RID: 3627 RVA: 0x000471DB File Offset: 0x000453DB
		public InputEventTrace.ReplayController Replay()
		{
			this.Disable();
			return new InputEventTrace.ReplayController(this);
		}

		// Token: 0x06000E2C RID: 3628 RVA: 0x000471EC File Offset: 0x000453EC
		public unsafe bool Resize(long newBufferSize, long newMaxBufferSize = -1L)
		{
			if (newBufferSize <= 0L)
			{
				throw new ArgumentException("Size must be positive", "newBufferSize");
			}
			if (this.m_EventBufferSize == newBufferSize)
			{
				return true;
			}
			if (newMaxBufferSize < newBufferSize)
			{
				newMaxBufferSize = newBufferSize;
			}
			byte* ptr = (byte*)UnsafeUtility.Malloc(newBufferSize, 4, Allocator.Persistent);
			if (ptr == null)
			{
				return false;
			}
			if (this.m_EventCount > 0L)
			{
				if (newBufferSize < this.m_EventBufferSize || this.m_HasWrapped)
				{
					InputEventPtr inputEventPtr = new InputEventPtr((InputEvent*)this.m_EventBufferHead);
					InputEvent* ptr2 = (InputEvent*)ptr;
					int num = 0;
					int num2 = 0;
					long num3 = this.m_EventSizeInBytes;
					int num4 = 0;
					while ((long)num4 < this.m_EventCount)
					{
						uint sizeInBytes = inputEventPtr.sizeInBytes;
						uint num5 = sizeInBytes.AlignToMultipleOf(4U);
						if (num3 <= newBufferSize)
						{
							UnsafeUtility.MemCpy((void*)ptr2, (void*)inputEventPtr.ToPointer(), (long)((ulong)sizeInBytes));
							ptr2 = InputEvent.GetNextInMemory(ptr2);
							num2 += (int)num5;
							num++;
						}
						num3 -= (long)((ulong)num5);
						if (!this.GetNextEvent(ref inputEventPtr))
						{
							break;
						}
						num4++;
					}
					this.m_HasWrapped = false;
					this.m_EventCount = (long)num;
					this.m_EventSizeInBytes = (long)num2;
				}
				else
				{
					UnsafeUtility.MemCpy((void*)ptr, (void*)this.m_EventBufferHead, this.m_EventSizeInBytes);
				}
			}
			if (this.m_EventBuffer != null)
			{
				UnsafeUtility.Free((void*)this.m_EventBuffer, Allocator.Persistent);
			}
			this.m_EventBufferSize = newBufferSize;
			this.m_EventBuffer = ptr;
			this.m_EventBufferHead = ptr;
			this.m_EventBufferTail = this.m_EventBuffer + this.m_EventSizeInBytes;
			this.m_MaxEventBufferSize = newMaxBufferSize;
			this.m_ChangeCounter++;
			return true;
		}

		// Token: 0x06000E2D RID: 3629 RVA: 0x00047350 File Offset: 0x00045550
		public unsafe void Clear()
		{
			byte* ptr = default(byte*);
			this.m_EventBufferTail = ptr;
			this.m_EventBufferHead = ptr;
			this.m_EventCount = 0L;
			this.m_EventSizeInBytes = 0L;
			this.m_ChangeCounter++;
			this.m_DeviceInfos = null;
		}

		// Token: 0x06000E2E RID: 3630 RVA: 0x00047398 File Offset: 0x00045598
		public void Enable()
		{
			if (this.m_Enabled)
			{
				return;
			}
			if (this.m_EventBuffer == null)
			{
				this.Allocate();
			}
			InputSystem.onEvent += new Action<InputEventPtr, InputDevice>(this.OnInputEvent);
			if (this.m_RecordFrameMarkers)
			{
				InputSystem.onBeforeUpdate += this.OnBeforeUpdate;
			}
			this.m_Enabled = true;
		}

		// Token: 0x06000E2F RID: 3631 RVA: 0x000473F9 File Offset: 0x000455F9
		public void Disable()
		{
			if (!this.m_Enabled)
			{
				return;
			}
			InputSystem.onEvent -= new Action<InputEventPtr, InputDevice>(this.OnInputEvent);
			InputSystem.onBeforeUpdate -= this.OnBeforeUpdate;
			this.m_Enabled = false;
		}

		// Token: 0x06000E30 RID: 3632 RVA: 0x00047438 File Offset: 0x00045638
		public unsafe bool GetNextEvent(ref InputEventPtr current)
		{
			if (this.m_EventBuffer == null)
			{
				return false;
			}
			if (this.m_EventBufferHead == null)
			{
				return false;
			}
			if (!current.valid)
			{
				current = new InputEventPtr((InputEvent*)this.m_EventBufferHead);
				return true;
			}
			byte* ptr = (byte*)current.Next().data;
			byte* ptr2 = this.m_EventBuffer + this.m_EventBufferSize;
			if (ptr == this.m_EventBufferTail)
			{
				return false;
			}
			if ((long)(ptr2 - ptr) < 20L || ((InputEvent*)ptr)->sizeInBytes == 0U)
			{
				ptr = this.m_EventBuffer;
				if (ptr == (byte*)current.ToPointer())
				{
					return false;
				}
			}
			current = new InputEventPtr((InputEvent*)ptr);
			return true;
		}

		// Token: 0x06000E31 RID: 3633 RVA: 0x000474D3 File Offset: 0x000456D3
		public IEnumerator<InputEventPtr> GetEnumerator()
		{
			return new InputEventTrace.Enumerator(this);
		}

		// Token: 0x06000E32 RID: 3634 RVA: 0x000474DB File Offset: 0x000456DB
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000E33 RID: 3635 RVA: 0x000474E3 File Offset: 0x000456E3
		public void Dispose()
		{
			this.Disable();
			this.Release();
		}

		// Token: 0x1700040D RID: 1037
		// (get) Token: 0x06000E34 RID: 3636 RVA: 0x000474F1 File Offset: 0x000456F1
		// (set) Token: 0x06000E35 RID: 3637 RVA: 0x000474FA File Offset: 0x000456FA
		private unsafe byte* m_EventBuffer
		{
			get
			{
				return this.m_EventBufferStorage;
			}
			set
			{
				this.m_EventBufferStorage = value;
			}
		}

		// Token: 0x1700040E RID: 1038
		// (get) Token: 0x06000E36 RID: 3638 RVA: 0x00047504 File Offset: 0x00045704
		// (set) Token: 0x06000E37 RID: 3639 RVA: 0x0004750D File Offset: 0x0004570D
		private unsafe byte* m_EventBufferHead
		{
			get
			{
				return this.m_EventBufferHeadStorage;
			}
			set
			{
				this.m_EventBufferHeadStorage = value;
			}
		}

		// Token: 0x1700040F RID: 1039
		// (get) Token: 0x06000E38 RID: 3640 RVA: 0x00047517 File Offset: 0x00045717
		// (set) Token: 0x06000E39 RID: 3641 RVA: 0x00047520 File Offset: 0x00045720
		private unsafe byte* m_EventBufferTail
		{
			get
			{
				return this.m_EventBufferTailStorage;
			}
			set
			{
				this.m_EventBufferTailStorage = value;
			}
		}

		// Token: 0x06000E3A RID: 3642 RVA: 0x0004752A File Offset: 0x0004572A
		private unsafe void Allocate()
		{
			this.m_EventBuffer = (byte*)UnsafeUtility.Malloc(this.m_EventBufferSize, 4, Allocator.Persistent);
		}

		// Token: 0x06000E3B RID: 3643 RVA: 0x0004753F File Offset: 0x0004573F
		private unsafe void Release()
		{
			this.Clear();
			if (this.m_EventBuffer != null)
			{
				UnsafeUtility.Free((void*)this.m_EventBuffer, Allocator.Persistent);
				this.m_EventBuffer = null;
			}
		}

		// Token: 0x06000E3C RID: 3644 RVA: 0x00047568 File Offset: 0x00045768
		private unsafe void OnBeforeUpdate()
		{
			if (this.m_RecordFrameMarkers)
			{
				InputEvent inputEvent = new InputEvent
				{
					type = InputEventTrace.FrameMarkerEvent,
					internalTime = InputRuntime.s_Instance.currentTime,
					sizeInBytes = (uint)UnsafeUtility.SizeOf<InputEvent>()
				};
				this.OnInputEvent(new InputEventPtr((InputEvent*)UnsafeUtility.AddressOf<InputEvent>(ref inputEvent)), null);
			}
		}

		// Token: 0x06000E3D RID: 3645 RVA: 0x000475C4 File Offset: 0x000457C4
		private unsafe void OnInputEvent(InputEventPtr inputEvent, InputDevice device)
		{
			if (inputEvent.handled)
			{
				return;
			}
			if (this.m_DeviceId != 0 && inputEvent.deviceId != this.m_DeviceId && inputEvent.type != InputEventTrace.FrameMarkerEvent)
			{
				return;
			}
			if (this.m_OnFilterEvent != null && !this.m_OnFilterEvent(inputEvent, device))
			{
				return;
			}
			if (this.m_EventBuffer == null)
			{
				return;
			}
			uint num = inputEvent.sizeInBytes.AlignToMultipleOf(4U);
			if ((ulong)num > (ulong)this.m_MaxEventBufferSize)
			{
				return;
			}
			if (this.m_EventBufferTail == null)
			{
				this.m_EventBufferHead = this.m_EventBuffer;
				this.m_EventBufferTail = this.m_EventBuffer;
			}
			byte* ptr = this.m_EventBufferTail + num;
			bool flag = ptr != this.m_EventBufferHead && this.m_EventBufferHead != this.m_EventBuffer;
			if (ptr != this.m_EventBuffer + this.m_EventBufferSize)
			{
				if (this.m_EventBufferSize < this.m_MaxEventBufferSize && !this.m_HasWrapped)
				{
					long num2 = Math.Max(this.m_GrowIncrementSize, (long)((ulong)num.AlignToMultipleOf(4U)));
					long num3 = this.m_EventBufferSize + num2;
					if (num3 > this.m_MaxEventBufferSize)
					{
						num3 = this.m_MaxEventBufferSize;
					}
					if (num3 < (long)((ulong)num))
					{
						return;
					}
					this.Resize(num3, -1L);
					ptr = this.m_EventBufferTail + num;
				}
				long num4 = this.m_EventBufferSize - (long)(this.m_EventBufferTail - this.m_EventBuffer);
				if (num4 < (long)((ulong)num))
				{
					this.m_HasWrapped = true;
					if (num4 >= 20L)
					{
						UnsafeUtility.MemClear((void*)this.m_EventBufferTail, 20L);
					}
					this.m_EventBufferTail = this.m_EventBuffer;
					ptr = this.m_EventBuffer + num;
					if (flag)
					{
						this.m_EventBufferHead = this.m_EventBuffer;
					}
					flag = ptr != this.m_EventBufferHead;
				}
			}
			if (flag)
			{
				byte* ptr2 = this.m_EventBufferHead;
				byte* ptr3 = this.m_EventBuffer + this.m_EventBufferSize - 20;
				while (ptr2 < ptr)
				{
					uint sizeInBytes = ((InputEvent*)ptr2)->sizeInBytes;
					ptr2 += sizeInBytes;
					this.m_EventCount -= 1L;
					this.m_EventSizeInBytes -= (long)((ulong)sizeInBytes);
					if (ptr2 != ptr3 || ((InputEvent*)ptr2)->sizeInBytes == 0U)
					{
						ptr2 = this.m_EventBuffer;
						break;
					}
				}
				this.m_EventBufferHead = ptr2;
			}
			byte* eventBufferTail = this.m_EventBufferTail;
			this.m_EventBufferTail = ptr;
			UnsafeUtility.MemCpy((void*)eventBufferTail, (void*)inputEvent.data, (long)((ulong)inputEvent.sizeInBytes));
			this.m_ChangeCounter++;
			this.m_EventCount += 1L;
			this.m_EventSizeInBytes += (long)((ulong)num);
			if (device != null)
			{
				bool flag2 = false;
				if (this.m_DeviceInfos != null)
				{
					for (int i = 0; i < this.m_DeviceInfos.Length; i++)
					{
						if (this.m_DeviceInfos[i].deviceId == device.deviceId)
						{
							flag2 = true;
							break;
						}
					}
				}
				if (!flag2)
				{
					ArrayHelpers.Append<InputEventTrace.DeviceInfo>(ref this.m_DeviceInfos, new InputEventTrace.DeviceInfo
					{
						m_DeviceId = device.deviceId,
						m_Layout = device.layout,
						m_StateFormat = device.stateBlock.format,
						m_StateSizeInBytes = (int)device.stateBlock.alignedSizeInBytes,
						m_FullLayoutJson = (InputControlLayout.s_Layouts.IsGeneratedLayout(device.m_Layout) ? InputSystem.LoadLayout(device.layout).ToJson() : null)
					});
				}
			}
			if (this.m_EventListeners.length > 0)
			{
				DelegateHelpers.InvokeCallbacksSafe<InputEventPtr>(ref this.m_EventListeners, new InputEventPtr((InputEvent*)eventBufferTail), "InputEventTrace.onEvent", null);
			}
		}

		// Token: 0x17000410 RID: 1040
		// (get) Token: 0x06000E3E RID: 3646 RVA: 0x00047930 File Offset: 0x00045B30
		private static FourCC kFileFormat
		{
			get
			{
				return new FourCC('I', 'E', 'V', 'T');
			}
		}

		// Token: 0x040005D1 RID: 1489
		private const int kDefaultBufferSize = 1048576;

		// Token: 0x040005D2 RID: 1490
		private static readonly ProfilerMarker k_InputEvenTraceMarker = new ProfilerMarker("InputEventTrace");

		// Token: 0x040005D3 RID: 1491
		[NonSerialized]
		private int m_ChangeCounter;

		// Token: 0x040005D4 RID: 1492
		[NonSerialized]
		private bool m_Enabled;

		// Token: 0x040005D5 RID: 1493
		[NonSerialized]
		private Func<InputEventPtr, InputDevice, bool> m_OnFilterEvent;

		// Token: 0x040005D6 RID: 1494
		[SerializeField]
		private int m_DeviceId;

		// Token: 0x040005D7 RID: 1495
		[NonSerialized]
		private CallbackArray<Action<InputEventPtr>> m_EventListeners;

		// Token: 0x040005D8 RID: 1496
		[SerializeField]
		private long m_EventBufferSize;

		// Token: 0x040005D9 RID: 1497
		[SerializeField]
		private long m_MaxEventBufferSize;

		// Token: 0x040005DA RID: 1498
		[SerializeField]
		private long m_GrowIncrementSize;

		// Token: 0x040005DB RID: 1499
		[SerializeField]
		private long m_EventCount;

		// Token: 0x040005DC RID: 1500
		[SerializeField]
		private long m_EventSizeInBytes;

		// Token: 0x040005DD RID: 1501
		[SerializeField]
		private ulong m_EventBufferStorage;

		// Token: 0x040005DE RID: 1502
		[SerializeField]
		private ulong m_EventBufferHeadStorage;

		// Token: 0x040005DF RID: 1503
		[SerializeField]
		private ulong m_EventBufferTailStorage;

		// Token: 0x040005E0 RID: 1504
		[SerializeField]
		private bool m_HasWrapped;

		// Token: 0x040005E1 RID: 1505
		[SerializeField]
		private bool m_RecordFrameMarkers;

		// Token: 0x040005E2 RID: 1506
		[SerializeField]
		private InputEventTrace.DeviceInfo[] m_DeviceInfos;

		// Token: 0x040005E3 RID: 1507
		private static int kFileVersion = 1;

		// Token: 0x02000219 RID: 537
		private class Enumerator : IEnumerator<InputEventPtr>, IEnumerator, IDisposable
		{
			// Token: 0x0600151F RID: 5407 RVA: 0x00062C40 File Offset: 0x00060E40
			public Enumerator(InputEventTrace trace)
			{
				this.m_Trace = trace;
				this.m_ChangeCounter = trace.m_ChangeCounter;
			}

			// Token: 0x06001520 RID: 5408 RVA: 0x00062C5B File Offset: 0x00060E5B
			public void Dispose()
			{
				this.m_Trace = null;
				this.m_Current = default(InputEventPtr);
			}

			// Token: 0x06001521 RID: 5409 RVA: 0x00062C70 File Offset: 0x00060E70
			public bool MoveNext()
			{
				if (this.m_Trace == null)
				{
					throw new ObjectDisposedException(this.ToString());
				}
				if (this.m_Trace.m_ChangeCounter != this.m_ChangeCounter)
				{
					throw new InvalidOperationException("Trace has been modified while enumerating!");
				}
				return this.m_Trace.GetNextEvent(ref this.m_Current);
			}

			// Token: 0x06001522 RID: 5410 RVA: 0x00062CC0 File Offset: 0x00060EC0
			public void Reset()
			{
				this.m_Current = default(InputEventPtr);
				this.m_ChangeCounter = this.m_Trace.m_ChangeCounter;
			}

			// Token: 0x170005B2 RID: 1458
			// (get) Token: 0x06001523 RID: 5411 RVA: 0x00062CDF File Offset: 0x00060EDF
			public InputEventPtr Current
			{
				get
				{
					return this.m_Current;
				}
			}

			// Token: 0x170005B3 RID: 1459
			// (get) Token: 0x06001524 RID: 5412 RVA: 0x00062CE7 File Offset: 0x00060EE7
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x04000BAC RID: 2988
			private InputEventTrace m_Trace;

			// Token: 0x04000BAD RID: 2989
			private int m_ChangeCounter;

			// Token: 0x04000BAE RID: 2990
			internal InputEventPtr m_Current;
		}

		// Token: 0x0200021A RID: 538
		[Flags]
		private enum FileFlags
		{
			// Token: 0x04000BB0 RID: 2992
			FixedUpdate = 1
		}

		// Token: 0x0200021B RID: 539
		public class ReplayController : IDisposable
		{
			// Token: 0x170005B4 RID: 1460
			// (get) Token: 0x06001525 RID: 5413 RVA: 0x00062CF4 File Offset: 0x00060EF4
			public InputEventTrace trace
			{
				get
				{
					return this.m_EventTrace;
				}
			}

			// Token: 0x170005B5 RID: 1461
			// (get) Token: 0x06001526 RID: 5414 RVA: 0x00062CFC File Offset: 0x00060EFC
			// (set) Token: 0x06001527 RID: 5415 RVA: 0x00062D04 File Offset: 0x00060F04
			public bool finished { get; private set; }

			// Token: 0x170005B6 RID: 1462
			// (get) Token: 0x06001528 RID: 5416 RVA: 0x00062D0D File Offset: 0x00060F0D
			// (set) Token: 0x06001529 RID: 5417 RVA: 0x00062D15 File Offset: 0x00060F15
			public bool paused { get; set; }

			// Token: 0x170005B7 RID: 1463
			// (get) Token: 0x0600152A RID: 5418 RVA: 0x00062D1E File Offset: 0x00060F1E
			// (set) Token: 0x0600152B RID: 5419 RVA: 0x00062D26 File Offset: 0x00060F26
			public int position { get; private set; }

			// Token: 0x170005B8 RID: 1464
			// (get) Token: 0x0600152C RID: 5420 RVA: 0x00062D2F File Offset: 0x00060F2F
			public IEnumerable<InputDevice> createdDevices
			{
				get
				{
					return this.m_CreatedDevices;
				}
			}

			// Token: 0x0600152D RID: 5421 RVA: 0x00062D3C File Offset: 0x00060F3C
			internal ReplayController(InputEventTrace trace)
			{
				if (trace == null)
				{
					throw new ArgumentNullException("trace");
				}
				this.m_EventTrace = trace;
			}

			// Token: 0x0600152E RID: 5422 RVA: 0x00062D5C File Offset: 0x00060F5C
			public void Dispose()
			{
				InputSystem.onBeforeUpdate -= this.OnBeginFrame;
				this.finished = true;
				foreach (InputDevice inputDevice in this.m_CreatedDevices)
				{
					InputSystem.RemoveDevice(inputDevice);
				}
				this.m_CreatedDevices = default(InlinedArray<InputDevice>);
			}

			// Token: 0x0600152F RID: 5423 RVA: 0x00062DCC File Offset: 0x00060FCC
			public InputEventTrace.ReplayController WithDeviceMappedFromTo(InputDevice recordedDevice, InputDevice playbackDevice)
			{
				if (recordedDevice == null)
				{
					throw new ArgumentNullException("recordedDevice");
				}
				if (playbackDevice == null)
				{
					throw new ArgumentNullException("playbackDevice");
				}
				this.WithDeviceMappedFromTo(recordedDevice.deviceId, playbackDevice.deviceId);
				return this;
			}

			// Token: 0x06001530 RID: 5424 RVA: 0x00062E00 File Offset: 0x00061000
			public InputEventTrace.ReplayController WithDeviceMappedFromTo(int recordedDeviceId, int playbackDeviceId)
			{
				for (int i = 0; i < this.m_DeviceIDMappings.length; i++)
				{
					if (this.m_DeviceIDMappings[i].Key == recordedDeviceId)
					{
						if (recordedDeviceId == playbackDeviceId)
						{
							this.m_DeviceIDMappings.RemoveAtWithCapacity(i);
						}
						else
						{
							this.m_DeviceIDMappings[i] = new KeyValuePair<int, int>(recordedDeviceId, playbackDeviceId);
						}
						return this;
					}
				}
				if (recordedDeviceId == playbackDeviceId)
				{
					return this;
				}
				this.m_DeviceIDMappings.AppendWithCapacity(new KeyValuePair<int, int>(recordedDeviceId, playbackDeviceId), 10);
				return this;
			}

			// Token: 0x06001531 RID: 5425 RVA: 0x00062E7D File Offset: 0x0006107D
			public InputEventTrace.ReplayController WithAllDevicesMappedToNewInstances()
			{
				this.m_CreateNewDevices = true;
				return this;
			}

			// Token: 0x06001532 RID: 5426 RVA: 0x00062E87 File Offset: 0x00061087
			public InputEventTrace.ReplayController OnFinished(Action action)
			{
				this.m_OnFinished = action;
				return this;
			}

			// Token: 0x06001533 RID: 5427 RVA: 0x00062E91 File Offset: 0x00061091
			public InputEventTrace.ReplayController OnEvent(Action<InputEventPtr> action)
			{
				this.m_OnEvent = action;
				return this;
			}

			// Token: 0x06001534 RID: 5428 RVA: 0x00062E9C File Offset: 0x0006109C
			public InputEventTrace.ReplayController PlayOneEvent()
			{
				InputEventPtr inputEventPtr;
				if (!this.MoveNext(true, out inputEventPtr))
				{
					throw new InvalidOperationException("No more events");
				}
				this.QueueEvent(inputEventPtr);
				return this;
			}

			// Token: 0x06001535 RID: 5429 RVA: 0x00062EC7 File Offset: 0x000610C7
			public InputEventTrace.ReplayController Rewind()
			{
				this.m_Enumerator = null;
				this.m_AllEventsByTime = null;
				this.m_AllEventsByTimeIndex = -1;
				this.position = 0;
				return this;
			}

			// Token: 0x06001536 RID: 5430 RVA: 0x00062EE6 File Offset: 0x000610E6
			public InputEventTrace.ReplayController PlayAllFramesOneByOne()
			{
				this.finished = false;
				InputSystem.onBeforeUpdate += this.OnBeginFrame;
				return this;
			}

			// Token: 0x06001537 RID: 5431 RVA: 0x00062F04 File Offset: 0x00061104
			public InputEventTrace.ReplayController PlayAllEvents()
			{
				this.finished = false;
				try
				{
					InputEventPtr inputEventPtr;
					while (this.MoveNext(true, out inputEventPtr))
					{
						this.QueueEvent(inputEventPtr);
					}
				}
				finally
				{
					this.Finished();
				}
				return this;
			}

			// Token: 0x06001538 RID: 5432 RVA: 0x00062F48 File Offset: 0x00061148
			public InputEventTrace.ReplayController PlayAllEventsAccordingToTimestamps()
			{
				List<InputEventPtr> list = new List<InputEventPtr>();
				InputEventPtr inputEventPtr;
				while (this.MoveNext(true, out inputEventPtr))
				{
					list.Add(inputEventPtr);
				}
				list.Sort((InputEventPtr a, InputEventPtr b) => a.time.CompareTo(b.time));
				this.m_Enumerator.Dispose();
				this.m_Enumerator = null;
				this.m_AllEventsByTime = list;
				this.position = 0;
				this.finished = false;
				this.m_StartTimeAsPerFirstEvent = -1.0;
				this.m_AllEventsByTimeIndex = -1;
				InputSystem.onBeforeUpdate += this.OnBeginFrame;
				return this;
			}

			// Token: 0x06001539 RID: 5433 RVA: 0x00062FE4 File Offset: 0x000611E4
			private void OnBeginFrame()
			{
				if (this.paused)
				{
					return;
				}
				InputEventPtr inputEventPtr;
				if (!this.MoveNext(false, out inputEventPtr))
				{
					if (this.m_AllEventsByTime == null || this.m_AllEventsByTimeIndex >= this.m_AllEventsByTime.Count)
					{
						this.Finished();
					}
					return;
				}
				int num;
				if (inputEventPtr.type == InputEventTrace.FrameMarkerEvent)
				{
					InputEventPtr inputEventPtr2;
					if (!this.MoveNext(false, out inputEventPtr2))
					{
						this.Finished();
						return;
					}
					if (inputEventPtr2.type == InputEventTrace.FrameMarkerEvent)
					{
						num = this.position - 1;
						this.position = num;
						this.m_Enumerator.m_Current = inputEventPtr;
						return;
					}
					inputEventPtr = inputEventPtr2;
				}
				for (;;)
				{
					this.QueueEvent(inputEventPtr);
					InputEventPtr inputEventPtr3;
					if (!this.MoveNext(false, out inputEventPtr3))
					{
						break;
					}
					if (inputEventPtr3.type == InputEventTrace.FrameMarkerEvent)
					{
						goto Block_9;
					}
					inputEventPtr = inputEventPtr3;
				}
				if (this.m_AllEventsByTime == null || this.m_AllEventsByTimeIndex >= this.m_AllEventsByTime.Count)
				{
					this.Finished();
					return;
				}
				return;
				Block_9:
				this.m_Enumerator.m_Current = inputEventPtr;
				num = this.position - 1;
				this.position = num;
			}

			// Token: 0x0600153A RID: 5434 RVA: 0x000630E6 File Offset: 0x000612E6
			private void Finished()
			{
				this.finished = true;
				InputSystem.onBeforeUpdate -= this.OnBeginFrame;
				Action onFinished = this.m_OnFinished;
				if (onFinished == null)
				{
					return;
				}
				onFinished();
			}

			// Token: 0x0600153B RID: 5435 RVA: 0x00063110 File Offset: 0x00061310
			private void QueueEvent(InputEventPtr eventPtr)
			{
				double internalTime = eventPtr.internalTime;
				if (this.m_AllEventsByTime != null)
				{
					eventPtr.internalTime = this.m_StartTimeAsPerRuntime + (eventPtr.internalTime - this.m_StartTimeAsPerFirstEvent);
				}
				else
				{
					eventPtr.internalTime = InputRuntime.s_Instance.currentTime;
				}
				int id = eventPtr.id;
				int deviceId = eventPtr.deviceId;
				eventPtr.deviceId = this.ApplyDeviceMapping(deviceId);
				Action<InputEventPtr> onEvent = this.m_OnEvent;
				if (onEvent != null)
				{
					onEvent(eventPtr);
				}
				try
				{
					InputSystem.QueueEvent(eventPtr);
				}
				finally
				{
					eventPtr.internalTime = internalTime;
					eventPtr.id = id;
					eventPtr.deviceId = deviceId;
				}
			}

			// Token: 0x0600153C RID: 5436 RVA: 0x000631C0 File Offset: 0x000613C0
			private bool MoveNext(bool skipFrameEvents, out InputEventPtr eventPtr)
			{
				eventPtr = default(InputEventPtr);
				int num;
				if (this.m_AllEventsByTime == null)
				{
					if (this.m_Enumerator == null)
					{
						this.m_Enumerator = new InputEventTrace.Enumerator(this.m_EventTrace);
					}
					while (this.m_Enumerator.MoveNext())
					{
						num = this.position + 1;
						this.position = num;
						eventPtr = this.m_Enumerator.Current;
						if (!skipFrameEvents || !(eventPtr.type == InputEventTrace.FrameMarkerEvent))
						{
							return true;
						}
					}
					return false;
				}
				if (this.m_AllEventsByTimeIndex + 1 >= this.m_AllEventsByTime.Count)
				{
					this.position = this.m_AllEventsByTime.Count;
					this.m_AllEventsByTimeIndex = this.m_AllEventsByTime.Count;
					return false;
				}
				if (this.m_AllEventsByTimeIndex < 0)
				{
					this.m_StartTimeAsPerFirstEvent = this.m_AllEventsByTime[0].internalTime;
					this.m_StartTimeAsPerRuntime = InputRuntime.s_Instance.currentTime;
				}
				else if (this.m_AllEventsByTimeIndex < this.m_AllEventsByTime.Count - 1 && this.m_AllEventsByTime[this.m_AllEventsByTimeIndex + 1].internalTime > this.m_StartTimeAsPerFirstEvent + (InputRuntime.s_Instance.currentTime - this.m_StartTimeAsPerRuntime))
				{
					return false;
				}
				this.m_AllEventsByTimeIndex++;
				num = this.position + 1;
				this.position = num;
				eventPtr = this.m_AllEventsByTime[this.m_AllEventsByTimeIndex];
				return true;
			}

			// Token: 0x0600153D RID: 5437 RVA: 0x00063330 File Offset: 0x00061530
			private int ApplyDeviceMapping(int originalDeviceId)
			{
				for (int i = 0; i < this.m_DeviceIDMappings.length; i++)
				{
					KeyValuePair<int, int> keyValuePair = this.m_DeviceIDMappings[i];
					if (keyValuePair.Key == originalDeviceId)
					{
						return keyValuePair.Value;
					}
				}
				if (this.m_CreateNewDevices)
				{
					try
					{
						int num = this.m_EventTrace.deviceInfos.IndexOf((InputEventTrace.DeviceInfo x) => x.deviceId == originalDeviceId);
						if (num != -1)
						{
							InputEventTrace.DeviceInfo deviceInfo = this.m_EventTrace.deviceInfos[num];
							InternedString internedString = new InternedString(deviceInfo.layout);
							if (!InputControlLayout.s_Layouts.HasLayout(internedString))
							{
								if (string.IsNullOrEmpty(deviceInfo.m_FullLayoutJson))
								{
									return originalDeviceId;
								}
								InputSystem.RegisterLayout(deviceInfo.m_FullLayoutJson, null, null);
							}
							InputDevice inputDevice = InputSystem.AddDevice(internedString, null, null);
							this.WithDeviceMappedFromTo(originalDeviceId, inputDevice.deviceId);
							this.m_CreatedDevices.AppendWithCapacity(inputDevice, 10);
							return inputDevice.deviceId;
						}
					}
					catch
					{
					}
				}
				return originalDeviceId;
			}

			// Token: 0x04000BB4 RID: 2996
			private InputEventTrace m_EventTrace;

			// Token: 0x04000BB5 RID: 2997
			private InputEventTrace.Enumerator m_Enumerator;

			// Token: 0x04000BB6 RID: 2998
			private InlinedArray<KeyValuePair<int, int>> m_DeviceIDMappings;

			// Token: 0x04000BB7 RID: 2999
			private bool m_CreateNewDevices;

			// Token: 0x04000BB8 RID: 3000
			private InlinedArray<InputDevice> m_CreatedDevices;

			// Token: 0x04000BB9 RID: 3001
			private Action m_OnFinished;

			// Token: 0x04000BBA RID: 3002
			private Action<InputEventPtr> m_OnEvent;

			// Token: 0x04000BBB RID: 3003
			private double m_StartTimeAsPerFirstEvent;

			// Token: 0x04000BBC RID: 3004
			private double m_StartTimeAsPerRuntime;

			// Token: 0x04000BBD RID: 3005
			private int m_AllEventsByTimeIndex;

			// Token: 0x04000BBE RID: 3006
			private List<InputEventPtr> m_AllEventsByTime;
		}

		// Token: 0x0200021C RID: 540
		[Serializable]
		public struct DeviceInfo
		{
			// Token: 0x170005B9 RID: 1465
			// (get) Token: 0x0600153E RID: 5438 RVA: 0x00063478 File Offset: 0x00061678
			// (set) Token: 0x0600153F RID: 5439 RVA: 0x00063480 File Offset: 0x00061680
			public int deviceId
			{
				get
				{
					return this.m_DeviceId;
				}
				set
				{
					this.m_DeviceId = value;
				}
			}

			// Token: 0x170005BA RID: 1466
			// (get) Token: 0x06001540 RID: 5440 RVA: 0x00063489 File Offset: 0x00061689
			// (set) Token: 0x06001541 RID: 5441 RVA: 0x00063491 File Offset: 0x00061691
			public string layout
			{
				get
				{
					return this.m_Layout;
				}
				set
				{
					this.m_Layout = value;
				}
			}

			// Token: 0x170005BB RID: 1467
			// (get) Token: 0x06001542 RID: 5442 RVA: 0x0006349A File Offset: 0x0006169A
			// (set) Token: 0x06001543 RID: 5443 RVA: 0x000634A2 File Offset: 0x000616A2
			public FourCC stateFormat
			{
				get
				{
					return this.m_StateFormat;
				}
				set
				{
					this.m_StateFormat = value;
				}
			}

			// Token: 0x170005BC RID: 1468
			// (get) Token: 0x06001544 RID: 5444 RVA: 0x000634AB File Offset: 0x000616AB
			// (set) Token: 0x06001545 RID: 5445 RVA: 0x000634B3 File Offset: 0x000616B3
			public int stateSizeInBytes
			{
				get
				{
					return this.m_StateSizeInBytes;
				}
				set
				{
					this.m_StateSizeInBytes = value;
				}
			}

			// Token: 0x04000BBF RID: 3007
			[SerializeField]
			internal int m_DeviceId;

			// Token: 0x04000BC0 RID: 3008
			[SerializeField]
			internal string m_Layout;

			// Token: 0x04000BC1 RID: 3009
			[SerializeField]
			internal FourCC m_StateFormat;

			// Token: 0x04000BC2 RID: 3010
			[SerializeField]
			internal int m_StateSizeInBytes;

			// Token: 0x04000BC3 RID: 3011
			[SerializeField]
			internal string m_FullLayoutJson;
		}
	}
}
