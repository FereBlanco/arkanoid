using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;

namespace Scripts.Game
{

    public class CutsceneText : MonoBehaviour
    {
        [SerializeField] TMP_Text m_Text;
        [SerializeField, Min(0.01f)] float m_TextDelay = 0.05f;
        private WaitForSeconds m_WaitTime;
        [SerializeField, Multiline(4)] string[] m_Paragraphs;

        private void Awake()
        {
            Assert.IsNotNull(m_Text, "ERROR: m_Text not assigned in CutsceneText class");
            m_Text.text = string.Empty;
            m_WaitTime = new WaitForSeconds(m_TextDelay);
            StartCoroutine(ReadText());
        }

        IEnumerator ReadText()
        {
            foreach (var paragraph in m_Paragraphs)
            {
                for (int i = 0; i < paragraph.Length; i++)
                {
                    m_Text.text += paragraph[i];
                    yield return m_WaitTime;
                }
                m_Text.text = string.Empty;
            }
        }
    }
}
