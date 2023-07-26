/**
* ==========================================
* Copyright © AA. All rights reserved.
* Author：AA
* CreatTime：2023/07/24 17:27:29
* Version: v1.0
* Description：A sample to step page and Persistence Save.
* ==========================================
*/

using AA.Editor.EditorHelper.Base;
using AA.Editor.EditorHelper.Flow;
using UnityEditor;
using UnityEngine;

namespace AA.Editor.EditorHelper.Sample
{
    /// <summary>
    /// A sample to step page and Persistence Save
    /// </summary>
    public class StepPageSample : StepPage
    {
        /// <summary>
        /// Persistence Save for the int value, it will save to EditorPres automatically.
        /// </summary>
        [PersistenceSave] public int IntValueSampleProperty { get; private set; } = 10;
        
        /// <summary>
        /// Persistence Save for the string value, it will save to EditorPres automatically.
        /// </summary>
        [PersistenceSave] public string StringValueSampleProperty { get; private set; } = "StringValueSample";

        /// <summary>
        /// Draw some content with IMGUI.
        /// </summary>
        public override void OnDrawContent()
        {
            // 
            GUILayout.BeginHorizontal();
            GUILayout.Label("A sample to show the step page and persistence save:");
            var intValue = EditorGUILayout.IntField("IntValueSampleProperty", IntValueSampleProperty);
            if (intValue != IntValueSampleProperty)
            {
                IntValueSampleProperty = intValue;
                // Mark the data is dirty, will save in the base class automatically. You can save immediately but not performance-friendly。
                m_IsDirty = true;
            }
            var stringValue = EditorGUILayout.TextField("StringValueSampleProperty", StringValueSampleProperty);
            if (stringValue != StringValueSampleProperty)
            {
                StringValueSampleProperty = stringValue;
                // Mark the data is dirty, will save in the base class automatically.
                m_IsDirty = true;
            }
            // Should save here is performance friendly.
            base.OnDrawContent();
        }

    }

}
