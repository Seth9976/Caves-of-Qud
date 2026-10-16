using System;

namespace System.ComponentModel.Design
{
	/// <summary>Provides an interface for managing designer transactions and components.</summary>
	// Token: 0x0200045F RID: 1119
	public interface IDesignerHost : IServiceContainer, IServiceProvider
	{
		/// <summary>Gets a value indicating whether the designer host is currently loading the document.</summary>
		/// <returns>true if the designer host is currently loading the document; otherwise, false.</returns>
		// Token: 0x1700074E RID: 1870
		// (get) Token: 0x0600242B RID: 9259
		bool Loading { get; }

		/// <summary>Gets a value indicating whether the designer host is currently in a transaction.</summary>
		/// <returns>true if a transaction is in progress; otherwise, false.</returns>
		// Token: 0x1700074F RID: 1871
		// (get) Token: 0x0600242C RID: 9260
		bool InTransaction { get; }

		/// <summary>Gets the container for this designer host.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.IContainer" /> for this host.</returns>
		// Token: 0x17000750 RID: 1872
		// (get) Token: 0x0600242D RID: 9261
		IContainer Container { get; }

		/// <summary>Gets the instance of the base class used as the root component for the current design.</summary>
		/// <returns>The instance of the root component class.</returns>
		// Token: 0x17000751 RID: 1873
		// (get) Token: 0x0600242E RID: 9262
		IComponent RootComponent { get; }

		/// <summary>Gets the fully qualified name of the class being designed.</summary>
		/// <returns>The fully qualified name of the base component class.</returns>
		// Token: 0x17000752 RID: 1874
		// (get) Token: 0x0600242F RID: 9263
		string RootComponentClassName { get; }

		/// <summary>Gets the description of the current transaction.</summary>
		/// <returns>A description of the current transaction.</returns>
		// Token: 0x17000753 RID: 1875
		// (get) Token: 0x06002430 RID: 9264
		string TransactionDescription { get; }

		/// <summary>Occurs when this designer is activated.</summary>
		// Token: 0x1400003E RID: 62
		// (add) Token: 0x06002431 RID: 9265
		// (remove) Token: 0x06002432 RID: 9266
		event EventHandler Activated;

		/// <summary>Occurs when this designer is deactivated.</summary>
		// Token: 0x1400003F RID: 63
		// (add) Token: 0x06002433 RID: 9267
		// (remove) Token: 0x06002434 RID: 9268
		event EventHandler Deactivated;

		/// <summary>Occurs when this designer completes loading its document.</summary>
		// Token: 0x14000040 RID: 64
		// (add) Token: 0x06002435 RID: 9269
		// (remove) Token: 0x06002436 RID: 9270
		event EventHandler LoadComplete;

		/// <summary>Adds an event handler for the <see cref="E:System.ComponentModel.Design.IDesignerHost.TransactionClosed" /> event.</summary>
		// Token: 0x14000041 RID: 65
		// (add) Token: 0x06002437 RID: 9271
		// (remove) Token: 0x06002438 RID: 9272
		event DesignerTransactionCloseEventHandler TransactionClosed;

		/// <summary>Adds an event handler for the <see cref="E:System.ComponentModel.Design.IDesignerHost.TransactionClosing" /> event.</summary>
		// Token: 0x14000042 RID: 66
		// (add) Token: 0x06002439 RID: 9273
		// (remove) Token: 0x0600243A RID: 9274
		event DesignerTransactionCloseEventHandler TransactionClosing;

		/// <summary>Adds an event handler for the <see cref="E:System.ComponentModel.Design.IDesignerHost.TransactionOpened" /> event.</summary>
		// Token: 0x14000043 RID: 67
		// (add) Token: 0x0600243B RID: 9275
		// (remove) Token: 0x0600243C RID: 9276
		event EventHandler TransactionOpened;

		/// <summary>Adds an event handler for the <see cref="E:System.ComponentModel.Design.IDesignerHost.TransactionOpening" /> event.</summary>
		// Token: 0x14000044 RID: 68
		// (add) Token: 0x0600243D RID: 9277
		// (remove) Token: 0x0600243E RID: 9278
		event EventHandler TransactionOpening;

		/// <summary>Activates the designer that this host is hosting.</summary>
		// Token: 0x0600243F RID: 9279
		void Activate();

		/// <summary>Creates a component of the specified type and adds it to the design document.</summary>
		/// <returns>The newly created component.</returns>
		/// <param name="componentClass">The type of the component to create. </param>
		// Token: 0x06002440 RID: 9280
		IComponent CreateComponent(Type componentClass);

		/// <summary>Creates a component of the specified type and name, and adds it to the design document.</summary>
		/// <returns>The newly created component.</returns>
		/// <param name="componentClass">The type of the component to create. </param>
		/// <param name="name">The name for the component. </param>
		// Token: 0x06002441 RID: 9281
		IComponent CreateComponent(Type componentClass, string name);

		/// <summary>Creates a <see cref="T:System.ComponentModel.Design.DesignerTransaction" /> that can encapsulate event sequences to improve performance and enable undo and redo support functionality.</summary>
		/// <returns>A new instance of <see cref="T:System.ComponentModel.Design.DesignerTransaction" />. When you complete the steps in your transaction, you should call <see cref="M:System.ComponentModel.Design.DesignerTransaction.Commit" /> on this object.</returns>
		// Token: 0x06002442 RID: 9282
		DesignerTransaction CreateTransaction();

		/// <summary>Creates a <see cref="T:System.ComponentModel.Design.DesignerTransaction" /> that can encapsulate event sequences to improve performance and enable undo and redo support functionality, using the specified transaction description.</summary>
		/// <returns>A new <see cref="T:System.ComponentModel.Design.DesignerTransaction" />. When you have completed the steps in your transaction, you should call <see cref="M:System.ComponentModel.Design.DesignerTransaction.Commit" /> on this object.</returns>
		/// <param name="description">A title or description for the newly created transaction. </param>
		// Token: 0x06002443 RID: 9283
		DesignerTransaction CreateTransaction(string description);

		/// <summary>Destroys the specified component and removes it from the designer container.</summary>
		/// <param name="component">The component to destroy. </param>
		// Token: 0x06002444 RID: 9284
		void DestroyComponent(IComponent component);

		/// <summary>Gets the designer instance that contains the specified component.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.Design.IDesigner" />, or null if there is no designer for the specified component.</returns>
		/// <param name="component">The <see cref="T:System.ComponentModel.IComponent" /> to retrieve the designer for. </param>
		// Token: 0x06002445 RID: 9285
		IDesigner GetDesigner(IComponent component);

		/// <summary>Gets an instance of the specified, fully qualified type name.</summary>
		/// <returns>The type object for the specified type name, or null if the type cannot be found.</returns>
		/// <param name="typeName">The name of the type to load. </param>
		// Token: 0x06002446 RID: 9286
		Type GetType(string typeName);
	}
}
