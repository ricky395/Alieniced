//
//  CloudOnceCloudSave.h
//  CloudOnceCloudSave
//
//  Created by Trollpants Game Studio on 21/04/15.
//  Copyright (c) 2016 Trollpants Game Studio AS. All rights reserved.
//  Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

#import <Foundation/Foundation.h>

@interface CloudOnceCloudSave : NSObject

- (BOOL) setDevString: (NSString*) key : (NSString*) value;
- (const char*) getDevString: (NSString*) key;
- (BOOL) deleteDevString: (NSString*) key;
- (void) updateFromiCloud:(NSNotification*) notification;
- (char *) makeStringCopy: (const char *) string;
- (NSString *) createNSString: (const char *) string;

extern void UnitySendMessage(const char *, const char *, const char *);

@end
