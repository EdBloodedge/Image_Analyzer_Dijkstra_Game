/*
 * Created by SharpDevelop.
 * User: edblo
 * Date: 06/02/2022
 * Time: 02:00 p. m.
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace Circulos
{
	/// <summary>
	/// Description of Distance.
	/// </summary>
	public class Distance
	{
		int C_1, C_2, distance;
		
		public Distance(){}
		
		public Distance(int C_1, int C_2, int distance)
		{
			this.C_1 = C_1;
			this.C_2 = C_2;
			this.distance = distance;
		}
		
		public int GetDistance(){
			return this.distance;
		}
		
		public int GetC1(){
			return this.C_1;
		}
		
		public int GetC2(){
			return this.C_2;
		}
		
	}
}
