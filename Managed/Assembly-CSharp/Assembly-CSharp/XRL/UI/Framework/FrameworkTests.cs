using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace XRL.UI.Framework
{
	// Token: 0x020014F1 RID: 5361
	[TestFixture]
	public class FrameworkTests
	{
		// Token: 0x17000C7B RID: 3195
		// (get) Token: 0x0600D18C RID: 53644 RVA: 0x004DDA6D File Offset: 0x004DBC6D
		public NavigationController controller
		{
			get
			{
				return NavigationController.instance;
			}
		}

		// Token: 0x0600D18D RID: 53645 RVA: 0x004DDA74 File Offset: 0x004DBC74
		[SetUp]
		public void Setup()
		{
			NavigationController.instance = new NavigationController();
		}

		// Token: 0x0600D18E RID: 53646 RVA: 0x004DDA80 File Offset: 0x004DBC80
		[TearDown]
		public void Teardown()
		{
			NavigationController.instance.activeContext = null;
		}

		// Token: 0x17000C7C RID: 3196
		// (get) Token: 0x0600D18F RID: 53647 RVA: 0x004DDA8D File Offset: 0x004DBC8D
		public Event currentEvent
		{
			get
			{
				return NavigationController.currentEvent;
			}
		}

		// Token: 0x0600D190 RID: 53648 RVA: 0x004DDA94 File Offset: 0x004DBC94
		[Test]
		public void TestSimpleButtons()
		{
			int testPhase = 0;
			int asserts = 0;
			new NavigationContext
			{
				buttonHandlers = new Dictionary<InputButtonTypes, Action>
				{
					{
						InputButtonTypes.AcceptButton,
						delegate
						{
							Assert.AreEqual(1, testPhase);
							int asserts3 = asserts;
							asserts = asserts3 + 1;
						}
					},
					{
						InputButtonTypes.CancelButton,
						delegate
						{
							Assert.AreEqual(2, testPhase);
							int asserts2 = asserts;
							asserts = asserts2 + 1;
						}
					}
				}
			}.Activate(false);
			testPhase = 1;
			this.controller.FireInputButtonEvent(InputButtonTypes.AcceptButton, null);
			testPhase = 2;
			this.controller.FireInputButtonEvent(InputButtonTypes.CancelButton, null);
			Assert.AreEqual(2, asserts);
		}

		// Token: 0x0600D191 RID: 53649 RVA: 0x004DDB34 File Offset: 0x004DBD34
		[Test]
		public void TestEnterExit()
		{
			int testPhase = 0;
			int asserts = 0;
			NavigationContext firstContext = null;
			NavigationContext secondContext = null;
			List<Action> list = new List<Action>
			{
				delegate
				{
					if (testPhase == 1)
					{
						Assert.AreEqual(firstContext, this.currentEvent.data["to"]);
						Assert.AreEqual(null, this.currentEvent.data["from"]);
						int num = asserts;
						asserts = num + 1;
						return;
					}
					if (testPhase == 3)
					{
						Assert.AreEqual(secondContext, this.currentEvent.data["to"]);
						Assert.AreEqual(firstContext, this.currentEvent.data["from"]);
						int num = asserts;
						asserts = num + 1;
						return;
					}
					Assert.Fail("Should not be called: testPhase " + testPhase.ToString());
				},
				delegate
				{
					if (testPhase == 2)
					{
						Assert.AreEqual(secondContext, this.currentEvent.data["to"]);
						Assert.AreEqual(firstContext, this.currentEvent.data["from"]);
						int num2 = asserts;
						asserts = num2 + 1;
						num2 = testPhase;
						testPhase = num2 + 1;
						return;
					}
					if (testPhase == 4)
					{
						Assert.AreEqual(null, this.currentEvent.data["to"]);
						Assert.AreEqual(secondContext, this.currentEvent.data["from"]);
						int num2 = asserts;
						asserts = num2 + 1;
						num2 = testPhase;
						testPhase = num2 + 1;
						return;
					}
					Assert.Fail("Should not be called");
				},
				delegate
				{
					Assert.AreEqual(1, testPhase);
					Assert.AreEqual(firstContext, this.currentEvent.data["to"]);
					Assert.AreEqual(null, this.currentEvent.data["from"]);
					int asserts5 = asserts;
					asserts = asserts5 + 1;
				},
				delegate
				{
					Assert.AreEqual(2, testPhase);
					Assert.AreEqual(secondContext, this.currentEvent.data["to"]);
					Assert.AreEqual(firstContext, this.currentEvent.data["from"]);
					int asserts2 = asserts;
					asserts = asserts2 + 1;
				},
				delegate
				{
					Assert.AreEqual(3, testPhase);
					Assert.AreEqual(secondContext, this.currentEvent.data["to"]);
					Assert.AreEqual(firstContext, this.currentEvent.data["from"]);
					int asserts3 = asserts;
					asserts = asserts3 + 1;
				},
				delegate
				{
					Assert.AreEqual(4, testPhase);
					Assert.AreEqual(null, this.currentEvent.data["to"]);
					Assert.AreEqual(secondContext, this.currentEvent.data["from"]);
					int asserts4 = asserts;
					asserts = asserts4 + 1;
				}
			};
			NavigationContext navigationContext = new NavigationContext
			{
				enterHandler = list[0],
				exitHandler = list[1]
			};
			firstContext = new NavigationContext
			{
				enterHandler = list[2],
				exitHandler = list[3],
				parentContext = navigationContext
			};
			secondContext = new NavigationContext
			{
				enterHandler = list[4],
				exitHandler = list[5],
				parentContext = navigationContext
			};
			testPhase = 1;
			firstContext.Activate(false);
			testPhase = 2;
			secondContext.Activate(false);
			testPhase = 4;
			this.controller.activeContext = null;
			Assert.AreEqual(8, asserts);
		}

		// Token: 0x0600D192 RID: 53650 RVA: 0x004DDCA8 File Offset: 0x004DBEA8
		[Test]
		public void TestSimpleAxis()
		{
			int testPhase = 0;
			int asserts = 0;
			NavigationContext navigationContext = new NavigationContext
			{
				axisHandlers = new Dictionary<InputAxisTypes, Action>
				{
					{
						InputAxisTypes.NavigationXAxis,
						delegate
						{
							Assert.AreEqual(1, testPhase);
							int asserts3 = asserts;
							asserts = asserts3 + 1;
						}
					},
					{
						InputAxisTypes.NavigationYAxis,
						delegate
						{
							Assert.AreEqual(2, testPhase);
							int asserts2 = asserts;
							asserts = asserts2 + 1;
						}
					}
				}
			};
			this.controller.activeContext = navigationContext;
			testPhase = 1;
			this.controller.FireInputAxisEvent(InputAxisTypes.NavigationXAxis, null, 0);
			testPhase = 2;
			this.controller.FireInputAxisEvent(InputAxisTypes.NavigationYAxis, null, 0);
			testPhase = 3;
			this.controller.FireInputAxisEvent(InputAxisTypes.NavigationUAxis, null, 0);
			Assert.AreEqual(2, asserts);
		}

		// Token: 0x0600D193 RID: 53651 RVA: 0x004DDD64 File Offset: 0x004DBF64
		[Test]
		public void TestParentContextsWithCancel()
		{
			int testPhase = 0;
			int asserts = 0;
			NavigationContext navigationContext = new NavigationContext();
			Dictionary<InputButtonTypes, Action> dictionary = new Dictionary<InputButtonTypes, Action>();
			dictionary.Add(InputButtonTypes.AcceptButton, delegate
			{
				Assert.AreEqual(1, testPhase);
				int asserts4 = asserts;
				asserts = asserts4 + 1;
			});
			dictionary.Add(InputButtonTypes.CancelButton, delegate
			{
				Assert.Fail("Should not be called!");
			});
			navigationContext.buttonHandlers = dictionary;
			NavigationContext navigationContext2 = navigationContext;
			NavigationContext navigationContext3 = new NavigationContext
			{
				parentContext = navigationContext2,
				buttonHandlers = new Dictionary<InputButtonTypes, Action>
				{
					{
						InputButtonTypes.AcceptButton,
						delegate
						{
							Assert.AreEqual(1, testPhase);
							int asserts2 = asserts;
							asserts = asserts2 + 1;
						}
					},
					{
						InputButtonTypes.CancelButton,
						delegate
						{
							Assert.AreEqual(2, testPhase);
							int asserts3 = asserts;
							asserts = asserts3 + 1;
							this.currentEvent.Cancel();
						}
					}
				}
			};
			this.controller.activeContext = navigationContext3;
			testPhase = 1;
			this.controller.FireInputButtonEvent(InputButtonTypes.AcceptButton, null);
			testPhase = 2;
			this.controller.FireInputButtonEvent(InputButtonTypes.CancelButton, null);
			Assert.AreEqual(3, asserts);
		}

		// Token: 0x0600D194 RID: 53652 RVA: 0x004DDE60 File Offset: 0x004DC060
		[Test]
		public void TestIsActive()
		{
			NavigationContext navigationContext = new NavigationContext();
			NavigationContext navigationContext2 = new NavigationContext
			{
				parentContext = navigationContext
			};
			Assert.AreEqual(false, navigationContext.IsActive(true));
			Assert.AreEqual(false, navigationContext2.IsActive(true));
			Assert.AreEqual(false, navigationContext.IsActive(false));
			Assert.AreEqual(false, navigationContext2.IsActive(false));
			navigationContext.Activate(false);
			Assert.AreEqual(true, navigationContext.IsActive(true));
			Assert.AreEqual(true, navigationContext.IsActive(false));
			Assert.AreEqual(false, navigationContext2.IsActive(true));
			Assert.AreEqual(false, navigationContext2.IsActive(false));
			navigationContext2.Activate(false);
			Assert.AreEqual(true, navigationContext2.IsActive(true));
			Assert.AreEqual(true, navigationContext2.IsActive(true));
			Assert.AreEqual(true, navigationContext.IsActive(true));
			Assert.AreEqual(false, navigationContext.IsActive(false));
		}
	}
}
