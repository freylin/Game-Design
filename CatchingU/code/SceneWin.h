#ifndef _SCENEWIN_H_
#define _SCENEWIN_H_

#include "cocos2d.h"

class SceneWin : public cocos2d::Layer
{
public:
    static cocos2d::Scene* createScene();

    virtual bool init();
    
    // a selector callback

	void menuGameCallback(Ref* pSender);

	void menuStartCallback(Ref* pSender);

	void menuScoreCallback(Ref* pSender);
    
    CREATE_FUNC(SceneWin);
};

#endif
