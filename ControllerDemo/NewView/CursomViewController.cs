using System;

using UIKit;

namespace ControllerDemo
{
	public partial class CursomViewController : UIViewController
	{
		public CursomViewController () : base ("CursomViewController", null)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			// Perform any additional setup after loading the view, typically from a nib.
//			UIBarButtonItem leftBtn=new UIBarButtonItem("Page Up",UIBarButtonItemStyle.Done,this,null);
//			this.NavigationItem.LeftBarButtonItem = leftBtn;
//
//			UIBarButtonItem rightBtn=new UIBarButtonItem("Page Next",UIBarButtonItemStyle.Done,this,null);
//			this.NavigationItem.RightBarButtonItem = rightBtn;
		}

		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
			// Release any cached data, images, etc that aren't in use.
		}

		partial void UIButton14_TouchUpInside (UIButton sender)
		{
			ViewControllerNextOne oneview=new ViewControllerNextOne();
			oneview.Title="View One";
			this.NavigationController.PushViewController(oneview,false);

		}

		partial void UIButton28_TouchUpInside (UIButton sender)
		{
			ViewControllerNextTwo twoview=new ViewControllerNextTwo();
			twoview.Title="View Two";
			this.NavigationController.PushViewController(twoview,false);
		}
	}
}


