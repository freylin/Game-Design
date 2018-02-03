#include "SceneWin.h"
#include "SceneStart.h"
#include "SceneGame.h"
#include "SceneScore.h"
#include <ui/UITextField.h>

USING_NS_CC;

Scene* SceneWin::createScene()
{
    auto scene = Scene::create();
    auto layer = SceneWin::create();
    scene->addChild(layer);
    return scene;
}

bool SceneWin::init()
{
    if ( !Layer::init() )
    {
        return false;
    }


    Size visibleSize = Director::getInstance()->getVisibleSize();
    Vec2 origin = Director::getInstance()->getVisibleOrigin();

	auto label = Label::createWithTTF("U've caught me with       steps!", "Resources/fonts/Marker Felt.ttf", 50);
	label->setPosition(Vec2(origin.x + visibleSize.width/2, origin.y + visibleSize.height - label->getContentSize().height - 100));
	label->setColor(ccc3(0, 0, 0));
	this->addChild(label, 1);

	auto num = Label::createWithTTF("0", "Resources/fonts/Marker Felt.ttf", 50);
	num->setPosition(Vec2(origin.x + visibleSize.width/2 + 140, origin.y + visibleSize.height - label->getContentSize().height - 100));
	num->setColor(ccc3(0, 0, 0));
	num->setString(String::createWithFormat("%i",SceneScore::c[SceneStart::kk][10])->getCString());
	this->addChild(num, 1);

	if (SceneStart::k > 10) 
	{
		SceneStart::k -= 10;
		SceneStart::kk += 1;
	}
    
	/*//ÊäÈëÍæ¼ÒÐÕÃû
	auto textField = ui::TextField::create("Please input your name","Resources/fonts/simhei.tff",25);
	textField->setPosition(Vec2(origin.x + visibleSize.width/2, origin.y + visibleSize.height - label->getContentSize().height));
	textField->addEventListener(CC_CALLBACK_2(SceneWin::menuScoreCallback,this));
	this->addChild(textField,1);*/

	auto menu = Menu::create();
	menu->setPosition(Vec2(visibleSize.width/2 + origin.x, visibleSize.height/2 + origin.y));
	this->addChild(menu, 1);
	
    auto start = MenuItemImage::create("Resources/image/next1.png","Resources/image/next2.png");
	start->setTarget(this,menu_selector(SceneWin::menuGameCallback));
	start->setScale(0.5);
    menu->addChild(start);

	auto score = MenuItemImage::create("Resources/image/rank1.png","Resources/image/rank2.png");
	score->setTarget(this,menu_selector(SceneWin::menuScoreCallback));
	score->setScale(0.5);
    menu->addChild(score);
	
	auto back = MenuItemImage::create("Resources/image/back1.png","Resources/image/back2.png");
    back->setTarget(this,menu_selector(SceneWin::menuStartCallback));
	back->setScale(0.5);
	menu->addChild(back);

	menu->alignItemsHorizontally();

	
	auto sprite = Sprite::create("Resources/image/winBG.JPG");
    sprite->setPosition(Vec2(visibleSize.width/2 + origin.x, visibleSize.height/2 + origin.y));
   
	sprite->setScaleX(visibleSize.width / sprite->getContentSize().width);  
	sprite->setScaleY(visibleSize.height / sprite->getContentSize().height);

    this->addChild(sprite, 0);

    return true;
}

void SceneWin::menuGameCallback(Ref* pSender)
{
	auto sc = SceneGame::createScene();
	Director::getInstance()->replaceScene(sc);
}

void SceneWin::menuStartCallback(Ref* pSender)
{
	auto sc = SceneStart::createScene();
	Director::getInstance()->replaceScene(sc);
}

void SceneWin::menuScoreCallback(Ref* pSender)
{
	auto sc = SceneScore::createScene();
	Director::getInstance()->replaceScene(sc);
}