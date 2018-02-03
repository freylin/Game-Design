#ifndef _SCENEABOUT_H_
#define _SCENEABOUT_H_

#include "cocos2d.h"

class SceneAbout : public cocos2d::Layer
{
public:
	static cocos2d::Scene* createScene();

    virtual bool init();

    void menuCallback(cocos2d::Ref* pSender);


    CREATE_FUNC(SceneAbout);
};

#endif
