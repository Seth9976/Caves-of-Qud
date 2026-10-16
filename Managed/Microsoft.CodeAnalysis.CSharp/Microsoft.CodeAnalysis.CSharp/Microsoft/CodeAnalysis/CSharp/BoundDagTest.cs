using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000086 RID: 134
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal abstract class BoundDagTest : BoundNode
	{
		// Token: 0x06000B3A RID: 2874 RVA: 0x000520A4 File Offset: 0x000502A4
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override bool Equals([NotNullWhen(true)] object obj)
		{
			return this.Equals(obj as BoundDagTest);
		}

		// Token: 0x06000B3B RID: 2875 RVA: 0x000520B4 File Offset: 0x000502B4
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		private bool Equals(BoundDagTest other)
		{
			if (other == null || base.Kind != other.Kind)
			{
				return false;
			}
			if (this == other)
			{
				return true;
			}
			BoundDagTypeTest boundDagTypeTest = this as BoundDagTypeTest;
			if (boundDagTypeTest != null)
			{
				BoundDagTypeTest boundDagTypeTest2 = other as BoundDagTypeTest;
				if (boundDagTypeTest2 != null)
				{
					return boundDagTypeTest.Type.Equals(boundDagTypeTest2.Type, TypeCompareKind.AllIgnoreOptions);
				}
			}
			else
			{
				BoundDagNonNullTest boundDagNonNullTest = this as BoundDagNonNullTest;
				if (boundDagNonNullTest != null)
				{
					BoundDagNonNullTest boundDagNonNullTest2 = other as BoundDagNonNullTest;
					if (boundDagNonNullTest2 != null)
					{
						return boundDagNonNullTest.IsExplicitTest == boundDagNonNullTest2.IsExplicitTest;
					}
				}
				else if (this is BoundDagExplicitNullTest)
				{
					if (other is BoundDagExplicitNullTest)
					{
						return true;
					}
				}
				else
				{
					BoundDagValueTest boundDagValueTest = this as BoundDagValueTest;
					if (boundDagValueTest != null)
					{
						BoundDagValueTest boundDagValueTest2 = other as BoundDagValueTest;
						if (boundDagValueTest2 != null)
						{
							return boundDagValueTest.Value.Equals(boundDagValueTest2.Value);
						}
					}
					else
					{
						BoundDagRelationalTest boundDagRelationalTest = this as BoundDagRelationalTest;
						if (boundDagRelationalTest != null)
						{
							BoundDagRelationalTest boundDagRelationalTest2 = other as BoundDagRelationalTest;
							if (boundDagRelationalTest2 != null)
							{
								return boundDagRelationalTest.Relation == boundDagRelationalTest2.Relation && boundDagRelationalTest.Value.Equals(boundDagRelationalTest2.Value);
							}
						}
					}
				}
			}
			throw ExceptionUtilities.UnexpectedValue(this);
		}

		// Token: 0x06000B3C RID: 2876 RVA: 0x000521C4 File Offset: 0x000503C4
		public override int GetHashCode()
		{
			return Hash.Combine(base.Kind.GetHashCode(), this.Input.GetHashCode());
		}

		// Token: 0x06000B3D RID: 2877 RVA: 0x000521F5 File Offset: 0x000503F5
		protected BoundDagTest(BoundKind kind, SyntaxNode syntax, BoundDagTemp input, bool hasErrors = false)
			: base(kind, syntax, hasErrors)
		{
			this.Input = input;
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x06000B3E RID: 2878 RVA: 0x00052208 File Offset: 0x00050408
		public BoundDagTemp Input { get; }
	}
}
