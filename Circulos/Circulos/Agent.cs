/*
 * Created by SharpDevelop.
 * User: edblo
 * Date: 05/03/2022
 * Time: 07:00 p. m.
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;

namespace Circulos
{
	/// <summary>
	/// Description of Agent.
	/// </summary>
	public class Agent
	{
		
		int actualPosIndex;
		int actualVertexIndex;
		int nextVertex;
		int vel;
		bool stealth;
		int skillsLeft;
		
		List<int> verticesPath;
		List<int> visited;
		List<Point> path;
		
		public Agent(int originVertexIndex, Point originPos)
		{
			this.visited = new List<int>();
			this.verticesPath = new List<int>();
			this.path = new List<Point>();
			this.actualVertexIndex = originVertexIndex;
			this.actualPosIndex = 0;
			this.nextVertex = -1;
			this.vel = 20;	
			this.path.Add(originPos);
			this.stealth = false;
			this.skillsLeft = 2;
		}
		public Point GetActualPos(){
			return this.path[actualPosIndex];
		}
		public int GetNextVertex(){
			
			actualPosIndex = 0;
			
			if(verticesPath.Count > 0){
				nextVertex = VerticesPath[0];
				verticesPath.Remove(nextVertex);
			}else{
				nextVertex = -1;
			}
			
			return nextVertex;
		}
		public bool Walk(){
			
			
			if((actualPosIndex + vel) < path.Count){
				actualPosIndex += vel;
				return true;
			}
			actualPosIndex = path.Count-1;
			return false;
			
		}
		public void UseStealth(){
			
			stealth = true;
			skillsLeft--;
			
		}
		
		public int ActualPosIndex{
			get{
				return this.actualPosIndex;
			}
			set{
				this.actualPosIndex = value;
			}
		}
		public int ActualVertexIndex{
			get{
				return this.actualVertexIndex;
			}
			set{
				this.actualVertexIndex = value;
			}
		}
		public int NextVertex{
			get{
				return this.nextVertex;
			}
			set{
				this.nextVertex = value;
			}
		}
		public int PathCount{
			get{
				return this.path.Count;
			}
		}
		public List<Point> Path{
			get{
				return this.path;
			}
			set{
				this.path = value;
			}
		}
		public List<int> Visited{
		
			get{
				return this.visited;
			}
		
		}
		public int Vel{
		
			set{
				this.vel = value;
			}
		
		}
		public List<int> VerticesPath{
			get{
				return this.verticesPath;
			}
			set{
				this.verticesPath = value;
			}
		}
		public int SkillsLeft{
		
			get{
				return this.skillsLeft;
			}
			set{
				this.skillsLeft = value;
			}
		
		}
		public bool Stealth{
		
			get{
				return this.stealth;
			}
			set{
				this.stealth = value;
			}
		
		}
	}
	
	public class Predator{
		
		bool chasing;
		int sensorRadius;
		
		int actualPosIndex;
		int actualVertexIndex;
		int nextVertex;
		int vel;
		
		Circle bodyCircle;
		Circle sensorCircle;
		
		List<Point> path;
		Point[] radar;
		
		public Predator(int originVertexIndex, Point originPos)
		{
			
			this.chasing = false;
			this.path = new List<Point>();
			this.actualVertexIndex = originVertexIndex;
			this.actualPosIndex = 0;
			this.nextVertex = -1;
			this.vel = 10;	
			this.path.Add(originPos);
			this.bodyCircle = new Circle();
			this.bodyCircle.Center = originPos;
			this.bodyCircle.R = 20;
			this.sensorCircle = new Circle();
			this.sensorCircle.Center = originPos;
			this.sensorCircle.R = sensorRadius;
			radar = new Point[3];

		}
		
		public Point GetActualPos(){
			return this.path[actualPosIndex];
		}
		public bool Walk(){
			
			
			if((actualPosIndex + vel) < path.Count){
				actualPosIndex += vel;
				bodyCircle.Center = GetActualPos();
				return true;
			}
			actualPosIndex = path.Count-1;
			bodyCircle.Center = GetActualPos();
			return false;
			
		}
		
		public int ActualPosIndex{
			get{
				return this.actualPosIndex;
			}
			set{
				this.actualPosIndex = value;
			}
		}
		public int ActualVertexIndex{
			get{
				return this.actualVertexIndex;
			}
			set{
				this.actualVertexIndex = value;
			}
		}
		public int NextVertex{
			get{
				return this.nextVertex;
			}
			set{
				this.nextVertex = value;
			}
		}
		public List<Point> Path{
			get{
				return this.path;
			}
			set{
				this.path = value;
			}
			
		}
		public int Vel{
		
			get{
				return vel;
			}
			set{
				this.vel = value;
			}
		
		}
		public bool Chasing{
			get{
				return chasing;
			}
			set{
				chasing = value;
			}
		}
		public int SensorRadius{
			get{
				return sensorRadius;
			}
			set{
				sensorRadius = value;
			}
		}
		public Circle BodyCircle{
			get{
				return bodyCircle;
			}
			set{
				bodyCircle = value;
			}
		}
		public Circle SensorCircle{
			get{
				return sensorCircle;
			}
			set{
				sensorCircle = value;
			}
		}
		public Point[] Radar{
			get{
				return radar;
			}
			set{
				radar = value;
			}
		}
	}
}
