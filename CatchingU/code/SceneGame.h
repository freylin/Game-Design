#ifndef _SCENEGAME_H_
#define _SCENEGAME_H_

#include "cocos2d.h"

USING_NS_CC;

class SceneGame : public cocos2d::Layer
{
public:
	static cocos2d::Scene* createScene();

    virtual bool init();

    void menuCallback(cocos2d::Ref* pSender);

	void menuMusicOn(cocos2d::Ref* pSender);

	void menuMusicOff(cocos2d::Ref* pSender);


	void Block(int x, int y);

	void SceneGame::run(int (*dis)[10], bool (*b)[10]);

	virtual void onEnter();  
	virtual bool onTouchBegan(Touch* pTouch, Event* pEvent);

    CREATE_FUNC(SceneGame);
};

#endif
