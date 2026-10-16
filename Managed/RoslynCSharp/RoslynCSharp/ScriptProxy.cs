using System;
using System.Collections;
using System.Reflection;
using UnityEngine;

namespace RoslynCSharp
{
	// Token: 0x0200000E RID: 14
	public class ScriptProxy : IDisposable
	{
		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000096 RID: 150 RVA: 0x00003C84 File Offset: 0x00001E84
		public ScriptType ScriptType
		{
			get
			{
				this.CheckDisposed();
				return this.scriptType;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000097 RID: 151 RVA: 0x00003C92 File Offset: 0x00001E92
		public IScriptMemberProxy Fields
		{
			get
			{
				this.CheckDisposed();
				this.fields.throwOnError = true;
				return this.fields;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000098 RID: 152 RVA: 0x00003CAC File Offset: 0x00001EAC
		public IScriptMemberProxy SafeFields
		{
			get
			{
				this.CheckDisposed();
				this.fields.throwOnError = false;
				return this.fields;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000099 RID: 153 RVA: 0x00003CC6 File Offset: 0x00001EC6
		public IScriptMemberProxy Properties
		{
			get
			{
				this.CheckDisposed();
				this.properies.throwOnError = true;
				return this.properies;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x0600009A RID: 154 RVA: 0x00003CE0 File Offset: 0x00001EE0
		public IScriptMemberProxy SafeProperties
		{
			get
			{
				this.CheckDisposed();
				this.properies.throwOnError = false;
				return this.properies;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x0600009B RID: 155 RVA: 0x00003CFA File Offset: 0x00001EFA
		public object Instance
		{
			get
			{
				this.CheckDisposed();
				return this.instance;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x0600009C RID: 156 RVA: 0x00003D08 File Offset: 0x00001F08
		public global::UnityEngine.Object UnityInstance
		{
			get
			{
				this.CheckDisposed();
				return this.instance as global::UnityEngine.Object;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x0600009D RID: 157 RVA: 0x00003D1B File Offset: 0x00001F1B
		public MonoBehaviour BehaviourInstance
		{
			get
			{
				this.CheckDisposed();
				return this.instance as MonoBehaviour;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x0600009E RID: 158 RVA: 0x00003D2E File Offset: 0x00001F2E
		public ScriptableObject ScriptableInstance
		{
			get
			{
				this.CheckDisposed();
				return this.instance as ScriptableObject;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x0600009F RID: 159 RVA: 0x00003D41 File Offset: 0x00001F41
		public bool IsUnityObject
		{
			get
			{
				this.CheckDisposed();
				return this.scriptType.IsUnityObject;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x00003D54 File Offset: 0x00001F54
		public bool IsMonoBehaviour
		{
			get
			{
				this.CheckDisposed();
				return this.scriptType.IsMonoBehaviour;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x00003D67 File Offset: 0x00001F67
		public bool IsScriptableObject
		{
			get
			{
				this.CheckDisposed();
				return this.scriptType.IsScriptableObject;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x00003D7A File Offset: 0x00001F7A
		public bool IsDisposed
		{
			get
			{
				return this.instance == null;
			}
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00003D85 File Offset: 0x00001F85
		internal ScriptProxy(ScriptType scriptType, object instance)
		{
			this.scriptType = scriptType;
			this.instance = instance;
			this.fields = new ScriptFieldProxy(false, scriptType, this);
			this.properies = new ScriptPropertyProxy(false, scriptType, this);
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00003DB7 File Offset: 0x00001FB7
		public object Call(string methodName)
		{
			return this.Call(methodName, ProxyCallConvention.Any);
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00003DC4 File Offset: 0x00001FC4
		public object Call(string methodName, ProxyCallConvention callConvention)
		{
			this.CheckDisposed();
			MethodInfo methodInfo = this.scriptType.FindCachedMethod(methodName, false);
			if (methodInfo == null)
			{
				throw new TargetException(string.Format("Type '{0}' does not define a method called '{1}'", this.ScriptType, methodName));
			}
			object obj = methodInfo.Invoke(this.instance, null);
			if (obj is IEnumerator && (callConvention == ProxyCallConvention.Any || callConvention == ProxyCallConvention.UnityCoroutine))
			{
				IEnumerator enumerator = obj as IEnumerator;
				if (this.IsMonoBehaviour)
				{
					this.GetInstanceAs<MonoBehaviour>(false, null).StartCoroutine(enumerator);
				}
			}
			return obj;
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00003E40 File Offset: 0x00002040
		public object Call(string methodName, params object[] arguments)
		{
			return this.Call(methodName, ProxyCallConvention.Any, arguments);
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00003E4C File Offset: 0x0000204C
		public object Call(string methodName, ProxyCallConvention callConvention, params object[] arguments)
		{
			this.CheckDisposed();
			MethodInfo methodInfo = this.scriptType.FindCachedMethod(methodName, false);
			if (methodInfo == null)
			{
				throw new TargetException(string.Format("Type '{0}' does not define a method called '{1}'", this.ScriptType, methodName));
			}
			object obj = methodInfo.Invoke(this.instance, arguments);
			if (obj is IEnumerator && (callConvention == ProxyCallConvention.Any || callConvention == ProxyCallConvention.UnityCoroutine))
			{
				IEnumerator enumerator = obj as IEnumerator;
				if (this.IsMonoBehaviour)
				{
					this.GetInstanceAs<MonoBehaviour>(false, null).StartCoroutine(enumerator);
				}
			}
			return obj;
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00003EC8 File Offset: 0x000020C8
		public object SafeCall(string method)
		{
			return this.SafeCall(method, ProxyCallConvention.Any);
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00003ED4 File Offset: 0x000020D4
		public object SafeCall(string method, ProxyCallConvention callConvention)
		{
			object obj;
			try
			{
				this.CheckDisposed();
				obj = this.Call(method, callConvention);
			}
			catch
			{
				obj = null;
			}
			return obj;
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00003F08 File Offset: 0x00002108
		public object SafeCall(string method, params object[] arguments)
		{
			return this.SafeCall(method, ProxyCallConvention.Any, arguments);
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00003F14 File Offset: 0x00002114
		public object SafeCall(string method, ProxyCallConvention callConvention, params object[] arguments)
		{
			object obj;
			try
			{
				this.CheckDisposed();
				obj = this.Call(method, callConvention);
			}
			catch
			{
				obj = null;
			}
			return obj;
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00003F48 File Offset: 0x00002148
		public Type GetInstanceType()
		{
			this.CheckDisposed();
			return this.instance.GetType();
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00003F5C File Offset: 0x0000215C
		public T GetInstanceAs<T>(bool throwOnError, T errorValue = default(T))
		{
			if (throwOnError)
			{
				return (T)((object)this.instance);
			}
			T t;
			try
			{
				t = (T)((object)this.instance);
			}
			catch
			{
				t = errorValue;
			}
			return t;
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00003FA0 File Offset: 0x000021A0
		public virtual void Dispose()
		{
			this.CheckDisposed();
			if (this.IsUnityObject)
			{
				global::UnityEngine.Object.Destroy(this.UnityInstance);
			}
			if (this.instance is IDisposable)
			{
				(this.instance as IDisposable).Dispose();
			}
			this.scriptType = null;
			this.instance = null;
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00003FF1 File Offset: 0x000021F1
		public void MakePersistent()
		{
			if (this.IsUnityObject)
			{
				global::UnityEngine.Object.DontDestroyOnLoad(this.UnityInstance);
			}
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00004006 File Offset: 0x00002206
		private void CheckDisposed()
		{
			if (this.instance == null)
			{
				throw new ObjectDisposedException("The script has already been disposed. Unity types can be disposed automatically when the wrapped type is destroyed");
			}
		}

		// Token: 0x0400004A RID: 74
		private ScriptType scriptType;

		// Token: 0x0400004B RID: 75
		private ScriptFieldProxy fields;

		// Token: 0x0400004C RID: 76
		private ScriptPropertyProxy properies;

		// Token: 0x0400004D RID: 77
		private object instance;
	}
}
