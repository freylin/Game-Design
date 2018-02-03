#include "SceneScore.h"
#include "SceneStart.h"

USING_NS_CC;

char* SceneScore::s[3][11]={{{"Computer"},{"Computer"},{"Computer"},{"Computer"},{"Computer"},{"Computer"},{"Computer"},{"NieYiyao"},{"LianLian"},{"LinJin"},{"Computer"}}
								,{{"Computer"},{"Computer"},{"Computer"},{"Computer"},{"Computer"},{"Computer"},{"Computer"},{"NieYiyao"},{"LianLian"},{"LinJin"},{"Computer"}}
								,{{"Computer"},{"Computer"},{"Computer"},{"Computer"},{"Computer"},{"Computer"},{"Computer"},{"NieYiyao"},{"LianLian"},{"LinJin"},{"Computer"}}};
int SceneScore::c[3][11]={{25,20,15,10,5,4,3,2,1,0,50}
							,{25,20,15,10,5,4,3,2,1,0,50}
							,{25,20,15,10,5,4,3,2,1,0,50}};


Scene* SceneScore::createScene()
{
    auto scene = Scene::create();
    auto layer = SceneScore::create();
    scene->addChild(layer);
    return scene;
}

bool SceneScore::init()
{
    if ( !Layer::init() )
    {
        return false;
    }

    Size visibleSize = Director::getInstance()->getVisibleSize();
    Vec2 origin = Director::getInstance()->getVisibleOrigin();

    auto label = Label::createWithTTF("Rank", "Resources/fonts/Marker Felt.ttf", 50);
    label->setPosition(Vec2(origin.x + visibleSize.width/2,
                            origin.y + visibleSize.height - label->getContentSize().height));
	label->setColor(Color3B::RED);
    this->addChild(label, 0);

	static Label* name;
	static Label* sco;

	int i, j, x = 300, y = 30, xx =-350, yy = -400, t;
	char * tt;

	//排名刷新
	for (i = 0; i <= 2; i++)
	if (SceneScore::c[i][10]<=SceneScore::c[i][0])
	{
		t = SceneScore::c[i][10]; SceneScore::c[i][10] = SceneScore::c[i][0] + 1; SceneScore::c[i][0] = t;
		tt = SceneScore::s[i][10]; SceneScore::s[i][10] = SceneScore::s[i][0]; SceneScore::s[i][0] = tt;
		for (j = 1; j <= 9; j++)
		if (SceneScore::c[i][j-1]<=SceneScore::c[i][j])
		{
			t = SceneScore::c[i][j-1]; SceneScore::c[i][j-1] = SceneScore::c[i][j]; SceneScore::c[i][j] = t;
			tt = SceneScore::s[i][j-1]; SceneScore::s[i][j-1] = SceneScore::s[i][j]; SceneScore::s[i][j] = tt;
		}
	}

	//显示排名
	for (i = 0; i <= 2; i++)
		for (j = 0; j <= 9; j++)
	{
		name =  Label::createWithTTF(SceneScore::s[i][j], "Resources/fonts/Marker Felt.ttf", 25);
		name->setPosition(Vec2(origin.x + visibleSize.width/2 + xx + x*i,
                            origin.y + visibleSize.height - label->getContentSize().height + yy + y*j));
		name->setColor(Color3B::BLUE);
		this->addChild(name, 1);

		sco =  Label::createWithTTF("0", "Resources/fonts/Marker Felt.ttf", 25);
		sco->setPosition(Vec2(origin.x + visibleSize.width/2 + xx + 100 + x*i,
                            origin.y + visibleSize.height - label->getContentSize().height + yy + y*j));
		sco->setColor(Color3B::BLUE);
		this->addChild(sco, 1);
		sco->setString(String::createWithFormat("%i",SceneScore::c[i][j])->getCString());

	}

	auto sprite = Sprite::create("Resources/image/rankBG.JPG");
    sprite->setPosition(Vec2(visibleSize.width/2 + origin.x, visibleSize.height/2 + origin.y));
	sprite->setScaleX(visibleSize.width / sprite->getContentSize().width);  
	sprite->setScaleY(visibleSize.height / sprite->getContentSize().height);
    this->addChild(sprite, 0);




	auto closeItem = MenuItemImage::create(
                                           "Resources/image/back1.png",
                                           "Resources/image/back2.png",
                                           CC_CALLBACK_1(SceneScore::menuCallback, this));
    closeItem->setScale(0.5);
	closeItem->setPosition(Vec2(origin.x + visibleSize.width - closeItem->getContentSize().width/2 ,
                                origin.y + closeItem->getContentSize().height/2));

    auto menu2 = Menu::create(closeItem, NULL);
    menu2->setPosition(Vec2::ZERO);
    this->addChild(menu2, 1);

	return true;
}


void SceneScore::menuCallback(Ref* pSender)
{
	auto sc = SceneStart::createScene();
	Director::getInstance()->replaceScene(sc);
}

