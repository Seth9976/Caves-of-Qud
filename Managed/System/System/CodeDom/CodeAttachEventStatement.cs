using System;

namespace System.CodeDom
{
	/// <summary>Represents a statement that attaches an event-handler delegate to an event.</summary>
	// Token: 0x020002F6 RID: 758
	[Serializable]
	public class CodeAttachEventStatement : CodeStatement
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeAttachEventStatement" /> class.</summary>
		// Token: 0x0600182F RID: 6191 RVA: 0x0005F0B9 File Offset: 0x0005D2B9
		public CodeAttachEventStatement()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeAttachEventStatement" /> class using the specified event and delegate.</summary>
		/// <param name="eventRef">A <see cref="T:System.CodeDom.CodeEventReferenceExpression" /> that indicates the event to attach an event handler to. </param>
		/// <param name="listener">A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the new event handler. </param>
		// Token: 0x06001830 RID: 6192 RVA: 0x0005F0F9 File Offset: 0x0005D2F9
		public CodeAttachEventStatement(CodeEventReferenceExpression eventRef, CodeExpression listener)
		{
			this._eventRef = eventRef;
			this.Listener = listener;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeAttachEventStatement" /> class using the specified object containing the event, event name, and event-handler delegate.</summary>
		/// <param name="targetObject">A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the object that contains the event. </param>
		/// <param name="eventName">The name of the event to attach an event handler to. </param>
		/// <param name="listener">A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the new event handler. </param>
		// Token: 0x06001831 RID: 6193 RVA: 0x0005F10F File Offset: 0x0005D30F
		public CodeAttachEventStatement(CodeExpression targetObject, string eventName, CodeExpression listener)
			: this(new CodeEventReferenceExpression(targetObject, eventName), listener)
		{
		}

		/// <summary>Gets or sets the event to attach an event-handler delegate to.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeEventReferenceExpression" /> that indicates the event to attach an event handler to.</returns>
		// Token: 0x170004BC RID: 1212
		// (get) Token: 0x06001832 RID: 6194 RVA: 0x0005F120 File Offset: 0x0005D320
		// (set) Token: 0x06001833 RID: 6195 RVA: 0x0005F145 File Offset: 0x0005D345
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

		/// <summary>Gets or sets the new event-handler delegate to attach to the event.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the new event handler to attach.</returns>
		// Token: 0x170004BD RID: 1213
		// (get) Token: 0x06001834 RID: 6196 RVA: 0x0005F14E File Offset: 0x0005D34E
		// (set) Token: 0x06001835 RID: 6197 RVA: 0x0005F156 File Offset: 0x0005D356
		public CodeExpression Listener { get; set; }

		// Token: 0x04000D71 RID: 3441
		private CodeEventReferenceExpression _eventRef;
	}
}
