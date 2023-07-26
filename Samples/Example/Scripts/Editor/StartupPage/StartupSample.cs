/**
* ==========================================
* Copyright © AA. All rights reserved.
* Author：AA
* CreatTime：2023/07/22 22:40:20
* Version: v1.0
* Description：A sample to show a window after Unity launched.
* ==========================================
*/

// Uncomment the code below to enable the startup sample.
//#define ENABLE_STARTUP

using AA.Editor.EditorHelper.Startup;
using AA.Editor.EditorHelper.Utility;
using UnityEditor;
using UnityEngine;

namespace AA.Editor.EditorHelper.Sample
{
    /// <summary>
    /// A sample to show a window after Unity launched.
    /// </summary>
    [InitializeOnLoad]
    public class StartupSample : StartupBaseWindow
    {
        /// <summary>
        /// Draw the content.
        /// </summary>
        protected override void DrawContent()
        {
            base.DrawContent();
            //
            GUILayout.Label("It is a welcome page which be show after the Unity launched.");
        }

        #region Main

        /// <summary>
        /// A static instance of this.
        /// </summary>
        private static StartupSample m_Ins;

        #endregion

        #region Startup

        /// <summary>
        /// Perform editor checks as soon as the scripts are done compiling.
        /// </summary>
        static StartupSample()
        {
#if ENABLE_STARTUP
            EditorApplication.update += OnEditorStartup;
#endif
        }

        /// <summary>
        /// Call after the editor launched.
        /// </summary>
        private static void OnEditorStartup()
        {
            if (EditorApplication.isCompiling)
            {
                return;
            }
            EditorApplication.update -= OnEditorStartup;
            // 
            var key = $"{typeof(StartupSample).FullName}.IsDisplayedAtStartup";
            if (EditorPrefs.HasKey(key) || EditorPrefs.GetBool(key))
            {
                return;
            }
            ShowWindow();
            //
            EditorPrefs.SetBool(key, true);
        }

        #endregion

        #region Entry

        /// <summary>
        /// Show the window.
        /// </summary>
        public static void ShowWindow()
        {
            if (m_Ins != null)
            {
                m_Ins.Close();
                return;
            }

            m_Ins = GetWindow<StartupSample>($"{nameof(StartupSample)}-Welcome", true);
            m_Ins.minSize = new Vector2(350f, 400f);
        }

        #endregion

        #region Init

        /// <summary>
        /// The GUID of the head image.
        /// </summary>
        private const string HEAD_IMG_FILE_GUID = "ValidGUID";
        
        /// <summary>
        /// The GUID of the brand image.
        /// </summary>
        private const string BRAND_IMG_FILE_GUID = "ValidGUID";
        
        /// <summary>
        /// Initialize
        /// </summary>
        private void Init()
        {
            if (m_HeadImg == null && !HEAD_IMG_FILE_GUID.Equals("ValidGUID"))
            {
                m_HeadImg = Utilities.GetObjByGUID<Texture2D>(HEAD_IMG_FILE_GUID);
            }
            if (m_BrandImg == null && !BRAND_IMG_FILE_GUID.Equals("ValidGUID"))
            {
                m_BrandImg = Utilities.GetObjByGUID<Texture2D>(BRAND_IMG_FILE_GUID);
            }
        }

        #endregion

        #region U3D

        private void OnEnable()
        {
            Init();
        }

        #endregion
    }
}