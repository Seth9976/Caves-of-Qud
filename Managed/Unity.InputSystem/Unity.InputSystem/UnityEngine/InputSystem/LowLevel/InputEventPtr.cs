using System;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000EA RID: 234
	public struct InputEventPtr : IEquatable<InputEventPtr>
	{
		// Token: 0x06000DEA RID: 3562 RVA: 0x000462EE File Offset: 0x000444EE
		public unsafe InputEventPtr(InputEvent* eventPtr)
		{
			this.m_EventPtr = eventPtr;
		}

		// Token: 0x170003F2 RID: 1010
		// (get) Token: 0x06000DEB RID: 3563 RVA: 0x000462F7 File Offset: 0x000444F7
		public bool valid
		{
			get
			{
				return this.m_EventPtr != null;
			}
		}

		// Token: 0x170003F3 RID: 1011
		// (get) Token: 0x06000DEC RID: 3564 RVA: 0x00046306 File Offset: 0x00044506
		// (set) Token: 0x06000DED RID: 3565 RVA: 0x0004631D File Offset: 0x0004451D
		public unsafe bool handled
		{
			get
			{
				return this.valid && this.m_EventPtr->handled;
			}
			set
			{
				if (!this.valid)
				{
					throw new InvalidOperationException("The InputEventPtr is not valid.");
				}
				this.m_EventPtr->handled = value;
			}
		}

		// Token: 0x170003F4 RID: 1012
		// (get) Token: 0x06000DEE RID: 3566 RVA: 0x0004633E File Offset: 0x0004453E
		// (set) Token: 0x06000DEF RID: 3567 RVA: 0x00046355 File Offset: 0x00044555
		public unsafe int id
		{
			get
			{
				if (!this.valid)
				{
					return 0;
				}
				return this.m_EventPtr->eventId;
			}
			set
			{
				if (!this.valid)
				{
					throw new InvalidOperationException("The InputEventPtr is not valid.");
				}
				this.m_EventPtr->eventId = value;
			}
		}

		// Token: 0x170003F5 RID: 1013
		// (get) Token: 0x06000DF0 RID: 3568 RVA: 0x00046378 File Offset: 0x00044578
		public unsafe FourCC type
		{
			get
			{
				if (!this.valid)
				{
					return default(FourCC);
				}
				return this.m_EventPtr->type;
			}
		}

		// Token: 0x170003F6 RID: 1014
		// (get) Token: 0x06000DF1 RID: 3569 RVA: 0x000463A2 File Offset: 0x000445A2
		public unsafe uint sizeInBytes
		{
			get
			{
				if (!this.valid)
				{
					return 0U;
				}
				return this.m_EventPtr->sizeInBytes;
			}
		}

		// Token: 0x170003F7 RID: 1015
		// (get) Token: 0x06000DF2 RID: 3570 RVA: 0x000463B9 File Offset: 0x000445B9
		// (set) Token: 0x06000DF3 RID: 3571 RVA: 0x000463D0 File Offset: 0x000445D0
		public unsafe int deviceId
		{
			get
			{
				if (!this.valid)
				{
					return 0;
				}
				return this.m_EventPtr->deviceId;
			}
			set
			{
				if (!this.valid)
				{
					throw new InvalidOperationException("The InputEventPtr is not valid.");
				}
				this.m_EventPtr->deviceId = value;
			}
		}

		// Token: 0x170003F8 RID: 1016
		// (get) Token: 0x06000DF4 RID: 3572 RVA: 0x000463F1 File Offset: 0x000445F1
		// (set) Token: 0x06000DF5 RID: 3573 RVA: 0x00046410 File Offset: 0x00044610
		public unsafe double time
		{
			get
			{
				if (!this.valid)
				{
					return 0.0;
				}
				return this.m_EventPtr->time;
			}
			set
			{
				if (!this.valid)
				{
					throw new InvalidOperationException("The InputEventPtr is not valid.");
				}
				this.m_EventPtr->time = value;
			}
		}

		// Token: 0x170003F9 RID: 1017
		// (get) Token: 0x06000DF6 RID: 3574 RVA: 0x00046431 File Offset: 0x00044631
		// (set) Token: 0x06000DF7 RID: 3575 RVA: 0x00046450 File Offset: 0x00044650
		internal unsafe double internalTime
		{
			get
			{
				if (!this.valid)
				{
					return 0.0;
				}
				return this.m_EventPtr->internalTime;
			}
			set
			{
				if (!this.valid)
				{
					throw new InvalidOperationException("The InputEventPtr is not valid.");
				}
				this.m_EventPtr->internalTime = value;
			}
		}

		// Token: 0x170003FA RID: 1018
		// (get) Token: 0x06000DF8 RID: 3576 RVA: 0x00046471 File Offset: 0x00044671
		public unsafe InputEvent* data
		{
			get
			{
				return this.m_EventPtr;
			}
		}

		// Token: 0x170003FB RID: 1019
		// (get) Token: 0x06000DF9 RID: 3577 RVA: 0x0004647C File Offset: 0x0004467C
		internal unsafe FourCC stateFormat
		{
			get
			{
				FourCC type = this.type;
				if (type == 1398030676)
				{
					return StateEvent.FromUnchecked(this)->stateFormat;
				}
				if (type == 1145852993)
				{
					return DeltaStateEvent.FromUnchecked(this)->stateFormat;
				}
				string text = "Event must be a StateEvent or DeltaStateEvent but is ";
				InputEventPtr inputEventPtr = this;
				throw new InvalidOperationException(text + inputEventPtr.ToString());
			}
		}

		// Token: 0x170003FC RID: 1020
		// (get) Token: 0x06000DFA RID: 3578 RVA: 0x000464FC File Offset: 0x000446FC
		internal unsafe uint stateSizeInBytes
		{
			get
			{
				if (this.IsA<StateEvent>())
				{
					return StateEvent.From(this)->stateSizeInBytes;
				}
				if (this.IsA<DeltaStateEvent>())
				{
					return DeltaStateEvent.From(this)->deltaStateSizeInBytes;
				}
				string text = "Event must be a StateEvent or DeltaStateEvent but is ";
				InputEventPtr inputEventPtr = this;
				throw new InvalidOperationException(text + inputEventPtr.ToString());
			}
		}

		// Token: 0x170003FD RID: 1021
		// (get) Token: 0x06000DFB RID: 3579 RVA: 0x00046560 File Offset: 0x00044760
		internal unsafe uint stateOffset
		{
			get
			{
				if (this.IsA<DeltaStateEvent>())
				{
					return DeltaStateEvent.From(this)->stateOffset;
				}
				string text = "Event must be a DeltaStateEvent but is ";
				InputEventPtr inputEventPtr = this;
				throw new InvalidOperationException(text + inputEventPtr.ToString());
			}
		}

		// Token: 0x06000DFC RID: 3580 RVA: 0x000465AC File Offset: 0x000447AC
		public unsafe bool IsA<TOtherEvent>() where TOtherEvent : struct, IInputEventTypeInfo
		{
			if (this.m_EventPtr == null)
			{
				return false;
			}
			TOtherEvent totherEvent = default(TOtherEvent);
			return this.m_EventPtr->type == totherEvent.typeStatic;
		}

		// Token: 0x06000DFD RID: 3581 RVA: 0x000465EC File Offset: 0x000447EC
		public InputEventPtr Next()
		{
			if (!this.valid)
			{
				return default(InputEventPtr);
			}
			return new InputEventPtr(InputEvent.GetNextInMemory(this.m_EventPtr));
		}

		// Token: 0x06000DFE RID: 3582 RVA: 0x0004661C File Offset: 0x0004481C
		public unsafe override string ToString()
		{
			if (!this.valid)
			{
				return "null";
			}
			InputEvent inputEvent = *this.m_EventPtr;
			return inputEvent.ToString();
		}

		// Token: 0x06000DFF RID: 3583 RVA: 0x00046650 File Offset: 0x00044850
		public unsafe InputEvent* ToPointer()
		{
			return this;
		}

		// Token: 0x06000E00 RID: 3584 RVA: 0x0004665D File Offset: 0x0004485D
		public bool Equals(InputEventPtr other)
		{
			return this.m_EventPtr == other.m_EventPtr || InputEvent.Equals(this.m_EventPtr, other.m_EventPtr);
		}

		// Token: 0x06000E01 RID: 3585 RVA: 0x00046680 File Offset: 0x00044880
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			if (obj is InputEventPtr)
			{
				InputEventPtr inputEventPtr = (InputEventPtr)obj;
				return this.Equals(inputEventPtr);
			}
			return false;
		}

		// Token: 0x06000E02 RID: 3586 RVA: 0x000466AA File Offset: 0x000448AA
		public override int GetHashCode()
		{
			return this.m_EventPtr;
		}

		// Token: 0x06000E03 RID: 3587 RVA: 0x000466B4 File Offset: 0x000448B4
		public static bool operator ==(InputEventPtr left, InputEventPtr right)
		{
			return left.m_EventPtr == right.m_EventPtr;
		}

		// Token: 0x06000E04 RID: 3588 RVA: 0x000466C4 File Offset: 0x000448C4
		public static bool operator !=(InputEventPtr left, InputEventPtr right)
		{
			return left.m_EventPtr != right.m_EventPtr;
		}

		// Token: 0x06000E05 RID: 3589 RVA: 0x000466D7 File Offset: 0x000448D7
		public unsafe static implicit operator InputEventPtr(InputEvent* eventPtr)
		{
			return new InputEventPtr(eventPtr);
		}

		// Token: 0x06000E06 RID: 3590 RVA: 0x000466DF File Offset: 0x000448DF
		public unsafe static InputEventPtr From(InputEvent* eventPtr)
		{
			return new InputEventPtr(eventPtr);
		}

		// Token: 0x06000E07 RID: 3591 RVA: 0x000466E7 File Offset: 0x000448E7
		public unsafe static implicit operator InputEvent*(InputEventPtr eventPtr)
		{
			return eventPtr.data;
		}

		// Token: 0x06000E08 RID: 3592 RVA: 0x000466F0 File Offset: 0x000448F0
		public unsafe static InputEvent* FromInputEventPtr(InputEventPtr eventPtr)
		{
			return eventPtr.data;
		}

		// Token: 0x040005C5 RID: 1477
		private unsafe readonly InputEvent* m_EventPtr;
	}
}
