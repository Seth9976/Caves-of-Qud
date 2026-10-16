using System;

namespace System.Reflection
{
	// Token: 0x020008C6 RID: 2246
	internal abstract class SignatureHasElementType : SignatureType
	{
		// Token: 0x06004A5B RID: 19035 RVA: 0x000EF934 File Offset: 0x000EDB34
		protected SignatureHasElementType(SignatureType elementType)
		{
			this._elementType = elementType;
		}

		// Token: 0x17000BCC RID: 3020
		// (get) Token: 0x06004A5C RID: 19036 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		public sealed override bool IsTypeDefinition
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000BCD RID: 3021
		// (get) Token: 0x06004A5D RID: 19037 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		public sealed override bool IsGenericTypeDefinition
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06004A5E RID: 19038 RVA: 0x000040F7 File Offset: 0x000022F7
		protected sealed override bool HasElementTypeImpl()
		{
			return true;
		}

		// Token: 0x06004A5F RID: 19039
		protected abstract override bool IsArrayImpl();

		// Token: 0x06004A60 RID: 19040
		protected abstract override bool IsByRefImpl();

		// Token: 0x17000BCE RID: 3022
		// (get) Token: 0x06004A61 RID: 19041 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		public sealed override bool IsByRefLike
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06004A62 RID: 19042
		protected abstract override bool IsPointerImpl();

		// Token: 0x17000BCF RID: 3023
		// (get) Token: 0x06004A63 RID: 19043
		public abstract override bool IsSZArray { get; }

		// Token: 0x17000BD0 RID: 3024
		// (get) Token: 0x06004A64 RID: 19044
		public abstract override bool IsVariableBoundArray { get; }

		// Token: 0x17000BD1 RID: 3025
		// (get) Token: 0x06004A65 RID: 19045 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		public sealed override bool IsConstructedGenericType
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000BD2 RID: 3026
		// (get) Token: 0x06004A66 RID: 19046 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		public sealed override bool IsGenericParameter
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000BD3 RID: 3027
		// (get) Token: 0x06004A67 RID: 19047 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		public sealed override bool IsGenericTypeParameter
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000BD4 RID: 3028
		// (get) Token: 0x06004A68 RID: 19048 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		public sealed override bool IsGenericMethodParameter
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000BD5 RID: 3029
		// (get) Token: 0x06004A69 RID: 19049 RVA: 0x000EF943 File Offset: 0x000EDB43
		public sealed override bool ContainsGenericParameters
		{
			get
			{
				return this._elementType.ContainsGenericParameters;
			}
		}

		// Token: 0x17000BD6 RID: 3030
		// (get) Token: 0x06004A6A RID: 19050 RVA: 0x000EF950 File Offset: 0x000EDB50
		internal sealed override SignatureType ElementType
		{
			get
			{
				return this._elementType;
			}
		}

		// Token: 0x06004A6B RID: 19051
		public abstract override int GetArrayRank();

		// Token: 0x06004A6C RID: 19052 RVA: 0x000EF920 File Offset: 0x000EDB20
		public sealed override Type GetGenericTypeDefinition()
		{
			throw new InvalidOperationException("This operation is only valid on generic types.");
		}

		// Token: 0x06004A6D RID: 19053 RVA: 0x000EF316 File Offset: 0x000ED516
		public sealed override Type[] GetGenericArguments()
		{
			return Array.Empty<Type>();
		}

		// Token: 0x17000BD7 RID: 3031
		// (get) Token: 0x06004A6E RID: 19054 RVA: 0x000EF316 File Offset: 0x000ED516
		public sealed override Type[] GenericTypeArguments
		{
			get
			{
				return Array.Empty<Type>();
			}
		}

		// Token: 0x17000BD8 RID: 3032
		// (get) Token: 0x06004A6F RID: 19055 RVA: 0x000472BC File Offset: 0x000454BC
		public sealed override int GenericParameterPosition
		{
			get
			{
				throw new InvalidOperationException("Method may only be called on a Type for which Type.IsGenericParameter is true.");
			}
		}

		// Token: 0x17000BD9 RID: 3033
		// (get) Token: 0x06004A70 RID: 19056 RVA: 0x000EF958 File Offset: 0x000EDB58
		public sealed override string Name
		{
			get
			{
				return this._elementType.Name + this.Suffix;
			}
		}

		// Token: 0x17000BDA RID: 3034
		// (get) Token: 0x06004A71 RID: 19057 RVA: 0x000EF970 File Offset: 0x000EDB70
		public sealed override string Namespace
		{
			get
			{
				return this._elementType.Namespace;
			}
		}

		// Token: 0x06004A72 RID: 19058 RVA: 0x000EF97D File Offset: 0x000EDB7D
		public sealed override string ToString()
		{
			return this._elementType.ToString() + this.Suffix;
		}

		// Token: 0x17000BDB RID: 3035
		// (get) Token: 0x06004A73 RID: 19059
		protected abstract string Suffix { get; }

		// Token: 0x04002F34 RID: 12084
		private readonly SignatureType _elementType;
	}
}
