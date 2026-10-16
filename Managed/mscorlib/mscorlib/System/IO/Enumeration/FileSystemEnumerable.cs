using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace System.IO.Enumeration
{
	// Token: 0x02000B7A RID: 2938
	public class FileSystemEnumerable<TResult> : IEnumerable<TResult>, IEnumerable
	{
		// Token: 0x06006B0F RID: 27407 RVA: 0x0016E2D0 File Offset: 0x0016C4D0
		public FileSystemEnumerable(string directory, FileSystemEnumerable<TResult>.FindTransform transform, EnumerationOptions options = null)
		{
			if (directory == null)
			{
				throw new ArgumentNullException("directory");
			}
			this._directory = directory;
			if (transform == null)
			{
				throw new ArgumentNullException("transform");
			}
			this._transform = transform;
			this._options = options ?? EnumerationOptions.Default;
			this._enumerator = new FileSystemEnumerable<TResult>.DelegateEnumerator(this);
		}

		// Token: 0x17001277 RID: 4727
		// (get) Token: 0x06006B10 RID: 27408 RVA: 0x0016E32B File Offset: 0x0016C52B
		// (set) Token: 0x06006B11 RID: 27409 RVA: 0x0016E333 File Offset: 0x0016C533
		public FileSystemEnumerable<TResult>.FindPredicate ShouldIncludePredicate { get; set; }

		// Token: 0x17001278 RID: 4728
		// (get) Token: 0x06006B12 RID: 27410 RVA: 0x0016E33C File Offset: 0x0016C53C
		// (set) Token: 0x06006B13 RID: 27411 RVA: 0x0016E344 File Offset: 0x0016C544
		public FileSystemEnumerable<TResult>.FindPredicate ShouldRecursePredicate { get; set; }

		// Token: 0x06006B14 RID: 27412 RVA: 0x0016E34D File Offset: 0x0016C54D
		public IEnumerator<TResult> GetEnumerator()
		{
			return Interlocked.Exchange<FileSystemEnumerable<TResult>.DelegateEnumerator>(ref this._enumerator, null) ?? new FileSystemEnumerable<TResult>.DelegateEnumerator(this);
		}

		// Token: 0x06006B15 RID: 27413 RVA: 0x0016E365 File Offset: 0x0016C565
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x04003DAC RID: 15788
		private FileSystemEnumerable<TResult>.DelegateEnumerator _enumerator;

		// Token: 0x04003DAD RID: 15789
		private readonly FileSystemEnumerable<TResult>.FindTransform _transform;

		// Token: 0x04003DAE RID: 15790
		private readonly EnumerationOptions _options;

		// Token: 0x04003DAF RID: 15791
		private readonly string _directory;

		// Token: 0x02000B7B RID: 2939
		// (Invoke) Token: 0x06006B17 RID: 27415
		public delegate bool FindPredicate(ref FileSystemEntry entry);

		// Token: 0x02000B7C RID: 2940
		// (Invoke) Token: 0x06006B1B RID: 27419
		public delegate TResult FindTransform(ref FileSystemEntry entry);

		// Token: 0x02000B7D RID: 2941
		private sealed class DelegateEnumerator : FileSystemEnumerator<TResult>
		{
			// Token: 0x06006B1E RID: 27422 RVA: 0x0016E36D File Offset: 0x0016C56D
			public DelegateEnumerator(FileSystemEnumerable<TResult> enumerable)
				: base(enumerable._directory, enumerable._options)
			{
				this._enumerable = enumerable;
			}

			// Token: 0x06006B1F RID: 27423 RVA: 0x0016E388 File Offset: 0x0016C588
			protected override TResult TransformEntry(ref FileSystemEntry entry)
			{
				return this._enumerable._transform(ref entry);
			}

			// Token: 0x06006B20 RID: 27424 RVA: 0x0016E39B File Offset: 0x0016C59B
			protected override bool ShouldRecurseIntoEntry(ref FileSystemEntry entry)
			{
				FileSystemEnumerable<TResult>.FindPredicate shouldRecursePredicate = this._enumerable.ShouldRecursePredicate;
				return shouldRecursePredicate == null || shouldRecursePredicate(ref entry);
			}

			// Token: 0x06006B21 RID: 27425 RVA: 0x0016E3B4 File Offset: 0x0016C5B4
			protected override bool ShouldIncludeEntry(ref FileSystemEntry entry)
			{
				FileSystemEnumerable<TResult>.FindPredicate shouldIncludePredicate = this._enumerable.ShouldIncludePredicate;
				return shouldIncludePredicate == null || shouldIncludePredicate(ref entry);
			}

			// Token: 0x04003DB2 RID: 15794
			private readonly FileSystemEnumerable<TResult> _enumerable;
		}
	}
}
