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

### This function optimizes the given path and returns a new path
### source: the current position of the agent
### dest: the desired destination of the agent
### path: the path previously computed by the A* algorithm
### world: pointer to the world
def shortcutPath(source, dest, path, world, agent):
	nodes = [source]
	nodes.extend(path)
	nodes.append(dest)
	n = len(nodes)
	a = [i for i in range(n)]
	pre = [i for i in range(n)]
	a[0] = 0
	pre[0] = 0
	for i in range(1, n):
		a[i] = INFINITY/2
		found = False
		for j in range(i):
			if goodEdge(nodes[i], nodes[j], world, agent) and a[j]+distance(nodes[i], nodes[j])<a[i]:
				found = True
				a[i] = a[j]+distance(nodes[i], nodes[j])
				pre[i] = j
		if not found:
			a[i] = a[i-1]+distance(nodes[i], nodes[i-1])
			pre[i] = i-1
	path = []
	ind = n-1
	while ind!=0:
		path.append(nodes[ind])
		ind = pre[ind]
	path.remove(nodes[n-1])
	path.reverse()
	return path

def goodEdge(p1, p2, world, agent):
	if rayTraceWorld(p1, p2, world.getLines()) != None:
		return False
	for p in world.getPoints():
		if minimumDistance((p1, p2), p) < agent.getMaxRadius():
			return False

	return True

### This function changes the move target of the agent if there is an opportunity to walk a shorter path.
### This function should call nav.agent.moveToTarget() if an opportunity exists and may also need to modify nav.path.
### nav: the navigator object
### This function returns True if the moveTarget and/or path is modified and False otherwise
def mySmooth(nav):
	if nav.destination==None:
		return False
	if nav.path == None or nav.path == []:
		if goodEdge(nav.agent.getLocation(), nav.destination, nav.world, nav.agent) and nav.agent.moveTarget!=nav.destination:
			nav.path = []
			nav.agent.moveToTarget(nav.destination)
			return True
		return False
	path = []
	path.extend(nav.path)
	n = len(path)
	if n != 0:
		if goodEdge(nav.agent.getLocation(), nav.destination, nav.world, nav.agent):
			nav.path = []
			nav.agent.moveToTarget(nav.destination)
			return True

	while n > 0:
		if goodEdge(nav.agent.getLocation(), path[n-1], nav.world, nav.agent):
			target = path[n-1]
			for i in range(n):
				nav.path.remove(path[i])
			# pygame.draw.line(nav.agent.world.debug, (255,0,0), nav.agent.getLocation(), target, 2)
			nav.agent.moveToTarget(target)
			return True
		n = n-1
	return False


