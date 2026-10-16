using System;

namespace System.CodeDom
{
	/// <summary>Represents a statement that removes an event handler.</summary>
	// Token: 0x0200032B RID: 811
	[Serializable]
	public class CodeRemoveEventStatement : CodeStatement
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeRemoveEventStatement" /> class.</summary>
		// Token: 0x060019BC RID: 6588 RVA: 0x0005F0B9 File Offset: 0x0005D2B9
		public CodeRemoveEventStatement()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeRemoveEventStatement" /> class with the specified event and event handler.</summary>
		/// <param name="eventRef">A <see cref="T:System.CodeDom.CodeEventReferenceExpression" /> that indicates the event to detach the event handler from. </param>
		/// <param name="listener">A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the event handler to remove. </param>
		// Token: 0x060019BD RID: 6589 RVA: 0x00060C1E File Offset: 0x0005EE1E
		public CodeRemoveEventStatement(CodeEventReferenceExpression eventRef, CodeExpression listener)
		{
			this._eventRef = eventRef;
			this.Listener = listener;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeRemoveEventStatement" /> class using the specified target object, event name, and event handler.</summary>
		/// <param name="targetObject">A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the object that contains the event. </param>
		/// <param name="eventName">The name of the event. </param>
		/// <param name="listener">A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the event handler to remove. </param>
		// Token: 0x060019BE RID: 6590 RVA: 0x00060C34 File Offset: 0x0005EE34
		public CodeRemoveEventStatement(CodeExpression targetObject, string eventName, CodeExpression listener)
		{
			this._eventRef = new CodeEventReferenceExpression(targetObject, eventName);
			this.Listener = listener;
		}

		/// <summary>Gets or sets the event to remove a listener from.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeEventReferenceExpression" /> that indicates the event to remove a listener from.</returns>
		// Token: 0x1700052B RID: 1323
		// (get) Token: 0x060019BF RID: 6591 RVA: 0x00060C50 File Offset: 0x0005EE50
		// (set) Token: 0x060019C0 RID: 6592 RVA: 0x00060C75 File Offset: 0x0005EE75
		public CodeEventReferenceExpression Event
		{
			get
			{
				CodeEventReferenceExpression codeEventReferenceExpression;
				if ((codeEventReferenceExpression = this._eventRef) == null)
				{
					codeEventReferenceExpression = (this._eventRef = new CodeEventReferenceExpression());
				}
				return codeEventReferenceExpression;
			}
			set
			{
				this._eventRef = value;
			}
		}

		/// <summary>Gets or sets the event handler to remove.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the event handler to remove.</returns>
		// Token: 0x1700052C RID: 1324
		// (get) Token: 0x060019C1 RID: 6593 RVA: 0x00060C7E File Offset: 0x0005EE7E
		// (set) Token: 0x060019C2 RID: 6594 RVA: 0x00060C86 File Offset: 0x0005EE86
		public CodeExpression Listener { get; set; }

		// Token: 0x04000DF6 RID: 3574
		private CodeEventReferenceExpression _eventRef;
	}
}
