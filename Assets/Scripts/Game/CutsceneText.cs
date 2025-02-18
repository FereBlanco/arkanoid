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
        private int m_ParagraphIndex;

        private void Awake()
        {
            Assert.IsNotNull(m_Text, "ERROR: m_Text not assigned in CutsceneText class");
            m_ParagraphIndex = 0;
            ClearText();
            m_WaitTime = new WaitForSeconds(m_TextDelay);
        }

        public void ReadNextParagraph()
        {
            if (m_ParagraphIndex < m_Paragraphs.Length)
            {
                StartCoroutine(ReadParagraph());
                m_ParagraphIndex++;
            }
        }

        IEnumerator ReadParagraph()
        {
            var paragraph = m_Paragraphs[m_ParagraphIndex];
            for (int i = 0; i < paragraph.Length; i++)
            {
                m_Text.text += paragraph[i];
                yield return m_WaitTime;
            }
        }

        public void ClearText()
        {
            m_Text.text = string.Empty;
        }
    }
}
