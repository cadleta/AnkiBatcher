namespace AnkiBatcher
{
    public class Question
    {
        public string QuestionText { get; set; }
        public int QuestionType { get; set; } //remove Question Type?
        public string QuestionA { get; set; }
        public string QuestionB { get; set; }
        public string QuestionC { get; set; }
        public string QuestionD { get; set; }
        public string QuestionE { get; set; }
        public string Answer { get; set; }

        //Question Type:
        // 0 = TF
        // 1 = Single Choice
        // 2 = Mulitple Choice

    }

    public class MultipleChoiceQuestion : Question
    {
        public MultipleChoiceQuestion(string questionText, string questionA, string questionB, string questionC, string questionD, string questionE, string answer)
        {
            QuestionText = questionText;
            QuestionA = questionA;
            QuestionB = questionB;
            QuestionC = questionC;
            QuestionD = questionD;
            QuestionE = questionE;
            Answer = answer;
        }

    }

    public class TrueFalseQuestion : Question
    {
        public TrueFalseQuestion(string questionText, bool answer)
        {
            QuestionText = questionText;
            QuestionA = "True";
            QuestionB = "False";

            if (answer)
            {
                Answer = "1 0";
            } else
            {
                Answer = "0 1";
            }
        }

    }

    public class SimpleQuestion : Question
    {
        public SimpleQuestion(string questionText, string answer)
        {
            QuestionText = questionText;
            Answer = answer;
        }

    }


}
