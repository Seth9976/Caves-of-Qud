using System;
using System.Collections.Generic;
using UnityEngine.Serialization;

namespace UnityEngine.Events
{
	// Token: 0x0200034A RID: 842
	[Serializable]
	internal class PersistentCallGroup
	{
		// Token: 0x06002811 RID: 10257 RVA: 0x0004D1ED File Offset: 0x0004B3ED
		public PersistentCallGroup()
		{
			this.m_Calls = new List<PersistentCall>();
		}

		// Token: 0x17000664 RID: 1636
		// (get) Token: 0x06002812 RID: 10258 RVA: 0x0004D204 File Offset: 0x0004B404
		public int Count
		{
			get
			{
				return this.m_Calls.Count;
			}
		}

		// Token: 0x06002813 RID: 10259 RVA: 0x0004D224 File Offset: 0x0004B424
		public PersistentCall GetListener(int index)
		{
			return this.m_Calls[index];
		}

		// Token: 0x06002814 RID: 10260 RVA: 0x0004D244 File Offset: 0x0004B444
		public IEnumerable<PersistentCall> GetListeners()
		{
			return this.m_Calls;
		}

		// Token: 0x06002815 RID: 10261 RVA: 0x0004D25C File Offset: 0x0004B45C
		public void AddListener()
		{
			this.m_Calls.Add(new PersistentCall());
		}

		// Token: 0x06002816 RID: 10262 RVA: 0x0004D270 File Offset: 0x0004B470
		public void AddListener(PersistentCall call)
		{
			this.m_Calls.Add(call);
		}

		// Token: 0x06002817 RID: 10263 RVA: 0x0004D280 File Offset: 0x0004B480
		public void RemoveListener(int index)
		{
			this.m_Calls.RemoveAt(index);
		}

		// Token: 0x06002818 RID: 10264 RVA: 0x0004D290 File Offset: 0x0004B490
		public void Clear()
		{
			this.m_Calls.Clear();
		}

		// Token: 0x06002819 RID: 10265 RVA: 0x0004D2A0 File Offset: 0x0004B4A0
		public void RegisterEventPersistentListener(int index, Object targetObj, Type targetObjType, string methodName)
		{
			PersistentCall listener = this.GetListener(index);
			listener.RegisterPersistentListener(targetObj, targetObjType, methodName);
			listener.mode = PersistentListenerMode.EventDefined;
		}

		// Token: 0x0600281A RID: 10266 RVA: 0x0004D2CC File Offset: 0x0004B4CC
		public void RegisterVoidPersistentListener(int index, Object targetObj, Type targetObjType, string methodName)
		{
			PersistentCall listener = this.GetListener(index);
			listener.RegisterPersistentListener(targetObj, targetObjType, methodName);
			listener.mode = PersistentListenerMode.Void;
		}

		// Token: 0x0600281B RID: 10267 RVA: 0x0004D2F8 File Offset: 0x0004B4F8
		public void RegisterObjectPersistentListener(int index, Object targetObj, Type targetObjType, Object argument, string methodName)
		{
			PersistentCall listener = this.GetListener(index);
			listener.RegisterPersistentListener(targetObj, targetObjType, methodName);
			listener.mode = PersistentListenerMode.Object;
			listener.arguments.unityObjectArgument = argument;
		}

		// Token: 0x0600281C RID: 10268 RVA: 0x0004D330 File Offset: 0x0004B530
		public void RegisterIntPersistentListener(int index, Object targetObj, Type targetObjType, int argument, string methodName)
		{
			PersistentCall listener = this.GetListener(index);
			listener.RegisterPersistentListener(targetObj, targetObjType, methodName);
			listener.mode = PersistentListenerMode.Int;
			listener.arguments.intArgument = argument;
		}

		// Token: 0x0600281D RID: 10269 RVA: 0x0004D368 File Offset: 0x0004B568
		public void RegisterFloatPersistentListener(int index, Object targetObj, Type targetObjType, float argument, string methodName)
		{
			PersistentCall listener = this.GetListener(index);
			listener.RegisterPersistentListener(targetObj, targetObjType, methodName);
			listener.mode = PersistentListenerMode.Float;
			listener.arguments.floatArgument = argument;
		}

		// Token: 0x0600281E RID: 10270 RVA: 0x0004D3A0 File Offset: 0x0004B5A0
		public void RegisterStringPersistentListener(int index, Object targetObj, Type targetObjType, string argument, string methodName)
		{
			PersistentCall listener = this.GetListener(index);
			listener.RegisterPersistentListener(targetObj, targetObjType, methodName);
			listener.mode = PersistentListenerMode.String;
			listener.arguments.stringArgument = argument;
		}

		// Token: 0x0600281F RID: 10271 RVA: 0x0004D3D8 File Offset: 0x0004B5D8
		public void RegisterBoolPersistentListener(int index, Object targetObj, Type targetObjType, bool argument, string methodName)
		{
			PersistentCall listener = this.GetListener(index);
			listener.RegisterPersistentListener(targetObj, targetObjType, methodName);
			listener.mode = PersistentListenerMode.Bool;
			listener.arguments.boolArgument = argument;
		}

		// Token: 0x06002820 RID: 10272 RVA: 0x0004D410 File Offset: 0x0004B610
		public void UnregisterPersistentListener(int index)
		{
			PersistentCall listener = this.GetListener(index);
			listener.UnregisterPersistentListener();
		}

		// Token: 0x06002821 RID: 10273 RVA: 0x0004D430 File Offset: 0x0004B630
		public void RemoveListeners(Object target, string methodName)
		{
			List<PersistentCall> list = new List<PersistentCall>();
			for (int i = 0; i < this.m_Calls.Count; i++)
			{
				bool flag = this.m_Calls[i].target == target && this.m_Calls[i].methodName == methodName;
				if (flag)
				{
					list.Add(this.m_Calls[i]);
				}
			}
			this.m_Calls.RemoveAll(new Predicate<PersistentCall>(list.Contains));
		}

		// Token: 0x06002822 RID: 10274 RVA: 0x0004D4C4 File Offset: 0x0004B6C4
		public void Initialize(InvokableCallList invokableList, UnityEventBase unityEventBase)
		{
			foreach (PersistentCall persistentCall in this.m_Calls)
			{
				bool flag = !persistentCall.IsValid();
				if (!flag)
				{
					BaseInvokableCall runtimeCall = persistentCall.GetRuntimeCall(unityEventBase);
					bool flag2 = runtimeCall != null;
					if (flag2)
					{
						invokableList.AddPersistentInvokableCall(runtimeCall);
					}
				}
			}
		}

		// Token: 0x04000B47 RID: 2887
		[FormerlySerializedAs("m_Listeners")]
		[SerializeField]
		private List<PersistentCall> m_Calls;
	}
}
