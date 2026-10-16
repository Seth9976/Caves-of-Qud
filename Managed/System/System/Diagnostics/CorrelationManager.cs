using System;
using System.Collections;
using System.Runtime.Remoting.Messaging;

namespace System.Diagnostics
{
	/// <summary>Correlates traces that are part of a logical transaction.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000217 RID: 535
	public class CorrelationManager
	{
		// Token: 0x06000F57 RID: 3927 RVA: 0x0000219B File Offset: 0x0000039B
		internal CorrelationManager()
		{
		}

		/// <summary>Gets or sets the identity for a global activity.</summary>
		/// <returns>A <see cref="T:System.Guid" /> structure that identifies the global activity.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700026F RID: 623
		// (get) Token: 0x06000F58 RID: 3928 RVA: 0x00044B48 File Offset: 0x00042D48
		// (set) Token: 0x06000F59 RID: 3929 RVA: 0x00044B6F File Offset: 0x00042D6F
		public Guid ActivityId
		{
			get
			{
				object obj = CallContext.LogicalGetData("E2ETrace.ActivityID");
				if (obj != null)
				{
					return (Guid)obj;
				}
				return Guid.Empty;
			}
			set
			{
				CallContext.LogicalSetData("E2ETrace.ActivityID", value);
			}
		}

		/// <summary>Gets the logical operation stack from the call context.</summary>
		/// <returns>A <see cref="T:System.Collections.Stack" /> object that represents the logical operation stack for the call context.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000270 RID: 624
		// (get) Token: 0x06000F5A RID: 3930 RVA: 0x00044B81 File Offset: 0x00042D81
		public Stack LogicalOperationStack
		{
			get
			{
				return this.GetLogicalOperationStack();
			}
		}

		/// <summary>Starts a logical operation with the specified identity on a thread.</summary>
		/// <param name="operationId">An object identifying the operation.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="operationId" /> parameter is null. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F5B RID: 3931 RVA: 0x00044B89 File Offset: 0x00042D89
		public void StartLogicalOperation(object operationId)
		{
			if (operationId == null)
			{
				throw new ArgumentNullException("operationId");
			}
			this.GetLogicalOperationStack().Push(operationId);
		}

		/// <summary>Starts a logical operation on a thread.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F5C RID: 3932 RVA: 0x00044BA5 File Offset: 0x00042DA5
		public void StartLogicalOperation()
		{
			this.StartLogicalOperation(Guid.NewGuid());
		}

		/// <summary>Stops the current logical operation.</summary>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Diagnostics.CorrelationManager.LogicalOperationStack" /> property is an empty stack.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F5D RID: 3933 RVA: 0x00044BB7 File Offset: 0x00042DB7
		public void StopLogicalOperation()
		{
			this.GetLogicalOperationStack().Pop();
		}

		// Token: 0x06000F5E RID: 3934 RVA: 0x00044BC8 File Offset: 0x00042DC8
		private Stack GetLogicalOperationStack()
		{
			Stack stack = CallContext.LogicalGetData("System.Diagnostics.Trace.CorrelationManagerSlot") as Stack;
			if (stack == null)
			{
				stack = new Stack();
				CallContext.LogicalSetData("System.Diagnostics.Trace.CorrelationManagerSlot", stack);
			}
			return stack;
		}

		// Token: 0x040009B1 RID: 2481
		private const string transactionSlotName = "System.Diagnostics.Trace.CorrelationManagerSlot";

		// Token: 0x040009B2 RID: 2482
		private const string activityIdSlotName = "E2ETrace.ActivityID";
	}
}
