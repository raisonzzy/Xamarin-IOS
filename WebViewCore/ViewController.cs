using System;
using Foundation;
using UIKit;

namespace WebViewCore
{
	public partial class ViewController : UIViewController
	{
		UIWebView webView;
		UITextView txt;
		public ViewController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			txt = new UITextView (new CoreGraphics.CGRect (100, 50, 200, 30));
			txt.BackgroundColor = UIColor.Gray;
			txt.TextColor = UIColor.White;
			txt.Text="https://auth.alipay.com/login/index.htm";

			UIButton btn = new UIButton (UIButtonType.System);
			btn.Frame = new CoreGraphics.CGRect (330, 50, 100, 30);
			btn.SetTitle ("GO", UIControlState.Normal);
			btn.TouchUpInside += Btn_TouchUpInside;

			UIButton btnHtmlString = new UIButton (UIButtonType.System);
			btnHtmlString.Frame = new CoreGraphics.CGRect (450, 50, 100, 30);
			btnHtmlString.SetTitle ("Html String", UIControlState.Normal);
			btnHtmlString.TouchUpInside += BtnHtmlString_TouchUpInside;

			UIButton btnFile = new UIButton (UIButtonType.System);
			btnFile.Frame = new CoreGraphics.CGRect (580, 50, 100, 30);
			btnFile.SetTitle ("Load File", UIControlState.Normal);
			btnFile.TouchUpInside += BtnFile_TouchUpInside;

			// Perform any additional setup after loading the view, typically from a nib.
			webView=new UIWebView(new CoreGraphics.CGRect(100,100,600,700));
			webView.ContentMode = UIViewContentMode.Center;
			
			this.View.AddSubviews (txt, btn, webView, btnHtmlString, btnFile);

		}

		public void Btn_TouchUpInside(object sender,EventArgs e)
		{
			if (!string.IsNullOrEmpty (this.txt.Text)) {

				NSUrl url = new NSUrl (this.txt.Text);
				NSUrlRequest request = new NSUrlRequest (url);
				this.webView.LoadRequest (request);
			}
		}

		public void BtnHtmlString_TouchUpInside(object sender,EventArgs e)
		{
			this.webView.LoadHtmlString("<font color='blue'>Htmlstring</font>",null);
		}

		public void BtnFile_TouchUpInside(object sender,EventArgs e)
		{
			string path = NSBundle.MainBundle.PathForResource ("宽表整理 1201", "pdf");
			NSUrl url = NSUrl.FromFilename ("宽表整理 1201.pdf");
			NSData data = NSData.FromFile (path);
			this.webView.LoadData (data, "application/pdf", "UTF-8", url);
		}
		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
			// Release any cached data, images, etc that aren't in use.
		}
	}
}

