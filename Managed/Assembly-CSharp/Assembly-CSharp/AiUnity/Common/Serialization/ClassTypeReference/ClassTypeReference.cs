using System;
using AiUnity.Common.InternalLog;
using AiUnity.Common.Patterns;
using UnityEngine;

namespace AiUnity.Common.Serialization.ClassTypeReference
{
	// Token: 0x020002C1 RID: 705
	[Serializable]
	public sealed class ClassTypeReference : ISerializationCallbackReceiver, IValidateTypeReference
	{
		// Token: 0x17000359 RID: 857
		// (get) Token: 0x06001883 RID: 6275 RVA: 0x0007F584 File Offset: 0x0007D784
		// (set) Token: 0x06001884 RID: 6276 RVA: 0x0007F58C File Offset: 0x0007D78C
		public Type Type
		{
			get
			{
				return this._type;
			}
			set
			{
				if (value != null && !value.IsClass)
				{
					throw new ArgumentException(string.Format("'{0}' is not a class type.", value.FullName), "value");
				}
				this._type = value;
				this._classRef = ClassTypeReference.GetClassRef(value);
			}
		}

		// Token: 0x1700035A RID: 858
		// (get) Token: 0x06001885 RID: 6277 RVA: 0x0007F5D8 File Offset: 0x0007D7D8
		private static IInternalLogger Logger
		{
			get
			{
				return Singleton<CommonInternalLogger>.Instance;
			}
		}

		// Token: 0x06001886 RID: 6278 RVA: 0x0007F5DF File Offset: 0x0007D7DF
		public ClassTypeReference()
		{
		}

		// Token: 0x06001887 RID: 6279 RVA: 0x0007F5E7 File Offset: 0x0007D7E7
		public ClassTypeReference(string assemblyQualifiedClassName)
		{
			this.Type = ((!string.IsNullOrEmpty(assemblyQualifiedClassName)) ? Type.GetType(assemblyQualifiedClassName) : null);
		}

		// Token: 0x06001888 RID: 6280 RVA: 0x0007F606 File Offset: 0x0007D806
		public ClassTypeReference(Type type)
		{
			this.Type = type;
		}

		// Token: 0x06001889 RID: 6281 RVA: 0x0007F615 File Offset: 0x0007D815
		public static string GetClassRef(Type type)
		{
			if (!(type != null))
			{
				return string.Empty;
			}
			return type.FullName + ", " + type.Assembly.GetName().Name;
		}

		// Token: 0x0600188A RID: 6282 RVA: 0x0007F646 File Offset: 0x0007D846
		public bool IsValidType()
		{
			return this.Type != null;
		}

		// Token: 0x0600188B RID: 6283 RVA: 0x0007F654 File Offset: 0x0007D854
		public override string ToString()
		{
			if (!(this.Type != null))
			{
				return "(None)";
			}
			return this.Type.FullName;
		}

		// Token: 0x0600188C RID: 6284 RVA: 0x0007F678 File Offset: 0x0007D878
		void ISerializationCallbackReceiver.OnAfterDeserialize()
		{
			if (!string.IsNullOrEmpty(this._classRef))
			{
				this._type = Type.GetType(this._classRef);
				if (this._type == null)
				{
					Debug.LogWarning(string.Format("'{0}' was referenced but class type was not found.", this._classRef));
					return;
				}
			}
			else
			{
				this._type = null;
			}
		}

		// Token: 0x0600188D RID: 6285 RVA: 0x0007F6CE File Offset: 0x0007D8CE
		void ISerializationCallbackReceiver.OnBeforeSerialize()
		{
		}

		// Token: 0x0600188E RID: 6286 RVA: 0x0007F6D0 File Offset: 0x0007D8D0
		public static implicit operator ClassTypeReference(Type type)
		{
			return new ClassTypeReference(type);
		}

		// Token: 0x0600188F RID: 6287 RVA: 0x0007F6D8 File Offset: 0x0007D8D8
		public static implicit operator string(ClassTypeReference typeReference)
		{
			return typeReference._classRef;
		}

		// Token: 0x06001890 RID: 6288 RVA: 0x0007F6E0 File Offset: 0x0007D8E0
		public static implicit operator Type(ClassTypeReference typeReference)
		{
			return typeReference.Type;
		}

		// Token: 0x04000E8C RID: 3724
		[SerializeField]
		[HideInInspector]
		private string _classRef;

		// Token: 0x04000E8D RID: 3725
		private Type _type;
	}
}
