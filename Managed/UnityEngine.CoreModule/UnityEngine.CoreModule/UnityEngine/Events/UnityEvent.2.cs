using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine.Scripting;

namespace UnityEngine.Events
{
	// Token: 0x02000350 RID: 848
	[Serializable]
	public class UnityEvent<T0> : UnityEventBase
	{
		// Token: 0x06002853 RID: 10323 RVA: 0x0004DD90 File Offset: 0x0004BF90
		[RequiredByNativeCode]
		public UnityEvent()
		{
		}

		// Token: 0x06002854 RID: 10324 RVA: 0x0004DDA1 File Offset: 0x0004BFA1
		public void AddListener(UnityAction<T0> call)
		{
			base.AddCall(UnityEvent<T0>.GetDelegate(call));
		}

		// Token: 0x06002855 RID: 10325 RVA: 0x0004DC7E File Offset: 0x0004BE7E
		public void RemoveListener(UnityAction<T0> call)
		{
			base.RemoveListener(call.Target, call.Method);
		}

		// Token: 0x06002856 RID: 10326 RVA: 0x0004DDB4 File Offset: 0x0004BFB4
		protected override MethodInfo FindMethod_Impl(string name, Type targetObjType)
		{
			return UnityEventBase.GetValidMethodInfo(targetObjType, name, new Type[] { typeof(T0) });
		}

		// Token: 0x06002857 RID: 10327 RVA: 0x0004DDE0 File Offset: 0x0004BFE0
		internal override BaseInvokableCall GetDelegate(object target, MethodInfo theFunction)
		{
			return new InvokableCall<T0>(target, theFunction);
		}

		// Token: 0x06002858 RID: 10328 RVA: 0x0004DDFC File Offset: 0x0004BFFC
		private static BaseInvokableCall GetDelegate(UnityAction<T0> action)
		{
			return new InvokableCall<T0>(action);
		}

		// Token: 0x06002859 RID: 10329 RVA: 0x0004DE14 File Offset: 0x0004C014
		public void Invoke(T0 arg0)
		{
			List<BaseInvokableCall> list = base.PrepareInvoke();
			for (int i = 0; i < list.Count; i++)
			{
				InvokableCall<T0> invokableCall = list[i] as InvokableCall<T0>;
				bool flag = invokableCall != null;
				if (flag)
				{
					invokableCall.Invoke(arg0);
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
							this.m_InvokeArray = new object[1];
						}
						this.m_InvokeArray[0] = arg0;
						baseInvokableCall.Invoke(this.m_InvokeArray);
					}
				}
			}
		}

		// Token: 0x04000B50 RID: 2896
		private object[] m_InvokeArray = null;
	}
}
