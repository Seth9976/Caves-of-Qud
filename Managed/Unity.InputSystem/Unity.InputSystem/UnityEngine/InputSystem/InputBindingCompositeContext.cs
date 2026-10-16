using System;
using System.Collections.Generic;

namespace UnityEngine.InputSystem
{
	// Token: 0x02000029 RID: 41
	public struct InputBindingCompositeContext
	{
		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x060002EE RID: 750 RVA: 0x0000D0A0 File Offset: 0x0000B2A0
		public unsafe IEnumerable<InputBindingCompositeContext.PartBinding> controls
		{
			get
			{
				if (this.m_State == null)
				{
					yield break;
				}
				int totalBindingCount = this.m_State.totalBindingCount;
				int num;
				for (int bindingIndex = this.m_BindingIndex + 1; bindingIndex < totalBindingCount; bindingIndex = num)
				{
					InputActionState.BindingState bindingState = *this.m_State.GetBindingState(bindingIndex);
					if (!bindingState.isPartOfComposite)
					{
						break;
					}
					int controlStartIndex = bindingState.controlStartIndex;
					for (int i = 0; i < bindingState.controlCount; i = num)
					{
						InputControl inputControl = this.m_State.controls[controlStartIndex + i];
						yield return new InputBindingCompositeContext.PartBinding
						{
							part = bindingState.partIndex,
							control = inputControl
						};
						num = i + 1;
					}
					num = bindingIndex + 1;
				}
				yield break;
			}
		}

		// Token: 0x060002EF RID: 751 RVA: 0x0000D0B5 File Offset: 0x0000B2B5
		public float EvaluateMagnitude(int partNumber)
		{
			return this.m_State.EvaluateCompositePartMagnitude(this.m_BindingIndex, partNumber);
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x0000D0CC File Offset: 0x0000B2CC
		public TValue ReadValue<TValue>(int partNumber) where TValue : struct, IComparable<TValue>
		{
			if (this.m_State == null)
			{
				return default(TValue);
			}
			int num;
			return this.m_State.ReadCompositePartValue<TValue, InputBindingCompositeContext.DefaultComparer<TValue>>(this.m_BindingIndex, partNumber, null, out num, default(InputBindingCompositeContext.DefaultComparer<TValue>));
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x0000D10C File Offset: 0x0000B30C
		public TValue ReadValue<TValue>(int partNumber, out InputControl sourceControl) where TValue : struct, IComparable<TValue>
		{
			if (this.m_State == null)
			{
				sourceControl = null;
				return default(TValue);
			}
			int num;
			TValue tvalue = this.m_State.ReadCompositePartValue<TValue, InputBindingCompositeContext.DefaultComparer<TValue>>(this.m_BindingIndex, partNumber, null, out num, default(InputBindingCompositeContext.DefaultComparer<TValue>));
			if (num != -1)
			{
				sourceControl = this.m_State.controls[num];
				return tvalue;
			}
			sourceControl = null;
			return tvalue;
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x0000D164 File Offset: 0x0000B364
		public TValue ReadValue<TValue, TComparer>(int partNumber, TComparer comparer = default(TComparer)) where TValue : struct where TComparer : IComparer<TValue>
		{
			if (this.m_State == null)
			{
				return default(TValue);
			}
			int num;
			return this.m_State.ReadCompositePartValue<TValue, TComparer>(this.m_BindingIndex, partNumber, null, out num, comparer);
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x0000D19C File Offset: 0x0000B39C
		public TValue ReadValue<TValue, TComparer>(int partNumber, out InputControl sourceControl, TComparer comparer = default(TComparer)) where TValue : struct where TComparer : IComparer<TValue>
		{
			if (this.m_State == null)
			{
				sourceControl = null;
				return default(TValue);
			}
			int num;
			TValue tvalue = this.m_State.ReadCompositePartValue<TValue, TComparer>(this.m_BindingIndex, partNumber, null, out num, comparer);
			if (num != -1)
			{
				sourceControl = this.m_State.controls[num];
				return tvalue;
			}
			sourceControl = null;
			return tvalue;
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x0000D1EC File Offset: 0x0000B3EC
		public unsafe bool ReadValueAsButton(int partNumber)
		{
			if (this.m_State == null)
			{
				return false;
			}
			bool flag = false;
			int num;
			this.m_State.ReadCompositePartValue<float, InputBindingCompositeContext.DefaultComparer<float>>(this.m_BindingIndex, partNumber, &flag, out num, default(InputBindingCompositeContext.DefaultComparer<float>));
			return flag;
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x0000D227 File Offset: 0x0000B427
		public unsafe void ReadValue(int partNumber, void* buffer, int bufferSize)
		{
			InputActionState state = this.m_State;
			if (state == null)
			{
				return;
			}
			state.ReadCompositePartValue(this.m_BindingIndex, partNumber, buffer, bufferSize);
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x0000D243 File Offset: 0x0000B443
		public object ReadValueAsObject(int partNumber)
		{
			return this.m_State.ReadCompositePartValueAsObject(this.m_BindingIndex, partNumber);
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x0000D257 File Offset: 0x0000B457
		public double GetPressTime(int partNumber)
		{
			return this.m_State.GetCompositePartPressTime(this.m_BindingIndex, partNumber);
		}

		// Token: 0x040000F1 RID: 241
		internal InputActionState m_State;

		// Token: 0x040000F2 RID: 242
		internal int m_BindingIndex;

		// Token: 0x02000183 RID: 387
		public struct PartBinding
		{
			// Token: 0x1700055D RID: 1373
			// (get) Token: 0x060013BD RID: 5053 RVA: 0x0005C3D7 File Offset: 0x0005A5D7
			// (set) Token: 0x060013BE RID: 5054 RVA: 0x0005C3DF File Offset: 0x0005A5DF
			public int part { readonly get; set; }

			// Token: 0x1700055E RID: 1374
			// (get) Token: 0x060013BF RID: 5055 RVA: 0x0005C3E8 File Offset: 0x0005A5E8
			// (set) Token: 0x060013C0 RID: 5056 RVA: 0x0005C3F0 File Offset: 0x0005A5F0
			public InputControl control { readonly get; set; }
		}

		// Token: 0x02000184 RID: 388
		private struct DefaultComparer<TValue> : IComparer<TValue> where TValue : IComparable<TValue>
		{
			// Token: 0x060013C1 RID: 5057 RVA: 0x0005C3F9 File Offset: 0x0005A5F9
			public int Compare(TValue x, TValue y)
			{
				return x.CompareTo(y);
			}
		}
	}
}
