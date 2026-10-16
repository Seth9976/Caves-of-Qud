using System;
using System.CodeDom;
using System.Web.UI;

namespace System.Web.Compilation
{
	/// <summary>Evaluates expressions during page parsing.</summary>
	// Token: 0x0200064D RID: 1613
	public abstract class ExpressionBuilder
	{
		/// <summary>When overridden in a derived class, returns code that is used during page execution to obtain the evaluated expression.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> that is used for property assignment.</returns>
		/// <param name="entry">The object that represents information about the property bound to by the expression.</param>
		/// <param name="parsedData">The object containing parsed data as returned by <see cref="M:System.Web.Compilation.ExpressionBuilder.ParseExpression(System.String,System.Type,System.Web.Compilation.ExpressionBuilderContext)" />. </param>
		/// <param name="context">Contextual information for the evaluation of the expression.</param>
		// Token: 0x06004542 RID: 17730
		public abstract CodeExpression GetCodeExpression(BoundPropertyEntry entry, object parsedData, ExpressionBuilderContext context);

		/// <summary>When overridden in a derived class, returns an object that represents an evaluated expression.</summary>
		/// <returns>An object that represents the evaluated expression; otherwise, null if the inheritor does not implement <see cref="M:System.Web.Compilation.ExpressionBuilder.EvaluateExpression(System.Object,System.Web.UI.BoundPropertyEntry,System.Object,System.Web.Compilation.ExpressionBuilderContext)" />.</returns>
		/// <param name="target">The object containing the expression.</param>
		/// <param name="entry">The object that represents information about the property bound to by the expression.</param>
		/// <param name="parsedData">The object containing parsed data as returned by <see cref="M:System.Web.Compilation.ExpressionBuilder.ParseExpression(System.String,System.Type,System.Web.Compilation.ExpressionBuilderContext)" />.</param>
		/// <param name="context">Contextual information for the evaluation of the expression.</param>
		// Token: 0x06004543 RID: 17731 RVA: 0x00003BF6 File Offset: 0x00001DF6
		public virtual object EvaluateExpression(object target, BoundPropertyEntry entry, object parsedData, ExpressionBuilderContext context)
		{
			return null;
		}

		/// <summary>When overridden in a derived class, returns an object that represents the parsed expression.</summary>
		/// <returns>An <see cref="T:System.Object" /> containing the parsed representation of the expression; otherwise, null if <see cref="M:System.Web.Compilation.ExpressionBuilder.ParseExpression(System.String,System.Type,System.Web.Compilation.ExpressionBuilderContext)" /> is not implemented.</returns>
		/// <param name="expression">The value of the declarative expression.</param>
		/// <param name="propertyType">The type of the property bound to by the expression.</param>
		/// <param name="context">Contextual information for the evaluation of the expression.</param>
		// Token: 0x06004544 RID: 17732 RVA: 0x00003BF6 File Offset: 0x00001DF6
		public virtual object ParseExpression(string expression, Type propertyType, ExpressionBuilderContext context)
		{
			return null;
		}

		/// <summary>When overridden in a derived class, returns a value indicating whether the current <see cref="T:System.Web.Compilation.ExpressionBuilder" /> object supports no-compile pages. </summary>
		/// <returns>true if the <see cref="T:System.Web.Compilation.ExpressionBuilder" /> supports expression evaluation; otherwise, false.</returns>
		// Token: 0x170015AD RID: 5549
		// (get) Token: 0x06004545 RID: 17733 RVA: 0x00008A4D File Offset: 0x00006C4D
		public virtual bool SupportsEvaluate
		{
			get
			{
				return false;
			}
		}
	}
}
