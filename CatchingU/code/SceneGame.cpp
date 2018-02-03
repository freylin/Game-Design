#include "SceneGame.h"
#include "SceneStart.h"
#include "SceneFailed.h"
#include "SceneWin.h"
#include "SceneScore.h"
#include "SceneRole.h"
#include "time.h"
#include "SimpleAudioEngine.h"


USING_NS_CC;

Scene* SceneGame::createScene()
{
	auto scene = Scene::create();
	auto layer = SceneGame::create();
	scene->addChild(layer);
	return scene;
}

bool SceneGame::init()
{
	if ( !Layer::init() )
	{
		return false;
	}

	Size visibleSize = Director::getInstance()->getVisibleSize();
	Vec2 origin = Director::getInstance()->getVisibleOrigin();

	//窗口标题
	auto label = Label::createWithTTF("CatchingU", "Resources/fonts/Marker Felt.ttf", 50);
	label->setPosition(Vec2(origin.x + visibleSize.width/2,
		origin.y + visibleSize.height - label->getContentSize().height));
	label->setColor(ccc3(0, 0, 0));
	this->addChild(label, 1);

	/*//背景图片
	auto sprite = Sprite::create("Resources/image/2.JPG");
	sprite->setPosition(Vec2(visibleSize.width/2 + origin.x, visibleSize.height/2 + origin.y));
	sprite->setScaleX(visibleSize.width / sprite->getContentSize().width);  
	sprite->setScaleY(visibleSize.height / sprite->getContentSize().height);
	this->addChild(sprite, 0);*/


	//创建游戏主背景  
	Sprite* sprite2;
	if (SceneStart::kk == 0) sprite2 = Sprite::create("Resources/image/GameBG1.jpg");
	else if (SceneStart::kk == 1) sprite2 = Sprite::create("Resources/image/GameBG2.jpg");
	else sprite2 = Sprite::create("Resources/image/GameBG3.jpg");
	this->addChild(sprite2);

	//设置背景的位置  
	sprite2->setPosition(Vec2(visibleSize.width/2 + origin.x, visibleSize.height/2 + origin.y));
	sprite2->setScale((visibleSize.height - 0 )/sprite2->getContentSize().height);

	//左侧步数框
	auto count = Label::createWithTTF("Count", "Resources/fonts/Marker Felt.ttf", 50);
	count->setPosition(Vec2(origin.x + visibleSize.width/6 , origin.y + visibleSize.height/2 - count->getContentSize().height));
	count->setColor(ccc3(0, 0, 0));
	this->addChild(count, 1);


	//右侧菜单项
	auto menu = Menu::create();
	menu->setPosition(Vec2(visibleSize.width/6*5 + origin.x - 20, visibleSize.height/2 + origin.y - 100));
	this->addChild(menu, 1);

	auto m1 = MenuItemImage::create("Resources/image/musicon1.png","Resources/image/musicon2.png");
	m1->setScale(0.5);
    m1->setTarget(this,menu_selector(SceneGame::menuMusicOn));
	menu->addChild(m1);
    
	auto m2 = MenuItemImage::create("Resources/image/musicoff1.png","Resources/image/musicoff2.png");
	m2->setScale(0.5);
    m2->setTarget(this,menu_selector(SceneGame::menuMusicOff));
	menu->addChild(m2);

	auto closeItem = MenuItemImage::create(
		"Resources/image/back1.png",
		"Resources/image/back2.png",
		CC_CALLBACK_1(SceneGame::menuCallback, this));
	closeItem->setPosition(Vec2(origin.x + visibleSize.width - closeItem->getContentSize().width/2 ,
		origin.y + closeItem->getContentSize().height/2));
	closeItem->setScale(0.5);
	menu->addChild(closeItem);

	menu->alignItemsVertically();


	//创建格子
	SceneGame::Block(-1,-1);

	return true;
}

void SceneGame::menuMusicOn(cocos2d::Ref* pSender)
{
	 CocosDenshion::SimpleAudioEngine::getInstance()->resumeBackgroundMusic();
}

void SceneGame::menuMusicOff(cocos2d::Ref* pSender)
{
	CocosDenshion::SimpleAudioEngine::getInstance()->pauseBackgroundMusic();
}

void SceneGame::Block(int x, int y)
{

	int i,j,xx,yy,d,r=18,ii,jj,dis[10][10],f,ff,x1,y1,xk,yk,fz,f1,f2,wz,wl,x2,y2;
	int k, x3, y3, kk;
	static int a[10][10][3], x0 = 0, y0 = 0, xz = 0, yz = 0, c = 0;
	static Sprite* b2[10][10];
	static Sprite* b1[10][10];
	static Sprite* ll;
	static int dd[6][2]={{-1,0}, {-1,1}, {0,-1}, {0,1}, {1,1}, {1,0}};
	static int ddo[6][2]={{-1,-1}, {-1,0}, {0,-1}, {0,1}, {1,-1}, {1,0}};
	static bool b[10][10]; 
	static auto num = Label::createWithTTF("0", "Resources/fonts/Marker Felt.ttf", 50);

	
	Size visibleSize = Director::getInstance()->getVisibleSize();
	Vec2 origin = Director::getInstance()->getVisibleOrigin();

	if (x == -1)
	{
		//xx = 346; yy = 380; d = 36; r = d / 2;
		xx = 346; yy = 360; d = 36; r = d / 2;
		//xx = 173; yy = 720; d = 72; r = d / 2;
		for (i = 1; i <= 9; i++)
			for (j = 1; j <= 9; j++)
			{
				b[i][j] = 1;

				a[i][j][1]=xx + (j - 1) * d;
				if (i % 2 == 0) a[i][j][1] -= r;
				a[i][j][2]=yy - (i - 1) * d;

				b2[i][j] = Sprite::create("Resources/image/pot2.png");
				b2[i][j]->setScale(0.7);
				b2[i][j]->setPosition(a[i][j][1],a[i][j][2]);
				this->addChild(b2[i][j]);

				b1[i][j] = Sprite::create("Resources/image/pot1.png");
				b1[i][j]->setScale(0.7);
				b1[i][j]->setPosition(a[i][j][1],a[i][j][2]);
				this->addChild(b1[i][j]);
			}

			//动态精灵
			if (SceneRole::r ==1 ) {ll = Sprite::create("Resources/image/01.png"); SceneStart::na = "Aka";}
			else if (SceneRole::r == 2) {ll = Sprite::create("Resources/image/02.png"); SceneStart::na = "Mulas";}
			else if (SceneRole::r == 3) {ll = Sprite::create("Resources/image/03.png"); SceneStart::na = "Aou";}
			else if (SceneRole::r == 4) {ll = Sprite::create("Resources/image/04.png"); SceneStart::na = "Kulo";}
			else if (SceneRole::r == 5) {ll = Sprite::create("Resources/image/05.png"); SceneStart::na = "Midoly";}
			else {ll = Sprite::create("Resources/image/06.png"); SceneStart::na = "Kilo";}

			ll->setScale(0.3);
			this->addChild(ll, 1);
			x0 = a[5][5][1]; y0 = a[5][5][2];
			xz = 5; yz = 5;
			ll->setPosition(x0,y0);

			//难度：初始彩块个数
			k = SceneStart::k;
			srand(time(NULL)); 
			kk = 0;
			while (kk < k)
			{
				x3 = rand() % 9 + 1;
				y3 = rand() % 9 + 1;
				if ((x3 != 5 || y3 != 5) && b[x3][y3])
				{
					b[x3][y3] = 0;
					b1[x3][y3]->setVisible(false);
					kk++;
				}
			}

			//步数显示器
			c = 0;
			num = Label::createWithTTF("0", "Resources/fonts/Marker Felt.ttf", 50);
			num->setPosition(Vec2(origin.x + visibleSize.width/6 , origin.y + visibleSize.height/2 - num->getContentSize().height - 60));
			num->setColor(ccc3(0, 0, 0));
			this->addChild(num, 1);

	}
	else
	{
		fz = 1;
		f1 = 0;
		wz = 0;
		wl = 0;
		for (i = 1;i <= 9; i++)
		{
			for (j = 1; j <= 9; j++)
				if (x >= a[i][j][1]-r && x <= a[i][j][1]+r && y >= a[i][j][2]-r && y <= a[i][j][2]+r && b[i][j])
				{
					if (i == xz && j == yz) 
					{
						fz = 0;
						break;
					}
					//找到被点击的无色格子
					f1 = 1;
					b[i][j] = 0;
					b1[i][j]->setVisible(false);
					ii = i;
					jj = j;
					break;
				}
				if (fz == 0) break;
		}

		if (fz && f1)
		{
			SceneGame::run(dis,b);

			xk = 0; yk = 0;
			dis[0][0] = 9999;
			f1 = 0;
			f2 = 0;
			ff = 0;
			for (i = 0; i <= 5 ;i++)
			{
				if (xz % 2 ==1) { x1 = xz+dd[i][0]; y1 = yz+dd[i][1];}
				else { x1 = xz+ddo[i][0]; y1 = yz+ddo[i][1];}
				if (x1>0 && x1<10 && y1>0 && y1<10)
				{
					if (b[x1][y1])
					{
						f2 = 1;
						x2 = x1; y2 = y1;
						if (dis[x1][y1]<dis[xk][yk])
						{
							f1 = 1;
							xk = x1;
							yk = y1;
						}
					}

				}
				else
				{
					//游戏失败
					/*Size visibleSize = Director::getInstance()->getVisibleSize();
					Vec2 origin = Director::getInstance()->getVisibleOrigin();
					auto label2 = Label::createWithTTF("LianLian is a sb!", "Resources/fonts/simhei.ttf", 100);
					label2->setPosition(Vec2(origin.x + visibleSize.width/2,
						origin.y + visibleSize.height - label2->getContentSize().height));
					label2->setColor(ccc3(0, 0, 0));
					this->addChild(label2, 1);

					auto size = Director::sharedDirector()->getWinSize();  
					screen = RenderTexture::create(size.width, size.height);  
					auto *scene = Director::sharedDirector()->getRunningScene();  
					screen->begin();  
					scene->visit();//将当前的整个scene绘出来  
					screen->end();  


					/*screen->setPosition(Vec2(visibleSize.width/2 + origin.x, visibleSize.height/2 + origin.y));
					this->addChild(screen,2);*/
					ff = 1;
					auto sc = SceneFailed::createScene();
					Director::getInstance()->replaceScene(sc);

					break;
				}

			}

			//可以移动
			if (f1)  //有出口
			{
				//步数+1
				c += 1;
                num->setString(String::createWithFormat("%i",c)->getCString());

				x0 = a[xk][yk][1];
				y0 = a[xk][yk][2];
				xz = xk;
				yz = yk;
				auto moveTo = MoveTo::create(0.5,Vec2(x0,y0));
				ll->runAction(moveTo);
			}
			else if (f2) //已经被围住，但是可以动
			{
				//步数+1
				c += 1;
                num->setString(String::createWithFormat("%i",c)->getCString());

				x0 = a[x2][y2][1];
				y0 = a[x2][y2][2];
				xz = x2;
				yz = y2;
				auto moveTo = MoveTo::create(0.5,Vec2(x0,y0));
				ll->setVisible(false);
				//ll->runAction(moveTo);
			}
			else if (!ff)//动不了，游戏成功
			{
				c += 1;
				SceneScore::c[SceneStart::kk][10] = c;
				SceneScore::s[SceneStart::kk][10] = SceneStart::na;
				auto sc = SceneWin::createScene();
				Director::getInstance()->replaceScene(sc);
			}

		}

	}
}

void SceneGame::run(int (*dis)[10], bool (*b)[10])
{
	int i,j,t,w,h[100][3],x,y;
	bool bo[10][10];
	static int dd[6][2]={{-1,0}, {-1,1}, {0,-1}, {0,1}, {1,1}, {1,0}};
	static int ddo[6][2]={{-1,-1}, {-1,0}, {0,-1}, {0,1}, {1,-1}, {1,0}};

	t = 1; w = 0;

	for (i = 1; i <= 9 ;i++)
		for (j = 1; j <= 9; j++)
			if (b[i][j] && (i==1 || i==9 || j==1 || j==9))
			{
				bo[i][j] = 0;
				dis[i][j] = 1;
				h[++w][1] = i;
				h[w][2] = j;
			}
			else
			{
				bo[i][j] = 1;
				dis[i][j]=999;
			}

			while (t<=w)
			{
				for (i = 0; i <= 5 ;i++)
				{
					if (h[t][1] % 2 ==1) { x = h[t][1]+dd[i][0]; y = h[t][2]+dd[i][1];}
					else { x = h[t][1]+ddo[i][0]; y = h[t][2]+ddo[i][1];}

					if (x>0 && x<10 && y>0 && y<10)
						if (b[x][y] && bo[x][y])
						{
							dis[x][y] = dis[h[t][1]][h[t][2]]+1;
							bo[x][y] = 0;
							h[++w][1] = x;
							h[w][2] = y;
						}
				}
				t++;
			}
}


void SceneGame::onEnter(void)
{  
	auto dispatcher = Director::getInstance()->getEventDispatcher();  
	auto listener = EventListenerTouchOneByOne::create();  
	listener->onTouchBegan = CC_CALLBACK_2(SceneGame::onTouchBegan,this);  
	//listener->onTouchMoved = CC_CALLBACK_2(SceneGame::onTouchMoved,this);  
	//listener->onTouchEnded = CC_CALLBACK_2(SceneGame::onTouchEnded,this);  
	listener->setSwallowTouches(true);//不向下传递触摸  
	dispatcher->addEventListenerWithSceneGraphPriority(listener,this);

	Layer::onEnter();  

}  

bool SceneGame::onTouchBegan(Touch* pTouch, Event* pEvent)
{
	Object* obj = (Object*)pTouch;

	int x,y;
	//获取触摸点的窗口坐标
	CCPoint touchPoint=pTouch->getStartLocationInView();  
	touchPoint = CCDirector::sharedDirector()->convertToGL(touchPoint);  
	x=touchPoint.x;  
	y=touchPoint.y; 

	SceneGame::Block(x, y);

	return true;
}


void SceneGame::menuCallback(Ref* pSender)
{
	auto sc = SceneStart::createScene();
	Director::getInstance()->replaceScene(sc);
}