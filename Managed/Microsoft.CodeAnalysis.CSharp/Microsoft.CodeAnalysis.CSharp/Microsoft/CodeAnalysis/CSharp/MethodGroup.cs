using System;
using System.Collections.Immutable;
using System.Diagnostics;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.PooledObjects;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000067 RID: 103
	internal sealed class MethodGroup
	{
		// Token: 0x17000100 RID: 256
		// (get) Token: 0x06000944 RID: 2372 RVA: 0x00049854 File Offset: 0x00047A54
		// (set) Token: 0x06000945 RID: 2373 RVA: 0x0004985C File Offset: 0x00047A5C
		internal BoundExpression Receiver { get; private set; }

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x06000946 RID: 2374 RVA: 0x00049865 File Offset: 0x00047A65
		internal ArrayBuilder<MethodSymbol> Methods { get; }

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x06000947 RID: 2375 RVA: 0x0004986D File Offset: 0x00047A6D
		internal ArrayBuilder<TypeWithAnnotations> TypeArguments { get; }

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x06000948 RID: 2376 RVA: 0x00049875 File Offset: 0x00047A75
		// (set) Token: 0x06000949 RID: 2377 RVA: 0x0004987D File Offset: 0x00047A7D
		internal bool IsExtensionMethodGroup { get; private set; }

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x0600094A RID: 2378 RVA: 0x00049886 File Offset: 0x00047A86
		// (set) Token: 0x0600094B RID: 2379 RVA: 0x0004988E File Offset: 0x00047A8E
		internal DiagnosticInfo Error { get; private set; }

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x0600094C RID: 2380 RVA: 0x00049897 File Offset: 0x00047A97
		// (set) Token: 0x0600094D RID: 2381 RVA: 0x0004989F File Offset: 0x00047A9F
		internal LookupResultKind ResultKind { get; private set; }

		// Token: 0x0600094E RID: 2382 RVA: 0x000498A8 File Offset: 0x00047AA8
		private MethodGroup()
		{
			this.Methods = new ArrayBuilder<MethodSymbol>();
			this.TypeArguments = new ArrayBuilder<TypeWithAnnotations>();
		}

		// Token: 0x0600094F RID: 2383 RVA: 0x000498C6 File Offset: 0x00047AC6
		internal void PopulateWithSingleMethod(BoundExpression receiverOpt, MethodSymbol method, LookupResultKind resultKind = LookupResultKind.Viable, DiagnosticInfo error = null)
		{
			this.PopulateHelper(receiverOpt, resultKind, error);
			this.Methods.Add(method);
		}

		// Token: 0x06000950 RID: 2384 RVA: 0x000498E0 File Offset: 0x00047AE0
		internal void PopulateWithExtensionMethods(BoundExpression receiverOpt, ArrayBuilder<Symbol> members, ImmutableArray<TypeWithAnnotations> typeArguments, LookupResultKind resultKind = LookupResultKind.Viable, DiagnosticInfo error = null)
		{
			this.PopulateHelper(receiverOpt, resultKind, error);
			this.IsExtensionMethodGroup = true;
			foreach (Symbol symbol in members)
			{
				this.Methods.Add((MethodSymbol)symbol);
			}
			if (!typeArguments.IsDefault)
			{
				this.TypeArguments.AddRange(typeArguments);
			}
		}

		// Token: 0x06000951 RID: 2385 RVA: 0x0004993F File Offset: 0x00047B3F
		internal void PopulateWithNonExtensionMethods(BoundExpression receiverOpt, ImmutableArray<MethodSymbol> methods, ImmutableArray<TypeWithAnnotations> typeArguments, LookupResultKind resultKind = LookupResultKind.Viable, DiagnosticInfo error = null)
		{
			this.PopulateHelper(receiverOpt, resultKind, error);
			this.Methods.AddRange(methods);
			if (!typeArguments.IsDefault)
			{
				this.TypeArguments.AddRange(typeArguments);
			}
		}

		// Token: 0x06000952 RID: 2386 RVA: 0x0004996D File Offset: 0x00047B6D
		private void PopulateHelper(BoundExpression receiverOpt, LookupResultKind resultKind, DiagnosticInfo error)
		{
			this.Receiver = receiverOpt;
			this.Error = error;
			this.ResultKind = resultKind;
		}

		// Token: 0x06000953 RID: 2387 RVA: 0x00049984 File Offset: 0x00047B84
		public void Clear()
		{
			this.Receiver = null;
			this.Methods.Clear();
			this.TypeArguments.Clear();
			this.IsExtensionMethodGroup = false;
			this.Error = null;
			this.ResultKind = LookupResultKind.Empty;
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000954 RID: 2388 RVA: 0x000499B8 File Offset: 0x00047BB8
		public string Name
		{
			get
			{
				if (this.Methods.Count <= 0)
				{
					return null;
				}
				return this.Methods[0].Name;
			}
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x06000955 RID: 2389 RVA: 0x000499DB File Offset: 0x00047BDB
		public BoundExpression InstanceOpt
		{
			get
			{
				if (this.Receiver == null)
				{
					return null;
				}
				if (this.Receiver.Kind == BoundKind.TypeExpression)
				{
					return null;
				}
				return this.Receiver;
			}
		}

		// Token: 0x06000956 RID: 2390 RVA: 0x000499FE File Offset: 0x00047BFE
		[Conditional("DEBUG")]
		private void VerifyClear()
		{
		}

		// Token: 0x06000957 RID: 2391 RVA: 0x00049A00 File Offset: 0x00047C00
		public static MethodGroup GetInstance()
		{
			return MethodGroup.Pool.Allocate();
		}

		// Token: 0x06000958 RID: 2392 RVA: 0x00049A0C File Offset: 0x00047C0C
		public void Free()
		{
			this.Clear();
			MethodGroup.Pool.Free(this);
		}

		// Token: 0x06000959 RID: 2393 RVA: 0x00049A1F File Offset: 0x00047C1F
		private static ObjectPool<MethodGroup> CreatePool()
		{
			return new ObjectPool<MethodGroup>(() => new MethodGroup(), 10);
		}

		// Token: 0x04000445 RID: 1093
		public static readonly ObjectPool<MethodGroup> Pool = MethodGroup.CreatePool();
	}
}
