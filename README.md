# Image_Analyzer_Dijkstra_Game
Windows application in which you can upload an image, which will get analyzed by the program and will transform it into an interactive graph to start a simulation. The Simulation consists of little preys (which, to my amusement, are colored after perry the platypus) trying to get to a destination node, while evading hunters in the process.

This program uses multiple strategies to analyze images in a bitmap, and based on the colors and shapes in the image, produce a unique map which can then be used for the simulation.

-HOW TO USE-

The first button, the only one that you can select at the beginning, is to upload the image. You can use one of the example images in the folder "Images", or you can create your own! But be careful of reading the instrucions (explained on "IMAGE REQUIREMENTS TO CREATE A MAP"), otherwise the program might not work.
Once the image has been uploaded, you have to click in "Crear grafo" which stands for "create graph", then the map gets created and the simulation can begin!

As a little info, below the "Create graph" button there is a ListView in which you can see each node of the graph, and which node it is connected to. 

To start a simulation you have to add the following in the bitmap, by selecting a node while the option for it is also selected on the menu to the right:
-At least a prey (you can add more)
-A goal node (you can only add one, and all preys will try to get there)
-Any amount of hunters (can be zero, or up to the amount of nodes left at this point)

Aditionally, you can change some of the settings for the simulation in the menu. The settings you can change are the following:
-The speed of the preys (the speed of the hunters is determined by the preys. They go slower while unaware of preys, but get faster when they detect one in their range)
-The amount of "Stealth" skills the preys can use. This is a skill which will let the prey hide (by taking off his hat, like a certain platypus...) until it reaches another node. Being hidden means hunters will ignore it when it enters their range
-The range of the hunters, which will be marked by a big red circle.

Once everything is ready, you can start the simulation!

-IMAGE REQUIREMENTS TO CREATE A MAP-

The background has to be white. Anything other than white will be taken as a circle or an obstacle.
The program detects anything with at least 1 black pixel as a circle, which will then be converted into a node for the preys and hunters to be, or the goal in any case.
The circle doesn't have to be perfect, as the program can tell if a grey or cropped area is actually part of a circle, simply... blurry.
Anything that's a different color will be taken as an obstacle.

-HOW IT WORKS-

Image analyzer:
The image analyzer uses a brute force technique to inspect every pixel in the image, and tell if it's part of the background, a circle or an obstacle. This are the only options it can choose, and anything other than black or white whill be considered an obstacle. 
Once it detects any black pixel, it then will calculate the center of the circle, and afterwards it's height and width, and work with that. This means the circle doesn't have to be perfect neither in color nor shape, as long as it has at least a black pixel in it and a somewhat regular form.
After a circle has been detected it will create an object of the class Circle, which will have it's id, diameter, placing and other info saved.
If the image analyzer detects an obstacle, it will actually ignore it at this point of the simulation, because it only becomes important after we know exactly how many circles there are, so we can calculate the paths.
After it has completely analyzed the image, it will create a map, which will be shown _above_ the actual image, so the image remains intact. 

Map creator:
After we know the amount of circles and their placements, the map is created.
The first part is giving the circles a little pulishment. It will create an actually perfect (well, as perfect as the bitmap allows) black circle on the map _above_ the image for reasons explained before.
Then, using yet another selective brute force technique, it will try to create paths between all the circles in the map. The path is created by adding or decreasing value in the position of the circle and checking if it is able to reach the other circle without touching either a third non related circle, or an obstacle.
First it will ignore anything that is the circle's radious, as it will be part of the circle. Once it's done with that, it will check every pixel in a linear path to the center of the destination circle. If in the path it finds anything not white, the program will check if that pixel is part of the destination circle. If it is, then the path gets created and added to both circles for reference. If it isn't, it means there's an obstacle or another circle, and will delete the path and continue with the next.
Once all the paths are created, they will be shown in the bitmap in green.

Bitmap selector:
Once the map is fully created, you can use your mouse to hover above the circles, which will make them highlight, and select a circle to add anything you want from the menu.
The selector is very simple, as it takes the inputs of the mouse, and if you hover over the coordinates of a circle, it will change that circle's color to a lighter shade. If you click, it follows the same patern but adding the desired unit on the map.
The unit always gets placed in the middle of the circle, and is linked to an object.

Prey's dijkstra path finder:
Once a prey is created and a goal has been already selected, the best path will be marked with a lighter shade of green.
This path is calculated with a Dijkstra algorythim.
This best path takes into consideration all the possibilites and chooses the one that has the least amount of total time, while also being a finished path (meaning it reaches the destination).
If there's absolutely no way the prey can get to the destination (one of the two is in a different graph, which isn't connected to the other one) it will not show a best path, as there isn't one.

Hunter's prey radar:
Hunters are a bit dumber. They don't care about efficiency, they just see and pursue.
Hunters walk slowly initially, with a random nature. Once they get to a node without having detected a prey, they will choose a random node to go to and continue doing so.
If a prey without stealth skills left goes trough the radar of a hunter, they will be alerted. This means the hunter gets a more saturated shade of red, and gets a little arrow which chases the closest prey's position. Whenever an alerted hunter gets to a node, they will then choose the path which is closer to the prey, meaning they can choose a path that gets nowhere, or that effectively gets them further away from the prey.
If the prey gets out of the radar, the hunter will simply forget about it, and continue with it's random wandering.
However, if a hunter catches a prey, the prey will get eaten, and erased from the simulation.

End of simulation and state saving:
Once any prey is able to get to the destination without being eaten or all the preys are eaten, the simulation will stop. Any prey which isn't in a node will continue it's path until it's finished, and stay there for the next simulation. If 2 or more preys get to the same node this way, only one will be left, and the rest will be erased to avoid memory loss.
The hunters, on the other hand, will stay mid-path and will continue with their path once another simulation starts. 
To be able to start another you have to again complete the starting requirements of at least one prey, and a goal in a different node. Once that is done, you can continue doing simulations!!


