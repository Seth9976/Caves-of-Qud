using System;
using System.Collections.Generic;

namespace NUnit.Framework.Constraints
{
	// Token: 0x020000FA RID: 250
	public class ConstraintBuilder : IResolveConstraint
	{
		// Token: 0x060007B4 RID: 1972 RVA: 0x00012C06 File Offset: 0x00010E06
		public ConstraintBuilder()
		{
			this.ops = new ConstraintBuilder.OperatorStack(this);
			this.constraints = new ConstraintBuilder.ConstraintStack(this);
		}

		// Token: 0x060007B5 RID: 1973 RVA: 0x00012C28 File Offset: 0x00010E28
		public void Append(ConstraintOperator op)
		{
			op.LeftContext = this.lastPushed;
			if (this.lastPushed is ConstraintOperator)
			{
				this.SetTopOperatorRightContext(op);
			}
			this.ReduceOperatorStack(op.LeftPrecedence);
			this.ops.Push(op);
			this.lastPushed = op;
		}

		// Token: 0x060007B6 RID: 1974 RVA: 0x00012C74 File Offset: 0x00010E74
		public void Append(Constraint constraint)
		{
			if (this.lastPushed is ConstraintOperator)
			{
				this.SetTopOperatorRightContext(constraint);
			}
			this.constraints.Push(constraint);
			this.lastPushed = constraint;
			constraint.Builder = this;
		}

		// Token: 0x060007B7 RID: 1975 RVA: 0x00012CA4 File Offset: 0x00010EA4
		private void SetTopOperatorRightContext(object rightContext)
		{
			int leftPrecedence = this.ops.Top.LeftPrecedence;
			this.ops.Top.RightContext = rightContext;
			if (this.ops.Top.LeftPrecedence > leftPrecedence)
			{
				ConstraintOperator constraintOperator = this.ops.Pop();
				this.ReduceOperatorStack(constraintOperator.LeftPrecedence);
				this.ops.Push(constraintOperator);
			}
		}

		// Token: 0x060007B8 RID: 1976 RVA: 0x00012D0A File Offset: 0x00010F0A
		private void ReduceOperatorStack(int targetPrecedence)
		{
			while (!this.ops.Empty && this.ops.Top.RightPrecedence < targetPrecedence)
			{
				this.ops.Pop().Reduce(this.constraints);
			}
		}

		// Token: 0x060007B9 RID: 1977 RVA: 0x00012D44 File Offset: 0x00010F44
		public IConstraint Resolve()
		{
			if (!this.IsResolvable)
			{
				throw new InvalidOperationException("A partial expression may not be resolved");
			}
			while (!this.ops.Empty)
			{
				this.ops.Pop().Reduce(this.constraints);
			}
			return this.constraints.Pop();
		}

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x060007BA RID: 1978 RVA: 0x00012D92 File Offset: 0x00010F92
		private bool IsResolvable
		{
			get
			{
				return this.lastPushed is Constraint || this.lastPushed is SelfResolvingOperator;
			}
		}

		// Token: 0x040001AD RID: 429
		private readonly ConstraintBuilder.OperatorStack ops;

		// Token: 0x040001AE RID: 430
		private readonly ConstraintBuilder.ConstraintStack constraints;

		// Token: 0x040001AF RID: 431
		private object lastPushed;

		// Token: 0x0200019E RID: 414
		public class OperatorStack
		{
			// Token: 0x06000B09 RID: 2825 RVA: 0x0001AF8A File Offset: 0x0001918A
			public OperatorStack(ConstraintBuilder builder)
			{
			}

			// Token: 0x17000278 RID: 632
			// (get) Token: 0x06000B0A RID: 2826 RVA: 0x0001AF9D File Offset: 0x0001919D
			public bool Empty
			{
				get
				{
					return this.stack.Count == 0;
				}
			}

			// Token: 0x17000279 RID: 633
			// (get) Token: 0x06000B0B RID: 2827 RVA: 0x0001AFAD File Offset: 0x000191AD
			public ConstraintOperator Top
			{
				get
				{
					return this.stack.Peek();
				}
			}

			// Token: 0x06000B0C RID: 2828 RVA: 0x0001AFBA File Offset: 0x000191BA
			public void Push(ConstraintOperator op)
			{
				this.stack.Push(op);
			}

			// Token: 0x06000B0D RID: 2829 RVA: 0x0001AFC8 File Offset: 0x000191C8
			public ConstraintOperator Pop()
			{
				return this.stack.Pop();
			}

			// Token: 0x040002AD RID: 685
			private readonly Stack<ConstraintOperator> stack = new Stack<ConstraintOperator>();
		}

		// Token: 0x0200019F RID: 415
		public class ConstraintStack
		{
			// Token: 0x06000B0E RID: 2830 RVA: 0x0001AFD5 File Offset: 0x000191D5
			public ConstraintStack(ConstraintBuilder builder)
			{
				this.builder = builder;
			}

			// Token: 0x1700027A RID: 634
			// (get) Token: 0x06000B0F RID: 2831 RVA: 0x0001AFEF File Offset: 0x000191EF
			public bool Empty
			{
				get
				{
					return this.stack.Count == 0;
				}
			}

			// Token: 0x06000B10 RID: 2832 RVA: 0x0001AFFF File Offset: 0x000191FF
			public void Push(IConstraint constraint)
			{
				this.stack.Push(constraint);
				constraint.Builder = this.builder;
			}

			// Token: 0x06000B11 RID: 2833 RVA: 0x0001B019 File Offset: 0x00019219
			public IConstraint Pop()
			{
				IConstraint constraint = this.stack.Pop();
				constraint.Builder = null;
				return constraint;
			}

			// Token: 0x040002AE RID: 686
			private readonly Stack<IConstraint> stack = new Stack<IConstraint>();

			// Token: 0x040002AF RID: 687
			private readonly ConstraintBuilder builder;
		}
	}
}
