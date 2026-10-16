using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000FE RID: 254
	public class InputStateHistory<TValue> : InputStateHistory, IReadOnlyList<InputStateHistory<TValue>.Record>, IEnumerable<InputStateHistory<TValue>.Record>, IEnumerable, IReadOnlyCollection<InputStateHistory<TValue>.Record> where TValue : struct
	{
		// Token: 0x06000F15 RID: 3861 RVA: 0x0004A3E0 File Offset: 0x000485E0
		public InputStateHistory(int? maxStateSizeInBytes = null)
			: base(maxStateSizeInBytes ?? UnsafeUtility.SizeOf<TValue>())
		{
			int? num = maxStateSizeInBytes;
			int num2 = UnsafeUtility.SizeOf<TValue>();
			if ((num.GetValueOrDefault() < num2) & (num != null))
			{
				throw new ArgumentException("Max state size cannot be smaller than sizeof(TValue)", "maxStateSizeInBytes");
			}
		}

		// Token: 0x06000F16 RID: 3862 RVA: 0x0004A438 File Offset: 0x00048638
		public InputStateHistory(InputControl<TValue> control)
			: base(control)
		{
		}

		// Token: 0x06000F17 RID: 3863 RVA: 0x0004A444 File Offset: 0x00048644
		public InputStateHistory(string path)
			: base(path)
		{
			foreach (InputControl inputControl in base.controls)
			{
				if (!typeof(TValue).IsAssignableFrom(inputControl.valueType))
				{
					throw new ArgumentException(string.Format("Control '{0}' matched by '{1}' has value type '{2}' which is incompatible with '{3}'", new object[]
					{
						inputControl,
						path,
						inputControl.valueType.GetNiceTypeName(),
						typeof(TValue).GetNiceTypeName()
					}));
				}
			}
		}

		// Token: 0x06000F18 RID: 3864 RVA: 0x0004A4F0 File Offset: 0x000486F0
		~InputStateHistory()
		{
			base.Destroy();
		}

		// Token: 0x06000F19 RID: 3865 RVA: 0x0004A51C File Offset: 0x0004871C
		public unsafe InputStateHistory<TValue>.Record AddRecord(InputStateHistory<TValue>.Record record)
		{
			int num;
			InputStateHistory.RecordHeader* ptr = base.AllocateRecord(out num);
			InputStateHistory<TValue>.Record record2 = new InputStateHistory<TValue>.Record(this, num, ptr);
			record2.CopyFrom(record);
			return record2;
		}

		// Token: 0x06000F1A RID: 3866 RVA: 0x0004A548 File Offset: 0x00048748
		public unsafe InputStateHistory<TValue>.Record RecordStateChange(InputControl<TValue> control, TValue value, double time = -1.0)
		{
			InputEventPtr inputEventPtr;
			InputStateHistory<TValue>.Record record2;
			using (StateEvent.From(control.device, out inputEventPtr, Allocator.Temp))
			{
				byte* ptr = (byte*)StateEvent.From(inputEventPtr)->state - control.device.stateBlock.byteOffset;
				control.WriteValueIntoState(value, (void*)ptr);
				if (time >= 0.0)
				{
					inputEventPtr.time = time;
				}
				InputStateHistory.Record record = base.RecordStateChange(control, inputEventPtr);
				record2 = new InputStateHistory<TValue>.Record(this, record.recordIndex, record.header);
			}
			return record2;
		}

		// Token: 0x06000F1B RID: 3867 RVA: 0x0004A5E4 File Offset: 0x000487E4
		public new IEnumerator<InputStateHistory<TValue>.Record> GetEnumerator()
		{
			return new InputStateHistory<TValue>.Enumerator(this);
		}

		// Token: 0x06000F1C RID: 3868 RVA: 0x0004A5F1 File Offset: 0x000487F1
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x1700045A RID: 1114
		public InputStateHistory<TValue>.Record this[int index]
		{
			get
			{
				if (index < 0 || index >= base.Count)
				{
					throw new ArgumentOutOfRangeException(string.Format("Index {0} is out of range for history with {1} entries", index, base.Count), "index");
				}
				int num = base.UserIndexToRecordIndex(index);
				return new InputStateHistory<TValue>.Record(this, num, base.GetRecord(num));
			}
			set
			{
				if (index < 0 || index >= base.Count)
				{
					throw new ArgumentOutOfRangeException(string.Format("Index {0} is out of range for history with {1} entries", index, base.Count), "index");
				}
				int num = base.UserIndexToRecordIndex(index);
				new InputStateHistory<TValue>.Record(this, num, base.GetRecord(num)).CopyFrom(value);
			}
		}

		// Token: 0x02000228 RID: 552
		private struct Enumerator : IEnumerator<InputStateHistory<TValue>.Record>, IEnumerator, IDisposable
		{
			// Token: 0x06001578 RID: 5496 RVA: 0x00063D0C File Offset: 0x00061F0C
			public Enumerator(InputStateHistory<TValue> history)
			{
				this.m_History = history;
				this.m_Index = -1;
			}

			// Token: 0x06001579 RID: 5497 RVA: 0x00063D1C File Offset: 0x00061F1C
			public bool MoveNext()
			{
				if (this.m_Index + 1 >= this.m_History.Count)
				{
					return false;
				}
				this.m_Index++;
				return true;
			}

			// Token: 0x0600157A RID: 5498 RVA: 0x00063D44 File Offset: 0x00061F44
			public void Reset()
			{
				this.m_Index = -1;
			}

			// Token: 0x170005CD RID: 1485
			// (get) Token: 0x0600157B RID: 5499 RVA: 0x00063D4D File Offset: 0x00061F4D
			public InputStateHistory<TValue>.Record Current
			{
				get
				{
					return this.m_History[this.m_Index];
				}
			}

			// Token: 0x170005CE RID: 1486
			// (get) Token: 0x0600157C RID: 5500 RVA: 0x00063D60 File Offset: 0x00061F60
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x0600157D RID: 5501 RVA: 0x00063D6D File Offset: 0x00061F6D
			public void Dispose()
			{
			}

			// Token: 0x04000BDC RID: 3036
			private readonly InputStateHistory<TValue> m_History;

			// Token: 0x04000BDD RID: 3037
			private int m_Index;
		}

		// Token: 0x02000229 RID: 553
		public new struct Record : IEquatable<InputStateHistory<TValue>.Record>
		{
			// Token: 0x170005CF RID: 1487
			// (get) Token: 0x0600157E RID: 5502 RVA: 0x00063D6F File Offset: 0x00061F6F
			internal unsafe InputStateHistory.RecordHeader* header
			{
				get
				{
					return this.m_Owner.GetRecord(this.recordIndex);
				}
			}

			// Token: 0x170005D0 RID: 1488
			// (get) Token: 0x0600157F RID: 5503 RVA: 0x00063D82 File Offset: 0x00061F82
			internal int recordIndex
			{
				get
				{
					return this.m_IndexPlusOne - 1;
				}
			}

			// Token: 0x170005D1 RID: 1489
			// (get) Token: 0x06001580 RID: 5504 RVA: 0x00063D8C File Offset: 0x00061F8C
			public unsafe bool valid
			{
				get
				{
					return this.m_Owner != null && this.m_IndexPlusOne != 0 && this.header->version == this.m_Version;
				}
			}

			// Token: 0x170005D2 RID: 1490
			// (get) Token: 0x06001581 RID: 5505 RVA: 0x00063DB3 File Offset: 0x00061FB3
			public InputStateHistory<TValue> owner
			{
				get
				{
					return this.m_Owner;
				}
			}

			// Token: 0x170005D3 RID: 1491
			// (get) Token: 0x06001582 RID: 5506 RVA: 0x00063DBB File Offset: 0x00061FBB
			public int index
			{
				get
				{
					this.CheckValid();
					return this.m_Owner.RecordIndexToUserIndex(this.recordIndex);
				}
			}

			// Token: 0x170005D4 RID: 1492
			// (get) Token: 0x06001583 RID: 5507 RVA: 0x00063DD4 File Offset: 0x00061FD4
			public unsafe double time
			{
				get
				{
					this.CheckValid();
					return this.header->time;
				}
			}

			// Token: 0x170005D5 RID: 1493
			// (get) Token: 0x06001584 RID: 5508 RVA: 0x00063DE8 File Offset: 0x00061FE8
			public unsafe InputControl<TValue> control
			{
				get
				{
					this.CheckValid();
					ReadOnlyArray<InputControl> controls = this.m_Owner.controls;
					if (controls.Count == 1 && !this.m_Owner.m_AddNewControls)
					{
						return (InputControl<TValue>)controls[0];
					}
					return (InputControl<TValue>)controls[this.header->controlIndex];
				}
			}

			// Token: 0x170005D6 RID: 1494
			// (get) Token: 0x06001585 RID: 5509 RVA: 0x00063E44 File Offset: 0x00062044
			public InputStateHistory<TValue>.Record next
			{
				get
				{
					this.CheckValid();
					int num = this.m_Owner.RecordIndexToUserIndex(this.recordIndex);
					if (num + 1 >= this.m_Owner.Count)
					{
						return default(InputStateHistory<TValue>.Record);
					}
					int num2 = this.m_Owner.UserIndexToRecordIndex(num + 1);
					return new InputStateHistory<TValue>.Record(this.m_Owner, num2, this.m_Owner.GetRecord(num2));
				}
			}

			// Token: 0x170005D7 RID: 1495
			// (get) Token: 0x06001586 RID: 5510 RVA: 0x00063EAC File Offset: 0x000620AC
			public InputStateHistory<TValue>.Record previous
			{
				get
				{
					this.CheckValid();
					int num = this.m_Owner.RecordIndexToUserIndex(this.recordIndex);
					if (num - 1 < 0)
					{
						return default(InputStateHistory<TValue>.Record);
					}
					int num2 = this.m_Owner.UserIndexToRecordIndex(num - 1);
					return new InputStateHistory<TValue>.Record(this.m_Owner, num2, this.m_Owner.GetRecord(num2));
				}
			}

			// Token: 0x06001587 RID: 5511 RVA: 0x00063F08 File Offset: 0x00062108
			internal unsafe Record(InputStateHistory<TValue> owner, int index, InputStateHistory.RecordHeader* header)
			{
				this.m_Owner = owner;
				this.m_IndexPlusOne = index + 1;
				this.m_Version = header->version;
			}

			// Token: 0x06001588 RID: 5512 RVA: 0x00063F26 File Offset: 0x00062126
			internal Record(InputStateHistory<TValue> owner, int index)
			{
				this.m_Owner = owner;
				this.m_IndexPlusOne = index + 1;
				this.m_Version = 0U;
			}

			// Token: 0x06001589 RID: 5513 RVA: 0x00063F3F File Offset: 0x0006213F
			public TValue ReadValue()
			{
				this.CheckValid();
				return this.m_Owner.ReadValue<TValue>(this.header);
			}

			// Token: 0x0600158A RID: 5514 RVA: 0x00063F58 File Offset: 0x00062158
			public unsafe void* GetUnsafeMemoryPtr()
			{
				this.CheckValid();
				return this.GetUnsafeMemoryPtrUnchecked();
			}

			// Token: 0x0600158B RID: 5515 RVA: 0x00063F68 File Offset: 0x00062168
			internal unsafe void* GetUnsafeMemoryPtrUnchecked()
			{
				if (this.m_Owner.controls.Count == 1 && !this.m_Owner.m_AddNewControls)
				{
					return (void*)this.header->statePtrWithoutControlIndex;
				}
				return (void*)this.header->statePtrWithControlIndex;
			}

			// Token: 0x0600158C RID: 5516 RVA: 0x00063FAF File Offset: 0x000621AF
			public unsafe void* GetUnsafeExtraMemoryPtr()
			{
				this.CheckValid();
				return this.GetUnsafeExtraMemoryPtrUnchecked();
			}

			// Token: 0x0600158D RID: 5517 RVA: 0x00063FBD File Offset: 0x000621BD
			internal unsafe void* GetUnsafeExtraMemoryPtrUnchecked()
			{
				if (this.m_Owner.extraMemoryPerRecord == 0)
				{
					throw new InvalidOperationException("No extra memory has been set up for history records; set extraMemoryPerRecord");
				}
				return (void*)(this.header + this.m_Owner.bytesPerRecord / sizeof(InputStateHistory.RecordHeader) - this.m_Owner.extraMemoryPerRecord / sizeof(InputStateHistory.RecordHeader));
			}

			// Token: 0x0600158E RID: 5518 RVA: 0x00063FF8 File Offset: 0x000621F8
			public void CopyFrom(InputStateHistory<TValue>.Record record)
			{
				this.CheckValid();
				if (!record.valid)
				{
					throw new ArgumentException("Given history record is not valid", "record");
				}
				InputStateHistory.Record record2 = new InputStateHistory.Record(this.m_Owner, this.recordIndex, this.header);
				record2.CopyFrom(new InputStateHistory.Record(record.m_Owner, record.recordIndex, record.header));
				this.m_Version = record2.version;
			}

			// Token: 0x0600158F RID: 5519 RVA: 0x0006406A File Offset: 0x0006226A
			private unsafe void CheckValid()
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

			// Token: 0x06001590 RID: 5520 RVA: 0x000640A5 File Offset: 0x000622A5
			public bool Equals(InputStateHistory<TValue>.Record other)
			{
				return this.m_Owner == other.m_Owner && this.m_IndexPlusOne == other.m_IndexPlusOne && this.m_Version == other.m_Version;
			}

			// Token: 0x06001591 RID: 5521 RVA: 0x000640D4 File Offset: 0x000622D4
			public override bool Equals(object obj)
			{
				if (obj is InputStateHistory<TValue>.Record)
				{
					InputStateHistory<TValue>.Record record = (InputStateHistory<TValue>.Record)obj;
					return this.Equals(record);
				}
				return false;
			}

			// Token: 0x06001592 RID: 5522 RVA: 0x000640F9 File Offset: 0x000622F9
			public override int GetHashCode()
			{
				return (((((this.m_Owner != null) ? this.m_Owner.GetHashCode() : 0) * 397) ^ this.m_IndexPlusOne) * 397) ^ (int)this.m_Version;
			}

			// Token: 0x06001593 RID: 5523 RVA: 0x0006412B File Offset: 0x0006232B
			public override string ToString()
			{
				if (!this.valid)
				{
					return "<Invalid>";
				}
				return string.Format("{{ control={0} value={1} time={2} }}", this.control, this.ReadValue(), this.time);
			}

			// Token: 0x04000BDE RID: 3038
			private readonly InputStateHistory<TValue> m_Owner;

			// Token: 0x04000BDF RID: 3039
			private readonly int m_IndexPlusOne;

			// Token: 0x04000BE0 RID: 3040
			private uint m_Version;
		}
	}
}
