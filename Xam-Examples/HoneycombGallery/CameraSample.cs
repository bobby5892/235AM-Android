/*
 * Copyright (C) 2011 The Android Open Source Project
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
using Android.App;
using Android.OS;
using Android.Content.PM;

namespace com.example.monodroid.hcgallery
{
	[Activity (Label = "@string/camera_sample", ScreenOrientation = ScreenOrientation.Landscape)]
	public class CameraSample : Activity 
	{
		
		protected override void OnCreate (Bundle savedInstanceState) 
		{
			int themeId = this.Intent.Extras.GetInt ("theme");
			this.SetTheme (themeId);
			base.OnCreate (savedInstanceState);
			SetContentView (Resource.Layout.camera_sample);
		}
		
	}
}
