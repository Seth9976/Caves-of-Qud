using System;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata
{
	/// <summary>Provides debug information associated with a method definition. This information is stored in debug metadata.</summary>
	// Token: 0x020000CD RID: 205
	public readonly struct MethodDebugInformation
	{
		// Token: 0x06000797 RID: 1943 RVA: 0x000123A1 File Offset: 0x000105A1
		[NullableContext(1)]
		internal MethodDebugInformation(MetadataReader reader, MethodDebugInformationHandle handle)
		{
			this._reader = reader;
			this._rowId = handle.RowId;
		}

		// Token: 0x1700028D RID: 653
		// (get) Token: 0x06000798 RID: 1944 RVA: 0x000123B7 File Offset: 0x000105B7
		private MethodDebugInformationHandle Handle
		{
			get
			{
				return MethodDebugInformationHandle.FromRowId(this._rowId);
			}
		}

		/// <summary>Returns a blob encoding sequence points.</summary>
		/// <returns>A blob encoding sequence points, or a handle whose <see cref="P:System.Reflection.Metadata.BlobHandle.IsNil" /> property is <see langword="true" /> if the method doesn't have sequence points.</returns>
		// Token: 0x1700028E RID: 654
		// (get) Token: 0x06000799 RID: 1945 RVA: 0x000123C4 File Offset: 0x000105C4
		public BlobHandle SequencePointsBlob
		{
			get
			{
				return this._reader.MethodDebugInformationTable.GetSequencePoints(this.Handle);
			}
		}

		/// <summary>Gets the handle of the single document containing all sequence points of the method.</summary>
		/// <returns>The handle of the single document containing all sequence points of the method, or a handle whose <see cref="P:System.Reflection.Metadata.DocumentHandle.IsNil" /> property is <see langword="true" /> if the method doesn't have sequence points or spans multiple documents.</returns>
		// Token: 0x1700028F RID: 655
		// (get) Token: 0x0600079A RID: 1946 RVA: 0x000123DC File Offset: 0x000105DC
		public DocumentHandle Document
		{
			get
			{
				return this._reader.MethodDebugInformationTable.GetDocument(this.Handle);
			}
		}

		/// <summary>Returns a local signature handle.</summary>
		/// <returns>A local signature handle, or a handle whose <see cref="P:System.Reflection.Metadata.StandaloneSignatureHandle.IsNil" /> property is <see langword="true" /> if the method doesn't define any local variables.</returns>
		// Token: 0x17000290 RID: 656
		// (get) Token: 0x0600079B RID: 1947 RVA: 0x000123F4 File Offset: 0x000105F4
		public StandaloneSignatureHandle LocalSignature
		{
			get
			{
				if (this.SequencePointsBlob.IsNil)
				{
					return default(StandaloneSignatureHandle);
				}
				return StandaloneSignatureHandle.FromRowId(this._reader.GetBlobReader(this.SequencePointsBlob).ReadCompressedInteger());
			}
		}

		/// <summary>Returns a collection of sequence points decoded from <see cref="P:System.Reflection.Metadata.MethodDebugInformation.SequencePointsBlob" />.</summary>
		/// <returns>A collection of sequence points.</returns>
		// Token: 0x0600079C RID: 1948 RVA: 0x00012439 File Offset: 0x00010639
		public SequencePointCollection GetSequencePoints()
		{
			return new SequencePointCollection(this._reader.BlobHeap.GetMemoryBlock(this.SequencePointsBlob), this.Document);
		}

		/// <summary>Returns the kickoff method of the state machine.</summary>
		/// <returns>The kickoff method of the state machine, if the method is a <c>MoveNext</c> method of a state machine. Otherwise, it returns a handle whose <see cref="P:System.Reflection.Metadata.MethodDefinitionHandle.IsNil" /> property is <see langword="true" />.</returns>
		// Token: 0x0600079D RID: 1949 RVA: 0x0001245C File Offset: 0x0001065C
		public MethodDefinitionHandle GetStateMachineKickoffMethod()
		{
			return this._reader.StateMachineMethodTable.FindKickoffMethod(this._rowId);
		}

		// Token: 0x04000488 RID: 1160
		private readonly MetadataReader _reader;

		// Token: 0x04000489 RID: 1161
		private readonly int _rowId;
	}
}
