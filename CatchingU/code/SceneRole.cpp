#include "SceneRole.h"
#include "SceneStart.h"
#include "SceneGame.h"

USING_NS_CC;

int SceneRole::r = 0;

Scene* SceneRole::createScene()
{
    auto scene = Scene::create();
    auto layer = SceneRole::create();
    scene->addChild(layer);
    return scene;
}

bool SceneRole::init()
{
    if ( !Layer::init() )
    {
        return false;
    }
    
    Size visibleSize = Director::getInstance()->getVisibleSize();
    Vec2 origin = Director::getInstance()->getVisibleOrigin();

	int x = 150, y = -150, xx = 510;
	auto Name1 = Label::createWithTTF("Aka", "Resources/fonts/Marker Felt.ttf", 25);
	Name1->setColor(Color3B::BLUE);
	Name1->setPosition(Vec2(visibleSize.width/2 + origin.x - xx + x, visibleSize.height/2 + origin.y + y));
	this->addChild(Name1, 1);

	auto Name2 = Label::createWithTTF("Mulas ", "Resources/fonts/Marker Felt.ttf", 25);
	Name2->setColor(Color3B::BLUE);
	Name2->setPosition(Vec2(visibleSize.width/2 + origin.x - xx + x*2, visibleSize.height/2 + origin.y + y));
	this->addChild(Name2, 1);

	auto Name3 = Label::createWithTTF(" Aou", "Resources/fonts/Marker Felt.ttf", 25);
	Name3->setColor(Color3B::BLUE);
	Name3->setPosition(Vec2(visibleSize.width/2 + origin.x - xx + x*3, visibleSize.height/2 + origin.y + y));
	this->addChild(Name3, 1);

	auto Name4 = Label::createWithTTF("Kulo   ", "Resources/fonts/Marker Felt.ttf", 25);
	Name4->setColor(Color3B::BLUE);
	Name4->setPosition(Vec2(visibleSize.width/2 + origin.x - xx + x*4, visibleSize.height/2 + origin.y + y));
	this->addChild(Name4, 1);

	auto Name5 = Label::createWithTTF("Midoly      ", "Resources/fonts/Marker Felt.ttf", 25);
	Name5->setColor(Color3B::BLUE);
	Name5->setPosition(Vec2(visibleSize.width/2 + origin.x - xx + x*5, visibleSize.height/2 + origin.y + y));
	this->addChild(Name5, 1);

	auto Name6 = Label::createWithTTF("Kilo     ", "Resources/fonts/Marker Felt.ttf", 25);
	Name6->setColor(Color3B::BLUE);
	Name6->setPosition(Vec2(visibleSize.width/2 + origin.x - xx + x*6, visibleSize.height/2 + origin.y + y));
	this->addChild(Name6, 1);



	auto menu = Menu::create();
	menu->setPosition(Vec2(visibleSize.width/2 + origin.x, visibleSize.height/2 + origin.y));
	this->addChild(menu, 1);
	
    auto r1 = MenuItemImage::create("Resources/image/010.png","Resources/image/01.png");
	r1->setTarget(this,menu_selector(SceneRole::menuGameCallback1));
	r1->setScale(1);
    menu->addChild(r1);

	auto r2 = MenuItemImage::create("Resources/image/020.png","Resources/image/02.png");
    r2->setTarget(this,menu_selector(SceneRole::menuGameCallback2));
	r2->setScale(1);
	menu->addChild(r2);
    
	auto r3 = MenuItemImage::create("Resources/image/030.png","Resources/image/03.png");
	r3->setTarget(this,menu_selector(SceneRole::menuGameCallback3));
	r3->setScale(1);
    menu->addChild(r3);

	auto r4 = MenuItemImage::create("Resources/image/040.png","Resources/image/04.png");
    r4->setTarget(this,menu_selector(SceneRole::menuGameCallback4));
	r4->setScale(1);
	menu->addChild(r4);

	auto r5 = MenuItemImage::create("Resources/image/050.png","Resources/image/05.png");
    r5->setTarget(this,menu_selector(SceneRole::menuGameCallback5));
	r5->setScale(1);
	menu->addChild(r5);

	auto r6 = MenuItemImage::create("Resources/image/060.png","Resources/image/06.png");
    r6->setTarget(this,menu_selector(SceneRole::menuGameCallback6));
	r6->setScale(1);
	menu->addChild(r6);

	menu->alignItemsHorizontally();


	//返回主菜单按钮
	auto closeItem = MenuItemImage::create(
                                           "Resources/image/back1.png",
                                           "Resources/image/back2.png",
                                           CC_CALLBACK_1(SceneRole::menuCallback, this));
	closeItem->setPosition(Vec2(origin.x + visibleSize.width - closeItem->getContentSize().width/2 ,
                                origin.y + closeItem->getContentSize().height/2));
	closeItem->setScale(0.5);
    auto menu2 = Menu::create(closeItem, NULL);
    menu2->setPosition(Vec2::ZERO);
    this->addChild(menu2, 1);



	//背景界面
	auto sprite = Sprite::create("Resources/image/select.jpg");
    sprite->setPosition(Vec2(visibleSize.width/2 + origin.x, visibleSize.height/2 + origin.y));
   
	sprite->setScaleX(visibleSize.width / sprite->getContentSize().width);  
	sprite->setScaleY(visibleSize.height / sprite->getContentSize().height);

    this->addChild(sprite, 0);
    return true;
}

void SceneRole::menuGameCallback1(Ref* pSender)
{
	SceneRole::r = 1;
	auto sc = SceneGame::createScene();
	Director::getInstance()->replaceScene(sc);
}

void SceneRole::menuGameCallback2(Ref* pSender)
{
	SceneRole::r = 2;
	auto sc = SceneGame::createScene();
	Director::getInstance()->replaceScene(sc);
}

void SceneRole::menuGameCallback3(Ref* pSender)
{
	SceneRole::r = 3;
	auto sc = SceneGame::createScene();
	Director::getInstance()->replaceScene(sc);
}

void SceneRole::menuGameCallback4(Ref* pSender)
{
	SceneRole::r = 4;
	auto sc = SceneGame::createScene();
	Director::getInstance()->replaceScene(sc);
}

void SceneRole::menuGameCallback5(Ref* pSender)
{
	SceneRole::r = 5;
	auto sc = SceneGame::createScene();
	Director::getInstance()->replaceScene(sc);
}

void SceneRole::menuGameCallback6(Ref* pSender)
{
	SceneRole::r = 6;
	auto sc = SceneGame::createScene();
	Director::getInstance()->replaceScene(sc);
}

void SceneRole::menuCallback(Ref* pSender)
{
	auto sc = SceneStart::createScene();
	Director::getInstance()->replaceScene(sc);
}
