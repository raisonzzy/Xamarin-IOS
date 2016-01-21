using System;
using Foundation;
using UIKit;
using System.CodeDom.Compiler;

namespace UnwindSegue
{
 	partial class ViewController : UIViewController
	{
		public ViewController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			// Perform any additional setup after loading the view, typically from a nib.
			this.Title="Frist";
		}

		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
			// Release any cached data, images, etc that aren't in use.
		}

		[Action("unwindFromModalController:")]
		public void UnwindFromModalController(UIStoryboardSegue segue)
		{
			Console.WriteLine("Unwind From Modal Controller");
			ModalController modalController = (ModalController)segue.SourceViewController;

		}
	}
}

