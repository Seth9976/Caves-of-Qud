using System;
using System.Reflection;

namespace System.CodeDom
{
	/// <summary>Represents a type declaration for a class, structure, interface, or enumeration.</summary>
	// Token: 0x02000336 RID: 822
	[Serializable]
	public class CodeTypeDeclaration : CodeTypeMember
	{
		/// <summary>Occurs when the <see cref="P:System.CodeDom.CodeTypeDeclaration.BaseTypes" /> collection is accessed for the first time.</summary>
		// Token: 0x14000023 RID: 35
		// (add) Token: 0x060019F4 RID: 6644 RVA: 0x00060F78 File Offset: 0x0005F178
		// (remove) Token: 0x060019F5 RID: 6645 RVA: 0x00060FB0 File Offset: 0x0005F1B0
		public event EventHandler PopulateBaseTypes;

		/// <summary>Occurs when the <see cref="P:System.CodeDom.CodeTypeDeclaration.Members" /> collection is accessed for the first time.</summary>
		// Token: 0x14000024 RID: 36
		// (add) Token: 0x060019F6 RID: 6646 RVA: 0x00060FE8 File Offset: 0x0005F1E8
		// (remove) Token: 0x060019F7 RID: 6647 RVA: 0x00061020 File Offset: 0x0005F220
		public event EventHandler PopulateMembers;

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeTypeDeclaration" /> class.</summary>
		// Token: 0x060019F8 RID: 6648 RVA: 0x00061055 File Offset: 0x0005F255
		public CodeTypeDeclaration()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeTypeDeclaration" /> class with the specified name.</summary>
		/// <param name="name">The name for the new type. </param>
		// Token: 0x060019F9 RID: 6649 RVA: 0x0006107A File Offset: 0x0005F27A
		public CodeTypeDeclaration(string name)
		{
			base.Name = name;
		}

		/// <summary>Gets or sets the attributes of the type.</summary>
		/// <returns>A <see cref="T:System.Reflection.TypeAttributes" /> object that indicates the attributes of the type.</returns>
		// Token: 0x1700053A RID: 1338
		// (get) Token: 0x060019FA RID: 6650 RVA: 0x000610A6 File Offset: 0x0005F2A6
		// (set) Token: 0x060019FB RID: 6651 RVA: 0x000610AE File Offset: 0x0005F2AE
		public TypeAttributes TypeAttributes { get; set; } = TypeAttributes.Public;

		/// <summary>Gets the base types of the type.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeReferenceCollection" /> object that indicates the base types of the type.</returns>
		// Token: 0x1700053B RID: 1339
		// (get) Token: 0x060019FC RID: 6652 RVA: 0x000610B7 File Offset: 0x0005F2B7
		public CodeTypeReferenceCollection BaseTypes
		{
			get
			{
				if ((this._populated & 1) == 0)
				{
					this._populated |= 1;
					EventHandler populateBaseTypes = this.PopulateBaseTypes;
					if (populateBaseTypes != null)
					{
						populateBaseTypes(this, EventArgs.Empty);
					}
				}
				return this._baseTypes;
			}
		}

		/// <summary>Gets or sets a value indicating whether the type is a class or reference type.</summary>
		/// <returns>true if the type is a class or reference type; otherwise, false.</returns>
		// Token: 0x1700053C RID: 1340
		// (get) Token: 0x060019FD RID: 6653 RVA: 0x000610EE File Offset: 0x0005F2EE
		// (set) Token: 0x060019FE RID: 6654 RVA: 0x0006110E File Offset: 0x0005F30E
		public bool IsClass
		{
			get
			{
				return (this.TypeAttributes & TypeAttributes.ClassSemanticsMask) == TypeAttributes.NotPublic && !this._isEnum && !this._isStruct;
			}
			set
			{
				if (value)
				{
					this.TypeAttributes &= ~TypeAttributes.ClassSemanticsMask;
					this.TypeAttributes |= TypeAttributes.NotPublic;
					this._isStruct = false;
					this._isEnum = false;
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the type is a value type (struct).</summary>
		/// <returns>true if the type is a value type; otherwise, false.</returns>
		// Token: 0x1700053D RID: 1341
		// (get) Token: 0x060019FF RID: 6655 RVA: 0x0006113E File Offset: 0x0005F33E
		// (set) Token: 0x06001A00 RID: 6656 RVA: 0x00061146 File Offset: 0x0005F346
		public bool IsStruct
		{
			get
			{
				return this._isStruct;
			}
			set
			{
				if (value)
				{
					this.TypeAttributes &= ~TypeAttributes.ClassSemanticsMask;
					this._isEnum = false;
				}
				this._isStruct = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the type is an enumeration.</summary>
		/// <returns>true if the type is an enumeration; otherwise, false.</returns>
		// Token: 0x1700053E RID: 1342
		// (get) Token: 0x06001A01 RID: 6657 RVA: 0x00061168 File Offset: 0x0005F368
		// (set) Token: 0x06001A02 RID: 6658 RVA: 0x00061170 File Offset: 0x0005F370
		public bool IsEnum
		{
			get
			{
				return this._isEnum;
			}
			set
			{
				if (value)
				{
					this.TypeAttributes &= ~TypeAttributes.ClassSemanticsMask;
					this._isStruct = false;
				}
				this._isEnum = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the type is an interface.</summary>
		/// <returns>true if the type is an interface; otherwise, false.</returns>
		// Token: 0x1700053F RID: 1343
		// (get) Token: 0x06001A03 RID: 6659 RVA: 0x00061192 File Offset: 0x0005F392
		// (set) Token: 0x06001A04 RID: 6660 RVA: 0x000611A4 File Offset: 0x0005F3A4
		public bool IsInterface
		{
			get
			{
				return (this.TypeAttributes & TypeAttributes.ClassSemanticsMask) == TypeAttributes.ClassSemanticsMask;
			}
			set
			{
				if (value)
				{
					this.TypeAttributes &= ~TypeAttributes.ClassSemanticsMask;
					this.TypeAttributes |= TypeAttributes.ClassSemanticsMask;
					this._isStruct = false;
					this._isEnum = false;
					return;
				}
				this.TypeAttributes &= ~TypeAttributes.ClassSemanticsMask;
			}
		}

		/// <summary>Gets or sets a value indicating whether the type declaration is complete or partial.</summary>
		/// <returns>true if the class or structure declaration is a partial representation of the implementation; false if the declaration is a complete implementation of the class or structure. The default is false.</returns>
		// Token: 0x17000540 RID: 1344
		// (get) Token: 0x06001A05 RID: 6661 RVA: 0x000611F0 File Offset: 0x0005F3F0
		// (set) Token: 0x06001A06 RID: 6662 RVA: 0x000611F8 File Offset: 0x0005F3F8
		public bool IsPartial { get; set; }

		/// <summary>Gets the collection of class members for the represented type.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeMemberCollection" /> object that indicates the class members.</returns>
		// Token: 0x17000541 RID: 1345
		// (get) Token: 0x06001A07 RID: 6663 RVA: 0x00061201 File Offset: 0x0005F401
		public CodeTypeMemberCollection Members
		{
			get
			{
				if ((this._populated & 2) == 0)
				{
					this._populated |= 2;
					EventHandler populateMembers = this.PopulateMembers;
					if (populateMembers != null)
					{
						populateMembers(this, EventArgs.Empty);
					}
				}
				return this._members;
			}
		}

		/// <summary>Gets the type parameters for the type declaration.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeParameterCollection" /> that contains the type parameters for the type declaration.</returns>
		// Token: 0x17000542 RID: 1346
		// (get) Token: 0x06001A08 RID: 6664 RVA: 0x00061238 File Offset: 0x0005F438
		public CodeTypeParameterCollection TypeParameters
		{
			get
			{
				CodeTypeParameterCollection codeTypeParameterCollection;
				if ((codeTypeParameterCollection = this._typeParameters) == null)
				{
					codeTypeParameterCollection = (this._typeParameters = new CodeTypeParameterCollection());
				}
				return codeTypeParameterCollection;
			}
		}

		// Token: 0x04000E04 RID: 3588
		private readonly CodeTypeReferenceCollection _baseTypes = new CodeTypeReferenceCollection();

		// Token: 0x04000E05 RID: 3589
		private readonly CodeTypeMemberCollection _members = new CodeTypeMemberCollection();

		// Token: 0x04000E06 RID: 3590
		private bool _isEnum;

		// Token: 0x04000E07 RID: 3591
		private bool _isStruct;

		// Token: 0x04000E08 RID: 3592
		private int _populated;

		// Token: 0x04000E09 RID: 3593
		private const int BaseTypesCollection = 1;

		// Token: 0x04000E0A RID: 3594
		private const int MembersCollection = 2;

		// Token: 0x04000E0B RID: 3595
		private CodeTypeParameterCollection _typeParameters;
	}
}
