#ifndef _SCENESCORE_H_
#define _SCENESCORE_H_

#include "cocos2d.h"

class SceneScore : public cocos2d::Layer
{
public:
	static cocos2d::Scene* createScene();

    virtual bool init();

    void menuCallback(cocos2d::Ref* pSender);

	static char* s[3][11];
	static int c[3][11];

    CREATE_FUNC(SceneScore);
};

#endif
