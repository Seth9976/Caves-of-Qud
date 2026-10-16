using System;

namespace System.ComponentModel.Design.Serialization
{
	/// <summary>Provides a location into which statements can be serialized. This class cannot be inherited.</summary>
	// Token: 0x02000160 RID: 352
	public sealed class StatementContext
	{
		/// <summary>Gets a collection of statements offered by the statement context.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.Design.Serialization.ObjectStatementCollection" /> containing statements offered by the statement context. </returns>
		// Token: 0x1700022A RID: 554
		// (get) Token: 0x06000A9D RID: 2717 RVA: 0x000162F5 File Offset: 0x000144F5
		public ObjectStatementCollection StatementCollection
		{
			get
			{
				if (this._statements == null)
				{
					this._statements = new ObjectStatementCollection();
				}
				return this._statements;
			}
		}

		// Token: 0x04000278 RID: 632
		private ObjectStatementCollection _statements;
	}
}
