using System;

namespace System.Web.Compilation
{
	/// <summary>Specifies the prefix attribute to use for the expression builder. This class cannot be inherited. </summary>
	// Token: 0x02000600 RID: 1536
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
	public sealed class ExpressionPrefixAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Compilation.ExpressionPrefixAttribute" /> class.</summary>
		/// <param name="expressionPrefix">The prefix of the current <see cref="T:System.Web.Compilation.ExpressionBuilder" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="expressionPrefix" /> is null or an empty string ("").</exception>
		// Token: 0x0600426C RID: 17004 RVA: 0x000AD1B9 File Offset: 0x000AB3B9
		public ExpressionPrefixAttribute(string expressionPrefix)
		{
			if (string.IsNullOrEmpty(expressionPrefix))
			{
				throw new ArgumentNullException("expressionPrefix");
			}
			this._expressionPrefix = expressionPrefix;
		}

		/// <summary>Gets the prefix value for the current <see cref="T:System.Web.Compilation.ExpressionBuilder" /> object.</summary>
		/// <returns>The expression prefix for the configured <see cref="T:System.Web.Compilation.ExpressionBuilder" />.</returns>
		// Token: 0x17001523 RID: 5411
		// (get) Token: 0x0600426D RID: 17005 RVA: 0x000AD1DB File Offset: 0x000AB3DB
		public string ExpressionPrefix
		{
			get
			{
				return this._expressionPrefix;
			}
		}

		// Token: 0x040023A7 RID: 9127
		private string _expressionPrefix;
	}
}
