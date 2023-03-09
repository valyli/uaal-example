// [!] important set UnityFramework in Target Membership for this file
// [!]           and set Public header visibility

#import <Foundation/Foundation.h>

// NativeCallsProtocol defines protocol with methods you want to be called from managed
@protocol NativeCallsProtocol
@required
// Debug command  
- (void) sendDebugCmdToApp:(NSString*)reason cmd:(NSString*)cmd parameters:(NSString*)parameters;

// Show App Window
- (void) showHostMainWindow:(NSString*)color;
// Change unity window size
- (void) changeUnityWindowSize:(NSString*)reason x:(int)x y:(int)y w:(int)w h:(int)h;
// Set focus to which View 
- (void) setViewFocus:(NSString*)reason view:(NSString*)view focus:(bool)focus;
@end

__attribute__ ((visibility("default")))
@interface FrameworkLibAPI : NSObject
// call it any time after UnityFrameworkLoad to set object implementing NativeCallsProtocol methods
+(void) registerAPIforNativeCalls:(id<NativeCallsProtocol>) aApi;

@end


