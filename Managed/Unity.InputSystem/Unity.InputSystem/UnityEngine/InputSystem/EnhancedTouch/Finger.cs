using System;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.EnhancedTouch
{
	// Token: 0x0200009E RID: 158
	public class Finger
	{
		// Token: 0x17000336 RID: 822
		// (get) Token: 0x06000C46 RID: 3142 RVA: 0x000422BF File Offset: 0x000404BF
		public Touchscreen screen { get; }

		// Token: 0x17000337 RID: 823
		// (get) Token: 0x06000C47 RID: 3143 RVA: 0x000422C7 File Offset: 0x000404C7
		public int index { get; }

		// Token: 0x17000338 RID: 824
		// (get) Token: 0x06000C48 RID: 3144 RVA: 0x000422D0 File Offset: 0x000404D0
		public bool isActive
		{
			get
			{
				return this.currentTouch.valid;
			}
		}

		// Token: 0x17000339 RID: 825
		// (get) Token: 0x06000C49 RID: 3145 RVA: 0x000422EC File Offset: 0x000404EC
		public Vector2 screenPosition
		{
			get
			{
				Touch lastTouch = this.lastTouch;
				if (!lastTouch.valid)
				{
					return default(Vector2);
				}
				return lastTouch.screenPosition;
			}
		}

		// Token: 0x1700033A RID: 826
		// (get) Token: 0x06000C4A RID: 3146 RVA: 0x0004231C File Offset: 0x0004051C
		public Touch lastTouch
		{
			get
			{
				int count = this.m_StateHistory.Count;
				if (count == 0)
				{
					return default(Touch);
				}
				return new Touch(this, this.m_StateHistory[count - 1]);
			}
		}

		// Token: 0x1700033B RID: 827
		// (get) Token: 0x06000C4B RID: 3147 RVA: 0x00042358 File Offset: 0x00040558
		public Touch currentTouch
		{
			get
			{
				Touch lastTouch = this.lastTouch;
				if (!lastTouch.valid)
				{
					return default(Touch);
				}
				if (lastTouch.isInProgress)
				{
					return lastTouch;
				}
				if (lastTouch.updateStepCount == InputUpdate.s_UpdateStepCount)
				{
					return lastTouch;
				}
				return default(Touch);
			}
		}

		// Token: 0x1700033C RID: 828
		// (get) Token: 0x06000C4C RID: 3148 RVA: 0x000423A3 File Offset: 0x000405A3
		public TouchHistory touchHistory
		{
			get
			{
				return new TouchHistory(this, this.m_StateHistory, -1, -1);
			}
		}

		// Token: 0x06000C4D RID: 3149 RVA: 0x000423B4 File Offset: 0x000405B4
		internal unsafe Finger(Touchscreen screen, int index, InputUpdateType updateMask)
		{
			this.screen = screen;
			this.index = index;
			this.m_StateHistory = new InputStateHistory<TouchState>(screen.touches[index])
			{
				historyDepth = Touch.maxHistoryLengthPerFinger,
				extraMemoryPerRecord = UnsafeUtility.SizeOf<Touch.ExtraDataPerTouchState>(),
				onRecordAdded = new Action<InputStateHistory.Record>(this.OnTouchRecorded),
				onShouldRecordStateChange = new Func<InputControl, double, InputEventPtr, bool>(Finger.ShouldRecordTouch),
				updateMask = updateMask
			};
			this.m_StateHistory.StartRecording();
			if (screen.touches[index].isInProgress)
			{
				this.m_StateHistory.RecordStateChange(screen.touches[index], *screen.touches[index].value, -1.0);
			}
		}

		// Token: 0x06000C4E RID: 3150 RVA: 0x00042490 File Offset: 0x00040690
		private unsafe static bool ShouldRecordTouch(InputControl control, double time, InputEventPtr eventPtr)
		{
			if (!eventPtr.valid)
			{
				return false;
			}
			FourCC type = eventPtr.type;
			if (type != 1398030676 && type != 1145852993)
			{
				return false;
			}
			TouchState* ptr = (TouchState*)((byte*)control.currentStatePtr + control.stateBlock.byteOffset);
			return !ptr->isTapRelease;
		}

		// Token: 0x06000C4F RID: 3151 RVA: 0x000424F8 File Offset: 0x000406F8
		private unsafe void OnTouchRecorded(InputStateHistory.Record record)
		{
			int recordIndex = record.recordIndex;
			InputStateHistory.RecordHeader* recordUnchecked = this.m_StateHistory.GetRecordUnchecked(recordIndex);
			TouchState* statePtrWithoutControlIndex = (TouchState*)recordUnchecked->statePtrWithoutControlIndex;
			statePtrWithoutControlIndex->updateStepCount = InputUpdate.s_UpdateStepCount;
			Touch.s_GlobalState.playerState.haveBuiltActiveTouches = false;
			Touch.ExtraDataPerTouchState* ptr = (Touch.ExtraDataPerTouchState*)(recordUnchecked + this.m_StateHistory.bytesPerRecord / sizeof(InputStateHistory.RecordHeader) - UnsafeUtility.SizeOf<Touch.ExtraDataPerTouchState>() / sizeof(InputStateHistory.RecordHeader));
			ref Touch.ExtraDataPerTouchState ptr2 = ref *ptr;
			uint num = Touch.s_GlobalState.playerState.lastId + 1U;
			Touch.s_GlobalState.playerState.lastId = num;
			ptr2.uniqueId = num;
			ptr->accumulatedDelta = statePtrWithoutControlIndex->delta;
			if (statePtrWithoutControlIndex->phase != TouchPhase.Began)
			{
				if (recordIndex != this.m_StateHistory.m_HeadIndex)
				{
					int num2 = ((recordIndex == 0) ? (this.m_StateHistory.historyDepth - 1) : (recordIndex - 1));
					TouchState* statePtrWithoutControlIndex2 = (TouchState*)this.m_StateHistory.GetRecordUnchecked(num2)->statePtrWithoutControlIndex;
					statePtrWithoutControlIndex->delta -= statePtrWithoutControlIndex2->delta;
					statePtrWithoutControlIndex->beganInSameFrame = statePtrWithoutControlIndex2->beganInSameFrame && statePtrWithoutControlIndex2->updateStepCount == statePtrWithoutControlIndex->updateStepCount;
				}
			}
			else
			{
				statePtrWithoutControlIndex->beganInSameFrame = true;
			}
			switch (statePtrWithoutControlIndex->phase)
			{
			case TouchPhase.Began:
				DelegateHelpers.InvokeCallbacksSafe<Finger>(ref Touch.s_GlobalState.onFingerDown, this, "Touch.onFingerDown", null);
				return;
			case TouchPhase.Moved:
				DelegateHelpers.InvokeCallbacksSafe<Finger>(ref Touch.s_GlobalState.onFingerMove, this, "Touch.onFingerMove", null);
				return;
			case TouchPhase.Ended:
			case TouchPhase.Canceled:
				DelegateHelpers.InvokeCallbacksSafe<Finger>(ref Touch.s_GlobalState.onFingerUp, this, "Touch.onFingerUp", null);
				return;
			default:
				return;
			}
		}

		// Token: 0x06000C50 RID: 3152 RVA: 0x00042668 File Offset: 0x00040868
		private unsafe Touch FindTouch(uint uniqueId)
		{
			foreach (InputStateHistory<TouchState>.Record record in this.m_StateHistory)
			{
				if (((Touch.ExtraDataPerTouchState*)record.GetUnsafeExtraMemoryPtrUnchecked())->uniqueId == uniqueId)
				{
					return new Touch(this, record);
				}
			}
			return default(Touch);
		}

		// Token: 0x06000C51 RID: 3153 RVA: 0x000426D4 File Offset: 0x000408D4
		internal unsafe TouchHistory GetTouchHistory(Touch touch)
		{
			InputStateHistory<TouchState>.Record touchRecord = touch.m_TouchRecord;
			if (touchRecord.owner != this.m_StateHistory)
			{
				touch = this.FindTouch(touch.uniqueId);
				if (!touch.valid)
				{
					return default(TouchHistory);
				}
			}
			int touchId = touch.touchId;
			int num = touch.m_TouchRecord.index;
			int num2 = 0;
			if (touch.phase != TouchPhase.Began)
			{
				InputStateHistory<TouchState>.Record record = touch.m_TouchRecord.previous;
				while (record.valid)
				{
					TouchState* unsafeMemoryPtr = (TouchState*)record.GetUnsafeMemoryPtr();
					if (unsafeMemoryPtr->touchId != touchId)
					{
						break;
					}
					num2++;
					if (unsafeMemoryPtr->phase == TouchPhase.Began)
					{
						break;
					}
					record = record.previous;
				}
			}
			if (num2 == 0)
			{
				return default(TouchHistory);
			}
			num--;
			return new TouchHistory(this, this.m_StateHistory, num, num2);
		}

		// Token: 0x04000487 RID: 1159
		internal readonly InputStateHistory<TouchState> m_StateHistory;
	}
}
