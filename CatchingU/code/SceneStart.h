#ifndef _SCENESTART_H_
#define _SCENESTART_H_

#include "cocos2d.h"

class SceneStart : public cocos2d::Layer
{
public:
    static cocos2d::Scene* createScene();

    virtual bool init();
    
    // a selector callback
    void menuCloseCallback(cocos2d::Ref* pSender);

	void menuRoleCallback(Ref* pSender);

	void menuAboutCallback(Ref* pSender);

	void menuScoreCallback(Ref* pSender);
    
	static int k;//�ʿ����
	static int kk;//�Ѷ�
	static char* na;//�������
    // implement the "static create()" method manually
    CREATE_FUNC(SceneStart);
};

#endif
