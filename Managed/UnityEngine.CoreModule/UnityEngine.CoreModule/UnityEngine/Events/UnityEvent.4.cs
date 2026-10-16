using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine.Scripting;

namespace UnityEngine.Events
{
	// Token: 0x02000354 RID: 852
	[Serializable]
	public class UnityEvent<T0, T1, T2> : UnityEventBase
	{
		// Token: 0x06002869 RID: 10345 RVA: 0x0004E029 File Offset: 0x0004C229
		[RequiredByNativeCode]
		public UnityEvent()
		{
		}

		// Token: 0x0600286A RID: 10346 RVA: 0x0004E03A File Offset: 0x0004C23A
		public void AddListener(UnityAction<T0, T1, T2> call)
		{
			base.AddCall(UnityEvent<T0, T1, T2>.GetDelegate(call));
		}

		// Token: 0x0600286B RID: 10347 RVA: 0x0004DC7E File Offset: 0x0004BE7E
		public void RemoveListener(UnityAction<T0, T1, T2> call)
		{
			base.RemoveListener(call.Target, call.Method);
		}

		// Token: 0x0600286C RID: 10348 RVA: 0x0004E04C File Offset: 0x0004C24C
		protected override MethodInfo FindMethod_Impl(string name, Type targetObjType)
		{
			return UnityEventBase.GetValidMethodInfo(targetObjType, name, new Type[]
			{
				typeof(T0),
				typeof(T1),
				typeof(T2)
			});
		}

		// Token: 0x0600286D RID: 10349 RVA: 0x0004E094 File Offset: 0x0004C294
		internal override BaseInvokableCall GetDelegate(object target, MethodInfo theFunction)
		{
			return new InvokableCall<T0, T1, T2>(target, theFunction);
		}

		// Token: 0x0600286E RID: 10350 RVA: 0x0004E0B0 File Offset: 0x0004C2B0
		private static BaseInvokableCall GetDelegate(UnityAction<T0, T1, T2> action)
		{
			return new InvokableCall<T0, T1, T2>(action);
		}

		// Token: 0x0600286F RID: 10351 RVA: 0x0004E0C8 File Offset: 0x0004C2C8
		public void Invoke(T0 arg0, T1 arg1, T2 arg2)
		{
			List<BaseInvokableCall> list = base.PrepareInvoke();
			for (int i = 0; i < list.Count; i++)
			{
				InvokableCall<T0, T1, T2> invokableCall = list[i] as InvokableCall<T0, T1, T2>;
				bool flag = invokableCall != null;
				if (flag)
				{
					invokableCall.Invoke(arg0, arg1, arg2);
				}
				else
				{
					InvokableCall invokableCall2 = list[i] as InvokableCall;
					bool flag2 = invokableCall2 != null;
					if (flag2)
					{
						invokableCall2.Invoke();
					}
					else
					{
						BaseInvokableCall baseInvokableCall = list[i];
						bool flag3 = this.m_InvokeArray == null;
						if (flag3)
						{
							this.m_InvokeArray = new object[3];
						}
						this.m_InvokeArray[0] = arg0;
						this.m_InvokeArray[1] = arg1;
						this.m_InvokeArray[2] = arg2;
						baseInvokableCall.Invoke(this.m_InvokeArray);
					}
				}
			}
		}

		// Token: 0x04000B52 RID: 2898
		private object[] m_InvokeArray = null;
	}
}
