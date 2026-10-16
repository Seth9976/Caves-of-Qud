using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000FD RID: 253
	public class InputStateHistory : IDisposable, IEnumerable<InputStateHistory.Record>, IEnumerable, IInputStateChangeMonitor
	{
		// Token: 0x17000450 RID: 1104
		// (get) Token: 0x06000EEC RID: 3820 RVA: 0x00049AC7 File Offset: 0x00047CC7
		public int Count
		{
			get
			{
				return this.m_RecordCount;
			}
		}

		// Token: 0x17000451 RID: 1105
		// (get) Token: 0x06000EED RID: 3821 RVA: 0x00049ACF File Offset: 0x00047CCF
		public uint version
		{
			get
			{
				return this.m_CurrentVersion;
			}
		}

		// Token: 0x17000452 RID: 1106
		// (get) Token: 0x06000EEE RID: 3822 RVA: 0x00049AD7 File Offset: 0x00047CD7
		// (set) Token: 0x06000EEF RID: 3823 RVA: 0x00049ADF File Offset: 0x00047CDF
		public int historyDepth
		{
			get
			{
				return this.m_HistoryDepth;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentException("History depth cannot be negative", "value");
				}
				if (this.m_RecordBuffer.IsCreated)
				{
					throw new NotImplementedException();
				}
				this.m_HistoryDepth = value;
			}
		}

		// Token: 0x17000453 RID: 1107
		// (get) Token: 0x06000EF0 RID: 3824 RVA: 0x00049B0F File Offset: 0x00047D0F
		// (set) Token: 0x06000EF1 RID: 3825 RVA: 0x00049B17 File Offset: 0x00047D17
		public int extraMemoryPerRecord
		{
			get
			{
				return this.m_ExtraMemoryPerRecord;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentException("Memory size cannot be negative", "value");
				}
				if (this.m_RecordBuffer.IsCreated)
				{
					throw new NotImplementedException();
				}
				this.m_ExtraMemoryPerRecord = value;
			}
		}

		// Token: 0x17000454 RID: 1108
		// (get) Token: 0x06000EF2 RID: 3826 RVA: 0x00049B48 File Offset: 0x00047D48
		// (set) Token: 0x06000EF3 RID: 3827 RVA: 0x00049B7A File Offset: 0x00047D7A
		public InputUpdateType updateMask
		{
			get
			{
				InputUpdateType? updateMask = this.m_UpdateMask;
				if (updateMask == null)
				{
					return InputSystem.s_Manager.updateMask & ~InputUpdateType.Editor;
				}
				return updateMask.GetValueOrDefault();
			}
			set
			{
				if (value == InputUpdateType.None)
				{
					throw new ArgumentException("'InputUpdateType.None' is not a valid update mask", "value");
				}
				this.m_UpdateMask = new InputUpdateType?(value);
			}
		}

		// Token: 0x17000455 RID: 1109
		// (get) Token: 0x06000EF4 RID: 3828 RVA: 0x00049B9B File Offset: 0x00047D9B
		public ReadOnlyArray<InputControl> controls
		{
			get
			{
				return new ReadOnlyArray<InputControl>(this.m_Controls, 0, this.m_ControlCount);
			}
		}

		// Token: 0x17000456 RID: 1110
		public InputStateHistory.Record this[int index]
		{
			get
			{
				if (index < 0 || index >= this.m_RecordCount)
				{
					throw new ArgumentOutOfRangeException(string.Format("Index {0} is out of range for history with {1} entries", index, this.m_RecordCount), "index");
				}
				int num = this.UserIndexToRecordIndex(index);
				return new InputStateHistory.Record(this, num, this.GetRecord(num));
			}
			set
			{
				if (index < 0 || index >= this.m_RecordCount)
				{
					throw new ArgumentOutOfRangeException(string.Format("Index {0} is out of range for history with {1} entries", index, this.m_RecordCount), "index");
				}
				int num = this.UserIndexToRecordIndex(index);
				new InputStateHistory.Record(this, num, this.GetRecord(num)).CopyFrom(value);
			}
		}

		// Token: 0x17000457 RID: 1111
		// (get) Token: 0x06000EF7 RID: 3831 RVA: 0x00049C67 File Offset: 0x00047E67
		// (set) Token: 0x06000EF8 RID: 3832 RVA: 0x00049C6F File Offset: 0x00047E6F
		public Action<InputStateHistory.Record> onRecordAdded { get; set; }

		// Token: 0x17000458 RID: 1112
		// (get) Token: 0x06000EF9 RID: 3833 RVA: 0x00049C78 File Offset: 0x00047E78
		// (set) Token: 0x06000EFA RID: 3834 RVA: 0x00049C80 File Offset: 0x00047E80
		public Func<InputControl, double, InputEventPtr, bool> onShouldRecordStateChange { get; set; }

		// Token: 0x06000EFB RID: 3835 RVA: 0x00049C89 File Offset: 0x00047E89
		public InputStateHistory(int maxStateSizeInBytes)
		{
			if (maxStateSizeInBytes <= 0)
			{
				throw new ArgumentException("State size must be >= 0", "maxStateSizeInBytes");
			}
			this.m_AddNewControls = true;
			this.m_StateSizeInBytes = maxStateSizeInBytes.AlignToMultipleOf(4);
		}

		// Token: 0x06000EFC RID: 3836 RVA: 0x00049CC4 File Offset: 0x00047EC4
		public InputStateHistory(string path)
		{
			using (InputControlList<InputControl> inputControlList = InputSystem.FindControls(path))
			{
				this.m_Controls = inputControlList.ToArray(false);
				this.m_ControlCount = this.m_Controls.Length;
			}
		}

		// Token: 0x06000EFD RID: 3837 RVA: 0x00049D28 File Offset: 0x00047F28
		public InputStateHistory(InputControl control)
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			this.m_Controls = new InputControl[] { control };
			this.m_ControlCount = 1;
		}

		// Token: 0x06000EFE RID: 3838 RVA: 0x00049D60 File Offset: 0x00047F60
		public InputStateHistory(IEnumerable<InputControl> controls)
		{
			if (controls != null)
			{
				this.m_Controls = controls.ToArray<InputControl>();
				this.m_ControlCount = this.m_Controls.Length;
			}
		}

		// Token: 0x06000EFF RID: 3839 RVA: 0x00049D90 File Offset: 0x00047F90
		~InputStateHistory()
		{
			this.Dispose();
		}

		// Token: 0x06000F00 RID: 3840 RVA: 0x00049DBC File Offset: 0x00047FBC
		public void Clear()
		{
			this.m_HeadIndex = 0;
			this.m_RecordCount = 0;
			this.m_CurrentVersion += 1U;
		}

		// Token: 0x06000F01 RID: 3841 RVA: 0x00049DDC File Offset: 0x00047FDC
		public unsafe InputStateHistory.Record AddRecord(InputStateHistory.Record record)
		{
			int num;
			InputStateHistory.RecordHeader* ptr = this.AllocateRecord(out num);
			InputStateHistory.Record record2 = new InputStateHistory.Record(this, num, ptr);
			record2.CopyFrom(record);
			return record2;
		}

		// Token: 0x06000F02 RID: 3842 RVA: 0x00049E08 File Offset: 0x00048008
		public void StartRecording()
		{
			foreach (InputControl inputControl in this.controls)
			{
				InputState.AddChangeMonitor(inputControl, this, -1L, 0U);
			}
		}

		// Token: 0x06000F03 RID: 3843 RVA: 0x00049E60 File Offset: 0x00048060
		public void StopRecording()
		{
			foreach (InputControl inputControl in this.controls)
			{
				InputState.RemoveChangeMonitor(inputControl, this, -1L);
			}
		}

		// Token: 0x06000F04 RID: 3844 RVA: 0x00049EB8 File Offset: 0x000480B8
		public unsafe InputStateHistory.Record RecordStateChange(InputControl control, InputEventPtr eventPtr)
		{
			if (eventPtr.IsA<DeltaStateEvent>())
			{
				throw new NotImplementedException();
			}
			if (!eventPtr.IsA<StateEvent>())
			{
				throw new ArgumentException(string.Format("Event must be a state event but is '{0}' instead", eventPtr), "eventPtr");
			}
			byte* ptr = (byte*)StateEvent.From(eventPtr)->state - control.device.stateBlock.byteOffset;
			return this.RecordStateChange(control, (void*)ptr, eventPtr.time);
		}

		// Token: 0x06000F05 RID: 3845 RVA: 0x00049F28 File Offset: 0x00048128
		public unsafe InputStateHistory.Record RecordStateChange(InputControl control, void* statePtr, double time)
		{
			int num = this.m_Controls.IndexOfReference(control, this.m_ControlCount);
			if (num == -1)
			{
				if (!this.m_AddNewControls)
				{
					throw new ArgumentException(string.Format("Control '{0}' is not part of InputStateHistory", control), "control");
				}
				if ((ulong)control.stateBlock.alignedSizeInBytes > (ulong)((long)this.m_StateSizeInBytes))
				{
					throw new InvalidOperationException(string.Format("Cannot add control '{0}' with state larger than {1} bytes", control, this.m_StateSizeInBytes));
				}
				num = ArrayHelpers.AppendWithCapacity<InputControl>(ref this.m_Controls, ref this.m_ControlCount, control, 10);
			}
			int num2;
			InputStateHistory.RecordHeader* ptr = this.AllocateRecord(out num2);
			ptr->time = time;
			ref InputStateHistory.RecordHeader ptr2 = ref *ptr;
			uint num3 = this.m_CurrentVersion + 1U;
			this.m_CurrentVersion = num3;
			ptr2.version = num3;
			byte* ptr3 = ptr->statePtrWithoutControlIndex;
			if (this.m_ControlCount > 1 || this.m_AddNewControls)
			{
				ptr->controlIndex = num;
				ptr3 = ptr->statePtrWithControlIndex;
			}
			uint alignedSizeInBytes = control.stateBlock.alignedSizeInBytes;
			uint byteOffset = control.stateBlock.byteOffset;
			UnsafeUtility.MemCpy((void*)ptr3, (void*)((byte*)statePtr + byteOffset), (long)((ulong)alignedSizeInBytes));
			InputStateHistory.Record record = new InputStateHistory.Record(this, num2, ptr);
			Action<InputStateHistory.Record> onRecordAdded = this.onRecordAdded;
			if (onRecordAdded != null)
			{
				onRecordAdded(record);
			}
			return record;
		}

		// Token: 0x06000F06 RID: 3846 RVA: 0x0004A055 File Offset: 0x00048255
		public IEnumerator<InputStateHistory.Record> GetEnumerator()
		{
			return new InputStateHistory.Enumerator(this);
		}

		// Token: 0x06000F07 RID: 3847 RVA: 0x0004A062 File Offset: 0x00048262
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000F08 RID: 3848 RVA: 0x0004A06A File Offset: 0x0004826A
		public void Dispose()
		{
			this.StopRecording();
			this.Destroy();
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000F09 RID: 3849 RVA: 0x0004A07E File Offset: 0x0004827E
		protected void Destroy()
		{
			if (this.m_RecordBuffer.IsCreated)
			{
				this.m_RecordBuffer.Dispose();
				this.m_RecordBuffer = default(NativeArray<byte>);
			}
		}

		// Token: 0x06000F0A RID: 3850 RVA: 0x0004A0A4 File Offset: 0x000482A4
		private void Allocate()
		{
			if (!this.m_AddNewControls)
			{
				this.m_StateSizeInBytes = 0;
				foreach (InputControl inputControl in this.controls)
				{
					this.m_StateSizeInBytes = (int)Math.Max((uint)this.m_StateSizeInBytes, inputControl.stateBlock.alignedSizeInBytes);
				}
			}
			int num = this.bytesPerRecord * this.m_HistoryDepth;
			this.m_RecordBuffer = new NativeArray<byte>(num, Allocator.Persistent, NativeArrayOptions.UninitializedMemory);
		}

		// Token: 0x06000F0B RID: 3851 RVA: 0x0004A140 File Offset: 0x00048340
		protected internal int RecordIndexToUserIndex(int index)
		{
			if (index < this.m_HeadIndex)
			{
				return this.m_HistoryDepth - this.m_HeadIndex + index;
			}
			return index - this.m_HeadIndex;
		}

		// Token: 0x06000F0C RID: 3852 RVA: 0x0004A163 File Offset: 0x00048363
		protected internal int UserIndexToRecordIndex(int index)
		{
			return (this.m_HeadIndex + index) % this.m_HistoryDepth;
		}

		// Token: 0x06000F0D RID: 3853 RVA: 0x0004A174 File Offset: 0x00048374
		protected internal unsafe InputStateHistory.RecordHeader* GetRecord(int index)
		{
			if (!this.m_RecordBuffer.IsCreated)
			{
				throw new InvalidOperationException("History buffer has been disposed");
			}
			if (index < 0 || index >= this.m_HistoryDepth)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			return this.GetRecordUnchecked(index);
		}

		// Token: 0x06000F0E RID: 3854 RVA: 0x0004A1AD File Offset: 0x000483AD
		internal unsafe InputStateHistory.RecordHeader* GetRecordUnchecked(int index)
		{
			return (InputStateHistory.RecordHeader*)((byte*)this.m_RecordBuffer.GetUnsafePtr<byte>() + index * this.bytesPerRecord);
		}

		// Token: 0x06000F0F RID: 3855 RVA: 0x0004A1C4 File Offset: 0x000483C4
		protected internal unsafe InputStateHistory.RecordHeader* AllocateRecord(out int index)
		{
			if (!this.m_RecordBuffer.IsCreated)
			{
				this.Allocate();
			}
			index = (this.m_HeadIndex + this.m_RecordCount) % this.m_HistoryDepth;
			if (this.m_RecordCount == this.m_HistoryDepth)
			{
				this.m_HeadIndex = (this.m_HeadIndex + 1) % this.m_HistoryDepth;
			}
			else
			{
				this.m_RecordCount++;
			}
			return (InputStateHistory.RecordHeader*)((byte*)this.m_RecordBuffer.GetUnsafePtr<byte>() + this.bytesPerRecord * index);
		}

		// Token: 0x06000F10 RID: 3856 RVA: 0x0004A244 File Offset: 0x00048444
		protected unsafe TValue ReadValue<TValue>(InputStateHistory.RecordHeader* data) where TValue : struct
		{
			bool flag = this.m_ControlCount == 1 && !this.m_AddNewControls;
			InputControl inputControl = (flag ? this.controls[0] : this.controls[data->controlIndex]);
			InputControl<TValue> inputControl2 = inputControl as InputControl<TValue>;
			if (inputControl2 == null)
			{
				throw new InvalidOperationException(string.Format("Cannot read value of type '{0}' from control '{1}' with value type '{2}'", typeof(TValue).GetNiceTypeName(), inputControl, inputControl.valueType.GetNiceTypeName()));
			}
			byte* ptr = (flag ? data->statePtrWithoutControlIndex : data->statePtrWithControlIndex);
			ptr -= inputControl.stateBlock.byteOffset;
			return inputControl2.ReadValueFromState((void*)ptr);
		}

		// Token: 0x06000F11 RID: 3857 RVA: 0x0004A2F0 File Offset: 0x000484F0
		protected unsafe object ReadValueAsObject(InputStateHistory.RecordHeader* data)
		{
			bool flag = this.m_ControlCount == 1 && !this.m_AddNewControls;
			InputControl inputControl = (flag ? this.controls[0] : this.controls[data->controlIndex]);
			byte* ptr = (flag ? data->statePtrWithoutControlIndex : data->statePtrWithControlIndex);
			ptr -= inputControl.stateBlock.byteOffset;
			return inputControl.ReadValueFromStateAsObject((void*)ptr);
		}

		// Token: 0x06000F12 RID: 3858 RVA: 0x0004A368 File Offset: 0x00048568
		void IInputStateChangeMonitor.NotifyControlStateChanged(InputControl control, double time, InputEventPtr eventPtr, long monitorIndex)
		{
			bool currentUpdateType = InputState.currentUpdateType != InputUpdateType.None;
			InputUpdateType updateMask = this.updateMask;
			if (((currentUpdateType ? InputUpdateType.Dynamic : InputUpdateType.None) & updateMask) == InputUpdateType.None)
			{
				return;
			}
			if (this.onShouldRecordStateChange != null && !this.onShouldRecordStateChange(control, time, eventPtr))
			{
				return;
			}
			this.RecordStateChange(control, control.currentStatePtr, time);
		}

		// Token: 0x06000F13 RID: 3859 RVA: 0x0004A3AE File Offset: 0x000485AE
		void IInputStateChangeMonitor.NotifyTimerExpired(InputControl control, double time, long monitorIndex, int timerIndex)
		{
		}

		// Token: 0x17000459 RID: 1113
		// (get) Token: 0x06000F14 RID: 3860 RVA: 0x0004A3B0 File Offset: 0x000485B0
		internal int bytesPerRecord
		{
			get
			{
				return (this.m_StateSizeInBytes + this.m_ExtraMemoryPerRecord + ((this.m_ControlCount == 1 && !this.m_AddNewControls) ? 12 : 16)).AlignToMultipleOf(4);
			}
		}

		// Token: 0x04000644 RID: 1604
		private const int kDefaultHistorySize = 128;

		// Token: 0x04000647 RID: 1607
		internal InputControl[] m_Controls;

		// Token: 0x04000648 RID: 1608
		internal int m_ControlCount;

		// Token: 0x04000649 RID: 1609
		private NativeArray<byte> m_RecordBuffer;

		// Token: 0x0400064A RID: 1610
		private int m_StateSizeInBytes;

		// Token: 0x0400064B RID: 1611
		private int m_RecordCount;

		// Token: 0x0400064C RID: 1612
		private int m_HistoryDepth = 128;

		// Token: 0x0400064D RID: 1613
		private int m_ExtraMemoryPerRecord;

		// Token: 0x0400064E RID: 1614
		internal int m_HeadIndex;

		// Token: 0x0400064F RID: 1615
		internal uint m_CurrentVersion;

		// Token: 0x04000650 RID: 1616
		private InputUpdateType? m_UpdateMask;

		// Token: 0x04000651 RID: 1617
		internal readonly bool m_AddNewControls;

		// Token: 0x02000225 RID: 549
		private struct Enumerator : IEnumerator<InputStateHistory.Record>, IEnumerator, IDisposable
		{
			// Token: 0x06001559 RID: 5465 RVA: 0x0006371E File Offset: 0x0006191E
			public Enumerator(InputStateHistory history)
			{
				this.m_History = history;
				this.m_Index = -1;
			}

			// Token: 0x0600155A RID: 5466 RVA: 0x0006372E File Offset: 0x0006192E
			public bool MoveNext()
			{
				if (this.m_Index + 1 >= this.m_History.Count)
				{
					return false;
				}
				this.m_Index++;
				return true;
			}

			// Token: 0x0600155B RID: 5467 RVA: 0x00063756 File Offset: 0x00061956
			public void Reset()
			{
				this.m_Index = -1;
			}

			// Token: 0x170005BF RID: 1471
			// (get) Token: 0x0600155C RID: 5468 RVA: 0x0006375F File Offset: 0x0006195F
			public InputStateHistory.Record Current
			{
				get
				{
					return this.m_History[this.m_Index];
				}
			}

			// Token: 0x170005C0 RID: 1472
			// (get) Token: 0x0600155D RID: 5469 RVA: 0x00063772 File Offset: 0x00061972
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x0600155E RID: 5470 RVA: 0x0006377F File Offset: 0x0006197F
			public void Dispose()
			{
			}

			// Token: 0x04000BD0 RID: 3024
			private readonly InputStateHistory m_History;

			// Token: 0x04000BD1 RID: 3025
			private int m_Index;
		}

		// Token: 0x02000226 RID: 550
		[StructLayout(LayoutKind.Explicit)]
		protected internal struct RecordHeader
		{
			// Token: 0x170005C1 RID: 1473
			// (get) Token: 0x0600155F RID: 5471 RVA: 0x00063784 File Offset: 0x00061984
			public unsafe byte* statePtrWithControlIndex
			{
				get
				{
					fixed (byte* ptr = &this.m_StateWithControlIndex.FixedElementField)
					{
						return ptr;
					}
				}
			}

			// Token: 0x170005C2 RID: 1474
			// (get) Token: 0x06001560 RID: 5472 RVA: 0x000637A0 File Offset: 0x000619A0
			public unsafe byte* statePtrWithoutControlIndex
			{
				get
				{
					fixed (byte* ptr = &this.m_StateWithoutControlIndex.FixedElementField)
					{
						return ptr;
					}
				}
			}

			// Token: 0x04000BD2 RID: 3026
			[FieldOffset(0)]
			public double time;

			// Token: 0x04000BD3 RID: 3027
			[FieldOffset(8)]
			public uint version;

			// Token: 0x04000BD4 RID: 3028
			[FieldOffset(12)]
			public int controlIndex;

			// Token: 0x04000BD5 RID: 3029
			[FixedBuffer(typeof(byte), 1)]
			[FieldOffset(12)]
			private InputStateHistory.RecordHeader.<m_StateWithoutControlIndex>e__FixedBuffer m_StateWithoutControlIndex;

			// Token: 0x04000BD6 RID: 3030
			[FixedBuffer(typeof(byte), 1)]
			[FieldOffset(16)]
			private InputStateHistory.RecordHeader.<m_StateWithControlIndex>e__FixedBuffer m_StateWithControlIndex;

			// Token: 0x04000BD7 RID: 3031
			public const int kSizeWithControlIndex = 16;

			// Token: 0x04000BD8 RID: 3032
			public const int kSizeWithoutControlIndex = 12;

			// Token: 0x02000282 RID: 642
			[CompilerGenerated]
			[UnsafeValueType]
			[StructLayout(LayoutKind.Sequential, Size = 1)]
			public struct <m_StateWithControlIndex>e__FixedBuffer
			{
				// Token: 0x04000D28 RID: 3368
				public byte FixedElementField;
			}

			// Token: 0x02000283 RID: 643
			[CompilerGenerated]
			[UnsafeValueType]
			[StructLayout(LayoutKind.Sequential, Size = 1)]
			public struct <m_StateWithoutControlIndex>e__FixedBuffer
			{
				// Token: 0x04000D29 RID: 3369
				public byte FixedElementField;
			}
		}

		// Token: 0x02000227 RID: 551
		public struct Record : IEquatable<InputStateHistory.Record>
		{
			// Token: 0x170005C3 RID: 1475
			// (get) Token: 0x06001561 RID: 5473 RVA: 0x000637BB File Offset: 0x000619BB
			internal unsafe InputStateHistory.RecordHeader* header
			{
				get
				{
					return this.m_Owner.GetRecord(this.recordIndex);
				}
			}

			// Token: 0x170005C4 RID: 1476
			// (get) Token: 0x06001562 RID: 5474 RVA: 0x000637CE File Offset: 0x000619CE
			internal int recordIndex
			{
				get
				{
					return this.m_IndexPlusOne - 1;
				}
			}

			// Token: 0x170005C5 RID: 1477
			// (get) Token: 0x06001563 RID: 5475 RVA: 0x000637D8 File Offset: 0x000619D8
			internal uint version
			{
				get
				{
					return this.m_Version;
				}
			}

			// Token: 0x170005C6 RID: 1478
			// (get) Token: 0x06001564 RID: 5476 RVA: 0x000637E0 File Offset: 0x000619E0
			public unsafe bool valid
			{
				get
				{
					return this.m_Owner != null && this.m_IndexPlusOne != 0 && this.header->version == this.m_Version;
				}
			}

			// Token: 0x170005C7 RID: 1479
			// (get) Token: 0x06001565 RID: 5477 RVA: 0x00063807 File Offset: 0x00061A07
			public InputStateHistory owner
			{
				get
				{
					return this.m_Owner;
				}
			}

			// Token: 0x170005C8 RID: 1480
			// (get) Token: 0x06001566 RID: 5478 RVA: 0x0006380F File Offset: 0x00061A0F
			public int index
			{
				get
				{
					this.CheckValid();
					return this.m_Owner.RecordIndexToUserIndex(this.recordIndex);
				}
			}

			// Token: 0x170005C9 RID: 1481
			// (get) Token: 0x06001567 RID: 5479 RVA: 0x00063828 File Offset: 0x00061A28
			public unsafe double time
			{
				get
				{
					this.CheckValid();
					return this.header->time;
				}
			}

			// Token: 0x170005CA RID: 1482
			// (get) Token: 0x06001568 RID: 5480 RVA: 0x0006383C File Offset: 0x00061A3C
			public unsafe InputControl control
			{
				get
				{
					this.CheckValid();
					ReadOnlyArray<InputControl> controls = this.m_Owner.controls;
					if (controls.Count == 1 && !this.m_Owner.m_AddNewControls)
					{
						return controls[0];
					}
					return controls[this.header->controlIndex];
				}
			}

			// Token: 0x170005CB RID: 1483
			// (get) Token: 0x06001569 RID: 5481 RVA: 0x00063890 File Offset: 0x00061A90
			public InputStateHistory.Record next
			{
				get
				{
					this.CheckValid();
					int num = this.m_Owner.RecordIndexToUserIndex(this.recordIndex);
					if (num + 1 >= this.m_Owner.Count)
					{
						return default(InputStateHistory.Record);
					}
					int num2 = this.m_Owner.UserIndexToRecordIndex(num + 1);
					return new InputStateHistory.Record(this.m_Owner, num2, this.m_Owner.GetRecord(num2));
				}
			}

			// Token: 0x170005CC RID: 1484
			// (get) Token: 0x0600156A RID: 5482 RVA: 0x000638F8 File Offset: 0x00061AF8
			public InputStateHistory.Record previous
			{
				get
				{
					this.CheckValid();
					int num = this.m_Owner.RecordIndexToUserIndex(this.recordIndex);
					if (num - 1 < 0)
					{
						return default(InputStateHistory.Record);
					}
					int num2 = this.m_Owner.UserIndexToRecordIndex(num - 1);
					return new InputStateHistory.Record(this.m_Owner, num2, this.m_Owner.GetRecord(num2));
				}
			}

			// Token: 0x0600156B RID: 5483 RVA: 0x00063954 File Offset: 0x00061B54
			internal unsafe Record(InputStateHistory owner, int index, InputStateHistory.RecordHeader* header)
			{
				this.m_Owner = owner;
				this.m_IndexPlusOne = index + 1;
				this.m_Version = header->version;
			}

			// Token: 0x0600156C RID: 5484 RVA: 0x00063972 File Offset: 0x00061B72
			public TValue ReadValue<TValue>() where TValue : struct
			{
				this.CheckValid();
				return this.m_Owner.ReadValue<TValue>(this.header);
			}

			// Token: 0x0600156D RID: 5485 RVA: 0x0006398B File Offset: 0x00061B8B
			public object ReadValueAsObject()
			{
				this.CheckValid();
				return this.m_Owner.ReadValueAsObject(this.header);
			}

			// Token: 0x0600156E RID: 5486 RVA: 0x000639A4 File Offset: 0x00061BA4
			public unsafe void* GetUnsafeMemoryPtr()
			{
				this.CheckValid();
				return this.GetUnsafeMemoryPtrUnchecked();
			}

			// Token: 0x0600156F RID: 5487 RVA: 0x000639B4 File Offset: 0x00061BB4
			internal unsafe void* GetUnsafeMemoryPtrUnchecked()
			{
				if (this.m_Owner.controls.Count == 1 && !this.m_Owner.m_AddNewControls)
				{
					return (void*)this.header->statePtrWithoutControlIndex;
				}
				return (void*)this.header->statePtrWithControlIndex;
			}

			// Token: 0x06001570 RID: 5488 RVA: 0x000639FB File Offset: 0x00061BFB
			public unsafe void* GetUnsafeExtraMemoryPtr()
			{
				this.CheckValid();
				return this.GetUnsafeExtraMemoryPtrUnchecked();
			}

			// Token: 0x06001571 RID: 5489 RVA: 0x00063A09 File Offset: 0x00061C09
			internal unsafe void* GetUnsafeExtraMemoryPtrUnchecked()
			{
				if (this.m_Owner.extraMemoryPerRecord == 0)
				{
					throw new InvalidOperationException("No extra memory has been set up for history records; set extraMemoryPerRecord");
				}
				return (void*)(this.header + this.m_Owner.bytesPerRecord / sizeof(InputStateHistory.RecordHeader) - this.m_Owner.extraMemoryPerRecord / sizeof(InputStateHistory.RecordHeader));
			}

			// Token: 0x06001572 RID: 5490 RVA: 0x00063A44 File Offset: 0x00061C44
			public unsafe void CopyFrom(InputStateHistory.Record record)
			{
				if (!record.valid)
				{
					throw new ArgumentException("Given history record is not valid", "record");
				}
				this.CheckValid();
				InputControl control = record.control;
				int num = this.m_Owner.controls.IndexOfReference(control);
				if (num == -1)
				{
					if (!this.m_Owner.m_AddNewControls)
					{
						throw new InvalidOperationException(string.Format("Control '{0}' is not tracked by target history", record.control));
					}
					num = ArrayHelpers.AppendWithCapacity<InputControl>(ref this.m_Owner.m_Controls, ref this.m_Owner.m_ControlCount, control, 10);
				}
				int stateSizeInBytes = this.m_Owner.m_StateSizeInBytes;
				if (stateSizeInBytes != record.m_Owner.m_StateSizeInBytes)
				{
					throw new InvalidOperationException(string.Format("Cannot copy record from owner with state size '{0}' to owner with state size '{1}'", record.m_Owner.m_StateSizeInBytes, stateSizeInBytes));
				}
				InputStateHistory.RecordHeader* header = this.header;
				InputStateHistory.RecordHeader* header2 = record.header;
				UnsafeUtility.MemCpy((void*)header, (void*)header2, 12L);
				ref InputStateHistory.RecordHeader ptr = ref *header;
				InputStateHistory owner = this.m_Owner;
				uint num2 = owner.m_CurrentVersion + 1U;
				owner.m_CurrentVersion = num2;
				ptr.version = num2;
				this.m_Version = header->version;
				byte* ptr2 = header->statePtrWithoutControlIndex;
				if (this.m_Owner.controls.Count > 1 || this.m_Owner.m_AddNewControls)
				{
					header->controlIndex = num;
					ptr2 = header->statePtrWithControlIndex;
				}
				byte* ptr3 = ((record.m_Owner.m_ControlCount > 1 || record.m_Owner.m_AddNewControls) ? header2->statePtrWithControlIndex : header2->statePtrWithoutControlIndex);
				UnsafeUtility.MemCpy((void*)ptr2, (void*)ptr3, (long)stateSizeInBytes);
				int extraMemoryPerRecord = this.m_Owner.m_ExtraMemoryPerRecord;
				if (extraMemoryPerRecord > 0 && extraMemoryPerRecord == record.m_Owner.m_ExtraMemoryPerRecord)
				{
					UnsafeUtility.MemCpy(this.GetUnsafeExtraMemoryPtr(), record.GetUnsafeExtraMemoryPtr(), (long)extraMemoryPerRecord);
				}
				Action<InputStateHistory.Record> onRecordAdded = this.m_Owner.onRecordAdded;
				if (onRecordAdded == null)
				{
					return;
				}
				onRecordAdded(this);
			}

			// Token: 0x06001573 RID: 5491 RVA: 0x00063C1B File Offset: 0x00061E1B
			internal unsafe void CheckValid()
			{
				if (this.m_Owner == null || this.m_IndexPlusOne == 0)
				{
					throw new InvalidOperationException("Value not initialized");
				}
				if (this.header->version != this.m_Version)
				{
					throw new InvalidOperationException("Record is no longer valid");
				}
			}

			// Token: 0x06001574 RID: 5492 RVA: 0x00063C56 File Offset: 0x00061E56
			public bool Equals(InputStateHistory.Record other)
			{
				return this.m_Owner == other.m_Owner && this.m_IndexPlusOne == other.m_IndexPlusOne && this.m_Version == other.m_Version;
			}

			// Token: 0x06001575 RID: 5493 RVA: 0x00063C84 File Offset: 0x00061E84
			public override bool Equals(object obj)
			{
				if (obj is InputStateHistory.Record)
				{
					InputStateHistory.Record record = (InputStateHistory.Record)obj;
					return this.Equals(record);
				}
				return false;
			}

			// Token: 0x06001576 RID: 5494 RVA: 0x00063CA9 File Offset: 0x00061EA9
			public override int GetHashCode()
			{
				return (((((this.m_Owner != null) ? this.m_Owner.GetHashCode() : 0) * 397) ^ this.m_IndexPlusOne) * 397) ^ (int)this.m_Version;
			}

			// Token: 0x06001577 RID: 5495 RVA: 0x00063CDB File Offset: 0x00061EDB
			public override string ToString()
			{
				if (!this.valid)
				{
					return "<Invalid>";
				}
				return string.Format("{{ control={0} value={1} time={2} }}", this.control, this.ReadValueAsObject(), this.time);
			}

			// Token: 0x04000BD9 RID: 3033
			private readonly InputStateHistory m_Owner;

			// Token: 0x04000BDA RID: 3034
			private readonly int m_IndexPlusOne;

			// Token: 0x04000BDB RID: 3035
			private uint m_Version;
		}
	}
}
