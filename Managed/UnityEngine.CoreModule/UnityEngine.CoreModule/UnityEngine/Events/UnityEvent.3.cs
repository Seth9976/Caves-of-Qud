using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine.Scripting;

namespace UnityEngine.Events
{
	// Token: 0x02000352 RID: 850
	[Serializable]
	public class UnityEvent<T0, T1> : UnityEventBase
	{
		// Token: 0x0600285E RID: 10334 RVA: 0x0004DECE File Offset: 0x0004C0CE
		[RequiredByNativeCode]
		public UnityEvent()
		{
		}

		// Token: 0x0600285F RID: 10335 RVA: 0x0004DEDF File Offset: 0x0004C0DF
		public void AddListener(UnityAction<T0, T1> call)
		{
			base.AddCall(UnityEvent<T0, T1>.GetDelegate(call));
		}

		// Token: 0x06002860 RID: 10336 RVA: 0x0004DC7E File Offset: 0x0004BE7E
		public void RemoveListener(UnityAction<T0, T1> call)
		{
			base.RemoveListener(call.Target, call.Method);
		}

		// Token: 0x06002861 RID: 10337 RVA: 0x0004DEF0 File Offset: 0x0004C0F0
		protected override MethodInfo FindMethod_Impl(string name, Type targetObjType)
		{
			return UnityEventBase.GetValidMethodInfo(targetObjType, name, new Type[]
			{
				typeof(T0),
				typeof(T1)
			});
		}

		// Token: 0x06002862 RID: 10338 RVA: 0x0004DF2C File Offset: 0x0004C12C
		internal override BaseInvokableCall GetDelegate(object target, MethodInfo theFunction)
		{
			return new InvokableCall<T0, T1>(target, theFunction);
		}

		// Token: 0x06002863 RID: 10339 RVA: 0x0004DF48 File Offset: 0x0004C148
		private static BaseInvokableCall GetDelegate(UnityAction<T0, T1> action)
		{
			return new InvokableCall<T0, T1>(action);
		}

		// Token: 0x06002864 RID: 10340 RVA: 0x0004DF60 File Offset: 0x0004C160
		public void Invoke(T0 arg0, T1 arg1)
		{
			List<BaseInvokableCall> list = base.PrepareInvoke();
			for (int i = 0; i < list.Count; i++)
			{
				InvokableCall<T0, T1> invokableCall = list[i] as InvokableCall<T0, T1>;
				bool flag = invokableCall != null;
				if (flag)
				{
					invokableCall.Invoke(arg0, arg1);
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
							this.m_InvokeArray = new object[2];
						}
						this.m_InvokeArray[0] = arg0;
						this.m_InvokeArray[1] = arg1;
						baseInvokableCall.Invoke(this.m_InvokeArray);
					}
				}
			}
		}

		// Token: 0x04000B51 RID: 2897
		private object[] m_InvokeArray = null;
	}
}
