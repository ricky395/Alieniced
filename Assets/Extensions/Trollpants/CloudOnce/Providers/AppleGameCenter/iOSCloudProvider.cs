// <copyright file="iOSCloudProvider.cs" company="Trollpants Game Studio AS">
// Copyright (c) 2016 Trollpants Game Studio AS. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

#if UNITY_IOS
namespace Trollpants.CloudOnce.Internal.Providers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.SocialPlatforms;
    using Utils;

    /// <summary>
    /// Apple Game Center implementation of ICloudProvider
    /// </summary>
    public sealed class iOSCloudProvider : CloudProviderBase<iOSCloudProvider>
    {
        #region Constructor & properties

        private CloudOnceEvents cloudOnceEvents;
        private bool isInitializing;
        private bool isSigningIn;
        private bool cloudSaveEnabled = true;

        /// <summary>
        /// Prevents a default instance of the <see cref="iOSCloudProvider" /> class from being created.
        /// </summary>
        private iOSCloudProvider()
            : base("Apple Game Center")
        {
        }

        /// <summary>
        /// ID for currently signed in player. Will return an empty <see cref="string"/> if no player is signed in.
        /// </summary>
        public override string PlayerID
        {
            get { return IsSignedIn ? Social.localUser.id : string.Empty; }
        }

        /// <summary>
        /// Display name for currently signed in player. Will return an empty <see cref="string"/> if no player is signed in.
        /// </summary>
        public override string PlayerDisplayName
        {
            get { return IsSignedIn ? Social.localUser.userName : string.Empty; }
        }

        /// <summary>
        /// Profile picture for currently signed in player. Will return <c>null</c> if the user has no avatar, or it has not loaded yet.
        /// </summary>
        public override Texture2D PlayerImage
        {
            get { return IsSignedIn ? Social.localUser.image ?? Texture2D.whiteTexture : Texture2D.whiteTexture; }
        }

        /// <summary>
        /// Whether or not the user is currently signed in.
        /// </summary>
        public override bool IsSignedIn
        {
            get { return Social.localUser.authenticated; }
        }

        /// <summary>
        /// Whether or not Cloud Save is enabled.
        /// Disabling Cloud Save will make <c>Cloud.Storage.Save</c> only save to disk.
        /// </summary>
        public override bool CloudSaveEnabled
        {
            get { return cloudSaveEnabled; }
            set { cloudSaveEnabled = value; }
        }

        /// <summary>
        /// Interface for accessing cloud save on Apple Game Center.
        /// </summary>
        public override ICloudStorageProvider Storage { get; protected set; }

        #endregion /Constructor & properties

        #region Public methods

        /// <summary>
        /// Initializes Apple Game Center.
        /// </summary>
        /// <param name="activateCloudSave">Whether or not Cloud Saving should be activated.</param>
        /// <param name="autoSignIn">
        /// Whether or not <see cref="SignIn"/> will be called automatically once the cloud provider is initialized.
        /// Ignored on Amazon GameCircle as there is no way of avoiding auto sign in.
        /// </param>
        /// <param name="autoCloudLoad">
        /// Whether or not cloud data should be loaded automatically if the user is successfully signed in.
        /// Ignored if Cloud Saving is deactivated.
        /// </param>
        public override void Initialize(bool activateCloudSave = true, bool autoSignIn = true, bool autoCloudLoad = true)
        {
            // Ignore repeated calls on Initialize
            if (isInitializing)
            {
                return;
            }
#if CO_DEBUG
            Debug.Log("Initializing Apple Game Center.");
#endif
            isInitializing = true;
            cloudSaveEnabled = activateCloudSave;
#if CO_DEBUG
            Debug.Log(cloudSaveEnabled ? "iCloud support enabled." : "iCloud support disabled.");
#endif
            if (autoSignIn)
            {
                var onSignedIn = new UnityAction<bool>(arg0 =>
                {
                    cloudOnceEvents.RaiseOnInitializeComplete();
                    isInitializing = false;
                });
                SignIn(autoCloudLoad, onSignedIn);
                return;
            }

            if (cloudSaveEnabled && autoCloudLoad)
            {
                var iCloudWrapper = (iOSCloudSaveWrapper)Storage;
                iCloudWrapper.Load();
            }

            cloudOnceEvents.RaiseOnInitializeComplete();
            isInitializing = false;
        }

        /// <summary>
        /// Signs in to Apple Game Center.
        /// </summary>
        /// <param name="autoCloudLoad">
        /// Whether or not cloud data should be loaded automatically when the user is successfully signed in.
        /// Ignored if Cloud Saving is deactivated or the user fails to sign in.
        /// </param>
        /// <param name='callback'>
        /// The callback to call when authentication finishes. It will be called
        /// with <c>true</c> if authentication was successful, <c>false</c> otherwise.
        /// </param>
        public override void SignIn(bool autoCloudLoad = true, UnityAction<bool> callback = null)
        {
            if (isSigningIn)
            {
                return;
            }

            isSigningIn = true;
            if (IsSignedIn)
            {
#if CO_DEBUG
                Debug.Log("Already signed in to Apple Game Center." +
                          " If you want to change the user, that must be done from the iOS Settings menu.");
#endif
                CloudOnceUtils.SafeInvoke(callback, true);
                if (CloudSaveEnabled && autoCloudLoad)
                {
                    var iCloudWrapper = (iOSCloudSaveWrapper)Storage;
                    iCloudWrapper.Load();
                }

                isSigningIn = false;
                return;
            }

            Social.localUser.Authenticate(
                success =>
                {
                    if (success)
                    {
#if CO_DEBUG
                        Debug.Log("Successfully signed in to Apple Game Center.");
#endif
                        cloudOnceEvents.RaiseOnSignedInChanged(true);
                        cloudOnceEvents.RaiseOnPlayerImageDownloaded(Social.localUser.image);
                        UpdateAchievementsData();
                        if (CloudSaveEnabled && autoCloudLoad)
                        {
                            var iCloudWrapper = (iOSCloudSaveWrapper)Storage;
                            iCloudWrapper.Load();
                        }
                    }
                    else
                    {
#if CO_DEBUG
                        Debug.LogWarning("Failed to sign in to Apple Game Center.");
#endif
                        cloudOnceEvents.RaiseOnSignInFailed();
                    }

                    CloudOnceUtils.SafeInvoke(callback, success);
                    isSigningIn = false;
                });
        }

        /// <summary>
        /// Has no function on iOS. Apple wants users to only sign out of Game Center via the iOS settings menu.
        /// </summary>
        public override void SignOut()
        {
#if CO_DEBUG
            Debug.LogWarning("The SignOut method has no function on iOS. Apple wants users" +
                      " to only sign out of Game Center via the iOS Settings menu.");
#endif
        }

        /// <summary>
        /// Load the user profiles accociated with the given array of user IDs.
        /// </summary>
        /// <param name="userIDs">The users to retrieve profiles for.</param>
        /// <param name="callback">Callback to handle the user profiles.</param>
        public override void LoadUsers(string[] userIDs, Action<IUserProfile[]> callback)
        {
            Social.LoadUsers(userIDs, callback);
        }

        /// <summary>
        /// Method used by <see cref="Cloud"/> to initialize the Cloud provider.
        /// </summary>
        public void InternalInit(CloudOnceEvents events)
        {
            cloudOnceEvents = events;
            Storage = new iOSCloudSaveWrapper(events);
        }

        #endregion /Public methods

        private static void UpdateAchievementsData()
        {
            var type = typeof(Achievements);
            var allAchievements = new Dictionary<string, UnifiedAchievement>();
            foreach (var propertyInfo in type.GetProperties())
            {
                if (propertyInfo.PropertyType == typeof(UnifiedAchievement))
                {
                    allAchievements[propertyInfo.Name] = (UnifiedAchievement)propertyInfo.GetValue(null, null);
                }
            }

            Social.LoadAchievements(achievements =>
            {
                if (achievements.Length > 0)
                {
                    foreach (var achievement in achievements)
                    {
                        try
                        {
                            var kvp = allAchievements.Single(pair => pair.Value.ID == achievement.id);
                            allAchievements[kvp.Key].UpdateData(achievement.completed, achievement.percentCompleted, achievement.hidden);
                        }
                        catch
                        {
#if CO_DEBUG
                            Debug.Log(string.Format(
                                "An achievement ({0}) that doesn't exist in the Achievements class was loaded from native API.", achievement.id));
#endif
                        }
                    }
                }
            });
        }
    }
}
#endif
