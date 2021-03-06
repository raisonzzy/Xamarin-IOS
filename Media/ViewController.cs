﻿using System;
using Foundation;
using UIKit;
using MediaPlayer;
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
		UIButton btnPlay;
		MPMoviePlayerController moviePlayer;

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

			btnPlay= new UIButton (UIButtonType.System);
			btnPlay.Frame = new CoreGraphics.CGRect (580, 300, 200, 30);
			btnPlay.SetTitle ("播放视频", UIControlState.Normal);
			btnPlay.BackgroundColor = UIColor.Gray;
			btnPlay.SetTitleColor (UIColor.White, UIControlState.Normal);
			btnPlay.TouchUpInside += BtnPlay_TouchUpInside;

			imagePicker=new UIImagePickerController();
			//imagePicker.SourceType = UIImagePickerControllerSourceType.PhotoLibrary;
			imagePicker.FinishedPickingMedia += ImagePicker_FinishedPickingImage;
			imagePicker.Canceled+=ImagePicker_Canceled;

			this.View.AddSubviews (btn, imgView, btnAlbum, btnSelMedia, btnCamera, btnCustomCamera, btnPlay);
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

		public void BtnPlay_TouchUpInside(object sender,EventArgs e)
		{
			moviePlayer = new MPMoviePlayerController (NSUrl.FromFilename ("VID_20150904_123931.mp4"));
			moviePlayer.View.Frame = new CoreGraphics.CGRect (50, 50, 500, 700);
			this.View.AddSubview (moviePlayer.View);
			UIButton btnp = new UIButton (UIButtonType.System);
			btnp.Frame = new CoreGraphics.CGRect (580, 340, 90, 30);
			btnp.SetTitle ("Play", UIControlState.Normal);
			btnp.TouchUpInside += (send, ev) => {
				moviePlayer.Play ();
			};
			UIButton btnstop = new UIButton (UIButtonType.System);
			btnstop.Frame = new CoreGraphics.CGRect (680, 340, 90, 30);
			btnstop.SetTitle ("Stop", UIControlState.Normal);
			btnstop.TouchUpInside += (send, ev) => {
				moviePlayer.Stop ();
			};
			this.View.AddSubviews (btnp, btnstop);
		}

		public async void BtnCustomCamera_TouchUpInside(object sender,EventArgs e)
		{
			_flag=1;
			this.imagePicker.SourceType = UIImagePickerControllerSourceType.Camera;
			UIView vv = new UIView (this.imagePicker.View.Frame);
			UIToolbar toolbar = new UIToolbar (new CoreGraphics.CGRect (0, this.View.Frame.Height - 88, this.View.Frame.Width, 80));
			UIButton btnta = new UIButton ();
			btnta.Frame = new CoreGraphics.CGRect (0, 0, 50, 50);
			btnta.SetImage (UIImage.FromFile("1_130419110725_6.png"), UIControlState.Normal);
			btnta.ShowsTouchWhenHighlighted = true;
			btnta.TouchUpInside += (send, ev) => {
				if (this.imagePicker.CameraDevice == UIImagePickerControllerCameraDevice.Rear) {
					if (UIImagePickerController.IsCameraDeviceAvailable (UIImagePickerControllerCameraDevice.Front)) {
						this.imagePicker.CameraDevice = UIImagePickerControllerCameraDevice.Front;
					}
				} else {
					this.imagePicker.CameraDevice = UIImagePickerControllerCameraDevice.Rear;
				}
			};
			UIBarButtonItem spItem = new UIBarButtonItem (btnta);

			UIButton btntad = new UIButton ();
			btntad.Frame = new CoreGraphics.CGRect (60, 0, 50, 50);
			btntad.SetImage (UIImage.FromFile("1_130124110644_3.png"), UIControlState.Normal);
			btntad.ShowsTouchWhenHighlighted = true;
			btntad.TouchUpInside += (send, ev) => {
				imagePicker.TakePicture();
			};
			UIBarButtonItem dItem = new UIBarButtonItem (btntad);

			toolbar.Items = new UIBarButtonItem[]{ spItem, dItem };
			vv.AddSubview (toolbar);
			this.imagePicker.ShowsCameraControls = false;
			this.imagePicker.CameraOverlayView = vv;

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

