using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine.Scripting;

namespace UnityEngine.Events
{
	// Token: 0x0200034E RID: 846
	[Serializable]
	public class UnityEvent : UnityEventBase
	{
		// Token: 0x06002848 RID: 10312 RVA: 0x0004DC5D File Offset: 0x0004BE5D
		[RequiredByNativeCode]
		public UnityEvent()
		{
		}

		// Token: 0x06002849 RID: 10313 RVA: 0x0004DC6E File Offset: 0x0004BE6E
		public void AddListener(UnityAction call)
		{
			base.AddCall(UnityEvent.GetDelegate(call));
		}

		// Token: 0x0600284A RID: 10314 RVA: 0x0004DC7E File Offset: 0x0004BE7E
		public void RemoveListener(UnityAction call)
		{
			base.RemoveListener(call.Target, call.Method);
		}

		// Token: 0x0600284B RID: 10315 RVA: 0x0004DC94 File Offset: 0x0004BE94
		protected override MethodInfo FindMethod_Impl(string name, Type targetObjType)
		{
			return UnityEventBase.GetValidMethodInfo(targetObjType, name, new Type[0]);
		}

		// Token: 0x0600284C RID: 10316 RVA: 0x0004DCB4 File Offset: 0x0004BEB4
		internal override BaseInvokableCall GetDelegate(object target, MethodInfo theFunction)
		{
			return new InvokableCall(target, theFunction);
		}

		// Token: 0x0600284D RID: 10317 RVA: 0x0004DCD0 File Offset: 0x0004BED0
		private static BaseInvokableCall GetDelegate(UnityAction action)
		{
			return new InvokableCall(action);
		}

		// Token: 0x0600284E RID: 10318 RVA: 0x0004DCE8 File Offset: 0x0004BEE8
		public void Invoke()
		{
			List<BaseInvokableCall> list = base.PrepareInvoke();
			for (int i = 0; i < list.Count; i++)
			{
				InvokableCall invokableCall = list[i] as InvokableCall;
				bool flag = invokableCall != null;
				if (flag)
				{
					invokableCall.Invoke();
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
							this.m_InvokeArray = new object[0];
						}
						baseInvokableCall.Invoke(this.m_InvokeArray);
					}
				}
			}
		}

		// Token: 0x04000B4F RID: 2895
		private object[] m_InvokeArray = null;
	}
}
