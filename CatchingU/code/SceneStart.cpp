#include "SceneStart.h"
#include "SceneRole.h"
#include "SceneAbout.h"
#include "SceneScore.h"
#include "SimpleAudioEngine.h"

USING_NS_CC;

int SceneStart::k = 30;
int SceneStart::kk = 0;
char* SceneStart::na = "C";

Scene* SceneStart::createScene()
{
	static int x = 0;
	if (x == 0) CocosDenshion::SimpleAudioEngine::getInstance()->playBackgroundMusic("Resources/bgm/K's Theme.mp3", true);
	x++;
    auto scene = Scene::create();
    auto layer = SceneStart::create();
    scene->addChild(layer);
    return scene;
}

bool SceneStart::init()
{
    if ( !Layer::init() )
    {
        return false;
    }
    
    Size visibleSize = Director::getInstance()->getVisibleSize();
    Vec2 origin = Director::getInstance()->getVisibleOrigin();

    /*auto closeItem = MenuItemImage::create(
                                           "CloseNormal.png",
                                           "CloseSelected.png",
                                           CC_CALLBACK_1(SceneStart::menuCloseCallback, this));
    
	closeItem->setPosition(Vec2(origin.x + visibleSize.width - closeItem->getContentSize().width/2 ,
                                origin.y + closeItem->getContentSize().height/2));

    auto menu2 = Menu::create(closeItem, NULL);
    menu2->setPosition(Vec2::ZERO);
    this->addChild(menu2, 1);*/

    auto label = Label::createWithTTF("CatchingU", "Resources/fonts/Marker Felt.ttf", 50);
    label->setPosition(Vec2(origin.x + visibleSize.width/2,
                            origin.y + visibleSize.height - label->getContentSize().height));
    this->addChild(label, 1);


	auto menu = Menu::create();
	menu->setPosition(Vec2(visibleSize.width/2 + origin.x, visibleSize.height/2 + origin.y));
	this->addChild(menu, 1);
	
    auto start = MenuItemImage::create("Resources/image/start1.png","Resources/image/start2.png");
	start->setTarget(this,menu_selector(SceneStart::menuRoleCallback));
	start->setScale(0.5);
    menu->addChild(start);

	auto about = MenuItemImage::create("Resources/image/introduction1.png","Resources/image/introduction2.png");
    about->setTarget(this,menu_selector(SceneStart::menuAboutCallback));
	about->setScale(0.5);
	menu->addChild(about);
    
	auto score = MenuItemImage::create("Resources/image/rank1.png","Resources/image/rank2.png");
	score->setTarget(this,menu_selector(SceneStart::menuScoreCallback));
	score->setScale(0.5);
    menu->addChild(score);

	auto close = MenuItemImage::create("Resources/image/close1.png","Resources/image/close2.png");
	close->setTarget(this,menu_selector(SceneStart::menuCloseCallback));
	close->setScale(0.5);
    menu->addChild(close);

	menu->alignItemsVertically();





	auto sprite = Sprite::create("Resources/image/2.JPG");
    sprite->setPosition(Vec2(visibleSize.width/2 + origin.x, visibleSize.height/2 + origin.y));
   
	sprite->setScaleX(visibleSize.width / sprite->getContentSize().width);  
	sprite->setScaleY(visibleSize.height / sprite->getContentSize().height);

    this->addChild(sprite, 0);
    return true;
}

void SceneStart::menuRoleCallback(Ref* pSender)
{
	auto sc = SceneRole::createScene();
	Director::getInstance()->replaceScene(sc);
}

void SceneStart::menuAboutCallback(Ref* pSender)
{
	auto sc = SceneAbout::createScene();
	Director::getInstance()->replaceScene(sc);
}

void SceneStart::menuScoreCallback(Ref* pSender)
{
	auto sc = SceneScore::createScene();
	Director::getInstance()->replaceScene(sc);
}

void SceneStart::menuCloseCallback(Ref* pSender)
{
#if (CC_TARGET_PLATFORM == CC_PLATFORM_WP8) || (CC_TARGET_PLATFORM == CC_PLATFORM_WINRT)
	MessageBox("You pressed the close button. Windows Store Apps do not implement a close button.","Alert");
    return;
#endif

    Director::getInstance()->end();

#if (CC_TARGET_PLATFORM == CC_PLATFORM_IOS)
    exit(0);
#endif
}
