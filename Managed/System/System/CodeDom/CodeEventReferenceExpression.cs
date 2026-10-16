using System;

namespace System.CodeDom
{
	/// <summary>Represents a reference to an event.</summary>
	// Token: 0x0200030F RID: 783
	[Serializable]
	public class CodeEventReferenceExpression : CodeExpression
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeEventReferenceExpression" /> class.</summary>
		// Token: 0x060018E0 RID: 6368 RVA: 0x0005EE6C File Offset: 0x0005D06C
		public CodeEventReferenceExpression()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeEventReferenceExpression" /> class using the specified target object and event name.</summary>
		/// <param name="targetObject">A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the object that contains the event. </param>
		/// <param name="eventName">The name of the event to reference. </param>
		// Token: 0x060018E1 RID: 6369 RVA: 0x0005FB58 File Offset: 0x0005DD58
		public CodeEventReferenceExpression(CodeExpression targetObject, string eventName)
		{
			this.TargetObject = targetObject;
			this._eventName = eventName;
		}

		/// <summary>Gets or sets the object that contains the event.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the object that contains the event.</returns>
		// Token: 0x170004E8 RID: 1256
		// (get) Token: 0x060018E2 RID: 6370 RVA: 0x0005FB6E File Offset: 0x0005DD6E
		// (set) Token: 0x060018E3 RID: 6371 RVA: 0x0005FB76 File Offset: 0x0005DD76
		public CodeExpression TargetObject { get; set; }

		/// <summary>Gets or sets the name of the event.</summary>
		/// <returns>The name of the event.</returns>
		// Token: 0x170004E9 RID: 1257
		// (get) Token: 0x060018E4 RID: 6372 RVA: 0x0005FB7F File Offset: 0x0005DD7F
		// (set) Token: 0x060018E5 RID: 6373 RVA: 0x0005FB90 File Offset: 0x0005DD90
		public string EventName
		{
			get
			{
				return this._eventName ?? string.Empty;
			}
			set
			{
				this._eventName = value;
			}
		}

		// Token: 0x04000DAA RID: 3498
		private string _eventName;
	}
}
