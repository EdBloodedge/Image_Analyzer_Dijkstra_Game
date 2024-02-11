/*
 * Created by SharpDevelop.
 * User: edblo
 * Date: 25/05/2022
 * Time: 04:55 p. m.
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace Circulos
{
	/// <summary>
	/// Description of DijkstraElement.
	/// </summary>
	public class DijkstraElement
	{
	 	Vertex vertex;
		float weight;
		bool definitive;
		DijkstraElement previous;
		
		public DijkstraElement(Vertex v)
		{
			this.vertex = v;
			this.definitive = false;
			this.previous = this;
			this.weight = float.MaxValue;
		}
		
		public Vertex Vertex{
			get{
				return this.vertex;
			}
			set{
				this.vertex = value;
			}
		}
		public bool Definitive{
			get{
				return definitive;
			}
			set{
				definitive = value;
			}
		}
		public DijkstraElement Previous{
			get{
				return previous;
			}
			set{
				previous = value;
			}
		}
		public float Weight{
			get{
				return weight;
			}
			set{
				weight = value;
			}
		}
	}
}
