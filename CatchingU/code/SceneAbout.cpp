#include "SceneAbout.h"
#include "SceneStart.h"

USING_NS_CC;

Scene* SceneAbout::createScene()
{
    auto scene = Scene::create();
    auto layer = SceneAbout::create();
    scene->addChild(layer);
    return scene;
}

bool SceneAbout::init()
{
    if ( !Layer::init() )
    {
        return false;
    }

    Size visibleSize = Director::getInstance()->getVisibleSize();
    Vec2 origin = Director::getInstance()->getVisibleOrigin();

    /*auto label = Label::createWithTTF("Rule......BLABLA......", "Resources/fonts/Marker Felt.ttf", 50);
    label->setPosition(Vec2(origin.x + visibleSize.width/2,
                            origin.y + visibleSize.height - label->getContentSize().height));
    this->addChild(label, 1);*/

	auto sprite = Sprite::create("Resources/image/IntroBG.JPG");
    sprite->setPosition(Vec2(visibleSize.width/2 + origin.x, visibleSize.height/2 + origin.y));
	sprite->setScaleX(visibleSize.width / sprite->getContentSize().width);  
	sprite->setScaleY(visibleSize.height / sprite->getContentSize().height);
    this->addChild(sprite, 0);




	auto closeItem = MenuItemImage::create(
                                           "Resources/image/back1.png",
                                           "Resources/image/back2.png",
                                           CC_CALLBACK_1(SceneAbout::menuCallback, this));
    
	closeItem->setPosition(Vec2(origin.x + visibleSize.width - closeItem->getContentSize().width/2 ,
                                origin.y + closeItem->getContentSize().height/2));
	closeItem->setScale(0.5);

    auto menu2 = Menu::create(closeItem, NULL);
    menu2->setPosition(Vec2::ZERO);
    this->addChild(menu2, 1);

	return true;
}


void SceneAbout::menuCallback(Ref* pSender)
{
	auto sc = SceneStart::createScene();
	Director::getInstance()->replaceScene(sc);
}

