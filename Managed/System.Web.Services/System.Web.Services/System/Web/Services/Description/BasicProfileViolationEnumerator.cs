using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Web.Services.Description
{
	/// <summary>Enumerates the elements in a <see cref="T:System.Web.Services.Description.BasicProfileViolationCollection" />.</summary>
	// Token: 0x0200013C RID: 316
	public class BasicProfileViolationEnumerator : IEnumerator<BasicProfileViolation>, IDisposable, IEnumerator
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Description.BasicProfileViolationEnumerator" /> class.</summary>
		/// <param name="list">The <see cref="T:System.Web.Services.Description.BasicProfileViolationCollection" /> to be enumerated using this class.</param>
		// Token: 0x060009AB RID: 2475 RVA: 0x00043503 File Offset: 0x00041703
		public BasicProfileViolationEnumerator(BasicProfileViolationCollection list)
		{
			this.list = list;
			this.idx = -1;
			this.end = list.Count - 1;
		}

		/// <summary>Releases all resources used by the current instance of the <see cref="T:System.Web.Services.Description.BasicProfileViolationEnumerator" /> class.Releases all resources used by the <see cref="T:System.Web.Services.Description.BasicProfileViolationEnumerator" />. </summary>
		// Token: 0x060009AC RID: 2476 RVA: 0x00002B13 File Offset: 0x00000D13
		public void Dispose()
		{
		}

		/// <summary>Enumerates to the next element in the <see cref="T:System.Web.Services.Description.BasicProfileViolationCollection" />.</summary>
		/// <returns>false if the end of the collection is reached; otherwise true.</returns>
		// Token: 0x060009AD RID: 2477 RVA: 0x00043527 File Offset: 0x00041727
		public bool MoveNext()
		{
			if (this.idx >= this.end)
			{
				return false;
			}
			this.idx++;
			return true;
		}

		/// <summary>Gets the current <see cref="T:System.Web.Services.Description.BasicProfileViolation" /> element in the <see cref="T:System.Web.Services.Description.BasicProfileViolationCollection" />.</summary>
		/// <returns>A <see cref="T:System.Web.Services.Description.BasicProfileViolation" /> object representing the current element in the <see cref="T:System.Web.Services.Description.BasicProfileViolationCollection" />.</returns>
		// Token: 0x17000277 RID: 631
		// (get) Token: 0x060009AE RID: 2478 RVA: 0x00043548 File Offset: 0x00041748
		public BasicProfileViolation Current
		{
			get
			{
				return this.list[this.idx];
			}
		}

		/// <summary>Gets the current element in the <see cref="T:System.Web.Services.Description.BasicProfileViolationCollection" />. </summary>
		/// <returns>The current element in the collection.</returns>
		// Token: 0x17000278 RID: 632
		// (get) Token: 0x060009AF RID: 2479 RVA: 0x00043548 File Offset: 0x00041748
		object IEnumerator.Current
		{
			get
			{
				return this.list[this.idx];
			}
		}

		/// <summary>Sets the enumerator to its initial position, which is before the first element in the <see cref="T:System.Web.Services.Description.BasicProfileViolationCollection" />.</summary>
		// Token: 0x060009B0 RID: 2480 RVA: 0x0004355B File Offset: 0x0004175B
		void IEnumerator.Reset()
		{
			this.idx = -1;
		}

		// Token: 0x04000595 RID: 1429
		private BasicProfileViolationCollection list;

		// Token: 0x04000596 RID: 1430
		private int idx;

		// Token: 0x04000597 RID: 1431
		private int end;
	}
}
