using System;

namespace System.CodeDom
{
	/// <summary>Represents a declaration for an event of a type.</summary>
	// Token: 0x02000319 RID: 793
	[Serializable]
	public class CodeMemberEvent : CodeTypeMember
	{
		/// <summary>Gets or sets the data type of the delegate type that handles the event.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeReference" /> that indicates the delegate type that handles the event.</returns>
		// Token: 0x170004F9 RID: 1273
		// (get) Token: 0x0600191E RID: 6430 RVA: 0x0005FE40 File Offset: 0x0005E040
		// (set) Token: 0x0600191F RID: 6431 RVA: 0x0005FE6A File Offset: 0x0005E06A
		public CodeTypeReference Type
		{
			get
			{
				CodeTypeReference codeTypeReference;
				if ((codeTypeReference = this._type) == null)
				{
					codeTypeReference = (this._type = new CodeTypeReference(""));
				}
				return codeTypeReference;
			}
			set
			{
				this._type = value;
			}
		}

		/// <summary>Gets or sets the privately implemented data type, if any.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeReference" /> that indicates the data type that the event privately implements.</returns>
		// Token: 0x170004FA RID: 1274
		// (get) Token: 0x06001920 RID: 6432 RVA: 0x0005FE73 File Offset: 0x0005E073
		// (set) Token: 0x06001921 RID: 6433 RVA: 0x0005FE7B File Offset: 0x0005E07B
		public CodeTypeReference PrivateImplementationType { get; set; }

		/// <summary>Gets or sets the data type that the member event implements.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeReferenceCollection" /> that indicates the data type or types that the member event implements.</returns>
		// Token: 0x170004FB RID: 1275
		// (get) Token: 0x06001922 RID: 6434 RVA: 0x0005FE84 File Offset: 0x0005E084
		public CodeTypeReferenceCollection ImplementationTypes
		{
			get
			{
				CodeTypeReferenceCollection codeTypeReferenceCollection;
				if ((codeTypeReferenceCollection = this._implementationTypes) == null)
				{
					codeTypeReferenceCollection = (this._implementationTypes = new CodeTypeReferenceCollection());
				}
				return codeTypeReferenceCollection;
			}
		}

		// Token: 0x04000DBA RID: 3514
		private CodeTypeReference _type;

		// Token: 0x04000DBB RID: 3515
		private CodeTypeReferenceCollection _implementationTypes;
	}
}
