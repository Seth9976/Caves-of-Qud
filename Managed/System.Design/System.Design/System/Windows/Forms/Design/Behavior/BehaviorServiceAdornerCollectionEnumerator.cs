using System;
using System.Collections;

namespace System.Windows.Forms.Design.Behavior
{
	/// <summary>Supports iteration over a <see cref="T:System.Windows.Forms.Design.Behavior.BehaviorServiceAdornerCollection" />. </summary>
	// Token: 0x02000047 RID: 71
	public class BehaviorServiceAdornerCollectionEnumerator : IEnumerator
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Design.Behavior.BehaviorServiceAdornerCollectionEnumerator" /> class. </summary>
		/// <param name="mappings">The <see cref="T:System.Windows.Forms.Design.Behavior.BehaviorServiceAdornerCollection" /> for which to create the enumerator. </param>
		// Token: 0x0600026C RID: 620 RVA: 0x00008B4F File Offset: 0x00006D4F
		public BehaviorServiceAdornerCollectionEnumerator(BehaviorServiceAdornerCollection mappings)
		{
			if (mappings == null)
			{
				throw new ArgumentNullException("mappings");
			}
			this.mappings = mappings;
			this.Reset();
		}

		/// <summary>Gets the current element in the <see cref="T:System.Windows.Forms.Design.Behavior.BehaviorServiceAdornerCollection" />.</summary>
		/// <returns>The current element in the <see cref="T:System.Windows.Forms.Design.Behavior.BehaviorServiceAdornerCollection" />.</returns>
		// Token: 0x17000078 RID: 120
		// (get) Token: 0x0600026D RID: 621 RVA: 0x00008B72 File Offset: 0x00006D72
		public Adorner Current
		{
			get
			{
				if (this.index >= 0)
				{
					return this.mappings[this.index];
				}
				return null;
			}
		}

		// Token: 0x0600026E RID: 622 RVA: 0x00008B90 File Offset: 0x00006D90
		private void CheckState()
		{
			if (this.mappings.State != this.state)
			{
				throw new InvalidOperationException("Collection has changed");
			}
		}

		/// <summary>Advances the enumerator to the next element of the <see cref="T:System.Windows.Forms.Design.Behavior.BehaviorServiceAdornerCollection" />.</summary>
		/// <returns>true if the enumerator was successfully advanced to the next element; false if the enumerator was past the end of the collection.</returns>
		// Token: 0x0600026F RID: 623 RVA: 0x00008BB0 File Offset: 0x00006DB0
		public bool MoveNext()
		{
			this.CheckState();
			int num = this.index;
			this.index = num + 1;
			if (num < this.mappings.Count)
			{
				return true;
			}
			this.index--;
			return false;
		}

		/// <summary>Sets the enumerator to its initial position, which is before the first element in the collection.</summary>
		// Token: 0x06000270 RID: 624 RVA: 0x00008BF2 File Offset: 0x00006DF2
		public void Reset()
		{
			this.index = -1;
		}

		/// <summary>For a description of this member, see the <see cref="P:System.Collections.IEnumerator.Current" /> property.</summary>
		/// <returns>The current <see cref="T:System.Windows.Forms.Design.Behavior.Adorner" /> in the collection.</returns>
		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000271 RID: 625 RVA: 0x00008BFB File Offset: 0x00006DFB
		object IEnumerator.Current
		{
			get
			{
				return this.Current;
			}
		}

		/// <summary>For a description of this member, see the <see cref="M:System.Collections.IEnumerator.MoveNext" /> method.</summary>
		/// <returns>true if the enumerator was successfully advanced to the next element; false if the enumerator was past the end of the collection.</returns>
		// Token: 0x06000272 RID: 626 RVA: 0x00008C03 File Offset: 0x00006E03
		bool IEnumerator.MoveNext()
		{
			return this.MoveNext();
		}

		/// <summary>For a description of this member, see the <see cref="M:System.Collections.IEnumerator.Reset" /> method.</summary>
		// Token: 0x06000273 RID: 627 RVA: 0x00008C0B File Offset: 0x00006E0B
		void IEnumerator.Reset()
		{
			this.Reset();
		}

		// Token: 0x040000FB RID: 251
		private BehaviorServiceAdornerCollection mappings;

		// Token: 0x040000FC RID: 252
		private int index;

		// Token: 0x040000FD RID: 253
		private int state;
	}
}
