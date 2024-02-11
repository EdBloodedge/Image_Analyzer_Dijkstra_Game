/*
 * Created by SharpDevelop.
 * User: edblo
 * Date: 10/03/2022
 * Time: 01:27 p. m.
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Circulos
{
	/// <summary>
	/// Description of DebugForm.
	/// </summary>
	public partial class DebugForm : Form
	{
		public DebugForm(Bitmap bmp)
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			pictureBox1.Image = bmp;
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
	}
}
