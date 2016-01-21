using System;

using UIKit;

namespace ControllerDemo
{
	public partial class ViewControllerNextOne : UIViewController
	{
		public ViewControllerNextOne () : base ("ViewControllerNextOne", null)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			// Perform any additional setup after loading the view, typically from a nib.
			UIBarButtonItem leftBtn=new UIBarButtonItem("Page Up",UIBarButtonItemStyle.Plain,this,null);
			this.NavigationItem.LeftBarButtonItem = leftBtn;

			UIBarButtonItem rightBtn=new UIBarButtonItem("Page Next",UIBarButtonItemStyle.Done,this,null);
			this.NavigationItem.RightBarButtonItem = rightBtn;
		}

		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
			// Release any cached data, images, etc that aren't in use.
		}

	}
}


