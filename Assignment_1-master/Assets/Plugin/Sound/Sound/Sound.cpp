#define EXPORT_API _declspec(dllexport)

#include <iostream>
#include <Windows.h>
#include <mmsystem.h>

#pragma comment(lib,"Winmm.lib")

//https://mixkit.co/free-sound-effects/sword/
//Here is where I got my sound effect

extern "C"
{
	void EXPORT_API swordSwing()
	{
		PlaySound(TEXT("dagger.wav"), NULL, SND_FILENAME);
	}
}

//int main()
//{
//	swordSwing();
//}