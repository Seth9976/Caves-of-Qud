using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Operations;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x020001C3 RID: 451
	[NullableContext(1)]
	[Nullable(0)]
	internal abstract class Operation : IOperation
	{
		// Token: 0x060012E9 RID: 4841 RVA: 0x00039DF1 File Offset: 0x00037FF1
		protected Operation([Nullable(2)] SemanticModel semanticModel, SyntaxNode syntax, bool isImplicit)
		{
			this._owningSemanticModelOpt = semanticModel;
			this.Syntax = syntax;
			this.IsImplicit = isImplicit;
			this._parentDoNotAccessDirectly = Operation.s_unset;
		}

		// Token: 0x17000424 RID: 1060
		// (get) Token: 0x060012EA RID: 4842 RVA: 0x00039E19 File Offset: 0x00038019
		[Nullable(2)]
		public IOperation Parent
		{
			[NullableContext(2)]
			get
			{
				return this._parentDoNotAccessDirectly;
			}
		}

		// Token: 0x17000425 RID: 1061
		// (get) Token: 0x060012EB RID: 4843 RVA: 0x00039E21 File Offset: 0x00038021
		public bool IsImplicit { get; }

		// Token: 0x17000426 RID: 1062
		// (get) Token: 0x060012EC RID: 4844
		public abstract OperationKind Kind { get; }

		// Token: 0x17000427 RID: 1063
		// (get) Token: 0x060012ED RID: 4845 RVA: 0x00039E29 File Offset: 0x00038029
		public SyntaxNode Syntax { get; }

		// Token: 0x17000428 RID: 1064
		// (get) Token: 0x060012EE RID: 4846
		[Nullable(2)]
		public abstract ITypeSymbol Type
		{
			[NullableContext(2)]
			get;
		}

		// Token: 0x17000429 RID: 1065
		// (get) Token: 0x060012EF RID: 4847 RVA: 0x00039E31 File Offset: 0x00038031
		public string Language
		{
			get
			{
				return this.Syntax.Language;
			}
		}

		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x060012F0 RID: 4848
		[Nullable(2)]
		internal abstract ConstantValue OperationConstantValue
		{
			[NullableContext(2)]
			get;
		}

		// Token: 0x1700042B RID: 1067
		// (get) Token: 0x060012F1 RID: 4849 RVA: 0x00039E40 File Offset: 0x00038040
		[Nullable(new byte[] { 0, 2 })]
		public Optional<object> ConstantValue
		{
			[return: Nullable(new byte[] { 0, 2 })]
			get
			{
				if (this.OperationConstantValue == null || this.OperationConstantValue.IsBad)
				{
					return default(Optional<object>);
				}
				return new Optional<object>(this.OperationConstantValue.Value);
			}
		}

		// Token: 0x1700042C RID: 1068
		// (get) Token: 0x060012F2 RID: 4850 RVA: 0x00039E82 File Offset: 0x00038082
		IEnumerable<IOperation> IOperation.Children
		{
			get
			{
				return this.ChildOperations;
			}
		}

		// Token: 0x1700042D RID: 1069
		// (get) Token: 0x060012F3 RID: 4851 RVA: 0x00039E8F File Offset: 0x0003808F
		internal Operation.Enumerable ChildOperations
		{
			get
			{
				return new Operation.Enumerable(this);
			}
		}

		// Token: 0x060012F4 RID: 4852
		protected abstract IOperation GetCurrent(int slot, int index);

		// Token: 0x060012F5 RID: 4853
		[NullableContext(0)]
		[return: TupleElementNames(new string[] { "hasNext", "nextSlot", "nextIndex" })]
		protected abstract ValueTuple<bool, int, int> MoveNext(int previousSlot, int previousIndex);

		// Token: 0x1700042E RID: 1070
		// (get) Token: 0x060012F6 RID: 4854 RVA: 0x00039E97 File Offset: 0x00038097
		[Nullable(2)]
		SemanticModel IOperation.SemanticModel
		{
			[NullableContext(2)]
			get
			{
				SemanticModel owningSemanticModelOpt = this._owningSemanticModelOpt;
				if (owningSemanticModelOpt == null)
				{
					return null;
				}
				return owningSemanticModelOpt.ContainingModelOrSelf;
			}
		}

		// Token: 0x1700042F RID: 1071
		// (get) Token: 0x060012F7 RID: 4855 RVA: 0x00039EAA File Offset: 0x000380AA
		[Nullable(2)]
		internal SemanticModel OwningSemanticModel
		{
			[NullableContext(2)]
			get
			{
				return this._owningSemanticModelOpt;
			}
		}

		// Token: 0x060012F8 RID: 4856
		public abstract void Accept(OperationVisitor visitor);

		// Token: 0x060012F9 RID: 4857
		[NullableContext(2)]
		public abstract TResult Accept<TArgument, TResult>([Nullable(1)] OperationVisitor<TArgument, TResult> visitor, [Nullable(1)] TArgument argument);

		// Token: 0x060012FA RID: 4858 RVA: 0x00039EB2 File Offset: 0x000380B2
		[NullableContext(2)]
		protected void SetParentOperation(IOperation parent)
		{
			this._parentDoNotAccessDirectly = parent;
		}

		// Token: 0x060012FB RID: 4859 RVA: 0x00039EBB File Offset: 0x000380BB
		[NullableContext(2)]
		[return: NotNullIfNotNull("operation")]
		public static T SetParentOperation<[Nullable(0)] T>(T operation, IOperation parent) where T : IOperation
		{
			Operation operation2 = operation as Operation;
			if (operation2 != null)
			{
				operation2.SetParentOperation(parent);
			}
			return operation;
		}

		// Token: 0x060012FC RID: 4860 RVA: 0x00039ED8 File Offset: 0x000380D8
		[NullableContext(0)]
		[return: Nullable(new byte[] { 0, 1 })]
		public static ImmutableArray<T> SetParentOperation<T>([Nullable(new byte[] { 0, 1 })] ImmutableArray<T> operations, [Nullable(2)] IOperation parent) where T : IOperation
		{
			if (operations.Length == 0)
			{
				return operations;
			}
			foreach (T t in operations)
			{
				Operation.SetParentOperation<T>(t, parent);
			}
			return operations;
		}

		// Token: 0x060012FD RID: 4861 RVA: 0x00039F12 File Offset: 0x00038112
		[Conditional("DEBUG")]
		internal static void VerifyParentOperation([Nullable(2)] IOperation parent, IOperation child)
		{
		}

		// Token: 0x060012FE RID: 4862 RVA: 0x00039F18 File Offset: 0x00038118
		[NullableContext(0)]
		[Conditional("DEBUG")]
		internal static void VerifyParentOperation<T>([Nullable(2)] IOperation parent, [Nullable(new byte[] { 0, 1 })] ImmutableArray<T> children) where T : IOperation
		{
			foreach (T t in children)
			{
			}
		}

		// Token: 0x040005A0 RID: 1440
		protected static readonly IOperation s_unset = new EmptyOperation(null, null, true);

		// Token: 0x040005A1 RID: 1441
		[Nullable(2)]
		private readonly SemanticModel _owningSemanticModelOpt;

		// Token: 0x040005A2 RID: 1442
		[Nullable(2)]
		private IOperation _parentDoNotAccessDirectly;

		// Token: 0x040005A5 RID: 1445
		private static readonly ObjectPool<Queue<IOperation>> s_queuePool = new ObjectPool<Queue<IOperation>>(() => new Queue<IOperation>(), 10);

		// Token: 0x020005C9 RID: 1481
		[Nullable(0)]
		[NonDefaultable]
		internal readonly struct Enumerable : IEnumerable<IOperation>, IEnumerable
		{
			// Token: 0x06003655 RID: 13909 RVA: 0x0009921F File Offset: 0x0009741F
			internal Enumerable(Operation operation)
			{
				this._operation = operation;
			}

			// Token: 0x06003656 RID: 13910 RVA: 0x00099228 File Offset: 0x00097428
			public Operation.Enumerator GetEnumerator()
			{
				return new Operation.Enumerator(this._operation);
			}

			// Token: 0x06003657 RID: 13911 RVA: 0x00099238 File Offset: 0x00097438
			[return: Nullable(new byte[] { 0, 1 })]
			public ImmutableArray<IOperation> ToImmutableArray()
			{
				Operation operation = this._operation;
				NoneOperation noneOperation = operation as NoneOperation;
				if (noneOperation != null)
				{
					return noneOperation.Children;
				}
				InvalidOperation invalidOperation = operation as InvalidOperation;
				if (invalidOperation != null)
				{
					return invalidOperation.Children;
				}
				Operation.Enumerator enumerator = this.GetEnumerator();
				if (!enumerator.MoveNext())
				{
					return ImmutableArray<IOperation>.Empty;
				}
				ArrayBuilder<IOperation> instance = ArrayBuilder<IOperation>.GetInstance();
				foreach (IOperation operation2 in this)
				{
					instance.Add(operation2);
				}
				return instance.ToImmutableAndFree();
			}

			// Token: 0x06003658 RID: 13912 RVA: 0x000992E4 File Offset: 0x000974E4
			IEnumerator<IOperation> IEnumerable<IOperation>.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x06003659 RID: 13913 RVA: 0x000992F1 File Offset: 0x000974F1
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x040016A4 RID: 5796
			private readonly Operation _operation;
		}

		// Token: 0x020005CA RID: 1482
		[Nullable(0)]
		[NonDefaultable]
		internal struct Enumerator : IEnumerator<IOperation>, IEnumerator, IDisposable
		{
			// Token: 0x0600365A RID: 13914 RVA: 0x000992FE File Offset: 0x000974FE
			public Enumerator(Operation operation)
			{
				this._operation = operation;
				this._currentSlot = -1;
				this._currentIndex = -1;
			}

			// Token: 0x17000F53 RID: 3923
			// (get) Token: 0x0600365B RID: 13915 RVA: 0x00099315 File Offset: 0x00097515
			public IOperation Current
			{
				get
				{
					return this._operation.GetCurrent(this._currentSlot, this._currentIndex);
				}
			}

			// Token: 0x0600365C RID: 13916 RVA: 0x00099330 File Offset: 0x00097530
			public bool MoveNext()
			{
				ValueTuple<bool, int, int> valueTuple = this._operation.MoveNext(this._currentSlot, this._currentIndex);
				bool item = valueTuple.Item1;
				this._currentSlot = valueTuple.Item2;
				this._currentIndex = valueTuple.Item3;
				return item;
			}

			// Token: 0x0600365D RID: 13917 RVA: 0x00099373 File Offset: 0x00097573
			void IEnumerator.Reset()
			{
				this._currentSlot = -1;
				this._currentIndex = -1;
			}

			// Token: 0x17000F54 RID: 3924
			// (get) Token: 0x0600365E RID: 13918 RVA: 0x00099383 File Offset: 0x00097583
			[Nullable(2)]
			object IEnumerator.Current
			{
				[NullableContext(2)]
				get
				{
					return this.Current;
				}
			}

			// Token: 0x0600365F RID: 13919 RVA: 0x0009938B File Offset: 0x0009758B
			void IDisposable.Dispose()
			{
			}

			// Token: 0x040016A5 RID: 5797
			private readonly Operation _operation;

			// Token: 0x040016A6 RID: 5798
			private int _currentSlot;

			// Token: 0x040016A7 RID: 5799
			private int _currentIndex;
		}
	}
}
