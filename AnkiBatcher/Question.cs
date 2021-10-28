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

        public Question(string questionText, int questionType, string answer)
        {
            QuestionText = questionText;
            QuestionType = questionType;
            Answer = answer;
        }
        public Question(string questionText, int questionType, string questionA, string questionB, string questionC, string questionD, string answer)
        {
            QuestionText = questionText;
            QuestionType = questionType;
            QuestionA = questionA;
            QuestionB = questionB;
            QuestionC = questionC;
            QuestionD = questionD;
            Answer = answer;
        }

        public Question(string questionText, int questionType, string questionA, string questionB, string questionC, string questionD, string questionE, string answer)
        {
            QuestionText = questionText;
            QuestionType = questionType;
            QuestionA = questionA;
            QuestionB = questionB;
            QuestionC = questionC;
            QuestionD = questionD;
            QuestionE = questionE;
            Answer = answer;
        }
    }

}
