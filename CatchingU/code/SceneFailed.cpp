#include "SceneFailed.h"
#include "SceneStart.h"
#include "SceneGame.h"
#include "SceneScore.h"

USING_NS_CC;

Scene* SceneFailed::createScene()
{
    auto scene = Scene::create();
    auto layer = SceneFailed::create();
    scene->addChild(layer);
    return scene;
}

bool SceneFailed::init()
{
    if ( !Layer::init() )
    {
        return false;
    }
    
    Size visibleSize = Director::getInstance()->getVisibleSize();
    Vec2 origin = Director::getInstance()->getVisibleOrigin();

	auto menu = Menu::create();
	menu->setPosition(Vec2(visibleSize.width/2 + origin.x, visibleSize.height/2 + origin.y));
	this->addChild(menu, 1);
	
    auto start = MenuItemImage::create("Resources/image/restart1.png","Resources/image/restart2.png");
	start->setTarget(this,menu_selector(SceneFailed::menuGameCallback));
	start->setScale(0.5);
    menu->addChild(start);
	    
	auto score = MenuItemImage::create("Resources/image/rank1.png","Resources/image/rank2.png");
	score->setTarget(this,menu_selector(SceneFailed::menuScoreCallback));
	score->setScale(0.5);
    menu->addChild(score);

	auto back = MenuItemImage::create("Resources/image/back1.png","Resources/image/back2.png");
	back->setTarget(this,menu_selector(SceneFailed::menuStartCallback));
	back->setScale(0.5);
	menu->addChild(back);

	menu->alignItemsVertically();

	
	auto sprite = Sprite::create("Resources/image/failedBG.JPG");
    sprite->setPosition(Vec2(visibleSize.width/2 + origin.x, visibleSize.height/2 + origin.y));
   
	sprite->setScaleX(visibleSize.width / sprite->getContentSize().width);  
	sprite->setScaleY(visibleSize.height / sprite->getContentSize().height);

    this->addChild(sprite, 0);

    return true;
}

void SceneFailed::menuGameCallback(Ref* pSender)
{
	auto sc = SceneGame::createScene();
	Director::getInstance()->replaceScene(sc);
}

void SceneFailed::menuStartCallback(Ref* pSender)
{
	auto sc = SceneStart::createScene();
	Director::getInstance()->replaceScene(sc);
}

void SceneFailed::menuScoreCallback(Ref* pSender)
{
	auto sc = SceneScore::createScene();
	Director::getInstance()->replaceScene(sc);
}