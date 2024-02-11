/*
 * Created by SharpDevelop.
 * User: edblo
 * Date: 06/02/2022
 * Time: 12:51 a. m.
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;

namespace Circulos
{
	/// <summary>
	/// Description of Circle.
	/// </summary>
	public class Circle
	{
		int x_c;
		int y_c;
		int r;
		int id;
		Point center;
		
		
		public Circle(){
		
				
		
		}
		
		public Circle(int x, int y, int r, int id)
		{
			this.x_c = x;
			this.y_c = y;
			this.r = r;
			this.id = id;
			this.center.X = x;
			this.center.Y = y;
		}
		
		public override string ToString()
		{
			return string.Format("Circle {0}: X={1}, Y={2}, R={3}", id, x_c, y_c, r);
		}
		
		public int GetXCenter(){
			
			return this.x_c;
			
		}
		
		public int GetYCenter(){
			
			return this.y_c;
			
		}
		
		public int GetR(){
			
			return this.r;
			
		}
		
		public int R{
			get{
				return r;
			}
			set{
				r = value;
			}
		}
		
		public int GetId(){
			
			return this.id;
			
		}
		
		public Point Center{
			get{
				return this.center;
			}
			set{
				center = value;
			}
		}

	}
}
