using System;
using Unity.Collections.LowLevel.Unsafe;

namespace UnityEngine.InputSystem
{
	// Token: 0x02000028 RID: 40
	public abstract class InputBindingComposite<TValue> : InputBindingComposite where TValue : struct
	{
		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x060002E8 RID: 744 RVA: 0x0000CFED File Offset: 0x0000B1ED
		public override Type valueType
		{
			get
			{
				return typeof(TValue);
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x060002E9 RID: 745 RVA: 0x0000CFF9 File Offset: 0x0000B1F9
		public override int valueSizeInBytes
		{
			get
			{
				return UnsafeUtility.SizeOf<TValue>();
			}
		}

		// Token: 0x060002EA RID: 746
		public abstract TValue ReadValue(ref InputBindingCompositeContext context);

		// Token: 0x060002EB RID: 747 RVA: 0x0000D000 File Offset: 0x0000B200
		public unsafe override void ReadValue(ref InputBindingCompositeContext context, void* buffer, int bufferSize)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			int num = UnsafeUtility.SizeOf<TValue>();
			if (bufferSize < num)
			{
				throw new ArgumentException(string.Format("Expected buffer of at least {0} bytes but got buffer of only {1} bytes instead", UnsafeUtility.SizeOf<TValue>(), bufferSize), "bufferSize");
			}
			TValue tvalue = this.ReadValue(ref context);
			void* ptr = UnsafeUtility.AddressOf<TValue>(ref tvalue);
			UnsafeUtility.MemCpy(buffer, ptr, (long)num);
		}

		// Token: 0x060002EC RID: 748 RVA: 0x0000D068 File Offset: 0x0000B268
		public unsafe override object ReadValueAsObject(ref InputBindingCompositeContext context)
		{
			TValue tvalue = default(TValue);
			void* ptr = UnsafeUtility.AddressOf<TValue>(ref tvalue);
			this.ReadValue(ref context, ptr, UnsafeUtility.SizeOf<TValue>());
			return tvalue;
		}
	}
}
