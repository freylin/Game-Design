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
from moba import *

class MyMinion(Minion):
	
	def __init__(self, position, orientation, world, image = NPC, speed = SPEED, viewangle = 360, hitpoints = HITPOINTS, firerate = FIRERATE, bulletclass = SmallBullet):
		Minion.__init__(self, position, orientation, world, image, speed, viewangle, hitpoints, firerate, bulletclass)
		self.states = [Idle]
		### Add your states to self.states (but don't remove Idle)
		self.states.append(Move)
		self.states.append(ShootBT)
		self.states.append(ShootNPCs)

	def start(self):
		Minion.start(self)
		self.changeState(Idle)





############################
### Idle
###
### This is the default state of MyMinion. The main purpose of the Idle state is to figure out what state to change to and do that immediately.

class Idle(State):
	
	def enter(self, oldstate):
		State.enter(self, oldstate)
		# stop moving
		self.agent.stopMoving()
	
	def execute(self, delta = 0):
		State.execute(self, delta)
		team = self.agent.getTeam()
		# print "numSpawned:",self.agent.world.getBaseForTeam(team).numSpawned
		towers = self.agent.world.getEnemyTowers(team)
		target = None
		if towers != None and len(towers)>0:
			target = towers[0]
		else:
			bases = self.agent.world.getEnemyBases(team)
			if bases != None and len(bases)>0:
				target = bases[0]

		if target != None:
			self.agent.changeState(Move, target)

		return None

##############################
### Taunt
###
### This is a state given as an example of how to pass arbitrary parameters into a State.
### To taunt someome, Agent.changeState(Taunt, enemyagent)

class Taunt(State):

	def parseArgs(self, args):
		self.victim = args[0]

	def execute(self, delta = 0):
		if self.victim is not None:
			print "Hey " + str(self.victim) + ", I don't like you!"
		self.agent.changeState(Idle)

##############################
class Move(State):

	def parseArgs(self, args):
		self.target = args[0]

	def enter(self, oldstate):
		self.team = self.agent.getTeam()
		self.maxHp = self.agent.getMaxHitpoints()

	def execute(self, delta = 0):
		if self.agent.getHitpoints() == self.maxHp:
			enemy = self.agent.world.getEnemyNPCs(self.team)
			for e in enemy:
				if e in self.agent.getVisible() and distance(self.agent.getLocation(), e.getLocation()) <= BULLETRANGE:
					self.agent.changeState(ShootNPCs, e)
					return False

		if self.target.getHitpoints() <= 0:
			self.agent.changeState(Idle)
		elif distance(self.target.getLocation(), self.agent.getLocation()) <= BULLETRANGE and (self.target in self.agent.getVisible() or distance(self.target.getLocation(), self.agent.getLocation()) <= 1):
			self.agent.changeState(ShootBT, self.target)
		else:
			self.agent.navigateTo(self.target.getLocation())



class ShootBT(State):

	def parseArgs(self, args):
		self.target = args[0]

	def enter(self, oldstate):
		self.agent.stopMoving()

	def execute(self, delta = 0):
		if self.target.getHitpoints() <= 0:
			self.agent.changeState(Idle)
		else:
			self.agent.turnToFace(self.target.getLocation())
			self.agent.shoot()

class ShootNPCs(State):

	def parseArgs(self, args):
		self.target = args[0]

	def enter(self, oldstate):
		self.agent.stopMoving()
		self.maxHp = self.agent.getMaxHitpoints()

	def execute(self, delta = 0):
		if self.target.getHitpoints() <= 0 or self.target not in self.agent.getVisible() or distance(self.agent.getLocation(), self.target.getLocation()) > BULLETRANGE or self.agent.getHitpoints() < self.maxHp:
			self.agent.changeState(Idle)
		else:
			self.agent.turnToFace(self.target.getLocation())
			self.agent.shoot()

