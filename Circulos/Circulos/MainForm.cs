/*
 * Created by SharpDevelop.
 * User: edblo
 * Date: 06/02/2022
 * Time: 12:14 a. m.
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Circulos
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		List<Agent> AgentList;
		List<Predator> PredatorList;
		Graph CirclesGraph;
		Vertex LastMouseVertex;
		Vertex Destination;
		Bitmap bmpImage;
		Bitmap bmpAgent;
		List<Bitmap> bmpLayers;
		List<Graph> SubGraphs;
		List<Tree> KruskalTree;
		List<Tree> PrimTree;
		List<List<Edge>> KruskalEdges;
		List<List<Edge>> PrimEdges;
		
		
		bool image_selected, SelectionDrawn, Prim, Kruskal;
		int ObjectType;
		
		
		//Constructor
		
		public MainForm()
		{

			InitializeComponent();
			AgentList = new List<Agent>();
			PredatorList = new List<Predator>();
			CirclesGraph = new Graph();
			SubGraphs = new List<Graph>();
			bmpLayers = new List<Bitmap>();
			LastMouseVertex = null;
			Destination = null;
			buttonAnimation.Enabled = false;
			buttonCreateGraph.Enabled = false;
			numericUpDownVel.Enabled = false;
			numericUpDownSkills.Enabled = false;
			numericUpDownPredatorRadar.Enabled = false;
			//buttonPrim.Enabled = false;
			//buttonKruskal.Enabled = false;
			image_selected = false;
			Prim = false;
			Kruskal = false;
			PrimTree = new List<Tree>();
			KruskalTree = new List<Tree>();
			PrimEdges = new List<List<Edge>>();
			KruskalEdges = new List<List<Edge>>();
			buttonAgent.Enabled = false;
			buttonPredator.Enabled = false;
			buttonDestination.Enabled = false;
			

		}
		
		//Metodos de componentes de la ventana
		
		void PictureBoxImageMouseMove(object sender, MouseEventArgs e)
		{
			
			if(!image_selected)
					return;
			
			Brush brush = new SolidBrush(Color.DarkSlateGray);
			Graphics g = Graphics.FromImage(bmpLayers[Layer.mousemove]);
			
			Point bmpPoint;
			bmpPoint = PicBox_to_bitmap(e.X, e.Y);
			
			Vertex v = insideACircle(bmpPoint.X, bmpPoint.Y);
			Circle c;
			
			if(v!= null){
				if(v != LastMouseVertex){
					c = v.Circle;
					g.Clear(Color.Transparent);
					DrawCircle(brush, g, c);
					UpdateBitmap();
					SelectionDrawn = true;
					LastMouseVertex = v;
				}
			}
			else{
				if(SelectionDrawn){
					g.Clear(Color.Transparent);
					UpdateBitmap();
					SelectionDrawn = false;
					LastMouseVertex = null;
				}
			}
		}	
		void PictureBoxImageMouseClick(object sender, MouseEventArgs e)
		{
			if(!image_selected)
					return;
			
			
			Brush Originbrush = new SolidBrush(Color.DarkOrange);
			Brush Destinationbrush = new SolidBrush(Color.Yellow);
			Brush skinbrush = new SolidBrush(Color.Aquamarine);
			Brush predatorBrush = new SolidBrush(Color.Red);
			Brush activePredatorBrush = new SolidBrush(Color.DarkRed);
			Brush hatbrush = new SolidBrush(Color.SaddleBrown);
			Pen predatorPen = new Pen(Color.PaleVioletRed, 5);
			Pen radarPen = new Pen(Color.Red, 8);
			Graphics g = Graphics.FromImage(bmpLayers[Layer.selection]);
			Graphics a = Graphics.FromImage(bmpAgent);
			
			
			Point bmpPoint;
			bmpPoint = PicBox_to_bitmap(e.X, e.Y);
			
			Vertex v = insideACircle(bmpPoint.X, bmpPoint.Y);
			Circle c;
			
			if(v!= null){
				
				switch (ObjectType) {
					
					case 1:
						
						for(int i = 0; i<AgentList.Count; i++){
					
							if(AgentList[i].ActualVertexIndex == v.Id){
						
								AgentList.Remove(AgentList[i]);
								DrawShortestPaths();
								
								
								if(AgentList.Count == 0)
									buttonAnimation.Enabled = false;
								
								DrawAgents(a, skinbrush, hatbrush, predatorBrush, activePredatorBrush, predatorPen, radarPen);
								return;
						
							}
					
						}
				
						if(v == Destination){
							return;
						}
						
						for(int i=0; i<PredatorList.Count; i++){
							if(PredatorList[i].ActualVertexIndex == v.Id)
								return;
						}
				
						AgentList.Add(new Agent(v.Id, v.Circle.Center));
						
						if(Destination != null)
									buttonAnimation.Enabled = true;
						
						
						DrawAgents(a, skinbrush, hatbrush, predatorBrush, activePredatorBrush, predatorPen, radarPen);
						
						DrawShortestPaths();
						
						break;
						
					case 2:
						
						for(int i = 0; i<PredatorList.Count; i++){
					
							if(PredatorList[i].ActualVertexIndex == v.Id){
						
								PredatorList.Remove(PredatorList[i]);
								
								DrawAgents(a, skinbrush, hatbrush, predatorBrush, activePredatorBrush, predatorPen, radarPen);
								return;
						
							}
					
						}
				
						if(v == Destination){
							return;
						}
						
						for(int i =0; i<AgentList.Count; i++){
							if(AgentList[i].ActualVertexIndex == v.Id)
								return;
						}
				
						PredatorList.Add(new Predator(v.Id, v.Circle.Center));
					
						DrawAgents(a, skinbrush, hatbrush, predatorBrush, activePredatorBrush, predatorPen, radarPen);
						break;
					
					case 3:
						
						for(int i = 0; i<AgentList.Count; i++)
							if(AgentList[i].ActualVertexIndex == v.Id)
								return;
						
						for(int i =0; i<PredatorList.Count; i++){
							if(PredatorList[i].ActualVertexIndex == v.Id)
								return;
						}

				
						if(v == Destination){
							g.Clear(Color.Transparent);
							Destination = null;
							buttonAnimation.Enabled = false;
							DrawShortestPaths();
						}else{
							Destination = v;
							c = Destination.Circle;
							g.Clear(Color.Transparent);
							DrawCircle(Destinationbrush, g, c);
							
							if(AgentList.Count > 0){
								buttonAnimation.Enabled = true;
								DrawShortestPaths();
							}
						}
						
						break;
						
						
				}
				
				
				/*if(Origin != null){
					if(Destination != null){
						
						if(v == Destination)
							return;
						
						Origin = Destination;
						Destination = v;
						g.Clear(Color.Transparent);
						
						c = Origin.Circle;
						DrawCircle(Originbrush, g, c);
						labelDestination.Text = "Origen: " + c.GetId();
						
						
						c = Destination.Circle;
						DrawCircle(Destinationbrush, g, c);
						labelDestination.Text += "\nDestino: " + c.GetId();
						
					}
					else{
						
						if(v == Origin)
							return;
						
						Destination = v;
						c = Destination.Circle;
						DrawCircle(Destinationbrush, g, c);
						labelDestination.Text += "\nDestino: " + c.GetId();
						buttonAnimation.Enabled = true;  //Permite usar el boton
						numericUpDownVel.Enabled = true;
					}
				}
				else{
					Origin = v;
					c = Origin.Circle;
					DrawCircle(Originbrush, g, c);
					labelDestination.Text = "Origen: " + c.GetId();
					
				}*/

				UpdateBitmap();
				
			}
		}		
		void ButtonSelectionImageClick(object sender, EventArgs e)
		{
			openFileDialog1.ShowDialog();
			bmpImage = new Bitmap(openFileDialog1.FileName);
			bmpAgent = new Bitmap(bmpImage);
			
			/*DebugForm DebugWindow = new DebugForm(bmpImage);
			DebugWindow.ShowDialog();*/
			
			pictureBoxImage.BackgroundImage = bmpImage;
			pictureBoxImage.Refresh();
			pictureBoxImage.Image = bmpAgent;
			
			CirclesGraph.ClearList();
			SubGraphs.Clear();
			treeViewGraph.Nodes.Clear();
			//labelDestination.Text = "";
			labelInfo.Text = "";
			
			buttonCreateGraph.Enabled = true;
			buttonAnimation.Enabled = false;
			numericUpDownVel.Enabled = false;
			numericUpDownSkills.Enabled = false;
			numericUpDownPredatorRadar.Enabled = false;
			//buttonPrim.Enabled = false;
			//buttonKruskal.Enabled = false;
			image_selected = true;
			SelectionDrawn = false;
			
			Prim = false;
			Kruskal = false;
			
			LastMouseVertex = null;
			Destination = null;
			
			bmpLayers.Clear();
			
			Graphics g;
			
			g = Graphics.FromImage(bmpAgent);
			g.Clear(Color.Transparent);
			
			bmpLayers.Add(new Bitmap(bmpImage));
			
			for(int i=1; i<5; i++){
				bmpLayers.Add(new Bitmap(bmpImage.Width, bmpImage.Height));
				
				/*DebugForm DebW2 = new DebugForm(bmpLayers[i]);
				DebW2.ShowDialog();
				
				if(i != Layer.image){
					g = Graphics.FromImage(bmpLayers[i]);
					g.Clear(Color.Transparent);
				}*/
				
				
			}
			
		}
		void ButtonCreateGraphClick(object sender, EventArgs e)
		{
			
			Color color;
			
			for(int y = 1; y < bmpImage.Height; y++){   //recorrer el bitmap
				for(int x = 1; x < bmpImage.Width; x++){  
				
					color = bmpLayers[Layer.image].GetPixel(x,y);
					if(isBlack(color))		//encontrar un circulo (pixel negro)
					   FindCenter(x, y);		
					

				}
			}
			
			KruskalTree.Clear();
			PrimTree.Clear();
			CreateGraph();
			CreateSubGraphs();
			DrawEdges();
			DrawId();
			
			//CreateKruskal();
			//CreatePrim();
			
			TreeNode vertexNode;
			TreeNode edgeNode;
			Vertex auxVertex;
			
			AgentList.Clear();
			PredatorList.Clear();
			Destination = null;
			
			for(int i = 0; i < CirclesGraph.Count; i++){
				
				auxVertex = CirclesGraph.GetVertexAt(i);
				vertexNode = new TreeNode((i+1).ToString());
				
				for(int j = 0; j < auxVertex.EdgesCount; j++){
					edgeNode = new TreeNode(auxVertex.GetDestinationAt(j).Id.ToString());
					vertexNode.Nodes.Add(edgeNode);
				}
				
				treeViewGraph.Nodes.Add(vertexNode);
			}
				
			buttonCreateGraph.Enabled = false;
			//buttonPrim.Enabled = true;
			//buttonKruskal.Enabled = true;
			buttonAgent.Enabled = true;
			buttonPredator.Enabled = true;
			buttonDestination.Enabled = true;
			numericUpDownVel.Enabled = true;
			numericUpDownSkills.Enabled = true;
			numericUpDownPredatorRadar.Enabled = true;
			ObjectType = 0;
			labelInfo.Text = "Seleccione dos vértices.";
			
			
		}
		void ButtonAnimationClick(object sender, EventArgs e)
		{
			Graphics g = Graphics.FromImage(bmpLayers[Layer.ids]);
			g.Clear(Color.Transparent);
			UpdateBitmap();
			
			g = Graphics.FromImage(bmpAgent);
			                                          
			Graphics a = Graphics.FromImage(bmpLayers[Layer.selection]);
			Brush skinbrush = new SolidBrush(Color.Aquamarine);
			Brush hatbrush = new SolidBrush(Color.SaddleBrown);
			Brush predatorBrush = new SolidBrush(Color.Red);
			Brush activePredatorBrush = new SolidBrush(Color.DarkRed);
			Pen predatorPen = new Pen(Color.PaleVioletRed, 5);
			Pen radarPen = new Pen(Color.Red, 8);
			
			Random rand = new Random(DateTime.Now.Millisecond);
			
			List<int> AvailableVertices = new List<int>();
			int NextVertex, index;
			float bestAngle, auxAngle;
			float theta, dx, dy;
			Point[] Radar = new Point[3];
			Point DoofPos, PerryPos;
			
			bool animate = true;
			Vertex AgentVertex;
			
			Agent Perry;
			Predator Doof;
			
			
			
			for(int i = 0; i<AgentList.Count; i++){
				
				Perry = AgentList[i];
				
				Perry.VerticesPath.Clear();
				FindDijkstra(Perry);
				Perry.Vel = (int)numericUpDownVel.Value;
				Perry.SkillsLeft = (int)numericUpDownSkills.Value;
				Perry.GetNextVertex();
				Perry.Path = new List<Point>();
				Perry.Path.Add(CirclesGraph.GetVertexAt(Perry.ActualVertexIndex-1).Circle.Center);
			}
			
			for(int i=0; i<PredatorList.Count; i++){
				
				Doof = PredatorList[i];
				
				Doof.Vel = (int)numericUpDownVel.Value/4;
				Doof.SensorRadius = (int)numericUpDownPredatorRadar.Value;
				Doof.SensorCircle.R = Doof.SensorRadius;
				Doof.SensorCircle.Center = Doof.GetActualPos();
				
				if(Doof.ActualPosIndex == 0){
					AgentVertex = CirclesGraph.GetVertexAt(Doof.ActualVertexIndex - 1);
					NextVertex = rand.Next(0, AgentVertex.EdgesList.Count);
	
					Doof.NextVertex = AgentVertex.GetDestinationAt(NextVertex).Id;
					Doof.Path = CirclesGraph.GetVertexAt(Doof.ActualVertexIndex-1).GetPathAt(NextVertex);
				
				}
			}
			
			
			
			while(animate){
				
				animate = false;
				
				for(int i=0; i<AgentList.Count; i++){
				
					if(AgentList[i].NextVertex != -1)
						animate = true;
					
					if(AgentList[i].ActualVertexIndex == Destination.Id)
					   animate = true;
					
				}
				
				if(!animate){
					buttonAnimation.Enabled = false;
					return;
				}
				
				if(AgentList.Count == 0){
					buttonAnimation.Enabled = false;
					return;
				}
				
				for(int i=0; i<AgentList.Count; i++){
					
					Perry = AgentList[i];
					
					if(Perry.ActualVertexIndex == Destination.Id){//Si el agente llego al destino, podemos salir
						animate = false;
						Perry.Path = new List<Point>();
						Perry.Path.Add(CirclesGraph.GetVertexAt(Perry.ActualVertexIndex-1).Circle.Center);
					
						a.Clear(Color.Transparent);
						buttonAnimation.Enabled = false;
						UpdateBitmap();
					
					}else if(!Perry.Walk()){ //Si el agente ya no puede caminar...
						
						if(Perry.Stealth){
							Perry.Stealth = false;
						}
						
						if(Perry.NextVertex != -1){ //Y aun tiene un vertice siguiente
							Perry.ActualVertexIndex = AgentList[i].NextVertex; //Significa que llego al vertice, entonces se actualiza su posicion
							Perry.GetNextVertex(); //Y se obtiene un nuevo vertice siguiente
							
							Perry.Path = new List<Point>(); //Ademas de tambien limpiar el path
							Perry.Path.Add(CirclesGraph.GetVertexAt(AgentList[i].ActualVertexIndex-1).Circle.Center);//Y agregar unicamente su posicion actual real

							if(Perry.NextVertex != -1){ //Si este nuevo vertice siguiente existe
								AgentVertex = CirclesGraph.GetVertexAt(Perry.ActualVertexIndex-1); //Obtenemos el vertice en el que se encuentra el agente...
								Perry.Path = AgentVertex.GetPathAt(AgentVertex.GetEdgeTo(Perry.NextVertex));//Para obtener el path hacia el proximo vertice
							}
						}
						
					}
					
				}
				
				for(int i=0; i<PredatorList.Count; i++){
					
					Doof = PredatorList[i];
					AvailableVertices.Clear();
					
					if(Doof.Chasing){
						Doof.Vel = (int)((float)numericUpDownVel.Value*(float)1.2);
						if(Doof.Vel == numericUpDownVel.Value)
							Doof.Vel = Doof.Vel +1;
					}else
						Doof.Vel = (int)numericUpDownVel.Value/4;
					
					if(!Doof.Walk()){
						
						Doof.ActualPosIndex = 0;
						
						Doof.ActualVertexIndex = Doof.NextVertex;
						
						AgentVertex = CirclesGraph.GetVertexAt(Doof.ActualVertexIndex - 1);
						NextVertex = rand.Next(0, AgentVertex.EdgesList.Count);

						Doof.NextVertex = AgentVertex.GetDestinationAt(NextVertex).Id;
						Doof.Path = CirclesGraph.GetVertexAt(Doof.ActualVertexIndex-1).GetPathAt(NextVertex);
						
					}
					
					Doof.Chasing = false;
					
					Doof.SensorCircle.Center = Doof.GetActualPos();
					
					Perry = OverAPrey(Doof);
					
					if(Perry != null){
						
						PerryPos = Perry.GetActualPos();
						
						if(insideACircle(PerryPos.X, PerryPos.Y) == null){
							if(Perry.SkillsLeft == 0){
								AgentList.Remove(Perry);
							}
							else{
								Perry.UseStealth();
							}
						}
					}
					
					Perry = PreyInRange(Doof);
					
					if(Perry != null){
						
						if(Perry.SkillsLeft == 0){
							
							Doof.Chasing = true;
							
							AgentVertex = CirclesGraph.GetVertexAt(Doof.ActualVertexIndex - 1);
							
							bestAngle = 4;
							
							DoofPos = Doof.GetActualPos();
							PerryPos = Perry.GetActualPos();
									
							dx = PerryPos.X - DoofPos.X;
							dy = PerryPos.Y - DoofPos.Y;
									
							theta = (float)Math.Atan2(dy, dx);
							
							if(theta < 0)
								theta += (float) (2*Math.PI);
							
							Doof.Radar[0].X = (int)(40*Math.Cos((double)theta)) + DoofPos.X;
							Doof.Radar[1].X = (int)(30*Math.Cos((double)(theta+0.2))) + DoofPos.X;
							Doof.Radar[2].X = (int)(30*Math.Cos((double)(theta-0.2))) + DoofPos.X;
											
							Doof.Radar[0].Y = (int)(40*Math.Sin((double)theta)) + DoofPos.Y;
							Doof.Radar[1].Y = (int)(30*Math.Sin((double)(theta+0.2))) + DoofPos.Y;
							Doof.Radar[2].Y = (int)(30*Math.Sin((double)(theta-0.2))) + DoofPos.Y;
								
							if(Doof.ActualPosIndex == 0){
								
								for(int j=0; j<AgentVertex.EdgesCount; j++){ //Agrega todos los vertices disponibles desde el actual
									AvailableVertices.Add(j);
								}
								
								if(AvailableVertices.Count >= 0){
									
									index = 0;
								
									for(int j=0; j<=AvailableVertices.Count-1; j++){
											
										auxAngle = AgentVertex.GetAngleTo(AvailableVertices[j]);
										
										if(auxAngle < theta){
											auxAngle = theta-auxAngle;
										}else{
											auxAngle -= theta;
										}
										
										if(auxAngle > Math.PI)
											auxAngle = (float)((2*Math.PI) - auxAngle);
										
										if(auxAngle < bestAngle){
											bestAngle = auxAngle;
											index = AvailableVertices[j];
										
										}
		
									}
									
									NextVertex = AvailableVertices[index]; //Elije el proximo vertice
									Doof.Path = AgentVertex.GetPathAt(NextVertex); //Toma el nuevo path
									Doof.NextVertex = AgentVertex.GetDestinationAt(NextVertex).Id;
								}
							}
						}else{
							Perry.UseStealth();
						}
						
					}
				}
				
				DrawAgents(g, skinbrush, hatbrush, predatorBrush, activePredatorBrush, predatorPen, radarPen);
				pictureBoxImage.Refresh();
				
			}
			
			Destination = null;
			animate = true;
			
			for(int i = 0; i<AgentList.Count; i++)
				AgentList[i].VerticesPath.Clear(); //Eliminamos el camino de vertices para que ya no sigan avanzando tras terminar su path actual
			
			while(animate){ //Y hacemos que sigan caminando hasta que todos hayan llegado a su destino proximo
				
				animate = false; //Predisponemos que ya no avanzara ningun agente...
				
				for(int i = 0; i<AgentList.Count; i++){
					if(AgentList[i].Walk()){ //Pero si alguno puede caminar, entonces la simulacion debe seguir.
						animate = true;
					}
					
				}
				
				DrawAgents(g, skinbrush, hatbrush, predatorBrush, activePredatorBrush, predatorPen, radarPen);
				pictureBoxImage.Refresh();
				
			}
			
			for(int i=0; i<AgentList.Count; i++){
				
				Perry = AgentList[i];
				
				if(Perry.NextVertex != -1){
					Perry.ActualVertexIndex = Perry.NextVertex;
					Perry.GetNextVertex();
				}
				Perry.Path = new List<Point>();
				Perry.Path.Add(CirclesGraph.GetVertexAt(Perry.ActualVertexIndex-1).Circle.Center);
			}
			
			bool AgentInVertex; //Para saber si existe ya un agente en un vertice.
			
			for(int i=0; i<CirclesGraph.Count; i++){ //Para cada vertice del grafo...
				
				AgentInVertex = false; //Suponemos que no tiene un agente aun.
				
				for(int j=0; j<AgentList.Count; j++){ //Despues comprobamos todos los agentes.
					
					if(AgentList[j].ActualVertexIndex == i+1){ //Si el vertice del agente coincide con el que estamos revisando en el grafo
						
						if(AgentInVertex) //Si ya se tenia un agente en el vertice, el agente duplicado se elimina
							AgentList.Remove(AgentList[j]);
						else{
							AgentInVertex = true; //Si no, se marca que ya se encontro un agente en el vertice, por si se encuentra algun duplicado
						}
						
					}
				}
				
				AgentInVertex = false; //Suponemos que no tiene un agente aun.
				
				for(int j=0; j<PredatorList.Count; j++){ //Despues comprobamos todos los agentes.
					
					if(PredatorList[j].ActualVertexIndex == i+1){ //Si el vertice del agente coincide con el que estamos revisando en el grafo
						
						if(AgentInVertex) //Si ya se tenia un agente en el vertice, el agente duplicado se elimina
							PredatorList.Remove(PredatorList[j]);
						else{
							AgentInVertex = true; //Si no, se marca que ya se encontro un agente en el vertice, por si se encuentra algun duplicado
						}
						
					}
				}
				
			}
		
		}
		void ButtonPrimClick(object sender, EventArgs e)
		{
			
			DrawEdges();
			
			Graphics g = Graphics.FromImage(bmpLayers[Layer.edges]);
			Pen kruskalPen = new Pen(Color.Orange, 5);
			Pen primPen = new Pen(Color.Brown, 10);
			
			if(Prim){
				Prim = false;
			}else{
				Prim = true;
				for(int i=0; i<PrimEdges.Count; i++){
					for(int j=0; j<PrimEdges[i].Count; j++){
						g.DrawLine(primPen, PrimEdges[i][j].Vertex_a.Circle.Center,PrimEdges[i][j].Vertex_b.Circle.Center);
					}
				}
			}
			
			if(Kruskal)
				for(int i=0; i<KruskalEdges.Count; i++){
					for(int j=0; j<KruskalEdges[i].Count; j++){
						g.DrawLine(kruskalPen, KruskalEdges[i][j].Vertex_a.Circle.Center,KruskalEdges[i][j].Vertex_b.Circle.Center);
					}
				}
			
			UpdateBitmap();
			
		}
		void ButtonKruskalClick(object sender, EventArgs e)
		{

			DrawEdges();
			
			Graphics g = Graphics.FromImage(bmpLayers[Layer.edges]);
			Pen kruskalPen = new Pen(Color.Orange, 5);
			Pen primPen = new Pen(Color.Brown, 10);
			
			if(Prim){
				for(int i=0; i<PrimEdges.Count; i++){
					for(int j=0; j<PrimEdges[i].Count; j++){
						g.DrawLine(primPen, PrimEdges[i][j].Vertex_a.Circle.Center,PrimEdges[i][j].Vertex_b.Circle.Center);
					}
				}
					//CreatePrim();
			}
			
			if(Kruskal){
				Kruskal = false;
			}else{
				Kruskal = true;
				for(int i=0; i<KruskalEdges.Count; i++){
					for(int j=0; j<KruskalEdges[i].Count; j++){
						g.DrawLine(kruskalPen, KruskalEdges[i][j].Vertex_a.Circle.Center,KruskalEdges[i][j].Vertex_b.Circle.Center);
					}
				}
			}
			
			UpdateBitmap();
				
			
		}
		void ButtonAgentClick(object sender, EventArgs e)
		{
			buttonAgent.Enabled = false;
			buttonPredator.Enabled = true;
			buttonDestination.Enabled = true;
			ObjectType = 1;
			
		}
		void ButtonPredatorClick(object sender, EventArgs e)
		{
			buttonAgent.Enabled = true;
			buttonPredator.Enabled = false;
			buttonDestination.Enabled = true;
			ObjectType = 2;
		}
		void ButtonDestinationClick(object sender, EventArgs e)
		{
			buttonAgent.Enabled = true;
			buttonPredator.Enabled = true;
			buttonDestination.Enabled = false;
			ObjectType = 3;
		}
		//Metodos ajenos a la ventana
		
		void UpdateBitmap(){
		
			Graphics g = Graphics.FromImage(bmpImage);
			g.Clear(Color.Transparent);
			
			for(int i=0; i<bmpLayers.Count; i++)			
				g.DrawImage(bmpLayers[i], 0, 0, bmpImage.Width, bmpImage.Height);
				
			pictureBoxImage.Refresh();
			
		
		}
		void FindCenter(int x, int y){
			
			int xCenter, yCenter, xRight, xLeft, ySup, yInf;
			Vertex v;
			
			if((insideACircle(x, y)) != null)
				return;
			
			ySup = y;
			yInf = y;
			
			while(!isWhite(bmpLayers[Layer.image].GetPixel(x,yInf))) //encontrar centro
				yInf++;
			
			while(!isWhite(bmpLayers[Layer.image].GetPixel(x,ySup))) //encontrar centro
				ySup--;
			
			yCenter = (ySup + yInf)/2;
			
			xRight = x;
			xLeft = x;
			
			while(!isWhite(bmpLayers[Layer.image].GetPixel(xRight,yCenter)))
				xRight++;
			
			while(!isWhite(bmpLayers[Layer.image].GetPixel(xLeft,yCenter)))
				xLeft--;
			
			xCenter = (xRight+xLeft)/2;
			
			Circle c = new Circle(xCenter, yCenter, (yInf-yCenter), CirclesGraph.Count+1);
			v = new Vertex(c, c.GetId());
			CirclesGraph.AddVertex(v);

			
		}		
		void CreateGraph(){
			
			Vertex a, b;
			Edge auxEdge;
			int id = 0;
			
			for(int i = 0; i < CirclesGraph.Count; i++){
				
				for(int j = i+1; j<CirclesGraph.Count; j++){
					
					a = CirclesGraph.GetVertexAt(i);
					b = CirclesGraph.GetVertexAt(j);
					auxEdge = CreatePath(a.Circle, b.Circle);
					
					if(auxEdge != null){
						auxEdge.Vertex_a = a;						
						auxEdge.Vertex_b = b;
						auxEdge.Id = id++;
						CirclesGraph.GetVertexAt(i).AddEdge(auxEdge);
						CirclesGraph.GetVertexAt(j).AddEdge(auxEdge);
					}
					
				}
				
			}
			
		}
		void CreateSubGraphs(){
			
			List<Vertex> Checked = new List<Vertex>();
			List<Tree> GraphTrees = new List<Tree>();
			
			Queue<Vertex> Vertices = new Queue<Vertex>();
			
			Vertex auxVertex = null;
			int j = 0;
			
			for(int i = 0; i<CirclesGraph.Count; i++){
				auxVertex = CirclesGraph.GetVertexAt(i);
				
				if(!Checked.Contains(auxVertex)){
					
					GraphTrees.Add(new Tree(auxVertex));
					Vertices.Enqueue(auxVertex);
					Checked.Add(auxVertex);
					BFS(Checked, Vertices, GraphTrees[j++]);            
					
				}
			}
			
			for(int i=0; i<GraphTrees.Count; i++){
	
				SubGraphs.Add(new Graph());
				GraphTrees[i].AddToGraph(SubGraphs[i]);
			}
		}
		
		void CreateKruskal(){
			
			List<Edge> Edges = new List<Edge>();
			Vertex auxVertex;
			Edge auxEdge;
			float minor;
			List<List<Vertex>> CCs = new List<List<Vertex>>();
			
			KruskalEdges.Clear();
			
			for(int k = 0;  k<SubGraphs.Count; k++){
				
				Edges.Clear();
				KruskalEdges.Add(new List<Edge>());
				CCs.Clear();
				
				for(int i=0; i< SubGraphs[k].Count; i++){
					
					auxVertex = SubGraphs[k].GetVertexAt(i);
					
					for(int j = 0; j<auxVertex.EdgesCount; j++){
					
						if(!Edges.Contains(auxVertex.EdgesList[j]))
							Edges.Add(auxVertex.EdgesList[j]);
						
					}
					
					CCs.Add(new List<Vertex>());
					CCs[i].Add(auxVertex);
					
				}
				
				while (KruskalEdges[k].Count != SubGraphs[k].Count - 1) { //solucion
					
					auxEdge = Edges[0];
					minor = auxEdge.Weigth;
					
					for (int i = 1; i < Edges.Count; i++) { //seleccion
						
						if (Edges[i].Weigth < minor) {
							auxEdge = Edges[i];
							minor = auxEdge.Weigth;
						}
						
					}
					
					Edges.Remove(auxEdge);
					
					for (int i = 0; i < CCs.Count; i++) { //buscaCCde v_1
						if (CCs[i].Contains(auxEdge.Vertex_a)) {
							
							for (int j = 0; j < CCs.Count; j++) { //buscaCCde v_2
								if (CCs[j].Contains(auxEdge.Vertex_b)) {
									
									if (j != i) {
										
										KruskalEdges[k].Add(auxEdge); //prometedor = prometedor U a
										CCs[i].AddRange(CCs[j]);  //fusionaCC
										CCs.Remove(CCs[j]);
										break;
										
									}
								}
							}				
						}				
					}		
				}	
			}
		}
		void CreatePrim(){
			
			Vertex auxVertex, v_1, v_2;
			Edge auxEdge;
			float minor;
			List<Edge> Edges = new List<Edge>();
			List<Vertex> S = new List<Vertex>();
			
			PrimEdges.Clear();
			
			for(int k=0; k<SubGraphs.Count; k++){ //Para cada subgrafo
				
				Edges.Clear();
				S.Clear();
				PrimEdges.Add(new List<Edge>());
				
				auxVertex = SubGraphs[k].GetVertexAt(0);
				
				for(int i=0; i<auxVertex.EdgesCount; i++){
					
					Edges.Add(auxVertex.EdgesList[i]);
					
				}
				
				S.Add(auxVertex);
				
				while(PrimEdges[k].Count != SubGraphs[k].Count-1){ //Solucion
					
					auxEdge = Edges[0];
					minor = auxEdge.Weigth;
					
					for (int i = 1; i < Edges.Count; i++) { //seleccion
						
						if (Edges[i].Weigth < minor) {
							auxEdge = Edges[i];
							minor = auxEdge.Weigth;
						}
						
					}
					
					Edges.Remove(auxEdge);
					
					v_1 = auxEdge.Vertex_a;
					v_2 = auxEdge.Vertex_b;
					
					if((S.Contains(v_1) && !S.Contains(v_2)) || !S.Contains(v_1) && S.Contains(v_2)){
						//factible
						
						PrimEdges[k].Add(auxEdge); //prometedor U a
						
						if(S.Contains(v_1)){
							S.Add(v_2);
							
							for(int i=0; i<v_2.EdgesCount; i++){ //Candidatos U aristas de V2
								if(!S.Contains(v_2.GetDestinationAt(i)))
									Edges.Add(v_2.EdgesList[i]);
							}
						}else{
							S.Add(v_1);
							
							for(int i=0; i<v_1.EdgesCount; i++){
								if(!S.Contains(v_1.GetDestinationAt(i))){
								   	Edges.Add(v_1.EdgesList[i]);
								}
							}
						}		
					}	
				}
			}
		}
		void FindDijkstra(Agent agent){
			
			Graph graph = null;
			List<DijkstraElement> DijkstraList= new List<DijkstraElement>();
			DijkstraElement Selected;
			float newWeight;
			int edgeId;
			
			
			for(int i=0; i<SubGraphs.Count; i++){
				
				for(int j=0; j<SubGraphs[i].Count; j++){
					
					if(agent.ActualVertexIndex == SubGraphs[i].GetVertexAt(j).Id)
						graph = SubGraphs[i];
					
				}
				
			}
			
			if(graph.Contains(Destination)){
				
				//inicializa
				
				Selected = null;
				
				for(int i=0; i<graph.Count; i++){
					DijkstraList.Add(new DijkstraElement(graph.GetVertexAt(i)));
					
					if(graph.GetVertexAt(i).Id == agent.ActualVertexIndex)
						DijkstraList[i].Weight = 0;
				}
				
				for(int i=0; i<DijkstraList.Count; i++){ //Comienza el ciclo
					
					for(int j=0; j<DijkstraList.Count; j++){
						if(!DijkstraList[j].Definitive){
							Selected = DijkstraList[j];
							break;
						}
						
					}
					
					for(int j=0; j<DijkstraList.Count; j++){ //Selecciona candidato
						if(DijkstraList[j].Weight < Selected.Weight && !DijkstraList[j].Definitive)
							Selected = DijkstraList[j];
					}
					
					for(int j=0; j<DijkstraList.Count; j++){ //Actualiza pesos
						
						edgeId = Selected.Vertex.GetEdgeTo(DijkstraList[j].Vertex.Id);
						
						if(edgeId >= 0){
							
							newWeight = Selected.Weight + Selected.Vertex.GetEdgeAt(edgeId).Weigth;
							
							if(newWeight < DijkstraList[j].Weight){
								
								DijkstraList[j].Weight = newWeight;
								DijkstraList[j].Previous = Selected;
								
							}
						}
					}
					
					Selected.Definitive = true; //Cambia a definitivo
					
				}
				
				
				//Regresa el camino
				
				for(int i=0; i<DijkstraList.Count; i++)
					if(DijkstraList[i].Vertex == Destination){
					
						Selected = DijkstraList[i];
					
						while(Selected.Previous != Selected){
							
							agent.VerticesPath.Add(Selected.Vertex.Id);
							Selected = Selected.Previous;
							
						}
						
						
						agent.VerticesPath.Add(Selected.Vertex.Id);
						agent.VerticesPath.Reverse();
					
					}
						
						
				
				
			}else{
				
				agent.VerticesPath.Clear();
				
			}
			
		}
		
		void DrawId(){
			
			/*Graphics e = Graphics.FromImage(bmpLayers[Layer.ids]);
			Brush brocha = new SolidBrush(Color.GreenYellow);
			Font fuente = new Font("Arial", 25);
			int x, y, r;
			
			StringFormat format = new StringFormat();
			format.LineAlignment = StringAlignment.Center;
			format.Alignment = StringAlignment.Center;
			Rectangle box;
			
			for(int i = 0; i<CirclesGraph.Count; i++){  //marcar id
				x = CirclesGraph.GetVertexAt(i).Circle.GetXCenter();
				y = CirclesGraph.GetVertexAt(i).Circle.GetYCenter();
				r = CirclesGraph.GetVertexAt(i).Circle.GetR();
				box = new Rectangle(x-r*2, y-r*2, r*4, r*4);
				e.DrawString((CirclesGraph.GetVertexAt(i).Circle.GetId().ToString()), fuente, brocha, box, format);
			}
			UpdateBitmap();*/
		}
		void DrawEdges(){
			
			Point pt1;
			Point pt2;
			
			Pen pen = new Pen(Color.Green, 5);
			Brush brush = new SolidBrush(Color.Black);
			
			Graphics g = Graphics.FromImage(bmpLayers[Layer.edges]);
			
			g.Clear(Color.Transparent);
			
			for(int i=0; i<CirclesGraph.Count; i++){
				for(int j=i+1; j<CirclesGraph.Count; j++){
					
					if(CirclesGraph.GetVertexAt(i).GetEdgeTo(j+1) != -1){
					
						pt1 = CirclesGraph.GetVertexAt(i).Circle.Center;
						pt2 = CirclesGraph.GetVertexAt(j).Circle.Center;
					
						g.DrawLine(pen, pt1, pt2);
						
					}
					
				}
			}
			
			Circle c;
			
			for(int i=0; i<CirclesGraph.Count; i++){
				
				c = CirclesGraph.GetVertexAt(i).Circle;
				
				g.FillEllipse(brush, c.GetXCenter()-c.GetR(), c.GetYCenter()-c.GetR(), c.GetR()*2, c.GetR()*2);
				
			}
			
			UpdateBitmap();
			
		}
		void DrawCircle(Brush brush,Graphics g, Circle c){
			
			int x, y, r;
			
			x = c.Center.X;
			y = c.Center.Y;
			r = c.GetR();
			g.FillEllipse(brush, x-r, y-r, r*2, r*2);
			
		}
		void DrawAgent(Graphics g, Point AgentPos, Point[] Radar, Brush skinbrush, Brush hatbrush, Pen radarPen){
			
			g.Clear(Color.Transparent);
			g.DrawPolygon(radarPen, Radar);
			g.FillEllipse(skinbrush, AgentPos.X-15, AgentPos.Y-15, 30, 30);
			g.FillEllipse(hatbrush, AgentPos.X-10, AgentPos.Y-10, 20, 20);
			
			pictureBoxImage.Refresh();
		}
		
		void DrawAgents(Graphics g, Brush skinBrush, Brush hatBrush, Brush predatorBrush, Brush activePredatorBrush, Pen predatorPen, Pen radarPen){
			
			g.Clear(Color.Transparent);
			Point AgentPos;
			
			int R;
			
			for(int i = 0; i<AgentList.Count; i++){
				
				AgentPos = AgentList[i].Path[AgentList[i].ActualPosIndex];
			
		
				g.FillEllipse(skinBrush, AgentPos.X-15, AgentPos.Y-15, 30, 30);
				
				if(!AgentList[i].Stealth)
					g.FillEllipse(hatBrush, AgentPos.X-10, AgentPos.Y-10, 20, 20);
				
			}
			
			for(int i=0; i<PredatorList.Count; i++){
				
				AgentPos = PredatorList[i].Path[PredatorList[i].ActualPosIndex];
				R = PredatorList[i].SensorCircle.R;
				
				if(PredatorList[i].Chasing){
					g.FillEllipse(activePredatorBrush, AgentPos.X-20, AgentPos.Y-20, 40, 40);
					g.DrawEllipse(radarPen, AgentPos.X-R, AgentPos.Y-R, R*2, R*2);
					g.DrawPolygon(radarPen, PredatorList[i].Radar);
				}else{
					g.FillEllipse(predatorBrush, AgentPos.X-20, AgentPos.Y-20, 40, 40);
					g.DrawEllipse(predatorPen, AgentPos.X-R, AgentPos.Y-R, R*2, R*2);
				}
				
			}
			
			pictureBoxImage.Refresh();
			
		}
		
		bool Explore(Vertex root, Agent Perry){
			
			Graphics g = Graphics.FromImage(bmpAgent);
			Brush skinbrush = new SolidBrush(Color.Aquamarine);
			Brush hatbrush = new SolidBrush(Color.SaddleBrown);
			Pen radarPen = new Pen(Color.Red, 8);
			
			List<int> AvailableVertices = new List<int>();
			int NextVertex, index;
			float theta, dx, dy;
			Point AgentPos = new Point();
			Point DestPos = new Point();
			Point[] Radar = new Point[3];
			
			bool visited;
			
			float bestAngle = 4, auxAngle;
			
			Perry.Visited.Add(root.Id); //Agrega el origen a los visitados
			Perry.ActualVertexIndex = root.Id; //Pone al origen como al vertice actual
			
			if(Perry.ActualVertexIndex == Destination.Id){ //Revisa si ya llego al destino
				Radar[0].Y = Perry.GetActualPos().Y - 30;
				Radar[0].X = Perry.GetActualPos().X;
						
				Radar[1].Y = Perry.GetActualPos().Y - 35;
				Radar[1].X = Perry.GetActualPos().X + 5;
						
				Radar[2].Y = Perry.GetActualPos().Y - 35;
				Radar[2].X = Perry.GetActualPos().X - 5;
						
				DrawAgent(g, Perry.GetActualPos(), Radar, skinbrush, hatbrush, radarPen);
				return true;
			}
			
			for(int i=0; i<root.EdgesCount; i++){ //Agrega todos los vertices disponibles desde el actual
				AvailableVertices.Add(i);
			}
			
			DestPos = Destination.Circle.Center; // Encuentra la posicion del vertice destino, para el radar
			AgentPos = root.Circle.Center;
					
			dx = DestPos.X - AgentPos.X;
			dy = DestPos.Y - AgentPos.Y;
					
			theta = (float)Math.Atan2(dy, dx);
			
			if(theta < 0)
				theta += (float) (2*Math.PI);
			
			Radar[0].X = (int)(40*Math.Cos((double)theta)) + AgentPos.X;
			Radar[1].X = (int)(30*Math.Cos((double)(theta+0.2))) + AgentPos.X;
			Radar[2].X = (int)(30*Math.Cos((double)(theta-0.2))) + AgentPos.X;
							
			Radar[0].Y = (int)(40*Math.Sin((double)theta)) + AgentPos.Y;
			Radar[1].Y = (int)(30*Math.Sin((double)(theta+0.2))) + AgentPos.Y;
			Radar[2].Y = (int)(30*Math.Sin((double)(theta-0.2))) + AgentPos.Y;
					
			
			DrawAgent(g, AgentPos, Radar, skinbrush, hatbrush, radarPen);
			
			for(int i=AvailableVertices.Count-1; i>=0; i--){
				
				index = i;
				bestAngle = 4;
				
				for(int j=0; j<=i ; j++){
					
					auxAngle = root.GetAngleTo(AvailableVertices[j]);
					
					if(auxAngle < theta){
						auxAngle = theta-auxAngle;
					}else{
						auxAngle -= theta;
					}
					
					if(auxAngle > Math.PI)
						auxAngle = (float)((2*Math.PI) - auxAngle);
					
					if(auxAngle < bestAngle){
						bestAngle = auxAngle;
						index = AvailableVertices[j];
					}
					
				}
				
				visited = false; //Reinicia la bandera
				NextVertex = AvailableVertices[index]; //Elije el proximo vertice
				
				if(Prim){
					
					visited = true;
				
					for(int j=0; j<PrimEdges.Count; j++){
						
						if(PrimEdges[j].Contains(root.GetEdgeAt(NextVertex)))
							visited = false;
					
					}
				}
				
				if(Kruskal){
					
					if(!Prim)
						visited = true;
				
					for(int j=0; j<KruskalEdges.Count; j++){
						
						if(KruskalEdges[j].Contains(root.GetEdgeAt(NextVertex)))
							visited = false;
					
					}
				}
				
				for(int j=0; j<Perry.Visited.Count; j++){
					if(root.GetDestinationAt(NextVertex).Id == Perry.Visited[j]) //Revisa si el vertice ya ha sido visitado
						visited = true;
				}
				
				if(!visited){  
					Perry.ActualPosIndex = 0; //Reinicia el indice del path para iniciarlo de cero
					Perry.Path = root.GetPathAt(NextVertex); //Toma el nuevo path
					
					while(Perry.ActualPosIndex < Perry.PathCount-1){
				
						Perry.Walk();	
						AgentPos = Perry.GetActualPos();
					
						dx = DestPos.X - AgentPos.X;
						dy = DestPos.Y - AgentPos.Y;
					
						if(dy == 0){
							theta = 0;
							
						}
						else	
							theta = (float)Math.Atan((double) (dy/dx));
						
						if(dx < 0)
							theta += (float)Math.PI;
				
						Radar[0].X = (int)(40*Math.Cos((double)theta)) + AgentPos.X;
						Radar[1].X = (int)(30*Math.Cos((double)(theta+0.2))) + AgentPos.X;
						Radar[2].X = (int)(30*Math.Cos((double)(theta-0.2))) + AgentPos.X;
							
						Radar[0].Y = (int)(40*Math.Sin((double)theta)) + AgentPos.Y;
						Radar[1].Y = (int)(30*Math.Sin((double)(theta+0.2))) + AgentPos.Y;
						Radar[2].Y = (int)(30*Math.Sin((double)(theta-0.2))) + AgentPos.Y;
					
						
						DrawAgent(g, Perry.GetActualPos(), Radar, skinbrush, hatbrush, radarPen);
			
					}
					
					if(!Explore(root.GetDestinationAt(NextVertex), Perry)){
						Perry.ActualPosIndex = 0;
						Perry.Path = root.GetReturnPathAt(NextVertex);
						
						while(Perry.ActualPosIndex < Perry.PathCount-1){
				
							Perry.Walk();
							
							AgentPos = Perry.GetActualPos();
					
							dx = DestPos.X - AgentPos.X;
							dy = DestPos.Y - AgentPos.Y;
						
							if(dy == 0){
								theta = 0;
							}
							else	
								theta = (float)Math.Atan((double) (dy/dx));
					
							if(dx < 0)
								theta += (float)Math.PI;
				
							Radar[0].X = (int)(40*Math.Cos((double)theta)) + AgentPos.X;
							Radar[1].X = (int)(30*Math.Cos((double)(theta+0.2))) + AgentPos.X;
							Radar[2].X = (int)(30*Math.Cos((double)(theta-0.2))) + AgentPos.X;
							
							Radar[0].Y = (int)(40*Math.Sin((double)theta)) + AgentPos.Y;
							Radar[1].Y = (int)(30*Math.Sin((double)(theta+0.2))) + AgentPos.Y;
							Radar[2].Y = (int)(30*Math.Sin((double)(theta-0.2))) + AgentPos.Y;
							
							
							
							DrawAgent(g, Perry.GetActualPos(), Radar, skinbrush, hatbrush, radarPen);
			
						}
					}
					else{
						
						return true;
					}
				}
				
				AvailableVertices[index] = AvailableVertices[i];
			}
			
			return false;
			
		}
		bool BFS(List<Vertex> Checked, Queue<Vertex> Vertices, Tree PathsTree){
		
			if(Vertices.Count == 0)
				return false;
			
			Vertex root = Vertices.Dequeue();
			
			Node n = PathsTree.Search(root);
			
			if(root == Destination)
				return true;
			
			for(int i = 0; i<root.EdgesCount; i++){
					
				if(!Checked.Contains(root.GetDestinationAt(i))){
					Vertices.Enqueue(root.GetDestinationAt(i));
					Checked.Add(root.GetDestinationAt(i));
					n.Add(root.GetDestinationAt(i));
				}
			}
			
			if(BFS(Checked, Vertices, PathsTree))
				return true;

			return false;
			
		}
		bool FindShortestPath(Stack<Vertex> ShortestPath, Node root){
			
			if(root.Data == Destination){
				
				ShortestPath.Push(root.Data);
				return true;
			}
				
			for(int i=0; i<root.Sons.Count; i++){
				if(FindShortestPath(ShortestPath, root.Sons[i])){
				
					ShortestPath.Push(root.Data);
					return true;
					
				}
			}
			
			return false;
			
		}
		
		Edge CreatePath(Circle c_o, Circle c_f){
			
			Edge edge = new Edge();
		
			List<Point> path = new List<Point>();
			Point step;
			
			Point pt_o = c_o.Center;
			Point pt_f = c_f.Center;
			
			float xf = pt_f.X;
			float xo = pt_o.X;
			float xd = xf-xo;
			float x;
			
			float yf = pt_f.Y;
			float yo = pt_o.Y;
			float yd = yf-yo;
			float y;
			
			float m, b, theta, A, B;
			
			m = yd/xd;
			b = yo-m*xo;
				
			
			if(xd>0){
				
				theta = (float)Math.Atan2((double) yd, (double) xd);
				          
				A = c_o.GetR()*(float)Math.Cos(theta) + xo;
				B = c_o.GetR()*(float)Math.Sin(theta) + yo;
					
				if(xd>=yd){
						
					for(x=xo; x<=xf; x++){
							
						y = m*x + b;
							
						if(x>A+1){
								
							if(!isWhite(bmpLayers[Layer.image].GetPixel((int)Math.Round(x), (int)Math.Round(y)))){			   
								   	
							  	if(!isPtInto((int)Math.Round(x), (int)Math.Round(y), c_f))
								  		return null;
								   
							}
						}
							
						step = new Point((int)Math.Round(x), (int)Math.Round(y));
						path.Add(step);
							
					}
						
				}
				else{
						
					for(y=yo; y<=yf; y++){
							
						x = (y-b)/m;	
						if(y>B+1){
							if(!isWhite(bmpLayers[Layer.image].GetPixel((int)Math.Round(x), (int)Math.Round(y)))){	   
							  	if(!isPtInto((int)Math.Round(x), (int)Math.Round(y), c_f))
							  		return null;   
							}
						}
						step = new Point((int)Math.Round(x), (int)Math.Round(y));
						path.Add(step);
					}	
				}
			}
			else if(xd == 0){
				
				theta = (float)Math.PI/2;
				
				B = yo + c_o.GetR();;
				x = xo;
				
				for(y=yo; y<=yf; y++){
					
					if(y>B+1){
						if(!isWhite(bmpLayers[Layer.image].GetPixel((int)Math.Round(x), (int)Math.Round(y)))){	   
							 if(!isPtInto((int)Math.Round(x), (int)Math.Round(y), c_f))
							  	return null;   
						}
					}
					
					step = new Point((int)xo, (int)Math.Round(y));
					path.Add(step);
				}			
				
			}
			else{
				
				theta = (float)Math.Atan((double) m) + (float)Math.PI;
				
				A = xo + c_o.GetR()*(float)Math.Cos(theta);

				B = yo + c_o.GetR()*(float)Math.Sin(theta);
					
				if(-xd>=yd){
						
					for(x=xo; x>=xf; x--){
							
						y = m*x + b;
						
						if(x<A-1){
							
							if(!isWhite(bmpLayers[Layer.image].GetPixel((int)Math.Round(x), (int)Math.Round(y)))){
							  	if(!isPtInto((int)Math.Round(x), (int)Math.Round(y), c_f))
							  		return null;   
							}
						}
						
						step = new Point((int)Math.Round(x), (int)Math.Round(y));
						path.Add(step);	
					}
				}
				else{
						
					for(y=yo; y<=yf; y++){
						
						x = (y-b)/m;
						
						if(y>B+1){	
							if(!isWhite(bmpLayers[Layer.image].GetPixel((int)Math.Round(x), (int)Math.Round(y)))){	
							  	if(!isPtInto((int)Math.Round(x), (int)Math.Round(y), c_f))
							  		return null;
							}
						}
						
						step = new Point((int)Math.Round(x), (int)Math.Round(y));
						path.Add(step);
						
					}
				}			
			}
			
			edge.Path = path;
			edge.Angle = theta;
			edge.Weigth = (float)Math.Sqrt((double) (xd*xd + yd*yd));
			
			return edge;
		
		}
		
		Agent OverAPrey(Predator Doof){
			
			Agent Perry = null;
			
			for(int i=0; i<AgentList.Count; i++){
				
				if(isPtInto(AgentList[i].GetActualPos().X, AgentList[i].GetActualPos().Y, Doof.BodyCircle)){
					if(!AgentList[i].Stealth)
				   		Perry = AgentList[i];
				}
			}
			
			return Perry;
			
		}
		Agent PreyInRange(Predator Doof){
			
			Agent Perry = null;
			float dx, dy, c, closer = float.MaxValue;
			
			for(int i=0; i<AgentList.Count; i++){
				
				if(isPtInto(AgentList[i].GetActualPos().X, AgentList[i].GetActualPos().Y, Doof.SensorCircle)){
					if(!AgentList[i].Stealth){
						
						if(AgentList[i].GetActualPos().X > Doof.GetActualPos().X)
							dx = AgentList[i].GetActualPos().X - Doof.GetActualPos().X;
						else
							dx = Doof.GetActualPos().X - AgentList[i].GetActualPos().X;
						
						if(AgentList[i].GetActualPos().Y > Doof.GetActualPos().Y)
							dy = AgentList[i].GetActualPos().Y - Doof.GetActualPos().Y;
						else
							dy = Doof.GetActualPos().Y - AgentList[i].GetActualPos().Y;
						
						c = (dx*dx + dy*dy);
						
						if(c < closer){
							
							closer = c;
				   			Perry = AgentList[i];
				   			
						}
					}
				}
			}
			
			return Perry;
			
		}
		bool isPtInto(int x, int y, Circle c){
			
			float a, b, r;
			
			a = x - c.Center.X;
				
			if(a<0)
				a+=5;
			else
				a-=5;
			
			b = y - c.Center.Y;
			
			if(b<0)
				b+=5;
			else
				b-=5;
			
			r = c.GetR();
			
			if((a*a + b*b - r*r) <= 0)
				return true;
			
			return false;
			
			
		}
		Vertex insideACircle(int x, int y){
			
			int a, b, r, i;
			
			for(i = 0; i<CirclesGraph.Count; i++){  //evitar circulos repetidos
				
				a = x - CirclesGraph.GetVertexAt(i).Circle.GetXCenter();
				
				if(a<0)
					a+=3;
				else
					a-=3;
				
				b = y - CirclesGraph.GetVertexAt(i).Circle.GetYCenter();
				
				if(b<0)
					b+=3;
				else
					b-=3;
				
				r = CirclesGraph.GetVertexAt(i).Circle.GetR();
				
				if((a*a + b*b - r*r) <= 0)
					return CirclesGraph.GetVertexAt(i);
					
			}
			
			return null;
			
		}
		Point PicBox_to_bitmap(int xp, int yp){
			
			float wp = pictureBoxImage.Width;
			float wb = bmpImage.Width;
			
			float hp = pictureBoxImage.Height;
			float hb = bmpImage.Height;
			
			float rx = wp/wb;
			float ry = hp/hb;
			float rf;
			
			if(rx>ry)
				rf = ry;
			else
				rf = rx;
			
			float dx = (pictureBoxImage.Width - bmpImage.Width*rf)/2;
			float dy = (pictureBoxImage.Height - bmpImage.Height*rf)/2;
			
			float xb = (xp-dx)/rf;
			float yb = (yp-dy)/rf;
			
			Point pt = new Point((int)Math.Round(xb), (int)Math.Round(yb));
			
			return pt;
			
		}
		
		bool isBlack(Color color){
			
			if(color.R == 0)
				if(color.G == 0)
					if(color.B == 0)
						return true;
			
			return false;
			
		}
		bool isWhite(Color color){
			
			if(color.R == 255)
				if(color.G == 255)
					if(color.B == 255)
						return true;
			
			return false;
			
		}
		
		void DrawShortestPaths(){
			
			Graphics g = Graphics.FromImage(bmpLayers[Layer.ids]);
			Pen pen = new Pen(Color.LightGreen, 8);
			
			g.Clear(Color.Transparent);
			
			Vertex a;
			Vertex b;
			
			for(int i=0; i<AgentList.Count; i++){
				
				AgentList[i].VerticesPath.Clear();
				FindDijkstra(AgentList[i]);
				
				for(int j=0; j<AgentList[i].VerticesPath.Count-1; j++){
					
					
					a = CirclesGraph.GetVertexAt(AgentList[i].VerticesPath[j]-1);
					b = CirclesGraph.GetVertexAt(AgentList[i].VerticesPath[j+1]-1);
					
					g.DrawLine(pen, a.Circle.Center, b.Circle.Center);
					
				}
				
			}
			
			UpdateBitmap();
			
		}
		
	}
	

}
