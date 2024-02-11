/*
 * Created by SharpDevelop.
 * User: edblo
 * Date: 31/03/2022
 * Time: 12:36 a. m.
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

namespace Circulos
{
	/// <summary>
	/// Description of Tree.
	/// </summary>
	public class Tree
	{
		Node root;
		
		public Tree(Vertex root)
		{
			this.root = new Node(root);
		}
		
		public void AddToGraph(Graph graph){
			
			this.root.AddToGraph(graph);
			
		}
		public Node Search(Vertex data){
			
			if(this.root.Data == data)
				return this.root;
			
			return this.root.Search(data);
			
		}
		public Node Root{
			get{
				return this.root;
			}
		}
	}
	
	public class Node
	{
		
		Vertex data;
		List<Node> sons;
		
		public Node(Vertex data){
			
			this.data = data;
			sons = new List<Node>();
			
		}
		public Vertex Data{
			get{
				return this.data;
			}
		}
		public List<Node> Sons{
			get{
				return this.sons;
			}
		}
		public Node Search(Vertex data){
			
			Node n;
			
			for(int i=0; i<sons.Count; i++){
				
				if(sons[i].Data == data)
					return sons[i];
				
				n = sons[i].Search(data);
				
				if(n != null)
					return n;
				
			}
			
			return null;
			
		}
		public void Add(Vertex son){
			
			Node n = new Node(son);
			this.sons.Add(n);
			
		}
		public void AddToGraph(Graph graph){
			
			graph.AddVertex(this.data);
			
			for(int i=0; i<sons.Count; i++){
				sons[i].AddToGraph(graph);
			}
			
		}
	}
}
