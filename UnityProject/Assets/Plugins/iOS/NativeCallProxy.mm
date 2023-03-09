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
    void sendDebugCmdToApp(const char* reason, const char* cmd, const char* parameters) { return [api sendDebugCmdToApp:[NSString stringWithUTF8String:reason] cmd:[NSString stringWithUTF8String:cmd] parameters:[NSString stringWithUTF8String:parameters]]; }
    
    void showHostMainWindow(const char* color) { return [api showHostMainWindow:[NSString stringWithUTF8String:color]]; }
    void changeUnityWindowSize(const char* reason, int x, int y, int w, int h) {  [api changeUnityWindowSize:[NSString stringWithUTF8String:reason] x:x y:y w:w h:h]; }
    void setViewFocus(const char* reason, const char* view, bool focus) {  [api setViewFocus:[NSString stringWithUTF8String:reason] view:[NSString stringWithUTF8String:view] focus:focus]; }
}

