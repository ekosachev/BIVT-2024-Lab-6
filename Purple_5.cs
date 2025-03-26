namespace Lab_6
{
    public class Purple_5
    {
        public struct Response
        {
            private string _animal;
            private string _characterTrait;
            private string _concept;

            public string Animal => _animal;
            public string CharacterTrait => _characterTrait;
            public string Concept => _concept;

            public Response(string animal, string characterTrait, string concept)
            {
                this._animal = animal;
                this._characterTrait = characterTrait;
                this._concept = concept;
            }

            internal string GetAnswer(int question)
            {
                if (question > 3 || question < 1)
                    return "";
                switch (question)
                {
                    case 1:
                        return this.Animal;
                    case 2:
                        return this.CharacterTrait;
                    case 3:
                        return this.Concept;
                    default:
                        return "";
                }
            }

            public int CountVotes(Response[] responses, int questionNumber)
            {
                if (responses == null)
                    return 0;
                if (questionNumber < 1 || questionNumber > 3)
                    return 0;

                string thisAnswer = this.GetAnswer(questionNumber);

                return responses
                    .Where(r => r.GetAnswer(questionNumber) != null)
                    .Where(r => r.GetAnswer(questionNumber) == thisAnswer)
                    .ToArray()
                    .Length;
            }

            public void Print()
            {
                System.Console.WriteLine(
                    $"Ответ: {this.Animal}, {this.CharacterTrait}, {this.Concept}"
                );
            }
        }

        public struct Research
        {
            private string _name;
            private Response[] _responses;

            public string Name => _name;
            public Response[]? Responses
            {
                get
                {
                    if (this._responses == null)
                        return null;
                    Response[] copy = new Response[_responses.Length];
                    _responses.CopyTo(copy, 0);
                    return copy;
                }
            }

            public Research(string name)
            {
                this._name = name;
                this._responses = new Response[] { };
            }

            public void Add(string[] answers)
            {
                if (answers == null)
                    return;
                if (answers.Length != 3)
                    return;

                if (this.Responses == null)
                    return;
                Response newResponse = new Response(answers[0], answers[1], answers[2]);

                Response[] newArray = new Response[this.Responses.Length + 1];
                this.Responses.CopyTo(newArray, 0);
                newArray[this.Responses.Length] = newResponse;

                this._responses = newArray;
            }

            public string[] GetTopResponses(int question)
            {
                if (this.Responses == null)
                    return default;
                if (question < 1 || question > 3)
                    return new string[0];
                if (this.Responses.Length == 0)
                    return new string[0];

                var sorted = this
                    .Responses.GroupBy(
                        r => r.GetAnswer(question),
                        r => r.GetAnswer(question),
                        (answer, answers) => new { Key = answer, Count = answers.Count() }
                    )
                    .Where(r => r.Key != "")
                    .OrderByDescending(r => r.Count)
                    .ToArray();

                return sorted.Take(int.Min(5, sorted.Length)).Select(r => r.Key).ToArray();
            }

            public void Print()
            {
                System.Console.WriteLine($"Исследование {this.Name}");
                if (this.Responses == null)
                    return;
                if (this.Responses.Length == 0)
                    return;
                foreach (Response r in this.Responses)
                {
                    Console.Write("\t");
                    r.Print();
                }
            }
        }
    }
}
