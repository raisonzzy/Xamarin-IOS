using System;

using UIKit;

namespace SplitController
{
	public partial class DetailView : UIViewController
	{
		public UIToolbar MyTool
		{
			get{ return this.myToolBar;}
		}

		public DetailView () : base ("DetailView", null)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			// Perform any additional setup after loading the view, typically from a nib.
			//myToolBar=new UIToolbar();
		}

		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
			// Release any cached data, images, etc that aren't in use.
		}

	}

	public class SplitControllerDelegate:UISplitViewControllerDelegate
	{
		private DetailView detailView;
		public SplitControllerDelegate(DetailView controll)
		{
			this.detailView = controll;
		}

		public override void WillHideViewController (UISplitViewController svc, UIViewController aViewController, UIBarButtonItem barButtonItem, UIPopoverController pc)
		{
			barButtonItem.Title="First";
			this.detailView.MyTool.SetItems (new UIBarButtonItem[]{ barButtonItem }, true);
		}
		public override void WillShowViewController (UISplitViewController svc, UIViewController aViewController, UIBarButtonItem button)
		{
			
			this.detailView.MyTool.SetItems (new UIBarButtonItem[0], true);
		}
	}
}


