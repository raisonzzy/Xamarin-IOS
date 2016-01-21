using System;
using Foundation;
using UIKit;

namespace Media
{
	public partial class ViewController : UIViewController
	{
		UIImagePickerController imagePicker;
		UIImageView imgView;
		UIButton btn;
		UIButton btnAlbum;
		UIButton btnSelMedia;
		int _flag;
		UIWebView webview;
		UIButton btnCamera;
		UIButton btnCustomCamera;

		public ViewController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			// Perform any additional setup after loading the view, typically from a nib.
			imgView =new UIImageView(new CoreGraphics.CGRect(50,50,500,700));

			btn = new UIButton (UIButtonType.System);
			btn.Frame = new CoreGraphics.CGRect (580, 50, 200, 30);
			btn.SetTitle ("选择图像(默认照相库)", UIControlState.Normal);
			btn.BackgroundColor = UIColor.Gray;
			btn.SetTitleColor (UIColor.White, UIControlState.Normal);
			btn.TouchUpInside += Btn_TouchUpInside;

			btnAlbum= new UIButton (UIButtonType.System);
			btnAlbum.Frame = new CoreGraphics.CGRect (580, 100, 200, 30);
			btnAlbum.SetTitle ("选择图像(相册)", UIControlState.Normal);
			btnAlbum.BackgroundColor = UIColor.Gray;
			btnAlbum.SetTitleColor (UIColor.White, UIControlState.Normal);
			btnAlbum.TouchUpInside += BtnAlbum_TouchUpInside;

			btnSelMedia= new UIButton (UIButtonType.System);
			btnSelMedia.Frame = new CoreGraphics.CGRect (580, 250, 200, 30);
			btnSelMedia.SetTitle ("选择视频", UIControlState.Normal);
			btnSelMedia.BackgroundColor = UIColor.Gray;
			btnSelMedia.SetTitleColor (UIColor.White, UIControlState.Normal);
			btnSelMedia.TouchUpInside += BtnSelMedia_TouchUpInside;

			btnCamera= new UIButton (UIButtonType.System);
			btnCamera.Frame = new CoreGraphics.CGRect (580, 150, 200, 30);
			btnCamera.SetTitle ("使用相机", UIControlState.Normal);
			btnCamera.BackgroundColor = UIColor.Gray;
			btnCamera.SetTitleColor (UIColor.White, UIControlState.Normal);
			btnCamera.TouchUpInside += BtnCamera_TouchUpInside;

			btnCustomCamera= new UIButton (UIButtonType.System);
			btnCustomCamera.Frame = new CoreGraphics.CGRect (580, 200, 200, 30);
			btnCustomCamera.SetTitle ("自定义相机", UIControlState.Normal);
			btnCustomCamera.BackgroundColor = UIColor.Gray;
			btnCustomCamera.SetTitleColor (UIColor.White, UIControlState.Normal);
			btnCustomCamera.TouchUpInside += BtnCustomCamera_TouchUpInside;

			imagePicker=new UIImagePickerController();
			//imagePicker.SourceType = UIImagePickerControllerSourceType.PhotoLibrary;
			imagePicker.FinishedPickingMedia += ImagePicker_FinishedPickingImage;
			imagePicker.Canceled+=ImagePicker_Canceled;

			this.View.AddSubviews (btn, imgView, btnAlbum, btnSelMedia, btnCamera);
		}
		public async void Btn_TouchUpInside(object sender,EventArgs e)
		{
			_flag=1;
			this.imagePicker.SourceType = UIImagePickerControllerSourceType.PhotoLibrary;
			await this.PresentViewControllerAsync(this.imagePicker,true);
		}
		public async void BtnAlbum_TouchUpInside(object sender,EventArgs e)
		{
			_flag=1;
			this.imagePicker.SourceType = UIImagePickerControllerSourceType.SavedPhotosAlbum;
			await this.PresentViewControllerAsync (this.imagePicker, true);
		}

		public async void BtnCamera_TouchUpInside(object sender,EventArgs e)
		{
			_flag=1;
			this.imagePicker.SourceType = UIImagePickerControllerSourceType.Camera;
			this.imagePicker.CameraDevice = UIImagePickerControllerCameraDevice.Rear;
			//this.imagePicker.CameraCaptureMode = UIImagePickerControllerCameraCaptureMode.Photo;
			this.imagePicker.CameraFlashMode=UIImagePickerControllerCameraFlashMode.Auto;
			await this.PresentViewControllerAsync (this.imagePicker, true);
		}

		public async void BtnCustomCamera_TouchUpInside(object sender,EventArgs e)
		{
			_flag=1;
			this.imagePicker.SourceType = UIImagePickerControllerSourceType.Camera;

			await this.PresentViewControllerAsync (this.imagePicker, true);
		}

		public async void BtnSelMedia_TouchUpInside(object sender,EventArgs e)
		{
			_flag=2;
			this.imagePicker.MediaTypes = new string[]{ "public.movie" };
			webview = new UIWebView (new CoreGraphics.CGRect (50, 50, 500, 700));
			this.View.AddSubview (webview);
			await this.PresentViewControllerAsync (this.imagePicker, true);
		}

		public async void ImagePicker_FinishedPickingImage(object sender,UIImagePickerMediaPickedEventArgs e)
		{
			if (_flag == 1) {
				UIImage im = e.Info [UIImagePickerController.OriginalImage] as UIImage;
				this.imgView.Image = im;
			} else {
				NSUrl url = e.Info [UIImagePickerController.MediaURL] as NSUrl;
				NSUrlRequest request = new NSUrlRequest (url);
				webview.LoadRequest (request);
			}
			await imagePicker.DismissViewControllerAsync (true);
		}
		public async void ImagePicker_Canceled(object sender,EventArgs e)
		{
			await imagePicker.DismissViewControllerAsync (true);
		}
		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
			// Release any cached data, images, etc that aren't in use.
		}
	}
}

