using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem
{
	// Token: 0x02000030 RID: 48
	public abstract class InputControl<TValue> : InputControl where TValue : struct
	{
		// Token: 0x17000118 RID: 280
		// (get) Token: 0x060003E5 RID: 997 RVA: 0x00010B20 File Offset: 0x0000ED20
		public override Type valueType
		{
			get
			{
				return typeof(TValue);
			}
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x060003E6 RID: 998 RVA: 0x00010B2C File Offset: 0x0000ED2C
		public override int valueSizeInBytes
		{
			get
			{
				return UnsafeUtility.SizeOf<TValue>();
			}
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x060003E7 RID: 999 RVA: 0x00010B34 File Offset: 0x0000ED34
		public unsafe readonly ref TValue value
		{
			get
			{
				if (!InputSystem.s_Manager.readValueCachingFeatureEnabled || this.m_CachedValueIsStale || this.evaluateProcessorsEveryRead)
				{
					this.m_CachedValue = this.ProcessValue(*this.unprocessedValue);
					this.m_CachedValueIsStale = false;
				}
				return ref this.m_CachedValue;
			}
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x060003E8 RID: 1000 RVA: 0x00010B84 File Offset: 0x0000ED84
		internal readonly ref TValue unprocessedValue
		{
			get
			{
				if (base.currentStatePtr == null)
				{
					return ref this.m_UnprocessedCachedValue;
				}
				if (!InputSystem.s_Manager.readValueCachingFeatureEnabled || this.m_UnprocessedCachedValueIsStale)
				{
					this.m_UnprocessedCachedValue = this.ReadUnprocessedValueFromState(base.currentStatePtr);
					this.m_UnprocessedCachedValueIsStale = false;
				}
				return ref this.m_UnprocessedCachedValue;
			}
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x00010BD5 File Offset: 0x0000EDD5
		public unsafe TValue ReadValue()
		{
			return *this.value;
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x00010BE2 File Offset: 0x0000EDE2
		public TValue ReadValueFromPreviousFrame()
		{
			return this.ReadValueFromState(base.previousFrameStatePtr);
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x00010BF0 File Offset: 0x0000EDF0
		public TValue ReadDefaultValue()
		{
			return this.ReadValueFromState(base.defaultStatePtr);
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x00010BFE File Offset: 0x0000EDFE
		public unsafe TValue ReadValueFromState(void* statePtr)
		{
			if (statePtr == null)
			{
				throw new ArgumentNullException("statePtr");
			}
			return this.ProcessValue(this.ReadUnprocessedValueFromState(statePtr));
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x00010C1D File Offset: 0x0000EE1D
		public unsafe TValue ReadValueFromStateWithCaching(void* statePtr)
		{
			if (statePtr != base.currentStatePtr)
			{
				return this.ReadValueFromState(statePtr);
			}
			return *this.value;
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x00010C3B File Offset: 0x0000EE3B
		public unsafe TValue ReadUnprocessedValueFromStateWithCaching(void* statePtr)
		{
			if (statePtr != base.currentStatePtr)
			{
				return this.ReadUnprocessedValueFromState(statePtr);
			}
			return *this.unprocessedValue;
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x00010C59 File Offset: 0x0000EE59
		public unsafe TValue ReadUnprocessedValue()
		{
			return *this.unprocessedValue;
		}

		// Token: 0x060003F0 RID: 1008
		public unsafe abstract TValue ReadUnprocessedValueFromState(void* statePtr);

		// Token: 0x060003F1 RID: 1009 RVA: 0x00010C66 File Offset: 0x0000EE66
		public unsafe override object ReadValueFromStateAsObject(void* statePtr)
		{
			return this.ReadValueFromState(statePtr);
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x00010C74 File Offset: 0x0000EE74
		public unsafe override void ReadValueFromStateIntoBuffer(void* statePtr, void* bufferPtr, int bufferSize)
		{
			if (statePtr == null)
			{
				throw new ArgumentNullException("statePtr");
			}
			if (bufferPtr == null)
			{
				throw new ArgumentNullException("bufferPtr");
			}
			int num = UnsafeUtility.SizeOf<TValue>();
			if (bufferSize < num)
			{
				throw new ArgumentException(string.Format("bufferSize={0} < sizeof(TValue)={1}", bufferSize, num), "bufferSize");
			}
			TValue tvalue = this.ReadValueFromState(statePtr);
			void* ptr = UnsafeUtility.AddressOf<TValue>(ref tvalue);
			UnsafeUtility.MemCpy(bufferPtr, ptr, (long)num);
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x00010CE8 File Offset: 0x0000EEE8
		public unsafe override void WriteValueFromBufferIntoState(void* bufferPtr, int bufferSize, void* statePtr)
		{
			if (bufferPtr == null)
			{
				throw new ArgumentNullException("bufferPtr");
			}
			if (statePtr == null)
			{
				throw new ArgumentNullException("statePtr");
			}
			int num = UnsafeUtility.SizeOf<TValue>();
			if (bufferSize < num)
			{
				throw new ArgumentException(string.Format("bufferSize={0} < sizeof(TValue)={1}", bufferSize, num), "bufferSize");
			}
			TValue tvalue = default(TValue);
			UnsafeUtility.MemCpy(UnsafeUtility.AddressOf<TValue>(ref tvalue), bufferPtr, (long)num);
			this.WriteValueIntoState(tvalue, statePtr);
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x00010D60 File Offset: 0x0000EF60
		public unsafe override void WriteValueFromObjectIntoState(object value, void* statePtr)
		{
			if (statePtr == null)
			{
				throw new ArgumentNullException("statePtr");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (!(value is TValue))
			{
				value = Convert.ChangeType(value, typeof(TValue));
			}
			TValue tvalue = (TValue)((object)value);
			this.WriteValueIntoState(tvalue, statePtr);
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x00010DB4 File Offset: 0x0000EFB4
		public unsafe virtual void WriteValueIntoState(TValue value, void* statePtr)
		{
			throw new NotSupportedException(string.Format("Control '{0}' does not support writing", this));
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x00010DC8 File Offset: 0x0000EFC8
		public unsafe override object ReadValueFromBufferAsObject(void* buffer, int bufferSize)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			int num = UnsafeUtility.SizeOf<TValue>();
			if (bufferSize < num)
			{
				throw new ArgumentException(string.Format("Expecting buffer of at least {0} bytes for value of type {1} but got buffer of only {2} bytes instead", num, typeof(TValue).Name, bufferSize), "bufferSize");
			}
			TValue tvalue = default(TValue);
			UnsafeUtility.MemCpy(UnsafeUtility.AddressOf<TValue>(ref tvalue), buffer, (long)num);
			return tvalue;
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x00010E3C File Offset: 0x0000F03C
		private unsafe static bool CompareValue(ref TValue firstValue, ref TValue secondValue)
		{
			void* ptr = UnsafeUtility.AddressOf<TValue>(ref firstValue);
			void* ptr2 = UnsafeUtility.AddressOf<TValue>(ref secondValue);
			return UnsafeUtility.MemCmp(ptr, ptr2, (long)UnsafeUtility.SizeOf<TValue>()) != 0;
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x00010E68 File Offset: 0x0000F068
		public unsafe override bool CompareValue(void* firstStatePtr, void* secondStatePtr)
		{
			TValue tvalue = this.ReadValueFromState(firstStatePtr);
			TValue tvalue2 = this.ReadValueFromState(secondStatePtr);
			return InputControl<TValue>.CompareValue(ref tvalue, ref tvalue2);
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x00010E8E File Offset: 0x0000F08E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public TValue ProcessValue(TValue value)
		{
			this.ProcessValue(ref value);
			return value;
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x00010E9C File Offset: 0x0000F09C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void ProcessValue(ref TValue value)
		{
			if (this.m_ProcessorStack.length <= 0)
			{
				return;
			}
			value = this.m_ProcessorStack.firstValue.Process(value, this);
			if (this.m_ProcessorStack.additionalValues == null)
			{
				return;
			}
			for (int i = 0; i < this.m_ProcessorStack.length - 1; i++)
			{
				value = this.m_ProcessorStack.additionalValues[i].Process(value, this);
			}
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x00010F1C File Offset: 0x0000F11C
		internal TProcessor TryGetProcessor<TProcessor>() where TProcessor : InputProcessor<TValue>
		{
			if (this.m_ProcessorStack.length > 0)
			{
				TProcessor tprocessor = this.m_ProcessorStack.firstValue as TProcessor;
				if (tprocessor != null)
				{
					return tprocessor;
				}
				if (this.m_ProcessorStack.additionalValues != null)
				{
					for (int i = 0; i < this.m_ProcessorStack.length - 1; i++)
					{
						TProcessor tprocessor2 = this.m_ProcessorStack.additionalValues[i] as TProcessor;
						if (tprocessor2 != null)
						{
							return tprocessor2;
						}
					}
				}
			}
			return default(TProcessor);
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x00010FA8 File Offset: 0x0000F1A8
		internal override void AddProcessor(object processor)
		{
			InputProcessor<TValue> inputProcessor = processor as InputProcessor<TValue>;
			if (inputProcessor == null)
			{
				throw new ArgumentException(string.Concat(new string[]
				{
					"Cannot add processor of type '",
					processor.GetType().Name,
					"' to control of type '",
					base.GetType().Name,
					"'"
				}), "processor");
			}
			this.m_ProcessorStack.Append(inputProcessor);
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x00011018 File Offset: 0x0000F218
		protected override void FinishSetup()
		{
			using (IEnumerator<InputProcessor<TValue>> enumerator = this.m_ProcessorStack.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.cachingPolicy == InputProcessor.CachingPolicy.EvaluateOnEveryRead)
					{
						this.evaluateProcessorsEveryRead = true;
					}
				}
			}
			base.FinishSetup();
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x060003FE RID: 1022 RVA: 0x00011074 File Offset: 0x0000F274
		internal InputProcessor<TValue>[] processors
		{
			get
			{
				return this.m_ProcessorStack.ToArray();
			}
		}

		// Token: 0x0400013D RID: 317
		internal InlinedArray<InputProcessor<TValue>> m_ProcessorStack;

		// Token: 0x0400013E RID: 318
		private TValue m_CachedValue;

		// Token: 0x0400013F RID: 319
		private TValue m_UnprocessedCachedValue;

		// Token: 0x04000140 RID: 320
		internal bool evaluateProcessorsEveryRead;
	}
}
