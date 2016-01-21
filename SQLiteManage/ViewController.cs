using System;
using System.Collections.Generic;
using Foundation;
using UIKit;

namespace SQLiteManage
{
	public partial class ViewController : UIViewController
	{
		public UILabel lbl;
		UIPickerView CustomerDatePicker;

		public ViewController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			NSLocale locale = new NSLocale ("en");
			this.datePicker.Locale = locale;
			this.datePicker.Mode = UIDatePickerMode.Date;
			// Perform any additional setup after loading the view, typically from a nib.
			this.datePicker.ValueChanged+=DatePicker_ValueChanged;
			lbl = new UILabel ();
			lbl.Frame = new CoreGraphics.CGRect (100, 20, 200, 30);
			lbl.BackgroundColor = UIColor.Gray;
			this.View.AddSubview (lbl);
			CreateCustomerDatePicker ();

		}

		public void CreateCustomerDatePicker()
		{
			this.CustomerDatePicker = new UIPickerView (new CoreGraphics.CGRect (400, 150, 320, 250));
			this.CustomerDatePicker.Model = new CustomerPickerMode (this);
			this.View.AddSubview (this.CustomerDatePicker);
		}

		private void DatePicker_ValueChanged(object sender,EventArgs e)
		{
			NSDateFormatter formatDate = new NSDateFormatter ();
			formatDate.DateFormat = "YYYY/MM/dd HH:mm:ss";
			this.lbl.Text = formatDate.ToString (this.datePicker.Date);
		}

		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
			// Release any cached data, images, etc that aren't in use.
		}
	}

	public class CustomerPickerMode:UIPickerViewModel
	{
		private ViewController parentController;
		private List<string> fristList;
		private List<string> secondList;
		private List<string> thirdList;
		string fristSelectedString;
		string secondSelectedString;
		string thirdSelectedString;

		public CustomerPickerMode(ViewController contController)
		{
			this.parentController = contController;
			fristList = new List<string> (){ "1", "2", "3", "5" };
			secondList = new List<string> (){ "a", "b", "c", "e" };
			thirdList = new List<string> (){ "I", "II", "III", "IIIII" };

			fristSelectedString = fristList [0];
			secondSelectedString = secondList [0];
			thirdSelectedString = thirdList [0];
		}
		/// <summary>
		/// Gets the component count.
		/// </summary>
		/// <returns>The component count.</returns>
		/// <param name="pickerView">Picker view.</param>
		public override nint GetComponentCount (UIPickerView pickerView)
		{
			return 3;
		}
		/// <summary>
		/// Gets the rows in component.
		/// </summary>
		/// <returns>The rows in component.</returns>
		/// <param name="pickerView">Picker view.</param>
		/// <param name="component">Component.</param>
		public override nint GetRowsInComponent (UIPickerView pickerView, nint component)
		{
			switch (component) {
			case 0:
				{
					return fristList.Count;
				}
			case 1:
				{
					return secondList.Count;
				}
			default:
				{
					return thirdList.Count;
				}
			}
		}
		/// <summary>
		/// Gets the title.
		/// </summary>
		/// <returns>The title.</returns>
		/// <param name="pickerView">Picker view.</param>
		/// <param name="row">Row.</param>
		/// <param name="component">Component.</param>
		public override string GetTitle (UIPickerView pickerView, nint row, nint component)
		{
			switch (component) {
			case 0:
				{
					return fristList[(int)row];
				}
			case 1:
				{
					return secondList[(int)row];
				}
			default:
				{
					return thirdList[(int)row];
				}
			}
		}
		public override void Selected (UIPickerView pickerView, nint row, nint component)
		{
			switch (component) {
			case 0:
				{
					fristSelectedString= fristList[(int)row];
					break;
				}
			case 1:
				{
					secondSelectedString= secondList[(int)row];
					break;
				}
			default:
				{
					thirdSelectedString= thirdList[(int)row];
					break;
				}
			}



			parentController.lbl.Text = string.Format ("F={0};S={1};T={2}", fristSelectedString, secondSelectedString, thirdSelectedString);
			Console.WriteLine (parentController.lbl.Text);
		}

		public override void WillChangeValue (string forKey)
		{
			Console.WriteLine (DateTime.Now.ToString ());
		}
	}
}

