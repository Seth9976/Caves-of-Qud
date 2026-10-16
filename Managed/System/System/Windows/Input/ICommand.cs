using System;
using System.Runtime.CompilerServices;

namespace System.Windows.Input
{
	/// <summary>Defines a command.</summary>
	// Token: 0x0200017D RID: 381
	[TypeForwardedFrom("PresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35")]
	public interface ICommand
	{
		/// <summary>Defines the method that determines whether the command can execute in its current state.</summary>
		/// <returns>true if this command can be executed; otherwise, false.</returns>
		/// <param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to null.</param>
		// Token: 0x06000A20 RID: 2592
		bool CanExecute(object parameter);

		/// <summary>Defines the method to be called when the command is invoked.</summary>
		/// <param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to null.</param>
		// Token: 0x06000A21 RID: 2593
		void Execute(object parameter);

		/// <summary>Occurs when changes occur that affect whether or not the command should execute.</summary>
		// Token: 0x14000013 RID: 19
		// (add) Token: 0x06000A22 RID: 2594
		// (remove) Token: 0x06000A23 RID: 2595
		event EventHandler CanExecuteChanged;
	}
}
