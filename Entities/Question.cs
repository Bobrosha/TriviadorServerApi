using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TriviadorServerApi.Entities
{
    public class Question
    {
        public string TextQuestion { get; }
        public List<string> ListAnswers { get; }

        public Question(string textQuestion, List<string> listAnswers)
        {
            TextQuestion = textQuestion;
            ListAnswers = listAnswers;
        }
    }
}
