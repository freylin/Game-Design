#ifndef _SCENEFAILED_H_
#define _SCENEFAILED_H_

#include "cocos2d.h"

class SceneFailed : public cocos2d::Layer
{
public:
    // there's no 'id' in cpp, so we recommend returning the class instance pointer
    static cocos2d::Scene* createScene();

    virtual bool init();
    
    // a selector callback

	void menuGameCallback(Ref* pSender);

	void menuStartCallback(Ref* pSender);

	void menuScoreCallback(Ref* pSender);
    
    CREATE_FUNC(SceneFailed);
};

#endif
