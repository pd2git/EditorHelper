/**
* ==========================================
* Copyright © AA. All rights reserved.
* Author：AA
* CreatTime：2023/07/24 21:33:20
* Version: v1.0
* Description：A sample of the flow.
* ==========================================
*/

using System;
using System.Collections.Generic;
using AA.EditorHelper.Flow;
using AA.EditorHelper.Utility;
using UnityEditor;
using UnityEngine;

namespace AA.EditorHelper.Sample
{
    /// <summary>
    /// A sample of the flow.
    /// </summary>
    public class StepsFlowSample : FlowBaseWindow
    {
        #region Main

        /// <summary>
        /// A static instance of this.
        /// </summary>
        private static StepsFlowSample m_Ins;

        #endregion
        
        #region Flow

        /// <summary>
        /// Call on press 'Submit' button at the end of the flow.
        /// </summary>
        protected override void OnFlowSubmit()
        {
            if (EditorUtility.DisplayDialog("Info",
                "Are you sure to generate a new date picker with these configs above?",
                "Yes", "No"))
            {
                // Do some things after submitted.
            }
            else
            {
                Debug.LogWarning("Canceled the custom date picker generation.");
            }
        }

        /// <summary>
        /// Modify the steps of the flow.
        /// <br/>Eg. the 2nd step needs to modify the remaining steps in the flow. When you selected the option 1, it needs to delete the 3rd step, you can do it here.
        /// </summary>
        /// <param name="curStep">The type of the current step</param>
        /// <param name="affectedSteps">
        /// The type of steps which be affected.
        /// <br/>The 1st param: the type the step.
        /// <br/>The 2nd param: the new index to set(delete the step if set -1).</param>
        /// <returns>Whether modified the flow or not.</returns>
        private bool ModifyFlow(Type curStep, (Type, int)[] affectedSteps)
        {
            // Check
            if (m_StepInFlowList.Count <= 0 || affectedSteps.Length <= 0)
            {
                Debug.LogWarning(
                    $"Can not modify flow:The flow list({m_StepInFlowList.Count}) or the affected list({affectedSteps.Length}) is empty.");
                return false;
            }

            if (m_CurStepIndex >= m_StepInFlowList.Count - 1)
            {
                Debug.LogWarning(
                    $"Can not modify flow:The step index '{m_CurStepIndex}' is out of range of the flow list({m_StepInFlowList.Count}).");
                return false;
            }

            // Modify
            for (var indexAffect = 0; indexAffect < affectedSteps.Length; indexAffect++)
            {
                var stepType = affectedSteps[indexAffect].Item1;
                var stepIndex = affectedSteps[indexAffect].Item2;
                var isModified = false;
                // Found in main flow
                for (var index = 0; index < m_StepInFlowList.Count; index++)
                {
                    var stepInFlow = m_StepInFlowList[index];
                    if (stepInFlow != null && stepInFlow.GetType() == stepType)
                    {
                        // Delete
                        if (stepIndex < 0)
                        {
                            m_StepsOutOfFlowList.Add(stepInFlow);
                            m_StepInFlowList[index] = null;
                        }
                        // Modify the position
                        else
                        {
                            if (index != stepIndex)
                            {
                                // Keep the same count of the list.
                                if (!m_StepInFlowList.Remove(stepInFlow))
                                {
                                    Debug.LogWarning($"Can not move the step '{stepInFlow}'.");
                                    return false;
                                }

                                m_StepInFlowList.Insert(stepIndex, stepInFlow);
                            }
                        }

                        isModified = true;
                        break;
                    }
                }

                // Handle in the list out of the flow if not in the main flow.
                if (!isModified)
                {
                    for (var index = 0; index < m_StepsOutOfFlowList.Count; index++)
                    {
                        var stepInOutOfFlow = m_StepsOutOfFlowList[index];
                        if (stepInOutOfFlow.GetType() == stepType)
                        {
                            if (stepIndex < 0)
                            {
                                Debug.LogWarning("Can not delete the step in the list which store out of main flow.");
                                return false;
                            }

                            // 
                            m_StepInFlowList.Insert(stepIndex, stepInOutOfFlow);
                            m_StepsOutOfFlowList.Remove(stepInOutOfFlow);
                            break;
                        }
                    }
                }
            }

            // Delete the null steps in the flow.
            var indexDel = 0;
            do
            {
                if (m_StepInFlowList[indexDel] == null)
                {
                    m_StepInFlowList.RemoveAt(indexDel);
                }
                else
                {
                    indexDel++;
                }
            } while (indexDel < m_StepInFlowList.Count);

            return true;
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

            m_Ins = GetWindow<StepsFlowSample>(nameof(StepsFlowSample), true);
            m_Ins.minSize = new Vector2(300f, 500f);
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
            // Modify the default UI text if you want. 
            /* m_SubmitText = "提交";
            m_ProgressText = "进度";
            m_LastStepText = "上一步";
            m_NextStepText = "下一步";
            m_InvalidText = "当前设置无效";*/

            // Load Images
            if (m_HeadImg == null && !HEAD_IMG_FILE_GUID.Equals("ValidGUID"))
            {
                m_HeadImg = Utilities.GetObjByGUID<Texture2D>(HEAD_IMG_FILE_GUID);
            }
            if (m_BrandImg == null && !BRAND_IMG_FILE_GUID.Equals("ValidGUID"))
            {
                m_BrandImg = Utilities.GetObjByGUID<Texture2D>(BRAND_IMG_FILE_GUID);
            }

            // 1.Initialize the list of the steps which are out of the main flow.
            if (m_StepsOutOfFlowList == null)
            {
                m_StepsOutOfFlowList = new List<StepPage>();
            }

            // 2.Initialize the list of the steps which are in of the main flow.
            if (m_StepInFlowList == null)
            {
                // 
                var stepPageSample = new StepPageSample();
                stepPageSample.OnModifyFlow += ModifyFlow;
                // 
                m_StepInFlowList = new List<StepPage>
                {
                    stepPageSample
                };
            }
        }

        /// <summary>
        /// UnInitialize
        /// </summary>
        private void UnInit()
        {
            foreach (var stepPage in m_StepInFlowList)
            {
                stepPage.OnModifyFlow -= ModifyFlow;
            }
            m_StepInFlowList.Clear();
            
            foreach (var stepPage in m_StepsOutOfFlowList)
            {
                stepPage.OnModifyFlow -= ModifyFlow;
            }
            m_StepsOutOfFlowList.Clear();
        }

        #endregion

        #region Utilities

        /// <summary>
        /// Get the step instance by the step type.
        /// </summary>
        /// <typeparam name="T">The type of step.</typeparam>
        /// <returns>The instance of the step in the main flow.</returns>
        private T GetStepPage<T>() where T : StepPage
        {
            foreach (var stepPage in m_StepInFlowList)
            {
                var targetStepPage = stepPage as T;
                if (targetStepPage != null)
                {
                    return targetStepPage;
                }
            }

            return null;
        }

        #endregion

        #region U3D

        private void OnEnable()
        {
            Init();
        }
        
        private void OnDisable()
        {
            UnInit();
        }

        #endregion
    }
}