using System;

namespace System.ComponentModel.Design
{
	/// <summary>Defines identifiers that indicate the type of a selection.</summary>
	// Token: 0x02000473 RID: 1139
	[Flags]
	public enum SelectionTypes
	{
		/// <summary>Represents a regular selection. The selection service responds to the CTRL and SHIFT keys to support adding or removing components to or from the selection.</summary>
		// Token: 0x040010FD RID: 4349
		Auto = 1,
		/// <summary>Represents a regular selection. The selection service responds to the CTRL and SHIFT keys to support adding or removing components to or from the selection.</summary>
		// Token: 0x040010FE RID: 4350
		[Obsolete("This value has been deprecated. Use SelectionTypes.Auto instead.  http://go.microsoft.com/fwlink/?linkid=14202")]
		Normal = 1,
		/// <summary>Represents a selection that occurs when the content of a selection is replaced. The selection service replaces the current selection with the replacement.</summary>
		// Token: 0x040010FF RID: 4351
		Replace = 2,
		/// <summary>Represents a selection that occurs when the user presses on the mouse button while the mouse pointer is over a component. If the component under the pointer is already selected, it is promoted to become the primary selected component rather than being canceled.</summary>
		// Token: 0x04001100 RID: 4352
		[Obsolete("This value has been deprecated.  It is no longer supported. http://go.microsoft.com/fwlink/?linkid=14202")]
		MouseDown = 4,
		/// <summary>Represents a selection that occurs when the user releases the mouse button immediately after a component has been selected. If the newly selected component is already selected, it is promoted to be the primary selected component rather than being canceled.</summary>
		// Token: 0x04001101 RID: 4353
		[Obsolete("This value has been deprecated.  It is no longer supported. http://go.microsoft.com/fwlink/?linkid=14202")]
		MouseUp = 8,
		/// <summary>Represents a selection that occurs when a user clicks a component. If the newly selected component is already selected, it is promoted to be the primary selected component rather than being canceled.</summary>
		// Token: 0x04001102 RID: 4354
		[Obsolete("This value has been deprecated. Use SelectionTypes.Primary instead.  http://go.microsoft.com/fwlink/?linkid=14202")]
		Click = 16,
		/// <summary>Represents a primary selection that occurs when a user clicks on a component. If a component in the selection list is already selected, the component is promoted to be the primary selection.</summary>
		// Token: 0x04001103 RID: 4355
		Primary = 16,
		/// <summary>Represents a toggle selection that switches between the current selection and the provided selection. If a component is already selected and is passed into <see cref="Overload:System.ComponentModel.Design.ISelectionService.SetSelectedComponents" /> with a selection type of <see cref="F:System.ComponentModel.Design.SelectionTypes.Toggle" />, the component selection will be canceled.</summary>
		// Token: 0x04001104 RID: 4356
		Toggle = 32,
		/// <summary>Represents an add selection that adds the selected components to the current selection, maintaining the current set of selected components.</summary>
		// Token: 0x04001105 RID: 4357
		Add = 64,
		/// <summary>Represents a remove selection that removes the selected components from the current selection, maintaining the current set of selected components.</summary>
		// Token: 0x04001106 RID: 4358
		Remove = 128,
		/// <summary>Identifies the valid selection types as <see cref="F:System.ComponentModel.Design.SelectionTypes.Normal" />, <see cref="F:System.ComponentModel.Design.SelectionTypes.Replace" />, <see cref="F:System.ComponentModel.Design.SelectionTypes.MouseDown" />, <see cref="F:System.ComponentModel.Design.SelectionTypes.MouseUp" />, or <see cref="F:System.ComponentModel.Design.SelectionTypes.Click" />.</summary>
		// Token: 0x04001107 RID: 4359
		[Obsolete("This value has been deprecated. Use Enum class methods to determine valid values, or use a type converter. http://go.microsoft.com/fwlink/?linkid=14202")]
		Valid = 31
	}
}
