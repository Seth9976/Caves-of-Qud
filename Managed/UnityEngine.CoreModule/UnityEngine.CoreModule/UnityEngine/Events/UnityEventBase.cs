using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine.Scripting;
using UnityEngine.Serialization;

namespace UnityEngine.Events
{
	// Token: 0x0200034C RID: 844
	[UsedByNativeCode]
	[Serializable]
	public abstract class UnityEventBase : ISerializationCallbackReceiver
	{
		// Token: 0x0600282B RID: 10283 RVA: 0x0004D748 File Offset: 0x0004B948
		protected UnityEventBase()
		{
			this.m_Calls = new InvokableCallList();
			this.m_PersistentCalls = new PersistentCallGroup();
		}

		// Token: 0x0600282C RID: 10284 RVA: 0x0004D76F File Offset: 0x0004B96F
		void ISerializationCallbackReceiver.OnBeforeSerialize()
		{
			this.DirtyPersistentCalls();
		}

		// Token: 0x0600282D RID: 10285 RVA: 0x0004D76F File Offset: 0x0004B96F
		void ISerializationCallbackReceiver.OnAfterDeserialize()
		{
			this.DirtyPersistentCalls();
		}

		// Token: 0x0600282E RID: 10286 RVA: 0x0004D77C File Offset: 0x0004B97C
		protected MethodInfo FindMethod_Impl(string name, object targetObj)
		{
			return this.FindMethod_Impl(name, targetObj.GetType());
		}

		// Token: 0x0600282F RID: 10287
		protected abstract MethodInfo FindMethod_Impl(string name, Type targetObjType);

		// Token: 0x06002830 RID: 10288
		internal abstract BaseInvokableCall GetDelegate(object target, MethodInfo theFunction);

		// Token: 0x06002831 RID: 10289 RVA: 0x0004D79C File Offset: 0x0004B99C
		internal MethodInfo FindMethod(PersistentCall call)
		{
			Type type = typeof(Object);
			bool flag = !string.IsNullOrEmpty(call.arguments.unityObjectArgumentAssemblyTypeName);
			if (flag)
			{
				type = Type.GetType(call.arguments.unityObjectArgumentAssemblyTypeName, false) ?? typeof(Object);
			}
			Type type2 = ((call.target != null) ? call.target.GetType() : Type.GetType(call.targetAssemblyTypeName, false));
			return this.FindMethod(call.methodName, type2, call.mode, type);
		}

		// Token: 0x06002832 RID: 10290 RVA: 0x0004D82C File Offset: 0x0004BA2C
		internal MethodInfo FindMethod(string name, Type listenerType, PersistentListenerMode mode, Type argumentType)
		{
			MethodInfo methodInfo;
			switch (mode)
			{
			case PersistentListenerMode.EventDefined:
				methodInfo = this.FindMethod_Impl(name, listenerType);
				break;
			case PersistentListenerMode.Void:
				methodInfo = UnityEventBase.GetValidMethodInfo(listenerType, name, new Type[0]);
				break;
			case PersistentListenerMode.Object:
				methodInfo = UnityEventBase.GetValidMethodInfo(listenerType, name, new Type[] { argumentType ?? typeof(Object) });
				break;
			case PersistentListenerMode.Int:
				methodInfo = UnityEventBase.GetValidMethodInfo(listenerType, name, new Type[] { typeof(int) });
				break;
			case PersistentListenerMode.Float:
				methodInfo = UnityEventBase.GetValidMethodInfo(listenerType, name, new Type[] { typeof(float) });
				break;
			case PersistentListenerMode.String:
				methodInfo = UnityEventBase.GetValidMethodInfo(listenerType, name, new Type[] { typeof(string) });
				break;
			case PersistentListenerMode.Bool:
				methodInfo = UnityEventBase.GetValidMethodInfo(listenerType, name, new Type[] { typeof(bool) });
				break;
			default:
				methodInfo = null;
				break;
			}
			return methodInfo;
		}

		// Token: 0x06002833 RID: 10291 RVA: 0x0004D924 File Offset: 0x0004BB24
		internal int GetCallsCount()
		{
			return this.m_Calls.Count;
		}

		// Token: 0x06002834 RID: 10292 RVA: 0x0004D944 File Offset: 0x0004BB44
		public int GetPersistentEventCount()
		{
			return this.m_PersistentCalls.Count;
		}

		// Token: 0x06002835 RID: 10293 RVA: 0x0004D964 File Offset: 0x0004BB64
		public Object GetPersistentTarget(int index)
		{
			PersistentCall listener = this.m_PersistentCalls.GetListener(index);
			return (listener != null) ? listener.target : null;
		}

		// Token: 0x06002836 RID: 10294 RVA: 0x0004D990 File Offset: 0x0004BB90
		public string GetPersistentMethodName(int index)
		{
			PersistentCall listener = this.m_PersistentCalls.GetListener(index);
			return (listener != null) ? listener.methodName : string.Empty;
		}

		// Token: 0x06002837 RID: 10295 RVA: 0x0004D9BF File Offset: 0x0004BBBF
		private void DirtyPersistentCalls()
		{
			this.m_Calls.ClearPersistent();
			this.m_CallsDirty = true;
		}

		// Token: 0x06002838 RID: 10296 RVA: 0x0004D9D8 File Offset: 0x0004BBD8
		private void RebuildPersistentCallsIfNeeded()
		{
			bool callsDirty = this.m_CallsDirty;
			if (callsDirty)
			{
				this.m_PersistentCalls.Initialize(this.m_Calls, this);
				this.m_CallsDirty = false;
			}
		}

		// Token: 0x06002839 RID: 10297 RVA: 0x0004DA0C File Offset: 0x0004BC0C
		public void SetPersistentListenerState(int index, UnityEventCallState state)
		{
			PersistentCall listener = this.m_PersistentCalls.GetListener(index);
			bool flag = listener != null;
			if (flag)
			{
				listener.callState = state;
			}
			this.DirtyPersistentCalls();
		}

		// Token: 0x0600283A RID: 10298 RVA: 0x0004DA40 File Offset: 0x0004BC40
		public UnityEventCallState GetPersistentListenerState(int index)
		{
			bool flag = index < 0 || index > this.m_PersistentCalls.Count;
			if (flag)
			{
				throw new IndexOutOfRangeException(string.Format("Index {0} is out of range of the {1} persistent listeners.", index, this.GetPersistentEventCount()));
			}
			return this.m_PersistentCalls.GetListener(index).callState;
		}

		// Token: 0x0600283B RID: 10299 RVA: 0x0004DA9D File Offset: 0x0004BC9D
		protected void AddListener(object targetObj, MethodInfo method)
		{
			this.m_Calls.AddListener(this.GetDelegate(targetObj, method));
		}

		// Token: 0x0600283C RID: 10300 RVA: 0x0004DAB4 File Offset: 0x0004BCB4
		internal void AddCall(BaseInvokableCall call)
		{
			this.m_Calls.AddListener(call);
		}

		// Token: 0x0600283D RID: 10301 RVA: 0x0004DAC4 File Offset: 0x0004BCC4
		protected void RemoveListener(object targetObj, MethodInfo method)
		{
			this.m_Calls.RemoveListener(targetObj, method);
		}

		// Token: 0x0600283E RID: 10302 RVA: 0x0004DAD5 File Offset: 0x0004BCD5
		public void RemoveAllListeners()
		{
			this.m_Calls.Clear();
		}

		// Token: 0x0600283F RID: 10303 RVA: 0x0004DAE4 File Offset: 0x0004BCE4
		internal List<BaseInvokableCall> PrepareInvoke()
		{
			this.RebuildPersistentCallsIfNeeded();
			return this.m_Calls.PrepareInvoke();
		}

		// Token: 0x06002840 RID: 10304 RVA: 0x0004DB08 File Offset: 0x0004BD08
		protected void Invoke(object[] parameters)
		{
			List<BaseInvokableCall> list = this.PrepareInvoke();
			for (int i = 0; i < list.Count; i++)
			{
				list[i].Invoke(parameters);
			}
		}

		// Token: 0x06002841 RID: 10305 RVA: 0x0004DB40 File Offset: 0x0004BD40
		public override string ToString()
		{
			return base.ToString() + " " + base.GetType().FullName;
		}

		// Token: 0x06002842 RID: 10306 RVA: 0x0004DB70 File Offset: 0x0004BD70
		public static MethodInfo GetValidMethodInfo(object obj, string functionName, Type[] argumentTypes)
		{
			return UnityEventBase.GetValidMethodInfo(obj.GetType(), functionName, argumentTypes);
		}

		// Token: 0x06002843 RID: 10307 RVA: 0x0004DB90 File Offset: 0x0004BD90
		public static MethodInfo GetValidMethodInfo(Type objectType, string functionName, Type[] argumentTypes)
		{
			while (objectType != typeof(object) && objectType != null)
			{
				MethodInfo method = objectType.GetMethod(functionName, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, null, argumentTypes, null);
				bool flag = method != null;
				if (flag)
				{
					ParameterInfo[] parameters = method.GetParameters();
					bool flag2 = true;
					int num = 0;
					foreach (ParameterInfo parameterInfo in parameters)
					{
						Type type = argumentTypes[num];
						Type parameterType = parameterInfo.ParameterType;
						flag2 = type.IsPrimitive == parameterType.IsPrimitive;
						bool flag3 = !flag2;
						if (flag3)
						{
							break;
						}
						num++;
					}
					bool flag4 = flag2;
					if (flag4)
					{
						return method;
					}
				}
				objectType = objectType.BaseType;
			}
			return null;
		}

		// Token: 0x04000B4C RID: 2892
		private InvokableCallList m_Calls;

		// Token: 0x04000B4D RID: 2893
		[SerializeField]
		[FormerlySerializedAs("m_PersistentListeners")]
		private PersistentCallGroup m_PersistentCalls;

		// Token: 0x04000B4E RID: 2894
		private bool m_CallsDirty = true;
	}
}
