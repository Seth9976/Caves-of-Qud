using System;

namespace System.CodeDom
{
	/// <summary>Represents a reference to a field.</summary>
	// Token: 0x02000313 RID: 787
	[Serializable]
	public class CodeFieldReferenceExpression : CodeExpression
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeFieldReferenceExpression" /> class.</summary>
		// Token: 0x060018F8 RID: 6392 RVA: 0x0005EE6C File Offset: 0x0005D06C
		public CodeFieldReferenceExpression()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeFieldReferenceExpression" /> class using the specified target object and field name.</summary>
		/// <param name="targetObject">A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the object that contains the field. </param>
		/// <param name="fieldName">The name of the field. </param>
		// Token: 0x060018F9 RID: 6393 RVA: 0x0005FC5C File Offset: 0x0005DE5C
		public CodeFieldReferenceExpression(CodeExpression targetObject, string fieldName)
		{
			this.TargetObject = targetObject;
			this.FieldName = fieldName;
		}

		/// <summary>Gets or sets the object that contains the field to reference.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the object that contains the field to reference.</returns>
		// Token: 0x170004EC RID: 1260
		// (get) Token: 0x060018FA RID: 6394 RVA: 0x0005FC72 File Offset: 0x0005DE72
		// (set) Token: 0x060018FB RID: 6395 RVA: 0x0005FC7A File Offset: 0x0005DE7A
		public CodeExpression TargetObject { get; set; }

		/// <summary>Gets or sets the name of the field to reference.</summary>
		/// <returns>A string containing the field name.</returns>
		// Token: 0x170004ED RID: 1261
		// (get) Token: 0x060018FC RID: 6396 RVA: 0x0005FC83 File Offset: 0x0005DE83
		// (set) Token: 0x060018FD RID: 6397 RVA: 0x0005FC94 File Offset: 0x0005DE94
		public string FieldName
		{
			get
			{
				return this._fieldName ?? string.Empty;
			}
			set
			{
				this._fieldName = value;
			}
		}

		// Token: 0x04000DAD RID: 3501
		private string _fieldName;
	}
}
