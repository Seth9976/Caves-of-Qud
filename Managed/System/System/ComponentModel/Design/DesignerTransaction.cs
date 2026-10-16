using System;

namespace System.ComponentModel.Design
{
	/// <summary>Provides a way to group a series of design-time actions to improve performance and enable most types of changes to be undone.</summary>
	// Token: 0x0200044B RID: 1099
	public abstract class DesignerTransaction : IDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.DesignerTransaction" /> class with no description.</summary>
		// Token: 0x060023B3 RID: 9139 RVA: 0x00081199 File Offset: 0x0007F399
		protected DesignerTransaction()
			: this("")
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.DesignerTransaction" /> class using the specified transaction description.</summary>
		/// <param name="description">A description for this transaction. </param>
		// Token: 0x060023B4 RID: 9140 RVA: 0x000811A6 File Offset: 0x0007F3A6
		protected DesignerTransaction(string description)
		{
			this.Description = description;
		}

		/// <summary>Gets a value indicating whether the transaction was canceled.</summary>
		/// <returns>true if the transaction was canceled; otherwise, false.</returns>
		// Token: 0x1700073A RID: 1850
		// (get) Token: 0x060023B5 RID: 9141 RVA: 0x000811B5 File Offset: 0x0007F3B5
		// (set) Token: 0x060023B6 RID: 9142 RVA: 0x000811BD File Offset: 0x0007F3BD
		public bool Canceled { get; private set; }

		/// <summary>Gets a value indicating whether the transaction was committed.</summary>
		/// <returns>true if the transaction was committed; otherwise, false.</returns>
		// Token: 0x1700073B RID: 1851
		// (get) Token: 0x060023B7 RID: 9143 RVA: 0x000811C6 File Offset: 0x0007F3C6
		// (set) Token: 0x060023B8 RID: 9144 RVA: 0x000811CE File Offset: 0x0007F3CE
		public bool Committed { get; private set; }

		/// <summary>Gets a description for the transaction.</summary>
		/// <returns>A description for the transaction.</returns>
		// Token: 0x1700073C RID: 1852
		// (get) Token: 0x060023B9 RID: 9145 RVA: 0x000811D7 File Offset: 0x0007F3D7
		public string Description { get; }

		/// <summary>Cancels the transaction and attempts to roll back the changes made by the events of the transaction.</summary>
		// Token: 0x060023BA RID: 9146 RVA: 0x000811DF File Offset: 0x0007F3DF
		public void Cancel()
		{
			if (!this.Canceled && !this.Committed)
			{
				this.Canceled = true;
				GC.SuppressFinalize(this);
				this._suppressedFinalization = true;
				this.OnCancel();
			}
		}

		/// <summary>Commits this transaction.</summary>
		// Token: 0x060023BB RID: 9147 RVA: 0x0008120B File Offset: 0x0007F40B
		public void Commit()
		{
			if (!this.Committed && !this.Canceled)
			{
				this.Committed = true;
				GC.SuppressFinalize(this);
				this._suppressedFinalization = true;
				this.OnCommit();
			}
		}

		/// <summary>Raises the Cancel event.</summary>
		// Token: 0x060023BC RID: 9148
		protected abstract void OnCancel();

		/// <summary>Performs the actual work of committing a transaction.</summary>
		// Token: 0x060023BD RID: 9149
		protected abstract void OnCommit();

		// Token: 0x060023BE RID: 9150 RVA: 0x00081238 File Offset: 0x0007F438
		~DesignerTransaction()
		{
			this.Dispose(false);
		}

		/// <summary>Releases all resources used by the <see cref="T:System.ComponentModel.Design.DesignerTransaction" />. </summary>
		// Token: 0x060023BF RID: 9151 RVA: 0x00081268 File Offset: 0x0007F468
		void IDisposable.Dispose()
		{
			this.Dispose(true);
			if (!this._suppressedFinalization)
			{
				GC.SuppressFinalize(this);
			}
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.ComponentModel.Design.DesignerTransaction" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x060023C0 RID: 9152 RVA: 0x0008127F File Offset: 0x0007F47F
		protected virtual void Dispose(bool disposing)
		{
			this.Cancel();
		}

		// Token: 0x040010DC RID: 4316
		private bool _suppressedFinalization;
	}
}
