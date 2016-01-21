using System;

using UIKit;

namespace StoryBoard
{
	public partial class ViewController : UIViewController
	{
		public ViewController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			// Perform any additional setup after loading the view, typically from a nib.
		}

		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
			// Release any cached data, images, etc that aren't in use.
		}

		public override void PrepareForSegue (UIStoryboardSegue segue, Foundation.NSObject sender)
		{
			base.PrepareForSegue (segue, sender);
			Console.WriteLine ("segue {0}", segue.Identifier);
			if (segue.Identifier == "myseg") {
				SecondController sen = (SecondController)segue.DestinationViewController;
				sen.ParaString = this.txtPara.Text;
			}

		}
	}
}

