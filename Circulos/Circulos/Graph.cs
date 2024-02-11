/*
 * Created by SharpDevelop.
 * User: edblo
 * Date: 21/02/2022
 * Time: 10:51 p. m.
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;

namespace Circulos
{
	/// <summary>
	/// Description of Graph.
	/// </summary>
	public class Graph
	{
		List<Vertex> vertexList = new List<Vertex>();
		
		public Graph()
		{
		}
		public void ClearList(){  //Para vaciar el grafo
		
			this.vertexList.Clear();
		
		}
		public void AddVertex(Vertex v){
		
			this.vertexList.Add(v);
			
		}
		public Vertex GetVertexAt(int index){
			return this.vertexList[index];
		}
		public bool Contains(Vertex v){
			
			return vertexList.Contains(v);
			
		}
		public int Count{
		
			get{
			
				return vertexList.Count;
				
			}
		
		}
		
	}
	
	public class Vertex
	{
		List<Edge> edgesList = new List<Edge>();
		Circle circle;
		int id;
		
		public Vertex(Circle circle, int id)
		{
			this.id = id;
			this.circle = circle;
		}
		public void AddEdge(Edge e){
		
			this.edgesList.Add(e);
		
		}
		public float GetAngleTo(int edgeId){
			if(this == edgesList[edgeId].Vertex_a)
				return edgesList[edgeId].Angle;
			
			return (float)(Math.PI + edgesList[edgeId].Angle);
		}
		public Vertex GetDestinationAt(int edgeId){
		
			if(this == edgesList[edgeId].Vertex_a)
				return edgesList[edgeId].Vertex_b;
			
			return edgesList[edgeId].Vertex_a;
		
		}
		public List<Point> GetPathAt(int edgeId){
		
			if(this == edgesList[edgeId].Vertex_a)
				return edgesList[edgeId].Path;
			
			List<Point> InvPath = new List<Point>(edgesList[edgeId].Path);
			
			InvPath.Reverse();
			
			return InvPath;
			
			
		
		}
		public List<Point> GetReturnPathAt(int edgeId){
			if(this == edgesList[edgeId].Vertex_b)
				return edgesList[edgeId].Path;
			
			List<Point> InvPath = new List<Point>(edgesList[edgeId].Path);
			
			InvPath.Reverse();
			
			return InvPath;
		}
		public int GetEdgeTo(int id){
			
			for(int i=0; i<edgesList.Count; i++){
				if(GetDestinationAt(i).Id == id)
					return i;
			}
			
			return -1;
		}
		
		public Edge GetEdgeAt(int i){
			
			return edgesList[i];
			
		}
		
		public int Id{
			get{
				return this.id;
			}
		}
		public int EdgesCount{
			get{
				return edgesList.Count;
			}
		}
		public Circle Circle{
			get{
				return this.circle;
			}
		}
		public List<Edge> EdgesList{
			get{
				return this.edgesList;
			}
			set{
				this.edgesList = value;
			}
		}
	}
	
	public class Edge
	{
		Vertex vertex_a, vertex_b;
		List<Point> path;
		int id;
		float weigth;
		float angle;
		
		public Edge(){
			
			this.id = 0;
			this.path = null;
			this.vertex_a = null;
			this.vertex_b = null;
			
			
		}
		
		public Edge(List<Point> path, Vertex v_a, Vertex v_b, int id)
		{
			this.id = id;
			this.path = path;
			
			this.vertex_a = v_a;
			this.vertex_b = v_b;
			
		}
		public List<Point> Path{
			get{
				return this.path;
			}
			
			set{ 
				this.path = value;
			}

		}
		public Vertex Vertex_a{
			get{
				return this.vertex_a;
			}
			
			set{
				this.vertex_a = value;
			}
		}
		public Vertex Vertex_b{
			get{
				return this.vertex_b;
			}
			
			set{
				this.vertex_b = value;
			}
		}
		public int Id{
			get{
				return this.id;
			}
			set{
				this.id = value;
			}
		}
		public float Weigth{
			get{
				return this.weigth;
			}
			
			set{
				this.weigth = value;
				
			}
		}
		public float Angle{
			get{
				return this.angle;
			}
			set{
				this.angle = value;
			}
		}
	}
}
