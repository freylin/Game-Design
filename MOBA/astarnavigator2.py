'''
 * Copyright (c) 2014, 2015 Entertainment Intelligence Lab, Georgia Institute of Technology.
 * Originally developed by Mark Riedl.
 * Last edited by Mark Riedl 05/2015
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
'''

import sys, pygame, math, numpy, random, time, copy
from pygame.locals import * 

from constants import *
from utils import *
from core import *
from mycreatepathnetwork import *
from mynavigatorhelpers import *


###############################
### AStarNavigator
###
### Creates a path node network and implements the A* algorithm to create a path to the given destination.
			
class AStarNavigator(NavMeshNavigator):

	def __init__(self):
		NavMeshNavigator.__init__(self)
		

	### Create the path node network.
	### self: the navigator object
	### world: the world object
	def createPathNetwork(self, world):
		self.pathnodes, self.pathnetwork, self.navmesh = myCreatePathNetwork(world, self.agent)
		return None
		
	### Finds the shortest path from the source to the destination using A*.
	### self: the navigator object
	### source: the place the agent is starting from (i.e., its current location)
	### dest: the place the agent is told to go to
	def computePath(self, source, dest):
		self.setPath(None)
		### Make sure the next and dist matrices exist
		if self.agent != None and self.world != None: 
			self.source = source
			self.destination = dest
			### Step 1: If the agent has a clear path from the source to dest, then go straight there.
			###   Determine if there are no obstacles between source and destination (hint: cast rays against world.getLines(), check for clearance).
			###   Tell the agent to move to dest
			### Step 2: If there is an obstacle, create the path that will move around the obstacles.
			###   Find the path nodes closest to source and destination.
			###   Create the path by traversing the self.next matrix until the path node closest to the destination is reached
			###   Store the path by calling self.setPath()
			###   Tell the agent to move to the first node in the path (and pop the first node off the path)
			if clearShot(source, dest, self.world.getLines(), self.world.getPoints(), self.agent):
				self.agent.moveToTarget(dest)
			else:
				start = findClosestUnobstructed(source, self.pathnodes, self.world.getLinesWithoutBorders())
				end = findClosestUnobstructed(dest, self.pathnodes, self.world.getLinesWithoutBorders())
				if start != None and end != None:
					# print len(self.pathnetwork)
					newnetwork = unobstructedNetwork(self.pathnetwork, self.world.getGates())
					# print len(newnetwork)
					closedlist = []
					path, closedlist = astar(start, end, newnetwork)
					if path is not None and len(path) > 0:
						path = shortcutPath(source, dest, path, self.world, self.agent)
						self.setPath(path)
						if self.path is not None and len(self.path) > 0:
							first = self.path.pop(0)
							self.agent.moveToTarget(first)
		return None
		
	### Called when the agent gets to a node in the path.
	### self: the navigator object
	def checkpoint(self):
		myCheckpoint(self)
		return None

	### This function gets called by the agent to figure out if some shortcuts can be taken when traversing the path.
	### This function should update the path and return True if the path was updated.
	def smooth(self):
		return mySmooth(self)

	def update(self, delta):
		myUpdate(self, delta)


def unobstructedNetwork(network, worldLines):
	newnetwork = []
	for l in network:
		hit = rayTraceWorld(l[0], l[1], worldLines)
		if hit == None:
			newnetwork.append(l)
	return newnetwork




def astar(init, goal, network):
	path = []
	open = []
	closed = []
	### YOUR CODE GOES BELOW HERE ###
	open = [init]
	G = {}
	F = {}
	pre = {}
	G[init] = 0
	F[init] = distance(init, goal)
	pre[init] = init
	getEnd = False
	while len(open)>0:
		minNode = open[0]
		for node in open:
			if F[node] < F[minNode]:
				minNode = node

		if minNode == goal:
			getEnd = True
			break
		open.remove(minNode)
		closed.append(minNode)

		for edge in network:
			found = False
			if minNode == edge[0]:
				nextNode = edge[1]
				found = True
			elif minNode == edge[1]:
				nextNode = edge[0]
				found = True
			if found:
				if nextNode in closed:
					continue
				if nextNode not in open:
					open.append(nextNode)
					G[nextNode] = G[minNode] + distance(nextNode, minNode)
					F[nextNode] = G[nextNode] + distance(nextNode, goal)
					pre[nextNode] = minNode
				else:
					if G[nextNode] > G[minNode] + distance(nextNode, minNode):
						G[nextNode] = G[minNode] + distance(nextNode, minNode)
						F[nextNode] = G[nextNode] + distance(nextNode, goal)
						pre[nextNode] = minNode
	if getEnd:
		node = goal
		while node!=init:
			path.append(node)
			node = pre[node]
		path.append(init)
		path.reverse()
	### YOUR CODE GOES ABOVE HERE ###
	return path, closed
	

def myUpdate(nav, delta):
	### YOUR CODE GOES BELOW HERE ###
	# print nav.agent.distanceTraveled
	if (nav.world.gates != []):
		if rayTraceWorld(nav.agent.moveOrigin, nav.agent.moveTarget, nav.world.getGates()) != None:
			nav.agent.stop()
			nav.computePath(nav.agent.moveOrigin, nav.destination)
		else:
			for line in nav.world.getGates():
				if minimumDistance(line, nav.agent.moveTarget) < nav.agent.getMaxRadius():
					nav.agent.stop()
					nav.computePath(nav.agent.moveOrigin, nav.destination)
	### YOUR CODE GOES ABOVE HERE ###
	return None



def myCheckpoint(nav):
	### YOUR CODE GOES BELOW HERE ###
	### YOUR CODE GOES ABOVE HERE ###
	return None


### Returns true if the agent can get from p1 to p2 directly without running into an obstacle.
### p1: the current location of the agent
### p2: the destination of the agent
### worldLines: all the lines in the world
### agent: the Agent object
def clearShot(p1, p2, worldLines, worldPoints, agent):
	### YOUR CODE GOES BELOW HERE ###
	if rayTraceWorld(p1, p2, worldLines) != None:
		return False
	for p in worldPoints:
		if minimumDistance((p1, p2), p) < agent.getMaxRadius():
			return False
	return True
	### YOUR CODE GOES ABOVE HERE ###
	return False

