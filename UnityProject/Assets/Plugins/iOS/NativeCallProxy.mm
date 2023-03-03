#import <Foundation/Foundation.h>
#import "NativeCallProxy.h"


@implementation FrameworkLibAPI

id<NativeCallsProtocol> api = NULL;
+(void) registerAPIforNativeCalls:(id<NativeCallsProtocol>) aApi
{
    api = aApi;
}

@end


extern "C" {
    void showHostMainWindow(const char* color) { return [api showHostMainWindow:[NSString stringWithUTF8String:color]]; }
}

extern "C" {
    void changeUnityWindowSize(const char* reason, int x, int y, int w, int h) {  [api changeUnityWindowSize:[NSString stringWithUTF8String:reason] x:x y:y w:w h:h]; }
}

