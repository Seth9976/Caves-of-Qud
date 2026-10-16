using System;
using System.CodeDom;
using System.Collections;

namespace System.ComponentModel.Design.Serialization
{
	/// <summary>Holds a table of statements that is offered by the <see cref="T:System.ComponentModel.Design.Serialization.StatementContext" />. This class cannot be inherited.</summary>
	// Token: 0x02000159 RID: 345
	public sealed class ObjectStatementCollection : IEnumerable
	{
		// Token: 0x06000A7A RID: 2682 RVA: 0x000157CE File Offset: 0x000139CE
		internal ObjectStatementCollection()
		{
			this._statements = new Hashtable();
		}

		/// <summary>Determines whether the table contains the given statement owner.</summary>
		/// <returns>true if <paramref name="statementOwner" /> is in the table; otherwise, false.</returns>
		/// <param name="statementOwner">The owner of the statement collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="statementOwner" /> is null.</exception>
		// Token: 0x06000A7B RID: 2683 RVA: 0x000157E1 File Offset: 0x000139E1
		public bool ContainsKey(object statementOwner)
		{
			return this._statements.ContainsKey(statementOwner);
		}

		/// <summary>Returns an <see cref="T:System.Collections.IEnumerator" /> for the <see cref="T:System.ComponentModel.Design.Serialization.ObjectStatementCollection" />.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> for the <see cref="T:System.ComponentModel.Design.Serialization.ObjectStatementCollection" />.</returns>
		// Token: 0x06000A7C RID: 2684 RVA: 0x000157EF File Offset: 0x000139EF
		public IDictionaryEnumerator GetEnumerator()
		{
			return this._statements.GetEnumerator();
		}

		/// <summary>Gets the statement collection for the given owner.</summary>
		/// <returns>The statement collection for <paramref name="statementOwner" />, or null if <paramref name="statementOwner" /> is not in the table.</returns>
		/// <param name="statementOwner">The owner of the statement collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="statementOwner" /> is null.</exception>
		// Token: 0x17000226 RID: 550
		public CodeStatementCollection this[object statementOwner]
		{
			get
			{
				return this._statements[statementOwner] as CodeStatementCollection;
			}
		}

		/// <summary>For a description of this member, see the <see cref="M:System.Collections.IEnumerable.GetEnumerator" /> method.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate through the collection.</returns>
		// Token: 0x06000A7E RID: 2686 RVA: 0x0001580F File Offset: 0x00013A0F
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <summary>Populates the statement table with a statement owner.</summary>
		/// <param name="owner">The statement owner to add to the table.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="owner" /> is null.</exception>
		// Token: 0x06000A7F RID: 2687 RVA: 0x00015817 File Offset: 0x00013A17
		public void Populate(object owner)
		{
			if (this._statements[owner] == null)
			{
				this._statements[owner] = null;
			}
		}

		/// <summary>Populates the statement table with a collection of statement owners.</summary>
		/// <param name="statementOwners">A collection of statement owners to add to the table.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="statementOwner" /> is null.</exception>
		// Token: 0x06000A80 RID: 2688 RVA: 0x00015834 File Offset: 0x00013A34
		public void Populate(ICollection statementOwners)
		{
			foreach (object obj in statementOwners)
			{
				this.Populate(obj);
			}
		}

		// Token: 0x0400026C RID: 620
		private Hashtable _statements;
	}
}
