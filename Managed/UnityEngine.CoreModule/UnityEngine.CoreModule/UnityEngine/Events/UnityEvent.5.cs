using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine.Scripting;

namespace UnityEngine.Events
{
	// Token: 0x02000356 RID: 854
	[Serializable]
	public class UnityEvent<T0, T1, T2, T3> : UnityEventBase
	{
		// Token: 0x06002874 RID: 10356 RVA: 0x0004E1A3 File Offset: 0x0004C3A3
		[RequiredByNativeCode]
		public UnityEvent()
		{
		}

		// Token: 0x06002875 RID: 10357 RVA: 0x0004E1B4 File Offset: 0x0004C3B4
		public void AddListener(UnityAction<T0, T1, T2, T3> call)
		{
			base.AddCall(UnityEvent<T0, T1, T2, T3>.GetDelegate(call));
		}

		// Token: 0x06002876 RID: 10358 RVA: 0x0004DC7E File Offset: 0x0004BE7E
		public void RemoveListener(UnityAction<T0, T1, T2, T3> call)
		{
			base.RemoveListener(call.Target, call.Method);
		}

		// Token: 0x06002877 RID: 10359 RVA: 0x0004E1C4 File Offset: 0x0004C3C4
		protected override MethodInfo FindMethod_Impl(string name, Type targetObjType)
		{
			return UnityEventBase.GetValidMethodInfo(targetObjType, name, new Type[]
			{
				typeof(T0),
				typeof(T1),
				typeof(T2),
				typeof(T3)
			});
		}

		// Token: 0x06002878 RID: 10360 RVA: 0x0004E218 File Offset: 0x0004C418
		internal override BaseInvokableCall GetDelegate(object target, MethodInfo theFunction)
		{
			return new InvokableCall<T0, T1, T2, T3>(target, theFunction);
		}

		// Token: 0x06002879 RID: 10361 RVA: 0x0004E234 File Offset: 0x0004C434
		private static BaseInvokableCall GetDelegate(UnityAction<T0, T1, T2, T3> action)
		{
			return new InvokableCall<T0, T1, T2, T3>(action);
		}

		// Token: 0x0600287A RID: 10362 RVA: 0x0004E24C File Offset: 0x0004C44C
		public void Invoke(T0 arg0, T1 arg1, T2 arg2, T3 arg3)
		{
			List<BaseInvokableCall> list = base.PrepareInvoke();
			for (int i = 0; i < list.Count; i++)
			{
				InvokableCall<T0, T1, T2, T3> invokableCall = list[i] as InvokableCall<T0, T1, T2, T3>;
				bool flag = invokableCall != null;
				if (flag)
				{
					invokableCall.Invoke(arg0, arg1, arg2, arg3);
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
							this.m_InvokeArray = new object[4];
						}
						this.m_InvokeArray[0] = arg0;
						this.m_InvokeArray[1] = arg1;
						this.m_InvokeArray[2] = arg2;
						this.m_InvokeArray[3] = arg3;
						baseInvokableCall.Invoke(this.m_InvokeArray);
					}
				}
			}
		}

		// Token: 0x04000B53 RID: 2899
		private object[] m_InvokeArray = null;
	}
}
