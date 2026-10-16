using System;
using Mono.Collections.Generic;

namespace Mono.Cecil
{
	// Token: 0x02000222 RID: 546
	internal struct ImportGenericContext
	{
		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x06000BBA RID: 3002 RVA: 0x000285B8 File Offset: 0x000267B8
		public bool IsEmpty
		{
			get
			{
				return this.stack == null;
			}
		}

		// Token: 0x06000BBB RID: 3003 RVA: 0x000285C3 File Offset: 0x000267C3
		public ImportGenericContext(IGenericParameterProvider provider)
		{
			if (provider == null)
			{
				throw new ArgumentNullException("provider");
			}
			this.stack = null;
			this.Push(provider);
		}

		// Token: 0x06000BBC RID: 3004 RVA: 0x000285E1 File Offset: 0x000267E1
		public void Push(IGenericParameterProvider provider)
		{
			if (this.stack == null)
			{
				this.stack = new Collection<IGenericParameterProvider>(1) { provider };
				return;
			}
			this.stack.Add(provider);
		}

		// Token: 0x06000BBD RID: 3005 RVA: 0x0002860B File Offset: 0x0002680B
		public void Pop()
		{
			this.stack.RemoveAt(this.stack.Count - 1);
		}

		// Token: 0x06000BBE RID: 3006 RVA: 0x00028628 File Offset: 0x00026828
		public TypeReference MethodParameter(string method, int position)
		{
			for (int i = this.stack.Count - 1; i >= 0; i--)
			{
				MethodReference methodReference = this.stack[i] as MethodReference;
				if (methodReference != null && !(method != this.NormalizeMethodName(methodReference)))
				{
					return methodReference.GenericParameters[position];
				}
			}
			throw new InvalidOperationException();
		}

		// Token: 0x06000BBF RID: 3007 RVA: 0x00028683 File Offset: 0x00026883
		public string NormalizeMethodName(MethodReference method)
		{
			return method.DeclaringType.GetElementType().FullName + "." + method.Name;
		}

		// Token: 0x06000BC0 RID: 3008 RVA: 0x000286A8 File Offset: 0x000268A8
		public TypeReference TypeParameter(string type, int position)
		{
			for (int i = this.stack.Count - 1; i >= 0; i--)
			{
				TypeReference typeReference = ImportGenericContext.GenericTypeFor(this.stack[i]);
				if (!(typeReference.FullName != type))
				{
					return typeReference.GenericParameters[position];
				}
			}
			throw new InvalidOperationException();
		}

		// Token: 0x06000BC1 RID: 3009 RVA: 0x00028700 File Offset: 0x00026900
		private static TypeReference GenericTypeFor(IGenericParameterProvider context)
		{
			TypeReference typeReference = context as TypeReference;
			if (typeReference != null)
			{
				return typeReference.GetElementType();
			}
			MethodReference methodReference = context as MethodReference;
			if (methodReference != null)
			{
				return methodReference.DeclaringType.GetElementType();
			}
			throw new InvalidOperationException();
		}

		// Token: 0x06000BC2 RID: 3010 RVA: 0x0002873C File Offset: 0x0002693C
		public static ImportGenericContext For(IGenericParameterProvider context)
		{
			if (context == null)
			{
				return default(ImportGenericContext);
			}
			return new ImportGenericContext(context);
		}

		// Token: 0x0400034E RID: 846
		private Collection<IGenericParameterProvider> stack;
	}
}
