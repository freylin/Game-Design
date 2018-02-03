#ifndef _SCENEROLE_H_
#define _SCENEROLE_H_

#include "cocos2d.h"

class SceneRole : public cocos2d::Layer
{
public:
    // there's no 'id' in cpp, so we recommend returning the class instance pointer
    static cocos2d::Scene* createScene();

    virtual bool init();
    
    // a selector callback
    void menuCallback(cocos2d::Ref* pSender);

	void menuGameCallback1(Ref* pSender);
	void menuGameCallback2(Ref* pSender);
	void menuGameCallback3(Ref* pSender);
	void menuGameCallback4(Ref* pSender);
	void menuGameCallback5(Ref* pSender);
	void menuGameCallback6(Ref* pSender);

	static int r;

    CREATE_FUNC(SceneRole);
};

#endif
