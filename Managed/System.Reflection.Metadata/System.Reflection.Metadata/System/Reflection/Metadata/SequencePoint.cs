using System;
using System.Diagnostics;
using System.Reflection.Internal;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata
{
	/// <summary>Represents a Portable PDB sequence point.</summary>
	// Token: 0x020000CE RID: 206
	[DebuggerDisplay("{GetDebuggerDisplay(),nq}")]
	public readonly struct SequencePoint : IEquatable<SequencePoint>
	{
		/// <summary>Gets the source document that contains this sequence point.</summary>
		/// <returns>The source document that contains this sequence point.</returns>
		// Token: 0x17000291 RID: 657
		// (get) Token: 0x0600079E RID: 1950 RVA: 0x00012474 File Offset: 0x00010674
		public DocumentHandle Document { get; }

		/// <summary>Gets the IL offset of this sequence point from the start of the method body, in bytes.</summary>
		/// <returns>The IL offset of this sequence point from the start of the method body, in bytes.</returns>
		// Token: 0x17000292 RID: 658
		// (get) Token: 0x0600079F RID: 1951 RVA: 0x0001247C File Offset: 0x0001067C
		public int Offset { get; }

		/// <summary>Gets the line number of the first character in this sequence point.</summary>
		/// <returns>The line number of the first character in this sequence point.</returns>
		// Token: 0x17000293 RID: 659
		// (get) Token: 0x060007A0 RID: 1952 RVA: 0x00012484 File Offset: 0x00010684
		public int StartLine { get; }

		/// <summary>Gets the line number of the last character in this sequence point.</summary>
		/// <returns>The line number of the last character in this sequence point.</returns>
		// Token: 0x17000294 RID: 660
		// (get) Token: 0x060007A1 RID: 1953 RVA: 0x0001248C File Offset: 0x0001068C
		public int EndLine { get; }

		/// <summary>Gets the column number of the first character in this sequence point.</summary>
		/// <returns>The column number of the first character in this sequence point.</returns>
		// Token: 0x17000295 RID: 661
		// (get) Token: 0x060007A2 RID: 1954 RVA: 0x00012494 File Offset: 0x00010694
		public int StartColumn { get; }

		/// <summary>Gets the column number of the last character in this sequence point.</summary>
		/// <returns>The column number of the last character in this sequence point.</returns>
		// Token: 0x17000296 RID: 662
		// (get) Token: 0x060007A3 RID: 1955 RVA: 0x0001249C File Offset: 0x0001069C
		public int EndColumn { get; }

		// Token: 0x060007A4 RID: 1956 RVA: 0x000124A4 File Offset: 0x000106A4
		internal SequencePoint(DocumentHandle document, int offset)
		{
			this.Document = document;
			this.Offset = offset;
			this.StartLine = 16707566;
			this.StartColumn = 0;
			this.EndLine = 16707566;
			this.EndColumn = 0;
		}

		// Token: 0x060007A5 RID: 1957 RVA: 0x000124D8 File Offset: 0x000106D8
		internal SequencePoint(DocumentHandle document, int offset, int startLine, ushort startColumn, int endLine, ushort endColumn)
		{
			this.Document = document;
			this.Offset = offset;
			this.StartLine = startLine;
			this.StartColumn = (int)startColumn;
			this.EndLine = endLine;
			this.EndColumn = (int)endColumn;
		}

		/// <summary>Gets the hash code of this sequence point.</summary>
		/// <returns>The hash code of this sequence point.</returns>
		// Token: 0x060007A6 RID: 1958 RVA: 0x00012508 File Offset: 0x00010708
		public override int GetHashCode()
		{
			return Hash.Combine(this.Document.RowId, Hash.Combine(this.Offset, Hash.Combine(this.StartLine, Hash.Combine(this.StartColumn, Hash.Combine(this.EndLine, this.EndColumn)))));
		}

		/// <summary>Indicates whether the current sequence point is equal to the specified object.</summary>
		/// <param name="obj">The object to compare with the current object.</param>
		/// <returns>
		///   <see langword="true" /> if the current sequence point is equal to the <paramref name="obj" /> parameter; otherwise, <see langword="false" />.</returns>
		// Token: 0x060007A7 RID: 1959 RVA: 0x0001255A File Offset: 0x0001075A
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			return obj is SequencePoint && this.Equals((SequencePoint)obj);
		}

		/// <summary>Indicates whether the current object is equal to another object of the same type.</summary>
		/// <param name="other">An object to compare with this object.</param>
		/// <returns>
		///   <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.</returns>
		// Token: 0x060007A8 RID: 1960 RVA: 0x00012574 File Offset: 0x00010774
		public bool Equals(SequencePoint other)
		{
			return this.Document == other.Document && this.Offset == other.Offset && this.StartLine == other.StartLine && this.StartColumn == other.StartColumn && this.EndLine == other.EndLine && this.EndColumn == other.EndColumn;
		}

		/// <summary>Gets a value that indicates whether this sequence point is hidden.</summary>
		/// <returns>
		///   <see langword="true" /> if this sequence point is hidden; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000297 RID: 663
		// (get) Token: 0x060007A9 RID: 1961 RVA: 0x000125E2 File Offset: 0x000107E2
		public bool IsHidden
		{
			get
			{
				return this.StartLine == 16707566;
			}
		}

		// Token: 0x060007AA RID: 1962 RVA: 0x000125F4 File Offset: 0x000107F4
		private string GetDebuggerDisplay()
		{
			if (!this.IsHidden)
			{
				return string.Format("{0}: ({1}, {2}) - ({3}, {4})", new object[] { this.Offset, this.StartLine, this.StartColumn, this.EndLine, this.EndColumn });
			}
			return "<hidden>";
		}

		/// <summary>Specifies a line number value for a hidden sequence point.</summary>
		// Token: 0x0400048A RID: 1162
		public const int HiddenLine = 16707566;
	}
}
