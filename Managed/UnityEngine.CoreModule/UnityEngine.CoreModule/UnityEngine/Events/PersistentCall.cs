using System;
using System.Reflection;
using UnityEngine.Serialization;

namespace UnityEngine.Events
{
	// Token: 0x02000349 RID: 841
	[Serializable]
	internal class PersistentCall : ISerializationCallbackReceiver
	{
		// Token: 0x1700065E RID: 1630
		// (get) Token: 0x06002801 RID: 10241 RVA: 0x0004CE50 File Offset: 0x0004B050
		public Object target
		{
			get
			{
				return this.m_Target;
			}
		}

		// Token: 0x1700065F RID: 1631
		// (get) Token: 0x06002802 RID: 10242 RVA: 0x0004CE68 File Offset: 0x0004B068
		public string targetAssemblyTypeName
		{
			get
			{
				bool flag = string.IsNullOrEmpty(this.m_TargetAssemblyTypeName) && this.m_Target != null;
				if (flag)
				{
					this.m_TargetAssemblyTypeName = UnityEventTools.TidyAssemblyTypeName(this.m_Target.GetType().AssemblyQualifiedName);
				}
				return this.m_TargetAssemblyTypeName;
			}
		}

		// Token: 0x17000660 RID: 1632
		// (get) Token: 0x06002803 RID: 10243 RVA: 0x0004CEC0 File Offset: 0x0004B0C0
		public string methodName
		{
			get
			{
				return this.m_MethodName;
			}
		}

		// Token: 0x17000661 RID: 1633
		// (get) Token: 0x06002804 RID: 10244 RVA: 0x0004CED8 File Offset: 0x0004B0D8
		// (set) Token: 0x06002805 RID: 10245 RVA: 0x0004CEF0 File Offset: 0x0004B0F0
		public PersistentListenerMode mode
		{
			get
			{
				return this.m_Mode;
			}
			set
			{
				this.m_Mode = value;
			}
		}

		// Token: 0x17000662 RID: 1634
		// (get) Token: 0x06002806 RID: 10246 RVA: 0x0004CEFC File Offset: 0x0004B0FC
		public ArgumentCache arguments
		{
			get
			{
				return this.m_Arguments;
			}
		}

		// Token: 0x17000663 RID: 1635
		// (get) Token: 0x06002807 RID: 10247 RVA: 0x0004CF14 File Offset: 0x0004B114
		// (set) Token: 0x06002808 RID: 10248 RVA: 0x0004CF2C File Offset: 0x0004B12C
		public UnityEventCallState callState
		{
			get
			{
				return this.m_CallState;
			}
			set
			{
				this.m_CallState = value;
			}
		}

		// Token: 0x06002809 RID: 10249 RVA: 0x0004CF38 File Offset: 0x0004B138
		public bool IsValid()
		{
			return !string.IsNullOrEmpty(this.targetAssemblyTypeName) && !string.IsNullOrEmpty(this.methodName);
		}

		// Token: 0x0600280A RID: 10250 RVA: 0x0004CF68 File Offset: 0x0004B168
		public BaseInvokableCall GetRuntimeCall(UnityEventBase theEvent)
		{
			bool flag = this.m_CallState == UnityEventCallState.Off || theEvent == null;
			BaseInvokableCall baseInvokableCall;
			if (flag)
			{
				baseInvokableCall = null;
			}
			else
			{
				MethodInfo methodInfo = theEvent.FindMethod(this);
				bool flag2 = methodInfo == null;
				if (flag2)
				{
					baseInvokableCall = null;
				}
				else
				{
					bool flag3 = !methodInfo.IsStatic && this.target == null;
					if (flag3)
					{
						baseInvokableCall = null;
					}
					else
					{
						Object @object = (methodInfo.IsStatic ? null : this.target);
						switch (this.m_Mode)
						{
						case PersistentListenerMode.EventDefined:
							baseInvokableCall = theEvent.GetDelegate(@object, methodInfo);
							break;
						case PersistentListenerMode.Void:
							baseInvokableCall = new InvokableCall(@object, methodInfo);
							break;
						case PersistentListenerMode.Object:
							baseInvokableCall = PersistentCall.GetObjectCall(@object, methodInfo, this.m_Arguments);
							break;
						case PersistentListenerMode.Int:
							baseInvokableCall = new CachedInvokableCall<int>(@object, methodInfo, this.m_Arguments.intArgument);
							break;
						case PersistentListenerMode.Float:
							baseInvokableCall = new CachedInvokableCall<float>(@object, methodInfo, this.m_Arguments.floatArgument);
							break;
						case PersistentListenerMode.String:
							baseInvokableCall = new CachedInvokableCall<string>(@object, methodInfo, this.m_Arguments.stringArgument);
							break;
						case PersistentListenerMode.Bool:
							baseInvokableCall = new CachedInvokableCall<bool>(@object, methodInfo, this.m_Arguments.boolArgument);
							break;
						default:
							baseInvokableCall = null;
							break;
						}
					}
				}
			}
			return baseInvokableCall;
		}

		// Token: 0x0600280B RID: 10251 RVA: 0x0004D094 File Offset: 0x0004B294
		private static BaseInvokableCall GetObjectCall(Object target, MethodInfo method, ArgumentCache arguments)
		{
			Type type = typeof(Object);
			bool flag = !string.IsNullOrEmpty(arguments.unityObjectArgumentAssemblyTypeName);
			if (flag)
			{
				type = Type.GetType(arguments.unityObjectArgumentAssemblyTypeName, false) ?? typeof(Object);
			}
			Type typeFromHandle = typeof(CachedInvokableCall<>);
			Type type2 = typeFromHandle.MakeGenericType(new Type[] { type });
			ConstructorInfo constructor = type2.GetConstructor(new Type[]
			{
				typeof(Object),
				typeof(MethodInfo),
				type
			});
			Object @object = arguments.unityObjectArgument;
			bool flag2 = @object != null && !type.IsAssignableFrom(@object.GetType());
			if (flag2)
			{
				@object = null;
			}
			return constructor.Invoke(new object[] { target, method, @object }) as BaseInvokableCall;
		}

		// Token: 0x0600280C RID: 10252 RVA: 0x0004D175 File Offset: 0x0004B375
		public void RegisterPersistentListener(Object ttarget, Type targetType, string mmethodName)
		{
			this.m_Target = ttarget;
			this.m_TargetAssemblyTypeName = UnityEventTools.TidyAssemblyTypeName(targetType.AssemblyQualifiedName);
			this.m_MethodName = mmethodName;
		}

		// Token: 0x0600280D RID: 10253 RVA: 0x0004D197 File Offset: 0x0004B397
		public void UnregisterPersistentListener()
		{
			this.m_MethodName = string.Empty;
			this.m_Target = null;
			this.m_TargetAssemblyTypeName = string.Empty;
		}

		// Token: 0x0600280E RID: 10254 RVA: 0x0004D1B7 File Offset: 0x0004B3B7
		public void OnBeforeSerialize()
		{
			this.m_TargetAssemblyTypeName = UnityEventTools.TidyAssemblyTypeName(this.m_TargetAssemblyTypeName);
		}

		// Token: 0x0600280F RID: 10255 RVA: 0x0004D1B7 File Offset: 0x0004B3B7
		public void OnAfterDeserialize()
		{
			this.m_TargetAssemblyTypeName = UnityEventTools.TidyAssemblyTypeName(this.m_TargetAssemblyTypeName);
		}

		// Token: 0x04000B41 RID: 2881
		[FormerlySerializedAs("instance")]
		[SerializeField]
		private Object m_Target;

		// Token: 0x04000B42 RID: 2882
		[SerializeField]
		private string m_TargetAssemblyTypeName;

		// Token: 0x04000B43 RID: 2883
		[SerializeField]
		[FormerlySerializedAs("methodName")]
		private string m_MethodName;

		// Token: 0x04000B44 RID: 2884
		[FormerlySerializedAs("mode")]
		[SerializeField]
		private PersistentListenerMode m_Mode = PersistentListenerMode.EventDefined;

		// Token: 0x04000B45 RID: 2885
		[SerializeField]
		[FormerlySerializedAs("arguments")]
		private ArgumentCache m_Arguments = new ArgumentCache();

		// Token: 0x04000B46 RID: 2886
		[SerializeField]
		[FormerlySerializedAs("m_Enabled")]
		[FormerlySerializedAs("enabled")]
		private UnityEventCallState m_CallState = UnityEventCallState.RuntimeOnly;
	}
}
